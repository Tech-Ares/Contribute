using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 会员阅读公告时间
    /// </summary>
    [Table("czim_notice_read")]
    public class CzimNoticeRead : StringKeyEntity
    {
        /// <summary>
        /// 公告id
        /// </summary>
        public string NId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string MId { get; set; }
        /// <summary>
        /// 阅读公告最后时间
        /// </summary>
        public DateTime ReadDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_notice_read` (
                                                    `Id`,
                                                    `NId`,
                                                    `MId`,
                                                    `ReadDate`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @NId,
                                                    @MId,
                                                    @ReadDate,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_notice_read 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_notice_read 
                                        where id = @id;";
    }
}