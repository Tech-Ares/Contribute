using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Services.EaseImServices.Models
{
    /// <summary>
    /// 群组批量删除用户返回对象
    /// </summary>
    public class GroupDeleteUserListVo
    {
        public bool result { get; set; }
        public string action { get; set; }
        public string reason { get; set; }
        public string user { get; set; }
        public string groupid { get; set; }
    }
}
