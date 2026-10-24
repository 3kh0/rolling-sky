using UnityEngine;

// Distance windows, easing, clips and hazard footprints are from the original
// 1.4.7.1 TileAnimator. No Unity Animator/controller or substitute geometry.
public partial class TileAnimator
{
	private static void AnimateClassicTile(int row, int col)
	{
		switch (animatedRows[row][col].animType)
		{
		case Tile.AnimType.Building: RiseBuilding(row, col); break;
		case Tile.AnimType.BuildingSlider: SlideSlider(row, col); RiseBuilding(row, col); break;
		case Tile.AnimType.FloodLaser: FireFloodLaser(row, col); break;
		case Tile.AnimType.FloodLaserSlider: SlideSlider(row, col); FireFloodLaser(row, col); break;
		case Tile.AnimType.Tesla: MoveTesla(row, col); break;
		case Tile.AnimType.TeslaSlider: SlideSlider(row, col); MoveTesla(row, col); break;
		case Tile.AnimType.RadioTower: RadioPulse(row, col); break;
		case Tile.AnimType.Turret: FireTurret(row, col); break;
		case Tile.AnimType.Switcher: MoveSwitcher(row, col); break;
		case Tile.AnimType.ShintoRise: RiseShinto(row, col); break;
		case Tile.AnimType.ShintoFall: FallShinto(row, col); break;
		case Tile.AnimType.TetrisDownOneLine01:
		case Tile.AnimType.TetrisDownOneLine02:
		case Tile.AnimType.TetrisDownOneLine03:
		case Tile.AnimType.TetrisDownOneLine04:
		case Tile.AnimType.TetrisDownOneLine05:
		case Tile.AnimType.TetrisDownOneLine06:
		case Tile.AnimType.TetrisDownOneLine07:
		case Tile.AnimType.TetrisDownOneLine08:
			TetrisDownOneLine(row, col);
			break;
		case Tile.AnimType.OneBlockInToOffD:
		case Tile.AnimType.OneBlockInToOff:
			OneBlockInToOff(row, col);
			break;
		case Tile.AnimType.OneBlockOffToInD:
			OneBlockOffToInD(row, col);
			break;
		}
	}

	private static void RiseBuilding(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		if (distance > 15f) return;
		bool below, above;
		float value = MathUtils.ToPercent01(distance, 12f, 6f, out below, out above);
		if (below) return;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		for (int i = 0; i < tile.AnimPartTransforms.Length; i++)
		{
			float maximum;
			switch (i)
			{
			case 0: maximum = 0.685f; break;
			case 1: maximum = 0.88f; break;
			case 2: maximum = 0.505f; break;
			case 3: maximum = 1.559f; break;
			case 4: maximum = 0.723f; break;
			default:
				Debug.LogWarning("Tile Animator: Unhandled building part " + i);
				maximum = 0f;
				break;
			}
			float height = MathUtils.FromPercent(FloatAnim.ImpactLate(value, 0.1f), 0f, maximum);
			TransformUtils.SetY(tile.AnimPartTransforms[i], height, true);
		}
		if (above)
		{
			tile.PlaySound();
			completedTiles.Add(col);
		}
	}

	private static void RadioPulse(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		if (distance > 0f)
		{
			if (distance < 10f)
			{
				tile.PlaySound();
				completedTiles.Add(col);
			}
			else if (animatedRows[row].FirstCheck(col))
			{
				tile.TileParticle.gameObject.SetActive(true);
				tile.TileParticle.Play();
			}
		}
		if (distance <= -2f)
		{
			tile.TileParticle.Clear();
			tile.TileParticle.Stop();
			tile.TileParticle.gameObject.SetActive(false);
			completedTiles.Add(col);
		}
	}

