
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Security;
using Javax.Crypto;

namespace TouristAttractions
{
	public class FingerprintAuthenticationDialogFragment : DialogFragment, FingerprintUiHelper.Callback
	{
		FingerprintManager.CryptoObject mCryptoObject;
		FingerprintUiHelper mFingerprintUiHelper;
		View mFingerprintContent;


		FingerprintUiHelper.FingerprintUiHelperBuilder mFingerprintUiHelperBuilder;
		Button mCancelButton;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			RetainInstance = true;
			//SetStyle(DialogFragmentStyle.Normal, Android.Resource.Style.ThemeMaterialLightDialog);
		}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			Dialog.SetTitle("Touch to Check-in");
			var v = inflater.Inflate(Resource.Layout.fingerprint_dialog_container, container, false);
			mCancelButton = (Button)v.FindViewById(Resource.Id.cancel_button);
			mCancelButton.Click += (object sender, EventArgs e) => Dismiss();

			mFingerprintContent = v.FindViewById(Resource.Id.fingerprint_container);
			var fingerprintManager = (FingerprintManager)Context.GetSystemService(Context.FingerprintService);
			mFingerprintUiHelperBuilder = new FingerprintUiHelper.FingerprintUiHelperBuilder(fingerprintManager);
			mFingerprintUiHelper = mFingerprintUiHelperBuilder.Build(
				(ImageView)v.FindViewById(Resource.Id.fingerprint_icon),
				(TextView)v.FindViewById(Resource.Id.fingerprint_status), this);
			mCancelButton.Text = "Cancel";
			//mSecondDialogButton.Text = mSecondDialogButton.Resources.GetString(Resource.String.use_password);
			mFingerprintContent.Visibility = ViewStates.Visible;


			return v;
		}

		/// <summary>
		/// Sets the crypto object to be passed in when authenticating with fingerprint.
		/// </summary>
		/// <param name="cryptoObject">Crypto object.</param>
		public void SetCryptoObject(FingerprintManager.CryptoObject cryptoObject)
		{
			mCryptoObject = cryptoObject;
		}

		public override void OnResume()
		{
			base.OnResume();
			mFingerprintUiHelper.StartListening(mCryptoObject);

		}

		public override void OnPause()
		{
			base.OnPause();
			mFingerprintUiHelper.StopListening();

		}
		public void OnAuthenticated()
		{
			Toast.MakeText(Activity, "Check-in Success!", ToastLength.Short).Show();
			Dismiss();
		}

		public void OnError()
		{
			mFingerprintUiHelper.StopListening();

			//TODO: Do Something during errror
			Dismiss();
		}
	}
}

