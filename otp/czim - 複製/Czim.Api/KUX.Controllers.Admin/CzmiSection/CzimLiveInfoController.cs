using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimAdmin.Request.CzimLiveInfo;
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
    /// 直播控制器
    /// </summary>
    public class CzimLiveInfoController : AdminBaseController<CzimLiveInfoService>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="defaultService">默认服务</param>
        /// <param name="_accountService">用户服务</param>
        public CzimLiveInfoController(CzimLiveInfoService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
        {

        }
        /// <summary>
        /// 增加/修改直播
        /// </summary>
        /// <param name="dto">直播对象</param>
        /// <returns>成功/失败</returns>
        [HttpPost("saveorupdateliveinfo")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("SaveOrUpdateLiveInfoAsync")]

        public async Task<object> SaveOrUpdateLiveInfoAsync(CzimLiveInfoSaveDto dto)
        {
            var result =await DefaultService.SaveOrUpdateLiveInfoAsync(this.UserId,dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }
        //启动禁用
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="dto">启用/禁用对象</param>
        /// <returns></returns>
        [HttpPost("isactive")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("IsActiveAsync")]
        public async Task<(bool, string)> IsActiveAsync(LiveInfoIsActiveDto dto)
        {
            var result = await DefaultService.IsActiveAsync(UserId, dto);
            return result;
        }
        /// <summary>
        /// 查询所有直播列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("getliveinfolist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetLiveInfoListAsync")]
        public async Task<PageViewModel> GetLiveInfoListAsync(LiveInfoSearchDto dto)
        {
            var result = await DefaultService.GetLiveInfoListAsync(dto);
            return result;
        }
    }
}
