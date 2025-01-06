using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatRoom
{
    /// <summary>
    /// 聊天室模型
    /// </summary>
    public class ChatRoomDto
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        public string ChatRoomName { get; set; }

        /// <summary>
        /// 群图标
        /// </summary>
        public string ChatRoomImg { get; set; }

        /// <summary>
        /// 群公告
        /// </summary>
        public string ChatRoomNotice { get; set; }

        /// <summary>
        /// 群等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserNum { get; set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
    }
}
