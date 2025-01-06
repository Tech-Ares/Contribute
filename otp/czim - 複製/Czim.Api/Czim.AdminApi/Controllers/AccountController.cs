using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.DTO;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Czim.AdminApi.Controllers;

/// <summary>
/// 账户控制器
/// </summary>
public class AccountController : AdminBaseController
{
    /// <summary>
    /// 账户服务
    /// </summary>
    private readonly IAccountService _accountService;

    /// <summary>
    /// 环信基本配置服务
    /// </summary>
    private CzimEaseImSettingService czimEaseImSettingService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="accountService">账户服务</param>
    /// <param name="_czimEaseImSettingService">环信配置服务</param>
    public AccountController(IAccountService accountService, CzimEaseImSettingService _czimEaseImSettingService)
    {
        _accountService = accountService;
        czimEaseImSettingService = _czimEaseImSettingService;
    }

    /// <summary>
    /// 检查帐户并获取 token
    /// </summary>
    /// <param name="authUserDto">Dto</param>
    /// <returns></returns>
    [HttpPost("check")]
    [ActionDescriptor("CheckAsync", AuthCheck = false)]
    public async Task<object> CheckAsync([FromBody] AuthUserFormDto authUserDto)
    {
        var token = await this._accountService
            .CheckAccountAsync(authUserDto.UserName, authUserDto.Password, authUserDto.LoginCode);

        return token;

        //return new { token = tokenType + token, tokenType };
    }

    [HttpPost("gettoken")]
    [ActionDescriptor("CheckAsync", AuthCheck = false)]
    public async Task<string> GetToken([FromBody] string msgid)
    {
        var token = await this.czimEaseImSettingService.MessagesRecall("960870472617035936", "169665152221185");

        return default;

    }
}