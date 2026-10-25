using UnityEngine;

public partial class Tile : MonoBehaviour
{
	public enum Type
	{
		Ground = 0,
		Air = 1,
		Jump = 2,
		SuperJump = 3,
		Enemy = 4,
		Mover = 5,
		MoverAuto = 6,
		LaserTrigger = 7,
		Rotation = 8,
		Bean = 9,
		BackwardQuery = 10
	}

	public enum GroundType
	{
		Standard = 0,
		Jump = 1,
		Finish = 2,
		Fragile0 = 3,
		Fragile1 = 4,
		Fragile2 = 5,
		FragileChecker = 6,
		FragileRow = 7,
		FragileColumn = 8,
		Speed = 9,
		Null = 10
	}

	public enum PickupType
	{
		Big = 0,
		BigFloater = 1,
		BigSlider = 2,
		Star = 3,
		StarFloater = 4,
		StarSlider = 5,
		Null = 6
	}

	public enum DangerType
	{
		Danger = 0,
		Heat = 1,
		Riser = 2,
		RiserSlider = 3,
		FloaterGround = 4,
		FloaterAir = 5,
		Crusher = 6,
		CrusherSafe = 7,
		Binary = 8,
		BinaryOffset = 9,
		BinaryMini = 10,
		BinaryMiniOffset = 11,
		Roller = 12,
		Wyrm = 13,
		TallTree = 14,
		TallTreeSlider = 15,
		PalmTree = 16,
		PalmTreeSlider = 17,
		Pillar = 18,
		PillarSlider = 19,
		PillarSafe = 20,
		PillarSafeSlider = 21,
		PillarLaser = 22,
		Spotlight = 23,
		SpotlightSlider = 24,
		Laser = 25,
		LaserSlider = 26,
		Turbine = 27,
		TurbineSlider = 28,
		Null = 29,
		RadioTower = 30,
		ShintoSafe = 31,
		ShintoUnsafe = 32,
		Turret = 33,
		TurretOffset = 34,
		Switcher = 35,
		TetrisDownOneLine01 = 36,
		TetrisDownOneLine02 = 37,
		TetrisDownOneLine03 = 38,
		TetrisDownOneLine04 = 39,
		TetrisDownOneLine05 = 40,
		TetrisDownOneLine06 = 41,
		TetrisDownOneLine07 = 42,
		TetrisDownOneLine08 = 43,
		OneBlockInToOffD = 44,
		OneBlockInToOff = 45,
		OneBlockOffToInD = 46,
		EnemyRedLaserH,
		EnemyRedLaserV,
		EnemyGreenLaserH,
		EnemyGreenLaserV,
		EnemyFloaterAirFallGround,
		EnemyRotateFourBlock,
		EnemyLv9TakeBack,
		EnemeyLaserBlock,
		EnemyRetroSnaker01,
		EnemyRetroSnaker02,
		EnemySnakerWallRight,
		EnemySnakerWallLeft,
		EnemySnakerFrontMove,
		EnemySnakerCoiled01,
		EnemySnakerLeftMove,
		EnemySnakerRightMove,
		EnemyVerticalCannon,
		RoadUp,
		EnemyJumpFromR1,
		EnemyJumpFromL1,
		EnemyJumpFromL2,
		EnemyJumpFromR2,
		EnemyLeaningGeneralCube3,
		EnemyLeaningRiser,
		EnemyMiracle,
		EnemyLeaningTower,
		EnemyCollapseTower,
		EnemyTower,
		EnemyFallHammerL,
		EnemyFallHammerR,
		EnemyTombstone,
		EnemyGhost,
		EnemyGhostL,
		EnemyGhostR,
		EnemyWitchSoup,
		EnemySkeletonHand,
		EnemySkeletonL,
		EnemySkeletonR,
		EnemySkeleton1,
		EnemySkeleton2,
		EnemyGhostFormation1,
		EnemyGhostFormation2,
		EnemyBatSwarm,
		EnemyPumpkinLantern,
		EnemyDoorOpen,
		EnemyDoorClose,
		EnemyUFO,
		EnemyChrisCard_Tree,
		EnemyChrisCard_Snowman,
		EnemyChrisCard_Spirit,
		EnemyChrisCard_GingerbreadMan,
		EnemyChrisCard_Reindeer,
		EnemyChrisCard_Environmental,
		EnemyChrisCard_SantaClaus,
		ChristmasTreeTo3D,
		SnowManTo3D,
		ChristmasManTo3D,
		CakeManTo3D,
		ChristmasManRoller,
		JingleDoorSafe,
		ChristmasMan3D,
		SnowMan3D,
		CakeManToLeft,
		CakeManToRight,
		GiftBallnoon,
		Fireworks,
		RotateChristmasTree,
		EnemyPanda,
		EnemyPendulumL,
		EnemyPendulumR,
		EnemyStakesL,
		EnemyStakesR,
		EnemyExtendTile1,
		EnemyExtendTile2,
		EnemyExtendTile3,
		EnemyExtendTile4,
		EnemyExtendFictitious,
		EnemyDestroy1Left,
		EnemyDestroy1Right,
		EnemyDestroy2Left,
		EnemyDestroy2Right,
		EnemyBee,
		SuperJumper,
		EnemyGamePad,
		EnemyPlane,
		EnemyDestroy3,
		EnemyCube1, EnemyCube3, EnemyCube3Pillar, EnemyCube4, EnemyCube5, EnemyCube6, EnemyCube9,
		EnemyHalfround, TurnTile, EnemyLv20RollerLeft, EnemyLv20Rollerright,
		EnemyLevel23TileDown, EnemyPoker1, EnemyPoker2Left, EnemyPoker2Right, EnemyPoker3Left, EnemyPoker3Right,
		EnemySuccessiveJump, EnemyMagicDove, EnemyPoker1Lean, EnemyPokerTree, EnemyPokerTreeSlider,
		EnemyTileWhite, EnemyTileBlack, EnemyUmbrellaLeft, EnemyUmbrellaRight, EnemyPokerDoor, EnemyBigJoker, EnemySmallJoker,
		EnemySoldierLeft, EnemySoldierRight, PickDownLv25Cube, PickDownLv25Cube1, EnemyLv25Brigde, SimulationJumpTitle,
		EnemyLv25Sound, EnemyLv25SoundLeft, EnemyLv25SoundRight, EnemyLv25AirEye, EnemyLv25Sound3, EnemyLv25CocountTree,
		EnemyLv25XiaoBianLeft, EnemyLv25XiaoBianRight, EnemyLv25Tape, EnemyLv25CubeLive, EnemyLv25CubeDie,
		EnemyLv25MoveDici, EnemyLv25DownLightning, EnemyLv25Gyroscope, SuperJumperFictitious, EnemyLv25Spring,
		BackwardQueryLv26shunyiStart, BackwardQueryLv26shunyiEnd,
		EnemyLv26Door, EnemyLv26Pillars1, EnemyLv26Pillars2, EnemyLv26God1, EnemyLv26God2,
		EnemyLv26FirePlace, EnemyLv26Castle, EnemyLv26Dici, EnemyLv26DaDiShuLife, EnemyLv26DaDiShuDie, EnemyLv26Owl, EnemyLv26Tree,
		EnemyLv34Snow, EnemyLv28Wall, EnemyLv28Wall2, BeanLv28BouncePlate,
		BeanLv28BasketballStandsLeft, BeanLv28BasketballStandsRight, BeanLv28BasketballStandsCenter,
		EnemyLv28BlackPlayer, EnemyLv28PlayerLeft, EnemyLv28PlayerRight, EnemyLv28HatPlayer, EnemyLv28Tyre,
		EnemyLv28Doors, EnemyLv28Doors2, EnemyLv28NetDie, EnemySimulationDie, BeanLv28Button, BeanJump10Tile, BeanBack6Tile,
		EnemyLv30MoveAfterTile, EnemyLv30MoveBeforeTile, BeanLv32StartReverse, BeanLv32EndReverse,
		EnemyLv32HorizontalLever, EnemyLv32VerticalLever, EnemyLv32Door, EnemyLv32AirWheel, EnemyLv32LightLeft, EnemyLv32LightRight, EnemyLv32Cannon,
		EnemyLv34BigCane, EnemyLv34Tree1,
		EnemyLv37Forward1Tile, EnemyLv37Forward2Tile, EnemyLv37Forward3Tile, EnemyLv37Forward4Tile, EnemyLv37Forward5Tile,
		EnemyLv37Frisbee, EnemyLv37Pan, EnemyLv37Sound, EnemyLv37RobotLeft, EnemyLv37RobotRight, EnemyLv37Door,
		EnemyLv37KeyboardLeft, EnemyLv37KeyboardRight, EnemyLv37Gear,
		EnemyLv40PyramidBack, EnemyLv40PyramidLeft, EnemyLv40PyramidRight, EnemyLv40Bug, EnemyLv40Door,
		EnemyLv40Column1, EnemyLv40Column2, EnemyLv40Eye, EnemyLv40BackTriangle, EnemyLv40Triangle, EnemyLv40Snake,
		EnemyLv40LeftWall, EnemyLv40RightWall, EnemyLv40_5TileLeft, EnemyLv40_5TileRight,
		EnemyLv40_4TileLeft, EnemyLv40_4TileRight, EnemyLv40_3TileLeft, EnemyLv40_3TileRight, EnemyLv40_2TileLeft, EnemyLv40_2TileRight,
		BeanLv40ShowButton, BeanLv40HideButton, EnemyLv40_CrossLeft, EnemyLv40_CrossRight,
		EnemyLv30Frankenstein, EnemyLv30Corpse, EnemyLv30WitchRight, EnemyLv30SpiderDie,
		EnemyLv30SpiderLife, EnemyLv30Pumpkin, EnemyLv30Tombstone, EnemyLv30Vampire,
		EnemyLv30Pumpkin2, EnemyLv30Door, EnemyLv30Vampire2, EnemyLv30WitchLeft, BeanLv30Cobweb,
		EnemyLv42ThreeGlass, EnemyLv42Column, EnemyLv42Rise, EnemyLv42FieryRings,
		EnemyLv42PendulumLeft, EnemyLv42PendulumRight, EnemyLv42MonocycleRight, EnemyLv42MonocycleLeft,
		EnemyLv42ClownUp, BeanLv42ClownDown, EnemyLv42TrojanLeft, EnemyLv42TrojanRight,
		EnemyLv42Door, SimulationAirKillBall, EnemyLv42Circle,
		Enemy_807_Level69_mofang, Enemy_808_Level69_yanjing, Enemy_809_Level69_banyuanL, Enemy_810_Level69_banyuanR
	}

