using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.CzimMember;
using KUX.Models.CzimApp.AppBo;
using KUX.Models.CzimApp.Reponse.AppMember;
using KUX.Models.CzimApp.Request.AppLogin;
using KUX.Models.CzimApp.Request.AppMember;
using KUX.Models.CzimSection;
using KUX.Models.Enums;
using KUX.Repositories.CzimSection;
using KUX.Services.EaseImServices;
using Microsoft.AspNetCore.Http;

namespace KUX.Services.App.CzimApp;

/// <summary>
/// 会员服务
/// </summary>
public class CzimMemberService : AppBaseService<CzimMemberRepository>
{
    /// <summary>
    /// 请求上下文
    /// </summary>
    private readonly HttpContext httpContext;

    /// <summary>
    /// 环信服务
    /// </summary>
    private readonly EaseImService easeImServices;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    /// <param name="httpContextAccessor">http请求上下文</param>
    /// <param name="_easeImServices">环信服务</param>
    public CzimMemberService(CzimMemberRepository repository, IHttpContextAccessor httpContextAccessor,
        EaseImService _easeImServices) : base(repository)
    {
        httpContext = httpContextAccessor.HttpContext;
        easeImServices = _easeImServices;
    }

    /// <summary>
    /// 会员更新密码
    /// </summary>
    /// <param name="ownId">会员id</param>
    /// <param name="upwd">密码信息</param>
    /// <returns></returns>
    public async Task<bool> UpdPasswordAsync(string ownId, UpdPasswordDto upwd)
    {
        var updInfo = await this.Repository.Orm.Update<CzimMemberInfo>()
                                        .Set(s => s.PassWord, upwd.NewPwd.Md5Encrypt())
                                        .Where(w => w.Id == ownId && w.PassWord == upwd.OldPwd.Md5Encrypt())
                                        .ExecuteAffrowsAsync();
        if (updInfo <= 0)
        {
            MessageBox.Show("更新失败，密码错误！");
        }
        return updInfo > 0;
    }

    /// <summary>
    /// 记录会员登录登录信息
    /// </summary>
    /// <param name="ownid">用户帐号</param>
    /// <param name="ltd">登录信息</param>
    /// <returns></returns>
    public async Task<bool> LoginTimeInfoAsync(string ownid, LoginTimeDto ltd)
    {
        CzimMemberLoginRecord oal = new CzimMemberLoginRecord()
        {
            MemberId = ownid,
            LoginName = ltd.MeId,
            LoginTime = DateTime.Now,
            LoginModel = ltd.LoginModel,
            LoginOS = ltd.LoginOS,
            IsActive = true,
            LoginIP = httpContext?.GetClientIp()// httpContext?.Connection?.RemoteIpAddress?.ToString()
        };

        var result = await this.Repository.Orm.Insert<CzimMemberLoginRecord>(oal).ExecuteAffrowsAsync();

        return result > 0;
    }

    // 修改会员个人信息
    /// <summary>
    /// 更新会员基本信息
    /// </summary>
    /// <param name="mbid">会员基本信息</param>
    /// <param name="owner">会员信息</param>
    /// <returns></returns>
    public async Task<(bool, string)> UpdMemberBaseInfoAsync(MemberBaseInfoDto mbid, MemberCacheBo owner)
    {
        var upInfo = await this.Repository.Orm.Update<CzimMemberInfo>()
                                              .SetIf(!string.IsNullOrWhiteSpace(mbid.Mailbox), s => s.Mailbox, mbid.Mailbox)
                                              .SetIf(!string.IsNullOrWhiteSpace(mbid.Avatar), s => s.Avatar, mbid.Avatar)
                                              .SetIf(!string.IsNullOrWhiteSpace(mbid.Introduce), s => s.Introduce, mbid.Introduce)
                                              .SetIf(!string.IsNullOrWhiteSpace(mbid.NickName), s => s.NickName, mbid.NickName)
                                              .SetIf((int)(mbid.Gender) > 0, s => s.Gender, (int)mbid.Gender)
                                              .Where(w => w.Id == owner.Id)
                                              .ExecuteAffrowsAsync();
        if (upInfo > 0)
        {
            if (!string.IsNullOrWhiteSpace(mbid.NickName))
            {
                // 更新环信推送昵称接口
                var ease = await easeImServices.UpdUserNameAsync(owner.Id, mbid.NickName);
            }
            if (!string.IsNullOrWhiteSpace(mbid.NickName) || !string.IsNullOrWhiteSpace(mbid.Avatar))
            {
                // 更新用户属性
                var nickName = string.IsNullOrWhiteSpace(mbid.NickName) ? owner.NickName : mbid.NickName;
                var avatar = string.IsNullOrWhiteSpace(mbid.Avatar) ? owner.NickName : mbid.Avatar;

                var easeUser = await easeImServices.UpdUserMetaAsync(owner.Id, nickName, avatar);
            }

            // 更新缓存
            return (true, "更新成功");
        }

        return (false, "更新失败");
    }

