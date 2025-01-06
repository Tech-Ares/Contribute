using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.CzimAdmin.Request.CzimChatGroup
{
    /// <summary>
    /// 聊天室创建对象
    /// </summary>
    public class CzimChatGroupCreateDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群头像
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
        /// 是否允许群成员邀请别人加入此群。
        /// true：允许群成员邀请人加入此群，
        /// false：只有群主才可以往群里加人。
        /// 由于只有私有群才允许群成员邀请人加入此群，所以当群组为私有群时，该参数设置为true才有效。默认为false
        /// </summary>
        public bool AllowInvites { get; set; }
        /// <summary>
        /// 群成员上限
        /// </summary>
        public int Maxusers { get; set; }
        /// <summary>
        /// 主管理员id
        /// </summary>
        public string OwnId { get; set; }
        /// <summary>
        /// 群组类型：true：公开群，false：私有群。是否
        /// </summary>
        public bool IsPublic { get; set; }
    }
}
