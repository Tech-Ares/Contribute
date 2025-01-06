
using KUX.Models.CzimSection;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Repositories.CzimSection
{
    /// <summary>
    /// 直播仓储
    /// </summary>
    public class CzimLiveInfoReposity : ServiceRepository<CzimChatPreset>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="freeSql">orm</param>
        /// <param name="_dapperContext">orm</param>
        public CzimLiveInfoReposity(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {

        }
    }
}
