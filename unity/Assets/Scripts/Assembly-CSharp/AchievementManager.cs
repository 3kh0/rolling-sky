using Prime31;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	private const string accessAchivementsMsg = "access achievements";

	private const string leaderBoardNotSignedInMessage = "You must sign in to Game Center to access leaderboards.";

	private const string leaderBoardNotSignedInMessageAndroid = "You must sign in to Google Play Services to access leaderboards.";

	public static void ShowLeaderboard()
	{
		if (HasInternet())
		{
			if (PlayGameServices.isSignedIn())
			{
				PlayGameServices.showLeaderboards();
				return;
			}
			AlertManager.ShowAlert(string.Empty, "You must sign in to Google Play Services to access leaderboards.");
			PlayGameServices.authenticate();
		}
		else
		{
			AlertManager.ShowNoInternetAlert("access achievements", true);
		}
	}

	public static void ShowAchievements()
	{
		if (HasInternet())
		{
			if (PlayGameServices.isSignedIn())
			{
				PlayGameServices.showAchievements();
				return;
			}
			AlertManager.ShowAlert(string.Empty, "You must sign in to Google Play Services to access leaderboards.");
			PlayGameServices.authenticate();
		}
		else
		{
			AlertManager.ShowNoInternetAlert("access achievements", true);
		}
	}

	public static void AuthenticatePlayer()
	{
		if (HasInternet())
		{
			PlayGameServices.authenticate();
		}
	}

	public static void ReportWorldScore(int worldNum, int score)
	{
		string leaderboardID = string.Format("com.turbochilli.rollingsky.leaderboard.world{0}score", worldNum);
		switch (worldNum)
		{
		case 0:
			leaderboardID = "CgkI7dKT5vUUEAIQCw";
			break;
		case 1:
			leaderboardID = "CgkI7dKT5vUUEAIQDA";
			break;
		case 2:
			leaderboardID = "CgkI7dKT5vUUEAIQDQ";
			break;
		case 3:
			leaderboardID = "CgkI7dKT5vUUEAIQDg";
			break;
		case 4:
			leaderboardID = "CgkI7dKT5vUUEAIQDw";
			break;
		}
		string key = string.Format("achievements_score_world_{0}", worldNum);
		int num = PlayerPrefs.GetInt(key, 0);
		if (score > 0 && score > num)
		{
			ReportScore(leaderboardID, score);
			PlayerPrefs.SetInt(key, score);
		}
	}

	public static void WorldCompletedPerfectGemsAchievement(int worldNum)
	{
		string identifier = string.Format("com.turbochilli.rollingsky.achievement.completedworldperfect{0}", worldNum);
		switch (worldNum)
		{
		case 0:
			identifier = "CgkI7dKT5vUUEAIQBg";
			break;
		case 1:
			identifier = "CgkI7dKT5vUUEAIQBw";
			break;
		case 2:
			identifier = "CgkI7dKT5vUUEAIQCA";
			break;
		case 3:
			identifier = "CgkI7dKT5vUUEAIQCQ";
			break;
		case 4:
			identifier = "CgkI7dKT5vUUEAIQCg";
			break;
		}
		UnlockAchievement(identifier);
	}

	public static void UnlockWorldCompletedAchievement(int worldNum)
	{
		string identifier = string.Format("com.turbochilli.rollingsky.achievement.completedworld{0}", worldNum);
		switch (worldNum)
		{
		case 0:
			identifier = "CgkI7dKT5vUUEAIQAQ";
			break;
		case 1:
			identifier = "CgkI7dKT5vUUEAIQAg";
			break;
		case 2:
			identifier = "CgkI7dKT5vUUEAIQAw";
			break;
		case 3:
			identifier = "CgkI7dKT5vUUEAIQBA";
			break;
		case 4:
			identifier = "CgkI7dKT5vUUEAIQBQ";
			break;
		}
		UnlockAchievement(identifier);
	}

	private static void ReportScore(string leaderboardID, int score)
	{
		if (PlayGameServices.isSignedIn() && score != 0)
		{
			PlayGameServices.submitScore(leaderboardID, score, string.Empty);
		}
	}

	private static void UnlockAchievement(string identifier, bool showNotification = false)
	{
		if (!HasUnlockedAchievement(identifier) && !PlayerNotAuthenticated())
		{
			showNotification = true;
			PlayGameServices.unlockAchievement(identifier, showNotification);
			FlagAchievementUnlocked(identifier);
		}
	}

	private static bool PlayerNotAuthenticated()
	{
		return !PlayGameServices.isSignedIn();
	}

	private static bool HasUnlockedAchievement(string identifier)
	{
		string key = string.Format("amua_{0}", identifier);
		if (PlayerPrefs.HasKey(key))
		{
			return true;
		}
		return false;
	}

	private static void FlagAchievementUnlocked(string identifier)
	{
		string key = string.Format("amua_{0}", identifier);
		PlayerPrefs.SetInt(key, 1);
		PlayerPrefs.Save();
	}

	private static bool HasInternet()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}
}
