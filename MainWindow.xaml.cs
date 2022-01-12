using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Organizer
{
    public partial class MainWindow : Window
    {
        private enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public Dates dates;

        private Date date;
        private List<string> hours;
        private List<Note> currentDateNotes;
        public MainWindow()
        {
            InitializeComponent();
            dates = DataBase.GetAllDates();
            dates.FindAllNotes();
            List<string> months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            cboMonth.ItemsSource = months;

            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }


            cboMonth.SelectedItem = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
            cboYear.SelectedItem = DateTime.Today.Year;
            cboMonth.SelectedIndex = DateTime.Today.Month - 1;

            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();
        }

        private void RefreshCalendar()
        {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;

            int month = cboMonth.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);

            myCalendar.BuildCalendar(targetDate);
        }

        public void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            chosenDay.Text = $"{e.Day.Date.Day} {NumberToMonth(e.Day.Date.Month)} {e.Day.Date.Year}"; //e.Day.Date.ToString();
            date = new Date();
            date.Day = e.Day.Date.Day;
            date.Month = e.Day.Date.Month;
            date.Year = e.Day.Date.Year;
            date = DataBase.AddDate(date);

            currentDateNotes = dates.ListOfDates.Find(x => x.CompareDates(date)).Notes;
            hours = dates.ListOfDates.Find(x => x.CompareDates(date)).GetNotesHours();
            NotesList.ItemsSource = hours;


            //save the text edits to persistant storage
        }

        private void SaveChangesButtonClick(object sender, RoutedEventArgs e)
        {
            Note note = new Note();
            note.DateForeignKey = date.Id;
            note.Content = NoteBox.Text;
            note.SetStartHour(StartHour.Value.ToString());
            note.SetEndHour(EndHour.Value.ToString());
            note = DataBase.AddNote(note);
        }

        private void ComboBoxItem_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            item.IsSelected = true;
            NotesList.IsDropDownOpen = false;
            SetNoteToView(item.Content.ToString());
        }

        //private void NotesList_DropDownClosed(object sender, EventArgs e)
        //{
        //    foreach (var item in hours)
        //    {
        //        if(item == NotesList.SelectedItem.ToString())
        //        {
        //            MessageBox.Show(item);
        //        }
        //    }
        //}
        private void SetNoteToView(string hour)
        {
            int index = hours.FindIndex(x => x == hour);
            Note note = currentDateNotes[index];
            StartHour.Value = note.GetStartHourAsDateTime();
            EndHour.Value = note.GetEndHourAsDateTime();
            NoteBox.Text = note.Content;
        }
        private string NumberToMonth(int number)
        {
            Month month = (Month)number;
           
            return month switch
            {
                Month.January => "January",
                Month.February => "February",
                Month.March => "March",
                Month.April => "April",
                Month.May => "May",
                Month.June => "June",
                Month.July => "July",
                Month.August => "August",
                Month.September => "September",
                Month.October => "October",
                Month.November => "November",
                Month.December => "December",
                _ => throw new NotImplementedException()
            };
        }
    }
}
