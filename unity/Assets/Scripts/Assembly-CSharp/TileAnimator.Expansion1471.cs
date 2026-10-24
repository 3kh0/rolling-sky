using UnityEngine;

public partial class TileAnimator
{
	private static bool AnimateExpansionTile(int row, int col)
	{
		if (!ExpansionGameplay.Enabled) return false;
		if (AnimateLaterTile(row, col)) return true;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		float distance = animatedRows[row].Distance;
		switch (animatedRows[row][col].animType)
		{
		case Tile.AnimType.StarCollect: CollectPickup(row, col); break;
		case Tile.AnimType.StarSlider: SlideSlider(row, col); SpinExpansionCrown(row, col); break;
		case Tile.AnimType.Star: case Tile.AnimType.StarFloater: SpinExpansionCrown(row, col); break;
		case Tile.AnimType.MoveJumpLeft: MoveExpansionJump(row, col, true); break;
		case Tile.AnimType.MoveJumpRight: MoveExpansionJump(row, col, false); break;
		case Tile.AnimType.EnemyLaser: ExpansionLaser(tile, distance, false); break;
		case Tile.AnimType.EnemyRedLaserH: case Tile.AnimType.EnemyGreenLaserH: ExpansionLaser(tile, distance, true); break;
		case Tile.AnimType.EnemyFloaterAirFallGround:
			if (distance < 6f && distance > -2f) TransformUtils.SetZ(tile.AnimTransform, tile.AnimTransform.localPosition.z + 0.2f, true);
			break;
		case Tile.AnimType.RoadUp:
			if (distance < 6f && distance > -2f && tile.AnimTransform.localPosition.z >= -3f) TransformUtils.SetZ(tile.AnimTransform, tile.AnimTransform.localPosition.z - 0.2f, true);
			break;
		case Tile.AnimType.EnemyLv9TakeBack:
			if (distance < 4f && tile.AdditionTransform.localPosition.z < 0.2f) TransformUtils.SetZ(tile.AdditionTransform, tile.AdditionTransform.localPosition.z + 0.15f, true);
			break;
		case Tile.AnimType.EnemyRotateFourBlock: ExpansionClip(row, col, 20f, -5f, 16f, -8f, "blocksRotate", false); break;
		case Tile.AnimType.EnemyRetroSnaker01: case Tile.AnimType.EnemyRetroSnaker02: case Tile.AnimType.EnemySnakerWallRight: case Tile.AnimType.EnemySnakerWallLeft:
			ExpansionClip(row, col, 20f, -5f, 16f, -8f, "snake", true); break;
		case Tile.AnimType.EnemySnakerFrontMove: ExpansionClip(row, col, 16f, -5f, 16f, -8f, "snake", true); break;
		case Tile.AnimType.EnemySnakerCoiled01: ExpansionClip(row, col, 26f, -5f, 25f, -8f, "snake", true); break;
		case Tile.AnimType.EnemySnakerLeftMove: case Tile.AnimType.EnemySnakerRightMove: ExpansionClip(row, col, 15f, -5f, 16f, -8f, "snake", true); break;
		case Tile.AnimType.EnemyVerticalCannon: ExpansionClip(row, col, 20f, -5f, 8f, -8f, "verticalCannon", false); break;
		case Tile.AnimType.Rotation: GroundRotation(row, col); break;
		case Tile.AnimType.LeaningGeneralCube3:
			if (distance <= 15f && !tile.IsAnimChild) GroundRotation(row, col);
			RisePillar(row, col); break;
		case Tile.AnimType.EnemyJumpFromL1:
			if (distance < 16f && !tile.IsAnimChild) GroundRotation(row, col);
			ExpansionClip(row, col, 16f, -5f, 16f, -20f, "Box_one_LtoR", false); break;
		case Tile.AnimType.EnemyJumpFromR1:
			if (distance < 16f && !tile.IsAnimChild) GroundRotation(row, col);
			ExpansionClip(row, col, 16f, -5f, 16f, -20f, "Box_one_RtoL", false); break;
		case Tile.AnimType.EnemyLeaningRiser:
			if (distance < 20f && !tile.IsAnimChild) GroundRotation(row, col);
			ExpansionClip(row, col, 20f, -5f, 20f, -8f, "Restore", false); break;
		case Tile.AnimType.EnemyLeaningTower:
			if (distance < 14f && !tile.IsAnimChild) GroundRotation(row, col);
			ExpansionClip(row, col, 14f, -5f, 14f, -8f, "enemyTower_UP", false); break;
		case Tile.AnimType.EnemyTower: ExpansionClip(row, col, 17f, -5f, 8f, -8f, "enemyTower", true); break;
		case Tile.AnimType.EnemyCollapseTower: ExpansionClip(row, col, 12f, -5f, 12f, -10f, "enemyTower_down", false); break;
		case Tile.AnimType.EnemyJumpFromL2: ExpansionClip(row, col, 16f, -5f, 16f, -8f, "Box_Two_LtoR", false); break;
		case Tile.AnimType.EnemyJumpFromR2: ExpansionClip(row, col, 16f, -5f, 16f, -8f, "Box_Two_RtoL", false); break;
		case Tile.AnimType.EnemyMiracle: ExpansionClip(row, col, 17f, -5f, 8f, -8f, "Sign", false); break;
		case Tile.AnimType.EnemyFallHammerL: ExpansionImpactClip(row, col, 15f, 15f, -8f, "HammerTower", false, false); break;
		case Tile.AnimType.EnemyFallHammerR: ExpansionImpactClip(row, col, 15f, 15f, -8f, "HammerTower_R", false, false); break;
		case Tile.AnimType.EnemyTombstone: ExpansionClip(row, col, 15f, -5f, 10f, -15f, "anim01", false); break;
		case Tile.AnimType.EnemyGhost: ExpansionClip(row, col, 15f, -5f, 7f, -14f, "anim01", true, 8f); break;
		case Tile.AnimType.EnemyGhostL: case Tile.AnimType.EnemyGhostR: ExpansionClip(row, col, 15f, -5f, 12f, -16f, "anim01", true, 13f); break;
		case Tile.AnimType.EnemyWitchSoup: ExpansionSkeleton(row, col, 15f, 11f, -8f); break;
		case Tile.AnimType.EnemyPumpkinLantern: ExpansionSkeleton(row, col, 11f, 10f, -8f); break;
		case Tile.AnimType.EnemySkeletonHand: ExpansionSkeleton(row, col, 15f, 5f, -8f); break;
		case Tile.AnimType.EnemySkeletonL: case Tile.AnimType.EnemySkeletonR: ExpansionSkeleton(row, col, 19f, 18f, -8f); break;
		case Tile.AnimType.EnemySkeleton1: case Tile.AnimType.EnemySkeleton2: ExpansionSkeleton(row, col, 16f, 15f, -8f); break;
		case Tile.AnimType.EnemyGhostFormation1: case Tile.AnimType.EnemyGhostFormation2: ExpansionSkeleton(row, col, 22f, 20f, -8f); break;
		case Tile.AnimType.EnemyDoorOpen: case Tile.AnimType.EnemyDoorClose: ExpansionSkeleton(row, col, 19f, 3f, -8f); break;
		case Tile.AnimType.EnemyChrisCard_Tree: case Tile.AnimType.EnemyChrisCard_Snowman: case Tile.AnimType.EnemyChrisCard_Spirit: case Tile.AnimType.EnemyChrisCard_GingerbreadMan:
		case Tile.AnimType.ChristmasMan3D: case Tile.AnimType.SnowMan3D: ExpansionSkeleton(row, col, 22f, 10f, -10f); break;
		case Tile.AnimType.EnemyChrisCard_Reindeer: ExpansionSkeleton(row, col, 22f, 14f, -10f); break;
		case Tile.AnimType.EnemyChrisCard_Environmental: ExpansionSkeleton(row, col, 22f, 9f, -10f); break;
		case Tile.AnimType.EnemyChrisCard_SantaClaus: ExpansionSkeleton(row, col, 22f, 11f, -10f); break;
		case Tile.AnimType.ChristmasManTo3D: case Tile.AnimType.SnowManTo3D: ExpansionSkeleton(row, col, 22f, 7f, -10f); break;
		case Tile.AnimType.ChristmasTreeTo3D: ExpansionSkeleton(row, col, 15f, 14f, -10f); break;
		case Tile.AnimType.CakeManTo3D: case Tile.AnimType.CakeManToLeft: case Tile.AnimType.CakeManToRight: ExpansionSkeleton(row, col, 22f, 12f, -10f); break;
		case Tile.AnimType.JingleDoorRing: ExpansionSkeleton(row, col, 22f, 6f, -14f); break;
		case Tile.AnimType.GiftBallnoon: ExpansionSkeleton(row, col, 22f, 8f, -10f); break;
		case Tile.AnimType.RotateChristmasTree: ExpansionSkeleton(row, col, 22f, 15f, -10f); break;
		case Tile.AnimType.EnemyBatSwarm: case Tile.AnimType.Fireworks: ExpansionParticle(row, col); break;
		case Tile.AnimType.EnemyUFO: ExpansionImpactClip(row, col, 15f, 8f, -18f, "001", false, false); break;
		case Tile.AnimType.EnemyPanda: ExpansionClip(row, col, 17f, -5f, 17f, -6f, "Take 001", true, float.PositiveInfinity, 3f, true); break;
		case Tile.AnimType.EnemyPendulumL: case Tile.AnimType.EnemyPendulumR: ExpansionImpactClip(row, col, 15f, 12.5f, -3f, "Take 001", true, true); break;
		case Tile.AnimType.EnemyStakesL: case Tile.AnimType.EnemyStakesR: ExpansionClip(row, col, 15f, -5f, 6.5f, -8f, "Take 001", true, float.PositiveInfinity, 3f, true); break;
		case Tile.AnimType.EnemyExtendTile: ExpansionExtend(row, col); break;
		case Tile.AnimType.EnemyDestroyMove: ExpansionDestroy(row, col, ExpansionGameplay.SourceNumber == 20 ? 11f : 13f, "move"); break;
		case Tile.AnimType.EnemyDestroyUp: ExpansionDestroy(row, col, 8f, "up"); break;
		case Tile.AnimType.EnemyBee: ExpansionClip(row, col, 13f, -5f, 7f, -10f, "bee", false); break;
		case Tile.AnimType.EnemyGamePad: ExpansionClip(row, col, 20f, -13f, 13f, -5f, "gamepad", true, soundBefore: true); break;
		case Tile.AnimType.EnemyPlane: ExpansionClip(row, col, 20f, -13f, 13f, -5f, "plane", true, soundBefore: true); break;
		case Tile.AnimType.EnemeyLaserBlock: case Tile.AnimType.BigBean: break;
		default: return false;
		}
		return true;
	}

