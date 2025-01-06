using System;using System.Collections.Generic;using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KUX.Models.BaseEntity;
namespace KUX.Models.CzimSection
{
    /// <summary>
    /// 群组管理
    /// </summary>
    [Table("czim_chatgroup_info")]
    public class CzimChatgroupInfo : StringKeyEntity
    {
        /// <summary>
        /// 环信id
        /// </summary>
        public string EaseChatgroupId { get; set; }
        /// <summary>
        /// 群名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群图标
        /// </summary>
        public string GroupImg { get; set; }
        /// <summary>
        /// 群公告
        /// </summary>
        public string Announcement { get; set; }
        /// <summary>
        /// 群组描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 加入群组是否需要群主或者群管理员审批。true：是，false：否。
        /// </summary>
        public bool MemberSonly { get; set; }
        /// <summary>
        /// 是否允许群成员邀请别人加入此群。 true：允许群成员邀请人加入此群，false：只有群主才可以往群里加人。由于只有私有群才允许群成员邀请人加入此群，所以当群组为私有群时，该参数设置为true才有效。默认为false
        /// </summary>
        public bool AllowInvites { get; set; }
        /// <summary>
        /// 群成员上限，创建群组的时候设置需要设置，
        /// </summary>
        public int Maxusers { get; set; }
        /// <summary>
        /// 主管理员id
        /// </summary>
        public string OwnId { get; set; }
        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserNum { get; set; }
        /// <summary>
        /// 群组类型：true：公开群，false：私有群。是否
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 全员禁言
        /// </summary>
        public bool IsProhibit { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// 新增语句
        /// </summary>
        public string InserSql() => @" INSERT INTO `czim_chatgroup_info` (
                                                    `id`,
                                                    `EaseChatgroupId`,
                                                    `GroupName`,
                                                    `GroupImg`,
                                                    `Announcement`,
                                                    `Description`,
                                                    `MemberSonly`,
                                                    `AllowInvites`,
                                                    `Maxusers`,
                                                    `OwnId`,
                                                    `UserNum`,
                                                    `IsPublic`,
                                                    `IsProhibit`,
                                                    `IsDefault`,
                                                    `CrtDate`,
                                                    `IsActive`,
                                                    `CDate`,
                                                    `UDate`,
                                                    `CUser`,
                                                    `UUser`)
                                        VALUES (
                                                    @id,
                                                    @EaseChatgroupId,
                                                    @GroupName,
                                                    @GroupImg,
                                                    @Announcement,
                                                    @Description,
                                                    @MemberSonly,
                                                    @AllowInvites,
                                                    @Maxusers,
                                                    @OwnId,
                                                    @UserNum,
                                                    @IsPublic,
                                                    @IsProhibit,
                                                    @IsDefault,
                                                    @CrtDate,
                                                    @IsActive,
                                                    @CDate,
                                                    @UDate,
                                                    @CUser,
                                                    @UUser);";

        /// <summary>
        /// 激活语句
        /// </summary>
        public string ActiveSql() => @" update czim_chatgroup_info 
                                        set IsActive = @IsActive
                                        where id = @id;";

        /// <summary>
        /// 通过id查询对象
        /// </summary>
        public string GetByidSql() => @" select * 
                                        from czim_chatgroup_info 
                                        where id = @id;";
    }
}