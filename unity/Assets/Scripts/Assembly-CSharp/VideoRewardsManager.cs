using Prime31;
using UnityEngine;
using UnityEngine.Advertisements;

public class VideoRewardsManager : MonoBehaviour
{
	private enum Network
	{
		UnityAds = 0,
		AdColony = 1,
		AppLovin = 4,
		Chartboost = 3,
		Admob = 2
	}

	private struct PrioritizedNetwork
	{
		public readonly Network Name;

		public readonly int Priority;

		public readonly bool Videos;

		public readonly bool Images;

		public PrioritizedNetwork(Network name, int priority, bool videoNetwork, bool imageNetwork)
		{
			Name = name;
			Priority = priority;
			Videos = videoNetwork;
			Images = imageNetwork;
		}
	}

	private delegate int FunctionToGetGamesPlayed();

	public delegate void RewardCallback(bool rewardEarned);

	private const bool debugsEnabled = false;

	private const bool debugInitialize = false;

	private const bool debugPrioritize = false;

	private const bool debugDeciding = false;

	private const bool debugShowing = false;

	private const bool useOfferInterval = false;

	private const string admobInterstitialID = "ca-app-pub-9397713323853180/8985834004";

	private const string unityAds_videoZone = "rewardedVideoZone";

	private const string unityAds_imageZone = "pictureZone";

	private const string adColony_AppVersion = "1.0";

	private const string appLovin_SdkId = "0wArT6_9lPWsN4L4veHN5cBohoLX2fg3Zsu2ztBhHiXeotkhV_3bcFNI1ir4NV7yL0-IuA-9uWpfNWLuWOT8dr";

	private const string unityAds_GameId = "96657";

	private const string adColony_AppId = "aappb626ed5b5a194eeeb0";

	private const string adColony_ZoneId = "vz997ef69841de414b89";

	private static FunctionToGetGamesPlayed GetGamesPlayed = PlayerProfiler.GetGameNumber;

	private static Persistant.Int gameOfLastOffer = new Persistant.Int("GLVO", "game of last reward video offer", -1);

	public static bool videosEnabled = true;

	public static bool imagesEnabled = false;

	public static bool unityAdsVideosEnabled = true;

	public static bool unityAdsImagesEnabled = false;

	public static int unityAdsPriority = 1;

	public static bool adColonyVideosEnabled = false;

	public static bool adColonyImagesEnabled = false;

	public static int adColonyPriority = 0;

	public static bool appLovinVideosEnabled = true;

	public static bool appLovinImagesEnabled = false;

	public static int appLovinPriority = 2;

	public static bool chartboostVideosEnabled = false;

	public static bool chartboostImagesEnabled = false;

	public static int chartboostPriority = 3;

	public static bool admobVideosEnabled = false;

	public static bool admobImagesEnabled = false;

	public static int admobPriority = 4;

	public static int gamesBeforeStartCaching = 0;

	public static int gamesBeforeFirstOffer = 2;

	public static int minGamesBetweenOffers = 2;

	public static int maxGamesBetweenOffers = 2;

	private static int offerInterval;

	private static bool usingRandomOfferInterval;

	private static bool intervalSettingsInitialized;

	private static bool unityAdsInitialized;

	private static bool appLovinInitialized;

	private static bool appLovinVideoIsCached;

	private static bool appLovinImageIsCached;

	private static bool appLovinVideoSuccess;

	private static bool adColonyInitialized;

	private static bool adColonyVideoIsCached;

	private static bool adColonyImageIsCached;

	private static bool chartboostInitialized;

	private static bool chartboostVideoIsCached;

	private static bool chartboostImageIsCached;

	private static bool activeNetworksPrioritized;

	private static VideoRewardsManager thisScript;

	private static bool thisScriptInitialized;

	private static bool initialized;

	private static ArrayUtils.List<PrioritizedNetwork> activeNetworks;

	private static RewardCallback rewardCallbackFunction;

