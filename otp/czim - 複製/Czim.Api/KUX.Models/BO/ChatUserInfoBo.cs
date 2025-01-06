using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.BO
{
    /// <summary>
    /// 聊天用户信息
    /// </summary>
    public class ChatUserInfoBo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}
