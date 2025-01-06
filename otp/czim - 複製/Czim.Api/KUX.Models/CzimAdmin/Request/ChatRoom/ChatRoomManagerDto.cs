using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.ChatRoom
{
    /// <summary>
    /// 聊天室添加/移除管理员
    /// </summary>
    public class ChatRoomManagerDto
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 管理员id
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 添加或移除
        /// 1:添加
        /// 2:移除
        /// </summary>
        public int AddOrDel { get; set; }
    }
}
