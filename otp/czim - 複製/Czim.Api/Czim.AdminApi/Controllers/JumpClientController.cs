using Microsoft.AspNetCore.Mvc;

namespace Czim.AdminApi.Controllers;

/// <summary>
/// 跳转客户端
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class JumpClientController : Controller
{
    /// <summary>
    /// 默认路由
    /// </summary>
    /// <returns></returns>
    [Route("")]
    public IActionResult Index()
    {
        return Redirect("/swagger/index.html");
    }
}