using UnityEngine;

public partial class LevelConstructor
{
	// 1.4.7.1 DecideType: new named cases. Shared first-eight constructors are
	// identical except the crown randomization branch, handled here first.
	private static Tile DecideExpansionType(int value, float offset, int segment, int row, int col, bool flipped)
	{
		TileVal name = (TileVal)value;
		Tile.DangerType danger;
		Direction direction = Direction.Null;
		Tile.GroundType ground = Tile.GroundType.Standard;
		float? animationValue = null;
		switch (name)
		{
		case TileVal.PickupStarGround:
		case TileVal.PickupStarJump:
		case TileVal.PickupStarSliderLeft:
		case TileVal.PickupStarSliderRight:
		case TileVal.PickupStarJumpLeft:
		case TileVal.PickupStarJumpRight:
		{
			bool slider = name == TileVal.PickupStarSliderLeft || name == TileVal.PickupStarSliderRight;
			bool jump = name == TileVal.PickupStarJump || name == TileVal.PickupStarJumpLeft || name == TileVal.PickupStarJumpRight;
			direction = name == TileVal.PickupStarSliderLeft || name == TileVal.PickupStarJumpLeft ? Direction.Left :
				(name == TileVal.PickupStarSliderRight || name == TileVal.PickupStarJumpRight ? Direction.Right : Direction.Null);
			int index = 0;
			if (ClassicCrownRandom.Enabled && ClassicCrownRandom.GetCrownTypeOnCreate(ref index) == ClassicCrownRandom.CrownType.NOTHING)
			{
				if (slider) return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Ground, Tile.AnimType.Slider, direction);
				return MakeAsset(value, offset, segment, row, col, flipped, jump ? Tile.Type.Jump : Tile.Type.Ground);
			}
			Tile tile = MakeAsset(value, offset, segment, row, col, flipped, jump ? Tile.Type.Jump : Tile.Type.Ground,
				slider ? Tile.PickupType.StarSlider : Tile.PickupType.Star, direction);
			tile.CrownIndex = index;
			return tile;
		}
		case TileVal.MoveJumpLeft:
		case TileVal.MoveJumpRight: return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Jump);
		case TileVal.TriggerRedLaser:
		case TileVal.TriggerGreenLaser: return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.LaserTrigger);
		case TileVal.EnemySmallBean:
		case TileVal.EnemyBigBean: return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Bean);
		case TileVal.Lv10RotationTile: return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Ground, Tile.AnimType.Rotation, Direction.Null);
		case TileVal.EnemyRedLaserH: danger = Tile.DangerType.EnemyRedLaserH; break;
		case TileVal.EnemyRedLaserV: danger = Tile.DangerType.EnemyRedLaserV; break;
		case TileVal.EnemyGreenLaserH: danger = Tile.DangerType.EnemyGreenLaserH; break;
		case TileVal.EnemyGreenLaserV: danger = Tile.DangerType.EnemyGreenLaserV; break;
		case TileVal.EnemyFloaterAirFallGround: danger = Tile.DangerType.EnemyFloaterAirFallGround; break;
		case TileVal.EnemyRotateFourBlock: danger = Tile.DangerType.EnemyRotateFourBlock; break;
		case TileVal.EnemyLv9TakeBack: danger = Tile.DangerType.EnemyLv9TakeBack; break;
		case TileVal.EnemeyLaserBlock: danger = Tile.DangerType.EnemeyLaserBlock; break;
		case TileVal.EnemyRetroSnaker01: danger = Tile.DangerType.EnemyRetroSnaker01; break;
		case TileVal.EnemyRetroSnaker02: danger = Tile.DangerType.EnemyRetroSnaker02; break;
		case TileVal.EnemySnakerWallRight: danger = Tile.DangerType.EnemySnakerWallRight; break;
		case TileVal.EnemySnakerWallLeft: danger = Tile.DangerType.EnemySnakerWallLeft; break;
		case TileVal.EnemySnakerFrontMove: danger = Tile.DangerType.EnemySnakerFrontMove; break;
		case TileVal.EnemySnakerCoiled01: danger = Tile.DangerType.EnemySnakerCoiled01; break;
		case TileVal.EnemySnakerLeftMove: danger = Tile.DangerType.EnemySnakerLeftMove; break;
		case TileVal.EnemySnakerRightMove: danger = Tile.DangerType.EnemySnakerRightMove; break;
		case TileVal.EnemyVerticalCannon: danger = Tile.DangerType.EnemyVerticalCannon; break;
		case TileVal.EnemyTurretDown: danger = Tile.DangerType.Turret; direction = Direction.Top; break;
		case TileVal.EnemyTurretFragileDown: danger = Tile.DangerType.Turret; direction = Direction.Top; ground = Tile.GroundType.Fragile0; break;
		case TileVal.Lv10Roadup: danger = Tile.DangerType.RoadUp; break;
		case TileVal.Lv10JumpFromL1: danger = Tile.DangerType.EnemyJumpFromL1; break;
		case TileVal.Lv10JumpFromR1: danger = Tile.DangerType.EnemyJumpFromR1; break;
		case TileVal.Lv10JumpFromL2: danger = Tile.DangerType.EnemyJumpFromL2; break;
		case TileVal.Lv10JumpFromR2: danger = Tile.DangerType.EnemyJumpFromR2; break;
		case TileVal.Lv10LeaningGeneralCube3: danger = Tile.DangerType.EnemyLeaningGeneralCube3; animationValue = 3f; break;
		case TileVal.Lv10LeaningEnemyRiser: danger = Tile.DangerType.EnemyLeaningRiser; break;
		case TileVal.Lv10LeaningTower: danger = Tile.DangerType.EnemyLeaningTower; break;
		case TileVal.Lv10Tower: danger = Tile.DangerType.EnemyTower; break;
		case TileVal.Lv10CollapseTower: danger = Tile.DangerType.EnemyCollapseTower; break;
		case TileVal.Lv10Miracle: danger = Tile.DangerType.EnemyMiracle; break;
		case TileVal.LV10FallHammerL: danger = Tile.DangerType.EnemyFallHammerL; direction = Direction.Left; break;
		case TileVal.Lv10FallHammerR: danger = Tile.DangerType.EnemyFallHammerR; direction = Direction.Right; break;
		case TileVal.EnemyTombstone: danger = Tile.DangerType.EnemyTombstone; break;
		case TileVal.EnemyGhost: danger = Tile.DangerType.EnemyGhost; break;
		case TileVal.EnemyGhostL: danger = Tile.DangerType.EnemyGhostL; break;
		case TileVal.EnemyGhostR: danger = Tile.DangerType.EnemyGhostR; break;
		case TileVal.EnemyWitchSoup: danger = Tile.DangerType.EnemyWitchSoup; break;
		case TileVal.EnemyPumpkinLantern: danger = Tile.DangerType.EnemyPumpkinLantern; break;
		case TileVal.EnemySkeletonHand: danger = Tile.DangerType.EnemySkeletonHand; break;
		case TileVal.EnemySkeletonL: danger = Tile.DangerType.EnemySkeletonL; break;
		case TileVal.EnemySkeletonR: danger = Tile.DangerType.EnemySkeletonR; break;
		case TileVal.EnemySkeleton1: danger = Tile.DangerType.EnemySkeleton1; break;
		case TileVal.EnemySkeleton2: danger = Tile.DangerType.EnemySkeleton2; break;
		case TileVal.EnemyGhostFormation1: danger = Tile.DangerType.EnemyGhostFormation1; break;
		case TileVal.EnemyGhostFormation2: danger = Tile.DangerType.EnemyGhostFormation2; break;
		case TileVal.EnemyBatSwarm: danger = Tile.DangerType.EnemyBatSwarm; break;
		case TileVal.EnemyDoorOpen: danger = Tile.DangerType.EnemyDoorOpen; break;
		case TileVal.EnemyDoorClose: danger = Tile.DangerType.EnemyDoorClose; break;
		case TileVal.EnemyUFO: danger = Tile.DangerType.EnemyUFO; break;
		case TileVal.EnemyChrisCard_Tree: danger = Tile.DangerType.EnemyChrisCard_Tree; break;
		case TileVal.EnemyChrisCard_Snowman: danger = Tile.DangerType.EnemyChrisCard_Snowman; break;
		case TileVal.EnemyChrisCard_Spirit: danger = Tile.DangerType.EnemyChrisCard_Spirit; break;
		case TileVal.EnemyChrisCard_GingerbreadMan: danger = Tile.DangerType.EnemyChrisCard_GingerbreadMan; break;
		case TileVal.EnemyChrisCard_Reindeer: danger = Tile.DangerType.EnemyChrisCard_Reindeer; break;
		case TileVal.EnemyChrisCard_Environmental: danger = Tile.DangerType.EnemyChrisCard_Environmental; break;
		case TileVal.EnemyChrisCard_SantaClaus: danger = Tile.DangerType.EnemyChrisCard_SantaClaus; break;
		case TileVal.ChristmasTreeTo3D: danger = Tile.DangerType.ChristmasTreeTo3D; break;
		case TileVal.SnowManTo3D: danger = Tile.DangerType.SnowManTo3D; break;
		case TileVal.ChristmasManTo3D: danger = Tile.DangerType.ChristmasManTo3D; break;
		case TileVal.CakeManTo3D: danger = Tile.DangerType.CakeManTo3D; break;
		case TileVal.JingleDoor: danger = Tile.DangerType.JingleDoorSafe; break;
		case TileVal.ChristmasMan3D: danger = Tile.DangerType.ChristmasMan3D; break;
		case TileVal.SnowMan3D: danger = Tile.DangerType.SnowMan3D; break;
		case TileVal.CakeManToLeft: danger = Tile.DangerType.CakeManToLeft; break;
		case TileVal.CakeManToRight: danger = Tile.DangerType.CakeManToRight; break;
		case TileVal.GiftBallnoon: danger = Tile.DangerType.GiftBallnoon; break;
		case TileVal.Fireworks: danger = Tile.DangerType.Fireworks; break;
		case TileVal.RotateChristmasTree: danger = Tile.DangerType.RotateChristmasTree; break;
		case TileVal.EnemyPanda: danger = Tile.DangerType.EnemyPanda; break;
		case TileVal.EnemyPendulumL: danger = Tile.DangerType.EnemyPendulumL; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyPendulumR: danger = Tile.DangerType.EnemyPendulumR; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyStakesL: danger = Tile.DangerType.EnemyStakesL; direction = Direction.Left; break;
		case TileVal.EnemyStakesR: danger = Tile.DangerType.EnemyStakesR; direction = Direction.Right; break;
		case TileVal.EnemyExtendTile1: danger = Tile.DangerType.EnemyExtendTile1; break;
		case TileVal.EnemyExtendTile2: danger = Tile.DangerType.EnemyExtendTile2; break;
		case TileVal.EnemyExtendTile3: danger = Tile.DangerType.EnemyExtendTile3; break;
		case TileVal.EnemyExtendTile4: danger = Tile.DangerType.EnemyExtendTile4; break;
		case TileVal.EnemyExtendFictitious: danger = Tile.DangerType.EnemyExtendFictitious; break;
		case TileVal.EnemyDestroy1Left: danger = Tile.DangerType.EnemyDestroy1Left; break;
		case TileVal.EnemyDestroy1Right: danger = Tile.DangerType.EnemyDestroy1Right; break;
		case TileVal.EnemyDestroy2Left: danger = Tile.DangerType.EnemyDestroy2Left; break;
		case TileVal.EnemyDestroy2Right: danger = Tile.DangerType.EnemyDestroy2Right; break;
		case TileVal.EnemyDestroy3: danger = Tile.DangerType.EnemyDestroy3; break;
		case TileVal.EnemyBee: danger = Tile.DangerType.EnemyBee; break;
		case TileVal.EnemySuperJumper: danger = Tile.DangerType.SuperJumper; break;
		case TileVal.EnemyGamePad: danger = Tile.DangerType.EnemyGamePad; break;
		case TileVal.EnemyPlane: danger = Tile.DangerType.EnemyPlane; break;
		default: return null;
		}
		return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Enemy, ground, Tile.PickupType.Null,
			danger, Tile.AnimType.Null, direction, Tile.Edge.Null, animationValue);
	}

	private static void GroupExpansionRotation()
	{
		if (!currentTile.IsNeedRotation) return;
		int left = currentCol, right = currentCol;
		while (left > 0 && currentSegment[currentRow, left - 1] != null && currentSegment[currentRow, left - 1].IsNeedRotation) left--;
		while (right < 4 && currentSegment[currentRow, right + 1] != null && currentSegment[currentRow, right + 1].IsNeedRotation) right++;
		// Donor DFS includes the starting tile through its neighbour, but leaves
		// a singleton unparented. For even groups it chooses the right median.
		int parent = left + (right - left + 1) / 2;
		if (left != right && parent != currentCol)
		{
			currentTile.ConfigureAnimChild(parent);
			currentSegment[currentRow, parent].ConfigureAnimParent(currentCol);
		}
	}
}
