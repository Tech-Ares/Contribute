using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

public class SysRoleMenuFunctionRepository : ServiceRepository<SysRoleMenuFunction>
{
    public SysRoleMenuFunctionRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}