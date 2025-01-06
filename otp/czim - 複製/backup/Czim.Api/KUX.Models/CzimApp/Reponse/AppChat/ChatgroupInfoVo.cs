using System;
using System.Collections.Generic;
using KUX.Models.CzimApp.Reponse.AppMember;

namespace KUX.Models.CzimApp.Reponse.AppChat;

/// <summary>
/// 群组信息
/// </summary>
public class ChatgroupInfoVo
{

    /// <summary>
    /// 总人数
    /// </summary>
    /// <value></value>
    public int Count { get; set; }

    /// <summary>
    /// 群管理员信息
    /// </summary>
    /// <value></value>
    public List<AppChatContactsVo> Managers { get; set; }

    /// <summary>
    /// 环信id
    /// </summary>
    public string EaseChatgroupId { get; set; }
    /// <summary>
    /// 群名称
    /// </summary>
    public string GroupName { get; set; }
    /// <summary>
    /// 群图标
    /// </summary>
    public string GroupImg { get; set; }
    /// <summary>
    /// 群公告
    /// </summary>
    public string Announcement { get; set; }
    /// <summary>
    /// 群组描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 群组类型：true：公开群，false：私有群。是否
    /// </summary>
    public bool IsPublic { get; set; }
    /// <summary>
    /// 全员禁言
    /// </summary>
    public bool IsProhibit { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CrtDate { get; set; }
}