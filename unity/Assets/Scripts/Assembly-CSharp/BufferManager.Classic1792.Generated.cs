// Generated from the authenticated 1.7.9.2 registry and ImportManifest (Circus42 -> runtime39).
using System.Collections.Generic;

public partial class BufferManager
{
	private static readonly Dictionary<string, string> circusAnimationNames = new Dictionary<string, string>
	{
		{ "Take 001_090e8ba1f0", "Take 001" },
		{ "Take 001_867436c0e2", "Take 001" },
		{ "anim1_092e532da8", "anim1" },
		{ "anim1_0cdf7fa2a2", "anim1" },
		{ "anim1_11fd6648d1", "anim1" },
		{ "anim1_17aab14215", "anim1" },
		{ "anim1_2e5cb1da33", "anim1" },
		{ "anim1_352fdc76a5", "anim1" },
		{ "anim1_5985959e31", "anim1" },
		{ "anim1_60cd6510c1", "anim1" },
		{ "anim1_936014570b", "anim1" },
		{ "anim1_ae67fb481e", "anim1" },
		{ "anim1_b0c9a7f99c", "anim1" },
		{ "anim1_b399657534", "anim1" },
		{ "anim1_d03909f0f1", "anim1" },
		{ "anim1_d228027dad", "anim1" },
		{ "anim2_4257a1ff4d", "anim2" },
		{ "center_8a2f0f3cce", "center" },
		{ "center_s_d51a8eef94", "center_s" },
		{ "left1_229c34a8ac", "left1" },
		{ "left1_s_ddef4cc4ac", "left1_s" },
		{ "left2_04f5815a81", "left2" },
		{ "left2_s_145aa4aad6", "left2_s" },
		{ "right1_d771ed6baa", "right1" },
		{ "right1_s_45dd9682b1", "right1_s" },
		{ "right2_eeab478989", "right2" },
		{ "right2_s_3a5edc3268", "right2_s" },
	};

