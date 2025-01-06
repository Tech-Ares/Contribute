using System.Collections.Generic;

namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 添加好友到群
/// </summary>
public class AddChatRoomDto
{
    /// <summary>
    /// 目标id
    /// </summary>
    /// <value></value>
    public string TargetId { get; set; }

    /// <summary>
    /// 好友列表
    /// </summary>
    public List<string> TargetIds { get; set; }

    /// <summary>
    /// 群id
    /// </summary>
    /// <value></value>
    public string ChatRoomId { get; set; }

    /// <summary>
    /// 申请备注
    /// </summary>
    /// <value></value>
    public string Remarks { get; set; }
}