using KUX.Models.BaseEntity;

namespace KUX.Models.Entities.Framework;

/// <summary>
/// 系统账号
/// </summary>
public class SysUser : StringKeyEntity
{
    /// <summary>
    /// 账户名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 登录账号
    /// </summary>
    public string LoginName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 账户邮件地址
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 能否删除
    /// </summary>
    public int IsDelete { get; set; } = 1;

    /// <summary>
    /// 组织Id
    /// </summary>
    public string OrganizationId { get; set; }
}