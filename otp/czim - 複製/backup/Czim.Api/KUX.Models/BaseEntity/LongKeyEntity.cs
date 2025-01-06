using System.ComponentModel.DataAnnotations;

namespace KUX.Models.BaseEntity;

/// <summary>
/// 基础模型
/// </summary>
public class LongKeyEntity : DefaultBaseEntity<long>
{
    /// <summary>
    /// 主键id
    /// </summary>
    [Key]
    public override long Id { get; set; } = 0;
}