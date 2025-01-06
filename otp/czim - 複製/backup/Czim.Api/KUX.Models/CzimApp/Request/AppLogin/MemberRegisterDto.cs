namespace KUX.Models.CzimApp.Request.AppLogin
{
    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class MemberRegisterDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 重复密码
        /// </summary>
        public string PasswordTwo { get; set; }

        /// <summary>
        /// Verification Code
        /// 验证码
        /// </summary>
        public string VCode { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 会员昵称
        /// </summary>
        /// <value></value>
        public string NickName { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        public string MeId { get; set; }

        /// <summary>
        /// 推荐码
        /// </summary>
        public string ReferralCode { get; set; }
    }
}