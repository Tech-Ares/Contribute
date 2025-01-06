using KUX.Models.Entities.Framework;
using System.Collections.Generic;

namespace KUX.Models.DTO;

/// <summary>
/// 系统菜单dto
/// </summary>
public class SysMenuFormDto
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public SysMenu Form { get; set; }

    /// <summary>
    /// 方法ids
    /// </summary>
    public List<string> FunctionIds { get; set; }
}