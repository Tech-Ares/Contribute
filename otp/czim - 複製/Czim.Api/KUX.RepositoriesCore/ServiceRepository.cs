using FreeSql;
using KUX.Infrastructure;
using KUX.Infrastructure.ScanDIService.Interface;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Core.DapperUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KUX.Repositories.Core;

/// <summary>
/// 仓储服务基类
/// 实现自身注入
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class ServiceRepository<TModel> : BaseRepository<TModel, string>, IDIScopedSelf where TModel : class
{
    /// <summary>
    /// dapper上下文
    /// </summary>
    protected IDapperContext DapperContext;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql"></param>
    /// <param name="_dapperContext"></param>
    /// <param name="filter"></param>
    /// <param name="asTable"></param>
    public ServiceRepository(IFreeSql freeSql, IDapperContext _dapperContext,
        Expression<Func<TModel, bool>> filter = null,
        Func<string, string> asTable = null) : base(freeSql, filter, asTable)
    {
        DapperContext = _dapperContext;
    }

    /// <summary>
    /// App前端列表模型
    /// </summary>
    /// <typeparam name="T">泛型数据</typeparam>
    /// <param name="data">数据内容</param>
    /// <returns></returns>
    public Task<ApiViewCountModel<T>> AsAppDataModelAsync<T>(IEnumerable<T> data) where T : class
    {
        ApiViewCountModel<T> tcv = new ApiViewCountModel<T>();
        if (data != null &&
            data.Any())
        {
            tcv.Count = data.Count();
            tcv.DataList = data.ToList();
        }

        return Task.FromResult(tcv); ;
    }

    /// <summary>
    /// 转换为分页视图模型
    /// </summary>
    /// <param name="data"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="total"></param>
    /// <param name="columnHeads">表头</param>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public Task<PageViewModel> AsPageViewModelAsync<TData>(IEnumerable<TData> data, int page, int size,
        long total, List<TableViewColumn> columnHeads = default)
    {
        var PageViewModel = new PageViewModel();

        var propertyInfos = typeof(TData).GetProperties();
        var fieldNames = propertyInfos.Select(item => item.Name).ToList();

        //PageViewModel.Columns = fieldNames;

        var result = new List<Dictionary<string, object>>();
        foreach (var item in data)
        {
            var type = item.GetType();
            var dictionary = new Dictionary<string, object>();

            foreach (var fieldName in fieldNames)
            {
                dictionary[fieldName] = type.GetProperty(fieldName)?.GetValue(item);
            }

            result.Add(dictionary);
        }

        PageViewModel.Rows = result;
        PageViewModel.PageCount = (int)(total / size);
        PageViewModel.Page = page;
        PageViewModel.PageSize = size;
        PageViewModel.Total = (int)total;

        this.CreateColumnHeads(PageViewModel, fieldNames, columnHeads);

        return Task.FromResult(PageViewModel);
    }

    /// <summary>
    /// 分页视图扩展，增加DataList泛型List返回
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="data"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="total"></param>
    /// <param name="columnHeads">列头</param>
    /// <returns></returns>
    public Task<PageViewExtModel<TData>> AsPagingListViewModelAsync<TData>(IEnumerable<TData> data, int page, int size,
       long total, List<TableViewColumn> columnHeads = default)
    {
        var PageViewModel = new PageViewExtModel<TData>();

        var propertyInfos = typeof(TData).GetProperties();
        var fieldNames = propertyInfos.Select(item => item.Name).ToList();

        //PageViewModel.Columns = fieldNames;

        var result = new List<Dictionary<string, object>>();

        PageViewModel.Rows = result;
        PageViewModel.DataList = data.ToList();
        PageViewModel.PageCount = (int)(total / size);
        PageViewModel.Page = page;
        PageViewModel.PageSize = size;
        PageViewModel.Total = (int)total;
        // 创建列头
        this.CreateColumnHeads(PageViewModel, fieldNames, columnHeads);

        return Task.FromResult(PageViewModel);
    }

    /// <summary>
    /// 创建列头
    /// </summary>
    /// <param name="pagingViewModel"></param>
    /// <param name="fieldNames"></param>
    /// <param name="columnHeads"></param>
    private void CreateColumnHeads(PageViewModel pagingViewModel, List<string> fieldNames, List<TableViewColumn> columnHeads)
    {
        //var entityInfos = this.CacheEntity.GetEntityInfos(typeof(TModel).Name);

        foreach (var item in fieldNames)
        {
            //var title = entityInfos.Find(w => w.Name == item)?.Remark ?? item;
            pagingViewModel.Columns.Add(new TableViewColumn(item.FirstCharToLower(), item));
        }

        //如果 传入了 头信息 则 覆盖
        if (columnHeads == null)
        {
            return;
        }

        foreach (var item in columnHeads)
        {
            var columnHead =
                pagingViewModel.Columns.Find(w => w.FieldName.ToLower() == item.FieldName.ToLower());
            if (columnHead == null)
            {
                continue;
            }
            columnHead.Show = item.Show;
            columnHead.Title = item.Title;
            columnHead.Width = item.Width;
        }
    }
}