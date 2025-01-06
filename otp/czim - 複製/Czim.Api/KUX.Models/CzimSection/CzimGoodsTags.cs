using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 商品标签
    /// </summary>
    [Table("czim_goods_tags")]
    public class CzimGoodsTags : StringKeyEntity
    {
        /// <summary>
        /// 标签说明
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 标签贴图
        /// </summary>
        public string TagImg { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_goods_tags` (
                                                    `Id`,
                                                    `TagName`,
                                                    `TagImg`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @TagName,
                                                    @TagImg,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_goods_tags 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_goods_tags 
                                        where id = @id;";
    }
}