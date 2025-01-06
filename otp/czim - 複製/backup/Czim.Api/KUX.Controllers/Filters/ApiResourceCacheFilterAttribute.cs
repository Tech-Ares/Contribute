using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace KUX.Controllers.Filters;

/// <summary>
/// 接口资源加载过滤 拦截资源做缓存
/// 防无限调用
/// </summary>
public class ApiResourceCacheFilterAttribute : Attribute, IResourceFilter
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    private IMemoryCache _memoryCache;

    /// <summary>
    /// 缓存时间
    /// </summary>
    private readonly double _cacheTime;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="cacheTime">默认缓存时间（秒）</param>
    public ApiResourceCacheFilterAttribute(double cacheTime = 30)
    {
        _cacheTime = cacheTime;
    }

    /// <summary>
    /// 资源加载之前
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        //获取当前缓存服务
        _memoryCache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;

        // 生成资源key
        var apiResourceCacheKey = GetCacheKey(context.HttpContext);
        // 获取资源缓存
        var result = _memoryCache.Get(apiResourceCacheKey);

        if (result != null)
        {
            // 使用缓存
            context.Result = result as IActionResult;
        }
    }

    /// <summary>
    /// 资源加载之后
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        _memoryCache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;

        var apiResourceCacheKey = GetCacheKey(context.HttpContext);
        var result = _memoryCache.Get(apiResourceCacheKey);

        if (result == null && context.Result != null)
        {
            // 添加缓存
            _memoryCache.Set(apiResourceCacheKey, context.Result, DateTime.Now.AddSeconds(_cacheTime));
        }
    }

    /// <summary>
    /// 获取每个唯一请求的缓存 key
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private string GetCacheKey(HttpContext context)
    {
        var requestId = context.TraceIdentifier.Split(':')[0];
        return $"{requestId}=>{context.Request.Path.ToString()?.ToLower()}";
    }
}