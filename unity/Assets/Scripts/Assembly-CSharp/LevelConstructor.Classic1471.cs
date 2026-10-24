using UnityEngine;

public partial class LevelConstructor
{
	// Use names: the new map importer deliberately does not use donor ordinals
	// in the baseline TileVal namespace.
	private static Tile DecideClassicType(int typeVal, float offset, int segment, int row, int col, bool flipped)
	{
		Tile.DangerType danger;
		Tile.GroundType ground = Tile.GroundType.Standard;
		Direction direction = Direction.Null;
		switch ((TileVal)typeVal)
		{
		case TileVal.EnemyRadioTower: danger = Tile.DangerType.RadioTower; break;
		case TileVal.EnemyRadioTowerFragile: danger = Tile.DangerType.RadioTower; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyShinto: danger = Tile.DangerType.ShintoSafe; break;
		case TileVal.EnemyShintoFragile: danger = Tile.DangerType.ShintoSafe; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyShintoUnsafe: danger = Tile.DangerType.ShintoUnsafe; break;
		case TileVal.EnemyShintoUnsafeFragile: danger = Tile.DangerType.ShintoUnsafe; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyTurretLeft: danger = Tile.DangerType.Turret; direction = Direction.Left; break;
		case TileVal.EnemyTurretRight: danger = Tile.DangerType.Turret; direction = Direction.Right; break;
		case TileVal.EnemyTurretFragileLeft: danger = Tile.DangerType.Turret; direction = Direction.Left; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyTurretFragileRight: danger = Tile.DangerType.Turret; direction = Direction.Right; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyTurretLeftOffsetLeft:
		case TileVal.EnemyTurretLeftOffsetRight: danger = Tile.DangerType.TurretOffset; direction = Direction.Left; break;
		case TileVal.EnemyTurretRightOffsetLeft:
		case TileVal.EnemyTurretRightOffsetRight: danger = Tile.DangerType.TurretOffset; direction = Direction.Right; break;
		case TileVal.EnemyTurretFragileLeftOffsetLeft:
		case TileVal.EnemyTurretFragileLeftOffsetRight: danger = Tile.DangerType.TurretOffset; direction = Direction.Left; ground = Tile.GroundType.Fragile0; break;
		case TileVal.EnemyTurretFragileRightOffsetLeft:
		case TileVal.EnemyTurretFragileRightOffsetRight: danger = Tile.DangerType.TurretOffset; direction = Direction.Right; ground = Tile.GroundType.Fragile0; break;
		case TileVal.SwitcherLeft: danger = Tile.DangerType.Switcher; direction = Direction.Left; break;
		case TileVal.SwitcherRight: danger = Tile.DangerType.Switcher; direction = Direction.Right; break;
		case TileVal.SwitcherFragileLeft: danger = Tile.DangerType.Switcher; direction = Direction.Left; ground = Tile.GroundType.Fragile0; break;
		case TileVal.SwitcherFragileRight: danger = Tile.DangerType.Switcher; direction = Direction.Right; ground = Tile.GroundType.Fragile0; break;
		case TileVal.TetrisDownOneLine01: danger = Tile.DangerType.TetrisDownOneLine01; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine02: danger = Tile.DangerType.TetrisDownOneLine02; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine03: danger = Tile.DangerType.TetrisDownOneLine03; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine04: danger = Tile.DangerType.TetrisDownOneLine04; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine05: danger = Tile.DangerType.TetrisDownOneLine05; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine06: danger = Tile.DangerType.TetrisDownOneLine06; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine07: danger = Tile.DangerType.TetrisDownOneLine07; direction = Direction.Right; break;
		case TileVal.TetrisDownOneLine08: danger = Tile.DangerType.TetrisDownOneLine08; direction = Direction.Right; break;
		case TileVal.OneBlockInToOffD: danger = Tile.DangerType.OneBlockInToOffD; direction = Direction.Right; break;
		case TileVal.OneBlockInToOff: danger = Tile.DangerType.OneBlockInToOff; direction = Direction.Right; break;
		case TileVal.OneBlockOffToInD: danger = Tile.DangerType.OneBlockOffToInD; direction = Direction.Right; break;
		default: return null;
		}
		return MakeAsset(typeVal, offset, segment, row, col, flipped, Tile.Type.Enemy, danger, direction, ground);
	}

