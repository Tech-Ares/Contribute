using System;

namespace KUX.Models.CzimApp.AppBo
{
    /// <summary>
    /// 聊天室信息
    /// </summary>
    public class MemberChatRoomBo
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JoinDate { get; set; }

        /// <summary>
        /// 禁言时间
        /// </summary>
        public DateTime ForbiddenTime { get; set; }
    }
}