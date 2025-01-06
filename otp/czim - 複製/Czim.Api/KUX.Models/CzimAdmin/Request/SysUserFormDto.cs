using KUX.Models.Entities.Framework;
using System.Collections.Generic;

namespace KUX.Models.DTO;

/// <summary>
/// 系统账户表单
/// </summary>
public class SysUserFormDto
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public SysUser Form { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<string> RoleIds { get; set; }

    /// <summary>
    /// 岗位id
    /// </summary>
    public List<string> PostIds { get; set; }

}