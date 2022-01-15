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

		private event Action OnDataBaseDataChange;

		public Dates dates;

		private Date currentlyChosenDate;
		private List<string> hours;
		private List<Note> currentDateNotes;
		private Note currentlyChosenNote;
		public MainWindow()
		{
			InitializeComponent();
			FindNotes();
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
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			OnDataBaseDataChange += RefreshCalendar;
			OnDataBaseDataChange += FindNotes;
		}

		private void FindNotes()
		{
			dates = DataBase.GetAllDates();
			dates.FindAllNotes();
			if (currentlyChosenDate != null)
			{
				currentDateNotes = dates.ListOfDates.Find(x => x.CompareDates(currentlyChosenDate))?.Notes;
				hours = dates?.ListOfDates.Find(x => x.CompareDates(currentlyChosenDate))?.GetNotesHours();
				NotesList.ItemsSource = hours;
			}
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
			currentlyChosenDate = new Date();
			currentlyChosenDate.Day = e.Day.Date.Day;
			currentlyChosenDate.Month = e.Day.Date.Month;
			currentlyChosenDate.Year = e.Day.Date.Year;
			currentlyChosenDate = DataBase.AddDate(currentlyChosenDate);

			ClearNote();

			currentDateNotes = dates.ListOfDates.Find(x => x.CompareDates(currentlyChosenDate))?.Notes;
			hours = dates?.ListOfDates.Find(x => x.CompareDates(currentlyChosenDate))?.GetNotesHours();
			NotesList.ItemsSource = hours;
		}

		private void SaveChangesButtonClick(object sender, RoutedEventArgs e)
		{
			SaveNote(currentlyChosenNote);
		}

		private void ComboBoxItem_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ComboBoxItem item = sender as ComboBoxItem;
			item.IsSelected = true;
			NotesList.IsDropDownOpen = false;
			SetNoteToView(item.Content.ToString());
		}

		private void SetNoteToView(string hour)
		{
			int index = hours.FindIndex(x => x == hour);
			currentlyChosenNote = currentDateNotes[index];
			StartHour.Value = currentlyChosenNote.GetStartHourAsDateTime();
			EndHour.Value = currentlyChosenNote.GetEndHourAsDateTime();
			NoteBox.Text = currentlyChosenNote.Content;
			TagBox.Text = currentlyChosenNote.Tag;
		}

		private void SaveNote(Note note)
		{
			if (note != null || currentlyChosenDate == null)
			{
				note.DateForeignKey = currentlyChosenDate.Id;
				note.Content = NoteBox.Text;
				note.Tag = TagBox.Text;
				note.SetStartHour(StartHour.Value.ToString());
				note.SetEndHour(EndHour.Value.ToString());
				currentlyChosenNote = DataBase.AddNote(note);
				OnDataBaseDataChange.Invoke();
			}
		}

		private void NewNoteButtonClick(object sender, RoutedEventArgs e)
		{
			SaveNote(currentlyChosenNote);
			ClearNote();
		}

		private void ClearNote()
		{
			currentlyChosenNote = new Note();
			StartHour.Value = new DateTime();
			EndHour.Value = new DateTime();
			TagBox.Text = "";
			NoteBox.Text = "";
		}

		private void DeleteNoteClickButton(object sender, RoutedEventArgs e)
		{
			DeleteNote(currentlyChosenNote);	
		}

		private void DeleteNote(Note note)
		{
			DataBase.DeleteNote(note);
			OnDataBaseDataChange.Invoke();
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
