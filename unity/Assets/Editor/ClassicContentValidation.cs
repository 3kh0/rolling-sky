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
		int originalWorld = GameManager.WorldNum;
		try
		{
			for (int world = 6; world <= 8; world++)
			{
				GameManager.SetWorldNum(world);
				LevelDesigner.Reset();
				Require(LevelDesigner.WorldLength == lengths[world - 6], "Wrong world goal " + world);
				Require(!LevelDesigner.WorldIsEndless, "Classic world incorrectly became endless");
				Require(GameManager.GetNextWorldNum() == world + 1, "Wrong next-world boundary");
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
		Debug.Log("PASS: original five map buckets unchanged; all added maps loaded in source cell/row order; " +
			checkedCells + " normalized cells checked; reverse segment start, three accepted goals, next-world continuation, and three accepted stereo soundtracks verified.");
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
