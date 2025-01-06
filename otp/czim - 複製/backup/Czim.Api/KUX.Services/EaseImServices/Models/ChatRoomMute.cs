
namespace KUX.Services.EaseImServices.Models;
/// <summary>
/// 聊天室禁言
/// </summary>
public class ChatRoomMute
{
    /// <summary>
    /// 操作结果；true：添加成功；false：添加失败
    /// </summary>
    /// <value></value>
    public bool result { get; set; }
    /// <summary>
    /// 	禁言到期的时间戳（从1970年1月1日开始的毫秒数。如果禁言时间传的值为“-1”，
    /// 那么该时间戳为固定的4638873600000，请参考mute_duration参数的说明）
    /// </summary>
    /// <value></value>
    public long expire { get; set; }
    /// <summary>
    /// 被禁言用户的 ID
    /// </summary>
    /// <value></value>
    public string user { get; set; }
}