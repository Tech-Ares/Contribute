using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUX.Models.KuxAdmin.Reponse.CzimMember
{
    /// <summary>
    /// 会员详情
    /// </summary>
    public class CzimMemberVo
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 硬件识别码
        /// </summary>
        public string Meid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// Bsv会员编号
        /// </summary>
        public string BsvNumber { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Mailbox { get; set; }
        /// <summary>
        /// 性别            1：男2：女0：未知9：未说明
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 用户来源0：未知1：BsvApp2：官网3：MemberApp4：admin9：代理商
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// Referral Code            推荐人编号
        /// </summary>
        public string RNumber { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegDate { get; set; }
        /// <summary>
        /// 是否注册会员
        /// </summary>
        public bool IsMember { get; set; }
        /// <summary>
        /// 会员所属1：自然会员2：系统机器人
        /// </summary>
        public int MemberShip { get; set; }
        /// <summary>
        /// 专属客服id
        /// </summary>
        public string ExclusiveUserId { get; set; }
    }
}
