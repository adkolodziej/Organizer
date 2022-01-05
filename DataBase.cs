using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer
{
    class DataBase
    {
        private void DBUpdateAddRecord(string sURL, string sTitle)
        {
            sURL = sURL.Replace("'", "''");
            sTitle = sTitle.Replace("'", "''");
            string sSQL = "SELECT TOP 1 * FROM tbl_Details WHERE [URL] Like '" + sURL + "'";
            DataTable tbl = clsDB.Get_DataTable(sSQL);

            if (tbl.Rows.Count == 0)
            {
                string sql_Add = "INSERT INTO tbl_Details ([URL],[Title],[dtScan]) VALUES('" + sURL + "','" + sTitle + "',SYSDATETIME())";
                clsDB.Execute_SQL(sql_Add);
            }
            else
            {
                string ID = tbl.Rows[0]["IDDetail"].ToString();
                string sql_Update = "UPDATE tbl_Details SET [dtScan] = SYSDATETIME() WHERE IDDetail = " + ID;
                clsDB.Execute_SQL(sql_Update);
            }
        }
    }
}
