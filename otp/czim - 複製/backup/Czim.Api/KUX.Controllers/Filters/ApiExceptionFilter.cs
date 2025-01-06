using KUX.Models.ApiResultManage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace KUX.Controllers.Filters;

/// <summary>
/// 异常过滤器
/// </summary>
public class ApiExceptionFilter : IExceptionFilter, IOrderedFilter
{
    /// <summary>
    /// 日志文件
    /// </summary>
    private readonly ILogger<ApiExceptionFilter> _logger;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="logger">本地log服务</param>
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public int Order { get; set; } = int.MaxValue - 10;

    /// <summary>
    /// 异常信息
    /// </summary>
    /// <param name="context">异常上下文</param>
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is MessageBox error)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new JsonResult(error.GetApiResult());
            return;
        }

        if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new JsonResult(ApiResult.ResultMessage(ApiResultCodeEnum.UnAuth, "未授权!"));
            return;
        }

        //nlog 写入日志到 txt
        _logger.LogError(exception, context.HttpContext.Connection.RemoteIpAddress?.ToString());
        var message = $"服务端出现异常![异常消息：{exception.Message}]";

        // 异常重写
        var apiResult = ApiResult.ResultMessage(ApiResultCodeEnum.Error, message);
        context.Result = new JsonResult(apiResult);
    }
}