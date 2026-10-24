using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private const bool debugsEnabled = true;

	private const bool debugDeltaTime = false;

	private static bool inCountdown;

	private static DateTime countdownStart;

	private static float countdownTime;

	public static void StartCountdown(float countdownTimeInSeconds)
	{
		countdownStart = DateTime.UtcNow;
		countdownTime = countdownTimeInSeconds;
		inCountdown = true;
	}

	public static float GetCountdown()
	{
		if (inCountdown)
		{
			float num = (float)(DateTime.UtcNow - countdownStart).TotalSeconds;
			if (num >= countdownTime)
			{
				inCountdown = false;
				return 0f;
			}
			return countdownTime - num;
		}
		return 0f;
	}

	public static string GetCountdownAsString(bool showWords = false)
	{
		string asString;
		GetCountdown(out asString, showWords);
		return asString;
	}

	public static float GetCountdown(out string asString, bool showWords = false)
	{
		float countdown = GetCountdown();
		asString = ((!showWords) ? ToCountdownString(countdown) : ToCountdownStringFull(countdown));
		return countdown;
	}

	public static string ToCountdownString(float secondsLeft)
	{
		if (secondsLeft <= 0f)
		{
			return "0";
		}
		if (secondsLeft < 10f)
		{
			return string.Format("{0:0.0}", secondsLeft);
		}
		if (secondsLeft < 60f)
		{
			return string.Format("{0:0}", secondsLeft);
		}
		if (secondsLeft < 3600f)
		{
			float num = MathUtils.FlooredDivision(secondsLeft, 60f);
			float num2 = MathUtils.Floored(secondsLeft - num * 60f);
			return string.Format("{0}:{1:00}", num, num2);
		}
		if (secondsLeft < 36000f)
		{
			float num3 = MathUtils.CeiledDivision(secondsLeft, 60f);
			return string.Format("{0}", num3);
		}
		float num4 = MathUtils.CeiledDivision(secondsLeft, 3600f);
		return string.Format("{0:0.0}", num4);
	}

	public static string ToCountdownStringFull(float secondsLeft)
	{
		if (secondsLeft <= 0f)
		{
			return "0 seconds!";
		}
		if (secondsLeft < 10f)
		{
			return string.Format("{0:0.0} {1}", secondsLeft, Localizer.GetTerm("seconds"));
		}
		if (secondsLeft < 60f)
		{
			return string.Format("{0:0} {1}", secondsLeft, Localizer.GetTerm("seconds"));
		}
		if (secondsLeft < 3600f)
		{
			float num = MathUtils.FlooredDivision(secondsLeft, 60f);
			float num2 = MathUtils.Floored(secondsLeft - num * 60f);
			return string.Format("{0}:{1:00} {2}", num, num2, Localizer.GetTerm("minutes"));
		}
		if (secondsLeft < 36000f)
		{
			float num3 = MathUtils.CeiledDivision(secondsLeft, 60f);
			return string.Format("{0} {1}", num3, Localizer.GetTerm("minutes"));
		}
		float num4 = MathUtils.CeiledDivision(secondsLeft, 3600f);
		return string.Format("{0:0.0} {1}", num4, Localizer.GetTerm("hours"));
	}

	public static bool IsCountdownFinished()
	{
		return !inCountdown;
	}

	public static bool InCountdown()
	{
		return inCountdown;
	}
}
