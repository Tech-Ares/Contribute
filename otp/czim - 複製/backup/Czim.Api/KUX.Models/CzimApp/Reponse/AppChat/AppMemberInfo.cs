
using KUX.Models.Enums;
namespace KUX.Models.CzimApp.Reponse.AppChat;
/// <summary>
/// 会员明细
/// </summary>
public class AppMemberInfo
{
    /// <summary>
    /// 目标id
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 是否全局禁言
    /// </summary>
    /// <value></value>
    public bool IsForbidden { get; set; }

    /// <summary>
    /// 会员编号
    /// </summary>
    /// <value></value>
    public string BsvNumber { get; set; }

    /// <summary>
    /// 是否好友关系
    /// </summary>
    /// <value></value>
    public bool IsFriend { get; set; } = false;

    /// <summary>
    /// 性别
    /// </summary>
    /// <value></value>
    public GenderEnum Gender { get; set; }
}