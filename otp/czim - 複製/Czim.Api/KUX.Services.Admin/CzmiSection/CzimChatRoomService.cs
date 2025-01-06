using KUX.Infrastructure;
using KUX.Models.BO;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Reponse.ChatRoom;
using KUX.Models.CzimAdmin.Request.ChatRoom;
using KUX.Models.CzimAdmin.Request.CzimChat;
using KUX.Models.CzimSection;
using KUX.Models.Entities.CzimAdmin.ChatRoom;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUX.Models.Entities.Framework;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 聊天室服务
    /// </summary>
    public class CzimChatRoomService : AdminBaseService<CzimChatRoomRepository>
    {
        /// <summary>
        /// 环信服务
        /// </summary>
        private readonly EaseImService _easeImService;

        public CzimChatRoomService(CzimChatRoomRepository repository, EaseImService easeImService) : base(repository)
        {
            this._easeImService = easeImService;
        }

        /// <summary>
        /// 获取所有聊天室信息
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindChatRoomListAsync(ChatRoomSearchDto crsd)
        {
            //查询聊天室信息 关联 聊天室客服表
            var result = await this.Repository.Orm.Select<CzimChatroomInfo, CzimChatCustomer>()
                                    //关联条件为 当前聊天室的 web客服id = 聊天室客服的 web用户id
                                    .LeftJoin(l => l.t1.ManagerId == l.t2.UserId)
                                    //查询条件为 传入的对象不为空 并且 聊天室名称不为空 
                                    .WhereIf(crsd != null && !string.IsNullOrWhiteSpace(crsd.ChatroomName), w => w.t1.ChatRoomName.Contains(crsd.ChatroomName))
                                    //倒序排列 聊天室是否可用 
                                    .OrderByDescending(o => o.t1.IsActive)
                                    //倒序排列 创建时间 
                                    .OrderByDescending(o => o.t1.CrtDate)
                                    .Count(out var total)
                                    .Page(crsd.Page, crsd.Size)
                                    .ToListAsync(t => new
                                    {
                                        t.t1.Id,
                                        t.t1.IsActive,
                                        t.t1.Level,
                                        t.t1.ManagerId,
                                        t.t2.NickName,
                                        t.t1.UserNum,
                                        t.t1.CrtDate,
                                        t.t1.ChatRoomName,
                                        t.t1.ChatRoomImg,
                                        t.t1.ChatRoomNotice,
                                        t.t1.IsProhibit,
                                        t.t1.IsDefault
                                    });

            return await this.Repository.AsPageViewModelAsync(result, crsd.Page, crsd.Size, total);
        }

        //删除单个聊天室成员
        //参数为当前聊天室id(环信id)，要删除的成员id()
        /// <summary>
        /// 修改默认聊天室
        /// </summary>
        /// <param name="crad"></param>
        /// <returns></returns>
        public async Task<(bool, string)> DoDefaultAsync(ChatRoomActiveDto crad)
        {
            // 取消所有聊天的默认状态
            //将状态为默认聊天室的状态改为不默认 
            var updOld = await this.Repository.Orm.Update<CzimChatroomInfo>()
                                            .Set(s => s.IsDefault, false)
                                            .Where(s => s.IsDefault)
                                            .ExecuteAffrowsAsync();
            // 更新聊天室状态
            // 将当前聊天室状态改为默认
            var updNew = await this.Repository.Orm.Update<CzimChatroomInfo>()
                                    .Set(s => s.IsDefault, true)
                                    .Where(w => w.Id == crad.Id)
                                    .ExecuteAffrowsAsync();

            if (updNew > 0)
            {
                return (true, "重置成功！");
            }

            return (false, "聊天室异常，请选择正确聊天室！");
        }

        /// <summary>
        /// 获取聊天室所有管理员信息
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindManagerListAsync(ChatRoomSearchDto crsd)
        {
            //查询聊天室客服表 关联 聊天室会员表
            var query = await this.Repository.Orm.Select<CzimChatCustomer, CzimChatroomManager>()
                                    //左连接 聊天室客服表的userid = 聊天室管理员表的管理员id 
                                    .LeftJoin(l => l.t1.UserId == l.t2.ManagerId 
                                    //当前聊天室管理员的信息为可用状态 
                                    && l.t2.IsActive 
                                    //根据 聊天室id去查询聊天室管理员中的聊天室id
                                    && l.t2.ChatRoomId == crsd.ChatRoomId)
                                    .OrderByDescending(o => o.t2.CDate)
                                    .Page(crsd.Page, crsd.Size)
                                    .Count(out var total)
                                    .ToListAsync(t => new
                                    {
                                        t.t1.UserId,
                                        t.t1.Avatar,
                                        t.t1.NickName,
                                        //判断聊天室管理员表是否为空
                                        //为空则返回false 不为空则返回true
                                        IsManager = t.t2 != null,
                                        //判断聊天室管理员表不为空，
                                        //并且判断聊天室管理员表的是否超管字段是不是true
                                        //以上两个条件都满足则超管字段返回true，否则返回false
                                        IsSuper = (t.t2 != null && t.t2.IsSuper)
                                    });
            return await this.Repository.AsPageViewModelAsync(query, crsd.Page, crsd.Size, total);
        }

        /// <summary>
        /// 添加或移除聊天室管理员
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="crmd">聊天室管理信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> AddOrDelManagerAsync(string userId, ChatRoomManagerDto crmd)
        {
            // 判断当前聊天室是否有环信账号
            var chatRoomInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                        //根据传入的 聊天室id 查询 聊天室信息 并且 聊天室状态为可用
                                        .Where(s => s.Id == crmd.ChatRoomId && s.IsActive)
                                        .FirstAsync();

            var easeChatRoomId = "";
            //当前聊天室信息不为空 并且 聊天室id也不为空 
            if (chatRoomInfo != null && !string.IsNullOrWhiteSpace(chatRoomInfo?.Id))
            {
                //将当前聊天室的环信id赋值
                easeChatRoomId = chatRoomInfo.EaseChatRoomId;
                //判断聊天室是否有环信账号
                if (string.IsNullOrEmpty(easeChatRoomId))
                {
                    return (false, "没有聊天室的环信账号，请联系管理员");
                }
            }
            else
            {
                return (false, "未找到当前聊天室，聊天室异常！");
            }

            var doDate = DateTime.Now;
            //1是添加管理员 
            if (crmd.AddOrDel == 1)
            {
                // 是否已经是本聊天室的管理员
                var chatRoomManager = await this.Repository.Orm.Select<CzimChatroomManager>()
                                        //web后台账户id 和 聊天室id 去查询
                                       .Where(s => s.ManagerId == crmd.userId && s.ChatRoomId == crmd.ChatRoomId && s.IsActive)
                                       .FirstAsync();
                //如果查询结果不为空 则说明已经是当前聊天室管理员 
                if (chatRoomManager != null && !string.IsNullOrWhiteSpace(chatRoomManager?.Id))
                {
                    return (false, "已经是当前聊天室的管理员");
                }
                //聊天室管理员对象
                CzimChatroomManager bcm = new CzimChatroomManager()
                {
                    ChatRoomId = crmd.ChatRoomId,
                    ManagerId = crmd.userId,
                    CDate = doDate,
                    UDate = doDate,
                    CrtDate = doDate,
                    CUser = userId,
                    UUser = userId,
                    IsActive = true
                };
                //参数为：web端用户id 及 环信聊天室id
                //调用环信接口：添加环信聊天室的管理员
                var imUser = await _easeImService.ChatRoomAddAdmin(crmd.userId, easeChatRoomId);
                //环信返回结果为成功
                if (imUser?.result == "success")
                {
                    //则向数据库发送添加聊天室管理员数据的请求
                    var result = await this.Repository.Orm.Insert<CzimChatroomManager>(bcm).ExecuteAffrowsAsync();
                    //如果添加成功 则 提示
                    if (result > 0)
                    {
                        return (true, "管理员添加成功！");
                    }
                    //var deleteResult = await this.Repository.Orm.Delete<CzimChatroomManager>(bcm).ExecuteAffrowsAsync();
                    //如果没成功
                    //则 删除环信管理员
                    await _easeImService.ChatRoomDeleteAdmin(crmd.userId, easeChatRoomId);
                    return (false, "请勿重复操作！");
                }
                //如果环信返回结果为失败 则 提示 
                else
                {
                    return (false, "环信聊天室注册管理员身份失败，请联系管理员!");
                }
            }
            //2代表 删除管理员信息
            else if (crmd.AddOrDel == 2)
            {
                //
                //删除聊天室管理员信息时，先查询当前聊天室管理员是否存在
                var Info = await this.Repository.Orm.Select<CzimChatroomManager>()
                                                .Where(w => w.ChatRoomId == crmd.ChatRoomId && w.ManagerId == crmd.userId && w.IsActive)
                                                .FirstAsync();
                if (Info == null)
                {
                    return (false, "聊天室/管理员信息有误！");
                }
                // 判断如果为超管，禁止删除
                if (Info.IsSuper)
                {
                    return (false, "超管不可以移除！");
                }
                var delInfo = await this.Repository.Orm.Delete<CzimChatroomManager>()
                                                .Where(w => w.ChatRoomId == crmd.ChatRoomId &&
                                                                w.ManagerId == crmd.userId
                                                                && !w.IsSuper && w.IsActive)
                                                .ExecuteAffrowsAsync();
                if (delInfo > 0)
                {
                    // 删除环信管理员
                    await _easeImService.ChatRoomDeleteAdmin(crmd.userId, easeChatRoomId);
                    return (true, "管理员已移除！");
                }
            }

            return (false, "请选择正常操作");
        }

        /// <summary>
        /// 获取当前会员可加入的聊天室列表
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindChatRoomAbbreAsync(ChatRoomMemberSearchDto crsd)
        {
            if (string.IsNullOrWhiteSpace(crsd.MemberId))
            {
                MessageBox.Show("会员信息不能为空！");
                return default;
            }
            //获取当前可加入的聊天室信息
            var craEntity = await this.Repository.FindChatRoomAbbreAsync(crsd.MemberId, crsd.ChatroomName, crsd.Page, crsd.Size);
            //
            var result = craEntity.Item2.MapToList<ChatRoomAbbreEntity, ChatRoomInfoVo>();
            return await this.Repository.AsPageViewModelAsync(result, crsd.Page, crsd.Size, craEntity.Item1);
        }

        /// <summary>
        /// 开启聊天室频道频道
        /// </summary>
        /// <param name="aInfo">管理员信息</param>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> OpenChatRoomChanAsync(AccountInfo aInfo, ChatRoomActiveDto crad)
        {
            var cmInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                                    .Where(w => w.Id == crad.Id &&
                                                                w.IsActive)
                                                    .FirstAsync();

            if (cmInfo == null)
            {
                return (false, "聊天室不存在，或者已关闭！");
            }

            if (!aInfo.IsAdministrator)
            {
                // 判断是否有管理员权限
                if (cmInfo.ManagerId != aInfo.CUser)
                {
                    // 没有权限
                    return (false, "没有操作权限！");
                }
            }

            // 开启群频道

            return (true, "聊天室开放成功！");
        }

        /// <summary>
        /// 查询form
        /// </summary>
        /// <param name="id">聊天室id</param>
        /// <returns></returns>
        public async Task<CzimChatroomInfo> FindChatRoomAsync(string id)
        {
            var result = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                    .Where(w => w.Id == id)
                                    .FirstAsync();
            return result;
        }

        /// <summary>
        /// 新增/修改聊天室
        /// </summary>
        /// <param name="userId">创建人id</param>
        /// <param name="form">群信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> SaveFormAsync(string userId, ChatRoomDto form)
        {
            var doDate = DateTime.Now;

            CzimChatroomInfo bci = form.MapTo<ChatRoomDto, CzimChatroomInfo>();

            if (string.IsNullOrWhiteSpace(form.Id))
            {
                if (string.IsNullOrEmpty(form.ManagerId))
                {
                    throw new Exception("管理员不能为空");
                }
                bci.Id = Guid.NewGuid().ToString("N");
                bci.IsProhibit = false;
                // 执行新增
                bci.IsActive = true;
                bci.CrtDate = doDate;
                bci.CUser = userId;
                bci.UDate = doDate;
                bci.CDate = doDate;
                bci.UUser = userId;

                // 保存聊天室到环信
                var imChatRoom = await _easeImService.CreateChatRoomAsync(form.ChatRoomName, form.ChatRoomNotice, form.ManagerId, new List<string>() { form.ManagerId }, 100000);

                if (string.IsNullOrWhiteSpace(imChatRoom))
                {
                    MessageBox.Show("聊天室保存失败，请稍后再试！");
                }

                bci.EaseChatRoomId = imChatRoom;

                var result = await this.Repository.Orm.Insert(bci)
                                            .ExecuteAffrowsAsync();

                if (result > 0)
                {
                    CzimChatroomManager bcm = new CzimChatroomManager()
                    {
                        ChatRoomId = bci.Id,
                        ManagerId = bci.ManagerId,
                        IsSuper = true,
                        IsActive = true,
                        CrtDate = doDate,
                        CUser = userId,
                        UDate = doDate,
                        CDate = doDate,
                        UUser = userId,
                    };

                    // 保存超管到群管理员表中
                    await this.Repository.Orm.Insert<CzimChatroomManager>(bcm)
                                            .ExecuteAffrowsAsync();

                    return (true, "新增成功！");
                }

                MessageBox.Show("保存失败");
                return (false, "新增失败");
            }

            // 执行更改，不可以改超管信息
            var updInfo = await this.Repository.Orm.Update<CzimChatroomInfo>()
                                        .Set(s => new CzimChatroomInfo
                                        {
                                            ChatRoomImg = form.ChatRoomImg,
                                            ChatRoomName = form.ChatRoomName,
                                            ChatRoomNotice = form.ChatRoomNotice,
                                            Level = form.Level,
                                            //ManagerId = form.ManagerId,
                                            UserNum = form.UserNum,
                                            UDate = doDate,
                                            UUser = userId,
                                            ManagerId = form.ManagerId
                                        })
                                        .Where(w => w.Id == form.Id)
                                        .ExecuteAffrowsAsync();

            if (updInfo > 0)
            {
                return (true, "修改成功！");
            }

            return (false, "修改失败");
        }

        //查询所有可加入聊天室的会员
        /// <summary>
        /// 查询所有可加入聊天室的会员
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> ChatRoomAddMemberAsync(ChatRoomAddMemberSearchDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ChatRoomId))
            {
                MessageBox.Show("聊天室信息为空！");
                return default;
            }
            //传入当前聊天室id，
            //查询所有的会员信息czim_member_info 所有会员信息
            var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                                   .WhereIf(!string.IsNullOrWhiteSpace(dto.NickName), s => s.NickName.Contains(dto.NickName))
                                                   .ToListAsync();
            //当前聊天室的会员信息
            var chatRoomMember = await this.Repository.Orm.Select<CzimChatroomMember>()
                                                   .WhereIf(!string.IsNullOrEmpty(dto.ChatRoomId), s => s.ChatRoomId.Equals(dto.ChatRoomId))

                                                   .ToListAsync(t => t.MemberId);

            var mInfo = memberInfo.Where(w => !chatRoomMember.Contains(w.Id)).ToList();

            //var list = new List<CzimMemberInfo>();

            ////遍历 不属于当前聊天室的会员信息 全部添加到新的集合中
            //foreach (var itemMemberInfo in memberInfo)
            //{
            //    if (!chatRoomMember.Any(a => a.Equals(itemMemberInfo.Id)))
            //    {
            //        list.Add(itemMemberInfo);
            //    }
            //}
            var query = mInfo.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, mInfo.Count);
        }

        //当前聊天室不存在的会员信息
        /// <summary>
        /// 聊天室失效
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        public async Task<(bool, string)> IsActiveFormAsync(string userId, ChatRoomActiveDto crad)
        {
            var result = await this.Repository.Orm.Update<CzimChatroomInfo>()
                                    .Set(s => s.IsActive, crad.IsActive)
                                    .Set(s => s.UUser, userId)
                                    .Set(s => s.UDate, DateTime.Now)
                                    .Where(w => w.Id == crad.Id)
                                    .ExecuteAffrowsAsync();

            return (false, "操作失败，请选择聊天室重新处理");
        }

        /// <summary>
        /// 获取所有管理员信息
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetAllCustomerAsync()
        {
            //判定是否可以成为客服的人员时，必须是环信人员
            var result = await this.Repository.Orm.Select<SysUser, CzimChatCustomer>()
                                    .LeftJoin(s=>s.t1.Id==s.t2.UserId)
                                    .Where(w => w.t1.IsActive && w.t2.EaseChatCustomerId != null)
                                    .ToListAsync(t => new
                                    {
                                        ManagerId = t.t2.UserId,
                                        ManagerName = t.t2.NickName,
                                        Avatar = t.t2.Avatar
                                    });

            return result;
        }
        /// <summary>
        /// 获取当前聊天室用户的禁言状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool, object)> GetForbidState(ChatRoomOfMemberForbiddenStateDto dto)
        {
            //GetForbidState
            //查询当前聊天室会员信息
            var chatRoomMember = await Repository.Orm.Select<CzimChatroomMember>()
                                              .Where(s => s.ChatRoomId == dto.ChatRoomId && s.MemberId == dto.MemberId)
                                              .FirstAsync();
            //获取到禁言时间
            var forbiddenTime = chatRoomMember.ForbiddenTime;
            //0代表当前没有被禁言 天数代表还剩多少天禁言时间 
            //如果当前禁言时间不为空 
            if (forbiddenTime != DateTime.MinValue)
            {
                if (forbiddenTime<DateTime.Now)
                {
                    return (true, 0);
                }
                //则返回禁言时间 
                return (true, forbiddenTime);
            }
            else
            {
                return (true, 0);
            }
        }
        /// <summary>
        /// 当前聊天室所有会员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageViewModel> GetMemberOfChatRoomList(ChatRoomSearchDto dto)
        {
            var result = await this.Repository.Orm.Select<CzimChatroomInfo, CzimChatroomMember, CzimMemberInfo>()
                                                .LeftJoin(s => s.t2.ChatRoomId.Equals(s.t1.Id))
                                                .LeftJoin(s => s.t2.MemberId.Equals(s.t3.Id))
                                                .WhereIf(!string.IsNullOrEmpty(dto.ChatRoomId), s => s.t2.ChatRoomId == dto.ChatRoomId)
                                                .Count(out var total)
                                                .Page(dto.Page, dto.Size)
                                                .ToListAsync(s => new
                                                {
                                                    s.t3.NickName,
                                                    s.t3.Avatar,
                                                    s.t2.JoinDate,
                                                    s.t3.LoginName
                                                });
            return await this.Repository.AsPageViewModelAsync(result, dto.Page, dto.Size, total);
        }

        /// <summary>
        /// 获取当前聊天室所有成员
        /// </summary>
        /// <param name="dto">查询信息</param>
        /// <returns></returns>
        public async Task<object> GetAllMemberAsync(ChatRoomSearchDto dto)
        {
            var result = await this.Repository.GetAllMemberAsync(dto.ChatRoomId, dto.Page, dto.Size);

            return new { 
                Count = result.Item1,
                DataSource = result.Item2
            };
        }
    }
}