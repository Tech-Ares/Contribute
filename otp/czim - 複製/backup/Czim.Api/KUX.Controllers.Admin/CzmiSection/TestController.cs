using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using KUX.Services.EaseImServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection
{
    public class TestController : AdminBaseController<CzimChatCustomerService>
    {
        /// <summary>
        /// 环信服务
        /// </summary>
        private readonly EaseImService easeImService;
        public TestController(CzimChatCustomerService defaultService, IAccountService _accountService, EaseImService _easeImService) : base(defaultService, _accountService)
        {
            easeImService=_easeImService;
        }
        [HttpPost("AllBan/{memberId}/{Day}")]
        [ApiResourceCacheFilter(1)]
        [ActionDescriptor("AllBan")]
        public async Task<bool> AllBan(string memberId,int Day)
        {
            var calculate = 24 * 60 * 60 * 1000;
            var imInfo =await easeImService.ChatCustomerAllBan(memberId, Day * calculate);
            return imInfo;
        }
        [HttpPost("OneBan/{userid}/{groupid}/{day}")]
        [ApiResourceCacheFilter(1)]
        [ActionDescriptor("OneBan")]
        public async Task<bool> OneBan(string userid,string groupid,int day)
        {
            var millisecond = day * 24 * 60 * 60 * 1000;
            //当前时间减去1970的毫秒数
            var timp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
            var sum = millisecond + timp;
            var isadmin = await this.easeImService.ChatgroupDeleteManagerAsync(groupid,userid);
            var imInfo = await easeImService.ChatGroupMuteUser(new List<string>(){userid},groupid, sum);
            return imInfo[0].result;
        }
    }
}
