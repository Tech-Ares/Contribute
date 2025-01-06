using KUX.Models.CzimApp.Reponse.AppLive;
using KUX.Models.CzimApp.Request.AppLive;
using KUX.Repositories.CzimSection;
using KUX.Models.CzimSection;

namespace KUX.Services.App.CzimApp;

/// <summary>
/// App 系统服务
/// </summary>
public class CzimLiveService : AppBaseService<CzimSysRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public CzimLiveService(CzimSysRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 查询所有直播数据
    /// </summary>
    /// <param name="lsd">直播信息查询</param>
    /// <returns></returns>
    public async Task<List<AppAllLiveVo>> AllLiveAsync(LiveSearchDto lsd)
    {
        var result = await this.Repository.Orm.Select<CzimLiveInfo>()
                                        .Where(w => w.Title.Contains(lsd.LiveContent) || w.Content.Contains(lsd.LiveContent))
                                        .Where(w => w.IsActive)
                                        .ToListAsync(t => new AppAllLiveVo()
                                        {

                                            Id = t.Id,
                                            Title = t.Title,
                                            Content = t.Content,
                                            CrtDate = t.CrtDate,
                                            HeadPicture = t.HeadPicture,
                                            VideoUrl = t.VideoUrl,
                                            LiveStatus = t.LiveStatus,
                                            GiveLikeCount = t.GiveLikeCount
                                        });

        return result;
    }
}