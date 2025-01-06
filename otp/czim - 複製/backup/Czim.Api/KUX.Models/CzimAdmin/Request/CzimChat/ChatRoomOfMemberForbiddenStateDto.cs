using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChat
{
    /// <summary>
    /// 获取当前聊天室用户的禁言状态
    /// </summary>
    public class ChatRoomOfMemberForbiddenStateDto
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
    }
}
