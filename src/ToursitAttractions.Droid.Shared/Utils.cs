using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Gms.Maps.Model;
using Android.Gms.Wearable;
using Android.Preferences;
using Android.Support.V4.Content;
using Java.Text;


namespace ToursitAttractions.Droid.Shared
{
	public static class Utils
	{

		private static readonly string DISTANCE_KM_POSTFIX = "km";
		private static readonly string DISTANCE_M_POSTFIX = "m";
		private static readonly string PREFERENCES_GEOFENCE_ENABLED = "geofence";
		private static readonly string preferencesLat = "lat";
		private static readonly string preferencesLang = "lng";

		/// <summary>
		/// Check if the app has access to fine location permission. On pre-M devices this will always return true.
		/// </summary>
		/// <returns>The fine location permission.</returns>
		/// <param name="context">Context.</param>
		public static bool CheckFineLocationPermission(Context context)
		{
			const string permissionStr = Manifest.Permission.AccessFineLocation;
			var permission = ContextCompat.CheckSelfPermission(context, permissionStr);
			return (permission == Permission.Granted);
		}

		public static bool GetGeofenceEnabled(Context context)
		{
			var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
			return prefs.GetBoolean(PREFERENCES_GEOFENCE_ENABLED, true);
		}

		/// <summary>
		/// Store the location in the app preferences.
		/// </summary>
		/// <returns>The location.</returns>
		/// <param name="context">Context.</param>
		/// <param name="location">Location.</param>
		public static void StoreLocation(Context context, LatLng location)
		{
			var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
			ISharedPreferencesEditor editor = prefs.Edit();
			editor.PutLong(preferencesLat, Java.Lang.Double.DoubleToRawLongBits(location.Latitude));
			editor.PutLong(preferencesLang, Java.Lang.Double.DoubleToRawLongBits(location.Longitude));
			editor.Apply();
		}

		/// <summary>
		/// Fetch the location from app preferences.
		/// </summary>
		/// <returns>The location.</returns>
		/// <param name="context">Context.</param>
		public static LatLng GetLocation(Context context)
		{
			if (!CheckFineLocationPermission(context))
			{
				return null;
			}

			var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
			var lat = prefs.GetLong(preferencesLat, long.MaxValue);
			var lng = prefs.GetLong(preferencesLang, long.MaxValue);
			if (lat != long.MaxValue && lng != long.MaxValue)
			{
				var latDbl = Java.Lang.Double.LongBitsToDouble(lat);
				var lngDbl = Java.Lang.Double.LongBitsToDouble(lng);
				return new LatLng(latDbl, lngDbl);
			}
			return null;
		}

		/**
 * Get a list of all wearable nodes that are connected synchronously.
 * Only call this method from a background thread (it should never be
 * called from the main/UI thread as it blocks).
 */
		public static async Task<List<string>> GetNodes(GoogleApiClient client)
		{
			var localNodes = new List<string>();
			var connectedNodes = await WearableClass.NodeApi.GetConnectedNodesAsync(client);
			foreach (var item in connectedNodes.Nodes)
			{
				localNodes.Add(item.Id);
			}
			return localNodes;
		}

		/// <summary>
		/// Calculate distance between two LatLng points and format it nicely for  display.
		/// As this is a sample, it only statically supports metric units. 
		/// A production app should check locale and support the correct units.
		/// </summary>
		/// <returns>The <see cref="T:System.String"/>.</returns>
		/// <param name="point1">Point1.</param>
		/// <param name="point2">Point2.</param>
		public static string FormatDistanceBetween(LatLng point1, LatLng point2)
		{
			if (point1 == null || point2 == null)
			{
				return null;
			}

			NumberFormat numberFormat = NumberFormat.NumberInstance;
			double distance = 0.0; //TODO: //  Math.Round(SphericalUtil.computeDistanceBetween(point1, point2));

			// Adjust to KM if M goes over 1000 (see javadoc of method for note
			// on only supporting metric)
			if (distance >= 1000)
			{
				numberFormat.MaximumFractionDigits = 1;
				return numberFormat.Format(distance / 1000) + DISTANCE_KM_POSTFIX;
			}
			return numberFormat.Format(distance) + DISTANCE_M_POSTFIX;
		}

}
}

