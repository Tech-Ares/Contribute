using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Controllers.Filters;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Models.CzimAdmin.Request.ChatRoom;
using KUX.Models.CzimAdmin.Request.CzimChat;
using KUX.Models.CzimSection;
using KUX.Models.KuxAdmin.Request.ChatRoom;
using KUX.Repositories.Core.BaseModels;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.CzmiSection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.CzmiSection
{
    /// <summary>
    /// 客服聊天室管理
    /// </summary>
    public class CzimChatRoomController : AdminBaseController<CzimChatRoomService>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="defaultService">默认服务</param>
        /// <param name="_accountService">账户服务</param>
        public CzimChatRoomController(CzimChatRoomService defaultService, IAccountService _accountService)
            : base(defaultService, _accountService)
        {
        }

        // 新增群聊信息
        /// <summary>
        /// 保存聊天室信息
        /// </summary>
        /// <param name="form">聊天室信息</param>
        /// <returns></returns>
        [HttpPost("saveform")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("SaveFormAsync")]
        public async Task<object> SaveFormAsync([FromBody] ChatRoomDto form)
        {
            var result = await this.DefaultService.SaveFormAsync(this.UserId, form);

            if (result.Item1)
            {
                return "执行成功！";
            }
            MessageBox.Show("执行失败！");
            return result;
        }

        // 获取所有群信息
        /// <summary>
        /// 获取所有群(聊天室)信息
        /// </summary>
        /// <param name="crsd">聊天室查询信息</param>
        /// <returns></returns>
        [HttpPost("findchatroomlist")]
        [ApiResourceCacheFilter(2)]//防止重复发请求
        [ActionDescriptor("FindChatRoomListAsync")]//写日志
        public async Task<PageViewModel> FindChatRoomListAsync([FromBody] ChatRoomSearchDto crsd)
        {
            // 获取聊天室信息
            var data = await this.DefaultService.FindChatRoomListAsync(crsd);
            return data;
        }
        //获取指定用户的禁言状态
        /// <summary>
        /// 获取指定用户的禁言状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("getforbidstate")]
        [ApiResourceCacheFilter(1)]
        [ActionDescriptor("GetForbidStateAsync")]
        public async Task<object> GetForbidState(ChatRoomOfMemberForbiddenStateDto dto)
        {
            var result = await this.DefaultService.GetForbidState(dto);
            if (result.Item1)
            {
                return result.Item2;
            }
            return default;
        }
        /// <summary>
        /// 修改默认聊天室
        /// </summary>
        /// <param name="dto">聊天室信息</param>
        /// <returns></returns>
        [HttpPost("dodefault")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("DoDefaultAsync")]
        public async Task<object> DoDefaultAsync([FromBody] ChatRoomActiveDto dto)
        {
            // 更新默认聊天室
            var data = await this.DefaultService.DoDefaultAsync(dto);
            return data;
        }

        /// <summary>
        /// 获取所有管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallmanager")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetAllManagerAsync")]
        public async Task<object> GetAllManagerAsync()
        {
            var result = await this.DefaultService.GetAllCustomerAsync();
            return result;
        }

        // 查看群管理员
        /// <summary>
        /// 获取聊天管理员列表
        /// </summary>
        /// <param name="crsd">聊天室查询信息</param>
        /// <returns></returns>
        [HttpPost("findmanagerlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindManagerListAsync")]
        public async Task<PageViewModel> FindManagerListAsync([FromBody] ChatRoomSearchDto crsd)
        {
            // 获取聊天室信息
            var data = await this.DefaultService.FindManagerListAsync(crsd);
            return data;
        }

        // 聊天室添加管理员
        /// <summary>
        /// 聊天室添加/移除管理员
        /// </summary>
        /// <param name="crmd">聊天室查询信息</param>
        /// <returns></returns>
        [HttpPost("addordelmanager")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("AddOrDelManagerAsync")]
        public async Task<object> AddOrDelManagerAsync([FromBody] ChatRoomManagerDto crmd)
        {
            // 聊天室新增或删除管理员
            var data = await this.DefaultService.AddOrDelManagerAsync(this.UserId, crmd);
            return data;
        }

        /// <summary>
        /// 获取当前会员可加入的聊天室列表
        /// 检索用
        /// </summary>
        /// <param name="crsd">聊天室查询信息</param>
        /// <returns></returns>
        [HttpPost("findchatroomabbre")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindChatRoomAbbreAsync")]
        public async Task<PageViewModel> FindChatRoomAbbreAsync([FromBody] ChatRoomMemberSearchDto crsd)
        {
            // 获取服务地址
            var data = await this.DefaultService.FindChatRoomAbbreAsync(crsd);
            return data;
        }

        /// <summary>
        /// 查询当前群信息
        /// </summary>
        /// <param name="id">群id</param>
        /// <returns></returns>
        [HttpPost("findform/{id?}")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("FindFormAsync")]
        public async Task<CzimChatroomInfo> FindFormAsync([FromRoute] string id)
        {
            var result = await this.DefaultService.FindChatRoomAsync(id);
            return result;
        }

        /// <summary>
        /// 激活/删除
        /// </summary>
        /// <param name="crad">聊天室信息</param>
        /// <returns></returns>
        [HttpPost("isactiveform")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("IsActiveFormAsync")]
        public async Task<object> IsActiveFormAsync([FromBody] ChatRoomActiveDto crad)
        {
            var result = await this.DefaultService.IsActiveFormAsync(this.UserId, crad);

            return result;
        }

        //查询所有可加入聊天室的会员
        /// <summary>
        /// 查询所有可加入聊天室的会员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("chatroomaddmember")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("ChatRoomAddMemberAsync")]
        public async Task<PageViewModel> ChatRoomAddMemberAsync(ChatRoomAddMemberSearchDto dto)
        {
            var result = await this.DefaultService.ChatRoomAddMemberAsync(dto);
            return result;
        }

        /// <summary>
        /// 获取当前聊天室的所有会员列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("getmemberofchatroomlist")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetMemberOfChatRoomListAsync")]
        public async Task<PageViewModel> GetMemberOfChatRoomListAsync([FromBody] ChatRoomSearchDto dto)
        {
            return await this.DefaultService.GetMemberOfChatRoomList(dto);
        }

        /// <summary>
        /// 获取当前聊天室的所有会员列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("getallmember")]
        [ApiResourceCacheFilter(2)]
        [ActionDescriptor("GetAllMemberAsync")]
        public async Task<object> GetAllMemberAsync([FromBody] ChatRoomSearchDto dto)
        {
            return await this.DefaultService.GetAllMemberAsync(dto);
        }
    }
}