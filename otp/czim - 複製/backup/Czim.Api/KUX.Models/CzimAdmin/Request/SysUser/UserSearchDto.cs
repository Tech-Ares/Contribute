namespace KUX.Models.KuxAdmin.Request.SysUser
{
    /// <summary>
    /// 用户查询
    /// </summary>
    public class UserSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public string OrganizationId { get; set; }
    }
}