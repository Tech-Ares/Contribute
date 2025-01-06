using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatRoom
{
    /// <summary>
    /// 聊天室会员禁言
    /// </summary>
    public class ChatRoomMemberForbiddenDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 群组id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 禁言天数
        /// </summary>
        public int Day { get; set; } = -1;
    }
}
