using System.Collections.Generic;
using Prime31;

public class AnalyticsManager
{
	private const string IOS_KEY = "SFT3FSY4BCF9G6QYS8RG";

	private const string ANDROID_KEY = "VGGTRFPH397FNC4T8DR6";

	private static bool SESSION_STARTED;

	private static void EnsureSessionStart()
	{
		if (!SESSION_STARTED)
		{
			StartSession();
		}
	}

	public static void StartSession()
	{
		if (!SESSION_STARTED)
		{
			SESSION_STARTED = true;
			AnalyticsLogEvent.ClearColdBootAttemptsCounter();
			FlurryAnalytics.startSession("VGGTRFPH397FNC4T8DR6");
		}
	}

	public static void LogEvent(string eventName, bool isTimed = false)
	{
		EnsureSessionStart();
		FlurryAnalytics.logEvent(eventName, isTimed);
	}

	public static void LogEventWithParameters(string eventName, Dictionary<string, string> parameters, bool isTimed = false)
	{
		EnsureSessionStart();
		FlurryAnalytics.logEvent(eventName, parameters, isTimed);
	}

	public static void EndTimedEvent(string eventName)
	{
		EnsureSessionStart();
		FlurryAnalytics.endTimedEvent(eventName);
	}

	public static void endTimedEvent(string eventName, Dictionary<string, string> parameters)
	{
		EnsureSessionStart();
		FlurryAnalytics.endTimedEvent(eventName, parameters);
	}
}
