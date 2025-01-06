using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimLiveInfo
{
    /// <summary>
    /// 直播添加/修改对象
    /// </summary>
    public class CzimLiveInfoSaveDto
    {
        /// <summary>
        /// 创作id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 直播标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 直播时间（开始时间）
        /// </summary>
        public DateTime LiveDate { get; set; }
        /// <summary>
        /// 直播结束时间 
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 创作人id
        /// </summary>
        //public string CrtMemberId { get; set; }
        /// <summary>
        /// 录播地址
        /// </summary>
        public string VideoUrl { get; set; }
    }
}
