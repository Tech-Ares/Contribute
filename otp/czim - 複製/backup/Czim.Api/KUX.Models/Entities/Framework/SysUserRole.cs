using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 用户与角色绑定
/// </summary>
public class SysUserRole : StringKeyEntity
{
    /// <summary>
    /// 用户id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 角色id
    /// </summary>
    public string RoleId { get; set; }
}