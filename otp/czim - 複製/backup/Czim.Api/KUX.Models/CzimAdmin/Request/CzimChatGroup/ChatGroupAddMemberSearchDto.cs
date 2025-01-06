using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 查询所有加入群组的会员
    /// </summary>
    public class ChatGroupAddMemberSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 群组id
        /// </summary>
        public string ChatGroupId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