    /// <summary>
    /// 获取专属客服信息
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <returns></returns>
    public async Task<List<AppChatContactsVo>> GetExclusiveUserAsync(string ownId)
    {
        var result = await this.Repository.Orm.Select<CzimMemberInfo, CzimChatCustomer>()
                                    .LeftJoin(l => l.t1.ExclusiveUserId == l.t2.UserId)
                                    .Where(w => w.t1.Id == ownId && w.t2.IsActive)
                                    .ToListAsync(t => new AppChatContactsVo()
                                    {
                                        Avatar = t.t2.Avatar,
                                        NickName = t.t2.NickName,
                                        TargetId = t.t2.UserId
                                    });

        return result;
    }

    /// <summary>
    /// 获取所有联系人(好友)
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <param name="isAgent">是否推广员</param>
    /// <returns></returns>
    public async Task<List<AppChatContactsVo>> GetContactsAsync(string ownId, ContactSearchDto csd, bool isAgent = false)
    {
        // 查询所有好友关系
        var friendList = await this.Repository.Orm.Select<CzimChatFriendList, CzimChatCustomer>()
                                                .LeftJoin(l => l.t1.UserId == l.t2.UserId)
                                                .Where(w => w.t1.MemberId == ownId && w.t1.IsActive && w.t2.IsActive)
                                                .WhereIf(!string.IsNullOrWhiteSpace(csd.NickName), w => w.t2.NickName.Contains(csd.NickName))
                                                .ToListAsync(t => new AppChatContactsVo()
                                                {
                                                    Avatar = t.t2.Avatar,
                                                    NickName = t.t2.NickName,
                                                    TargetId = t.t2.UserId,
                                                    CzType = 0,
                                                });

        if (isAgent)
        {
            var af = await this.Repository.Orm.Select<CzimChatFriendList, CzimMemberInfo>()
                                                .LeftJoin(l => l.t1.MemberId == l.t2.Id)
                                                .Where(w => w.t1.UserId == ownId && w.t1.IsActive && w.t2.IsActive)
                                                .WhereIf(!string.IsNullOrWhiteSpace(csd.NickName), w => w.t2.NickName.Contains(csd.NickName))
                                                .ToListAsync(t => new AppChatContactsVo()
                                                {
                                                    Avatar = t.t2.Avatar,
                                                    NickName = t.t2.NickName,
                                                    TargetId = t.t2.Id,
                                                    CzType = 0,
                                                });

            if (af?.Count > 0)
            {
                friendList.AddRange(af);
            }
        }

        return friendList.Distinct().ToList();
    }

