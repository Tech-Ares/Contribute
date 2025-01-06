using System.Collections.Generic;

namespace KUX.Services.EaseImServices.Models;

public class ChatRoomBatchAddUser
{
    /// <summary>
    /// 新成员
    /// </summary>
    public List<string> newmembers { get; set; }
}