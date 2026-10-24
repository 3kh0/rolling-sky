using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AppLovin
{
	public const float AD_POSITION_CENTER = -10000f;

	public const float AD_POSITION_LEFT = -20000f;

	public const float AD_POSITION_RIGHT = -30000f;

	public const float AD_POSITION_TOP = -40000f;

	public const float AD_POSITION_BOTTOM = -50000f;

	private const char _InternalPrimarySeparator = '\u001c';

	private const char _InternalSecondarySeparator = '\u001d';

	public AndroidJavaClass applovinFacade = new AndroidJavaClass("com.applovin.sdk.unity.AppLovinFacade");

	public AndroidJavaObject currentActivity;

	public static AppLovin DefaultPlugin;

	public AppLovinTargetingData targetingData;

	public AppLovin(AndroidJavaObject activity)
	{
		if (activity == null)
		{
			throw new MissingReferenceException("No parent activity specified");
		}
		currentActivity = activity;
		targetingData = new AppLovinTargetingData(currentActivity);
	}

	public AppLovin()
	{
		targetingData = new AppLovinTargetingData();
	}

	public static AppLovin getDefaultPlugin()
	{
		if (DefaultPlugin == null && !Application.isEditor)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			DefaultPlugin = new AppLovin(androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"));
		}
		return DefaultPlugin;
	}

	public AppLovinTargetingData getTargetingData()
	{
		return targetingData;
	}

	public void initializeSdk()
	{
		applovinFacade.CallStatic("InitializeSdk", currentActivity);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public void showAd()
	{
		applovinFacade.CallStatic("ShowAd", currentActivity);
	}

	public void showInterstitial()
	{
		applovinFacade.CallStatic("ShowInterstitial", currentActivity);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public void hideAd()
	{
		applovinFacade.CallStatic("HideAd", currentActivity);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public void setAdPosition(float x, float y)
	{
		applovinFacade.CallStatic("SetAdPosition", currentActivity, x, y);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public void setAdWidth(int width)
	{
		applovinFacade.CallStatic("SetAdWidth", currentActivity, width);
	}

	public void setVerboseLoggingOn(string sdkKey)
	{
		applovinFacade.CallStatic("SetVerboseLoggingOn", "true");
	}

	public void setSdkKey(string sdkKey)
	{
		applovinFacade.CallStatic("SetSdkKey", currentActivity, sdkKey);
	}

	public void preloadInterstitial()
	{
		applovinFacade.CallStatic("PreloadInterstitial", currentActivity);
	}

	public bool hasPreloadedInterstitial()
	{
		string value = applovinFacade.CallStatic<string>("IsInterstitialReady", new object[1] { currentActivity });
		return bool.Parse(value);
	}

	public bool isInterstitialShowing()
	{
		string value = applovinFacade.CallStatic<string>("IsInterstitialShowing", new object[1] { currentActivity });
		return bool.Parse(value);
	}

	public void setAdListener(string gameObjectToNotify)
	{
		applovinFacade.CallStatic("SetUnityAdListener", gameObjectToNotify);
	}

	public void setRewardedVideoUsername(string username)
	{
		applovinFacade.CallStatic("SetIncentivizedUsername", currentActivity, username);
	}

	public void loadIncentInterstitial()
	{
		applovinFacade.CallStatic("LoadIncentInterstitial", currentActivity);
	}

	public void showIncentInterstitial()
	{
		applovinFacade.CallStatic("ShowIncentInterstitial", currentActivity);
	}

	public bool isIncentInterstitialReady()
	{
		string value = applovinFacade.CallStatic<string>("IsIncentReady", new object[1] { currentActivity });
		return bool.Parse(value);
	}

	public bool isPreloadedInterstitialVideo()
	{
		string value = applovinFacade.CallStatic<string>("IsCurrentInterstitialVideo", new object[1] { currentActivity });
		return bool.Parse(value);
	}

	public void trackEvent(string eventType, IDictionary<string, string> parameters)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (parameters != null)
		{
			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				if (parameter.Key != null && parameter.Value != null)
				{
					stringBuilder.Append(parameter.Key);
					stringBuilder.Append('\u001d');
					stringBuilder.Append(parameter.Value);
					stringBuilder.Append('\u001c');
				}
			}
		}
		applovinFacade.CallStatic("TrackEvent", currentActivity, eventType, stringBuilder.ToString());
	}

	public void enableImmersiveMode()
	{
		applovinFacade.CallStatic("EnableImmersiveMode", currentActivity);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public static void ShowAd()
	{
		getDefaultPlugin().showAd();
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public static void ShowAd(float x, float y)
	{
		SetAdPosition(x, y);
		ShowAd();
	}

	public static void ShowInterstitial()
	{
		getDefaultPlugin().showInterstitial();
	}

	public static void LoadRewardedInterstitial()
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().loadIncentInterstitial();
		}
	}

	public static void ShowRewardedInterstitial()
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().showIncentInterstitial();
		}
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public static void HideAd()
	{
		getDefaultPlugin().hideAd();
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public static void SetAdPosition(float x, float y)
	{
		getDefaultPlugin().setAdPosition(x, y);
	}

	[Obsolete("Banners are deprecated and will be removed in a future SDK version. Consider using interstitials or rewarded videos instead.")]
	public static void SetAdWidth(int width)
	{
		getDefaultPlugin().setAdWidth(width);
	}

	public static void SetSdkKey(string sdkKey)
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().setSdkKey(sdkKey);
		}
	}

	public static void SetVerboseLoggingOn(string verboseLogging)
	{
		getDefaultPlugin().setVerboseLoggingOn(verboseLogging);
	}

	public static AppLovinTargetingData GetTargetingData()
	{
		return getDefaultPlugin().getTargetingData();
	}

	public static void PreloadInterstitial()
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().preloadInterstitial();
		}
	}

	public static bool HasPreloadedInterstitial()
	{
		return getDefaultPlugin().hasPreloadedInterstitial();
	}

	public static bool IsInterstitialShowing()
	{
		return getDefaultPlugin().isInterstitialShowing();
	}

	public static bool IsIncentInterstitialReady()
	{
		return false;
		return getDefaultPlugin().isIncentInterstitialReady();
	}

	public static bool IsPreloadedInterstitialVideo()
	{
		return getDefaultPlugin().isPreloadedInterstitialVideo();
	}

	public static void InitializeSdk()
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().initializeSdk();
		}
	}

	public static void SetUnityAdListener(string gameObjectToNotify)
	{
		if (!Application.isEditor)
		{
			getDefaultPlugin().setAdListener(gameObjectToNotify);
		}
	}

	public static void SetRewardedVideoUsername(string username)
	{
		getDefaultPlugin().setRewardedVideoUsername(username);
	}

	public static void TrackEvent(string eventType, IDictionary<string, string> parameters)
	{
		getDefaultPlugin().trackEvent(eventType, parameters);
	}

	public static void EnableImmersiveMode()
	{
		getDefaultPlugin().enableImmersiveMode();
	}
}
