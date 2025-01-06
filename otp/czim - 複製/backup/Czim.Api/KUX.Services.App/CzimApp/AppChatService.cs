using KUX.Models.ApiResultManage;
using KUX.Models.CzimApp.Reponse.AppMember;
using KUX.Models.CzimApp.Request.AppChat;
using KUX.Models.CzimSection;
using KUX.Repositories.CzimSection;
using KUX.Services.EaseImServices;
using KUX.Repositories.Core.BaseModels;
using KUX.Models.CzimApp.Reponse.AppChat;
using KUX.Models.CzimApp.AppBo;
using KUX.Models.Enums;
namespace KUX.Services.App.CzimApp;

/// <summary>
/// App 聊天相关服务
/// </summary>
public class AppChatService : AppBaseService<CzimChatRepository>
{
    /// <summary>
    /// 环信服务
    /// </summary>
    private readonly EaseImService easeImService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    /// <param name="_easeImService">环信服务</param>
    /// <returns></returns>
    public AppChatService(CzimChatRepository repository, EaseImService _easeImService) : base(repository)
    {
        easeImService = _easeImService;
    }

    /// <summary>
    /// 查询所有用户的头像和昵称
    /// </summary>
    /// <param name="ownid">当前用户id</param>
    /// <param name="targetIds">目标id</param>
    /// <returns></returns>
    public async Task<List<AppChatContactsVo>> GetUserExtAsync(string ownid, List<string> targetIds)
    {
        // 所有会员信息
        var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo>()
                                        .Where(w => targetIds.Contains(w.Id))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.Id,
                                            NickName = t.NickName,
                                            Avatar = t.Avatar,
                                            CzType = 0,
                                        });
        // 所有客服信息
        var userInfo = await this.Repository.Orm.Select<CzimChatCustomer>()
                                        .Where(w => targetIds.Contains(w.UserId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.UserId,
                                            NickName = t.NickName,
                                            Avatar = t.Avatar,
                                            CzType = 0,
                                        });
        var targetList = new List<AppChatContactsVo>();
        targetList.AddRange(memberInfo);
        targetList.AddRange(userInfo);

        return targetList.DistinctBy(d => d.TargetId).ToList();
    }

    /// <summary>
    /// 查询所有聊天室的背景和说明
    /// </summary>
    /// <param name="ownid">当前用户id</param>
    /// <param name="targetIds">目标id</param>
    /// <returns></returns>
    public async Task<List<AppChatContactsVo>> GetChatRoomExtAsync(string ownid, List<string> targetIds)
    {
        // 所有聊天室信息
        var chatroomInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                        .Where(w => targetIds.Contains(w.EaseChatRoomId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.EaseChatRoomId,
                                            NickName = t.ChatRoomName,
                                            Avatar = t.ChatRoomImg,
                                            CzType = 2,
                                        });

        // 所有群信息
        var groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                        .Where(w => targetIds.Contains(w.EaseChatgroupId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.EaseChatgroupId,
                                            NickName = t.GroupName,
                                            Avatar = t.GroupImg,
                                            CzType = 1
                                        });

        List<AppChatContactsVo> accvList = new List<AppChatContactsVo>();
        if (chatroomInfo?.Count > 0)
        {
            accvList.AddRange(chatroomInfo);

        }
        if (groupInfo?.Count > 0)
        {
            accvList.AddRange(groupInfo);
        }
        return accvList.DistinctBy(d => d.TargetId).ToList();
    }

    /// <summary>
    /// 撤回单聊消息
    /// </summary>
    /// <param name="ai">管理员信息</param>
    /// <param name="cmd">聊天消息</param>
    /// <returns></returns>
    public async Task<(bool, string)> MsgRecallAsync(string ownId, RecallmessageDto cmd)
    {
        var doDate = DateTime.Now;

        // 调用环信消息撤回接口
        var result = await this.easeImService.MessagesRecallAsync(cmd.MsgId, cmd.ReceiveId, "chat");

        if (result)
        {
            return (true, "");
        }

        return (false, "撤回异常！");
    }

    /// <summary>
    /// 管理员撤回群聊消息
    /// </summary>
    /// <param name="ai">管理员信息</param>
    /// <param name="cmd">聊天消息</param>
    /// <returns></returns>
    public async Task<(bool, string)> MsgRecallChanAsync(string ownId, RecallmessageDto cmd)
    {
        var doDate = DateTime.Now;

        // 调用环信消息撤回接口
        var result = await this.easeImService.MessagesRecallAsync(cmd.MsgId, cmd.ReceiveId, "groupchat");

        if (result)
        {
            return (true, "");
        }

        return (false, "撤回异常！");
    }

    /// <summary>
    /// 查询所有群的图片和说明
    /// </summary>
    /// <param name="targetIds">目标id</param>
    /// <returns></returns>
    public async Task<List<AppChatContactsVo>> GetChatGroupExtAsync(List<string> targetIds)
    {
        // 所有聊天室信息
        var chatroomInfo = await this.Repository.Orm.Select<CzimChatroomInfo>()
                                        .Where(w => targetIds.Contains(w.EaseChatRoomId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.EaseChatRoomId,
                                            NickName = t.ChatRoomName,
                                            Avatar = t.ChatRoomImg,
                                            CzType = 2,
                                        });

        // 所有群信息
        var groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                        .Where(w => targetIds.Contains(w.EaseChatgroupId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.EaseChatgroupId,
                                            NickName = t.GroupName,
                                            Avatar = t.GroupImg,
                                            CzType = 1
                                        });

        List<AppChatContactsVo> accvList = new List<AppChatContactsVo>();
        if (chatroomInfo?.Count > 0)
        {
            accvList.AddRange(chatroomInfo);

        }
        if (groupInfo?.Count > 0)
        {
            accvList.AddRange(groupInfo);
        }
        return accvList.DistinctBy(d => d.TargetId).ToList();
    }

    /// <summary>
    /// 获取群信息
    /// </summary>
    /// <param name="cgId">群id，或者环信id</param>
    /// <returns></returns>    
    public async Task<ChatgroupInfoVo> GetChatgroupInfoAsync(string cgId)
    {
        // 群信息
        var groupInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                            .Where(w => w.Id == cgId || w.EaseChatgroupId == cgId)
                                            .FirstAsync();
        if (groupInfo == null || string.IsNullOrWhiteSpace(groupInfo.EaseChatgroupId))
        {
            MessageBox.Show("群信息出错，请稍后重试！");
        }

        var groupManager = await this.Repository.Orm.Select<CzimChatgroupMember, CzimMemberInfo, CzimChatCustomer>()
                                        .LeftJoin(l => l.t1.MemberId == l.t2.Id && l.t2.IsActive)
                                        .LeftJoin(l => l.t1.MemberId == l.t3.UserId && l.t3.IsActive)
                                        .Where(w => groupInfo.Id.Equals(w.t1.ChatgroupId) && w.t1.IsManager)
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.t2 == null ? t.t3.EaseChatCustomerId : t.t2.EaseId,
                                            NickName = t.t2 == null ? t.t3.NickName : t.t2.NickName,
                                            Avatar = t.t2 == null ? t.t3.Avatar : t.t2.Avatar,
                                            CzType = 0
                                        });
        var count = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                    .Where(w => w.ChatgroupId == groupInfo.Id && w.IsActive)
                                    .CountAsync();

        ChatgroupInfoVo civ = new ChatgroupInfoVo()
        {
            Count = (int)count,
            GroupImg = groupInfo.GroupImg,
            Description = groupInfo.Description,
            Announcement = groupInfo.Announcement,
            EaseChatgroupId = groupInfo.EaseChatgroupId,
            CrtDate = groupInfo.CrtDate,
            GroupName = groupInfo.GroupName,
            IsProhibit = groupInfo.IsProhibit,
            IsPublic = groupInfo.IsPublic,
            Managers = groupManager

        };

        return civ;
    }

    /// <summary>
    /// 获取群组所有成员
    /// </summary>
    /// <param name="targetId">群id/环信id</param>
    /// <returns></returns>
    public async Task<ApiViewCountModel<AppChatContactsVo>> GetChatGroupMemberAsync(string targetId)
    {
        // 群所有人员信息
        var groupMember = await this.Repository.Orm.Select<CzimChatgroupMember, CzimMemberInfo, CzimChatCustomer>()
                                        .LeftJoin(l => l.t1.MemberId == l.t2.Id && l.t2.IsActive)
                                        .LeftJoin(l => l.t1.MemberId == l.t3.UserId && l.t3.IsActive)
                                        .Where(w => targetId.Equals(w.t1.EaseChatgroupId) || targetId.Equals(w.t1.ChatgroupId))
                                        .ToListAsync(t => new AppChatContactsVo
                                        {
                                            TargetId = t.t2 == null ? t.t3.EaseChatCustomerId : t.t2.EaseId,
                                            NickName = t.t2 == null ? t.t3.NickName : t.t2.NickName,
                                            Avatar = t.t2 == null ? t.t3.Avatar : t.t2.Avatar,
                                            CzType = 0
                                        });
        return await this.Repository.AsAppDataModelAsync<AppChatContactsVo>(groupMember);

        // return groupMember.Distinct().ToList();
    }

    /// <summary>
    /// 查询会员信息
    /// </summary>
    /// <param name="mcb">会员缓存信息</param>
    /// <param name="targetId">目标id</param>
    /// <returns></returns>
    public async Task<AppMemberInfo> GetMemberInfoAsync(MemberCacheBo mcb, string targetId)
    {
        var memberInfo = await this.Repository.Orm.Select<CzimMemberInfo, CzimMemberCapital>()
                                            .LeftJoin(l => l.t1.Id == l.t2.MId)
                                            .Where(w => w.t1.Id == targetId)
                                            .Where(w => w.t1.IsActive && w.t2.IsActive)
                                            .FirstAsync(f => new AppMemberInfo
                                            {
                                                NickName = f.t1.NickName,
                                                TargetId = f.t1.Id,
                                                Avatar = f.t1.Avatar,
                                                BsvNumber = f.t1.BsvNumber,
                                                IsForbidden = (f.t2 != null && f.t2.ForbiddenTime > DateTime.Now),
                                                IsFriend = false,
                                                Gender = (GenderEnum)f.t1.Gender
                                            });
        if (memberInfo == null || string.IsNullOrWhiteSpace(memberInfo.TargetId))
        {
            // 如果非会员，查询客服信息
            memberInfo = await this.Repository.Orm.Select<CzimChatCustomer>()
                                            .Where(w => w.UserId == targetId && w.IsActive)
                                            .FirstAsync(f => new AppMemberInfo
                                            {
                                                NickName = f.NickName,
                                                TargetId = f.UserId,
                                                Avatar = f.Avatar,
                                                BsvNumber = "",
                                                IsForbidden = false,
                                                IsFriend = false,
                                                Gender = GenderEnum.Man
                                            });

        }

        if(memberInfo != null && !string.IsNullOrWhiteSpace(memberInfo.TargetId))
        {
            memberInfo.IsFriend = await this.Repository.Orm.Select<CzimChatFriendList>()
                                                .Where(w=>(w.MemberId == mcb.Id && w.UserId == targetId) || 
                                                          (w.UserId == mcb.Id && w.MemberId == targetId))
                                                .Where(w=>w.IsActive)
                                                .AnyAsync(); 

        }

        return memberInfo;
    }

    /// <summary>
    /// 更新群公告
    /// </summary>
    /// <param name="ownid">用户id</param>
    /// <param name="und">更新信息</param>
    /// <returns></returns>
    public async Task<bool> UpdChatgroupNoticeAsync(string ownid, UpdcgNoticeDto und)
    {
        // 查询群信息
        var _cgInfo = await this.Repository.Orm.Select<CzimChatgroupInfo>()
                                        .Where(w => w.Id == und.ChatgroupId || w.EaseChatgroupId == und.ChatgroupId)
                                        .FirstAsync();
        if (_cgInfo == null || string.IsNullOrWhiteSpace(_cgInfo.EaseChatgroupId))
        {
            MessageBox.Show("请选择正确群！");
        }

        // 查看是否管理员
        var _isM = await this.Repository.Orm.Select<CzimChatgroupMember>()
                                        .Where(w => w.ChatgroupId == _cgInfo.Id &&
                                                    w.MemberId == ownid &&
                                                    w.IsManager &&
                                                    w.IsActive)
                                        .AnyAsync();
        if (!_isM)
        {
            MessageBox.Show("非管理员不可以更新群公告！");
        }

        var _updInfo = await this.Repository.Orm.Update<CzimChatgroupInfo>()
                                                .Set(s => s.Announcement, und.Announcement)
                                                .Set(s => s.UDate, DateTime.Now)
                                                .Where(w => w.Id == _cgInfo.Id)
                                                .ExecuteAffrowsAsync();

        if (_updInfo > 0)
        {
            // 更新环信群公告接口
            var _easeA = await this.easeImService.ChatgroupUpdAnnouncementAsync(_cgInfo.EaseChatgroupId, und.Announcement);
            return _easeA;
        }

        return false;
    }
}