using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatPreset
{
    /// <summary>
    /// 预设聊天消息添加dto
    /// </summary>
    public class CzimChatPresetAddDto
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Abbre { get; set; }
        /// <summary>
        /// 模糊查询
        /// </summary>
        public string Vague { get; set; }
    }
}
