using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Reponse.Chat;
using KUX.Models.CzimAdmin.Request.ChatInfo;
using KUX.Models.CzimAdmin.Request.ChatRoom;
using KUX.Models.CzimAdmin.Request.CzimChat;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using KUX.Services.EaseImServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection;

/// <summary>
/// 客服聊天管理
/// </summary>
public class CzimChatController : AdminBaseController<CzimChatService>
{
    /// <summary>
    /// 环信服务
    /// </summary>
    private readonly EaseImService easeImService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_accountService">账户服务</param>
    /// <param name="_easeImService">环信服务</param>
    public CzimChatController(CzimChatService defaultService, IAccountService _accountService, EaseImService _easeImService)
        : base(defaultService, _accountService)
    {
        this.easeImService = _easeImService;
    }

    // 获取所有群信息
    /// <summary>
    /// 获取所有聊天室信息
    /// </summary>
    /// <param name="crsd">聊天室查询信息</param>
    /// <returns></returns>
    [HttpPost("getchatroom")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("GetChatRoomListAsync")]
    public async Task<PageViewModel> GetChatRoomListAsync([FromBody] ChatRoomSearchDto crsd)
    {
        if (crsd == null)
        {
            crsd = new ChatRoomSearchDto()
            {
                Page = 1,
                Size = 20
            }; ;
        }
        // 获取聊天室信息
        var data = await this.DefaultService.GetChatRoomListAsync(this.UserId, crsd);
        return data;
    }

    /// <summary>
    /// 获取所有联系人信息
    /// </summary>
    /// <param name="crsd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("getcontacts")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("ContactsListAsync")]
    public async Task<PageViewModel> ContactsListAsync([FromBody] ChatRoomSearchDto crsd)
    {
        if (crsd == null)
        {
            crsd = new ChatRoomSearchDto()
            {
                Page = 1,
                Size = 20
            };
        }
        // 获取联系人
        var data = await this.DefaultService.ContactsListAsync(this.UserId, crsd.Page, crsd.Size);
        return data;
    }

    // 查询预设消息 ChatPresetSearchDto

    /// <summary>
    /// 获取所有预设消息
    /// </summary>
    /// <param name="crsd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("getpreset")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("ContactsListAsync")]
    public async Task<List<string>> PresetListAsync([FromBody] ChatPresetSearchDto crsd)
    {
        // 获取预设信息
        var data = await this.DefaultService.PresetListAsync(this.UserId, crsd);
        return data;
    }

    // 管理员点击头像加好友
    /// <summary>
    /// 管理员群中添加好友
    /// </summary>
    /// <param name="crsd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("addfriend")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("AddFriendListAsync")]
    public async Task<bool> AddFriendListAsync([FromBody] AddFriendDto crsd)
    {
        if (crsd == null || string.IsNullOrWhiteSpace(crsd.TargetId))
        {
            MessageBox.Show("请选择好友用户");
        }

        // 获取预设信息
        var data = await this.DefaultService.AddFriendListAsync(this.UserId, crsd);
        return data;
    }

    /// <summary>
    /// 聊天室开启/关闭全员禁言
    /// </summary>
    /// <param name="crpd">禁言状态</param>
    /// <returns></returns>
    [HttpPost("chatroomprohibit")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("ChatRoomProhibitAsync")]
    public async Task<object> ChatRoomProhibitAsync([FromBody] ChatRoomProhibitDto crpd)
    {
        var aInfo = this.accountService.GetAccountInfo();

        var result = await this.DefaultService.ChatRoomProhibitAsync(aInfo, crpd);
        if (result.Item1)
        {
            return result.Item2;
        }
        return result;
    }

    /// <summary>
    /// 群组开启/关闭群体禁言
    /// </summary>
    /// <param name="crad"></param>
    /// <returns></returns>
    [HttpPost("chatgroupprohibit")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("ChatGroupProhibitAsync")]
    public async Task<object> ChatGroupProhibitAsync(ChatRoomProhibitDto crad)
    {
        var aInfo = this.accountService.GetAccountInfo();

        var result = await this.DefaultService.ChatGroupProhibitAsync(aInfo, crad);
        if (result.Item1)
        {
            return result.Item2;
        }
        return default;
    }