	private static void RiseExpansionCrusher(int row, int col)
	{
		if (animatedRows[row].Distance > 15f) return;
		float offset = animatedRows[row][col].altAnim.IsTrue ? -0.6f : -0.1725f;
		float height = FloatAnim.PulseWeighted(GetDistanceCycle(row, 12f, offset), 0.85f) * 2f - 1f;
		bool below, above;
		float clamped = MathUtils.Clamp01(height, out below, out above);
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		TransformUtils.SetY(tile.AnimTransform, clamped * 2f, true);
		float spin = 0.05f;
		if (height >= -0.25f && height <= 1f)
		{
			spin = FloatAnim.Pulse01(MathUtils.ToPercent(height, -0.25f, 1f));
			if (animatedRows[row].JustSwitchedCycle(col, below) && clamped <= 0f) { tile.PlaySound(); ShakeCamera(0.5f, row); }
			else foreach (Transform part in tile.AnimPartTransforms) TransformUtils.SetZ(part, 0.2f * clamped, true);
		}
		tile.AltAnimTransform.Rotate(Vector3.up * spin * 1000f * Time.deltaTime);
		if (DeviceQualityChecker.QualityIsHigh()) tile.ShadowTransform.localScale = Vector3.one * MathUtils.FromPercent(1f - clamped, 0.7f, 1f);
	}

