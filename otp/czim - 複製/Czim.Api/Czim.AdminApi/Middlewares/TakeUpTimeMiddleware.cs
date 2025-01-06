﻿using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Czim.AdminApi.Middlewares;

/// <summary>
/// 统计耗时 并记录日志中 中间件
/// </summary>
public class TakeUpTimeMiddleware : IMiddleware
{
    /// <summary>
    /// 计时器
    /// </summary>
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// 日志服务
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// 操作日志服务
    /// </summary>
    private readonly SysOperationLogService _sysOperationLogService;

    /// <summary>
    /// 请求上下文
    /// </summary>
    private readonly HttpContext _httpContext;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="logger">日志</param>
    /// <param name="sysOperationLogService">操作日志服务</param>
    /// <param name="iHttpContextAccessor">请求上下文</param>
    public TakeUpTimeMiddleware(ILogger<TakeUpTimeMiddleware> logger, SysOperationLogService sysOperationLogService, IHttpContextAccessor iHttpContextAccessor)
    {
        this._stopwatch ??= new Stopwatch();
        _logger = logger;
        _sysOperationLogService = sysOperationLogService;
        _httpContext = iHttpContextAccessor.HttpContext;
    }

    /// <summary>
    /// 中间件执行
    /// </summary>
    /// <param name="context">请求上下文</param>
    /// <param name="next">下一个管道方法</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //获取body
        var bodyString = await this.ReadBodyAsync();

        //记录 api 执行耗时
        _stopwatch.Restart();

        await next.Invoke(context);

        _stopwatch.Stop();

        if (this.IsApi(context))
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;
            var path = context.Request.Path;
            var log = $"{remoteIpAddress} 请求：{path} 耗时：{_stopwatch.ElapsedMilliseconds} 毫秒!";
            _logger.LogInformation(log);

            await _sysOperationLogService.WriteInLogAsync(_stopwatch.ElapsedMilliseconds, bodyString);
        }
    }

    /// <summary>
    /// 判断请求类型 是否 是 api
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private bool IsApi(HttpContext context)
    {
        var contentTypes = new[] { "application/json", "text/html" };
        if (string.IsNullOrWhiteSpace(context.Response.ContentType))
        {
            return false;
        }
        return contentTypes.Any(w => context.Response.ContentType.StartsWith(w));
    }

    /// <summary>
    /// 读取 body 信息
    /// </summary>
    /// <returns></returns>
    private async Task<string> ReadBodyAsync()
    {
        //获取body
        _httpContext.Request.EnableBuffering();//可以实现多次读取Body
        var sr = new StreamReader(_httpContext.Request.Body);
        var bodyString = await sr.ReadToEndAsync();
        _httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

        return bodyString;
    }
}