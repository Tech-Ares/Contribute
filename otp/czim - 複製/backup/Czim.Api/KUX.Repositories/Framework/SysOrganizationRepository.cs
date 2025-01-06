using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

/// <summary>
/// 组织仓储
/// </summary>
public class SysOrganizationRepository : ServiceRepository<SysOrganization>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql">freeSql</param>
    /// <param name="dapperContext">dapper上下文</param>
    public SysOrganizationRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}