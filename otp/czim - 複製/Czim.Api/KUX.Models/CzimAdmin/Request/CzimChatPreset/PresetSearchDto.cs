using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatPreset
{
    /// <summary>
    /// 聊天预设信息查询信息
    /// </summary>
    public class PresetSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 聊天内容
        /// </summary>
        public string Context { get; set; }
    }
}
