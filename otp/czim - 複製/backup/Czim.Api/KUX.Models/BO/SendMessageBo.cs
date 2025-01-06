using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.BO
{
    /// <summary>
    /// 消息发送模型
    /// </summary>
    public class SendMessageBo
    {
        /// <summary>
        /// 发送人信息
        /// </summary>
        public ChatUserInfoBo SendUser { get; set; }

        /// <summary>
        /// 发送内容信息
        /// </summary>
        public ChatMessageBo SendContent { get; set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime SendDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 接收人id
        /// </summary>
        public string ReceiveId { get; set; }

        /// <summary>
        /// 当前消息唯一识别
        /// </summary>
        public string SendMessageId { get; set; }
    }
}
