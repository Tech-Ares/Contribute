using KUX.Repositories.CzimSection;
using KUX.Services.Admin.ServicesAdmin;
using KUX.Services.EaseImServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUX.Services.Admin.CzmiSection;

/// <summary>
/// 环信配置服务
/// </summary>
public class CzimEaseImSettingService : AdminBaseService<CzimEaseImSettingRepository>
{
    /// <summary>
    /// 环信服务
    /// </summary>
    private readonly EaseImService easeImService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository">默认缓存</param>
    /// <param name="_easeImService">环信服务</param>
    public CzimEaseImSettingService(CzimEaseImSettingRepository repository,
        EaseImService _easeImService) : base(repository)
    {
        easeImService = _easeImService;
    }

    public async Task<string> CreateUsers()
    {
        var result = await this.easeImService.CreateUsersTestAsync("admin123", "移动测试", "admin123");
        return default;
    }

    public async Task<string> DeleteUsersAsync()
    {
        var result = await this.easeImService.DeleteUsersAsync("4462c5efdc144783aa6782859a44ad25");
        return default;
    }

    public async Task<string> CreateChatRoomAsync()
    {
        List<string> members = new List<string>() {
        "admin",
        };
        var result = await this.easeImService.CreateChatRoomAsync("测试一", "这是第一个聊天室", "admin", members);
        return result;
    }

    public async Task<bool> MessagesRecall(string msgid, string crid)
    {
        var result = await this.easeImService.MessagesRecallAsync(msgid, crid, "groupchat");

        return default;
    }
}