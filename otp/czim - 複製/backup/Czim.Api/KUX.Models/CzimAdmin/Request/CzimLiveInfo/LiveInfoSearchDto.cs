using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimLiveInfo
{
    /// <summary>
    /// 直播查询对象
    /// </summary>
    public class LiveInfoSearchDto: BaseSearchDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 直播开始时间 
        /// </summary>
        public DateTime LiveDate { get; set; }
        /// <summary>
        /// 直播结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
