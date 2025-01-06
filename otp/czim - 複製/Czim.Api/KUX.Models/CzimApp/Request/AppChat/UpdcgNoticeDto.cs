namespace KUX.Models.CzimApp.Request.AppChat;

/// <summary>
/// 更新群公告
/// </summary>
public class UpdcgNoticeDto
{
    /// <summary>
    /// 群id（czimid或者环信id）
    /// </summary>
    public string ChatgroupId { get; set; }

    /// <summary>
    /// 群公告数据
    /// </summary>
    public string Announcement { get; set; }
}