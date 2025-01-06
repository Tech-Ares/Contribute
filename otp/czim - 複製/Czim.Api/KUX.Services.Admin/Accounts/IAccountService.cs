using KUX.Infrastructure.ScanDIService.Interface;
using KUX.Models.BO;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Accounts;

/// <summary>
/// 当前登录账户服务
/// </summary>
public interface IAccountService : IDIScoped
{
    /// <summary>
    /// 获取账户信息
    /// </summary>
    /// <returns></returns>
    AccountInfo GetAccountInfo();

    /// <summary>
    /// 账户id
    /// </summary>
    public abstract string UserId { get; }

    /// <summary>
    /// 是否超管
    /// </summary>
    public abstract bool IsAdmin { get; }

    /// <summary>
    /// 检查账户密码信息
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<object> CheckAccountAsync(string name, string password, string code);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task<int> ChangePasswordAsync(string oldPassword, string newPassword);
}