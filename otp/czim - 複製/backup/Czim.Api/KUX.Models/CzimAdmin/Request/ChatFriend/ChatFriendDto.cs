using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.ChatFriend
{
    /// <summary>
    /// 会员好友查询dto
    /// </summary>
    public class ChatFriendDto : BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
    }
}
