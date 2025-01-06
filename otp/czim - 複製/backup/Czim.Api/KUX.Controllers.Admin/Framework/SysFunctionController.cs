using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 功能控制器
/// </summary>
[ControllerDescriptor("D721FC55-2174-40EB-BB37-5C54A158525A")]
public class SysFunctionController : AdminBaseController<SysFunctionService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService"></param>
    /// <param name="_accountService"></param>
    public SysFunctionController(SysFunctionService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="size"></param>
    /// <param name="page"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(1)]
    [HttpPost("FindList/{size}/{page}")]
    public async Task<PageViewModel> FindListAsync([FromRoute] int size, [FromRoute] int page, [FromBody] SysFunction search)
    {
        return await this.DefaultService.FindListAsync(page, size, search);
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost("DeleteList")]
    public async Task<bool> DeleteListAsync([FromBody] List<string> ids)
    {
        await this.DefaultService.DeleteListAsync(ids);
        return true;
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("FindForm/{id?}")]
    public async Task<Dictionary<string, object>> FindFormAsync([FromRoute] string id)
    {
        return await this.DefaultService.FindFormAsync(id);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("SaveForm")]
    public async Task<SysFunction> SaveFormAsync([FromBody] SysFunction form)
    {
        return await this.DefaultService.SaveFormAsync(form);
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(10)]
    [HttpPost("ExportExcel")]
    public async Task<FileContentResult> ExportExcelAsync([FromBody] SysFunction search)
        => this.File(await this.DefaultService.ExportExcelAsync(search), Tools.GetFileContentType[".xls"].ToStr(), $"{Guid.NewGuid()}.xls");
}