using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 会员称号表
    /// </summary>
    [Table("czim_talent_member")]
    public class CzimTalentMember : StringKeyEntity
    {
        /// <summary>
        /// 称号id
        /// </summary>
        public string TalentId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 称号段位
        /// </summary>
        public int TalentRank { get; set; }
        /// <summary>
        /// 正在使用
        /// </summary>
        public bool IsUse { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_talent_member` (
                                                    `id`,
                                                    `TalentId`,
                                                    `MemberId`,
                                                    `TalentRank`,
                                                    `IsUse`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @id,
                                                    @TalentId,
                                                    @MemberId,
                                                    @TalentRank,
                                                    @IsUse,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_talent_member 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_talent_member 
                                        where id = @id;";
    }
}