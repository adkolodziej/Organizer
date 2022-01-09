using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Organizer
{
    public static class DataBase
    {
        public static Note GetNote(int dateForeignKey, TimeSpan startHour)
        {
            SqlConnection cn_connection = clsDB.Get_DB_Connection();
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }
            Note note = new Note();
            using (cn_connection)
            {
                string oString = "SELECT * FROM Note WHERE DateForeignKey=@dateForeignKey AND StartHour=@startHour";
                SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                oCmd.Parameters.AddWithValue("@dateForeignKey", dateForeignKey);
                oCmd.Parameters.AddWithValue("@startHour", startHour);
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        {
                            note.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            note.DateForeignKey = Convert.ToInt32(reader["DateForeignKey"] as int? ?? default(int));
                            note.SetStartHour(reader["StartHour"].ToString(), true);
                            note.SetEndHour(reader["EndHour"].ToString(), true);
                            note.Tag = Convert.ToString(reader["Tag"] as string);
                            note.Content = Convert.ToString(reader["Content"] as string);
                        }
                    }
                    cn_connection.Close();
                }
            }
            return note;
        }

        public static List<Note> GetNotesFromDate(int dateId)
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
                            note.Id = Convert.ToInt32(reader["Id"] as int? ?? default(int));
                            note.DateForeignKey = Convert.ToInt32(reader["DateForeignKey"] as int? ?? default(int));
                            note.SetStartHour(reader["StartHour"].ToString(), true);
                            note.SetEndHour(reader["EndHour"].ToString(), true);
                            note.Tag = Convert.ToString(reader["Tag"] as string);
                            note.Content = Convert.ToString(reader["Content"] as string);
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
                oCmd.Parameters.AddWithValue("@day", day);
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

        public static Date AddDate(Date date)
        {
            Date tempDate = GetDate(date.Day, date.Month, date.Year);
            if (tempDate.Id == 0)
            {
                clsDB.ExecuteSQLQuery("INSERT INTO Date ([Day],[Month],[Year]) VALUES ('" + date.Day + "' ,'" + date.Month + "', '" + date.Year + "')");
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
            date = GetDate(date.Day, date.Month, date.Year);
            return date;
        }

        public static Note AddNote(Note note)
        {
            Note tempNote = GetNote(note.DateForeignKey, note.StartHour);
            if (tempNote.Id == 0)
            {
                clsDB.ExecuteSQLQuery("INSERT INTO Note ([DateForeignKey],[StartHour],[EndHour],[Tag],[Content]) VALUES ('" + note.DateForeignKey + "' ,'" + note.StartHour + "', '" + note.EndHour + "','" + note.Tag + "' ,'" + note.Content + "')");
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
                    string oString = "UPDATE Note SET StartHour=@startHour, EndHour=@endHour, Tag=@tag, Content=@content WHERE Id=@id";
                    SqlCommand oCmd = new SqlCommand(oString, cn_connection);
                    oCmd.Parameters.AddWithValue("@id", note.Id);
                    oCmd.Parameters.AddWithValue("@startHour", note.StartHour);
                    oCmd.Parameters.AddWithValue("@endHour", note.EndHour);
                    oCmd.Parameters.AddWithValue("@tag", note.Tag);
                    oCmd.Parameters.AddWithValue("@content", note.Content);
                    oCmd.ExecuteNonQuery();
                }
            }
            note = GetNote(note.DateForeignKey, note.StartHour);
            return note;
        }
    }
}
