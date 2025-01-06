using KUX.Models.CzimApp.Request.AppChat;

/// <summary>
/// 消息撤回模型
/// </summary>
public class RecallmessageDto
{
    /// <summary>
    /// 接收人id
    /// </summary>
    public string ReceiveId { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string MsgId { get; set; }

}


