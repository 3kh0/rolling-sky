using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// Run in an isolated batch editor before building. This does not enter play mode,
// change scenes, save preferences, or claim to verify gameplay or audible output.
public static class ClassicContentValidation
{
	private static void Require(bool condition, string message)
	{
		if (!condition) throw new InvalidOperationException("Classic content: " + message);
	}

	public static void Validate()
	{
		Require(Application.unityVersion == "2017.4.40f1", "Wrong editor version");
		ValidateMenu();
		for (int difficulty = 0; difficulty < 26; difficulty++)
			Require(new SegmentID(SegmentType.Level, difficulty).DifficultyChar == ((char)('A' + difficulty)).ToString(),
				"Changed original segment label " + difficulty);
		Require(new SegmentID(SegmentType.Level, 26).ToString() == "LevelAA" &&
			new SegmentTracker.SegmentID(1, SegmentType.Level, 32, 7, 10).ToString() == "Level AG7",
			"Extended segment labels cannot describe the added worlds");
		GameObject recycledRoot = new GameObject("Hidden tile reset validation");
		try
		{
			Tile tile = recycledRoot.AddComponent<Tile>();
			tile.HideRoot(true);
			Require(tile.IsHidden && !recycledRoot.activeSelf, "Hidden tile did not hide its root");
			tile.ResetVariables();
			Require(!tile.IsHidden && recycledRoot.activeSelf, "Recycled hidden tile remained inactive");
		}
		finally { UnityEngine.Object.DestroyImmediate(recycledRoot); }
		int checkedCells = 0;
		foreach (SegmentType type in new SegmentType[] { SegmentType.Level, SegmentType.End })
		{
			LevelLoader.BucketGroup group = new LevelLoader.BucketGroup(type, Resources.Load<TextAsset>("Buckets/" + type));
			group.Load();
			List<int>[] baseline = new List<int>[5];
			for (int world = 0; world < 5; world++) baseline[world] = Flatten(group[world]);
			group.AppendClassicWorlds();
			for (int world = 0; world < 5; world++)
				Require(Equal(baseline[world], Flatten(group[world])), "Appending maps changed baseline " + type + (world + 1));
			for (int world = 6; world <= LevelCatalog.Count; world++)
			{
				LevelLoader.SegmentBucket bucket = group[world - 1];
				Require(bucket.Difficulty == world - 1 && !bucket.IsAborted, "Wrong or aborted bucket " + type + world);
				LevelCatalog.Level level = LevelCatalog.Get(world);
				TextAsset source = Resources.Load<TextAsset>(level.ResourceRoot + "/Maps/" + type + level.SourceNumber);
				Require(source != null && source.text.IndexOf('\r') < 0, "Missing/non-LF map " + type + world);
				List<int> expected = ReadNormalizedCells(source.text);
				Require(Equal(expected, Flatten(bucket)), "Cell/row/marker mismatch " + type + world);
				checkedCells += expected.Count;
				bucket.Reset();
				bucket.ChooseSegment();
				Require(LevelLoader.ChosenSegmentIndex == (type == SegmentType.Level ? bucket.SegmentsTotal - 1 : 0),
					"Incorrect initial playback segment " + type + world);
				Resources.UnloadAsset(source);
			}
		}
		int[] lengths = { 1095, 1150, 914 };
		int[] samples = { 5306112, 5649408, 4387968 };
		int[] nextWorlds = { 7, 8, 39 };
		int originalWorld = GameManager.WorldNum;
		try
		{
			for (int world = 6; world <= 8; world++)
			{
				GameManager.SetWorldNum(world);
				LevelDesigner.Reset();
				Require(LevelDesigner.WorldLength == lengths[world - 6], "Wrong world goal " + world);
				Require(!LevelDesigner.WorldIsEndless, "Classic world incorrectly became endless");
				Require(GameManager.GetNextWorldNum() == nextWorlds[world - 6], "Wrong next-world boundary");
				string path = "Assets/Resources/Classic1471/Audio/Music" + world + ".wav";
				AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
				Require(clip != null && clip.frequency == 44100 && clip.channels == 2 && clip.samples == samples[world - 6],
					"Imported soundtrack dimensions differ: " + path);
				AudioImporter importer = (AudioImporter)AssetImporter.GetAtPath(path);
				Require(!importer.forceToMono && importer.defaultSampleSettings.sampleRateSetting == AudioSampleRateSetting.PreserveSampleRate,
					"Soundtrack was downmixed or resampled: " + path);
			}
		}
		finally
		{
			GameManager.SetWorldNum(originalWorld);
			LevelDesigner.Reset();
		}
		ValidateAddedWorlds();
		ValidateAddedHazards();
		Debug.Log("PASS: original five map buckets unchanged; all added maps loaded in source cell/row order; " +
			checkedCells + " normalized cells checked; reverse segment start, three accepted goals, next-world continuation, and three accepted stereo soundtracks verified.");
	}

