using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 菜单与功能绑定
/// </summary>
public class SysMenuFunction : StringKeyEntity
{
    public string MenuId { get; set; }
    public string FunctionId { get; set; }
}