using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.BO;
using KUX.Models.DTO;
using KUX.Models.Entities.Framework;
using KUX.Models.Request.SysMenu;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Consts;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 菜单服务
/// </summary>
public class SysMenuService : AdminBaseService<SysMenuRepository>
{
    private readonly SysFunctionRepository _sysFunctionRepository;
    private readonly SysMenuFunctionRepository _sysMenuFunctionRepository;
    private readonly SysRoleMenuFunctionRepository _sysRoleMenuFunctionRepository;
    private readonly AccountInfo _accountInfo;
    private readonly AdminConfiguration _adminConfiguration;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="sysFunctionRepository"></param>
    /// <param name="sysMenuFunctionRepository"></param>
    /// <param name="sysRoleMenuFunctionRepository"></param>
    /// <param name="accountService"></param>
    /// <param name="appConfiguration"></param>
    public SysMenuService(SysMenuRepository repository,
        SysFunctionRepository sysFunctionRepository,
        SysMenuFunctionRepository sysMenuFunctionRepository,
        SysRoleMenuFunctionRepository sysRoleMenuFunctionRepository,
        IAccountService accountService,
        AdminConfiguration appConfiguration) : base(
        repository)
    {
        _sysFunctionRepository = sysFunctionRepository;
        _sysMenuFunctionRepository = sysMenuFunctionRepository;
        _sysRoleMenuFunctionRepository = sysRoleMenuFunctionRepository;
        _adminConfiguration = appConfiguration;
        this._accountInfo = accountService.GetAccountInfo();
    }

    /// <summary>
    /// 获取所有菜单列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysMenuInfoBo>> MenuListAsync()
    {
        //所有菜单
        var sysMenuAllList = await this.Repository.Select
                                .OrderBy(w => w.Number)
                                .ToListAsync();

        // 获取所有菜单接口

        // 所有菜单方法
        var sysFunctionList = await this.Repository.Orm.Select<SysFunction>()
                                                .OrderBy(o => o.Number)
                                                .ToListAsync();
        // 菜单方法列表
        var sysMenuFunctionList = await this.Repository.Orm.Select<SysMenuFunction>()
                                                .ToListAsync();

        //设置菜单 Map
        var sysMenusMap = this.EditeMenus(string.Empty, sysMenuAllList, sysFunctionList, sysMenuFunctionList);

        return sysMenusMap;
    }

