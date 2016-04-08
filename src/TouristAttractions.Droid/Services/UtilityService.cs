using System;
using Android.App;
using Android.Content;

namespace TouristAttractions
{
	/// <summary>
	///  A utility IntentService, used for a variety of asynchronous background
	///  operations that do not necessarily need to be tied to a UI.
	/// </summary>
	[Service]
	[IntentFilter(new String[]{"com.nnish.TouristAttractions.UtilityService"})]
	public class UtilityService : IntentService
	{
		public static readonly string ActionGeofenceTriggered = "geofence_triggered";
    	private static readonly string actionLocationUpdated = "location_updated";
    	private static readonly string actionRequestLocation = "request_location";
    	private static readonly string actionAddGeoFences = "add_geofences";
    	private static readonly string actionClearNotification = "clear_notification";
    	private static readonly string actionClearRemoteNotifications = "clear_remote_notifications";
    	private static readonly string actionFakeUpdate = "fake_update";
    	private static readonly string extraTestMicroApp = "test_microapp";

		public UtilityService()
		{
		}

		protected override void OnHandleIntent(Intent intent)
		{
			throw new NotImplementedException();
		}

		public static void RequestLocation(Context context)
		{
			var intent = new Intent(context, typeof(UtilityService));
			intent.SetAction(actionLocationUpdated);
			context.StartService(intent);
		}
	}
}

