using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 群成员
    /// </summary>
    [Table("czim_chatgroup_member")]
    public class CzimChatgroupMember : StringKeyEntity
    {
        /// <summary>
        /// 群id
        /// </summary>
        public string ChatgroupId { get; set; }
        /// <summary>
        /// 环信聊天室id
        /// </summary>
        public string EaseChatgroupId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsManager { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JoinDate { get; set; }
        /// <summary>
        /// 禁言时间
        /// </summary>
        public DateTime ForbiddenTime { get; set; }
        /// <summary>
        /// 引入人
        /// </summary>
        public string PullUser { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chatgroup_member` (
                                                    `id`,
                                                    `ChatgroupId`,
                                                    `EaseChatgroupId`,
                                                    `MemberId`,
                                                    `IsManager`,
                                                    `JoinDate`,
                                                    `ForbiddenTime`,
                                                    `PullUser`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @ChatgroupId,
                                                    @EaseChatgroupId,
                                                    @MemberId,
                                                    @IsManager,
                                                    @JoinDate,
                                                    @ForbiddenTime,
                                                    @PullUser,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chatgroup_member 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chatgroup_member 
                                        where id = @id;";
    }
}