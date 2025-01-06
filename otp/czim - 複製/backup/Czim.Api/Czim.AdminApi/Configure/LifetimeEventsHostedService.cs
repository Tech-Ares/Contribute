using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Czim.AdminApi.Configure;

/// <summary>
/// 生命周期监听
/// </summary>
public class LifetimeEventsHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="logger">日志</param>
    /// <param name="appLifetime">生命周期</param>
    public LifetimeEventsHostedService(
        ILogger<LifetimeEventsHostedService> logger,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
    }

    /// <summary>
    /// 创建监听
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 服务停止
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 服务启动
    /// </summary>
    private void OnStarted()
    {
        _logger.LogInformation("应用启动.");

        // Perform post-startup activities here
    }

    /// <summary>
    /// 服务停止中
    /// </summary>
    private void OnStopping()
    {
        _logger.LogInformation("应用停止中.");

        // Perform on-stopping activities here
    }

    /// <summary>
    /// 服务停止之后
    /// </summary>
    private void OnStopped()
    {
        _logger.LogInformation("应用已停止.");

        // Perform post-stopped activities here
    }
}