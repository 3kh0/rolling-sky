using System;
using UnityEngine;

namespace Prime31
{
	public class AdMobAndroid
	{
		private static AndroidJavaObject _admobPlugin;

		static AdMobAndroid()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return;
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.AdMobPlugin"))
			{
				_admobPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
		}

		public static void setTagForChildDirectedTreatment(bool shouldTag)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("setTagForChildDirectedTreatment", shouldTag);
			}
		}

		public static void setTestDevices(string[] testDevices)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				IntPtr methodID = AndroidJNI.GetMethodID(_admobPlugin.GetRawClass(), "setTestDevices", "([Ljava/lang/String;)V");
				AndroidJNI.CallVoidMethod(_admobPlugin.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(new object[1] { testDevices }));
			}
		}

		public static void createBanner(string adUnitId, AdMobAndroidAd type, AdMobAdPlacement placement)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				createBanner(adUnitId, (int)type, (int)placement);
			}
		}

		public static void createBanner(string adUnitId, int type, int placement)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("createBanner", adUnitId, type, placement);
			}
		}

		public static void destroyBanner()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("destroyBanner");
			}
		}

		public static void hideBanner(bool shouldHide)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("hideBanner", shouldHide);
			}
		}

		public static void refreshAd()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("refreshAd");
			}
		}

		public static float getAdViewHeight()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return 0f;
			}
			return _admobPlugin.Call<float>("getAdViewHeight", new object[0]);
		}

		public static void requestInterstital(string interstitialUnitId)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("requestInterstital", interstitialUnitId);
			}
		}

		public static bool isInterstitalReady()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return false;
			}
			return _admobPlugin.Call<bool>("isInterstitalReady", new object[0]);
		}

		public static void displayInterstital()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_admobPlugin.Call("displayInterstital");
			}
		}
	}
}
