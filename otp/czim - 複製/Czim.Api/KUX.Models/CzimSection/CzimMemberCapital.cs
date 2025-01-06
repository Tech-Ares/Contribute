using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 用户明细表
    /// </summary>
    [Table("czim_member_capital")]
    public class CzimMemberCapital : DefaultBaseEntity
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        [Key]
        public string MId { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }
        /// <summary>
        /// 资金
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// 锁定资金
        /// </summary>
        public decimal CapitalLocking { get; set; }
        /// <summary>
        /// 会员到期时间
        /// </summary>
        public DateTime FrozenTime { get; set; }
        /// <summary>
        /// 每日免费次数
        /// </summary>
        public int FreeTimesPerDay { get; set; }
        /// <summary>
        /// 全局禁言时间
        /// </summary>
        public DateTime ForbiddenTime { get; set; }
        /// <summary>
        /// 登录城市
        /// </summary>
        public string LoginCity { get; set; }
        /// <summary>
        /// 登录纬度
        /// </summary>
        public decimal LoginLat { get; set; }
        /// <summary>
        /// 登录经度
        /// </summary>
        public decimal LoginLong { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_member_capital` (
                                                    `MId`,
                                                    `LastLoginDate`,
                                                    `Capital`,
                                                    `CapitalLocking`,
                                                    `FrozenTime`,
                                                    `FreeTimesPerDay`,
                                                    `ForbiddenTime`,
                                                    `LoginCity`,
                                                    `LoginLat`,
                                                    `LoginLong`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @MId,
                                                    @LastLoginDate,
                                                    @Capital,
                                                    @CapitalLocking,
                                                    @FrozenTime,
                                                    @FreeTimesPerDay,
                                                    @ForbiddenTime,
                                                    @LoginCity,
                                                    @LoginLat,
                                                    @LoginLong,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";
    }
}