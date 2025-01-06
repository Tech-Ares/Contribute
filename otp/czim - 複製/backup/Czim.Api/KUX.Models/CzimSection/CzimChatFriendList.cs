using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 好友列表
    /// </summary>
    [Table("czim_chat_friend_list")]
    public class CzimChatFriendList : StringKeyEntity
    {
        /// <summary>
        /// 会员id(app会员id)
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 好友id（管理员userId）
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 成为好友时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 左删右
        /// </summary>
        public bool JoinLtr { get; set; }
        /// <summary>
        /// 右删左
        /// </summary>
        public bool JoinRtl { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chat_friend_list` (
                                                    `Id`,
                                                    `MemberId`,
                                                    `UserId`,
                                                    `CrtDate`,
                                                    `JoinLtr`,
                                                    `JoinRtl`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @MemberId,
                                                    @UserId,
                                                    @CrtDate,
                                                    @JoinLtr,
                                                    @JoinRtl,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chat_friend_list 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chat_friend_list 
                                        where id = @id;";
    }
}