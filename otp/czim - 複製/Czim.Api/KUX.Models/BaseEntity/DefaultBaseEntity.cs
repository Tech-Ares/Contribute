using System;
using System.ComponentModel.DataAnnotations;

namespace KUX.Models.BaseEntity;

/// <summary>
/// 基础模型
/// 包含属性 UpdateTime、CreateTime
/// </summary>
public class DefaultBaseEntity
{
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 创建人
    /// </summary>
    public virtual string CUser { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public virtual string UUser { get; set; }

    /// <summary>
    /// 是否可用(默认可用)
    /// </summary>
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// 基础模型
/// 包含属性 Id、UpdateTime、CreateTime
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class DefaultBaseEntity<TKey> : DefaultBaseEntity
{
    [Key]
    public virtual TKey Id { get; set; } = default;
}