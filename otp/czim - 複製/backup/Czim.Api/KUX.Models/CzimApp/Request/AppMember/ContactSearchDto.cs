namespace KUX.Models.CzimApp.Request.AppMember;

/// <summary>
/// 查询联系人
/// </summary>
public class ContactSearchDto
{

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 会员编号
    /// </summary>
    public string CzimNumber { get; set; }

    /// <summary>
    /// 登录名
    /// </summary>
    /// <value></value>
    public string LoginName { get; set; }

}