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
    /// 聊天仓储
    /// </summary>
    public class CzimChatRepository : ServiceRepository<CzimChatroomInfo>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="freeSql">freeSql</param>
        /// <param name="_dapperContext">dapper</param>
        public CzimChatRepository(IFreeSql freeSql, IDapperContext _dapperContext) : base(freeSql, _dapperContext)
        {
        }

        /// <summary>
        /// 获取所有目标信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<string>> GetChatTargetAsync(string userId, int page, int size)
        {
            string sql = $@"select DISTINCT TargetId
                            from(
                                (select ReceiveId AS TargetId,
                            				1 as IsSend,
                            				SendDate
                            		from bsv_chat_single_info
                            		where sendid = @userId
                            		ORDER BY SendDate desc)
                            union all
                                (select SendId as TargetId,
                            			2 as IsSend,
                            			SendDate
                            		from bsv_chat_single_info
                            		where receiveId = @userId
                            		ORDER BY SendDate desc)
                            		) aa
                            limit {(page - 1) * size},{size}";

            var result = await this.DapperContext.QueryAsync<string>(sql, new { userId });

            return result.ToList();
        }

        /// <summary>
        /// 获取所有的联系人信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="page">当前页面</param>
        /// <param name="size">页面大小</param>
        /// <returns></returns>
        public async Task<(int, List<CzimMemberInfo>)> ContactsListAsync(string userId, int page, int size)
        {
            var sqlc = $@"
                    select count(Distinct aa.Id)
                        from (
                         select cmi.*
                            from czim_chat_friend_list ccfl
                            left join czim_member_info cmi on ccfl.MemberId = cmi.Id
                            where ccfl.UserId = @userId
                            and ccfl.IsActive = 1
                     	    and cmi.IsActive = 1
                         union all
                         select * from czim_member_info where ExclusiveUserId = @userId and IsActive = 1
                        ) aa ;";

            var sql = $@"
                      select Distinct *
                        from (
                         select cmi.*
                            from czim_chat_friend_list ccfl
                            left join czim_member_info cmi on ccfl.MemberId = cmi.Id
                            where ccfl.UserId = @userId
                            and ccfl.IsActive = 1
                     	    and cmi.IsActive = 1
                         union all
                         select * from czim_member_info where ExclusiveUserId = @userId and IsActive = 1
                        ) aa
                        limit {(page - 1) * size},{size}";

            var result = await this.DapperContext.QueryMultipleAsync<int, CzimMemberInfo>($"{sqlc}{sql}", new { userId });

            return (result.Item1.FirstOrDefault(), result.Item2.ToList());
        }

        /// <summary>
        /// 获取所有好友信息
        /// </summary>
        /// <param name="ownId">会员id</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<string>> FriendListAsync(string ownId, int page, int size)
        {
            string sql = $@"select UserId
                                from bsv_chat_friend_list
                                where MemberId = @mid
                                and IsActive = 1
                                limit {(page - 1) * size},{size}";

            var result = await this.DapperContext.QueryAsync<string>(sql, new { mid = ownId });

            return result.ToList();
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
	                     from bsv_chatroom_info bci
	                     left join bsv_chat_customer bcc on bci.ManagerId = bcc.UserId
	                     where bci.IsActive = 1
	                     	and not EXISTS (select 1
	            								from bsv_chatroom_member bcm
	            								where bcm.MemberId = @memberId
	            								and bcm.CRId = bci.Id)
                            {where};
                        select
	            			bcc.NickName as ManagerName
	            			,bci.*
	                     from bsv_chatroom_info bci
	                     left join bsv_chat_customer bcc on bci.ManagerId = bcc.UserId
	                     where bci.IsActive = 1
	                     	and not EXISTS (select 1
	            								from bsv_chatroom_member bcm
	            								where bcm.MemberId = @memberId
	            								and bcm.CRId = bci.Id)
                            {where}
                            order by bci.CrtDate desc
                            limit {(page - 1) * size},{size}
                            ";

            var result = await this.DapperContext.QueryMultipleAsync<int, ChatRoomAbbreEntity>(sql, new { memberId, crname = $"%{crname}%" });

            return (result.Item1.FirstOrDefault(), result.Item2.ToList());
        }

        /// <summary>
        /// 获取第一条聊天记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="targetIds">目标id</param>
        /// <returns></returns>
        public async Task<List<CzimChatSingleInfo>> GetFirstChatInfoAsync(string userId, List<string> targetIds)
        {
            string sql = @"
                            SELECT *
                            FROM (
                            		(SELECT *
                            				FROM (
                            						SELECT bcsi.*,
                            							ROW_NUMBER() over ( PARTITION BY bcsi.SendId, bcsi.ReceiveId ORDER BY bcsi.SendDate DESC ) AS group_index
                            						FROM bsv_chat_single_info bcsi
                            						WHERE bcsi.SendId = @userId
                            							AND bcsi.ReceiveId IN @targetIds
                            						) dd
                            				WHERE dd.group_index = 1
                            		)
                            		UNION ALL
                            		(SELECT *
                            				FROM (
                            						SELECT
                            							bcsi.*,
                            							ROW_NUMBER() over ( PARTITION BY bcsi.SendId, bcsi.ReceiveId ORDER BY bcsi.SendDate DESC ) AS group_index
                            						FROM bsv_chat_single_info bcsi
                            						WHERE bcsi.ReceiveId = @userId
                            							AND bcsi.SendId IN @targetIds
                            							) bb
                            					WHERE bb.group_index = 1
                            			)
                            	) ff ";
            //-- WHERE ff.group_index = 1";

            var result = await this.DapperContext.QueryAsync<CzimChatSingleInfo>(sql, new { userId, targetIds = targetIds.ToArray() });

            return result.ToList();
        }

        /// <summary>
        /// 获取所有的单聊记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="page">当前页面</param>
        /// <param name="size">页面大小</param>
        /// <param name="receiveId">聊天目标</param>
        /// <returns></returns>
        public async Task<List<CzimChatSingleInfo>> SingleRecordAsync(string userId, int page, int size, string receiveId)
        {
            var sql = $@"
                       select * from (
                        SELECT
                        	*
                        FROM
                        	(
                        		( SELECT * FROM bsv_chat_single_info WHERE SendId = @userId AND ReceiveId = @receiveId ORDER BY SendDate DESC LIMIT {(page - 1) * size},{size} )
                                UNION ALL
                        		( SELECT * FROM bsv_chat_single_info WHERE ReceiveId = @userId AND SendId = @receiveId ORDER BY SendDate DESC LIMIT {(page - 1) * size},{size} )
                        	) bb
                        ORDER BY
                        	bb.SendDate DESC
                        	LIMIT {(page - 1) * size},{size}
                        ) cc
                            order by cc.SendDate
                        ";

            var result = await this.DapperContext.QueryAsync<CzimChatSingleInfo>(sql, new { userId, receiveId });

            return result.ToList();
        }

        /// <summary>
        /// 获取所有的群聊记录
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="size">页面大小</param>
        /// <param name="receiveId">聊天目标</param>
        /// <returns></returns>
        public async Task<List<CzimChatGroupchatInfo>> ChatRoomRecordAsync(int page, int size, string receiveId)
        {
            var sql = $@"
                        select * from (
                        SELECT *
                            FROM bsv_chat_groupchat_info
                            WHERE ChatRoomId = @receiveId
                            ORDER BY SendDate DESC
                            LIMIT {(page - 1) * size},{size}
                        ) bb
                        order by bb.SendDate
                        ";

            var result = await this.DapperContext.QueryAsync<CzimChatGroupchatInfo>(sql, new { receiveId });

            return result.ToList();
        }

        /// <summary>
        /// 获取客服相关所有群组信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="page">当前页面</param>
        /// <param name="size">页面大小</param>
        /// <returns></returns>
        public async Task<(int, List<CzimChatgroupInfo>)> GetChatGroupListAsync(string userId, int page, int size)
        {
            var sqlC = @"
      	                    select COUNT(DISTINCT ChatgroupId)
                            	from (
                            	    select ccm.ChatgroupId
                            			from czim_chatgroup_member ccm
                            				where (ccm.MemberId = @userId and IsManager = 1)
                            				and ccm.IsActive = 1
                            	    UNION all
                            		select cci.id as ChatgroupId
                            			from czim_chatgroup_info cci
                            			where cci.OwnId = @userId
                            			and cci.IsActive = 1) aa ;";

            var sql = $@"select ccinfo.*
                           from
                            (
                            	select DISTINCT ChatgroupId
                            	from (
                            	    select ccm.ChatgroupId
                            			from czim_chatgroup_member ccm
                            				where (ccm.MemberId = @userId and IsManager = 1)
                            				and ccm.IsActive = 1
                            	    UNION all
                            		select cci.id as ChatgroupId
                            			from czim_chatgroup_info cci
                            			where cci.OwnId = @userId
                            			and cci.IsActive = 1) aa
                            			) bb
                            left join czim_chatgroup_info ccinfo on bb.ChatgroupId = ccinfo.Id
                            limit {(page - 1) * size},{size}";

            var result = await this.DapperContext.QueryMultipleAsync<int, CzimChatgroupInfo>($"{sqlC}{sql}", new { userId });

            return (result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
    }
}