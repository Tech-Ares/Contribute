﻿using KUX.Infrastructure.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace KUX.Repositories.Redis;

/// <summary>
/// Redis 模块
/// </summary>
public class RepositoryRedisModule
{
    /// <summary>
    /// 注册 Redis 模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    public static void RegisterRedisRepository(IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IRedisService, RedisService>(serviceProvider => new RedisService(connectionString));
    }
}