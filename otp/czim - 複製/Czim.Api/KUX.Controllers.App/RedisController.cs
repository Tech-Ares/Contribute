using KUX.Controllers.Filters;
using KUX.Infrastructure.Redis;
using KUX.Models.CzimApp.AppBo;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers.App;

/// <summary>
/// 测试redis
/// </summary>
[ApiResultFilter]
[Route("api/[controller]")]
public class RedisController : ControllerBase
{
    ///// <summary>
    ///// 缓存服务
    ///// </summary>
    //private readonly IRedisService redisService;

    //public RedisController(IRedisService _redisService)
    //{
    //    redisService = _redisService;
    //}

    ///// <summary>
    ///// 测试 消息订阅
    ///// </summary>
    ///// <param name="key"></param>
    ///// <returns></returns>
    //[HttpGet("{key}")]
    //public string Test(string key)
    //{
    //    var result = redisService.FindByKeyAsync<string>(key, 7).Result;

    //    var test = redisService.AddOrUpdateByKeyAsync<string>(key, "admin123", DateTime.Now.AddMinutes(1), 7).Result;

    //    return result.ToString();
    //}
}