	public enum AnimType
	{
		Jump = 0,
		Riser = 1,
		RiserSlider = 2,
		Crusher = 3,
		CrusherFaller = 4,
		BinaryHammer = 5,
		BinarySlasher = 6,
		BinarySlasherMini = 7,
		Roller = 8,
		Wyrm = 9,
		Floater = 10,
		Tower = 11,
		TowerSlider = 12,
		Tree = 13,
		TreeSlider = 14,
		PalmTree = 15,
		PalmTreeSlider = 16,
		Pyramid = 17,
		PyramidSlider = 18,
		Pillar = 19,
		PillarSlider = 20,
		Laser = 21,
		LaserSlider = 22,
		Spotlight = 23,
		SpotlightSlider = 24,
		Pickup = 25,
		PickupFloater = 26,
		PickupSlider = 27,
		PickupCollect = 28,
		Star = 29,
		StarFloater = 30,
		StarSlider = 31,
		StarCollect = 32,
		Slider = 33,
		Speeder = 34,
		Save = 35,
		Mover = 36,
		Faller = 37,
		Turbine = 38,
		TurbineSlider = 39,
		Null = 40,
		Building = 41,
		BuildingSlider = 42,
		Tesla = 43,
		TeslaSlider = 44,
		FloodLaser = 45,
		FloodLaserSlider = 46,
		RadioTower = 47,
		Turret = 48,
		Switcher = 49,
		TetrisDownOneLine01 = 50,
		TetrisDownOneLine02 = 51,
		TetrisDownOneLine03 = 52,
		TetrisDownOneLine04 = 53,
		TetrisDownOneLine05 = 54,
		TetrisDownOneLine06 = 55,
		TetrisDownOneLine07 = 56,
		TetrisDownOneLine08 = 57,
		OneBlockInToOffD = 58,
		OneBlockInToOff = 59,
		OneBlockOffToInD = 60,
		ShintoRise = 61,
		ShintoFall = 62,
		MoveJumpLeft,
		MoveJumpRight,
		EnemyLaser,
		EnemyRedLaserH,
		EnemyGreenLaserH,
		EnemyFloaterAirFallGround,
		EnemyRotateFourBlock,
		EnemyLv9TakeBack,
		EnemeyLaserBlock,
		EnemyRetroSnaker01,
		EnemyRetroSnaker02,
		EnemySnakerWallRight,
		EnemySnakerWallLeft,
		EnemySnakerFrontMove,
		EnemySnakerCoiled01,
		EnemySnakerLeftMove,
		EnemySnakerRightMove,
		EnemyVerticalCannon,
		RoadUp,
		Rotation,
		EnemyJumpFromL1,
		EnemyJumpFromR1,
		EnemyJumpFromL2,
		EnemyJumpFromR2,
		LeaningGeneralCube3,
		EnemyLeaningRiser,
		EnemyMiracle,
		EnemyLeaningTower,
		EnemyCollapseTower,
		EnemyTower,
		EnemyFallHammerL,
		EnemyFallHammerR,
		EnemyTombstone,
		EnemyGhost,
		EnemyGhostL,
		EnemyGhostR,
		EnemyWitchSoup,
		EnemySkeletonHand,
		EnemySkeletonL,
		EnemySkeletonR,
		EnemySkeleton1,
		EnemySkeleton2,
		EnemyGhostFormation1,
		EnemyGhostFormation2,
		EnemyBatSwarm,
		EnemyPumpkinLantern,
		EnemyDoorOpen,
		EnemyDoorClose,
		EnemyUFO,
		EnemyChrisCard_Tree,
		EnemyChrisCard_Snowman,
		EnemyChrisCard_Spirit,
		EnemyChrisCard_GingerbreadMan,
		EnemyChrisCard_Reindeer,
		EnemyChrisCard_Environmental,
		EnemyChrisCard_SantaClaus,
		ChristmasTreeTo3D,
		SnowManTo3D,
		ChristmasManTo3D,
		CakeManTo3D,
		ChristmasManRoller,
		JingleDoorRing,
		ChristmasMan3D,
		SnowMan3D,
		CakeManToLeft,
		CakeManToRight,
		GiftBallnoon,
		Fireworks,
		RotateChristmasTree,
		EnemyPanda,
		EnemyPendulumL,
		EnemyPendulumR,
		EnemyStakesL,
		EnemyStakesR,
		EnemyExtendTile,
		EnemyDestroyUp,
		EnemyDestroyMove,
		EnemyBee,
		EnemyGamePad,
		EnemyPlane,
		BigBean,
		EnemyCube1, EnemyCube3, EnemyCube3Pillar, EnemyCube4, EnemyCube5, EnemyCube6, EnemyCube9,
		EnemyHalfround, TurnTileCenter, TurnTileLeft1, TurnTileLeft2, TurnTileRight1, TurnTileRight2, EnemyLv20Roller,
		EnemyLevel23TileDown, EnemyPoker1, EnemyPoker2Left, EnemyPoker2Right, EnemyPoker3Left, EnemyPoker3Right,
		EnemyMagicDove, EnemyPoker1Lean, EnemyPokerTree, EnemyPokerTreeSlider,
		EnemyTileWhite, EnemyTileBlack, EnemyUmbrellaLeft, EnemyUmbrellaRight, EnemyPokerDoor, EnemyBigJoker, EnemySmallJoker,
		EnemySoldierLeft, EnemySoldierRight, PickDownLv25Cube, PickDownLv25Cube1, EnemyLv25Brigde, BeanLv25BrigdeUp, BeanLv25BrigdeDown,
		EnemyLv25Sound, EnemyLv25SoundLeft, EnemyLv25SoundRight, EnemyLv25AirEye, EnemyLv25Sound3, EnemyLv25CocountTree,
		EnemyLv25XiaoBianLeft, EnemyLv25XiaoBianRight, EnemyLv25Tape, EnemyLv25CubeLive, EnemyLv25CubeDie,
		EnemyLv25MoveDici, EnemyLv25DownLightning, EnemyLv25Gyroscope, EnemyLv25Spring,
		BackwardQueryLv26shunyiStart, BackwardQueryLv26shunyiEnd,
		EnemyLv26Door, EnemyLv26Pillars1, EnemyLv26Pillars2, EnemyLv26God1, EnemyLv26God2,
		EnemyLv26FirePlace, EnemyLv26Castle, EnemyLv26Dici, EnemyLv26DaDiShuLife, EnemyLv26DaDiShuDie, EnemyLv26Owl, EnemyLv26Tree,
		EnemyLv34Snow, EnemyLv28Wall, EnemyLv28Wall2, BeanLv28BouncePlate,
		BeanLv28BasketballStandsLeft, BeanLv28BasketballStandsRight, BeanLv28BasketballStandsCenter,
		EnemyLv28BlackPlayer, EnemyLv28PlayerLeft, EnemyLv28PlayerRight, EnemyLv28HatPlayer, EnemyLv28Tyre,
		EnemyLv28Doors, EnemyLv28Doors2, EnemyLv28NetDie, BeanLv28Button, BeanJump10Tile, BeanBack6Tile,
		EnemyLv30MoveAfterTile, EnemyLv30MoveBeforeTile, BeanLv32StartReverse, BeanLv32EndReverse,
		EnemyLv32HorizontalLever, EnemyLv32VerticalLever, EnemyLv32Door, EnemyLv32AirWheel, EnemyLv32LightLeft, EnemyLv32LightRight, EnemyLv32Cannon,
		EnemyLv34BigCane, EnemyLv34Tree1,
		EnemyLv37Forward1Tile, EnemyLv37Forward2Tile, EnemyLv37Forward3Tile, EnemyLv37Forward4Tile, EnemyLv37Forward5Tile,
		EnemyLv37Frisbee, EnemyLv37Pan, EnemyLv37Sound, EnemyLv37RobotLeft, EnemyLv37RobotRight, EnemyLv37Door,
		EnemyLv37KeyboardLeft, EnemyLv37KeyboardRight, EnemyLv37Gear,
		EnemyLv40PyramidBack, EnemyLv40PyramidLeft, EnemyLv40PyramidRight, EnemyLv40Bug, EnemyLv40Door,
		EnemyLv40Column1, EnemyLv40Column2, EnemyLv40Eye, EnemyLv40BackTriangle, EnemyLv40Triangle, EnemyLv40Snake,
		EnemyLv40LeftWall, EnemyLv40RightWall, EnemyLv40_5TileLeft, EnemyLv40_5TileRight,
		EnemyLv40_4TileLeft, EnemyLv40_4TileRight, EnemyLv40_3TileLeft, EnemyLv40_3TileRight, EnemyLv40_2TileLeft, EnemyLv40_2TileRight,
		BeanLv40ShowButton, BeanLv40HideButton, EnemyLv40_CrossLeft, EnemyLv40_CrossRight,
		GroundTileLeft, GroundTileRight, GroundTileBefore, GroundTileAfter,
		EnemyLv30Frankenstein, EnemyLv30Corpse, EnemyLv30WitchRight, EnemyLv30SpiderDie,
		EnemyLv30SpiderLife, EnemyLv30Pumpkin, EnemyLv30Tombstone, EnemyLv30Vampire,
		EnemyLv30Pumpkin2, EnemyLv30Door, EnemyLv30Vampire2, EnemyLv30WitchLeft, BeanLv30Cobweb,
		EnemyLv42ThreeGlass, EnemyLv42Column, EnemyLv42Rise, EnemyLv42FieryRings,
		EnemyLv42PendulumLeft, EnemyLv42PendulumRight, EnemyLv42MonocycleRight, EnemyLv42MonocycleLeft,
		EnemyLv42ClownUp, BeanLv42ClownDown, EnemyLv42TrojanLeft, EnemyLv42TrojanRight,
		EnemyLv42Door, EnemyLv42Circle,
		Enemy_807_Level69_mofang, Enemy_808_Level69_yanjing, Enemy_809_Level69_banyuanL, Enemy_810_Level69_banyuanR
	}

	public enum Edge
	{
		Top = 0,
		Bottom = 1,
		Left = 2,
		Right = 3,
		TopLeft = 4,
		TopRight = 5,
		BottomLeft = 6,
		BottomRight = 7,
		BottomLeftRight = 8,
		TopLeftRight = 9,
		TopBottomRight = 10,
		TopBottomLeft = 11,
		TopBottom = 12,
		LeftRight = 13,
		All = 14,
		Null = 15
	}

	private struct IndexedType
	{
		public MathUtils.IntTrio Index;

		public Type Type;

		public DangerType DangerType;

		public bool HasPickup;

		public IndexedType(MathUtils.IntTrio index, Type type, DangerType dangerType, bool hasPickup)
		{
			Index = index;
			Type = type;
			DangerType = dangerType;
			HasPickup = hasPickup;
		}
	}

	private struct IndexedTransform
	{
		public MathUtils.IntTrio Index;

		public Transform Transform;