    /// <summary>
    /// 查询联系人信息
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <param name="isAgent">是否推广员</param>
    /// <returns></returns>
    public async Task<List<AppSearchContactsVo>> SearchContactsAsync(string ownId, VagueSearchDto csd, bool isAgent = false)
    {
        // 查询条件为null，返回默认值
        if (csd == null || string.IsNullOrWhiteSpace(csd.VagueInfo))
        {
            return default;
        }

        // 如果当前用户不为推广员，查询所有联系人信息
        if (!isAgent)
        {
            // 如果普通用户，只能搜索到推广员
            var agentInfo = await this.Repository.Orm.Select<CzimMemberInfo, CzimChatFriendList>()
                                                    .LeftJoin(l => l.t1.Id == l.t2.UserId && l.t2.MemberId == ownId)
                                                    .Where(w => w.t1.IsActive && w.t1.IsAgent)
                                                    .WhereIf(!string.IsNullOrWhiteSpace(csd.VagueInfo),
                                                                    w => (w.t1.BsvNumber.Contains(csd.VagueInfo) ||
                                                                       w.t1.LoginName.Contains(csd.VagueInfo) ||
                                                                       w.t1.BsvNumber.Contains(csd.VagueInfo))
                                                             )
                                                    .ToListAsync(t => new AppSearchContactsVo()
                                                    {
                                                        Avatar = t.t1.Avatar,
                                                        NickName = t.t1.NickName,
                                                        TargetId = t.t1.Id,
                                                        IsFriend = (t.t2 != null && t.t2.IsActive)
                                                    });

            return agentInfo;
        }
        else
        {
            // 查询所有的memberInfo数据
            var af = await this.Repository.Orm.Select<CzimMemberInfo, CzimChatFriendList>()
                                                    .LeftJoin(l => l.t1.Id == l.t2.MemberId && l.t2.UserId == ownId)
                                                    .Where(w => w.t1.IsActive)
                                                    .WhereIf(!string.IsNullOrWhiteSpace(csd.VagueInfo),
                                                            w => (w.t1.BsvNumber.Contains(csd.VagueInfo) ||
                                                               w.t1.LoginName.Contains(csd.VagueInfo) ||
                                                               w.t1.BsvNumber.Contains(csd.VagueInfo))
                                                     )
                                                    .ToListAsync(t => new AppSearchContactsVo()
                                                    {
                                                        Avatar = t.t1.Avatar,
                                                        NickName = t.t1.NickName,
                                                        TargetId = t.t1.Id,
                                                        IsFriend = (t.t2 != null && t.t2.IsActive)
                                                    });
            return af;
        }
    }

    /// <summary>
    /// 通过id查询联系人信息
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <param name="isAgent">是否推广员</param>
    /// <returns></returns>
    public async Task<AppSearchByIdVo> GetByIdAsync(string ownId, SearchByIdDto csd, bool isAgent = false)
    {
        // 查询条件为null，返回默认值
        if (csd == null ||
            string.IsNullOrWhiteSpace(csd.MemberId))
        {
            return default;
        }

        // 如果当前用户不为推广员，查询所有联系人信息
        if (!isAgent)
        {
            // 如果普通用户，只能搜索到推广员
            var agentInfo = await this.Repository.Orm.Select<CzimMemberInfo, CzimChatFriendList>()
                                            .LeftJoin(l => l.t1.Id == l.t2.UserId && l.t2.MemberId == ownId)
                                            .Where(w => w.t1.IsActive && w.t1.IsAgent && w.t1.Id == csd.MemberId)
                                            .FirstAsync(t => new AppSearchByIdVo()
                                            {
                                                Avatar = t.t1.Avatar,
                                                NickName = t.t1.NickName,
                                                TargetId = t.t1.Id,
                                                IsFriend = (t.t2 != null && t.t2.IsActive),
                                                Introduce = t.t1.Introduce,
                                                Gender = (GenderEnum)t.t1.Gender
                                            });

            return agentInfo;
        }
        else
        {
            // 查询所有的memberInfo数据
            var af = await this.Repository.Orm.Select<CzimMemberInfo, CzimChatFriendList>()
                                                    .LeftJoin(l => l.t1.Id == l.t2.MemberId && l.t2.UserId == ownId)
                                                    .Where(w => w.t1.IsActive && w.t1.Id == csd.MemberId)
                                                    .FirstAsync(t => new AppSearchByIdVo()
                                                    {
                                                        Avatar = t.t1.Avatar,
                                                        NickName = t.t1.NickName,
                                                        TargetId = t.t1.Id,
                                                        IsFriend = (t.t2 != null && t.t2.IsActive),
                                                        Introduce = t.t1.Introduce,
                                                        Gender = (GenderEnum)t.t1.Gender
                                                    });
            return af;
        }
    }

