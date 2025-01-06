namespace KUX.Models.CzimApp.Reponse.AppMember;

/// <summary>
/// App 联系人模型
/// </summary>
public class AppChatContactsVo
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
    /// 目标类型
    /// 0：单聊
    /// 1：群组
    /// 2：聊天室
    /// </summary>
    /// <value></value>
    public int CzType { get; set; }
}