	private static bool videoRewardsDisabled
	{
		get
		{
			return !videosEnabled;
		}
	}

	private static bool imageRewardsDisabled
	{
		get
		{
			return !imagesEnabled;
		}
	}

	private static bool delayedCachingEnabled
	{
		get
		{
			return gamesBeforeStartCaching > 0;
		}
	}

	public static bool VideoRewardsEnabled
	{
		get
		{
			return videosEnabled;
		}
	}

	public static bool VideoRewardsDisabled
	{
		get
		{
			return videoRewardsDisabled;
		}
	}

	public static void OnRefreshServerParameters()
	{
		if (!initialized)
		{
			Initialize();
		}
		else
		{
			thisScript.InitializeAds();
		}
	}

	private static void DiscardCallbacks()
	{
		if (rewardCallbackFunction != null)
		{
			rewardCallbackFunction = null;
		}
	}

	private static void Reward(bool rewardEarned)
	{
		if (rewardCallbackFunction != null)
		{
			rewardCallbackFunction(rewardEarned);
			rewardCallbackFunction = null;
		}
	}

	private static void SetupStaticDelegates()
	{
	}

	public static void Initialize()
	{
		if (!initialized)
		{
			InitializeThisScript();
			thisScript.InitializeAds();
			initialized = true;
		}
	}

	private void Awake()
	{
		if (!initialized)
		{
			thisScript = base.gameObject.GetComponent<VideoRewardsManager>();
			Initialize();
		}
	}

	private void OnLevelWasLoaded()
	{
		DiscardCallbacks();
	}

	private void OnDisable()
	{
		DiscardCallbacks();
		thisScript = null;
		thisScriptInitialized = false;
	}

	public static bool CanShowVideo()
	{
		string reason = null;
		bool result;
		if (videoRewardsDisabled)
		{
			result = false;
			reason = "video rewards is currently DISABLED";
		}
		else if (activeNetworks == null || activeNetworks.IsEmpty)
		{
			result = false;
			reason = "no ad-networks are enabled";
		}
		else if (HaveVideoReady())
		{
			result = TimeToOfferVideo(out reason);
		}
		else
		{
			result = false;
			reason = "no video is cached and ready to be offer";
		}
		return result;
	}

	public static bool CanShowImage()
	{
		string text = null;
		bool result;
		if (imageRewardsDisabled)
		{
			result = false;
			text = "image rewards is currently DISABLED";
		}
		else if (activeNetworks == null || activeNetworks.IsEmpty)
		{
			result = false;
			text = "no ad-networks are enabled";
		}
		else if (HaveImageReady())
		{
			result = true;
		}
		else
		{
			result = false;
			text = "no image is cached and ready to be offer";
		}
		return result;
	}

	public static void ShowVideoForReward(RewardCallback rewardCallback)
	{
		if (!videosEnabled)
		{
			return;
		}
		rewardCallbackFunction = rewardCallback;
		TryPrioritizeNetworks();
		string value = null;
		bool flag = false;
		for (int i = 0; i < activeNetworks.Length; i++)
		{
			Network network = activeNetworks[i].Name;
			if (SuccesfullyShowedVideoFrom(network))
			{
				flag = true;
				value = ((i != 0) ? "backup " : "prioritized ");
				value += network;
				break;
			}
		}
		if (flag)
		{
			string.IsNullOrEmpty(value);
			gameOfLastOffer.Set(GetGamesPlayed());
		}
		else
		{
			Debug.LogError("VIDEO REWARDS: failed to show any reward-video ad as none were ready - this should have been checked before attempting to show, using the function VideoRewardsManager.CanOfferVideo()");
		}
	}

