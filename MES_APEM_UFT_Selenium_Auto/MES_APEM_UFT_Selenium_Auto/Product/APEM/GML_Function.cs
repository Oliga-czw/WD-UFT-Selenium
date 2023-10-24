using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    class GML_Function
    {
        public static void RestoreDataBase()
        {
            string database = "master";
            string sourceName1 = Base_Directory.ProjectDir + "Data\\Input\\GML_DATABASE\\aebrs.BAK'";
            string sourceName2 = Base_Directory.ProjectDir + "Data\\Input\\GML_DATABASE\\APEMMMDMMVT.BAK'";
            SqlHelper helper = new SqlHelper();
            string SQL1 = $" ALTER DATABASE AeBRS SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE AeBRS";
            string SQL2 = $"RESTORE DATABASE AeBRS FROM DISK = '" + sourceName1;
            string SQL3 = $"RESTORE DATABASE APEMMMDMMVT FROM DISK = '" + sourceName2;
            helper.ExecuteNonQuery(SQL1, database);
            helper.ExecuteNonQuery(SQL2, database);
            helper.ExecuteNonQuery(SQL3, database);
        }
        public static void RestoreDBData()
        {
            string database = "master";
            SqlHelper helper = new SqlHelper();
            string SQL1 = @"DROP LOGIN  AEBRS";
            string SQL2 = @"use master
                            EXEC sp_addlogin  'AEBRS', 'Aspen000', 'AEBRS'
                            EXEC sp_addrolemember 'db_datawriter', 'AEBRS'
                            EXEC sp_addrolemember 'db_datareader', 'AEBRS'
                            EXEC sp_addrolemember 'db_owner', 'AEBRS'";
            string SQL3 = @"use aebrs
                            exec sp_change_users_login 'update_one', 'aebrs', 'aebrs'";
            helper.ExecuteNonQuery(SQL1, database);
            helper.ExecuteNonQuery(SQL2, database);
            helper.ExecuteNonQuery(SQL3);
        }

    }
}
