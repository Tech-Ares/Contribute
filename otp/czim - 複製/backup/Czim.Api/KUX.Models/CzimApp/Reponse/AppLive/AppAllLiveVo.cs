using System;

namespace KUX.Models.CzimApp.Reponse.AppLive;
/// <summary>
/// 所有直播信息
/// </summary>
public class AppAllLiveVo
{
    /// <summary>
    /// 直播id
    /// </summary>
    /// <value></value>
    public string Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    /// <value></value>
    public DateTime CrtDate { get; set; }

    /// <summary>
    /// 视频地址
    /// </summary>
    /// <value></value>
    public string VideoUrl { get; set; }
    /// <summary>
    /// 文本内容
    /// </summary>
    /// <value></value>
    public string Content { get; set; }
    /// <summary>
    /// 封面图片
    /// </summary>
    /// <value></value>
    public string HeadPicture { get; set; }

    /// <summary>
    /// 直播状态
    /// </summary>
    /// <value></value>
    public int LiveStatus { get; set; }
    /// <summary>
    /// 点赞人数
    /// </summary>
    /// <value></value>
    public int GiveLikeCount { get; set; }

}
