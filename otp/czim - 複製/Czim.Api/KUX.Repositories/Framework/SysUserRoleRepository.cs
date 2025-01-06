using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

/// <summary>
/// 系统用户角色仓储
/// </summary>
public class SysUserRoleRepository : ServiceRepository<SysUserRole>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql"></param>
    /// <param name="dapperContext"></param>
    public SysUserRoleRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}