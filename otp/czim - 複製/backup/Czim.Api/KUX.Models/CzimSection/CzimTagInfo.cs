using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 视频标签信息
    /// </summary>
    [Table("czim_tag_info")]
    public class CzimTagInfo : StringKeyEntity
    {
        /// <summary>
        /// 标签说明
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 标签类型1：系统标签2：自定义标签3：用户自定义标签
        /// </summary>
        public int TagType { get; set; }
        /// <summary>
        /// 标签贴图
        /// </summary>
        public string TagImg { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_tag_info` (
                                                    `Id`,
                                                    `TagName`,
                                                    `TagType`,
                                                    `TagImg`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @TagName,
                                                    @TagType,
                                                    @TagImg,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_tag_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_tag_info 
                                        where id = @id;";
    }
}