using StackExchange.Redis;

namespace KUX.Infrastructure.Redis;

/// <summary>
/// Redis 服务类
/// </summary>
public class RedisService : IRedisService
{
    ///// <summary>
    ///// get database
    ///// </summary>
    //public IDatabase Database
    //{
    //    get
    //    {
    //        return Multiplexer.GetDatabase(DefalutDataBase);
    //    }
    //    private set { }
    //}

    /// <summary>
    /// 获取redis db 链接
    /// </summary>
    /// <param name="db">链接db</param>
    /// <returns></returns>
    public IDatabase GetDatabase(int db)
    {
        if (db >= 0 && db <= 15)
        {
            return _multiplexer.GetDatabase(db);
        }

        return _multiplexer.GetDatabase(DefalutDataBase);
    }

    /// <summary>
    /// redis 服务
    /// </summary>
    public IServer iServer { get; }

    /// <summary>
    /// 多路复用器
    /// </summary>
    private IConnectionMultiplexer _multiplexer { get; }

    /// <summary>
    /// 多路复用器
    /// </summary>
    public IConnectionMultiplexer Multiplexer
    {
        get
        {
            return _multiplexer;
        }
        private set { }
    }

    /// <summary>
    /// 默认库
    /// </summary>
    private int DefalutDataBase { get; set; }

    ///// <summary>
    ///// 设置默认库
    ///// </summary>
    ///// <returns></returns>
    //public void SetDefaultDataBase(int db)
    //{
    //    this.DefalutDataBase = db;
    //}

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    public RedisService(string connectionString)
    {
        this._multiplexer = ConnectionMultiplexer.Connect(connectionString);

        this.DefalutDataBase = _multiplexer.GetDatabase().Database;

        //this.Database = Multiplexer.GetDatabase();

        var _defaultConn = "";

        var connectArray = connectionString.Split(',');

        if (connectArray.Length > 0)
        {
            foreach (var connect in connectArray)
            {
                if (connect.Contains(":") &&
                    connect.Split(".").Length == 4)
                {
                    _defaultConn = connect;

                    break;
                }
            }
        }

        this.iServer = _multiplexer.GetServer(_defaultConn);
    }

    /// <summary>
    /// 析构
    /// </summary>
    public void Dispose()
    {
        if (this.Multiplexer == null)
        {
            return;
        }
        this.Multiplexer.Close();
        this.Multiplexer.Dispose();
    }
}