	private static void MoveTesla(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		Transform[] parts = tile.AnimPartTransforms;
		ParticleSystem effect = tile.AnimParticleSystem;
		Transform target = tile.AdditionTransform;
		if (Mathf.Floor(target.parent.rotation.eulerAngles.z) == 90f)
		{
			target.eulerAngles = new Vector3(180f, 0f, -90f);
		}
		target.rotation = default(Quaternion);
		if (Mathf.Floor(target.parent.parent.rotation.eulerAngles.z) == 180f)
		{
			target.eulerAngles = new Vector3(0f, 270f, 0f);
		}
		if (distance < 0f)
		{
			effect.Stop();
			completedTiles.Add(col);
		}
		else if (distance >= 14f)
		{
			if (effect.gameObject.activeSelf) effect.gameObject.SetActive(false);
			parts[0].localPosition = new Vector3(0f, -1.1f, 0f);
			parts[1].localPosition = new Vector3(0f, -0.2f, 0f);
			parts[2].localPosition = new Vector3(0f, -0.55f, 0f);
			parts[3].localPosition = new Vector3(0f, -0.9f, 0f);
		}
		else if (distance >= 8f)
		{
			if (effect.gameObject.activeSelf) effect.gameObject.SetActive(false);
			if (animatedRows[row].FirstCheck(col)) tile.PlayAltSound();
			float value = FloatAnim.Smooth((distance - 8f) / 6f);
			parts[0].localPosition = new Vector3(0f, Mathf.Lerp(0f, -1.1f, value), 0f);
			parts[1].localPosition = new Vector3(0f, -0.2f, 0f);
			parts[2].localPosition = new Vector3(0f, -0.55f, 0f);
			parts[3].localPosition = new Vector3(0f, -0.9f, 0f);
		}
		else if (distance >= 6f)
		{
			if (!effect.gameObject.activeSelf)
			{
				effect.gameObject.SetActive(true);
				effect.Play();
			}
			if (animatedRows[row].SecondCheck(col))
			{
				tile.AltAudioPlayer.Stop();
				tile.PlayAltSound();
			}
			parts[0].localPosition = Vector3.zero;
			float value = FloatAnim.ImpactLate((distance - 6f) / 2f);
			parts[1].localPosition = new Vector3(0f, Mathf.Lerp(0f, -0.2f, value), 0f);
			parts[2].localPosition = new Vector3(0f, Mathf.Lerp(0f, -0.55f, value), 0f);
			parts[3].localPosition = new Vector3(0f, Mathf.Lerp(0f, -0.9f, value), 0f);
		}
		else
		{
			if (!effect.gameObject.activeSelf) effect.gameObject.SetActive(true);
			parts[0].localPosition = Vector3.zero;
			parts[1].localPosition = Vector3.zero;
			parts[2].localPosition = Vector3.zero;
			parts[3].localPosition = Vector3.zero;
		}
	}

