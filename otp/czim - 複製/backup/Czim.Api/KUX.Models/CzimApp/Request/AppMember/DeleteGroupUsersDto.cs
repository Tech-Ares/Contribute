using System.Collections.Generic;

namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 群组移除用户
/// </summary>
public class DeleteGroupUsersDto
{
    /// <summary>
    /// 人员列表
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