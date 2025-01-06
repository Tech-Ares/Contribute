using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 群组添加管理员对象
    /// </summary>
    public class GroupAddManagerDto
    {
        /// <summary>
        /// 群组id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
    }
}
