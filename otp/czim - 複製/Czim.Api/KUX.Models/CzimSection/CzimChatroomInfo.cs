using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 聊天室主表
    /// </summary>
    [Table("czim_chatroom_info")]
    public class CzimChatroomInfo : StringKeyEntity
    {
        /// <summary>
        /// 群名称
        /// </summary>
        public string ChatRoomName { get; set; }
        /// <summary>
        /// 群图标
        /// </summary>
        public string ChatRoomImg { get; set; }
        /// <summary>
        /// 群公告
        /// </summary>
        public string ChatRoomNotice { get; set; }
        /// <summary>
        /// 群等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 主管理员
        /// </summary>
        public string ManagerId { get; set; }
        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserNum { get; set; }
        /// <summary>
        /// 全员禁言
        /// </summary>
        public bool IsProhibit { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 环信id
        /// </summary>
        public string EaseChatRoomId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chatroom_info` (
                                                    `id`,
                                                    `ChatRoomName`,
                                                    `ChatRoomImg`,
                                                    `ChatRoomNotice`,
                                                    `Level`,
                                                    `ManagerId`,
                                                    `UserNum`,
                                                    `IsProhibit`,
                                                    `IsDefault`,
                                                    `CrtDate`,
                                                    `EaseChatRoomId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @ChatRoomName,
                                                    @ChatRoomImg,
                                                    @ChatRoomNotice,
                                                    @Level,
                                                    @ManagerId,
                                                    @UserNum,
                                                    @IsProhibit,
                                                    @IsDefault,
                                                    @CrtDate,
                                                    @EaseChatRoomId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chatroom_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chatroom_info 
                                        where id = @id;";
    }
}