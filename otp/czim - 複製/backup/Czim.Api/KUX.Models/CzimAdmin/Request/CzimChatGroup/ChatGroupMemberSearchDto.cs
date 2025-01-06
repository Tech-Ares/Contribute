using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 当前可加入的群组列表
    /// </summary>
    public class ChatGroupMemberSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string ChatGroupName { get; set; }

        /// <summary>
        /// 群组id
        /// </summary>
        //public string ChatGroupId { get; set; }
    }
}
