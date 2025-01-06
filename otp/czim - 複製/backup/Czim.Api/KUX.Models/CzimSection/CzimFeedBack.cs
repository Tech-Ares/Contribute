using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 
    /// </summary>
    [Table("czim_feed_back")]
    public class CzimFeedBack : StringKeyEntity
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsHandle { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleUser { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_feed_back` (
                                                    `Id`,
                                                    `MemberId`,
                                                    `Content`,
                                                    `IsHandle`,
                                                    `HandleUser`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @Id,
                                                    @MemberId,
                                                    @Content,
                                                    @IsHandle,
                                                    @HandleUser,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_feed_back 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_feed_back 
                                        where id = @id;";
    }
}