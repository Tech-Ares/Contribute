using KUX.Controllers.Admin.ControllersAdmin;
using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.DTO;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Framework;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KUX.Controllers.Admin.Framework;

/// <summary>
/// 个人中心
/// </summary>
[ControllerDescriptor()]
public class PersonalCenterController : AdminBaseController<SysUserService>
{
    ///// <summary>
    ///// 用户账户信息
    ///// </summary>
    //private readonly IAccountService _accountService;

    /// <summary>
    /// 用户仓储
    /// </summary>
    private readonly SysUserRepository _sysUserRepository;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultService">默认服务</param>
    /// <param name="accountService">账户服务</param>
    /// <param name="sysUserRepository">用户仓储</param>
    public PersonalCenterController(SysUserService defaultService, IAccountService accountService, SysUserRepository sysUserRepository) 
        : base(defaultService, accountService)
    {
        //_accountService = accountService;
        _sysUserRepository = sysUserRepository;
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("ChangePassword")]
    public async Task<int> ChangePasswordAsync([FromBody] ChangePasswordFormDto form)
        => await this.accountService.ChangePasswordAsync(form.OldPassword, form.NewPassword);

    /// <summary>
    /// 更改用户信息
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("SaveForm")]
    public async Task<SysUser> SaveFormAsync([FromBody] SysUser form)
    {
        var accountInfo = this.accountService.GetAccountInfo();
        var sysUser = await _sysUserRepository.FindAsync(accountInfo.Id);
        sysUser.Name = form.Name;
        sysUser.LoginName = form.LoginName;
        sysUser.Email = form.Email;
        sysUser.Phone = form.Phone;
        return await _sysUserRepository.InsertOrUpdateAsync(sysUser);
    }
}