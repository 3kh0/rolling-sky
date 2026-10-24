using System.Collections.Generic;
using UnityEngine;

public partial class BufferManager : MonoBehaviour
{
	private struct BufferedTileGeo
	{
		public readonly Tile.Type TileType;

		public readonly Transform GeoTransform;

		public BufferedTileGeo(Tile.Type tileType, Transform geoTransform)
		{
			TileType = tileType;
			GeoTransform = geoTransform;
		}
	}

	private class GeoBuffer
	{
		private Transform[] storedGeo;

		private MathUtils.Index currentIndex;

		private MathUtils.Index returnedIndex;

		public PrefabID Type { get; private set; }

		public int Size { get; private set; }

		public string Name { get; private set; }

		public bool IsFilled { get; private set; }

		public bool IsNotFilled
		{
			get
			{
				return !IsFilled;
			}
		}

		public int TotalActive { get; private set; }

		public int TotalAvailable { get; private set; }

		public int FewestAvailable { get; private set; }

		public bool IsRootBuffer { get; private set; }

		public bool IsNotRootBuffer
		{
			get
			{
				return !IsRootBuffer;
			}
		}

		public bool HasWorldRestrictions { get; private set; }

		public ArrayUtils.Array<int> WorldRestrictions { get; private set; }

		public GeoBuffer(PrefabID bufferType, int bufferSize)
		{
			ConfigureGeoBuffer(bufferType, bufferSize);
		}

		public GeoBuffer(PrefabID bufferType, int bufferSize, params int[] worldRestrictions)
		{
			ConfigureGeoBuffer(bufferType, bufferSize, worldRestrictions);
		}

		private void ConfigureGeoBuffer(PrefabID bufferType, int bufferSize, int[] worldRestrictions = null)
		{
			Type = bufferType;
			Size = bufferSize;
			storedGeo = new Transform[Size];
			HasWorldRestrictions = worldRestrictions != null;
			if (HasWorldRestrictions)
			{
				if (worldRestrictions.Length == 1 && worldRestrictions[0] < 0)
				{
					int num = -worldRestrictions[0];
					int[] array = new int[6];
					int num2 = 0;
					for (int i = 0; i < array.Length; i++)
					{
						if (i == num)
						{
							num2++;
						}
						array[i] = i + num2;
					}
					worldRestrictions = array;
				}
				WorldRestrictions = new ArrayUtils.Array<int>(worldRestrictions);
			}
			Name = Type.ToString();
			currentIndex = new MathUtils.Index(Size);
			returnedIndex = new MathUtils.Index(Size);
			TotalActive = 0;
			TotalAvailable = 0;
			FewestAvailable = 0;
			IsFilled = false;
			IsRootBuffer = Type.prefabType == PrefabType.Root && Type.prefabName == PrefabName.Root;
		}

		public bool Give(Transform geoToAdd, bool hideGeo)
		{
			bool result = false;
			if (geoToAdd == null)
			{
				Debug.LogWarning("BFMN: Geo Buffer Warning: Attempt to add null geometry to geoBuffer: " + ToString());
			}
			else
			{
				HoldGeo(geoToAdd, hideGeo);
				TotalActive--;
				TotalAvailable++;
				returnedIndex.Append();
				if (IsRootBuffer)
				{
					Tile component = geoToAdd.gameObject.GetComponent<Tile>();
					if (component.IsInUse)
					{
						Debug.LogWarning(string.Format("BFMN: Geo Buffer recevied non-reset Tile.  Resetting now: ({0})", geoToAdd.name));
						component.ResetTile();
					}
				}
			}
			return result;
		}

		public void CreateGeo()
		{
			Transform transform = BufferManager.CreateGeo(Type);
			HoldGeo(transform, true);
			storedGeo[TotalAvailable] = transform;
			TotalAvailable++;
		}

		public void Fill()
		{
			if (IsFilled)
			{
				Debug.LogError("BFMN: ERROR: Attempt to reundantly fill already filled buffer: " + Type);
				return;
			}
			for (int i = 0; i < Size; i++)
			{
				CreateGeo();
			}
			IsFilled = true;
			FewestAvailable = TotalAvailable;
		}

		public void Clear()
		{
			storedGeo = null;
			IsFilled = false;
		}

		public void Reclaim(bool moveOffscreen)
		{
			if (IsFilled)
			{
				for (int i = 0; i < storedGeo.Length; i++)
				{
					HoldGeo(storedGeo[i], moveOffscreen);
				}
				currentIndex.Reset();
			}
		}

		public void ReclaimRoot()
		{
			int num = 0;
			for (int i = 0; i < storedGeo.Length; i++)
			{
				if (storedGeo[i].gameObject.GetComponent<Tile>().ResetTile())
				{
					num++;
				}
			}
		}

