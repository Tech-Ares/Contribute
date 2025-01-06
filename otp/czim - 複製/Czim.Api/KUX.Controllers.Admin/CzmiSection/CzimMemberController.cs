using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.ChatCustomer;
using KUX.Models.CzimAdmin.Request.CzimMember;
using KUX.Models.KuxAdmin.Reponse.CzimMember;
using KUX.Models.KuxAdmin.Request.ChatFriend;
using KUX.Models.KuxAdmin.Request.CzimMember;
using KUX.Models.KuxAdmin.Request.CzimMemberLogin;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection;

/// <summary>
/// 会员中心
/// </summary>
[ControllerDescriptor("")]
public class CzimMemberController : AdminBaseController<CzimMemberService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="accountService">账户服务</param>
    public CzimMemberController(CzimMemberService defaultService, IAccountService accountService) : base(defaultService, accountService)
    {
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("findlist")]
    [ApiResourceCacheFilter(0.5)]
    [ActionDescriptor("FindListAsync")]
    public async Task<PageViewModel> FindListAsync([FromBody] MemberSearchDto search)
    {
        return await this.DefaultService.FindListAsync(search);
    }

    /// <summary>
    /// 删除会员信息
    /// </summary>
    /// <param name="search">用户信息</param>
    /// <returns></returns>
    [HttpPost("delmember")]
    [ApiResourceCacheFilter(0.5)]
    [ActionDescriptor("DelMemberAsync")]
    public async Task<bool> DelMemberAsync([FromBody] MemberLoginSearchDto search)
    {
        var _owner = this.accountService.GetAccountInfo();
        if (_owner == null || !_owner.IsAdministrator)
        {
            MessageBox.Show("非管理员不可以操作会员删除！");
        }
        if (search == null || string.IsNullOrWhiteSpace(search.MemberId))
        {
            MessageBox.Show("请选择会员信息！");
        }

        return await this.DefaultService.DelMemberAsync(_owner.UserId, search.MemberId);
    }

    /// <summary>
    /// 客服添加会员接口
    /// </summary>
    /// <param name="dto">传入的对象</param>
    /// <returns></returns>
    [HttpPost("customeraddfriend")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("CustomerAddFriendAsync")]
    public async Task<object> CustomerAddFriendAsync(CustomerAddFriendDto dto)
    {
        var result = await this.DefaultService.CustomerAddFriendAsync(dto);
        if (result.Item1)
        {
            return result.Item2;
        }
        return default;
    }

    /// <summary>
    /// 查询所有可被会员添加的客服
    /// </summary>
    /// <param name="dto">会员添加客服实体</param>
    /// <returns>客服列表</returns>
    [HttpPost("memberaddroomcustomerlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberAddRoomCustomerListAsync")]
    public async Task<PageViewModel> MemberAddRoomCustomerListAsync(MemberAddRoomCustomerSearchDto dto)
    {
        return await this.DefaultService.MemberAddRoomCustomerListAsync(dto);
    }

    /// <summary>
    /// 查询所有可被客服添加的会员
    /// </summary>
    /// <param name="dto">客服添加会员实体</param>
    /// <returns>客服列表</returns>
    [HttpPost("customeraddroommemberlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("CustomerAddRoomMemberListAsync")]
    public async Task<PageViewModel> CustomerAddRoomMemberListAsync(CustomerAddRoomMemberSearchDto dto)
    {
        return await this.DefaultService.CustomerAddRoomMemberListAsync(dto);
    }

    /// <summary>
    /// 登录日志
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("findloginrecordlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("FindLoginRecordListAsync")]
    public async Task<PageViewModel> FindLoginRecordListAsync([FromBody] MemberLoginSearchDto search)
    {
        return await this.DefaultService.FindLoginRecordList(search);
    }

    /// <summary>
    /// 会员加聊天室记录
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("memberjoinchatroomFindlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberJoinChatRoomFindListAsync")]
    public async Task<PageViewModel> MemberJoinChatRoomFindListAsync([FromBody] MemberJoinGroupSearchDto search)
    {
        return await this.DefaultService.MemberJoinChatRoomFindListAsync(search);
    }

    /// <summary>
    /// 会员加群组记录
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("memberjoinchatgroupfindlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberJoinChatGroupFindListAsync")]
    public async Task<PageViewModel> MemberJoinChatGroupFindListAsync([FromBody] MemberJoinGroupSearchDto search)
    {
        return await this.DefaultService.MemberJoinChatGroupFindListAsync(search);
    }

    /// <summary>
    /// 会员加群记录
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("groupmemberfindlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("GroupMemberFindListAsync")]
    public async Task<PageViewModel> GroupMemberFindListAsync([FromBody] MemberJoinGroupSearchDto search)
    {
        return await this.DefaultService.GroupMemberFindListAsync(search);
    }

    /// <summary>
    /// 会员好友记录
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("memberfriendfindlist")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MemberfriendFindListAsync")]
    public async Task<PageViewModel> MemberfriendFindListAsync([FromBody] ChatFriendDto search)
    {
        return await this.DefaultService.MemberfriendFindList(search);
    }

    // 将会员拉到聊天室 ChatRoomAddMemberDto
    /// <summary>
    /// 将会员添加到聊天室
    /// </summary>
    /// <param name="cramd">会员聊天室信息</param>
    /// <returns></returns>
    [HttpPost("joinchatroom")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("JoinChatRoomAsync")]
    public async Task<string> JoinChatRoomAsync([FromBody] ChatRoomAddMemberDto cramd)
    {
        var aInfo = this.accountService.GetAccountInfo();
        if (aInfo == null)
        {
            MessageBox.Show("请登录帐号");
        }

        if (!aInfo.IsAdministrator)
        {
            MessageBox.Show("帐号权限不足！");
        }

        if (cramd == null)
        {
            MessageBox.Show("请选择加入的会员和聊天室信息！");
        }
        if (string.IsNullOrWhiteSpace(cramd.MemberId))
        {
            MessageBox.Show("请选择会员！");
        }

        if (string.IsNullOrWhiteSpace(cramd.ChatRoomId))
        {
            MessageBox.Show("请选择正确的聊天室信息！");
        }

        // 添加会员到聊天室
        var data = await this.DefaultService.JoinChatRoomAsync(this.UserId, cramd);

        if (data.Item1)
        {
            return data.Item2;
        }
        MessageBox.Show(data.Item2);
        return default;
    }

    /// <summary>
    /// 将会员添加到群
    /// </summary>
    /// <param name="cramd">会员群信息</param>
    /// <returns></returns>
    [HttpPost("joinchatgroup")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("JoinChatgroupAsync")]
    public async Task<string> JoinChatgroupAsync([FromBody] ChatgroupAddMemberDto cramd)
    {
        var aInfo = this.accountService.GetAccountInfo();

        if (cramd == null || cramd.MemberId == null)
        {
            MessageBox.Show("请选择加入群的会员！");
        }

        if (string.IsNullOrWhiteSpace(cramd.ChatgroupId))
        {
            MessageBox.Show("请选择正确的聊天室信息！");
        }

        // 添加会员到聊天室
        var data = await this.DefaultService.JoinChatgroupAsync(aInfo, cramd);

        if (data.Item1)
        {
            return data.Item2;
        }
        MessageBox.Show(data.Item2);
        return default;
    }

    /// <summary>
    /// 会员详情
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("findmember")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("FindMemberAsync")]
    public async Task<CzimMemberVo> FindMemberAsync([FromBody] MemberSearchDto search)
    {
        return await this.DefaultService.FindMember(search);
    }

    // 添加机器人
    /// <summary>
    /// 添加机器人
    /// </summary>
    /// <param name="mad">会员信息</param>
    /// <returns></returns>
    [HttpPost("addrobot")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("AddRobotAsync")]
    public async Task<object> AddRobotAsync([FromBody] MemberAddDto mad)
    {
        var hasCount = 0;
        //机器人
        if (mad.MemberType == 1)
        {
            if (mad != null && mad.AddCount > 0)
            {
                // 执行生成操作
                for (int i = 0; i < mad.AddCount; i++)
                {
                    var result = await this.DefaultService.AddRobotAsync(this.UserId, mad);
                    if (result.Item1)
                    {
                        hasCount++;
                    }
                }

                return hasCount;
            }
        }
        //新增用户
        var regist = await this.DefaultService.AddMemberAsync(Guid.NewGuid().ToString("N"), this.UserId, mad.Gender, mad.LoginName, mad.Pwd, mad.NickName);
        if (regist.Item1)
        {
            return (true, "添加成员成功");
        }

        MessageBox.Show("请选择机器人数量");
        return 0;
    }
}