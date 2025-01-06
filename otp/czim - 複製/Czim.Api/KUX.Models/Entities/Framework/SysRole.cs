﻿using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 角色
/// </summary>
public class SysRole : StringKeyEntity
{
    /// <summary>
    /// 编号
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 能否删除
    /// </summary>
    public int IsDelete { get; set; } = 1;
}