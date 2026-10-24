using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// Keep the accepted baseline separate from subsequent player builds.
public static class FidelityBuild
{
	[UnityEditor.Callbacks.PostProcessScene]
	public static void StripWebMusicReferences()
	{
		if (!BuildPipeline.isBuildingPlayer || EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL) return;
		foreach (MusicPlayer player in UnityEngine.Object.FindObjectsOfType<MusicPlayer>())
			player.music = new AudioClip[0];
	}

	public static void BuildWebGL()
	{
		Build("FidelityBaseline");
	}

	public static void BuildWebPlayer()
	{
		Build("WebPlayer");
	}

	public static void ConfigureMusic()
	{
		for (int world = 1; world <= LevelCatalog.Count; world++)
		{
			LevelCatalog.Level level = LevelCatalog.Get(world);
			string path = world <= 5 ? "Assets/AudioClip/Music" + world + ".mp3" :
				"Assets/Resources/" + level.ResourceRoot + "/Audio/Music" + level.SourceNumber + ".wav";
			AudioImporter importer = AssetImporter.GetAtPath(path) as AudioImporter;
			if (importer == null) throw new InvalidOperationException("Missing soundtrack importer: " + path);
			AudioImporterSampleSettings settings = importer.defaultSampleSettings;
			bool changed = settings.loadType != AudioClipLoadType.CompressedInMemory || importer.preloadAudioData;
			// Unity 2017.4 Streaming drops its WebAudio buffer at Stop. Retain it
			// between preview/retry/resume; MusicPlayer explicitly loads and evicts PCM.
			settings.loadType = AudioClipLoadType.CompressedInMemory;
			importer.defaultSampleSettings = settings;
			importer.preloadAudioData = false;
			if (importer.ContainsSampleSettingsOverride("WebGL"))
			{
				settings = importer.GetOverrideSampleSettings("WebGL");
				changed |= settings.loadType != AudioClipLoadType.CompressedInMemory;
				settings.loadType = AudioClipLoadType.CompressedInMemory;
				if (!importer.SetOverrideSampleSettings("WebGL", settings))
					throw new InvalidOperationException("Cannot configure WebGL soundtrack: " + path);
			}
			if (changed) importer.SaveAndReimport();
			AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
			if (clip == null || clip.loadType != AudioClipLoadType.CompressedInMemory || clip.preloadAudioData)
				throw new InvalidOperationException("Soundtrack cannot retain explicitly prepared audio: " + path);
		}
		Debug.Log("PASS: all " + LevelCatalog.Count + " soundtracks retain decoded audio with automatic preloading disabled; original sample/codec settings preserved.");
	}

	private static void Build(string name)
	{
		if (Application.unityVersion != "2017.4.40f1")
		{
			throw new InvalidOperationException("Use Unity 2017.4.40f1 for the fidelity baseline. Do not silently upgrade the project.");
		}
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
		{
			throw new InvalidOperationException("Launch Unity with -buildTarget WebGL and its WebGL support module installed.");
		}

		string project = Path.GetDirectoryName(Application.dataPath);
		string output = Path.GetFullPath(Path.Combine(project, "Builds/" + name));
		if (Directory.Exists(output) || File.Exists(output))
		{
			throw new InvalidOperationException("Build output already exists; preserve it before making another build: " + output);
		}

		List<string> scenes = new List<string>();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if (scene.enabled)
			{
				if (!File.Exists(Path.Combine(project, scene.path)))
				{
					throw new FileNotFoundException("Missing original scene", scene.path);
				}
				scenes.Add(scene.path);
			}
		}
		if (scenes.Count == 0)
		{
			throw new InvalidOperationException("No original scenes are enabled in EditorBuildSettings.");
		}

		ConfigureMusic();

		// Resources assets are always packed, even without a C# reference. Temporarily
		// take only the 18 music sources out of Assets; restore bytes AND GUIDs in
		// finally. Editor/native playback and the original content validators keep
		// their unchanged source assets. The first five are excluded by the scene
		// callback above. SFX remain untouched in both scenes and Resources.
		string stash = Path.Combine(project, "Library/StreamingMusicBuild");
		if (Directory.Exists(stash))
			throw new InvalidOperationException("Restore the interrupted build's music sources before rebuilding: " + stash);
		Directory.CreateDirectory(stash);
		List<string> moved = new List<string>();
		try
		{
			for (int world = 6; world <= LevelCatalog.Count; world++)
			{
				LevelCatalog.Level level = LevelCatalog.Get(world);
				string path = "Assets/Resources/" + level.ResourceRoot + "/Audio/Music" + level.SourceNumber + ".wav";
				foreach (string source in new string[] { path, path + ".meta" })
				{
					File.Move(source, Path.Combine(stash, Path.GetFileName(source)));
					moved.Add(source);
				}
			}
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
			// Unity 2017.4 returns an error string, not the newer BuildReport API.
			string error = BuildPipeline.BuildPlayer(scenes.ToArray(), output, BuildTarget.WebGL, BuildOptions.None);
			if (!string.IsNullOrEmpty(error)) throw new InvalidOperationException("WebGL build failed: " + error);
		}
		finally
		{
			foreach (string source in moved)
				File.Move(Path.Combine(stash, Path.GetFileName(source)), source);
			Directory.Delete(stash);
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		}
		if (!File.Exists(Path.Combine(output, "index.html")))
		{
			throw new InvalidOperationException("Unity returned without a WebGL index.html. The player has not been built.");
		}
		Debug.Log("Unity WebGL player built at " + output + ". Visual and gameplay comparison is still required.");
	}
}
