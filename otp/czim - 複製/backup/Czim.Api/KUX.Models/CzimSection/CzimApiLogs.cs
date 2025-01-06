using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// api接口日志
    /// </summary>
    [Table("czim_api_logs")]
    public class CzimApiLogs : LongKeyEntity
    {
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 请求ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestPath { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 总耗时
        /// </summary>
        public int TotalTimes { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string ErrorContent { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_api_logs` (
                                                    `CrtDate`,
                                                    `Ip`,
                                                    `RequestPath`,
                                                    `Level`,
                                                    `TotalTimes`,
                                                    `ErrorContent`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @CrtDate,
                                                    @Ip,
                                                    @RequestPath,
                                                    @Level,
                                                    @TotalTimes,
                                                    @ErrorContent,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_api_logs 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_api_logs 
                                        where id = @id;";
    }
}