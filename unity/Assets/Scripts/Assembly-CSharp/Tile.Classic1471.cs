using UnityEngine;

// The 1.4.7.1 City/Sci-Tech/Tetris additions. Existing enum ordinals and
// first-five enemy configuration remain in Tile.cs.
public partial class Tile
{
	public enum DangerSpreadType
	{
		Null,
		OneSide,
		BothSides,
		Area,
		Cross,
		Corners,
		Connected,
		Offset,
		TopLeftRight,
		BottomLeftRight,
		Triangle,
		Cube4,
		Cube6,
		Cube9,
		Halfround,
		Roller
	}

	public Animation newAnimTrasFromAnimation;
	public ParticleSystem TileParticle;
	public ParticleSystem AnimParticleSystem;
	public float turret_laser_z_scale;
	public bool IsTurret { get; private set; }
	public bool IsDangerHeatSpreader { get; private set; }
	public DangerSpreadType dangerSpreadType { get; private set; }
	public int dangerSpreadLength { get; private set; }

	private static PrefabName ClassicPrefab(PrefabName prefab)
	{
		if (!LevelDesigner.IsClassicWorld)
		{
			return prefab;
		}
		// Generation can be ahead of the displayed theme. These are donor IDs
		// obtained through the theme owner's mapping, not baseline theme slots.
		int theme = LevelDesigner.GeneratingDonorThemeNumber;
		bool tetris = theme == 12 || theme == 13;
		bool forest = theme == 14;
		if (!tetris && !forest)
		{
			return prefab;
		}
		switch (prefab)
		{
		case PrefabName.TreeSmall1: return forest ? PrefabName.Them14TreeSmall1 : PrefabName.Lv8TreeSmall1;
		case PrefabName.TreeSmall2: return forest ? PrefabName.Them14TreeSmall2 : PrefabName.Lv8TreeSmall2;
		case PrefabName.TreeTall: return forest ? PrefabName.Them14TreeTall : PrefabName.Lv8TreeTall;
		case PrefabName.Shinto: return forest ? PrefabName.Them14Shinto : PrefabName.Lv8Shinto;
		case PrefabName.Switcher: return PrefabName.Lv8Switcher;
		case PrefabName.Pillar: return PrefabName.Lv8Pillar;
		default: return prefab;
		}
	}

