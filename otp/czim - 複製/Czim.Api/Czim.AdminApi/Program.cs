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

// ����nolog��־����
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

#region ��������
var builder = WebApplication.CreateBuilder(args);

//��ַ
builder.WebHost.UseUrls("http://*:5610");

//ʹ�� Nlog
builder.WebHost.UseNLog();

//����NLog
NLogUtil.Init(logger);

logger.Debug("��ʼ�� Main !");

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // ���������
    options.Filters.Add<ApiExceptionFilter>();
    // ��Դ����
    options.Filters.Add<ApiResourceCacheFilterAttribute>();
    // ��Ȩ������
    options.Filters.Add<ApiAuthorizationActionFilter>();
})
.AddJsonOptions(options =>
{
    //���� ����� Dictionary ��ô �� json ���л� �� key ���ַ� ���� С�շ� ����
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    // ʱ���ʽ��
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    // ʱ��Ϊnull ��ʽ��
    options.JsonSerializerOptions.Converters.Add(new DateTimeNullJsonConverter());

    // Ĭ��ȥ����ֵ
    //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // long��ת�ַ�������js���ȶ�ʧ
    options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());

});

// ·��Сд
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//���񹹽�
AppConfigureServices.Build(builder.Services, builder.Configuration);

var app = builder.Build();

#endregion

#region Ӧ�÷�������

// ��ȡ��ǰ����
var messageQueueProvider = app.Services.GetRequiredService<IMessageQueueProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KUX.WebHost v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

//���ù���
AppConfigure.Build(app, app.Environment, app.Services, messageQueueProvider);

//��������
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

#endregion