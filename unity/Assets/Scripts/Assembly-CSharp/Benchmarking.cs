using System;
using UnityEngine;

public class Benchmarking : MonoBehaviour
{
	private const string ClassCode = "BNMK";

	private const int maxTimeStreamNumber = 10;

	private static DateTime?[] benchmarkTimers = new DateTime?[11];

	private static int currentBenchmarkTimer = 0;

	private static DateTime? benchmarkTimer
	{
		get
		{
			return benchmarkTimers[currentBenchmarkTimer];
		}
		set
		{
			benchmarkTimers[currentBenchmarkTimer] = value;
		}
	}

	public static float StartTiming()
	{
		float secondsTimed = GetSecondsTimed();
		benchmarkTimer = DateTime.UtcNow;
		return secondsTimed;
	}

	public static float StartTiming(int timeStreamNumber)
	{
		SwitchToTimeStream(timeStreamNumber);
		return StartTiming();
	}

	public static void SwitchToTimeStream(int timeStreamNumber)
	{
		if (timeStreamNumber >= 0 && timeStreamNumber < benchmarkTimers.Length)
		{
			currentBenchmarkTimer = timeStreamNumber;
			return;
		}
		Debug.LogError(string.Format("BNMK: ERROR: Attempt to switch to time stream number {0}, which is outside the valid range of 0 to {1}. Defaulting to 0", timeStreamNumber, 10));
		currentBenchmarkTimer = 0;
	}

	public static void ResetTimeStream()
	{
		currentBenchmarkTimer = 0;
	}

	public static float GetSecondsTimed()
	{
		float totalSecondsTimed;
		CanGetSecondsTimed(out totalSecondsTimed);
		return totalSecondsTimed;
	}

	public static bool CanGetSecondsTimed(out float totalSecondsTimed)
	{
		if (benchmarkTimer.HasValue)
		{
			totalSecondsTimed = (float)((DateTime.UtcNow - benchmarkTimer.Value).TotalMilliseconds / 1000.0);
			return true;
		}
		totalSecondsTimed = -1f;
		return false;
	}

	public static bool CanNotGetSecondsTimed(out float totalSecondsTimed)
	{
		return !CanGetSecondsTimed(out totalSecondsTimed);
	}

	public static string GetSecondsTimedString()
	{
		string secondsTimedString;
		CanGetSecondsTimedString(out secondsTimedString);
		return secondsTimedString;
	}

	public static string GetSecondsTimedString(int totalEventsCounted)
	{
		return GetSecondsTimedString((float)totalEventsCounted);
	}

	public static string GetSecondsTimedString(float totalEventsCounted)
	{
		string secondsTimedString;
		CanGetSecondsTimedString(out secondsTimedString, totalEventsCounted);
		return secondsTimedString;
	}

	public static bool CanGetSecondsTimedString(out string secondsTimedString)
	{
		float totalSecondsTimed;
		if (CanGetSecondsTimed(out totalSecondsTimed))
		{
			secondsTimedString = totalSecondsTimed.ToString("0.00");
			return true;
		}
		secondsTimedString = null;
		return false;
	}

	public static bool CanNotGetSecondsTimedString(out string secondsTimedString)
	{
		return !CanGetSecondsTimedString(out secondsTimedString);
	}

	public static bool CanGetSecondsTimedString(out string secondsTimedString, int totalEventsCounted)
	{
		return CanGetSecondsTimedString(out secondsTimedString, (float)totalEventsCounted);
	}

	public static bool CanGetSecondsTimedString(out string secondsTimedString, float totalEventsCounted)
	{
		float totalSecondsTimed;
		if (CanGetSecondsTimed(out totalSecondsTimed))
		{
			float num = totalSecondsTimed / totalEventsCounted;
			secondsTimedString = string.Format("{0:0} in {1:0.000} = {2:0.0000}", totalEventsCounted, totalSecondsTimed, num);
			return true;
		}
		secondsTimedString = null;
		return false;
	}

	public static bool CanNotGetSecondsTimedString(out string secondsTimedString, int totalEventsCounted)
	{
		return CanNotGetSecondsTimedString(out secondsTimedString, (float)totalEventsCounted);
	}

	public static bool CanNotGetSecondsTimedString(out string secondsTimedString, float totalEventsCounted)
	{
		return !CanGetSecondsTimedString(out secondsTimedString, totalEventsCounted);
	}

	public static void TryDebugSecondsTimed()
	{
		string secondsTimedString;
		if (CanGetSecondsTimedString(out secondsTimedString))
		{
			Debug.Log(string.Format("{0}: {1}", "BNMK", secondsTimedString));
		}
	}

	public static void TryDebugSecondsTimed(int totalEventsCounted)
	{
		TryDebugSecondsTimed((float)totalEventsCounted);
	}

	public static void TryDebugSecondsTimed(float totalEventsCounted)
	{
		string secondsTimedString;
		if (CanGetSecondsTimedString(out secondsTimedString, totalEventsCounted))
		{
			Debug.Log(string.Format("{0}: {1}", "BNMK", secondsTimedString));
		}
	}

	public static void TryDebugSecondsTimed(string debugPrefix)
	{
		string secondsTimedString;
		if (CanGetSecondsTimedString(out secondsTimedString))
		{
			Debug.Log(string.Format("{0}: {1}: {2}", "BNMK", debugPrefix, secondsTimedString));
		}
	}

	public static void TryDebugSecondsTimed(string debugPrefix, int totalEventsCounted)
	{
		TryDebugSecondsTimed((float)totalEventsCounted);
	}

	public static void TryDebugSecondsTimed(string debugPrefix, float totalEventsCounted)
	{
		string secondsTimedString;
		if (CanGetSecondsTimedString(out secondsTimedString, totalEventsCounted))
		{
			Debug.Log(string.Format("{0}: {1}: {2}", "BNMK", debugPrefix, secondsTimedString));
		}
	}

	public static void TryDebugSecondsTimed(int totalEventsCounted, string debugPrefix)
	{
		TryDebugSecondsTimed(debugPrefix, (float)totalEventsCounted);
	}

	public static void TryDebugSecondsTimed(float totalEventsCounted, string debugPrefix)
	{
		TryDebugSecondsTimed(debugPrefix, totalEventsCounted);
	}
}
