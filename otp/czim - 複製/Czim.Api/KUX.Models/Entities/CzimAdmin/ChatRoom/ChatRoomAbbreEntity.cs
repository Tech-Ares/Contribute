using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.Entities.CzimAdmin.ChatRoom
{
    /// <summary>
    /// 聊天室缩略模型
    /// </summary>
    public class ChatRoomAbbreEntity
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// 人数上限
        /// </summary>
        public int UserNum { get; set; }

        /// <summary>
        /// 聊天室名称
        /// </summary>
        public string ChatRoomName { get; set; }

        /// <summary>
        /// 聊天室封面
        /// </summary>
        public string ChatRoomImg { get; set; }
    }
}
