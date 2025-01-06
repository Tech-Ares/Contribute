using KUX.Models.Enums;

namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 会员基本信息
/// </summary>
public class MemberBaseInfoDto
{
    /// <summary>
    /// 会员昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Mailbox { get; set; }

    /// <summary>
    /// 会员头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Introduce { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Gender { get; set; }
}