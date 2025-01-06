namespace KUX.Models.CzimApp.Reponse.AppSystem;

/// <summary>
/// 系统更新版本
/// </summary>
public class SystemVersionVo
{
    /// <summary>
    /// 版本编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 手机系统1：苹果2：安卓
    /// </summary>
    public int PhoneSystem { get; set; } = 1;

    /// <summary>
    /// 版本编号
    /// </summary>
    public int VersionCode { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 渠道
    /// </summary>
    public string Channel { get; set; } = "";

    /// <summary>
    /// 更新提示
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// 更新内容
    /// </summary>
    public string Content { get; set; } = "";

    /// <summary>
    /// 是否强制更新
    /// </summary>
    public bool Focus { get; set; } = false;

    /// <summary>
    /// 安卓安装包地址
    /// </summary>
    public string ApkDownLoadUrl { get; set; } = "";

    /// <summary>
    /// ios外链地址
    /// </summary>
    public string IosLinkUrl { get; set; } = "";
}