	private static void FireTurret(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		ParticleSystem effect = tile.AnimParticleSystem;
		Transform laser = tile.AltAnimTransform;
		TileVal typeVal = (TileVal)tile.TypeVal;
		Quaternion rotation = laser.localRotation;
		// This is the same transform assigned to baseline CameraControl's private
		// mainCamGroup in Setup; no camera-controller API change is required.
		Vector3 offset = Camera.main.transform.parent.parent.position - laser.position;
		Vector2 cameraDirection = new Vector2(offset.y, offset.z).normalized;
		if (typeVal != TileVal.EnemyTurretRight && typeVal != TileVal.EnemyTurretFragileRight &&
			typeVal != TileVal.EnemyTurretRightOffsetRight && typeVal != TileVal.EnemyTurretRightOffsetLeft &&
			typeVal != TileVal.EnemyTurretFragileRightOffsetRight && typeVal != TileVal.EnemyTurretFragileRightOffsetLeft)
		{
			rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, -Mathf.Atan2(cameraDirection.x, cameraDirection.y) * 57.29578f, rotation.eulerAngles.z);
			laser.localScale = new Vector3(laser.localScale.x, -1f, laser.localScale.z);
		}
		else
		{
			rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, Mathf.Atan2(cameraDirection.x, cameraDirection.y) * 57.29578f - 180f, rotation.eulerAngles.z);
		}
		laser.localRotation = rotation;
		if (distance < 9f && distance >= 7f)
		{
			if (animatedRows[row].FirstCheck(col))
			{
				tile.AudioPlayer.pitch = 1f + Random.Range(0f, 0.1f) - 0.05f;
				tile.PlaySound();
			}
			foreach (Transform part in tile.AnimPartTransforms[0])
			{
				if (part.name == "turret_body")
				{
					float value = FloatAnim.Impact((distance - 7f) / 2f, 0.01f);
					part.localPosition = new Vector3(0f, -0.03f, Mathf.Lerp(0f, 0.3f, value));
					break;
				}
			}
		}
		if (distance < 5f && distance > 4f)
		{
			if (animatedRows[row].SecondCheck(col))
			{
				tile.AltAudioPlayer.pitch = 1f + Random.Range(0f, 0.1f) - 0.05f;
				tile.PlayAltSound();
			}
			if (!effect.gameObject.activeSelf)
			{
				effect.gameObject.SetActive(true);
				effect.Play();
			}
			foreach (Transform part in tile.AnimPartTransforms[0])
			{
				if (part.name == "laser") part.GetComponent<Renderer>().enabled = true;
				if (part.name == "turret_body") part.localPosition = new Vector3(0f, -0.03f, 0f);
			}
		}
		else if (distance < -2f)
		{
			effect.gameObject.SetActive(false);
		}
	}

	private static void ResetAnimation(Animation animation)
	{
		animation.gameObject.transform.localPosition = Vector3.zero;
		animation.gameObject.SetActive(false);
	}

	// The eight donor methods are identical except 08 re-enables part1 at the
	// second stage. Their distinct original AnimationClips supply the geometry.
	private static void TetrisDownOneLine(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		Animation animation = tile.newAnimTrasFromAnimation;
		if (distance < 13f && distance > 11f)
		{
			if (animatedRows[row].FirstCheck(col))
			{
				animation.gameObject.SetActive(true);
				animation.Play("DownPartOne");
				tile.PlaySound();
			}
		}
		else if (distance < 6f && distance > -5f)
		{
			if (animatedRows[row].SecondCheck(col))
			{
				if (animatedRows[row][col].animType == Tile.AnimType.TetrisDownOneLine08) animation.gameObject.SetActive(true);
				animation.Play("DownPartTwo");
				tile.TileParticle.gameObject.SetActive(true);
				tile.TileParticle.Play();
				tile.PlayAltSound();
			}
			if (distance < -2f)
			{
				tile.TileParticle.Stop();
				tile.TileParticle.gameObject.SetActive(false);
				ResetAnimation(animation);
				completedTiles.Add(col);
			}
		}
		else if (distance > 14f)
		{
			ResetAnimation(animation);
		}
	}

	private static void OneBlockInToOff(int row, int col)
	{
		AnimateSingleTetrisBlock(row, col, 10f, 8f, -7f);
	}

	private static void OneBlockOffToInD(int row, int col)
	{
		AnimateSingleTetrisBlock(row, col, 12f, 10f, -9f);
	}

	private static void AnimateSingleTetrisBlock(int row, int col, float soundStart, float effectStart, float end)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		if (distance < soundStart && distance > effectStart)
		{
			if (animatedRows[row].FirstCheck(col)) tile.PlaySound();
		}
		else if (distance < effectStart && distance > end)
		{
			Animation animation = tile.newAnimTrasFromAnimation;
			if (animatedRows[row].SecondCheck(col))
			{
				animation.gameObject.SetActive(true);
				animation.gameObject.transform.localPosition = new Vector3(0f, 0f, -50f);
				tile.TileParticle.gameObject.SetActive(true);
				tile.TileParticle.Play();
			}
			if (distance < -2f)
			{
				tile.TileParticle.Stop();
				tile.TileParticle.gameObject.SetActive(false);
				ResetAnimation(animation);
				completedTiles.Add(col);
			}
		}
		else if (distance > 12f)
		{
			ResetAnimation(tile.newAnimTrasFromAnimation);
		}
	}

	private static void MoveSwitcher(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		Transform target = tile.AnimTransform;
		Quaternion rotation = Quaternion.Inverse(tile.RootTransform.localRotation);
		if (distance > 5f)
		{
			target.localPosition = new Vector3(0f, 0f, -1f);
		}
		else if (distance < 5f && distance >= 3.5f)
		{
			float value = (distance - 3.5f) / 1.5f;
			if (tile.direction == Direction.Left) target.localPosition = rotation * new Vector3(Mathf.Lerp(-1f, 0f, value), 0f, -1f);
			if (tile.direction == Direction.Right) target.localPosition = rotation * new Vector3(Mathf.Lerp(1f, 0f, value), 0f, -1f);
		}
		else if (distance < 3.5f)
		{
			if (tile.direction == Direction.Left) target.localPosition = rotation * new Vector3(-1f, 0f, -1f);
			if (tile.direction == Direction.Right) target.localPosition = rotation * new Vector3(1f, 0f, -1f);
		}
	}

	private static void RiseShinto(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		Transform part1 = tile.AnimTransform;
		Transform part2 = tile.AltAnimTransform;
		if (distance > 7f)
		{
			part1.localPosition = new Vector3(0f, 0f, 0.6f);
			part2.localPosition = new Vector3(0f, -1f, 0f);
		}
		else if (distance < 7f && distance >= 5f)
		{
			if (animatedRows[row].FirstCheck(col)) tile.PlaySound();
			part1.localPosition = new Vector3(0f, 0f, Mathf.Lerp(0f, 0.6f, (distance - 5.5f) / 1.5f));
		}
		else if (distance < 4.5f && distance >= 3f)
		{
			if (animatedRows[row].SecondCheck(col)) tile.PlayAltSound();
			part1.localPosition = Vector3.zero;
			part2.localPosition = new Vector3(0f, Mathf.Lerp(0f, -1f, (distance - 3f) / 1.5f), 0f);
		}
		else if (distance < 3f)
		{
			part1.localPosition = Vector3.zero;
			part2.localPosition = Vector3.zero;
		}
	}

	private static void FallShinto(int row, int col)
	{
		float distance = animatedRows[row].Distance;
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		Transform part1 = tile.AnimTransform;
		Transform part2 = tile.AltAnimTransform;
		if (distance > 7f)
		{
			part1.localPosition = Vector3.zero;
			part2.localPosition = Vector3.zero;
		}
		else if (distance < 7f && distance >= 5f)
		{
			if (animatedRows[row].FirstCheck(col)) tile.PlayAltSound();
			part2.localPosition = new Vector3(0f, Mathf.Lerp(-1f, 0f, (distance - 5.5f) / 1.5f), 0f);
		}
		else if (distance < 4.5f && distance >= 3f)
		{
			if (animatedRows[row].SecondCheck(col)) tile.PlaySound();
			part1.localPosition = new Vector3(0f, 0f, Mathf.Lerp(0.6f, 0f, (distance - 3f) / 1.5f));
			part2.localPosition = new Vector3(0f, -1f, 0f);
		}
		else if (distance < 3f)
		{
			part1.localPosition = new Vector3(0f, 0f, 0.6f);
			part2.localPosition = new Vector3(0f, -1f, 0f);
		}
	}

	private static void FireFloodLaser(int row, int col)
	{
		bool below, above;
		float value = MathUtils.ToPercent01(animatedRows[row].Distance, 15f, 5f, out below, out above);
		Tile tile = GameManager.TileBoard[animatedRows[row][col].index];
		if (!below)
		{
			if (above)
			{
				if (animatedRows[row].SecondCheck(col))
				{
					tile.SwitchAddition();
					tile.PlayAltSound();
					tile.AltAnimTransform.GetComponent<ParticleEmitter>().emit = false;
					tile.AltAnimTransform.GetComponent<ParticleAnimator>().rndForce = new Vector3(100f, 0f, 100f);
					// Donor also requests a post effect, but ActivateEffect always
					// returns because HighEffectDeviceCheck is hard-coded true.
					ShakeCamera(0.8f);
					MathUtils.IntTrio origin = animatedRows[row][col].index;
					for (int i = 0; i < 15; i++)
					{
						MathUtils.IntTrio index = new MathUtils.IntTrio(origin.x, origin.y - i, origin.z);
						if (GameManager.TileBoard.IsValid(index))
						{
							if (GameManager.TileBoard[index] == null) GameManager.TileBoard[index] = LevelConstructor.CreateFakeGroundTile(origin, index);
							GameManager.TileBoard[index].SetAsDangerous(0.5f, null, Tile.DangerType.Heat);
						}
					}
				}
				float opening = MathUtils.ToPercent01(animatedRows[row].Distance, 5f, 4.5f, out below, out above);
				float flap = 1f;
				if (above)
				{
					float retract = MathUtils.ToPercent01(animatedRows[row].Distance, 4.5f, 0f, out below, out above);
					TransformUtils.SetZ(tile.AnimTransform, FloatAnim.Smooth(MathUtils.Inverse(retract), true, true), true);
				}
				else
				{
					flap = FloatAnim.Smooth(opening, false, true);
					TransformUtils.SetZ(tile.AnimTransform, flap, true);
				}
				foreach (Transform part in tile.AnimPartTransforms) part.localEulerAngles = new Vector3(180f * flap, 0f, 0f);
			}
			else
			{
				ParticleEmitter emitter = tile.AltAnimTransform.GetComponent<ParticleEmitter>();
				if (animatedRows[row].FirstCheck(col))
				{
					tile.PlaySound();
					emitter.emit = true;
				}
				float charge = FloatAnim.Smooth(value, true, false);
				TransformUtils.SetY(tile.AnimTransform, charge * 0.075f * FloatAnim.Wave(value * 15f, -1f, 1f, 0f), true);
				emitter.maxEmission = emitter.minEmission = 50f * charge;
			}
		}
		float pan = GetPan(row, col, 3f);
		tile.AudioPlayer.panStereo = pan;
		tile.AltAudioPlayer.panStereo = pan;
	}
}
