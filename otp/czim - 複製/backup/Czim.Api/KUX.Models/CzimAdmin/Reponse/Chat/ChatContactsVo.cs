namespace KUX.Models.CzimAdmin.Reponse.Chat;

/// <summary>
/// 聊天联系人信息
/// </summary>
public class ChatContactsVo
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
    /// 系统id
    /// </summary>
    public string CzimId { get; set; }

    /// <summary>
    /// 目标类型
    /// 0:signal
    /// 1:group
    /// 2:chatroom
    /// </summary>
    /// <value></value>
    public int CzType { get; set; } = 0;

    public int Maxusers { get; set; }

    /// <summary>
    /// 全员禁言
    /// </summary>
    public bool IsProhibit { get; set; }
}