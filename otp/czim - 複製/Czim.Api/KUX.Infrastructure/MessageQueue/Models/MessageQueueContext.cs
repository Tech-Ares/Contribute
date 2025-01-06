using System;

namespace KUX.Infrastructure.MessageQueue.Models;

/// <summary>
/// 消息队列上下文
/// </summary>
public class MessageQueueContext
{
    public MessageQueueContext(string key, object message, Action<object, IServiceProvider> call)
    {
        Key = key;
        Message = message;
        Call = call;
    }

    /// <summary>
    /// 队列主键
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public object Message { get; set; }

    /// <summary>
    /// 队列回调函数
    /// </summary>
    public Action<object, IServiceProvider> Call { get; set; }
}