namespace KUX.Models.CzimAdmin.Request.CzimMember;

/// <summary>
/// 新增会员模型
/// </summary>
public class MemberAddDto
{
    /// <summary>
    /// 推广员类型 1：机器人 2：普通用户
    /// </summary>
    public int MemberType { get; set; }
    /// <summary>
    /// 增加数量
    /// </summary>
    public int AddCount { get; set; }

    /// <summary>
    /// 性别            1：男2：女0：未知9：未说明
    /// </summary>
    public int Gender { get; set; }
    /// <summary>
    /// 登录名
    /// </summary>
    public string LoginName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd { get; set; }
}