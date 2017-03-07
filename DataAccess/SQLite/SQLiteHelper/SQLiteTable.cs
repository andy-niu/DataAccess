using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    /// <summary>
    /// SQLlite帮助类
    /// </summary>
    public class SQLiteTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName = "";
        /// <summary>
        /// Columns
        /// </summary>
        public SQLiteColumnList Columns = new SQLiteColumnList();

        /// <summary>
        /// 构造
        /// </summary>
        public SQLiteTable()
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">参数</param>
        public SQLiteTable(string name)
        {
            TableName = name;
        }
    }
}