using System.Collections.Generic;

namespace KUX.Models.DTO;

/// <summary>
/// 角色菜单dto
/// </summary>
public class SysRoleMenuFunctionFormDto
{
    /// <summary>
    /// 角色id
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    /// 菜单id
    /// </summary>
    public string MenuId { get; set; }

    /// <summary>
    /// 菜单列表
    /// </summary>
    public List<string> FunctionIds { get; set; }
}