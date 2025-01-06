using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 聊天室客服好友列表对象
    /// </summary>
    public class ChatCustomerOfFriendDto: BaseSearchDto
    {
        /// <summary>
        /// web会员id
        /// </summary>
        public string UserId { get; set; }
    }
}
