using UnityEngine;

// Ported from the Mono 1.7.8.2 source; selected by resource identity, not slot.
public partial class LevelConstructor
{
	// Signed Mono 1.7.9.2 LevelConstructor.cs:1188-1214.
	private static Tile Decide1792Type(int tileTypeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped)
	{
		switch ((TileVal)tileTypeVal)
		{
		case TileVal.EnemyLv42ThreeGlass: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42ThreeGlass);
		case TileVal.EnemyLv42Column: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42Column);
		case TileVal.EnemyLv42Rise: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42Rise);
		case TileVal.EnemyLv42FieryRings: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42FieryRings);
		case TileVal.EnemyLv42PendulumLeft: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42PendulumLeft);
		case TileVal.EnemyLv42PendulumRight: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42PendulumRight);
		case TileVal.EnemyLv42MonocycleRight: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42MonocycleRight);
		case TileVal.EnemyLv42MonocycleLeft: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42MonocycleLeft);
		case TileVal.EnemyLv42ClownUp: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42ClownUp);
		case TileVal.BeanLv42ClownDown: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv42ClownDown);
		case TileVal.EnemyLv42TrojanLeft: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42TrojanLeft);
		case TileVal.EnemyLv42TrojanRight: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42TrojanRight);
		case TileVal.EnemyLv42Door: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42Door);
		case TileVal.SimulationAirKillBall: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.SimulationAirKillBall);
		case TileVal.EnemyLv42Circle: return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv42Circle);
		default: return null;
		}
	}

	private static bool Setup1782Tile()
	{
		if ((TileVal)currentTile.TypeVal == TileVal.BeanLv34Jump1Tile)
		{
			currentTile.CreateTile(PrefabType.Jump, PrefabName.BeanLv34Jump1Tile, MaterialName.General, 0f);
			return true;
		}
		if (currentTile.IsGeoHidden && ((currentTile.type == Tile.Type.Enemy && currentTile.dangerType >= Tile.DangerType.BackwardQueryLv26shunyiStart) ||
			currentTile.dangerType == Tile.DangerType.BeanBack6Tile || currentTile.dangerType == Tile.DangerType.BeanLv42ClownDown))
		{
			currentTile.CreateEmptyTile();
			return true;
		}
		return false;
	}

	private static Tile Decide1782Type(int tileTypeVal, float segmentOffset, int segmentNum, int rowNum, int tileNum, bool tileFlipped)
	{
		Tile tile = Decide1792Type(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped);
		if (tile != null) return tile;
		switch (tileTypeVal)
		{
		// Recovered 3.2.5 native LevelConstructor.DecideType (0x8b58b8).
		case (int)TileVal.Enemy_807_Level69_mofang:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Enemy_807_Level69_mofang);
		case (int)TileVal.Enemy_808_Level69_yanjing:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Enemy_808_Level69_yanjing);
		case (int)TileVal.Enemy_809_Level69_banyuanL:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Enemy_809_Level69_banyuanL);
		case (int)TileVal.Enemy_810_Level69_banyuanR:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Enemy_810_Level69_banyuanR);
		case (int)TileVal.EnemyLv30Frankenstein:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Frankenstein);
		case (int)TileVal.EnemyLv30Corpse:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Corpse);
		case (int)TileVal.EnemyLv30WitchRight:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30WitchRight);
		case (int)TileVal.EnemyLv30WitchLeft:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30WitchLeft);
		case (int)TileVal.EnemyLv30SpiderDie:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30SpiderDie);
		case (int)TileVal.EnemyLv30SpiderLife:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30SpiderLife);
		case (int)TileVal.EnemyLv30Pumpkin:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Pumpkin);
		case (int)TileVal.EnemyLv30Tombstone:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Tombstone);
		case (int)TileVal.EnemyLv30Vampire:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Vampire);
		case (int)TileVal.EnemyLv30Door:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Door);
		case (int)TileVal.EnemyLv30Pumpkin2:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Pumpkin2);
		case (int)TileVal.EnemyLv30Vampire2:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30Vampire2);
		case (int)TileVal.BeanLv30Cobweb:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv30Cobweb);
		case (int)TileVal.BeanSimulationDeceleration:
			return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean);
		case (int)TileVal.EnemyLv40DownTile:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.Standard);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownAlphaTile:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.GroundType.FragileChecker);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.GroundTileLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.GroundTileLeft, Direction.Left);
		case (int)TileVal.GroundTileRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.GroundTileRight, Direction.Right);
		case (int)TileVal.GroundTileBefore:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.GroundTileBefore, Direction.Top);
		case (int)TileVal.GroundTileAfter:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.GroundTileAfter, Direction.Bottom);
		case (int)TileVal.EnemyLv40DownPickupJump:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump, Tile.PickupType.Big);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownPickupStarGround:
		case (int)TileVal.EnemyLv40DownPickupStarJump:
		{
			Tile.Type type = tileTypeVal == (int)TileVal.EnemyLv40DownPickupStarGround ? Tile.Type.Ground : Tile.Type.Jump;
			int index = 0;
			if (ClassicCrownRandom.Enabled && ClassicCrownRandom.GetCrownTypeOnCreate(ref index) == ClassicCrownRandom.CrownType.NOTHING)
				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type);
			else
			{
				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, type, Tile.PickupType.Star);
				tile.CrownIndex = index;
			}
			tile.HideRoot(true);
			return tile;
		}
		case (int)TileVal.EnemyLv40DownPillar1:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Pillar, 1f);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownLaserFragile:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.Laser, Tile.GroundType.Fragile0);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownMoverForwards:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Mover, Direction.Top);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownRotationTile:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Ground, Tile.AnimType.Rotation, Direction.Null);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownJumpFromL1:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyJumpFromL1, Direction.Null);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownJumpFromR1:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyJumpFromR1, Direction.Null);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownJumpFromL2:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyJumpFromL2, Direction.Null);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40DownJumpFromR2:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyJumpFromR2, Direction.Null);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.BackwardQueryLv26shunyiStart:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.BackwardQuery);
		case (int)TileVal.BackwardQueryLv26shunyiEnd:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.BackwardQuery);
		case (int)TileVal.EnemyLv26Door:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Door);
		case (int)TileVal.EnemyLv26Pillars1:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Pillars1);
		case (int)TileVal.EnemyLv26God1:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26God1);
		case (int)TileVal.EnemyLv26FirePlace:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean);
		case (int)TileVal.EnemyLv26Castle:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Castle);
		case (int)TileVal.EnemyLv26Pillars2:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Pillars2);
		case (int)TileVal.EnemyLv26Dici:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Dici);
		case (int)TileVal.EnemyLv26God2:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26God2);
		case (int)TileVal.EnemyLv26DaDiShuLife:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26DaDiShuLife);
		case (int)TileVal.EnemyLv26DaDiShuDie:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26DaDiShuDie);
		case (int)TileVal.EnemyLv26Owl:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Owl);
		case (int)TileVal.EnemyLv26Tree:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv26Tree);
		case (int)TileVal.BeanLv28Net:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean);
		case (int)TileVal.BeanSimulationNet:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean);
		case (int)TileVal.BeanLv28BouncePlate:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv28BouncePlate);
		case (int)TileVal.BeanLv28BasketballStandsLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv28BasketballStandsLeft);
		case (int)TileVal.BeanLv28BasketballStandsRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv28BasketballStandsRight);
		case (int)TileVal.BeanLv28BasketballStandsCenter:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv28BasketballStandsCenter);
		case (int)TileVal.EnemyLv28NetDie:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28NetDie);
		case (int)TileVal.EnemyLv28Wall:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28Wall);
		case (int)TileVal.EnemyLv28Wall2:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28Wall2);
		case (int)TileVal.EnemyLv28BlackPlayer:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28BlackPlayer);
		case (int)TileVal.EnemyLv28PlayerLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28PlayerLeft);
		case (int)TileVal.EnemyLv28PlayerRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28PlayerRight);
		case (int)TileVal.EnemyLv28HatPlayer:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28HatPlayer);
		case (int)TileVal.EnemyLv28Tyre:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28Tyre);
		case (int)TileVal.EnemyLv28Doors:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28Doors);
		case (int)TileVal.EnemyLv28Doors2:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv28Doors2);
		case (int)TileVal.EnemySimulationDie:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.EnemySimulationDie);
		case (int)TileVal.BeanLv28Button:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv28Button);
		case (int)TileVal.EnemyLv30MoveAfterTile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30MoveAfterTile);
		case (int)TileVal.EnemyLv30MoveBeforeTile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv30MoveBeforeTile);
		case (int)TileVal.BeanJump10Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanJump10Tile);
		case (int)TileVal.BeanBack6Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanBack6Tile);
		case (int)TileVal.BeanLv32StartReverse:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv32StartReverse);
		case (int)TileVal.BeanLv32EndReverse:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv32EndReverse);
		case (int)TileVal.EnemyLv32Door:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32Door);
		case (int)TileVal.EnemyLv32AirWheel:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32AirWheel);
		case (int)TileVal.EnemyLv32LightLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32LightLeft);
		case (int)TileVal.EnemyLv32LightRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32LightRight);
		case (int)TileVal.EnemyLv32HorizontalLever:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32HorizontalLever);
		case (int)TileVal.EnemyLv32VerticalLever:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32VerticalLever);
		case (int)TileVal.EnemyLv32Cannon:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv32Cannon);
		case (int)TileVal.EnemyLv34Snow:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.EnemyLv34Snow);
		case (int)TileVal.BeanLv34Jump1Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Jump);
		case (int)TileVal.EnemyLv34BigCane:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv34BigCane);
		case (int)TileVal.EnemyLv34Tree1:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv34Tree1);
		case (int)TileVal.EnemyLv37Forward1Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Forward1Tile);
		case (int)TileVal.EnemyLv37Forward2Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Forward2Tile);
		case (int)TileVal.EnemyLv37Forward3Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Forward3Tile);
		case (int)TileVal.EnemyLv37Forward4Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Forward4Tile);
		case (int)TileVal.EnemyLv37Forward5Tile:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Forward5Tile);
		case (int)TileVal.EnemyLv37Frisbee:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Frisbee);
		case (int)TileVal.EnemyLv37Pan:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Pan);
		case (int)TileVal.EnemyLv37Sound:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Sound);
		case (int)TileVal.EnemyLv37RobotLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37RobotLeft);
		case (int)TileVal.EnemyLv37RobotRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37RobotRight);
		case (int)TileVal.EnemyLv37Door:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Door);
		case (int)TileVal.EnemyLv37KeyboardRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37KeyboardRight);
		case (int)TileVal.EnemyLv37KeyboardLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37KeyboardLeft);
		case (int)TileVal.EnemyLv37Gear:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv37Gear);
		case (int)TileVal.EnemyLv40PyramidBack:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40PyramidBack);
		case (int)TileVal.EnemyLv40PyramidRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40PyramidRight);
		case (int)TileVal.EnemyLv40PyramidLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40PyramidLeft);
		case (int)TileVal.EnemyLv40Bug:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Bug);
		case (int)TileVal.EnemyLv40Door:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Door);
		case (int)TileVal.EnemyLv40Column1:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Column1);
		case (int)TileVal.EnemyLv40Column2:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Column2);
		case (int)TileVal.EnemyLv40Eye:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Eye);
		case (int)TileVal.EnemyLv40BackTriangle:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40BackTriangle);
		case (int)TileVal.EnemyLv40DownBackTriangle:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40BackTriangle);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40Triangle:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Triangle);
		case (int)TileVal.EnemyLv40Snake:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Snake);
		case (int)TileVal.EnemyLv40DownSnake:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40Snake);
				tile.HideRoot(true);
				return tile;
		case (int)TileVal.EnemyLv40LeftWall:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40LeftWall);
				Ball.Classic1782OutsideWalls.Add(tile.GetPosition());
				return tile;
		case (int)TileVal.EnemyLv40RightWall:

				tile = MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40RightWall);
				Ball.Classic1782OutsideWalls.Add(tile.GetPosition());
				return tile;
		case (int)TileVal.EnemyLv40_5TileLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_5TileLeft);
		case (int)TileVal.EnemyLv40_5TileRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_5TileRight);
		case (int)TileVal.EnemyLv40_4TileLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_4TileLeft);
		case (int)TileVal.EnemyLv40_4TileRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_4TileRight);
		case (int)TileVal.EnemyLv40_3TileLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_3TileLeft);
		case (int)TileVal.EnemyLv40_3TileRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_3TileRight);
		case (int)TileVal.EnemyLv40_2TileLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_2TileLeft);
		case (int)TileVal.EnemyLv40_2TileRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_2TileRight);
		case (int)TileVal.BeanLv40ShowButton:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv40ShowButton);
		case (int)TileVal.BeanLv40HideButton:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Bean, Tile.DangerType.BeanLv40HideButton);
		case (int)TileVal.EnemyLv40_CrossLeft:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_CrossLeft);
		case (int)TileVal.EnemyLv40_CrossRight:

				return MakeAsset(tileTypeVal, segmentOffset, segmentNum, rowNum, tileNum, tileFlipped, Tile.Type.Enemy, Tile.DangerType.EnemyLv40_CrossRight);
		default: return null;
		}
	}
}
