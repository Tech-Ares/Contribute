using KUX.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Infrastructure.Controllers;

/// <summary>
/// api接口默认控制器
/// </summary>
/// <typeparam name="TDefaultService"></typeparam>
public class ApiBaseController<TDefaultService> : ApiBaseController where TDefaultService : class
{
    /// <summary>
    /// 默认服务
    /// </summary>
    protected readonly TDefaultService DefaultService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    public ApiBaseController(TDefaultService defaultService)
    {
        this.DefaultService = defaultService;
    }
}

/// <summary>
/// 控制器服务
/// </summary>
[ApiResultFilter]
[ApiController]
[Route("api/[controller]")]
public class ApiBaseController : ControllerBase
{
}