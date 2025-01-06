using System.Collections.Generic;

namespace KUX.Models.CzimAdmin.Request.CzimMember;

/// <summary>
/// 群添加成员信息
/// </summary>
public class ChatgroupAddMemberDto
{
    /// <summary>
    /// 会员id
    /// </summary>
    public string MemberId { get; set; }

    /// <summary>
    /// 聊天室id
    /// </summary>
    public string ChatgroupId { get; set; }

    /// <summary>
    /// 引入人编号
    /// </summary>
    public string PullId { get; set; }

}