using UnityEngine;

public partial class Tile
{
	public bool IsSuccessiveJump { get; private set; }
	public bool IsSimulationJump { get; private set; }

	private bool ConfigureLaterEnemy()
	{
		if (!ExpansionGameplay.Later) return false;
		switch (dangerType)
		{
		case DangerType.EnemyCube1: ExpansionRig(PrefabName.EnemyCube1, AnimType.EnemyCube1, true); break;
		case DangerType.EnemyCube3: ExpansionRig(PrefabName.EnemyCube3, AnimType.EnemyCube3, true); LaterSpread(DangerSpreadType.Triangle, 1); break;
		case DangerType.EnemyCube3Pillar: ExpansionRig(PrefabName.EnemyCube3Pillar, AnimType.EnemyCube3Pillar, true); break;
		case DangerType.EnemyCube4: ExpansionRig(PrefabName.EnemyCube4, AnimType.EnemyCube4, true); LaterSpread(DangerSpreadType.Cube4, 1); break;
		case DangerType.EnemyCube5: ExpansionRig(PrefabName.EnemyCube5, AnimType.EnemyCube5, true); break;
		case DangerType.EnemyCube6: ExpansionRig(PrefabName.EnemyCube6, AnimType.EnemyCube6, true); LaterSpread(DangerSpreadType.Cube6, 1); break;
		case DangerType.EnemyCube9: ExpansionRig(PrefabName.EnemyCube9, AnimType.EnemyCube9, true); LaterSpread(DangerSpreadType.Cube9, 2); break;
		case DangerType.EnemyHalfround: ExpansionRig(PrefabName.EnemyHalfround, AnimType.EnemyHalfround, true); LaterSpread(DangerSpreadType.Halfround, 2); break;
		case DangerType.EnemyLv20RollerLeft: ExpansionRig(PrefabName.EnemyLv20RollerLeft, AnimType.EnemyLv20Roller, true); LaterSpread(DangerSpreadType.Roller, 2); break;
		case DangerType.EnemyLv20Rollerright: ExpansionRig(PrefabName.EnemyLv20Rollerright, AnimType.EnemyLv20Roller, true); LaterSpread(DangerSpreadType.Roller, 2); break;
		case DangerType.TurnTile:
			ExpansionRig(PrefabName.TurnTile, AnimType.TurnTileCenter, true);
			switch ((TileVal)TypeVal)
			{
			case TileVal.TurnTileLeft1: animType = AnimType.TurnTileLeft1; break;
			case TileVal.TurnTileLeft2: animType = AnimType.TurnTileLeft2; break;
			case TileVal.TurnTileRight1: animType = AnimType.TurnTileRight1; break;
			case TileVal.TurnTileRight2: animType = AnimType.TurnTileRight2; break;
			}
			IsGeoHidden = true; break;
		case DangerType.EnemyLevel23TileDown: ExpansionRig(PrefabName.EnemyLevel23TileDown, AnimType.EnemyLevel23TileDown, true); IsGeoHidden = true; break;
		case DangerType.EnemyPoker1: ExpansionRig(PrefabName.EnemyPoker1, AnimType.EnemyPoker1, false); break;
		case DangerType.EnemyPoker2Left: ExpansionRig(PrefabName.EnemyPoker2Left, AnimType.EnemyPoker2Left, false); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyPoker2Right: ExpansionRig(PrefabName.EnemyPoker2Right, AnimType.EnemyPoker2Right, false); ExpansionSpread(DangerSpreadType.OneSide); break;
		case DangerType.EnemyPoker3Left: ExpansionRig(PrefabName.EnemyPoker3Left, AnimType.EnemyPoker3Left, false); ExpansionSpread(DangerSpreadType.BottomLeftRight); break;
		case DangerType.EnemyPoker3Right: ExpansionRig(PrefabName.EnemyPoker3Right, AnimType.EnemyPoker3Right, false); ExpansionSpread(DangerSpreadType.BottomLeftRight); break;
		case DangerType.EnemyPokerDoor: ExpansionRig(PrefabName.EnemyPokerDoor, AnimType.EnemyPokerDoor, false); ExpansionSpread(DangerSpreadType.TopLeftRight); break;
		case DangerType.EnemyPoker1Lean: ExpansionRig(PrefabName.EnemyPoker1Lean, AnimType.EnemyPoker1Lean, false); break;
		case DangerType.EnemyPokerTree: ExpansionRig(PrefabName.EnemyPokerTree, AnimType.EnemyPokerTree, false); break;
		case DangerType.EnemyPokerTreeSlider: ExpansionRig(PrefabName.EnemyPokerTree, AnimType.EnemyPokerTreeSlider, false); IsSlider = true; break;
		case DangerType.EnemySuccessiveJump: ExpansionRig(PrefabName.EnemySuccessiveJump, AnimType.Null, false); IsJump = IsSuccessiveJump = IsAdditionSwitchable = true; IsDangerous = false; break;
		case DangerType.EnemyMagicDove: ExpansionRig(PrefabName.EnemyMagicDove, AnimType.EnemyMagicDove, false); break;
		case DangerType.EnemyTileWhite: ExpansionRig(PrefabName.EnemyTileWhite, AnimType.EnemyTileWhite, false); IsGeoHidden = true; break;
		case DangerType.EnemyTileBlack: ExpansionRig(PrefabName.EnemyTileBlack, AnimType.EnemyTileBlack, false); IsGeoHidden = true; break;
		case DangerType.EnemyUmbrellaLeft: ExpansionRig(PrefabName.EnemyUmbrellaLeft, AnimType.EnemyUmbrellaLeft, false); break;
		case DangerType.EnemyUmbrellaRight: ExpansionRig(PrefabName.EnemyUmbrellaRight, AnimType.EnemyUmbrellaRight, false); break;
		case DangerType.EnemyBigJoker: ExpansionRig(PrefabName.EnemyBigJoker, AnimType.EnemyBigJoker, false); break;
		case DangerType.EnemySmallJoker: ExpansionRig(PrefabName.EnemySmallJoker, AnimType.EnemySmallJoker, false); break;
		case DangerType.EnemySoldierLeft: ExpansionRig(PrefabName.EnemySoldierLeft, AnimType.EnemySoldierLeft, false); break;
		case DangerType.EnemySoldierRight: ExpansionRig(PrefabName.EnemySoldierRight, AnimType.EnemySoldierRight, false); break;
		case DangerType.PickDownLv25Cube: ExpansionRig(PrefabName.PickDownLv25Cube, AnimType.PickDownLv25Cube, false); IsGeoHidden = true; break;
		case DangerType.PickDownLv25Cube1: ExpansionRig(PrefabName.PickDownLv25Cube1, AnimType.PickDownLv25Cube1, false); IsGeoHidden = true; break;
		case DangerType.EnemyLv25Brigde: ExpansionRig(PrefabName.EnemyLv25Brigde, AnimType.EnemyLv25Brigde, false); break;
		case DangerType.SimulationJumpTitle: IsDangerous = HasAddition = HasAudio = HasAltAudio = false; IsGeoHidden = IsJump = IsSimulationJump = true; break;
		case DangerType.SuperJumperFictitious: ExpansionRig(PrefabName.SuperJumperFictitious, AnimType.Null, true); IsJump = IsSuperJump = true; IsDangerous = false; break;
		case DangerType.EnemyLv25Sound: ExpansionRig(PrefabName.EnemyLv25Sound, AnimType.EnemyLv25Sound, false); break;
		case DangerType.EnemyLv25SoundLeft: ExpansionRig(PrefabName.EnemyLv25SoundLeft, AnimType.EnemyLv25SoundLeft, false); break;
		case DangerType.EnemyLv25SoundRight: ExpansionRig(PrefabName.EnemyLv25SoundRight, AnimType.EnemyLv25SoundRight, false); break;
		case DangerType.EnemyLv25Sound3: ExpansionRig(PrefabName.EnemyLv25Sound3, AnimType.EnemyLv25Sound3, false); break;
		case DangerType.EnemyLv25AirEye: ExpansionRig(PrefabName.EnemyLv25AirEye, AnimType.EnemyLv25AirEye, false); break;
		case DangerType.EnemyLv25CocountTree: ExpansionRig(PrefabName.EnemyLv25CocountTree, AnimType.EnemyLv25CocountTree, false); break;
		case DangerType.EnemyLv25XiaoBianLeft: ExpansionRig(PrefabName.EnemyLv25XiaoBianLeft, AnimType.EnemyLv25XiaoBianLeft, false); break;
		case DangerType.EnemyLv25XiaoBianRight: ExpansionRig(PrefabName.EnemyLv25XiaoBianRight, AnimType.EnemyLv25XiaoBianRight, false); break;
		case DangerType.EnemyLv25Tape: ExpansionRig(PrefabName.EnemyLv25Tape, AnimType.EnemyLv25Tape, false); break;
		case DangerType.EnemyLv25CubeLive: ExpansionRig(PrefabName.EnemyLv25CubeLive, AnimType.EnemyLv25CubeLive, false); IsGeoHidden = true; break;
		case DangerType.EnemyLv25CubeDie: ExpansionRig(PrefabName.EnemyLv25CubeDie, AnimType.EnemyLv25CubeDie, false); IsGeoHidden = true; break;
		case DangerType.EnemyLv25MoveDici: ExpansionRig(PrefabName.EnemyLv25MoveDici, AnimType.EnemyLv25MoveDici, false); IsGeoHidden = true; break;
		case DangerType.EnemyLv25DownLightning: ExpansionRig(PrefabName.EnemyLv25DownLightning, AnimType.EnemyLv25DownLightning, false); break;
		case DangerType.EnemyLv25Gyroscope: ExpansionRig(PrefabName.EnemyLv25Gyroscope, AnimType.EnemyLv25Gyroscope, false); break;
		case DangerType.EnemyLv25Spring: ExpansionRig(PrefabName.EnemyLv25Spring, AnimType.EnemyLv25Spring, false); break;
		default: return false;
		}
		return true;
	}
	private void LaterSpread(DangerSpreadType spread, int length)
	{
		ExpansionSpread(spread);
		dangerSpreadLength = length;
		IsDangerHeatSpreader = true;
	}
	private bool ConfigureLaterBean()
	{
		if (!ExpansionGameplay.Later) return false;
		PrefabName prefab;
		switch ((TileVal)TypeVal)
		{
		case TileVal.BeanLv25Accelerate: prefab = PrefabName.BeanLv25Accelerate; animType = AnimType.BigBean; break;
		case TileVal.BeanLv25Deceleration: prefab = PrefabName.BeanLv25Deceleration; animType = AnimType.BigBean; break;
		case TileVal.BeanLv25BrigdeUp: prefab = PrefabName.BeanLv25BrigdeUp; animType = AnimType.BeanLv25BrigdeUp; break;
		case TileVal.BeanLv25BrigdeDown: prefab = PrefabName.BeanLv25BrigdeDown; animType = AnimType.BeanLv25BrigdeDown; break;
		default: return false;
		}
		AdditionPrefabID = new PrefabID(PrefabType.Bean, prefab);
		HasAddition = true;
		HasAudio = HasAltAudio = IsAdditionSwitchable = false;
		return true;
	}
	private bool EatLaterBean()
	{
		if (!ExpansionGameplay.Later) return false;
		switch ((TileVal)TypeVal)
		{
		case TileVal.BeanLv25Accelerate: GameManager.theBall.mBallSpeedcoefficient = 2f; GameManager.theBall.AccelerateSound.Play(); break;
		case TileVal.BeanLv25Deceleration: GameManager.theBall.mBallSpeedcoefficient = 0.5f; GameManager.theBall.DecelerationSound.Play(); break;
		case TileVal.BeanLv25BrigdeUp: GameManager.theBall.state = Ball.State.BrideUp; break;
		case TileVal.BeanLv25BrigdeDown: GameManager.theBall.state = Ball.State.BrideDown; break;
		default: return false;
		}
		return true;
	}
	private bool CreateLaterAnimator()
	{
		if (!ExpansionGameplay.Later || animType < AnimType.EnemyCube1) return false;
		BindExpansionRig();
		AdditionTransform.eulerAngles = Vector3.zero;
		int y = Index.y, x = Index.z;
		switch (animType)
		{
		case AnimType.TurnTileCenter: newAnimTrasFromAnimation.Play("center_s"); break;
		case AnimType.TurnTileLeft1: newAnimTrasFromAnimation.Play("left1_s"); break;
		case AnimType.TurnTileLeft2: newAnimTrasFromAnimation.Play("left2_s"); break;
		case AnimType.TurnTileRight1: newAnimTrasFromAnimation.Play("right1_s"); break;
		case AnimType.TurnTileRight2: newAnimTrasFromAnimation.Play("right2_s"); break;
		case AnimType.EnemyLv25Sound3:
			SetAppointTileDanger(y, x); SetAppointTileDanger(y, x + 1); SetAppointTileDanger(y, x - 1); break;
		case AnimType.EnemyLv25SoundLeft:
			SetAppointTileDanger(y, x - 1); SetAppointTileDanger(y, x - 2); SetAppointTileDanger(y, x + 1); break;
		case AnimType.EnemyLv25SoundRight:
			SetAppointTileDanger(y, x + 1); SetAppointTileDanger(y, x + 2); SetAppointTileDanger(y, x - 1); break;
		case AnimType.EnemyLv25AirEye:
			SetAppointTileDanger(y + 2, x + 2); SetAppointTileDanger(y + 2, x - 2); SetAppointTileDanger(y - 2, x + 2); SetAppointTileDanger(y - 2, x - 2); break;
		case AnimType.EnemyLv25Gyroscope:
			if (direction == Direction.Right) SetAppointTileDanger(y, x - 1);
			else if (direction == Direction.Left) SetAppointTileDanger(y, x + 1);
			SetAppointTileDanger(y - 1, x);
			AnimTransform = TransformUtils.Find("anim", AdditionTransform); break;
		case AnimType.EnemyLv25Spring: for (int i = 1; i < 6; i++) SetAppointTileDanger(y + i, x); break;
		}
		return true;
	}
	private bool TryLaterKillBall(DangerType hazard, bool jumping, Vector3 position, out bool kill)
	{
		kill = false;
		if (!ExpansionGameplay.Later || hazard < DangerType.EnemyCube1) return false;
		switch (hazard)
		{
		case DangerType.EnemyCube1: case DangerType.EnemyPoker1: case DangerType.EnemyPoker2Left: case DangerType.EnemyPoker2Right:
		case DangerType.EnemyPoker3Left: case DangerType.EnemyPoker3Right: case DangerType.EnemyPoker1Lean:
		case DangerType.EnemyUmbrellaLeft: case DangerType.EnemyUmbrellaRight: case DangerType.EnemySoldierLeft: case DangerType.EnemySoldierRight:
		case DangerType.EnemyLv25Tape: kill = !jumping; break;
		case DangerType.EnemyCube5: case DangerType.EnemyPokerTree: case DangerType.EnemyPokerTreeSlider:
			kill = !jumping || (GeoTransform != null && Mathf.Abs(GeoTransform.position.y - position.y) <= 0.2f); break;
		case DangerType.EnemyCube3Pillar: case DangerType.EnemyCube4: case DangerType.EnemyMagicDove: case DangerType.EnemyTileBlack:
		case DangerType.EnemyBigJoker: case DangerType.EnemySmallJoker: case DangerType.EnemyLv25Sound:
		case DangerType.EnemyLv25XiaoBianLeft: case DangerType.EnemyLv25XiaoBianRight: case DangerType.EnemyLv25MoveDici:
		case DangerType.EnemyLv25Sound3: case DangerType.EnemyLv25CocountTree: case DangerType.EnemyLv25DownLightning:
		case DangerType.EnemyLv25Gyroscope: case DangerType.EnemyLv25Spring: kill = true; break;
		case DangerType.EnemyLv25SoundLeft: case DangerType.EnemyLv25AirEye: kill = jumping; break;
		case DangerType.EnemyLv25Brigde: kill = !(System.Math.Abs(Ball.Position.x - RootTransform.localPosition.x) < 0.3f); break;
		case DangerType.EnemyLv25CubeDie: IsFallen = true; break;
		// Native unhandled cases (including SoundRight) are nonlethal at the
		// source; their spread cells, not a guessed symmetric rule, kill.
		}
		if (kill && IsBeamSwitchable) SwitchBeam();
		if (kill && IsSlider && AnimTransform != null) kill = Mathf.Abs(AnimTransform.position.x - position.x) <= 0.5f;
		return true;
	}
}
