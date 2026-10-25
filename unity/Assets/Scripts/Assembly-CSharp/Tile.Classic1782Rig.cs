using UnityEngine;

// Ported from the Mono 1.7.8.2 source; selected by resource identity, not slot.
public partial class Tile
{
	private bool Create1782Animator()
	{
		if (!ExpansionGameplay.Classic1782 || animType < AnimType.BackwardQueryLv26shunyiStart) return false;
		if (Is1782MovingGround) return true;
		BindExpansionRig();
		switch (animType)
		{
		// Native 3.2.5 footprints (Tile.CreateAnimator, 0x5703e0).
		case AnimType.Enemy_807_Level69_mofang:
			AdditionTransform.eulerAngles = Vector3.zero;
			break;
		case AnimType.Enemy_808_Level69_yanjing:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z + 1);
			break;
		case AnimType.Enemy_809_Level69_banyuanL:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z + 1);
			SetAppointTileDanger(Index.y, Index.z + 2);
			break;
		case AnimType.Enemy_810_Level69_banyuanR:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z - 1);
			SetAppointTileDanger(Index.y, Index.z - 2);
			break;
		// Signed Mono 1.7.9.2 Tile.CreateAnimator:6585-6631,6709-6733,6826-6861.
		case AnimType.EnemyLv42Column:
		case AnimType.EnemyLv42Rise:
		case AnimType.EnemyLv42ClownUp:
			AdditionTransform.eulerAngles = Vector3.zero;
			break;
		case AnimType.EnemyLv42Door:
			AdditionTransform.eulerAngles = Vector3.zero;
			AnimParticleSystem.gameObject.SetActive(false);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.EnemyLv32LightLeft);
			SetAppointTileDanger(Index.y, Index.z - 2, DangerType.EnemyLv32LightLeft);
			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.EnemyLv32LightLeft);
			SetAppointTileDanger(Index.y, Index.z + 2, DangerType.EnemyLv32LightLeft);
			break;
		case AnimType.EnemyLv42ThreeGlass:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z - 1);
			SetAppointTileDanger(Index.y, Index.z + 1);
			SetAppointTileDanger(Index.y + 2, Index.z);
			break;
		case AnimType.EnemyLv42FieryRings:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y + 1, Index.z - 1, DangerType.TallTree);
			SetAppointTileDanger(Index.y + 1, Index.z + 1, DangerType.TallTree);
			break;
		case AnimType.EnemyLv42PendulumLeft:
		case AnimType.EnemyLv42TrojanLeft:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z - 1);
			break;
		case AnimType.EnemyLv42PendulumRight:
		case AnimType.EnemyLv42TrojanRight:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z + 1);
			break;
		case AnimType.EnemyLv42MonocycleLeft:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z - 2, DangerType.EnemyLv26Dici);
			break;
		case AnimType.EnemyLv42MonocycleRight:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z + 2, DangerType.EnemyLv26Dici);
			break;
		case AnimType.BeanLv42ClownDown:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			AnimTransform.eulerAngles = new Vector3(90f, 180f, 0f);
			AltAnimTransform = TransformUtils.Find("altAnim", AdditionTransform);
			break;
		case AnimType.EnemyLv42Circle:
			AnimTransform = TransformUtils.Find("anim", AdditionTransform);
			break;
		case AnimType.EnemyLv28Tyre:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y + 4, Index.z);
			break;
		case AnimType.EnemyLv26Door:
		case AnimType.EnemyLv26Castle:
		case AnimType.EnemyLv26Pillars2:
		case AnimType.EnemyLv28Wall:
		case AnimType.EnemyLv28Wall2:
		case AnimType.EnemyLv28Doors:
		case AnimType.EnemyLv28Doors2:
		case AnimType.EnemyLv30Vampire:
		case AnimType.EnemyLv32Door:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1);
			SetAppointTileDanger(Index.y, Index.z - 1);
			break;
		case AnimType.EnemyLv26God1:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y - 1, Index.z + 1);
			SetAppointTileDanger(Index.y - 1, Index.z - 1);
			break;
		case AnimType.BeanLv28BasketballStandsLeft:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.TallTree);
			break;
		case AnimType.BeanLv28BasketballStandsRight:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.TallTree);
			break;
		case AnimType.BeanLv28BasketballStandsCenter:

			AdditionTransform.eulerAngles = Vector3.zero;

			break;
		case AnimType.EnemyLv28HatPlayer:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z, DangerType.FloaterAir);
			break;
		case AnimType.EnemyLv26Pillars1:
		case AnimType.EnemyLv26Dici:
		case AnimType.EnemyLv26God2:
		case AnimType.EnemyLv26DaDiShuDie:
		case AnimType.EnemyLv26Owl:
		case AnimType.EnemyLv26Tree:
		case AnimType.EnemyLv28BlackPlayer:
		case AnimType.EnemyLv28PlayerLeft:
		case AnimType.EnemyLv28PlayerRight:
		case AnimType.BeanLv28Button:

			AdditionTransform.eulerAngles = Vector3.zero;

			break;
		case AnimType.EnemyLv26FirePlace:

		{
			AdditionTransform.eulerAngles = new Vector3(0f, 180f, 180f);
			bool flag3 = LevelDesigner.GeneratingDonorThemeNumber == 43;
			AdditionTransform.Find("state01").Find("blue_fire").gameObject.SetActive(flag3);
			AdditionTransform.Find("state01").Find("red_fire").gameObject.SetActive(!flag3);
			SetAppointTileDanger(Index.y, Index.z);
			break;
		}
		case AnimType.BackwardQueryLv26shunyiStart:

			AdditionTransform.localPosition = new Vector3(0f, 0f, -0.6f);
			AdditionTransform.FindChild("state01").localPosition = Vector3.zero;
			AdditionTransform.eulerAngles = new Vector3(90f, 0f, 0f);

			break;
		case AnimType.EnemyLv28NetDie:

			AdditionTransform.eulerAngles = new Vector3(90f, -180f, 0f);
			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.TallTree);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.TallTree);
			break;
		case AnimType.EnemyLv26DaDiShuLife:

			AdditionTransform.eulerAngles = Vector3.zero;

			break;
		case AnimType.BeanBack6Tile:

			AdditionTransform.eulerAngles = new Vector3(-90f, 0f, 0f);
			if (!DeviceQualityChecker.QualityIsHigh())
			{
				GameObject gameObject2 = AdditionTransform.Find("state01/level_30_fantanban/tx_back/lizi").gameObject;
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			break;
		case AnimType.BeanJump10Tile:

			if (!DeviceQualityChecker.QualityIsHigh())
			{
				GameObject gameObject = AdditionTransform.Find("state01/jiduan/lizi").gameObject;
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			break;
		case AnimType.BeanLv28BouncePlate:

			AdditionTransform.eulerAngles = new Vector3(0f, 180f, 0f);
			break;
		case AnimType.EnemyLv34Snow:

			AdditionTransform.eulerAngles = new Vector3(0f, 0f, 180f);
			break;
		case AnimType.BeanLv40ShowButton:

			AdditionTransform.eulerAngles = new Vector3(90f, 180f, 0f);
			break;
		case AnimType.EnemyLv30Door:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y, Index.z + 2, DangerType.TallTree);
			SetAppointTileDanger(Index.y, Index.z - 2, DangerType.TallTree);
			break;
		case AnimType.EnemyLv30Frankenstein:
		case AnimType.EnemyLv30Tombstone:
		case AnimType.EnemyLv30Pumpkin2:
			AdditionTransform.eulerAngles = Vector3.zero;
			SetAppointTileDanger(Index.y - 1, Index.z);
			break;
		case AnimType.EnemyLv30Corpse:
		case AnimType.EnemyLv30WitchRight:
		case AnimType.EnemyLv30WitchLeft:
		case AnimType.EnemyLv30SpiderDie:
		case AnimType.EnemyLv30SpiderLife:
		case AnimType.EnemyLv30Pumpkin:
		case AnimType.EnemyLv30Vampire2:
		case AnimType.BeanLv30Cobweb:
		case AnimType.EnemyLv32LightLeft:
		case AnimType.EnemyLv32LightRight:
		case AnimType.EnemyLv34BigCane:
		case AnimType.EnemyLv34Tree1:
		case AnimType.EnemyLv37Forward1Tile:
		case AnimType.EnemyLv37Forward2Tile:
		case AnimType.EnemyLv37Forward3Tile:
		case AnimType.EnemyLv37Forward4Tile:
		case AnimType.EnemyLv37Forward5Tile:
		case AnimType.EnemyLv37Frisbee:
		case AnimType.EnemyLv37Pan:
		case AnimType.EnemyLv37Sound:
		case AnimType.EnemyLv40Column1:
		case AnimType.EnemyLv40Column2:
		case AnimType.EnemyLv40BackTriangle:
		case AnimType.EnemyLv40Snake:
		case AnimType.EnemyLv40LeftWall:
		case AnimType.EnemyLv40RightWall:
		case AnimType.EnemyLv40_5TileLeft:
		case AnimType.EnemyLv40_5TileRight:
		case AnimType.EnemyLv40_4TileLeft:
		case AnimType.EnemyLv40_4TileRight:
		case AnimType.EnemyLv40_3TileLeft:
		case AnimType.EnemyLv40_3TileRight:
		case AnimType.EnemyLv40_2TileLeft:
		case AnimType.EnemyLv40_2TileRight:

			AdditionTransform.eulerAngles = Vector3.zero;

			break;
		case AnimType.EnemyLv40Triangle:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 2);
			SetAppointTileDanger(Index.y, Index.z - 2);
			SetAppointTileDanger(Index.y + 1, Index.z + 2);
			SetAppointTileDanger(Index.y + 1, Index.z - 2);
			SetAppointTileDanger(Index.y - 1, Index.z + 2);
			SetAppointTileDanger(Index.y - 1, Index.z - 2);
			SetAppointTileDanger(Index.y - 1, Index.z, DangerType.EnemyLv32LightLeft);
			SetAppointTileDanger(Index.y + 1, Index.z, DangerType.EnemyLv32LightLeft);
			break;
		case AnimType.EnemyLv40Eye:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.EnemyLv32LightLeft);
			break;
		case AnimType.EnemyLv40Door:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.EnemyLv32LightLeft);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.EnemyLv32LightLeft);
			break;
		case AnimType.EnemyLv37Gear:
		case AnimType.EnemyLv40Bug:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1);
			SetAppointTileDanger(Index.y, Index.z - 1);
			break;
		case AnimType.EnemyLv37RobotLeft:
		case AnimType.EnemyLv37KeyboardLeft:
		case AnimType.EnemyLv40PyramidLeft:
		case AnimType.EnemyLv40_CrossLeft:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z - 1);
			break;
		case AnimType.EnemyLv37RobotRight:
		case AnimType.EnemyLv37KeyboardRight:
		case AnimType.EnemyLv40PyramidRight:
		case AnimType.EnemyLv40_CrossRight:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1);
			break;
		case AnimType.EnemyLv37Door:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.TallTree);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.TallTree);
			SetAppointTileDanger(Index.y, Index.z + 2, DangerType.TallTree);
			SetAppointTileDanger(Index.y, Index.z - 2, DangerType.TallTree);
			break;
		case AnimType.EnemyLv30MoveBeforeTile:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y - 2, Index.z);
			break;
		case AnimType.EnemyLv30MoveAfterTile:
		case AnimType.EnemyLv40PyramidBack:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y + 2, Index.z);
			break;
		case AnimType.EnemyLv32AirWheel:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.FloaterAir);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.FloaterAir);
			break;
		case AnimType.EnemyLv32HorizontalLever:

			AdditionTransform.eulerAngles = new Vector3(90f, 180f, 0f);
			SetAppointTileDanger(Index.y, Index.z + 1, DangerType.EnemyLv26Dici);
			SetAppointTileDanger(Index.y, Index.z - 1, DangerType.EnemyLv26Dici);
			break;
		case AnimType.EnemyLv32VerticalLever:

			AdditionTransform.eulerAngles = new Vector3(0f, -90f, 90f);
			SetAppointTileDanger(Index.y - 1, Index.z, DangerType.EnemyLv26Dici);
			SetAppointTileDanger(Index.y + 1, Index.z, DangerType.EnemyLv26Dici);
			break;
		case AnimType.EnemyLv32Cannon:

			AdditionTransform.eulerAngles = Vector3.zero;

			SetAppointTileDanger(Index.y - 1, Index.z, DangerType.EnemyLv26Dici);
			SetAppointTileDanger(Index.y - 2, Index.z, DangerType.EnemyLv26Dici);
			SetAppointTileDanger(Index.y - 3, Index.z, DangerType.EnemyLv26Dici);
			break;
		default: return false;
		}
		return true;
	}
}
