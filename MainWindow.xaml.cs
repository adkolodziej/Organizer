﻿using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Organizer
{
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public Dates dates;
        private Date date;
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;

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
            chosenDay.Text = e.Day.Date.ToString();
            date = new Date();
            date.Day = e.Day.Date.Day;
            date.Month = e.Day.Date.Month;
            date.Year = e.Day.Date.Year;
            date = DataBase.AddDate(date);
            //save the text edits to persistant storage
        }

        private void SaveChangesButtonClick(object sender, RoutedEventArgs e)
        {
            Note note = new Note();
            note.DateForeignKey = date.Id;
            note.Content = NoteBox.Text;
            note.SetStartHour(StartHour.Value.ToString());
            note.SetEndHour(EndHour.Value.ToString());
            note=DataBase.AddNote(note);
        }
    }
}
