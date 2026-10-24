using UnityEngine;

public class ProgressManager
{
	public class Progress
	{
		public readonly int LevelNumber;

		private PersistantSecured.Int storedProgress;

		private PersistantSecured.Int storedGems;

		private PersistantSecured.Int storedCrowns;

		public int ProgressPercentage { get; private set; }

		public int ProgressPercentageLast { get; private set; }

		public int ProgressPercentageUpToLast { get; private set; }

		public int ProgressPercentageThisSession { get; private set; }

		public float ProgressPercent { get; private set; }

		public float ProgressPercentLast { get; private set; }

		public float ProgressPercentUpToLast { get; private set; }

		public float ProgressPercentThisSession { get; private set; }

		public bool IsComplete { get; private set; }

		public bool IsNotComplete
		{
			get
			{
				return !IsComplete;
			}
		}

		public bool RecordWasJustBroken { get; private set; }

		public bool WasJustCompleted { get; private set; }

		public bool WasJustCompletedForFirstTime { get; private set; }

		public bool WasJustReCompleted { get; private set; }

		public int Gems { get; private set; }

		public int GemsLast { get; private set; }

		public int Crowns { get; private set; }

		public int CrownsLast { get; private set; }

		public Progress(int levelNumber)
		{
			LevelNumber = levelNumber;
			string text = LevelNumber.ToString();
			string playerPrefsKey = "P" + text;
			string playerPrefsKey2 = "G" + text;
			storedProgress = new PersistantSecured.Int(playerPrefsKey, 0);
			storedGems = new PersistantSecured.Int(playerPrefsKey2, 0);
			// Same independent crown key as the donor; the suffix remains our save
			// slot, never the native release ID. Existing P/G keys are untouched.
			storedCrowns = new PersistantSecured.Int("CrownsX" + text, 0);
			ProgressPercentage = storedProgress.Get();
			ProgressPercent = MathUtils.FromPercentageInt(ProgressPercentage);
			ProgressPercentageLast = ProgressPercentage;
			ProgressPercentLast = ProgressPercent;
			ProgressPercentageUpToLast = ProgressPercentage;
			ProgressPercentUpToLast = ProgressPercent;
			ProgressPercentageThisSession = 0;
			ProgressPercentThisSession = 0f;
			Gems = storedGems.Get();
			GemsLast = Gems;
			Crowns = CrownsClamped(storedCrowns.Get(), LevelNumber);
			CrownsLast = Crowns;
			IsComplete = ProgressPercentage >= 100;
			RecordWasJustBroken = false;
			WasJustCompleted = false;
			WasJustCompletedForFirstTime = false;
			WasJustReCompleted = false;
		}

		public bool Set(int newProgress, int gemsCollected, int crownsCollected = 0)
		{
			ProgressPercentageUpToLast = ProgressPercentage;
			ProgressPercentUpToLast = ProgressPercent;
			ProgressPercentageLast = newProgress;
			ProgressPercentLast = MathUtils.FromPercentageInt(newProgress);
			bool flag = newProgress > ProgressPercentage;
			bool flag2 = false;
			if (flag)
			{
				ProgressPercentage = newProgress;
				ProgressPercent = MathUtils.FromPercentageInt(ProgressPercentage);
				if (storedProgress.Set(ProgressPercentage))
				{
					Debug.Log(string.Format("PGMN: Saved secured progress value of {0}% for World {1} to PlayerPrefs", ProgressPercentage, LevelNumber));
				}
				if (!IsComplete && ProgressPercentage >= 100)
				{
					IsComplete = true;
					flag2 = true;
				}
			}
			RecordWasJustBroken = flag;
			WasJustCompletedForFirstTime = flag2;
			WasJustCompleted = newProgress >= 100;
			WasJustReCompleted = !flag2 && WasJustCompleted;
			if (newProgress > ProgressPercentageThisSession)
			{
				ProgressPercentageThisSession = newProgress;
				ProgressPercentThisSession = MathUtils.FromPercentageInt(newProgress);
			}
			GemsLast = gemsCollected;
			if (gemsCollected > Gems)
			{
				Gems = gemsCollected;
				if (storedGems.Set(Gems))
				{
					Debug.Log(string.Format("PGMN: Saved secured gems earned value of {0}% for World {1} to PlayerPrefs", Gems, LevelNumber));
				}
			}
			CrownsLast = CrownsClamped(crownsCollected, LevelNumber);
			if (CrownsLast > Crowns)
			{
				Crowns = CrownsLast;
				storedCrowns.Set(Crowns);
			}
			return flag;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}%", LevelNumber, ProgressPercentage);
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugAdding = false;

	private const bool debugMoving = false;

	private const bool debugChanging = false;

	private const bool debugRecordAttempts = false;

	private const string progressPlayerPrefsSuffix = "P";

	private const string gemsPlayerPrefsSuffix = "G";

	// Local surplus earned from mystery boxes only. No initial stock, shop, ads,
	// automatic equip or consumption policy is implied by this stored balance.
	private static readonly PersistantSecured.Int boxShieldInventory = new PersistantSecured.Int("ClassicBoxShields", 0);

	private static Progress[] progress;

	private static bool initialized;

	private static int currentLevelNumber
	{
		get
		{
			return GameManager.WorldIndex;
		}
	}

	public static void SetProgressFor(int levelNumber, int scorePercentage, int gemsCollected, int crownsCollected = 0)
	{
		TryInitialize();
		progress[levelNumber].Set(scorePercentage, gemsCollected, crownsCollected);
	}

	public static int GetProgress(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentage;
	}

