using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.BO;
using KUX.Models.CzimAdmin.Request.ChatCustomer;
using KUX.Models.CzimAdmin.Request.CzimMember;
using KUX.Models.CzimSection;
using KUX.Models.DTO;
using KUX.Models.Entities.Framework;
using KUX.Models.KuxAdmin.Reponse.CzimMember;
using KUX.Models.KuxAdmin.Request.ChatFriend;
using KUX.Models.KuxAdmin.Request.CzimMember;
using KUX.Models.KuxAdmin.Request.CzimMemberLogin;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 会员服务
/// </summary>
public class CzimMemberService : AdminBaseService<CzimMemberRepository>
{
    /// <summary>
    /// 配置服务
    /// </summary>
    private readonly AdminConfiguration _adminConfiguration;

    /// <summary>
    /// 环信服务
    /// </summary>
    private readonly EaseImService easeImService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    /// <param name="adminConfiguration">配置文件</param>
    /// <param name="_easeImService">环信服务</param>
    public CzimMemberService(CzimMemberRepository repository, AdminConfiguration adminConfiguration,
        EaseImService _easeImService) : base(repository)
    {
        _adminConfiguration = adminConfiguration;
        easeImService = _easeImService;
    }

    /// <summary>
    /// 客服添加会员接口
    /// </summary>
    /// <param name="dto">传入的对象</param>
    /// <returns></returns>
    public async Task<(bool, string)> CustomerAddFriendAsync(CustomerAddFriendDto dto)
    {
        //非空判断
        //web端的用户id
        if (string.IsNullOrEmpty(dto.UserId))
        {
            return (false, "好友不能为空，请重新选择好友");
        }
        //app用户id
        if (string.IsNullOrEmpty(dto.MemberId))
        {
            return (false, "会员不能为空，请重新选择");
        }
        //好友列表实体
        var FriendList = new CzimChatFriendList()
        {
            MemberId = dto.MemberId,
            UserId = dto.UserId,
            CrtDate = DateTime.Now
        };

        var query = await this.Repository.Orm.Insert(FriendList).ExecuteAffrowsAsync();
        if (query > 0)
        {
            return (true, "添加成功！");
        }
        else
        {
            return (false, "添加失败");
        }
    }

