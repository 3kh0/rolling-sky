using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Advertisements.Optional;

namespace UnityEngine.Advertisements
{
	internal class UnityAds : MonoBehaviour
	{
		public static bool isShowing = false;

		public static bool isInitialized = false;

		public static bool allowPrecache = true;

		private static bool initCalled = false;

		private static UnityAds sharedInstance;

		private static string _rewardItemNameKey = string.Empty;

		private static string _rewardItemPictureKey = string.Empty;

		private static bool _resultDelivered = false;

		private static Action<ShowResult> resultCallback = null;

		private static string _versionString = Application.unityVersion + "_" + Advertisement.version;

		public static UnityAds SharedInstance
		{
			get
			{
				if (!sharedInstance)
				{
					sharedInstance = (UnityAds)Object.FindObjectOfType(typeof(UnityAds));
				}
				if (!sharedInstance)
				{
					GameObject gameObject = new GameObject();
					gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
					GameObject gameObject2 = gameObject;
					sharedInstance = gameObject2.AddComponent<UnityAds>();
					gameObject2.name = "UnityAdsPluginBridgeObject";
					Object.DontDestroyOnLoad(gameObject2);
				}
				return sharedInstance;
			}
		}

		public void Init(string gameId, bool testModeEnabled)
		{
			if (initCalled)
			{
				return;
			}
			initCalled = true;
			try
			{
				if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					Utils.LogError("Internet not reachable, can't initialize ads");
					return;
				}
				IPHostEntry hostEntry = Dns.GetHostEntry("impact.applifier.com");
				if (hostEntry.AddressList.Length == 1 && hostEntry.AddressList[0].Equals(new IPAddress(new byte[4] { 127, 0, 0, 1 })))
				{
					Utils.LogError("Video ad server resolves to localhost (due to ad blocker?), can't initialize ads");
					return;
				}
			}
			catch (Exception ex)
			{
				Utils.LogDebug("Exception during connectivity check: " + ex.Message);
			}
			UnityAdsExternal.init(gameId, testModeEnabled, SharedInstance.gameObject.name, _versionString);
		}

		public void Awake()
		{
			if (base.gameObject == SharedInstance.gameObject)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		public static bool isSupported()
		{
			return UnityAdsExternal.isSupported();
		}

		public static string getSDKVersion()
		{
			return UnityAdsExternal.getSDKVersion();
		}

		public static void setLogLevel(Advertisement.DebugLevel logLevel)
		{
			UnityAdsExternal.setLogLevel(logLevel);
		}

		public static bool canShowZone(string zone)
		{
			if (!isInitialized || isShowing)
			{
				return false;
			}
			return UnityAdsExternal.canShowZone(zone);
		}

		public static bool hasMultipleRewardItems()
		{
			return UnityAdsExternal.hasMultipleRewardItems();
		}

		public static List<string> getRewardItemKeys()
		{
			List<string> list = new List<string>();
			string rewardItemKeys = UnityAdsExternal.getRewardItemKeys();
			return new List<string>(rewardItemKeys.Split(';'));
		}

		public static string getDefaultRewardItemKey()
		{
			return UnityAdsExternal.getDefaultRewardItemKey();
		}

		public static string getCurrentRewardItemKey()
		{
			return UnityAdsExternal.getCurrentRewardItemKey();
		}

		public static bool setRewardItemKey(string rewardItemKey)
		{
			return UnityAdsExternal.setRewardItemKey(rewardItemKey);
		}

		public static void setDefaultRewardItemAsRewardItem()
		{
			UnityAdsExternal.setDefaultRewardItemAsRewardItem();
		}

		public static string getRewardItemNameKey()
		{
			if (_rewardItemNameKey == null || _rewardItemNameKey.Length == 0)
			{
				fillRewardItemKeyData();
			}
			return _rewardItemNameKey;
		}

		public static string getRewardItemPictureKey()
		{
			if (_rewardItemPictureKey == null || _rewardItemPictureKey.Length == 0)
			{
				fillRewardItemKeyData();
			}
			return _rewardItemPictureKey;
		}

		public static Dictionary<string, string> getRewardItemDetailsWithKey(string rewardItemKey)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string empty = string.Empty;
			empty = UnityAdsExternal.getRewardItemDetailsWithKey(rewardItemKey);
			if (empty != null)
			{
				List<string> list = new List<string>(empty.Split(';'));
				Utils.LogDebug("UnityAndroid: getRewardItemDetailsWithKey() rewardItemDataString=" + empty);
				if (list.Count == 2)
				{
					dictionary.Add(getRewardItemNameKey(), list.ToArray().GetValue(0).ToString());
					dictionary.Add(getRewardItemPictureKey(), list.ToArray().GetValue(1).ToString());
				}
			}
			return dictionary;
		}

