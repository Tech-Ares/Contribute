namespace KUX.Models.KuxAdmin.Request.SysRole
{
    /// <summary>
    /// 角色查询模型
    /// </summary>
    public class RoleSearchDto : BaseSearchDto
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
    }
}