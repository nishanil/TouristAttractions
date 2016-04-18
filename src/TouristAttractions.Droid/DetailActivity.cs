
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Runtime;
using Android.Security.Keystore;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Security;
using Java.Security.Cert;
using Javax.Crypto;

namespace TouristAttractions
{
	[Activity(Label="", Theme = "@style/XYZAppTheme.Detail", ParentActivity=typeof(AttractionListActivity))]
	public class DetailActivity : AppCompatActivity
	{
		private static readonly string EXTRA_ATTRACTION = "attraction";

		static readonly string KEY_NAME = "my_key";
		static readonly int FINGERPRINT_PERMISSION_REQUEST_CODE = 0;
		public bool IsFingerPrintReady { get; set; }

		FingerprintModule fingerprintModule;
		KeyguardManager mKeyguardManager;
		FingerprintManager mFingerprintManager;
		FingerprintAuthenticationDialogFragment mFragment;
		KeyStore mKeyStore;
		KeyGenerator mKeyGenerator;
		public Cipher mCipher;

		public static void Launch(Activity activity, string attraction, View heroView)
		{
			Intent intent = GetLaunchIntent(activity, attraction);
			if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				var options = ActivityOptions.MakeSceneTransitionAnimation(
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

			fingerprintModule = new FingerprintModule(this);
			mKeyguardManager = fingerprintModule.ProvidesKeyguardManager(this);
			mKeyStore = fingerprintModule.ProvidesKeystore();
			mKeyGenerator = fingerprintModule.ProvidesKeyGenerator();
			mCipher = fingerprintModule.ProvidesCipher(mKeyStore);

			RequestPermissions(new[] { Manifest.Permission.UseFingerprint }, FINGERPRINT_PERMISSION_REQUEST_CODE);

		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			if (requestCode == FINGERPRINT_PERMISSION_REQUEST_CODE && grantResults[0] == Android.Content.PM.Permission.Granted)
			{
				if (!mKeyguardManager.IsKeyguardSecure)
				{
					IsFingerPrintReady = false;
					// Show a message that the user hasn't set up a fingerprint or lock screen.
					Toast.MakeText(this, "Secure lock screen hasn't set up.\n"
						+ "Go to 'Settings -> Security -> Fingerprint' to set up a fingerprint", ToastLength.Long).Show();
					return;
				}
				mFingerprintManager = (FingerprintManager)GetSystemService(Context.FingerprintService);
				if (!mFingerprintManager.HasEnrolledFingerprints)
				{
					IsFingerPrintReady = false;
					// This happens when no fingerprints are registered.
					Toast.MakeText(this, "Go to 'Settings -> Security -> Fingerprint' " +
						"and register at least one fingerprint", ToastLength.Long).Show();
					return;
				}

				CreateKey();

				IsFingerPrintReady = true;
			}
		}

		public bool InitCipher()
		{
			try
			{
				mKeyStore.Load(null);
				var key = mKeyStore.GetKey(KEY_NAME, null);
				mCipher.Init(CipherMode.EncryptMode, key);
				return true;
			}
			catch (KeyPermanentlyInvalidatedException)
			{
				return false;
			}
			catch (KeyStoreException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
			catch (CertificateException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
			catch (UnrecoverableKeyException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
			catch (IOException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
			catch (NoSuchAlgorithmException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
			catch (InvalidKeyException e)
			{
				throw new RuntimeException("Failed to init Cipher", e);
			}
		}

		/// <summary>
		/// Creates a symmetric key in the Android Key Store which can only be used after the user 
		/// has authenticated with fingerprint.
		/// </summary>
		public void CreateKey()
		{
			// The enrolling flow for fingerprint. This is where you ask the user to set up fingerprint
			// for your flow. Use of keys is necessary if you need to know if the set of
			// enrolled fingerprints has changed.
			try
			{
				mKeyStore.Load(null);
				// Set the alias of the entry in Android KeyStore where the key will appear
				// and the constrains (purposes) in the constructor of the Builder
				mKeyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME,
					KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
					.SetBlockModes(KeyProperties.BlockModeCbc)
					// Require the user to authenticate with a fingerprint to authorize every use
					// of the key
					.SetUserAuthenticationRequired(true)
					.SetEncryptionPaddings(KeyProperties.EncryptionPaddingPkcs7)
					.Build());
				mKeyGenerator.GenerateKey();
			}
			catch (NoSuchAlgorithmException e)
			{
				throw new RuntimeException(e);
			}
			catch (InvalidAlgorithmParameterException e)
			{
				throw new RuntimeException(e);
			}
			catch (CertificateException e)
			{
				throw new RuntimeException(e);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
		}

	}
}

