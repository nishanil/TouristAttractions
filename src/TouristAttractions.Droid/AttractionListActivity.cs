using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using ToursitAttractions.Droid.Shared;
using Android.Support.V4.App;
using Android;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Content.PM;

namespace TouristAttractions
{
	[Activity(Label = "Tourist Attractions", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
	public class AttractionListActivity : AppCompatActivity
	{
		private const int permissionReq = 0;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			if (savedInstanceState == null)
			{
				SupportFragmentManager
					.BeginTransaction().Add(Resource.Id.container, new AttractionListFragment()).Commit();
			}

			if (!Utils.CheckFineLocationPermission(this))
			{
				if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessFineLocation))
				{
					ShowPermissionSnackbar();
				}
				else {
					if (savedInstanceState == null)
						RequestFineLocationPermission();
				}
			}
			else{
				// Otherwise permission is granted (which is always the case on pre-M devices)
				FineLocationPermissionGranted();
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			UtilityService.RequestLocation(this);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			//TODO: 
			switch (item.ItemId)
			{
				case Resource.Id.test_toggle_geofence:
					var isGeoFenceEnabled = Utils.GetGeofenceEnabled(this);
					Utils.StoreGeofenceEnabled(this, !isGeoFenceEnabled);
					Toast.MakeText(this, isGeoFenceEnabled ?
						"Debug: Geofencing trigger disabled" :
					               "Debug: Geofencing trigger enabled", ToastLength.Short).Show();
					return true;

			}
			return base.OnOptionsItemSelected(item);
		}

		/// <summary>
		/// Permissions result Callback
		/// </summary>
		/// <returns>The request permissions result.</returns>
		/// <param name="requestCode">Request code.</param>
		/// <param name="permissions">Permissions.</param>
		/// <param name="grantResults">Grant results.</param>
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			switch (requestCode)
			{
				case permissionReq:
					{
						if (grantResults[0] == Permission.Granted)
						{
							FineLocationPermissionGranted();
						}
						break;
					}
			}
		}


		/// <summary>
		/// Requests the fine location permission.
		/// </summary>
		/// <returns>The fine location permission.</returns>
		private void RequestFineLocationPermission()
		{
			ActivityCompat.RequestPermissions(this,
			                                  new string[] { Manifest.Permission.AccessFineLocation }, permissionReq);
		}

		/// <summary>
		/// Fines the location permission granted.
		/// </summary>
		/// <returns>The location permission granted.</returns>
		private void FineLocationPermissionGranted()
		{
			
			UtilityService.AddGeofences(this);
			UtilityService.RequestLocation(this);
		}

		/// <summary>
		/// Shows the permission snackbar.
		/// </summary>
		/// <returns>The permission snackbar.</returns>
		private void ShowPermissionSnackbar()
		{
			Snackbar.Make(FindViewById(Resource.Id.container), Resource.String.permission_explanation, Snackbar.LengthLong)
					.SetAction(Resource.String.permission_explanation_action, (o) => { RequestFineLocationPermission(); })
					.Show();
		}

		/// <summary>
		///  Show a basic debug dialog to provide more info on the built-in debug options.
		/// </summary>
		/// <returns>The debug dialog.</returns>
		/// <param name="titleResId">Title res identifier.</param>
		/// <param name="bodyResId">Body res identifier.</param>
		private void ShowDebugDialog(int titleResId, int bodyResId)
		{
			Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this)
					.SetTitle(titleResId)
					.SetMessage(bodyResId)
				.SetPositiveButton(Android.Resource.String.Ok, (o, e) => { });
			builder.Create().Show();
		}
	}
}


