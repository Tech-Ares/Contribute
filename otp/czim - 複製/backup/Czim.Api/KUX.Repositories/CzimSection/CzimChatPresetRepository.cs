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
    /// 预设聊天消息
    /// </summary>
    public class CzimChatPresetRepository : ServiceRepository<CzimChatPreset>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="freeSql">freesql</param>
        /// <param name="_dapperContext">dapper</param>
        public CzimChatPresetRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }
    }
}
