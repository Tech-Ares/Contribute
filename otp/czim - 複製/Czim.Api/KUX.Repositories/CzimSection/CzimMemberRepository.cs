using KUX.Models.CzimSection;
using KUX.Repositories.Core;
using KUX.Repositories.Core.DapperUtil;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Repositories.CzimSection;

/// <summary>
/// 会员仓储
/// </summary>
public class CzimMemberRepository : ServiceRepository<CzimMemberInfo>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="freeSql">freeSql</param>
    /// <param name="dapperContext">dapper上下文</param>
    public CzimMemberRepository(IFreeSql freeSql, IDapperContext dapperContext) : base(freeSql, dapperContext)
    {
    }

    /// <summary>
    /// 从种子表中获取会员编码
    /// </summary>
    /// <param name="bzs">会员种子表信息</param>
    /// <returns></returns>
    public async Task<int> GetNumberSeedAsync(CzimZnumberSeed bzs)
    {
        var sql = bzs.InserSql();

        sql += " select @@IDENTITY;";

        var result = await this.DapperContext.QueryFirstOrDefaultAsync<int>(sql, bzs);

        return result;
    }

    /// <summary>
    /// 获取会员信息
    /// </summary>
    /// <param name="mid">会员id</param>
    /// <returns></returns>
    public async Task<(CzimMemberInfo, CzimMemberCapital, List<CzimChatroomMember>)> GetMemberById(string mid)
    {
        var msql = @"select * from czim_member_info where id = @mid;
                        select * from czim_member_capital where mid = @mid;
                        select ChatRoomId,MemberId,JoinDate,ForbiddenTime
                            from czim_chatroom_member where MemberId = @mid
                        UNION all
                        select ChatRoomId,ManagerId as MemberId,CrtDate as JoinDate,'2020-01-05 17:13:12' as ForbiddenTime 
                            from czim_chatroom_manager where ManagerId = @mid;";

        var result = await this.DapperContext.QueryMultipleAsync<CzimMemberInfo, CzimMemberCapital, CzimChatroomMember>(msql, new { mid });

        if (result != null)
        {
            return (result.Item1.FirstOrDefault(), result.Item2.FirstOrDefault(), result.Item3.Distinct().ToList());
        }

        return default;
    }



}