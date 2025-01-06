using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.CzimMember;
using KUX.Models.CzimApp.AppBo;
using KUX.Models.CzimApp.Reponse.AppMember;
using KUX.Models.CzimApp.Request.AppMember;
using KUX.Services.App.Accounts;
using KUX.Services.App.CzimApp;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers.Admin.CzmiSection;

/// <summary>
/// 会员管理
/// </summary>
public class MemberController : AppBaseController<CzimMemberService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_authorMemberService">基本授权服务</param>
    public MemberController(CzimMemberService defaultService, AuthorMemberService _authorMemberService)
        : base(defaultService, _authorMemberService)
    {
    }

    // 更新会员信息
    /// <summary>
    /// 会员个人基本信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("baseinfo")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("BaseInfoAsync")]
    public MemberCacheBo BaseInfoAsync()
    {
        //会员id
        var memberInfo = this.authorMemberService.GetMemberInfo();
        return memberInfo;
    }

    // 更新密码
    /// <summary>
    /// 更新会员密码
    /// </summary>
    /// <param name="upwd">密码信息</param>
    /// <returns>返回密码信息 </returns>
    [HttpPost("updpwd")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("UpdPasswordAsync")]
    public async Task<bool> UpdPasswordAsync([FromBody] UpdPasswordDto upwd)
    {
        if (upwd == null)
        {
            MessageBox.Show("信息填写不正确！");
        }
        if (string.IsNullOrWhiteSpace(upwd.OldPwd))
        {
            MessageBox.Show("请填写旧密码");
        }

        if (string.IsNullOrEmpty(upwd.NewPwd) || string.IsNullOrWhiteSpace(upwd.NewPwd2) || upwd.NewPwd2 != upwd.NewPwd)
        {
            MessageBox.Show("新密码输入不正确！");
        }
        //会员id
        var isUpd = await this.DefaultService.UpdPasswordAsync(this.OwnId, upwd);
        return isUpd;
    }

    /// <summary>
    /// 更新会员基本信息
    /// </summary>
    /// <param name="mbid">会员基本信息</param>
    /// <returns></returns>
    [HttpPost("updinfo")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("UpdMemberBaseInfoAsync")]
    public async Task<string> UpdMemberBaseInfoAsync([FromBody] MemberBaseInfoDto mbid)
    {
        //会员id
        if (string.IsNullOrWhiteSpace(this.OwnId))
        {
            MessageBox.Show("实名失败，请重新登录");
        }
        if (mbid == null)
        {
            MessageBox.Show("请填写正确会员信息");
        }
        var result = await this.DefaultService.UpdMemberBaseInfoAsync(mbid, this.authorMemberService.GetMemberInfo());

        if (result.Item1)
        {
            // 更新缓存
            this.authorMemberService.UpdMemberCache(this.OwnId);

            return result.Item2;
        }
        MessageBox.Show(result.Item2);

        return "";
    }

    // 获取专属客服接口
    /// <summary>
    /// 获取专属客服接口
    /// </summary>
    /// <returns></returns>
    [HttpPost("getexclusive")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("GetExclusiveUserAsync")]
    public async Task<List<AppChatContactsVo>> GetExclusiveUserAsync()
    {
        var data = await this.DefaultService.GetExclusiveUserAsync(this.OwnId);

        return data;
    }

    /// <summary>
    /// 获取所有好友信息
    /// </summary>
    /// <param name="csd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("getcontacts")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("ContactsListAsync")]
    public async Task<List<AppChatContactsVo>> GetContactsAsync([FromBody] ContactSearchDto csd)
    {
        // 获取所有联系人接口
        var data = await this.DefaultService.GetContactsAsync(this.OwnId, csd, this.authorMemberService.GetMemberInfo().IsAgent);
        return data;
    }

    /// <summary>
    /// 通过手机号\会员编号\登录名查询人员信息
    /// </summary>
    /// <param name="csd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("searchcontacts")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("SearchContactsAsync")]
    public async Task<List<AppSearchContactsVo>> SearchContactsAsync([FromBody] VagueSearchDto csd)
    {
        // 获取所有联系人接口
        var data = await this.DefaultService.SearchContactsAsync(this.OwnId, csd, this.authorMemberService.GetMemberInfo().IsAgent);
        return data;
    }

    /// <summary>
    /// 通过id查询会员信息
    /// </summary>
    /// <param name="sbd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("getbyid")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("GetByIdAsync")]
    public async Task<AppSearchContactsVo> GetByIdAsync([FromBody] SearchByIdDto sbd)
    {
        // 获取联系人接口
        var data = await this.DefaultService.GetByIdAsync(this.OwnId, sbd, this.authorMemberService.GetMemberInfo().IsAgent);
        return data;
    }

    // 添加好友
    /// <summary>
    /// 通过id添加好友
    /// </summary>
    /// <param name="afd">联系人信息</param>
    /// <returns></returns>
    [HttpPost("addfriend")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("AddFriendAsync")]
    public async Task<bool> AddFriendAsync([FromBody] AddFriendDto afd)
    {
        if (afd == null || string.IsNullOrWhiteSpace(afd.TargetId))
        {
            MessageBox.Show("请选择要添加的好友");
        }

        // 获取所有联系人接口
        var data = await this.DefaultService.AddFriendAsync(this.OwnId, afd, this.authorMemberService.GetMemberInfo().IsAgent);
        return data;
    }

    /// <summary>
    /// 添加好友到聊天室列表
    /// </summary>
    /// <param name="acr">聊天室信息</param>
    /// <returns></returns>
    [HttpPost("addchatroom")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("AddChatRoomAsync")]
    public async Task<bool> AddChatRoomAsync([FromBody] AddChatRoomDto acr)
    {
        if (!this.authorMemberService.GetMemberInfo().IsAgent)
        {
            MessageBox.Show("没有操作权限");
        }
        if (acr == null || (string.IsNullOrWhiteSpace(acr?.TargetId) && acr?.TargetIds?.Count <= 0))
        {
            MessageBox.Show("请选择要添加的好友");
        }
        if (string.IsNullOrWhiteSpace(acr?.ChatRoomId))
        {
            MessageBox.Show("请选择要加入的聊天室！");
        }
        // 获取所有联系人接口
        var data = await this.DefaultService.AddChatRoomAsync(this.OwnId, acr);
        return data;
    }

    /// <summary>
    /// 获取所有聊天室信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatroom")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("GetChatRoomAsync")]
    public async Task<List<AppChatRoomVo>> GetChatRoomAsync()
    {
        // 获取所有联系人接口
        var data = await this.DefaultService.GetChatRoomAsync(this.OwnId);
        return data;
    }

    // 会员填写推荐码 UseReferral
    /// <summary>
    /// 会员使用推荐码
    /// </summary>
    /// <param name="murd">推荐码信息</param>
    /// <returns></returns>
    [HttpPost("usereferral")]
    [ActionDescriptor("UseReferralAsync")]
    public async Task<string> UseReferralAsync([FromBody] MemberUseReferralDto murd)
    {
        var mInfo = this.authorMemberService.GetMemberInfo();

        if (mInfo == null || string.IsNullOrWhiteSpace(mInfo.BsvNumber))
        {
            MessageBox.Show("会员信息获取失败，请重新登录");
        }

        if (!string.IsNullOrWhiteSpace(mInfo?.RNumber) && mInfo.RNumber != "000000")
        {
            MessageBox.Show("已有推荐人！");
        }

        var suInfo = await this.DefaultService.UseReferralAsync(mInfo.Id, murd);
        if (suInfo.Item1)
        {
            return suInfo.Item2;
        }
        MessageBox.Show("推荐码失效！");

        return "";
    }

    /// <summary>
    /// 会员建议收集
    /// </summary>
    /// <param name="fbd">推荐码信息</param>
    /// <returns></returns>
    [HttpPost("feedback")]
    [ActionDescriptor("FeedBackAsync")]
    public async Task<bool> FeedBackAsync([FromBody] FeedBackDto fbd)
    {
        if (fbd == null || string.IsNullOrWhiteSpace(fbd.Content))
        {
            MessageBox.Show("请填写您的宝贵建议！");
        }
        var suInfo = await this.DefaultService.FeedBackAsync(this.OwnId, fbd);
        return true;
    }

    /// <summary>
    /// 获取所有群信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("getchatgroup")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("GetChatGroupAsync")]
    public async Task<List<AppChatgroupVo>> GetChatGroupAsync()
    {
        // 获取所有群接口
        var data = await this.DefaultService.GetChatgroupAsync(this.OwnId);
        return data;
    }

    /// <summary>
    /// 添加好友到群组
    /// </summary>
    /// <param name="pgd">群组信息</param>
    /// <returns></returns>
    [HttpPost("pullgroup")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("PullGroupAsync")]
    public async Task<bool> PullGroupAsync([FromBody] PullGroupDto pgd)
    {
        if (!this.authorMemberService.GetMemberInfo().IsAgent)
        {
            MessageBox.Show("没有操作权限");
        }
        if (pgd == null ||
            pgd?.MemberIds?.Count <= 0)
        {
            MessageBox.Show("请选择要添加的好友");
        }
        if (string.IsNullOrWhiteSpace(pgd?.ChatgroupId))
        {
            MessageBox.Show("请选择要加入的聊天室！");
        }
        // 获取所有联系人接口
        var data = await this.DefaultService.PullGroupAsync(this.OwnId, pgd);
        return data;
    }

    /// <summary>
    /// 管理员批量移除群成员
    /// </summary>
    /// <param name="pgd">群组信息</param>
    /// <returns></returns>
    [HttpPost("deletgroupuser")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("DeleteGroupUserAsync")]
    public async Task<bool> DeleteGroupUserAsync([FromBody] DeleteGroupUsersDto pgd)
    {
        if (pgd == null ||
            pgd?.MemberIds?.Count <= 0)
        {
            MessageBox.Show("请选择要移除的人员");
        }
        if (string.IsNullOrWhiteSpace(pgd?.ChatgroupId))
        {
            MessageBox.Show("请选择要调整的群！");
        }

        // 获取所有联系人接口
        var data = await this.DefaultService.DeleteGroupUserAsync(this.OwnId, pgd);
        return data;
    }

    // 扫码加好友，加群，加聊天室
    /// <summary>
    /// 扫码加好友，加群，加聊天室
    /// </summary>
    /// <param name="pgd">扫码信息</param>
    /// <returns></returns>
    [HttpPost("scanqrcode")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("ScanQrCodeAsync")]
    public async Task<bool> ScanQrCodeAsync([FromBody] ScanQrCodeDto pgd)
    {
        if (pgd == null || string.IsNullOrWhiteSpace(pgd.QrCode))
        {
            MessageBox.Show("二维码错误，无法识别！");
        }
        // 获取所有联系人接口
        var data = await this.DefaultService.ScanQrCodeAsync(this.OwnId, pgd);
        return data;
    }
}