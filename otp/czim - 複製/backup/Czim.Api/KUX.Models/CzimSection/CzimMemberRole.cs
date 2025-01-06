using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 会员权限
    /// </summary>
    [Table("czim_member_role")]
    public class CzimMemberRole : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_member_role` (
                                                    `Id`,
                                                    `MId`,
                                                    `RoleId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @Id,
                                                    @MId,
                                                    @RoleId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_member_role 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_member_role 
                                        where id = @id;";
    }
}