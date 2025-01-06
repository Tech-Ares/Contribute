using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 群组查询dto
    /// </summary>
    public class ChatGroupSearchDto: BaseSearchDto
    {
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群组id
        /// </summary>
        public string GroupId { get; set; }
    }
}
