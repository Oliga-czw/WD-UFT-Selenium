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
            string xml = "05 aspen wd inventory bulk load.xml";
            string xml3 = "07 aspen wd orders bulk load.xml";
            string xml4 = "10 aspen wd signautres_106691 bulk load.xml";
            string xml5 = "14 aspen wd deviation_43325 bulk load.xml";
            string[] files = new string[] { xml4,xml5};
            //string xml2 ="02 aspen wd scales bulk load.xml";
            WD_Fuction.initial_data();
            //WD_Fuction.Bulkload(xml);
            //WD_Fuction.Bulkload(xml4);
            //WD_Fuction.Bulkload_Overwrite(xml2);
            //WD_Fuction.Bulkload(files);
            //WD_Fuction.WDSign();

            //string sql = "SELECT CODE FROM EBR_USER";
            //List<List<string>> users = helper.Execute(sql);
            //List<string> IDs = users.Select(p => p[0]).ToList();
            //Console.WriteLine(IDs[0]);
            //string update = $"update EBR_WD_HU set EXPIRATION_DATE='2035-08-26 08:25:00.000' where HU_TAG = 'X0125002'";
            //helper.ExecuteNonQuery(update);
            //string str = "cd C:\\Program Files"; 
            //string[] source =  { "booth1" };

            //var searchList = new List<string>() { "booth1", "Full", "qaone1(qaone1)", "Full Clean", "Clean" };
            //foreach (string searchTerm in searchList)
            //{

            //    var matchQuery = from word in source
            //                     where word.Equals(searchTerm)//, StringComparison.InvariantCultureIgnoreCase
            //                     select word;
            //    // Count the matches, which executes the query.  
            //    int wordCount = matchQuery.Count();
            //    Console.WriteLine(searchTerm + wordCount);
            //verify no.<tr> == ferquency(line+cerified) 
            //Base_Assert.AreEqual(row.Count, wordCount, searchTerm);

            //}


        }
    }
}
