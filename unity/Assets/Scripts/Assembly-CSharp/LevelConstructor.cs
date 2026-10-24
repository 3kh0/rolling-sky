using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public partial class LevelConstructor : MonoBehaviour
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct SegmentData
	{
		public Tile[,] Segment { get; private set; }

		public int Number { get; private set; }

		public int TilesHigh { get; private set; }

		public int TilesWide { get; private set; }

		public float EndPosition { get; private set; }

		public SegmentData(Tile[,] segment, int segmentNum, float segmentOffset)
		{
			Number = segmentNum;
			Segment = segment;
			TilesHigh = segment.GetLength(0);
			TilesWide = segment.GetLength(1);
			EndPosition = segmentOffset + (float)TilesHigh;
		}
	}

	private const bool debugGroups = false;

	private const bool devMode = false;

	private const bool isolateJumps = true;

	private const float startingTilePosX = 0.5f;

	private static Transform worldParent;

	private static bool sgementHasFragiles;

	private static bool segmentHasMobiles;

	private static bool segmentHasRisers;

	private static bool segmentHasTallTrees;

	private static bool segmentHasDangerSpreaders;

	private static Dictionary<Direction, bool> emptyCorners;

	private static int emptyCornerssTotal;

	private static Transform[] allParents;

	private static int currentRow;

	private static int currentCol;

	private static Tile[,] currentSegment;

	private static bool[] currentSides;

	public static Dictionary<TileVal, int> levelKey { get; private set; }

	private static Tile currentTile
	{
		get
		{
			return currentSegment[currentRow, currentCol];
		}
	}

	public static SegmentData CreateSegment(int segmentsTravelled, float segmentOffset, SegmentType? forcedType = null)
	{
		sgementHasFragiles = false;
		segmentHasMobiles = false;
		segmentHasRisers = false;
		segmentHasTallTrees = false;
		segmentHasDangerSpreaders = false;
		if (segmentsTravelled == 0)
		{
			if (worldParent != null)
			{
				worldParent.name += "OLD";
			}
			worldParent = new GameObject("_World").transform;
			SegmentTracker.Reset();
		}
		LevelLoader.ChooseSegment(segmentsTravelled, segmentOffset, forcedType);
		int? num = null;
		int chosenSegmentLength = LevelLoader.ChosenSegmentLength;
		currentSegment = new Tile[chosenSegmentLength, LevelLoader.ChosenSegmentWidth];
		bool flag = LevelLoader.ChosenSegmentCanFlip && Randomizer.FlipACoin();
		for (int i = 0; i < currentSegment.GetLength(0); i++)
		{
			for (int j = 0; j < currentSegment.GetLength(1); j++)
			{
				int num2 = chosenSegmentLength - (i + 1);
				int num3 = ((!flag) ? j : (LevelLoader.ChosenSegmentWidth - (j + 1)));
				int tileTypeVal = LevelLoader.ChosenSegmentTiles[num2, num3];
				currentSegment[i, j] = DecideType(tileTypeVal, segmentOffset, segmentsTravelled, i, j, flag);
			}
		}
		for (int k = 0; k < currentSegment.GetLength(0); k++)
		{
			for (int l = 0; l < currentSegment.GetLength(1); l++)
			{
				if (currentSegment[k, l] != null)
				{
					currentRow = k;
					currentCol = l;
					SetupCurrentTile();
				}
			}
		}
		TryColorMobileSections();
		TryBlockRisers();
		TrySpreadDangers();
		TryPairWavers();
		SegmentTracker.AddSegment(segmentsTravelled, LevelLoader.ChosenType, LevelLoader.ChosenDifficulty, LevelLoader.ChosenSegmentIndex, chosenSegmentLength, segmentOffset, LevelLoader.ChosenSegmentBucketLength, flag);
		return new SegmentData(currentSegment, segmentsTravelled, segmentOffset);
	}

	public static Tile CreateFakeTile(MathUtils.IntTrio oldTileIndex, MathUtils.IntTrio newTileIndex)
	{
		Tile tile = GameManager.TileBoard[oldTileIndex];
		Tile tile2 = DecideType(tile.TypeVal, tile.SegmentOffset, newTileIndex.x, newTileIndex.y, newTileIndex.z, tile.IsFlipped);
		tile2.CreateTile(tile.PrefabID.prefabType, tile.PrefabID.prefabName, tile.MaterialName, 0f);
		return tile2;
	}

	public static Tile CreateFakeGroundTile(MathUtils.IntTrio oldTileIndex, MathUtils.IntTrio newTileIndex)
	{
		Tile tile = GameManager.TileBoard[oldTileIndex];
		Tile tile2 = DecideType(1, tile.SegmentOffset, newTileIndex.x, newTileIndex.y, newTileIndex.z, tile.IsFlipped);
		tile2.CreateTile(PrefabType.Ground, PrefabName.TileX, MaterialName.General, 0f);
		return tile2;
	}

	private static Tile DecideType(int tileTypeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped)
	{
		Tile tile = null;
		try
		{
			if (ExpansionGameplay.Later)
			{
				tile = DecideLaterType(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped);
				if (tile != null) return tile;
			}
			if (ExpansionGameplay.Enabled)
			{
				tile = DecideExpansionType(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped);
				if (tile != null) return tile;
			}
			tile = DecideClassicType(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped);
			if (tile != null)
			{
				return tile;
			}
			switch (tileTypeVal)
			{
			case 0:
				return null;
			case 1:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard);
			case 69:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.Top);
			case 70:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.Bottom);
			case 71:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.Left);
			case 72:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.Right);
			case 81:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopLeft);
			case 83:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopRight);
			case 82:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.BottomLeft);
			case 84:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.BottomRight);
			case 94:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.BottomLeftRight);
			case 93:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopLeftRight);
			case 96:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopBottomRight);
			case 95:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopBottomLeft);
			case 107:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.TopBottom);
			case 108:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard, Tile.Edge.LeftRight);
			case 3:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Fragile0);
			case 4:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Fragile1);
			case 5:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Fragile2);
			case 6:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.FragileChecker);
			case 7:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.FragileRow);
			case 8:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.FragileColumn);
			case 2:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump);
			case 32:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.SuperJump);
			case 49:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Big);
			case 50:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Big, Tile.GroundType.Fragile0);
			case 51:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Big, Tile.GroundType.FragileChecker);
			case 52:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.BigFloater);
			case 53:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.BigFloater, Tile.GroundType.Fragile0);
			case 54:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump, Tile.PickupType.Big);
			case 55:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.BigSlider, Direction.Left);
			case 56:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.BigSlider, Direction.Right);
			case 117:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Star);
			case 118:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Star, Tile.GroundType.Fragile0);
			case 119:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.Star, Tile.GroundType.FragileChecker);
			case 120:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump, Tile.PickupType.Star);
			case 129:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.StarFloater);
			case 130:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.StarFloater, Tile.GroundType.Fragile0);
			case 131:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.PickupType.StarFloater, Tile.GroundType.FragileChecker);
			case 132:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump, Tile.PickupType.StarFloater);
			case 29:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.FloaterGround);
			case 31:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.FloaterAir);
			case 30:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.FloaterGround, Tile.GroundType.Fragile0);
			case 13:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Riser);
			case 14:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Riser, Tile.GroundType.Fragile0);
			case 15:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Riser, Tile.GroundType.FragileChecker);
			case 16:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Riser, Tile.GroundType.FragileRow);
			case 17:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.RiserSlider, Direction.Left);
			case 18:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.RiserSlider, Direction.Right);
			case 9:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Crusher);
			case 10:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.CrusherSafe);
			case 11:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Crusher, Tile.GroundType.Fragile0);
			case 12:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.CrusherSafe, Tile.GroundType.FragileChecker);
			case 25:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Roller, Direction.Left);
			case 26:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Roller, Direction.Right);
			case 27:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Roller, Direction.Left, Tile.GroundType.Fragile0);
			case 28:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Roller, Direction.Right, Tile.GroundType.Fragile0);
			case 65:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Binary, Direction.Left);
			case 66:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Binary, Direction.Right);
			case 67:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Binary, Direction.Left, Tile.GroundType.Fragile0);
			case 68:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Binary, Direction.Right, Tile.GroundType.Fragile0);
			case 61:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMini, Direction.Left);
			case 62:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMini, Direction.Right);
			case 63:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMini, Direction.Left, Tile.GroundType.Fragile0);
			case 64:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMini, Direction.Right, Tile.GroundType.Fragile0);
			case 77:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryOffset, Direction.Left);
			case 78:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryOffset, Direction.Right);
			case 79:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryOffset, Direction.Left, Tile.GroundType.Fragile0);
			case 80:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryOffset, Direction.Right, Tile.GroundType.Fragile0);
			case 73:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMiniOffset, Direction.Left);
			case 74:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMiniOffset, Direction.Right);
			case 75:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMiniOffset, Direction.Left, Tile.GroundType.Fragile0);
			case 76:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.BinaryMiniOffset, Direction.Right, Tile.GroundType.Fragile0);
			case 85:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Pillar, 1f);
			case 86:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Pillar, 2f);
			case 87:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Pillar, 3f);
			case 88:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Pillar, 4f);
			case 89:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PillarSafe, 1f);
			case 90:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PillarSafe, 2f);
			case 91:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PillarSafe, 3f);
			case 92:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PillarSafe, 4f);
			case 105:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PillarLaser, 3f);
			case 21:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.TallTree);
			case 22:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.TallTree, Tile.GroundType.Fragile0);
			case 23:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.TallTreeSlider, Direction.Left);
			case 24:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.TallTreeSlider, Direction.Right);
			case 33:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PalmTree);
			case 34:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PalmTree, Tile.GroundType.Fragile0);
			case 35:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PalmTreeSlider, Direction.Left);
			case 36:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.PalmTreeSlider, Direction.Right);
			case 97:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Spotlight);
			case 98:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Spotlight, Tile.GroundType.Fragile0);
			case 99:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.SpotlightSlider, Direction.Left);
			case 100:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.SpotlightSlider, Direction.Right);
			case 101:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Laser);
			case 102:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Laser, Tile.GroundType.Fragile0);
			case 103:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.LaserSlider, Direction.Left);
			case 104:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.LaserSlider, Direction.Right);
			case 37:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Mover, Direction.Top);
			case 38:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Mover, Direction.Bottom);
			case 39:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Mover, Direction.Left);
			case 40:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Mover, Direction.Right);
			case 41:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Top);
			case 42:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Bottom);
			case 43:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Left);
			case 44:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Right);
			case 45:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Top, Tile.Edge.All);
			case 46:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Bottom, Tile.Edge.All);
			case 47:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Left, Tile.Edge.All);
			case 48:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Right, Tile.Edge.All);
			case 59:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Left, Tile.Edge.TopBottomLeft);
			case 57:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Left, Tile.Edge.TopBottomRight);
			case 58:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Right, Tile.Edge.TopBottomLeft);
			case 60:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.MoverAuto, Direction.Right, Tile.Edge.TopBottomRight);
			case 19:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.Slider, Direction.Left);
			case 20:
				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.Slider, Direction.Right);
			case 106:
			case 109:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
			case 115:
			case 116:
			case 121:
			case 122:
			case 123:
			case 124:
			case 125:
			case 126:
			case 127:
			case 128:
				break;
			}
		}
		catch
		{
		}
		Debug.LogError(string.Format("Error DTY_UFK - unable to find TileVal number '{0}'", tileTypeVal));
		return null;
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, Tile.DangerType.Null, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.GroundType groundType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, groundType, Tile.PickupType.Null, Tile.DangerType.Null, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.PickupType pickupType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, pickupType, Tile.DangerType.Null, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.PickupType pickupType, Direction direction)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, pickupType, Tile.DangerType.Null, Tile.AnimType.Null, direction, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.PickupType pickupType, Tile.GroundType groundType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, groundType, pickupType, Tile.DangerType.Null, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.DangerType dangerType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, dangerType, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.DangerType dangerType, Direction direction)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, dangerType, Tile.AnimType.Null, direction, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.DangerType dangerType, Direction direction, Tile.GroundType groundType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, groundType, Tile.PickupType.Null, dangerType, Tile.AnimType.Null, direction, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.DangerType dangerType, Tile.GroundType groundType)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, groundType, Tile.PickupType.Null, dangerType, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.AnimType animType, Direction direction)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, Tile.DangerType.Null, animType, direction, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Direction direction)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, Tile.DangerType.Null, Tile.AnimType.Null, direction, Tile.Edge.Null, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.GroundType groundType, Tile.Edge edge)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, groundType, Tile.PickupType.Null, Tile.DangerType.Null, Tile.AnimType.Null, Direction.Null, edge, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Direction direction, Tile.Edge edge)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, Tile.DangerType.Null, Tile.AnimType.Null, direction, edge, null);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.DangerType dangerType, float animValue)
	{
		return MakeAsset(typeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.GroundType.Standard, Tile.PickupType.Null, dangerType, Tile.AnimType.Null, Direction.Null, Tile.Edge.Null, animValue);
	}

	private static Tile MakeAsset(int typeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped, Tile.Type type, Tile.GroundType groundType, Tile.PickupType pickupType, Tile.DangerType dangerType, Tile.AnimType animType, Direction direction, Tile.Edge edge, float? animValue)
	{
		Transform geo = BufferManager.GetGeo(PrefabType.Root, PrefabName.Root);
		geo.localEulerAngles = Vector3.zero;
		GameObject gameObject = geo.gameObject;
		Tile component = gameObject.GetComponent<Tile>();
		component.transform.parent = worldParent;
		float posX = 0.5f + (float)tileNum;
		float posY = segmentOffset + (float)rowNum + 0.5f;
		component.SetAttributes(gameObject, segmentOffset, segmentNum, rowNum, tileNum, posX, posY, gameObject.transform.position.z, typeVal, tileFlipped, type, groundType, pickupType, dangerType, animType, direction, edge, animValue);
		return component;
	}

	private static void SetupCurrentTile()
	{
		if (currentTile.IsDangerSpreader)
		{
			segmentHasDangerSpreaders = true;
		}
		if (!segmentHasMobiles && currentTile.IsMobile)
		{
			segmentHasMobiles = true;
		}
		bool flag = false;
		switch (currentTile.type)
		{
		case Tile.Type.Jump:
		case Tile.Type.SuperJump:
		{
			PrefabName prefabName = ((currentTile.type != Tile.Type.SuperJump) ? PrefabName.Normal : PrefabName.Super);
			if (ExpansionGameplay.Enabled)
			{
				if ((TileVal)currentTile.TypeVal == TileVal.MoveJumpLeft) prefabName = PrefabName.MoveJumpLeft;
				if ((TileVal)currentTile.TypeVal == TileVal.MoveJumpRight) prefabName = PrefabName.MoveJumpRight;
			}
			currentTile.CreateTile(PrefabType.Jump, prefabName, MaterialName.General, 0f);
			flag = true;
			break;
		}
		case Tile.Type.Enemy:
			switch (currentTile.dangerType)
			{
			case Tile.DangerType.FloaterAir:
			case Tile.DangerType.EnemyFloaterAirFallGround:
			case Tile.DangerType.RoadUp:
			case Tile.DangerType.EnemyExtendTile1:
			case Tile.DangerType.EnemyExtendTile2:
			case Tile.DangerType.EnemyExtendTile3:
			case Tile.DangerType.EnemyExtendTile4:
			case Tile.DangerType.EnemyExtendFictitious:
			case Tile.DangerType.TurnTile:
			case Tile.DangerType.EnemyLevel23TileDown:
			case Tile.DangerType.EnemyTileWhite:
			case Tile.DangerType.EnemyTileBlack:
			case Tile.DangerType.PickDownLv25Cube:
			case Tile.DangerType.PickDownLv25Cube1:
			case Tile.DangerType.SimulationJumpTitle:
			case Tile.DangerType.EnemyLv25CubeLive:
			case Tile.DangerType.EnemyLv25CubeDie:
			case Tile.DangerType.EnemyLv25MoveDici:
				currentTile.CreateEmptyTile();
				flag = true;
				break;
			case Tile.DangerType.Binary:
			case Tile.DangerType.BinaryOffset:
			case Tile.DangerType.BinaryMini:
			case Tile.DangerType.BinaryMiniOffset:
			case Tile.DangerType.Wyrm:
				segmentHasDangerSpreaders = true;
				break;
			case Tile.DangerType.TallTree:
			case Tile.DangerType.TallTreeSlider:
				segmentHasTallTrees = true;
				break;
			case Tile.DangerType.Riser:
			case Tile.DangerType.RiserSlider:
				segmentHasRisers = true;
				break;
			}
			break;
		}
		if (flag)
		{
			return;
		}
		if (ExpansionGameplay.Enabled) GroupExpansionRotation();
		if (currentTile.IsSlider && currentTile.TileIndex != 0)
		{
			Tile tile = null;
			for (int i = 0; i < currentTile.TileIndex; i++)
			{
				Tile tile2 = currentSegment[currentTile.RowIndex, i];
				if (tile2 != null && tile2.IsSlider)
				{
					if (tile == null)
					{
						tile = tile2;
					}
					break;
				}
			}
			if (tile != null)
			{
				currentTile.ConfigureAnimChild(tile.TileIndex);
				tile.ConfigureAnimParent(currentTile.TileIndex);
			}
		}
		if (currentTile.IsSlider || currentTile.IsRoller)
		{
			int? leftLimit = null;
			int? rightLimit = null;
			int? leftQueue = null;
			int? rightQueue = null;
			for (int j = 0; j < currentSegment.GetLength(1); j++)
			{
				if (j == currentTile.TileIndex)
				{
					continue;
				}
				bool flag2 = false;
				bool flag3 = false;
				Tile tile3 = currentSegment[currentTile.RowIndex, j];
				if (currentTile.IsSlider)
				{
					flag2 = tile3 != null && tile3.IsNotSlider && tile3.IsNotAirbourne;
				}
				else if (tile3 == null)
				{
					flag2 = true;
				}
				else
				{
					tile3.SetAsCannotHaveMiniPickup();
					if (tile3.dangerType == Tile.DangerType.Riser)
					{
						flag2 = true;
					}
					else if (tile3.IsRoller)
					{
						flag3 = true;
					}
				}
				if (flag2)
				{
					if (j >= currentTile.TileIndex)
					{
						rightLimit = j;
						break;
					}
					leftLimit = j;
				}
				else
				{
					if (!flag3)
					{
						continue;
					}
					if (j < currentTile.TileIndex)
					{
						if (leftQueue.HasValue)
						{
							Debug.LogError(string.Format("LNCN: ERROR: Attempt to queue multiple anims for {0}", currentTile));
						}
						else
						{
							leftQueue = j;
						}
					}
					else if (rightQueue.HasValue)
					{
						Debug.LogError(string.Format("LNCN: ERROR: Attempt to queue multiple anims for {0}", currentTile));
					}
					else
					{
						rightQueue = j;
					}
				}
			}
			if (leftLimit.HasValue || rightLimit.HasValue || leftQueue.HasValue || rightQueue.HasValue)
			{
				if (currentTile.IsAnimChild && (leftLimit.HasValue || rightLimit.HasValue))
				{
					currentSegment[currentTile.RowIndex, currentTile.AnimParentTileNum].ConfigureMovementLimits(leftLimit, rightLimit);
				}
				currentTile.ConfigureMovementLimits(leftLimit, rightLimit, leftQueue, rightQueue);
			}
		}
		float prefabRotations;
		PrefabName prefabName2 = ChooseCurrentPrefabName(out prefabRotations);
		if (ExpansionGameplay.Enabled && currentTile.IsNeedRotation) prefabName2 = PrefabName.TileLv10;
		PrefabType prefabType;
		MaterialName materialName;
		if (LevelDesigner.GenerateAltGround && currentTile.IsNotFragile)
		{
			prefabType = PrefabType.Ground;
			prefabName2 = PrefabName.TileOD;
			prefabRotations = Randomizer.GetRandomInt(0, 3);
			materialName = MaterialName.General;
		}
		else if (currentTile.IsFragile)
		{
			prefabType = PrefabType.Ground;
			materialName = MaterialName.Fragile;
		}
		else
		{
			prefabType = PrefabType.Ground;
			materialName = MaterialName.General;
		}
		currentTile.CreateTile(prefabType, prefabName2, materialName, prefabRotations);
	}

	private static PrefabName ChooseCurrentPrefabName(out float prefabRotations)
	{
		PrefabName prefabName = PrefabName.Null;
		prefabRotations = 0f;
		int emptySides;
		int num;
		CalculateCurrentSides(out emptySides, out num);
		switch (emptySides)
		{
		case 0:
			switch (num)
			{
			case 0:
				prefabName = PrefabName.TileX;
				break;
			case 1:
				prefabName = PrefabName.TileX1;
				prefabRotations = RotationsForCorner();
				break;
			case 2:
				if (CornersAreAdjacent())
				{
					prefabName = PrefabName.TileX2a;
					prefabRotations = RotationsForX2a();
				}
				else
				{
					prefabName = PrefabName.TileX2b;
					prefabRotations = RotationsForX2b();
				}
				break;
			case 3:
				prefabName = PrefabName.TileX3;
				prefabRotations = RotationsForCorner(false);
				break;
			case 4:
				prefabName = PrefabName.TileX4;
				break;
			default:
				Debug.LogError(string.Format("LVCN: ERROR: unexpected number of emptyCorners for X-type tile of: {0} ({1} {2})", num, currentTile.RowIndex, currentTile.TileIndex));
				prefabName = PrefabName.TileX;
				break;
			}
			break;
		case 1:
			prefabRotations = RotationsForSide();
			switch (num)
			{
			case 0:
				prefabName = PrefabName.TileT;
				break;
			case 1:
			{
				int num2 = RotationsForCorner();
				prefabName = (((prefabRotations == 0f && num2 == 2) || (prefabRotations == 1f && num2 == 3) || (prefabRotations == 2f && num2 == 0) || (prefabRotations == 3f && num2 == 1)) ? PrefabName.TileT1b : PrefabName.TileT1a);
				break;
			}
			case 2:
				prefabName = PrefabName.TileT2;
				break;
			default:
				Debug.LogError(string.Format("LVCN: ERROR: unexpected number of emptyCorners for T-type tile of: {0} ({1} {2})", num, currentTile.RowIndex, currentTile.TileIndex));
				prefabName = PrefabName.TileT;
				break;
			}
			break;
		case 2:
			if (CurrentSidesAreAdjacent())
			{
				switch (num)
				{
				case 0:
					prefabName = PrefabName.TileL;
					break;
				case 1:
					prefabName = PrefabName.TileL1;
					break;
				default:
					Debug.LogError(string.Format("LVCN: ERROR: unexpected number of emptyCorners for L-type tile of: {0} ({1} {2})", num, currentTile.RowIndex, currentTile.TileIndex));
					prefabName = PrefabName.TileL;
					break;
				}
				prefabRotations = RotationsForL();
			}
			else
			{
				prefabName = PrefabName.TileI;
				prefabRotations = RotationsForI();
			}
			break;
		case 3:
			prefabName = PrefabName.TileD;
			prefabRotations = RotationsForSide(false);
			break;
		case 4:
			prefabName = PrefabName.TileO;
			currentTile.SetAsIsolated();
			break;
		default:
			prefabName = PrefabName.TileO;
			currentTile.SetAsIsolated();
			break;
		}
		if (emptyCorners != null)
		{
			emptyCorners.Clear();
		}
		return prefabName;
	}

	private static void CalculateCurrentSides(out int emptySides, out int emptyCorners)
	{
		if (currentSides == null)
		{
			currentSides = new bool[8];
		}
		emptyCorners = 0;
		emptySides = 0;
		for (int i = 0; i < 8; i++)
		{
			int yOffset = currentRow;
			int xOffset = currentCol;
			Directions.ToOffset(i, ref xOffset, ref yOffset);
			bool flag = false;
			if (yOffset >= 0 && yOffset < currentSegment.GetLength(0) && xOffset >= 0 && xOffset < currentSegment.GetLength(1))
			{
				flag = AreConnected(currentTile, currentSegment[yOffset, xOffset], (Direction)i);
			}
			if (!flag)
			{
				if (Directions.IsCorner(i))
				{
					emptyCorners++;
				}
				else
				{
					emptySides++;
				}
			}
			currentSides[i] = flag;
		}
		if (emptyCorners > 0 && emptySides > 0)
		{
			if (!currentSides[1] && (!currentSides[0] || !currentSides[2]))
			{
				currentSides[1] = true;
				emptyCorners--;
			}
			if (!currentSides[3] && (!currentSides[2] || !currentSides[4]))
			{
				currentSides[3] = true;
				emptyCorners--;
			}
			if (!currentSides[5] && (!currentSides[4] || !currentSides[6]))
			{
				currentSides[5] = true;
				emptyCorners--;
			}
			if (!currentSides[7] && (!currentSides[6] || !currentSides[0]))
			{
				currentSides[7] = true;
				emptyCorners--;
			}
		}
	}

	private static int RotationsForCorner(bool empty = true)
	{
		int result = 0;
		if (currentSides[1] != empty)
		{
			result = 0;
		}
		else if (currentSides[3] != empty)
		{
			result = 1;
		}
		else if (currentSides[5] != empty)
		{
			result = 2;
		}
		else if (currentSides[7] != empty)
		{
			result = 3;
		}
		return result;
	}

	private static int RotationsForSide(bool empty = true)
	{
		int result = 0;
		if (currentSides[0] != empty)
		{
			result = 0;
		}
		else if (currentSides[2] != empty)
		{
			result = 1;
		}
		else if (currentSides[4] != empty)
		{
			result = 2;
		}
		else if (currentSides[6] != empty)
		{
			result = 3;
		}
		return result;
	}

	private static int RotationsForX2a()
	{
		int result = 0;
		if (!currentSides[1] && !currentSides[3])
		{
			result = 0;
		}
		else if (!currentSides[3] && !currentSides[5])
		{
			result = 1;
		}
		else if (!currentSides[5] && !currentSides[7])
		{
			result = 2;
		}
		else if (!currentSides[7] && !currentSides[1])
		{
			result = 3;
		}
		return result;
	}

	private static int RotationsForX2b()
	{
		if (currentSides[3])
		{
			return 0;
		}
		return 1;
	}

	private static int RotationsForL()
	{
		int result = 0;
		if (currentSides[0] && currentSides[2])
		{
			result = 2;
		}
		else if (currentSides[2] && currentSides[4])
		{
			result = 3;
		}
		else if (currentSides[4] && currentSides[6])
		{
			result = 0;
		}
		else if (currentSides[6] && currentSides[0])
		{
			result = 1;
		}
		return result;
	}

	private static int RotationsForI()
	{
		int result = 0;
		if (currentSides[0] || currentSides[4])
		{
			result = 1;
		}
		return result;
	}

	private static bool CornersAreAdjacent()
	{
		return (!currentSides[1] && !currentSides[3]) || (!currentSides[3] && !currentSides[5]) || (!currentSides[5] && !currentSides[7]) || (!currentSides[7] && !currentSides[1]);
	}

	private static bool CurrentSidesAreAdjacent()
	{
		return (!currentSides[0] && !currentSides[2]) || (!currentSides[2] && !currentSides[4]) || (!currentSides[4] && !currentSides[6]) || (!currentSides[6] && !currentSides[0]);
	}

	private static void TryColorMobileSections()
	{
		if (!segmentHasMobiles || LevelDesigner.GenerateAltGround)
		{
			return;
		}
		for (int i = 0; i < currentSegment.GetLength(0); i++)
		{
			for (int j = 0; j < currentSegment.GetLength(1); j++)
			{
				if (!(currentSegment[i, j] != null) || !currentSegment[i, j].IsMobile)
				{
					continue;
				}
				bool isMover = currentSegment[i, j].IsMover;
				if (currentSegment[i, j].IsIsolated)
				{
					MakeMobile(i, j, isMover);
					continue;
				}
				ArrayUtils.List<MathUtils.IntTrio> tileSection = new ArrayUtils.List<MathUtils.IntTrio>();
				ConnectTiles(ref tileSection, currentSegment[i, j], currentSegment);
				for (int k = 0; k < tileSection.Length; k++)
				{
					MakeMobile(tileSection[k].y, tileSection[k].z, isMover);
				}
			}
		}
	}

	private static void MakeMobile(int rowNum, int tileNum, bool isRegularMover)
	{
		MaterialName newMaterialName;
		if (isRegularMover)
		{
			currentSegment[rowNum, tileNum].SetAsMover();
			newMaterialName = MaterialName.Mover;
		}
		else
		{
			currentSegment[rowNum, tileNum].SetAsMoverAuto();
			newMaterialName = MaterialName.MoverAuto;
		}
		currentSegment[rowNum, tileNum].ChangePrefabTypeTo(PrefabType.Ground, newMaterialName);
	}

	private static void TryBlockRisers()
	{
		if (!segmentHasRisers || !LevelDesigner.GeneratePyramids)
		{
			return;
		}
		for (int i = 0; i < currentSegment.GetLength(0); i++)
		{
			for (int j = 0; j < currentSegment.GetLength(1); j++)
			{
				if (!IsRiser(currentSegment[i, j]))
				{
					continue;
				}
				int? num = null;
				bool flag;
				if (i + 1 < currentSegment.GetLength(0) && j + 1 < currentSegment.GetLength(1))
				{
					flag = true;
					for (int k = i; k <= i + 1; k++)
					{
						int num2 = j;
						while (num2 <= j + 1)
						{
							if ((k == i && num2 == j) || IsRiser(currentSegment[k, num2]))
							{
								num2++;
								continue;
							}
							goto IL_00a3;
						}
						continue;
						IL_00a3:
						flag = false;
						break;
					}
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					num = 2;
				}
				if (!num.HasValue)
				{
					continue;
				}
				for (int l = i; l < i + num.Value; l++)
				{
					for (int m = j; m < j + num.Value; m++)
					{
						if (l == i && m == j)
						{
							currentSegment[l, m].MakeSuperRiser(num.Value);
						}
						else
						{
							currentSegment[l, m].HideRiser();
						}
					}
				}
			}
		}
	}

	private static bool IsRiser(Tile queryTile)
	{
		return queryTile != null && queryTile.dangerType == Tile.DangerType.Riser;
	}

	private static void TrySpreadDangers()
	{
		if (!segmentHasDangerSpreaders)
		{
			return;
		}
		for (int i = 0; i < currentSegment.GetLength(0); i++)
		{
			for (int j = 0; j < currentSegment.GetLength(1); j++)
			{
				Tile tile = currentSegment[i, j];
				if (!(tile != null) || !tile.IsDangerSpreader)
				{
					continue;
				}
				if (tile.dangerSpreadType != Tile.DangerSpreadType.Null)
				{
					if (!SpreadLaterDanger(tile, i, j)) SpreadClassicDanger(tile, i, j);
					continue;
				}
				switch (tile.dangerType)
				{
				case Tile.DangerType.Binary:
				case Tile.DangerType.BinaryOffset:
				case Tile.DangerType.BinaryMini:
				case Tile.DangerType.BinaryMiniOffset:
				case Tile.DangerType.Wyrm:
				{
					int? num = null;
					switch (tile.dangerType)
					{
					case Tile.DangerType.Wyrm:
						num = 3;
						break;
					case Tile.DangerType.Binary:
						num = 2;
						break;
					case Tile.DangerType.BinaryOffset:
					case Tile.DangerType.BinaryMini:
						num = 1;
						break;
					default:
						Debug.LogError(string.Format("LVCN: ERROR: Attempt to spread danger from invalid dangerType tile of {0} ({1))\n  Double click this message check LevenConstructor.SpreadDangers case statement.", tile.dangerType, tile));
						break;
					case Tile.DangerType.BinaryMiniOffset:
						break;
					}
					if (!num.HasValue)
					{
						break;
					}
					int num2;
					int num3;
					switch (tile.direction)
					{
					case Direction.Left:
						num2 = MathUtils.MaxInt(j - num.Value, 0);
						num3 = j - 1;
						break;
					case Direction.Right:
						num2 = j + 1;
						num3 = MathUtils.MinInt(j + num.Value, currentSegment.GetLength(1) - 1);
						break;
					default:
						Debug.LogError(string.Format("LVCN: ERROR: Attempt to spread danger from valid dangerType tile of {0} ({1)), but in unexpected direction of {2}.\nDouble click this message check LevenConstructor.SpreadDangers case statement.", tile.dangerType, tile, tile.direction));
						num2 = -1;
						num3 = -1;
						break;
					}
					for (int k = num2; k <= num3; k++)
					{
						if (currentSegment[i, k] != null && currentSegment[i, k].IsNotDangerous)
						{
							currentSegment[i, k].SetAsDangerous(null, 0.125f);
						}
					}
					break;
				}
				default:
					Debug.LogError(string.Format("LVCN: ERROR: Attempt to spread danger from unexpected dangerType tile of {0}, {1}.\nDouble click this message check LevenConstructor.SpreadDangers case statement.", tile.dangerType, tile));
					break;
				}
			}
		}
	}

	private static void TryPairWavers()
	{
	}

	public static ArrayUtils.List<MathUtils.IntTrio> GetTileSection(MathUtils.IntTrio tileIndex)
	{
		ArrayUtils.List<MathUtils.IntTrio> tileSection = new ArrayUtils.List<MathUtils.IntTrio>();
		ConnectTiles(ref tileSection, tileIndex);
		return tileSection;
	}

	private static void ConnectTiles(ref ArrayUtils.List<MathUtils.IntTrio> tileSection, int segmentNum, int rowNum, int tileNum)
	{
		ConnectTiles(ref tileSection, new MathUtils.IntTrio(segmentNum, rowNum, tileNum));
	}

	private static void ConnectTiles(ref ArrayUtils.List<MathUtils.IntTrio> tileSection, MathUtils.IntTrio currentTileIndex)
	{
		tileSection.Add(currentTileIndex);
		for (int i = 0; i < 4; i++)
		{
			Direction direction = Directions.MainDirections[i];
			int num = currentTileIndex.y;
			int num2 = currentTileIndex.z;
			switch (direction)
			{
			case Direction.Top:
				num++;
				break;
			case Direction.Bottom:
				num--;
				break;
			case Direction.Left:
				num2--;
				break;
			case Direction.Right:
				num2++;
				break;
			default:
				Debug.LogError(string.Format("LVCN: ERROR: Attempt to convert unhandled Direction case of {0} into adjacent tile index.  Check case statement in ConnectTiles()", direction));
				break;
			}
			if (!GameManager.TileBoard.IsNotNull(currentTileIndex.x, num, num2))
			{
				continue;
			}
			MathUtils.IntTrio intTrio = new MathUtils.IntTrio(currentTileIndex.x, num, num2);
			if (tileSection.ContainsNot(intTrio))
			{
				Tile queryTile = GameManager.TileBoard[currentTileIndex];
				Tile adjacentTile = GameManager.TileBoard[intTrio];
				if (AreConnected(queryTile, adjacentTile, direction))
				{
					ConnectTiles(ref tileSection, intTrio);
				}
			}
		}
	}

	private static void ConnectTiles(ref ArrayUtils.List<MathUtils.IntTrio> tileSection, Tile currentTile, Tile[,] tileSegment)
	{
		tileSection.Add(currentTile.Index);
		for (int i = 0; i < 4; i++)
		{
			Direction direction = Directions.MainDirections[i];
			int num = currentTile.Index.y;
			int num2 = currentTile.Index.z;
			switch (direction)
			{
			case Direction.Top:
				num++;
				break;
			case Direction.Bottom:
				num--;
				break;
			case Direction.Left:
				num2--;
				break;
			case Direction.Right:
				num2++;
				break;
			default:
				Debug.LogError(string.Format("LVCN: ERROR: Attempt to convert unhandled Direction case of {0} into adjacent tile index.  Check case statement in ConnectTiles()", direction));
				break;
			}
			if (num < 0 || num >= tileSegment.GetLength(0) || num2 < 0 || num2 >= tileSegment.GetLength(1))
			{
				continue;
			}
			Tile tile = tileSegment[num, num2];
			if (tile != null)
			{
				MathUtils.IntTrio index = tile.Index;
				if (tileSection.ContainsNot(index) && AreConnected(currentTile, tile, direction))
				{
					ConnectTiles(ref tileSection, tile, tileSegment);
				}
			}
		}
	}

	public static List<MathUtils.IntTrio> GetSectionAdjacents(Tile[] tileSection)
	{
		Tile[,] array = GameManager.TileBoard.CurrentSegment;
		List<MathUtils.IntTrio> list = new List<MathUtils.IntTrio>();
		Direction[] array2 = new Direction[4]
		{
			Direction.Top,
			Direction.Right,
			Direction.Bottom,
			Direction.Left
		};
		foreach (Direction direction in array2)
		{
			foreach (Tile tile in tileSection)
			{
				MathUtils.IntTrio index = new MathUtils.IntTrio(tile.SegmentIndex, tile.RowIndex, tile.TileIndex);
				MathUtils.IntTrio intTrio = AdjacentIndex(index, direction);
				if (GameManager.TileBoard.IsNotNull(intTrio) && !list.Contains(intTrio))
				{
					list.Add(intTrio);
				}
			}
		}
		return list;
	}

	private static List<Tile> AddToist(Tile entry, List<Tile> runningList)
	{
		if (runningList == null)
		{
			runningList = new List<Tile>();
		}
		runningList.Add(entry);
		return runningList;
	}

	private static bool IsValidIndex(intPair index, Tile[,] tileBoard)
	{
		return IsValidIndex(index.i, index.j, tileBoard);
	}

	private static bool IsValidIndex(int rowNum, int tileNum, Tile[,] tileBoard)
	{
		return rowNum >= 0 && rowNum < tileBoard.GetLength(0) && tileNum >= 0 && tileNum < tileBoard.GetLength(1);
	}

	private static Tile AdjacentTile(Direction side, Tile queryTile, Tile[,] tileBoard)
	{
		if (queryTile == null)
		{
			return null;
		}
		intPair index = new intPair(queryTile.RowIndex, queryTile.TileIndex);
		index = AdjacentIndex(index, side);
		if (!IsValidIndex(index, tileBoard))
		{
			return null;
		}
		return tileBoard[index.i, index.j];
	}

	private static Tile.GroundType AdjacentFragileType(Tile queryTile, Tile[,] tileBoard)
	{
		Direction[] array = new Direction[2]
		{
			Direction.Left,
			Direction.Right
		};
		foreach (Direction side in array)
		{
			Tile.GroundType groundType = AdjacentFragiles(queryTile, tileBoard, side);
			if (groundType != Tile.GroundType.Null)
			{
				return groundType;
			}
		}
		Direction[] array2 = new Direction[2]
		{
			Direction.Top,
			Direction.Bottom
		};
		foreach (Direction side2 in array2)
		{
			Tile.GroundType groundType = AdjacentFragiles(queryTile, tileBoard, side2);
			if (groundType != Tile.GroundType.Null)
			{
				return groundType;
			}
		}
		return Tile.GroundType.Fragile0;
	}

	private static Tile.GroundType AdjacentFragiles(Tile queryTile, Tile[,] tileBoard, Direction side)
	{
		Tile tile = AdjacentTile(side, queryTile, tileBoard);
		if (tile != null && tile.IsFragile && tile.groundType != Tile.GroundType.Fragile2)
		{
			return tile.groundType;
		}
		return Tile.GroundType.Null;
	}

	private static MathUtils.IntTrio AdjacentIndex(MathUtils.IntTrio index, Direction direction)
	{
		switch (direction)
		{
		case Direction.Top:
			index.y++;
			break;
		case Direction.TopRight:
			index.z++;
			index.y++;
			break;
		case Direction.Right:
			index.z++;
			break;
		case Direction.BottomRight:
			index.z++;
			index.y--;
			break;
		case Direction.Bottom:
			index.y--;
			break;
		case Direction.BottomLeft:
			index.z--;
			index.y--;
			break;
		case Direction.Left:
			index.z--;
			break;
		case Direction.TopLeft:
			index.z--;
			index.y++;
			break;
		}
		return index;
	}

	private static intPair AdjacentIndex(intPair index, Direction direction)
	{
		switch (direction)
		{
		case Direction.Top:
			index.i++;
			break;
		case Direction.TopRight:
			index.j++;
			index.i++;
			break;
		case Direction.Right:
			index.j++;
			break;
		case Direction.BottomRight:
			index.j++;
			index.i--;
			break;
		case Direction.Bottom:
			index.i--;
			break;
		case Direction.BottomLeft:
			index.j--;
			index.i--;
			break;
		case Direction.Left:
			index.j--;
			break;
		case Direction.TopLeft:
			index.j--;
			index.i++;
			break;
		}
		return index;
	}

	private static bool SideIsNotConnected(Direction side, Tile queryTile, Tile[,] tileBoard)
	{
		return !SideIsConnected(side, queryTile, tileBoard);
	}

	private static bool SideIsConnected(Direction side, Tile queryTile, Tile[,] tileBoard)
	{
		Tile adjacentTile = AdjacentTile(side, queryTile, tileBoard);
		return AreConnected(queryTile, adjacentTile, side);
	}

	private static bool AreConnected(Tile queryTile, Tile adjacentTile, Direction side)
	{
		bool result = true;
		if (queryTile == null || adjacentTile == null)
		{
			result = false;
		}
		else if (queryTile.IsIsolated || adjacentTile.IsIsolated)
		{
			result = false;
		}
		else if (queryTile.IsGeoHidden || adjacentTile.IsGeoHidden)
		{
			result = false;
		}
		else if (queryTile.IsAirbourne || adjacentTile.IsAirbourne)
		{
			result = false;
		}
		else if (queryTile.IsJump || adjacentTile.IsJump)
		{
			result = false;
		}
		else if (queryTile.IsMoved || adjacentTile.IsMoved)
		{
			result = false;
		}
		else if (queryTile.dangerType == Tile.DangerType.Switcher || adjacentTile.dangerType == Tile.DangerType.Switcher)
		{
			result = false;
		}
		else if (queryTile.IsFragile || adjacentTile.IsFragile)
		{
			result = queryTile.IsFragile && adjacentTile.IsFragile && queryTile.groundType != Tile.GroundType.FragileChecker && adjacentTile.groundType != Tile.GroundType.FragileChecker && ((queryTile.groundType != Tile.GroundType.FragileRow && adjacentTile.groundType != Tile.GroundType.FragileRow) ? ((queryTile.groundType != Tile.GroundType.FragileColumn && adjacentTile.groundType != Tile.GroundType.FragileColumn) ? (queryTile.groundType == adjacentTile.groundType) : (queryTile.TileIndex == adjacentTile.TileIndex)) : (queryTile.RowIndex == adjacentTile.RowIndex));
		}
		else if (queryTile.IsSlider)
		{
			if (!adjacentTile.IsSlider || queryTile.RowIndex != adjacentTile.RowIndex)
			{
				result = false;
			}
		}
		else if (adjacentTile.IsSlider)
		{
			result = false;
		}
		else if (queryTile.HasEdge || adjacentTile.HasEdge)
		{
			switch (side)
			{
			case Direction.Top:
				result = !queryTile.HasTopEdge && !adjacentTile.HasBottomEdge;
				break;
			case Direction.Bottom:
				result = !queryTile.HasBottomEdge && !adjacentTile.HasTopEdge;
				break;
			case Direction.Left:
				result = !queryTile.HasLeftEdge && !adjacentTile.HasRightEdge;
				break;
			case Direction.Right:
				result = !queryTile.HasRightEdge && !adjacentTile.HasLeftEdge;
				break;
			default:
				result = true;
				break;
			}
		}
		return result;
	}
}
