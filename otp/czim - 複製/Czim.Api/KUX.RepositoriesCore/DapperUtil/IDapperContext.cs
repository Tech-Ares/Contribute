using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KUX.Repositories.Core.DapperUtil
{
    /// <summary>
    /// Dapper 连接上下文
    /// </summary>
    public interface IDapperContext
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        IDbConnection TransContext();

        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>Number of rows affected</returns>
        int Execute(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>Number of rows affected</returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
             int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>The first cell selected</returns>
        object ExecuteScalar(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute parameterized SQL, returning the new inserted identity
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        object MysqlExecuteIdentity(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="buffered">Buffered</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Executes a single-row query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        T MysqlQueryFirst<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Executes a single-row query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// </summary>
        /// <typeparam name="TFirst">First Result</typeparam>
        /// <typeparam name="TSecond">Second Resutl</typeparam>
        /// <param name="sql">Sql Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        Tuple<IList<TFirst>, IList<TSecond>> QueryMultiple<TFirst, TSecond>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// Async
        /// </summary>
        /// <typeparam name="TFirst">First Result</typeparam>
        /// <typeparam name="TSecond">Second Resutl</typeparam>
        /// <param name="sql">Sql Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        Task<Tuple<IList<TFirst>, IList<TSecond>>> QueryMultipleAsync<TFirst, TSecond>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>> QueryMultiple<TFirst, TSecond, TThird>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// Async
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>>> QueryMultipleAsync<TFirst, TSecond, TThird>(string sql, object param = null,
             int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// Async
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFour"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>, IList<TFour>>> QueryMultipleAsync<TFirst, TSecond, TThird, TFour>(string sql, object param = null,
             int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDbOBject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Tuple<IList<TDbOBject>, int> QueryPaged<TDbOBject>(string sql, object param = null, int? commandTimeout = default(int?),
           CommandType? commandType = default(CommandType?));
    }
}