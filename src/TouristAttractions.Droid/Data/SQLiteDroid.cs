using System;
using System.IO;
using TouristAttractions.Portable;

namespace TouristAttractions
{

	public class SQLiteDroid : ISQLiteProvider
	{
		public SQLiteDroid() { }
		public SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "TouristAttractions.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}

