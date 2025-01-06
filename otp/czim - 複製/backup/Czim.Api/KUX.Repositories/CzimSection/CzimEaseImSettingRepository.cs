using KUX.Models.CzimSection;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.CzimSection
{
    /// <summary>
    /// 环信配置仓储仓储
    /// </summary>
    public class CzimEaseImSettingRepository : ServiceRepository<CzimEaseImSetting>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="freeSql">freeSql</param>
        /// <param name="_dapperContext">dapper</param>
        public CzimEaseImSettingRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }
    }
}