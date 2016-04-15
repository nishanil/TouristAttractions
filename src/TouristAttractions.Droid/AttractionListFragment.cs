using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Com.Google.Maps.Android;
using ToursitAttractions;
using ToursitAttractions.Droid.Shared;
using static TouristAttractions.TouristAttractionsHelper;

namespace TouristAttractions
{
	public class AttractionListFragment : Android.Support.V4.App.Fragment
	{
		private AttractionAdapter adapter;
		public static LatLng LatestLocation;
		private int imageSize;
		public static bool IsItemClicked;


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			imageSize = Resources.GetDimensionPixelSize(Resource.Dimension.image_size)
								 * Constants.ImageAnimMultiplier;
			LatestLocation = Utils.GetLocation(this.Activity);
			var attractions = LoadAttractionsFromLocation(LatestLocation);
			adapter = new AttractionAdapter(this.Activity, attractions);


			View view = inflater.Inflate(Resource.Layout.fragment_main, container, false);
			var recyclerView =
				view.FindViewById<AttractionsRecyclerView>(Android.Resource.Id.List);
			recyclerView.SetEmptyView(view.FindViewById(Android.Resource.Id.Empty));
			recyclerView.HasFixedSize = true;
			recyclerView.SetAdapter(adapter);
			//adapter.NotifyDataSetChanged();
			return view;
		}

		public override void OnResume()
		{
			//TODO:
			IsItemClicked = false;
			base.OnResume();


		}
		public override void OnPause()
		{
			//TODO:
			base.OnPause();
		}

		private List<Attraction> LoadAttractionsFromLocation(LatLng curLatLng)
		{
			string closestCity = GetClosestCity(curLatLng);
			if (closestCity != null)
			{
				var attractions = Attractions[closestCity];

				//TODO: 
							// if (curLatLng != null) {
				//			Collections.sort(attractions,
				//					new Comparator<Attraction>() {
				//						@Override

				//						public int compare(Attraction lhs, Attraction rhs)
				//	{
				//		double lhsDistance = SphericalUtil.computeDistanceBetween(
				//				lhs.location, curLatLng);
				//		double rhsDistance = SphericalUtil.computeDistanceBetween(
				//				rhs.location, curLatLng);
				//		return (int)(lhsDistance - rhsDistance);
				//	}
				//}
			 //               );

				return attractions;
			}
			return null;
		}

	}

	public class AttractionAdapter : RecyclerView.Adapter
	{
		private List<Attraction> attractions;
		private Context context;

		public event EventHandler<int> ItemClick;

		public AttractionAdapter(Context context, List<Attraction> attractions)
		{
			this.context = context;
			this.Attractions = attractions;
		}
		public override int ItemCount
		{
			get
			{
				return Attractions == null ? 0 : Attractions.Count;
			}
		}

		public List<Attraction> Attractions
		{
			get
			{
				return attractions;
			}

			set
			{
				attractions = value;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var attraction = Attractions[position];
			var viewHolder = holder as AttractionViewHolder;
			viewHolder.TitleTextView.Text = attraction.Name;
			viewHolder.DescriptionTextView.Text = attraction.Description;
			Koush.UrlImageViewHelper.SetUrlDrawable(viewHolder.ImageView, attraction.ImageUrl.AbsoluteUri);
			//TODO: Glide
			//	Glide.with(mContext)
			//		   .load(attraction.imageUrl)
			//		   .diskCacheStrategy(DiskCacheStrategy.SOURCE)
			//		   .placeholder(R.drawable.empty_photo)
			//		   .override(mImageSize, mImageSize)
			//                  .into(holder.mImageView);
			//TODO: Distance Utils
			String distance =  Utils.FormatDistanceBetween(AttractionListFragment.LatestLocation, attraction.Location);
			if (TextUtils.IsEmpty(distance))
			{
				viewHolder.OverlayTextView.Visibility = ViewStates.Gone;
			}
			else {
				viewHolder.OverlayTextView.Visibility = ViewStates.Visible;
				viewHolder.OverlayTextView.Text = distance;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var inflater = LayoutInflater.From(context);
			View view = inflater.Inflate(Resource.Layout.list_row, parent, false);
			return new AttractionViewHolder(view, OnItemClick);
		}


		public void OnItemClick(int position, View view)
		{
			if (!AttractionListFragment.IsItemClicked)
			{
				AttractionListFragment.IsItemClicked = true;
				var heroView = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
				DetailActivity.Launch(
					(Activity)context, attractions[position].Name, heroView);
			}
		}
	}


	public class AttractionViewHolder : RecyclerView.ViewHolder
	{

		TextView titleTextView;
		TextView descriptionTextView;
		TextView overlayTextView;
		ImageView imageView;

		public TextView TitleTextView
		{
			get
			{
				return titleTextView;
			}

			set
			{
				titleTextView = value;
			}
		}

		public TextView DescriptionTextView
		{
			get
			{
				return descriptionTextView;
			}

			set
			{
				descriptionTextView = value;
			}
		}

		public TextView OverlayTextView
		{
			get
			{
				return overlayTextView;
			}

			set
			{
				overlayTextView = value;
			}
		}

		public ImageView ImageView
		{
			get
			{
				return imageView;
			}

			set
			{
				imageView = value;
			}
		}

		public AttractionViewHolder(View view, Action<int, View> listener) : base(view)
		{

			TitleTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			DescriptionTextView = view.FindViewById<TextView>(Android.Resource.Id.Text2);
			OverlayTextView = view.FindViewById<TextView>(Resource.Id.overlaytext); ;
			ImageView = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
			view.Click +=  (sender, e) => listener(base.AdapterPosition, view);
		}

	}
}



