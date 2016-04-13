using System;
namespace ToursitAttractions.Droid.Shared
{
	public static class Constants
	{
		public static readonly bool UseMicroApp = true;

		public static readonly int GOOGLE_API_CLIENT_TIMEOUT_S = 10; // 10 seconds
		public static readonly string GOOGLE_API_CLIENT_ERROR_MSG =
			"Failed to connect to GoogleApiClient (error code = %d)";

		// Used to size the images in the mobile app so they can animate cleanly from list to detail
		public static readonly int ImageAnimMultiplier = 2;

		// Resize images sent to Wear to 400x400px
		public static readonly int WEAR_IMAGE_SIZE = 400;

		// Except images that can be set as a background with parallax, set width 640x instead
		public static readonly int WEAR_IMAGE_SIZE_PARALLAX_WIDTH = 640;

		// The minimum bottom inset percent to use on a round screen device
		public static readonly float WEAR_ROUND_MIN_INSET_PERCENT = 0.08f;

		// Max # of attractions to show at once
		public static readonly int MAX_ATTRACTIONS = 4;

		// Notification IDs
		public static readonly int MOBILE_NOTIFICATION_ID = 100;
		public static readonly int WEAR_NOTIFICATION_ID = 200;

		// Intent and bundle extras
		public static readonly string EXTRA_ATTRACTIONS = "extra_attractions";
		public static readonly string EXTRA_ATTRACTIONS_URI = "extra_attractions_uri";
		public static readonly string EXTRA_TITLE = "extra_title";
		public static readonly string EXTRA_DESCRIPTION = "extra_description";
		public static readonly string EXTRA_LOCATION_LAT = "extra_location_lat";
		public static readonly string EXTRA_LOCATION_LNG = "extra_location_lng";
		public static readonly string EXTRA_DISTANCE = "extra_distance";
		public static readonly string EXTRA_CITY = "extra_city";
		public static readonly string EXTRA_IMAGE = "extra_image";
		public static readonly string EXTRA_IMAGE_SECONDARY = "extra_image_secondary";
		public static readonly string EXTRA_TIMESTAMP = "extra_timestamp";

		// Wear Data API paths
		public static readonly string ATTRACTION_PATH = "/attraction";
		public static readonly string START_PATH = "/start";
		public static readonly string START_ATTRACTION_PATH = START_PATH + "/attraction";
		public static readonly string START_NAVIGATION_PATH = START_PATH + "/navigation";
		public static readonly string CLEAR_NOTIFICATIONS_PATH = "/clear";

		// Maps values
		public static readonly string MAPS_INTENT_URI = "geo:0,0?q=";
		public static readonly string MAPS_NAVIGATION_INTENT_URI = "google.navigation:mode=w&q=";
	}
}

