using KUX.Infrastructure.ScanDIService.Interface;
using Microsoft.AspNetCore.Http;
using System;

namespace KUX.Infrastructure.Token;

/// <summary>
/// token 服务
/// </summary>
public class TokenService : IDITransientSelf
{
    private readonly AdminConfiguration _appConfiguration;
    private readonly HttpContext _httpContext;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="appConfiguration"></param>
    /// <param name="httpContextAccessor"></param>
    public TokenService(AdminConfiguration appConfiguration, IHttpContextAccessor httpContextAccessor)
    {
        _appConfiguration = appConfiguration;
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <summary>
    /// 根据 id 创建token
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string CreateTokenByAccountId(string id)
    {
        return JwtTokenUtil.CreateToken(id.ToString(), this._appConfiguration.JwtSecurityKey, this._appConfiguration.JwtKeyName, DateTime.Now.AddHours(24 * 3));
    }

    /// <summary>
    /// 获取 token 并得到 id
    /// </summary>
    /// <returns></returns>
    public string GetAccountIdByToken()
    {
        if (this._httpContext == null)
        {
            return string.Empty;
        }

        var token = this._httpContext.Request.Headers[this._appConfiguration.AuthorizationKeyName].ToString();

        if (string.IsNullOrWhiteSpace(token))
        {
            return default;
        }

        if (this._httpContext.User.Identity == null)
        {
            return default;
        }

        //var id = JwtTokenUtil.ReadJwtToken(token).ToGuid();

        return this._httpContext.User.Identity.Name;
    }
}