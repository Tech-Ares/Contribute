using KUX.Models.CzimAdmin.Reponse.ChatRoom;
using KUX.Models.CzimSection;
using KUX.Models.Entities.CzimAdmin.ChatRoom;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Repositories.CzimSection
{
    /// <summary>
    /// 聊天室仓储
    /// </summary>
    public class CzimChatRoomRepository : ServiceRepository<CzimChatroomInfo>
    {
        public CzimChatRoomRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }

        // 获取可加入的聊天室信息  ChatRoomAbbreEntity
        /// <summary>
        /// 获取可加入的聊天室信息
        /// </summary>
        /// <param name="memberId">当前会员id</param>
        /// <param name="crname">聊天室名称</param>
        /// <param name="page">当前页面</param>
        /// <param name="size">页面大小</param>
        /// <returns></returns>
        public async Task<(int, List<ChatRoomAbbreEntity>)> FindChatRoomAbbreAsync(string memberId, string crname, int page, int size)
        {
            var where = "";

            if (!string.IsNullOrWhiteSpace(crname))
            {
                where = @" and bci.ChatRoomName like @crname";
            }

            var sql = $@" select count(bci.Id)
	                     from czim_chatroom_info bci
	                     left join czim_chat_customer bcc on bci.ManagerId = bcc.UserId
	                     where bci.IsActive = 1
	                     	and not EXISTS (select 1
	            								from czim_chatroom_member bcm
	            								where bcm.MemberId = @memberId
	            								and bcm.ChatRoomId = bci.Id)
                            {where};
                        select
	            			bcc.NickName as ManagerName
	            			,bci.*
	                     from czim_chatroom_info bci
	                     left join czim_chat_customer bcc on bci.ManagerId = bcc.UserId
	                     where bci.IsActive = 1
	                     	and not EXISTS (select 1
	            								from czim_chatroom_member bcm
	            								where bcm.MemberId = @memberId
	            								and bcm.ChatRoomId = bci.Id)
                            {where}
                            order by bci.CrtDate desc
                            limit {(page - 1) * size},{size}
                            ";

            var result = await this.DapperContext.QueryMultipleAsync<int, ChatRoomAbbreEntity>(sql, new { memberId, crname = $"%{crname}%" });

            return (result.Item1.FirstOrDefault(), result.Item2.ToList());
        }

        /// <summary>
        /// 获取聊天室所有成员（包含管理员）
        /// </summary>
        /// <param name="chatroomId">聊天室id</param>
        /// <param name="page">当前页面大小</param>
        /// <param name="size">当前页面</param>
        /// <returns></returns>
        public async Task<(int, List<ChatRoomAllMemberVo>)> GetAllMemberAsync(string chatroomId, int page, int size)
        {
            var sqlc = @"select count(*)
                        	from (
                        		select cmi.NickName
                        		    ,cmi.Avatar
                        		    ,ccm.JoinDate
                        		    ,cmi.LoginName
                        		    ,0 IsManager
									,ccm.ForbiddenTime
                        		from czim_chatroom_member ccm
                        		left join czim_member_info cmi on ccm.MemberId = cmi.Id
                        		where ccm.ChatRoomId = @chatroomId
                        		and ccm.IsActive = 1
                        		union all
                        		select ccc.NickName
                        		    ,ccc.Avatar
                        		    ,ccm.CrtDate JoinDate
                        		    ,'' LoginName
                        		    ,1 IsManager
                                    ,DATE_FORMAT(DATE_SUB(NOW(),INTERVAL 1 DAY),'%Y-%m-%d') ForbiddenTime
                        		from czim_chatroom_manager ccm
                        		left join czim_chat_customer ccc on ccm.ManagerId = ccc.UserId
                        		where ccm.ChatRoomId = @chatroomId
                        		and ccm.IsActive = 1 ) aa ;";

            var sql = $@" select *
                        	from (
                        		select cmi.NickName
                        		    ,cmi.Avatar
                        		    ,ccm.JoinDate
                        		    ,cmi.LoginName
                        		    ,0 IsManager
                                    ,ccm.ForbiddenTime
                        		from czim_chatroom_member ccm
                        		left join czim_member_info cmi on ccm.MemberId = cmi.Id
                        		where ccm.ChatRoomId = @chatroomId
                        		and ccm.IsActive = 1
                        		union all
                        		select ccc.NickName
                        		    ,ccc.Avatar
                        		    ,ccm.CrtDate JoinDate
                        		    ,'' LoginName
                        		    ,1 IsManager
                                    ,DATE_FORMAT(DATE_SUB(NOW(),INTERVAL 1 DAY),'%Y-%m-%d') ForbiddenTime
                        		from czim_chatroom_manager ccm
                        		left join czim_chat_customer ccc on ccm.ManagerId = ccc.UserId
                        		where ccm.ChatRoomId = @chatroomId
                        		and ccm.IsActive = 1 ) aa
                        order by aa.IsManager desc
                        limit {(page - 1) * size},{size}";

            var result = await this.DapperContext.QueryMultipleAsync<int, ChatRoomAllMemberVo>($"{sqlc}{sql}", new { chatroomId });
            return (result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
    }
}