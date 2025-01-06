using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatRoom
{
    /// <summary>
    /// 开启关闭全员禁言
    /// </summary>
    public class ChatRoomProhibitDto
    {
        /// <summary>
        /// 群组id
        /// </summary>
        public string ChatGroupId { get; set; }
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }
        /// <summary>
        /// 全员禁言
        /// true:开启
        /// false:关闭
        /// </summary>
        public bool IsProhibit { get; set; }
        /// <summary>
        /// 禁言时间(小时)
        /// </summary>
        //public long Time { get; set; }
    }
}
