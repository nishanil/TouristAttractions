using System;
namespace TouristAttractions.Portable
{
	public interface ISQLiteProvider
	{
		SQLite.SQLiteConnection GetConnection();
	}
}

