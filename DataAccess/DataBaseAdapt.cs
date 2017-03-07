using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    //private class DataBaseAdapt 
    //{
    //    public FactoryAdo dataBaseType = null;
    //    public string dbName { get; set; }
    //    public DataBaseAdapt()
    //    {
    //        var dataBase = Base.GetAppSetting<string>("DataBase");
    //        switch (dataBase)
    //        {
    //            case "SqlServer":
    //                dataBaseType = new SqlServer.AdoHelper(Base.GetAppSetting(dbName));
    //                break;
    //            case "MySql":
    //                dataBaseType = new MySql.AdoHelper(Base.GetAppSetting(dbName));
    //                break;
    //            default:
    //                dataBaseType = new SqlServer.AdoHelper(Base.GetAppSetting(dbName));
    //                break;
    //        }
    //    }
    //}

    //private interface FactoryAdo
    //{

    //    /// <summary>
    //    /// Executes a query that returns no results
    //    /// </summary>
    //    /// <param name="qry">Query text</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>The number of rows affected</returns>
    //    int ExecNonQuery(string qry, params object[] Args);

    //    /// <summary>
    //    /// Executes a stored Procedure that returns no results
    //    /// </summary>
    //    /// <param name="Proc">Name of stored Proceduret</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>The number of rows affected</returns>
    //    int ExecNonQueryProc(string Proc, params object[] Args);

    //    /// <summary>
    //    /// Executes a query that returns a single value
    //    /// </summary>
    //    /// <param name="qry">Query text</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>Value of first column and first row of the results</returns>
    //    object ExecScalar(string qry, params object[] Args);

    //    /// <summary>
    //    /// Executes a query that returns a single value
    //    /// </summary>
    //    /// <param name="Proc">Name of stored Proceduret</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>Value of first column and first row of the results</returns>
    //    object ExecScalarProc(string qry, params object[] Args);


    //    /// <summary>
    //    /// Executes a query and returns the results as a DataSet
    //    /// </summary>
    //    /// <param name="qry">Query text</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>Results as a DataSet</returns>
    //    DataSet ExecDataSet(string qry, params object[] Args);

    //    /// <summary>
    //    /// Executes a stored Procedure and returns the results as a Data Set
    //    /// </summary>
    //    /// <param name="Proc">Name of stored Proceduret</param>
    //    /// <param name="Args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
    //    /// <returns>Results as a DataSet</returns>
    //    DataSet ExecDataSetProc(string qry, params object[] Args);
    //}
}
