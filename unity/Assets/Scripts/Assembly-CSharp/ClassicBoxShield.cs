using UnityEngine;

// The free, in-run branch of native PropsShield. Inventory/ads are deliberately
// outside gameplay. A second box while active awards inventory, not a refill.
public static class ClassicBoxShield
{
	private enum ShieldState { None, Using, Trigger, Over }
	private static ShieldState state;
	private static float duration = 0.5f;
	private static int interval, colorIndex;
	private static Color savedColor;
	private static Texture savedTexture;
	private static bool savedAppearance;
	private static readonly Color[] colors = { Color.blue, Color.yellow, Color.black, Color.green, Color.gray, Color.grey, Color.white };
	public static bool IsActive { get { return state == ShieldState.Using || state == ShieldState.Trigger; } }
	public static bool IsTriggered { get { return state == ShieldState.Trigger; } }

	// True means a free shield became active. False means the caller must credit
	// one earned inventory shield (native ShieldThisGame++), without extending it.
	public static bool GrantBoxShield()
	{
		if (!ExpansionGameplay.Enabled || IsActive) return false;
		Reset();
		state = ShieldState.Using;
		GameManager.theBall.ShowShield();
		return true;
	}
	// Call only on the donor KillBall route, before Destroy/OnDeath, not Fall.
	public static bool TryInterceptDeath()
	{
		if (!ExpansionGameplay.Enabled || !IsActive) return false;
		if (state == ShieldState.Using)
		{
			savedColor = MaterialManager.Materials[MaterialName.Ball].GetColor("_ColorD");
			savedTexture = MaterialManager.Materials[MaterialName.BallQuad].mainTexture;
			savedAppearance = true;
			state = ShieldState.Trigger;
			if (UserSettings.IsSoundOn() && !GameManager.DisableSounds) GameManager.theBall.ShieldSound.Play();
#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif
		}
		return true;
	}
	// MaterialManager calls these after a normal recolor/texture application.
	// Native restores its currentBallColor/currentBallIndex, not the appearance
	// at the first collision, which may predate a theme transition.
	public static void ThemeColorChanged(Color color)
	{
		if (savedAppearance) savedColor = color;
	}
	public static void ThemeTextureChanged(Texture texture)
	{
		if (savedAppearance) savedTexture = texture;
	}
	public static void UpdateFrame()
	{
		if (state != ShieldState.Trigger) return;
		if (interval % 4 == 0)
		{
			if (DeviceQualityChecker.QualityIsPour())
			{
				// The seven original WorldTextures[0..6] are Ball1..Ball7,
				// not the accepted baseline's similarly numbered theme textures.
				Texture texture = Resources.Load<Texture2D>("Classic1471/Themes/Textures/In-Game/Ball/Ball" + (colorIndex + 1));
				if (texture == null) throw new System.InvalidOperationException("Missing native shield ball texture " + (colorIndex + 1));
				MaterialManager.Materials[MaterialName.BallQuad].mainTexture = texture;
			}
			else MaterialManager.Materials[MaterialName.Ball].SetColor("_ColorD", colors[colorIndex]);
			if (++colorIndex > 6) colorIndex = 0;
		}
		interval++;
		duration -= Time.deltaTime;
		if (duration <= 0f)
		{
			duration = 0f;
			RestoreAppearance();
			GameManager.theBall.HideShield();
			state = ShieldState.Over;
		}
	}
	public static void Reset()
	{
		RestoreAppearance();
		duration = 0.5f;
		state = ShieldState.None;
		interval = colorIndex = 0;
		if (GameManager.theBall != null) GameManager.theBall.HideShield();
	}
	private static void RestoreAppearance()
	{
		if (!savedAppearance) return;
		MaterialManager.Materials[MaterialName.Ball].SetColor("_ColorD", savedColor);
		MaterialManager.Materials[MaterialName.BallQuad].mainTexture = savedTexture;
		savedAppearance = false;
	}
}