    /// <summary>
    /// 添加好友
    /// </summary>
    /// <param name="ownid">当前用户id</param>
    /// <param name="afd">添加好友信息</param>
    /// <param name="isAgent">是否推广员</param>
    /// <returns></returns>
    public async Task<bool> AddFriendAsync(string ownid, AddFriendDto afd, bool isAgent)
    {
        if (isAgent)
        {
            // 如果是推广员，可以加任何人为好友
            var hasFriend = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                .Where(w => w.UserId == ownid && w.MemberId == afd.TargetId && w.IsActive)
                                                .AnyAsync();
            if (hasFriend)
            {
                MessageBox.Show("已存在好友关系！");
            }

            // 添加好友关系
            CzimChatFriendList ccfl = new CzimChatFriendList()
            {
                MemberId = afd.TargetId,
                UserId = ownid,
                IsActive = true,
                CrtDate = DateTime.Now,
                CUser = ownid,
                UUser = ownid,
            };
            var result = await this.Repository.Orm.Insert<CzimChatFriendList>(ccfl).ExecuteAffrowsAsync();

            return result > 0;
        }
        else
        {
            // 判断目标是否是推广员
            var agent = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(w => w.UserId == afd.TargetId && w.IsActive)
                                        .AnyAsync();
            if (!agent)
            {
                MessageBox.Show("未知帐号，不可以添加好友！");
            }

            // 判断是否存在好友关系
            var hasFriend = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                .Where(w => w.MemberId == ownid && w.UserId == afd.TargetId && w.IsActive)
                                                .AnyAsync();
            if (hasFriend)
            {
                MessageBox.Show("已存在好友关系！");
            }

            // 添加好友关系
            CzimChatFriendList ccfl = new CzimChatFriendList()
            {
                MemberId = ownid,
                UserId = afd.TargetId,
                IsActive = true,
                CrtDate = DateTime.Now,
            };
            var result = await this.Repository.Orm.Insert<CzimChatFriendList>(ccfl).ExecuteAffrowsAsync();

            return result > 0;
        }
    }

    /// <summary>
    /// 拉好友入聊天室
    /// </summary>
    /// <param name="ownid">当前用户id</param>
    /// <param name="afd">添加好友信息</param>
    /// <returns></returns>
    public async Task<bool> AddChatRoomAsync(string ownid, AddChatRoomDto afd)
    {
        // 当前使用环信id，app无法获取当前id
        var crInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                    .Where(w => w.EaseChatRoomId == afd.ChatRoomId && w.IsActive)
                                    .FirstAsync();

        if (crInfo == null || !crInfo.IsActive)
        {
            MessageBox.Show("当前聊天室异常，不可以添加成员！");
        }

        // 管理员才可以拉人进聊天室
        var isManager = await this.Repository.Orm.Select<CzimChatroomManager>()
                                    .Where(w => w.ManagerId == ownid && w.IsActive && w.ChatRoomId == crInfo.Id)
                                    .AnyAsync();
        if (!isManager)
        {
            MessageBox.Show("需要管理员才可以添加好友！");
        }

        List<string> _memberIds = new List<string>();
        if (afd.TargetIds?.Count > 0)
        {
            _memberIds.AddRange(afd.TargetIds);
        }
        if (!string.IsNullOrWhiteSpace(afd.TargetId))
        {
            _memberIds.Add(afd.TargetId);
        }
        _memberIds = _memberIds.Distinct().ToList();

        if (_memberIds.Count > 60)
        {
            MessageBox.Show("一次最多添加60人！");
        }

        // 判断是否存在群成员
        var hasJoin = await this.Repository.Orm.Select<CzimChatroomMember>()
                                    .Where(w => w.ChatRoomId == crInfo.Id && w.IsActive && _memberIds.Contains(w.MemberId))
                                    .AnyAsync();
        if (hasJoin)
        {
            return true;
        }

        List<CzimChatroomMember> ccms = new List<CzimChatroomMember>();

        if (_memberIds?.Count > 0)
        {
            _memberIds.ForEach(w => ccms.Add(

                new CzimChatroomMember()
                {
                    MemberId = w,
                    ChatRoomId = crInfo.Id,
                    IsActive = true,
                    JoinDate = DateTime.Now,
                    ForbiddenTime = DateTime.Now.AddDays(-1),
                    PullUser = ownid,
                    CUser = ownid,
                    UUser = ownid
                }

                ));
        }
        var result = await this.Repository.Orm.Insert<CzimChatroomMember>(ccms).ExecuteAffrowsAsync();

        if (result > 0)
        {
            // 添加环信群列表
            var imInfo = await easeImServices.ChatRoomBatchAddUsersAsync(_memberIds, crInfo.EaseChatRoomId);
            if (imInfo > 0)
            {
                return true;
            }

            await this.Repository.Orm.Delete<CzimChatroomMember>(ccms).ExecuteAffrowsAsync();
            MessageBox.Show("会员添加失败！，可能是聊天服务器异常，请稍后再试！");
        }
        return false;
    }

