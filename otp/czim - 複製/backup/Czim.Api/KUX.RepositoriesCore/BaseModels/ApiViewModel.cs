using System.Collections.Generic;

namespace KUX.Repositories.Core.BaseModels
{
    /// <summary>
    /// api 接口返回数据
    /// </summary>
    public class ApiViewModel
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 泛型列表数据
    /// </summary>
    public class ApiViewCountModel<T> : ApiViewModel where T : class
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        public List<T> DataList { get; set; }
    }
}