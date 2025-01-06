using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimAdmin.Request.CzimChatPreset;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection
{
    /// <summary>
    /// 预设聊天消息
    /// </summary>
    public class CzimChatPresetController : AdminBaseController<CzimChatPresetService>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="defaultService">默认控制器</param>
        /// <param name="_accountService">用户服务</param>
        public CzimChatPresetController(CzimChatPresetService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
        {

        }
        /// <summary>
        /// 获取所有预设列表
        /// </summary>
        /// <param name="gsd">商品查询条件</param>
        /// <returns></returns>
        [HttpPost("findlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindListAsync")]
        public async Task<PageViewModel> FindListAsync([FromBody] PresetSearchDto gsd)
        {
            var result = await this.DefaultService.FindListAsync(gsd);
            return result;
        }
        /// <summary>
        /// 保存/修改
        /// </summary>
        /// <param name="gsd">商品查询条件</param>
        /// <returns></returns>
        [HttpPost("saveform")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("SaveFormAsync")]
        public async Task<object> SaveFormAsync([FromBody] CzimChatPresetAddDto gsd)
        {
            var result = await this.DefaultService.SaveFormAsync(this.UserId,gsd);
            return result;
        }
        /// <summary>
        /// 激活/失效
        /// </summary>
        /// <param name="gsd">商品查询条件</param>
        /// <returns></returns>
        [HttpPost("isactiveform")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("IsActiveFormAsync")]
        public async Task<object> IsActiveFormAsync([FromBody] CzimChatPresetActiveDto gsd)
        {
            var result = await this.DefaultService.IsActiveFormAsync(this.UserId, gsd);
            return result;
        }
    }
}
