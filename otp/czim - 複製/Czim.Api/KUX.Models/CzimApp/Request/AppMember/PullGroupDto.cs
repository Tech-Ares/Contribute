using System.Collections.Generic;

namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 群组拉人信息
/// </summary>
public class PullGroupDto
{
    /// <summary>
    /// 好友列表
    /// </summary>
    public List<string> MemberIds { get; set; }

    /// <summary>
    /// 群id/环信id
    /// </summary>
    /// <value></value>
    public string ChatgroupId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <value></value>
    public string Remarks { get; set; }
}