using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimMember
{
    /// <summary>
    /// 聊天室添加会员信息
    /// </summary>
    public class ChatRoomAddMemberDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 引入人编号
        /// </summary>
        public string PullId { get; set; }
    }
}
