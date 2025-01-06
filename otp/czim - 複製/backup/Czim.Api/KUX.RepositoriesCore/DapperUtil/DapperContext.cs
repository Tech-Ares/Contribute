using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KUX.Repositories.Core.DapperUtil
{
    /// <summary>
    /// Dapper sql上下文
    /// </summary>
    public class DapperContext : IDapperContext
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private IDbConnection _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public DapperContext(IDbConnection db)
        {
            _db = db;
        }

        /// <summary>
        /// 连接上下文
        /// </summary>
        public virtual IDbConnection Context
        {
            get
            {
                if (_db.State != ConnectionState.Open)
                {
                    _db.Open();
                }

                return _db;
            }
        }

        /// <summary>
        /// 获取事务连接对象
        /// </summary>
        /// <returns></returns>
        public IDbConnection TransContext()
        {
            if (_db.State != ConnectionState.Open)
            {
                _db.Open();
            }
            return _db;
        }

        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>Number of rows affected</returns>
        public int Execute(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var result = Context.Execute(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="transaction">transaction</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>Number of rows affected</returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var result = await Context.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);

            return result;
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>The first cell selected</returns>
        public object ExecuteScalar(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var result = Context.ExecuteScalar(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// Execute parameterized SQL, returning the new inserted identity
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        public object MysqlExecuteIdentity(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            sql = sql.TrimEnd();
            if (sql.Substring(sql.Length - 1, 1) == ";")
            {
                sql += " select @@IDENTITY;";
            }
            else
            {
                sql += "; select @@IDENTITY;";
            }
            var result = ExecuteScalar(sql, param, commandTimeout, commandType);
            return result;
        }

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
        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var result = Context.Query<T>(sql, param, null, buffered, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql">SQL Statement</param>
        /// <param name="param">Parameters</param>
        /// <param name="commandTimeout">Command Timeout</param>
        /// <param name="commandType">Command Type</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var result = await Context.QueryAsync<T>(sql, param, null, commandTimeout, commandType);

            return result;
        }

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
        public T MysqlQueryFirst<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            var result = Query<T>(sql, param, buffered, commandTimeout, commandType);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">Data typed as per T</typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            return await Context.QueryFirstOrDefaultAsync<T>(sql, param, null, commandTimeout, commandType);
            //return Task.FromResult(result);
        }

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
        public Tuple<IList<TFirst>, IList<TSecond>> QueryMultiple<TFirst, TSecond>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var multi = Context.QueryMultiple(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();

                var result = new Tuple<IList<TFirst>, IList<TSecond>>(first, second);

                return result;
            }
        }

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
        public async Task<Tuple<IList<TFirst>, IList<TSecond>>> QueryMultipleAsync<TFirst, TSecond>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var multi = await Context.QueryMultipleAsync(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();

                var result = new Tuple<IList<TFirst>, IList<TSecond>>(first, second);

                return result;
            }
        }

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
        public Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>> QueryMultiple<TFirst, TSecond, TThird>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var multi = Context.QueryMultiple(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();
                var third = multi.Read<TThird>().ToList();

                var result = new Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>>(first, second, third);

                return result;
            }
        }

        /// <summary>
        /// Execute a command that returns two result sets, and access each in tuple
        /// for async
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>>> QueryMultipleAsync<TFirst, TSecond, TThird>(string sql, object param = null,
            int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var multi = await Context.QueryMultipleAsync(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();
                var third = multi.Read<TThird>().ToList();

                var result = new Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>>(first, second, third);

                return result;
            }
        }

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
        public async Task<Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>, IList<TFour>>> QueryMultipleAsync<TFirst, TSecond, TThird, TFour>(string sql, object param = null,
             int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var multi = await Context.QueryMultipleAsync(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();
                var third = multi.Read<TThird>().ToList();
                var four = multi.Read<TFour>().ToList();
                var result = new Tuple<IList<TFirst>, IList<TSecond>, IList<TThird>, IList<TFour>>(first, second, third, four);

                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDbOBject"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Tuple<IList<TDbOBject>, int> QueryPaged<TDbOBject>(string sql, object param = null, int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            using (var multi = Context.QueryMultiple(sql, param, null, commandTimeout, commandType))
            {
                var first = multi.Read<TDbOBject>().ToList();
                var second = multi.Read<int>().ToList();

                var result = new Tuple<IList<TDbOBject>, int>(first, second.FirstOrDefault());
                return result;
            }
        }
    }
}