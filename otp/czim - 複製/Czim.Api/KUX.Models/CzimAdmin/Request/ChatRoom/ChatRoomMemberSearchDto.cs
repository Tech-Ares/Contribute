using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.ChatRoom
{
    /// <summary>
    /// 聊天室会员查询接口
    /// </summary>
    public class ChatRoomMemberSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 聊天室名称
        /// </summary>
        public string ChatroomName { get; set; }

        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }
    }
}
