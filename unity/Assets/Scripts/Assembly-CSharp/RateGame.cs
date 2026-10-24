using UnityEngine;

public class RateGame : MonoBehaviour
{
	private static Persistant.Bool hasRatedGame = new Persistant.Bool("RatedGame");

	private static string rateUrl;

	public bool HasRatedGame
	{
		get
		{
			return hasRatedGame.Get();
		}
	}

	public bool HasNotRatedGame
	{
		get
		{
			return !HasRatedGame;
		}
	}

	public static void SetUrl(string rateGameUrl)
	{
		rateUrl = rateGameUrl;
	}

	public static void SendToRateGame()
	{
		if (rateUrl == null)
		{
			Debug.LogError("RTGM: ERROR: Attempt to send to rate-game url before any rate-game url was set!  Please call RateGame.SetUrl to set the rate-game url");
		}
		else if (HasInternet())
		{
			hasRatedGame.Set(true);
			Application.OpenURL(rateUrl);
		}
		else
		{
			AlertManager.ShowNoInternetAlert("rate game", true);
		}
	}

	private static bool HasInternet()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}
}
