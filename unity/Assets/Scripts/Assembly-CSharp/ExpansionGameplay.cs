// Native identity must not be inferred from a menu/catalogue position.
public static class ExpansionGameplay
{
	public static int SourceNumber { get { return LevelCatalog.Get(GameManager.WorldNum).SourceNumber; } }
	// Later donors inherit these shared gameplay paths, but keep their own
	// serialized assets and prefab aliases.
	public static bool Classic1782
	{
		get
		{
			string root = LevelCatalog.Get(GameManager.WorldNum).ResourceRoot;
			return root == "Classic1782" || root == "Classic1792" || root == "Classic325";
		}
	}
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
