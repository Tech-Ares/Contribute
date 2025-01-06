using KUX.Models.KuxAdmin.Request;
namespace KUX.Models.CzimAdmin.Request.CzimChat;

/// <summary>
/// 群信息查询
/// </summary>
public class ChatgroupSearchDto : BaseSearchDto
{
    /// <summary>
    /// 群名称
    /// </summary>
    public string ChatgroupName { get; set; }

    /// <summary>
    /// 群id
    /// </summary>
    public string ChatgroupId { get; set; }

}