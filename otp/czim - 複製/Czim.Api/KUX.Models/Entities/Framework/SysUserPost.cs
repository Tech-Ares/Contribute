using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 用户与岗位绑定表
/// </summary>
public class SysUserPost : StringKeyEntity
{
    /// <summary>
    /// 账户Id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 岗位Id
    /// </summary>
    public string PostId { get; set; }
}