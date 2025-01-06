using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.SysRoleMenuFunction;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 角色菜单功能控制器
/// </summary>
[ControllerDescriptor("BD024F3A-99A7-4407-861C-A016F243F222")]
public class SysRoleMenuFunctionController : AdminBaseController<SysRoleMenuFunctionService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_accountService">账户服务</param>
    public SysRoleMenuFunctionController(SysRoleMenuFunctionService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="form">菜单方法</param>
    /// <returns></returns>
    [HttpPost("SaveForm")]
    public async Task<string> SaveFormAsync([FromBody] SysRoleMenuFunctionDto form)
    {
        if (form == null)
        {
            MessageBox.Show("请选择保存的菜单功能！");
        }

        if (string.IsNullOrWhiteSpace(form.RoleId))
        {
            MessageBox.Show("请选择角色！");
        }
        return await this.DefaultService.SaveFormAsync(form);
    }

    #region 角色菜单功能 Tree

    /// <summary>
    /// 获取菜单功能树
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetRoleMenuFunctionTree/{roleId}")]
    public async Task<object> FindRoleMenuFunctionTreeAsync(string roleId)
    {
        var (guids, objects) = await this.DefaultService.GetRoleMenuFunctionTreeAsync(roleId);

        return new { expandedRowKeys = guids, list = objects };
    }

    #endregion 角色菜单功能 Tree
}