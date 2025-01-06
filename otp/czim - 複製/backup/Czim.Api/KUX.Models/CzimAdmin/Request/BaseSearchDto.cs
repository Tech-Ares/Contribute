namespace KUX.Models.KuxAdmin.Request
{
    /// <summary>
    /// 基础查询dto
    /// </summary>
    public class BaseSearchDto
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
    }
}