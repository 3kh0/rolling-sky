using System;

namespace Prime31
{
	public class AdMobAndroidManager : AbstractManager
	{
		public static event Action receivedAdEvent;

		public static event Action<string> failedToReceiveAdEvent;

		public static event Action dismissingScreenEvent;

		public static event Action leavingApplicationEvent;

		public static event Action presentingScreenEvent;

		public static event Action interstitialReceivedAdEvent;

		public static event Action interstitialDismissingScreenEvent;

		public static event Action<string> interstitialFailedToReceiveAdEvent;

		public static event Action interstitialLeavingApplicationEvent;

		public static event Action interstitialPresentingScreenEvent;

		static AdMobAndroidManager()
		{
			AbstractManager.initialize(typeof(AdMobAndroidManager));
		}

		public void dismissingScreen(string empty)
		{
			if (AdMobAndroidManager.dismissingScreenEvent != null)
			{
				AdMobAndroidManager.dismissingScreenEvent();
			}
		}

		public void failedToReceiveAd(string error)
		{
			if (AdMobAndroidManager.failedToReceiveAdEvent != null)
			{
				AdMobAndroidManager.failedToReceiveAdEvent(error);
			}
		}

		public void leavingApplication(string empty)
		{
			if (AdMobAndroidManager.leavingApplicationEvent != null)
			{
				AdMobAndroidManager.leavingApplicationEvent();
			}
		}

		public void presentingScreen(string empty)
		{
			if (AdMobAndroidManager.presentingScreenEvent != null)
			{
				AdMobAndroidManager.presentingScreenEvent();
			}
		}

		public void receivedAd(string empty)
		{
			if (AdMobAndroidManager.receivedAdEvent != null)
			{
				AdMobAndroidManager.receivedAdEvent();
			}
		}

		public void interstitialDismissingScreen(string empty)
		{
			if (AdMobAndroidManager.interstitialDismissingScreenEvent != null)
			{
				AdMobAndroidManager.interstitialDismissingScreenEvent();
			}
		}

		public void interstitialFailedToReceiveAd(string error)
		{
			if (AdMobAndroidManager.interstitialFailedToReceiveAdEvent != null)
			{
				AdMobAndroidManager.interstitialFailedToReceiveAdEvent(error);
			}
		}

		public void interstitialLeavingApplication(string empty)
		{
			if (AdMobAndroidManager.interstitialLeavingApplicationEvent != null)
			{
				AdMobAndroidManager.interstitialLeavingApplicationEvent();
			}
		}

		public void interstitialPresentingScreen(string empty)
		{
			if (AdMobAndroidManager.interstitialPresentingScreenEvent != null)
			{
				AdMobAndroidManager.interstitialPresentingScreenEvent();
			}
		}

		public void interstitialReceivedAd(string empty)
		{
			if (AdMobAndroidManager.interstitialReceivedAdEvent != null)
			{
				AdMobAndroidManager.interstitialReceivedAdEvent();
			}
		}
	}
}
