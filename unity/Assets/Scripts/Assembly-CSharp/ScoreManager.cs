using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private const int vitaForLife = 5;

	private static PersistantSecured.Int storedCurrency = new PersistantSecured.Int("SFX_VOL", "Currency", "ScoreManager", 0);

	private static PersistantSecured.Int storedHighscore = new PersistantSecured.Int("MUS_VOL", "Highscore", "ScoreManager", 0);

	private static float scoreFractions;

	public static int Score { get; private set; }

	public static int Highscore { get; private set; }

	public static int Currency { get; private set; }

	public static float Vita { get; private set; }

	public static float Lives { get; private set; }

	public static float LivesUsed { get; private set; }

	public static int VitaAsInt { get; private set; }

	public static int LivesAsInt { get; private set; }

	public static int LivesUsedAsInt { get; private set; }

	public static string ScoreAsString { get; private set; }

	public static string HighscoreAsString { get; private set; }

	public static string CurrencyAsString { get; private set; }

	public static bool HighscoreThisGame { get; private set; }

	public static void Initialize()
	{
		Score = 0;
		scoreFractions = 0f;
		Vita = 0f;
		Lives = 0f;
		LivesUsed = 0f;
		Currency = storedCurrency.Get();
		Highscore = storedHighscore.Get();
		HighscoreThisGame = false;
		DisplayOnScreen();
	}

	public static void AddVita(int addedVita)
	{
		SetVita(Vita + (float)addedVita);
	}

	public static void AddVita(float addedVita)
	{
		SetVita(Vita + addedVita);
	}

	public static bool HasLife()
	{
		return Lives > 1f;
	}

	public static void TakeLife()
	{
		SetVita(Vita - 5f);
		LivesUsed += 1f;
		LivesUsedAsInt++;
	}

	private static void SetVita(int newVitaAmmount)
	{
		SetVita((float)newVitaAmmount);
	}

	private static void SetVita(float newVitaAmmount)
	{
		Vita = newVitaAmmount;
		Lives = Vita / 5f;
		VitaAsInt = MathUtils.Floored(Vita);
		LivesAsInt = MathUtils.Floored(Lives);
		DisplayOnScreen();
	}

	public static void AddScore()
	{
		AddScore(1);
	}

	public static void AddScore(float currencyFractionToAdd)
	{
		scoreFractions += currencyFractionToAdd;
		if (scoreFractions >= 1f)
		{
			float num = MathUtils.FlooredAsFloat(scoreFractions);
			AddScore((int)num);
			scoreFractions -= num;
		}
	}

	public static void AddScore(int currencyToAdd)
	{
		int score = Score + currencyToAdd;
		SetScore(score);
		AddCurrency(currencyToAdd);
	}

	public static void SetScore(float newScore)
	{
		SetScore((int)newScore);
	}

	public static void SetScore(int newScore)
	{
		Score = newScore;
		ScoreAsString = newScore.ToString();
		if (newScore > Highscore)
		{
			SetHighscore(newScore);
			HighscoreThisGame = true;
		}
		DisplayOnScreen();
	}

	private static void SetHighscore(int newHighscore)
	{
		Highscore = newHighscore;
		HighscoreAsString = newHighscore.ToString();
		storedHighscore.Set(newHighscore);
	}

	public static void AddCurrency()
	{
		AddCurrency(1);
	}

	public static void AddCurrency(int currencyToAdd)
	{
		int currency = storedCurrency.Get() + currencyToAdd;
		SetCurrency(currency);
	}

	public static void TakeCurrency(int currencyToTake)
	{
		int num = storedCurrency.Get() - currencyToTake;
		if (num < 0)
		{
			num = 0;
		}
		SetCurrency(num);
	}

	public static void SetCurrency(float newCurrencyAmmount)
	{
		SetCurrency((int)newCurrencyAmmount);
	}

	public static void SetCurrency(int newCurrencyAmmount)
	{
		Currency = newCurrencyAmmount;
		CurrencyAsString = newCurrencyAmmount.ToString();
		storedCurrency.Set(newCurrencyAmmount);
	}

	public static void DisplayOnScreen()
	{
		GameHud.SetSaves(Lives, LivesAsInt);
	}
}
