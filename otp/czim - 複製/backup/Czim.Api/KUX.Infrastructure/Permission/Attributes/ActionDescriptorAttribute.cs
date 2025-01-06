using System;

namespace KUX.Infrastructure.Permission.Attributes;

/// <summary>
/// action 功能模块描述
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ActionDescriptorAttribute : Attribute
{
    /// <summary>
    /// 功能名称
    /// </summary>
    private readonly string _functionName;

    /// <summary>
    /// 授权检查 默认检查
    /// </summary>
    public bool AuthCheck = true;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="functionName">方法名称</param>
    public ActionDescriptorAttribute(string functionName)
    {
        _functionName = functionName;
    }

    /// <summary>
    /// 获取功能名称
    /// </summary>
    /// <returns></returns>
    public string GetFunctionName() => this._functionName;
}