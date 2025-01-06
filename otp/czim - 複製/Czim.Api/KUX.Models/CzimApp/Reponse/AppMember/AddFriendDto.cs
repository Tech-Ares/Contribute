namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 添加好友
/// </summary>
public class AddFriendDto
{
    /// <summary>
    /// 目标id
    /// </summary>
    /// <value></value>
    public string TargetId { get; set; }

    /// <summary>
    /// 申请备注
    /// </summary>
    /// <value></value>
    public string Remarks { get; set; }

}