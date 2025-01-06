using KUX.Infrastructure;
using KUX.Models.ApiResultManage;
using KUX.Models.DTO;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.ServicesAdmin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 数据字典服务
/// </summary>
public class SysDictionaryService : AdminBaseService<SysDictionaryRepository>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认仓储</param>
    public SysDictionaryService(SysDictionaryRepository repository) : base(repository)
    {
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(int page, int size, SysDictionary search)
    {
        var query = await this.Repository.Orm.Select<SysDictionary, SysDictionary>()
                                            .LeftJoin(l => l.t1.ParentId == l.t2.Id)
                                            .WhereIf(!string.IsNullOrWhiteSpace(search?.ParentId), w => w.t1.ParentId == search.ParentId)
                                            .WhereIf(!string.IsNullOrWhiteSpace(search?.Name), a => a.t1.Name.Contains(search.Name))
                                            .Where(w => w.t1.IsActive)
                                            .Page(page, size)
                                            .Count(out var total)
                                            .ToListAsync(t => new
                                            {
                                                t.t1.Id,
                                                t.t1.Sort,
                                                t.t1.Code,
                                                t.t1.Name,
                                                t.t1.Value,
                                                PName = t.t2.Name,
                                                UDate = t.t1.UDate.ToString("yyyy-MM-dd"),
                                                CDate = t.t1.CDate.ToString("yyyy-MM-dd"),
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
        await this.Repository.DeleteAsync(d => ids.Contains(d.Id));
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

        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form.NullSafe();
        return res;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public async Task<SysDictionary> SaveFormAsync(SysDictionary form)
    {
        return await this.Repository.InsertOrUpdateAsync(form);
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="search">查询条件</param>
    /// <returns></returns>
    public async Task<byte[]> ExportExcelAsync(SysDictionary search)
    {
        var tableViewModel = await this.FindListAsync(1, 999999, search);
        return this.ExportExcelByPageViewModel(tableViewModel, null, "Id");
    }

    /// <summary>
    /// 获取字典树
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysDictionaryDto>> GetDictionaryTreeAsync()
    {
        var allDictionary = await this.Repository.Orm.Select<SysDictionary>()
                                    .ToListAsync();
        return this.CreateDictionaryTree(string.Empty, allDictionary);
    }

    /// <summary>
    /// 创建字典树
    /// </summary>
    /// <param name="id"></param>
    /// <param name="allDictionary"></param>
    /// <returns></returns>
    private List<SysDictionaryDto> CreateDictionaryTree(string id, List<SysDictionary> allDictionary)
    {
        List<SysDictionaryDto> result = new();

        var data = new List<SysDictionary>();
        if (id == string.Empty)
        {
            data = allDictionary.Where(w => w.ParentId == null ||
                                            w.ParentId == string.Empty)
                                .ToList();
        }
        else
        {
            data = allDictionary.Where(w => w.ParentId == id)
                                .ToList();
        }

        foreach (var item in data)
        {
            var model = item.MapTo<SysDictionary, SysDictionaryDto>();
            model.Children = this.CreateDictionaryTree(item.Id, allDictionary);
            result.Add(model);
        }

        return result;
    }

    /// <summary>
    /// 根据 Code 获取 字典集
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<List<SysDictionaryDto>> GetDictionaryByCodeAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            MessageBox.Show("参数Code是空!");
        }

        var dictionary = await this.Repository.Orm.Select<SysDictionary>()
                                    .Where(w => w.Code == code)
                                    .FirstAsync();
        if (dictionary == null)
        {
            return default;
        }
        var dictionarys = await this.Repository.Select.Where(w => w.ParentId == dictionary.Id).ToListAsync();
        if (dictionarys.Any())
        {
            return default;
        }
        var result = new List<SysDictionaryDto>();
        return this.CreateDictionaryTree(dictionary.Id, dictionarys);
    }
}