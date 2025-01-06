using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 用户实名信息
    /// </summary>
    [Table("czim_member_authreal")]
    public class CzimMemberAuthreal : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 身份证正面
        /// </summary>
        public string IdCardFront { get; set; }
        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IdCardBack { get; set; }
        /// <summary>
        /// 手持身份证照片
        /// </summary>
        public string IdCardHold { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 审核状态0：申请中，1：通过，2：拒绝
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditDate { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditUser { get; set; }
        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string RefuseReasons { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_member_authreal` (
                                                    `Id`,
                                                    `MId`,
                                                    `RealName`,
                                                    `IdCard`,
                                                    `IdCardFront`,
                                                    `IdCardBack`,
                                                    `IdCardHold`,
                                                    `CrtDate`,
                                                    `AuditStatus`,
                                                    `AuditDate`,
                                                    `AuditUser`,
                                                    `RefuseReasons`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @Id,
                                                    @MId,
                                                    @RealName,
                                                    @IdCard,
                                                    @IdCardFront,
                                                    @IdCardBack,
                                                    @IdCardHold,
                                                    @CrtDate,
                                                    @AuditStatus,
                                                    @AuditDate,
                                                    @AuditUser,
                                                    @RefuseReasons,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_member_authreal 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_member_authreal 
                                        where id = @id;";
    }
}