namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 聊天室管理员
/// </summary>
public class ChatRoomAdmin
{
    /// <summary>
    /// 操作结果；success：添加成功
    /// </summary>
    /// <value></value>
    public string result { get; set; }
    /// <summary>
    /// 添加为聊天室管理员的用户 ID
    /// </summary>
    public string newadmin { get; set; }
}