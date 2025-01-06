using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 推荐码下载表
    /// </summary>
    [Table("czim_recomm_download")]
    public class CzimRecommDownload : StringKeyEntity
    {
        /// <summary>
        /// 推荐码
        /// </summary>
        public string BsvNumber { get; set; }
        /// <summary>
        /// 下载ip地址
        /// </summary>
        public string Dlip { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUse { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_recomm_download` (
                                                    `id`,
                                                    `BsvNumber`,
                                                    `Dlip`,
                                                    `IsUse`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @BsvNumber,
                                                    @Dlip,
                                                    @IsUse,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_recomm_download 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_recomm_download 
                                        where id = @id;";
    }
}