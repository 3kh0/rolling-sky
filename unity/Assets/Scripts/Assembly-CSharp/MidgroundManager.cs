using UnityEngine;

public class MidgroundManager : MonoBehaviour
{
	private class Block
	{
		public readonly PrefabID PrefabID;

		public readonly Transform transform;

		public readonly bool HasTransform;

		private readonly Vector3 defaultPosition;

		private readonly float riseHeight;

		private readonly float direction;

		private float loweringTimer;

		private bool isLowering;

		private bool isLowered;

		private bool isNotLowering
		{
			get
			{
				return !isLowering;
			}
		}

		private bool isNotLowered
		{
			get
			{
				return !isLowered;
			}
		}

		public Block()
		{
			BlockData data = CurrentBlockData;
			riseHeight = data.RiseHeight;
			float offset;
			PrefabName prefab = data.GetPrefab(out HasTransform, out offset, out direction);
			if (HasTransform)
			{
				PrefabID = new PrefabID(PrefabType.Midground, prefab);
				transform = BufferManager.GetGeo(PrefabID);
				float startingHeight = data.StartingHeight;
				defaultPosition = new Vector3(2.5f + offset, blocksEnd + globalOffset, startingHeight);
				TransformUtils.Hide(transform);
			}
			else
			{
				PrefabID = PrefabID.Null;
				transform = null;
			}
			isLowering = false;
			isLowered = false;
			blocksEnd += 1f;
		}

		public void Name(string blockName)
		{
			if (HasTransform)
			{
				transform.name = blockName;
			}
		}

		public void UpdateRise(float risePercent)
		{
			if (!HasTransform || !isNotLowered)
			{
				return;
			}
			float num = 0f;
			if (isLowering)
			{
				loweringTimer -= Time.smoothDeltaTime;
				if (loweringTimer <= 0f)
				{
					isLowered = true;
					num = 1f;
				}
				else
				{
					num = 1f - loweringTimer / 2f;
					num = FloatAnim.PowerSmooth(num, true, false, 2);
				}
			}
			float value = ((!(risePercent >= 1f)) ? risePercent : 1f);
			value = FloatAnim.PowerSmooth(value, false, true, 2);
			if (isLowering)
			{
				value *= 1f - num;
			}
			Vector3 position = defaultPosition;
			position.z -= value * riseHeight;
			if (isLowering)
			{
				position.y -= num * 10f;
				position.z += num * 15f;
			}
			transform.position = position;
		}

		public void StartLowering()
		{
			if (HasTransform && isNotLowering && isNotLowered)
			{
				isLowering = true;
				loweringTimer = 2f;
			}
		}

		public void Clear()
		{
			if (HasTransform)
			{
				BufferManager.GiveGeo(transform, PrefabID, true);
				transform.name += "-CLEARED";
			}
		}
	}

	private struct BlockData
	{
		public readonly PrefabName[] Prefabs;

		public readonly float Height;

		public readonly float RiseHeight;

		public readonly float StartingHeight;

		public readonly int BlankChances;

		private MathUtils.Range offsetRange;

		public BlockData(int number, float blockHeight, float blockLowerAmmount, float minX, float maxX, int blankChances, params PrefabName[] prefabs)
		{
			Height = blockHeight;
			RiseHeight = blockLowerAmmount;
			StartingHeight = Height + RiseHeight;
			Prefabs = prefabs;
			offsetRange = new MathUtils.Range(minX, maxX);
			BlankChances = blankChances;
		}

		public PrefabName GetPrefab(out bool hasTransform, out float offset, out float direction)
		{
			PrefabName result = Prefabs[Randomizer.GetRandomIndex(Prefabs.Length)];
			int randomInt = Randomizer.GetRandomInt(0, 1 + BlankChances);
			int num = randomInt;
			if (num == 0 || num == 1)
			{
				bool flag = randomInt == 1;
				direction = ((!flag) ? 1f : (-1f));
				hasTransform = true;
				offset = offsetRange.Random * direction;
			}
			else
			{
				hasTransform = false;
				offset = 0f;
				direction = 0f;
			}
			return result;
		}
	}

	private const bool nameBlocks = true;

	private const float blockStartingPoint = -3f;

	private const float blockRisenPercent = 0.2f;

	private const float backgroundDepth = 30f;

	private const float backgroundParallax = 0.15f;

	private const float worldCenter = 2.5f;

	private const float loweringAmmountZ = 15f;

	private const float loweringAmmountY = 10f;

	private const float loweringTime = 2f;

	private const string backgroundLerpName = "_Ammount";

	private const string backgroundAltMaterialName = "_AltTex";

