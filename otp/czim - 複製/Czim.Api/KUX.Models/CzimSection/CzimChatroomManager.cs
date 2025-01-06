using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 聊天室管理员列表
    /// </summary>
    [Table("czim_chatroom_manager")]
    public class CzimChatroomManager : StringKeyEntity
    {
        /// <summary>
        /// 聊天室id
        /// </summary>
        public string ChatRoomId { get; set; }
        /// <summary>
        /// 管理员(sysuser表id)
        /// </summary>
        public string ManagerId { get; set; }
        /// <summary>
        /// 是否超管
        /// </summary>
        public bool IsSuper { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chatroom_manager` (
                                                    `id`,
                                                    `ChatRoomId`,
                                                    `ManagerId`,
                                                    `IsSuper`,
                                                    `CrtDate`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @ChatRoomId,
                                                    @ManagerId,
                                                    @IsSuper,
                                                    @CrtDate,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chatroom_manager 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chatroom_manager 
                                        where id = @id;";
    }
}