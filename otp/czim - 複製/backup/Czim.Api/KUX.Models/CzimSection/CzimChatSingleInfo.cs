using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 聊天信息，单聊表
    /// </summary>
    [Table("czim_chat_single_info")]
    public class CzimChatSingleInfo : StringKeyEntity
    {
        /// <summary>
        /// 发送人id
        /// </summary>
        public string SendId { get; set; }
        /// <summary>
        /// 接收人id
        /// </summary>
        public string ReceiveId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string ChatMessage { get; set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime SendDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chat_single_info` (
                                                    `id`,
                                                    `SendId`,
                                                    `ReceiveId`,
                                                    `ChatMessage`,
                                                    `SendDate`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @SendId,
                                                    @ReceiveId,
                                                    @ChatMessage,
                                                    @SendDate,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chat_single_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chat_single_info 
                                        where id = @id;";
    }
}