using UnityEngine;

namespace Prime31
{
	public class FlurryAds
	{
		private static AndroidJavaObject _plugin;

		static FlurryAds()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return;
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.ads.FlurryAds"))
			{
				_plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
		}

		public static void enableAds(bool enableTestAds = false)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("enableAds", enableTestAds);
			}
		}

		public static void fetchAdsForSpace(string adSpace, FlurryAdPlacement adSize)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("fetchAdsForSpace", adSpace, (int)adSize);
			}
		}

		public static void displayAd(string adSpace, FlurryAdPlacement adSize, long timeout)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("displayAd", adSpace, (int)adSize, timeout);
			}
		}

		public static void removeAd(string adSpace)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("removeAd", adSpace);
			}
		}

		public static void checkIfAdIsAvailable(string adSpace, FlurryAdPlacement adSize, long timeout)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("isAdAvailable", adSpace, (int)adSize, timeout);
			}
		}
	}
}
