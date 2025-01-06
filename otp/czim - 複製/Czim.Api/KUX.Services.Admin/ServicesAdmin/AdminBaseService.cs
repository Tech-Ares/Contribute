using KUX.Infrastructure;
using KUX.Infrastructure.Services;
using KUX.Repositories.Core.BaseModels;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KUX.Services.Admin.ServicesAdmin;

/// <summary>
/// admin接口基础服务
/// </summary>
/// <typeparam name="TRepository">泛型仓储</typeparam>
public class AdminBaseService<TRepository> : FrameworkBaseService<TRepository> where TRepository : class
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository"></param>
    public AdminBaseService(TRepository repository) : base(repository)
    {
    }

    #region 导出 Excel

    /// <summary>
    /// 导出 Excel
    /// </summary>
    /// <param name="PageViewModel"></param>
    /// <param name="byName">别名</param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    protected virtual byte[] ExportExcelByPageViewModel(PageViewModel PageViewModel,
        Dictionary<string, string> byName = null, params string[] ignore)
    {
        var workbook = new HSSFWorkbook();
        var sheet = workbook.CreateSheet();
        //数据
        var data = PageViewModel.Rows;
        var cols = ignore == null ? PageViewModel.Columns :
            PageViewModel.Columns.Where(w => !ignore.Any(i => i.ToLower() == w.FieldName.ToLower()))
            .ToList();

        //填充表头
        var dataRow = sheet.CreateRow(0);
        foreach (var item in cols)
        {
            var index = cols.IndexOf(item);
            if (byName != null && byName.ContainsKey(item.FieldName))
            {
                dataRow.CreateCell(index).SetCellValue(byName[item.FieldName]);
            }
            else
            {
                dataRow.CreateCell(index).SetCellValue(item.Title ?? item.FieldName);
            }

            sheet.SetColumnWidth(index, 30 * 256);
        }

        //填充内容
        for (var i = 0; i < data.Count; i++)
        {
            var item = data[i];
            dataRow = sheet.CreateRow(i + 1);
            foreach (var col in cols)
            {
                if (col.FieldName.StartsWith("_")) continue;
                var index = cols.IndexOf(col);
                var name = col.FieldName.FirstCharToUpper();
                if (!item.ContainsKey(name)) continue;
                var value = item[name];
                dataRow.CreateCell(index).SetCellValue(value == null ? "" : value.ToString());
            }
        }

        //填充byte
        using var ms = new MemoryStream();
        workbook.Write(ms);
        return ms.ToArray();
    }

    #endregion 导出 Excel
}