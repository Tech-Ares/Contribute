using KUX.Infrastructure.ScanDIService.Attributes;
using KUX.Infrastructure.ScanDIService.Enums;
using Microsoft.Extensions.Configuration;
using System;

namespace KUX.Infrastructure;

/// <summary>
/// 程序配置信息映射类 appsettings.json
/// </summary>
[DIService(DIType.Singleton)]
public class AppConfiguration
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="configuration">配置文件</param>
    public AppConfiguration(IConfiguration configuration)
    {
        this._configuration = configuration;
        //AppConfig
        this.Mapping(nameof(AppConfiguration));
    }

    /// <summary>
    /// 映射数据 到 属性
    /// </summary>
    /// <param name="key"></param>
    private void Mapping(string key)
    {
        var properties = this.GetType().GetProperties();
        foreach (var item in properties)
        {
            var value = _configuration[$"{key}:{item.Name}"];

            if (item.PropertyType == typeof(Guid))
            {
                item.SetValue(this, value.ToGuid());
            }
            else if (item.PropertyType == typeof(int))
            {
                item.SetValue(this, value.ToInt32());
            }
            else
            {
                item.SetValue(this, value);
            }
        }
    }

    /// <summary>
    /// app 标题
    /// </summary>
    public string AppTitle { get; set; }

    /// <summary>
    /// jwt key
    /// </summary>
    public string JwtKeyName { get; set; }

    /// <summary>
    /// jwt 秘钥
    /// </summary>
    public string JwtSecurityKey { get; set; }

    /// <summary>
    /// author key
    /// </summary>
    public string AuthorizationKeyName { get; set; }
}