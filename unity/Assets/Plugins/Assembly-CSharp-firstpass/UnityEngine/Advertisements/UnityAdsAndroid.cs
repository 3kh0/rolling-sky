namespace UnityEngine.Advertisements
{
	internal class UnityAdsAndroid : UnityAdsPlatform
	{
		private static AndroidJavaObject unityAds;

		private static AndroidJavaObject unityAdsUnity;

		private static AndroidJavaObject currentActivity;

		private static bool wrapperInitialized;

		private AndroidJavaObject getAndroidWrapper()
		{
			if (!wrapperInitialized)
			{
				wrapperInitialized = true;
				unityAdsUnity = new AndroidJavaObject("com.unity3d.ads.android.unity3d.UnityAdsUnityWrapper");
			}
			return unityAdsUnity;
		}

		public override void init(string gameId, bool testModeEnabled, string gameObjectName, string unityVersion)
		{
			Utils.LogDebug("UnityAndroid: init(), gameId=" + gameId + ", testModeEnabled=" + testModeEnabled + ", gameObjectName=" + gameObjectName);
			if (Advertisement.UnityDeveloperInternalTestMode)
			{
				getAndroidWrapper().Call("enableUnityDeveloperInternalTestMode");
			}
			currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			getAndroidWrapper().Call("init", gameId, currentActivity, testModeEnabled, (int)Advertisement.debugLevel, gameObjectName, unityVersion);
		}

		public override bool show(string zoneId, string rewardItemKey, string options)
		{
			Utils.LogDebug("UnityAndroid: show()");
			return getAndroidWrapper().Call<bool>("show", new object[3] { zoneId, rewardItemKey, options });
		}

		public override void hide()
		{
			Utils.LogDebug("UnityAndroid: hide()");
			getAndroidWrapper().Call("hide");
		}

		public override bool isSupported()
		{
			Utils.LogDebug("UnityAndroid: isSupported()");
			return getAndroidWrapper().Call<bool>("isSupported", new object[0]);
		}

		public override string getSDKVersion()
		{
			Utils.LogDebug("UnityAndroid: getSDKVersion()");
			return getAndroidWrapper().Call<string>("getSDKVersion", new object[0]);
		}

		public override bool canShowZone(string zone)
		{
			return getAndroidWrapper().Call<bool>("canShowZone", new object[1] { zone });
		}

		public override bool hasMultipleRewardItems()
		{
			Utils.LogDebug("UnityAndroid: hasMultipleRewardItems()");
			return getAndroidWrapper().Call<bool>("hasMultipleRewardItems", new object[0]);
		}

		public override string getRewardItemKeys()
		{
			Utils.LogDebug("UnityAndroid: getRewardItemKeys()");
			return getAndroidWrapper().Call<string>("getRewardItemKeys", new object[0]);
		}

		public override string getDefaultRewardItemKey()
		{
			Utils.LogDebug("UnityAndroid: getDefaultRewardItemKey()");
			return getAndroidWrapper().Call<string>("getDefaultRewardItemKey", new object[0]);
		}

		public override string getCurrentRewardItemKey()
		{
			Utils.LogDebug("UnityAndroid: getCurrentRewardItemKey()");
			return getAndroidWrapper().Call<string>("getCurrentRewardItemKey", new object[0]);
		}

		public override bool setRewardItemKey(string rewardItemKey)
		{
			Utils.LogDebug("UnityAndroid: setRewardItemKey() rewardItemKey=" + rewardItemKey);
			return getAndroidWrapper().Call<bool>("setRewardItemKey", new object[1] { rewardItemKey });
		}

		public override void setDefaultRewardItemAsRewardItem()
		{
			Utils.LogDebug("UnityAndroid: setDefaultRewardItemAsRewardItem()");
			getAndroidWrapper().Call("setDefaultRewardItemAsRewardItem");
		}

		public override string getRewardItemDetailsWithKey(string rewardItemKey)
		{
			Utils.LogDebug("UnityAndroid: getRewardItemDetailsWithKey() rewardItemKey=" + rewardItemKey);
			return getAndroidWrapper().Call<string>("getRewardItemDetailsWithKey", new object[1] { rewardItemKey });
		}

		public override string getRewardItemDetailsKeys()
		{
			Utils.LogDebug("UnityAndroid: getRewardItemDetailsKeys()");
			return getAndroidWrapper().Call<string>("getRewardItemDetailsKeys", new object[0]);
		}

		public override void setLogLevel(Advertisement.DebugLevel logLevel)
		{
			Utils.LogDebug("UnityAndroid: setLogLevel()");
			getAndroidWrapper().Call("setLogLevel", (int)logLevel);
		}
	}
}
