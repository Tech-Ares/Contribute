using Czim.AdminApi.Middlewares;
using KUX.Infrastructure;
using KUX.Infrastructure.Redis;
using KUX.Infrastructure.ScanDIService;
using KUX.Repositories.Core.DapperUtil;
using KUX.Repositories.Core.FreeSqlUtil;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Czim.AdminApi.Configure;

/// <summary>
/// 项目启动配置
/// </summary>
public class AppConfigureServices
{
    /// <summary>
    /// 获取swagger版本标记
    /// </summary>
    private static readonly IEnumerable<string> _versionList = typeof(ApiVersions).GetEnumNames().OrderBy(w => w);

    /// <summary>
    /// 文件前缀
    /// </summary>
    public static readonly string prefixString = "KUX.";

    /// <summary>
    /// 获取 Swagger 版本集合
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> GetVersionList() => _versionList;

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void Build(IServiceCollection services, IConfiguration configuration)
    {
        //jwtKey
        var jwtKeyName = configuration["AdminConfiguration:JwtKeyName"];
        // jwt 密钥
        var jwtSecurityKey = configuration["AdminConfiguration:JwtSecurityKey"];
        // 数据库 字符串
        var connectionString = configuration["AdminConfiguration:AdminConnectionString"];
        // redis 连接字符串
        var connectionStringRedis = configuration["AdminConfiguration:ConnectionStringRedis"];

        // 生命周期监听
        services.AddSingleton<IHostedService, LifetimeEventsHostedService>();

        #region 取消默认验证Api 接收参数模型 的 验证特性 如有 [ApiController]

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        #endregion 取消默认验证Api 接收参数模型 的 验证特性 如有 [ApiController]

        #region HttpContext、IMemoryCache

        // 添加http请求
        services.AddHttpClient();

        // 上下文注入
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // 缓存注入
        services.AddMemoryCache();

        #endregion HttpContext、IMemoryCache

        #region 数据库仓储注册 、 自动扫描服务注册 、 中间件注册

        //配置freesql
        FreeSqlCoreModule.RegisterFreeSql(services, connectionString, DefaultDatabaseType.MySql, $"{prefixString}Repositories");

        // dapper 注入
        services.AddDapper(connectionString, KUX.Repositories.Core.SqlProvider.MySql);

        //配置redis
        RedisExtensions.RegisterRedisService(services, connectionStringRedis);

        //扫描服务自动化注册
        services.ScanningAppServices(prefixString);

        //添加时间监控中间件
        services.AddScoped<TakeUpTimeMiddleware>();

        #endregion 数据库仓储注册 、 自动扫描服务注册 、 中间件注册

        #region 跨域配置 配置跨域处理

        services.AddCors(options =>
        {
            options.AddPolicy("WebHostCors", builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                //.AllowAnyOrigin()
                //.AllowCredentials();
                //6877
            });
        });

        #endregion 跨域配置 配置跨域处理

        #region JWT

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //是否验证Issuer
                    ValidateAudience = true, //是否验证Audience
                    ValidateLifetime = true, //是否验证失效时间
                    ValidateIssuerSigningKey = true, //是否验证SecurityKey
                    ValidAudience = jwtKeyName, //Audience
                    ValidIssuer = jwtKeyName, //Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey)) //拿到SecurityKey
                };
            });

        #endregion JWT

        #region Swagger 注册Swagger生成器，定义一个和多个Swagger 文档

        services.AddSwaggerGen(options =>
        {
            foreach (var item in _versionList)
            {
                options.SwaggerDoc(item, new OpenApiInfo
                {
                    Title = item
                });
            }

            //为 Swagger JSON and UI设置xml文档注释路径
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
            .Where(w => w.EndsWith(".xml") && w.Contains(prefixString))
            .Select(w => w)
            .ToList()
            .ForEach(w => options.IncludeXmlComments(w, true))
            ;

            #region Jwt token 配置

            //option.OperationFilter<AppService.SwaggerParameterFilter>(); // 给每个接口配置授权码传入参数文本框
            //
            options.OperationFilter<AddResponseHeadersFilter>();
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            //很重要！这里配置安全校验，和之前的版本不一样
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            ////开启 oauth2 安全描述
            //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //{
            //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
            //    In = ParameterLocation.Header,
            //    Name = "Authorization",
            //    Type = SecuritySchemeType.ApiKey,
            //    //Scheme = "basic",
            //});
            //开启 oauth2 安全描述
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                BearerFormat = "JWT"
                //Scheme = "basic",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

            #endregion Jwt token 配置
        });

        #endregion Swagger 注册Swagger生成器，定义一个和多个Swagger 文档
    }
}