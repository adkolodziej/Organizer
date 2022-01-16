using Organizer.Models;
using System.Collections.Generic;

namespace Organizer.Filters
{
	public class FilteredData
	{
		private static FilteredData instance;
		private Dictionary<int, bool> dateFilterPair;

		public Dictionary<int, bool> DateFilterPair => dateFilterPair;

		private FilteredData()
		{
			dateFilterPair = new Dictionary<int, bool>();
		}

		public static FilteredData GetInstance()
		{
			if (instance == null)
			{
				instance = new FilteredData();
			}
			return instance;
		}

		public void FilterData(FilterType filterType, Dates dates, string filter)
		{
			dateFilterPair = new Dictionary<int, bool>();
			switch (filterType)
			{
				case FilterType.Tag:
					{
						FilterByNotesTag(dates, filter);
						break;
					}
				case FilterType.StartHour:
					{
						FilterByNotesStartHour(dates, filter);
						break;
					}
				case FilterType.EndHour:
					{
						FilterByNotesEndHour(dates, filter);
						break;
					}
				case FilterType.Content:
					{
						FilterByNotesContent(dates, filter);
						break;
					}
				case FilterType.None:
					{
						break;
					}
				default:
					break;
			}
		}

		public void FilterByNotesTag(Dates dates, string tag)
		{
			foreach (Date date in dates.ListOfDates)
			{
				Note note = date.Notes.Find(x => x.Tag == tag);
				if (note != null)
				{
					dateFilterPair.Add(date.Id, true);
				}
				else
				{
					dateFilterPair.Add(date.Id, false);
				}
			}
		}

		public void FilterByNotesStartHour(Dates dates, string startHour)
		{
			foreach (Date date in dates.ListOfDates)
			{
				Note note = date.Notes.Find(x => x.CompareStartHour(startHour));
				if (note != null)
				{
					dateFilterPair.Add(date.Id, true);
				}
				else
				{
					dateFilterPair.Add(date.Id, false);
				}
			}
		}

		public void FilterByNotesEndHour(Dates dates, string endHour)
		{
			foreach (Date date in dates.ListOfDates)
			{
				Note note = date.Notes.Find(x => x.CompareEndHour(endHour));
				if (note != null)
				{
					dateFilterPair.Add(date.Id, true);
				}
				else
				{
					dateFilterPair.Add(date.Id, false);
				}
			}
		}

		public void FilterByNotesContent(Dates dates, string content)
		{
			foreach (Date date in dates.ListOfDates)
			{
				Note note = date.Notes.Find(x => x.Content.Contains(content));
				if (note != null)
				{
					dateFilterPair.Add(date.Id, true);
				}
				else
				{
					dateFilterPair.Add(date.Id, false);
				}
			}
		}
	}
}