	public static void ShowImage()
	{
		if (!imagesEnabled)
		{
			return;
		}
		TryPrioritizeNetworks();
		string value = null;
		bool flag = false;
		for (int i = 0; i < activeNetworks.Length; i++)
		{
			Network network = activeNetworks[i].Name;
			if (SuccesfullyShowedImageFrom(network))
			{
				flag = true;
				value = ((i != 0) ? "backup " : "prioritized ");
				value += network;
				break;
			}
		}
		if (flag)
		{
			string.IsNullOrEmpty(value);
		}
		else
		{
			Debug.LogError("VIDEO REWARDS: failed to show any images ad as none were ready - this should have been checked before attempting to show, using the function VideoRewardsManager.CanOfferImage()");
		}
	}

	private static void InitializeThisScript()
	{
		if (!thisScriptInitialized)
		{
			thisScriptInitialized = true;
			if (thisScript == null)
			{
				thisScript = new GameObject("Video Rewards Manager").AddComponent<VideoRewardsManager>();
			}
			Object.DontDestroyOnLoad(thisScript.gameObject);
			SetupStaticDelegates();
		}
	}

	private void InitializeAds()
	{
		bool flag = true;
		int num = 0;
		if (delayedCachingEnabled)
		{
			num = GetGamesPlayed();
			flag = num >= gamesBeforeStartCaching;
		}
		if (flag && (videosEnabled || imagesEnabled) && (unityAdsVideosEnabled || adColonyVideosEnabled || appLovinVideosEnabled || chartboostVideosEnabled || unityAdsImagesEnabled || adColonyImagesEnabled || appLovinImagesEnabled || chartboostImagesEnabled))
		{
			if (activeNetworks == null)
			{
				activeNetworks = new ArrayUtils.List<PrioritizedNetwork>(EnumUtils.TotalEnums<Network>());
			}
			else
			{
				activeNetworks.Clear();
			}
			activeNetworksPrioritized = false;
			if (InitializeUnityAds())
			{
				activeNetworks.Add(new PrioritizedNetwork(Network.UnityAds, unityAdsPriority, unityAdsVideosEnabled, unityAdsImagesEnabled));
			}
			if (InitializeAdColony())
			{
				activeNetworks.Add(new PrioritizedNetwork(Network.AdColony, adColonyPriority, adColonyVideosEnabled, adColonyImagesEnabled));
			}
			if (InitializeAppLovin())
			{
				activeNetworks.Add(new PrioritizedNetwork(Network.AppLovin, appLovinPriority, appLovinVideosEnabled, appLovinImagesEnabled));
			}
			if (InitializeChartboost())
			{
				activeNetworks.Add(new PrioritizedNetwork(Network.Chartboost, chartboostPriority, chartboostVideosEnabled, chartboostImagesEnabled));
			}
			if (InitializeAdmob())
			{
				activeNetworks.Add(new PrioritizedNetwork(Network.Admob, admobPriority, admobVideosEnabled, admobImagesEnabled));
			}
			InitializeIntervalSettings();
		}
	}

	private bool InitializeUnityAds()
	{
		/*if (!unityAdsInitialized && (unityAdsVideosEnabled || unityAdsImagesEnabled) && Advertisement.isSupported)
		{
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("96657");
			unityAdsInitialized = true;
		}
		return unityAdsInitialized;*/
		return false;
	}

	private bool InitializeAdColony()
	{
		return adColonyInitialized;
	}

	private bool InitializeAppLovin()
	{
		/*if (!appLovinInitialized && (appLovinVideosEnabled || appLovinImagesEnabled))
		{
			AppLovin.SetSdkKey("0wArT6_9lPWsN4L4veHN5cBohoLX2fg3Zsu2ztBhHiXeotkhV_3bcFNI1ir4NV7yL0-IuA-9uWpfNWLuWOT8dr");
			AppLovin.InitializeSdk();
			AppLovin.SetUnityAdListener(base.gameObject.name);
			appLovinInitialized = true;
			AppLovin.PreloadInterstitial();
			AppLovin.LoadRewardedInterstitial();
			Debug.Log("VRMN: DEBUG: Initialized AppLovin");
		}*/
		return false;
	}

