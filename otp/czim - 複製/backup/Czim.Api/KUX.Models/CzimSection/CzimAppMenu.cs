using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// bsv app菜单信息
    /// </summary>
    [Table("czim_app_menu")]
    public class CzimAppMenu : StringKeyEntity
    {
        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int MenuSort { get; set; }
        /// <summary>
        /// App菜单说明
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuImg { get; set; }
        /// <summary>
        /// 菜单静默图标
        /// </summary>
        public string MenuImgSilence { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_app_menu` (
                                                    `Id`,
                                                    `MenuSort`,
                                                    `MenuName`,
                                                    `MenuImg`,
                                                    `MenuImgSilence`,
                                                    `IsActive`,
                                                    `CUser`,
                                                    `CDate`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @Id,
                                                    @MenuSort,
                                                    @MenuName,
                                                    @MenuImg,
                                                    @MenuImgSilence,
                                                    @IsActive,
                                                    @CUser,
                                                    @CDate,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_app_menu 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_app_menu 
                                        where id = @id;";
    }
}