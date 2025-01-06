namespace KUX.Models.CzimApp.Request.AppSystem;

/// <summary>
/// 版本信息
/// </summary>
public class SystemVersionDto
{
    /// <summary>
    /// 版本
    /// </summary>
    public long Code { get; set; }

    /// <summary>
    /// 手机系统
    /// 1：安卓
    /// 2：苹果
    /// </summary>
    public int PhoneSystem { get; set; }

    /// <summary>
    /// 渠道
    /// </summary>
    public string Channel { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    public string Version { get; set; }
}