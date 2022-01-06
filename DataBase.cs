using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer
{
    public static class DataBase
    {
        private static void DBUpdateAddRecord(string sURL, string sTitle)
        {
            sURL = sURL.Replace("'", "''");
            sTitle = sTitle.Replace("'", "''");
            string sSQL = "SELECT TOP 1 * FROM tbl_Details WHERE [URL] Like '" + sURL + "'";
            DataTable tbl = clsDB.Get_DataTable(sSQL);

            if (tbl.Rows.Count == 0)
            {
                string sql_Add = "INSERT INTO tbl_Details ([URL],[Title],[dtScan]) VALUES('" + sURL + "','" + sTitle + "',SYSDATETIME())";
                clsDB.ExecuteSQLQuery(sql_Add);
            }
            else
            {
                string ID = tbl.Rows[0]["IDDetail"].ToString();
                string sql_Update = "UPDATE tbl_Details SET [dtScan] = SYSDATETIME() WHERE IDDetail = " + ID;
                clsDB.ExecuteSQLQuery(sql_Update);
            }
        }

        public static List<Note> GetNotes(int dateId)
        {
            SqlConnection cn_connection = clsDB.Get_DB_Connection();
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }
            List<Note> notes = new List<Note>();

            using (cn_connection)
            {
                string oString = "Select * from Note where DateForeignKey=@dateId";
                SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                oCmd.Parameters.AddWithValue("@dateId", dateId);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Note note = new Note();
                        if (!reader.IsDBNull(reader.GetOrdinal("DateForeignKey")))
                        {
                            note.DateForeignKey = Convert.ToInt32(reader["DateForeignKey"] as int? ?? default(int));
                            note.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            note.SetStartHour(reader["StartHour"] as string);
                            note.SetEndHour(reader["EndHour"] as string);
                            note.Content = reader["Content"] as string;
                        }
                        notes.Add(note);
                    }
                    cn_connection.Close();
                }
            }
            return notes;
        }

        public static Date GetDate(int id)
        {
            SqlConnection cn_connection = clsDB.Get_DB_Connection();
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }
            Date date = new Date();
            using (cn_connection)
            {
                string oString = "Select * from Date where Id=@id";
                SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                oCmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        {
                            date.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            date.Day = Convert.ToInt32(reader["Day"] as int? ?? default(int));
                            date.Month = Convert.ToInt32(reader["Month"] as int? ?? default(int));
                            date.Year = Convert.ToInt32(reader["Year"] as int? ?? default(int));
                        }
                    }
                    cn_connection.Close();
                }
            }
            return date;
        }

        public static Date GetDate(int day, int month, int year)
        {
            SqlConnection cn_connection = clsDB.Get_DB_Connection();
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }
            Date date = new Date();
            using (cn_connection)
            {
                string oString = "SELECT * FROM Date WHERE Day=@day AND Month=@month AND Year=@year";
                SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                oCmd.Parameters.AddWithValue("@day",day);
                oCmd.Parameters.AddWithValue("@month", month);
                oCmd.Parameters.AddWithValue("@year", year);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        {
                            date.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            date.Day = Convert.ToInt32(reader["Day"] as int? ?? default(int));
                            date.Month = Convert.ToInt32(reader["Month"] as int? ?? default(int));
                            date.Year = Convert.ToInt32(reader["Year"] as int? ?? default(int));
                        }
                    }
                    cn_connection.Close();
                }
            }
            return date;
        }

        public static Dates GetAllDates()
        {
            SqlConnection cn_connection = clsDB.Get_DB_Connection();
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }
            List<Date> tempDates = new List<Date>();

            using (cn_connection)
            {
                string oString = "Select * from Date";
                SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Date date = new Date();
                        if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        {
                            date.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            date.Day = Convert.ToInt32(reader["Day"] as int? ?? default(int));
                            date.Month = Convert.ToInt32(reader["Month"] as int? ?? default(int));
                            date.Year = Convert.ToInt32(reader["Year"] as int? ?? default(int));
                        }
                        tempDates.Add(date);
                    }
                    cn_connection.Close();
                }
            }
            return new Dates(tempDates);
        }

        public static void AddDate(Date date)
        {
            if(date.Id==0)
            {
                clsDB.ExecuteSQLQuery("INSERT INTO Date ([Day],[Month],[Year]) VALUES ('"+date.Day+"' ,'"+date.Month+"', '"+date.Year+"')");
                date = GetDate(date.Day, date.Month, date.Year);
            }
            else
            {
                SqlConnection cn_connection = clsDB.Get_DB_Connection();
                if (cn_connection.State != ConnectionState.Open)
                {
                    cn_connection.Open();
                }

                using (cn_connection)
                {
                    string oString = "UPDATE Date SET Day=@day, Month=@month, Year=@year WHERE Id=@id";
                    SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                    oCmd.Parameters.AddWithValue("@id", date.Id);
                    oCmd.Parameters.AddWithValue("@day", date.Day);
                    oCmd.Parameters.AddWithValue("@month", date.Month);
                    oCmd.Parameters.AddWithValue("@year", date.Year);
                    oCmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddNote(int dayId, Note note)
        {

        }
    }
}
