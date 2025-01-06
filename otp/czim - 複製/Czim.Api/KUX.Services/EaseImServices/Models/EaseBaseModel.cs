using System.Collections.Generic;

namespace KUX.Services.EaseImServices.Models;

/// <summary>
/// 基础接口
/// </summary>
public class EaseReponseModel
{
    /// <summary>
    /// 请求类型
    /// </summary>
    public string action { get; set; }

    /// <summary>
    /// app的uuid
    /// </summary>
    public string application { get; set; }

    /// <summary>
    /// 请求路由
    /// </summary>
    public string path { get; set; }

    /// <summary>
    /// 请求完整url
    /// </summary>
    public string uri { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public long timestamp { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int duration { get; set; }

    /// <summary>
    /// 组织名
    /// </summary>
    public string organization { get; set; }

    /// <summary>
    /// app名
    /// </summary>
    public string applicationName { get; set; }
    public object properties { get; set; }

    public int count { get; set; }
}

/// <summary>
/// 环信接口返回基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T2"></typeparam>
public class EaseBaseModel<T, T2> : EaseReponseModel where T : class
{
    /// <summary>
    /// 返回实体
    /// </summary>
    public List<T> entities { get; set; }

    /// <summary>
    /// 返回数据模型
    /// </summary>
    public List<T2> data { get; set; }
}

/// <summary>
/// 环信接口返回基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T2"></typeparam>
public class EaseCreateChatRoomModel<T, T2> : EaseReponseModel where T : class
{
    /// <summary>
    /// 返回实体
    /// </summary>
    public List<T> entities { get; set; }

    /// <summary>
    /// 返回数据模型
    /// </summary>
    public T2 data { get; set; }
}
/// <summary>
/// data数据模型
/// </summary>
public class EaseDataModel
{
    public string id { get; set; }

    public bool description { get; set; }
    public bool maxusers { get; set; }
    public bool groupname { get; set; }

    public bool success { get; set; }
}

