using KUX.Models.Entities.Framework;
using System.Collections.Generic;

namespace KUX.Models.BO
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public class SysMenuInfoBo
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public string Id { get; set; }

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
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 菜单头
        /// </summary>
        public SysMenuMetaBo Meta { get; set; }

        public object ApiList { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<SysMenuInfoBo> Children { get; set; }

        /// <summary>
        /// 菜单方法列表
        /// </summary>
        public List<SysFunctionBo> Functions { get; set; }
    }
}