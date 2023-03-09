using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestMethod]
        public void DB_TEST()
        {
            //WD_Fuction.CleanInventoryData();
            //WD_Fuction.CleanOrdersData();
            //string xml = "05 aspen wd inventory bulk load.xml";
            //WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();

            //string sql = "SELECT CODE FROM EBR_USER";
            //List<List<string>> users = helper.Execute(sql);
            //List<string> IDs = users.Select(p => p[0]).ToList();
            //Console.WriteLine(IDs[0]);
            //string update = $"update EBR_WD_HU set EXPIRATION_DATE='2035-08-26 08:25:00.000' where HU_TAG = 'X0125002'";
            //helper.ExecuteNonQuery(update);
            //string str = "cd C:\\Program Files"; 



        }
    }
}
