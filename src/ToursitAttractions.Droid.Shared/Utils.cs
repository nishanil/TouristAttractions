using System;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps.Model;
using Android.Support.V4.Content;
using Java.Text;


namespace ToursitAttractions.Droid.Shared
{
	public static class Utils
	{

		private static readonly string DISTANCE_KM_POSTFIX = "km";
		private static readonly string DISTANCE_M_POSTFIX = "m";
		
		/// <summary>
		/// Check if the app has access to fine location permission. On pre-M devices this will always return true.
		/// </summary>
		/// <returns>The fine location permission.</returns>
		/// <param name="context">Context.</param>
		public static bool CheckFineLocationPermission(Context context)
		{
			const string permission = Manifest.Permission.AccessFineLocation;
			return (ContextCompat.CheckSelfPermission(context, permission) == (int)Permission.Granted);

		}

		/**
     * Calculate distance between two LatLng points and format it nicely for
     * display. As this is a sample, it only statically supports metric units.
     * A production app should check locale and support the correct units.
     */
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

