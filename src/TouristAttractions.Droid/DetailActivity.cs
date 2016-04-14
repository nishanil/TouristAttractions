
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TouristAttractions
{
	[Activity(Label="", Theme = "@style/XYZAppTheme.Detail", ParentActivity=typeof(AttractionListActivity))]
	public class DetailActivity : AppCompatActivity
	{
		private static readonly string EXTRA_ATTRACTION = "attraction";

		public static void Launch(Activity activity, String attraction, View heroView)
		{
			Intent intent = GetLaunchIntent(activity, attraction);
			if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				ActivityOptionsCompat options = ActivityOptionsCompat.MakeSceneTransitionAnimation(
					activity, heroView, heroView.TransitionName);
				ActivityCompat.StartActivity(activity, intent, options.ToBundle());
			}
			else {
				activity.StartActivity(intent);
			}
		}

		public static Intent GetLaunchIntent(Context context, string attraction)
		{
			Intent intent = new Intent(context, typeof(DetailActivity));
			intent.PutExtra(EXTRA_ATTRACTION, attraction);
			return intent;
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			var attraction = Intent.GetStringExtra(EXTRA_ATTRACTION);
			if (savedInstanceState == null)
			{
				SupportFragmentManager.BeginTransaction()
										.Add(Resource.Id.container, DetailFragment.CreateInstance(attraction))
										.Commit();
			}

		}
	}
}

