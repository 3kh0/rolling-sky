using System;

// Runtime numbers are save slots and menu positions, not the donor's release IDs.
// In particular, keep the accepted first eight slots unchanged.
public static class LevelCatalog
{
	public sealed class Level
	{
		public int WorldNumber { get; private set; }
		public int SourceNumber { get; private set; }
		public string ResourceRoot { get; private set; }
		public string Title { get; private set; }
		public int MaxGems { get; private set; }
		public int? UnlockParentWorldNumber { get; private set; }
		public bool IsExpansion { get { return WorldNumber >= 9; } }
		// Donor IDs, not menu positions. Keep the explicit catalogue entries below
		// compatible with export_expansion_maps.py's constructor parser.
		public int MaxCrowns
		{
			get
			{
				return IsExpansion && (SourceNumber == 16 || SourceNumber == 17 || SourceNumber == 19 ||
					(SourceNumber >= 21 && SourceNumber <= 25)) ? 3 : 0;
			}
		}

		public Level(int worldNumber, int sourceNumber, string title, int maxGems, int? unlockParentWorldNumber = null)
		{
			WorldNumber = worldNumber;
			SourceNumber = sourceNumber;
			ResourceRoot = sourceNumber <= 20 ? "Classic1471" : "Classic1561";
			Title = title;
			MaxGems = maxGems;
			UnlockParentWorldNumber = unlockParentWorldNumber;
		}
	}

	public const int Count = 23;

	private static readonly Level[] levels =
	{
		new Level(1, 1, "Massif", 20),
		new Level(2, 2, "Sky", 20),
		new Level(3, 3, "Forest", 20),
		new Level(4, 4, "Desert", 20),
		new Level(5, 5, "Volcano", 20),
		new Level(6, 6, "City", 20),
		new Level(7, 7, "Sci-Tech", 20),
		new Level(8, 8, "Tetris", 20),
		new Level(9, 9, "E-labyrinth", 20),
		new Level(10, 10, "Castle in the Sky", 20),
		new Level(11, 11, "Halloween", 20),
		new Level(12, 12, "Ghost Dance", 20),
		new Level(13, 14, "Deep Space", 20),
		new Level(14, 15, "Christmas", 20),
		new Level(15, 17, "Kung Fu", 10),
		new Level(16, 19, "1UP", 10),
		new Level(17, 21, "Cube", 10),
		new Level(18, 23, "Poker", 10),
		new Level(19, 25, "Reggae", 10),
		new Level(20, 13, "Illusion", 20, 1),
		new Level(21, 16, "Monody", 10, 2),
		new Level(22, 22, "Matrix", 10, 17),
		new Level(23, 24, "Joker", 10, 18)
	};

	public static Level Get(int worldNumber)
	{
		if (worldNumber < 1 || worldNumber > Count)
			throw new ArgumentOutOfRangeException("worldNumber");
		return levels[worldNumber - 1];
	}

	public static bool IsUnlocked(int worldNumber)
	{
		int? parent = Get(worldNumber).UnlockParentWorldNumber;
		return !parent.HasValue || ProgressManager.IsWorldComplete(parent.Value - 1);
	}

	public static int NextUnlockedWorld(int worldNumber)
	{
		Get(worldNumber);
		for (int offset = 1; offset <= Count; offset++)
		{
			int next = (worldNumber - 1 + offset) % Count + 1;
			if (IsUnlocked(next)) return next;
		}
		throw new InvalidOperationException("The catalogue has no unlocked level.");
	}
}
