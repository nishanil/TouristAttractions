using System;
using TouristAttractions.Portable;

namespace TouristAttractions
{
	public static class App
	{
		public static ISQLiteProvider DataConnection => new SQLiteDroid();
	}
}

