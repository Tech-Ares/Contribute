using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimApp.Request.AppLogin;
using KUX.Services.App.Accounts;
using KUX.Services.App.CzimApp;
using Microsoft.AspNetCore.Mvc;

namespace Czim.AppApi.Controllers;

/// <summary>
/// app帐号登录
/// 注册相关
/// </summary>
public class AccountController : AppBaseController<CzimMemberService>
{
    /// <summary>
    /// token 类型
    /// </summary>
    private const string tokenType = "Bearer ";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">会员服务</param>
    /// <param name="_authorMemberService">会员授权服务</param>
    public AccountController(CzimMemberService defaultService,
        AuthorMemberService _authorMemberService) :
        base(defaultService, _authorMemberService)
    {
    }

    /// <summary>
    /// 登录帐户并获取 token
    /// </summary>
    /// <param name="acd">登录相关信息</param>
    /// <returns></returns>
    [HttpPost("login")]
    [ActionDescriptor("LoginAsync", AuthCheck = false)]
    [ApiResourceCacheFilter(1)]
    public async Task<object> LoginAsync([FromBody] AppLoginDto acd)
    {
        if (acd == null ||
            string.IsNullOrWhiteSpace(acd.LoginName) ||
            string.IsNullOrWhiteSpace(acd.Password))
        {
            MessageBox.Show("请输入账号密码");
        }

        if (string.IsNullOrWhiteSpace(acd?.MeId))
        {
            MessageBox.Show("请使用移动设备登录");
        }

        var token = await this.authorMemberService
                 .CheckAccountAsync(acd?.LoginName, acd.Password, acd.MeId);
        //var token = "";
        //// 判断是帐号密码登录，还是设备id登录
        //if (!string.IsNullOrWhiteSpace(acd.LoginName) &&
        //    !string.IsNullOrWhiteSpace(acd.Password))
        //{
        //    // 如果登录帐号不为空，使用手机号登录
        //    token = await this.authorMemberService
        //                    .CheckAccountAsync(acd.LoginName, acd.Password, acd.MeId);
        //}
        //else
        //{
        //    token = await this.authorMemberService.CheckMemberAsync(acd.MeId);
        //}

        return new { token = tokenType + token, tokenType };
    }

    /// <summary>
    /// 用户登录记录
    /// </summary>
    /// <param name="ltd">登录相关信息</param>
    /// <returns></returns>
    [HttpPost("timeinfo")]
    [ActionDescriptor("LoginTimeInfoAsync", AuthCheck = false)]
    [ApiResourceCacheFilter(1)]
    public async Task<bool> LoginTimeInfoAsync([FromBody] LoginTimeDto ltd)
    {
        var result = await this.DefaultService.LoginTimeInfoAsync(this.OwnId, ltd);

        return result;
    }

    /// <summary>
    /// 用户注册(非手机号注册)
    /// </summary>
    /// <param name="registerAccountDto">会员信息</param>
    /// <returns></returns>
    [HttpPost("register")]
    [ActionDescriptor("RegisterAsync", AuthCheck = false)]
    [ApiResourceCacheFilter(2)]
    public async Task<object> RegisterAsync([FromBody] MemberRegisterDto registerAccountDto)
    {
        if (registerAccountDto == null)
        {
            MessageBox.Show("请填写正确注册信息！");
        }

        if (string.IsNullOrWhiteSpace(registerAccountDto?.LoginName))
        {
            MessageBox.Show("请输入账号！");
        }

        if (string.IsNullOrWhiteSpace(registerAccountDto?.PassWord) ||
            registerAccountDto.PassWord != registerAccountDto.PasswordTwo)
        {
            MessageBox.Show("请输入正确的密码！");
        }

        // 执行帐号注册
        var hasReg = await this.authorMemberService.RegisterMemberAsync(registerAccountDto);

        return new { token = $"{tokenType}{hasReg}", tokenType };
    }
}