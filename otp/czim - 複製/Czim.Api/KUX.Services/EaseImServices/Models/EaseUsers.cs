namespace KUX.Services.EaseImServices.Models;
/// <summary>
/// 创建user结果
/// </summary>
public class EaseUsers
{
    /// <summary>
    /// 用户的UUID，标识字段
    /// </summary>
    public string uuid { get; set; }

    /// <summary>
    /// “user”用户类型
    /// </summary>
    public string type { get; set; }

    /// <summary>
    ///  用户名，也就是环信 ID
    /// </summary>
    public string username { get; set; }

    /// <summary>
    /// 昵称（可选），在 iOS Apns 推送时会使用的昵称（仅在推送通知栏内显示的昵称），
    /// 并不是用户个人信息的昵称，环信是不保存用户昵称，
    /// 头像等个人信息的，需要自己服务器保存并与给自己用户注册的IM用户名绑定
    /// </summary>
    public string nickname { get; set; }

    /// <summary>
    /// 用户是否已激活，“true”已激活，“false“封禁，封禁需要通过解禁接口进行解禁，才能正常登录
    /// </summary>
    public bool activated { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public long created { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public long modified { get; set; }
}
