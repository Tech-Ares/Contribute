using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Services.EaseImServices.Models
{
    /// <summary>
    /// 添加/删除群成员 返回对象
    /// </summary>
    public class GroupAddUserVo
    {
        public bool result { get; set; }
        public string groupid { get; set; }
        public string action { get; set; }
        public string user { get; set; }
    }
}
