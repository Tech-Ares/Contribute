using KUX.Infrastructure.MessageQueue.Models;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KUX.Infrastructure.MessageQueue;

/// <summary>
/// 消息队列提供者
/// </summary>
public class MessageQueueProvider : IMessageQueueProvider
{
    /// <summary>
    /// 线程安全集合类
    /// </summary>
    private readonly BlockingCollection<MessageQueueContext> blockingCollection;

    /// <summary>
    /// 信号
    /// </summary>
    private static ManualResetEvent _mre = new ManualResetEvent(false);

    /// <summary>
    /// 服务提供商
    /// </summary>
    private readonly IServiceProvider _services;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="services">服务提供者</param>
    public MessageQueueProvider(IServiceProvider services)
    {
        blockingCollection = new BlockingCollection<MessageQueueContext>();
        _services = services;
    }

    /// <summary>
    /// 是否完成
    /// </summary>
    /// <returns></returns>
    private bool IsComleted() => blockingCollection.IsCompleted;

    /// <summary>
    /// 执行队列方法
    /// </summary>
    /// <returns></returns>
    public virtual Task<bool> RunAsync()
    {
        Task.Factory.StartNew(() =>
        {
            //从队列中取元素。
            while (true)
            {
                try
                {
                    if (!IsComleted() && blockingCollection.Count > 0)
                    {
                        var messageQueueModel = blockingCollection.Take();
                        messageQueueModel.Call?.Invoke(messageQueueModel.Message, _services);
                        Console.WriteLine("消费:" + messageQueueModel.Key);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _mre.Reset();

                _mre.WaitOne();
                Thread.Sleep(1);
            }
        });

        return Task.FromResult(true);
    }

    /// <summary>
    /// 发布消息队列
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="message">消息内容</param>
    /// <param name="action">回调方法</param>
    /// <returns></returns>
    public virtual Task<bool> SendMessageQueueAsync(string key, object message, Action<object, IServiceProvider> action)
    {
        blockingCollection.Add(new MessageQueueContext(key, message, action));
        _mre.Set();
        return Task.FromResult(true);
    }
}