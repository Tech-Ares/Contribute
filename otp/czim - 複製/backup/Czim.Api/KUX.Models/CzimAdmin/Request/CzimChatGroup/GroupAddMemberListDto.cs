using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 群组批量添加成员的对象
    /// </summary>
    public class GroupAddMemberListDto
    {
        /// <summary>
        /// 成员id (一次最多六十位)
        /// </summary>
        public List<string> MemberIds { get; set; }
        /// <summary>
        /// 群组id
        /// </summary>
        public string ChatGroupId { get; set; }
    }
}
