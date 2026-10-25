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
