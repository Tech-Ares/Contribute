using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 基础设置
    /// </summary>
    [Table("czim_base_setting")]
    public class CzimBaseSetting : StringKeyEntity
    {
        /// <summary>
        /// 配置说明
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 配置类型
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string TypeValue { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_base_setting` (
                                                    `id`,
                                                    `TypeName`,
                                                    `TypeCode`,
                                                    `TypeValue`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @TypeName,
                                                    @TypeCode,
                                                    @TypeValue,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_base_setting 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_base_setting 
                                        where id = @id;";
    }
}