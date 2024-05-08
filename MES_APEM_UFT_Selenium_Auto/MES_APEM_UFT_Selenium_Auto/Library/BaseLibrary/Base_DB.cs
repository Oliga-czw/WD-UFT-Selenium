using System;
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
        public void ExecuteSQLQuery(string SQL, ref DataSet ds)
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
        public SqlDataReader ExecuteReader(string SQL)
        {
            using (var mySQLconnection = this.SqlConnnection())
            {
                try
                {
                    mySQLconnection.Open();
                    var SQLcommand = mySQLconnection.CreateCommand();
                    SQLcommand.CommandType = CommandType.Text;
                    SQLcommand.CommandText = SQL;
                    SqlDataReader reader = SQLcommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // 假设结果集有两列：Lowest Active Transaction 和 Oldest Active Transaction  
                            Console.WriteLine("Lowest Active Transaction: " + reader[0].ToString());
                            Console.WriteLine("Oldest Active Transaction: " + reader[1].ToString());
                            // 注意：实际的列名和顺序可能因 SQL Server 版本和配置而异  
                        }
                    }
                    else
                    {
                        Console.WriteLine("No active transactions found.");
                    }

                    mySQLconnection.Close();
                    return reader;
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
