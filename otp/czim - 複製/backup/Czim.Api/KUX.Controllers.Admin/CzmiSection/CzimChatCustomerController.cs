using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimAdmin.Request.ChatCustomer;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection
{
    /// <summary>
    /// 客服管理
    /// </summary>
    public class CzimChatCustomerController : AdminBaseController<CzimChatCustomerService>
    {
        public CzimChatCustomerController(CzimChatCustomerService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
        {

        }

        /// <summary>
        /// 获取所有客服列表
        /// </summary>
        /// <param name="ccsd">客服信息</param>
        /// <returns></returns>
        [HttpPost("findcustomerlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindCustomerListAsync")]
        public async Task<PageViewModel> FindCustomerListAsync([FromBody] ChatCustomerSearchDto ccsd)
        {
            // 获取所有客服列表
            var data = await this.DefaultService.FindCustomerListAsync(ccsd);
            return data;
        }

        /// <summary>
        /// 获取用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("finduser")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindUserAsync")]
        public async Task<object> FindUserAsync()
        {
            var result =await DefaultService.FindUserAsync(this.UserId);
            return result;
        }

        //修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("updateuserinfo")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("UpdateUserInfoAsync")]
        public async Task<object> UpdateUserInfoAsync(UpdateUserInfoDto dto)
        {
            var result = await DefaultService.UpdateUserInfoAsync(this.UserId,dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }
        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("updatepassword")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("UpdatePasswordAsync")]
        public async Task<object> UpdatePasswordAsync(UpdatePasswordDto dto)
        {
            var result = await DefaultService.UpdatePasswordAsync(this.UserId,dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }
        /// <summary>
        /// 客服好友列表
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        [HttpPost("getchatcustomeroffriend")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetChatCustomerOfFriendAsync")]
        public async Task<PageViewModel> GetChatCustomerOfFriendAsync(ChatCustomerOfFriendDto dto)
        {
            var result =await this.DefaultService.GetChatCustomerOfFriendAsync(dto);
            return result;
        }
        /// <summary>
        /// 获取所有可以成为客服的帐号
        /// </summary>
        /// <returns></returns>
        [HttpGet("findcustomeruser")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindCustomerUserAsync")]
        public async Task<object> FindCustomerUserAsync()
        {
            var data = await this.DefaultService.FindCustomerUserAsync();
            return data;
        }
        /// <summary>
        /// 新增推广专员
        /// </summary>
        /// <returns></returns>
        //[HttpPost("addagent")]
        //[ApiResourceCacheFilter(2)]
        //[ActionDescriptor("AddAgentAsync")]
        //public async Task<object> AddAgentAsync(ChatCustomerSaveDto dto)
        //{
        //    var res = await this.DefaultService.AddAgent(this.UserId, dto);
        //    return res;
        //}
        // 保存客服信息
        /// <summary>
        /// 保存客服信息
        /// </summary>
        /// <param name="ccsd">客服信息</param>
        /// <returns></returns>
        [HttpPost("savecustomer")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("SaveCustomerAsync")]
        public async Task<object> SaveCustomerAsync([FromBody] ChatCustomerSaveDto ccsd)
        {
            // 新增保存客服帐号
            var data = await this.DefaultService.SaveCustomerAsync(this.UserId, ccsd);
            return data;
        }

        /// <summary>
        /// 激活/失效
        /// </summary>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        [HttpPost("customeractive")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("CustomerActiveAsync")]
        public async Task<object> CustomerActiveAsync([FromBody] ChatRoomActiveDto crad)
        {

            var result = await this.DefaultService.CustomerActiveAsync(this.UserId, crad);
            return result;
        }
    }
}