	// Native EnemySkeleton / AnimatorPlay and their identical clip routines.
	// Bounds are deliberately strict; IsPlayed is shared by the row for this
	// frame only, while FirstCheck is persistent per tile.
	private static void ExpansionClip(int row, int col, float outer, float visibleMin, float play, float reset, string clip, bool sound,
		float visibleMax = float.PositiveInfinity, float playMin = float.NegativeInfinity, bool soundBefore = false)
	{
		AnimatedRow ar = animatedRows[row];
		float d = ar.Distance;
		if (d >= outer) return;
		Tile tile = GameManager.TileBoard[ar[col].index];
		if (d < visibleMax && d > visibleMin)
		{
			Animation animation = tile.newAnimTrasFromAnimation;
			animation.gameObject.SetActive(true);
			if (d > playMin && d < play && !ar.IsPlayed && ar.FirstCheck(col))
			{
				if (sound && soundBefore) tile.PlaySound();
				animation.Play(clip);
				ar.IsPlayed = true;
				if (sound && !soundBefore) tile.PlaySound();
			}
		}
		else if (d < reset) ResetAnimation(tile.newAnimTrasFromAnimation);
	}
	private static void ExpansionSkeleton(int row, int col, float outer, float play, float reset)
	{
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		ExpansionClip(row, col, outer, -5f, play, reset, "anim01", tile.HasAudio && tile.AudioPlayer != null);
	}
	private static void ExpansionImpactClip(int row, int col, float outer, float play, float reset, string clip, bool soundOnStart, bool activateOnStart)
	{
		AnimatedRow ar = animatedRows[row];
		float d = ar.Distance;
		if (d >= outer) return;
		Tile tile = GameManager.TileBoard[ar[col].index];
		if (d > -5f)
		{
			Animation animation = tile.newAnimTrasFromAnimation;
			if (!activateOnStart) animation.gameObject.SetActive(true);
			if (d > 3f && d < play && !ar.IsPlayed && ar.FirstCheck(col))
			{
				if (soundOnStart) tile.PlaySound();
				animation.Play(clip);
				if (activateOnStart) animation.gameObject.SetActive(true);
				ar.IsPlayed = true;
			}
			else if (d < 3f && ar.SecondCheck(col)) { tile.PlaySound(); ShakeCamera(0.4f); }
		}
		else if (d < reset) ResetAnimation(tile.newAnimTrasFromAnimation);
	}
	private static void GroundRotation(int row, int col)
	{
		float d = animatedRows[row].Distance;
		if (d > 17f) return;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		float? angle = tile.yAngle;
		if (!tile.yAngleIsInit)
		{
			if (!angle.HasValue) return;
			tile.transform.eulerAngles = new Vector3(0f, angle.Value, 0f);
			tile.yAngleIsInit = true;
		}
		bool below, above;
		float percent = MathUtils.ToPercent01(d, 8f, 4f, out below, out above);
		if (!below) TransformUtils.SetAngleY(tile, (1f - percent) * angle.Value);
		if (above) TransformUtils.SetAngleY(tile, 0f);
	}
	private static void ExpansionLaser(Tile tile, float distance, bool horizontal)
	{
		ParticleSystem effect = tile.AnimParticleSystem;
		if (distance < (horizontal ? 10f : 8f) && distance > -1f)
		{
			if (!effect.gameObject.activeSelf) { effect.gameObject.SetActive(true); effect.Play(); }
			if (horizontal)
			{
				Transform beam = tile.AltAnimTransform;
				Quaternion rotation = beam.localRotation;
				Vector3 delta = Camera.main.transform.parent.parent.position - beam.position;
				Vector2 normal = new Vector2(delta.z, delta.y).normalized;
				rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, Mathf.Atan2(normal.x, normal.y) * 57.29578f - 180f, rotation.eulerAngles.z);
				beam.localRotation = rotation;
			}
		}
		else if (effect.gameObject.activeSelf) effect.gameObject.SetActive(false);
	}
	private static void ExpansionParticle(int row, int col)
	{
		AnimatedRow ar = animatedRows[row];
		float d = ar.Distance;
		if (d >= 10f) return;
		Tile tile = GameManager.TileBoard[ar[col].index];
		if (d < 10f && d > -15f)
		{
			if (ar.FirstCheck(col)) { tile.TileParticle.gameObject.SetActive(true); tile.TileParticle.Play(); tile.PlaySound(); }
		}
		else if (d < -15f) { tile.TileParticle.Stop(); tile.TileParticle.gameObject.SetActive(false); }
	}
	private static void ExpansionExtend(int row, int col)
	{
		AnimatedRow ar = animatedRows[row];
		float d = ar.Distance;
		if (d >= 5f) return;
		Tile tile = GameManager.TileBoard[ar[col].index];
		if (d > 1f)
		{
			if (d < (ExpansionGameplay.SourceNumber == 20 ? 5f : 2.5f) && !ar.IsPlayed && ar.FirstCheck(col))
			{
				tile.PlaySound(); tile.newAnimTrasFromAnimation.Play("Take 001"); ar.IsPlayed = true;
			}
		}
		else if (d < 1f && d > 0f)
		{
			if (ar.SecondCheck(col)) foreach (Transform particle in tile.extendPartices) particle.gameObject.SetActive(true);
		}
		else if (d < -8f) foreach (Transform particle in tile.extendPartices) particle.gameObject.SetActive(false);
	}
	private static void ExpansionDestroy(int row, int col, float play, string clip)
	{
		AnimatedRow ar = animatedRows[row];
		float d = ar.Distance;
		if (d >= 13f) return;
		Tile tile = GameManager.TileBoard[ar[col].index];
		if (d > -5f)
		{
			if (d < play && !ar.IsPlayed && ar.FirstCheck(col))
			{
				tile.newAnimTrasFromAnimation.Play(clip); tile.newAnimTrasFromAnimation.gameObject.SetActive(true); ar.IsPlayed = true;
			}
			if (d <= 0f && d >= -1f && tile.TileIndex == Ball.TilePosition.x && Ball.breakEnable && !tile.isBroken)
			{
				tile.isBroken = true;
				if (tile.newAnimTrasFromAnimation != null) tile.newAnimTrasFromAnimation.gameObject.SetActive(false);
				tile.TileParticle.gameObject.SetActive(true); tile.TileParticle.Play(); tile.PlaySound();
				GameManager.theBall.ShowShockWave(); ShakeCamera(0.5f);
			}
		}
		else if (d < -5f) ResetAnimation(tile.newAnimTrasFromAnimation);
	}
	private static void MoveExpansionJump(int row, int col, bool left)
	{
		float distance = animatedRows[row].Distance;
		if (distance > 16f) return;
		float phase = distance / 8f - Mathf.FloorToInt(distance / 8f);
		float amount = phase > 0.5f ? (1f - phase) * 2f : phase * 2f;
		TransformUtils.SetX(GameManager.TileBoard[animatedRows[row][col].index].GeoTransform, left ? amount : -amount, true);
	}
	private static void SpinExpansionCrown(int row, int col)
	{
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		if (tile.IsStarJumpSlider) MoveExpansionJump(row, col, tile.direction == Direction.Left);
		float d = animatedRows[row].Distance;
		if (d >= 15f) return;
		if (d < 15f && d > -5f)
		{
			if (animatedRows[row].FirstCheck(col)) tile.AdditionTransform.gameObject.SetActive(true);
			if (!ClassicCrownRandom.Enabled && tile.PickupTransform != null) tile.PickupTransform.Rotate(Vector3.up * 20f * Time.deltaTime * 6f);
		}
		else if (d < -5f) tile.AdditionTransform.gameObject.SetActive(false);
	}
}
