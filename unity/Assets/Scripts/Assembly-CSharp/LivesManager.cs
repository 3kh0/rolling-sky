using UnityEngine;

public class LivesManager : MonoBehaviour
{
	public static int MaximumLives = GameManager.StartingLives;

	public static int StartingLives = GameManager.StartingLives;

	private static PersistantSecured.IntRanged lives;

	private static Persistant.Bool isRecharging;

	private static Persistant.DateTime rechargeStartTime;

	private static Persistant.Int recharges;

	private static bool isInitialized;

	public static bool OutOfLives
	{
		get
		{
			TryInitialize();
			// The web port has unlimited attempts, including with an exhausted save.
			return false;
		}
	}

	public static int LivesLeft
	{
		get
		{
			TryInitialize();
			// BallHolder needs a finite display count, not an attempt budget.
			return MaximumLives;
		}
	}

	public static bool HasALife
	{
		get
		{
			return !OutOfLives;
		}
	}

	public static string LivesLeftAsString
	{
		get
		{
			return LivesLeft.ToString();
		}
	}

	public static bool LivesAreRechargingNow
	{
		get
		{
			return isRecharging.Get();
		}
	}

	public static int TotalPreviousRecharges
	{
		get
		{
			return recharges.Get();
		}
	}

	public static bool LivesHaveRechargedBefore
	{
		get
		{
			return recharges.Get() > 0;
		}
	}

	public static bool LivesHaveNotRechargedBefore
	{
		get
		{
			return recharges.Get() <= 0;
		}
	}

	private static void TryInitialize()
	{
		if (!isInitialized)
		{
			lives = new PersistantSecured.IntRanged("LVS", 0, MaximumLives, StartingLives);
			isRecharging = new Persistant.Bool("RIS", "Is Recharging");
			rechargeStartTime = new Persistant.DateTime("RTI", "Recharge Start Time");
			recharges = new Persistant.Int("RTO", "Recharges");
			isInitialized = true;
		}
	}

	public static void TakeLife()
	{
		TryInitialize();
		// Death still ends the run, but never spends a ball or changes the save.
	}

	public static void StartLivesRecharging()
	{
		rechargeStartTime.SetAsNow();
		isRecharging.Set(true);
	}

	public static void RechargeLives(int livesToAdd)
	{
		TryInitialize();
		lives.Append(livesToAdd);
		recharges.Append();
		isRecharging.Set(false);
	}

	public static bool LivesHaveRecharged(float minutesForRecharge)
	{
		float secondsUntilRecharge;
		return LivesHaveRecharged(minutesForRecharge, out secondsUntilRecharge);
	}

	public static bool LivesHaveRecharged(float secondsForRecharge, out float secondsUntilRecharge)
	{
		if (isRecharging.Get())
		{
			float secondsSinceNow = rechargeStartTime.GetSecondsSinceNow();
			secondsUntilRecharge = secondsForRecharge - secondsSinceNow;
			return secondsUntilRecharge <= 0f;
		}
		StartLivesRecharging();
		secondsUntilRecharge = secondsForRecharge;
		return false;
	}
}
