using System.Collections.Generic;

namespace KUX.Models.CzimAdmin.Request.CzimChat
{
    /// <summary>
    /// 查询扩展信息
    /// </summary>
    public class ExtSearchDto
    {
        /// <summary>
        /// 目标id
        /// </summary>
        public List<string> TargetIds { get; set; }
    }
}