    /// <summary>
    /// 创建编辑菜单树
    /// </summary>
    /// <param name="id">父菜单id</param>
    /// <param name="sysMenuList">菜单列表</param>
    /// <param name="sysFunctionList">方法列表</param>
    /// <param name="sysMenuFunctionList">菜单方法列表</param>
    /// <returns></returns>
    public List<SysMenuInfoBo> EditeMenus(string id, List<SysMenu> sysMenuList, List<SysFunction> sysFunctionList, List<SysMenuFunction> sysMenuFunctionList)
    {
        var menus = string.IsNullOrWhiteSpace(id)
            ? sysMenuList.Where(w => string.IsNullOrWhiteSpace(w.ParentId)).ToList()
            : sysMenuList.Where(w => w.ParentId == id).ToList();

        List<SysMenuInfoBo> menuDic = new();

        if (menus?.Count > 0)
        {
            foreach (var menu in menus)
            {
                var smib = new SysMenuInfoBo()
                {
                    Id = menu.Id,
                    Path = menu.Path,
                    Number = menu.Number.Value,
                    Name = menu.Name,
                    Meta = new SysMenuMetaBo
                    {
                        Title = menu.Title,
                        Icon = menu.Icon,
                        Type = menu.MenuType,
                        Affix = menu.Affix,
                    },
                    Component = menu.ComponentName,
                    ParentId = menu.ParentId,
                    Children = this.EditeMenus(menu.Id, sysMenuList, sysFunctionList, sysMenuFunctionList),
                    Functions = new List<SysFunctionBo>()
                };

                // 查询菜单方法
                sysFunctionList.ForEach(m =>
                {
                    var sfb = new SysFunctionBo()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Number = m.Number.Value,
                        IsSelect = false
                    };
                    sfb.IsSelect = sysMenuFunctionList.Any(a => a.MenuId == menu.Id && a.FunctionId == sfb.Id);

                    smib.Functions.Add(sfb);
                });
                menuDic.Add(smib);
            }
        }
        return menuDic;
    }

    /// <summary>
    /// 保存菜单数据
    /// </summary>
    /// <param name="userId">管理员id</param>
    /// <param name="form">菜单表单</param>
    /// <returns></returns>
    public async Task<object> SaveMenuAsync(string userId, SaveMenuDto form)
    {
        var doDate = DateTime.Now;
        SysMenu menu = new SysMenu()
        {
            ParentId = form.ParentId,
            Number = form.Number,
            Name = form.Name,
            Path = form.Path,
            ComponentName = form.Component,
            Title = form.Meta?.Title,
            MenuType = form.Meta.Type,
            Icon = form.Meta?.Icon,
            Affix = form.Meta.Affix,
            UUser = userId,
            UDate = doDate,
            IsActive = true,
        };
        int result = 0;
        // 生成菜单信息
        if (string.IsNullOrWhiteSpace(form.Id) || form.Id.StartsWith("linshi-"))
        {
            // 新增菜单
            menu.CDate = doDate;
            menu.CUser = userId;

            result = await this.Repository.Orm.Insert<SysMenu>(menu).ExecuteAffrowsAsync();
        }
        else
        {
            // 修改菜单
            menu.Id = form.Id;
            result = await this.Repository.Orm.Update<SysMenu>()
                                                .SetSource(menu)
                                                .ExecuteAffrowsAsync();
        }
        if (result > 0)
        {
            // 删除菜单function，重新增加菜单function
            var delFunction = await this.Repository.Orm.Delete<SysMenuFunction>()
                                            .Where(w => w.MenuId == menu.Id)
                                            .ExecuteAffrowsAsync();

            if (form.Functions?.Count > 0)
            {
                List<SysMenuFunction> smf = new List<SysMenuFunction>();
                form.Functions.ForEach(f =>
                {
                    if (f.IsSelect)
                    {
                        smf.Add(new SysMenuFunction()
                        {
                            FunctionId = f.Id,
                            MenuId = menu.Id,
                            IsActive = true,
                            CDate = doDate,
                            UDate = doDate,
                            CUser = userId,
                            UUser = userId,
                        });
                    }
                });
                if (smf?.Count > 0)
                {
                    await this.Repository.Orm.Insert<SysMenuFunction>(smf).ExecuteAffrowsAsync();
                }
            }

            return menu.Id;
        }

        MessageBox.Show("操作异常");
        return default;
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(int page, int size, SysMenu search)
    {
        var query = await this.Repository.Orm.Select<SysMenu, SysMenu>()
                            .LeftJoin(l => l.t1.ParentId == l.t2.Id)
                            .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), a => a.t1.Name.Contains(search.Name))
                            .Page(page, size)
                            .Count(out var total)
                            .ToListAsync(t => new
                            {
                                t.t1.Id,
                                t.t1.Number,
                                t.t1.Name,
                                t.t1.Url,
                                PName = t.t2.Name,
                                t.t1.ComponentName,
                                t.t1.Router,
                                t.t1.Icon,
                                t.t1.Close,
                                t.t1.Show,
                                CDate = t.t1.CDate.ToString("yyyy-MM-dd"),
                                UDate = t.t1.UDate.ToString("yyyy-MM-dd"),
                            });

        return await this.Repository.AsPageViewModelAsync(query, page, size, total);
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task DeleteListAsync(List<string> ids)
    {
        // 删除菜单权限方法
        await this._sysRoleMenuFunctionRepository.DeleteAsync(w => ids.Contains(w.MenuId));

        // 删除菜单方法
        await this._sysMenuFunctionRepository.DeleteAsync(w => ids.Contains(w.MenuId));

        // 删除菜单
        await this.Repository.DeleteAsync(d => ids.Contains(d.Id));

        // 循环删除所有子菜单
        await this.Repository.DeleteAsync(d => ids.Contains(d.ParentId));
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> FindFormAsync(string id)
    {
        var res = new Dictionary<string, object>();

        var form = await this.Repository.FindAsync(id);
        var allFunctions = await this._sysFunctionRepository.Select
            .OrderBy(w => w.Number)
            .ToListAsync();
        var functionIds = await this._sysMenuFunctionRepository.Select
            .Where(w => w.MenuId == id)
            .ToListAsync(w => w.FunctionId);

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form.NullSafe();
        res[nameof(allFunctions)] = allFunctions;
        res[nameof(functionIds)] = functionIds;
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<SysMenu> SaveFormAsync(SysMenuFormDto form)
    {
        var model = form.Form;
        var functionIds = form.FunctionIds;

        model = await this.Repository.InsertOrUpdateAsync(model);

        await this._sysMenuFunctionRepository.DeleteAsync(w => w.MenuId == model.Id);
        if (functionIds.Count <= 0) return model;

        var sysMenuFunctionList = await this._sysMenuFunctionRepository.Select
            .Where(w => w.MenuId == model.Id)
            .ToListAsync();
        foreach (var item in functionIds)
        {
            var sysMenuFunction = sysMenuFunctionList.FirstOrDefault(w => w.FunctionId == item) ?? new SysMenuFunction();
            sysMenuFunction.Id = string.IsNullOrWhiteSpace(sysMenuFunction.Id) ? string.Empty : sysMenuFunction.Id;
            sysMenuFunction.FunctionId = item;
            sysMenuFunction.MenuId = model.Id;
            await this._sysMenuFunctionRepository.InsertAsync(sysMenuFunction);
        }

        return model;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(SysMenu search)
    {
        var tableViewModel = await this.FindListAsync(1, 999999, search);
        return this.ExportExcelByPageViewModel(tableViewModel, null, "Id");
    }

    #region 创建系统左侧菜单

    /// <summary>
    /// 根据角色ID 获取菜单
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetMenusByUserIdAsync()
    {
        // 获取所有菜单
        var sysMenus = await this.GetMenusByCurrentRoleAsync();
        //设置菜单 Map
        var sysMenusMap = this.CreateNewMenus(string.Empty, sysMenus);

        // 查询权限
        var data = new
        {
            menu = sysMenusMap,
            //permissions = new List<string>() {
            //"list.add",
            //"list.edit",
            //"list.delete",
            //"user.add",
            //"user.edit",
            //"user.delete",
            //}
        };

        return data;
    }

    /// <summary>
    /// 创建newui菜单
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sysMenuList"></param>
    public List<Dictionary<string, object>> CreateNewMenus(string id, List<SysMenu> sysMenuList)
    {
        var menus = string.IsNullOrWhiteSpace(id)
            ? sysMenuList.Where(w => string.IsNullOrWhiteSpace(w.ParentId)).ToList()
            : sysMenuList.Where(w => w.ParentId == id).ToList();

        return menus.Select(item => new Dictionary<string, object>
        {
            ["path"] = item.Path,
            ["name"] = item.Name,
            ["meta"] = new
            {
                title = item.Title,
                icon = item.Icon,
                type = item.MenuType,
                affix = item.Affix,
            },
            ["component"] = item.ComponentName,
            ["parentId"] = item.ParentId,
            ["children"] = this.CreateNewMenus(item.Id, sysMenuList)
        }).ToList();
    }

    /// <summary>
    /// 根据角色ID 获取菜单
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysMenu>> GetMenusByCurrentRoleAsync()
    {
        var sysMenuAllList = await this.Repository.Orm.Select<SysMenu>()
                                        .Where(w => w.Show == 1)
                                        .OrderBy(w => w.Number)
                                        .ToListAsync();

        if (this._accountInfo.IsAdministrator)
        {
            return sysMenuAllList;
        }

        // 账户角色
        var aRoles = this._accountInfo.SysRoles.Select(s => s.Id).ToList();

        // 获取当前用户所有菜单
        var sysMenuList = await this.Repository.Orm.Select<SysMenu, SysRoleMenuFunction, SysFunction>()
                                                .LeftJoin(l => l.t1.Id == l.t2.MenuId)
                                                .LeftJoin(l => l.t3.Id == l.t2.FunctionId && l.t3.ByName == AdminFunctionConsts.Function_Display)
                                                .Where(w => aRoles.Contains(w.t2.RoleId) && w.t1.Show == 1 && w.t2.IsActive && w.t1.IsActive)
                                                .ToListAsync(t => t.t1);

        var newSysMenuList = new List<SysMenu>();

        foreach (var item in sysMenuList)
        {
            this.CheckUpperLevel(sysMenuAllList, sysMenuList, newSysMenuList, item);
            var item1 = item;
            if (newSysMenuList.Find(w => w.Id == item1.Id) == null)
            {
                newSysMenuList.Add(item);
            }
        }

        return newSysMenuList.OrderBy(w => w.Number).ToList();
    }

    /// <summary>
    /// 检查菜单上一层级
    /// </summary>
    /// <param name="sysMenuAllList">系统菜单列表</param>
    /// <param name="oldSysMenuList">旧系统菜单</param>
    /// <param name="newSysMenuList">新系统菜单列表</param>
    /// <param name="menu">当前菜单信息</param>
    private void CheckUpperLevel(List<SysMenu> sysMenuAllList, List<SysMenu> oldSysMenuList,
        List<SysMenu> newSysMenuList, SysMenu menu)
    {
        if (oldSysMenuList.Any(w => w.Id == menu.ParentId) ||
            newSysMenuList?.Any(w => w.Id == menu.ParentId) == true)
        {
            // 如果存在上级菜单
            return;
        }

        var item = sysMenuAllList.Find(w => w.Id == menu.ParentId);
        if (item == null)
        {
            // 系统菜单里面不存在上级菜单
            return;
        }

        newSysMenuList.Add(item);

        this.CheckUpperLevel(sysMenuAllList, oldSysMenuList, newSysMenuList, item);
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sysMenuList"></param>
    public List<Dictionary<string, object>> CreateMenus(string id, List<SysMenu> sysMenuList)
    {
        var menus = string.IsNullOrWhiteSpace(id)
            ? sysMenuList.Where(w => string.IsNullOrWhiteSpace(w.ParentId)).ToList()
            : sysMenuList.Where(w => w.ParentId == id).ToList();

        return menus.Select(item => new Dictionary<string, object>
        {
            ["id"] = item.Id,
            ["name"] = item.Name,
            ["componentName"] = item.ComponentName,
            ["url"] = item.Url,
            ["router"] = item.Router,
            ["jumpUrl"] = string.IsNullOrWhiteSpace(item.JumpUrl) ? item.Router : item.JumpUrl,
            ["icon"] = item.Icon,
            ["close"] = item.Close,
            ["parentId"] = item.ParentId,
            ["children"] = this.CreateMenus(item.Id, sysMenuList)
        }).ToList();
    }

    /// <summary>
    /// 获取拥有的菜单对象的权限
    /// </summary>
    /// <param name="sysMenuList"></param>
    /// <returns></returns>
    public async Task<List<Dictionary<string, object>>> GetPowerByMenusAsync(List<SysMenu> sysMenuList)
    {
        var uRoles = this._accountInfo.SysRoles.Select(w => w.Id).ToList();

        var sysFunctionList = await this._sysFunctionRepository.Select.OrderBy(w => w.Number).ToListAsync();
        var sysMenuFunctionList = await this._sysMenuFunctionRepository.Select.ToListAsync();
        var sysRoleMenuFunctionList = await this._sysRoleMenuFunctionRepository.Select
                                                    .Where(w => uRoles.Contains(w.RoleId))
                                                    .ToListAsync();

        var res = new List<Dictionary<string, object>>();

        if (this._accountInfo.IsAdministrator)
        {
            foreach (var item in sysMenuList)
            {
                var power = new Dictionary<string, object>
                {
                    ["MenuId"] = item.Id
                };
                foreach (var sysFunction in sysFunctionList)
                {
                    if (string.IsNullOrWhiteSpace(sysFunction.ByName))
                    {
                        continue;
                    }

                    var isPower = sysMenuFunctionList
                                            .Any(w => w.MenuId == item.Id &&
                                                        w.FunctionId == sysFunction.Id);
                    if (sysFunction.ByName == AdminFunctionConsts.Function_Display ||
                        item.ParentId == this._adminConfiguration.SysMenuId)
                    {
                        isPower = true;
                    }
                    power.Add(sysFunction.ByName, isPower);
                }

                res.Add(power);
            }

            return res;
        }

        foreach (var item in sysMenuList)
        {
            var power = new Dictionary<string, object>
            {
                ["MenuId"] = item.Id
            };
            foreach (var sysFunction in sysFunctionList)
            {
                if (string.IsNullOrWhiteSpace(sysFunction.ByName)) continue;

                if (_accountInfo.SysRoles.Select(w => w.Id).Any())
                {
                    var isPower = sysRoleMenuFunctionList
                                    .Any(w => uRoles.Contains(w.RoleId) &&
                                                    w.MenuId == item.Id &&
                                                    w.FunctionId == sysFunction.Id);
                    power.Add(sysFunction.ByName, isPower);
                }
                else
                {
                    power.Add(sysFunction.ByName, false);
                }
            }

            res.Add(power);
        }

        return res;
    }

    /// <summary>
    /// 根据菜单获取权限
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, bool>> GetPowerStateByMenuId(string menuId)
    {
        var sysMenu = await this.Repository.FindAsync(menuId);
        var sysFunctionList = await this._sysFunctionRepository.Select.OrderBy(w => w.Number).ToListAsync();
        var sysMenuFunctionList = await this._sysMenuFunctionRepository.Select.ToListAsync();

        //系统角色
        var sysRoles = this._accountInfo.SysRoles.Select(w => w.Id).ToList();

        var sysRoleMenuFunctionList = await this._sysRoleMenuFunctionRepository.Select
                                                .Where(w => sysRoles.Contains(w.RoleId))
                                                .ToListAsync();

        var power = new Dictionary<string, bool>();

        if (this._accountInfo.IsAdministrator)
        {
            foreach (var item in sysFunctionList)
            {
                if (string.IsNullOrWhiteSpace(item.ByName))
                {
                    continue;
                }

                var isPower = sysMenuFunctionList.Any(w => w.MenuId == menuId && w.FunctionId == item.Id);
                if (item.ByName == AdminFunctionConsts.Function_Display ||
                    sysMenu.ParentId == this._adminConfiguration.SysMenuId)
                {
                    isPower = true;
                }
                power.Add(item.ByName, isPower);
            }

            return power;
        }

        foreach (var item in sysFunctionList)
        {
            if (string.IsNullOrWhiteSpace(item.ByName)) continue;

            if (_accountInfo.SysRoles.Select(w => w.Id).Any())
            {
                var isPower = sysRoleMenuFunctionList
                    .Any(w => sysRoles.Contains(w.RoleId) && w.MenuId == menuId &&
                              w.FunctionId == item.Id);
                power.Add(item.ByName, isPower);
            }
            else
            {
                power.Add(item.ByName, false);
            }
        }

        return power;
    }

    /// <summary>
    /// 通过id获取菜单信息
    /// </summary>
    /// <param name="menuId">菜单id</param>
    /// <returns></returns>
    public async Task<SysMenu> GetMenuByIdAsync(string menuId) => await this.Repository.FindAsync(menuId);

    #endregion 创建系统左侧菜单

    #region 创建菜单 功能 树

    /// <summary>
    /// 获取菜单方法树
    /// </summary>
    /// <returns></returns>
    public async Task<(List<object>, List<string>, List<string>)> GetMenuFunctionTreeAsync()
    {
        var sysMenus = await this.Repository.Select.OrderBy(w => w.Number).ToListAsync();
        var sysFunctions = await this._sysFunctionRepository.Select.OrderBy(w => w.Number).ToListAsync();
        var sysMenuFunctions =
            await this._sysMenuFunctionRepository.Select.OrderBy(w => w.CDate).ToListAsync();
        List<string> defaultExpandedKeys = new();
        List<string> defaultCheckedKeys = new();
        var tree = this.CreateMenuFunctionTree("", sysMenus, sysFunctions, sysMenuFunctions, defaultExpandedKeys, defaultCheckedKeys);
        return (tree, defaultExpandedKeys, defaultCheckedKeys);
    }

    /// <summary>
    /// 获取菜单与功能树
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sysMenuAllList"></param>
    /// <param name="sysFunctions"></param>
    /// <param name="sysMenuFunctions"></param>
    /// <param name="defaultExpandedKeys"></param>
    /// <param name="defaultCheckedKeys"></param>
    /// <returns></returns>
    private List<object> CreateMenuFunctionTree(string id, List<SysMenu> sysMenuAllList,
        List<SysFunction> sysFunctions,
        List<SysMenuFunction> sysMenuFunctions, List<string> defaultExpandedKeys, List<string> defaultCheckedKeys)
    {
        var res = new List<object>();
        var menus = string.IsNullOrWhiteSpace(id)
            ? sysMenuAllList.Where(w => string.IsNullOrWhiteSpace(w.ParentId)).ToList()
            : sysMenuAllList.Where(w => w.ParentId == id).ToList();

        foreach (var item in menus)
        {
            var children = new List<object>();
            if (sysMenuAllList.Any(w => w.ParentId == item.Id))
            {
                defaultExpandedKeys.Add(item.Id);

                children = this.CreateMenuFunctionTree(item.Id, sysMenuAllList, sysFunctions, sysMenuFunctions,
                    defaultExpandedKeys, defaultCheckedKeys);
            }
            else
            {
                //if (string.IsNullOrWhiteSpace(item.Menu_Url)) continue;
                //遍历功能
                foreach (var function in sysFunctions)
                {
                    //判断是否 该菜单下 是否勾选了 该功能
                    var isChecked = sysMenuFunctions.Any(w => w.FunctionId == function.Id && w.MenuId == item.Id);

                    var key = $"{item.Id}${function.Id}";
                    if (isChecked) defaultCheckedKeys.Add(key);

                    children.Add(new
                    {
                        key,
                        title = $"{function.Name}-{function.ByName}-{function.Number}",
                        disabled = true,
                        children = new ArrayList()
                    });
                }
            }

            res.Add(new
            {
                key = item.Id,
                title = $"{item.Name}-{item.Number}",
                disableCheckbox = true,
                children
            });
        }

        return res;
    }

    #endregion 创建菜单 功能 树
}