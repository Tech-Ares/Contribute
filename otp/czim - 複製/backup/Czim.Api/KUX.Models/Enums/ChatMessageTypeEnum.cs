using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.Enums
{
    /// <summary>
    /// 聊天消息类型枚举
    /// </summary>
    public enum ChatMessageTypeEnum
    {
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = -1,

        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text = 1,

        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Picture = 2,

        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        Voice = 3,

        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video = 4,

        /// <summary>
        /// 商品链接
        /// </summary>
        [Description("商品链接")]
        ProductLink = 5,
    }
}
