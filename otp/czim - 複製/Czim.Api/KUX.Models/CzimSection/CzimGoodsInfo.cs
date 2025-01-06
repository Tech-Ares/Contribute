using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Table("czim_goods_info")]
    public class CzimGoodsInfo : StringKeyEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 商品封面
        /// </summary>
        public string GoodsImg { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        public string TagInfo { get; set; }
        /// <summary>
        /// 营运类型1：自营2：商家3：个人
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 价格（钻石，金币）
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 审核状态 0：待 1：通过2：拒绝
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 商品定位
        /// </summary>
        public string GoodsCity { get; set; }
        /// <summary>
        /// 热度（观看次数）
        /// </summary>
        public int WatchCount { get; set; }
        /// <summary>
        /// 点赞次数
        /// </summary>
        public int GiveLikeCount { get; set; }
        /// <summary>
        /// 评论次数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 是否认证商家
        /// </summary>
        public bool IsAuth { get; set; }
        /// <summary>
        /// 认证押金
        /// </summary>
        public int AuthDeposit { get; set; }
        /// <summary>
        /// 关联商家id
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_goods_info` (
                                                    `id`,
                                                    `Title`,
                                                    `Content`,
                                                    `GoodsImg`,
                                                    `TagInfo`,
                                                    `OperationType`,
                                                    `Price`,
                                                    `AuditStatus`,
                                                    `GoodsCity`,
                                                    `WatchCount`,
                                                    `GiveLikeCount`,
                                                    `CommentCount`,
                                                    `IsAuth`,
                                                    `AuthDeposit`,
                                                    `MerchantId`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @Title,
                                                    @Content,
                                                    @GoodsImg,
                                                    @TagInfo,
                                                    @OperationType,
                                                    @Price,
                                                    @AuditStatus,
                                                    @GoodsCity,
                                                    @WatchCount,
                                                    @GiveLikeCount,
                                                    @CommentCount,
                                                    @IsAuth,
                                                    @AuthDeposit,
                                                    @MerchantId,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_goods_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_goods_info 
                                        where id = @id;";
    }
}