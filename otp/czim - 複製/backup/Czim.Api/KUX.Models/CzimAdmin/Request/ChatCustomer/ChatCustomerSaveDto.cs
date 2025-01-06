using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 保存客服信息
    /// </summary>
    public class ChatCustomerSaveDto
    {
        /// <summary>
        /// app密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 账号名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 是否是推广专员
        /// </summary>
        public bool  IsAgent { get; set; }
    }
}
