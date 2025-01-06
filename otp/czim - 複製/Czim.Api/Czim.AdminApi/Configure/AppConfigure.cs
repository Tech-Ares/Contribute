using Czim.AdminApi.Middlewares;
using KUX.Infrastructure;
using KUX.Infrastructure.MessageQueue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Czim.AdminApi.Configure;

/// <summary>
/// 应用程序配置
/// </summary>
public class AppConfigure
{
    /// <summary>
    /// 配置构建
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="messageQueueProvider"></param>
    public static void Build(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IMessageQueueProvider messageQueueProvider)
    {
        // 使用静态文件
        app.UseStaticFiles();

        #region 注册服务提供者

        serviceProvider.RegisterServiceProvider();

        #endregion 注册服务提供者

        #region JWT

        app.UseAuthentication();
        app.UseAuthorization();

        #endregion JWT

        #region Swagger

        //启用中间件服务生成Swagger作为JSON终结点
        app.UseSwagger();
        //启用中间件服务对swagger-ui，指定Swagger JSON终结点
        app.UseSwaggerUI(option =>
        {
            foreach (var item in AppConfigureServices.GetVersionList()) option.SwaggerEndpoint($"{item}/swagger.json", item);
            option.RoutePrefix = "swagger";
        });

        #endregion Swagger

        #region 使用跨域 警告: 通过终结点路由，CORS 中间件必须配置为在对UseRouting和UseEndpoints的调用之间执行。 配置不正确将导致中间件停止正常运行。

        app.UseCors("WebHostCors");

        #endregion 使用跨域 警告: 通过终结点路由，CORS 中间件必须配置为在对UseRouting和UseEndpoints的调用之间执行。 配置不正确将导致中间件停止正常运行。

        #region 使用 Api 耗时计算中间件

        app.UseMiddleware<TakeUpTimeMiddleware>();

        #endregion 使用 Api 耗时计算中间件

        #region 消息队列启动

        messageQueueProvider.RunAsync().Wait();

        #endregion 消息队列启动
    }
}