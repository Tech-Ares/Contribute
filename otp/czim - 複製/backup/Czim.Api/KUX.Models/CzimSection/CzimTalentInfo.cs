using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 称号说明
    /// </summary>
    [Table("czim_talent_info")]
    public class CzimTalentInfo : StringKeyEntity
    {
        /// <summary>
        /// 称号说明
        /// </summary>
        public string TalentName { get; set; }
        /// <summary>
        /// 激活图标
        /// </summary>
        public string ActiveImg { get; set; }
        /// <summary>
        /// 非激活图标
        /// </summary>
        public string DeActiveImg { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_talent_info` (
                                                    `id`,
                                                    `TalentName`,
                                                    `ActiveImg`,
                                                    `DeActiveImg`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @id,
                                                    @TalentName,
                                                    @ActiveImg,
                                                    @DeActiveImg,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_talent_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_talent_info 
                                        where id = @id;";
    }
}