		public IndexedTransform(MathUtils.IntTrio index, Transform transform)
		{
			Index = index;
			Transform = transform;
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugFalling = false;

	private const bool debugReceivingBall = true;

	private const bool debugEdging = false;

	private const bool debugMoverChanging = false;

	private const bool showRecycled = false;

	public MathUtils.IntTrio Index;

	public Transform RootTransform;

	public Transform GeoTransform;

	public Transform AdditionTransform;

	public Transform AnimTransform;

	public Transform AltAnimTransform;

	public Transform[] AnimPartTransforms;

	public Transform ShadowTransform;

	public Transform ShadowAltTransform;

	public Transform SliderTransform;

	public Transform PickupTransform;

	public AudioSource AudioPlayer;

	public AudioSource AltAudioPlayer;

	private ArrayUtils.List<MathUtils.IntTrio> tileSection;

	private ArrayUtils.List<IndexedTransform> childrenTileGeos;

	private float geoRotations;

	private Vector3 position;

	private Material material;

	private Material additionalMaterial;

	private bool assignMaterial;

	private MathUtils.Tri alternateAnim = default(MathUtils.Tri);

	private int? leftAnimLimit;

	private int? rightAnimLimit;

	private int? leftAnimQueue;

	private int? rightAnimQueue;

	private int animChildrenTotal;

	private float? hitMarginX;

	private float? hitMarginY;

	private float? animValue;

	private FrameAnim.SequenceAuto tileGroundSwitch;

	private FrameAnim.SequenceAuto tileAdditionSwitch;

	private FrameAnim.SequenceAuto tileBeamSwitch;

	private MathUtils.IntPair movedOffset;

	public PrefabID PrefabID { get; private set; }

	public PrefabID AdditionPrefabID { get; private set; }

	public MaterialName MaterialName { get; private set; }

	public int TypeVal { get; private set; }

	public int SegmentIndex
	{
		get
		{
			return Index.x;
		}
		set
		{
			Index.x = value;
		}
	}

	public int RowIndex
	{
		get
		{
			return Index.y;
		}
		set
		{
			Index.y = value;
		}
	}

	public int TileIndex
	{
		get
		{
			return Index.z;
		}
		set
		{
			Index.z = value;
		}
	}

	public int? TileSectionKey { get; private set; }

	public float SegmentOffset { get; private set; }

	public int? AdditionOffset { get; private set; }

	public Direction direction { get; private set; }

	public Edge edge { get; private set; }

	public bool HasEdge { get; private set; }

	public bool HasTopEdge { get; private set; }

	public bool HasBottomEdge { get; private set; }

	public bool HasLeftEdge { get; private set; }

	public bool HasRightEdge { get; private set; }

	public int Value { get; private set; }

	public float ExtraValuePercent { get; private set; }

	public Type type { get; private set; }

	public Type? fakeType { get; private set; }

	public GroundType groundType { get; private set; }

	public PickupType pickupType { get; private set; }

	public DangerType dangerType { get; private set; }

	public DangerType? fakeDangerType { get; private set; }

	public AnimType animType { get; private set; }

	public bool IsJump { get; private set; }

	public bool IsSuperJump { get; private set; }

	public bool HasPickup { get; private set; }

	public bool HasFakePickup { get; private set; }

	public bool HasStar { get; private set; }

	public bool HasAddition { get; private set; }

	public bool HasAdditionShadow { get; private set; }

	public bool HasAdditionBase { get; private set; }

	public bool HasAudio { get; private set; }

	public bool HasAltAudio { get; private set; }

	public bool IsGroundSwitchable { get; private set; }

	public bool IsGroundRecolorable { get; private set; }

	public bool IsAdditionSwitchable { get; private set; }

	public bool IsBeamSwitchable { get; private set; }

	public bool IsAnimated { get; private set; }

	public bool IsAnimChild { get; private set; }

	public bool IsAnimParent { get; private set; }

	public bool IsAnimSkipped { get; private set; }

	public bool IsDangerous { get; private set; }

	public bool IsDangerSpreader { get; private set; }

	public bool IsSlider { get; private set; }

	public bool IsRoller { get; private set; }

	public bool IsFragile { get; private set; }

	public bool IsFallen { get; private set; }

	public bool IsMover { get; private set; }

	public bool IsMoverAuto { get; private set; }

	public bool IsMobile
	{
		get
		{
			return IsMover || IsMoverAuto;
		}
	}

	public bool IsMobileTrigger { get; private set; }

	public bool IsSpeeder { get; private set; }

	public bool CanHaveMiniPickup { get; private set; }

	public bool IsIsolated { get; private set; }

	public bool IsGeoHidden { get; private set; }

	public bool IsAirbourne { get; private set; }

	public bool IsFlipped { get; private set; }

	public bool IsNotJump
	{
		get
		{
			return !IsJump;
		}
	}

	public bool IsNotSuperJump
	{
		get
		{
			return !IsSuperJump;
		}
	}

	public bool IsNotFragile
	{
		get
		{
			return !IsFragile;
		}
	}

	public bool IsNotDangerous
	{
		get
		{
			return !IsDangerous;
		}
	}

	public bool IsNotSlider
	{
		get
		{
			return !IsSlider;
		}
	}

	public bool IsNotIsolated
	{
		get
		{
			return !IsIsolated;
		}
	}

	public bool IsNotGeoHidden
	{
		get
		{
			return !IsGeoHidden;
		}
	}

	public bool IsNotAirbourne
	{
		get
		{
			return !IsAirbourne;
		}
	}

	public bool IsNotFallen
	{
		get
		{
			return !IsFallen;
		}
	}

	public bool IsAnimNotSkipped
	{
		get
		{
			return !IsAnimSkipped;
		}
	}

	public bool IsActivated { get; private set; }

	public bool IsMoved { get; private set; }

	public bool IsMoveReplaced { get; private set; }

	public bool IsScored { get; private set; }

	public bool IsNotActivated
	{
		get
		{
			return !IsActivated;
		}
	}

	public bool IsNotMoved
	{
		get
		{
			return !IsMoved;
		}
	}

	public bool IsNotMoveReplaced
	{
		get
		{
			return !IsMoveReplaced;
		}
	}

	public bool IsNotScored
	{
		get
		{
			return !IsScored;
		}
	}

	public bool IsInUse { get; private set; }

	public bool IsGeoDisplayed { get; private set; }

	public bool IsAdditionDisplayed { get; private set; }

	public bool HasChildGeosDisplayed { get; private set; }

	public bool IsReset
	{
		get
		{
			return !IsInUse;
		}
	}

	public bool IsGeoNotDisplayed
	{
		get
		{
			return !IsGeoDisplayed;
		}
	}

	public bool IsAdditionNotDisplayed
	{
		get
		{
			return !IsAdditionDisplayed;
		}
	}

	public float Height { get; private set; }

	private Tile thisTile
	{
		get
		{
			return base.gameObject.GetComponent<Tile>();
		}
	}

	public int AnimParentTileNum { get; private set; }

	public void SetAttributes(GameObject assetObject, float segmentOffset, int segmentNum, int rowNum, int tileNum, float posX, float posY, float posZ, int typeVal, bool tileFlipped, Type type, GroundType groundType, PickupType pickupType, DangerType enemyType, AnimType animType, Direction direction, Edge edge, float? animValue)
	{
		if (IsInUse)
		{
			Debug.LogWarning(string.Format("TILE: ERROR: Attempting to reuse Tile script that had not been reset!  Resetting now: {0}", ToString()));
			ResetTile();
		}
		TypeVal = typeVal;
		SegmentOffset = segmentOffset;
		Index = new MathUtils.IntTrio(segmentNum, rowNum, tileNum);
		RootTransform = assetObject.transform;
		if (RootTransform == null)
		{
			Debug.LogError("TILE: ERROR: Received NULL RootTransform for tile" + ToString());
		}
		IsFlipped = tileFlipped;
		this.type = type;
		this.groundType = groundType;
		this.pickupType = pickupType;
		dangerType = enemyType;
		this.animType = animType;
		this.direction = direction;
		this.edge = edge;
		this.animValue = animValue;
		position = new Vector3(posX, posY, posZ);
		RootTransform.position = position;
		ConfigureType();
		ConfigureEdge();
		IsInUse = true;
	}

	public void SetType(Type newType)
	{
		type = newType;
	}

	public void SetFakeType(Type newType, DangerType newDangerType)
	{
		fakeType = newType;
		fakeDangerType = newDangerType;
	}

	public void SetGroundType(GroundType groundType)
	{
		this.groundType = groundType;
	}

	public void SetAsDangerous(float? hitMarginX, float? hitMarginY, DangerType dangerType = DangerType.Danger)
	{
		IsDangerous = true;
		this.dangerType = dangerType;
		CanHaveMiniPickup = false;
		this.hitMarginX = hitMarginX;
		this.hitMarginY = hitMarginY;
	}

	public void SetAltAnim()
	{
		alternateAnim = new MathUtils.Tri(true);
	}

	public void Activate(bool on = true)
	{
		IsActivated = on;
		if (IsMobileTrigger && tileAdditionSwitch != null && on == tileAdditionSwitch.isFirst)
		{
			SwitchAddition();
		}
	}

	public void ActivateFragile()
	{
		IsActivated = true;
		if (!GameManager.DisableMaterialSwitching)
		{
			GeoTransform.GetComponent<Renderer>().material = MaterialManager.Materials[MaterialName.FragileActive];
		}
	}

	public void SetAsIsolated(bool on = true)
	{
		IsIsolated = on;
	}

	public void SetAsMover()
	{
		IsMover = true;
	}

	public void SetAsMoverAuto()
	{
		IsMoverAuto = true;
	}

	public void SetAsMoved(MathUtils.IntPair movedOffset, bool hasFakePickup)
	{
		IsMoved = true;
		HasFakePickup = hasFakePickup;
		this.movedOffset = new MathUtils.IntPair(this.movedOffset.x + movedOffset.x, this.movedOffset.y + movedOffset.y);
		if (HasPickup && HasFakePickup)
		{
			HasPickup = false;
		}
		IsMoveReplaced = true;
	}

	public void SetAsFallen()
	{
		IsFallen = true;
		fakeDangerType = DangerType.Null;
	}

	public void SetAsAirbourne()
	{
		IsAirbourne = true;
	}

	private void ConfigureEdge()
	{
		HasEdge = this.edge != Edge.Null;
		HasTopEdge = false;
		HasBottomEdge = false;
		HasLeftEdge = false;
		HasRightEdge = false;
		if (!HasEdge)
		{
			return;
		}
		if (this.edge == Edge.All)
		{
			IsIsolated = true;
			HasEdge = false;
			return;
		}
		if (IsFlipped)
		{
			Edge edge = Edge.Null;
			switch (this.edge)
			{
			case Edge.BottomLeft:
				edge = Edge.BottomRight;
				break;
			case Edge.BottomRight:
				edge = Edge.BottomLeft;
				break;
			case Edge.TopBottomRight:
				edge = Edge.TopBottomLeft;
				break;
			case Edge.Left:
				edge = Edge.Right;
				break;
			case Edge.Right:
				edge = Edge.Left;
				break;
			case Edge.TopLeft:
				edge = Edge.TopRight;
				break;
			case Edge.TopBottomLeft:
				edge = Edge.TopBottomRight;
				break;
			case Edge.TopRight:
				edge = Edge.TopLeft;
				break;
			}
			if (edge != Edge.Null)
			{
				this.edge = edge;
			}
		}
		switch (this.edge)
		{
		case Edge.Bottom:
			HasBottomEdge = true;
			break;
		case Edge.BottomLeft:
			HasBottomEdge = true;
			HasLeftEdge = true;
			break;
		case Edge.BottomRight:
			HasBottomEdge = true;
			HasRightEdge = true;
			break;
		case Edge.TopBottomRight:
			HasTopEdge = true;
			HasBottomEdge = true;
			HasRightEdge = true;
			break;
		case Edge.Left:
			HasLeftEdge = true;
			break;
		case Edge.BottomLeftRight:
			HasBottomEdge = true;
			HasLeftEdge = true;
			HasRightEdge = true;
			break;
		case Edge.LeftRight:
			HasLeftEdge = true;
			HasRightEdge = true;
			break;
		case Edge.Right:
			HasRightEdge = true;
			break;
		case Edge.TopLeftRight:
			HasTopEdge = true;
			HasLeftEdge = true;
			HasRightEdge = true;
			break;
		case Edge.Top:
			HasTopEdge = true;
			break;
		case Edge.TopBottom:
			HasTopEdge = true;
			HasBottomEdge = true;
			break;
		case Edge.TopLeft:
			HasTopEdge = true;
			HasLeftEdge = true;
			break;
		case Edge.TopBottomLeft:
			HasTopEdge = true;
			HasBottomEdge = true;
			HasLeftEdge = true;
			break;
		case Edge.TopRight:
			HasTopEdge = true;
			HasRightEdge = true;
			break;
		default:
			Debug.LogError(string.Format("TILE: ERROR: Tile.ConfigureEdge() encountered unhandled Tile.Edge type of {0} (Flipped: {1}, Tile: {2}).  Check logic", this.edge, IsFlipped, ToString()));
			break;
		}
	}

	private void ConfigureType()
	{
		switch (type)
		{
		case Type.Ground:
			CanHaveMiniPickup = true;
			break;
		case Type.Jump:
		case Type.SuperJump:
			ConfigureJump();
			break;
		case Type.Enemy:
			ConfigureEnemy();
			break;
		case Type.Mover:
		case Type.MoverAuto:
			ConfigureMover();
			break;
		case Type.LaserTrigger:
		case Type.Bean:
			ConfigureExpansionTrigger();
			break;
		case Type.BackwardQuery:
			Configure1782Query();
			break;
		}
		if (pickupType != PickupType.Null && IsNotDangerous)
		{
			ConfigurePickup();
		}
		ConfigureFragile();
		ConfigureSlider();
		if (ExpansionGameplay.Enabled) ConfigureRotation();
		Direction direction = this.direction;
		if (IsFlipped && this.direction != Direction.Null)
		{
			this.direction = Directions.GetFlippedHorizontal(this.direction);
		}
	}

	private void ConfigureEnemy()
	{
		IsDangerous = true;
		HasAddition = true;
		IsAdditionSwitchable = true;
		HasAdditionShadow = false;
		HasAdditionBase = false;
		HasAudio = true;
		HasAltAudio = false;
		if (!Configure1782Enemy() && !ConfigureLaterEnemy() && !ConfigureExpansionEnemy() && !ConfigureClassicEnemy())
		{
			ConfigureBaselineEnemy();
		}
		if (ExpansionGameplay.Enabled) ConfigureExpansionSharedEnemy();
		if (ExpansionGameplay.Classic1782) Configure1782SharedEnemy();
		if (LevelDesigner.IsClassicWorld && !ExpansionGameplay.Classic1782)
		{
			AdditionPrefabID = new PrefabID(AdditionPrefabID.prefabType, ClassicPrefab(AdditionPrefabID.prefabName));
			AdditionPrefabID = BufferManager.ClassicPrefabID(AdditionPrefabID);
		}
	}

	private void ConfigureBaselineEnemy()
	{
		switch (dangerType)
		{
		case DangerType.Riser:
		case DangerType.RiserSlider:
		{
			PrefabName prefabName3 = PrefabName.Riser;
			if (LevelDesigner.GenerateTrees)
			{
				if (Randomizer.FlipACoin())
				{
					prefabName3 = PrefabName.TreeSmall1;
					alternateAnim.Value = false;
				}
				else
				{
					prefabName3 = PrefabName.TreeSmall2;
					alternateAnim.IsSpecialTrue = true;
				}
				if (dangerType == DangerType.RiserSlider)
				{
					animType = AnimType.TreeSlider;
					IsSlider = true;
				}
				else
				{
					animType = AnimType.Tree;
				}
				HasAdditionShadow = true;
				IsAdditionSwitchable = false;
				HasAdditionBase = true;
			}
			else if (LevelDesigner.GeneratePyramids)
			{
				prefabName3 = PrefabName.PyramidSmall;
				if (dangerType == DangerType.RiserSlider)
				{
					animType = AnimType.PyramidSlider;
					IsSlider = true;
				}
				else
				{
					animType = AnimType.Pyramid;
				}
				HasAdditionShadow = true;
				IsAdditionSwitchable = false;
				HasAdditionBase = false;
			}
			else
			{
				prefabName3 = PrefabName.Riser;
				if (dangerType == DangerType.RiserSlider)
				{
					animType = AnimType.RiserSlider;
					IsSlider = true;
				}
				else
				{
					animType = AnimType.Riser;
				}
				HasAdditionBase = true;
			}
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefabName3);
			break;
		}
		case DangerType.FloaterGround:
		case DangerType.FloaterAir:
		{
			PrefabName prefabName2 = PrefabName.Floater;
			prefabName2 = PrefabName.Floater;
			animType = AnimType.Floater;
			if (dangerType == DangerType.FloaterAir)
			{
				IsAirbourne = true;
				IsGeoHidden = true;
			}
			else
			{
				HasAdditionShadow = true;
			}
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefabName2);
			break;
		}
		case DangerType.Crusher:
		case DangerType.CrusherSafe:
			animType = AnimType.Crusher;
			if (dangerType == DangerType.CrusherSafe)
			{
				alternateAnim.Value = true;
			}
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Crusher);
			HasAdditionShadow = true;
			break;
		case DangerType.Roller:
			IsRoller = true;
			animType = AnimType.Roller;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Roller);
			IsAdditionSwitchable = true;
			HasAdditionShadow = true;
			HasAudio = false;
			break;
		case DangerType.Binary:
		case DangerType.BinaryOffset:
		case DangerType.BinaryMini:
		case DangerType.BinaryMiniOffset:
			if (dangerType == DangerType.Binary || dangerType == DangerType.BinaryOffset)
			{
				bool flag;
				if (LevelDesigner.GenerateHammersAndSlashers)
				{
					flag = Randomizer.FlipACoin();
				}
				else if (LevelDesigner.GenerateHammers)
				{
					flag = true;
				}
				else if (LevelDesigner.GenerateSlashers)
				{
					flag = false;
				}
				else
				{
					Debug.LogError(string.Format("TILE: ERROR: Unable to determine Binary type for Enemy Binary {0} based on LevelDesigner values - check LevelDesigner BinaryMode type values", ToString()));
					flag = true;
				}
				if (flag)
				{
					animType = AnimType.BinaryHammer;
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Pounder);
				}
				else
				{
					animType = AnimType.BinarySlasher;
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Slasher);
				}
			}
			else
			{
				AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.BinaryMini);
				animType = AnimType.BinarySlasherMini;
			}
			alternateAnim.Value = direction == Direction.Right;
			IsDangerSpreader = true;
			HasAdditionBase = true;
			IsAdditionSwitchable = false;
			if (dangerType == DangerType.BinaryOffset)
			{
				hitMarginY = 0.125f;
				AdditionOffset = ((direction != Direction.Right) ? 1 : (-1));
			}
			break;
		case DangerType.Wyrm:
			animType = AnimType.Wyrm;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Wyrm);
			IsDangerSpreader = true;
			IsAdditionSwitchable = false;
			HasAudio = false;
			break;
		case DangerType.TallTree:
		case DangerType.TallTreeSlider:
		{
			PrefabName prefabName = PrefabName.TreeTall;
			if (LevelDesigner.GenerateTowers)
			{
				prefabName = PrefabName.Tower;
				if (dangerType == DangerType.TallTreeSlider)
				{
					animType = AnimType.TowerSlider;
					IsSlider = true;
				}
				else
				{
					animType = AnimType.Tower;
				}
			}
			else
			{
				prefabName = PrefabName.TreeTall;
				if (dangerType == DangerType.TallTreeSlider)
				{
					animType = AnimType.TreeSlider;
					IsSlider = true;
				}
				else
				{
					animType = AnimType.Tree;
				}
				HasAdditionShadow = true;
				alternateAnim.IsTrue = true;
			}
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefabName);
			HasAdditionBase = true;
			IsAdditionSwitchable = false;
			break;
		}
		case DangerType.PalmTree:
		case DangerType.PalmTreeSlider:
			if (dangerType == DangerType.PalmTreeSlider)
			{
				animType = AnimType.PalmTreeSlider;
				IsSlider = true;
			}
			else
			{
				animType = AnimType.PalmTree;
			}
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.TreePalm);
			HasAdditionBase = true;
			IsAdditionSwitchable = false;
			HasAdditionShadow = true;
			break;
		case DangerType.Pillar:
		case DangerType.PillarSlider:
		case DangerType.PillarSafe:
		case DangerType.PillarSafeSlider:
		case DangerType.PillarLaser:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Pillar);
			IsAdditionSwitchable = false;
			if (dangerType == DangerType.PillarSlider || dangerType == DangerType.PillarSafeSlider)
			{
				animType = AnimType.PillarSlider;
				IsSlider = true;
				break;
			}
			animType = AnimType.Pillar;
			if (dangerType == DangerType.PillarLaser)
			{
				alternateAnim.IsSpecialTrue = true;
				IsAdditionSwitchable = true;
				HasAltAudio = true;
				AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.PillarLaser);
			}
			break;
		case DangerType.Laser:
		case DangerType.LaserSlider:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Laser);
			if (dangerType == DangerType.LaserSlider)
			{
				animType = AnimType.LaserSlider;
				IsSlider = true;
			}
			else
			{
				animType = AnimType.Laser;
			}
			HasAudio = true;
			HasAltAudio = true;
			break;
		case DangerType.Spotlight:
		case DangerType.SpotlightSlider:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Spotlight);
			if (dangerType == DangerType.SpotlightSlider)
			{
				animType = AnimType.SpotlightSlider;
				IsSlider = true;
			}
			else
			{
				animType = AnimType.Spotlight;
			}
			HasAudio = false;
			break;
		default:
			Debug.LogError(string.Format("Error CEN_UET - unexpected enemyType of {0} for enemy tile {1}", dangerType, RootTransform.gameObject.name));
			break;
		}
	}

	public void MakeSuperRiser(int superRiserSize)
	{
		PrefabName prefabName = PrefabName.Riser;
		bool flag = true;
		if (flag)
		{
			switch (superRiserSize)
			{
			case 2:
				prefabName = PrefabName.PyramidMid;
				alternateAnim.Value = true;
				break;
			case 3:
				prefabName = PrefabName.PyramidBig;
				alternateAnim.Special = true;
				break;
			default:
				Debug.LogError(string.Format("TILE: ERROR: Received unhandled superRiserSize of {0} for altRiser - this type has not yet been configure in Tile.MakeSuperRiser()", superRiserSize));
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			Debug.LogError(string.Format("TILE: ERROR: Received unhandled superRiserSize of {0} for normal Riser - this type has not yet been configured in Tile.MakeSuperRiser()", superRiserSize));
		}
		AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefabName);
	}

	public void HideRiser()
	{
		animType = AnimType.Null;
		HasAddition = false;
		IsAdditionSwitchable = false;
		dangerType = DangerType.Danger;
	}

	private void ConfigurePickup()
	{
		HasPickup = true;
		CanHaveMiniPickup = false;
		Value = 1;
		ExtraValuePercent = 0f;
		switch (pickupType)
		{
		case PickupType.Big:
			animType = AnimType.Pickup;
			HasStar = false;
			break;
		case PickupType.BigFloater:
			animType = AnimType.PickupFloater;
			HasStar = false;
			break;
		case PickupType.BigSlider:
			animType = AnimType.PickupSlider;
			IsSlider = true;
			HasStar = false;
			break;
		case PickupType.Star:
			animType = AnimType.Star;
			HasStar = true;
			break;
		case PickupType.StarFloater:
			animType = AnimType.StarFloater;
			HasStar = true;
			break;
		case PickupType.StarSlider:
			animType = AnimType.StarSlider;
			IsSlider = true;
			HasStar = true;
			break;
		default:
			Debug.LogError(string.Format("TILE: ERROR: Encountered unhandled pickup type of {0} in Tile.ConfigurePickup()'s case statement.  Check logic", pickupType));
			animType = AnimType.Pickup;
			break;
		}
		HasAddition = true;
		PrefabName prefabName;
		if (HasStar)
		{
			prefabName = ExpansionGameplay.Enabled ? PrefabName.pickupCrown : PrefabName.PickupStar;
			HasAdditionBase = !ExpansionGameplay.Enabled;
		}
		else
		{
			prefabName = PrefabName.PickupBig;
		}
		AdditionPrefabID = new PrefabID(PrefabType.Pickup, prefabName);
		if (ExpansionGameplay.Enabled)
		{
			IsStarJumpSlider = (TileVal)TypeVal == TileVal.PickupStarJumpLeft || (TileVal)TypeVal == TileVal.PickupStarJumpRight;
		}
	}

	private void ConfigureMover()
	{
		PrefabName prefabName;
		if (IsType(Type.Mover))
		{
			IsMover = true;
			prefabName = PrefabName.ArrowMover;
		}
		else
		{
			IsMoverAuto = true;
			prefabName = PrefabName.ArrowMoverAuto;
		}
		IsMobileTrigger = true;
		HasAudio = true;
		HasAddition = true;
		AdditionPrefabID = new PrefabID(PrefabType.Arrow, prefabName);
	}

	private void ConfigureFragile()
	{
		IsFragile = groundType == GroundType.Fragile0 || groundType == GroundType.Fragile1 || groundType == GroundType.Fragile2 || groundType == GroundType.FragileChecker || groundType == GroundType.FragileRow || groundType == GroundType.FragileColumn;
		if (IsFragile)
		{
			IsGroundRecolorable = true;
			if (IsNotIsolated)
			{
				CanHaveMiniPickup = false;
			}
		}
	}

	private void ConfigureSpeeder()
	{
		groundType = GroundType.Speed;
		animType = AnimType.Speeder;
		IsSpeeder = true;
		IsGroundSwitchable = true;
	}

	private void ConfigureJump()
	{
		groundType = GroundType.Jump;
		IsJump = true;
		IsGroundSwitchable = true;
		IsSuperJump = type == Type.SuperJump;
	}

	private void ConfigureFinish()
	{
		groundType = GroundType.Finish;
	}

	private void ConfigureSave()
	{
	}

	private void ConfigureSlider()
	{
		if (!IsSlider)
		{
			IsSlider = animType == AnimType.Slider;
		}
		animChildrenTotal = 0;
	}

	private void SetAsFragile()
	{
		IsFragile = true;
		groundType = GroundType.Fragile0;
		IsGroundSwitchable = true;
	}

	public void SetAsCannotHaveMiniPickup()
	{
		CanHaveMiniPickup = false;
	}

	public void ConfigureAnimChild(int animParentTileNum)
	{
		AnimParentTileNum = animParentTileNum;
		IsAnimChild = true;
		if (ExpansionGameplay.Enabled)
		{
			if (IsNeedRotation) return;
			if (animType == AnimType.StarSlider) { animType = AnimType.Star; return; }
			if (animType == AnimType.PyramidSlider) { animType = AnimType.Pyramid; return; }
			if (ExpansionGameplay.Later && animType == AnimType.EnemyPokerTreeSlider) { animType = AnimType.EnemyPokerTree; return; }
		}
		switch (animType)
		{
		case AnimType.RiserSlider:
			animType = AnimType.Riser;
			break;
		case AnimType.TreeSlider:
			animType = AnimType.Tree;
			break;
		case AnimType.PalmTreeSlider:
			animType = AnimType.PalmTree;
			break;
		case AnimType.TowerSlider:
			animType = AnimType.Tower;
			break;
		case AnimType.LaserSlider:
			animType = AnimType.Laser;
			break;
		case AnimType.SpotlightSlider:
			animType = AnimType.Spotlight;
			break;
		case AnimType.PillarSlider:
			animType = AnimType.Pillar;
			break;
		case AnimType.PickupSlider:
			animType = AnimType.Pickup;
			break;
		case AnimType.Roller:
			alternateAnim.Value = true;
			break;
		default:
			IsAnimSkipped = true;
			break;
		}
	}

	public void ConfigureAnimParent(int animChildTileNum)
	{
		int num = animChildTileNum - TileIndex;
		if (!IsAnimParent)
		{
			IsAnimParent = true;
			animChildrenTotal = num;
		}
		else if (num > animChildrenTotal)
		{
			animChildrenTotal = num;
		}
	}

	public void ReceiveChild(MathUtils.IntTrio childIndex)
	{
		GameManager.TileBoard[childIndex].GeoTransform.parent = GeoTransform;
		if (childrenTileGeos == null)
		{
			childrenTileGeos = new ArrayUtils.List<IndexedTransform>();
		}
		childrenTileGeos.Add(new IndexedTransform(childIndex, GameManager.TileBoard[childIndex].GeoTransform));
		HasChildGeosDisplayed = true;
	}

	public void ConfigureMovementLimits(int? leftLimit, int? rightLimit, int? leftQueue = null, int? rightQueue = null)
	{
		if (leftAnimLimit.HasValue)
		{
			if (leftLimit.HasValue && leftLimit.Value > leftAnimLimit.Value)
			{
				leftAnimLimit = leftLimit;
			}
		}
		else
		{
			leftAnimLimit = leftLimit;
		}
		if (rightAnimLimit.HasValue)
		{
			if (rightLimit.HasValue && rightLimit.Value < rightAnimLimit.Value)
			{
				rightAnimLimit = rightLimit;
			}
		}
		else
		{
			rightAnimLimit = rightLimit;
		}
		if (IsRoller)
		{
			leftAnimQueue = rightQueue;
			rightAnimQueue = leftQueue;
		}
		else
		{
			leftAnimQueue = leftQueue;
			rightAnimQueue = rightQueue;
		}
	}

	public void CreateTile(PrefabType prefabType, PrefabName prefabName, MaterialName materialName, float prefabRotations = 0f)
	{
		geoRotations = prefabRotations;
		SetupTilePrefab(prefabType, prefabName, materialName);
		if (HasAddition)
		{
			AddAdditionPrefabToTile();
		}
	}

	public void CreateEmptyTile()
	{
		PrefabID = PrefabID.Null;
		if (HasAddition)
		{
			AddAdditionPrefabToTile();
		}
	}

	private void SetupTilePrefab(PrefabType prefabType, PrefabName prefabName, MaterialName materialName)
	{
		PrefabID = new PrefabID(prefabType, prefabName);
		MaterialName = materialName;
		if (ExpansionGameplay.Enabled)
		{
			if (prefabName == PrefabName.MoveJumpLeft) animType = AnimType.MoveJumpLeft;
			if (prefabName == PrefabName.MoveJumpRight) animType = AnimType.MoveJumpRight;
		}
	}

	public void ChangePrefabTypeTo(PrefabType newPrefabType)
	{
		SetupTilePrefab(newPrefabType, PrefabID.prefabName, MaterialName);
	}

	public void ChangePrefabTypeTo(PrefabType newPrefabType, MaterialName newMaterialName)
	{
		SetupTilePrefab(newPrefabType, PrefabID.prefabName, newMaterialName);
	}

	public void AddToTile(PrefabType prefabType, PrefabName prefabName)
	{
		AdditionPrefabID = new PrefabID(prefabType, prefabName);
		HasAddition = true;
		AddAdditionPrefabToTile();
	}

	private void AddAdditionPrefabToTile()
	{
		if (AdditionPrefabID.prefabType == PrefabType.Pickup)
		{
			HasPickup = true;
			CanHaveMiniPickup = false;
		}
		else if (AdditionPrefabID.prefabType != PrefabType.Enemy && AdditionPrefabID.prefabType != PrefabType.Arrow &&
			!(ExpansionGameplay.Classic1782 && (type == Type.Bean || type == Type.LaserTrigger || type == Type.BackwardQuery)))
		{
			Debug.LogError(string.Format("TILE: ERROR: Attempt to add unexpected addition of {0} to Tile {1} - check Tile.AddAdditionPrefabToTile() function's logic", AdditionPrefabID, ToString()));
		}
	}

	public void Display()
	{
		if (IsGeoDisplayed)
		{
			return;
		}
		IsGeoDisplayed = true;
		RootTransform.localEulerAngles = Vector3.zero;
		if (PrefabID.IsNotNull && IsNotGeoHidden)
		{
			bool flag = false;
			GeoTransform = BufferManager.GetGeo(PrefabID);
			GeoTransform.parent = RootTransform;
			GeoTransform.localPosition = Vector3.zero;
			GeoTransform.eulerAngles = new Vector3(0f, 0f, (0f - geoRotations) * 90f);
			if (!GameManager.DisableMaterialSwitching)
			{
				switch (MaterialName)
				{
				case MaterialName.General:
				case MaterialName.Fragile:
				case MaterialName.Mover:
				case MaterialName.MoverAuto:
					if (GeoTransform.GetComponent<Renderer>() != null)
					{
						GeoTransform.GetComponent<Renderer>().material = MaterialManager.Materials[MaterialName];
					}
					break;
				}
			}
			if (IsMobile || IsSlider)
			{
				IsGroundSwitchable = false;
			}
			if (IsGroundSwitchable)
			{
				tileGroundSwitch = new FrameAnim.SequenceAuto(GeoTransform);
			}
		}
		if (HasAddition && IsAdditionNotDisplayed)
		{
			DisplayAddition();
		}
		if (IsBeamSwitchable)
		{
			Debug.Log("!!!");
			Transform parentTransform = TransformUtils.FindChild(GeoTransform, "beam");
			tileBeamSwitch = new FrameAnim.SequenceAuto(parentTransform, "beam");
		}
		if (HasAudio || HasAltAudio)
		{
			if (AdditionTransform == null)
			{
				Debug.LogError("TILE: ERROR: Attempt to find audio source on empty Addition transform for: " + ToString());
			}
			else
			{
				if (HasAudio)
				{
					Transform transform = TransformUtils.Find("sound", AdditionTransform);
					if (transform == null)
					{
						Debug.LogError("TILE: ERROR: Attempt to find audio source on a tile which didn't have an audio transform, specifically: " + ToString());
					}
					else
					{
						AudioPlayer = transform.GetComponent<AudioSource>();
					}
				}
				if (HasAltAudio)
				{
					Transform transform2 = TransformUtils.Find("altSound", AdditionTransform);
					if (transform2 == null)
					{
						Debug.LogError("TILE: ERROR: Attempt to find audio alternate source on a tile which didn't have an audio transform, specifically: " + ToString());
					}
					else
					{
						AltAudioPlayer = transform2.GetComponent<AudioSource>();
					}
				}
			}
		}
		if (animType == AnimType.Null)
		{
			return;
		}
		if (IsAnimChild)
		{
			if (IsSlider)
			{
				GameManager.TileBoard[SegmentIndex, RowIndex, AnimParentTileNum].ReceiveChild(Index);
			}
			else if (!IsNeedRotation)
			{
				Debug.LogError("TILE: ERROR: Found unexpected parented-animation child that was neither a slider nor a strafer, but a " + ToString());
			}
		}
		if (IsAnimNotSkipped)
		{
			CreateAnimator();
		}
		if (ExpansionGameplay.Enabled && IsNeedRotation && !IsAnimChild)
		{
			yAngle = 30 * NextExpansionRotationSign();
		}
	}

	private void DisplayAddition(Transform geoToUse = null)
	{
		if (!HasAddition || !IsAdditionNotDisplayed)
		{
			return;
		}
		IsAdditionDisplayed = true;
		if (ExpansionGameplay.Enabled && PrefabID.prefabName == PrefabName.Null &&
			(animType == AnimType.EnemyExtendTile || animType == AnimType.EnemyFloaterAirFallGround || animType == AnimType.RoadUp ||
			(ExpansionGameplay.Later && IsGeoHidden))) GeoTransform = null;
		AdditionTransform = BufferManager.GetGeo(AdditionPrefabID);
		if (AdditionTransform == null)
		{
			Debug.LogError(string.Format(string.Format("TILE: ERROR: Recieved null addition transform for {0}, with Addition PrefabID {1}", ToString(), AdditionPrefabID)));
		}
		if (GeoTransform == null)
		{
			AdditionTransform.parent = RootTransform;
		}
		else
		{
			AdditionTransform.parent = GeoTransform;
		}
		AdditionTransform.localPosition = Vector3.zero;
		AdditionTransform.localEulerAngles = Vector3.zero;
		if (AdditionOffset.HasValue)
		{
			TransformUtils.MoveX(AdditionTransform, AdditionOffset.Value);
		}
		if (IsMobileTrigger)
		{
			float num = Directions.ToFloat(direction) / 2f;
			if (num != geoRotations)
			{
				float num2 = num - geoRotations;
				AdditionTransform.Rotate(Vector3.back * num2 * 90f);
			}
			IsAdditionSwitchable = true;
		}
		if (IsAdditionSwitchable)
		{
			tileAdditionSwitch = new FrameAnim.SequenceAuto(AdditionTransform);
		}
		if (!GameManager.DisableMaterialSwitching && HasAdditionShadow)
		{
			Material material = null;
			switch (MaterialName)
			{
			case MaterialName.General:
				material = MaterialManager.Materials[MaterialName.General];
				break;
			case MaterialName.Fragile:
				material = MaterialManager.Materials[MaterialName.Fragile];
				break;
			case MaterialName.Mover:
				material = MaterialManager.Materials[MaterialName.Mover];
				break;
			case MaterialName.MoverAuto:
				material = MaterialManager.Materials[MaterialName.MoverAuto];
				break;
			default:
				Debug.LogError(string.Format("TILE: ERROR: Found unexpected addition shadow on ground material type {0}", MaterialName));
				break;
			}
			if (material != null)
			{
				Transform[] array = TransformUtils.FindAll("shadow", AdditionTransform);
				foreach (Transform transform in array)
				{
					transform.GetComponent<Renderer>().material = material;
				}
			}
		}
		if (!HasAdditionBase || GameManager.DisableMaterialSwitching)
		{
			return;
		}
		Material material2 = null;
		switch (MaterialName)
		{
		case MaterialName.General:
		case MaterialName.Mover:
		case MaterialName.MoverAuto:
			material2 = MaterialManager.Materials[MaterialName.General];
			break;
		case MaterialName.Fragile:
			material2 = MaterialManager.Materials[MaterialName.Fragile];
			break;
		default:
			Debug.LogError(string.Format("TILE: ERROR: Found unexpected addition base on ground material type {0}", MaterialName));
			break;
		}
		if (material2 != null)
		{
			Transform[] array2 = TransformUtils.FindAll("base", AdditionTransform);
			foreach (Transform transform2 in array2)
			{
				transform2.GetComponent<Renderer>().material = material2;
			}
		}
	}

	public void SwitchGround()
	{
		if (IsGroundSwitchable)
		{
			tileGroundSwitch.ShowLast();
		}
	}

	public void RecolorGround()
	{
		if (!IsGroundRecolorable)
		{
			return;
		}
		if (IsFragile)
		{
			if (!GameManager.DisableMaterialSwitching)
			{
				GeoTransform.GetComponent<Renderer>().material = MaterialManager.Materials[MaterialName.FragileActive];
			}
		}
		else
		{
			Debug.LogError(string.Format("TILE: ERROR: Attempt to recolor non-fragile tile {0}.  This functionality has only been coded for fragile tiles, so recheck logic", ToString()));
		}
	}

	public void SwitchAddition(bool animSwitch = false)
	{
		if (IsAdditionSwitchable)
		{
			tileAdditionSwitch.Next();
		}
		if (animSwitch)
		{
			tileAdditionSwitch.UpdateAlternate();
		}
	}

	public bool IsAdditionSwitched()
	{
		if (IsAdditionSwitchable)
		{
			return tileAdditionSwitch.isLast;
		}
		return false;
	}

	public bool IsAdditionNotSwitched()
	{
		return !IsAdditionSwitched();
	}

	public void SwitchBeam()
	{
		if (IsBeamSwitchable)
		{
			tileBeamSwitch.Next();
		}
	}

	public void HideBeam()
	{
		if (IsBeamSwitchable)
		{
			tileBeamSwitch.Hide();
		}
	}

	public void SetParticleVisibility(bool visibility)
	{
		ParticleEmitter[] componentsInChildren = base.transform.GetComponentsInChildren<ParticleEmitter>();
		foreach (ParticleEmitter particleEmitter in componentsInChildren)
		{
			Transform item = particleEmitter.transform;
			if (visibility)
			{
				TransformUtils.Show(item);
				particleEmitter.emit = false;
			}
			else
			{
				TransformUtils.Hide(item);
			}
		}
	}

	public void PlaySound()
	{
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds && GameManager.GameStarted)
		{
			if (HasAudio)
			{
				if (!ExpansionGameplay.Enabled || CanPlayExpansionSound()) AudioPlayer.Play();
			}
			else
			{
				Debug.LogError("TILE: ERROR: Attempt to PlaySound() on a tile WITHOUT a predefined sound effect: " + ToString());
			}
		}
	}

	public void PlayAltSound()
	{
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds)
		{
			if (HasAltAudio)
			{
				AltAudioPlayer.Play();
			}
			else
			{
				Debug.LogError("TILE: ERROR: Attempt to PlayAltSound() on a tile WITHOUT a predefined sound effect: " + ToString());
			}
		}
	}

	public void PlayNewSound(AudioName soundName)
	{
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds)
		{
			Transform geo = BufferManager.GetGeo(PrefabType.General, PrefabName.Sound);
			geo.parent = RootTransform;
			geo.localPosition = Vector3.zero;
			AudioSource component = geo.GetComponent<AudioSource>();
			component.clip = GameManager.Sounds[soundName];
			if (ExpansionGameplay.Classic1782 && soundName == AudioName.Jump)
				component.volume = (TileVal)TypeVal == TileVal.BeanLv34Jump1Tile ? 0.25f : 0.6f;
			component.Play();
		}
	}

	private void CreateAnimator()
	{
		bool flag = false;
		Transform transform = null;
		Transform transform2 = null;
		switch (animType)
		{
		case AnimType.Riser:
		case AnimType.RiserSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			for (int k = 0; k < AnimPartTransforms.Length; k++)
			{
				TransformUtils.SetY(AnimPartTransforms[k], 0f, true);
			}
			break;
		}
		case AnimType.Floater:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			ShadowTransform = TransformUtils.Find("shadow", AdditionTransform);
			ShadowAltTransform = TransformUtils.Find("shadowAlt", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			ShadowTransform.eulerAngles = Vector3.zero;
			if (DeviceQualityChecker.QualityIsHigh())
			{
				ShadowAltTransform.eulerAngles = Vector3.zero;
				ShadowAltTransform.localScale = Vector3.one * 0.46f;
			}
			else
			{
				TransformUtils.Hide(ShadowAltTransform);
			}
			for (int n = 0; n < AnimPartTransforms.Length; n++)
			{
				AnimPartTransforms[n].localEulerAngles = new Vector3(70.45f, 0f, 0f);
			}
			break;
		}
		case AnimType.Crusher:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			ShadowTransform = TransformUtils.Find("shadow", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			if (alternateAnim.IsFalse)
			{
				SwitchAddition();
			}
			break;
		case AnimType.BinaryHammer:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			break;
		case AnimType.BinarySlasher:
		case AnimType.BinarySlasherMini:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			float x = ((!alternateAnim.IsTrue) ? 180f : 0f);
			TransformUtils.Find("altAnim", AdditionTransform).localEulerAngles = new Vector3(x, 0f, 0f);
			break;
		}
		case AnimType.Roller:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			SliderTransform = AnimTransform;
			if (alternateAnim.IsFalse)
			{
				SwitchAddition();
			}
			alternateAnim.Value = direction == Direction.Right;
			break;
		case AnimType.Wyrm:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			AltAnimTransform.localEulerAngles = Vector3.zero;
			AnimTransform.localPosition = Vector3.zero;
			for (int m = 0; m < AnimPartTransforms.Length; m++)
			{
				AnimPartTransforms[m].localEulerAngles = Vector3.zero;
			}
			break;
		}
		case AnimType.Tree:
		case AnimType.TreeSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			for (int i = 0; i < AnimPartTransforms.Length; i++)
			{
				TransformUtils.SetY(AnimPartTransforms[i], 0f, true);
			}
			break;
		}
		case AnimType.PalmTree:
		case AnimType.PalmTreeSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			for (int num6 = 0; num6 < 10; num6++)
			{
				AnimPartTransforms[num6].localPosition = Vector3.zero;
			}
			for (int num7 = 10; num7 < AnimPartTransforms.Length; num7++)
			{
				AnimPartTransforms[num7].localEulerAngles = Vector3.zero;
			}
			break;
		}
		case AnimType.Tower:
		case AnimType.TowerSlider:
		{
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform, true);
			AnimTransform = AnimPartTransforms[0];
			for (int num = 0; num < AnimPartTransforms.Length; num++)
			{
				if (num < 4)
				{
					float newY = ((num != 3) ? 0.01f : 0.125f);
					TransformUtils.SetY(AnimPartTransforms[num], newY, true);
				}
				else
				{
					float num2 = ((num < 8) ? 35f : ((num >= 12) ? 30f : 20f));
					AnimPartTransforms[num].localEulerAngles = new Vector3(0f, 0f, 0f - num2);
				}
			}
			AdditionTransform.eulerAngles = new Vector3(0f, 0f, 0f);
			break;
		}
		case AnimType.Pillar:
		case AnimType.PillarSlider:
		case AnimType.LeaningGeneralCube3:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			bool flag2 = dangerType == DangerType.PillarSafe || dangerType == DangerType.PillarSafeSlider;
			int num3 = -1;
			if (flag2)
			{
				num3 = (int)animValue.Value - 1;
			}
			else
			{
				TransformUtils.Hide(AnimPartTransforms[AnimPartTransforms.Length - 1]);
			}
			for (int num4 = 0; num4 < AnimPartTransforms.Length - 1; num4++)
			{
				Transform transform3 = AnimPartTransforms[num4];
				int num5 = 0;
				if (flag2 && num4 >= num3)
				{
					num5 = 1;
					if (num4 == num3)
					{
						transform3 = AnimPartTransforms[AnimPartTransforms.Length - 1];
						TransformUtils.Hide(AnimPartTransforms[num4]);
					}
				}
				transform3.localPosition = new Vector3(0f, 0f - (float)(num4 + num5), 0f);
			}
			break;
		}
		case AnimType.Pyramid:
		case AnimType.PyramidSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			Vector3 localPosition = ((!alternateAnim.IsTrue) ? new Vector3(0f, 0f, 0f) : new Vector3(0.5f, 0f, 0.5f));
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			AltAnimTransform.localPosition = localPosition;
			AltAnimTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
			AnimTransform.localEulerAngles = Vector3.zero;
			if (ExpansionGameplay.Enabled)
			{
				ShadowTransform = TransformUtils.Find("shadow", AdditionTransform);
				ShadowTransform.localPosition = Vector3.zero;
				foreach (Transform part in AnimPartTransforms) part.localPosition = Vector3.zero;
			}
			flag = true;
			break;
		}
		case AnimType.Laser:
		case AnimType.LaserSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			for (int l = 0; l < AnimPartTransforms.Length; l++)
			{
				AnimPartTransforms[l].localPosition = Vector3.zero;
				AnimPartTransforms[l].localEulerAngles = Vector3.zero;
			}
			AltAnimTransform.GetComponent<ParticleEmitter>().emit = false;
			AltAnimTransform.GetComponent<ParticleAnimator>().rndForce = Vector3.zero;
			break;
		}
		case AnimType.Spotlight:
		case AnimType.SpotlightSlider:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			break;
		case AnimType.Turbine:
		case AnimType.TurbineSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			for (int j = 0; j < AnimPartTransforms.Length; j++)
			{
				AnimPartTransforms[j].localPosition = Vector3.zero;
			}
			break;
		}
		case AnimType.Pickup:
		case AnimType.PickupFloater:
		case AnimType.PickupSlider:
		case AnimType.Star:
		case AnimType.StarFloater:
		case AnimType.StarSlider:
			PickupTransform = TransformUtils.Find("pickup", AdditionTransform);
			PickupTransform.localPosition = Vector3.zero;
			PickupTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			if (HasStar && !ExpansionGameplay.Enabled)
			{
				TransformUtils.Find("base", AdditionTransform).eulerAngles = new Vector3(270f, 0f, 0f);
			}
			else if (HasStar)
			{
				PickupTransform.rotation = Quaternion.Euler(new Vector3(90f, 180f, 0f));
				PickupTransform.Find("crownpart").gameObject.SetActive(!ClassicCrownRandom.Enabled);
				PickupTransform.Find("boxpart").gameObject.SetActive(ClassicCrownRandom.Enabled);
			}
			break;
		default:
			if (!Create1782Animator() && !CreateLaterAnimator() && !CreateExpansionAnimator() && !CreateClassicAnimator())
			{
				Debug.LogError(string.Format("TILE: ERROR: Unexpected AnimType of {0} for tile {1} - please extend function CreateAnimator to allow for {0} type Animations", animType, ToString()));
			}
			break;
		case AnimType.Slider:
			break;
		}
		if (IsSlider)
		{
			SliderTransform = GeoTransform;
		}
		if (!flag && AnimTransform != null)
		{
			AnimTransform.localPosition = Vector3.zero;
			AnimTransform.eulerAngles = new Vector3(270f, 0f, 0f);
		}
		if (ShadowTransform != null && IsAirbourne)
		{
			TransformUtils.Hide(ShadowTransform);
			Debug.Log("!!!");
		}
		TileAnimator.Add(Index, animType, alternateAnim, direction, animValue);
		IsAnimated = true;
	}

	public bool TryPickup(bool ballIsRolling, bool ballIsJumping, Transform target)
	{
		if (IsNotScored && IsNotFallen)
		{
			if (HasPickup)
			{
				if (CanPickup(ballIsRolling, ballIsJumping))
				{
					Pickup(target);
				}
			}
			else if (HasFakePickup)
			{
				MathUtils.IntTrio index = Index;
				index.y -= movedOffset.y;
				index.z -= movedOffset.x;
				IsScored = GameManager.TileBoard[index].TryPickup(ballIsRolling, ballIsJumping, target);
			}
		}
		return IsScored;
	}

	private bool CanPickup(bool ballIsRolling, bool ballIsJumping)
	{
		return (ballIsRolling && (type == Type.Ground || type == Type.Jump)) || (ballIsJumping && (type == Type.Air || animType == AnimType.PickupFloater));
	}

	public void Pickup(Transform target)
	{
		AnimType? animType = null;
		if (!IsJump && !IsFragile)
		{
			switch (this.animType)
			{
			case AnimType.Pickup:
				animType = AnimType.PickupCollect;
				break;
			case AnimType.Star:
				if (ExpansionGameplay.Enabled) animType = AnimType.StarCollect;
				break;
			default:
				Debug.LogError(string.Format("TILE: ERROR: Unhandled Pickup case for tile.AnimType of {0}", this.animType));
				break;
			case AnimType.Jump:
			case AnimType.MoveJumpLeft:
			case AnimType.MoveJumpRight:
			case AnimType.StarSlider:
			case AnimType.PickupSlider:
			case AnimType.Faller:
				break;
			}
		}
		if (animType.HasValue)
		{
			TileAnimator.Alter(Index, animType.Value);
		}
		else
		{
			ScorePickup();
		}
	}

	public void ScorePickup()
	{
		if (IsNotScored)
		{
			if (ExpansionGameplay.Enabled) GameManager.ApplyScoreFor(pickupType, CrownIndex);
			else GameManager.ApplyScoreFor(pickupType);
			BufferManager.HoldGeo(AdditionTransform, true);
			IsAdditionDisplayed = false;
			IsScored = true;
		}
	}

	public void Jump()
	{
		if (ExpansionGameplay.Enabled && (IsSuperJump || IsSuccessiveJump))
		{
			SwitchAddition();
			ParticleManager.Detonate(PrefabName.Particles_PickupBig);
			PlaySound();
			return;
		}
		if (ExpansionGameplay.Later && IsSimulationJump) return;
		SwitchGround();
		TileAnimator.Add(Index, AnimType.Jump);
	}

	public void Fragment(float animOffset, bool instantFall = false)
	{
		if (!instantFall && RootTransform.position.y + 1f - animOffset > 1f)
		{
			animOffset = RootTransform.position.y + 1.01f - 1f;
		}
		ActivateFragile();
		if (IsNotIsolated)
		{
			ArrayUtils.List<MathUtils.IntTrio> list = LevelConstructor.GetTileSection(Index);
			int length = list.Length;
			childrenTileGeos = new ArrayUtils.List<IndexedTransform>(length - 1);
			HasChildGeosDisplayed = true;
			for (int i = 0; i < length; i++)
			{
				MathUtils.IntTrio intTrio = list[i];
				if (MathUtils.IntTrio.AreNotEqual(intTrio, Index))
				{
					GameManager.TileBoard[intTrio].GeoTransform.parent = GeoTransform;
					GameManager.TileBoard[intTrio].ActivateFragile();
					childrenTileGeos.Add(new IndexedTransform(intTrio, GameManager.TileBoard[intTrio].GeoTransform));
				}
			}
		}
		TileAnimator.Add(Index, AnimType.Faller, new MathUtils.Tri(instantFall), direction, animOffset);
	}

	public void FallSection()
	{
		SetAsFallen();
		if (IsNotIsolated)
		{
			for (int i = 0; i < childrenTileGeos.Length; i++)
			{
				GameManager.TileBoard[childrenTileGeos[i].Index].SetAsFallen();
			}
		}
	}

	public void Move(float animOffset)
	{
		IsActivated = true;
		tileAdditionSwitch.ShowLast();
		MathUtils.IntPair intPair2 = Directions.ToOffset(direction);
		ArrayUtils.List<MathUtils.IntTrio> adjacentAutoMovers = new ArrayUtils.List<MathUtils.IntTrio>();
		if (IsIsolated)
		{
			MathUtils.IntTrio index = Index;
			index.y += intPair2.y;
			index.z += intPair2.x;
			if (GameManager.TileBoard.IsValid(index))
			{
				if (GameManager.TileBoard[index] == null)
				{
					GameManager.TileBoard[index] = LevelConstructor.CreateFakeTile(Index, index);
				}
				else
				{
					GameManager.TileBoard[index].SetFakeType(type, dangerType);
				}
				GameManager.TileBoard[index].SetAsMoved(Directions.ToOffset(direction), HasPickup);
				StoreAdjacents(index, null, ref adjacentAutoMovers);
			}
		}
		else
		{
			tileSection = LevelConstructor.GetTileSection(Index);
			int length = tileSection.Length;
			childrenTileGeos = new ArrayUtils.List<IndexedTransform>(length - 1);
			HasChildGeosDisplayed = true;
			ArrayUtils.List<IndexedType> list = new ArrayUtils.List<IndexedType>(length);
			for (int i = 0; i < length; i++)
			{
				MathUtils.IntTrio intTrio = tileSection[i];
				if (ExpansionGameplay.Classic1782 && GameManager.TileBoard[intTrio].IsHidden) continue;
				if (MathUtils.IntTrio.AreNotEqual(intTrio, Index))
				{
					GameManager.TileBoard[intTrio].GeoTransform.parent = GeoTransform;
					childrenTileGeos.Add(new IndexedTransform(intTrio, GameManager.TileBoard[intTrio].GeoTransform));
				}
				MathUtils.IntTrio index = intTrio;
				index.y += intPair2.y;
				index.z += intPair2.x;
				if (GameManager.TileBoard.IsValid(index))
				{
					if (GameManager.TileBoard[index] == null)
					{
						GameManager.TileBoard[index] = LevelConstructor.CreateFakeTile(intTrio, index);
					}
					else
					{
						list.Add(new IndexedType(index, GameManager.TileBoard[intTrio].type, GameManager.TileBoard[intTrio].dangerType, GameManager.TileBoard[intTrio].HasPickup));
					}
					GameManager.TileBoard[index].SetAsMoved(Directions.ToOffset(direction), GameManager.TileBoard[intTrio].HasPickup);
				}
				StoreAdjacents(index, tileSection, ref adjacentAutoMovers);
			}
			if (list.IsNotEmpty)
			{
				for (int j = 0; j < list.Length; j++)
				{
					GameManager.TileBoard[list[j].Index].SetFakeType(list[j].Type, list[j].DangerType);
				}
			}
			list.Clear();
		}
		TileAnimator.Add(Index, AnimType.Mover, new MathUtils.Tri(false), direction, animOffset);
		if (adjacentAutoMovers.IsNotEmpty)
		{
			animOffset += 0.35f;
			for (int k = 0; k < adjacentAutoMovers.Length; k++)
			{
				GameManager.TileBoard[adjacentAutoMovers[k]].Move(animOffset + 0.35f);
			}
		}
		adjacentAutoMovers.Clear();
	}

	private bool ShouldMoveAutoMover(MathUtils.IntTrio tileIndex)
	{
		return GameManager.TileBoard[tileIndex].IsMobileTrigger && GameManager.TileBoard[tileIndex].IsMoverAuto && GameManager.TileBoard[tileIndex].IsNotActivated && GameManager.TileBoard[tileIndex].IsNotMoved;
	}

	private void StoreAdjacents(MathUtils.IntTrio tileAhead, ArrayUtils.List<MathUtils.IntTrio> tileSection, ref ArrayUtils.List<MathUtils.IntTrio> adjacentAutoMovers)
	{
		IsMoved = true;
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if ((i != 0 && j != 0) || (i == 0 && j == 0))
				{
					continue;
				}
				MathUtils.IntTrio intTrio = tileAhead;
				intTrio.y += i;
				intTrio.z += j;
				if (!GameManager.TileBoard.IsNotNull(intTrio))
				{
					continue;
				}
				bool flag = false;
				if (IsNotIsolated)
				{
					for (int k = 0; k < tileSection.Length; k++)
					{
						if (MathUtils.IntTrio.AreEqual(intTrio, tileSection[k]))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					continue;
				}
				if (GameManager.TileBoard[intTrio].IsIsolated)
				{
					if (ShouldMoveAutoMover(intTrio))
					{
						adjacentAutoMovers.AddNew(intTrio);
					}
					continue;
				}
				ArrayUtils.List<MathUtils.IntTrio> list = LevelConstructor.GetTileSection(intTrio);
				for (int l = 0; l < list.Length; l++)
				{
					if (ShouldMoveAutoMover(list[l]))
					{
						adjacentAutoMovers.AddNew(list[l]);
					}
				}
			}
		}
	}

	public void FinishMove()
	{
		if (IsIsolated)
		{
			IsAirbourne = true;
			return;
		}
		int num = 0;
		for (int i = 0; i < tileSection.Length; i++)
		{
			if (GameManager.TileBoard[tileSection[i]].IsNotMoveReplaced)
			{
				GameManager.TileBoard[tileSection[i]].SetAsAirbourne();
				num++;
			}
		}
		tileSection.Clear();
	}

	public void SetHeight(float newHeight)
	{
		Height = newHeight;
		base.transform.position = new Vector3(position.x, position.y, newHeight);
	}

	public void Offset(Vector3 ammount)
	{
		base.transform.position = position + ammount;
	}

	public void DebugHighlight()
	{
	}

	public void CenterTile()
	{
	}

	public Vector3 GetPosition()
	{
		return position;
	}

	public bool IsSameTileAs(Tile queryTile)
	{
		return TileIndex == queryTile.TileIndex && RowIndex == queryTile.RowIndex;
	}

	public bool IsNotSameTileAs(Tile queryTile)
	{
		return !IsSameTileAs(queryTile);
	}

	public bool IsType(Type queryType)
	{
		return type == queryType;
	}

	public bool IsType(params Type[] queryTypes)
	{
		bool result = false;
		for (int i = 0; i < queryTypes.Length; i++)
		{
			if (type == queryTypes[i])
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public bool IsNotType(Type queryType)
	{
		return !IsType(queryType);
	}

	public bool IsNotType(params Type[] queryTypes)
	{
		return !IsType(queryTypes);
	}

	public bool IsSafe()
	{
		if (type == Type.Jump)
		{
			return true;
		}
		if (IsMobileTrigger && IsMover)
		{
			return true;
		}
		if (HasPickup && pickupType != PickupType.BigFloater && pickupType != PickupType.StarFloater)
		{
			return true;
		}
		return false;
	}

	public bool IsMaybeSafe()
	{
		if (type == Type.Ground)
		{
			return true;
		}
		if (IsDangerous)
		{
			return dangerType == DangerType.CrusherSafe || dangerType == DangerType.FloaterGround;
		}
		if (IsFragile)
		{
			return groundType == GroundType.FragileChecker || groundType == GroundType.FragileRow;
		}
		if (IsSlider)
		{
			return true;
		}
		return false;
	}

	public bool CanKillBall()
	{
		if (fakeDangerType.HasValue)
		{
			return fakeDangerType.Value != DangerType.Null;
		}
		return dangerType != DangerType.Null;
	}

	public bool ShouldKillBall(bool ballJumping, Vector3 ballPos, out DangerType dangerTypeUsed)
	{
		bool flag = false;
		dangerTypeUsed = ((!fakeDangerType.HasValue) ? dangerType : fakeDangerType.Value);
		if (ExpansionGameplay.Classic1782 && IsHidden) return false;
		if (Try1782KillBall(dangerTypeUsed, ballJumping, ballPos, out flag)) return flag;
		if (ExpansionGameplay.Enabled && (TryLaterKillBall(dangerTypeUsed, ballJumping, ballPos, out flag) || TryExpansionKillBall(dangerTypeUsed, ballJumping, ballPos, out flag)))
		{
			return flag;
		}
		if (TryClassicKillBall(dangerTypeUsed, ballJumping, ballPos, out flag))
		{
			return flag;
		}
		switch (dangerTypeUsed)
		{
		case DangerType.Riser:
		case DangerType.RiserSlider:
		case DangerType.Binary:
		case DangerType.BinaryMini:
		case DangerType.Wyrm:
		case DangerType.Spotlight:
		case DangerType.SpotlightSlider:
		case DangerType.Laser:
		case DangerType.LaserSlider:
		case DangerType.Turbine:
		case DangerType.TurbineSlider:
			flag = !ballJumping;
			break;
		case DangerType.Pillar:
		case DangerType.PillarSlider:
		case DangerType.PillarSafe:
		case DangerType.PillarSafeSlider:
		case DangerType.PillarLaser:
			flag = !ballJumping && dangerType != DangerType.PillarSafe && dangerType != DangerType.PillarSafeSlider;
			break;
		case DangerType.TallTree:
		case DangerType.TallTreeSlider:
		case DangerType.PalmTree:
		case DangerType.PalmTreeSlider:
			flag = !ballJumping || ((Mathf.Abs(GeoTransform.position.y - ballPos.y) <= 0.2f) ? true : false);
			break;
		case DangerType.Danger:
		case DangerType.Heat:
		case DangerType.BinaryOffset:
		case DangerType.BinaryMiniOffset:
			if (ballJumping)
			{
				flag = false;
			}
			else if (hitMarginY.HasValue)
			{
				float? num = hitMarginY;
				flag = num.HasValue && Mathf.Abs(RootTransform.position.y - ballPos.y) <= num.Value;
			}
			else if (hitMarginX.HasValue)
			{
				float? num2 = hitMarginX;
				flag = num2.HasValue && Mathf.Abs(RootTransform.position.x - ballPos.x) <= num2.Value;
			}
			else
			{
				flag = true;
			}
			break;
		case DangerType.FloaterGround:
		case DangerType.FloaterAir:
			flag = ballJumping && ((Mathf.Abs(GeoTransform.position.y - ballPos.y) <= 0.2f) ? true : false);
			break;
		case DangerType.Crusher:
		case DangerType.CrusherSafe:
			flag = !ballJumping && dangerType == DangerType.Crusher;
			break;
		case DangerType.Roller:
			flag = !ballJumping && Mathf.Abs(AnimTransform.position.x - ballPos.x) <= 0.5f;
			break;
		default:
			Debug.LogError(string.Format("Error CEH_UAT - attempt to check enemy hit against non-enemy tile {0} of type {1} and anim/enemyType {2}", base.gameObject.name, PrefabID, animType));
			flag = false;
			break;
		}
		if (flag && IsBeamSwitchable)
		{
			SwitchBeam();
		}
		if (flag)
		{
			Debug.Log(string.Format("TILE: DEBUG: Impacting {0} on tile {1}", dangerType, ToString()));
		}
		if (flag && IsSlider)
		{
			flag = Mathf.Abs(AnimTransform.position.x - ballPos.x) <= 0.5f;
		}
		return flag;
	}

	public void AnimateSaveTile()
	{
		TileAnimator.AddSpecial(AnimType.Save);
	}

	public override string ToString()
	{
		return string.Format("{0} @ ({1}, {2})", PrefabID, RowIndex, TileIndex);
	}

	public bool ResetTile()
	{
		bool result = false;
		if (IsInUse)
		{
			if (IsGroundSwitchable && tileGroundSwitch != null)
			{
				tileGroundSwitch.Clear();
			}
			if (IsAdditionSwitchable && tileAdditionSwitch != null)
			{
				tileAdditionSwitch.Clear();
			}
			if (IsBeamSwitchable && tileBeamSwitch != null)
			{
				tileBeamSwitch.Clear();
			}
			TileAnimator.TryRemove(Index, false);
			ResetVariables();
			result = true;
			IsInUse = false;
		}
		return result;
	}

	public void ResetVariables()
	{
		ResetExpansionVariables();
		IsJump = false;
		IsSuperJump = false;
		HasPickup = false;
		HasFakePickup = false;
		HasStar = false;
		CanHaveMiniPickup = false;
		HasAddition = false;
		HasAdditionShadow = false;
		HasAdditionBase = false;
		HasAudio = false;
		HasAltAudio = false;
		IsAnimated = false;
		IsDangerous = false;
		IsDangerSpreader = false;
		IsDangerHeatSpreader = false;
		IsTurret = false;
		dangerSpreadType = DangerSpreadType.Null;
		dangerSpreadLength = 0;
		turret_laser_z_scale = 0f;
		newAnimTrasFromAnimation = null;
		TileParticle = null;
		AnimParticleSystem = null;
		IsFragile = false;
		IsSpeeder = false;
		IsMover = false;
		IsMoverAuto = false;
		IsSlider = false;
		IsRoller = false;
		IsAnimChild = false;
		IsGroundSwitchable = false;
		IsGroundRecolorable = false;
		IsAdditionSwitchable = false;
		IsBeamSwitchable = false;
		IsIsolated = false;
		IsGeoHidden = false;
		IsAirbourne = false;
		IsFallen = false;
		IsMobileTrigger = false;
		IsFlipped = false;
		IsAnimSkipped = false;
		HasEdge = false;
		HasTopEdge = false;
		HasBottomEdge = false;
		HasLeftEdge = false;
		HasRightEdge = false;
		IsActivated = false;
		IsMoved = false;
		IsMoveReplaced = false;
		IsScored = false;
		IsGeoDisplayed = false;
		IsAdditionDisplayed = false;
		assignMaterial = false;
		alternateAnim.Value = false;
		edge = Edge.Null;
		direction = Direction.Null;
		AdditionOffset = null;
		leftAnimLimit = null;
		rightAnimLimit = null;
		leftAnimQueue = null;
		rightAnimQueue = null;
		hitMarginX = null;
		hitMarginY = null;
		fakeType = null;
		fakeDangerType = null;
		movedOffset = MathUtils.IntPair.Zero;
	}
}
