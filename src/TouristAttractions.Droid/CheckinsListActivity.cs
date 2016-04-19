
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TouristAttractions.Portable;
using Android.Support.V7.App;

namespace TouristAttractions
{
	[Activity(Label = "Check-ins", ParentActivity = typeof(AttractionListActivity))]
	public class CheckinsListActivity : AppCompatActivity
	{
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_checkins);

			var listView = FindViewById<ListView>(Resource.Id.checkins_list);
			listView.Adapter =  new CheckinsListAdapter(this, new CheckinDataManager(App.DataConnection).GetItems().ToList());

		}
	}

	public class CheckinsListAdapter : BaseAdapter<CheckinItem>
	{
		List<CheckinItem> items;
		Activity context;
		public CheckinsListAdapter(Activity context, List<CheckinItem> items) : base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
			get { return items.Count; }
		}

		public override CheckinItem this[int position]
		{
			get
			{
				return items[position];
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Place;
			return view;
		}
	}
}

