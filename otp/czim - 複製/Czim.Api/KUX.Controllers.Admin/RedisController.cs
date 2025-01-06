using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Services.EaseImServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin;

[ApiResultFilter]
[Route("api/[controller]")]
public class RedisController : AdminBaseController
{
    //private readonly BaseRedisRepository _redisRepository;

    private EaseImService easeImService;

    public RedisController(EaseImService _easeImService)
    {
        easeImService = _easeImService; ;
    }

    /// <summary>
    /// 测试 消息订阅
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet("{key}")]
    public async Task<string> Test(string key)
    {
        //天数转换为秒
        var millisecond = 1 * 24 * 60 * 60 * 1000;
        //当前时间减去1970的毫秒数
        var timp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        var sum = millisecond + timp;

        await easeImService.ChatGroupMuteUser(new System.Collections.Generic.List<string>() { "86933e7560a74d6e8c80f7f8f60ba56b" },"170662861799425", sum);

        return "调用成功!";
    }
}