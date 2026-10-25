using System;

// Runtime numbers are stable save/asset slots, not menu positions or release IDs.
public static class LevelCatalog
{
	public sealed class Level
	{
		public int WorldNumber { get; private set; }
		public int SourceNumber { get; private set; }
		public string ResourceRoot { get; private set; }
		public string Title { get; private set; }
		public int MaxGems { get; private set; }
		public bool IsExpansion { get { return WorldNumber >= 9; } }
		// Donor IDs, not menu positions. Keep the explicit catalogue entries below
		// compatible with export_expansion_maps.py's constructor parser.
		public int MaxCrowns
		{
			get
			{
				return IsExpansion && (SourceNumber == 16 || SourceNumber == 17 || SourceNumber == 19 ||
					(SourceNumber >= 21 && SourceNumber <= 25) || SourceNumber == 27 || SourceNumber == 28 ||
					SourceNumber == 29 || SourceNumber == 37 || SourceNumber == 38 || SourceNumber == 40) ? 3 : 0;
			}
		}

		public Level(int worldNumber, int sourceNumber, string title, int maxGems)
		{
			WorldNumber = worldNumber;
			SourceNumber = sourceNumber;
			ResourceRoot = sourceNumber <= 20 ? "Classic1471" : sourceNumber <= 25 ? "Classic1561" : "Classic1782";
			Title = title;
			MaxGems = maxGems;
		}
	}

	public const int Count = 33;

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
		new Level(20, 13, "Illusion", 20),
		new Level(21, 16, "Monody", 10),
		new Level(22, 22, "Matrix", 10),
		new Level(23, 24, "JOKER", 10),
		// Native resource IDs differ from the historical main/bonus numbering.
		new Level(24, 27, "Mental Rave", 10),
		new Level(25, 28, "Street Basketball", 10),
		new Level(26, 29, "Fairy Tale", 10),
		new Level(27, 33, "Hoop Dream", 10),
		new Level(28, 35, "The Winter", 10),
		new Level(29, 36, "Cloud", 10),
		new Level(30, 37, "Neon", 10),
		new Level(31, 38, "Laser", 10),
		new Level(32, 40, "Egypt", 10),
		new Level(33, 41, "The Eye of Horus", 10)
	};

	// https://rolling-sky.fandom.com/wiki/Level (revision 148880), easy to hard.
	// Use our classic variants: Cloud B, Massif B, Illusion B, Forest B,
	// Hoop Dream A (not the 2026 extended B), and Monody A.
	private static readonly int[] menuOrder =
	{
		29, 27, 28, 1, 33, 20, // 1 star
		2, 31, 24, 26, 23, // 2 stars
		21, 3, 12, 4, 15, 22, // 3 stars
		5, 6, 7, 8, 19, 25, // 4 stars
		32, 11, 10, 18, 13, 9, 14, 30, 17, // 5 stars
		16 // 6 stars
	};

	public static Level Get(int worldNumber)
	{
		if (worldNumber < 1 || worldNumber > Count)
			throw new ArgumentOutOfRangeException("worldNumber");
		return levels[worldNumber - 1];
	}

	public static Level AtMenuIndex(int index)
	{
		return Get(menuOrder[index]);
	}

	public static int MenuIndexOf(int worldNumber)
	{
		Get(worldNumber);
		return Array.IndexOf(menuOrder, worldNumber);
	}

	public static int NextWorld(int worldNumber)
	{
		return AtMenuIndex((MenuIndexOf(worldNumber) + 1) % Count).WorldNumber;
	}
}