	public static float GetProgressFloat(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercent;
	}

	public static int GetLastProgress(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentageLast;
	}

	public static float GetLastProgressFloat(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentLast;
	}

	public static int GetProgressUpToLast(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentageUpToLast;
	}

	public static float GetProgressFloatUpToLast(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentUpToLast;
	}

	public static int GetProgressThisSession(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentageThisSession;
	}

	public static float GetProgressFloatThisSession(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].ProgressPercentThisSession;
	}

	public static int GetProgress()
	{
		return GetProgress(currentLevelNumber);
	}

	public static float GetProgressFloat()
	{
		return GetProgressFloat(currentLevelNumber);
	}

	public static int GetLastProgress()
	{
		return GetLastProgress(currentLevelNumber);
	}

	public static float GetLastProgressFloat()
	{
		return GetLastProgressFloat(currentLevelNumber);
	}

	public static int GetProgressUpToLast()
	{
		return GetProgressUpToLast(currentLevelNumber);
	}

	public static float GetProgressFloatUpToLast()
	{
		return GetProgressFloatUpToLast(currentLevelNumber);
	}

	public static int GetProgressThisSession()
	{
		return GetProgressThisSession(currentLevelNumber);
	}

	public static float GetProgressFloatThisSession()
	{
		return GetProgressFloatThisSession(currentLevelNumber);
	}

	public static bool IsWorldComplete(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].IsComplete;
	}

	public static bool IsWorldNotComplete(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].IsNotComplete;
	}

	public static bool WasJustCompleted(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].WasJustCompleted;
	}

	public static bool WasJustCompletedForFirstTime(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].WasJustCompletedForFirstTime;
	}

	public static bool WasJustReCompleted(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].WasJustReCompleted;
	}

	public static bool IsWorldComplete()
	{
		return IsWorldComplete(currentLevelNumber);
	}

	public static bool IsWorldNotComplete()
	{
		return IsWorldNotComplete(currentLevelNumber);
	}

	public static bool WasJustCompleted()
	{
		return WasJustCompleted(currentLevelNumber);
	}

	public static bool WasJustCompletedForFirstTime()
	{
		return WasJustCompletedForFirstTime(currentLevelNumber);
	}

	public static bool WasJustReCompleted()
	{
		return WasJustReCompleted(currentLevelNumber);
	}

	public static bool HighscoreWasJustBeaten(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].RecordWasJustBroken;
	}

	public static bool HighscoreWasJustBeaten(int levelNumber, out bool wasJustCompleted)
	{
		TryInitialize();
		wasJustCompleted = progress[levelNumber].WasJustCompletedForFirstTime;
		return progress[levelNumber].RecordWasJustBroken;
	}

	public static bool HighscoreWasJustBeaten(int levelNumber, out bool wasJustCompleted, out bool wasJustReCompleted)
	{
		TryInitialize();
		wasJustCompleted = progress[levelNumber].WasJustCompletedForFirstTime;
		wasJustReCompleted = progress[levelNumber].WasJustReCompleted;
		return progress[levelNumber].RecordWasJustBroken;
	}

	public static bool HighscoreWasJustBeaten()
	{
		return HighscoreWasJustBeaten(currentLevelNumber);
	}

	public static bool HighscoreWasJustBeaten(out bool wasJustCompleted)
	{
		return HighscoreWasJustBeaten(currentLevelNumber, out wasJustCompleted);
	}

	public static bool HighscoreWasJustBeaten(out bool wasJustCompleted, out bool wasJustReCompleted)
	{
		return HighscoreWasJustBeaten(currentLevelNumber, out wasJustCompleted, out wasJustReCompleted);
	}

	public static int GetGems(int levelNumber)
	{
		TryInitialize();
		return GemsClamped(progress[levelNumber].Gems, levelNumber);
	}

	public static int GetLastGems(int levelNumber)
	{
		TryInitialize();
		return GemsClamped(progress[levelNumber].GemsLast, levelNumber);
	}

	public static int GetGems()
	{
		return GetGems(currentLevelNumber);
	}

	public static int GetLastGems()
	{
		return GetLastGems(currentLevelNumber);
	}

	public static int GetCrowns(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].Crowns;
	}

	public static int GetLastCrowns(int levelNumber)
	{
		TryInitialize();
		return progress[levelNumber].CrownsLast;
	}

	public static int GetCrowns()
	{
		return GetCrowns(currentLevelNumber);
	}

	public static int GetLastCrowns()
	{
		return GetLastCrowns(currentLevelNumber);
	}

	public static int GetBoxShieldInventory()
	{
		return Mathf.Clamp(boxShieldInventory.Get(), 0, 99999999);
	}

	public static void RecordSurplusBoxShield()
	{
		// Native maximum; add at collection, never again at finish/reload.
		int count = GetBoxShieldInventory();
		if (count < 99999999) boxShieldInventory.Set(count + 1);
	}

	private static void TryInitialize()
	{
		if (!initialized)
		{
			progress = new Progress[GameManager.TotalWorlds];
			for (int i = 0; i < progress.Length; i++)
			{
				progress[i] = new Progress(i);
			}
			initialized = true;
		}
	}

	private static int GemsClamped(int gems, int levelNumber)
	{
		return Mathf.Clamp(gems, 0, LevelCatalog.Get(levelNumber + 1).MaxGems);
	}

	private static int CrownsClamped(int crowns, int levelNumber)
	{
		return Mathf.Clamp(crowns, 0, LevelCatalog.Get(levelNumber + 1).MaxCrowns);
	}
}