		public void Show(string zoneId = null, ShowOptions options = null)
		{
			string text = null;
			_resultDelivered = false;
			if (options != null)
			{
				if (options.resultCallback != null)
				{
					resultCallback = options.resultCallback;
				}
				ShowOptionsExtended showOptionsExtended = options as ShowOptionsExtended;
				text = ((showOptionsExtended == null || showOptionsExtended.gamerSid == null || showOptionsExtended.gamerSid.Length <= 0) ? options.gamerSid : showOptionsExtended.gamerSid);
			}
			if (!isInitialized || isShowing)
			{
				deliverCallback(ShowResult.Failed);
			}
			else if (text != null)
			{
				if (!show(zoneId, string.Empty, new Dictionary<string, string> { { "sid", text } }))
				{
					deliverCallback(ShowResult.Failed);
				}
			}
			else if (!show(zoneId))
			{
				deliverCallback(ShowResult.Failed);
			}
		}

		public static bool show(string zoneId = null)
		{
			return show(zoneId, string.Empty, null);
		}

		public static bool show(string zoneId, string rewardItemKey)
		{
			return show(zoneId, rewardItemKey, null);
		}

		public static bool show(string zoneId, string rewardItemKey, Dictionary<string, string> options)
		{
			if (!isShowing)
			{
				isShowing = true;
				if ((bool)SharedInstance)
				{
					string options2 = parseOptionsDictionary(options);
					if (UnityAdsExternal.show(zoneId, rewardItemKey, options2))
					{
						return true;
					}
				}
			}
			return false;
		}

		private static void deliverCallback(ShowResult result)
		{
			isShowing = false;
			if (resultCallback != null && !_resultDelivered)
			{
				_resultDelivered = true;
				resultCallback(result);
				resultCallback = null;
			}
		}

		public static void hide()
		{
			if (isShowing)
			{
				UnityAdsExternal.hide();
			}
		}

		private static void fillRewardItemKeyData()
		{
			string rewardItemDetailsKeys = UnityAdsExternal.getRewardItemDetailsKeys();
			if (rewardItemDetailsKeys != null && rewardItemDetailsKeys.Length > 2)
			{
				List<string> list = new List<string>(rewardItemDetailsKeys.Split(';'));
				_rewardItemNameKey = list.ToArray().GetValue(0).ToString();
				_rewardItemPictureKey = list.ToArray().GetValue(1).ToString();
			}
		}

		private static string parseOptionsDictionary(Dictionary<string, string> options)
		{
			string text = string.Empty;
			if (options != null)
			{
				bool flag = false;
				if (options.ContainsKey("noOfferScreen"))
				{
					text = text + ((!flag) ? string.Empty : ",") + "noOfferScreen:" + options["noOfferScreen"];
					flag = true;
				}
				if (options.ContainsKey("openAnimated"))
				{
					text = text + ((!flag) ? string.Empty : ",") + "openAnimated:" + options["openAnimated"];
					flag = true;
				}
				if (options.ContainsKey("sid"))
				{
					text = text + ((!flag) ? string.Empty : ",") + "sid:" + options["sid"];
					flag = true;
				}
				if (options.ContainsKey("muteVideoSounds"))
				{
					text = text + ((!flag) ? string.Empty : ",") + "muteVideoSounds:" + options["muteVideoSounds"];
					flag = true;
				}
				if (options.ContainsKey("useDeviceOrientationForVideo"))
				{
					text = text + ((!flag) ? string.Empty : ",") + "useDeviceOrientationForVideo:" + options["useDeviceOrientationForVideo"];
					flag = true;
				}
			}
			return text;
		}

		public void onHide()
		{
			isShowing = false;
			deliverCallback(ShowResult.Skipped);
			Utils.LogDebug("onHide");
		}

		public void onShow()
		{
			Utils.LogDebug("onShow");
		}

		public void onVideoStarted()
		{
			Utils.LogDebug("onVideoStarted");
		}

		public void onVideoCompleted(string parameters)
		{
			if (parameters != null)
			{
				List<string> list = new List<string>(parameters.Split(';'));
				string text = list.ToArray().GetValue(0).ToString();
				bool flag = list.ToArray().GetValue(1).ToString() == "true";
				Utils.LogDebug("onVideoCompleted: " + text + " - " + flag);
				if (flag)
				{
					deliverCallback(ShowResult.Skipped);
				}
				else
				{
					deliverCallback(ShowResult.Finished);
				}
			}
		}

		public void onFetchCompleted()
		{
			isInitialized = true;
			Utils.LogDebug("onFetchCompleted");
		}

		public void onFetchFailed()
		{
			Utils.LogDebug("onFetchFailed");
		}
	}
}
