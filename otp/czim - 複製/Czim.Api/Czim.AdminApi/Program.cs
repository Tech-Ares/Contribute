using KUX.Infrastructure.MessageQueue;
using KUX.Infrastructure.NLogService;
using KUX.Infrastructure.TextJson;
using Czim.AdminApi.Configure;
using Czim.AdminApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using KUX.Controllers.Filters;

// 创建nolog日志对象
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

#region 创建主机
var builder = WebApplication.CreateBuilder(args);

//地址
builder.WebHost.UseUrls("http://*:5610");

//使用 Nlog
builder.WebHost.UseNLog();

//设置NLog
NLogUtil.Init(logger);

logger.Debug("初始化 Main !");

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // 错误过滤器
    options.Filters.Add<ApiExceptionFilter>();
    // 资源缓存
    options.Filters.Add<ApiResourceCacheFilterAttribute>();
    // 授权过滤器
    options.Filters.Add<ApiAuthorizationActionFilter>();
})
.AddJsonOptions(options =>
{
    //设置 如果是 Dictionary 那么 在 json 序列化 是 key 的字符 采用 小驼峰 命名
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    // 时间格式化
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    // 时间为null 格式化
    options.JsonSerializerOptions.Converters.Add(new DateTimeNullJsonConverter());

    // 默认去掉空值
    //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // long型转字符串，防js精度丢失
    options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());

});

// 路由小写
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//服务构建
AppConfigureServices.Build(builder.Services, builder.Configuration);

var app = builder.Build();

#endregion

#region 应用服务启动

// 获取当前队列
var messageQueueProvider = app.Services.GetRequiredService<IMessageQueueProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KUX.WebHost v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

//配置构建
AppConfigure.Build(app, app.Environment, app.Services, messageQueueProvider);

//结束配置
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

#endregion