	private bool ConfigureClassicEnemy()
	{
		if (LevelDesigner.IsClassicWorld)
		{
			// Donor stores the footprint before neighboring RadioTower/Turret
			// fields can replace dangerType with Heat. Retain baseline model setup,
			// but don't infer these spread lengths later from a mutated dangerType.
			switch (dangerType)
			{
			case DangerType.Binary:
			case DangerType.BinaryOffset:
			case DangerType.BinaryMini:
			case DangerType.BinaryMiniOffset:
				dangerSpreadType = DangerSpreadType.OneSide;
				dangerSpreadLength = dangerType == DangerType.Binary ? 2 : (dangerType == DangerType.BinaryMiniOffset ? 0 : 1);
				break;
			}
			if ((dangerType == DangerType.TallTree || dangerType == DangerType.TallTreeSlider)
				&& !LevelDesigner.GenerateTowers && (LevelDesigner.GenerateTeslas || LevelDesigner.GenerateBuildings))
			{
				IsSlider = dangerType == DangerType.TallTreeSlider;
				if (LevelDesigner.GenerateTeslas)
				{
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.TeslaTower);
					animType = IsSlider ? AnimType.TeslaSlider : AnimType.Tesla;
					HasAltAudio = true;
				}
				else
				{
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Building);
					animType = IsSlider ? AnimType.BuildingSlider : AnimType.Building;
				}
				HasAdditionBase = true;
				IsAdditionSwitchable = false;
				return true;
			}
			if (LevelDesigner.GenerateFloodLights)
			{
				if (dangerType == DangerType.Laser || dangerType == DangerType.LaserSlider)
				{
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.FloodLaser);
					IsSlider = dangerType == DangerType.LaserSlider;
					animType = IsSlider ? AnimType.FloodLaserSlider : AnimType.FloodLaser;
					HasAltAudio = true;
					return true;
				}
				if (dangerType == DangerType.Spotlight || dangerType == DangerType.SpotlightSlider)
				{
					AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.FloodLight);
					IsSlider = dangerType == DangerType.SpotlightSlider;
					animType = IsSlider ? AnimType.SpotlightSlider : AnimType.Spotlight;
					HasAudio = false;
					return true;
				}
			}
		}
		switch (dangerType)
		{
		case DangerType.RadioTower:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.RadioTower);
			animType = AnimType.RadioTower;
			IsDangerSpreader = true;
			IsDangerHeatSpreader = true;
			dangerSpreadLength = 1;
			dangerSpreadType = DangerSpreadType.Cross;
			HasAdditionBase = true;
			return true;
		case DangerType.Turret:
		case DangerType.TurretOffset:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.Turret);
			animType = AnimType.Turret;
			IsDangerSpreader = true;
			IsDangerHeatSpreader = true;
			IsTurret = true;
			dangerSpreadLength = 1;
			dangerSpreadType = DangerSpreadType.Connected;
			HasAltAudio = true;
			if (dangerType == DangerType.TurretOffset)
			{
				switch ((TileVal)TypeVal)
				{
				case TileVal.EnemyTurretLeftOffsetLeft:
				case TileVal.EnemyTurretRightOffsetLeft:
				case TileVal.EnemyTurretFragileLeftOffsetLeft:
				case TileVal.EnemyTurretFragileRightOffsetLeft:
					AdditionOffset = -1;
					break;
				case TileVal.EnemyTurretLeftOffsetRight:
				case TileVal.EnemyTurretRightOffsetRight:
				case TileVal.EnemyTurretFragileLeftOffsetRight:
				case TileVal.EnemyTurretFragileRightOffsetRight:
					AdditionOffset = 1;
					break;
				}
			}
			return true;
		case DangerType.Switcher:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, ClassicPrefab(PrefabName.Switcher));
			animType = AnimType.Switcher;
			IsDangerSpreader = true;
			dangerSpreadType = DangerSpreadType.Offset;
			dangerSpreadLength = 1;
			HasAudio = false;
			return true;
		case DangerType.ShintoSafe:
		case DangerType.ShintoUnsafe:
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, ClassicPrefab(PrefabName.Shinto));
			HasAdditionBase = true;
			IsDangerSpreader = true;
			dangerSpreadType = dangerType == DangerType.ShintoSafe ? DangerSpreadType.TopLeftRight : DangerSpreadType.BottomLeftRight;
			animType = dangerType == DangerType.ShintoSafe ? AnimType.ShintoRise : AnimType.ShintoFall;
			dangerSpreadLength = 1;
			HasAltAudio = true;
			return true;
		case DangerType.TetrisDownOneLine01:
			ConfigureTetris(PrefabName.TetrisDownOneLine01, AnimType.TetrisDownOneLine01, 1, true, true);
			return true;
		case DangerType.TetrisDownOneLine02:
			ConfigureTetris(PrefabName.TetrisDownOneLine02, AnimType.TetrisDownOneLine02, 1, true, true);
			return true;
		case DangerType.TetrisDownOneLine03:
			ConfigureTetris(PrefabName.TetrisDownOneLine03, AnimType.TetrisDownOneLine03, 0, true, true);
			return true;
		case DangerType.TetrisDownOneLine04:
			ConfigureTetris(PrefabName.TetrisDownOneLine04, AnimType.TetrisDownOneLine04, 1, true, true);
			return true;
		case DangerType.TetrisDownOneLine05:
			ConfigureTetris(PrefabName.TetrisDownOneLine05, AnimType.TetrisDownOneLine05, 0, false, true);
			return true;
		case DangerType.TetrisDownOneLine06:
			ConfigureTetris(PrefabName.TetrisDownOneLine06, AnimType.TetrisDownOneLine06, 0, false, true);
			return true;
		case DangerType.TetrisDownOneLine07:
			ConfigureTetris(PrefabName.TetrisDownOneLine07, AnimType.TetrisDownOneLine07, 0, false, true);
			return true;
		case DangerType.TetrisDownOneLine08:
			ConfigureTetris(PrefabName.TetrisDownOneLine08, AnimType.TetrisDownOneLine08, 1, false, true);
			return true;
		case DangerType.OneBlockInToOffD:
			ConfigureTetris(PrefabName.OneBlockInToOffD, AnimType.OneBlockInToOffD, 0, false, false);
			return true;
		case DangerType.OneBlockInToOff:
			ConfigureTetris(PrefabName.OneBlockInToOff, AnimType.OneBlockInToOff, 0, false, false);
			return true;
		case DangerType.OneBlockOffToInD:
			ConfigureTetris(PrefabName.OneBlockOffToInD, AnimType.OneBlockOffToInD, 0, false, false);
			return true;
		default:
			return false;
		}
	}

	private void ConfigureTetris(PrefabName prefab, AnimType animation, int spreadLength, bool heat, bool altAudio)
	{
		AdditionPrefabID = new PrefabID(PrefabType.Enemy, prefab);
		animType = animation;
		IsDangerSpreader = true;
		dangerSpreadType = DangerSpreadType.OneSide;
		IsDangerHeatSpreader = heat;
		dangerSpreadLength = spreadLength;
		HasAudio = true;
		HasAltAudio = altAudio;
	}

	private bool CreateClassicAnimator()
	{
		// Root tiles are pooled independently of their enemy geometry. Unlike the
		// old animations, RadioTower/Turret/Tetris do not all bind AnimTransform;
		// don't let CreateAnimator's shared epilogue move a previous pool occupant.
		AnimTransform = null;
		AltAnimTransform = null;
		AnimPartTransforms = null;
		switch (animType)
		{
		case AnimType.Building:
		case AnimType.BuildingSlider:
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform, true);
			AnimTransform = AnimPartTransforms[0];
			foreach (Transform part in AnimPartTransforms)
			{
				part.localPosition = Vector3.zero;
			}
			AdditionTransform.eulerAngles = Vector3.zero;
			return true;
		case AnimType.FloodLaser:
		case AnimType.FloodLaserSlider:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			AnimPartTransforms = TransformUtils.FindAll("part", AdditionTransform);
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			foreach (Transform part in AnimPartTransforms)
			{
				part.localEulerAngles = Vector3.zero;
			}
			AltAnimTransform.GetComponent<ParticleEmitter>().emit = false;
			AltAnimTransform.GetComponent<ParticleAnimator>().rndForce = Vector3.zero;
			return true;
		case AnimType.Tesla:
		case AnimType.TeslaSlider:
		{
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			Transform tower = null;
			foreach (Transform part in AnimTransform)
			{
				if (part.name == "tesla_tower") tower = part;
				if (part.name == "radio_pulse") AnimParticleSystem = part.GetChild(0).GetComponent<ParticleSystem>();
			}
			AnimPartTransforms = new Transform[] { tower, AltAnimTransform.GetChild(0), AltAnimTransform.GetChild(1), AltAnimTransform.GetChild(2) };
			AnimPartTransforms[0].localPosition = new Vector3(0f, -1.1f, 0f);
			AnimPartTransforms[1].localPosition = new Vector3(0f, -0.2f, 0f);
			AnimPartTransforms[2].localPosition = new Vector3(0f, -0.55f, 0f);
			AnimPartTransforms[3].localPosition = new Vector3(0f, -0.9f, 0f);
			AnimParticleSystem.gameObject.SetActive(false);
			return true;
		}
		case AnimType.RadioTower:
			AdditionTransform.eulerAngles = new Vector3(270f, 0f, 0f);
			foreach (Transform part in TransformUtils.FindAll("RadioPulse", AdditionTransform)[0])
			{
				if (part.name == "Particle System") TileParticle = part.GetComponent<ParticleSystem>();
			}
			return true;
		case AnimType.Turret:
			AnimPartTransforms = TransformUtils.FindAll("anim", AdditionTransform);
			if (direction == Direction.Left) AnimPartTransforms[0].eulerAngles = new Vector3(0f, 0f, -90f);
			else if (direction == Direction.Right) AnimPartTransforms[0].eulerAngles = new Vector3(0f, 0f, 90f);
			foreach (Transform part in AnimPartTransforms[0])
			{
				if (part.name == "laser")
				{
					part.localScale = new Vector3(1f, 1f, turret_laser_z_scale);
					part.localPosition = new Vector3(0f, 0f, -0.3f);
					part.GetComponent<Renderer>().enabled = false;
					AltAnimTransform = part;
				}
				if (part.name == "particles")
				{
					AnimParticleSystem = part.GetComponent<ParticleSystem>();
					AnimParticleSystem.time = 0f;
					part.gameObject.SetActive(false);
				}
				if (part.name == "turret_body") part.localPosition = new Vector3(0f, -0.03f, 0.3f);
			}
			return true;
		case AnimType.Switcher:
			AnimTransform = TransformUtils.FindAll("anim", AdditionTransform)[0];
			RootTransform = AnimTransform.parent.parent.parent;
			return true;
		case AnimType.ShintoRise:
		case AnimType.ShintoFall:
			foreach (Transform part in TransformUtils.FindAll("anim", AdditionTransform)[0])
			{
				if (part.name == "part1")
				{
					AnimTransform = part;
					foreach (Transform child in part)
					{
						if (child.name == "part2") { AltAnimTransform = child; break; }
					}
				}
				part.eulerAngles = new Vector3(270f, 0f, 0f);
			}
			return true;
		case AnimType.TetrisDownOneLine01:
		case AnimType.TetrisDownOneLine02:
		case AnimType.TetrisDownOneLine03:
		case AnimType.TetrisDownOneLine04:
		case AnimType.TetrisDownOneLine05:
		case AnimType.TetrisDownOneLine06:
		case AnimType.TetrisDownOneLine07:
		case AnimType.TetrisDownOneLine08:
			GeoTransform.eulerAngles = Vector3.zero;
			BindTetrisAnimation(true);
			return true;
		case AnimType.OneBlockInToOffD:
		case AnimType.OneBlockInToOff:
		case AnimType.OneBlockOffToInD:
			AdditionTransform.localEulerAngles = -GeoTransform.eulerAngles;
			BindTetrisAnimation(false);
			return true;
		default:
			return false;
		}
	}

	private void BindTetrisAnimation(bool sample)
	{
		foreach (Transform part in TransformUtils.FindAll("nani", AdditionTransform)[0])
		{
			if (part.name == "part1")
			{
				newAnimTrasFromAnimation = part.GetComponent<Animation>();
				if (sample) newAnimTrasFromAnimation.Sample();
			}
			if (part.name == "Particle") TileParticle = part.GetComponent<ParticleSystem>();
		}
	}

	private bool TryClassicKillBall(DangerType hazard, bool jumping, Vector3 ballPos, out bool kill)
	{
		kill = false;
		switch (hazard)
		{
		case DangerType.RadioTower:
			kill = !jumping || (GeoTransform != null && Mathf.Abs(GeoTransform.position.y - ballPos.y) <= 0.2f);
			return true;
		case DangerType.Turret:
		case DangerType.TurretOffset:
		case DangerType.Switcher:
		case DangerType.OneBlockInToOffD:
		case DangerType.OneBlockOffToInD:
			kill = !jumping;
			return true;
		case DangerType.OneBlockInToOff:
			return true;
		case DangerType.ShintoSafe:
		case DangerType.ShintoUnsafe:
		case DangerType.TetrisDownOneLine01:
		case DangerType.TetrisDownOneLine02:
		case DangerType.TetrisDownOneLine03:
		case DangerType.TetrisDownOneLine04:
		case DangerType.TetrisDownOneLine05:
		case DangerType.TetrisDownOneLine06:
		case DangerType.TetrisDownOneLine07:
		case DangerType.TetrisDownOneLine08:
			kill = true;
			return true;
		default:
			return false;
		}
	}
}
