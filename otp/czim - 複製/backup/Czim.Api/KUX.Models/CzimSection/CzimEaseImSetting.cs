using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 环信基础配置
    /// </summary>
    [Table("czim_ease_im_setting")]
    public class CzimEaseImSetting : StringKeyEntity
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Orgname { get; set; }
        /// <summary>
        /// App名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 开发者账号
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 客户端密钥
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiresTime { get; set; }
        /// <summary>
        /// 当前账号uuid
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_ease_im_setting` (
                                                    `Id`,
                                                    `Orgname`,
                                                    `AppName`,
                                                    `AppKey`,
                                                    `ClientId`,
                                                    `ClientSecret`,
                                                    `AccessToken`,
                                                    `ExpiresTime`,
                                                    `ApplicationId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @Id,
                                                    @Orgname,
                                                    @AppName,
                                                    @AppKey,
                                                    @ClientId,
                                                    @ClientSecret,
                                                    @AccessToken,
                                                    @ExpiresTime,
                                                    @ApplicationId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_ease_im_setting 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_ease_im_setting 
                                        where id = @id;";
    }
}