using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimApp.Reponse.AppLive;
using KUX.Models.CzimApp.Request.AppLive;
using KUX.Services.App.Accounts;
using KUX.Services.App.CzimApp;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers.App.AppSection;

/// <summary>
/// 发现（直播）服务
/// </summary>
public class AppLiveController : AppBaseController<CzimLiveService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_authorMemberService">用户授权服务</param>
    public AppLiveController(CzimLiveService defaultService,
        AuthorMemberService _authorMemberService) : base(defaultService, _authorMemberService)
    {
    }

    /// <summary>
    /// 查询所有直播数据
    /// </summary>
    /// <param name="lsd">查询数据</param>
    /// <returns></returns>
    [HttpPost("alllive")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("AllLiveAsync", AuthCheck = false)]
    public async Task<List<AppAllLiveVo>> AllLiveAsync([FromBody] LiveSearchDto lsd)
    {
        var lives = await this.DefaultService.AllLiveAsync(lsd);
        if (lives?.Count > 0)
        {
            return lives;
        }
        return default;
    }
}