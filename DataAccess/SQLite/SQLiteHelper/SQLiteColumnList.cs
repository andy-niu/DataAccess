using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    /// <summary>
    /// SQLiteColumnList
    /// </summary>
    public class SQLiteColumnList : IList<SQLiteColumn>
    {
        List<SQLiteColumn> _lst = new List<SQLiteColumn>();

        private void CheckColumnName(string colName)
        {
            for (int i = 0; i < _lst.Count; i++)
            {
                if (_lst[i].ColumnName == colName)
                    throw new Exception("Column name of \"" + colName + "\" is already existed.");
            }
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(SQLiteColumn item)
        {
            return _lst.IndexOf(item);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, SQLiteColumn item)
        {
            CheckColumnName(item.ColumnName);

            _lst.Insert(index, item);
        }
        /// <summary>
        /// RemoveAt
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _lst.RemoveAt(index);
        }
        /// <summary>
        /// SQLiteColumn
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SQLiteColumn this[int index]
        {
            get
            {
                return _lst[index];
            }
            set
            {
                if (_lst[index].ColumnName != value.ColumnName)
                {
                    CheckColumnName(value.ColumnName);
                }

                _lst[index] = value;
            }
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="item"></param>
        public void Add(SQLiteColumn item)
        {
            CheckColumnName(item.ColumnName);

            _lst.Add(item);
        }
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _lst.Clear();
        }
        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(SQLiteColumn item)
        {
            return _lst.Contains(item);
        }
        /// <summary>
        /// CopyTo
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(SQLiteColumn[] array, int arrayIndex)
        {
            _lst.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Count
        /// </summary>
        public int Count
        {
            get { return _lst.Count; }
        }
        /// <summary>
        /// IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(SQLiteColumn item)
        {
            return _lst.Remove(item);
        }
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<SQLiteColumn> GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
    }

}
