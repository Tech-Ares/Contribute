using KUX.Infrastructure.Controllers;
using KUX.Infrastructure.Permission.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Czim.AdminApi.Controllers;

/// <summary>
/// 异常页面
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
[ControllerDescriptor]
public class HomeController : ApiBaseController
{
    /// <summary>
    /// 错误页面
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Home/Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        return Content("程序异常，请查看错误日志!");
    }
}