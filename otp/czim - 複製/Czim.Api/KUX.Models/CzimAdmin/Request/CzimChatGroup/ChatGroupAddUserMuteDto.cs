using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 群组添加用户禁言对象
    /// </summary>
    public class ChatGroupAddUserMuteDto
    {
        /// <summary>
        /// 禁言天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 群组id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string MemberId { get; set; }
    }
}
