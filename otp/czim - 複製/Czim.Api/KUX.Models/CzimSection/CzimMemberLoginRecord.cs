using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 会员登录信息
    /// </summary>
    [Table("czim_member_login_record")]
    public class CzimMemberLoginRecord : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登录ip
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 登录机型
        /// </summary>
        public string LoginModel { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public string LoginOS { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_member_login_record` (
                                                    `id`,
                                                    `MemberId`,
                                                    `LoginName`,
                                                    `LoginTime`,
                                                    `LoginIP`,
                                                    `LoginModel`,
                                                    `LoginOS`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @MemberId,
                                                    @LoginName,
                                                    @LoginTime,
                                                    @LoginIP,
                                                    @LoginModel,
                                                    @LoginOS,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_member_login_record 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_member_login_record 
                                        where id = @id;";
    }
}