    /// <summary>
    /// 群组会员禁言 单个会员禁言/解禁
    /// </summary>
    /// <param name="crad"></param>
    /// <returns></returns>
    [HttpPost("memberforbiddengroup")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberForbiddenGroupAsync")]
    public async Task<object> MemberForbiddenGroupAsync(ChatRoomMemberForbiddenDto crad)
    {
        var aInfo = this.accountService.GetAccountInfo();
       
        var result = await this.DefaultService.MemberForbiddenGroupAsync(aInfo, crad);
        if (result.Item1)
        {
            return result.Item2;
        }
        return false;
    }

    // 聊天室会员禁言
    /// <summary>
    /// 单个会员禁言/解禁
    /// </summary>
    /// <param name="crpd">禁言内容</param>
    /// <returns></returns>
    [HttpPost("memberforbidden")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberForbiddenAsync")]
    public async Task<object> MemberForbiddenAsync([FromBody] ChatRoomMemberForbiddenDto crpd)
    {
        var aInfo = this.accountService.GetAccountInfo();

        var result = await this.DefaultService.MemberForbiddenAsync(aInfo, crpd);
        if (result.Item1)
        {
            return result.Item2;
        }
        return false;
    }

    /// <summary>
    /// 聊天室/群组会员全局禁言/解禁(时间传0天即解禁)
    /// </summary>
    /// <param name="cmd">设置聊天室会员全局禁言对象</param>
    /// <returns>返回一个元组(bool,string) 第一个参数代表是否成功，第二个是异常或者成功信息</returns>
    [HttpPost("allchatroomban")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("AllChatRoomBanAsync")]
    public async Task<object> AllChatRoomBanAsync(AllChatRoomBanDto cmd)
    {
        var aInfo = this.accountService.GetAccountInfo();

        var result = await DefaultService.AllChatRoomBan(this.UserId, cmd);

        if (result.Item1)
        {
            return result.Item2;
        }
        return default;
    }

    /// <summary>
    /// 管理员撤回群聊消息
    /// </summary>
    /// <param name="cmd">聊天消息</param>
    /// <returns></returns>
    [HttpPost("recallmessage")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("DoRecallChanAsync")]
    public async Task<string> DoRecallChanAsync([FromBody] ChatMessageDto cmd)
    {
        var result = await this.DefaultService.DoRecallChanAsync(this.accountInfo, cmd);

        if (result.Item1)
        {
            return result.Item2;
        }

        return default;
    }

    /// <summary>
    /// 管理员撤回单聊消息
    /// </summary>
    /// <param name="cmd">聊天消息</param>
    /// <returns></returns>
    [HttpPost("recallone")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("DoRecallOneAsync")]
    public async Task<string> DoRecallOneAsync([FromBody] ChatMessageDto cmd)
    {
        var result = await this.DefaultService.DoRecallOneAsync(this.accountInfo, cmd);

        if (result.Item1)
        {
            return result.Item2;
        }

        return default;
    }

    /// <summary>
    /// 通过id获取所有用户昵称和头像
    /// </summary>
    /// <returns></returns>
    [HttpPost("getuserext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetUserExtAsync")]
    public async Task<List<ChatContactsVo>> GetUserExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetUserExtAsync(this.UserId, sesd.TargetIds);
        return data;
    }

    /// <summary>
    /// 通过id获取所有聊天室头像，名称信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatroomext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatRoomExtAsync")]
    public async Task<List<ChatContactsVo>> GetChatRoomExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetChatRoomExtAsync(this.UserId, sesd.TargetIds);
        return data;
    }

    //
    /// <summary>
    /// 获取所有群信息
    /// </summary>
    /// <param name="crsd">群查询信息</param>
    /// <returns></returns>
    [HttpPost("getchatgroup")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("GetChatGroupListAsync")]
    public async Task<PageViewModel> GetChatGroupListAsync([FromBody] ChatgroupSearchDto crsd)
    {
        if (crsd == null)
        {
            crsd = new ChatgroupSearchDto()
            {
                Page = 1,
                Size = 20
            };
        }
        // 获取聊天室信息
        var data = await this.DefaultService.GetChatGroupListAsync(this.UserId, crsd);
        return data;
    }

    /// <summary>
    /// 通过id获取所有群头像，名称信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatgroupext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatGroupExtAsync")]
    public async Task<List<ChatContactsVo>> GetChatGroupExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetChatGroupExtAsync(this.UserId, sesd.TargetIds);
        return data;
    }

    /// <summary>
    /// 通过群组id获取所有成员
    /// @功能使用
    /// </summary>
    /// <param name="sesd">群组信息</param>
    /// <returns></returns>
    [HttpPost("getgroupmember")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetGroupMemberAsync")]
    public async Task<List<ChatContactsVo>> GetGroupMemberAsync([FromBody] ChatgroupSearchDto sesd)
    {
        if (sesd == null || string.IsNullOrWhiteSpace(sesd?.ChatgroupId))
        {
            MessageBox.Show("请选择群组");
            return default;
        }
        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetGroupMemberAsync(this.UserId, sesd);
        return data;
    }
}