	private static void ValidateAddedWorlds()
	{
		// Exact native goals and decoded sample dimensions, not duration
		// estimates. 8Bits is mono; Midnight Carnival is 48 kHz.
		int[] worlds = { 34, 35, 36, 37, 38, 39, 40, 41 };
		int[] goals = { 780, 946, 1150, 1142, 1105, 1180, 764, 490 };
		int[] rates = { 44100, 44100, 44100, 48000, 44100, 44100, 44100, 44100 };
		int[] channels = { 2, 1, 2, 2, 2, 2, 2, 2 };
		int[] samples = { 4384512, 5113728, 5672448, 6044544, 5647104, 5647104, 4035456, 5294592 };
		int originalWorld = GameManager.WorldNum;
		try
		{
			for (int i = 0; i < worlds.Length; i++)
			{
				GameManager.SetWorldNum(worlds[i]);
				LevelDesigner.Reset();
				LevelCatalog.Level level = LevelCatalog.Get(worlds[i]);
				Require(LevelDesigner.WorldLength == goals[i] && !LevelDesigner.WorldIsEndless,
					"Wrong original goal: " + level.Title);
				Require(level.MaxGems == (worlds[i] == 41 ? 20 : 10) && level.MaxCrowns == (worlds[i] == 41 ? 0 : 3),
					"Wrong pickup goals: " + level.Title);
				Require(ClassicCrownRandom.Enabled == (worlds[i] == 34 || worlds[i] == 35),
					"Wrong original crown mode: " + level.Title);
				string path = "Assets/Resources/" + level.ResourceRoot + "/Audio/Music" + level.SourceNumber + ".wav";
				ExpansionPrefabImport.CheckAudio(new ClassicPrefabImport.AudioRecord {
					path = path, frequency = rates[i], channels = channels[i], frames = samples[i]
				});
				AudioImporter importer = (AudioImporter)AssetImporter.GetAtPath(path);
				Require(!importer.forceToMono && importer.defaultSampleSettings.sampleRateSetting == AudioSampleRateSetting.PreserveSampleRate,
					"Soundtrack was downmixed or resampled: " + level.Title);
			}
		}
		finally { GameManager.SetWorldNum(originalWorld); LevelDesigner.Reset(); }
		Debug.Log("PASS: eight additional native goals, gem/crown modes and original mono/stereo/sample-rate soundtrack dimensions verified.");
	}

	private static void ValidateAddedHazards()
	{
		int[] worlds = { 37, 39, 41 };
		Tile.DangerType[][] dangers = {
			new Tile.DangerType[] { Tile.DangerType.EnemyLv30Frankenstein, Tile.DangerType.EnemyLv30SpiderDie,
				Tile.DangerType.EnemyLv30SpiderLife, Tile.DangerType.EnemyLv30Door },
			new Tile.DangerType[] { Tile.DangerType.EnemyLv42PendulumLeft, Tile.DangerType.EnemyLv42Column,
				Tile.DangerType.EnemyLv42Door, Tile.DangerType.BeanLv42ClownDown },
			new Tile.DangerType[] { Tile.DangerType.Enemy_807_Level69_mofang, Tile.DangerType.Enemy_808_Level69_yanjing,
				Tile.DangerType.Enemy_809_Level69_banyuanL, Tile.DangerType.Enemy_810_Level69_banyuanR }
		};
		bool[][] groundedKills = { new bool[] { true, true, false, false },
			new bool[] { true, true, false, false }, new bool[] { true, true, true, true } };
		bool[][] jumpingKills = { new bool[] { true, false, true, false },
			new bool[] { true, false, true, false }, new bool[] { true, false, false, false } };
		int originalWorld = GameManager.WorldNum;
		GameObject root = new GameObject("Native hazard validation");
		try
		{
			Tile tile = root.AddComponent<Tile>();
			for (int world = 0; world < worlds.Length; world++)
			{
				GameManager.SetWorldNum(worlds[world]);
				for (int i = 0; i < dangers[world].Length; i++)
				{
					tile.ResetVariables();
					tile.SetAsDangerous(null, null, dangers[world][i]);
					Tile.DangerType used;
					Require(tile.ShouldKillBall(false, Vector3.zero, out used) == groundedKills[world][i] &&
						tile.ShouldKillBall(true, Vector3.zero, out used) == jumpingKills[world][i],
						"Native grounded/airborne collision changed: " + dangers[world][i]);
				}
				tile.HideRoot(true);
				tile.SetAsDangerous(null, null, dangers[world][0]);
				Tile.DangerType hiddenDanger;
				Require(!tile.ShouldKillBall(false, Vector3.zero, out hiddenDanger), "Hidden hazard killed the ball");
			}
		}
		finally { UnityEngine.Object.DestroyImmediate(root); GameManager.SetWorldNum(originalWorld); }
		Debug.Log("PASS: native Midnight/Circus/Sunny grounded, airborne and hidden hazard collision contracts verified.");
	}

