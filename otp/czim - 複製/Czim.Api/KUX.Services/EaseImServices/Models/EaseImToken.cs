namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 环信token模型
/// </summary>
public class EaseImToken
{
    /// <summary>
    /// 当前id的uuid
    /// </summary>
    public string application { get; set; }

    /// <summary>
    /// token
    /// </summary>
    public string access_token { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public long expires_in { get; set; }
}