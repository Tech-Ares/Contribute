using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 操作日志控制器
/// </summary>
[ControllerDescriptor("10e7b3cf-d846-4b1b-7662-08d95814698b")]
public class SysOperationLogController : AdminBaseController<SysOperationLogService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService"></param>
    /// <param name="_accountService"></param>
    public SysOperationLogController(SysOperationLogService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <param name="size"></param>
    /// <param name="page"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("FindList/{size}/{page}")]
    public async Task<PageViewModel> FindListAsync([FromRoute] int size, [FromRoute] int page, [FromBody] SysOperationLog search)
    {
        return await DefaultService.FindListAsync(page, size, search);
    }

    [HttpGet("DeleteAllData")]
    public async Task<bool> DeleteAllDataAsync()
    {
        return await DefaultService.DeletedAllData();
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
}