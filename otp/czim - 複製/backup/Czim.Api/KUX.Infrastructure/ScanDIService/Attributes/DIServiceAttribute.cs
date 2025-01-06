using KUX.Infrastructure.ScanDIService.Enums;
using System;

namespace KUX.Infrastructure.ScanDIService.Attributes;

/// <summary>
/// 服务标记 用于 程序 启动 扫描到后自动 注册服务
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class DIServiceAttribute : Attribute
{
    private readonly DIType _dIType;

    /// <summary>
    /// 忽略当前 对象 服务
    /// </summary>
    public bool IgnoreCurrent { get; set; } = false;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dIType"></param>
    public DIServiceAttribute(DIType dIType = DIType.Transient)
    {
        this._dIType = dIType;
    }

    public DIType GetAppServiceType()
    {
        return this._dIType;
    }
}