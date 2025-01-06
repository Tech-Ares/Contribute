namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 聊天室添加成员模型
/// </summary>
public class ChatRoomUser
{
    /// <summary>
    /// 添加结果，true表示添加成功，false表示添加失败
    /// </summary>
    /// <value></value>
    public bool result { get; set; }
    /// <summary>
    /// 执行的操作，add_member表示向聊天室添加成员
    /// </summary>
    /// <value></value>
    public string action { get; set; }
    /// <summary>
    /// 聊天室 ID，聊天室唯一标识符，由环信服务器生成。
    /// </summary>
    /// <value></value>
    public string id { get; set; }
    /// <summary>
    /// 添加到聊天室的用户
    /// </summary>
    /// <value></value>
    public string user { get; set; }

}