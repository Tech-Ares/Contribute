using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// app版本
    /// </summary>
    [Table("czim_app_version")]
    public class CzimAppVersion : LongKeyEntity
    {
        /// <summary>
        /// 版本编号
        /// </summary>
        public int VersionCode { get; set; }
        /// <summary>
        /// 手机系统1：安卓2：苹果
        /// </summary>
        public int PhoneSystem { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 更新提示
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool Focus { get; set; }
        /// <summary>
        /// 安卓安装包地址
        /// </summary>
        public string ApkDownLoadUrl { get; set; }
        /// <summary>
        /// ios外链地址
        /// </summary>
        public string IosLinkUrl { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_app_version` (
                                                    `VersionCode`,
                                                    `PhoneSystem`,
                                                    `Channel`,
                                                    `Version`,
                                                    `Title`,
                                                    `Content`,
                                                    `Focus`,
                                                    `ApkDownLoadUrl`,
                                                    `IosLinkUrl`,
                                                    `UpdateDate`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @VersionCode,
                                                    @PhoneSystem,
                                                    @Channel,
                                                    @Version,
                                                    @Title,
                                                    @Content,
                                                    @Focus,
                                                    @ApkDownLoadUrl,
                                                    @IosLinkUrl,
                                                    @UpdateDate,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_app_version 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_app_version 
                                        where id = @id;";
    }
}