		public bool AppliesToCurrentWorld()
		{
			bool result = true;
			if (worldNumPersistantBuffered.HasValue && worldNumPersistantBuffered.Value == GameManager.WorldNum)
			{
				result = false;
			}
			else if (HasWorldRestrictions && WorldRestrictions.ContainsNot(GameManager.WorldNum))
			{
				result = false;
			}
			return result;
		}

		public Transform Get()
		{
			Transform transform = null;
			transform = storedGeo[currentIndex.Value];
			currentIndex.Append();
			TryToggle(transform, true);
			TotalActive++;
			TotalAvailable--;
			if (TotalAvailable < FewestAvailable)
			{
				FewestAvailable = TotalAvailable;
			}
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] != transform && componentsInChildren[i].CompareTag("Buffered"))
					{
						HoldGeo(componentsInChildren[i], false);
					}
				}
			}
			if (transform.transform.position.y > (float)GameManager.BallTilePosition.y - 9f)
			{
				if (debugReuseWarnings)
				{
					Debug.LogWarning(string.Format("BFMN: Geo Buffer not large enough - reusing active geo for {0}", Name));
				}
				HoldGeo(transform, true);
			}
			return transform;
		}

		public override string ToString()
		{
			return GetAsString(true, false);
		}

		public string ToString(bool includeName)
		{
			return GetAsString(includeName, false);
		}

		public string ToString(bool includeName, bool fullInfo)
		{
			return GetAsString(includeName, fullInfo);
		}

		private string GetAsString(bool includeName, bool fullInfo)
		{
			string arg = ((!includeName) ? null : string.Format("{0,-20}: ", Name));
			arg = ((!IsFilled) ? string.Format("{0}{1,3}", arg, Size) : string.Format("{0}{1,3}, {2,3}", arg, FewestAvailable, Size));
			if (fullInfo)
			{
				arg = string.Format("{0}\n{1,3}, {2,3}", arg, currentIndex, returnedIndex);
			}
			return arg;
		}
	}

	private const bool deviceDebugBenchmarking = false;

	private const string benchmarkingPrefix = "Benchmarking";

	private const int benchmarkingTimeStream = 2;

	private const bool debugNameGeo = false;

	private const bool debugRoots = false;

	private const bool bufferingEnabled = true;

	private const bool usingDebugMaterial = false;

	private const bool preBuffer = true;

	private const bool persistantBuffers = true;

	private const bool destructiveBuffers = true;

	private const bool displayDebugAtEnd = false;

	private const bool toggleParticles = true;

	private const string bufferedTag = "Buffered";

	private const string bufferManagerObject = "BufferManager";

	private const string bufferedGeoParentName = "_Buffered";

	private const float bufferedGeoParentDistance = -1000f;

	private const string fontName = "diamond_";

	public const int RowsToBufferAhead = 25;

	public const int RowsToBufferBehind = 10;

	public const int RowsToBufferTotal = 36;

	public const int RowsToBufferStartingAddition = 5;

	private const int defaultBufferSize = 10;

	private static bool debugsEnabled = true;

	private static bool debugPreBuffering = false;

	private static bool debugReuseWarnings = (byte)((debugsEnabled ? 1u : 0u) & 1u) != 0;

	private static bool showCapacityWarnings = true;

	private static Vector3 displayTextPos = new Vector3(-0.135f, 0.215f, 0.75f);

	private static Vector3 displayTextScale = new Vector3(0.0076f, 0.004f, 1f);

	private static GeoBuffer[] geoBuffers = new GeoBuffer[56]
	{
		new GeoBuffer(new PrefabID(PrefabType.Root, PrefabName.Root), 650),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX), 100),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX1), 25),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX2a), 20),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX2b), 10),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX3), 5),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileX4), 10),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileT), 120),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileT1a), 15),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileT1b), 15),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileT2), 20),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileL), 45),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileL1), 20),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileI), 95),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileD), 70),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileO), 120),
		new GeoBuffer(new PrefabID(PrefabType.Ground, PrefabName.TileOD), 200),
		new GeoBuffer(new PrefabID(PrefabType.Pickup, PrefabName.PickupBig), 10),
		new GeoBuffer(new PrefabID(PrefabType.Pickup, PrefabName.PickupStar), 2),
		new GeoBuffer(new PrefabID(PrefabType.Pickup, PrefabName.PickupText), 15),
		new GeoBuffer(new PrefabID(PrefabType.Particles, PrefabName.PickupBig), 15),
		new GeoBuffer(new PrefabID(PrefabType.Particles, PrefabName.PickupStar), 2),
		new GeoBuffer(new PrefabID(PrefabType.Particles, PrefabName.PortalStart), 1),
		new GeoBuffer(new PrefabID(PrefabType.Particles, PrefabName.PortalEnd), 1),
		new GeoBuffer(new PrefabID(PrefabType.Particles, PrefabName.Heat), 1),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Riser), 35),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Crusher), 25),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Roller), 15),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Pounder), 10),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Slasher), 5),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.BinaryMini), 15),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Floater), 30),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Tower), 25),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.TreeTall), 35),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.TreeSmall1), 50),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.TreeSmall2), 50),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.TreePalm), 10),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.PyramidSmall), 35),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.PyramidMid), 5),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Laser), 10),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Spotlight), 10),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.Pillar), 70),
		new GeoBuffer(new PrefabID(PrefabType.Enemy, PrefabName.PillarLaser), 10),
		new GeoBuffer(new PrefabID(PrefabType.Arrow, PrefabName.ArrowMover), 15),
		new GeoBuffer(new PrefabID(PrefabType.Arrow, PrefabName.ArrowMoverAuto), 15),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block1), 60),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block2), 60),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block3L), 45),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block3R), 45),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block4L), 20),
		new GeoBuffer(new PrefabID(PrefabType.Midground, PrefabName.Block4R), 20),
		new GeoBuffer(new PrefabID(PrefabType.General, PrefabName.Ball), 1),
		new GeoBuffer(new PrefabID(PrefabType.General, PrefabName.FinishLine), 2),
		new GeoBuffer(new PrefabID(PrefabType.General, PrefabName.Portal), 1),
		new GeoBuffer(new PrefabID(PrefabType.General, PrefabName.Sound), 50),
		new GeoBuffer(new PrefabID(PrefabType.Jump, PrefabName.Normal), 55)
	};

	public GameObject tileRootPrefab;

	public GameObject tileRootDebugPrefab;

	public GameObject groundPrefabX;

	public GameObject groundPrefabX1;

	public GameObject groundPrefabX2a;

	public GameObject groundPrefabX2b;

	public GameObject groundPrefabX3;

	public GameObject groundPrefabX4;

	public GameObject groundPrefabT;

	public GameObject groundPrefabT1a;

	public GameObject groundPrefabT1b;

	public GameObject groundPrefabT2;

	public GameObject groundPrefabL;

	public GameObject groundPrefabL1;

	public GameObject groundPrefabI;

	public GameObject groundPrefabD;

	public GameObject groundPrefabO;

	public GameObject groundPrefabOD;

	public GameObject ballPrefab;

	public GameObject pickupBigPrefab;

	public GameObject pickupStarPrefab;

	public GameObject pickupTextPrefab;

	public GameObject finishLinePrefab;

	public GameObject portalPrefab;

	public GameObject soundPrefab;

	public GameObject jumpPrefab;

	public GameObject riserPrefab;

	public GameObject crusherPrefab;

	public GameObject rollerPrefab;

	public GameObject hammerPrefab;

	public GameObject slasherPrefab;

	public GameObject binaryMiniPrefab;

	public GameObject floaterPrefab;

	public GameObject towerPrefab;

	public GameObject treeTallPrefab;

	public GameObject treeSmall1Prefab;

	public GameObject treeSmall2Prefab;

	public GameObject treePalmPrefab;

	public GameObject pyramidSmallPrefab;

	public GameObject pyramidMidPrefab;

	public GameObject pyramidBigPrefab;

	public GameObject laserPrefab;

	public GameObject spotlightPrefab;

	public GameObject pillarPrefab;

	public GameObject pillarLaserPrefab;

	public GameObject pickupExplosionPrefab;

	public GameObject pickupExplosionPrefabLight;

	public GameObject portalStartExplosionPrefab;

	public GameObject portalEndExplosionPrefab;

	public GameObject heatExplosionPrefab;

	public GameObject laserExplosionPrefab;

	public GameObject arrowMoverPrefab;

	public GameObject arrowMoverAutoPrefab;

	public GameObject midgroundBlock1Prefab;

	public GameObject midgroundBlock2Prefab;

	public GameObject midgroundBlock3LPrefab;

	public GameObject midgroundBlock3RPrefab;

	public GameObject midgroundBlock4LPrefab;

	public GameObject midgroundBlock4RPrefab;

	public GameObject debugText;

	public Material debugMaterial;

	private static Transform bufferedGeoParent;

	private static Transform bufferManager;

	private static Vector3 bufferedGeoParentPos = new Vector3(-1000f, -1000f, 0f);

	private static int totalGeosCreated;

	private static int totalTilesBufferedNow;

	private static int totalTilesDestroyed;

	private static TextMesh displayText;

	private static bool setup;

	private static bool persistantBuffersCreated;

	private static int lastReclaimedRow;

	private static int? worldNumPersistantBuffered;

	public static Material DebugMaterial;

	public static string BenchmarkingString { get; private set; }

	public static float BenchmarkingFloat { get; private set; }

	public static Dictionary<PrefabType, Dictionary<PrefabName, GameObject>> Prefabs { get; private set; }

	private static void PrintCreationDebug(PrefabID prefabType, string geoTransformName)
	{
		if (showCapacityWarnings)
		{
			Debug.Log(string.Format("BFMN: Needed to create new {0} type: {1}", prefabType, geoTransformName));
		}
	}

	private void OnEnable()
	{
		if (setup)
		{
			if (base.transform != bufferManager)
			{
				base.enabled = false;
			}
			return;
		}
		Prefabs = new Dictionary<PrefabType, Dictionary<PrefabName, GameObject>>();
		Prefabs.Add(PrefabType.Ground, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX, groundPrefabX);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX1, groundPrefabX1);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX2a, groundPrefabX2a);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX2b, groundPrefabX2b);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX3, groundPrefabX3);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileX4, groundPrefabX4);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileT, groundPrefabT);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileT1a, groundPrefabT1a);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileT1b, groundPrefabT1b);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileT2, groundPrefabT2);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileL, groundPrefabL);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileL1, groundPrefabL1);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileI, groundPrefabI);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileD, groundPrefabD);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileO, groundPrefabO);
		Prefabs[PrefabType.Ground].Add(PrefabName.TileOD, groundPrefabOD);
		Prefabs.Add(PrefabType.Jump, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Jump].Add(PrefabName.Normal, jumpPrefab);
		Prefabs.Add(PrefabType.Pickup, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Pickup].Add(PrefabName.PickupBig, pickupBigPrefab);
		Prefabs[PrefabType.Pickup].Add(PrefabName.PickupStar, pickupStarPrefab);
		Prefabs[PrefabType.Pickup].Add(PrefabName.PickupText, pickupTextPrefab);
		Prefabs.Add(PrefabType.Enemy, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Enemy].Add(PrefabName.Riser, riserPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Crusher, crusherPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Roller, rollerPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Pounder, hammerPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Slasher, slasherPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.BinaryMini, binaryMiniPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Floater, floaterPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Tower, towerPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.TreeTall, treeTallPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.TreeSmall1, treeSmall1Prefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.TreeSmall2, treeSmall2Prefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.TreePalm, treePalmPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.PyramidSmall, pyramidSmallPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.PyramidMid, pyramidMidPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.PyramidBig, pyramidBigPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Laser, laserPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Spotlight, spotlightPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.Pillar, pillarPrefab);
		Prefabs[PrefabType.Enemy].Add(PrefabName.PillarLaser, pillarLaserPrefab);
		Prefabs.Add(PrefabType.Arrow, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Arrow].Add(PrefabName.ArrowMover, arrowMoverPrefab);
		Prefabs[PrefabType.Arrow].Add(PrefabName.ArrowMoverAuto, arrowMoverAutoPrefab);
		Prefabs.Add(PrefabType.Midground, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Midground].Add(PrefabName.Block1, midgroundBlock1Prefab);
		Prefabs[PrefabType.Midground].Add(PrefabName.Block2, midgroundBlock2Prefab);
		Prefabs[PrefabType.Midground].Add(PrefabName.Block3L, midgroundBlock3LPrefab);
		Prefabs[PrefabType.Midground].Add(PrefabName.Block3R, midgroundBlock3RPrefab);
		Prefabs[PrefabType.Midground].Add(PrefabName.Block4L, midgroundBlock4LPrefab);
		Prefabs[PrefabType.Midground].Add(PrefabName.Block4R, midgroundBlock4RPrefab);
		Prefabs.Add(PrefabType.General, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.General].Add(PrefabName.Ball, ballPrefab);
		Prefabs[PrefabType.General].Add(PrefabName.FinishLine, finishLinePrefab);
		Prefabs[PrefabType.General].Add(PrefabName.Portal, portalPrefab);
		Prefabs[PrefabType.General].Add(PrefabName.Sound, soundPrefab);
		Prefabs.Add(PrefabType.Debug, new Dictionary<PrefabName, GameObject>());
		Prefabs[PrefabType.Debug].Add(PrefabName.DebugText, debugText);
		Prefabs.Add(PrefabType.Save, new Dictionary<PrefabName, GameObject>());
		Prefabs.Add(PrefabType.Particles, new Dictionary<PrefabName, GameObject>());
		GameObject value = ((!DeviceQualityChecker.QualityIsPour()) ? pickupExplosionPrefab : pickupExplosionPrefabLight);
		Prefabs[PrefabType.Particles].Add(PrefabName.PickupBig, value);
		Prefabs[PrefabType.Particles].Add(PrefabName.PickupStar, value);
		Prefabs[PrefabType.Particles].Add(PrefabName.PortalStart, portalStartExplosionPrefab);
		Prefabs[PrefabType.Particles].Add(PrefabName.PortalEnd, portalEndExplosionPrefab);
		Prefabs[PrefabType.Particles].Add(PrefabName.Heat, heatExplosionPrefab);
		Prefabs[PrefabType.Particles].Add(PrefabName.Laser, laserExplosionPrefab);
		Prefabs.Add(PrefabType.Root, new Dictionary<PrefabName, GameObject>());
		GameObject value2 = tileRootPrefab;
		Prefabs[PrefabType.Root].Add(PrefabName.Root, value2);
		RegisterClassicPrefabs();
		RegisterExpansionPrefabs();
		DebugMaterial = debugMaterial;
		bufferManager = base.transform;
		setup = true;
	}

	private static void RegisterClassicPrefabs()
	{
		List<GeoBuffer> buffers = new List<GeoBuffer>(geoBuffers);
		// Original geoBuffer6..8 capacities, taking the largest where levels share a pool.
		// Separate IDs keep the accepted first-five prefabs and persistent pools unchanged.
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicRiser, "01_enemyRiser", 47, 6, 7, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicFloater, "07_enemyFloater", 15, 6, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicCrusher, "02_enemyCrusher", 25, 6, 7, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicPounder, "04_enemyHammer", 3, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicSlasher, "05_enemySlasher", 5, 6);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicBinaryMini, "06_enemyBinaryMini", 8, 6, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicRoller, "03_enemyRoller", 14, 6, 7, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicTreePalm, "12_enemyTreePalm", 10, 6);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TreePalmAlt, "12a_enemyTreePalmAlt", 10, 7, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicPillar, "18_enemyPillar", 70, 6, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicPillarLaser, "19_enemyPillarLaser", 10, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.ClassicSpotlight, "17_enemySpotlight", 15, 7, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Shinto, "25_Shinto", 10, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Building, "20_enemyBuilding", 30, 6);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Turret, "24_Turret", 20, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Switcher, "18_enemyPillar", 30, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8Switcher, "48_Lv8Switcher", 30, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8Pillar, "48_Lv8Switcher", 20, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8TreeTall, "39_Lv8TreeTall", 15, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8TreeSmall1, "36_Lv8TreeSmall1", 20, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8TreeSmall2, "37_Lv8TreeSmall2", 20, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Lv8Shinto, "40_Lv8Shinto", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Them14TreeTall, "46_Them14TreeTall", 10, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Them14TreeSmall1, "44_Them14TreeSmall1", 5, 8);
		// geoBuffer8 omits this selected variant; the donor's general buffer table reserves twenty.
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Them14TreeSmall2, "45_Them14TreeSmall2", 20, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.Them14Shinto, "47_Them14Shinto", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine01, "28_TetrisDownOneLine01", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine02, "29_TetrisDownOneLine02", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine03, "30_TetrisDownOneLine03", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine04, "31_TetrisDownOneLine04", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine05, "32_TetrisDownOneLine05", 5, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine06, "33_TetrisDownOneLine06", 10, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine07, "34_TetrisDownOneLine07", 15, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TetrisDownOneLine08, "35_TetrisDownOneLine08", 10, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.OneBlockInToOffD, "42_Lv8InvisibieInToOffD", 15, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.OneBlockInToOff, "41_Lv8InvisibieInToOff", 10, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.OneBlockOffToInD, "43_Lv8InvisibieOffToInD", 25, 8);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.RadioTower, "23_radioTower", 10, 6, 7);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.FloodLaser, "22_floodLaser", 10, 6);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.FloodLight, "21_floodLight", 35, 6);
		AddClassicPrefab(buffers, PrefabType.Enemy, PrefabName.TeslaTower, "27_Tesla", 10, 7, 8);
		// Midgrounds also appear while browsing the menu, before WorldNum changes.
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground1, "midground1a", 60);
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground2, "midground2a", 60);
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground5, "midground5", 60);
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground8L, "midground8L", 40);
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground8R, "midground8R", 10);
		AddClassicPrefab(buffers, PrefabType.Midground, PrefabName.ClassicMidground9, "midground9", 40);
		geoBuffers = buffers.ToArray();
	}

	private static void AddClassicPrefab(List<GeoBuffer> buffers, PrefabType type, PrefabName name, string resourceName, int capacity, params int[] worlds)
	{
		string path = "Classic1471/Prefabs/" + resourceName;
		GameObject prefab = Resources.Load<GameObject>(path);
		if (prefab == null)
		{
			throw new System.InvalidOperationException("Missing original classic prefab: " + path);
		}
		Prefabs[type].Add(name, prefab);
		PrefabID id = new PrefabID(type, name);
		buffers.Add(worlds.Length == 0 ? new GeoBuffer(id, capacity) : new GeoBuffer(id, capacity, worlds));
	}

	public static PrefabID ClassicPrefabID(PrefabID id)
	{
		if (id.prefabType != PrefabType.Enemy)
		{
			return id;
		}
		PrefabName name;
		switch (id.prefabName)
		{
			case PrefabName.Riser: name = PrefabName.ClassicRiser; break;
			case PrefabName.Floater: name = PrefabName.ClassicFloater; break;
			case PrefabName.Crusher: name = PrefabName.ClassicCrusher; break;
			case PrefabName.Pounder: name = PrefabName.ClassicPounder; break;
			case PrefabName.Slasher: name = PrefabName.ClassicSlasher; break;
			case PrefabName.BinaryMini: name = PrefabName.ClassicBinaryMini; break;
			case PrefabName.Roller: name = PrefabName.ClassicRoller; break;
			case PrefabName.TreePalm: name = PrefabName.ClassicTreePalm; break;
			case PrefabName.Pillar: name = PrefabName.ClassicPillar; break;
			case PrefabName.PillarLaser: name = PrefabName.ClassicPillarLaser; break;
			case PrefabName.Spotlight: name = PrefabName.ClassicSpotlight; break;
			default: return id;
		}
		return new PrefabID(id.prefabType, name);
	}

	public static void Initialize()
	{
		totalGeosCreated = 0;
		totalTilesBufferedNow = 0;
		totalTilesDestroyed = 0;
		int num = 0;
		if (!persistantBuffersCreated || GameManager.WorldNum != worldNumPersistantBuffered.Value)
		{
			for (int i = 0; i < geoBuffers.Length; i++)
			{
				if (geoBuffers[i].AppliesToCurrentWorld())
				{
					totalGeosCreated += FillBuffer(i);
					num++;
				}
			}
		}
		if (totalGeosCreated > 0 && debugPreBuffering)
		{
			Debug.Log(string.Format("BFMN: DEBUG: PrefBuffered {0} geos of {1} types", totalGeosCreated, num));
		}
	}

	public static void DisplayRow()
	{
		DisplayRow(GetLeadingRow());
	}

	public static void DisplayStartingRows()
	{
		for (int i = 0; i <= GetLeadingRow(); i++)
		{
			DisplayRow(i);
		}
	}

	public static void GiveGeo(Transform givenGeo, PrefabID givenGeoPrefabId)
	{
		GiveGeo(givenGeo, givenGeoPrefabId, false);
	}

	public static void GiveGeo(Transform givenGeo, PrefabID givenGeoPrefabId, bool hideGivenGeo)
	{
		// A particle or tile can return after the menu/world selection changes.
		// Return it to the pool that created it, not the newly selected revision.
		PrefabID originalID;
		if (givenGeo != null && originalGeoIDs.TryGetValue(givenGeo, out originalID))
			givenGeoPrefabId = originalID;
		if (IsBufferable(givenGeoPrefabId))
		{
			GiveGeoToBuffer(givenGeoPrefabId, givenGeo, hideGivenGeo);
		}
		else
		{
			QueueForDestruction(givenGeo);
		}
	}

	public static Transform GetGeo(PrefabType prefabType, PrefabName prefabName)
	{
		return GetGeo(new PrefabID(prefabType, prefabName));
	}

	public static Transform GetGeo(PrefabID prefabType)
	{
		prefabType = ResolveExpansionPrefab(prefabType);
		Transform transform;
		if (IsBufferable(prefabType))
		{
			transform = GetGeoFromBuffer(prefabType);
		}
		else
		{
			Debug.LogWarning("Non bufferable geo detected!!!");
			transform = CreateGeo(prefabType);
			ParentToGroup(transform, true);
		}
		return transform;
	}

	public static void HoldGeo(Transform geoToHold, bool hideHeldGeo)
	{
		ParentToGroup(geoToHold, hideHeldGeo);
		TryToggle(geoToHold, false);
	}

	public static bool IsBufferable(PrefabID prefabId)
	{
		return true;
	}

	public static int GetBufferSize(PrefabType geoPrefabType, PrefabName geoPrefabName)
	{
		return GetBufferSize(new PrefabID(geoPrefabType, geoPrefabName));
	}

	public static int GetBufferSize(PrefabID geoType)
	{
		int result = 0;
		int num = BufferIndexFromId(geoType);
		if (num != -1)
		{
			result = geoBuffers[num].Size;
		}
		return result;
	}

	public static int GetTotalAvailable(PrefabType geoPrefabType, PrefabName geoPrefabName)
	{
		return GetTotalAvailable(new PrefabID(geoPrefabType, geoPrefabName));
	}

	public static int GetTotalAvailable(PrefabID geoType)
	{
		int result = 0;
		int num = BufferIndexFromId(geoType);
		if (num != -1)
		{
			result = geoBuffers[num].TotalAvailable;
		}
		return result;
	}

	public static int GetFewestAvailable(PrefabType geoPrefabType, PrefabName geoPrefabName)
	{
		return GetFewestAvailable(new PrefabID(geoPrefabType, geoPrefabName));
	}

	public static int GetFewestAvailable(PrefabID geoType)
	{
		int result = 0;
		int num = BufferIndexFromId(geoType);
		if (num != -1)
		{
			result = geoBuffers[num].FewestAvailable;
		}
		return result;
	}

	public static string GetBufferInfo(PrefabType geoPrefabType, PrefabName geoPrefabName)
	{
		return GetBufferInfo(new PrefabID(geoPrefabType, geoPrefabName));
	}

	public static string GetBufferInfo(PrefabID geoType)
	{
		string result = null;
		int num = BufferIndexFromId(geoType);
		if (num != -1)
		{
			result = geoBuffers[num].ToString(false, true);
		}
		return result;
	}

	public static void Reclaim(bool fullReclaim)
	{
		if (!fullReclaim)
		{
			bool flag = false;
			for (int i = 0; i < geoBuffers.Length; i++)
			{
				if (geoBuffers[i].IsRootBuffer)
				{
					geoBuffers[i].ReclaimRoot();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Debug.LogError("BFMN: ERROR: Was unable to find Tile Root buffer to trigger reclaimation of tiles");
			}
		}
		for (int j = 0; j < geoBuffers.Length; j++)
		{
			geoBuffers[j].Reclaim(fullReclaim);
		}
	}

	private static int GetLeadingRow()
	{
		return GameManager.BallRowNum + 25;
	}

	private static void DisplayRow(int rowNumToDisplay)
	{
		if (!GameManager.TileBoard.RowIsWithinWorld(rowNumToDisplay))
		{
			return;
		}
		bool flag = true;
		if (GameManager.TileBoard.IsRowInvalid(rowNumToDisplay))
		{
			LevelDesigner.TryChangeGeneratingTheme(rowNumToDisplay);
			GameManager.TileBoard.AddSegment();
			if (GameManager.TileBoard.RowIsNotWithinWorld(rowNumToDisplay))
			{
				flag = false;
			}
		}
		if (flag)
		{
			TryAffectRow(rowNumToDisplay);
		}
	}

	private static void TryAffectRow(int rowNum, bool recordActions = true)
	{
		if (GameManager.TileBoard.IsRowValid(rowNum))
		{
			for (int i = 0; i < 5; i++)
			{
				if (GameManager.TileBoard[rowNum, i] != null && GameManager.TileBoard[rowNum, i].IsGeoNotDisplayed)
				{
					GameManager.TileBoard[rowNum, i].Display();
				}
			}
			if (ExpansionGameplay.Enabled)
			{
				// A rotating group can refer to a later tile in this same row.
				// Bind only after every tile's original geometry exists.
				for (int i = 0; i < 5; i++)
				{
					Tile tile = GameManager.TileBoard[rowNum, i];
					if (tile != null) tile.ApplyRotationParent();
				}
			}
		}
		else
		{
			Debug.LogWarning(string.Format("BFMN: WARNING: WTF - tried to affect a row that didn't exist, specifically {0}", rowNum));
		}
	}

	private static Transform CreateGeo(PrefabID prefabId)
	{
		GameObject original;
		try
		{
			original = Prefabs[prefabId.prefabType][prefabId.prefabName];
		}
		catch
		{
			if (GameManager.WorldNum >= 9)
				throw new System.InvalidOperationException("Undefined expansion prefab: " + prefabId);
			Debug.Log(string.Format("BFMN: ERROR: Prefab {0} is NOT defined in BufferManager.OnEnable()!", prefabId));
			original = Prefabs[PrefabType.Ground][PrefabName.TileO];
		}
		if (original == null) original = LoadExpansionPrefab(prefabId);
		GameObject gameObject = (GameObject)Object.Instantiate(original);
		RestoreClassicAnimationNames(gameObject);
		gameObject.tag = "Buffered";
		Object.DontDestroyOnLoad(gameObject);
		originalGeoIDs.Add(gameObject.transform, prefabId);
		return gameObject.transform;
	}

	private static Transform GetGeoFromBuffer(PrefabID geoType)
	{
		Transform transform = null;
		bool flag = false;
		for (int i = 0; i < geoBuffers.Length; i++)
		{
			if (geoBuffers[i].Type.Equals(geoType))
			{
				if (geoBuffers[i].IsNotFilled)
				{
					Debug.LogWarning(string.Format("BFMN: ERROR: Attempt to get geo {0} from Buffer before that buffer was filled with geo,  Filling buffer now.", geoType));
					geoBuffers[i].Fill();
				}
				transform = geoBuffers[i].Get();
				flag = true;
				if (transform == null)
				{
					Debug.LogWarning(string.Format("BFMN: WARNING: Recieved null geo from apparently NON-empty geoBuffer: {0}", geoBuffers[i]));
				}
				break;
			}
		}
		if (!flag)
		{
			Debug.LogError(string.Format("BFMN: WARNING: Unable to find a geoBuffer for '{0}' type geometry - please define one to allow buffering of this type of geo)", geoType.ToString()));
		}
		return transform;
	}

	private static bool GiveGeoToBuffer(PrefabID geoType, Transform geoToAdd, bool hideGeo)
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < geoBuffers.Length; i++)
		{
			if (geoBuffers[i].Type.Equals(geoType))
			{
				flag2 = geoBuffers[i].Give(geoToAdd, hideGeo);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Debug.LogWarning(string.Format("BFMN: WARNING: Unable to find a geoBuffer for '{0}' type geometry - please define one to allow buffering of this type of geo)", geoType.ToString()));
			QueueForDestruction(geoToAdd);
			flag2 = true;
		}
		if (flag2)
		{
			totalTilesDestroyed++;
		}
		else
		{
			totalTilesBufferedNow++;
		}
		return flag2;
	}

	private static int FillBuffer(int bufferIndex)
	{
		int result = 0;
		if (geoBuffers[bufferIndex].IsNotFilled)
		{
			geoBuffers[bufferIndex].Fill();
			result = geoBuffers[bufferIndex].Size;
		}
		return result;
	}

	private static int BufferIndexFromId(PrefabID geoType)
	{
		geoType = ResolveExpansionPrefab(geoType);
		int result = -1;
		for (int i = 0; i < geoBuffers.Length; i++)
		{
			if (geoBuffers[i].Type.Equals(geoType))
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private static void QueueForDestruction(Transform geoToDestroy)
	{
		if (geoToDestroy == null)
		{
			Debug.Log("BFMN: Attempt to destroy null gameObject");
		}
		else
		{
			Debug.LogWarning("BFMN: Removing unneeded: " + geoToDestroy.name);
		}
	}

	private static void TryToggle(Transform geoToToggle, bool enable)
	{
		if (geoToToggle != null)
		{
			ParticleEmitter[] componentsInChildren = geoToToggle.GetComponentsInChildren<ParticleEmitter>();
			foreach (ParticleEmitter particleEmitter in componentsInChildren)
			{
				particleEmitter.emit = enable;
			}
		}
	}

	private static void ParentToGroup(Transform geoToParent, bool moveGeo)
	{
		if (bufferedGeoParent == null)
		{
			bufferedGeoParent = new GameObject("_Buffered").transform;
			bufferedGeoParent.position = bufferedGeoParentPos;
			Object.DontDestroyOnLoad(bufferedGeoParent);
		}
		if (geoToParent == null)
		{
			Debug.LogError("BFMN: ERROR: Attempted to parent Null geo to Buffer Parent Group");
			return;
		}
		geoToParent.parent = bufferedGeoParent;
		if (moveGeo)
		{
			geoToParent.localPosition = Vector3.zero;
			geoToParent.localEulerAngles = Vector3.zero;
		}
	}

	public static string ToString(bool sort)
	{
		string text;
		if (sort)
		{
			text = ToSortedString();
		}
		else
		{
			text = "BFMN: Buffers' status:\n" + geoBuffers[0].ToString();
			for (int i = 1; i < geoBuffers.Length; i++)
			{
				text = text + "\n" + geoBuffers[i].ToString();
			}
		}
		return text;
	}

	private static string ToSortedString()
	{
		ArrayUtils.List<int> list = new ArrayUtils.List<int>(geoBuffers.Length);
		ArrayUtils.List<int> list2 = new ArrayUtils.List<int>(geoBuffers.Length);
		MathUtils.IntPairNullable intPairNullable = new MathUtils.IntPairNullable(null, null);
		for (int i = 0; i < geoBuffers.Length; i++)
		{
			if (geoBuffers[i].IsNotFilled || geoBuffers[i].FewestAvailable == geoBuffers[i].Size)
			{
				list2.Add(i);
			}
			else
			{
				list.Add(i);
			}
		}
		string text;
		if (list.IsEmpty)
		{
			text = null;
		}
		else
		{
			for (int j = 0; j < list.Length - 1; j++)
			{
				for (int k = 0; k < list.Length - (1 + j); k++)
				{
					if (geoBuffers[list[k]].FewestAvailable > geoBuffers[list[k + 1]].FewestAvailable)
					{
						int value = list[k];
						list[k] = list[k + 1];
						list[k + 1] = value;
					}
				}
			}
			text = null;
			for (int l = 0; l < list.Length; l++)
			{
				text = text + "\n\t" + geoBuffers[list[l]].ToString();
			}
		}
		string text2;
		if (list2.IsEmpty)
		{
			text2 = null;
		}
		else
		{
			text2 = "\nUNUSED:";
			for (int m = 1; m < list2.Length; m++)
			{
				text2 = text2 + "\n\t" + geoBuffers[list2[m]].ToString();
			}
		}
		return "BFMN: Buffers' status:" + text + text2;
	}
}
