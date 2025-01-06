using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 商品评论表
    /// </summary>
    [Table("czim_goods_comment")]
    public class CzimGoodsComment : StringKeyEntity
    {
        /// <summary>
        /// 评论人id
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 商品id（表 bsv_goods_info）
        /// </summary>
        public string GoodsId { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentDate { get; set; }
        /// <summary>
        /// 评论星级
        /// </summary>
        public int Star { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 点赞次数
        /// </summary>
        public int GiveLikeCount { get; set; }
        /// <summary>
        /// 0:待审核，1:通过，2：屏蔽
        /// </summary>
        public int IsShielded { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_goods_comment` (
                                                    `id`,
                                                    `MemberId`,
                                                    `GoodsId`,
                                                    `CommentDate`,
                                                    `Star`,
                                                    `Content`,
                                                    `GiveLikeCount`,
                                                    `IsShielded`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `CUser`,
                                                    `UUser`,
                                                    `UDate`)
                                        VALUES (
                                                    @id,
                                                    @MemberId,
                                                    @GoodsId,
                                                    @CommentDate,
                                                    @Star,
                                                    @Content,
                                                    @GiveLikeCount,
                                                    @IsShielded,
                                                    @IsActive,
                                                    @CDate,
                                                    @CUser,
                                                    @UUser,
                                                    @UDate);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_goods_comment 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_goods_comment 
                                        where id = @id;";
    }
}