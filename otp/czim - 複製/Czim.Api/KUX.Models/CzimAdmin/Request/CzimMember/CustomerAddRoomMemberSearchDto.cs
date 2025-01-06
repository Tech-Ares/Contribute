using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimMember
{
    /// <summary>
    /// 查询所有可被客服添加的会员
    /// </summary>
    public class CustomerAddRoomMemberSearchDto:BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
