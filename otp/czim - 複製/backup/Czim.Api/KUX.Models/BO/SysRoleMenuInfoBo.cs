using System.Collections.Generic;

namespace KUX.Models.BO
{
    /// <summary>
    /// 角色菜单列表
    /// </summary>
    public class SysRoleMenuInfoBo
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<SysRoleMenuInfoBo> Children { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 菜单方法列表
        /// </summary>
        public List<SysRoleMenuFunctionBo> Functions { get; set; }
    }
}