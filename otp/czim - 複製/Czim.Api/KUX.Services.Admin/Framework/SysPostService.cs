using KUX.Infrastructure;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 岗位服务
/// </summary>
public class SysPostService : AdminBaseService<SysPostRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public SysPostService(SysPostRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(int page, int size, SysPost search)
    {
        var query = await this.Repository.Select
                            .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), a => a.Name.Contains(search.Name))
                            .OrderBy(w => w.Number)
                            .Page(page, size)
                            .Count(out var total)
                            .ToListAsync(w => new
                            {
                                w.Id,
                                w.Number,
                                w.Code,
                                w.Name,
                                //State = w.State == StateEnum.正常 ? "正常" : "停用",
                                w.State,
                                //State=w.State.ToString(),
                                UDate = w.UDate.ToString("yyyy-MM-dd"),
                                CDate = w.CDate.ToString("yyyy-MM-dd"),
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
        await this.Repository.DeleteAsync(w => ids.Contains(w.Id));
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
            form.Number = (maxNum ?? 0) + 1;
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
    public async Task<SysPost> SaveFormAsync(SysPost form)
    {
        return await this.Repository.InsertOrUpdateAsync(form);
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(SysPost search)
    {
        var tableViewModel = await this.FindListAsync(1, 999999, search);
        return this.ExportExcelByPageViewModel(tableViewModel);
    }
}