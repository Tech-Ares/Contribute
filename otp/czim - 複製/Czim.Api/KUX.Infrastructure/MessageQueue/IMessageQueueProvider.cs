using KUX.Infrastructure.ScanDIService.Interface;
using System;
using System.Threading.Tasks;

namespace KUX.Infrastructure.MessageQueue;

/// <summary>
/// 消息队列提供者
/// </summary>
public interface IMessageQueueProvider : IDISingleton
{
    /// <summary>
    /// 启动运行
    /// </summary>
    /// <returns></returns>
    Task<bool> RunAsync();

    /// <summary>
    /// 发布消息队列
    /// </summary>
    /// <param name="key">关键key</param>
    /// <param name="message">消息内容</param>
    /// <param name="action">回调方法</param>
    /// <returns></returns>
    Task<bool> SendMessageQueueAsync(string key, object message, Action<object, IServiceProvider> action);
}