using KUX.Infrastructure.ScanDIService.Interface;

namespace KUX.Infrastructure.Services;

/// <summary>
/// 服务层 基类
/// </summary>
/// <typeparam name="TRepository">默认仓储 类型</typeparam>
public class FrameworkBaseService<TRepository> : IDITransientSelf where TRepository : class
{
    /// <summary>
    /// 默认 仓储
    /// </summary>
    protected readonly TRepository Repository;

    /// <summary>
    ///  构造
    /// </summary>
    /// <param name="repository">泛型仓储</param>
    public FrameworkBaseService(TRepository repository)
    {
        Repository = repository;
    }
}