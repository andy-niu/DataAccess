using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SqlServer
{
    /// <summary>
    /// SQlServer操作
    /// </summary>
    public class DBClientPro : DBClient
    {
        /// <summary>
        /// 查询返回Datatable结果集
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public override DataTable GetData(string proc, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSetProc(proc, args).Tables[0];
            }
        }
        /// <summary>
        /// 查询返回一个DataSet结果集
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public override DataSet GetDataSet(string proc, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecDataSetProc(proc, args);
            }
        }
        /// <summary>
        /// 查询返回一个Object结果集
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public override object Scalar(string proc, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecScalarProc(proc, args);
            }
        }
        /// <summary>
        /// 执行SQL语句返回bool类型结果
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public override bool Execute(string proc, string dbName, params object[] args)
        {
            using (var ado = new AdoHelper(Base.GetAppSetting(dbName)))
            {
                return ado.ExecNonQueryProc(proc, args) > 0 ? true : false;
            }
        }


        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="proc">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public override bool Execute(string proc, string dbName, object model)
        {
            using (var dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                return dapper.ExecuteStored(proc, model);
            }
        }

        /// <summary>
        /// 查询返回列表实体数据
        /// </summary>
        /// <param name="proc">sql</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="model">实体（支持动态类 例：new { }）</param>
        /// <returns></returns>
        public override IEnumerable<T> Query<T>(string proc, string dbName, object model)
        {
            using (var dapper = new DapperContext(Base.GetAppSetting(dbName)))
            {
                IEnumerable<T> _list = default(IEnumerable<T>);
                _list = dapper.QueryStored<T>(proc, model);
                return _list;
            }
        }
    }
}
