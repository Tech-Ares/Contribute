using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.Entities.Framework;
using KUX.Models.Request.SysMenu;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 菜单控制器
/// </summary>
[ControllerDescriptor("E5D4DA6B-AAB0-4AAA-982F-43673E8152C0")]
public class SysMenuController : AdminBaseController<SysMenuService>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="_accountService">账户服务</param>
    public SysMenuController(SysMenuService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
    {
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <returns></returns>
    [ApiResourceCacheFilter(5)]
    [HttpGet("list")]
    [ActionDescriptor("MenuListAsync", AuthCheck = false)]
    public async Task<object> MenuListAsync()
    {
        return await this.DefaultService.MenuListAsync();
    }

    /// <summary>
    /// 保存菜单
    /// </summary>
    /// <param name="form">菜单信息</param>
    /// <returns></returns>
    [HttpPost("save")]
    [ActionDescriptor("SaveMenuAsync")]
    public async Task<object> SaveMenuAsync([FromBody] SaveMenuDto form)
    {
        // 保存菜单
        var result = await this.DefaultService.SaveMenuAsync(this.accountInfo.Id, form);
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
        if (ids == null || ids.Count <= 0)
        {
            MessageBox.Show("请选择菜单！");
        }

        await this.DefaultService.DeleteListAsync(ids);
        return true;
    }

    ///// <summary>
    ///// 获取列表
    ///// </summary>
    ///// <param name="size"></param>
    ///// <param name="page"></param>
    ///// <param name="search"></param>
    ///// <returns></returns>
    ////[ApiResourceCacheFilter(1)]
    //[HttpPost("FindList/{size}/{page}")]
    //public async Task<PageViewModel> FindListAsync([FromRoute] int size, [FromRoute] int page, [FromBody] SysMenu search)
    //{
    //    return await this.DefaultService.FindListAsync(page, size, search);
    //}

    ///// <summary>
    ///// 查询表单数据
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet("FindForm/{id?}")]
    //public async Task<Dictionary<string, object>> FindFormAsync([FromRoute] string id)
    //{
    //    return await this.DefaultService.FindFormAsync(id);
    //}

    ///// <summary>
    ///// 保存
    ///// </summary>
    ///// <param name="form"></param>
    ///// <returns></returns>
    //[HttpPost("SaveForm")]
    //public async Task<SysMenu> SaveFormAsync([FromBody] SysMenuFormDto form)
    //{
    //    return await this.DefaultService.SaveFormAsync(form);
    //}

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [ApiResourceCacheFilter(10)]
    [HttpPost("ExportExcel")]
    public async Task<FileContentResult> ExportExcelAsync([FromBody] SysMenu search)
        => this.File(await this.DefaultService.ExportExcelAsync(search), Tools.GetFileContentType[".xls"].ToStr(),
            $"{Guid.NewGuid()}.xls");

    #region 获取 菜单 树

    /// <summary>
    /// 获取菜单列表 以及 页面按钮权限
    /// </summary>
    /// <returns></returns>
    [HttpGet("FindSysMenuTree")]
    public async Task<object> FindSysMenuTreeAsync()
    {
        var allList = await DefaultService.GetMenusByCurrentRoleAsync();

        return new
        {
            userName = this.accountInfo.Name,
            list = this.DefaultService.CreateMenus(string.Empty, allList),
            allList,
            powerState = await this.DefaultService.GetPowerByMenusAsync(allList)
        };
    }

    /// <summary>
    /// 获取菜单功能树
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMenusFunctionTree")]
    public async Task<object> FindMenuFunctionTreeAsync()
    {
        var menuFunctionTree = await this.DefaultService.GetMenuFunctionTreeAsync();

        return new
        {
            tree = menuFunctionTree.Item1,
            defaultExpandedKeys = menuFunctionTree.Item2,
            defaultCheckedKeys = menuFunctionTree.Item3
        };
    }

    #endregion 获取 菜单 树
}