using KUX.Infrastructure;
using KUX.Models.CzimApp.Reponse.AppSystem;
using KUX.Models.CzimApp.Request.AppSystem;
using KUX.Models.CzimSection;
using KUX.Repositories.CzimSection;

namespace KUX.Services.App.CzimApp;

/// <summary>
/// App 系统服务
/// </summary>
public class CzimSysService : AppBaseService<CzimSysRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public CzimSysService(CzimSysRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 检查当前版本
    /// </summary>
    /// <param name="systemVersionDto">系统版本信息</param>
    /// <returns></returns>
    public async Task<SystemVersionVo> VersionCheckAsync(SystemVersionDto svd)
    {
        var result = await this.Repository.Orm.Select<CzimAppVersion>()
                                .Where(w => w.PhoneSystem == svd.PhoneSystem)
                                .Where(w => w.IsActive)
                                .OrderByDescending(o => o.Id)
                                .FirstAsync();

        if (result != null && result.Id > 0)
        {
            return result.MapTo<CzimAppVersion, SystemVersionVo>();
        }
        return default;
    }

    /// <summary>
    /// 通过版本号判断是否显示隐藏信息
    /// </summary>
    /// <param name="svd">版本信息</param>
    /// <returns></returns>
    public async Task<bool> VersionHideAsync(SystemVersionDto svd)
    {
        var result = await this.Repository.Orm.Select<CzimAppVersion>()
                                    .Where(w => w.IsActive && w.PhoneSystem == 2 && w.Version == svd.Version)
                                    .AnyAsync();

        if (result)
        {
            return true;
        }
        return false;
    }
}