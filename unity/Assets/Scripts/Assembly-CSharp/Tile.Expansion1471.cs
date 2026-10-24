using UnityEngine;

public partial class Tile
{
	public bool isTrigger;
	public bool isEat;
	public int CrownIndex;
	public bool IsStarJumpSlider;
	public bool IsNeedRotation;
	public float? yAngle;
	public bool yAngleIsInit;
	public bool isBroken;
	public Transform[] extendPartices;
	private bool rotationParentApplied;
	private static int rotationSignCounter;
	private static int NextExpansionRotationSign() { return ++rotationSignCounter % 2 == 0 ? 1 : -1; }

	private void ResetExpansionVariables()
	{
		isTrigger = isEat = IsStarJumpSlider = IsNeedRotation = rotationParentApplied = yAngleIsInit = isBroken = false;
		IsSuccessiveJump = IsSimulationJump = false;
		CrownIndex = 0;
		yAngle = null;
		extendPartices = null;
	}

	private void ConfigureRotation()
	{
		IsNeedRotation = animType == AnimType.Rotation || animType == AnimType.EnemyJumpFromL1 || animType == AnimType.EnemyJumpFromR1 ||
			animType == AnimType.LeaningGeneralCube3 || animType == AnimType.EnemyLeaningRiser || animType == AnimType.EnemyLeaningTower;
	}

	public void ApplyRotationParent()
	{
		if (IsNeedRotation && IsAnimChild && !rotationParentApplied)
		{
			GameManager.TileBoard[SegmentIndex, RowIndex, AnimParentTileNum].ReceiveChild(Index);
			rotationParentApplied = true;
		}
	}

