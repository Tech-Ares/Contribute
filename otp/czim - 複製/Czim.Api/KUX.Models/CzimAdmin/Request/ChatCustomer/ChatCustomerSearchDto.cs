using KUX.Models.KuxAdmin.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.ChatCustomer
{
    /// <summary>
    /// 客服人员查询
    /// </summary>
    public class ChatCustomerSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 客服昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
