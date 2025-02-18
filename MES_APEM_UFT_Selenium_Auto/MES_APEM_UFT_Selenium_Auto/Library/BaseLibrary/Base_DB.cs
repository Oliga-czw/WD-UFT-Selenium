﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    public class SqlHelper
    {
        static string server = Environment.MachineName;
        //static string server = "ziru-2022-2.qae.aspentech.com";
        static string database = DBInfo.Info["AeBRS"];
        static string user = DBInfo.Info["username"];
        static string password = DBInfo.Info["password"];
        //private string ConStr = "Data Source = " + server + "; Database=" + database + "; User Id = " + user + "; Password = " + password;
        private string ConStr = $"Data Source ={server}; Database={database};User Id={user};Password={password}";
        public SqlConnection SqlConnnection()
        {
            var SQLConnection = new SqlConnection(ConStr);
            return SQLConnection;
        }
        private void ExecuteSQLQuery(string SQL, ref DataSet ds)
        {
            using (var mySQLconnection = this.SqlConnnection())
            {
                try
                {
                    mySQLconnection.Open();
                    var SQLcommand = mySQLconnection.CreateCommand();
                    SQLcommand.CommandType = CommandType.Text;
                    SQLcommand.CommandText = SQL;
                    var MMDMSQLDataAdapter = new SqlDataAdapter();
                    MMDMSQLDataAdapter.SelectCommand = SQLcommand;
                    MMDMSQLDataAdapter.Fill(ds);
                    mySQLconnection.Close();
                }
                catch (SqlException e)
                {
                    throw e;
                }


            }
        }
        private List<List<string>> DataSetToListString(DataSet ds)
        {
            List<List<string>> valuelist = new List<List<string>>();
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                List<string> temp = new List<string>();
                object[] rowIem = row.ItemArray;
                foreach (object item in rowIem)
                {
                    temp.Add(item.ToString());
                }
                valuelist.Add(temp);
            }
            return valuelist;
        }
        public List<List<string>> Execute(string SQL)
        {
            DataSet ds = new DataSet();
            ExecuteSQLQuery(SQL, ref ds);

            List<List<string>> valuelist = DataSetToListString(ds);

            return valuelist;
        }

        public void ExecuteNonQuery(string SQL)
        {
            using (var mySQLconnection = this.SqlConnnection())
            {
                try
                {
                    mySQLconnection.Open();
                    var SQLcommand = mySQLconnection.CreateCommand();
                    SQLcommand.CommandType = CommandType.Text;
                    SQLcommand.CommandText = SQL;
                    int r = SQLcommand.ExecuteNonQuery();
                    Console.WriteLine("({0} row affected)", r);
                    mySQLconnection.Close();
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }
        public void ExecuteNonQuery(string SQL,string database)
        {
            string ConStr = $"Data Source ={server}; Database={database};User Id={user};Password={password}";
            var SQLConnection = new SqlConnection(ConStr);
            using (var mySQLconnection = SQLConnection)
            {
                try
                {
                    mySQLconnection.Open();
                    var SQLcommand = mySQLconnection.CreateCommand();
                    SQLcommand.CommandType = CommandType.Text;
                    SQLcommand.CommandText = SQL;
                    int r = SQLcommand.ExecuteNonQuery();
                    Console.WriteLine("({0} row affected)", r);
                    mySQLconnection.Close();
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }
    }


}
