using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Reponse.Chat;
using KUX.Models.CzimAdmin.Request.CzimChatGroup;
using KUX.Models.CzimSection;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using KUX.Services.EaseImServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection
{
    /// <summary>
    /// 群组服务
    /// </summary>
    public class CzimChatGroupService : AdminBaseService<CzimChatGroupRepository>
    {
        /// <summary>
        /// 注入的环信服务
        /// </summary>
        private readonly EaseImService easeImService;

        /// <summary>
        /// 注入读取配置文件服务
        /// </summary>
        private readonly AdminConfiguration appConfiguration;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository">默认仓储</param>
        /// <param name="_easeImService">环信服务</param>
        /// <param name="_appConfiguration">读取配置文件</param>
        public CzimChatGroupService(CzimChatGroupRepository repository, EaseImService _easeImService, AdminConfiguration _appConfiguration) : base(repository)
        {
            easeImService = _easeImService;
            appConfiguration = _appConfiguration;
        }

        /// <summary>
        /// 获取所有群组信息
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindChatGroupListAsync(ChatGroupSearchDto crsd)
        {
            //查询聊天室信息 关联 聊天室客服表
            var result = (await this.Repository.Orm.Select<CzimChatgroupInfo, CzimChatCustomer>()
                                    //关联条件为 当前聊天室的 web客服id = 聊天室客服的 web用户id
                                    .LeftJoin(l => l.t1.OwnId == l.t2.UserId)
                                    //查询条件为 传入的对象不为空 并且 聊天室名称不为空
                                    .WhereIf(crsd != null && !string.IsNullOrWhiteSpace(crsd.GroupName), w => w.t1.GroupName.Contains(crsd.GroupName))
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
                                        t.t1.OwnId,
                                        t.t1.UserNum,
                                        CrtDate = t.t1.CrtDate != DateTime.MinValue ? t.t1.CrtDate.ToString("yyyy-MM-dd HH:mm") : "",
                                        t.t1.GroupName,
                                        t.t1.GroupImg,
                                        t.t1.Announcement,
                                        t.t1.IsProhibit,
                                        t.t1.IsDefault,
                                        t.t1.IsPublic,
                                        t.t2.NickName,
                                        t.t2.Avatar,
                                        t.t1.Maxusers
                                    })).Distinct();

            return await this.Repository.AsPageViewModelAsync(result, crsd.Page, crsd.Size, total);
        }

        /// <summary>
        /// 当前群组所有会员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageViewModel> GetMemberOfChatGroupListAsync(ChatGroupSearchDto dto)
        {
            var result = await this.Repository.Orm.Select<CzimChatgroupInfo, CzimChatgroupMember, CzimMemberInfo>()
                                                .LeftJoin(s => s.t2.ChatgroupId.Equals(s.t1.Id))
                                                .LeftJoin(s => s.t2.MemberId.Equals(s.t3.Id))
                                                .WhereIf(!string.IsNullOrEmpty(dto.GroupId), s => s.t2.ChatgroupId == dto.GroupId)
                                                .Where(s => s.t1.IsActive && s.t2.IsActive && s.t3.IsActive)
                                                .OrderByDescending(s => s.t2.IsManager)
                                                .OrderBy(s => s.t2.JoinDate)
                                                .Count(out var total)
                                                .Page(dto.Page, dto.Size)
                                                .ToListAsync(s => new
                                                {
                                                    s.t3.NickName,
                                                    s.t3.Avatar,
                                                    s.t2.MemberId,
                                                    IsManager = s.t2.IsManager ? 1 : 0,
                                                    IsForbidden = s.t2.ForbiddenTime >= DateTime.Now ? 1 : 0,
                                                    s.t2.JoinDate,
                                                    s.t3.LoginName
                                                });
            return await this.Repository.AsPageViewModelAsync(result, dto.Page, dto.Size, total);
        }

        /// <summary>
        /// 获取群组所有管理员信息
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindManagerListAsync(ChatGroupSearchDto crsd)
        {
            var _groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                                .Where(w => w.Id == crsd.GroupId)
                                                .FirstAsync();

            if (_groupInfo == null || string.IsNullOrWhiteSpace(_groupInfo.EaseChatgroupId))
            {
                MessageBox.Show("群组异常！");
            }

            //查询聊天室客服表 关联 聊天室会员表
            var query = await this.Repository.Orm.Select<CzimChatCustomer, SysUser, CzimChatgroupMember>()
                                                    .LeftJoin(s => s.t2.Id == s.t1.UserId)
                                                    .LeftJoin(l => l.t1.UserId == l.t3.MemberId && l.t3.ChatgroupId == crsd.GroupId && l.t3.IsActive)
                                                    .Where(l => l.t1.IsActive && l.t2.IsActive)
                                                    .OrderByDescending(o => o.t3.IsManager)
                                                    .OrderByDescending(o => o.t2.CDate)
                                                    .Page(crsd.Page, crsd.Size)
                                                    .Count(out var total)
                                                    .ToListAsync(t => new ChatGroupManagerVo()
                                                    {
                                                        UserId = t.t1.UserId,
                                                        Avatar = t.t1.Avatar,
                                                        NickName = t.t1.NickName,
                                                        IsManager = t.t3 == null ? false : t.t3.IsManager,
                                                        IsOwner = false
                                                    });

            foreach (var item in query)
            {
                if (item.UserId == _groupInfo.OwnId)
                {
                    item.IsOwner = true;
                    break;
                }
            }
            return await this.Repository.AsPageViewModelAsync(query, crsd.Page, crsd.Size, total);
        }

        /// <summary>
        /// 添加群组管理员
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupAddManagerAsync(string userId, GroupAddManagerDto dto)
        {
            //判断当前是否是管理员的逻辑
            //var isManager = await Repository.Orm.Select<CzimChatgroupMember>()
            //    .Where(s=>s.IsActive&&s.MemberId==userId&&s.IsManager&&s.ChatgroupId==dto.GroupId)
            //    .FirstAsync();

            //if (isManager==null)
            //{
            //    MessageBox.Show("当前登录账号并不是当前群组的管理员！不能添加群组管理员");
            //}

            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.GroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                MessageBox.Show("用户信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.GroupId && s.IsActive)
                                            .FirstAsync();
            if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
            {
                MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
            }
            //当前管理员是否存在环信id
            var customer = await Repository.Orm.Select<CzimChatCustomer>()
                                                .Where(s => s.UserId == dto.UserId && s.IsActive)
                                                .FirstAsync();
            if (string.IsNullOrEmpty(customer.EaseChatCustomerId))
            {
                MessageBox.Show("管理员账户的环信账号存在异常，请联系管理员！");
            }
            //传入参数 聊天室id 用户id
            //判断当前群成员列表是否存在当前成员
            var groupMemberInfo = await Repository.Orm.Select<CzimChatgroupMember>()
                                                .Where(s => s.MemberId == dto.UserId && s.ChatgroupId == dto.GroupId)
                                                .FirstAsync();
            //不存在需要添加
            if (groupMemberInfo == null)
            {
                //先添加群组成员
                GroupAddManagerDto memberdto = new GroupAddManagerDto()
                {
                    GroupId = dto.GroupId,
                    UserId = dto.UserId
                };
                //调用群组添加成员接口 添加到成员信息表中并且添加到环信服务器上
                var imInfoMember = await GroupAddMemberAsync(userId, memberdto);
                if (!imInfoMember.Item1)
                {
                    MessageBox.Show("添加异常，请联系管理员！");
                }
            }

            //调用环信接口 聊天室添加管理员
            var imInfo = await easeImService.ChatgroupAddManagerAsync(imGroup.EaseChatgroupId, dto.UserId);

            if (imInfo)
            {
                var chatGroupResult = await Repository.Orm.Update<CzimChatgroupMember>()
                                                       .Set(s => s.IsManager, true)
                                                       .Where(w => w.ChatgroupId == dto.GroupId &&
                                                                        w.EaseChatgroupId == imGroup.EaseChatgroupId &&
                                                                        w.MemberId == dto.UserId)
                                                            .ExecuteAffrowsAsync();
                if (chatGroupResult > 0)
                {
                    return (true, "添加管理员成功！");
                }

                // 添加失败，需要删除环信管理员

                return (false, "添加管理员失败！");
            }

            return (false, "添加群成员时环信出现异常，请联系管理员！");
        }

        //移除群组管理员
        /// <summary>
        /// 移除群组管理员
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="dto">要移除的对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupDeleteManagerAsync(string userId, GroupAddManagerDto dto)
        {
            //缺判断当前是否是超管或者管理员的逻辑
            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.GroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                MessageBox.Show("用户信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.GroupId && s.IsActive)
                                            .FirstAsync();
            if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
            {
                MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
            }
            //当前管理员是否存在环信id
            var customer = await Repository.Orm.Select<CzimChatCustomer>()
                                                .Where(s => s.UserId == dto.UserId && s.IsActive)
                                                .FirstAsync();
            if (string.IsNullOrEmpty(customer.EaseChatCustomerId))
            {
                MessageBox.Show("管理员账户的环信账号存在异常，请联系管理员！");
            }
            var imInfo = await easeImService.ChatgroupDeleteManagerAsync(imGroup.EaseChatgroupId, dto.UserId);
            if (imInfo == "success")
            {
                var chatGroupResult = await Repository.Orm.Update<CzimChatgroupMember>()
                                                      .Where(s => s.MemberId == dto.UserId && s.ChatgroupId == dto.GroupId && s.IsActive)
                                                      .Set(s => s.IsActive, false)
                                                      .ExecuteAffrowsAsync();
                if (chatGroupResult > 0)
                {
                    return (true, "移除管理员成功！");
                }
                return (false, "移除管理员失败！");
            }
            return (false, "环信异常！请联系管理员");
        }

        //添加单个群组成员
        /// <summary>
        /// 添加单个群组成员
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupAddMemberAsync(string userId, GroupAddManagerDto dto)
        {
            //缺判断当前是否是超管或者管理员的逻辑
            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.GroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                MessageBox.Show("用户信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.GroupId && s.IsActive)
                                            .FirstAsync();
            if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
            {
                MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
            }
            //当前用户是否存在环信账号
            var memberInfo = await Repository.Orm.Select<CzimMemberInfo>()
                                                .Where(s => s.Id == dto.UserId && s.IsActive)
                                                .FirstAsync();
            //当前客服/推广员是否存在环信账号
            var customerInfo = await Repository.Orm.Select<CzimChatCustomer>()
                                                 .Where(s => s.UserId == dto.UserId && s.IsActive)
                                                 .FirstAsync();
            if (memberInfo != null)
            {
                if (string.IsNullOrEmpty(memberInfo.EaseId))
                {
                    MessageBox.Show("会员的环信账号存在异常，请联系管理员！");
                }
            }
            if (customerInfo != null)
            {
                if (string.IsNullOrEmpty(customerInfo.EaseChatCustomerId))
                {
                    MessageBox.Show("会员的环信账号存在异常，请联系管理员！");
                }
            }
            //传入参数 聊天室id 用户id
            //判断当前传入的用户id是否是群主id
            var isAdmin = await Repository.Orm.Select<CzimChatgroupInfo>().Where(s => s.OwnId == dto.UserId && s.Id == dto.GroupId).FirstAsync();
            if (isAdmin != null)
            {
                MessageBox.Show("添加的成员是本群群主，不能添加！");
            }
            //调用环信接口 聊天室添加管理员
            var imResult = await easeImService.ChatgroupAddUserAsync(imGroup.EaseChatgroupId, dto.UserId);

            if (imResult)
            {
                var chatGroupMember = new CzimChatgroupMember()
                {
                    ChatgroupId = dto.GroupId,
                    EaseChatgroupId = imGroup.EaseChatgroupId,
                    MemberId = dto.UserId,
                    IsActive = true,
                    IsManager = false,
                    JoinDate = nowTime,
                    CDate = nowTime,
                    CUser = userId
                };
                var chatGroupResult = await Repository.Orm.Insert(chatGroupMember).ExecuteAffrowsAsync();
                if (chatGroupResult > 0)
                {
                    return (true, "添加会员成功！");
                }
                return (false, "添加会员失败！");
            }
            return (false, "添加管理员时环信出现异常，请联系管理员！");
        }

        //批量添加群组成员
        /// <summary>
        /// 批量添加群组成员
        /// </summary>
        /// <param name="userId">管理员id</param>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupAddMemberListAsync(string userId, GroupAddMemberListDto dto)
        {
            //缺判断当前是否是超管或者管理员的逻辑
            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.ChatGroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (dto.MemberIds.Count <= 0)
            {
                MessageBox.Show("成员信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.ChatGroupId && s.IsActive)
                                            .FirstAsync();
            if (imGroup != null)
            {
                if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
                {
                    MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
                }
            }

            //传入参数 聊天室id 用户id
            //调用环信接口 聊天室添加管理员
            var imResult = await easeImService.ChatgroupAddUserListAsync(imGroup.EaseChatgroupId, dto.MemberIds);

            if (imResult == null || imResult.Count <= 0)
            {
                MessageBox.Show("环信聊天室添加会员失败，请联系管理员");
            }

            List<CzimChatgroupMember> ccms = new List<CzimChatgroupMember>();
            imResult.ForEach(i =>
            {
                // 执行群保存会员操作
                CzimChatgroupMember bcm = new CzimChatgroupMember()
                {
                    CDate = nowTime,
                    JoinDate = nowTime,
                    UDate = nowTime,
                    UUser = userId,
                    CUser = userId,
                    ChatgroupId = dto.ChatGroupId,
                    IsActive = true,
                    EaseChatgroupId = imGroup.EaseChatgroupId,
                    MemberId = i,
                    PullUser = userId
                };
                ccms.Add(bcm);
            });
            if (ccms.Count > 0)
            {
                var addMcr = await this.Repository.Orm.Insert(ccms).ExecuteAffrowsAsync();
                //聊天室app会员信息添加
                if (addMcr > 0 && addMcr == ccms.Count)
                {
                    return (true, "新增成功");
                }
            }

            //如果环信聊天室添加app会员信息成功，聊天室app会员信息添加失败，则要删除当前环信聊天室的数据
            await easeImService.ChatgroupDeleteUserListAsync(imGroup.EaseChatgroupId, imResult);

            // 删除本地数据
            var delMcr = this.Repository.Orm.Delete<CzimChatgroupMember>(imResult.ToArray());

            return (false, "环信群会员信息未添加成功，请联系管理员！");
        }

        //移除单个群组成员
        /// <summary>
        /// 移除单个群组成员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupDeleteMemberAsync(string userId, GroupAddManagerDto dto)
        {
            //缺判断当前是否是超管或者管理员的逻辑
            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.GroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                MessageBox.Show("用户信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.GroupId && s.IsActive)
                                            .FirstAsync();
            if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
            {
                MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
            }
            //是否存在环信id
            var memberInfo = await Repository.Orm.Select<CzimChatCustomer>()
                                                .Where(s => s.UserId == dto.UserId && s.IsActive)
                                                .FirstAsync();
            if (string.IsNullOrEmpty(memberInfo?.EaseChatCustomerId))
            {
                MessageBox.Show("会员的环信账号存在异常，请联系管理员！");
            }

            var imInfo = await easeImService.ChatgroupDeleteUserAsync(imGroup.EaseChatgroupId, dto.UserId);
            if (imInfo)
            {
                var member = await Repository.Orm.Delete<CzimChatgroupMember>()
                                                .Where(s => s.MemberId == dto.UserId && s.ChatgroupId == dto.GroupId)
                                                .ExecuteAffrowsAsync();
                if (member > 0)
                {
                    return (true, "移除群组成员成功");
                }
                return (false, "移除群组成员失败！");
            }
            return (false, "移除群组中的环信成员失败！请联系管理员");
        }

        //查询所有可加入群组的会员
        /// <summary>
        /// 查询所有可加入群组的会员
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> ChatGroupAddMemberAsync(ChatGroupAddMemberSearchDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ChatGroupId))
            {
                MessageBox.Show("群组信息为空！");
                return default;
            }
            ////传入当前聊天室id，
            //查询所有的会员信息czim_member_info 所有会员信息
            var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                                   .WhereIf(!string.IsNullOrWhiteSpace(dto.NickName), s => s.NickName.Contains(dto.NickName))
                                                   .ToListAsync();
            //当前聊天室的会员信息
            var chatRoomMember = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                                   .Where(s => s.ChatgroupId.Equals(dto.ChatGroupId))
                                                   .ToListAsync(t => t.MemberId);

            var mInfo = memberInfo.Where(w => !chatRoomMember.Contains(w.Id)).ToList();

            var query = mInfo.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, mInfo.Count);
        }

        //批量移除群组成员
        /// <summary>
        /// 批量移除群组成员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool, string)> GroupDeleteMemberListAsync(string userId, GroupAddMemberListDto dto)
        {
            //缺判断当前是否是超管或者管理员的逻辑
            var nowTime = DateTime.Now;
            if (string.IsNullOrEmpty(dto.ChatGroupId))
            {
                MessageBox.Show("群组信息不能为空");
            }
            if (dto.MemberIds.Count <= 0)
            {
                MessageBox.Show("成员信息不能为空");
            }
            //首先判断当前聊天室是否存在环信id
            var imGroup = await Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(s => s.Id == dto.ChatGroupId && s.IsActive)
                                            .FirstAsync();
            if (string.IsNullOrEmpty(imGroup.EaseChatgroupId))
            {
                MessageBox.Show("当前聊天室的环信账号存在异常，请联系管理员");
            }

            //传入参数 聊天室id 用户id
            //调用环信接口 聊天室添加管理员
            var imResult = await easeImService.ChatgroupDeleteUserListAsync(imGroup.EaseChatgroupId, dto.MemberIds);

            if (imResult == null || imResult.Count <= 0)
            {
                MessageBox.Show("前聊天室的环信账号存在异常，请联系管理员");
            }

            List<CzimChatgroupMember> ccms = new List<CzimChatgroupMember>();
            imResult.ForEach(i =>
            {
                // 执行群删除会员操作
                CzimChatgroupMember bcm = new CzimChatgroupMember()
                {
                    CDate = nowTime,
                    JoinDate = nowTime,
                    UDate = nowTime,
                    UUser = userId,
                    CUser = userId,
                    ChatgroupId = dto.ChatGroupId,
                    IsActive = true,
                    EaseChatgroupId = imGroup.EaseChatgroupId,
                    MemberId = i,
                    PullUser = userId
                };
                ccms.Add(bcm);
            });
            if (ccms.Count > 0)
            {
                var addMcr = await this.Repository.Orm.Delete<List<CzimChatgroupMember>>(ccms).ExecuteAffrowsAsync();
                //聊天室app会员信息添加
                if (addMcr > 0 && addMcr == ccms.Count)
                {
                    return (true, "删除成功");
                }
            }

            //如果环信聊天室添加app会员信息成功，聊天室app会员信息添加失败，则要删除当前环信聊天室的数据
            await easeImService.ChatgroupDeleteUserListAsync(imGroup.EaseChatgroupId, imResult);

            // 删除本地数据
            var delMcr = this.Repository.Orm.Delete<CzimChatgroupMember>(imResult.ToArray());

            return (false, "环信群会员信息未删除成功，请联系管理员！");
        }

        /// <summary>
        /// 获取当前群组中用户的禁言状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<(bool, object)> GetForbidStateAsync(ChatGroupOfMemberForbiddenStateDto dto)
        {
            // 获取会员当前状态
            var _mInfo = await this.Repository.Orm.Select<CzimMemberCapital>()
                                            .Where(w => w.MId == dto.MemberId)
                                            .FirstAsync();

            if (_mInfo == null || string.IsNullOrWhiteSpace(_mInfo.MId))
            {
                MessageBox.Show("当前用户不存在");
            }

            if (_mInfo.ForbiddenTime >= DateTime.Now)
            {
                //则返回禁言时间
                return (true, _mInfo.ForbiddenTime);
            }

            //查询当前聊天室会员信息
            var chatRoomMember = await Repository.Orm.Select<CzimChatgroupMember>()
                                              .Where(s => s.ChatgroupId == dto.ChatGroupId && s.MemberId == dto.MemberId)
                                              .FirstAsync();
            if (chatRoomMember == null)
            {
                return (true, -1);
            }
            //获取到禁言时间
            var forbiddenTime = chatRoomMember?.ForbiddenTime;
            //0代表当前没有被禁言 天数代表还剩多少天禁言时间
            //如果当前禁言时间不为空
            if (forbiddenTime != DateTime.MinValue)
            {
                if (forbiddenTime < DateTime.Now)
                {
                    return (false, 0);
                }
                //则返回禁言时间
                return (true, forbiddenTime);
            }
            else
            {
                return (false, 0);
            }
        }

        /// <summary>
        /// 获取当前会员可加入的群组列表
        /// </summary>
        /// <param name="dto">群组查询条件</param>
        /// <returns></returns>
        public async Task<PageViewModel> FindChatGroupAbbreAsync(ChatGroupMemberSearchDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MemberId))
            {
                MessageBox.Show("会员信息不能为空！");
                return default;
            }
            //查询所有的群组列表
            var allGroupList = await Repository.Orm.Select<CzimChatgroupInfo>()
                                               .WhereIf(!string.IsNullOrWhiteSpace(dto.ChatGroupName), s => s.GroupName.Contains(dto.ChatGroupName)).ToListAsync();
            //查询当前用户(普通用户)已经加入的群组列表
            var memberJoinGroupList = await Repository.Orm.Select<CzimChatgroupMember>()
                                                      .Where(s => s.MemberId == dto.MemberId)
                                                      .ToListAsync(s => s.ChatgroupId);
            //查询当前用户(群主身份)
            var memberJoinGroupIsAdmin = await Repository.Orm.Select<CzimChatgroupInfo>()
                                                       .Where(s => s.OwnId == dto.MemberId)
                                                       .ToListAsync(s => s.Id);

            //var mInfo = memberInfo.Where(w => !chatRoomMember.Contains(w.Id)).ToList();
            allGroupList = allGroupList.Where(s => !memberJoinGroupList.Contains(s.Id) && !memberJoinGroupIsAdmin.Contains(s.Id))
                                       .Skip((dto.Page - 1) * dto.Size).Take(dto.Size)
                                       .ToList();

            return await this.Repository.AsPageViewModelAsync(allGroupList, dto.Page, dto.Size, allGroupList.Count);
        }

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="userId">当前登录用户id</param>
        /// <param name="dto">创建群组的对象</param>
        /// <returns></returns>
        public async Task<(bool, string)> CreateChatGroupAsync(string userId, CzimChatGroupCreateDto dto)
        {
            var nowTime = DateTime.Now;

            #region 必填控制

            if (string.IsNullOrWhiteSpace(dto.GroupName))
            {
                MessageBox.Show("群组名称为必填项！");
            }
            if (string.IsNullOrWhiteSpace(dto.Description))
            {
                MessageBox.Show("群组描述为必填项！");
            }
            if (string.IsNullOrWhiteSpace(dto.IsPublic.ToString()))
            {
                MessageBox.Show("群组类型为必填项！");
            }
            if (string.IsNullOrWhiteSpace(dto.Maxusers.ToString()))
            {
                MessageBox.Show("群人员上限为必填项！");
            }

            #endregion 必填控制

            //根据群主id去查询客服表 判断当前群主id是否有环信id
            //没有就提示群主信息异常，请联系管理员
            var customer = await Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(s => s.IsActive && s.UserId == dto.OwnId)
                                        .FirstAsync();
            if (string.IsNullOrWhiteSpace(customer?.EaseChatCustomerId))
            {
                MessageBox.Show("群主信息异常，请联系管理员！");
            }

            //环信群组对象
            var ImGroupCreateDto = new ImGroupCreateDto()
            {
                GroupName = dto.GroupName,
                desc = dto.Description,
                IsOpen = dto.IsPublic,
                MaxUsers = dto.Maxusers,
                AllowInvites = dto.AllowInvites,
                MembersOnly = dto.MemberSonly,
                Owner = dto.OwnId
            };

            //创建环信群组 返回环信群组id
            var imGroupInfo = await easeImService.CreateChatGroupAsync(ImGroupCreateDto);

            //判断环信群组id不为空
            if (!string.IsNullOrWhiteSpace(imGroupInfo))
            {
                var groupInfo = new CzimChatgroupInfo()
                {
                    EaseChatgroupId = imGroupInfo,
                    GroupName = dto.GroupName,
                    GroupImg = dto.GroupImg,
                    Announcement = dto.Announcement,
                    Description = dto.Description,
                    MemberSonly = dto.MemberSonly,
                    AllowInvites = dto.AllowInvites,
                    Maxusers = dto.Maxusers,
                    OwnId = dto.OwnId,
                    CrtDate = nowTime,
                    CDate = nowTime,
                    CUser = userId,
                    IsActive = true,
                    IsPublic = dto.IsPublic
                };
                var isInsert = await this.Repository.Orm.Insert(groupInfo).ExecuteAffrowsAsync();

                if (isInsert > 0)
                {
                    return (true, "新增群组成功！");
                }
                return (false, "新增群组失败！");
            }

            return (false, "环信群组新增异常，请联系管理员");
        }

        /// <summary>
        /// 修改默认群组
        /// </summary>
        /// <param name="crad"></param>
        /// <returns></returns>
        public async Task<(bool, string)> DoDefaultAsync(ChatGroupActiveDto crad)
        {
            // 取消所有聊天的默认状态
            //将状态为默认聊天室的状态改为不默认
            var updOld = await this.Repository.Orm.Update<CzimChatgroupInfo>()
                                            .Set(s => s.IsDefault, false)
                                            .Where(s => s.IsDefault)
                                            .ExecuteAffrowsAsync();
            // 更新聊天室状态
            // 将当前聊天室状态改为默认
            var updNew = await this.Repository.Orm.Update<CzimChatgroupInfo>()
                                    .Set(s => s.IsDefault, true)
                                    .Where(w => w.Id == crad.Id)
                                    .ExecuteAffrowsAsync();

            if (updNew > 0)
            {
                return (true, "重置成功！");
            }

            return (false, "群组异常，请选择正确聊天室！");
        }

        /// <summary>
        /// 查询指定的群组信息
        /// </summary>
        /// <param name="id">群组id</param>
        /// <returns></returns>
        public async Task<CzimChatgroupInfo> FindChatGroupAsync(string id)
        {
            var result = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                    .Where(w => w.Id == id)
                                    .FirstAsync();
            return result;
        }
    }
}