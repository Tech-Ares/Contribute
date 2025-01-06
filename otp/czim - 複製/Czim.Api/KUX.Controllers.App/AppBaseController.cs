using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Services.App.Accounts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace KUX.Controllers.Admin.ControllersAdmin;

/// <summary>
/// App默认路由
/// </summary>
/// <typeparam name="TDefaultService">默认服务</typeparam>
//[Authorize]//是否授权 Authorize
[ApiController]
[ApiResultFilter]
[ApiExplorerSettings(GroupName = nameof(ApiVersions.App))]
[Route("baseapp/[controller]")]
public class AppBaseController<TDefaultService> : ControllerBase where TDefaultService : class
{
    /// <summary>
    /// 默认服务
    /// </summary>
    protected readonly TDefaultService DefaultService;

    /// <summary>
    /// 会员授权服务
    /// </summary>
    protected readonly AuthorMemberService authorMemberService;

    /// <summary>
    /// 游客id
    /// </summary>
    protected readonly string tourist = "tourist";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">业务服务</param>
    /// <param name="_authorMemberService">会员授权服务</param>
    public AppBaseController(TDefaultService defaultService,
        AuthorMemberService _authorMemberService)
    {
        this.DefaultService = defaultService;
        this.authorMemberService = _authorMemberService;
    }

    /// <summary>
    /// 当前会员Id
    /// </summary>
    protected string OwnId => authorMemberService.Mid;

    /// <summary>
    /// 当前会员Id
    /// </summary>
    protected string OwnOrTourId
    {
        get
        {
            if (authorMemberService == null ||
                string.IsNullOrWhiteSpace(authorMemberService.Mid))
            {
                return this.tourist;
            }

            return authorMemberService.Mid;
        }
    }

    /// <summary>
    /// 获取绝对路径
    /// </summary>
    /// <param name="virtualPath">文件路径</param>
    /// <returns></returns>
    protected string GetAbsolutePath(string virtualPath)
    {
        string path = virtualPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        if (path[0] == '~')
        {
            path = path.Remove(0, 2);
        }
        string rootPath = HttpContext.RequestServices.GetService<IWebHostEnvironment>().WebRootPath;

        return Path.Combine(rootPath, path);
    }
}