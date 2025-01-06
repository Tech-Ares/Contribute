using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    public class UpdateUserInfoDto
    {
        
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
