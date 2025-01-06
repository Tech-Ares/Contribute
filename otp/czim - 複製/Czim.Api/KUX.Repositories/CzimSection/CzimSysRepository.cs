using KUX.Models.CzimSection;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.CzimSection
{
    /// <summary>
    /// 系统设置仓储
    /// </summary>
    public class CzimSysRepository : ServiceRepository<CzimAppVersion>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="freeSql">freeSql</param>
        /// <param name="_dapperContext">dapper</param>
        public CzimSysRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }
    }
}