    /// <summary>
    /// 获取所有聊天室列表
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <returns></returns>
    public async Task<List<AppChatRoomVo>> GetChatRoomAsync(string ownId)
    {
        // 查询所有聊天室列表
        var chatRoomList = await this.Repository.Orm.Select<CzimChatroomMember, CzimChatroomInfo>()
                                                .LeftJoin(l => l.t1.ChatRoomId == l.t2.Id)
                                                .Where(w => w.t1.MemberId == ownId && w.t1.IsActive && w.t2.IsActive)
                                                .ToListAsync(t => new AppChatRoomVo()
                                                {
                                                    Avatar = t.t2.ChatRoomImg,
                                                    NickName = t.t2.ChatRoomName,
                                                    TargetId = t.t2.EaseChatRoomId,
                                                    ChatRoomId = t.t2.Id,
                                                    ChatRoomNotice = t.t2.ChatRoomNotice
                                                });

        var chatRoomManager = await this.Repository.Orm.Select<CzimChatroomManager, CzimChatroomInfo>()
                                                    .LeftJoin(l => l.t1.ChatRoomId == l.t2.Id)
                                                    .Where(w => w.t1.ManagerId == ownId && w.t1.IsActive && w.t2.IsActive)
                                                    .ToListAsync(t => new AppChatRoomVo()
                                                    {
                                                        Avatar = t.t2.ChatRoomImg,
                                                        NickName = t.t2.ChatRoomName,
                                                        TargetId = t.t2.EaseChatRoomId,
                                                        ChatRoomId = t.t2.Id,
                                                        ChatRoomNotice = t.t2.ChatRoomNotice
                                                    });

        List<AppChatRoomVo> _chatRooms = new List<AppChatRoomVo>();
        if (chatRoomList?.Count > 0)
        {
            _chatRooms.AddRange(chatRoomList);
        }
        if (chatRoomManager?.Count > 0)
        {
            _chatRooms.AddRange(chatRoomManager);
        }
        return _chatRooms.Distinct().ToList();
    }

    /// <summary>
    /// 会员使用推荐码
    /// </summary>
    /// <param name="ownId">会员id</param>
    /// <param name="murd">推荐码信息</param>
    /// <returns></returns>
    public async Task<(bool, string)> UseReferralAsync(string ownId, MemberUseReferralDto murd)
    {
        //通过推荐码查询会员
        //var rInfo = await this.Repository.Orm.Select<CzimMemberInfo, CzimMemberCapital>()
        //                            .LeftJoin(l => l.t1.Id == l.t2.MId)
        //                            .Where(w => w.t1.Id == murd.ReferralCode)
        //                            .Where(w => w.t1.IsActive && w.t2.IsActive)
        //                            .FirstAsync(f => f.t2);

        var rInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                            .Where(w => w.BsvNumber == murd.ReferralCode && w.IsAgent && w.IsActive)
                            .FirstAsync();

        if (rInfo == null || string.IsNullOrWhiteSpace(rInfo.BsvNumber))
        {
            MessageBox.Show("推荐码失效，或推荐人异常");
            return (false, "推荐码失效，或推荐人异常！");
        }

        // 更新推荐码
        var updMember = await this.Repository.Orm.Update<CzimMemberInfo>()
                                    .Set(s => s.RNumber, murd.ReferralCode)
                                    .Set(s => s.UDate, DateTime.Now)
                                    .Where(w => w.Id == ownId)
                                    .ExecuteAffrowsAsync();

        if (updMember > 0)
        {
            // 判断是否有好友关系，没有好友关系，则添加好友关系

            var hasFriend = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                .Where(w => w.MemberId == ownId && w.UserId == rInfo.Id)
                                                .AnyAsync();
            if (!hasFriend)
            {
                CzimChatFriendList ccfl = new CzimChatFriendList()
                {
                    MemberId = ownId,
                    CDate = DateTime.Now,
                    CrtDate = DateTime.Now,
                    UDate = DateTime.Now,
                    IsActive = true,
                    UserId = rInfo.Id,
                };

                await this.Repository.Orm.Insert<CzimChatFriendList>(ccfl).ExecuteAffrowsAsync();
            }

            return (true, "使用成功！");
        }
        MessageBox.Show("请勿重复绑定推荐码");

        return (false, "");
    }

