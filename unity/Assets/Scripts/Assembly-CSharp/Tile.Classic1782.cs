using UnityEngine;

// Ported from the Mono 1.7.8.2 source; selected by resource identity, not slot.
public partial class Tile
{
	private bool Configure1782Enemy()
	{
		if (!ExpansionGameplay.Classic1782) return false;
		switch (dangerType)
		{
		case DangerType.EnemyLv26Door:

			animType = AnimType.EnemyLv26Door;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Door);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26Pillars1:

			animType = AnimType.EnemyLv26Pillars1;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Pillars1);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26God1:

			animType = AnimType.EnemyLv26God1;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26God1);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26Castle:

			animType = AnimType.EnemyLv26Castle;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Castle);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26Pillars2:

			animType = AnimType.EnemyLv26Pillars2;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Pillars2);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26Dici:

			animType = AnimType.EnemyLv26Dici;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Dici);
			HasAudio = false;
			HasAltAudio = false;
			IsGeoHidden = true;
			break;
		case DangerType.EnemyLv26God2:

			animType = AnimType.EnemyLv26God2;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26God2);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26DaDiShuDie:

			animType = AnimType.EnemyLv26DaDiShuDie;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26DaDiShuDie);
			IsGeoHidden = true;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26DaDiShuLife:

			animType = AnimType.EnemyLv26DaDiShuLife;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26DaDiShuLife);
			IsDangerous = false;
			IsGeoHidden = true;
			HasAudio = false;
			HasAltAudio = false;
			IsJump = true;
			break;
		case DangerType.EnemyLv26Owl:

			animType = AnimType.EnemyLv26Owl;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Owl);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv26Tree:

			animType = AnimType.EnemyLv26Tree;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv26Tree);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28Wall:

			animType = AnimType.EnemyLv28Wall;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28Wall);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28Wall2:

			animType = AnimType.EnemyLv28Wall2;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28Wall2);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28BlackPlayer:

			animType = AnimType.EnemyLv28BlackPlayer;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28BlackPlayer);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28PlayerLeft:

			animType = AnimType.EnemyLv28PlayerLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28PlayerLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28PlayerRight:

			animType = AnimType.EnemyLv28PlayerRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28PlayerRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28HatPlayer:

			animType = AnimType.EnemyLv28HatPlayer;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28HatPlayer);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28Tyre:

			animType = AnimType.EnemyLv28Tyre;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28Tyre);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28Doors:

			animType = AnimType.EnemyLv28Doors;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28Doors);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28Doors2:

			animType = AnimType.EnemyLv28Doors2;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28Doors2);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv28NetDie:

			animType = AnimType.EnemyLv28NetDie;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv28NetDie);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv30MoveAfterTile:

			animType = AnimType.EnemyLv30MoveAfterTile;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv30MoveAfterTile);
			HasAudio = true;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv30MoveBeforeTile:

			animType = AnimType.EnemyLv30MoveBeforeTile;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv30MoveBeforeTile);
			HasAudio = true;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32Door:

			animType = AnimType.EnemyLv32Door;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32Door);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32AirWheel:

			animType = AnimType.EnemyLv32AirWheel;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32AirWheel);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32LightLeft:

			animType = AnimType.EnemyLv32LightLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32LightLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32LightRight:

			animType = AnimType.EnemyLv32LightRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32LightRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32HorizontalLever:

			animType = AnimType.EnemyLv32HorizontalLever;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32HorizontalLever);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32VerticalLever:

			animType = AnimType.EnemyLv32VerticalLever;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32VerticalLever);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv32Cannon:

			animType = AnimType.EnemyLv32Cannon;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv32Cannon);
			HasAudio = true;
			HasAltAudio = true;
			break;
		case DangerType.EnemyLv34BigCane:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv34BigCane;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv34BigCane);
			HasAudio = true;
			HasAltAudio = true;
			break;
		case DangerType.EnemyLv34Tree1:

			animType = AnimType.EnemyLv34Tree1;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv34Tree1);
			HasAudio = true;
			HasAltAudio = true;
			break;
		case DangerType.EnemyLv37Forward1Tile:
		case DangerType.EnemyLv37Forward2Tile:
		case DangerType.EnemyLv37Forward3Tile:
		case DangerType.EnemyLv37Forward4Tile:
		case DangerType.EnemyLv37Forward5Tile:

			if (dangerType == DangerType.EnemyLv37Forward1Tile)
			{
				animType = AnimType.EnemyLv37Forward1Tile;
			}
			else if (dangerType == DangerType.EnemyLv37Forward2Tile)
			{
				animType = AnimType.EnemyLv37Forward2Tile;
			}
			else if (dangerType == DangerType.EnemyLv37Forward3Tile)
			{
				animType = AnimType.EnemyLv37Forward3Tile;
			}
			else if (dangerType == DangerType.EnemyLv37Forward4Tile)
			{
				animType = AnimType.EnemyLv37Forward4Tile;
			}
			else if (dangerType == DangerType.EnemyLv37Forward5Tile)
			{
				animType = AnimType.EnemyLv37Forward5Tile;
			}
			IsGeoHidden = true;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Forward1Tile);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37Frisbee:

			animType = AnimType.EnemyLv37Frisbee;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Frisbee);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37Pan:

			animType = AnimType.EnemyLv37Pan;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Pan);
			IsDangerous = false;
			IsJump = true;
			IsSuperJump = true;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37Sound:

			animType = AnimType.EnemyLv37Sound;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Sound);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37RobotLeft:

			animType = AnimType.EnemyLv37RobotLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37RobotLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37RobotRight:

			animType = AnimType.EnemyLv37RobotRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37RobotRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37Door:

			animType = AnimType.EnemyLv37Door;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Door);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37KeyboardRight:

			animType = AnimType.EnemyLv37KeyboardRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37KeyboardRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37KeyboardLeft:

			animType = AnimType.EnemyLv37KeyboardLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37KeyboardLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv37Gear:

			animType = AnimType.EnemyLv37Gear;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv37Gear);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40PyramidBack:

			animType = AnimType.EnemyLv40PyramidBack;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40PyramidBack);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40PyramidRight:

			animType = AnimType.EnemyLv40PyramidRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40PyramidRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40PyramidLeft:

			animType = AnimType.EnemyLv40PyramidLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40PyramidLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Bug:

			animType = AnimType.EnemyLv40Bug;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Bug);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Door:

			animType = AnimType.EnemyLv40Door;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Door);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Column1:

			animType = AnimType.EnemyLv40Column1;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Column1);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Column2:

			animType = AnimType.EnemyLv40Column2;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Column2);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Eye:

			animType = AnimType.EnemyLv40Eye;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Eye);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40BackTriangle:

			animType = AnimType.EnemyLv40BackTriangle;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40BackTriangle);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Triangle:

			animType = AnimType.EnemyLv40Triangle;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Triangle);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40Snake:

			animType = AnimType.EnemyLv40Snake;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40Snake);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40LeftWall:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40LeftWall;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40LeftWall);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40RightWall:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40RightWall;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40RightWall);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_5TileLeft:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_5TileLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_5TileLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_5TileRight:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_5TileRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_5TileRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_4TileLeft:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_4TileLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_4TileLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_4TileRight:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_4TileRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_4TileRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_3TileLeft:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_3TileLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_3TileLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_3TileRight:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_3TileRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_3TileRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_2TileLeft:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_2TileLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_2TileLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_2TileRight:

			IsGeoHidden = true;
			animType = AnimType.EnemyLv40_2TileRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_2TileRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_CrossLeft:

			animType = AnimType.EnemyLv40_CrossLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_CrossLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case DangerType.EnemyLv40_CrossRight:

			animType = AnimType.EnemyLv40_CrossRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.EnemyLv40_CrossRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		default: return false;
		}
		return true;
	}
	private bool Configure1782Bean()
	{
		if (!ExpansionGameplay.Classic1782 || TypeVal < 2000) return false;
		HasAddition = true;
		switch (TypeVal)
		{
		case (int)TileVal.EnemyLv26FirePlace:

			animType = AnimType.EnemyLv26FirePlace;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.EnemyLv26FirePlace);
			HasAudio = false;
			HasAltAudio = false;
			IsAdditionSwitchable = false;
			break;
		case (int)TileVal.BeanLv28BouncePlate:

			animType = AnimType.BeanLv28BouncePlate;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv28BouncePlate);
			IsAdditionSwitchable = false;
			HasAudio = false;
			HasAltAudio = false;
			IsJump = true;
			IsSuperJump = true;
			IsDangerous = false;
			break;
		case (int)TileVal.BeanLv28Net:

			animType = AnimType.BigBean;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv28Net);
			IsAdditionSwitchable = false;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanSimulationNet:

			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanSimulationNet);
			IsAdditionSwitchable = false;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv28BasketballStandsLeft:

			animType = AnimType.BeanLv28BasketballStandsLeft;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.BeanLv28BasketballStandsLeft);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv28BasketballStandsRight:

			animType = AnimType.BeanLv28BasketballStandsRight;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.BeanLv28BasketballStandsRight);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv28BasketballStandsCenter:

			animType = AnimType.BeanLv28BasketballStandsCenter;
			AdditionPrefabID = new PrefabID(PrefabType.Enemy, PrefabName.BeanLv28BasketballStandsCenter);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.EnemySimulationDie:

			IsGeoHidden = true;
			HasAddition = false;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv28Button:

			animType = AnimType.BeanLv28Button;
			if (LevelDesigner.GeneratingDonorThemeNumber == 50 || LevelDesigner.GeneratingDonorThemeNumber == 51)
			{
				AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv32Button);
			}
			else
			{
				AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv28Button);
			}
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanJump10Tile:

			animType = AnimType.BeanJump10Tile;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanJump10Tile);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanBack6Tile:

			IsGeoHidden = true;
			animType = AnimType.BeanBack6Tile;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanBack6Tile);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv32StartReverse:

			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv32StartReverse);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv32EndReverse:

			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv32EndReverse);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.EnemyLv34Snow:

			animType = AnimType.EnemyLv34Snow;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.EnemyLv34Snow);
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BeanLv40ShowButton:

			animType = AnimType.BeanLv40ShowButton;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv40ShowButton);
			HasAudio = false;
			HasAltAudio = false;
			IsJump = true;
			break;
		case (int)TileVal.BeanLv40HideButton:

			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BeanLv40HideButton);
			HasAudio = false;
			HasAltAudio = false;
			break;
				default: return false;
		}
		return true;
	}
	private void Configure1782Query()
	{
		HasAddition = true;
		switch (TypeVal)
		{
		case (int)TileVal.BackwardQueryLv26shunyiStart:

			animType = AnimType.BackwardQueryLv26shunyiStart;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BackwardQueryLv26shunyiStart);
			IsAdditionSwitchable = false;
			HasAudio = false;
			HasAltAudio = false;
			break;
		case (int)TileVal.BackwardQueryLv26shunyiEnd:

			animType = AnimType.BackwardQueryLv26shunyiStart;
			AdditionPrefabID = new PrefabID(PrefabType.Bean, PrefabName.BackwardQueryLv26shunyiEnd);
			IsAdditionSwitchable = false;
			HasAudio = false;
			HasAltAudio = false;
			break;
				}
	}
}
