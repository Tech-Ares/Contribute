using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 聊天客服表
    /// </summary>
    [Table("czim_chat_customer")]
    public class CzimChatCustomer : StringKeyEntity
    {
        /// <summary>
        /// 后台帐号id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 客服头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 客服昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 服务人次
        /// </summary>
        public int ManTime { get; set; }
        /// <summary>
        /// 是否推广专员
        /// </summary>
        public bool IsAgent { get; set; }
        /// <summary>
        /// 环信Id
        /// </summary>
        public string EaseChatCustomerId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chat_customer` (
                                                    `id`,
                                                    `UserId`,
                                                    `Avatar`,
                                                    `NickName`,
                                                    `ManTime`,
                                                    `IsAgent`,
                                                    `EaseChatCustomerId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @UserId,
                                                    @Avatar,
                                                    @NickName,
                                                    @ManTime,
                                                    @IsAgent,
                                                    @EaseChatCustomerId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chat_customer 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chat_customer 
                                        where id = @id;";
    }
}