	private bool InitializeChartboost()
	{
		return chartboostInitialized;
	}

	private bool InitializeAdmob()
	{
		AdMobAndroid.requestInterstital("ca-app-pub-9397713323853180/8985834004");
		return true;
	}

	private string InitializeIntervalSettings()
	{
		return null;
	}

	private static void TryPrioritizeNetworks()
	{
		int length = activeNetworks.Length;
		if (!activeNetworksPrioritized)
		{
			for (int i = 0; i < length - 1; i++)
			{
				for (int j = 0; j < length - (1 + i); j++)
				{
					if (activeNetworks[j + 1].Priority > activeNetworks[j].Priority)
					{
						PrioritizedNetwork value = activeNetworks[j];
						activeNetworks[j] = activeNetworks[j + 1];
						activeNetworks[j + 1] = value;
					}
				}
			}
			activeNetworksPrioritized = true;
		}
		string text = null;
		for (int k = 0; k < length; k++)
		{
			text = ((!string.IsNullOrEmpty(text)) ? (text + ", " + activeNetworks[k].Name) : activeNetworks[k].Name.ToString());
		}
	}

	private static bool HaveVideoReady()
	{
		bool result = false;
		if (videosEnabled && (UnityAdsHasVideoReady() || AdColonyHasVideoReady() || AppLovinHasVideoReady() || ChartboostHasVideoReady()))
		{
			result = true;
		}
		return result;
	}

	private static bool HaveImageReady()
	{
		bool result = false;
		if (imagesEnabled && (UnityAdsHasImageReady() || AdColonyHasImageReady() || AppLovinHasImageReady() || ChartboostHasImageReady() || AdmobHasImageReady()))
		{
			result = true;
		}
		return result;
	}

	private static bool TimeToOfferVideo(out string reason)
	{
		bool flag = false;
		flag = true;
		reason = "no restrictions are placed on when to offer a video";
		return flag;
	}

	private static bool UnityAdsHasVideoReady()
	{
		return unityAdsVideosEnabled && Advertisement.IsReady("rewardedVideoZone");
	}

	private static bool UnityAdsHasImageReady()
	{
		return unityAdsImagesEnabled && Advertisement.IsReady("pictureZone");
	}

	private static bool AdColonyHasVideoReady()
	{
		return adColonyVideosEnabled && adColonyVideoIsCached;
	}

	private static bool AdColonyHasImageReady()
	{
		return adColonyImagesEnabled && adColonyImageIsCached;
	}

	private static bool AppLovinHasVideoReady()
	{
		return appLovinVideosEnabled && (appLovinVideoIsCached || AppLovin.IsIncentInterstitialReady());
	}

	private static bool AppLovinHasImageReady()
	{
		return appLovinImagesEnabled && AppLovin.HasPreloadedInterstitial();
	}

	private static bool ChartboostHasVideoReady()
	{
		return false;
	}

	private static bool ChartboostHasImageReady()
	{
		return false;
	}

	private static bool AdmobHasImageReady()
	{
		return AdMobAndroid.isInterstitalReady();
	}

	private static void ShowAdmobImage()
	{
		AdMobAndroid.displayInterstital();
		AdMobAndroid.requestInterstital("ca-app-pub-9397713323853180/8985834004");
	}

	private static bool SuccesfullyShowedVideoFrom(Network networkToTry)
	{
		bool result = false;
		switch (networkToTry)
		{
		case Network.UnityAds:
			if (UnityAdsHasVideoReady())
			{
				ShowUnityAdsVideo();
				result = true;
			}
			break;
		case Network.AdColony:
			if (AdColonyHasVideoReady())
			{
				ShowAdColonyVideo();
				result = true;
			}
			break;
		case Network.AppLovin:
			if (AppLovinHasVideoReady())
			{
				ShowAppLovinVideo();
				result = true;
			}
			break;
		case Network.Chartboost:
			if (ChartboostHasVideoReady())
			{
				ShowChartboostVideo();
				result = true;
			}
			break;
		default:
			Debug.LogError(string.Format("VIDEO REWARDS ERROR: recieved unexpected Network type value of {0} for VideoRewardsManager.SuccesfullyShowedVideoFrom(Network networkToTry) - check case statement to include this Network type", networkToTry.ToString()));
			break;
		}
		return result;
	}

