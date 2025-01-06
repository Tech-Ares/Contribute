using KUX.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KUX.Controllers;

/// <summary>
/// 公共接口控制器
/// </summary>
[ApiResultFilter]
[Route("api/[controller]")]
public class PublicController : ControllerBase
{
    //private readonly SysDictionaryService _sysDictionaryService;

    //public PublicController(SysDictionaryService sysDictionaryService)
    //{
    //    _sysDictionaryService = sysDictionaryService;
    //}

    ///// <summary>
    ///// 根据编码获取 字典集合
    ///// </summary>
    ///// <param name="code"></param>
    ///// <returns></returns>
    //[HttpGet("GetDictionary/{code}")]
    //public Task<List<SysDictionaryDto>> GetDictionaryByCodeAsync([FromRoute] string code)
    //{
    //    return this._sysDictionaryService.GetDictionaryByCodeAsync(code);
    //}
}