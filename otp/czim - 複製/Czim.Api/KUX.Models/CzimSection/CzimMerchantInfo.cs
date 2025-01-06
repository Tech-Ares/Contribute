using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// otc商铺表
    /// </summary>
    [Table("czim_merchant_info")]
    public class CzimMerchantInfo : StringKeyEntity
    {
        /// <summary>
        /// 会员账号id
        /// </summary>
        public string MId { get; set; }
        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 商铺地址
        /// </summary>
        public string StoreAdress { get; set; }
        /// <summary>
        /// 交易次数
        /// </summary>
        public int TradeNum { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        public double ScoringRate { get; set; }
        /// <summary>
        /// 上级Id（推荐人id）
        /// </summary>
        public string PCId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_merchant_info` (
                                                    `Id`,
                                                    `MId`,
                                                    `StoreName`,
                                                    `StoreAdress`,
                                                    `TradeNum`,
                                                    `ScoringRate`,
                                                    `PCId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @Id,
                                                    @MId,
                                                    @StoreName,
                                                    @StoreAdress,
                                                    @TradeNum,
                                                    @ScoringRate,
                                                    @PCId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_merchant_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_merchant_info 
                                        where id = @id;";
    }
}