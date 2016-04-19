using System;
using SQLite;

namespace TouristAttractions.Portable
{
	public class CheckinItem
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get;
			set;
		}

		public DateTime Time
		{
			get;
			set;
		}

		public string Place
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}
	}
}

