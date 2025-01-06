using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.ChatRoom
{
    /// <summary>
    /// 聊天室查询dto
    /// </summary>
    public class ChatRoomSearchDto : BaseSearchDto
    {
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
