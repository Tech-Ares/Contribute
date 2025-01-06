using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Reponse.ChatRoom
{
    /// <summary>
    /// 聊天室信息
    /// </summary>
    public class ChatRoomInfoVo
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 聊天室等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 管理员id
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// 人数上限
        /// </summary>
        public int UserNum { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// 聊天室名
        /// </summary>
        public string ChatRoomName { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string ChatRoomImg { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string ChatRoomNotice { get; set; }

        /// <summary>
        /// 是否开放
        /// </summary>
        public bool IsOpenUp { get; set; }

        /// <summary>
        /// 当前在线人数
        /// </summary>
        public int OnLineNum { get; set; }

        /// <summary>
        /// 是否全员禁言
        /// </summary>
        public bool IsProhibit { get; set; }

        /// <summary>
        /// 是否默认聊天室
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
