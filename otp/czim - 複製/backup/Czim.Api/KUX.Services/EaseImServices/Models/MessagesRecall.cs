using System.Collections.Generic;

namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 消息撤回响应值
/// </summary>
public class MessagesRecall
{
    /// <summary>
    /// 所有消息列表
    /// </summary>
    /// <value></value>
    public List<MessagesRecallInfo> msgs { get; set; }

}

/// <summary>
/// 消息撤回内容
/// </summary>
public class MessagesRecallInfo
{

    /// <summary>
    /// 消息id
    /// </summary>
    /// <value></value>
    public string msg_id { get; set; }

    /// <summary>
    /// 返回结果，成功是yes
    /// </summary>
    /// <value></value>
    public string recalled { get; set; }

    /// <summary>
    /// 消息撤回方
    /// </summary>
    /// <value></value>
    public string from { get; set; }

    /// <summary>
    /// 撤回消息方
    /// </summary>
    /// <value></value>
    public string to { get; set; }

    /// <summary>
    /// 消息类型， chat或者groupchat
    /// </summary>
    /// <value></value>
    public string chattype { get; set; }
}