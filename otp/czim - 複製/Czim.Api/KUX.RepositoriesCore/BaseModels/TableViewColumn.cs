﻿namespace KUX.Repositories.Core.BaseModels;

/// <summary>
/// 列头信息
/// </summary>
public class TableViewColumn
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="title">标题</param>
    public TableViewColumn(string fieldName, string title)
    {
        this.FieldName = fieldName;
        this.Title = title;
        if (fieldName.ToLower() == "Id".ToLower())
        {
            this.Show = false;
        }
        else
        {
            this.Show = !fieldName.StartsWith("_");
        }
    }
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="title">标题</param>
    /// <param name="width">宽度</param>
    public TableViewColumn(string fieldName, string title, int width)
    {
        this.FieldName = fieldName;
        this.Title = title;
        if (fieldName.ToLower() == "Id".ToLower())
        {
            this.Show = false;
        }
        else
        {
            this.Show = !fieldName.StartsWith("_");
        }

        this.Width = width.ToString();
    }

    public TableViewColumn(string fieldName, string title, bool show, int width)
    {
        this.FieldName = fieldName;
        this.Title = title;
        this.Show = show;
        this.Width = width.ToString();
    }

    /// <summary>
    /// 字段名称
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// 标题名称
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool Show { get; set; } = true;

    /// <summary>
    /// 列宽度
    /// </summary>
    public string Width { get; set; } = string.Empty;
}