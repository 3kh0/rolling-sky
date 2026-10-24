using System;

namespace UnityEngine.Advertisements
{
	public static class Advertisement
	{
		public enum DebugLevel
		{
			None = 0,
			Error = 1,
			Warning = 2,
			Info = 4,
			Debug = 8,
			[Obsolete("Use Advertisement.DebugLevel.None instead")]
			NONE = 0,
			[Obsolete("Use Advertisement.DebugLevel.Error instead")]
			ERROR = 1,
			[Obsolete("Use Advertisement.DebugLevel.Warning instead")]
			WARNING = 2,
			[Obsolete("Use Advertisement.DebugLevel.Info instead")]
			INFO = 4,
			[Obsolete("Use Advertisement.DebugLevel.Debug instead")]
			DEBUG = 8
		}

		public static readonly string version = "1.3.2";

		private static DebugLevel _debugLevel = ((!Debug.isDebugBuild) ? ((DebugLevel)7) : ((DebugLevel)15));

		public static DebugLevel debugLevel
		{
			get
			{
				return _debugLevel;
			}
			set
			{
				_debugLevel = value;
				UnityAds.setLogLevel(_debugLevel);
			}
		}

		public static bool isSupported
		{
			get
			{
				return Application.isEditor || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
			}
		}

		public static bool isInitialized
		{
			get
			{
				return UnityAds.isInitialized;
			}
		}

		[Obsolete("Advertisement.allowPrecache is no longer supported and does nothing")]
		public static bool allowPrecache
		{
			get
			{
				return UnityAds.allowPrecache;
			}
			set
			{
				UnityAds.allowPrecache = value;
			}
		}

		public static bool isShowing
		{
			get
			{
				return UnityAds.isShowing;
			}
		}

		public static bool UnityDeveloperInternalTestMode { get; set; }

		public static void Initialize(string appId, bool testMode = false)
		{
			UnityAds.SharedInstance.Init(appId, testMode);
		}

		public static void Show(string zoneId = null, ShowOptions options = null)
		{
			UnityAds.SharedInstance.Show(zoneId, options);
		}

		public static bool IsReady(string zoneId = null)
		{
			return UnityAds.canShowZone(zoneId);
		}

		[Obsolete("Use Advertisement.IsReady method instead")]
		public static bool isReady(string zoneId = null)
		{
			return IsReady(zoneId);
		}
	}
}
