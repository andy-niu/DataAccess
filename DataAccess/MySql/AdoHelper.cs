using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess.MySql
{
    internal class AdoHelper : IDisposable
    {
        // Internal members
        protected string _connString = null;
        protected MySqlConnection _conn = null;
        protected MySqlTransaction _trans = null;
        protected bool _disposed = false;

        /// <summary>
        /// Sets or returns the connection string use by all instances of this class.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Returns the current SqlTransaction object or null if no transaction
        /// is in effect.
        /// </summary>
        public MySqlTransaction Transaction { get { return _trans; } }

        /// <summary>
        /// Constructor using global connection string.
        /// </summary>
        public AdoHelper()
        {
            _connString = ConnectionString;
            Connect();
        }

        /// <summary>
        /// Constructure using connection string override
        /// </summary>
        /// <param name="connString">Connection string for this instance</param>
        public AdoHelper(string connString)
        {
            _connString = connString;
            Connect();
        }

        // Creates a SqlConnection using the current connection string
        protected void Connect()
        {
            _conn = new MySqlConnection(_connString);
            _conn.Open();
        }

        /// <summary>
        /// Constructs a SqlCommand with the given parameters. This method is normally called
        /// from the other methods and not called directly. But here it is if you need access
        /// to it.
        /// </summary>
        /// <param name="query">SQL query or stored Procedure name</param>
        /// <param name="type">Type of SQL command</param>
        /// <param name="Args">Query arguments. Arguments should be in pairs where one is the
        /// name of the parameter and the second is the value. The very last argument can
        /// optionally be a SqlParameter object for specifying a custom argument type</param>
        /// <returns></returns>
        public MySqlCommand CreateCommand(string query, CommandType type, params object[] Args)
        {
            MySqlCommand cmd = new MySqlCommand(query, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;

            // Set command type
            cmd.CommandType = type;

            // Construct SQL parameters
            for (int i = 0; i < Args.Length; i++)
            {
                if (Args[i] is string && i < (Args.Length - 1))
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = (string)Args[i];
                    parm.Value = Args[++i];
                    cmd.Parameters.Add(parm);
                }
                else if (Args[i] is SqlParameter)
                {
                    cmd.Parameters.Add((SqlParameter)Args[i]);
                }
                else throw new ArgumentException("Invalid number or type of arguments supplied");
            }
            return cmd;
        }

        #region Exec Members

        /// <summary>
        /// Executes a query that returns no results
        /// </summary>
        /// <param name="query">Query text</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQuery(string query, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(query, CommandType.Text, Args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a stored Procedure that returns no results
        /// </summary>
        /// <param name="Proc">Name of stored Proceduret</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQueryProc(string Proc, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(Proc, CommandType.StoredProcedure, Args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="query">Query text</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalar(string query, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(query, CommandType.Text, Args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="query">Name of stored Proceduret</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalarProc(string query, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(query, CommandType.StoredProcedure, Args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="query">Query text</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public MySqlDataReader ExecDataReader(string query, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(query, CommandType.Text, Args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a stored Procedure and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="proc">Name of stored Proceduret</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public MySqlDataReader ExecDataReaderProc(string proc, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(proc, CommandType.StoredProcedure, Args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a DataSet
        /// </summary>
        /// <param name="query">Query text</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSet(string query, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(query, CommandType.Text, Args))
            {
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Executes a stored Procedure and returns the results as a Data Set
        /// </summary>
        /// <param name="proc">Name of stored Proceduret</param>
        /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSetProc(string proc, params object[] Args)
        {
            using (MySqlCommand cmd = CreateCommand(proc, CommandType.StoredProcedure, Args))
            {
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        #endregion

        #region Transaction Members

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>The new SqlTransaction object</returns>
        public MySqlTransaction BeginTransaction()
        {
            Rollback();
            _trans = _conn.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        /// Commits any transaction in effect.
        /// </summary>
        public void Commit()
        {
            if (_trans != null)
            {
                _trans.Commit();
                _trans = null;
            }
        }

        /// <summary>
        /// Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans = null;
            }
        }

        #endregion

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
                        Rollback();
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
