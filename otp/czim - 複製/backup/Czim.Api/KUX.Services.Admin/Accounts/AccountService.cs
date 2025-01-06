using KUX.Infrastructure;
using KUX.Infrastructure.Token;
using KUX.Models.BO;
using KUX.Models.ApiResultManage;
using KUX.Models.Consts;
using KUX.Models.CzimSection;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Accounts;

/// <summary>
/// 当前登录账户服务
/// </summary>
public class AccountService : IAccountService
{
    /// <summary>
    /// 账户信息
    /// </summary>
    private readonly AccountInfo _accountInfo;

    /// <summary>
    /// 程序配置
    /// </summary>
    private readonly AdminConfiguration _appConfiguration;

    /// <summary>
    /// token服务
    /// </summary>
    private readonly TokenService _tokenService;

    /// <summary>
    /// 组织仓储
    /// </summary>
    private readonly SysOrganizationRepository _sysOrganizationRepository;

    /// <summary>
    /// 用户仓储
    /// </summary>
    private readonly SysUserRepository _sysUserRepository;

    /// <summary>
    /// token 类型
    /// </summary>
    private const string tokenType = "Bearer ";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="sysUserRepository"></param>
    /// <param name="appConfiguration"></param>
    /// <param name="tokenService"></param>
    /// <param name="sysOrganizationRepository"></param>
    public AccountService(SysUserRepository sysUserRepository,
        AdminConfiguration appConfiguration,
        TokenService tokenService,
        SysOrganizationRepository sysOrganizationRepository)
    {
        _sysUserRepository = sysUserRepository;
        _appConfiguration = appConfiguration;
        _tokenService = tokenService;
        _sysOrganizationRepository = sysOrganizationRepository;
        this._accountInfo = this.FindAccountInfoByToken().Result;
    }

    /// <summary>
    /// 根据用户信息获取 Account 对象
    /// </summary>
    /// <returns></returns>
    private async Task<AccountInfo> FindAccountInfoByToken()
    {
        var id = _tokenService.GetAccountIdByToken();

        if (string.IsNullOrWhiteSpace(id))
        {
            return default;
        }

        var sysUser = await this._sysUserRepository.FindAsync(id);
        if (sysUser == null)
        {
            return default;
        }
        // 用户角色
        var sysRoles = await this._sysUserRepository.Orm.Select<SysRole, SysUserRole>()
                                                        .LeftJoin(l => l.t1.Id == l.t2.RoleId)
                                                        .Where(w => w.t2.UserId == id)
                                                        .ToListAsync(t => t.t1);
        // 用户岗位
        var sysPosts = await this._sysUserRepository.Orm.Select<SysPost, SysUserPost>()
                                    .LeftJoin(l => l.t1.Id == l.t2.PostId)
                                    .Where(w => w.t2.UserId == id)
                                    .ToListAsync(t => t.t1);
        // 用户部门
        var sysOrganization = await this._sysOrganizationRepository.FindAsync(sysUser.OrganizationId);

        var accountInfo = new AccountInfo();
        accountInfo = sysUser.MapTo<SysUser, AccountInfo>();
        accountInfo.IsAdministrator = sysRoles.Any(w => w.Id == this._appConfiguration.AdminRoleId);
        accountInfo.SysRoles = sysRoles;
        accountInfo.SysPosts = sysPosts;
        accountInfo.SysOrganization = sysOrganization;

        return accountInfo;
    }

    /// <summary>
    /// 获取当前登录账户信息
    /// </summary>
    /// <returns></returns>
    public virtual AccountInfo GetAccountInfo() => this._accountInfo;

    /// <summary>
    /// 账户id
    /// </summary>
    public virtual string UserId => (this._accountInfo == null || string.IsNullOrWhiteSpace(this._accountInfo.Id)) ? "" : this._accountInfo.Id;

    /// <summary>
    /// 是否超管
    /// </summary>
    public virtual bool IsAdmin => (this._accountInfo == null || string.IsNullOrWhiteSpace(this._accountInfo.Id)) ? false : this._accountInfo.IsAdministrator;

    /// <summary>
    /// 检查账户 登录信息 返回token等信息
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public virtual async Task<object> CheckAccountAsync(string name, string password, string code)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("请输入账户名!");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("请输入密码！");
        }
        // if (string.IsNullOrWhiteSpace(code))
        //  MessageBox.Show("请输入验证码!");
        var sysUser = await this._sysUserRepository.Select.Where(w => w.LoginName == name).FirstAsync();
        if (sysUser == null)
        {
            MessageBox.Show("账户或者密码错误!");
        }

        //if (sysUser.Password.Trim() != Tools.Md5Encrypt(password))
        if (sysUser.Password.Trim() != password)
        {
            MessageBox.Show("账户或者密码错误!");
        }

        //string code = Tools.GetCookie("loginCode");
        //if (string.IsNullOrEmpty(code)) throw new MessageBox("验证码失效");
        //if (!code.ToLower().Equals(loginCode.ToLower())) throw new MessageBox("验证码不正确");

        // 生成user对象
        var token = _tokenService.CreateTokenByAccountId(sysUser.Id);
        // 查询客服帐号
        var customer = await this._sysUserRepository.Orm.Select<CzimChatCustomer>()
                                                        .Where(s => s.UserId == sysUser.Id)
                                                        .FirstAsync();
        string isEaseid = "";
        if (customer != null)
        {
            isEaseid = customer.EaseChatCustomerId;
        }
        //查询customer表，判断有没有环信id，有则未true，没有则为false
        var data = new
        {
            token = tokenType + token,
            userInfo = new
            {
                userId = sysUser.Id,
                userName = sysUser.Name,
                dashboard = "0",
                role = new List<string>() { "SA", "admin", "Auditor" },
                easePwd = $"{sysUser.Id}{CzimConsts.EaseSalt}".Md5Encrypt(),
                isEase = !string.IsNullOrEmpty(isEaseid),
                nickName = string.IsNullOrWhiteSpace(customer?.NickName) ? sysUser.Name : customer.NickName,
                avatar = customer?.Avatar
            }
        };

        return data;
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public virtual async Task<int> ChangePasswordAsync(string oldPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(oldPassword))
        {
            MessageBox.Show("旧密码不能为空！");
        }
        if (string.IsNullOrEmpty(newPassword))
        {
            MessageBox.Show("新密码不能为空！");
        }
        var sysUser = await this._sysUserRepository.FindAsync(this.GetAccountInfo().Id);
        if (sysUser.Password != oldPassword)
        {
            MessageBox.Show("旧密码不正确！");
        }
        sysUser.Password = newPassword;
        return await this._sysUserRepository.UpdateAsync(sysUser);
    }
}