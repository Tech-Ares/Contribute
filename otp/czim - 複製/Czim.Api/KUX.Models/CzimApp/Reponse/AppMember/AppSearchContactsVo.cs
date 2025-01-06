namespace KUX.Models.CzimApp.Reponse.AppMember;

/// <summary>
/// App 联系人模型
/// </summary>
public class AppSearchContactsVo : AppChatContactsVo
{

    /// <summary>
    /// 是否已是好友
    /// </summary>
    public bool IsFriend { get; set; }
}