    /// <summary>
    /// 查询所有可被会员添加的客服
    /// </summary>
    /// <param name="dto">会员添加客服实体</param>
    /// <returns>客服列表</returns>
    public async Task<PageViewModel> MemberAddRoomCustomerListAsync(MemberAddRoomCustomerSearchDto dto)
    {
        //app用户id不能为空（用户）
        if (string.IsNullOrWhiteSpace(dto.MemberId))
        {
            MessageBox.Show("会员信息不能为空");
            return default;
        }
        //查询所有客服
        var chatCustomer = await this.Repository.Orm.Select<CzimChatCustomer>()
                                                //查询app账号的昵称
                                                .WhereIf(!string.IsNullOrWhiteSpace(dto.NickName), s => s.NickName.Contains(dto.NickName))
                                                //查询app用户是否为可用
                                                .Where(s => s.IsActive)
                                                .ToListAsync();
        //查询当前会员的好友列表
        var chatFriendList = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                //查询app用户的用户id
                                                .WhereIf(!string.IsNullOrWhiteSpace(dto.MemberId), s => s.MemberId == dto.MemberId)
                                                //返回web用户id
                                                .ToListAsync(s => s.UserId);
        //web客服 查询web用户id不等于好友列表中的web用户id
        //过滤掉web客服中已经添加过的app用户
        var Customer = chatCustomer.Where(w => !chatFriendList.Contains(w.UserId)).ToList();
        //分页
        var query = Customer.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, Customer.Count);
    }

    /// <summary>
    /// 查询所有可被客服添加的会员
    /// </summary>
    /// <param name="dto">客服添加会员实体</param>
    /// <returns>客服列表</returns>
    public async Task<PageViewModel> CustomerAddRoomMemberListAsync(CustomerAddRoomMemberSearchDto dto)
    {
        //判断当前app用户id是否为空
        if (string.IsNullOrWhiteSpace(dto.CustomerId))
        {
            MessageBox.Show("会员信息不能为空");
            return default;
        }
        //查询手机端app用户
        var chatMember = await this.Repository.Orm.Select<CzimMemberInfo>()
                                                //模糊查询app用户昵称
                                                .WhereIf(!string.IsNullOrWhiteSpace(dto.NickName), s => s.NickName.Contains(dto.NickName))
                                                .ToListAsync();
        //查询好友列表
        var chatFriendList = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                //过滤当前web端用户id
                                                .WhereIf(!string.IsNullOrWhiteSpace(dto.CustomerId), s => s.UserId == dto.CustomerId)
                                                .ToListAsync(s => s.MemberId);
        //查询app用户 过滤掉好友列表 中的app账号id
        //就是过滤掉 当前web用户 已经添加过的 app账号
        var Customer = chatMember.Where(w => !chatFriendList.Contains(w.Id)).ToList();

        var query = Customer.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, Customer.Count);
    }

    //
    /// <summary>
    /// 获取用户列表数据
    /// </summary>
    /// <param name="search">查询条件</param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(MemberSearchDto search)
    {
        //获取app用户信息及明细信息
        var query = await this.Repository.Orm.Select<CzimMemberInfo, CzimMemberCapital>()
                                 .LeftJoin(l => l.t1.Id == l.t2.MId)
                                 .WhereIf(!string.IsNullOrWhiteSpace(search?.NickName), a => a.t1.NickName.Contains(search.NickName))
                                 .Page(search.Page, search.Size)
                                 .Count(out var total)
                                 .OrderByDescending(s => s.t1.IsAgent)
                                 .OrderByDescending(s => s.t1.CDate)
                                 .ToListAsync(t => new
                                 {
                                     t.t1.Id,
                                     t.t1.LoginName,
                                     t.t1.IsAgent,
                                     t.t1.Avatar,
                                     t.t1.BsvNumber,
                                     t.t1.NickName,
                                     t.t1.RegDate,
                                     LastLoginDate = t.t2.LastLoginDate == DateTime.MinValue ? "-" : t.t2.LastLoginDate.ToString("yyyy-MM-dd HH:mm"),
                                     CDate = t.t1.CDate.ToString("yyyy-MM-dd"),
                                     UDate = t.t1.UDate.ToString("yyyy-MM-dd"),
                                 });

        return await this.Repository.AsPageViewModelAsync(query, search.Page, search.Size, total);
    }

    /// <summary>
    /// 删除会员信息
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <param name="memberId">会员id</param>
    /// <returns></returns>
    public async Task<bool> DelMemberAsync(string userId, string memberId)
    {
        var _delMember = await this.Repository.Orm.Delete<CzimMemberInfo>()
                                            .Where(w => w.Id == memberId)
                                            .ExecuteAffrowsAsync();

        if (_delMember > 0)
        {
            // 删除扩展表、好友关系表、群成员表
            await this.Repository.Orm.Delete<CzimMemberCapital>()
                                        .Where(w => w.MId == memberId)
                                        .ExecuteAffrowsAsync();
            // 删除好友关系
            await this.Repository.Orm.Delete<CzimChatFriendList>()
                                        .Where(w => w.MemberId == memberId)
                                        .ExecuteAffrowsAsync();

            // 删除群信息
            await this.Repository.Orm.Delete<CzimChatgroupMember>()
                                        .Where(w => w.MemberId == memberId)
                                        .ExecuteAffrowsAsync();
            // 删除聊天室信息
            await this.Repository.Orm.Delete<CzimChatroomMember>()
                                        .Where(w => w.MemberId == memberId)
                                        .ExecuteAffrowsAsync();

            // 删除环信信息
            await this.easeImService.DeactivateUserAsync(memberId);
            await this.easeImService.DeleteUsersAsync(memberId);

            return true;
        }

        return false;
    }

    /// <summary>
    /// 聊天室添加会员
    /// </summary>
    /// <param name="userId">管理员id</param>
    /// <param name="cramd">聊天室会员信息</param>
    /// <returns></returns>
    public async Task<(bool, string)> JoinChatRoomAsync(string userId, ChatRoomAddMemberDto cramd)
    {
        // 查询聊天室
        var cmInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                    //根据传入的聊天室id去查询聊天室信息 可用的聊天室
                                    .Where(w => w.Id == cramd.ChatRoomId && w.IsActive)
                                    .FirstAsync();
        //聊天室为空 或者 聊天室id为空 或者 聊天室中的环信id为空 提示异常
        if (cmInfo == null ||
            string.IsNullOrWhiteSpace(cmInfo.Id) ||
            string.IsNullOrWhiteSpace(cmInfo.EaseChatRoomId))
        {
            return (false, "聊天室异常，请刷新后重试！");
        }

        var doDate = DateTime.Now;
        // 判断app会员是否已经加入当前聊天室 加入就返回true
        var hasMcr = await this.Repository.Orm.Select<CzimChatroomMember>()
                                    .Where(w => w.MemberId == cramd.MemberId && w.ChatRoomId == cramd.ChatRoomId)
                                    .AnyAsync();

        if (hasMcr)
        {
            //已加入，就提示已经加入
            return (false, "会员已属于群成员");
        }
        //参数：聊天室app会员id和聊天室id 调用环信添加聊天室app会员的方法，返回聊天室的对象
        var imInfo = await easeImService.ChatRoomAddUser(cramd.MemberId, cmInfo.EaseChatRoomId);
        if (imInfo == null || imInfo.result == false || string.IsNullOrWhiteSpace(imInfo.id))
        {
            return (false, "环信聊天室添加会员信息异常，请联系管理员！");
        }
        // 聊天室app会员信息
        CzimChatroomMember bcm = new CzimChatroomMember()
        {
            CDate = doDate,
            JoinDate = doDate,
            UDate = doDate,
            UUser = userId,
            CUser = userId,
            ChatRoomId = cramd.ChatRoomId,
            IsActive = true,
            MemberId = cramd.MemberId,
            PullUser = userId
        };
        //聊天室app会员信息添加
        var addMcr = await this.Repository.Orm.Insert(bcm).ExecuteAffrowsAsync();
        if (addMcr > 0)
        {
            return (true, "添加成功");
        }
        //如果环信聊天室添加app会员信息成功，聊天室app会员信息添加失败，则要删除当前环信聊天室的数据
        await easeImService.ChatRoomDeleteUser(cramd.MemberId, cmInfo.EaseChatRoomId);
        return (false, "环信聊天室会员信息未添加成功，请联系管理员！");
    }

    /// <summary>
    /// 群添加会员
    /// </summary>
    /// <param name="aInfo">管理员信息</param>
    /// <param name="cramd">成员列表</param>
    /// <returns></returns>
    public async Task<(bool, string)> JoinChatgroupAsync(AccountInfo aInfo, ChatgroupAddMemberDto cramd)
    {
        var canAdd = false;
        if (aInfo == null)
        {
            MessageBox.Show("请登录帐号");
        }

        if (aInfo.IsAdministrator)
        {
            canAdd = true;
        }

        // 查询群
        var cgInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                    //根据传入的聊天室id去查询聊天室信息 可用的聊天室
                                    .Where(w => w.Id == cramd.ChatgroupId && w.IsActive)
                                    .FirstAsync();
        //聊天室为空 或者 聊天室id为空 或者 聊天室中的环信id为空 提示异常
        if (cgInfo == null ||
            string.IsNullOrWhiteSpace(cgInfo.Id) ||
            string.IsNullOrWhiteSpace(cgInfo.EaseChatgroupId))
        {
            return (false, "群异常，请刷新后重试！");
        }

        // 判断为管理员才可以添加
        if (!canAdd)
        {
            // 判断是不是管理员 或者超管

            canAdd = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                        .Where(w => w.ChatgroupId == cramd.ChatgroupId && w.MemberId == aInfo.Id && w.IsManager && w.IsActive)
                                        .AnyAsync();
            if (!canAdd)
            {
                canAdd = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                                .Where(w => w.Id == cramd.ChatgroupId && w.OwnId == aInfo.Id && w.IsActive)
                                                .AnyAsync();
            }
        }

        if (!canAdd)
        {
            MessageBox.Show("非管理员或群管理员，不可以添加群成员！");
        }

        var doDate = DateTime.Now;

        //参数：调用环信群添加成员id
        var imInfo = await easeImService.ChatgroupAddUserAsync(cgInfo.EaseChatgroupId, cramd.MemberId);

        // 执行群保存会员操作
        CzimChatgroupMember bcm = new CzimChatgroupMember()
        {
            CDate = doDate,
            JoinDate = doDate,
            UDate = doDate,
            UUser = aInfo.Id,
            CUser = aInfo.Id,
            ChatgroupId = cramd.ChatgroupId,
            IsActive = true,
            EaseChatgroupId = cgInfo.EaseChatgroupId,
            MemberId = cramd.MemberId,
            PullUser = aInfo.Id
        };
        //聊天室app会员信息添加
        var addMcr = await this.Repository.Orm.Insert(bcm).ExecuteAffrowsAsync();
        if (addMcr > 0)
        {
            return (true, "新增成功");
        }

        //如果环信聊天室添加app会员信息成功，聊天室app会员信息添加失败，则要删除当前环信聊天室的数据
        await easeImService.ChatgroupDeleteUserAsync(cgInfo.EaseChatgroupId, cramd.MemberId);

        // 删除本地数据
        var delMcr = this.Repository.Orm.Delete<CzimChatgroupMember>(cramd.MemberId);

        return (false, "环信群会员信息未添加成功，请联系管理员！");
    }

    /// <summary>
    /// 会员详情
    /// </summary>
    /// <returns></returns>
    public async Task<CzimMemberVo> FindMember(MemberSearchDto dto)
    {
        //查询app会员信息
        var data = await this.Repository.Orm.Select<CzimMemberInfo>()
            //根据当前app会员id
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.Id.Equals(dto.MemberId))
            .FirstAsync(s => new CzimMemberVo()
            {
                LoginName = s.LoginName,
                Meid = s.Meid,
                PassWord = s.PassWord,
                BsvNumber = s.BsvNumber,
                NickName = s.NickName,
                Phone = s.Phone,
                Mailbox = s.Mailbox,
                Gender = s.Gender,
                Birthday = s.Birthday,
                Avatar = s.Avatar,
                Introduce = s.Introduce,
                Channel = s.Channel,
                RNumber = s.RNumber,
                RegDate = s.RegDate,
                IsMember = s.IsMember,
                MemberShip = s.MemberShip,
                ExclusiveUserId = s.ExclusiveUserId
            });
        return data;
    }

    /// <summary>
    /// 登录日志
    /// </summary>
    /// <param name="dto">查询条件</param>
    /// <returns></returns>
    public async Task<PageViewModel> FindLoginRecordList(MemberLoginSearchDto dto)
    {
        //查询当前app会员登录信息，关联app会员表
        var query = await this.Repository.Orm.Select<CzimMemberLoginRecord, CzimMemberInfo>()
            //app会员登录信息的app会员id=app会员的id
            .LeftJoin(s => s.t1.MemberId == s.t2.Id)
            //根据app会员id查询
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.t1.MemberId == dto.MemberId)
            .OrderByDescending(o => o.t1.LoginTime)
            .Page(dto.Page, dto.Size)
            .Count(out var total)
            .ToListAsync(s => new
            {
                s.t1.LoginName,
                s.t1.LoginOS,
                s.t1.LoginTime,
                s.t1.LoginModel,
                s.t1.LoginIP,
                s.t2.NickName,
                s.t2.RegDate
            });
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, total);
    }

    /// <summary>
    /// 会员加群组记录
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PageViewModel> MemberJoinChatGroupFindListAsync(MemberJoinGroupSearchDto dto)
    {
        List<CzimChatgroupInfo> allGroup = new();
        //查询当前聊天室app会员信息，关联聊天室信息
        var queryMember = await this.Repository.Orm.Select<CzimChatgroupMember, CzimChatgroupInfo>()
            //聊天室主键id等于聊天室app会员信息的聊天室id
            .LeftJoin(s => s.t2.Id.Equals(s.t1.ChatgroupId))
            //根据传入的app会员id查询
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.t1.MemberId == dto.MemberId)
            .ToListAsync(s => new CzimChatgroupInfo()
            {
                GroupImg = s.t2.GroupImg,
                OwnId = s.t2.OwnId,
                GroupName = s.t2.GroupName,
                Description = s.t2.Description
            }
            );
        allGroup.AddRange(queryMember);
        var queryOwn = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                          .Where(s => s.OwnId == dto.MemberId)
                                          .ToListAsync();

        allGroup.AddRange(queryOwn);

        allGroup = allGroup.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();

        return await this.Repository.AsPageViewModelAsync(allGroup, dto.Page, dto.Size, allGroup.Count);
    }

    /// <summary>
    /// 会员加聊天室记录
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PageViewModel> MemberJoinChatRoomFindListAsync(MemberJoinGroupSearchDto dto)
    {
        //查询当前聊天室app会员信息，关联聊天室信息
        var query = await this.Repository.Orm.Select<CzimChatroomMember, CzimChatroomInfo>()
            //聊天室主键id等于聊天室app会员信息的聊天室id
            .LeftJoin(s => s.t2.Id.Equals(s.t1.ChatRoomId))
            //根据传入的app会员id查询
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.t1.MemberId.Contains(dto.MemberId))
            .Page(dto.Page, dto.Size)
            .Count(out var total)
            .ToListAsync(s => new
            {
                s.t2.ChatRoomImg,
                s.t2.ManagerId,
                s.t2.ChatRoomName,
                s.t2.ChatRoomNotice
            }
            );
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, total);
    }

    /// <summary>
    /// 会员加群记录
    /// </summary>
    /// <param name="dto">查询数据</param>
    /// <returns></returns>
    public async Task<PageViewModel> GroupMemberFindListAsync(MemberJoinGroupSearchDto dto)
    {
        //查询当前群app会员信息，关联群信息
        var query = await this.Repository.Orm.Select<CzimChatgroupMember, CzimChatgroupInfo>()
            .LeftJoin(s => s.t2.Id.Equals(s.t1.ChatgroupId))
            //根据传入的app会员id查询
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.t1.MemberId.Contains(dto.MemberId))
            .Page(dto.Page, dto.Size)
            .Count(out var total)
            .ToListAsync(s => new
            {
                s.t2.GroupImg,
                s.t2.OwnId,
                s.t2.GroupName,
                s.t2.Announcement,
            }
            );
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, total);
    }

    /// <summary>
    /// 会员好友记录
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PageViewModel> MemberfriendFindList(ChatFriendDto dto)
    {
        //查询当前好友列表 关联 聊天室客服
        var query = await this.Repository.Orm.Select<CzimChatFriendList, CzimChatCustomer>()
            //好友列表的app会员id 等于 聊天室客服的会员id
            .LeftJoin(s => s.t1.UserId.Equals(s.t2.UserId))
            //根据app会员id查询数据
            .WhereIf(!string.IsNullOrEmpty(dto.MemberId), s => s.t1.MemberId.Equals(dto.MemberId))
            .Where(s => s.t2.IsActive && s.t1.IsActive)
            .Page(dto.Page, dto.Size)
            .Count(out var total)
            .ToListAsync(s => new
            {
                s.t1.Id,
                avatar = s.t2.Avatar,
                nickName = s.t2.NickName,
                manTime = s.t2.ManTime,
                crtDate = s.t1.CrtDate
            });
        return await this.Repository.AsPageViewModelAsync(query, dto.Page, dto.Size, total);
    }

    /// <summary>
    /// 生成机器人操作
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <param name="mad">账户信息</param>
    /// <returns></returns>
    public async Task<(bool, string)> AddRobotAsync(string userId, MemberAddDto mad)
    {
        var regist = (false, "");
        //新增机器人

        regist = await this.AddMemberAsync("", userId, mad.Gender, "", "", "机器用户");
        if (regist.Item1)
        {
            return (true, "添加机器人成功");
        }
        return (false, "失败,请联系管理员");
    }

    /// <summary>
    /// 会员新增
    /// </summary>
    /// <param name="mid">会员id</param>
    /// <param name="userId">管理员id</param>
    /// <param name="gender">性别</param>
    /// <param name="loginName">登录名</param>
    /// <param name="pwd">密码</param>
    /// <param name="nickName">昵称</param>
    /// <param name="isAgent">是否推广员</param>
    /// <returns>注册是否成功，环信账号id</returns>
    public async Task<(bool, string)> AddMemberAsync(string mid, string userId, int gender, string loginName, string pwd, string nickName, bool isAgent = false)
    {
        var doDate = DateTime.Now;

        // 生成app会员对象
        CzimMemberInfo fm = new CzimMemberInfo()
        {
            Phone = "",
            LoginName = loginName,
            PassWord = pwd.Md5Encrypt(),
            Channel = 4,
            RNumber = "000000",
            RegDate = doDate,
            Meid = Guid.NewGuid().ToString("N"),
            CDate = doDate,
            UDate = doDate,
            IsActive = true,
            MemberShip = 2,
            Gender = gender,
            Introduce = "啥都没填写",
            IsMember = true,
            IsAgent = isAgent,
            NickName = nickName,
            UUser = userId,
            CUser = userId
        };

        if (!string.IsNullOrWhiteSpace(mid))
        {
            //将会员id赋值给app会员对象的主键
            fm.Id = mid;
        }

        //参数:app会员id，app会员昵称
        // 为app会员注册环信账号      --app端注册账号时也需要注册环信账号，为确保数据统一完整
        var easeResult = await easeImService.CreateUsersAsync(fm.Id, fm.NickName);

        //判断环信返回的对象为空 或者
        if (easeResult == null ||
            //环信返回的环信账户对象的长度小于等于0
            easeResult.entities?.Count <= 0 ||
            //环信返回的环信账户对象是不可用的
            !easeResult.entities[0].activated)
        {
            // 参数:app账号的id
            // 执行删除环信账号操作
            await easeImService.DeleteUsersAsync(fm.Id);
            MessageBox.Show("注册失败，无法注册聊天服务器！");
        }

        // 生成环信id
        //将环信返回的uuid赋值给app账号的环信id
        fm.EaseId = easeResult.entities[0].uuid;

        // 生成会员编号，Number
        CzimZnumberSeed fas = new CzimZnumberSeed()
        {
            //将当前app会员id赋值给mid
            MId = fm.Id,
            IsActive = true,
            CDate = DateTime.Now,
            UDate = DateTime.Now,
            CUser = fm.Id,
            UUser = fm.Id
        };

        // 获取会员编号
        //新增一条会员编号，返回最终的执行结果
        var number = await this.Repository.GetNumberSeedAsync(fas);

        if (number > 0)
        {
            //将会员编号 保存到 app会员对象中的会员编号
            fm.BsvNumber = number.ToString();//.PadLeft(6, '0');
            //判断app登录账户是否为空
            if (string.IsNullOrWhiteSpace(loginName))
            {
                //为空，则将app登录名称和密码设置为默认
                fm.LoginName = $"r_{fm.BsvNumber}";
                //密码md5加密
                fm.PassWord = $"r_{fm.BsvNumber}".Md5Encrypt();
            }
            //判断昵称为空
            if (string.IsNullOrWhiteSpace(nickName))
            {
                //则昵称赋值为默认
                fm.NickName = $"临时_{fm.BsvNumber}";
            }
        }

        // 获取专属客服
        var customer = await this.Repository.Orm.Select<CzimChatCustomer>()
                                    //当前状态为可用 并且不是推广员
                                    .Where(w => w.IsActive && !w.IsAgent)
                                    //正序排序根据当前服务人次
                                    .OrderBy(o => o.ManTime)
                                    //返回第一个客服，就是服务人次最少的那个客服
                                    .FirstAsync();
        //若是当前专属客服不为空
        if (customer != null &&
            //当前web客服的userid不为空
            !string.IsNullOrWhiteSpace(customer.UserId))
        {
            //则就将专属客服userid赋值给默认客服id
            fm.ExclusiveUserId = customer.UserId;
        }
        else
        {
            // 固定客服id
            //需要改为配置文件，方便配置
            fm.ExclusiveUserId = "e62b08484a5f4e2194a7023a5504beb4";
        }

        // 新增app会员信息
        var insInfo = await this.Repository.Orm.Insert<CzimMemberInfo>(fm).ExecuteAffrowsAsync();
        //判断app会员信息 执行增加返回结果 大于0
        //若新增app会员信息成功
        if (insInfo > 0)
        {
            // 则更新专属客服服务次数
            var updcustomer = await this.Repository.Orm.Update<CzimChatCustomer>()
                                        //服务人次+1
                                        .Set(s => s.ManTime + 1)
                                        //根据当前的web专属客服id去 查询 客服信息
                                        .Where(w => w.UserId == fm.ExclusiveUserId)
                                        .ExecuteAffrowsAsync();

            // 生成会员资金表
            CzimMemberCapital omc = new CzimMemberCapital()
            {
                MId = fm.Id,
                LastLoginDate = DateTime.Now,
                Capital = 0.0M,
                CapitalLocking = 0.0M,
                //会员失效时间 设备比当前时间少一天，是为确保会员永久不失效，
                //后期要管控app会员失效时间可能要调整
                FrozenTime = DateTime.Now.AddDays(-1),
                IsActive = true,
            };

            // 保存会员扩展表
            var insCapital = await this.Repository.Orm
                                        .Insert<CzimMemberCapital>(omc)
                                        .ExecuteAffrowsAsync();
            //会员扩展表保存成功之后   返回app会员的环信id
            if (insCapital > 0)
            {
                return (true, fm.EaseId);
            }

            //确保数据统一性
            // 删除主表注册记录
            var delInfo = await this.Repository.DeleteAsync(fm.Id);
            // 删除环信id
            await easeImService.DeleteUsersAsync(fm.Id);
            MessageBox.Show("会员注册失败，请联系管理员！ ");
        }

        return (false, "");
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<bool> DeleteListAsync(List<string> ids)
    {
        return default;
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> FindFormAsync(string id)
    {
        var res = new Dictionary<string, object>();

        var form = await this.Repository.FindAsync(id);

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form.NullSafe();
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<SysMenu> SaveFormAsync(SysMenuFormDto form)
    {
        return default;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(MemberSearchDto search)
    {
        search.Page = 1;
        search.Size = int.MaxValue - 1;
        var tableViewModel = await this.FindListAsync(search);
        return this.ExportExcelByPageViewModel(tableViewModel, null, "Id");
    }
}