using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.CzimMember
{
    /// <summary>
    /// 会员加群查询dto
    /// </summary>
    public class MemberJoinGroupSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
    }
}
