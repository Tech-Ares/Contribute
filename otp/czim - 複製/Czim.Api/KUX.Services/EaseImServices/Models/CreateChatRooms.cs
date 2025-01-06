using System.Collections.Generic;

namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 创建聊天室
/// </summary>
public class CreateChatRooms
{
    /// <summary>
    ///  聊天室名称，此属性为必须的
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 聊天室描述，此属性为必须的
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// 聊天室成员最大数（包括聊天室创建者），值为数值类型，此属性为可选的
    /// </summary>
    public string maxusers { get; set; }

    /// <summary>
    /// 聊天室的管理员，此属性为必须的
    /// </summary>
    public string owner { get; set; }

    /// <summary>
    /// 聊天室成员，此属性为可选的，但是如果加了此项，数组元素至少一个
    /// </summary>
    public List<string> members { get; set; }
}