	private void ConfigureExpansionTrigger()
	{
		if (ConfigureLaterBean()) return;
		HasAddition = true;
		HasAudio = true;
		HasAltAudio = false;
		IsAdditionSwitchable = true;
		switch ((TileVal)TypeVal)
		{
		case TileVal.TriggerRedLaser: AdditionPrefabID = new PrefabID(PrefabType.LaserTrigger, PrefabName.TrrigerRedLaser); break;
		case TileVal.TriggerGreenLaser: AdditionPrefabID = new PrefabID(PrefabType.LaserTrigger, PrefabName.TrrigerGreenLaser); break;
		case TileVal.EnemySmallBean: AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.EnemySmallBean); break;
		case TileVal.EnemyBigBean: AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.EnemyBigBean); animType = AnimType.BigBean; break;
		}
	}

	// Every entry below is ConfigureEnemy's native prefab/animation/audio triple;
	// non-default footprints and geometry flags follow the table.
	private bool ConfigureExpansionEnemy()
	{
		if (!ExpansionGameplay.Enabled) return false;
		switch (dangerType)
		{
		case DangerType.EnemyRedLaserH: ExpansionRig(PrefabName.EnemyRedLaserH, AnimType.EnemyRedLaserH, false); break;
		case DangerType.EnemyRedLaserV: ExpansionRig(PrefabName.EnemyRedLaserV, AnimType.EnemyLaser, false); break;
		case DangerType.EnemyGreenLaserH: ExpansionRig(PrefabName.EnemyGreenLaserH, AnimType.EnemyGreenLaserH, false); break;
		case DangerType.EnemyGreenLaserV: ExpansionRig(PrefabName.EnemyGreenLaserV, AnimType.EnemyLaser, false); break;
		case DangerType.EnemyFloaterAirFallGround: ExpansionRig(PrefabName.EnemyFloaterAirFallGround, AnimType.EnemyFloaterAirFallGround, false); IsAirbourne = IsGeoHidden = true; break;
		case DangerType.EnemyRotateFourBlock: ExpansionRig(PrefabName.EnemyRotateFourBlock, AnimType.EnemyRotateFourBlock, false); ExpansionSpread(DangerSpreadType.Corners); break;
		case DangerType.EnemyLv9TakeBack: ExpansionRig(PrefabName.EnemyLv9TakeBack, AnimType.EnemyLv9TakeBack, false); break;
		case DangerType.EnemeyLaserBlock: ExpansionRig(PrefabName.EnemeyLaserBlock, AnimType.EnemeyLaserBlock, false); break;
		case DangerType.EnemyRetroSnaker01: ExpansionRig(PrefabName.EnemyRetroSnaker01, AnimType.EnemyRetroSnaker01, true); break;
		case DangerType.EnemyRetroSnaker02: ExpansionRig(PrefabName.EnemyRetroSnaker02, AnimType.EnemyRetroSnaker02, true); break;
		case DangerType.EnemySnakerWallRight: ExpansionRig(PrefabName.EnemySnakerWallRight, AnimType.EnemySnakerWallRight, true); break;
		case DangerType.EnemySnakerWallLeft: ExpansionRig(PrefabName.EnemySnakerWallLeft, AnimType.EnemySnakerWallLeft, true); break;
		case DangerType.EnemySnakerFrontMove: ExpansionRig(PrefabName.EnemySnakerFrontMove, AnimType.EnemySnakerFrontMove, true); break;
		case DangerType.EnemySnakerCoiled01: ExpansionRig(PrefabName.EnemySnakerCoiled01, AnimType.EnemySnakerCoiled01, true); break;
		case DangerType.EnemySnakerLeftMove: ExpansionRig(PrefabName.EnemySnakerLeftMove, AnimType.EnemySnakerLeftMove, true); break;
		case DangerType.EnemySnakerRightMove: ExpansionRig(PrefabName.EnemySnakerRightMove, AnimType.EnemySnakerRightMove, true); break;
		case DangerType.EnemyVerticalCannon: ExpansionRig(PrefabName.EnemyVerticalCannon, AnimType.EnemyVerticalCannon, false); break;
		case DangerType.RoadUp: ExpansionRig(PrefabName.RoadUp, AnimType.RoadUp, false); IsGeoHidden = true; break;
		case DangerType.EnemyJumpFromL1: ExpansionRig(PrefabName.EnemyJumpLtoR1, AnimType.EnemyJumpFromL1, false); break;
		case DangerType.EnemyJumpFromR1: ExpansionRig(PrefabName.EnemyJumpRtoL1, AnimType.EnemyJumpFromR1, false); break;
		case DangerType.EnemyJumpFromL2: ExpansionRig(PrefabName.EnemyJumpFromL2, AnimType.EnemyJumpFromL2, false); IsDangerSpreader = true; break;
		case DangerType.EnemyJumpFromR2: ExpansionRig(PrefabName.EnemyJumpFromR2, AnimType.EnemyJumpFromR2, false); break;
		case DangerType.EnemyLeaningGeneralCube3: ExpansionRig(PrefabName.Lv10PillarLaser, AnimType.LeaningGeneralCube3, true); IsAdditionSwitchable = false; break;
		case DangerType.EnemyLeaningRiser: ExpansionRig(PrefabName.EnemyLeaningRiser, AnimType.EnemyLeaningRiser, false); break;
		case DangerType.EnemyLeaningTower: ExpansionRig(PrefabName.EnemyLeaningTower, AnimType.EnemyLeaningTower, false); break;
		case DangerType.EnemyTower: ExpansionRig(PrefabName.EnemyTower, AnimType.EnemyTower, true); break;
		case DangerType.EnemyCollapseTower: ExpansionRig(PrefabName.EnemyCollapseTower, AnimType.EnemyCollapseTower, false); break;
		case DangerType.EnemyMiracle: ExpansionRig(PrefabName.EnemyMiracle, AnimType.EnemyMiracle, false); break;
		case DangerType.EnemyFallHammerL: ExpansionRig(PrefabName.EnemyFallHammerL, AnimType.EnemyFallHammerL, true); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyFallHammerR: ExpansionRig(PrefabName.EnemyFallHammerR, AnimType.EnemyFallHammerR, true); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyTombstone: ExpansionRig(Randomizer.FlipACoin() ? PrefabName.EnemyTombstone1 : PrefabName.EnemyTombstone2, AnimType.EnemyTombstone, false); break;
		case DangerType.EnemyGhost: ExpansionRig(PrefabName.EnemyGhost, AnimType.EnemyGhost, true); break;
		case DangerType.EnemyGhostL: ExpansionRig(PrefabName.EnemyGhostL, AnimType.EnemyGhostL, true); break;
		case DangerType.EnemyGhostR: ExpansionRig(PrefabName.EnemyGhostR, AnimType.EnemyGhostR, true); break;
		case DangerType.EnemyWitchSoup: ExpansionRig(PrefabName.EnemyWitchSoup, AnimType.EnemyWitchSoup, false); break;
		case DangerType.EnemySkeletonHand: ExpansionRig(PrefabName.EnemySkeletonHand, AnimType.EnemySkeletonHand, false); break;
		case DangerType.EnemySkeletonL: ExpansionRig(PrefabName.EnemySkeletonL, AnimType.EnemySkeletonL, false); break;
		case DangerType.EnemySkeletonR: ExpansionRig(PrefabName.EnemySkeletonR, AnimType.EnemySkeletonR, false); break;
		case DangerType.EnemySkeleton1: ExpansionRig(PrefabName.EnemySkeleton1, AnimType.EnemySkeleton1, false); break;
		case DangerType.EnemySkeleton2: ExpansionRig(PrefabName.EnemySkeleton2, AnimType.EnemySkeleton2, false); break;
		case DangerType.EnemyGhostFormation1: ExpansionRig(PrefabName.EnemyGhostFormation1, AnimType.EnemyGhostFormation1, false); break;
		case DangerType.EnemyGhostFormation2: ExpansionRig(PrefabName.EnemyGhostFormation2, AnimType.EnemyGhostFormation2, false); break;
		case DangerType.EnemyBatSwarm: ExpansionRig(PrefabName.EnemyBatSwarm, AnimType.EnemyBatSwarm, true); break;
		case DangerType.EnemyPumpkinLantern: ExpansionRig(PrefabName.EnemyPumpkinLantern, AnimType.EnemyPumpkinLantern, true); break;
		case DangerType.EnemyDoorOpen: ExpansionRig(PrefabName.EnemyDoorOpen, AnimType.EnemyDoorOpen, true); break;
		case DangerType.EnemyDoorClose: ExpansionRig(PrefabName.EnemyDoorClose, AnimType.EnemyDoorClose, true); break;
		case DangerType.EnemyUFO: ExpansionRig(PrefabName.Lv14UFO, AnimType.EnemyUFO, true); break;
		case DangerType.EnemyChrisCard_Tree: ExpansionRig(PrefabName.EnemyChrisCard_Tree, AnimType.EnemyChrisCard_Tree, false); break;
		case DangerType.EnemyChrisCard_Snowman: ExpansionRig(PrefabName.EnemyChrisCard_Snowman, AnimType.EnemyChrisCard_Snowman, true); break;
		case DangerType.EnemyChrisCard_Spirit: ExpansionRig(PrefabName.EnemyChrisCard_Spirit, AnimType.EnemyChrisCard_Spirit, true); break;
		case DangerType.EnemyChrisCard_GingerbreadMan: ExpansionRig(PrefabName.EnemyChrisCard_GingerbreadMan, AnimType.EnemyChrisCard_GingerbreadMan, true); break;
		case DangerType.EnemyChrisCard_Reindeer: ExpansionRig(PrefabName.EnemyChrisCard_Reindeer, AnimType.EnemyChrisCard_Reindeer, true); break;
		case DangerType.EnemyChrisCard_Environmental: ExpansionRig(PrefabName.EnemyChrisCard_Environmental, AnimType.EnemyChrisCard_Environmental, true); break;
		case DangerType.EnemyChrisCard_SantaClaus: ExpansionRig(PrefabName.EnemyChrisCard_SantaClaus, AnimType.EnemyChrisCard_SantaClaus, true); break;
		case DangerType.ChristmasTreeTo3D: ExpansionRig(PrefabName.ChristmasTreeTo3D, AnimType.ChristmasTreeTo3D, true); break;
		case DangerType.SnowManTo3D: ExpansionRig(PrefabName.SnowManTo3D, AnimType.SnowManTo3D, true); break;
		case DangerType.ChristmasManTo3D: ExpansionRig(PrefabName.ChristmasManTo3D, AnimType.ChristmasManTo3D, true); break;
		case DangerType.CakeManTo3D: ExpansionRig(PrefabName.CakeManTo3D, AnimType.CakeManTo3D, true); break;
		case DangerType.JingleDoorSafe: ExpansionRig(PrefabName.JingleDoor, AnimType.JingleDoorRing, true); ExpansionSpread(DangerSpreadType.TopLeftRight); break;
		case DangerType.ChristmasMan3D: ExpansionRig(PrefabName.ChristmasMan3D, AnimType.ChristmasMan3D, true); break;
		case DangerType.SnowMan3D: ExpansionRig(PrefabName.SnowMan3D, AnimType.SnowMan3D, true); break;
		case DangerType.CakeManToLeft: ExpansionRig(PrefabName.CakeManToLeft, AnimType.CakeManToLeft, true); break;
		case DangerType.CakeManToRight: ExpansionRig(PrefabName.CakeManToRight, AnimType.CakeManToRight, true); break;
		case DangerType.GiftBallnoon: ExpansionRig(PrefabName.GiftBallnoon, AnimType.GiftBallnoon, false); break;
		case DangerType.Fireworks: ExpansionRig(PrefabName.Fireworks, AnimType.Fireworks, true); break;
		case DangerType.RotateChristmasTree: ExpansionRig(PrefabName.RotateChristmasTree, AnimType.RotateChristmasTree, true); break;
		case DangerType.EnemyPanda: ExpansionRig(PrefabName.EnemyPanda, AnimType.EnemyPanda, true); break;
		case DangerType.EnemyPendulumL: ExpansionRig(PrefabName.EnemyPendulumL, AnimType.EnemyPendulumL, true); break;
		case DangerType.EnemyPendulumR: ExpansionRig(PrefabName.EnemyPendulumR, AnimType.EnemyPendulumR, true); break;
		case DangerType.EnemyStakesL: ExpansionRig(PrefabName.EnemyStakesL, AnimType.EnemyStakesL, true); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyStakesR: ExpansionRig(PrefabName.EnemyStakesR, AnimType.EnemyStakesR, true); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyExtendTile1: ExpansionRig(PrefabName.EnemyExtendTile1, AnimType.EnemyExtendTile, true); IsGeoHidden = true; break;
		case DangerType.EnemyExtendTile2: ExpansionRig(PrefabName.EnemyExtendTile2, AnimType.EnemyExtendTile, true); IsGeoHidden = true; break;
		case DangerType.EnemyExtendTile3: ExpansionRig(PrefabName.EnemyExtendTile3, AnimType.EnemyExtendTile, true); IsGeoHidden = true; break;
		case DangerType.EnemyExtendTile4: ExpansionRig(PrefabName.EnemyExtendTile4, AnimType.EnemyExtendTile, true); IsGeoHidden = true; break;
		case DangerType.EnemyExtendFictitious: IsGeoHidden = true; HasAddition = HasAudio = HasAltAudio = false; break;
		case DangerType.EnemyDestroy1Left: ExpansionRig(PrefabName.EnemyDestroy1Left, AnimType.EnemyDestroyMove, true); break;
		case DangerType.EnemyDestroy1Right: ExpansionRig(PrefabName.EnemyDestroy1Right, AnimType.EnemyDestroyMove, true); break;
		case DangerType.EnemyDestroy2Left: ExpansionRig(PrefabName.EnemyDestroy2Left, AnimType.EnemyDestroyMove, true); break;
		case DangerType.EnemyDestroy2Right: ExpansionRig(PrefabName.EnemyDestroy2Right, AnimType.EnemyDestroyMove, true); break;
		case DangerType.EnemyDestroy3: ExpansionRig(PrefabName.EnemyDestroy3, AnimType.EnemyDestroyUp, true); break;
		case DangerType.EnemyBee: ExpansionRig(PrefabName.EnemyBee, AnimType.EnemyBee, false); break;
		case DangerType.SuperJumper: ExpansionRig(PrefabName.SuperJumper, AnimType.Null, true); IsJump = IsSuperJump = true; IsDangerous = false; break;
		case DangerType.EnemyGamePad: ExpansionRig(PrefabName.EnemyGamePad, AnimType.EnemyGamePad, true); ExpansionSpread(DangerSpreadType.TopLeftRight); break;
		case DangerType.EnemyPlane: ExpansionRig(PrefabName.EnemyPlane, AnimType.EnemyPlane, true); ExpansionSpread(DangerSpreadType.Cross); IsDangerHeatSpreader = true; break;
		default: return false;
		}
		return true;
	}

	private void ExpansionRig(PrefabName prefab, AnimType animation, bool audio)
	{
		AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefab);
		animType = animation;
		HasAudio = audio;
		HasAltAudio = false;
		IsDangerSpreader = false;
	}
	private void ExpansionSpread(DangerSpreadType spread)
	{
		IsDangerSpreader = true;
		dangerSpreadType = spread;
		dangerSpreadLength = 1;
	}

	private void ConfigureExpansionSharedEnemy()
	{
		int theme = LevelDesigner.GeneratingDonorThemeNumber;
		bool castle = theme == 18 || theme == 19 || theme == 20 || theme == 21 || theme == 29;
		bool halloween = theme == 22 || theme == 23 || theme == 24;
		bool christmas = theme == 27 || theme == 28;
		bool aliens = theme == 25 || theme == 26;
		bool pacman = theme == 34 || theme == 35 || theme == 36;
		bool retro = theme == 15 || theme == 16 || theme == 17;
		bool bamboo = theme == 30 || theme == 31;
		bool crystal = theme == 32 || theme == 33;
		bool cube = ExpansionGameplay.Later && (theme == 37 || theme == 38);
		bool reggae = ExpansionGameplay.Later && (theme == 41 || theme == 42);
		PrefabName prefab = AdditionPrefabID.prefabName;
		switch (dangerType)
		{
		case DangerType.Riser:
		case DangerType.RiserSlider:
			if (LevelDesigner.GenerateTrees)
			{
				if (halloween) prefab = PrefabName.Lv11TreeSmall;
				if (crystal) { prefab = PrefabName.Lv8TreeSmall2; alternateAnim.IsSpecialTrue = true; }
			}
			else if (!LevelDesigner.GeneratePyramids)
			{
				if (castle) prefab = PrefabName.Lv10Riser;
				else if (aliens) prefab = PrefabName.LV14EnemyRiser;
				else if (christmas) prefab = PrefabName.Lv15Riser;
			}
			break;
		case DangerType.FloaterGround:
		case DangerType.FloaterAir: if (theme == 15 || theme == 16) prefab = PrefabName.LV9_Floater; break;
		case DangerType.Crusher:
		case DangerType.CrusherSafe: if (castle) prefab = PrefabName.Lv10Crusher; break;
		case DangerType.Roller:
			if (aliens || bamboo) prefab = PrefabName.LV14EnemyRoller;
			else if (christmas || crystal) prefab = PrefabName.ChristmasManRoller;
			else if (reggae) prefab = PrefabName.EnemyLv25GunLun;
			break;
		case DangerType.Binary:
		case DangerType.BinaryOffset: if (animType == AnimType.BinarySlasher && (christmas || crystal)) prefab = PrefabName.Lv15Slasher; break;
		case DangerType.TallTree:
		case DangerType.TallTreeSlider:
			if (!LevelDesigner.GenerateTowers && !LevelDesigner.GenerateTeslas && !LevelDesigner.GenerateBuildings)
			{
				if (crystal) prefab = PrefabName.Lv8TreeTall;
				else if (christmas) prefab = PrefabName.Lv15Talltree;
				else if (halloween) prefab = PrefabName.LV11TreeTall;
				else if (aliens) prefab = PrefabName.LV14EnemyBuilding;
			}
			break;
		case DangerType.Pillar:
		case DangerType.PillarSlider:
		case DangerType.PillarSafe:
		case DangerType.PillarSafeSlider:
			if (retro || cube) prefab = PrefabName.Lv9Pillar;
			else if (castle || theme == 24 || pacman) prefab = PrefabName.Lv10Pillar;
			break;
		case DangerType.PillarLaser:
			if (retro || cube || theme == 18 || theme == 19 || pacman) prefab = PrefabName.Lv9PillarLaser;
			else if (halloween) prefab = PrefabName.Lv11EnemyPillarLaser;
			break;
		case DangerType.Laser:
		case DangerType.LaserSlider: if (bamboo) prefab = PrefabName.LV17Laser; break;
		case DangerType.RadioTower:
			if (aliens) prefab = PrefabName.LV14RadioTower;
			else if (christmas) prefab = PrefabName.Lv15Bigtree;
			break;
		case DangerType.Switcher:
			if (retro || cube) prefab = PrefabName.Lv9Switcher;
			else if (castle || pacman) prefab = PrefabName.Lv10Pillar;
			break;
		case DangerType.ShintoSafe:
		case DangerType.ShintoUnsafe:
			if (crystal) prefab = PrefabName.Them14Shinto;
			else if (castle || theme == 2) prefab = PrefabName.Lv10Shinto;
			else if (aliens) prefab = PrefabName.LV14Shinto;
			else if (bamboo) prefab = PrefabName.LV17Shinto;
			break;
		case DangerType.EnemyLv9TakeBack: if (castle || pacman || cube) prefab = PrefabName.EnemyLv10TakeBack; break;
		case DangerType.EnemyBee: if (cube) prefab = PrefabName.EnemyLv20FlyCube; break;
		case DangerType.EnemyDestroy3: if (cube) prefab = PrefabName.EnemyLv20CubeDes; break;
		}
		AdditionPrefabID = new PrefabID(AdditionPrefabID.prefabType, prefab);
	}

	public void HidenMatchTriggerLaser()
	{
		TileVal trigger = (TileVal)TypeVal;
		if (trigger != TileVal.TriggerGreenLaser && trigger != TileVal.TriggerRedLaser) return;
		HideExpansionLaser(RowIndex, TileIndex - 1, trigger);
		HideExpansionLaser(RowIndex, TileIndex + 1, trigger);
		for (int i = -1; i < 4; i++)
		{
			HideExpansionLaser(RowIndex + i, TileIndex - 1, trigger);
			HideExpansionLaser(RowIndex + i, TileIndex, trigger);
			HideExpansionLaser(RowIndex + i, TileIndex + 1, trigger);
		}
		HideExpansionLaser(RowIndex + 4, TileIndex, trigger);
		isTrigger = true;
	}
	private void HideExpansionLaser(int row, int col, TileVal trigger)
	{
		if (row < 0 || row >= GameManager.TileBoard.CurrentSegment.GetLength(0) || col < 0 || col > 4) return;
		Tile target = GameManager.TileBoard[new MathUtils.IntTrio(Index.x, row, col)];
		if (target == null) return;
		TileVal name = (TileVal)target.TypeVal;
		if ((trigger == TileVal.TriggerGreenLaser && (name == TileVal.EnemyGreenLaserH || name == TileVal.EnemyGreenLaserV)) ||
			(trigger == TileVal.TriggerRedLaser && (name == TileVal.EnemyRedLaserH || name == TileVal.EnemyRedLaserV)))
		{
			target.AdditionTransform.localPosition = new Vector3(0f, 0f, 10000f);
			target.dangerType = DangerType.Null;
		}
	}
	public void EatBean()
	{
		if (EatLaterBean()) return;
		if ((TileVal)TypeVal == TileVal.EnemyBigBean)
		{
			Ball.breakEnable = true;
			GameManager.theBall.ShowCollapsingForce();
			ParticleManager.Detonate(PrefabName.Particles_PickupBig);
		}
		SwitchAddition();
		isEat = true;
		if (AudioPlayer != null) PlaySound();
	}

	private bool TryExpansionKillBall(DangerType hazard, bool jumping, Vector3 ballPos, out bool kill)
	{
		kill = false;
		if ((hazard == DangerType.Heat || hazard == DangerType.Danger || hazard == DangerType.BinaryOffset || hazard == DangerType.BinaryMiniOffset) && animType == AnimType.EnemyPlane)
		{
			kill = true;
			return true;
		}
		switch (hazard)
		{
		case DangerType.EnemyRedLaserH: case DangerType.EnemyRedLaserV: case DangerType.EnemyGreenLaserH: case DangerType.EnemyGreenLaserV:
		case DangerType.EnemeyLaserBlock: case DangerType.EnemySnakerFrontMove: case DangerType.EnemyVerticalCannon:
		case DangerType.EnemyJumpFromL1: case DangerType.EnemyJumpFromR1: case DangerType.EnemyJumpFromL2: case DangerType.EnemyJumpFromR2:
		case DangerType.EnemyLeaningGeneralCube3: case DangerType.EnemyLeaningRiser: case DangerType.EnemyLeaningTower:
		case DangerType.EnemyCollapseTower: case DangerType.EnemyTower: case DangerType.EnemyFallHammerL: case DangerType.EnemyFallHammerR:
		case DangerType.EnemyTombstone: case DangerType.EnemyGhost: case DangerType.EnemyGhostL: case DangerType.EnemyGhostR:
		case DangerType.EnemyWitchSoup: case DangerType.EnemyPumpkinLantern: case DangerType.EnemySkeletonHand:
		case DangerType.EnemyPanda: case DangerType.EnemyPendulumL: case DangerType.EnemyPendulumR: case DangerType.EnemyStakesL: case DangerType.EnemyStakesR:
		case DangerType.EnemyChrisCard_Reindeer: case DangerType.ChristmasTreeTo3D: case DangerType.SnowManTo3D: case DangerType.CakeManTo3D:
		case DangerType.JingleDoorSafe: case DangerType.SnowMan3D: case DangerType.CakeManToLeft: case DangerType.CakeManToRight:
			kill = !jumping; break;
		case DangerType.EnemyRetroSnaker02: case DangerType.EnemySnakerWallRight: case DangerType.EnemySnakerWallLeft: case DangerType.EnemyMiracle:
		case DangerType.EnemySkeleton1: case DangerType.EnemySkeleton2: case DangerType.EnemyUFO: case DangerType.EnemyGamePad: case DangerType.EnemyPlane:
		case DangerType.EnemyChrisCard_Tree: case DangerType.EnemyChrisCard_Snowman: case DangerType.EnemyChrisCard_Environmental: case DangerType.EnemyChrisCard_SantaClaus:
		case DangerType.ChristmasManTo3D: case DangerType.ChristmasMan3D: case DangerType.GiftBallnoon: case DangerType.RotateChristmasTree:
			kill = true; break;
		case DangerType.EnemyDestroy1Left: case DangerType.EnemyDestroy1Right: case DangerType.EnemyDestroy2Left: case DangerType.EnemyDestroy2Right: case DangerType.EnemyDestroy3:
			kill = !jumping && !Ball.breakEnable; break;
		case DangerType.EnemyBee: kill = jumping; break;
		case DangerType.ChristmasManRoller: kill = !jumping && Mathf.Abs(AnimTransform.position.x - ballPos.x) <= 0.5f; break;
		case DangerType.EnemyFloaterAirFallGround: case DangerType.EnemyRotateFourBlock: case DangerType.EnemyLv9TakeBack: case DangerType.EnemyRetroSnaker01:
		case DangerType.EnemySnakerCoiled01: case DangerType.EnemySnakerLeftMove: case DangerType.EnemySnakerRightMove: case DangerType.RoadUp:
		case DangerType.EnemySkeletonL: case DangerType.EnemySkeletonR: case DangerType.EnemyGhostFormation1: case DangerType.EnemyGhostFormation2:
		case DangerType.EnemyBatSwarm: case DangerType.EnemyDoorOpen: case DangerType.EnemyDoorClose: case DangerType.SuperJumper:
		case DangerType.EnemyChrisCard_Spirit: case DangerType.EnemyChrisCard_GingerbreadMan: case DangerType.Fireworks:
		case DangerType.EnemyExtendTile1: case DangerType.EnemyExtendTile2: case DangerType.EnemyExtendTile3: case DangerType.EnemyExtendTile4: case DangerType.EnemyExtendFictitious:
			break;
		default: return false;
		}
		if (kill && IsBeamSwitchable) SwitchBeam();
		if (kill && IsSlider && AnimTransform != null) kill = Mathf.Abs(AnimTransform.position.x - ballPos.x) <= 0.5f;
		return true;
	}
}
