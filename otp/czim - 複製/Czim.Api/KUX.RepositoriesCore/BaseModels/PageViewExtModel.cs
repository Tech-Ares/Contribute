using System.Collections.Generic;

namespace KUX.Repositories.Core.BaseModels
{
    /// <summary>
    /// 分页扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageViewExtModel<T> : PageViewModel
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> DataList { get; set; }
    }
}