	private static bool SuccesfullyShowedImageFrom(Network networkToTry)
	{
		bool result = false;
		switch (networkToTry)
		{
		case Network.UnityAds:
			if (UnityAdsHasImageReady())
			{
				ShowUnityAdsImage();
				result = true;
			}
			break;
		case Network.AppLovin:
			if (AppLovinHasImageReady())
			{
				ShowAppLovinImage();
				result = true;
			}
			break;
		case Network.Chartboost:
			if (ChartboostHasImageReady())
			{
				ShowChartboostImage();
				result = true;
			}
			break;
		case Network.Admob:
			if (AdmobHasImageReady())
			{
				ShowAdmobImage();
				result = true;
			}
			break;
		default:
			Debug.LogError(string.Format("VIDEO REWARDS ERROR: recieved unexpected Network type value of {0} for VideoRewardsManager.SuccesfullyShowedImageFrom(Network networkToTry) - check case statement to include this Network type", networkToTry.ToString()));
			break;
		}
		return result;
	}

	private static void ShowUnityAdsVideo()
	{
		ShowOptions showOptions = new ShowOptions();
		showOptions.resultCallback = UnityAdsVideoCallback;
		Advertisement.Show("rewardedVideoZone", showOptions);
	}

	private static void ShowUnityAdsImage()
	{
		ShowOptions showOptions = new ShowOptions();
		showOptions.resultCallback = UnityAdsImageCallback;
		Advertisement.Show("pictureZone", showOptions);
	}

	private static void ShowAdColonyVideo()
	{
	}

	private static void ShowAppLovinVideo()
	{
		AppLovin.ShowRewardedInterstitial();
	}

	private static void ShowAppLovinImage()
	{
		AppLovin.ShowInterstitial();
	}

	private static void ShowChartboostVideo()
	{
	}

	private static void ShowChartboostImage()
	{
	}

	private static void UnityAdsVideoCallback(ShowResult result)
	{
		bool rewardEarned = false;
		switch (result)
		{
		case ShowResult.Finished:
			rewardEarned = true;
			break;
		}
		Reward(rewardEarned);
	}

	private static void UnityAdsImageCallback(ShowResult result)
	{
	}

	private void onAppLovinEventReceived(string recievedEvent)
	{
		if (recievedEvent.Contains("REWARDAPPROVEDINFO"))
		{
			appLovinVideoSuccess = true;
		}
		else if (recievedEvent.Contains("LOADEDREWARDED"))
		{
			appLovinVideoIsCached = true;
		}
		else if (recievedEvent.Contains("LOADREWARDEDFAILED"))
		{
			appLovinVideoIsCached = false;
		}
		else if (recievedEvent.Contains("HIDDENREWARDED"))
		{
			if (appLovinVideoSuccess)
			{
				Reward(true);
				appLovinVideoSuccess = true;
			}
			appLovinVideoIsCached = false;
			AppLovin.LoadRewardedInterstitial();
		}
		if (!recievedEvent.Contains("DISPLAYEDINTER"))
		{
			if (recievedEvent.Contains("HIDDENINTER"))
			{
				appLovinImageIsCached = false;
				AppLovin.PreloadInterstitial();
			}
			else if (recievedEvent.Contains("LOADEDINTER"))
			{
				appLovinImageIsCached = true;
			}
			else if (string.Equals(recievedEvent, "LOADINTERFAILED"))
			{
				appLovinImageIsCached = false;
			}
		}
	}
}
