using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 获取当前群组用户的禁言状态
    /// </summary>
    public class ChatGroupOfMemberForbiddenStateDto
    {
        /// <summary>
        /// 群组id
        /// </summary>
        public string ChatGroupId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
    }
}