	// Port of 1.4.7.1 TrySpreadDangers. First-five Binary/Wyrm spreading is
	// still handled by the unchanged branch; classic Binary uses saved metadata.
	private static void SpreadClassicDanger(Tile tile, int row, int col)
	{
		int length = tile.dangerSpreadLength;
		if (length <= 0) return;
		Tile.DangerType hazard = tile.IsDangerHeatSpreader ? Tile.DangerType.Heat : Tile.DangerType.Danger;
		switch (tile.dangerSpreadType)
		{
		case Tile.DangerSpreadType.OneSide:
		{
			int start;
			int end;
			if (tile.direction == Direction.Left)
			{
				start = MathUtils.MaxInt(col - length, 0);
				end = col - 1;
			}
			else if (tile.direction == Direction.Right)
			{
				start = col + 1;
				end = MathUtils.MinInt(col + length, currentSegment.GetLength(1) - 1);
			}
			else
			{
				Debug.LogError("LVCN: Invalid one-sided classic danger direction: " + tile.direction);
				return;
			}
			for (int x = start; x <= end; x++)
			{
				Tile target = currentSegment[row, x];
				// Donor OneSide uses Danger with a Y margin, even for heat spreaders.
				if (target != null && target.IsNotDangerous) target.SetAsDangerous(null, 0.125f);
			}
			break;
		}
		case Tile.DangerSpreadType.Area:
		case Tile.DangerSpreadType.Cross:
		case Tile.DangerSpreadType.Corners:
			for (int x = MathUtils.MaxInt(col - length, 0); x <= MathUtils.MinInt(col + length, currentSegment.GetLength(1) - 1); x++)
			{
				for (int y = MathUtils.MaxInt(row - length, 0); y <= MathUtils.MinInt(row + length, currentSegment.GetLength(0) - 1); y++)
				{
					// Preserve the donor's x/row exclusion test, including row == col.
					bool spread = (x != col || x != row) &&
						(tile.dangerSpreadType == Tile.DangerSpreadType.Area ||
						(tile.dangerSpreadType == Tile.DangerSpreadType.Cross && (y == row || x == col)) ||
						(tile.dangerSpreadType == Tile.DangerSpreadType.Corners && y != row && x != col));
					if (spread && currentSegment[y, x] != null) currentSegment[y, x].SetAsDangerous(null, null, hazard);
				}
			}
			break;
		case Tile.DangerSpreadType.Connected:
		{
			bool left = tile.direction == Direction.Left;
			int step = left ? -1 : 1;
			float beamLength = 0f;
			bool stopped = false;
			for (int x = col + step; x >= 0 && x < currentSegment.GetLength(1); x += step)
			{
				Tile target = currentSegment[row, x];
				beamLength += 1f;
				if (target == null) continue;
				if (target.dangerType == Tile.DangerType.Turret || target.AdditionOffset.HasValue)
				{
					stopped = true;
					break;
				}
				// This left/right asymmetry (and Top following Right) is original.
				if (!left || !target.IsDangerous) target.SetAsDangerous(null, null, hazard);
			}
			tile.turret_laser_z_scale = stopped ? beamLength : 100f;
			break;
		}
		case Tile.DangerSpreadType.TopLeftRight:
			tile.SetAsDangerous(null, null, Tile.DangerType.FloaterAir);
			SpreadClassicSide(row, col - 1, Tile.DangerType.TallTree);
			SpreadClassicSide(row, col + 1, Tile.DangerType.TallTree);
			break;
		case Tile.DangerSpreadType.BottomLeftRight:
			tile.SetAsDangerous(null, null);
			SpreadClassicSide(row, col - 1, Tile.DangerType.Danger);
			SpreadClassicSide(row, col + 1, Tile.DangerType.Danger);
			break;
		case Tile.DangerSpreadType.Offset:
			tile.SetAsDangerous(null, null, Tile.DangerType.Null);
			if (tile.direction == Direction.Left) SpreadClassicSide(row, col - 1, hazard);
			else if (tile.direction == Direction.Right) SpreadClassicSide(row, col + 1, hazard);
			break;
		default:
			Debug.LogError("LVCN: Unsupported classic danger spread: " + tile.dangerSpreadType);
			break;
		}
	}

	private static void SpreadClassicSide(int row, int col, Tile.DangerType hazard)
	{
		if (!IsValidIndex(row, col, currentSegment)) return;
		Tile target = currentSegment[row, col];
		if (target != null && !target.IsDangerous) target.SetAsDangerous(null, null, hazard);
	}
}
