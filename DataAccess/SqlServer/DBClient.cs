using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.SqlServer
{
    /// <summary>
    /// SQlServer数据库操作
    /// </summary>
    public class DBClient
    {
        /// <summary>
        /// 查询返回Datatable结果集
        /// </summary>
        /// <param name="qry">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual DataTable GetData(string qry, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSet(qry, args).Tables[0];
            }
        }
        /// <summary>
        /// 查询返回一个DataSet结果集
        /// </summary>
        /// <param name="qry">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string qry, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSet(qry, args);
            }
        }
        /// <summary>
        /// 查询返回一个Object结果集
        /// </summary>
        /// <param name="qry">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual object Scalar(string qry, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecScalar(qry, args);
            }
        }
        /// <summary>
        /// 执行SQL语句返回bool类型结果
        /// </summary>
        /// <param name="qry">查询语句</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual bool Execute(string qry, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecNonQuery(qry, args) > 0 ? true : false;
            }
        }

        /// <summary>
        /// 大数据复制
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="dbName"></param>
        public virtual void ExecSqlBulkCopy(DataTable dt,string tableName, string dbName)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                ado.ExecSqlBulkCopy(dt, tableName);
            }
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="qry">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public virtual bool Execute(string qry, string dbName, object model)
        {
            using (var dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                return dapper.Execute(qry, model);
            }
        }

        /// <summary>
        /// 查询返回列表实体数据
        /// </summary>
        /// <param name="qry">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string qry, string dbName, object model)
        {
            using (var dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                IEnumerable<T> _list = default(IEnumerable<T>);
                _list = dapper.Query<T>(qry, model);
                return _list;
            }
        }

        /// <summary>
        /// 2表联查返回数据
        /// </summary>
        /// <param name="qry">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="func">多表连接Func</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string qry, string dbName, Func<MySqlConnection, IEnumerable<T>> func)
        {
            using (var dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                IEnumerable<T> _list = default(IEnumerable<T>);
                _list = dapper.Query<T>(qry, func);
                return _list;
            }
        }

    }
}
