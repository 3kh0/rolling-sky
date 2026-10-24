using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfiler : MonoBehaviour
{
	private const bool debugsEnabled = true;

	private const bool debugDeaths = true;

	private const bool debugNew = true;

	private const bool debugNewQuery = true;

	private const bool debugLastDeath = true;

	private const bool debugRatios = true;

	private const bool debugHighestRowNum = true;

	private const bool debugAppending = true;

	private const int totalPreviousToTrack = 10;

	private const string encounteredName = "TypesEncountered";

	private const string deathsName = "TypeDeaths";

	private const int premiumKey = 42;

	private static Persistant.Int game = new Persistant.Int("Game", "Game", 0);

	private static Persistant.Int recording = new Persistant.Int("Recording", "Recording", 0);

	private static Persistant.Int tutorial = new Persistant.Int("Tutorial", "Tutorial", 0);

	private static Persistant.Bool recordingExplained = new Persistant.Bool("Recording Explained", "Recording Explained");

	private static Persistant.Bool alertsExplained = new Persistant.Bool("Alerts Explained", "Alerts Explained");

	private static Persistant.Bool autoPauseExplained = new Persistant.Bool("AutoPause Explained", "Auto-Pause Explained");

	private static Persistant.Bool restoreExplained = new Persistant.Bool("Restore Explained", "Restore Explained");

	private static Persistant.Bool iapsExplained = new Persistant.Bool("IAPs Explained", "IAPs Explained");

	private static PersistantSecured.Int premium = new PersistantSecured.Int("Premium", "Premium Mode", 0);

	private static PersistantCollection.IntDict playerSurvivals = new PersistantCollection.IntDict("TypeSurvived", "BucketType Survived List", true);

	private static PersistantCollection.IntDict playerDeaths = new PersistantCollection.IntDict("TypeDied", "BucketType Died List", true);

	private static PersistantCollection.IntList playtimes = new PersistantCollection.IntList("PlayTimes", "Play Times List", 10);

	private static PersistantCollection.IntList segmentCounts = new PersistantCollection.IntList("Segments", "Segment Counts List", 10);

	private static PersistantCollection.RunningValue playtime = new PersistantCollection.RunningValue("PlayTime", "Playtime Running Value");

	private static PersistantCollection.RunningValue segmentCount = new PersistantCollection.RunningValue("Sgements", "Sgement Counts Running Value");

	private static Persistant.Int lastDeathType = new Persistant.Int("LastDeath", "Last Death");

	private static Persistant.DateTime lastDeathTime = new Persistant.DateTime("LastDeathTime", "Last Death");

	private static PersistantSecured.Int highscore = new PersistantSecured.Int("GRN", "Greatest Row Num", 0);

	private static Dictionary<int, float> typesRatios;

	private static bool typesRatiosUpdated;

	private static int? lastBucketTypeNum;

	private static DateTime playtimeStart;

	private static float? averagePlaytime;

	public static int GameNumber
	{
		get
		{
			return GetGameNumber();
		}
	}

	public static int GamesPlayed
	{
		get
		{
			return GetGamesPlayed();
		}
	}

	public static int RecordingNumber
	{
		get
		{
			return GetRecordingNumber();
		}
	}

	public static int RecordingsMade
	{
		get
		{
			return GetRecordingsMade();
		}
	}

	public static int TutorialNumber
	{
		get
		{
			return GetTutorialNumber();
		}
	}

	public static int TutorialsShown
	{
		get
		{
			return GetTutorialsShown();
		}
	}

	public static bool HavePurchasedPremium
	{
		get
		{
			return premium.Get() == 42;
		}
	}

	public static bool HaveNotPurchasedPremium
	{
		get
		{
			return !HavePurchasedPremium;
		}
	}

	public static bool IsFirstGame
	{
		get
		{
			return GameNumber == 1;
		}
	}

	public static bool IsNotFirstGame
	{
		get
		{
			return !IsFirstGame;
		}
	}

	public static bool HaveExplainedRecording
	{
		get
		{
			return recordingExplained.Get();
		}
	}

	public static bool HaveExplainedAlerts
	{
		get
		{
			return alertsExplained.Get();
		}
	}

	public static bool HaveExplainedAutoPause
	{
		get
		{
			return autoPauseExplained.Get();
		}
	}

	public static bool HaveExplainedRestore
	{
		get
		{
			return restoreExplained.Get();
		}
	}

	public static bool HaveExplainedIaps
	{
		get
		{
			return iapsExplained.Get();
		}
	}

	public static bool HaveNotExplainedRecording
	{
		get
		{
			return !HaveExplainedRecording;
		}
	}

	public static bool HaveNotExplainedAlerts
	{
		get
		{
			return alertsExplained.Get();
		}
	}

	public static bool HaveNotExplainedAutoPause
	{
		get
		{
			return !HaveExplainedAutoPause;
		}
	}

	public static bool HaveNotExplainedRestore
	{
		get
		{
			return !HaveExplainedRestore;
		}
	}

	public static bool HaveNotExplainedIaps
	{
		get
		{
			return !HaveExplainedIaps;
		}
	}

	public static void OnGameplayStart()
	{
		playtimeStart = DateTime.UtcNow;
		game.Append();
		Debug.Log(string.Format("Player Profiler: Debug: Appending Game Number to {0} (Games played: {1})", GetGameNumber(), GetGamesPlayed()));
	}

	public static void OnGameplayEnd()
	{
	}

	public static void OnRecordingStart()
	{
		recording.Append();
		Debug.Log(string.Format("Player Profiler: Debug: Appending Recording Number to {0} (Recordings made: {1})", GetRecordingNumber(), GetRecordingsMade()));
	}

	public static void OnTutorial()
	{
		tutorial.Append();
		Debug.Log(string.Format("Player Profiler: Debug: Appending Tutorial Number to {0} (Tutorials shown: {1})", GetTutorialNumber(), GetTutorialsShown()));
	}

	public static void OnExplainedRecording()
	{
		recordingExplained.Set(true);
	}

	public static void OnExplainedAlerts()
	{
		alertsExplained.Set(true);
	}

	public static void OnExplainedAutoPause()
	{
		autoPauseExplained.Set(true);
	}

	public static void OnExplainedRestore()
	{
		restoreExplained.Set(true);
	}

	public static void OnExplainedIaps()
	{
		iapsExplained.Set(true);
	}

	public static void OnPremiumModePurchase()
	{
		premium.Set(42);
		PlayerPrefs.Save();
	}

	public static void AddNewSurvived(SegmentType bucketType)
	{
		AddNewSurvived((int)bucketType);
	}

	public static void AddNewSurvived(int bucketTypeNum)
	{
		playerSurvivals.Append(bucketTypeNum);
		typesRatiosUpdated = false;
	}

	public static void OnDeath()
	{
		lastDeathTime.SetAsNow();
		int currentSegmentTypeAsInt = SegmentTracker.CurrentSegmentTypeAsInt;
		playerDeaths.Append(currentSegmentTypeAsInt);
		lastDeathType.Set(currentSegmentTypeAsInt);
		typesRatiosUpdated = false;
		int num = (int)(DateTime.UtcNow - playtimeStart).TotalSeconds;
		playtimes.Add(num);
		playtime.Append(num);
		int currentSegmentCount = SegmentTracker.CurrentSegmentCount;
		segmentCounts.Add(currentSegmentCount);
		segmentCount.Append(currentSegmentCount);
		int num2 = highscore.Get();
		int ballRowNum = GameManager.BallRowNum;
		if (ballRowNum > num2)
		{
			bool flag = highscore.Set(ballRowNum);
			Debug.Log(string.Format("PGMN: DEBUG: Increasing highestRowNumber from {0} to {1}. Secured: {2}", num2, ballRowNum, flag));
		}
		Debug.Log(string.Format("PGMN: DEBUG: Recording new death on segment board {0} (last board {1}) after {2} seconds of playtime and {3} segments.\n  Average playtime: {4:0.0} ({5:0}/{6:0}), recent average playtime: {7:0.0} ({8:0}/{9:0}).   Average segments: {10:0.0} ({11:0}/{12:0}), recent average segments: {13:0.0} ({14:0}/{15:0})\n  Deaths so far: {16}\n  Ratios so far: {17}", SegmentTracker.CurrentSegmentIDAsString, SegmentTracker.LastSegmentIDAsString, num, currentSegmentCount, playtime.GetAverage(), playtime.GetSum(), playtime.GetCount(), playtimes.GetAverage(), playtimes.GetSum(), playtimes.Length, segmentCount.GetAverage(), segmentCount.GetSum(), segmentCount.GetCount(), segmentCounts.GetAverage(), segmentCounts.GetSum(), segmentCounts.Length, GetDeathsDebugString(), GetRatiosDebugString()));
	}

	public static int GetGameNumber()
	{
		return game.Get();
	}

	public static int GetGamesPlayed()
	{
		return MathUtils.MaxInt(GetGameNumber() - 1, 0);
	}

	public static int GetRecordingNumber()
	{
		return recording.Get();
	}

	public static int GetRecordingsMade()
	{
		return MathUtils.MaxInt(GetRecordingNumber() - 1, 0);
	}

	public static int GetTutorialNumber()
	{
		return tutorial.Get();
	}

	public static int GetTutorialsShown()
	{
		return MathUtils.MaxInt(GetTutorialNumber() - 1, 0);
	}

	public static bool HaveSurvived(SegmentType bucketType)
	{
		return HaveSurvived((int)bucketType);
	}

	public static bool HaveSurvived(int bucketTypeNum)
	{
		return playerSurvivals.Contains(bucketTypeNum);
	}

	public static bool HaveNotSurvived(SegmentType bucketType)
	{
		return !HaveSurvived((int)bucketType);
	}

	public static bool HaveNotSurvived(int bucketTypeNum)
	{
		return !HaveSurvived(bucketTypeNum);
	}

	public static int GetTimesDied(SegmentType bucketType)
	{
		return GetTimesDied((int)bucketType);
	}

	public static int GetTimesSurvived(SegmentType bucketType)
	{
		return GetTimesSurvived((int)bucketType);
	}

	public static int GetTimesTried(SegmentType bucketType)
	{
		return GetTimesTried((int)bucketType);
	}

	public static float GetRatio(SegmentType bucketType)
	{
		return GetRatio((int)bucketType);
	}

	public static int GetTimesDied(int bucketTypeNum)
	{
		return playerDeaths.Get(bucketTypeNum);
	}

	public static int GetTimesSurvived(int bucketTypeNum)
	{
		return playerSurvivals.Get(bucketTypeNum);
	}

	public static int GetTimesTried(int bucketTypeNum)
	{
		return GetTimesSurvived(bucketTypeNum) + GetTimesDied(bucketTypeNum);
	}

	public static float GetRatio(int bucketTypeNum)
	{
		TryCalculateRatios();
		if (typesRatios.ContainsKey(bucketTypeNum))
		{
			return typesRatios[bucketTypeNum];
		}
		return -1f;
	}

	public static bool LastDeathWasNot(SegmentType bucketType)
	{
		return !LastDeathWas((int)bucketType);
	}

	public static bool LastDeathWasNot(int bucketTypeNum)
	{
		return !LastDeathWas(bucketTypeNum);
	}

	public static bool LastDeathWas(SegmentType bucketType)
	{
		return LastDeathWas((int)bucketType);
	}

	public static bool LastDeathWas(int bucketTypeNum)
	{
		bool flag = lastDeathType.Get() == bucketTypeNum;
		if (flag)
		{
			lastDeathType.Clear();
		}
		return flag;
	}

	public static string GetSurvivedsString()
	{
		return playerSurvivals.ToString();
	}

	public static string GetDeathsString()
	{
		return playerDeaths.ToString();
	}

	public static float GetAveragePlaytime()
	{
		return playtime.GetAverage();
	}

	public static int GetAveragePlaytimeAsInt()
	{
		return playtime.GetAverageAsInt();
	}

	public static float GetRecentAveragePlaytime()
	{
		return playtimes.GetAverage();
	}

	public static int GetRecentAveragePlaytimeAsInt()
	{
		return playtimes.GetAverageAsInt();
	}

	public static float GetAverageSgementCount()
	{
		return segmentCount.GetAverage();
	}

	public static int GetAverageSgementCountAsInt()
	{
		return segmentCount.GetAverageAsInt();
	}

	public static float GetRecentAverageSgementCount()
	{
		return segmentCounts.GetAverage();
	}

	public static int GetRecentAverageSgementCountAsInt()
	{
		return segmentCounts.GetAverageAsInt();
	}

	public static int GetLastSegmentCount()
	{
		return segmentCounts.GetLast();
	}

	public static int GetRowsHighscore()
	{
		return highscore.Get();
	}

	public static float GetLastSegmentCountAsFloat()
	{
		return GetLastSegmentCount();
	}

	public static float GetRowsHighscoreAsFloat()
	{
		return GetRowsHighscore();
	}

	public static string GetLastSegmentCountAsString()
	{
		return GetLastSegmentCount().ToString();
	}

	public static string GetRowsHighscoreAsString()
	{
		return GetRowsHighscore().ToString();
	}

	private static void TryCalculateRatios()
	{
		if (typesRatiosUpdated)
		{
			return;
		}
		if (typesRatios == null)
		{
			typesRatios = new Dictionary<int, float>();
		}
		if (!lastBucketTypeNum.HasValue)
		{
			lastBucketTypeNum = EnumUtils.MaxEnumAsInt<SegmentType>();
		}
		int num = 0;
		while (true)
		{
			int? num2 = lastBucketTypeNum;
			if (!num2.HasValue || num >= num2.Value)
			{
				break;
			}
			float num3 = GetTimesDied(num);
			if (num3 > 0f)
			{
				float value = (float)GetTimesTried(num) / num3;
				if (typesRatios.ContainsKey(num))
				{
					typesRatios[num] = value;
				}
				else
				{
					typesRatios.Add(num, value);
				}
			}
			num++;
		}
		typesRatiosUpdated = true;
	}

	private static string GetDeathsDebugString()
	{
		string text = null;
		for (int i = 0; i < playerDeaths.Length; i++)
		{
			SegmentType keyByIndex = (SegmentType)playerDeaths.GetKeyByIndex(i);
			int valueByIndex = playerDeaths.GetValueByIndex(i);
			string text2 = string.Format("{0} = {1}", keyByIndex, valueByIndex);
			text = ((text != null) ? (text + ", " + text2) : text2);
		}
		return text;
	}

	private static string GetRatiosDebugString()
	{
		TryCalculateRatios();
		string text = null;
		if ((float)typesRatios.Count > 0f)
		{
			foreach (KeyValuePair<int, float> typesRatio in typesRatios)
			{
				SegmentType key = (SegmentType)typesRatio.Key;
				float value = typesRatio.Value;
				string text2 = string.Format("{0} = {1:0.0}", key, value);
				text = ((text != null) ? (text + ", " + text2) : text2);
			}
		}
		return text;
	}
}
