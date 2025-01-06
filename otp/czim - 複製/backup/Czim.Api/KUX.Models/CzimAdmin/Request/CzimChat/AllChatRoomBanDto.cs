using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChat
{
    /// <summary>
    /// 设置用户全局禁言对象
    /// </summary>

    public class AllChatRoomBanDto
    {
        /// <summary>
        /// app端用户id
        /// </summary>
        public string MemberId { get; set; }
        ///// <summary>
        ///// 单聊禁言时长(天数)
        ///// 0表示取消该帐号的单聊消息禁言 -1表示该帐号被设置永久禁言
        ///// </summary>
        ////public int AloneMuteTime { get; set; }
        /// <summary>
        /// 聊天室消息禁言时间(天数)
        /// 0表示取消该帐号的单聊消息禁言 -1表示该帐号被设置永久禁言
        /// </summary>
        public int Day { get; set; }
    }
}
