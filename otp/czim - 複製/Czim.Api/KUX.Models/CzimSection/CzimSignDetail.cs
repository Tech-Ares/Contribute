using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 会员签到表
    /// </summary>
    [Table("czim_sign_detail")]
    public class CzimSignDetail : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 签到日期
        /// </summary>
        public DateTime SignDate { get; set; }
        /// <summary>
        /// 签到年份
        /// </summary>
        public int SignYear { get; set; }
        /// <summary>
        /// 签到月份
        /// </summary>
        public int SignMonth { get; set; }
        /// <summary>
        /// 签到月份
        /// </summary>
        public int SignDay { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_sign_detail` (
                                                    `Id`,
                                                    `MemberId`,
                                                    `SignDate`,
                                                    `SignYear`,
                                                    `SignMonth`,
                                                    `SignDay`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @MemberId,
                                                    @SignDate,
                                                    @SignYear,
                                                    @SignMonth,
                                                    @SignDay,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_sign_detail 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_sign_detail 
                                        where id = @id;";
    }
}