    /// <summary>
    /// 建议收集
    /// </summary>
    /// <param name="ownid">用户id</param>
    /// <param name="fbd">建议内容</param>
    /// <returns></returns>
    public async Task<bool> FeedBackAsync(string ownid, FeedBackDto fbd)
    {
        CzimFeedBack cfb = new CzimFeedBack()
        {
            MemberId = ownid,
            CDate = DateTime.Now,
            UDate = DateTime.Now,
            Content = fbd.Content,
            IsActive = true,
            IsHandle = false
        };

        var result = await this.Repository.Orm.Insert<CzimFeedBack>(cfb).ExecuteAffrowsAsync();

        return result > 0;
    }

    /// <summary>
    /// 获取所有群列表
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <returns></returns>
    public async Task<List<AppChatgroupVo>> GetChatgroupAsync(string ownId)
    {
        var chatgroup = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(w => w.OwnId == ownId && w.IsActive)
                                            .ToListAsync(t => new AppChatgroupVo()
                                            {
                                                Avatar = t.GroupImg,
                                                GroupName = t.GroupName,
                                                TargetId = t.EaseChatgroupId,
                                                ChatgroupId = t.Id,
                                                Announcement = t.Announcement
                                            });

        // 查询所有群列表
        var chatgroupList = await this.Repository.Orm.Select<CzimChatgroupMember, CzimChatgroupInfo>()
                                                    .LeftJoin(l => l.t1.ChatgroupId == l.t2.Id)
                                                    .Where(w => w.t1.MemberId == ownId && w.t1.IsActive && w.t2.IsActive)
                                                    .ToListAsync(t => new AppChatgroupVo()
                                                    {
                                                        Avatar = t.t2.GroupImg,
                                                        GroupName = t.t2.GroupName,
                                                        TargetId = t.t2.EaseChatgroupId,
                                                        ChatgroupId = t.t2.Id,
                                                        Announcement = t.t2.Announcement
                                                    });
        List<AppChatgroupVo> acv = new List<AppChatgroupVo>();
        if (chatgroup?.Count > 0)
        {
            acv.AddRange(chatgroup);
        }
        if (chatgroupList?.Count > 0)
        {
            acv.AddRange(chatgroupList);
        }
        return acv.DistinctBy(d => d.TargetId).ToList();
    }

