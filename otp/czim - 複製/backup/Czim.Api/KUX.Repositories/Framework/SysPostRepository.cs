using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

/// <summary>
/// 系统岗位表
/// </summary>
public class SysPostRepository : ServiceRepository<SysPost>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql"></param>
    /// <param name="dapperContext"></param>
    public SysPostRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}