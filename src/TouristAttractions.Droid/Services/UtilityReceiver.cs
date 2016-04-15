using System;
using Android.Content;
using Android.Support.V4.Content;

namespace TouristAttractions
{
	[BroadcastReceiver]
	public class UtilityReceiver : WakefulBroadcastReceiver
	{
		
		public override void OnReceive(Context context, Intent intent)
		{
			// Pass right over to UtilityService class, the wakeful receiver is
			// just needed in case the geofence is triggered while the device
			// is asleep otherwise the service may not have time to trigger the
			// notification.
			intent.SetClass(context, typeof(UtilityService));
			intent.SetAction(UtilityService.ActionGeofenceTriggered);
			StartWakefulService(context, intent);
		}
	}
}

