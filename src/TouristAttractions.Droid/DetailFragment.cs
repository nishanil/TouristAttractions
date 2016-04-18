using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using static TouristAttractions.TouristAttractionsHelper;
using Android.Support.Design.Widget;
using Android.Gms.Maps.Model;
using ToursitAttractions.Droid.Shared;
using Android.Hardware.Fingerprints;

namespace TouristAttractions
{
	public class DetailFragment : Fragment
	{
		private static readonly string ExtraAttraction = "attraction";
    	private Attraction attraction;



		public static DetailFragment CreateInstance(string attractionName)
		{
			DetailFragment detailFragment = new DetailFragment();
			Bundle bundle = new Bundle();
			bundle.PutString(ExtraAttraction, attractionName);
			detailFragment.Arguments = bundle;
			return detailFragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			HasOptionsMenu = true;

			var view = inflater.Inflate(Resource.Layout.fragment_detail, container, false);
			var attractionName = Arguments.GetString(ExtraAttraction);
			attraction = FindAttraction(attractionName);

			if (attraction == null)
			{
				Activity.Finish();
				return null;
			}

			var nameTextView = view.FindViewById<TextView>(Resource.Id.nameTextView);
			var descTextView = view.FindViewById<TextView>(Resource.Id.descriptionTextView);
			var distanceTextView = view.FindViewById<TextView>(Resource.Id.distanceTextView);
			var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
			var mapFab = view.FindViewById<FloatingActionButton>(Resource.Id.mapFab);

			var checkInButton = view.FindViewById<Button>(Resource.Id.checkinButton);

			checkInButton.Click += (sender, e) => {
				var detailActivity = (DetailActivity)Activity;
				if (detailActivity.IsFingerPrintReady && detailActivity.InitCipher())
				{
					var fingerPrintFrag = new FingerprintAuthenticationDialogFragment();
					fingerPrintFrag.SetCryptoObject(new FingerprintManager.CryptoObject(detailActivity.mCipher));
					fingerPrintFrag.Show(FragmentManager, "my_frag");
				}
			};

			LatLng location = Utils.GetLocation(Activity);
			//TODO:
			string distance = string.Empty;
			//String distance = Utils.formatDistanceBetween(location, mAttraction.location);
			//if (TextUtils.isEmpty(distance))
			//{
			//	distanceTextView.setVisibility(View.GONE);
			//}

			nameTextView.Text = attractionName;
			distanceTextView.Text = distance;
			descTextView.Text = attraction.LongDescription;

			Koush.UrlImageViewHelper.SetUrlDrawable(imageView, attraction.ImageUrl.AbsoluteUri);


			//TODO: Glide

			//int imageSize = getResources().getDimensionPixelSize(R.dimen.image_size)
			//   * Constants.IMAGE_ANIM_MULTIPLIER;
			//Glide.with(getActivity())
			//		.load(mAttraction.imageUrl)
			//		.diskCacheStrategy(DiskCacheStrategy.SOURCE)
			//		.placeholder(R.color.lighter_gray)
			//		.override(imageSize, imageSize)
  			//             .into(imageView);

			mapFab.Click += (o,e)=> {
				var intent = new Intent(Intent.ActionView);
				intent.SetData(Android.Net.Uri.Parse(Constants.MapsIntentUri +
													 Android.Net.Uri.Encode(attraction.Name + ", " + attraction.City)));
				StartActivity(intent);
			};


			return view;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
				case Android.Resource.Id.Home:
					// Some small additions to handle "up" navigation correctly
					var upIntent = NavUtils.GetParentActivityIntent(Activity);
					upIntent.AddFlags(ActivityFlags.ClearTask);


					// Check if up activity needs to be created (usually when
					// detail screen is opened from a notification or from the
					// Wearable app
					if (NavUtils.ShouldUpRecreateTask(Activity, upIntent)
					    || Activity.IsTaskRoot)
					{

						// Synthesize parent stack
						Android.App.TaskStackBuilder.Create(Activity)
								.AddNextIntentWithParentStack(upIntent)
								.StartActivities();
					}

					if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
					{
						// On Lollipop+ we finish so to run the nice animation
						Activity.FinishAfterTransition();
						return true;
					}

					// Otherwise let the system handle navigating "up"
					return false;
					
			}
			return base.OnOptionsItemSelected(item);
		}

		/**
     * Really hacky loop for finding attraction in our static content provider.
     * Obviously would not be used in a production app.
     */
		private Attraction FindAttraction(string attractionName)
		{

			//TODO: change to linq
			foreach (var item in Attractions)
			{
				var attractions = Attractions[item.Key];
				foreach (var a in attractions)
				{
					if (a.Name == attractionName )
					{
						return a;
					}
				}
			}
			return null;
		}



	}
}

