using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimMember
{
    /// <summary>
    /// 会员添加客服的查询实体
    /// </summary>
    public class MemberAddRoomCustomerSearchDto: BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
