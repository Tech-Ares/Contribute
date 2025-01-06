using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.Enums
{
    /// <summary>
    /// 环信消息类型枚举
    /// </summary>
    public enum CzTypeEnum
    {
        /// <summary>
        /// 单聊
        /// </summary>
        [Description("单聊")]
        SingleChat = 0,

        /// <summary>
        /// 群组
        /// </summary>
        [Description("群组")]
        GroupChat = 1,

        /// <summary>
        /// 聊天室
        /// </summary>
        [Description("聊天室")]
        ChatRoom = 2,

    }
}
