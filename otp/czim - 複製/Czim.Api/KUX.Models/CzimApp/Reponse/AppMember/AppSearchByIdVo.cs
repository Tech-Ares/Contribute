using KUX.Models.CzimApp.Reponse.AppMember;
using KUX.Models.Enums;

/// <summary>
/// 查询会员信息
/// </summary>
public class AppSearchByIdVo : AppSearchContactsVo
{
    /// <summary>
    /// 登录名
    /// </summary>
    /// <value></value>
    public string LoginName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Introduce { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Gender { get; set; }
}