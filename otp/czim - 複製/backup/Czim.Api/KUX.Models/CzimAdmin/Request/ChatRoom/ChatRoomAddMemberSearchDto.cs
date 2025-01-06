using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatRoom
{
    /// <summary>
    /// 查询所有可加入聊天室的会员
    /// </summary>
    public class ChatRoomAddMemberSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
