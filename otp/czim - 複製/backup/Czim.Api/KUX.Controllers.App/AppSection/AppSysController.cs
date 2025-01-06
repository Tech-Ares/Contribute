using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimApp.Reponse.AppSystem;
using KUX.Models.CzimApp.Request.AppSystem;
using KUX.Services.App.Accounts;
using KUX.Services.App.CzimApp;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers.App.AppSection;

/// <summary>
/// 系统服务
/// </summary>
public class AppSysController : AppBaseController<CzimSysService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_authorMemberService">用户授权服务</param>
    public AppSysController(CzimSysService defaultService,
        AuthorMemberService _authorMemberService) : base(defaultService, _authorMemberService)
    {
    }

    /// <summary>
    /// 版本查询
    /// </summary>
    /// <returns></returns>
    [HttpPost("versioncheck")]
    [ApiResourceCacheFilter(1)]
    [ActionDescriptor("VersionCheckAsync", AuthCheck = false)]
    public async Task<SystemVersionVo> VersionCheckAsync(SystemVersionDto systemVersionDto)
    {
        var version = await this.DefaultService.VersionCheckAsync(systemVersionDto);
        if (version?.Id > 0)
        {
            return version;
        }
        MessageBox.Show("无版本数据");
        return default;
        //return this.ResultWarn<SystemVersionVo>("无版本数据！", null);
    }

    //通过版本号查询隐藏内容是否可以发布
    /// <summary>
    /// 通过版本号判断隐藏内容是否可以发布
    /// </summary>
    /// <param name="svd"></param>
    /// <returns></returns>
    [HttpPost("verhide")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("VersionHideAsync", AuthCheck = false)]
    public async Task<bool> VersionHideAsync([FromBody] SystemVersionDto svd)
    {
        if (svd == null || string.IsNullOrWhiteSpace(svd.Version))
        {
            return false;
        }

        if (svd.PhoneSystem == 1)
        {
            return true;
        }

        var ver = await this.DefaultService.VersionHideAsync(svd);

        return ver;
    }

    ///// <summary>
    ///// 地区查询
    ///// </summary>
    ///// <returns></returns>
    //[HttpPost("checkarea")]
    //[ActionDescriptor("GetAreaByTypeAsync")]
    //public async Task<ApiResult<TCountVo<SysAreaCodeAbbreVo>>> GetAreaByTypeAsync()
    //{
    //    var areas = await this.DefaultService.GetAreaByTypeAsync();

    //    return this.ResultOk("查询成功！", areas);
    //}
}