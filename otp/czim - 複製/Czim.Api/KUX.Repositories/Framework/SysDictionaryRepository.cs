using KUX.Models.Entities.Framework;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;

namespace KUX.Repositories.Framework;

/// <summary>
/// 数据字典仓储
/// </summary>
public class SysDictionaryRepository : ServiceRepository<SysDictionary>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql">freeSql</param>
    /// <param name="dapperContext">dapper 上下文</param>
    public SysDictionaryRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }
}