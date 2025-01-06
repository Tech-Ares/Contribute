using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Services.EaseImServices.Models
{
    /// <summary>
    /// 创建群组对象
    /// </summary>
    public class ImGroupCreateDto
    {
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群组描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 是否是公开群
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 群组成员最大数(包括群主)
        /// </summary>
        public int MaxUsers { get; set; }
        /// <summary>
        /// 是否允许群成员邀请别人加入此群
        /// true：允许群成员邀请人加入此群，
        /// false：只有群主或者管理员才可以往群里加人。注：如果是公开群（public为true），则不允许群成员邀请别人加入此群
        /// </summary>
        public bool AllowInvites { get; set; }
        /// <summary>
        /// 用户申请入群是否需要群主或者群管理员审批，默认是false。
        /// </summary>
        public bool MembersOnly { get; set; }
        /// <summary>
        /// 群组的群主
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 群组成员，此属性为可选的，但是如果加了此项，数组元素至少一个，
        /// 不能超过100个（注：群主user1不需要写入到members里面）
        /// </summary>
        public List<string> Members { get; set; }
    }
}
