using Organizer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace Organizer
{
    public class Calendar : Control
    {
        public Dates dates;
        public ObservableCollection<CalendarDay> Days { get; set; }
        public ObservableCollection<string> DayNames { get; set; }
        public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(Calendar));

        public event EventHandler<DayChangedEventArgs> DayChanged;

        public DateTime CurrentDate
        {
            get { return (DateTime)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        static Calendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));
        }

        public Calendar()
        {
            DataContext = this;
            CurrentDate = DateTime.Today;

            DayNames = new ObservableCollection<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            Days = new ObservableCollection<CalendarDay>();
            BuildCalendar(DateTime.Today);
        }

        public void BuildCalendar(DateTime targetDate)
        {
            Days.Clear();
            InitialArtificialData.CreateArtificialDates();
            dates = DataBase.GetAllDates();
            dates.FindAllNotes();
            //Calculate when the first day of the month is and work out an 
            //offset so we can fill in any boxes before that.
            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);
            int offset = DayOfWeekNumber(d.DayOfWeek) - 1;
            if (offset != 1) d = d.AddDays(-offset);

            //Show 6 weeks each with 7 days = 42
            for (int box = 1; box <= 42; box++)
            {
                CalendarDay day = new CalendarDay { Date = d, Enabled = true, IsTargetMonth = targetDate.Month == d.Month };
                day.PropertyChanged += Day_Changed;
                day.ButtonClicked += OnDayChosen;
                day.IsToday = d == DateTime.Today;
                Date date = dates.ListOfDates.Find(x => x.Day == day.Date.Day && x.Month == day.Date.Month && x.Year == day.Date.Year && x.Notes != null && x.Notes.Count > 0);
                if (date != null)
                {
                    day.Notes = "Notes";
                }
                Days.Add(day);
                d = d.AddDays(1);
            }
        }

        private void Day_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Notes") return;
            if (DayChanged == null) return;

            DayChanged(this, new DayChangedEventArgs(sender as CalendarDay));
        }

        private void OnDayChosen(object sender, EventArgs e)
        {
            if (DayChanged == null) return;

            DayChanged(this, new DayChangedEventArgs(sender as CalendarDay));
        }

        private static int DayOfWeekNumber(DayOfWeek dow)
        {
            return Convert.ToInt32(dow.ToString("D"));
        }
    }

    public class DayChangedEventArgs : EventArgs
    {
        public CalendarDay Day { get; private set; }

        public DayChangedEventArgs(CalendarDay day)
        {
            this.Day = day;
        }
    }
}