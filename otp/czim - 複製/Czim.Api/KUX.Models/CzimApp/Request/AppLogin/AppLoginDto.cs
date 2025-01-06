namespace KUX.Models.CzimApp.Request.AppLogin
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    public class AppLoginDto
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 验证码（正式登录填写）
        /// </summary>
        public string LoginCode { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string MeId { get; set; }
    }
}