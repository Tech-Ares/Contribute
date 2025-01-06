using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 添加推广员dto
    /// </summary>
    public class AddAgentDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 是否是推广专员
        /// </summary>
        public int IsAgent { get; set; }
    }
}
