using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public const bool GodMode = false;

	public const bool DisableNone = false;

	public const bool DisableAll = false;

	public const bool DisableMidgrounds = false;

	public const bool DisableMaterials = false;

	public const bool DisableWorldThemes = true;

	public const bool DisableHud = false;

	public const bool DisableRiserBlocking = false;

	public const bool DisableAnimation = false;

	public const bool DisableBallShadowScaling = true;

	public const bool DisableBallShadows = false;

	public const bool DisableParticles = false;

	public const bool DisablePersistantBuffering = false;

	public const bool DisablePreBuffering = false;

	public const bool DisableRowBuffering = false;

	public const bool DisableSegmentBuffering = false;

	public const bool DisableWorldBlending = true;

	public const bool ManualSegmentControl = false;

	public const bool PersistantMusic = true;

	public const bool DestructiveBuffers = true;

	public const bool AllowAutoGameOverHits = false;

	public const bool FragmentedBallStart = true;

	public const bool PersistChoices = false;

	public const bool ReuseScene = true;

	public const bool ExplainIapEveryTime = true;

	private const bool showDebugPortal = false;

	private const bool debugTutorials = true;

	private const bool resetOnExit = false;

	private const bool resetOnExitConfirmed = false;

	public const bool Benchmark = false;

	public const bool DebugSegmentsInGame = false;

	public const bool DebugSegmentsOnDeath = false;

	public const bool DebugSegmentAdding = false;

	public const bool DebugBallDeaths = false;

	public const bool DebugPreGameTime = false;

	public const bool DebugPlayTime = true;

	public const bool DebugSpeed = false;

	public const bool DebugRandomSeed = true;

	public const bool DebugCurrentSegment = true;

	public const bool DebugRowOnSpace = true;

	public const bool ShowSaves = false;

	public const string HashTag = "RollingSky";

	public const int SceneMenu = 0;

	public const int SceneMain = 1;

	public const int SceneDebug = 2;

	public const double VersionNumber = 3.3;

	public const float VersionNumberFloat = 3.3f;

	private const int targetFpsNormal = 60;

	private const int targetFpsPour = 30;

	private const int autoPauseFps = 5;

	public const int JumpDistance = 4;

	public const int SuperJumpDistance = 30;

	public const float JumpHeight = 2f;

	public const int FragileSafeDistance = 1;

	public const int TotalWorlds = LevelCatalog.Count;

	public const float GameOverDelay = 1.5f;

	public const float PreGameDelay = 1.5f;

	public const float BallLeadDistance = 15f;

	public const int MaxSafeJumpDistance = 20;

	public const int SafeImmunityDistance = 3;

	public const float BallAirBuffer = 0.15f;

	public const float TileMoverAutoDistance = 0.35f;

	public const float StartingRowOffset = 2f;

	public const float DangerWidth = 0.125f;

	public const float LaserDangerWidth = 0.5f;

	public const float IdealLandingPosition = 0.25f;

	public const float LiftRange = 3.5f;

	public const int GemsPerWorld = 20;

	public const int ValuePerPickup = 1;

	public const bool HasMiniPickups = false;

	public const int ValuePerLife = 5;

	public const bool RewardHighscore = true;

	public const bool PersistentMusic = false;

	public const bool DevMode = false;

	public const bool AllowEmptyLocations = false;

	public const bool IsolateJumps = true;

	public const bool ObviousSafeRisers = true;

	public const bool SkipTutorials = true;

	public const bool PitchShift = false;

	public const bool RecolorWorldEnd = false;

	public const bool LeadWarningsEnabled = false;

	public const bool AnimatedSaving = false;

	public const float GameMusicVolume = 1f;

	public const float GameSoundVolume = 1f;

	public const float GameMusicVolumePreGame = 0.3f;

	public const float ExplodeSoundVolume = 0.5f;

	public const float PickupSoundVolume = 1f;

	public const float MiniPickupSoundVolume = 0.25f;

	public const float JumpSoundVolume = 0.6f;

	public const float FragileSoundVolume = 0.8f;

	public const float EnemyRiseSoundVolume = 0.3f;

	public const float EnemyRiseQuietSoundVolume = 0.5f;

	public const float EnemySharpenSoundVolume = 0.3f;

	public const float GameMusicSlowMoPitch = 0.9f;

	public const float HammerSoundDistanceMin = 1f;

	public const float HammerSoundDistanceMax = 15f;

	public const float HammerSoundPan = 1f;

	public const float CrusherSoundDistanceMin = 1f;

	public const float CrusherSoundDistanceMax = 15f;

	public const float CrusherSoundPan = 3f;

	public const float LaserSoundPan = 3f;

	public const float BallPowerupSuperJumpWingAngle = 600f;

	public const float BallPowerupSuperJumpWingUpdateSpeed = 12f;

	public const string AnimName = "anim";

	public const string PickupName = "pickup";

	public const string ShadowName = "shadow";

	public const string ShadowAltName = "shadowAlt";

	public const string AltAnimName = "altAnim";

	public const string BaseName = "base";

	public const string PartName = "part";

	public const string PartSetName = "allParts";

	public const string BeamName = "beam";

	public const string SoundName = "sound";

	public const string AltSoundName = "altSound";

	public const float ShadowHeight = -0.01f;

	public const float tileThickness = 0.2f;

	public const float tileGroundHeight = 0.15f;

	public const float FragileTriggerDistance = 1f;

	public const float FragileShakeDistance = 0.5f;

	public const float FragileFallDistance = 4f;

	public const float FragileAnimDistance = 4.5f;

	public const float FragileShakeAmmount = 0.05f;

	public const float FragileShakes = 2f;

	public const float FragileShakeAngle = 0.5f;

	public const float FragileFallAmmount = 10f;

	public const float FragileFallAngle = 90f;

	public const float TileMoveDistance = 1f;

	public const float TileJumpDistance = 2f;

	public const float TileJumpHeight = 0.5f;

	public const float SaveAnimDistance = 0.5f;

	public const float EnemyCrushserRiseOffsetSafe = -0.6f;

	public const float EnemyCrushserRiseOffsetDanger = -0.1725f;

	public const float EnemyCrusherRotarSpeed = 1000f;

	public const float EnemyCrusherRisePeriod = 12f;

	public const float EnemyCrusherRiseHeight = 2f;

	public const float EnemyCrusherSpinRangeMin = -0.25f;

	public const float EnemyCrusherSpinRangeMax = 1f;

	public const float EnemyCrusherSpinAmntMin = 0.05f;

	public const float EnemyCrusherSpinnerExtension = 0.2f;

	public const float EnemyCrusherShadowScaleMin = 0.7f;

	public const float EnemyCrusherShadowScaleMax = 1f;

	public const float EnemyCrusherPulseWeight = 0.85f;

	public const float EnemyHitMarginHeight = 0.5f;

	public const float EnemyHitMarginDepth = 0.2f;

	public const float TurbineReactDistanceStart = 10f;

	public const float TurbineReactDistanceEnd = 3f;

	public const float TurbineSpinnerExtension = 0.5f;

	public const float TurbineRotarSpeed = 1100f;

	public const float EnemyRiserReactEnd = 2.65f;

	public const float EnemyRiserReactStart = 5.65f;

	public const float EnemyRiserRiseHeight = 0.625f;

	public const float EnemyRiserPartInterval = 0.225f;

	public const float EnemyRiserPartGap = 1.4444447f;

	public const float EnemyFloaterRiseHeight = 2f;

	public const float EnemyFloaterRiseStart = 10f;

	public const float EnemyFloaterRiseEnd = 3f;

	public const float EnemyFloaterReactStart = 2f;

	public const float EnemyFloaterReactEnd = 1f;

	public const float EnemyFloaterFlushAngle = 70.45f;

	public const float EnemyFloaterTwitchAmmount = 7.5f;

	public const float EnemyFloaterTileOffset = 0.1f;

	public const float EnemyFloaterShadowAltScaleMin = 0.5f;

	public const float EnemyFloaterShadowScaleMin = 0.46f;

	public const float EnemyFloaterShadowScaleMax = 1f;

	public const float EnemyRollerRollAmmount = 180f;

	public const float EnemyRollerPeriod = 20f;

	public const float EnemyRollerOffset = 0f;

	public const float EnemyRollerHitMargin = 0.5f;

	public const float EnemyHammerPeriod = 6f;

	public const float EnemyHammerOffset = 0f;

	public const int EnemyHammerTorque = 3;

	public const float EnemyHammerArc = 146f;

	public const float EnemySlasherPeriod = 4f;

	public const float EnemySlasherOffset = 0f;

	public const float EnemyBinaryMiniPeriod = 4f;

	public const float EnemyBinaryMiniOffset = 0f;

	public const float EnemyBinaryMiniVolume = 0.75f;

	public const float EnemySmallTree1LeafHeight = 0.3f;

	public const float EnemySmallTree1LeafOvershoot = 1.1666666f;

	public const float EnemySmallTree1TrunkHeight = 0.4f;

	public const float EnemySmallTree1ReactEnd = 2.65f;

	public const float EnemySmallTree1ReactStart = 12.65f;

	public const float EnemySmallTree2LeafHeight = 0.075f;

	public const float EnemySmallTree2LeafOvershoot = 1.6666666f;

	public const float EnemySmallTree2TrunkHeight = 0.85f;

	public const float EnemySmallTree2ReactEnd = 2.65f;

	public const float EnemySmallTree2ReactStart = 12.65f;

	public const float EnemySmallTree2BobAmmount = 0.125f;

	public const float EnemySmallTree2LeafStart = 0.25f;

	public const float EnemySmallTree2LeafEnd = 0.4f;

	public const float EnemyTallTreeLeafHeight = 0.3f;

	public const float EnemyTallTreeLeafOvershoot = 1.1666666f;

	public const float EnemyTallTreeTrunkHeight = 0.4f;

	public const float EnemyTallTreeReactEnd = 2.65f;

	public const float EnemyTallTreeReactStart = 12.65f;

	public const float EnemyTowerReactEnd = 2.65f;

	public const float EnemyTowerReactStart = 8.65f;

	public const float EnemyTowerAimEnd = 2f;

	public const float EnemyTowerSectionHeightMin = 0.01f;

	public const float EnemyTowerTopSectionHeightMin = 0.125f;

	public const float EnemyTowerSectionHeightMax = 0.5f;

	public const float EnemyTowerTopSectionHeightMax = 0.25f;

	public const int EnemyTowerTotalSections = 4;

	public const float EnemyTowerBaseSpikeAngle = 35f;

	public const float EnemyTowerMidSpikeAngle = 20f;

	public const float EnemyTowerTopSpikeAngle = 30f;

	public const int EnemyTowerAimerIndex = 16;

	public const int EnemyPalmTreeTrunkParts = 10;

	public const float EnemyPalmTreeTrunkOvershoot = 1.1f;

	public const float EnemyPalmTreeTrunkPartHeight = 0.2f;

	public const float EnemyPalmTreeRiseEnd = 4f;

	public const float EnemyPalmTreeRiseStart = 6f;

	public const float EnemyPalmTreeGrowEnd = 2.25f;

	public const float EnemyPalmTreeGrowStart = 4.25f;

	public const int EnemyPalmTreeLeaves = 4;

	public const int EnemyPalmTreeLeafParts = 10;

	public const int EnemyPalmTreeLeafPartsWithBase = 11;

	public const int EnemyPalmTreeLeafPartsTotal = 44;

	public const float EnemyPalmTreeLeafRotation = 30f;

	public const float EnemyPalmTreeLeafRebound = 0.1f;

	public const float EnemyPalmTreeLeafOvershoot = 1.1f;

	public const float EnemyPalmTreeLeafOvershootTime = 0.75f;

	public const float EnemyPyramidReactEnd = 1f;

	public const float EnemyPyramidReactStart = 10f;

	public const float EnemyPyramidRotation = 0f;

	public const float EnemyPyramidBuildStartPercent = 0.4f;

	public const float EnemyPyramidBuildEndPercent = 0.8f;

	public const float EnemyPyramidFallPercent = 0.85f;

	public const float EnemyPyramidBuildSpacing = 0.3f;

	public const float EnemyPillarReactEnd = 3f;

	public const float EnemyPillarReactStart = 13f;

	public const float EnemyPillarReactDistance = 2.5f;

	public const float EnemyPillarFallStart = 0.5f;

	public const float EnemyPillarFallEnd = -3.5f;

	public const float EnemyPillarRebound = 0.0125f;

	public const float EnemyLaserFireDistance = 1f;

	public const float EnemyLaserReactDistanceEnd = 5f;

	public const float EnemyLaserReactDistanceStart = 15f;

	public const float EnemyLaserRecoilDistance = 4.5f;

	public const float EnemyLaserResetDistance = 0f;

	public const float EnemyLaserCloseDistanceStart = 0f;

	public const float EnemyLaserCloseDistanceEnd = -1f;

	public const float EnemyLaserEyeOpenAmmount = 0.05f;

	public const float EnemyLaserRecoilPoint = 0.125f;

	public const float EnemyLaserShakeAmplitude = 0.075f;

	public const float EnemyLaserShakeFrequency = 15f;

	public const float EnemyLaserRecoilAmmount = 0.25f;

	public const float EnemyLaserChargeParticlesMax = 50f;

	public const float EnemyLaserKillProximity = 0.55f;

	public const float EnemyLaserHeatProximity = 2f;

	public const float EnemyMaxImpactDistance = 20f;

	public const float EnemyMaxWooshDistance = 20f;

	public const float CameraShakeMaxDistance = 20f;

	public const float CameraShakesMax = 7f;

	public const float CameraShakeForceMax = 0.5f;

	public const float CameraShakeVibrateForce = 0.5f;

	public const float EnemyCrusherShakeForce = 0.5f;

	public const float EnemyPounderShakeForce = 0.5f;

	public const float EnemyLaserShakeForce = 0.8f;

	public const float PickupBigCollectionTime = 0.2f;

	public const float PickupMiniCollectionTime = 0.4f;

	public const float PickupHiddenCollectionTime = 0.3f;

	public const float PickupBigCollectionRange = 0.375f;

	public const float PickupMiniCollectionRange = 0.25f;

	public const float PickupHiddenCollectionRange = 0.05f;

	public const float PickupTargetOffsetZ = 0.5f;

	public const float PickupReactDistancePosMin = 1.75f;

	public const float PickupReactDistancePosMax = 12f;

	public const float PickupReactDistanceNegMin = 0f;

	public const float PickupReactDistanceNegMax = 1f;

	public const float PickupRiseHeightMax = 0.7f;

	public const int PickupMaxCurrenyToDisplay = 150;

	public const float PickupSpinSpeed = 20f;

	public const float PickupSpinAcceleration = 10f;

	public const float PickupFloaterGroundHeight = 0f;

	public const float PickupFloaterRisenHeight = 3f;

	public const float SliderPacePeriod = 20f;

	public const float SliderPaceOffset = 0.0125f;

	public const float WorkingAspectRatio = 0.5623053f;

	public const float popupWaitTimeMax = 2f;

	public const float menuSlideTime = 0.5f;

	public const float aspectRatioDefault = 0.5625f;

	public const float starsPopTime = 0.5f;

	public const float starsPopAmmountComplete = 0.1f;

	public const float starsPopAmmountPerfect = 0.25f;

	public const float starsPopSpeed = 0.6f;

	public const float hudWorldWidth = 0.65f;

	public const float slideBarFadeOutTime = 1.5f;

	public const float slidePromptFadeOutTime = 0.5f;

	public const float slidePromptFadeInTime = 1f;

	public const float backgroundXPanAmnt = 0.01f;

	public const float backgroundYPanAmnt = 0.1f;

	public const float backgroundPanUpdate = 1f;

	public const float backgroundPanMargin = 0.0001f;

	public const float destructionTime = 3f;

	public const float destructionGravity = 1f;

	public const float destructionForceUpMin = 2f;

	public const float destructionForceUpMax = 10f;

	public const float destructionMaxMoveY = 8f;

	public const float destructionLowerMinMoveX = 0.25f;

	public const float destructionHigherMinMoveX = 1f;

	public const float destructionMaxMoveX = 1.25f;

	public const float destructionMaxTorque = 10f;

	public const float CameraRatioX = 0.75f;

	public const float CameraRatioMultX = 1f;

	public const float CameraPosSpeedX = 6.5f;

	public const float CameraPosMarginX = 0.001f;

	public const float CameraPosYStart = 3f;

	public const float CameraPosYEnd = 3.5f;

	public const float CameraPosTransitionTime = 1f;

	public const float CameraPosZStart = 5f;

	public const float CameraPosZEnd = 4f;

	public const float BallDeathShakeForce = 0.5f;

	public const int materialSets = 2;

	public const string colorNameDefault = "_Color";

	public const string textureNameDefault = "_MainTex";

	public const string colorGlowNameDefault = "_GlowAmnt";

	public const int retriesTillUnlock = 5;

	public const float minFps = 30f;

	public const float maxDeltaTime = 1f / 30f;

	public const float pauseMargin = 0.2f;

	public const float scoreMin = 0.1f;

	public const string AlphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public const float NormalJumpDistanceAsFloat = 4f;

	public static int? DebugWorldNum = null;

	public static int? DebugStart = null;

	public static SegmentType? DebugSegmentType = null;

	public static int? DebugDifficulty = null;

	public static int? DebugSegmentNum = null;

	public static DeviceQualityChecker.Quality? DebugQuality = null;

	public static bool Mute = false;

	public static bool DisableSounds = false || Mute;

	public static bool DisableMusic = false || Mute;

	public static bool DisableMaterialSwitching = false;

	public static int RandomSeed = 35;

	public static int LiftScanRange = MathUtils.Ceiled(3.5f);

	public static int StartingLives = 20;

	public static int LivesPerVideo = 10;

	public static int LivesPerRecharge = 10;

	public static float SecondsPerRecharge = 120f;

	public static float SecondsPerRechargeShort = 60f;

	public static float SecondsPerFakeRecharge = 3f;

	public static bool InterstitialsEnabled = false;

	public static int InterstitialLivesNum = 5;

	public static int GamesBeforeFirstTutorial = 3;

	public static int GamesBeforeSecondTutorial = 6;

	public static int CompetantPercentage = 50;

	public Transform cameraOverlay;

	public Transform gameOverBackgroundOverlay;

	public Transform background;

	public Transform gameplayTopGroup;

	public Transform gameOverGroup;

	public Transform prompt;

	public Transform nextWorldGroup;

	public Transform getLivesGroup;

	public Transform pausedGroup;

	public Transform scoreText;

	public Transform scoreGroup;

	public Transform progressGroup;

	public Transform progressOffsetGroup;

	public Transform progressText;

	public Transform progressTextShadow;

	public Transform progressPercentSymbol;

	public Transform progressPercentSymbolShadow;

	public Transform progressBar;

	public Transform gemsGroup;

	public Transform gemsText;

	public Transform gemsTextShadow;

	public Transform gemsTotalText;

	public Transform gemsTotalTextShadow;

	public Transform gemsIcon;

	public Transform gemsIconShadow;

	public Transform savesBar;

	public Transform savesText;

	public Transform livesText;

	public Transform livesTextShadow;

	public Transform nextWorldLock;

	public Transform nextWorldOverlay;

	public Transform nextWorldTitleQuad;

	public Transform backButton;

	public Transform playButton;

	public Transform resumeButton;

	public Transform pauseButton;

	public Transform nextButton;

	public Transform recordButton;

	public Transform getBallsButton;

	public Transform videoButton;

	public Transform shopButton;

	public Transform debugText;

	public Transform debugButton;

	public Transform getLivesTitle;

	public Transform getLivesTime;

	public Transform getLivesTimeTitle;

	public Transform getLivesClock;

	public Transform pausedTitle;

	public Transform pausedText;

	public Transform tutorial;

	public Transform tutorialTitle;

	public Transform tutorialMessage;

	public Transform tutorialButton;

	public AudioClip jumpSound;

	public AudioClip fragileSound;

	public static Dictionary<AudioName, AudioClip> Sounds;

	private Dictionary<HudPart, Transform> hudParts;

	private static bool ballActive;

	private static bool portalActive;

	private static bool holderActive;

	private static Transform portalGeo;

	private static Transform holderGeo;

	private static PortalAnimator portal;

	private static BallHolder holder;

	private static PrefabID ballPrefabID = new PrefabID(PrefabType.General, PrefabName.Ball);

	private static PrefabID portalPrefabID = new PrefabID(PrefabType.General, PrefabName.Portal);

	private static PrefabID holderPrefabID = new PrefabID(PrefabType.General, PrefabName.Holder);

	private static int? worldNumStored;

	private static bool fadeInWorld;

	public static float PlayTime;

	private static float preGameTimer;

	private static float gameOverTimer;

	private static bool gameOverTimeElapsed;

	private static bool endGame;

	private static ButtonUtils.Name gameOverButtonPressed;

	private static Vector3 currentBallMoveVector;

	private static Tile currentTile;

	private static int lastTileIndexY;

	private static int lastBallRowNum;

	private static Tile lastLeadTile;

	private static bool transitioningWorldNow;

	private static float transitioningWorldTimer;

	private static float transitioningWorldTime;

	private static bool onSafeTile;

	private static bool wasOnSafeTile;

	private static MathUtils.IntPair saveTileIndex;

	public static SmartBoard TileBoard = new SmartBoard();

	private static bool worldNeedsClearing;

	public static AspectRatio aspectRatio = new AspectRatio(0.5625f);

	private static bool targetFpsSet;

	private static bool animatePortals;

	private static int? livesLastGame;

	private static GameManager thisScript;

	private static bool reusingScene;

	private static bool isInitialized;

	private static bool isInitializedStatic;

	private static bool startRequested;

	private static bool resumeRequested;

	private static bool scoredLastTile;

	public static string BenchmarkingString { get; private set; }

	public static float BenchmarkingFloat { get; private set; }

	public static Ball theBall { get; private set; }

	public static int WorldNum
	{
		get
		{
			if (worldNumStored.HasValue)
			{
				return worldNumStored.Value;
			}
			if (DebugWorldNum.HasValue)
			{
				return DebugWorldNum.Value;
			}
			return 1;
		}
		private set
		{
			worldNumStored = value;
		}
	}

	public static int WorldIndex
	{
		get
		{
			return WorldNum - 1;
		}
	}

	public static int WorldEndLength { get; private set; }

	public static bool FirstAttempt { get; private set; }

	public static bool GameStarted { get; private set; }

	public static bool GameOver { get; private set; }

	public static bool GamePaused { get; private set; }

	public static bool PromptStart { get; private set; }

	public static bool PortalStart { get; private set; }

	public static bool PausedStart { get; private set; }

	public static bool IsRecording { get; private set; }

	public static Vector3 BallPosition
	{
		get
		{
			return Ball.Position;
		}
	}

	public static MathUtils.IntPair BallTilePosition
	{
		get
		{
			return Ball.TilePosition;
		}
	}

	public static int BallRowNum
	{
		get
		{
			return Ball.RowNum;
		}
	}

	public static int BallTileNum
	{
		get
		{
			return Ball.TileNum;
		}
	}

	public static float BallRowPercent
	{
		get
		{
			return Ball.RowPercent;
		}
	}

	public static float BallDistance
	{
		get
		{
			return Ball.Distance;
		}
	}

	public static float WorldCenter { get; private set; }

	public static int StartOffset { get; private set; }

	public static int GemsThisGame { get; private set; }

	public static int CrownsThisGame { get; private set; }

	// Native surplus reward: collecting another shield does not refill an active
	// one. The free-shield mechanics live in ClassicBoxShield, not scoring.
	// Surplus is persisted separately; inventory redemption is not yet ported.
	public static int ShieldThisGame { get; private set; }

	public void OnApplicationQuit()
	{
		SaveProgress();
	}

	public void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			startRequested = false;
			resumeRequested = false;
			TryPauseGame();
			SaveProgress();
		}
	}

	public static void SaveProgress()
	{
		PlayerPrefs.Save();
	}

	private void Start()
	{
		Initialize();
	}

	public static void PreStart()
	{
		if (!Application.isEditor)
		{
			DebugWorldNum = null;
			DebugStart = null;
			DebugSegmentType = null;
			DebugDifficulty = null;
			DebugSegmentNum = null;
			DebugQuality = null;
			Mute = false;
		}
		Randomizer.SeedRandom(RandomSeed);
	}

	private void Initialize()
	{
		ServerConfigs.Refresh();
		PreStart();
		ClassicBoxShield.Reset();
		ClassicGameplayClock.Reset();
		LevelLoader.Reset();
		GameStarted = false;
		startRequested = false;
		resumeRequested = false;
		TryClearWorld();
		Ball.ResetStoredPosition();
		RateGame.SetUrl("http://tiny.cc/eqmbtx");
		if (!isInitializedStatic)
		{
			Input.multiTouchEnabled = false;
			SetTargetFrameRate();
			Sounds = new Dictionary<AudioName, AudioClip>();
			Sounds.Add(AudioName.Jump, jumpSound);
			Sounds.Add(AudioName.Fragile, fragileSound);
			thisScript = base.gameObject.GetComponent<GameManager>();
			isInitializedStatic = true;
		}
		if (!reusingScene)
		{
			PortraitViewport viewport = PortraitViewport.Attach(Camera.main);
			if (viewport != null)
			{
				viewport.Pin(gameplayTopGroup, true);
				viewport.Pin(backButton, true);
				viewport.Pin(playButton.parent, false);
				viewport.Pin(resumeButton, false);
				viewport.Pin(prompt, false);
			}
			hudParts = new Dictionary<HudPart, Transform>();
			hudParts.Add(HudPart.GameplayTop, gameplayTopGroup);
			hudParts.Add(HudPart.GameOverGroup, gameOverGroup);
			hudParts.Add(HudPart.Prompt, prompt);
			hudParts.Add(HudPart.NextWorldGroup, nextWorldGroup);
			hudParts.Add(HudPart.GetLivesGroup, getLivesGroup);
			hudParts.Add(HudPart.PausedGroup, pausedGroup);
			hudParts.Add(HudPart.ScoreText, scoreText);
			hudParts.Add(HudPart.ScoreGroup, scoreGroup);
			hudParts.Add(HudPart.ProgressGroup, progressGroup);
			hudParts.Add(HudPart.ProgressOffsetGroup, progressOffsetGroup);
			hudParts.Add(HudPart.ProgressText, progressText);
			hudParts.Add(HudPart.ProgressTextShadow, progressTextShadow);
			hudParts.Add(HudPart.ProgressPercentSymbol, progressPercentSymbol);
			hudParts.Add(HudPart.ProgressPercentSymbolShadow, progressPercentSymbolShadow);
			hudParts.Add(HudPart.ProgressBar, progressBar);
			hudParts.Add(HudPart.GemsGroup, gemsGroup);
			hudParts.Add(HudPart.GemsText, gemsText);
			hudParts.Add(HudPart.GemsTextShadow, gemsTextShadow);
			hudParts.Add(HudPart.GemsTotalText, gemsTotalText);
			hudParts.Add(HudPart.GemsTotalTextShadow, gemsTotalTextShadow);
			hudParts.Add(HudPart.GemsIcon, gemsIcon);
			hudParts.Add(HudPart.GemsIconShadow, gemsIconShadow);
			hudParts.Add(HudPart.SavesBar, savesBar);
			hudParts.Add(HudPart.SavesText, savesText);
			hudParts.Add(HudPart.LivesText, livesText);
			hudParts.Add(HudPart.LivesTextShadow, livesTextShadow);
			hudParts.Add(HudPart.NextWorldLock, nextWorldLock);
			hudParts.Add(HudPart.NextWorldOverlay, nextWorldOverlay);
			hudParts.Add(HudPart.NextWorldTitleQuad, nextWorldTitleQuad);
			hudParts.Add(HudPart.CameraOverlay, cameraOverlay);
			hudParts.Add(HudPart.GameOverBackgroundOverlay, gameOverBackgroundOverlay);
			hudParts.Add(HudPart.BackButton, backButton);
			hudParts.Add(HudPart.PlayButton, playButton);
			hudParts.Add(HudPart.ResumeButton, resumeButton);
			hudParts.Add(HudPart.PauseButton, pauseButton);
			hudParts.Add(HudPart.NextButton, nextButton);
			hudParts.Add(HudPart.RecordButton, recordButton);
			hudParts.Add(HudPart.GetBallsButton, getBallsButton);
			hudParts.Add(HudPart.VideoButton, videoButton);
			hudParts.Add(HudPart.ShopButton, shopButton);
			hudParts.Add(HudPart.DebugText, debugText);
			hudParts.Add(HudPart.DebugButton, debugButton);
			hudParts.Add(HudPart.GetLivesTitle, getLivesTitle);
			hudParts.Add(HudPart.GetLivesTime, getLivesTime);
			hudParts.Add(HudPart.GetLivesTimeTitle, getLivesTimeTitle);
			hudParts.Add(HudPart.GetLivesClock, getLivesClock);
			hudParts.Add(HudPart.PausedTitle, pausedTitle);
			hudParts.Add(HudPart.Tutorial, tutorial);
			hudParts.Add(HudPart.TutorialTitle, tutorialTitle);
			hudParts.Add(HudPart.TutorialMessage, tutorialMessage);
			hudParts.Add(HudPart.TutorialButton, tutorialButton);
		}
		MusicPlayer.Initialize(0.3f);
		if (DebugWorldNum.HasValue)
		{
			WorldNum = DebugWorldNum.Value;
		}
		MusicPlayer.Prepare(WorldIndex);
		PromptStart = WorldNum == 1;
		PortalStart = !PromptStart;
		LevelDesigner.Reset();
		LevelLoader.Reset();
		MaterialManager.Initialize(LevelDesigner.GetStartingThemeNumber(WorldNum), hudParts[HudPart.ScoreText]);
		BufferManager.Initialize();
		WorldCenter = 2.5f;
		theBall = BufferManager.GetGeo(PrefabType.General, PrefabName.Ball).GetComponent<Ball>();
		ExpansionBallAssets.Initialize(theBall);
		float num = ((!DebugStart.HasValue) ? 2f : 0f);
		theBall.Initialize(2.5f, num);
		ballActive = true;
		lastBallRowNum = BallRowNum;
		CrownsThisGame = 0;
		ShieldThisGame = 0;
		// Reset builds tiles and assigns the ten native candidate indices, so the
		// run's pool must exist before it, not when the player starts rolling.
		ClassicCrownRandom.StartRun();
		TileBoard.Reset();
		animatePortals = DeviceQualityChecker.QualityIsHigh();
		if (PortalStart)
		{
			CreatePortal(false, (int)num);
			theBall.Hide();
		}
		GameInput.Initialize();
		if (reusingScene)
		{
			GameHud.HideHud();
		}
		else
		{
			GameHud.Initialize(hudParts, theBall);
		}
		GameHud.OnSceneStart();
		CameraControl.Initialize(PortalStart);
		MidgroundManager.InitializeForWorld(WorldIndex);
		FinishManager.Initialize(2.5f);
		ParticleManager.Initialize(theBall.transform);
		VideoSharingManager.Initialize();
		VideoRewardsManager.Initialize();
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		PlayTime = 0f;
		GemsThisGame = 0;
		GameOver = false;
		GamePaused = false;
		endGame = false;
		gameOverTimeElapsed = false;
		wasOnSafeTile = false;
		transitioningWorldNow = false;
		reusingScene = false;
		isInitialized = true;
		if (DebugStart.HasValue)
		{
			StartOffset = DebugStart.Value - LevelLoader.ChosenSegmentStart;
		}
		else
		{
			StartOffset = 0;
		}
		PausedStart = false;
		if (LivesManager.OutOfLives)
		{
			GameHud.ShowGetLives();
			PausedStart = true;
		}
		else
		{
			int tutorialNumber = PlayerProfiler.TutorialNumber;
			if (tutorialNumber < 2)
			{
				int num2 = tutorialNumber + 1;
				int gameNumber = PlayerProfiler.GameNumber;
				int progressThisSession = ProgressManager.GetProgressThisSession();
				bool flag = false;
				bool flag2 = false;
				switch (num2)
				{
				case 1:
					flag = gameNumber >= GamesBeforeFirstTutorial;
					break;
				case 2:
					flag = gameNumber >= GamesBeforeSecondTutorial;
					flag2 = progressThisSession >= CompetantPercentage;
					break;
				default:
					Debug.LogError(string.Format("GameManager: Error: Attempt to show tutorials number {0}, which is not yet setup", num2));
					break;
				}
				if (flag)
				{
					string text = ((!flag2) ? "Showing" : "Skipping");
					int num3 = ((num2 != 1) ? GamesBeforeSecondTutorial : GamesBeforeFirstTutorial);
					Debug.Log(string.Format("GameManager: Debug: {0} tutorial number {1} now, after {2} previous games (threshold: {3}, highest progress this session is {4}%)", text, num2, gameNumber, num3, progressThisSession));
					PlayerProfiler.OnTutorial();
					if (!flag2)
					{
						GameHud.ShowTutorial();
						PausedStart = true;
					}
				}
			}
		}
		if (!PausedStart && PromptStart)
		{
			GameHud.ShowPrompt();
		}
	}

	public static void Restart(bool reloadScene)
	{
		isInitialized = false;
		if (PausedStart || !reloadScene)
		{
			Debug.Log("GMMN: DBEUG: Resetting using Reuse method");
			reusingScene = true;
			thisScript.Initialize();
		}
		else
		{
			Debug.Log("GMMN: DBEUG: Resetting using Restart method");
			reusingScene = false;
			Application.LoadLevel(1);
		}
	}

	private static void TryStartGame()
	{
		if (!GameStarted)
		{
			StartGame();
		}
	}

	private static void StartGame()
	{
		if (GameStarted) return;
		float? startAtSeconds = null;
		if (DebugStart.HasValue)
		{
			startAtSeconds = LevelDesigner.TimeOf(StartOffset);
			float num = startAtSeconds.Value / 46.67f * 9.75f;
			startAtSeconds -= num;
		}
		// Start/seeking is asynchronous for browser media. Wait for actual playback,
		// not just metadata/canplay, before moving the ball or advancing its clock.
		startRequested = !MusicPlayer.BeginGameplay(WorldIndex, startAtSeconds);
		if (startRequested) return;
		theBall.StartRolling();
		CameraControl.StartCamera();
		GameHud.OnGameplayStart();
		PlayerProfiler.OnGameplayStart();
		if (portal != null)
		{
			portal.Deactivate();
		}
		if (IsRecording)
		{
			VideoSharingManager.StartRecording();
			PlayerProfiler.OnRecordingStart();
		}
		PlayTime = 0f;
		GameStarted = true;
		if (PortalStart)
		{
			theBall.Jump(null, true, false);
			ParticleManager.Detonate(PrefabName.PortalStart);
			if (holderActive)
			{
				holder.TakeBall();
			}
		}
	}

	private void Update()
	{
		ClassicGameplayClock.UpdateFrame();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			bool flag = false;
			if (GameStarted)
			{
				if (GameOver)
				{
					flag = true;
				}
				else if (GamePaused)
				{
					UnpauseGame();
				}
				else
				{
					PauseGame();
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				gameOverButtonPressed = ButtonUtils.Name.Back;
				EndGame(false);
			}
		}
		if (!isInitialized)
		{
			return;
		}
		// Keep the preparation message current while waiting for the first press,
		// even when leaving the menu canceled its pending playback request.
		if (!GameStarted && !startRequested) MusicPlayer.Prepare(WorldIndex);
		GameHud.UpdateHud();
		GameInput.UpdateInputs();
		if (endGame)
		{
			startRequested = false;
			resumeRequested = false;
			EndGame(false);
		}
		if (startRequested) StartGame();
		if (resumeRequested) UnpauseGame();
		if (GamePaused)
		{
			return;
		}
		// A network stall must not let the level run ahead of its soundtrack.
		if (GameStarted && !GameOver && !MusicPlayer.PlaybackReady(WorldIndex)) return;
		UpdateMovement();
		if (GameStarted)
		{
			if (GameOver)
			{
				UpdateGameOver();
			}
			else
			{
				UpdateCurrentTile();
				ClassicBoxShield.UpdateFrame();
				UpdateCollisions();
				UpdateWorldTransition();
				UpdateVelocity();
			}
		}
		MidgroundManager.UpdateMidground();
		TileAnimator.UpdateAnimation();
		CameraControl.UpdateCamera();
		FinishManager.UpdateGoals();
		if (portalActive)
		{
			portal.UpdatePortal();
		}
		if (holderActive)
		{
			holder.UpdateHolder();
		}
	}

	private static void UpdateMovement()
	{
		if (GameStarted)
		{
			PlayTime += Time.deltaTime;
		}
		if (ballActive)
		{
			theBall.UpdateBall();
		}
	}

	private static void UpdateGameOver()
	{
		if (!gameOverTimeElapsed)
		{
			gameOverTimer += Time.deltaTime;
			if (gameOverTimer > 1.5f)
			{
				gameOverTimer = 0f;
				gameOverTimeElapsed = true;
				OnGameOver();
			}
		}
	}

	private static void UpdateCurrentTile()
	{
		currentTile = null;
		int theme = LevelDesigner.CurrentDonorThemeNumber;
		bool movingBackward = ExpansionGameplay.Classic1782 && BallRowNum < lastBallRowNum &&
			((theme >= 45 && theme <= 49) || theme == 52 || theme == 53);
		if (BallRowNum != lastBallRowNum && !movingBackward)
		{
			lastBallRowNum = BallRowNum;
			OnMovedToNextTile();
		}
		if (IsValidMapIndex(BallTilePosition.y, BallTilePosition.x))
		{
			currentTile = TileBoard[BallTilePosition.y, BallTilePosition.x];
			if (ExpansionGameplay.Classic1782 && currentTile != null && currentTile.IsHidden) currentTile = null;
		}
		else
		{
			currentTile = null;
		}
	}

	private static void UpdateCollisions()
	{
		bool isEditor = Application.isEditor;
		Tile.DangerType dangerTypeUsed;
		if (currentTile != null && currentTile.CanKillBall() && currentTile.ShouldKillBall(theBall.IsJumping, BallPosition, out dangerTypeUsed))
		{
			KillBall(dangerTypeUsed);
		}
		if (ExpansionGameplay.Enabled && currentTile != null)
		{
			if (currentTile.type == Tile.Type.LaserTrigger && !currentTile.isTrigger)
			{
				currentTile.HidenMatchTriggerLaser();
				currentTile.SwitchAddition();
				if (currentTile.AudioPlayer != null) currentTile.PlaySound();
			}
			else if (currentTile.type == Tile.Type.Bean && !currentTile.isEat)
			{
				currentTile.EatBean();
			}
		}
		if (theBall.CanFall(currentTile))
		{
			FallToDeath();
		}
	}

	private static void UpdateVelocity()
	{
		if (LevelDesigner.VelocityIsNotMaxed)
		{
			theBall.SetVelocity(LevelDesigner.GetVelocity(BallRowNum));
		}
	}

	public static void OnPress()
	{
		if (GameStarted)
		{
			endGame = GameHud.CheckButtons(out gameOverButtonPressed);
			return;
		}
		if (PausedStart)
		{
			endGame = GameHud.CheckButtons(out gameOverButtonPressed);
			return;
		}
		endGame = GameHud.CheckButtons(out gameOverButtonPressed);
		if (gameOverButtonPressed == ButtonUtils.Name.Null)
		{
			StartGame();
		}
	}

	public static void OnHolding()
	{
	}

	public static void OnHold()
	{
	}

	public static void OnDepress()
	{
	}

	public static void OnDrag()
	{
	}

	public static void OnInput(float worldPositionPressed)
	{
		if (GameStarted && !GameOver)
		{
			theBall.SetTargetHorizontalWorldXPos(worldPositionPressed);
		}
	}

	public static void TryPauseGame()
	{
		if (GameStarted && !GameOver)
		{
			PauseGame();
		}
	}

	public static void PauseGame()
	{
		resumeRequested = false;
		GamePaused = true;
		GameHud.ShowPause();
		MusicPlayer.Pause();
		if (VideoSharingManager.IsRecording)
		{
			VideoSharingManager.PauseRecording();
		}
	}

	public static void UnpauseGame()
	{
		resumeRequested = !MusicPlayer.ResumeGameplay(WorldIndex);
		if (resumeRequested) return;
		GamePaused = false;
		GameHud.HidePause();
		if (VideoSharingManager.IsRecordingPaused)
		{
			VideoSharingManager.ResumeRecording();
		}
	}

	private static void StartWorldTransition(float transitionTime)
	{
		transitioningWorldTime = transitionTime;
		transitioningWorldTimer = transitionTime;
		transitioningWorldNow = true;
	}

	private static void UpdateWorldTransition()
	{
		if (transitioningWorldNow)
		{
			transitioningWorldTimer -= Time.smoothDeltaTime;
			if (transitioningWorldTimer <= 0f)
			{
				EndWorldTransition();
				return;
			}
			float num = 1f - transitioningWorldTimer / transitioningWorldTime;
			float num2 = FloatAnim.Smooth(num);
			MaterialManager.Recolor(num);
		}
	}

	private static void EndWorldTransition()
	{
		MaterialManager.EndRecolor();
		transitioningWorldNow = false;
		MidgroundManager.StartGeneratingFor(LevelDesigner.CurrentWorldThemeIndex);
	}

	private static bool NotOnSaveTile()
	{
		return !OnSaveTile();
	}

	private static bool OnSaveTile()
	{
		bool flag;
		if (onSafeTile)
		{
			MathUtils.IntPair ballTilePosition = BallTilePosition;
			flag = ballTilePosition.y == saveTileIndex.y && ballTilePosition.x == saveTileIndex.x;
			if (!flag)
			{
				onSafeTile = false;
				wasOnSafeTile = true;
				flag = false;
			}
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	private static bool JustLeftSaveTile()
	{
		bool result;
		if (wasOnSafeTile)
		{
			result = BallTilePosition.y - saveTileIndex.y < 3;
			wasOnSafeTile = false;
		}
		else
		{
			result = false;
		}
		return result;
	}

	public static int GetDistanceToNextSafeTile()
	{
		int? num = null;
		int num2 = 3;
		bool flag = false;
		do
		{
			bool flag2 = true;
			int rowIndex = BallRowNum + num2;
			for (int i = 0; i < TileBoard.CurrentWidth; i++)
			{
				Tile tile = TileBoard[rowIndex, i];
				if (!(tile != null))
				{
					continue;
				}
				if (tile.IsSafe())
				{
					if (!num.HasValue)
					{
						num = num2;
					}
					flag = true;
				}
				else if (tile.IsMaybeSafe())
				{
					if (!num.HasValue)
					{
						num = num2;
					}
					flag2 = false;
				}
			}
			if (flag)
			{
				break;
			}
			if (flag2)
			{
				num = null;
			}
			num2++;
			if (num2 > 20)
			{
				Debug.LogWarning(string.Format("GMMN: ERROR: GameManager.GetDistanceToNextSafeTile() was unable to find any safe tiles within the maximum distance of {0} tiles - default to normal jumping distance of {1} tiles.  Sorry :S", 20, 4));
				flag = true;
				if (!num.HasValue)
				{
					num = 4;
				}
				break;
			}
		}
		while (!flag);
		if (num.HasValue)
		{
			return num.Value;
		}
		Debug.LogError("GMMN: ERROR: Somehow GameManager.GetDistanceToNextSafeTile() was unable to find a safeTileDistance, even though flagged foundSafeTile?!  Check logic");
		return 4;
	}

	public static void SlowMo()
	{
		theBall.SlowMo();
	}

	public static void KillBall(Tile.DangerType killingDangerType)
	{
		if (ClassicBoxShield.TryInterceptDeath()) return;
		theBall.Destroy(killingDangerType);
		OnDeath();
	}

	public static void FallToDeath()
	{
		theBall.Fall();
		OnDeath();
		theBall.StartFakingWorldAnimations();
	}

	private static void OnDeath()
	{
		GameHud.OnGameplayEnd();
		FinishManager.OnDeath();
		PlayerProfiler.OnDeath();
		OnGameplayEnd();
		if (PlayerProfiler.HaveNotPurchasedPremium)
		{
			LivesManager.TakeLife();
			if (LivesManager.OutOfLives)
			{
				LivesManager.StartLivesRecharging();
			}
		}
		GameOver = true;
		worldNeedsClearing = true;
		MusicPlayer.Stop();
	}

	public static void OnWorldEnd()
	{
		GameOver = true;
		OnGameplayEnd();
		AnalyticsLogEvent.OnWorldComplete(WorldIndex);
		AchievementManager.UnlockWorldCompletedAchievement(WorldIndex);
		if (ProgressManager.GetLastGems() >= LevelCatalog.Get(WorldNum).MaxGems)
		{
			AchievementManager.WorldCompletedPerfectGemsAchievement(WorldIndex);
		}
		gameOverTimeElapsed = true;
		OnGameOver();
	}

	public static void OnGameplayEnd()
	{
		int lastProgress = ProgressManager.GetLastProgress();
		int lastGems = ProgressManager.GetLastGems();
		if (VideoSharingManager.IsRecording)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string worldName = LevelDesigner.WorldName;
			bool flag = ProgressManager.HighscoreWasJustBeaten();
			dictionary.Add("World", worldName);
			dictionary.Add("Progress", lastProgress);
			dictionary.Add("Highscore", flag);
			string text = string.Format("{0}% in World {1}", lastProgress, worldName);
			text = ((!flag) ? ("I scored " + text) : ("New Highscore of " + text));
			if (lastGems > 0)
			{
				dictionary.Add("Gems", lastGems);
				switch (lastGems)
				{
				case 1:
					text = string.Format("{0}, with 1 gem", text);
					break;
				case 20:
					text = string.Format("{0}, with ALL gems!", text);
					break;
				case 19:
					text = string.Format("{0}, missing only 1 gem!", text, lastGems);
					break;
				default:
					text = string.Format("{0}, with {1} gems", text, lastGems);
					break;
				}
			}
			text = text + " #" + "RollingSky";
			dictionary.Add("Title", text);
			VideoSharingManager.SetMetaData(dictionary);
		}
		SaveProgress();
		PlayerProfiler.OnGameplayEnd();
		AnalyticsLogEvent.OnGameplayEnd(WorldIndex, lastProgress, SegmentTracker.CurrentSegmentID.ToString());
		AchievementManager.ReportWorldScore(WorldIndex, lastProgress);
	}

	private static void OnGameOver()
	{
		if (VideoSharingManager.IsRecording)
		{
			VideoSharingManager.StopRecording();
		}
		ClassicBoxShield.Reset();
		GameHud.ShowHud();
	}

	private static void OnMovedToNextTile()
	{
		if (scoredLastTile)
		{
			scoredLastTile = false;
		}
		else
		{
			scoredLastTile = true;
		}
		BufferManager.DisplayRow();
		TileBoard.UpdateCurrentSegment(BallRowNum);
		if (LevelDesigner.ShouldRecolorWorld(BallRowNum))
		{
			MaterialManager.StartRecolorTo(LevelDesigner.CurrentWorldThemeIndex);
			EndWorldTransition();
		}
		SegmentTracker.OnMoveForward();
		theBall.Update1782Floor();
	}

	public static bool TileIsSolid(int rowIndex, int tileIndex)
	{
		return TileIsNotNull(rowIndex, tileIndex) && TileBoard[rowIndex, tileIndex].IsNotAirbourne && TileBoard[rowIndex, tileIndex].IsNotFallen;
	}

	private static bool TileIsNotNull(int rowIndex, int tileIndex)
	{
		return TileBoard.IsNotNull(rowIndex, tileIndex);
	}

	private static bool IsValidMapIndex(int rowIndex, int tileIndex)
	{
		return TileBoard.IsValid(rowIndex, tileIndex);
	}

	public static void ApplyScoreFor(Tile.PickupType pickupType, int crownIndex)
	{
		// The accepted first eight retain their original pickup/scoring path.
		if (!ExpansionGameplay.Enabled || (pickupType != Tile.PickupType.Star &&
			pickupType != Tile.PickupType.StarFloater && pickupType != Tile.PickupType.StarSlider))
		{
			ApplyScoreFor(pickupType);
			return;
		}
		ClassicCrownRandom.CrownType reward = ClassicCrownRandom.Enabled
			? ClassicCrownRandom.GetCrownType(crownIndex) : ClassicCrownRandom.CrownType.CROWN;
		if (reward == ClassicCrownRandom.CrownType.NOTHING) return;
		if (reward == ClassicCrownRandom.CrownType.BOX)
		{
			ParticleManager.Detonate(PrefabName.Particles_PickupBox);
			if (UserSettings.IsSoundOn() && !DisableSounds) theBall.BoxSound.Play();
			if (ClassicCrownRandom.GetRewardType() == ClassicCrownRandom.RewardType.SHIELD)
			{
				if (!ClassicBoxShield.GrantBoxShield())
				{
					ShieldThisGame++;
					ProgressManager.RecordSurplusBoxShield();
				}
			}
			// BALL adds no value in this unlimited-balls port; neither reward is a gem.
			return;
		}
		CrownsThisGame++;
		GameHud.DisplayCrowns(CrownsThisGame);
		ParticleManager.Detonate(ClassicCrownRandom.Enabled ? PrefabName.Particles_PickupCrown : PrefabName.PickupBig);
		if (UserSettings.IsSoundOn() && !DisableSounds) theBall.CrownSound.Play();
	}

	public static void ApplyScoreFor(Tile.PickupType pickupType)
	{
		GemsManager.AddGem();
		GemsThisGame++;
		int num = 1;
		ScoreManager.AddVita(num);
		ParticleManager.Detonate(PrefabName.PickupBig);
		if (UserSettings.IsSoundOn() && !DisableSounds)
		{
			theBall.PickupSound.Play();
		}
		ShowScoreFor(num);
	}

	public static void ShowScoreFor(int value)
	{
		PrefabID prefabID = new PrefabID(PrefabType.Pickup, PrefabName.PickupText);
		Transform geo = BufferManager.GetGeo(prefabID);
		CollectionTextAnimator collectionTextAnimator = geo.GetComponent<CollectionTextAnimator>();
		if (collectionTextAnimator == null)
		{
			collectionTextAnimator = geo.gameObject.AddComponent<CollectionTextAnimator>();
		}
		collectionTextAnimator.Initialize(theBall.transform, prefabID, value);
	}

	private static bool TileIsType(Tile.Type queryType, int rowPos, int tilePos)
	{
		return TileBoard[rowPos, tilePos] != null && TileBoard[rowPos, tilePos].IsType(Tile.Type.Ground);
	}

	public static string IntToChar(int integer)
	{
		if (integer < 0)
		{
			return "MIN";
		}
		if (integer >= "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length)
		{
			return "MAX";
		}
		return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(integer, 1);
	}

	public static void SetWorldNum(int newWorldNum, bool fadeInNextWorld = true)
	{
		WorldNum = ValidatedWorldNum(newWorldNum);
		fadeInWorld = fadeInNextWorld;
	}

	public static bool QueryAndResetFadeInWorld()
	{
		bool result;
		if (fadeInWorld)
		{
			result = true;
			fadeInWorld = false;
		}
		else
		{
			result = false;
		}
		return result;
	}

	public static void SetWorldEnd(int worldEndLength, int worldLength)
	{
		WorldEndLength = worldEndLength;
		CreatePortal(true, worldLength - 1);
	}

	private static void CreatePortal(bool endingPortal, int portalRow)
	{
		if (portalGeo == null)
		{
			portalGeo = BufferManager.GetGeo(PrefabType.General, PrefabName.Portal);
		}
		portalGeo.position = new Vector3(2.5f, portalRow, 0f);
		if (portal == null)
		{
			portal = portalGeo.gameObject.GetComponent<PortalAnimator>();
		}
		portal.Initialize(endingPortal, animatePortals);
		if (!endingPortal && LivesManager.HasALife)
		{
			portal.TryActivate();
		}
		portalActive = true;
	}

	private static void CreateHolder(int holderRow)
	{
		if (holderGeo == null)
		{
			holderGeo = BufferManager.GetGeo(PrefabType.General, PrefabName.Holder);
		}
		holderGeo.position = new Vector3(2.5f, holderRow, 0f);
		if (holder == null)
		{
			holder = holderGeo.gameObject.GetComponent<BallHolder>();
		}
		holder.Initialize(LivesManager.LivesLeft);
		holderActive = true;
	}

	public static void ClosePortal()
	{
	}

	public static void StartEndingWorld()
	{
		theBall.StartWorldEnd(WorldEndLength);
		GameHud.OnGameplayEnd();
	}

	public static void EndGame(bool moveToNextWorld)
	{
		startRequested = false;
		resumeRequested = false;
		ClassicBoxShield.Reset();
		worldNeedsClearing = true;
		TileAnimator.Clear();
		CameraControl.Clear();
		BufferManager.Reclaim(false);
		ButtonUtils.Name name = gameOverButtonPressed;
		if (name == ButtonUtils.Name.Back)
		{
			Application.LoadLevel(0);
			return;
		}
		if (moveToNextWorld)
		{
			WorldNum = GetNextWorldNum();
		}
		Restart(moveToNextWorld);
	}

	public static void TryClearWorld()
	{
		if (worldNeedsClearing)
		{
			ClearWorld();
			ClearCores();
		}
	}

	public static void ClearWorld()
	{
		MidgroundManager.TryClear();
		FinishManager.Clear();
		TileAnimator.Clear();
		BufferManager.Reclaim(true);
		GC.Collect();
		worldNeedsClearing = false;
	}

	public static void ClearCores()
	{
		if (theBall != null)
		{
			TransformUtils.Hide(theBall.transform);
			BufferManager.GiveGeo(theBall.transform, ballPrefabID);
			ballActive = false;
		}
		if (portalGeo != null)
		{
			TransformUtils.Hide(portalGeo);
			BufferManager.GiveGeo(portalGeo, portalPrefabID);
			portalActive = false;
		}
	}

	private static int ValidatedWorldNum(int newWorldNum)
	{
		if (newWorldNum < 1 || newWorldNum > TotalWorlds)
		{
			return 1;
		}
		return newWorldNum;
	}

	public static int GetNextWorldNum()
	{
		return LevelCatalog.NextWorld(WorldNum);
	}

	public static int GetNextWorldIndex()
	{
		return GetNextWorldNum() - 1;
	}

	public static void SetTargetFrameRate()
	{
		if (!targetFpsSet)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			Application.targetFrameRate = -1;
#else
			if (DeviceQualityChecker.QualityIsPour())
			{
				Application.targetFrameRate = 60;
			}
			else
			{
				Application.targetFrameRate = 60;
			}
#endif
			targetFpsSet = true;
		}
	}

	public static void SetRecording(bool on)
	{
		IsRecording = on;
		Debug.Log("Settings GameManager.IsRecording to: " + on);
	}

	public static bool HasInternet()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}

	public static void RefreshServerParameters()
	{
		StartingLives = ServerConfigs.GetIntFromPlayerPrefs(StartingLives, "LivesStarting");
		LivesPerVideo = ServerConfigs.GetIntFromPlayerPrefs(LivesPerVideo, "LivesPerVideo");
		LivesPerRecharge = ServerConfigs.GetIntFromPlayerPrefs(LivesPerRecharge, "LivesPerRecharge");
		SecondsPerRecharge = ServerConfigs.GetFloatFromPlayerPrefs(SecondsPerRecharge, "SecondsPerRecharge");
		SecondsPerRechargeShort = ServerConfigs.GetFloatFromPlayerPrefs(SecondsPerRechargeShort, "SecondsPerRechargeNoVideo");
		GamesBeforeFirstTutorial = ServerConfigs.GetIntFromPlayerPrefs(GamesBeforeFirstTutorial, "GamesBeforeFirstTutorial");
		GamesBeforeSecondTutorial = ServerConfigs.GetIntFromPlayerPrefs(GamesBeforeSecondTutorial, "GamesBeforeSecondTutorial");
		CompetantPercentage = ServerConfigs.GetIntFromPlayerPrefs(CompetantPercentage, "CompetantPercentage");
		InterstitialsEnabled = ServerConfigs.GetBoolFromPlayerPrefs(InterstitialsEnabled, "Interstitials_Enabled");
		InterstitialLivesNum = ServerConfigs.GetIntFromPlayerPrefs(InterstitialLivesNum, "Interstitials_LivesNum");
		VideoRewardsManager.videosEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.videosEnabled, "VideoRewards_VideosEnabled");
		VideoRewardsManager.imagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.imagesEnabled, "VideoRewards_ImagesEnabled");
		VideoRewardsManager.unityAdsVideosEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.unityAdsVideosEnabled, "VideoRewards_UnityAds_VideosEnabled");
		VideoRewardsManager.unityAdsImagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.unityAdsImagesEnabled, "VideoRewards_UnityAds_ImagesEnabled");
		VideoRewardsManager.adColonyVideosEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.adColonyVideosEnabled, "VideoRewards_AdColony_VideosEnabled");
		VideoRewardsManager.adColonyImagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.adColonyImagesEnabled, "VideoRewards_AdColony_ImagesEnabled");
		VideoRewardsManager.appLovinVideosEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.appLovinVideosEnabled, "VideoRewards_AppLovin_VideosEnabled");
		VideoRewardsManager.appLovinImagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.appLovinImagesEnabled, "VideoRewards_AppLovin_ImagesEnabled");
		VideoRewardsManager.chartboostVideosEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.chartboostVideosEnabled, "VideoRewards_Chartboost_VideosEnabled");
		VideoRewardsManager.chartboostImagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.chartboostImagesEnabled, "VideoRewards_Chartboost_ImagesEnabled");
		VideoRewardsManager.unityAdsPriority = ServerConfigs.GetIntFromPlayerPrefs(VideoRewardsManager.unityAdsPriority, "VideoRewards_UnityAds_Priority");
		VideoRewardsManager.adColonyPriority = ServerConfigs.GetIntFromPlayerPrefs(VideoRewardsManager.adColonyPriority, "VideoRewards_AdColony_Priority");
		VideoRewardsManager.appLovinPriority = ServerConfigs.GetIntFromPlayerPrefs(VideoRewardsManager.appLovinPriority, "VideoRewards_AppLovin_Priority");
		VideoRewardsManager.chartboostPriority = ServerConfigs.GetIntFromPlayerPrefs(VideoRewardsManager.chartboostPriority, "VideoRewards_Chartboost_Priority");
		VideoRewardsManager.admobImagesEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoRewardsManager.admobImagesEnabled, "VideoRewards_Admob_ImagesEnabled");
		VideoRewardsManager.admobPriority = ServerConfigs.GetIntFromPlayerPrefs(VideoRewardsManager.admobPriority, "VideoRewards_Admob_Priority");
		VideoSharingManager.videoSharingEnabled = ServerConfigs.GetBoolFromPlayerPrefs(VideoSharingManager.videoSharingEnabled, "VideoSharing_Enabled");
		DeviceQualityChecker.androidHighQualityMemoryMinimum = ServerConfigs.GetIntFromPlayerPrefs(DeviceQualityChecker.androidHighQualityMemoryMinimum, "Android_HighQualityMemoryMinimum");
		VideoRewardsManager.OnRefreshServerParameters();
	}
}
