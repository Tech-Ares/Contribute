using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 修改web账户密码对象
    /// </summary>
    public class UpdatePasswordDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