	private static BlockData[] blockData = new BlockData[62]
	{
		new BlockData(0, 5f, 15f, 0f, 12f, 4, PrefabName.Block1),
		new BlockData(1, 8f, 15f, 2f, 7f, 2, PrefabName.Block2),
		new BlockData(2, 7.5f, 27f, 4f, 9f, 0, PrefabName.Block3L, PrefabName.Block3R),
		new BlockData(3, 7.75f, 21f, 0f, 10f, 12, PrefabName.Block4L, PrefabName.Block4R),
		new BlockData(4, 5f, 15f, 0f, 12f, 2, PrefabName.Block1),
		new BlockData(5, 5f, 15f, 0f, 12f, 4, PrefabName.Block1),
		new BlockData(6, 5f, 15f, 0f, 12f, 2, PrefabName.Block1),
		new BlockData(7, 7.75f, 21f, 0f, 10f, 12, PrefabName.Block4L, PrefabName.Block4R),
		new BlockData(8, 8f, 15f, 2f, 7f, 2, PrefabName.Block2),
		// Original associations, not filenames inferred from the theme number.
		new BlockData(9, 8f, 15f, 2f, 7f, 2, PrefabName.ClassicMidground5),
		new BlockData(10, 8f, 15f, 2f, 7f, 2, PrefabName.ClassicMidground5),
		new BlockData(11, 5f, 15f, 0f, 12f, 4, PrefabName.ClassicMidground1),
		new BlockData(12, 5f, 15f, 0f, 12f, 4, PrefabName.ClassicMidground1),
		new BlockData(13, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground8L, PrefabName.ClassicMidground8R),
		new BlockData(14, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground9, PrefabName.ClassicMidground9),
		new BlockData(15, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground9, PrefabName.ClassicMidground9),
		new BlockData(16, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground10L, PrefabName.ClassicMidground10R),
		new BlockData(17, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground11L, PrefabName.ClassicMidground11R),
		new BlockData(18, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground12L, PrefabName.ClassicMidground12R),
		new BlockData(19, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(20, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(21, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(22, 5f, 15f, 0f, 12f, 2, PrefabName.ClassicMidground1),
		new BlockData(23, 14f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground14),
		new BlockData(24, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground15),
		new BlockData(25, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground15),
		new BlockData(26, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground16),
		new BlockData(27, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground16),
		new BlockData(28, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground15),
		new BlockData(29, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground15),
		new BlockData(30, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(31, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(32, 12f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground13L, PrefabName.ClassicMidground13R),
		new BlockData(33, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground19, PrefabName.ClassicMidground19),
		new BlockData(34, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground19, PrefabName.ClassicMidground19),
		new BlockData(35, 8f, 12f, 2f, 12f, 6, PrefabName.ClassicMidground19, PrefabName.ClassicMidground19),
		new BlockData(36, 8f, 15f, 2f, 7f, 2, PrefabName.ClassicMidground2),
		new BlockData(37, 7.5f, 27f, 4f, 9f, 0, PrefabName.ClassicMidground3L, PrefabName.ClassicMidground3R),
		new BlockData(38, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1561Midground16),
		new BlockData(39, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1561Midground16),
		new BlockData(40, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1561Midground10L, PrefabName.Classic1561Midground10R),
		new BlockData(41, 5f, 15f, 0f, 12f, 4, PrefabName.Classic1561Midground1),
		new BlockData(42, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1561Midground16),
		new BlockData(43, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1561Midground16),
		new BlockData(44, 8f, 15f, 2f, 7f, 2, PrefabName.Classic1782Block2),
		new BlockData(45, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground11L, PrefabName.Classic1782midground11R),
		new BlockData(46, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(47, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(48, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(49, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(50, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(51, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(52, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(53, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(54, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(55, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(56, 7.75f, 21f, 0f, 10f, 12, PrefabName.Classic1782Block4L, PrefabName.Classic1782Block4R),
		new BlockData(57, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(58, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(59, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(60, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16),
		new BlockData(61, 8f, 12f, 2f, 12f, 6, PrefabName.Classic1782midground16)
	};

	private static BlockData classicNeutralBlock = new BlockData(8, 8f, 15f, 2f, 7f, 2, PrefabName.ClassicMidground2);
	private static bool worldContextIsClassic;

	private static BlockData CurrentBlockData
	{
		get { return currentWorldNum.Value == 8 && (LevelDesigner.IsClassicWorld || worldContextIsClassic) ? classicNeutralBlock : blockData[currentWorldNum.Value]; }
	}

	private static bool isInitialized;

	private static float blocksEnd;

	private static Block[] blocks;

	private static float blocksLengthAsFloat;

	private static MathUtils.Index currentBlock;

	private static int? lastBlock;

	private static MathUtils.Index risingBlock;

	private static MathUtils.Index currentWorldNum;

	private static bool firstUpdate;

	private static int tilesUntilBlockChange;

	private static float globalOffset;

	private static float lastPosition;

	public static void Initialize(int worldIndex)
	{
		ConfigureMidground(worldIndex, null);
	}

	public static void InitializeForWorld(int worldIndex)
	{
		worldContextIsClassic = worldIndex >= 5;
		Initialize(LevelDesigner.GetStartingThemeNumber(worldIndex + 1) - 1);
	}

	public static void StartGeneratingForWorld(int worldIndex)
	{
		worldContextIsClassic = worldIndex >= 5;
		StartGeneratingFor(LevelDesigner.GetStartingThemeNumber(worldIndex + 1) - 1);
	}

	private static void ConfigureMidground(int worldIndex, float? offset)
	{
		if (!DeviceQualityChecker.QualityIsNotPour())
		{
			return;
		}
		if (offset.HasValue)
		{
			globalOffset = offset.Value;
		}
		else
		{
			globalOffset = 0f;
		}
		if (isInitialized)
		{
			currentWorldNum.Set(worldIndex);
			if (blocks != null)
			{
				ClearBlocks();
			}
		}
		else
		{
			int num = MathUtils.Ceiled(30f);
			blocks = new Block[num];
			blocksLengthAsFloat = blocks.Length;
			currentWorldNum = new MathUtils.Index(blockData.Length, worldIndex);
			currentBlock = new MathUtils.Index(num);
			risingBlock = new MathUtils.Index(num);
		}
		blocksEnd = -3f;
		for (int i = 0; i < blocks.Length; i++)
		{
			blocks[i] = new Block();
			TryNameBlock(i, true);
		}
		tilesUntilBlockChange = 1;
		currentBlock.Set(-1);
		isInitialized = true;
		lastPosition = Camera.main.transform.position.y;
	}

	public static void UpdateMidground()
	{
		if (!isInitialized)
		{
			return;
		}
		float y = Camera.main.transform.position.y;
		int num = MathUtils.Floored(y) - MathUtils.Floored(lastPosition);
		lastPosition = y;
		if (num > 0)
		{
			tilesUntilBlockChange -= num;
			if (tilesUntilBlockChange <= 0)
			{
				if (lastBlock.HasValue)
				{
					blocks[lastBlock.Value].UpdateRise(1f);
				}
				lastBlock = currentBlock.Value;
				currentBlock.Append();
				blocks[currentBlock.Value].Clear();
				blocks[currentBlock.Value] = new Block();
				TryNameBlock(currentBlock.Value, false);
				tilesUntilBlockChange = 1;
			}
		}
		risingBlock.Set(currentBlock);
		for (int i = 0; i < blocks.Length; i++)
		{
			float num2 = i;
			float num3 = num2 / blocksLengthAsFloat;
			float num4 = y % 1f / blocksLengthAsFloat;
			blocks[risingBlock.Value].UpdateRise(num3 + num4);
			risingBlock.Descend();
		}
	}

	public static void SwitchTo(int newWorldIndex, float offset)
	{
		if (isInitialized)
		{
			if (MathUtils.IndexIsNotWithin(newWorldIndex, blockData))
			{
				Debug.LogWarning(string.Format("BGMN: ERROR: Attempt to SwitchTo() new world num with a value of {0} - outside of value range 0 to {1}.  Indexing back within range", newWorldIndex, blockData.Length - 1));
			}
			TryClear();
			ConfigureMidground(newWorldIndex, offset);
		}
	}

	public static void StartGeneratingFor(int newWorldIndex)
	{
		if (isInitialized)
		{
			if (MathUtils.IndexIsNotWithin(newWorldIndex, blockData))
			{
				Debug.LogWarning(string.Format("BGMN: ERROR: Attempt to StartGeneratingFor() new world num with a value of {0} - outside of value range 0 to {1}.  Indexing back within range", newWorldIndex, blockData.Length - 1));
			}
			currentWorldNum.Set(newWorldIndex);
			StartLoweringOld();
		}
	}

	public static void TryClear()
	{
		if (isInitialized)
		{
			for (int i = 0; i < blocks.Length; i++)
			{
				blocks[i].Clear();
			}
			isInitialized = false;
		}
	}

	private static void ClearBlocks()
	{
		for (int i = 0; i < blocks.Length; i++)
		{
			blocks[i].Clear();
		}
	}

	private static void TryNameBlock(int blockIndex, bool createdAtStart)
	{
		string arg = ((!createdAtStart) ? "-RE" : null);
		string blockName = string.Format("Block {0}.{1}{2}", currentWorldNum.Value, blockIndex, arg);
		blocks[blockIndex].Name(blockName);
	}

	private static void StartLoweringOld()
	{
		for (int i = 0; i < blocks.Length; i++)
		{
			blocks[i].StartLowering();
		}
	}
}
