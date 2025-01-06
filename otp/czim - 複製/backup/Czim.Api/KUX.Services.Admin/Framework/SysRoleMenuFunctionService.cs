using KUX.Models.BO;
using KUX.Models.CzimAdmin.Request.SysRoleMenuFunction;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 角色 菜单 功能服务
/// </summary>
public class SysRoleMenuFunctionService : AdminBaseService<SysRoleMenuFunctionRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public SysRoleMenuFunctionService(SysRoleMenuFunctionRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<string> SaveFormAsync(SysRoleMenuFunctionDto form)
    {
        var roleId = form.RoleId;
        // 删除旧数据
        await this.Repository.DeleteAsync(w => w.RoleId == roleId);

        List<SysRoleMenuFunction> smfList = new List<SysRoleMenuFunction>();

        var crmflist = CreatRoleMenuFunction(form.RoleMenuFunctionList, roleId);

        smfList.AddRange(crmflist);

        if (smfList.Count > 0)
        {
            await this.Repository.InsertAsync(smfList);
        }

        return roleId;
    }

    /// <summary>
    /// 创建角色菜单方法
    /// </summary>
    /// <param name="form"></param>
    /// <param name="roleId"></param>
    private static List<SysRoleMenuFunction> CreatRoleMenuFunction(List<SysRoleMenuInfoBo> form, string roleId)
    {
        List<SysRoleMenuFunction> smfList = new List<SysRoleMenuFunction>();
        form?.ForEach(menuFunction =>
        {
            if (menuFunction.Functions?.Count > 0)
            {
                foreach (var func in menuFunction.Functions)
                {
                    if (func.IsSelected)
                    {
                        SysRoleMenuFunction smf = new SysRoleMenuFunction()
                        {
                            MenuId = menuFunction.MenuId,
                            FunctionId = func.FuncId,
                            RoleId = roleId,
                            IsActive = true,
                            CDate = DateTime.Now,
                            UDate = DateTime.Now
                        };
                        smfList.Add(smf);
                    }
                }
            }
            if (menuFunction.Children?.Count > 0)
            {
                smfList.AddRange(CreatRoleMenuFunction(menuFunction.Children, roleId));
            }
        });
        return smfList;
    }

    #region 角色菜单功能 Tree

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    /// <param name="roleId">角色id</param>
    /// <returns></returns>
    public async Task<(string, List<SysRoleMenuInfoBo>)> GetRoleMenuFunctionTreeAsync(string roleId)
    {
        // 菜单列表
        var sysMenuList = await this.Repository.Orm.Select<SysMenu>()
                                                .Where(w => w.IsActive)
                                                .OrderBy(o => o.Number).ToListAsync();

        // 系统方法列表
        var sysFunctionList = await this.Repository.Orm.Select<SysFunction>()
                                        .Where(w => w.IsActive)
                                        .OrderBy(o => o.Number)
                                        .ToListAsync();

        // 菜单方法列表
        var sysMenuFunctionList = await this.Repository.Orm.Select<SysMenuFunction>()
                                                .OrderBy(w => w.CDate)
                                                .ToListAsync();

        // 循环菜单，判断方法时候可用

        //SysRoleMenuInfoBo

        // 角色菜单方法
        var sysRoleMenuFunctionList = await this.Repository.Select
                                                .Where(w => w.RoleId == roleId && w.IsActive)
                                                .ToListAsync();

        // 所有选中菜单
        var selectMenuList = sysRoleMenuFunctionList.Select(s => s.MenuId).Distinct().ToList();

        var result = this.CreateRoleMenuFunctionTree(roleId, "", sysMenuList, sysFunctionList,
                            sysMenuFunctionList,
                            sysRoleMenuFunctionList);

        return (roleId, result);
    }

    /// <summary>
    /// 创建菜单树
    /// </summary>
    /// <param name="roleId">角色id</param>
    /// <param name="pid">父组件</param>
    /// <param name="sysMenuAllList">菜单列表</param>
    /// <param name="sysFunctionList">方法列表</param>
    /// <param name="sysMenuFunctionList">菜单方法列表</param>
    /// <param name="sysRoleMenuFunctionList">权限菜单方法列表</param>
    /// <returns></returns>
    private List<SysRoleMenuInfoBo> CreateRoleMenuFunctionTree(string roleId, string pid, List<SysMenu> sysMenuAllList,
        List<SysFunction> sysFunctionList, List<SysMenuFunction> sysMenuFunctionList,
        List<SysRoleMenuFunction> sysRoleMenuFunctionList)
    {
        //权限菜单信息
        List<SysRoleMenuInfoBo> sysRoleMenuInfoBos = new List<SysRoleMenuInfoBo>();

        // 获取菜单
        var menus = string.IsNullOrWhiteSpace(pid)
             ? sysMenuAllList.Where(w => string.IsNullOrWhiteSpace(w.ParentId)).ToList()
             : sysMenuAllList.Where(w => w.ParentId == pid).ToList();

        // 循环菜单，获取菜单信息
        foreach (var item in menus)
        {
            SysRoleMenuInfoBo srmib = new SysRoleMenuInfoBo()
            {
                MenuId = item.Id,
                Title = item.Title,
                Number = item.Number.Value,
                ParentId = item.ParentId,
            };

            srmib.Children = this.CreateRoleMenuFunctionTree(roleId, item.Id, sysMenuAllList, sysFunctionList,
                sysMenuFunctionList,
                sysRoleMenuFunctionList);

            if (srmib.Children?.Count <= 0)
            {
                // 增加菜单方法
                srmib.Functions = new List<SysRoleMenuFunctionBo>();

                sysFunctionList.ForEach(f =>
                {
                    SysRoleMenuFunctionBo srmfb = new SysRoleMenuFunctionBo()
                    {
                        FuncId = f.Id,
                        IsSelected = sysRoleMenuFunctionList.Any(a => a.MenuId == item.Id && a.RoleId == roleId && a.FunctionId == f.Id),
                        CanUse = sysMenuFunctionList.Any(a => a.MenuId == item.Id),
                        Number = f.Number.Value,
                        Title = f.Title,
                    };

                    srmib.Functions.Add(srmfb);
                });
            }

            sysRoleMenuInfoBos.Add(srmib);
        }
        return sysRoleMenuInfoBos;
    }

    #endregion 角色菜单功能 Tree
}