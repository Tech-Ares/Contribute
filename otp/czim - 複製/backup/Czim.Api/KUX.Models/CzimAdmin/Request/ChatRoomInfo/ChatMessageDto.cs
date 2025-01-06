using KUX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatInfo
{
    /// <summary>
    /// 聊天消息模型
    /// </summary>
    public class ChatMessageDto
    {
        /// <summary>
        /// 1：单聊
        /// 2：群聊
        /// </summary>
        public int ChatType { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        public string ReceiveId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public ChatMessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
    }
}
