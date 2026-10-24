using UnityEngine;

public partial class LevelConstructor
{
	private static Tile DecideLaterType(int value, float offset, int segment, int row, int col, bool flipped)
	{
		Tile.DangerType danger;
		Direction direction = Direction.Null;
		Tile.GroundType ground = Tile.GroundType.Standard;
		switch ((TileVal)value)
		{
		case TileVal.BeanLv25Accelerate: case TileVal.BeanLv25Deceleration: case TileVal.BeanLv25BrigdeUp: case TileVal.BeanLv25BrigdeDown:
			return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Bean);
		case TileVal.EnemyCube1: danger = Tile.DangerType.EnemyCube1; break;
		case TileVal.EnemyCube3: danger = Tile.DangerType.EnemyCube3; break;
		case TileVal.EnemyCube3Pillar: danger = Tile.DangerType.EnemyCube3Pillar; break;
		case TileVal.EnemyCube4: danger = Tile.DangerType.EnemyCube4; break;
		case TileVal.EnemyCube5: danger = Tile.DangerType.EnemyCube5; break;
		case TileVal.EnemyCube6: danger = Tile.DangerType.EnemyCube6; break;
		case TileVal.EnemyCube9: danger = Tile.DangerType.EnemyCube9; break;
		case TileVal.EnemyHalfround: danger = Tile.DangerType.EnemyHalfround; break;
		case TileVal.TurnTileCenter: case TileVal.TurnTileLeft1: case TileVal.TurnTileLeft2: case TileVal.TurnTileRight1: case TileVal.TurnTileRight2:
			danger = Tile.DangerType.TurnTile; break;
		case TileVal.EnemyLv20RollerLeft: danger = Tile.DangerType.EnemyLv20RollerLeft; direction = Direction.Left; break;
		case TileVal.EnemyLv20Rollerright: danger = Tile.DangerType.EnemyLv20Rollerright; direction = Direction.Right; break;
		case TileVal.EnemyLevel23TileDown: danger = Tile.DangerType.EnemyLevel23TileDown; break;
		case TileVal.EnemyPoker1: danger = Tile.DangerType.EnemyPoker1; break;
		case TileVal.EnemyPoker2Left: danger = Tile.DangerType.EnemyPoker2Left; direction = Direction.Left; break;
		case TileVal.EnemyPoker2Right: danger = Tile.DangerType.EnemyPoker2Right; direction = Direction.Right; break;
		case TileVal.EnemyPoker3Left: danger = Tile.DangerType.EnemyPoker3Left; break;
		case TileVal.EnemyPoker3Right: danger = Tile.DangerType.EnemyPoker3Right; break;
		case TileVal.EnemySuccessiveJump: danger = Tile.DangerType.EnemySuccessiveJump; break;
		case TileVal.EnemyMagicDove: danger = Tile.DangerType.EnemyMagicDove; break;
		case TileVal.EnemyPoker1Lean: danger = Tile.DangerType.EnemyPoker1Lean; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyPokerTree: danger = Tile.DangerType.EnemyPokerTree; break;
		case TileVal.EnemyPokerTreeLeft: danger = Tile.DangerType.EnemyPokerTreeSlider; direction = Direction.Left; break;
		case TileVal.EnemyPokerTreeRight: danger = Tile.DangerType.EnemyPokerTreeSlider; direction = Direction.Right; break;
		case TileVal.EnemyTileWhite: danger = Tile.DangerType.EnemyTileWhite; break;
		case TileVal.EnemyTileBlack: danger = Tile.DangerType.EnemyTileBlack; break;
		case TileVal.EnemyUmbrellaLeft: danger = Tile.DangerType.EnemyUmbrellaLeft; break;
		case TileVal.EnemyUmbrellaRight: danger = Tile.DangerType.EnemyUmbrellaRight; break;
		case TileVal.EnemyPokerDoor: danger = Tile.DangerType.EnemyPokerDoor; break;
		case TileVal.EnemyBigJoker: danger = Tile.DangerType.EnemyBigJoker; break;
		case TileVal.EnemySmallJoker: danger = Tile.DangerType.EnemySmallJoker; break;
		case TileVal.EnemySoldierLeft: danger = Tile.DangerType.EnemySoldierLeft; break;
		case TileVal.EnemySoldierRight: danger = Tile.DangerType.EnemySoldierRight; break;
		case TileVal.PickDownLv25Cube: danger = Tile.DangerType.PickDownLv25Cube; break;
		case TileVal.PickDownLv25Cube1: danger = Tile.DangerType.PickDownLv25Cube1; break;
		case TileVal.EnemyLv25Brigde: danger = Tile.DangerType.EnemyLv25Brigde; break;
		case TileVal.SimulationJumpTitle: danger = Tile.DangerType.SimulationJumpTitle; break;
		case TileVal.EnemyLv25Sound: danger = Tile.DangerType.EnemyLv25Sound; break;
		case TileVal.EnemyLv25SoundLeft: danger = Tile.DangerType.EnemyLv25SoundLeft; break;
		case TileVal.EnemyLv25SoundRight: danger = Tile.DangerType.EnemyLv25SoundRight; break;
		case TileVal.EnemyLv25AirEye: danger = Tile.DangerType.EnemyLv25AirEye; break;
		case TileVal.EnemyLv25Sound3: danger = Tile.DangerType.EnemyLv25Sound3; break;
		case TileVal.EnemyLv25CocountTree: danger = Tile.DangerType.EnemyLv25CocountTree; break;
		case TileVal.EnemyLv25XiaoBianLeft: danger = Tile.DangerType.EnemyLv25XiaoBianLeft; break;
		case TileVal.EnemyLv25XiaoBianRight: danger = Tile.DangerType.EnemyLv25XiaoBianRight; break;
		case TileVal.EnemyLv25Tape: danger = Tile.DangerType.EnemyLv25Tape; break;
		case TileVal.EnemyLv25CubeLive: danger = Tile.DangerType.EnemyLv25CubeLive; break;
		case TileVal.EnemyLv25CubeDie: danger = Tile.DangerType.EnemyLv25CubeDie; break;
		case TileVal.EnemyLv25MoveDici: danger = Tile.DangerType.EnemyLv25MoveDici; break;
		case TileVal.EnemyLv25DownLightning: danger = Tile.DangerType.EnemyLv25DownLightning; break;
		case TileVal.EnemyLv25GyroscopeLeft: danger = Tile.DangerType.EnemyLv25Gyroscope; direction = Direction.Left; break;
		case TileVal.EnemyLv25GyroscopeRight: danger = Tile.DangerType.EnemyLv25Gyroscope; direction = Direction.Right; break;
		case TileVal.EnemySuperJumperFictitious: danger = Tile.DangerType.SuperJumperFictitious; break;
		case TileVal.EnemyLv25Spring: danger = Tile.DangerType.EnemyLv25Spring; break;
		default: return null;
		}
		return MakeAsset(value, offset, segment, row, col, flipped, Tile.Type.Enemy, danger, direction, ground);
	}

	private static bool SpreadLaterDanger(Tile tile, int row, int col)
	{
		if (!ExpansionGameplay.Later) return false;
		switch (tile.dangerSpreadType)
		{
		case Tile.DangerSpreadType.Triangle:
		case Tile.DangerSpreadType.Cube6:
			tile.SetAsDangerous(null, null, Tile.DangerType.FloaterAir);
			SpreadClassicSide(row, col - 1, Tile.DangerType.Riser);
			SpreadClassicSide(row, col + 1, Tile.DangerType.Riser);
			if (tile.dangerSpreadType == Tile.DangerSpreadType.Cube6 && row > 1)
			{
				SpreadClassicSide(row - 1, col, Tile.DangerType.FloaterAir);
				SpreadClassicSide(row - 2, col, Tile.DangerType.Riser);
			}
			break;
		case Tile.DangerSpreadType.Cube9:
		case Tile.DangerSpreadType.Halfround:
			tile.SetAsDangerous(null, null, Tile.DangerType.TallTree);
			bool cube9 = tile.dangerSpreadType == Tile.DangerSpreadType.Cube9;
			SpreadClassicSide(row, col - 1, cube9 ? Tile.DangerType.FloaterAir : Tile.DangerType.TallTree);
			SpreadClassicSide(row, col + 1, cube9 ? Tile.DangerType.FloaterAir : Tile.DangerType.TallTree);
			SpreadClassicSide(row, col - 2, cube9 ? Tile.DangerType.TallTree : Tile.DangerType.Riser);
			SpreadClassicSide(row, col + 2, cube9 ? Tile.DangerType.TallTree : Tile.DangerType.Riser);
			break;
		case Tile.DangerSpreadType.Cube4:
			tile.SetAsDangerous(null, null, Tile.DangerType.TallTree);
			SpreadClassicSide(row, col - 1, Tile.DangerType.Riser);
			SpreadClassicSide(row + 1, col - 1, Tile.DangerType.Riser);
			break;
		case Tile.DangerSpreadType.Roller:
			tile.SetAsDangerous(null, null, Tile.DangerType.Riser);
			SpreadClassicSide(row, col - (tile.direction == Direction.Left ? 2 : 1), Tile.DangerType.Riser);
			SpreadClassicSide(row, col + (tile.direction == Direction.Left ? 1 : 2), Tile.DangerType.Riser);
			break;
		default: return false;
		}
		return true;
	}
}
