using Organizer.Models;

namespace Organizer
{
    public static class InitialArtificialData
    {
        public static void CreateArtificialDates()
        {
            Date date = new Date();
            date.Day = 1;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 5;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 8;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 12;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 15;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 18;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 29;
            date.Month = 1;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 7;
            date.Month = 2;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 14;
            date.Month = 2;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 18;
            date.Month = 2;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);

            date = new Date();
            date.Day = 27;
            date.Month = 2;
            date.Year = 2022;
            date = DataBase.AddDate(date);
            CreateArtificialNotes(date.Id);
        }

        public static void CreateArtificialNotes(int dateId)
        {
            Note note = new Note();
            note.Content = "Test note with nothing to communicate";
            note.DateForeignKey = dateId;
            note.SetStartHour("coś 10:00:00");
            note.SetEndHour("coś 11:00:00");
            note.Tag = "Something";
            DataBase.AddNote(note);

            note = new Note();
            note.Content = ".Net Platform classes";
            note.DateForeignKey = dateId;
            note.SetStartHour("coś 11:00:00");
            note.SetEndHour("coś 14:00:00");
            note.Tag = "Classes";
            DataBase.AddNote(note);

            note = new Note();
            note.Content = "Homework";
            note.DateForeignKey = dateId;
            note.SetStartHour("coś 15:00:00");
            note.SetEndHour("coś 16:00:00");
            note.Tag = "Homework";
            DataBase.AddNote(note);

            note = new Note();
            note.Content = "Champions League football match";
            note.DateForeignKey = dateId;
            note.SetStartHour("coś 17:00:00");
            note.SetEndHour("coś 18:30:00");
            note.Tag = "Hobby";
            DataBase.AddNote(note);
        }
    }
}
