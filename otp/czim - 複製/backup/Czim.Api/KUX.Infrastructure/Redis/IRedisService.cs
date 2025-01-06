using StackExchange.Redis;
using System;

namespace KUX.Infrastructure.Redis;

/// <summary>
/// redis 缓存接口
/// </summary>
public interface IRedisService : IDisposable
{
    //IDatabase Database { get; }

    IDatabase GetDatabase(int db);

    IConnectionMultiplexer Multiplexer { get; }

    /// <summary>
    /// redis服务
    /// </summary>
    IServer iServer { get; }

    ///// <summary>
    ///// 设置默认库
    ///// </summary>
    ///// <param name="db"></param>
    //void SetDefaultDataBase(int db);
}