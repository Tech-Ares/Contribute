using FreeSql;
using System;
using System.Linq.Expressions;

namespace KUX.Repositories.Core.FreeSqlUtil;

/// <summary>
/// FreeSql 默认仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class FreeSqlBaseRepository<TEntity> : BaseRepository<TEntity> where TEntity : class
{
    public FreeSqlBaseRepository(IFreeSql fsql, Expression<Func<TEntity, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
    {
    }
}

/// <summary>
/// App服务默认仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class AppBaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey> where TEntity : class
{
    public AppBaseRepository(IFreeSql fsql, Expression<Func<TEntity, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
    {
    }
}