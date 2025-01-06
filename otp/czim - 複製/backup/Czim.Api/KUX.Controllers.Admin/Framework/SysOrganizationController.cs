using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.Entities.Framework;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 组织机构控制器
/// </summary>
[ControllerDescriptor("0ABFD53B-6BDE-42C6-9F99-E32775BC31DD")]
public class SysOrganizationController : AdminBaseController<SysOrganizationService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService"></param>
    /// <param name="_accountService"></param>
    public SysOrganizationController(SysOrganizationService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpPost("FindList")]
    public async Task<dynamic> FindListAsync([FromBody] SysOrganization search)
    {
        var (expandedRowKeys, data) = await this.DefaultService.FindListAsync(search);

        return new
        {
            expandedRowKeys,
            rows = data
        };
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
    /// <param name="parentId"></param>
    /// <returns></returns>
    [HttpGet("FindForm/{id?}/{parentId?}")]
    public async Task<Dictionary<string, object>> FindFormAsync([FromRoute] string id, string parentId)
    {
        return await this.DefaultService.FindFormAsync(id, parentId);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("SaveForm")]
    public async Task<SysOrganization> SaveFormAsync([FromBody] SysOrganization form)
    {
        return await this.DefaultService.SaveFormAsync(form);
    }
}