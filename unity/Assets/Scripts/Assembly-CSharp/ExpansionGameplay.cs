// Native identity must not be inferred from a menu/catalogue position.
public static class ExpansionGameplay
{
	public static int SourceNumber { get { return LevelCatalog.Get(GameManager.WorldNum).SourceNumber; } }
	public static bool Classic1782 { get { return LevelCatalog.Get(GameManager.WorldNum).ResourceRoot == "Classic1782"; } }
	public static bool Later { get { return Classic1782 || (LevelCatalog.Get(GameManager.WorldNum).IsExpansion && SourceNumber >= 21 && SourceNumber <= 25); } }
	public static bool Enabled
	{
		get
		{
			if (!LevelCatalog.Get(GameManager.WorldNum).IsExpansion) return false;
			int source = SourceNumber;
			return (source >= 9 && source <= 17) || source == 19 || Later;
		}
	}
}
