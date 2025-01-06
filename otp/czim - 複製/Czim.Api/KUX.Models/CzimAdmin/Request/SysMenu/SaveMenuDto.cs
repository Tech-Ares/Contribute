using KUX.Models.BO;
using System.Collections.Generic;

namespace KUX.Models.Request.SysMenu
{
    /// <summary>
    /// 保存菜单模型
    /// </summary>
    public class SaveMenuDto
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public int Number { get; set; } = 0;

        /// <summary>
        /// 父菜单节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 菜单头
        /// </summary>
        public SysMenuMetaBo Meta { get; set; }

        /// <summary>
        /// 接口信息
        /// </summary>
        public object ApiList { get; set; }

        /// <summary>
        /// 方法列表
        /// </summary>
        public List<SysFunctionBo> Functions { get; set; }
    }
}