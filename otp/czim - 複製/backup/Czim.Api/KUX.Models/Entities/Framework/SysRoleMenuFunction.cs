using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 角色菜单功能绑定
/// </summary>
public class SysRoleMenuFunction : StringKeyEntity
{
    public string RoleId { get; set; }
    public string MenuId { get; set; }
    public string FunctionId { get; set; }
}