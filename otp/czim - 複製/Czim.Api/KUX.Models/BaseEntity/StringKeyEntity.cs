using System;
using System.ComponentModel.DataAnnotations;

namespace KUX.Models.BaseEntity;

/// <summary>
/// 基础模型
/// 包含属性 Id、UpdateTime、CreateTime
/// </summary>
public class StringKeyEntity : DefaultBaseEntity<string>
{
    /// <summary>
    /// 主键id
    /// </summary>
    [Key]
    public override string Id { get; set; } = Guid.NewGuid().ToString("N");
}