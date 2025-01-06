namespace KUX.Models.CzimApp.Reponse.AppMember;

/// <summary>
/// App 聊天室模型
/// </summary>
public class AppChatRoomVo
{
    /// <summary>
    /// 目标id（环信聊天室id）
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    /// 群公告
    /// </summary>
    public string ChatRoomNotice { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 聊天室id（czim的聊天室id）
    /// </summary>
    public string ChatRoomId { get; set; }
}