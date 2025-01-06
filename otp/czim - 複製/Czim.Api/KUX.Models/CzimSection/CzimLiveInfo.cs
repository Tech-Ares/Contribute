using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 视频直播地址
    /// </summary>
    [Table("czim_live_info")]
    public class CzimLiveInfo : StringKeyEntity
    {
        /// <summary>
        /// 视频编号（使用种子表id）
        /// </summary>
        public long Number { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 创作人id
        /// </summary>
        public string CrtMemberId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 直播时间（开始时间）
        /// </summary>
        public DateTime LiveDate { get; set; }
        /// <summary>
        /// 直播结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 录播地址
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 视频贴图
        /// </summary>
        public string HeadPicture { get; set; }
        /// <summary>
        /// 推流地址
        /// </summary>
        public string PushFlowUrl { get; set; }
        /// <summary>
        /// 播放地址
        /// </summary>
        public string PlayUrl { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsOvert { get; set; }
        /// <summary>
        /// 可见度1：私人，2：团队，3：部分
        /// </summary>
        public int Openness { get; set; }
        /// <summary>
        /// 审核状态 0：待 -1：未通过 1：通过
        /// </summary>
        public int IsExamine { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime ExamineDate { get; set; }
        /// <summary>
        /// 直播状态0：未开始1：直播中2：已结束
        /// </summary>
        public int LiveStatus { get; set; }
        /// <summary>
        /// 点赞次数
        /// </summary>
        public int GiveLikeCount { get; set; }
        /// <summary>
        /// 评论次数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int SignUpCount { get; set; }
        /// <summary>
        /// 是否精华
        /// </summary>
        public bool IsCream { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_live_info` (
                                                    `id`,
                                                    `Number`,
                                                    `Title`,
                                                    `CrtMemberId`,
                                                    `CrtDate`,
                                                    `LiveDate`,
                                                    `EndDate`,
                                                    `VideoUrl`,
                                                    `Content`,
                                                    `HeadPicture`,
                                                    `PushFlowUrl`,
                                                    `PlayUrl`,
                                                    `IsOvert`,
                                                    `Openness`,
                                                    `IsExamine`,
                                                    `ExamineDate`,
                                                    `LiveStatus`,
                                                    `GiveLikeCount`,
                                                    `CommentCount`,
                                                    `SignUpCount`,
                                                    `IsCream`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @Number,
                                                    @Title,
                                                    @CrtMemberId,
                                                    @CrtDate,
                                                    @LiveDate,
                                                    @EndDate,
                                                    @VideoUrl,
                                                    @Content,
                                                    @HeadPicture,
                                                    @PushFlowUrl,
                                                    @PlayUrl,
                                                    @IsOvert,
                                                    @Openness,
                                                    @IsExamine,
                                                    @ExamineDate,
                                                    @LiveStatus,
                                                    @GiveLikeCount,
                                                    @CommentCount,
                                                    @SignUpCount,
                                                    @IsCream,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_live_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_live_info 
                                        where id = @id;";
    }
}