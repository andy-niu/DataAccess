using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    /// <summary>
    /// SQLite Column
    /// </summary>
    public class SQLiteColumn
    {
        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName = "";
        /// <summary>
        /// Primary Key
        /// </summary>
        public bool PrimaryKey = false;
        /// <summary>
        /// Column Data Type
        /// </summary>
        public ColType ColDataType = ColType.Text;
        /// <summary>
        /// Auto Increment
        /// </summary>
        public bool AutoIncrement = false;
        /// <summary>
        /// Not Null
        /// </summary>
        public bool NotNull = false;
        /// <summary>
        /// Default Value
        /// </summary>
        public string DefaultValue = "";

        /// <summary>
        /// 构造
        /// </summary>
        public SQLiteColumn(){ }

        /// <summary>
        /// 有参数构造
        /// </summary>
        /// <param name="colName"></param>
        public SQLiteColumn(string colName)
        {
            ColumnName = colName;
            PrimaryKey = false;
            ColDataType = ColType.Text;
            AutoIncrement = false;
        }
        /// <summary>
        /// 有参数构造
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="colDataType"></param>
        public SQLiteColumn(string colName, ColType colDataType)
        {
            ColumnName = colName;
            PrimaryKey = false;
            ColDataType = colDataType;
            AutoIncrement = false;
        }
        /// <summary>
        /// 有参数构造
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="autoIncrement"></param>
        public SQLiteColumn(string colName, bool autoIncrement)
        {
            ColumnName = colName;

            if (autoIncrement)
            {
                PrimaryKey = true;
                ColDataType = ColType.Integer;
                AutoIncrement = true;
            }
            else
            {
                PrimaryKey = false;
                ColDataType = ColType.Text;
                AutoIncrement = false;
            }
        }
        /// <summary>
        /// 有参数构造
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="colDataType"></param>
        /// <param name="primaryKey"></param>
        /// <param name="autoIncrement"></param>
        /// <param name="notNull"></param>
        /// <param name="defaultValue"></param>
        public SQLiteColumn(string colName, ColType colDataType, bool primaryKey, bool autoIncrement, bool notNull, string defaultValue)
        {
            ColumnName = colName;

            if (autoIncrement)
            {
                PrimaryKey = true;
                ColDataType = ColType.Integer;
                AutoIncrement = true;
            }
            else
            {
                PrimaryKey = primaryKey;
                ColDataType = colDataType;
                AutoIncrement = false;
                NotNull = notNull;
                DefaultValue = defaultValue;
            }
        }
    }
}