	private static void RegisterExpansion1792(List<GeoBuffer> buffers)
	{
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX, "Classic1792/Prefabs/01_X", 100, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX1, "Classic1792/Prefabs/02_X1", 20, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX2a, "Classic1792/Prefabs/03_X2a", 15, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX2b, "Classic1792/Prefabs/04_X2b", 12, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX3, "Classic1792/Prefabs/05_X3", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileX4, "Classic1792/Prefabs/06_X4", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileT, "Classic1792/Prefabs/07_T", 80, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileT1a, "Classic1792/Prefabs/08_T1a", 15, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileT1b, "Classic1792/Prefabs/09_T1b", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileT2, "Classic1792/Prefabs/10_T2", 15, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileL, "Classic1792/Prefabs/11_L", 45, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileL1, "Classic1792/Prefabs/12_L1", 30, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileI, "Classic1792/Prefabs/13_I", 90, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileD, "Classic1792/Prefabs/14_D", 70, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Ground, PrefabName.Classic1792TileO, "Classic1792/Prefabs/15_O", 120, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Jump, PrefabName.Classic1792Normal, "Classic1792/Prefabs/07_jump", 60, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Jump, PrefabName.Classic1792MoveJumpLeft, "Classic1792/Prefabs/07_jump 1", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Jump, PrefabName.Classic1792MoveJumpRight, "Classic1792/Prefabs/07_jump 1", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Pickup, PrefabName.Classic1792PickupBig, "Classic1792/Prefabs/01_pickup", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Pickup, PrefabName.Classic1792PickupStar, "Classic1792/Prefabs/02_star", 2, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Pickup, PrefabName.Classic1792pickupCrown, "Classic1792/Prefabs/04_crown", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792TreePalmAlt, "Classic1792/Prefabs/12a_enemyTreePalmAlt", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792Them14TreeSmall2, "Classic1792/Prefabs/45_Them14TreeSmall2", 20, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792PyramidMid, "Classic1792/Prefabs/14_enemyPyramidMid", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792Laser, "Classic1792/Prefabs/16_enemyLaser", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792Spotlight, "Classic1792/Prefabs/17_enemySpotlight", 44, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792SuperJumper, "Classic1792/Prefabs/153_level19SuperJumper", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLevel23TileDown, "Classic1792/Prefabs/166_Level23TileDown", 53, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792TurnTile, "Classic1792/Prefabs/168_Level20Turn", 96, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv20RollerLeft, "Classic1792/Prefabs/170_level20RollerLeft", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv20Rollerright, "Classic1792/Prefabs/171_level20RollerRight", 2, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemySuccessiveJump, "Classic1792/Prefabs/178_Level23SuccessiveJump", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv30MoveAfterTile, "Classic1792/Prefabs/264_Level30_MoveAfterTile", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv30MoveBeforeTile, "Classic1792/Prefabs/263_Level30_MoveBeforeTile", 4, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv37Forward1Tile, "Classic1792/Prefabs/306_Level37_Forward", 9, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42ThreeGlass, "Classic1792/Prefabs/343_EnemyLv42ThreeGlass", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Column, "Classic1792/Prefabs/344_EnemyLv42Column", 44, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Rise, "Classic1792/Prefabs/345_EnemyLv42Rise", 46, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42FieryRings, "Classic1792/Prefabs/346_EnemyLv42FieryRings", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42PendulumLeft, "Classic1792/Prefabs/347_EnemyLv42PendulumLeft", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42PendulumRight, "Classic1792/Prefabs/348_EnemyLv42PendulumRight", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42MonocycleRight, "Classic1792/Prefabs/349_EnemyLv42MonocycleRight", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42MonocycleLeft, "Classic1792/Prefabs/350_EnemyLv42MonocycleLeft", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42ClownUp, "Classic1792/Prefabs/352_EnemyLv42ClownUp", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42TrojanLeft, "Classic1792/Prefabs/353_EnemyLv42TrojanLeft", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42TrojanRight, "Classic1792/Prefabs/354_EnemyLv42TrojanRight", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Door, "Classic1792/Prefabs/356_EnemyLv42Door", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Circle, "Classic1792/Prefabs/357_EnemyLv42Circle", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Arrow, PrefabName.Classic1792ArrowMover, "Classic1792/Prefabs/01_moveArrow", 40, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Arrow, PrefabName.Classic1792ArrowMoverAuto, "Classic1792/Prefabs/02_moveAutoArrow", 50, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792Heat, "Classic1792/Prefabs/3_heatExplosion", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792PortalStart, "Classic1792/Prefabs/2A_portalStartExplosion", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792PortalEnd, "Classic1792/Prefabs/4_laserExplosion", 1, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792Particles_PickupBig, "Classic1792/Prefabs/1A_pickupExplosion", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792Particles_PickupBox, "Classic1792/Prefabs/1A_boxExplosion", 10, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Particles, PrefabName.Classic1792Particles_PickupCrown, "Classic1792/Prefabs/1A_CrownExplosion", 3, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Midground, PrefabName.Classic1792midground16, "Classic1792/Prefabs/midground16", 40, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Bean, PrefabName.Classic1792BeanLv25Accelerate, "Classic1792/Prefabs/202_Level25jiasu", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Bean, PrefabName.Classic1792BeanLv25Deceleration, "Classic1792/Prefabs/203_Level25jiansu", 5, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Bean, PrefabName.Classic1792BeanLv42ClownDown, "Classic1792/Prefabs/351_BeanLv42ClownDown", 4, 39);
		RegisterExpansionPrefab(buffers, PrefabType.Bean, PrefabName.Classic1792SimulationAirKillBall, "Classic1792/Prefabs/358_SimulationAirKillBall", 4, 39);
		circusExpansionAliases.Add(new PrefabID(PrefabType.Arrow, PrefabName.ArrowMover), new PrefabID(PrefabType.Arrow, PrefabName.Classic1792ArrowMover));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Arrow, PrefabName.ArrowMoverAuto), new PrefabID(PrefabType.Arrow, PrefabName.Classic1792ArrowMoverAuto));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Bean, PrefabName.BeanLv25Accelerate), new PrefabID(PrefabType.Bean, PrefabName.Classic1792BeanLv25Accelerate));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Bean, PrefabName.BeanLv25Deceleration), new PrefabID(PrefabType.Bean, PrefabName.Classic1792BeanLv25Deceleration));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Bean, PrefabName.BeanLv42ClownDown), new PrefabID(PrefabType.Bean, PrefabName.Classic1792BeanLv42ClownDown));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Bean, PrefabName.SimulationAirKillBall), new PrefabID(PrefabType.Bean, PrefabName.Classic1792SimulationAirKillBall));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.ClassicLaser), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Laser));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.ClassicPyramidMid), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792PyramidMid));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.ClassicSpotlight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Spotlight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLevel23TileDown), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLevel23TileDown));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv20RollerLeft), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv20RollerLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv20Rollerright), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv20Rollerright));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv30MoveAfterTile), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv30MoveAfterTile));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv30MoveBeforeTile), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv30MoveBeforeTile));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Forward1Tile), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv37Forward1Tile));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42Circle), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Circle));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42ClownUp), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42ClownUp));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42Column), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Column));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42Door), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Door));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42FieryRings), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42FieryRings));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42MonocycleLeft), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42MonocycleLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42MonocycleRight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42MonocycleRight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42PendulumLeft), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42PendulumLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42PendulumRight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42PendulumRight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42Rise), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42Rise));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42ThreeGlass), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42ThreeGlass));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42TrojanLeft), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42TrojanLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv42TrojanRight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemyLv42TrojanRight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.EnemySuccessiveJump), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792EnemySuccessiveJump));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.Laser), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Laser));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterLaser), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Laser));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterPyramidMid), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792PyramidMid));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterSpotlight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Spotlight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterSuperJumper), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792SuperJumper));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterThem14TreeSmall2), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Them14TreeSmall2));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.LaterTreePalmAlt), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792TreePalmAlt));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.PyramidMid), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792PyramidMid));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.Spotlight), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Spotlight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.SuperJumper), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792SuperJumper));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.Them14TreeSmall2), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792Them14TreeSmall2));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.TreePalmAlt), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792TreePalmAlt));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Enemy, PrefabName.TurnTile), new PrefabID(PrefabType.Enemy, PrefabName.Classic1792TurnTile));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileD), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileD));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileI), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileI));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileL), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileL));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileL1), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileL1));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileO), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileO));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileT), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileT));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileT1a), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileT1a));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileT1b), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileT1b));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileT2), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileT2));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX1), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX1));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX2a), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX2a));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX2b), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX2b));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX3), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX3));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Ground, PrefabName.TileX4), new PrefabID(PrefabType.Ground, PrefabName.Classic1792TileX4));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Jump, PrefabName.LaterMoveJumpLeft), new PrefabID(PrefabType.Jump, PrefabName.Classic1792MoveJumpLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Jump, PrefabName.LaterMoveJumpRight), new PrefabID(PrefabType.Jump, PrefabName.Classic1792MoveJumpRight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Jump, PrefabName.MoveJumpLeft), new PrefabID(PrefabType.Jump, PrefabName.Classic1792MoveJumpLeft));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Jump, PrefabName.MoveJumpRight), new PrefabID(PrefabType.Jump, PrefabName.Classic1792MoveJumpRight));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Jump, PrefabName.Normal), new PrefabID(PrefabType.Jump, PrefabName.Classic1792Normal));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.Heat), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Heat));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.LaterParticles_PickupBig), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupBig));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.LaterParticles_PickupBox), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupBox));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.LaterParticles_PickupCrown), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupCrown));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.Particles_PickupBig), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupBig));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.Particles_PickupBox), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupBox));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.Particles_PickupCrown), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupCrown));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.PickupBig), new PrefabID(PrefabType.Particles, PrefabName.Classic1792Particles_PickupBig));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.PortalEnd), new PrefabID(PrefabType.Particles, PrefabName.Classic1792PortalEnd));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Particles, PrefabName.PortalStart), new PrefabID(PrefabType.Particles, PrefabName.Classic1792PortalStart));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Pickup, PrefabName.LaterpickupCrown), new PrefabID(PrefabType.Pickup, PrefabName.Classic1792pickupCrown));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Pickup, PrefabName.PickupBig), new PrefabID(PrefabType.Pickup, PrefabName.Classic1792PickupBig));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Pickup, PrefabName.PickupStar), new PrefabID(PrefabType.Pickup, PrefabName.Classic1792PickupStar));
		circusExpansionAliases.Add(new PrefabID(PrefabType.Pickup, PrefabName.pickupCrown), new PrefabID(PrefabType.Pickup, PrefabName.Classic1792pickupCrown));
	}
}
