using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Models.BO;
using KUX.Services.Admin.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers.Admin.ControllersAdmin;

/// <summary>
/// 后台默认路由
/// </summary>
/// <typeparam name="TDefaultService">默认服务</typeparam>
//[Authorize]//是否授权 Authorize
[ApiController]
[ApiResultFilter]
[ApiExplorerSettings(GroupName = nameof(ApiVersions.Admin))]
[Route("baseadmin/[controller]")]
public class AdminBaseController<TDefaultService> : ControllerBase where TDefaultService : class
{
    /// <summary>
    /// 默认服务
    /// </summary>
    protected readonly TDefaultService DefaultService;

    /// <summary>
    /// 系统账户服务
    /// </summary>
    protected IAccountService accountService;

    /// <summary>
    /// 账户信息
    /// </summary>
    protected AccountInfo accountInfo => this.accountService.GetAccountInfo();

    /// <summary>
    /// 用户id
    /// </summary>
    protected string UserId => accountService.UserId;

    /// <summary>
    /// 是否超管
    /// </summary>
    protected bool IsAdmin => accountService.IsAdmin;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_accountService">账户服务</param>
    public AdminBaseController(TDefaultService defaultService, IAccountService _accountService)
    {
        this.DefaultService = defaultService;
        this.accountService = _accountService;
    }
}

/// <summary>
/// 不需要授权的控制器
/// </summary>
//[Authorize]//是否授权 Authorize
[ApiController]
[ApiResultFilter]
[Route("baseadmin/[controller]")]
[ApiExplorerSettings(GroupName = nameof(ApiVersions.Admin))]
public class AdminBaseController : ControllerBase
{
}