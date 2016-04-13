using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;

namespace TouristAttractions
{
	public class AttractionsRecyclerView : RecyclerView
	{
		private View emptyView;

		public EmtpyAdapterDataObserver DataObserver { get; set; }

		public AttractionsRecyclerView(Context context) : base(context)
		{

		}

		public AttractionsRecyclerView(Context context, Android.Util.IAttributeSet attrs): base(context, attrs)
		{

		}
		public AttractionsRecyclerView(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{ }

		public void SetEmptyView(View emptyView)
		{
			this.emptyView = emptyView;
			DataObserver = new EmtpyAdapterDataObserver(this.UdpateEmptyView);
		}

		public override void SetAdapter(RecyclerView.Adapter adapter)
		{
			if (base.GetAdapter() != null)
			{
				base.GetAdapter().UnregisterAdapterDataObserver(DataObserver);

			}
			if (adapter != null) {
				adapter.RegisterAdapterDataObserver(DataObserver);
			}


			base.SetAdapter(adapter);
			UdpateEmptyView();
		}

		private void UdpateEmptyView()
		{
			if (emptyView != null && base.GetAdapter() != null)
			{
				bool showEmptyView = base.GetAdapter().ItemCount == 0;
				emptyView.Visibility = showEmptyView ? ViewStates.Visible : ViewStates.Gone;
				Visibility = showEmptyView ? ViewStates.Gone : ViewStates.Visible;
			}
		}

	}


	public class EmtpyAdapterDataObserver : RecyclerView.AdapterDataObserver
	{
		Action action;
		public EmtpyAdapterDataObserver(Action action)
		{
			this.action = action;
		}
		public override void OnChanged()
		{
			base.OnChanged();
			action();
		}
	}
}