    /// <summary>
    /// 拉好友入群
    /// </summary>
    /// <param name="ownid">当前用户id</param>
    /// <param name="afd">添加好友信息</param>
    /// <returns></returns>
    public async Task<bool> PullGroupAsync(string ownid, PullGroupDto afd)
    {
        // 当前使用环信id，app无法获取当前id
        var crInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                    .Where(w => (w.EaseChatgroupId == afd.ChatgroupId || w.Id == afd.ChatgroupId))
                                    .Where(w => w.IsActive)
                                    .FirstAsync();

        if (crInfo == null || !crInfo.IsActive)
        {
            MessageBox.Show("当前群不可以添加成员！");
        }

        // 管理员才可以拉人进聊天室
        var isManager = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    .Where(w => w.MemberId == ownid && w.IsActive && w.ChatgroupId == crInfo.Id)
                                    .AnyAsync();

        if (!isManager)
        {
            //判断是否是群主
            isManager = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                             .Where(s => s.OwnId == ownid && s.IsActive && s.Id == crInfo.Id)
                                             .AnyAsync();
        }


        if (!isManager)
        {
            MessageBox.Show("需要群管理才可以添加成员！");
        }

        List<string> _memberIds = new List<string>();
        if (afd.MemberIds?.Count > 0)
        {
            _memberIds.AddRange(afd.MemberIds);
        }
        _memberIds = _memberIds.Distinct().ToList();

        if (_memberIds.Count > 60)
        {
            MessageBox.Show("一次最多添加60人！");
        }

        // 查询群成员
        var hasMembers = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                        .Where(w => w.ChatgroupId == crInfo.Id && _memberIds.Contains(w.MemberId))
                                        .ToListAsync(t => t.MemberId);

        if (hasMembers?.Count > 0)
        {
            // 移除已有成员
            foreach (var hm in hasMembers)
            {
                _memberIds.Remove(hm);
            }
        }

        List<CzimChatgroupMember> ccms = new List<CzimChatgroupMember>();

        if (_memberIds?.Count > 0)
        {
            _memberIds.ForEach(w => ccms.Add(
                new CzimChatgroupMember()
                {
                    MemberId = w,
                    ChatgroupId = crInfo.Id,
                    EaseChatgroupId = crInfo.EaseChatgroupId,
                    IsActive = true,
                    JoinDate = DateTime.Now,
                    ForbiddenTime = DateTime.Now.AddDays(-1),
                    PullUser = ownid,
                    IsManager = false,
                    CUser = ownid,
                    UUser = ownid
                }
                ));
        }
        var result = await this.Repository.Orm.Insert<CzimChatgroupMember>(ccms).ExecuteAffrowsAsync();

        if (result > 0)
        {
            // 添加环信群列表
            var imInfo = await easeImServices.ChatgroupAddUserListAsync(crInfo.EaseChatgroupId, _memberIds);
            if (imInfo?.Count > 0)
            {
                return true;
            }

            await this.Repository.Orm.Delete<CzimChatgroupMember>(ccms).ExecuteAffrowsAsync();
            MessageBox.Show("会员添加失败！，可能是环信聊天异常，请稍后再试！");
        }
        return false;
    }

