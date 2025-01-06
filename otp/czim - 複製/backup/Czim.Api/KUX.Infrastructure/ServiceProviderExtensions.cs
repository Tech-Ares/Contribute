using Microsoft.Extensions.DependencyInjection;
using System;

namespace KUX.Infrastructure;

/// <summary>
/// 服务提供者扩展
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    private static IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造私有化
    /// </summary>
    static ServiceProviderExtensions()
    {
        if (_serviceProvider == null)
        {
        }
    }

    /// <summary>
    /// 注册 服务提供者
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static void RegisterServiceProvider(this IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取服务提供者
    /// </summary>
    /// <returns></returns>
    public static IServiceProvider GetServiceProvider() => _serviceProvider;

    /// <summary>
    /// 创建服务域
    /// </summary>
    /// <returns></returns>
    public static IServiceScope CreateScope() => _serviceProvider.CreateScope();
}