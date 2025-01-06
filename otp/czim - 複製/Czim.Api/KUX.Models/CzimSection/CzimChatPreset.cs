using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 预设聊天消息
    /// </summary>
    [Table("czim_chat_preset")]
    public class CzimChatPreset : StringKeyEntity
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 缩略
        /// </summary>
        public string Abbre { get; set; }
        /// <summary>
        /// 模糊查询
        /// </summary>
        public string Vague { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chat_preset` (
                                                    `id`,
                                                    `Context`,
                                                    `Abbre`,
                                                    `Vague`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @id,
                                                    @Context,
                                                    @Abbre,
                                                    @Vague,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chat_preset 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chat_preset 
                                        where id = @id;";
    }
}