    /// <summary>
    /// 移除群成员
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <param name="pgd">移除信息</param>
    /// <returns></returns>
    public async Task<bool> DeleteGroupUserAsync(string ownId, DeleteGroupUsersDto pgd)
    {
        // 当前使用环信id，app无法获取当前id
        var crInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                    .Where(w => w.EaseChatgroupId == pgd.ChatgroupId || w.Id == pgd.ChatgroupId)
                                    .FirstAsync();

        if (crInfo == null)
        {
            MessageBox.Show("当前群异常，不可以操作！");
        }

        // 管理员才可以踢人
        var isManager = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    .Where(w => w.MemberId == ownId && w.IsActive && w.ChatgroupId == crInfo.Id)
                                    .AnyAsync();
        if (!isManager)
        {
            MessageBox.Show("需要管理员才可以移除成员！");
        }

        List<string> _memberIds = new List<string>();
        if (pgd.MemberIds?.Count > 0)
        {
            _memberIds.AddRange(pgd.MemberIds);
        }
        _memberIds = _memberIds.Distinct().ToList();

        if (_memberIds.Count > 60)
        {
            MessageBox.Show("一次最多移除60人！");
        }

        var result = await this.easeImServices.ChatgroupDeleteUserListAsync(crInfo.EaseChatgroupId, _memberIds);

        if (result?.Count > 0)
        {
            await this.Repository.Orm.Delete<CzimChatgroupMember>().Where(w => result.Contains(w.MemberId) &&
                                                                    w.ChatgroupId == crInfo.Id &&
                                                                    w.EaseChatgroupId == crInfo.EaseChatgroupId)
                                                                    .ExecuteAffrowsAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 扫码加好友\群\聊天室
    /// </summary>
    /// <param name="ownId">会员信息</param>
    /// <param name="pgd">扫码信息</param>
    /// <returns></returns>
    public async Task<bool> ScanQrCodeAsync(string ownId, ScanQrCodeDto pgd)
    {
        var doResult = false;
        switch (pgd.CzType)
        {
            case CzTypeEnum.SingleChat:
                // 单聊加好友
                doResult = await ScanAddFriendAsync(ownId, pgd.QrCode);
                break;

            case CzTypeEnum.GroupChat:
                // 加入群组
                doResult = await ScanAddGroupAsync(ownId, pgd.QrCode);
                break;

            case CzTypeEnum.ChatRoom:
                // 加入聊天室
                doResult = true;
                break;

            default:
                break;
        }

        return doResult;
    }

    /// <summary>
    /// 扫码加好友
    /// </summary>
    /// <param name="ownId">用户信息</param>
    /// <param name="qrcode">码串（个人码）</param>
    /// <returns></returns>
    private async Task<bool> ScanAddFriendAsync(string ownId, string qrcode)
    {
        var _fid = "";
        // 查询码串信息
        var _mInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                        .Where(w => w.Id == qrcode || w.BsvNumber == qrcode)
                                        .FirstAsync();

        if (_mInfo != null && !string.IsNullOrWhiteSpace(_mInfo.EaseId))
        {
            _fid = _mInfo.Id;
        }

        if (string.IsNullOrWhiteSpace(_fid))
        {
            var _userInfo = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(w => w.UserId == qrcode)
                                        .FirstAsync();

            if (_userInfo != null && !string.IsNullOrWhiteSpace(_userInfo.EaseChatCustomerId))
            {
                _fid = _userInfo.UserId;
            }
        }

        if (string.IsNullOrWhiteSpace(_fid))
        {
            MessageBox.Show("二维码错误，无法添加好友");
        }

        // 判断好友是否存在
        var _hasF = await this.Repository.Orm.Select<CzimChatFriendList>()
                                        .Where(w => (w.MemberId == ownId && w.UserId == qrcode) ||
                                                        (w.UserId == ownId && w.MemberId == qrcode))
                                        .AnyAsync();
        if (_hasF)
        {
            return true;
        }

        // 执行好友保存
        // 判断客服账号才可以加好友
        var _isCustomer = await this.Repository.Orm.Select<CzimChatCustomer>()
                            .Where(w => w.UserId == ownId)
                            .AnyAsync();
        // qr是客服
        var _isCustomerTwo = await this.Repository.Orm.Select<CzimChatCustomer>()
                               .Where(w => w.UserId == qrcode)
                               .AnyAsync();

        if (!_isCustomer && !_isCustomerTwo)
        {
            MessageBox.Show("此二维码无法加好友！");
        }
        CzimChatFriendList czimChatFriendList = new CzimChatFriendList()
        {
            CDate = DateTime.Now,
            UDate = DateTime.Now,
            CrtDate = DateTime.Now,
            IsActive = true,
        };

        if (_isCustomer)
        {
            czimChatFriendList.MemberId = qrcode;
            czimChatFriendList.UserId = ownId;
        }
        else if (_isCustomerTwo)
        {
            czimChatFriendList.MemberId = ownId;
            czimChatFriendList.UserId = qrcode;
        }

        var result = await this.Repository.Orm.Insert<CzimChatFriendList>(czimChatFriendList).ExecuteAffrowsAsync();

        return result > 0;
    }

    /// <summary>
    /// 扫码加群
    /// </summary>
    /// <param name="ownId">用户id</param>
    /// <param name="qrcode">群id</param>
    /// <returns></returns>
    private async Task<bool> ScanAddGroupAsync(string ownId, string qrcode)
    {
        var _groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                    .Where(w => w.Id == qrcode || w.EaseChatgroupId == qrcode)
                                    .FirstAsync();

        if (_groupInfo == null || string.IsNullOrWhiteSpace(_groupInfo.EaseChatgroupId))
        {
            MessageBox.Show("二维码扫码无效！");
        }

        // 判断是否是成员
        var _hasGroup = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                            .Where(w => w.MemberId == ownId && w.ChatgroupId == _groupInfo.Id)
                                            .AnyAsync();

        if (_hasGroup)
        {
            return true;
        }

        // 调用环信接口，添加群组
        var _easeJoin = await this.easeImServices.ChatgroupAddUserAsync(_groupInfo.EaseChatgroupId, ownId);
        if (_easeJoin)
        {

            // 加入群
            CzimChatgroupMember ccm = new CzimChatgroupMember()
            {
                CDate = DateTime.Now,
                JoinDate = DateTime.Now,
                UDate = DateTime.Now,
                ChatgroupId = _groupInfo.Id,
                EaseChatgroupId = _groupInfo.EaseChatgroupId,
                IsActive = true,
                MemberId = ownId,
            };
            var joinResult = await this.Repository.Orm.Insert<CzimChatgroupMember>(ccm).ExecuteAffrowsAsync();
            return joinResult > 0;
        }
        MessageBox.Show("聊天服务器异常，请稍后再试！");
        return false;
    }

}