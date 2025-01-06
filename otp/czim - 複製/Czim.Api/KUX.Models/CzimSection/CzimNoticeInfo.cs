using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 公告内容
    /// </summary>
    [Table("czim_notice_info")]
    public class CzimNoticeInfo : StringKeyEntity
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string HeadImg { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 公告时间（创建时间）
        /// </summary>
        public DateTime NDate { get; set; }
        /// <summary>
        /// 正在使用
        /// </summary>
        public bool InUse { get; set; }
        /// <summary>
        /// 是否有外链 0：无，1：有
        /// </summary>
        public bool IsExternalLink { get; set; }
        /// <summary>
        /// 外部链接 isExternalLink=true情况下存在
        /// </summary>
        public string ExternalLink { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_notice_info` (
                                                    `Id`,
                                                    `HeadImg`,
                                                    `Title`,
                                                    `Content`,
                                                    `NDate`,
                                                    `InUse`,
                                                    `IsExternalLink`,
                                                    `ExternalLink`,
                                                    `CrtDate`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @HeadImg,
                                                    @Title,
                                                    @Content,
                                                    @NDate,
                                                    @InUse,
                                                    @IsExternalLink,
                                                    @ExternalLink,
                                                    @CrtDate,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_notice_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_notice_info 
                                        where id = @id;";
    }
}