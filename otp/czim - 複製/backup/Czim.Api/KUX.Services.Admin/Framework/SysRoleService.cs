using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.Entities.Framework;
using KUX.Models.KuxAdmin.Request.SysRole;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 角色服务
/// </summary>
public class SysRoleService : AdminBaseService<SysRoleRepository>
{
    /// <summary>
    /// 用户角色
    /// </summary>
    private readonly SysUserRoleRepository _sysUserRoleRepository;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">角色仓储</param>
    /// <param name="sysUserRoleRepository">用户角色仓储</param>
    public SysRoleService(SysRoleRepository repository, SysUserRoleRepository sysUserRoleRepository) : base(
        repository)
    {
        _sysUserRoleRepository = sysUserRoleRepository;
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(RoleSearchDto search)
    {
        var query = await this.Repository.Select
                                .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), a => a.Name.Contains(search.Name))
                                .OrderBy(w => w.Number)
                                .Page(search.Page, search.Size)
                                .Count(out var total)
                                .ToListAsync(w => new
                                {
                                    w.Id,
                                    w.Number,
                                    w.Name,
                                    w.Remark,
                                    CDate = w.CDate.ToString("yyyy-MM-dd"),
                                    UDate = w.UDate.ToString("yyyy-MM-dd"),
                                });

        return await this.Repository.AsPageViewModelAsync(query, search.Page, search.Size, total);
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task DeleteListAsync(List<string> ids)
    {
        var roleName = "";
        foreach (var item in ids)
        {
            var role = await this.Repository.FindAsync(item);
            if (role.IsDelete != 1)
            {
                roleName += $"{role.Name},";
                continue;
            }
            // 删除角色
            await this.Repository.DeleteAsync(role);

            // 删除用户角色
            await this._sysUserRoleRepository.DeleteAsync(w => w.RoleId == item);
        }
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            MessageBox.Show($"{roleName} 等不能删除!");
        }
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
        form = form.NullSafe();

        if (string.IsNullOrWhiteSpace(id))
        {
            var maxNum = await this.Repository.Select.MaxAsync(w => w.Number);
            form.Number = maxNum + 1;
        }

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form;
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="userId">账户id</param>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<SysRole> SaveFormAsync(string userId, SysRole form)
    {
        form.UDate = DateTime.Now;
        form.UUser = userId;
        if (string.IsNullOrWhiteSpace(form.Id))
        {
            form.Id = Guid.NewGuid().ToString("N");
            form.CUser = userId;
            form.CDate = DateTime.Now;
            form.IsActive = true;
        }
        var result = await this.Repository.InsertOrUpdateAsync(form);
        return result;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(RoleSearchDto search)
    {
        search.Page = 1;
        search.Size = int.MaxValue;

        var tableViewModel = await this.FindListAsync(search);
        return this.ExportExcelByPageViewModel(tableViewModel, null, "Id");
    }
}