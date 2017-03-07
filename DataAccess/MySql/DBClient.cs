using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.MySql
{
    /// <summary>
    /// MySql数据库操作
    /// </summary>
    public class DBClient
    {
        /// <summary>
        /// 查询返回Datatable结果集
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual DataTable GetData(string query, string dbName, params object[] args)
        {
            using (AdoHelper ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSet(query, args).Tables[0];
            }
        }
        /// <summary>
        /// 查询返回一个DataSet结果集
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string query, string dbName, params object[] args)
        {
            using (AdoHelper ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSet(query, args);
            }
        }
        /// <summary>
        /// 查询返回一个Object结果集
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual object Scalar(string query, string dbName, params object[] args)
        {
            using (AdoHelper ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecScalar(query, args);
            }
        }
        /// <summary>
        /// 执行SQL语句返回bool类型结果
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual bool Execute(string query, string dbName, params object[] args)
        {
            using (AdoHelper ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecNonQuery(query, args) > 0 ? true : false;
            }
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="query">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public virtual bool Execute(string query, string dbName, object model)
        {
            using(DapperContext dapper=new DapperContext(Base.GetAppSetting(dbName)))
            {
                return dapper.Execute(query, model);
            }
        }

        /// <summary>
        /// 查询返回列表实体数据
        /// </summary>
        /// <param name="query">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string query, string dbName, object model)
        {
            using (DapperContext dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                IEnumerable<T> _list = default(IEnumerable<T>);
                _list = dapper.Query<T>(query, model);
                return _list;
            }
        }

        /// <summary>
        /// 2表联查返回数据
        /// </summary>
        /// <param name="query">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="func">多表连接Func</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string query, string dbName, Func<MySqlConnection, IEnumerable<T>> func)
        {
            using (DapperContext dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                IEnumerable<T> _list = default(IEnumerable<T>);
                _list = dapper.Query<T>(query, func);
                return _list;
            }
        }
    }
}
