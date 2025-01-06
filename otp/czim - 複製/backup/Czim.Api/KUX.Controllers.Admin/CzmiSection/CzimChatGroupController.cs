using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.CzimAdmin.Request.CzimChatGroup;
using KUX.Models.CzimSection;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection
{
    /// <summary>
    /// 群组服务
    /// </summary>
    public class CzimChatGroupController : AdminBaseController<CzimChatGroupService>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultService">默认服务</param>
        /// <param name="_accountService">用户服务</param>
        public CzimChatGroupController(CzimChatGroupService defaultService, IAccountService _accountService) : base(defaultService, _accountService)
        {
        }

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("createchatgroup")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("CreateChatGroupAsync")]
        public async Task<object> CreateChatGroupAsync(CzimChatGroupCreateDto dto)
        {
            var result = await DefaultService.CreateChatGroupAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        /// <summary>
        /// 获取所有群组信息
        /// </summary>
        /// <param name="crsd">聊天室查询条件</param>
        /// <returns></returns>
        [HttpPost("findchatgrouplist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindChatGroupListAsync")]
        public async Task<PageViewModel> FindChatGroupListAsync(ChatGroupSearchDto crsd)
        {
            var result = await DefaultService.FindChatGroupListAsync(crsd);
            return result;
        }

        /// <summary>
        /// 查询指定的群组信息
        /// </summary>
        /// <param name="id">群组id</param>
        /// <returns></returns>
        [HttpPost("findchatgroup/{id?}")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindChatGroupAsync")]
        public async Task<CzimChatgroupInfo> FindChatGroupAsync(string id)
        {
            var result = await DefaultService.FindChatGroupAsync(id);
            return result;
        }

        /// <summary>
        /// 获取当前会员可加入的群组列表
        /// </summary>
        /// <param name="dto">群组查询条件</param>
        /// <returns></returns>
        [HttpPost("findchatgroupabbre")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindChatGroupAbbreAsync")]
        public async Task<PageViewModel> FindChatGroupAbbreAsync(ChatGroupMemberSearchDto dto)
        {
            var result = await DefaultService.FindChatGroupAbbreAsync(dto);
            return result;
        }

        /// <summary>
        /// 查询所有可加入群组的会员
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        [HttpPost("chatgroupaddmember")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("ChatGroupAddMemberAsync")]
        public async Task<PageViewModel> ChatGroupAddMemberAsync(ChatGroupAddMemberSearchDto dto)
        {
            var result = await DefaultService.ChatGroupAddMemberAsync(dto);
            return result;
        }

        /// <summary>
        /// 当前群组所有会员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("getmemberofchatgrouplist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetMemberOfChatGroupListAsync")]
        public async Task<PageViewModel> GetMemberOfChatGroupListAsync(ChatGroupSearchDto dto)
        {
            var result = await DefaultService.GetMemberOfChatGroupListAsync(dto);
            return result;
        }

        /// <summary>
        /// 获取当前群组所有管理员信息
        /// </summary>
        /// <param name="crsd">群组查询条件</param>
        /// <returns></returns>
        [HttpPost("findmanagerlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindManagerListAsync")]
        public async Task<PageViewModel> FindManagerListAsync(ChatGroupSearchDto crsd)
        {
            var result = await DefaultService.FindManagerListAsync(crsd);
            return result;
        }

        /// <summary>
        /// 添加群组管理员
        /// </summary>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        [HttpPost("groupaddmanager")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupAddManagerAsync")]
        public async Task<object> GroupAddManagerAsync([FromBody] GroupAddManagerDto dto)
        {
            var result = await DefaultService.GroupAddManagerAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        //移除群组管理员
        /// <summary>
        /// 移除群组管理员
        /// </summary>
        /// <param name="dto">要移除的对象</param>
        /// <returns></returns>
        [HttpPost("groupdeletemanager")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupDeleteManagerAsync")]
        public async Task<object> GroupDeleteManagerAsync(GroupAddManagerDto dto)
        {
            var result = await DefaultService.GroupDeleteManagerAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        /// <summary>
        /// 批量移除群组成员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("groupdeletememberlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupDeleteMemberListAsync")]
        public async Task<object> GroupDeleteMemberListAsync(GroupAddMemberListDto dto)
        {
            var result = await DefaultService.GroupDeleteMemberListAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        /// <summary>
        /// 获取当前聊天室用户的禁言状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("getforbidstate")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetForbidStateAsync")]
        public async Task<object> GetForbidStateAsync(ChatGroupOfMemberForbiddenStateDto dto)
        {
            var result = await DefaultService.GetForbidStateAsync(dto);

            // 0:正常 1：禁言 2：不存在
            if (result.Item2 is string && result.Item2.ToString() == "-1")
            {
                return new
                {
                    result = 2,
                    time = result.Item2
                };
            }

            return new
            {
                result = result.Item1 ? 1 : 0,
                time = result.Item2
            };
            //if (result.Item1)
            //{
            //    return result.Item2;
            //}
            //return default;
        }

        /// <summary>
        /// 单个移除群组成员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("groupdeletemember")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupDeleteMemberAsync")]
        public async Task<object> GroupDeleteMemberAsync(GroupAddManagerDto dto)
        {
            var result = await DefaultService.GroupDeleteMemberAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        //添加单个群组成员
        /// <summary>
        /// 添加单个群组成员
        /// </summary>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        [HttpPost("groupaddmember")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupAddMemberAsync")]
        public async Task<object> GroupAddMemberAsync(GroupAddManagerDto dto)
        {
            var result = await DefaultService.GroupAddMemberAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        //批量添加群组成员
        /// <summary>
        /// 批量添加群组成员
        /// </summary>
        /// <param name="dto">对象</param>
        /// <returns></returns>
        [HttpPost("groupaddmemberlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GroupAddMemberListAsync")]
        public async Task<object> GroupAddMemberListAsync(GroupAddMemberListDto dto)
        {
            var result = await DefaultService.GroupAddMemberListAsync(this.UserId, dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }

        /// <summary>
        /// 修改默认群组
        /// </summary>
        /// <param name="crad"></param>
        /// <returns></returns>
        [HttpPost("dodefault")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("DoDefaultAsync")]
        public async Task<(bool, string)> DoDefaultAsync(ChatGroupActiveDto crad)
        {
            var result = await DefaultService.DoDefaultAsync(crad);
            return result;
        }
    }
}