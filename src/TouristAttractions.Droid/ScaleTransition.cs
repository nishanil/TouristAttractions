using System;
using Android.Animation;
using Android.Content;
using Android.Transitions;
using Android.Util;
using Android.Views;

namespace TouristAttractions
{
	/// <summary>
	/// A simple scale transition class to allow an element to scale in or out.
 	/// This is used by the floating action button on the attraction detail screen
 	/// when it appears and disappears during the Activity transitions.
	/// </summary>
	public class ScaleTransition : Android.Transitions.Visibility
	{
		public ScaleTransition(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
		{
			
		}

		public override Android.Animation.Animator OnAppear(Android.Views.ViewGroup sceneRoot, Android.Views.View view, TransitionValues startValues, TransitionValues endValues)
		{
			return CreateAnimation(view, 0, 1);
		}

		public override Android.Animation.Animator OnDisappear(Android.Views.ViewGroup sceneRoot, Android.Views.View view, TransitionValues startValues, TransitionValues endValues)
		{
			return CreateAnimation(view, 1, 0);	
		}

		public Animator CreateAnimation(View view, float startScale, float endScale)
		{
			view.ScaleX = startScale;
			view.ScaleY = startScale;
			PropertyValuesHolder holderX = PropertyValuesHolder.OfFloat("scaleX", startScale, endScale);
			PropertyValuesHolder holderY = PropertyValuesHolder.OfFloat("scaleY", startScale, endScale);
			return ObjectAnimator.OfPropertyValuesHolder(view, holderX, holderY);
		}
	}
}

