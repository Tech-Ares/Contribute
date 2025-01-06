using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Services.Admin.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;

namespace Czim.AdminApi.Filters;

/// <summary>
/// 后台 权限 管理 控制
/// </summary>
public class ApiAuthorizationActionFilter : IActionFilter
{
    /// <summary>
    /// 账号服务
    /// </summary>
    private readonly IAccountService _accountService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="accountService">账户服务</param>
    public ApiAuthorizationActionFilter(IAccountService accountService)
    {
        //_sysMenuService = sysMenuService;
        _accountService = accountService;
    }

    /// <summary>
    /// Action 执行前
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnActionExecuting(ActionExecutingContext context)
    {
        // 获取action授权
        var actionDescriptorAttribute = (ActionDescriptorAttribute)context.ActionDescriptor.EndpointMetadata
                                        .FirstOrDefault(w => w is ActionDescriptorAttribute);

        if (actionDescriptorAttribute != null && !actionDescriptorAttribute.AuthCheck)
        {
            // 如果不需要授权，则不进行授权验证
            return;
        }

        var routeValues = context.ActionDescriptor.RouteValues;
        //var areaName = routeValues["area"];
        var controllerName = routeValues["controller"];
        var actionName = routeValues["action"];

        var controller = (ControllerBase)context.Controller;
        //获取 class 上面所有的 自定义 特性
        var customAttributes = controller.GetType().GetCustomAttributes();

        //处理 控制器 class 上面 带有 ControllerDescriptorAttribute 标记
        var adminApiDescribeAttribute = (ControllerDescriptorAttribute)customAttributes
            .FirstOrDefault(w => w is ControllerDescriptorAttribute);
        if (adminApiDescribeAttribute == null)
        {
            return;
        }

        var httpContext = context.HttpContext;
        const string unAuthMessage = "未授权,请先登录授权!";

        #region 检查是否登录 授权

        if (this._accountService.GetAccountInfo() == null)
        {
            var data = ApiResult.ResultMessage(ApiResultCodeEnum.UnAuth, unAuthMessage);
            context.Result = new JsonResult(data);
        }

        #endregion 检查是否登录 授权

        var menuId = adminApiDescribeAttribute.GetMenuId();

        #region 检查页面权限信息

        if (string.IsNullOrWhiteSpace(menuId))
        {
            return;
        }

        if (actionDescriptorAttribute == null)
        {
            return;
        }

        //var functionName = actionDescriptorAttribute.GetFunctionName();

        ////收集用户权限 未授权让他重新登录
        //var power = this._sysMenuService.GetPowerStateByMenuId(menuId).Result;

        //if (power.ContainsKey(functionName) && !power[functionName])
        //{
        //    var data = ApiResult.ResultMessage(ApiResultCodeEnum.UnAuth, unAuthMessage);
        //    context.Result = new JsonResult(data);
        //}

        #endregion 检查页面权限信息

        //LogUtil.Write("OnActionExecuting");
    }

    /// <summary>
    /// Action 执行后
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnActionExecuted(ActionExecutedContext context)
    {
        // LogUtil.Write("OnActionExecuted");
    }
}