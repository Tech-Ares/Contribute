using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace KUX.Entity.Generate
{
    public class DapperDbContext
    {
        public string ReadConnStr { get; set; }
        public string WriteConnStr { get; set; }
        public IDbTransaction Transaction { get; set; }
        public IDbConnection Connection { get; set; }

        public DapperDbContext()
        {
        }

        public DapperDbContext(string readConnStr, string writeConnStr)
        {
            this.ReadConnStr = readConnStr;
            this.WriteConnStr = writeConnStr;
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
            OpenConnection(true);

            var result = Connection.Execute(sql, param, Transaction, commandTimeout, commandType);

            CloseConnection();

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
            OpenConnection(true);
            var result = Connection.ExecuteScalar(sql, param, Transaction, commandTimeout, commandType);
            CloseConnection();
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
        public object ExecuteIdentity(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
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
            OpenConnection();
            var result = Connection.Query<T>(sql, param, Transaction, buffered, commandTimeout, commandType);

            CloseConnection();

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
        public T QueryFirst<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            OpenConnection();

            sql = sql.Replace(";", "") + " limit 0, 1;";

            var result = Query<T>(sql, param, buffered, commandTimeout, commandType);

            CloseConnection();

            return result.FirstOrDefault();
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
        public Tuple<IList<TFirst>, IList<TSecond>> QueryMultiple<TFirst, TSecond>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            OpenConnection();

            using (var multi = Connection.QueryMultiple(sql, param, Transaction, commandTimeout, commandType))
            {
                var first = multi.Read<TFirst>().ToList();
                var second = multi.Read<TSecond>().ToList();

                var result = new Tuple<IList<TFirst>, IList<TSecond>>(first, second);

                CloseConnection();

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
            OpenConnection();

            using (var multi = Connection.QueryMultiple(sql, param, Transaction, commandTimeout, commandType))
            {
                var first = multi.Read<TDbOBject>().ToList();
                var second = multi.Read<int>().ToList();

                var result = new Tuple<IList<TDbOBject>, int>(first, second.FirstOrDefault());

                CloseConnection();

                return result;
            }
        }

        public void OpenConnection(bool isWrite = false)
        {
            if (Transaction != null && Transaction.Connection.State == ConnectionState.Open)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                if (isWrite)
                {
                    Connection = new MySqlConnection(WriteConnStr);
                }
                else
                {
                    Connection = new MySqlConnection(ReadConnStr);
                }
            }
        }

        public void CloseConnection()
        {
            if (Transaction == null)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }

            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            Connection = new MySqlConnection(WriteConnStr);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
            return Transaction;
        }

        public void SetTransaction(IDbTransaction tran)
        {
            Transaction = tran;
        }

        public void Commit()
        {
            Transaction.Commit();
            this.Dispose();
        }

        public void RollBack()
        {
            Transaction.Rollback();
            this.Dispose();
        }

        /// <summary>
        /// 事务语句统一执行
        /// 通过事务环境执行分布式事务
        /// </summary>
        /// <param name="ac">委托</param>
        /// <returns></returns>
        public static bool TransactionExecute(Action ac)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    ac.Invoke();
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}