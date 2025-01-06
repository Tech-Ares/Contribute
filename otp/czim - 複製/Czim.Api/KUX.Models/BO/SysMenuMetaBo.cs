namespace KUX.Models.BO
{
    /// <summary>
    /// 系统菜单头
    /// </summary>
    public class SysMenuMetaBo
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单类型
        /// menu
        /// button
        /// link
        /// ifream
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Affix { get; set; }
    }
}