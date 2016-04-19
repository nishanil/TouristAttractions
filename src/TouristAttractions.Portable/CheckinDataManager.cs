using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace TouristAttractions.Portable
{
	public class CheckinDataManager
	{
		static object locker = new object();

		SQLiteConnection database;


		public CheckinDataManager(ISQLiteProvider connectionProvider)
		{
			database = connectionProvider.GetConnection();
			// create the tables
			database.CreateTable<CheckinItem>();
		}

		public IEnumerable<CheckinItem> GetItems()
		{
			lock (locker)
			{
				return (from i in database.Table<CheckinItem>() select i).ToList().OrderByDescending(c => c.Time.Date)
																		 .ThenBy(c => c.Time.TimeOfDay);
			}
		}


		public CheckinItem GetItem(int id)
		{
			lock (locker)
			{
				return database.Table<CheckinItem>().FirstOrDefault(x => x.Id == id);
			}
		}

		public int SaveItem(CheckinItem item)
		{
			lock (locker)
			{
				if (item.Id != 0)
				{
					database.Update(item);
					return item.Id;
				}
				else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker)
			{
				return database.Delete<CheckinItem>(id);
			}
		}
	}
}

