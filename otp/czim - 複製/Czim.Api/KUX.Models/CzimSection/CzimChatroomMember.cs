using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 聊天室成员表
    /// </summary>
    [Table("czim_chatroom_member")]
    public class CzimChatroomMember : StringKeyEntity
    {
        /// <summary>
        /// 群id
        /// </summary>
        public string ChatRoomId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
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
        public string InserSql() => @" INSERT INTO `czim_chatroom_member` (
                                                    `id`,
                                                    `ChatRoomId`,
                                                    `MemberId`,
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
                                                    @ChatRoomId,
                                                    @MemberId,
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
        public string ActiveSql() => @" update czim_chatroom_member 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chatroom_member 
                                        where id = @id;";
    }
}