using System;
using System.Collections.Generic;
using UnityEngine;

public partial class BufferManager
{
	private static readonly Dictionary<PrefabID, string> expansionPrefabPaths = new Dictionary<PrefabID, string>();
	private static readonly Dictionary<PrefabID, PrefabID> earlierExpansionAliases = new Dictionary<PrefabID, PrefabID>();
	private static readonly Dictionary<PrefabID, PrefabID> laterExpansionAliases = new Dictionary<PrefabID, PrefabID>();
	private static readonly Dictionary<PrefabID, PrefabID> newerExpansionAliases = new Dictionary<PrefabID, PrefabID>();
	private static readonly Dictionary<PrefabID, PrefabID> circusExpansionAliases = new Dictionary<PrefabID, PrefabID>();
	private static readonly Dictionary<PrefabID, PrefabID> sunnyExpansionAliases = new Dictionary<PrefabID, PrefabID>();
	private static readonly Dictionary<Transform, PrefabID> originalGeoIDs = new Dictionary<Transform, PrefabID>();
	private static Material laterPropsMaterial;
	private static Material newerPropsMaterial;
	private static Material circusPropsMaterial;

	private static void RegisterExpansionPrefabs()
	{
		List<GeoBuffer> buffers = new List<GeoBuffer>(geoBuffers);
		RegisterExpansion1471(buffers);
		RegisterExpansion1561(buffers);
		RegisterExpansion1782(buffers);
		RegisterExpansion1792(buffers);
		RegisterExpansion325(buffers);
		geoBuffers = buffers.ToArray();
	}

	private static void RegisterExpansionPrefab(List<GeoBuffer> buffers, PrefabType type, PrefabName name,
		string path, int capacity, params int[] worlds)
	{
		PrefabID id = new PrefabID(type, name);
		expansionPrefabPaths.Add(id, path);
		if (!Prefabs.ContainsKey(type)) Prefabs.Add(type, new Dictionary<PrefabName, GameObject>());
		// Avoid loading every world's meshes, textures and sounds in the menu.
		// The existing 6–8 bindings are retained; new bindings load on first use.
		if (!Prefabs[type].ContainsKey(name)) Prefabs[type].Add(name, null);
		MergeExpansionPool(buffers, id, capacity, worlds);
	}

	private static void MergeExpansionPool(List<GeoBuffer> buffers, PrefabID id, int capacity, int[] worlds)
	{
		for (int i = 0; i < buffers.Count; i++)
		{
			GeoBuffer existing = buffers[i];
			if (!existing.Type.Equals(id)) continue;
			if (existing.IsFilled) throw new InvalidOperationException("Cannot change an active geometry pool: " + id);
			capacity = Math.Max(capacity, existing.Size);
			if (!existing.HasWorldRestrictions)
			{
				buffers[i] = new GeoBuffer(id, capacity);
			}
			else
			{
				List<int> combined = new List<int>(worlds);
				for (int j = 0; j < existing.WorldRestrictions.Length; j++)
				{
					int world = existing.WorldRestrictions[j];
					if (!combined.Contains(world)) combined.Add(world);
				}
				buffers[i] = new GeoBuffer(id, capacity, combined.ToArray());
			}
			return;
		}
		buffers.Add(new GeoBuffer(id, capacity, worlds));
	}

	public static PrefabID ResolveExpansionPrefab(PrefabID id)
	{
		if (GameManager.WorldNum < 9 || id.IsNull) return id;
		string root = LevelCatalog.Get(GameManager.WorldNum).ResourceRoot;
		Dictionary<PrefabID, PrefabID> aliases = root == "Classic325" ? sunnyExpansionAliases : root == "Classic1792" ? circusExpansionAliases : root == "Classic1782" ? newerExpansionAliases : LevelCatalog.Get(GameManager.WorldNum).SourceNumber >= 21
			? laterExpansionAliases : earlierExpansionAliases;
		PrefabID resolved;
		return aliases.TryGetValue(id, out resolved) ? resolved : id;
	}

	private static GameObject LoadExpansionPrefab(PrefabID id)
	{
		string path;
		if (!expansionPrefabPaths.TryGetValue(id, out path))
			throw new InvalidOperationException("Missing prefab binding: " + id);
		GameObject prefab = Resources.Load<GameObject>(path);
		if (prefab == null) throw new InvalidOperationException("Missing original expansion prefab: " + path);
		if (path.StartsWith("Classic1561/", StringComparison.Ordinal))
		{
			// Native ApplyPrefabMaterials replaces the bundled 15_mogu material
			// with the scene's packaged Props binding; it is not theme-recolored.
			foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
			{
				if (renderer.sharedMaterial == null || !laterPropsMaterialNames.Contains(renderer.sharedMaterial.name)) continue;
				if (laterPropsMaterial == null)
					laterPropsMaterial = Resources.Load<Material>("Classic1561/Materials/15_mogu");
				if (laterPropsMaterial == null) throw new InvalidOperationException("Missing original Props material");
				renderer.sharedMaterial = laterPropsMaterial;
			}
		}
		if (path.StartsWith("Classic1782/", StringComparison.Ordinal))
		{
			foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
			{
				Material material = renderer.sharedMaterial;
				if (material == null || !(material.name == "15_mogu" || material.name == "15_mogu_6f8006d1de" ||
					material.name == "15_mogu_55f7c15e10" || material.name == "15_mogu_d2f52d6eae")) continue;
				if (newerPropsMaterial == null)
					newerPropsMaterial = Resources.Load<Material>("Classic1782/Materials/15_mogu");
				if (newerPropsMaterial == null) throw new InvalidOperationException("Missing original 1.7.8.2 Props material");
				renderer.sharedMaterial = newerPropsMaterial;
			}
		}
		if (path.StartsWith("Classic1792/", StringComparison.Ordinal))
		{
			foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
			{
				Material material = renderer.sharedMaterial;
				if (material == null || !(material.name == "15_mogu" || material.name == "15_mogu_6f8006d1de" ||
					material.name == "15_mogu_55f7c15e10")) continue;
				if (circusPropsMaterial == null)
					circusPropsMaterial = Resources.Load<Material>("Classic1792/Materials/15_mogu");
				if (circusPropsMaterial == null) throw new InvalidOperationException("Missing original 1.7.9.2 Props material");
				renderer.sharedMaterial = circusPropsMaterial;
			}
		}
		Prefabs[id.prefabType][id.prefabName] = prefab;
		return prefab;
	}

	internal static void RestoreClassicAnimationNames(GameObject instance)
	{
		// Import may rename clips to exported filenames. Rename only each live
		// instance's states, leaving clips, prefab assets and playback values intact.
		foreach (Animation animation in instance.GetComponentsInChildren<Animation>(true))
		{
			List<AnimationState> states = new List<AnimationState>();
			foreach (AnimationState state in animation) states.Add(state);
			foreach (AnimationState state in states)
			{
				string nativeName;
				if (!earlierAnimationNames.TryGetValue(state.name, out nativeName) &&
					!laterAnimationNames.TryGetValue(state.name, out nativeName) &&
					!newerAnimationNames.TryGetValue(state.name, out nativeName) &&
					!circusAnimationNames.TryGetValue(state.name, out nativeName) &&
					!sunnyAnimationNames.TryGetValue(state.name, out nativeName)) continue;
				AnimationState existing = animation[nativeName];
				if (existing != null && existing != state)
					throw new InvalidOperationException("Ambiguous native animation state: " + nativeName);
				state.name = nativeName;
			}
		}
	}
}
