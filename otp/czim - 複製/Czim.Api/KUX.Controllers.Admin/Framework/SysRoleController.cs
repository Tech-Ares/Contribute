using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.Entities.Framework;
using KUX.Models.KuxAdmin.Request.SysRole;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 角色控制器
/// </summary>
[ControllerDescriptor("60AE9382-31AB-4276-A379-D3876E9BB783")]
public class SysRoleController : AdminBaseController<SysRoleService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_accountService">账户服务</param>
    public SysRoleController(SysRoleService defaultService, IAccountService _accountService)
        : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(1)]
    [HttpPost("findlist")]
    public async Task<PageViewModel> FindListAsync([FromBody] RoleSearchDto search)
    {
        return await this.DefaultService.FindListAsync(search);
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id">角色id</param>
    /// <returns></returns>
    [HttpGet("findform/{id?}")]
    [ApiResourceCacheFilter(1)]
    public async Task<Dictionary<string, object>> FindFormAsync([FromRoute] string id)
    {
        var result = await this.DefaultService.FindFormAsync(id);
        return result;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="form">角色对象</param>
    /// <returns></returns>
    [HttpPost("saveform")]
    public async Task<SysRole> SaveFormAsync([FromBody] SysRole form)
    {
        // 保存对象
        if (form == null)
        {
            MessageBox.Show("请填写正确对象");
        }

        if (string.IsNullOrWhiteSpace(form.Name))
        {
            MessageBox.Show("名称必填写");
        }

        var result = await this.DefaultService.SaveFormAsync(this.UserId, form);

        return result;
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost("deletelist")]
    public async Task<bool> DeleteListAsync([FromBody] List<string> ids)
    {
        await this.DefaultService.DeleteListAsync(ids);
        return true;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(10)]
    [HttpPost("ExportExcel")]
    public async Task<FileContentResult> ExportExcelAsync([FromBody] RoleSearchDto search)
        => this.File(await this.DefaultService.ExportExcelAsync(search), Tools.GetFileContentType[".xls"].ToStr(), $"{Guid.NewGuid()}.xls");
}