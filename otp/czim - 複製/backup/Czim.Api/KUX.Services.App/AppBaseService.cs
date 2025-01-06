using KUX.Infrastructure.Services;

namespace KUX.Services.App
{
    /// <summary>
    /// App接口基础服务
    /// </summary>
    /// <typeparam name="TRepository">泛型仓储</typeparam>
    public class AppBaseService<TRepository> : FrameworkBaseService<TRepository> where TRepository : class
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository"></param>
        public AppBaseService(TRepository repository) : base(repository)
        {
        }
    }
}