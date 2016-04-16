using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using Android.Gms.Wearable;
using Android.Graphics;
using Android.Locations;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Java.Util.Concurrent;
using ToursitAttractions.Droid.Shared;


namespace TouristAttractions
{
	/// <summary>
	///  A utility IntentService, used for a variety of asynchronous background
	///  operations that do not necessarily need to be tied to a UI.
	/// </summary>
	[Service]
	[IntentFilter(new String[] { "com.nnish.UtilityService" })]
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

		public static IntentFilter GetLocationUpdatedIntentFilter()
		{
			return new IntentFilter(actionLocationUpdated);
		}

		public static void TriggerWearTest(Context context, bool microApp)
		{
			var intent = new Intent(context, typeof(UtilityService));
			intent.SetAction(actionFakeUpdate);
			intent.PutExtra(extraTestMicroApp, microApp);
			context.StartService(intent);
		}

		public static void AddGeofences(Context context)
		{
			var intent = new Intent(context, typeof(UtilityService));
			intent.SetAction(actionAddGeoFences);
			context.StartService(intent);
		}

		public static void RequestLocation(Context context)
		{
			var intent = new Intent(context, typeof(UtilityService));
			intent.SetAction(actionRequestLocation);
			context.StartService(intent);
		}

		public static void ClearNotification(Context context)
		{
			var intent = new Intent(context, typeof(UtilityService));
			intent.SetAction(actionClearNotification);
			context.StartService(intent);
		}

		public static Intent GetClearRemoteNotificationsIntent(Context context)
		{
			var intent = new Intent(context, typeof(UtilityService));

			intent.SetAction(actionClearRemoteNotifications);
			return intent;
		}


		protected override void OnHandleIntent(Intent intent)
		{
			var action = intent != null ? intent.Action : null;
			if (action == actionAddGeoFences)
			{
				AddGeofencesInternal();
			}
			else if (action == ActionGeofenceTriggered)
			{
				GeofenceTriggered(intent);
			}
			else if (action == actionRequestLocation)
			{
				RequestLocationInternal();
			}
			else if (action == actionLocationUpdated)
			{
				LocationUpdated(intent);
			}
			else if (action == actionClearNotification)
			{
				ClearNotificationInternal();
			}
			else if (action == actionClearRemoteNotifications)
			{
				ClearRemoteNotifications();
			}
			else if (action == actionFakeUpdate)
			{
				var currentLocation = Utils.GetLocation(this);

				// If location unknown use test city, otherwise use closest city
				string city = currentLocation == null ? TouristAttractionsHelper.TestCity :
						TouristAttractionsHelper.GetClosestCity(currentLocation);

				ShowNotification(city,
								 intent.GetBooleanExtra(extraTestMicroApp, Constants.UseMicroApp));
			}
		}

		/**
	     * Add geofences using Play Services
	     */
		private async void AddGeofencesInternal()
		{
			Log.Verbose("UtilityService", actionAddGeoFences);

			if (!Utils.CheckFineLocationPermission(this))
			{
				return;
			}

			GoogleApiClient googleApiClient = new GoogleApiClient.Builder(this)
					.AddApi(LocationServices.API)
					.Build();

			// It's OK to use blockingConnect() here as we are running in an
			// IntentService that executes work on a separate (background) thread.
			ConnectionResult connectionResult = googleApiClient.BlockingConnect(
				Constants.GOOGLE_API_CLIENT_TIMEOUT_S, TimeUnit.Seconds);

			if (connectionResult.IsSuccess && googleApiClient.IsConnected)
			{
				var pendingIntent = PendingIntent.GetBroadcast(
					this, 0, new Intent(this, typeof(UtilityReceiver)), 0);

				//TODO: Move to new api

				var status = await LocationServices.GeofencingApi.AddGeofencesAsync(googleApiClient,
											TouristAttractionsHelper.GetGeofenceList(), pendingIntent);
				
				googleApiClient.Disconnect();
			}
			else {
				Log.Error("UtilityService", string.Format(Constants.GOOGLE_API_CLIENT_ERROR_MSG,
														  connectionResult.ErrorCode));
			}
		}

