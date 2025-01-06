using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request.CzimMemberLogin
{
    /// <summary>
    /// 登录日志查询dto
    /// </summary>
    public class MemberLoginSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
    }
}
