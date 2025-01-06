using KUX.Infrastructure.Permission.Attributes;
using KUX.Models.ApiResultManage;
using KUX.Services.App.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Czim.AdminApi.Filters;

/// <summary>
/// app 管理 控制
/// </summary>
public class ApiAuthorizationActionFilter : IActionFilter
{
    /// <summary>
    /// 会员授权服务
    /// </summary>
    private readonly AuthorMemberService authorMemberService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="_authorMemberService">会员服务</param>
    public ApiAuthorizationActionFilter(
        AuthorMemberService _authorMemberService)
    {
        authorMemberService = _authorMemberService;
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

        #region 判断控制器分类

        // 获取控制器
        var controller = (ControllerBase)context.Controller;
        //获取 controller 上面所有的 自定义 特性
        var customAttributes = controller.GetType().GetCustomAttributes();

        #endregion 判断控制器分类

        var routeValues = context.ActionDescriptor.RouteValues;
        //var areaName = routeValues["area"];
        var controllerName = routeValues["controller"];
        var actionName = routeValues["action"];

        var httpContext = context.HttpContext;
        const string unAuthMessage = "未授权,请先登录授权!";

        // 走app授权验证
        if (this.authorMemberService.GetMemberInfo() == null)
        {
            var data = ApiResult.ResultMessage(ApiResultCodeEnum.UnAuth, unAuthMessage);
            context.Result = new JsonResult(data);
        }
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