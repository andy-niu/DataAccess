
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SQLite
{
    /// <summary>
    /// SQLite帮助类
    /// </summary>
    public class DBClient
    {
        /// <summary>
        /// 获取datatable数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public static DataTable GetTable(string query, Dictionary<string, object> dic = null)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    return sh.Select(query, dic);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="dic_data">更新数据</param>
        /// <param name="dic_where">条件</param>
        public static void Update(string tableName,Dictionary<string, object> dic_data,Dictionary<string, object> dic_where)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                { 
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Update(tableName, dic_data, dic_where);
                }
            }
        }
        /// <summary>
        /// 更具条件更新
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="dic_data">更新数据</param>
        /// <param name="field">条件字段</param>
        /// <param name="value">条件值</param>
        public static void Update(string tableName, Dictionary<string, object> dic_data, string field, object value)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Update(tableName, dic_data, field, value);
                }
            }
        }
        /// <summary>
        /// 添加返回ID
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dic_data"></param>
        /// <returns></returns>
        public static int Add(string tableName, Dictionary<string, object> dic_data)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Insert(tableName, dic_data);
                    return (int)sh.LastInsertRowId();
                }
            }
        }
        /// <summary>
        /// 执行获取语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dic_data"></param>
        /// <returns></returns>
        public static int Execute(string tableName, Dictionary<string, object> dic_data = null)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    sh.Execute(tableName, dic_data);
                    return (int)sh.LastInsertRowId();
                }
            }
        }
        /// <summary>
        /// 获取Scalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dicParameters"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string query, Dictionary<string, object> dicParameters = null)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    var result = sh.ExecuteScalar(query, dicParameters);
                    return (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
                }
            }
        } 
        /// <summary>
        /// 获取所有列
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetTableColumns(string tableName){
            using (SQLiteConnection conn = new SQLiteConnection(Base.GetAppSetting("SQLite")))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    return sh.GetColumnStatus(tableName);
                }
            }
        }
    }
}
