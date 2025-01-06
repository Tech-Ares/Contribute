using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimAdmin.Request.CzimBaseSetting;
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
    /// 基础服务
    /// </summary>
    public class CzimBaseSettingController : AdminBaseController<CzimBaseSettingService>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="defaultService">默认服务</param>
        /// <param name="_accountService">用户服务</param>
        public CzimBaseSettingController(CzimBaseSettingService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
        {

        }
        //修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto">保存/修改对象</param>
        /// <returns></returns>
        [HttpPost("saveorupdate")]
        [ApiResourceCacheFilter(1)]
        [ActionDescriptor("SaveOrUpdateAsync")]
        public async Task<object> SaveOrUpdateAsync(CzimBaseSettingAddDto dto)
        {
            var result =await DefaultService.SaveOrUpdateAsync(dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }
        //查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("findlist")]
        [ApiResourceCacheFilter(1)]
        [ActionDescriptor("FindListAsync")]
        public async Task<PageViewModel> FindListAsync(CzimBaseSettingSearchDto dto)
        {
            var result = await DefaultService.FindListAsync(dto);
            
            return result;
        }
    }
}
