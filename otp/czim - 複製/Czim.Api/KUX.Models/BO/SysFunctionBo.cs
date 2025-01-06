namespace KUX.Models.BO
{
    /// <summary>
    /// 菜单方法模型
    /// </summary>
    public class SysFunctionBo
    {
        /// <summary>
        /// 方法id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelect { get; set; }
    }
}