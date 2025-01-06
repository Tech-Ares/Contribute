namespace KUX.Models.CzimApp.Request.AppLogin
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginTimeDto
    {
        /// <summary>
        /// 登录帐号或者 meid
        /// </summary>
        public string MeId { get; set; }

        /// <summary>
        /// 登录机型
        /// </summary>
        public string LoginModel { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public string LoginOS { get; set; }
    }
}