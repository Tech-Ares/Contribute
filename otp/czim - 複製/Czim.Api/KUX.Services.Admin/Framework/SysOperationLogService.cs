using KUX.Infrastructure;
using KUX.Infrastructure.MessageQueue;
using KUX.Models.Entities.Framework;
using KUX.Repositories.Core.BaseModels;
using KUX.Repositories.Framework;
using KUX.Services.Admin.Accounts;
using KUX.Services.Admin.ServicesAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Services.Admin.Framework;

/// <summary>
/// 操作日服务
/// </summary>
public class SysOperationLogService : AdminBaseService<SysOperationLogRepository>
{
    /// <summary>
    /// 请求上下文
    /// </summary>
    private readonly HttpContext _httpContext;

    /// <summary>
    /// 后台账号服务
    /// </summary>
    private readonly IAccountService _accountService;

    /// <summary>
    /// 消息队列
    /// </summary>
    private readonly IMessageQueueProvider _messageQueueProvider;

    /// <summary>
    /// 系统用户仓储
    /// </summary>
    private readonly SysUserRepository _sysUserRepository;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="iHttpContextAccessor"></param>
    /// <param name="accountService"></param>
    /// <param name="messageQueueProvider"></param>
    /// <param name="sysUserRepository"></param>
    public SysOperationLogService(SysOperationLogRepository repository,
        IHttpContextAccessor iHttpContextAccessor,
        IAccountService accountService,
        IMessageQueueProvider messageQueueProvider,
        SysUserRepository sysUserRepository) : base(repository)
    {
        this._httpContext = iHttpContextAccessor.HttpContext;
        _accountService = accountService;
        this._messageQueueProvider = messageQueueProvider;
        _sysUserRepository = sysUserRepository;
    }

    /// <summary>
    /// 写入操作日志
    /// </summary>
    /// <returns></returns>
    public async Task WriteInLogAsync(long time, string bodyString)
    {
        var queryString = _httpContext.Request.QueryString.ToString();
        var apiUrl = _httpContext.Request.Path;
        //获取请求ip
        var ip = _httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ip))
        {
            ip = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        //
        var clientInfo = _httpContext.GetBrowserClientInfo();
        var browser = clientInfo?.UA.Family + clientInfo?.UA.Major;
        var os = clientInfo?.OS.Family + clientInfo?.OS.Major;

        //本机不记录
        // if (_IP == "::1") return;

        var formString = string.Empty;

        //form
        try
        {
            //读取 表单 信息
            var form = await _httpContext.Request.ReadFormAsync();
            if (form != null)
            {
                var _Dictionary = new Dictionary<string, object>();
                foreach (var key in form.Keys)
                {
                    _Dictionary[key] = form[key];
                }

                formString = _Dictionary.Serialize();// JsonConvert.SerializeObject(_Dictionary);
            }
        }
        catch (Exception) { }

        var userInfo = _accountService.GetAccountInfo();

        // 登录日志
        var sysOperationLog = new SysOperationLog
        {
            Api = apiUrl,
            Ip = ip,
            Form = formString,
            QueryString = queryString,
            FormBody = bodyString,
            UserId = userInfo?.Id,
            TakeUpTime = time,
            Browser = browser,
            OS = os,
        };

        await _messageQueueProvider.SendMessageQueueAsync("WriteInLogAsync", sysOperationLog, (value, serviceProvider) =>
        {
            using (var scope = ServiceProviderExtensions.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<SysOperationLogRepository>();
                repository.InsertAsync((SysOperationLog)value).Wait();
            }
        });
    }

    /// <summary>
    /// 获取列表数据
    /// </summary>
    /// <param name="page">page</param>
    /// <param name="size">size</param>
    /// <param name="search">search</param>
    /// <returns></returns>
    public async Task<PageViewModel> FindListAsync(int page, int size, SysOperationLog search)
    {
        var query = await this.Repository.Orm.Select<SysOperationLog, SysUser>()
                                .LeftJoin(l => l.t1.UserId == l.t2.Id)
                                .WhereIf(!string.IsNullOrWhiteSpace(search.Api), w => w.t1.Api.Contains(search.Api))
                                .WhereIf(!string.IsNullOrWhiteSpace(search.Browser), w => w.t1.Browser.Contains(search.Browser))
                                .WhereIf(!string.IsNullOrWhiteSpace(search.Ip), w => w.t1.Ip.Contains(search.Ip))
                                .WhereIf(!string.IsNullOrWhiteSpace(search.OS), w => w.t1.OS.Contains(search.OS))
                                .Page(page, size)
                                .Count(out var total)
                                .ToListAsync(t => new
                                {
                                    t.t1.Id,
                                    t.t1.Api,
                                    t.t1.Browser,
                                    t.t1.Ip,
                                    t.t1.OS,
                                    t.t1.TakeUpTime,
                                    UserName = t.t2.Name,
                                    t.t2.LoginName,
                                    CDate = t.t1.CDate.ToString("yyyy-MM-dd hh:mm:ss")
                                });
        return await this.Repository.AsPageViewModelAsync(query, page, size, total);
    }

    /// <summary>
    /// 删除所有数据
    /// </summary>
    /// <returns></returns>
    public async Task<bool> DeletedAllData()
    {
        int i = await Repository.DeleteAsync(w => 1 == 1);
        if (i >= 0)
        {
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }

    /// <summary>
    /// 查询表单数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> FindFormAsync(string id)
    {
        var res = new Dictionary<string, object>();
        var form = await this.Repository.FindAsync(id);
        form = form.NullSafe();
        var use = await _sysUserRepository.FindAsync(form.UserId);
        use = use.NullSafe();
        res[nameof(id)] = string.IsNullOrWhiteSpace(id) ? "" : id;
        res[nameof(form)] = form;
        res[nameof(use)] = use;
        return res;
    }
}