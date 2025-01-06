using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 
    /// </summary>
    [Table("czim_sign_info")]
    public class CzimSignInfo : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 最后一次签到日期
        /// </summary>
        public DateTime LastSignDate { get; set; }
        /// <summary>
        /// 连续次数
        /// </summary>
        public int ConTimes { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_sign_info` (
                                                    `Id`,
                                                    `MemberId`,
                                                    `LastSignDate`,
                                                    `ConTimes`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @MemberId,
                                                    @LastSignDate,
                                                    @ConTimes,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_sign_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_sign_info 
                                        where id = @id;";
    }
}