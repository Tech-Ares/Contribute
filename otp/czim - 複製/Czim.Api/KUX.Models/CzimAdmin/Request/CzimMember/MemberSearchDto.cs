namespace KUX.Models.KuxAdmin.Request.CzimMember;

/// <summary>
/// 角色查询模型
/// </summary>
public class MemberSearchDto : BaseSearchDto
{
    /// <summary>
    /// 角色名
    /// </summary>
    public string NickName { get; set; }
    /// <summary>
    /// 会员id
    /// </summary>
    public string MemberId { get; set; }
}
