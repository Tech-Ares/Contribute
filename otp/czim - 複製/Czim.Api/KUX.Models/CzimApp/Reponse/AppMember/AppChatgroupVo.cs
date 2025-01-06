namespace KUX.Models.CzimApp.Reponse.AppMember;

/// <summary>
/// App 群模型
/// </summary>
public class AppChatgroupVo
{
    /// <summary>
    /// 目标id（环信群id）
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    /// 聊天室id（czim的聊天室id）
    /// </summary>
    public string ChatgroupId { get; set; }

    /// <summary>
    /// 群公告
    /// </summary>
    public string Announcement { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string GroupName { get; set; }
}