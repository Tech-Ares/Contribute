using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 客服添加好友dto
    /// </summary>
    public class CustomerAddFriendDto
    {
        /// <summary>
        /// 好友id（管理员userId）
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 会员id(app会员id)
        /// </summary>
        public string MemberId { get; set; }
    }
}
