namespace UnityEngine.Advertisements
{
	internal static class Utils
	{
		private static void Log(Advertisement.DebugLevel debugLevel, string message)
		{
			if ((Advertisement.debugLevel & debugLevel) != Advertisement.DebugLevel.None)
			{
				Debug.Log(message);
			}
		}

		public static void LogDebug(string message)
		{
			Log(Advertisement.DebugLevel.Debug, "Debug: " + message);
		}

		public static void LogInfo(string message)
		{
			Log(Advertisement.DebugLevel.Info, "Info:" + message);
		}

		public static void LogWarning(string message)
		{
			Log(Advertisement.DebugLevel.Warning, "Warning:" + message);
		}

		public static void LogError(string message)
		{
			Log(Advertisement.DebugLevel.Error, "Error: " + message);
		}
	}
}
