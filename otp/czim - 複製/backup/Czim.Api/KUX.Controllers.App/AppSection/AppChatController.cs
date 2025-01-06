using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimApp.Reponse.AppMember;
using KUX.Models.CzimApp.Request.AppChat;
using KUX.Services.App.Accounts;
using KUX.Services.App.CzimApp;
using Microsoft.AspNetCore.Mvc;
using KUX.Repositories.Core.BaseModels;
using KUX.Models.CzimApp.Reponse.AppChat;

namespace KUX.Controllers.Admin.CzmiSection;

/// <summary>
/// 聊天相关接口
/// </summary>
public class AppChatController : AppBaseController<AppChatService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_authorMemberService">基本授权服务</param>
    public AppChatController(AppChatService defaultService, AuthorMemberService _authorMemberService)
        : base(defaultService, _authorMemberService)
    {
    }

    /// <summary>
    /// 获取所有用户昵称和头像
    /// </summary>
    /// <returns></returns>
    [HttpPost("getuserext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetUserExtAsync")]
    public async Task<List<AppChatContactsVo>> GetUserExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetUserExtAsync(this.OwnId, sesd.TargetIds);
        return data;
    }

    /// <summary>
    /// 获取所有聊天室头像，名称信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatroomext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatRoomExtAsync")]
    public async Task<List<AppChatContactsVo>> GetChatRoomExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有联系人头像和昵称
        var data = await this.DefaultService.GetChatRoomExtAsync(this.OwnId, sesd.TargetIds);
        return data;
    }

    // 消息撤回
    /// <summary>
    /// 消息撤回
    /// </summary>
    /// <param name="sesd">消息内容</param>
    /// <returns></returns>
    [HttpPost("msgrecall")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("MsgRecallAsync")]
    public async Task<string> MsgRecallAsync([FromBody] RecallmessageDto sesd)
    {
        if (sesd == null || string.IsNullOrWhiteSpace(sesd?.MsgId))
        {
            MessageBox.Show("请选择撤回的消息！");
            return default;
        }

        //撤回消息
        var data = await this.DefaultService.MsgRecallAsync(this.OwnId, sesd);
        return data.Item2;
    }

    /// <summary>
    /// 聊天室/群消息撤回
    /// </summary>
    /// <param name="cmd">聊天消息</param>
    /// <returns></returns>
    [HttpPost("recallchan")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("MsgRecallChanAsync")]
    public async Task<string> MsgRecallChanAsync([FromBody] RecallmessageDto cmd)
    {
        if (cmd == null || string.IsNullOrWhiteSpace(cmd?.MsgId))
        {
            MessageBox.Show("请选择撤回的消息！");
            return default;
        }

        var result = await this.DefaultService.MsgRecallChanAsync(this.OwnId, cmd);

        if (result.Item1)
        {
            return result.Item2;
        }

        return default;
    }

    // 漫游消息，获取所有群图标和名称
    /// <summary>
    /// 漫游消息
    /// 获取所有群图标，名称信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatgroupext")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatGroupExtAsync")]
    public async Task<List<AppChatContactsVo>> GetChatGroupExtAsync([FromBody] ExtSearchDto sesd)
    {
        if (sesd == null || sesd?.TargetIds?.Count <= 0)
        {
            return default;
        }

        // 获取所有群图片和名称
        var data = await this.DefaultService.GetChatGroupExtAsync(sesd.TargetIds);
        return data;
    }

    // 查询当前群消息
    /// <summary>
    /// 查询当前群信息
    /// </summary>
    /// <param name="gmsd">群id信息</param>
    /// <returns></returns>
    [HttpPost("getchatgroupinfo")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatgroupInfoAsync")]
    public async Task<ChatgroupInfoVo> GetChatgroupInfoAsync([FromBody] GroupMemberSearchDto gmsd)
    {
        if (gmsd == null || string.IsNullOrWhiteSpace(gmsd.TargetId))
        {
            MessageBox.Show("请选择群信息！");
        }

        var data = await this.DefaultService.GetChatgroupInfoAsync(gmsd.TargetId);
        return data;
    }

    /// <summary>
    /// 查询所有群成员(详情)
    /// </summary>
    /// <param name="gmsd">群id信息</param>
    /// <returns></returns>
    [HttpPost("getchatgroupmember")]
    [ApiResourceCacheFilter(5)]
    [ActionDescriptor("GetChatGroupMemberAsync")]
    public async Task<ApiViewCountModel<AppChatContactsVo>> GetChatGroupMemberAsync([FromBody] GroupMemberSearchDto gmsd)
    {
        if (gmsd == null || string.IsNullOrWhiteSpace(gmsd.TargetId))
        {
            MessageBox.Show("请选择群信息！");
        }

        var data = await this.DefaultService.GetChatGroupMemberAsync(gmsd.TargetId);
        return data;
    }

    // 查询群成员信息（个人信息）
    /// <summary>
    /// 查询群成员(个人信息)
    /// </summary>
    /// <param name="gmsd">群id信息</param>
    /// <returns></returns>
    [HttpPost("getmemberInfo")]
    [ApiResourceCacheFilter(3)]
    [ActionDescriptor("GetMemberInfoAsync")]
    public async Task<AppMemberInfo> GetMemberInfoAsync([FromBody] GroupMemberSearchDto gmsd)
    {
        if (gmsd == null || string.IsNullOrWhiteSpace(gmsd.TargetId))
        {
            MessageBox.Show("请选择人员 信息！"); 
        }

        var data = await this.DefaultService.GetMemberInfoAsync(this.authorMemberService.GetMemberInfo(), gmsd.TargetId);
        return data;
    }

    // 更改群公告
    /// <summary>
    /// 更改群公告(管理员)
    /// </summary>
    /// <param name="sesd">群公告信息</param>
    /// <returns></returns>
    [HttpPost("updacgnotice")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("UpdChatgroupNoticeAsync")]
    public async Task<bool> UpdChatgroupNoticeAsync([FromBody] UpdcgNoticeDto sesd)
    {
        if (sesd == null)
        {
            MessageBox.Show("请填写群公告");
            return default;
        }

        // 更改群公告
        var data = await this.DefaultService.UpdChatgroupNoticeAsync(this.OwnId, sesd);
        return data;
    }
}