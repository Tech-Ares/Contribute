using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

/// <summary>
/// 用户岗位仓储
/// </summary>
public class SysUserPostRepository : ServiceRepository<SysUserPost>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql">freesql</param>
    /// <param name="dapperContext">dapper 上下文</param>
    public SysUserPostRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}