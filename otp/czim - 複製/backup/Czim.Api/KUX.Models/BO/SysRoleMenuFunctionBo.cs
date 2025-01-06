namespace KUX.Models.BO
{
    /// <summary>
    /// 角色菜单方法
    /// </summary>
    public class SysRoleMenuFunctionBo
    {
        /// <summary>
        /// 方法id
        /// </summary>
        public string FuncId { get; set; }

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
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool CanUse { get; set; }
    }
}