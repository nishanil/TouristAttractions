using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ToursitAttractions;

namespace TouristAttractions
{
	public class AttractionListFragment : Fragment
	{
		public AttractionListFragment()
		{
		}
	}

	public class AttractionAdapter : RecyclerView.Adapter, ViewHolder.IItemClickListener
	{
		public List<Attraction> attractions;
		private Context context;

		public AttractionAdapter(Context context, List<Attraction> attractions)
		{
			this.context = context;
			this.attractions = attractions;
		}
		public override int ItemCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			throw new NotImplementedException();
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var inflater = LayoutInflater.From(context);
			View view = inflater.Inflate(Resource.Layout.list_row, parent, false);
			return new ViewHolder(view, this);
		}

		public void OnItemClick(View view, int position)
		{
			throw new NotImplementedException();
		}
	}


	public class ViewHolder : RecyclerView.ViewHolder
	{

		TextView mTitleTextView;
		TextView mDescriptionTextView;
		TextView mOverlayTextView;
		ImageView mImageView;
		IItemClickListener mItemClickListener;


		public ViewHolder(View view, IItemClickListener itemClickListener) : base(view)
		{

			//mTitleTextView = view.FindViewById<TextView>(Android.Resource.Id.text1);
		//mDescriptionTextView = (TextView)view.findViewById(android.R.id.text2);
		//mOverlayTextView = (TextView)view.findViewById(R.id.overlaytext);
		//mImageView = (ImageView)view.findViewById(android.R.id.icon);
		//mItemClickListener = itemClickListener;
		//view.setOnClickListener(this);
		}


	//	public void onClick(View v)
	//{
	//	mItemClickListener.onItemClick(v, getAdapterPosition());
	//}


	public interface IItemClickListener
	{
		void OnItemClick(View view, int position);
	}

}



