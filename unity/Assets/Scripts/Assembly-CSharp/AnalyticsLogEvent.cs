using System.Collections.Generic;
using UnityEngine;

public class AnalyticsLogEvent
{
	public static void ClearColdBootAttemptsCounter()
	{
		for (int i = 1; i <= 6; i++)
		{
			ResetColdBootAttemptsBylevel(i);
		}
		PlayerPrefs.Save();
	}

	public static void OnGameplayEnd(int worldIndex, int percentageReached, string deathSegmentID)
	{
		IncrementTotalAttemptsByLevel(worldIndex);
		IncrementAttemptsPerColdBootBylevel(worldIndex);
		string empty = string.Empty;
		empty = ((percentageReached <= 10) ? "0_10" : ((percentageReached <= 20) ? "10_20" : ((percentageReached <= 30) ? "20_30" : ((percentageReached <= 40) ? "30_40" : ((percentageReached <= 50) ? "40_50" : ((percentageReached <= 60) ? "50_60" : ((percentageReached <= 70) ? "60_70" : ((percentageReached <= 80) ? "70_80" : ((percentageReached > 90) ? "90_100" : "80_90")))))))));
		AnalyticsManager.LogEventWithParameters(string.Format("Game_Over_Level_{0}", worldIndex), new Dictionary<string, string>
		{
			{
				"total_attempts",
				TotalAttemptsByLevel(worldIndex).ToString()
			},
			{
				"atempts_since_cold_boot",
				AttemptsPerColdBootByLevel(worldIndex).ToString()
			},
			{ "percentage_reached", empty },
			{
				"auto_recharge_count",
				AutoRechargeCount().ToString()
			},
			{
				"rewarded_videos_watched",
				RewardedVideosWatched().ToString()
			},
			{ "death_segment_id", deathSegmentID }
		});
	}

	public static void OnWorldComplete(int worldIndex)
	{
		IncrementTotalAttemptsByLevel(worldIndex);
		IncrementAttemptsPerColdBootBylevel(worldIndex);
		AnalyticsManager.LogEventWithParameters(string.Format("Completed_Level_{0}", worldIndex), new Dictionary<string, string>
		{
			{
				"total_attempts",
				TotalAttemptsByLevel(worldIndex).ToString()
			},
			{
				"atempts_since_cold_boot",
				AttemptsPerColdBootByLevel(worldIndex).ToString()
			},
			{
				"auto_recharge_count",
				AutoRechargeCount().ToString()
			},
			{
				"rewarded_videos_watched",
				RewardedVideosWatched().ToString()
			}
		});
	}

	public static void OnRecharge()
	{
		IncrementIntPref("ale_autoRechargeCount");
	}

	public static void OnVideoRewardClicked()
	{
		IncrementRewardedVideosWatched();
		Dictionary<string, string> parameters = CreateTotalAttemptsForLevelsEventParametersDict();
		AnalyticsManager.LogEventWithParameters("Video_Reward_Recharge_Clicked", parameters);
	}

	public static void UpgradePackClicked()
	{
		Dictionary<string, string> parameters = CreateTotalAttemptsForLevelsEventParametersDict();
		AnalyticsManager.LogEventWithParameters("Upgrade_Pack_Clicked", parameters);
	}

	public static void UpgradePackPurchased()
	{
		Dictionary<string, string> parameters = CreateTotalAttemptsForLevelsEventParametersDict();
		AnalyticsManager.LogEventWithParameters("Upgrade_Pack_Purchased", parameters);
	}

	public static void UpgradePackPurchaseCancelled()
	{
		Dictionary<string, string> parameters = CreateTotalAttemptsForLevelsEventParametersDict();
		AnalyticsManager.LogEventWithParameters("Upgrade_Pack_Purchase_Cancelled", parameters);
	}

	private static Dictionary<string, string> CreateTotalAttemptsForLevelsEventParametersDict()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 1; i <= 6; i++)
		{
			dictionary.Add(string.Format("total_attempts_level_{0}", i), TotalAttemptsByLevel(i).ToString());
		}
		return dictionary;
	}

	private static int TotalAttemptsByLevel(int level)
	{
		return GetIntPref(string.Format("ale_totalAttempts_{0}", level));
	}

	private static void IncrementTotalAttemptsByLevel(int level)
	{
		IncrementIntPref(string.Format("ale_totalAttempts_{0}", level));
	}

	private static int AttemptsPerColdBootByLevel(int level)
	{
		return GetIntPref(string.Format("ale_attemptsCold_{0}", level));
	}

	private static void IncrementAttemptsPerColdBootBylevel(int level)
	{
		IncrementIntPref(string.Format("ale_attemptsCold_{0}", level));
	}

	private static void ResetColdBootAttemptsBylevel(int level)
	{
		PlayerPrefs.SetInt(string.Format("ale_attemptsCold_{0}", level), 0);
	}

	private static void IncrementRewardedVideosWatched()
	{
		IncrementIntPref("ale_rewardVidWatched");
	}

	private static int AutoRechargeCount()
	{
		return GetIntPref("ale_autoRechargeCount");
	}

	private static int RewardedVideosWatched()
	{
		return GetIntPref("ale_rewardVidWatched");
	}

	private static int GetIntPref(string key)
	{
		return PlayerPrefs.GetInt(key, 0);
	}

	private static void IncrementIntPref(string key)
	{
		int value = GetIntPref(key) + 1;
		PlayerPrefs.SetInt(key, value);
	}
}
