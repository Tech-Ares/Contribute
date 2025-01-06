using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.BO;
using KUX.Models.DTO;
using KUX.Models.KuxAdmin.Request.SysUser;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 系统用户管理
/// </summary>
[ControllerDescriptor("38D864FF-F6E7-43AF-8C5C-8BBCF9FA586D")]
public class SysUserController : AdminBaseController<SysUserService>
{
    /// <summary>
    /// 系统菜单服务
    /// </summary>
    private readonly SysMenuService _sysMenuService;

    /// <summary>
    /// 系统组织服务
    /// </summary>
    private readonly SysOrganizationService _sysOrganizationService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="sysMenuService">系统菜单服务</param>
    /// <param name="sysOrganizationService">系统组织部门服务</param>
    /// <param name="_accountService">账户服务</param>
    public SysUserController(SysUserService defaultService,
        SysMenuService sysMenuService,
        SysOrganizationService sysOrganizationService, IAccountService _accountService) : base(defaultService, _accountService)
    {
        _sysMenuService = sysMenuService;
        _sysOrganizationService = sysOrganizationService;
    }

    /// <summary>
    /// 获取列表（一秒缓存）
    /// </summary>
    /// <param name="search">查询条件</param>
    /// <returns></returns>
    [ApiResourceCacheFilter(1)]
    [HttpPost("findlist")]
    public async Task<PageViewModel> FindListAsync([FromBody] UserSearchDto search)
    {
        return await this.DefaultService.FindListAsync(search);
    }
    
    //判断用户是否是超管 是超管就取sysuser，是普通客服就取chatcustomer

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
    [HttpGet("findform/{id?}")]
    [ApiResourceCacheFilter(1)]
    public async Task<Dictionary<string, object>> FindFormAsync([FromRoute] string id)
    {
        return await this.DefaultService.FindFormAsync(id);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="form">账户信息</param>
    /// <returns></returns>
    [HttpPost("saveform")]
    [ApiResourceCacheFilter(1)]
    public async Task<string> SaveFormAsync([FromBody] SysUserFormDto form)
    {
        var result = await this.DefaultService.SaveFormAsync(this.UserId, form);

        if (result)
        {
            return "账户保存成功";
        }

        return "";
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(10)]
    [HttpPost("ExportExcel")]
    public async Task<FileContentResult> ExportExcelAsync([FromBody] UserSearchDto search)
        => this.File(await this.DefaultService.ExportExcelAsync(search), Tools.GetFileContentType[".xls"].ToStr(),
            $"{Guid.NewGuid()}.xls");

    /// <summary>
    /// 获取当前账户菜单
    /// </summary>
    /// <returns></returns>
    [HttpPost("menu")]
    [ApiResourceCacheFilter(2)]
    [ActionDescriptor("MenuAsync")]
    public async Task<object> MenuAsync()
    {
        // 获取当前用户的菜单信息
        var sysMenus = await this._sysMenuService.GetMenusByUserIdAsync();

        // 生产菜单信息
        return sysMenus;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [ApiResourceCacheFilter(2)]
    [HttpGet("info")]
    public async Task<AccountInfo> GetUserInfoAsync()
    {
        var userInfo = this.accountInfo;
        var sysMenus = await this._sysMenuService.GetMenusByCurrentRoleAsync();
        //设置菜单 Map
        var sysMenusMap = this._sysMenuService.CreateMenus(string.Empty, sysMenus);
        userInfo.Menus = sysMenusMap;
        //设置菜单权限
        userInfo.MenuPowers = await this._sysMenuService.GetPowerByMenusAsync(sysMenus);

        return userInfo;
    }

    /// <summary>
    /// 获取部门树
    /// </summary>
    /// <returns></returns>
    [HttpPost("SysOrganizationTree")]
    public async Task<dynamic> GetSysDepartmentTreeAsync()
    {
        var (expandedRowKeys, data) = await this._sysOrganizationService.FindListAsync(null);

        return new
        {
            expandedRowKeys,
            rows = await this.DefaultService.GetSysDepartmentTreeAsync(data)
        };
    }
}