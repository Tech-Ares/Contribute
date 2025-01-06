using KUX.Infrastructure;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 组织服务
/// </summary>
public class SysOrganizationService : AdminBaseService<SysOrganizationRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public SysOrganizationService(SysOrganizationRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<(List<string> expandedRowKeys, List<SysOrganization> res)> FindListAsync(SysOrganization search)
    {
        var query = await this.Repository.Select
                                .WhereIf(search?.State == null, w => w.State == StateEnum.正常)
                                .WhereIf(search?.State != null, w => w.State == search.State)
                                .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), w => w.Name.Contains(search.Name))
                                .ToListAsync();
        var expandedRowKeys = query.Select(w => w.Id).ToList();

        var data = query.Where(w => string.IsNullOrWhiteSpace(w.ParentId))
                        .OrderBy(w => w.OrderNumber)
                        .ToList();

        return (expandedRowKeys, data);
    }

    /// <summary>
    /// 根据id数组删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task DeleteListAsync(List<string> ids)
    {
        var sysOrganizations = await this.Repository.Select
                                            .Where(w => ids.Contains(w.Id))
                                            .ToListAsync();
        await DelTreeSysOrganizationsAsync(sysOrganizations);
    }

    /// <summary>
    /// 删除组织树
    /// </summary>
    /// <param name="list">组织列表</param>
    /// <returns></returns>
    private async Task DelTreeSysOrganizationsAsync(List<SysOrganization> list)
    {
        foreach (var item in list)
        {
            if (item.Children.Count > 0)
            {
                await DelTreeSysOrganizationsAsync(item.Children.ToList());
            }
            await this.Repository.DeleteAsync(item.Id);
        }
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> FindFormAsync(string id, string parentId)
    {
        var res = new Dictionary<string, object>();
        var form = await this.Repository.FindAsync(id);
        form = form.NullSafe();

        if (string.IsNullOrWhiteSpace(id))
        {
            var maxNum = await this.Repository.Select
                                    .WhereIf(string.IsNullOrWhiteSpace(parentId), w => w.ParentId == null || w.ParentId == string.Empty)
                                    .WhereIf(!string.IsNullOrWhiteSpace(parentId), w => w.ParentId == parentId)
                                    .MaxAsync(w => w.OrderNumber);
            form.OrderNumber = (maxNum ?? 0) + 1;
        }

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form;
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<SysOrganization> SaveFormAsync(SysOrganization form)
    {
        return await this.Repository.InsertOrUpdateAsync(form);
    }
}