		/**
   * Called when a geofence is triggered
   */
		private async void GeofenceTriggered(Intent intent)
		{
			Log.Verbose("UtilityService", ActionGeofenceTriggered);

			// Check if geofences are enabled
			bool geofenceEnabled = Utils.GetGeofenceEnabled(this);

			// Extract the geofences from the intent
			var geoEvent = GeofencingEvent.FromIntent(intent);
			var geofences = geoEvent.TriggeringGeofences;

			if (geofenceEnabled && geofences != null && geofences.Count > 0)
			{
				if (geoEvent.GeofenceTransition == Geofence.GeofenceTransitionEnter)
				{
					//TODO:
					// Trigger the notification based on the first geofence
					//ShowNotification(geofences.get(0).getRequestId(), Constants.USE_MICRO_APP);
				}
				else if (geoEvent.GeofenceTransition == Geofence.GeofenceTransitionExit)
				{
					// Clear notifications
					ClearNotificationInternal();
					await ClearRemoteNotifications();
				}
			}
			Android.Support.V4.Content.WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
		}

		private void RequestLocationInternal()
		{
			Log.Verbose("UtilityService", actionRequestLocation);

			if (!Utils.CheckFineLocationPermission(this))
			{
				return;
			}

			GoogleApiClient googleApiClient = new GoogleApiClient.Builder(this)
					.AddApi(LocationServices.API)
					.Build();

			// It's OK to use blockingConnect() here as we are running in an
			// IntentService that executes work on a separate (background) thread.
			ConnectionResult connectionResult = googleApiClient.BlockingConnect(
				Constants.GOOGLE_API_CLIENT_TIMEOUT_S, TimeUnit.Seconds);

			if (connectionResult.IsSuccess && googleApiClient.IsConnected)
			{

				Intent locationUpdatedIntent = new Intent(this, typeof(UtilityService));
				locationUpdatedIntent.SetAction(actionLocationUpdated);

				// Send last known location out first if available
				var location = LocationServices.FusedLocationApi.GetLastLocation(googleApiClient);
				if (location != null)
				{
					Intent lastLocationIntent = new Intent(locationUpdatedIntent);
					lastLocationIntent.PutExtra(
										FusedLocationProviderApi.KeyLocationChanged, location);

					StartService(lastLocationIntent);
				}

				// Request new location
				var locationRequest = new LocationRequest()
								.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);
				LocationServices.FusedLocationApi.RequestLocationUpdates(
					googleApiClient, locationRequest,
					PendingIntent.GetService(this, 0, locationUpdatedIntent, 0));

				googleApiClient.Disconnect();
			}
			else {
				Log.Error("UtilityService", string.Format(Constants.GOOGLE_API_CLIENT_ERROR_MSG,
														  connectionResult.ErrorCode));
			}
		}

		/// <summary>
		///Called when the location has been updated
		/// </summary>
		/// <returns>The updated.</returns>
		/// <param name="intent">Intent.</param>
		private void LocationUpdated(Intent intent)
		{
			Log.Verbose("UtilityService", actionLocationUpdated);

			// Extra new location
			var location =
				intent.GetParcelableExtra(FusedLocationProviderApi.KeyLocationChanged) as Location;

			if (location != null)
			{
				LatLng latLngLocation = new LatLng(location.Latitude, location.Longitude);

				// Store in a local preference as well
				Utils.StoreLocation(this, latLngLocation);

				// Send a local broadcast so if an Activity is open it can respond
				// to the updated location
				LocalBroadcastManager.GetInstance(this).SendBroadcast(intent);
			}
		}

		/// <summary>
		/// Clears the local device notification.
		/// </summary>
		/// <returns>The notification internal.</returns>
		private void ClearNotificationInternal()
		{
			Log.Verbose("UtilityService", actionClearNotification);
			NotificationManagerCompat.From(this).Cancel(Constants.MOBILE_NOTIFICATION_ID);
		}


		/// <summary>
		/// Clears remote device notifications using the Wearable message API
		/// </summary>
		/// <returns>The remote notifications.</returns>
		private async Task ClearRemoteNotifications()
		{
			Log.Verbose("UtilityService", actionClearRemoteNotifications);
			GoogleApiClient googleApiClient = new GoogleApiClient.Builder(this)
																 .AddApi(Android.Gms.Wearable.WearableClass.API)
																 .Build();

			// It's OK to use blockingConnect() here as we are running in an
			// IntentService that executes work on a separate (background) thread.
			ConnectionResult connectionResult = googleApiClient.BlockingConnect(
				Constants.GOOGLE_API_CLIENT_TIMEOUT_S, TimeUnit.Seconds);

			if (connectionResult.IsSuccess && googleApiClient.IsConnected)
			{

				// Loop through all nodes and send a clear notification message
				foreach (var item in await Utils.GetNodes(googleApiClient))
				{
					await WearableClass.MessageApi.SendMessageAsync(googleApiClient, item, Constants.CLEAR_NOTIFICATIONS_PATH, null);
				}
				googleApiClient.Disconnect();
			}
		}


		private void ShowNotification(string cityId, bool microApp)
		{

			var attractions = TouristAttractionsHelper.Attractions[cityId];

			if (microApp)
			{
				//TODO:
				// If micro app we first need to transfer some data over
				//sendDataToWearable(attractions);
			}

			// The first (closest) tourist attraction
			Attraction attraction = attractions[0];

			// Limit attractions to send
			int count = (attractions.Count) > Constants.MAX_ATTRACTIONS ?
								   Constants.MAX_ATTRACTIONS : attractions.Count;

			// Pull down the tourist attraction images from the network and store
			var bitmaps = new Dictionary<string, Bitmap>();
			try
			{
				for (int i = 0; i < count; i++)
				{
					//TODO:
					//bitmaps.Put(attractions[i].Name,
					//		Glide.with(this)
					//				.load(attractions.get(i).imageUrl)
					//				.asBitmap()
					//				.diskCacheStrategy(DiskCacheStrategy.SOURCE)
					//				.into(Constants.WEAR_IMAGE_SIZE, Constants.WEAR_IMAGE_SIZE)
					//				.get());
				}
			}
			catch (Exception e)
			{
				Log.Error("UtilityService", "Error fetching image from network: " + e);
			}

			// The intent to trigger when the notification is tapped
			PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0,
					DetailActivity.GetLaunchIntent(this, attraction.Name),
																	PendingIntentFlags.UpdateCurrent);

			// The intent to trigger when the notification is dismissed, in this case
			// we want to clear remote notifications as well
			PendingIntent deletePendingIntent =
					PendingIntent.GetService(this, 0, GetClearRemoteNotificationsIntent(this), 0);

			// Construct the main notification
			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
					.SetStyle(new NotificationCompat.BigPictureStyle()
									.BigPicture(bitmaps[attraction.Name])
									.SetBigContentTitle(attraction.Name)
									.SetSummaryText(GetString(Resource.String.nearby_attraction))
					)
					.SetLocalOnly(microApp)
					.SetContentTitle(attraction.Name)
					.SetContentText(GetString(Resource.String.nearby_attraction))
					.SetSmallIcon(Resource.Drawable.ic_stat_maps_pin_drop)
					.SetContentIntent(pendingIntent)
					.SetDeleteIntent(deletePendingIntent)
				.SetColor(Resources.GetColor(Resource.Color.colorPrimary, Theme))
				.SetCategory(Notification.CategoryRecommendation)
				.SetAutoCancel(true);

			if (!microApp)
			{
				// If not a micro app, create some wearable pages for
				// the other nearby tourist attractions.
				List<Notification> pages = new List<Notification>();
				for (int i = 1; i < count; i++)
				{

					// Calculate the distance from current location to tourist attraction
					String distance = Utils.FormatDistanceBetween(
						Utils.GetLocation(this), attractions[i].Location);

					// Construct the notification and add it as a page
					pages.Add(new NotificationCompat.Builder(this)
							.SetContentTitle(attractions[i].Name)
							.SetContentText(distance)
							.SetSmallIcon(Resource.Drawable.ic_stat_maps_pin_drop)
							.Extend(new NotificationCompat.WearableExtender()
									.SetBackground(bitmaps[attractions[i].Name])
							)
							  .Build());
				}
				builder.Extend(new NotificationCompat.WearableExtender().AddPages(pages));
			}

			// Trigger the notification
			NotificationManagerCompat.From(this).Notify(
					Constants.MOBILE_NOTIFICATION_ID, builder.Build());
		}

		/**
 * Transfer the required data over to the wearable
 * @param attractions list of attraction data to transfer over
 */
		private void SendDataToWearable(List<Attraction> attractions)
		{
			GoogleApiClient googleApiClient = new GoogleApiClient.Builder(this)
																 .AddApi(WearableClass.API)
					.Build();

			// It's OK to use blockingConnect() here as we are running in an
			// IntentService that executes work on a separate (background) thread.
			ConnectionResult connectionResult = googleApiClient.BlockingConnect(
				Constants.GOOGLE_API_CLIENT_TIMEOUT_S, TimeUnit.Seconds);

			// Limit attractions to send
			int count = attractions.Count > Constants.MAX_ATTRACTIONS ?
								   Constants.MAX_ATTRACTIONS : attractions.Count;

			List<DataMap> attractionsData = new List<DataMap>(count);

			for (int i = 0; i < count; i++)
			{
				Attraction attraction = attractions[i];

				Bitmap image = null;
				Bitmap secondaryImage = null;

				try
				{
					//TODO:
					// Fetch and resize attraction image bitmap
					//image = Glide.with(this)
					//		.load(attraction.imageUrl)
					//		.asBitmap()
					//		.diskCacheStrategy(DiskCacheStrategy.SOURCE)
					//		.into(Constants.WEAR_IMAGE_SIZE_PARALLAX_WIDTH, Constants.WEAR_IMAGE_SIZE)
					//		.get();

					//secondaryImage = Glide.with(this)
					//		.load(attraction.secondaryImageUrl)
					//		.asBitmap()
					//		.diskCacheStrategy(DiskCacheStrategy.SOURCE)
					//		.into(Constants.WEAR_IMAGE_SIZE_PARALLAX_WIDTH, Constants.WEAR_IMAGE_SIZE)
					//		.get();
				}
				catch (Exception e)
				{
					Log.Error("UtilityService", "Exception loading bitmap from network");
				}

				if (image != null && secondaryImage != null)
				{

					DataMap attractionData = new DataMap();

					string distance = Utils.FormatDistanceBetween(
							Utils.GetLocation(this), attraction.Location);

					attractionData.PutString(Constants.EXTRA_TITLE, attraction.Name);
					attractionData.PutString(Constants.EXTRA_DESCRIPTION, attraction.Description);
					attractionData.PutDouble(
							Constants.EXTRA_LOCATION_LAT, attraction.Location.Latitude);
					attractionData.PutDouble(
							Constants.EXTRA_LOCATION_LNG, attraction.Location.Longitude);
					attractionData.PutString(Constants.EXTRA_DISTANCE, distance);
					attractionData.PutString(Constants.EXTRA_CITY, attraction.City);

					//TODO:
					//	attractionData.PutAsset(Constants.EXTRA_IMAGE,
					//		Utils.CreateAssetFromBitmap(image));
					//attractionData.PutAsset(Constants.EXTRA_IMAGE_SECONDARY,
					//		Utils.CreateAssetFromBitmap(secondaryImage));

					attractionsData.Add(attractionData);
				}
			}

			if (connectionResult.IsSuccess && googleApiClient.IsConnected
				&& attractionsData.Count > 0)
			{

				PutDataMapRequest dataMap = PutDataMapRequest.Create(Constants.ATTRACTION_PATH);
				dataMap.DataMap.PutDataMapArrayList(Constants.EXTRA_ATTRACTIONS, attractionsData);
				//TODO:
				//dataMap.DataMap.PutLong(Constants.EXTRA_TIMESTAMP, DateTime.Now.ToFileTime);
				PutDataRequest request = dataMap.AsPutDataRequest();
				request.SetUrgent();

				//TODO: Async/Await
				// Send the data over
				//            var result =
				//					WearableClass.DataApi.PutDataItem(googleApiClient, request));

				//            if (!result.).isSuccess()) {
				//                Log.e(TAG, String.format("Error sending data using DataApi (error code = %d)",
				//                        result.getStatus().getStatusCode()));
				//            }

				//} else {
				//            Log.e(TAG, String.format(Constants.GOOGLE_API_CLIENT_ERROR_MSG,
				//                    connectionResult.getErrorCode()));
				//        }
				googleApiClient.Disconnect();
			}
		}
	}
}

