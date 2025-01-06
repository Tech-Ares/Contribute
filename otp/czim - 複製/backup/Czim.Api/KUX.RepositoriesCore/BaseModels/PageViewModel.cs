using System.Collections.Generic;

namespace KUX.Repositories.Core.BaseModels
{
    /// <summary>
    /// 分页数据模型
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// 列信息
        /// </summary>
        public List<TableViewColumn> Columns { get; set; } = new List<TableViewColumn>();

        /// <summary>
        /// 转换后数据
        /// </summary>
        public List<Dictionary<string, object>> Rows { get; set; } = new List<Dictionary<string, object>>();

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 一页显示多少条
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page  { get; set; }
    }
}