	public static void ValidateMenu()
	{
		// https://rolling-sky.fandom.com/wiki/Level
		// we use names so the saves cant overlap
		string[] titles = ("Sunny|Cloud|Hoop Dream|The Winter|Massif|The Eye of Horus|Illusion|" +
			"Sky|Laser|Mental Rave|Fairy Tale|8Bits|JOKER|Dazzle|Monody|Forest|Ghost Dance|Desert|Candy|Kung Fu|Matrix|" +
			"Volcano|Ignite|City|Sci-Tech|Tetris|Circus|Reggae|Relics|Street Basketball|Egypt|Halloween|Castle in the Sky|" +
			"Poker|Deep Space|Midnight Carnival|E-labyrinth|Christmas|Neon|Cube|1UP").Split('|');
		int[] sources = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 17, 19, 21,
			23, 25, 13, 16, 22, 24, 27, 28, 29, 33, 35, 36, 37, 38, 40, 41, 18, 20, 26, 30, 32, 42, 39, 70 };
		Require(LevelCatalog.Count == titles.Length && LevelCatalog.Count == sources.Length, "Wrong catalogue size");
		HashSet<int> worlds = new HashSet<int>();
		int originalWorld = GameManager.WorldNum;
		try
		{
			for (int index = 0; index < titles.Length; index++)
			{
				LevelCatalog.Level level = LevelCatalog.AtMenuIndex(index);
				Require(level.Title == titles[index], "Wrong name/order at menu position " + index);
				Require(worlds.Add(level.WorldNumber) && LevelCatalog.MenuIndexOf(level.WorldNumber) == index,
					"Missing or repeated menu world " + level.Title);
				Require(LevelCatalog.Get(index + 1).SourceNumber == sources[index], "Moved save/asset slot " + (index + 1));
				GameManager.SetWorldNum(level.WorldNumber);
				Require(GameManager.WorldNum == level.WorldNumber, "Level still locked: " + level.Title);
				Require(LevelCatalog.Get(GameManager.GetNextWorldNum()).Title == titles[(index + 1) % titles.Length],
					"Wrong next level after " + level.Title);
			}
		}
		finally { GameManager.SetWorldNum(originalWorld); }
		Debug.Log("PASS: all 41 levels selectable without completion prerequisites; real names and wiki menu/next-level order verified; save/asset slots preserved.");
	}

	private static List<int> Flatten(LevelLoader.SegmentBucket bucket)
	{
		List<int> cells = new List<int>();
		foreach (LevelLoader.Segment segment in bucket.Segments)
			for (int row = 0; row < segment.Tiles.GetLength(0); row++)
				for (int column = 0; column < 5; column++) cells.Add(segment.Tiles[row, column]);
		return cells;
	}

	private static List<int> ReadNormalizedCells(string text)
	{
		List<int> cells = new List<int>();
		bool data = false;
		bool started = false;
		foreach (string line in text.Split('\n'))
		{
			if (line == "data=") { data = true; continue; }
			if (!data || line.Length == 0) continue;
			string[] values = line.TrimEnd(',').Split(',');
			Require(values.Length == 5, "Map width is not five");
			int[] row = Array.ConvertAll(values, int.Parse);
			foreach (int value in row) if (value != 0) started = true;
			if (!started) continue;
			foreach (int value in row)
			{
				int normalized = value;
				if (value >= 109 && value <= 116)
				{
					int[] markerGrounds = { 0, 1, 3, 2 };
					normalized = markerGrounds[(value - 109) % 4];
				}
				else if ((value >= 121 && value <= 126) || (value >= 133 && value <= 144))
				{
					// StampPlus*, Stamp* and StampNot* configure segment weights,
					// not geometry. Both native donors consume them as empty cells.
					// Keep the cell (and row), rather than dropping the directive.
					normalized = 0;
				}
				cells.Add(normalized);
			}
		}
		return cells;
	}

	private static bool Equal(List<int> first, List<int> second)
	{
		if (first.Count != second.Count) return false;
		for (int i = 0; i < first.Count; i++) if (first[i] != second[i]) return false;
		return true;
	}
}
