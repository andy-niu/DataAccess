using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SqlServer
{
    internal class DapperContext : IDisposable
    {
        protected string _connString = null;
        protected internal SqlConnection _conn = null;
        protected bool _disposed = false;
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Constructor using global connection string.
        /// </summary>
        public DapperContext()
        {
            _connString = ConnectionString;
            Connect();
        }

        /// <summary>
        /// Constructure using connection string override
        /// </summary>
        /// <param name="connString">Connection string for this instance</param>
        public DapperContext(string connString)
        {
            _connString = connString;
            Connect();
        }

        // Creates a SqlConnection using the current connection string
        protected void Connect()
        {
            _conn = new SqlConnection(_connString);
            _conn.Open();
        }


        /// <summary>
        /// 执行增删改操作(包括批量操作)
        /// </summary>
        /// <param name="sql">sql语句(有参数参数化)</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public bool Execute(string sql, object param)
        {
            bool isSuccess = false;
            try
            {
                var result = _conn.Execute(sql, param);
                return result > 0 ? true : false;
            }
            catch (Exception ex) { Base.Logger(ex); }
            return isSuccess;
        }

        /// <summary>
        /// 执行存储过程操作
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="param">参数化值</param>
        /// <returns>返回存储过程是否执行成功</returns>
        public bool ExecuteStored(string proc, object param)
        {
            bool isSuccess = false;
            try
            {
                int result = _conn.Execute(proc, param, commandType: CommandType.StoredProcedure);
                isSuccess = result > 0 ? true : false;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        /// <summary>
        /// 执行存储过程操作
        /// </summary>
        /// <param name="storedName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>返回存储过程要返回的值</returns>
        public DynamicParameters ExecuteStored(string storedName, DynamicParameters param)
        {
            try
            {
                _conn.Execute(storedName, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { Base.Logger(ex); }
            return param;
        }

        /// <summary>
        /// 执行存储过程查询操作
        /// </summary>
        /// <typeparam name="T">返回集合的类型</typeparam>
        /// <param name="storedName">存储过程</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public IEnumerable<T> QueryStored<T>(string storedName, object param)
        {
            IEnumerable<T> _list = default(IEnumerable<T>);
            try
            {
                _list = _conn.Query<T>(storedName, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) { Base.Logger(ex); }
            return _list;
        }

        /// <summary>
        /// 查询操作返回默认第一条数据(如返回null则创建默认类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(string sql, object param)
        {
            var model = default(T);
            try
            {
                model = _conn.Query<T>(sql, param).FirstOrDefault();
            }
            catch (Exception ex) { Base.Logger(ex); }
            return model == null ? Activator.CreateInstance<T>() : model;
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <typeparam name="T">返回集合的类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param)
        {
            IEnumerable<T> _list = default(IEnumerable<T>);
            try
            {
                _list = _conn.Query<T>(sql, param);
            }
            catch (Exception ex) { Base.Logger(ex); }
            return _list;
        }
        /// <summary>
        /// 联表查询返回实体数据
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="func">类型转换方法</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, Func<SqlConnection, IEnumerable<T>> func)
        {
            IEnumerable<T> _list = default(IEnumerable<T>);
            try
            {
                _list = func(_conn);
            }
            catch (Exception ex) { Base.Logger(ex); }
            return _list;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Need to dispose managed resources if being called manually
                if (disposing)
                {
                    if (_conn != null)
                    {
                        //Rollback();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
