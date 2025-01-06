using KUX.Models.BO;
using System.Collections.Generic;

namespace KUX.Models.CzimAdmin.Request.SysRoleMenuFunction;


/// <summary>
/// 系统菜单权限
/// </summary>
public class SysRoleMenuFunctionDto
{
    /// <summary>
    /// 角色id
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    /// 角色列表内容
    /// </summary>
    public List<SysRoleMenuInfoBo> RoleMenuFunctionList { get; set; }
}