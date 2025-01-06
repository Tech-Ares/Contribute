using System.Collections.Generic;

namespace KUX.Models.CzimApp.Request.AppChat
{
    /// <summary>
    /// 用户id
    /// </summary>
    public class ExtSearchDto
    {
        /// <summary>
        /// 目标id
        /// </summary>
        public List<string> TargetIds { get; set; }
    }
}