using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Request
{
    /// <summary>
    /// 失效/激活单据
    /// </summary>
    public class AdminActiveDto
    {
        /// <summary>
        /// 表单
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 激活/失效处理
        /// </summary>
        public bool IsActive { get; set; }
    }
}
