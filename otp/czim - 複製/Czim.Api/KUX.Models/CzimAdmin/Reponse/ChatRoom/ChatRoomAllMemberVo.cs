using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Reponse.ChatRoom
{
    /// <summary>
    /// 聊天室所有成员 对象
    /// </summary>
    public class ChatRoomAllMemberVo
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 加入时间 
        /// </summary>
        public DateTime JoinDate { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 是否是管理员 
        /// </summary>
        public int IsManager { get; set; }
        /// <summary>
        /// 禁言时间 
        /// </summary>
        public DateTime ForbiddenTime { get; set; }
        /// <summary>
        /// 是否被禁言
        /// </summary>
        public int IsForbidden 
        {
            get { return ForbiddenTime > DateTime.Now ? 1 : 0; }
           
        }
    }
}
