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
    /// 客服仓储
    /// </summary>
    public class CzimChatCustomerRepository : ServiceRepository<CzimChatCustomer>
    {
        public CzimChatCustomerRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }
    }
}
