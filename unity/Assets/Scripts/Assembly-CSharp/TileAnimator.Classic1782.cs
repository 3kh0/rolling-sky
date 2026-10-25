using UnityEngine;

// Ported from the Mono 1.7.8.2 source; selected by resource identity, not slot.
public partial class TileAnimator
{
	private static bool Animate1782Tile(int animRowNum, int animTileNum)
	{
		if (!ExpansionGameplay.Classic1782) return false;
		switch (animatedRows[animRowNum][animTileNum].animType)
		{
		case Tile.AnimType.GroundTileLeft:

				Native1782MoveJumpTileLeft(animRowNum, animTileNum);
				break;
		case Tile.AnimType.GroundTileRight:

				Native1782MoveJumpTileRight(animRowNum, animTileNum);
				break;
		case Tile.AnimType.GroundTileBefore:

				Native1782MoveJumpTileAfter(animRowNum, animTileNum);
				break;
		case Tile.AnimType.GroundTileAfter:

				Native1782MoveJumpTileBefore(animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv26Door:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 9f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Pillars1:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26God1:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 15f, -30f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Castle:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 12f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Pillars2:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Dici:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26God2:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26DaDiShuLife:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 0f, -20f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26DaDiShuDie:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Owl:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -20f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv26Tree:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 13f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv34Snow:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -3f, -15f);
				break;
		case Tile.AnimType.EnemyLv26FirePlace:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -3f, -5f);
				break;
		case Tile.AnimType.BackwardQueryLv26shunyiStart:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -3f, -2f);
				break;
		case Tile.AnimType.EnemyLv28Wall:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 13f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28Wall2:

				Native1782NotPlayAnimation(animRowNum, animTileNum, 18f, -3f, -15f);
				break;
		case Tile.AnimType.EnemyLv28BlackPlayer:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28PlayerLeft:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28PlayerRight:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28HatPlayer:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28Tyre:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28Doors:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28Doors2:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.BeanLv28BasketballStandsLeft:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, string.Empty, false);
				break;
		case Tile.AnimType.BeanLv28BasketballStandsRight:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, string.Empty, false);
				break;
		case Tile.AnimType.BeanLv28BasketballStandsCenter:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, string.Empty, false);
				break;
		case Tile.AnimType.BeanLv28Button:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 0f, -20f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv30MoveAfterTile:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 5f, -15f, "anim1", true);
				break;
		case Tile.AnimType.EnemyLv30MoveBeforeTile:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 5f, -15f, "anim1", true);
				break;
		case Tile.AnimType.BeanBack6Tile:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -5f, -5f);
				break;
		case Tile.AnimType.BeanJump10Tile:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -3f, -10f);
				break;
		case Tile.AnimType.BeanLv32StartReverse:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -5f, -5f);
				break;
		case Tile.AnimType.BeanLv32EndReverse:

				Native1782NoAnimation(animRowNum, animTileNum, 18f, -3f, -5f);
				break;
		case Tile.AnimType.EnemyLv32Door:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", true, Tile.AnimType.EnemyLv32Cannon);
				break;
		case Tile.AnimType.EnemyLv32AirWheel:

				Native1782AnimatorPlayCreatReset(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv32LightLeft:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", true, Tile.AnimType.EnemyLv32Cannon);
				break;
		case Tile.AnimType.EnemyLv32LightRight:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 10f, -15f, "anim1", true, Tile.AnimType.EnemyLv32Cannon);
				break;
		case Tile.AnimType.EnemyLv32Cannon:

				Native1782AnimatorPlayShakeCamerastatic(animRowNum, animTileNum, 18f, -3f, 7f, -15f, "anim1", true, Tile.AnimType.EnemyLv32Cannon);
				break;
		case Tile.AnimType.EnemyLv34BigCane:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv34Tree1:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37Forward1Tile:

				Native1782MoveTile(1, animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv37Forward2Tile:

				Native1782MoveTile(2, animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv37Forward3Tile:

				Native1782MoveTile(3, animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv37Forward4Tile:

				Native1782MoveTile(4, animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv37Forward5Tile:

				Native1782MoveTile(5, animRowNum, animTileNum);
				break;
		case Tile.AnimType.EnemyLv37Frisbee:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37Pan:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37Sound:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37RobotLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37RobotRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37Door:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37KeyboardRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37KeyboardLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv37Gear:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40PyramidBack:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40PyramidRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40PyramidLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Bug:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Door:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Column1:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Column2:

				Native1782AnimatorPlay(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Eye:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40BackTriangle:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Triangle:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40Snake:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40LeftWall:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40RightWall:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_5TileLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_5TileRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_4TileLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_4TileRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_3TileLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_3TileRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_2TileLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_2TileRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_CrossLeft:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv40_CrossRight:

				Native1782AnimatorPlay2(animRowNum, animTileNum, 18f, -3f, 8f, -15f, "anim1", false);
				break;
		case Tile.AnimType.EnemyLv28NetDie:
		case Tile.AnimType.BeanLv28BouncePlate:
		case Tile.AnimType.EnemyLv32VerticalLever:
		case Tile.AnimType.EnemyLv32HorizontalLever:
		case Tile.AnimType.BeanLv40ShowButton:

				break;
					default: return false;
		}
		return true;
	}
	private static AnimatedRow native1782Row;
	private static float native1782Distance;
	private static Animation native1782Animation;
	private static void Native1782NoAnimation(int animRowNum, int animTileNum, float showAnimMaxDist, float showAnimMinDis, float resetAnimDis)
	{
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (native1782Distance >= showAnimMaxDist)
		{
			return;
		}
		if (native1782Distance < showAnimMaxDist && !native1782Row.IsPlayed && native1782Distance > showAnimMinDis && native1782Row.FirstCheck(animTileNum))
		{
			Tile tile = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index];
			tile.AdditionTransform.gameObject.SetActive(true);
			native1782Row.IsPlayed = true;
		}
		else if (native1782Distance < resetAnimDis && !native1782Row.IsReset && native1782Row.SecondCheck(animTileNum))
		{
			GameObject gameObject = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AdditionTransform.gameObject;
			if (gameObject != null)
			{
				GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].AdditionTransform.gameObject.SetActive(false);
			}
			native1782Row.IsReset = true;
		}
	}

	private static void Native1782NotPlayAnimation(int animRowNum, int animTileNum, float showAnimMaxDist, float showAnimMinDis, float resetAnimDis)
	{
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (!(native1782Distance >= showAnimMaxDist))
		{
			if (native1782Distance < showAnimMaxDist && native1782Distance > showAnimMinDis)
			{
				Tile tile = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index];
				native1782Animation = tile.newAnimTrasFromAnimation;
				native1782Animation.gameObject.SetActive(true);
			}
			else if (native1782Distance < resetAnimDis)
			{
				native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
				Native1782ResetAnimation(native1782Animation);
			}
		}
	}

	private static void Native1782AnimatorPlay(int animRowNum, int animTileNum, float dis1, float dis2, float dis3, float dis4, string animName, bool sound, Tile.AnimType animType = Tile.AnimType.TeslaSlider)
	{
		Tile tile = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index];
		if (tile != null && tile.IsHidden)
		{
			return;
		}
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (native1782Distance >= dis1)
		{
			return;
		}
		if (native1782Distance > dis2)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			native1782Animation.gameObject.SetActive(true);
			if (native1782Distance < dis3 && !native1782Row.IsPlayed && native1782Row.FirstCheck(animTileNum))
			{
				if (sound)
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				if (!animName.Equals(string.Empty))
				{
					native1782Animation.Play(animName);
				}
				native1782Row.IsPlayed = true;
			}
		}
		else if (native1782Distance < dis4)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			Native1782ResetAnimation(native1782Animation, animType);
		}
	}

	private static void Native1782AnimatorPlay2(int animRowNum, int animTileNum, float dis1, float dis2, float dis3, float dis4, string animName, bool sound, Tile.AnimType animType = Tile.AnimType.TeslaSlider)
	{
		Tile tile = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index];
		if (tile != null && tile.IsHidden)
		{
			return;
		}
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (native1782Distance >= dis1)
		{
			return;
		}
		if (native1782Distance > dis2)
		{
			if (native1782Distance < dis3 && !native1782Row.IsPlayed && native1782Row.FirstCheck(animTileNum))
			{
				native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
				native1782Animation.gameObject.SetActive(true);
				if (sound)
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				if (!animName.Equals(string.Empty))
				{
					native1782Animation.Play(animName);
				}
				native1782Row.IsPlayed = true;
			}
		}
		else if (native1782Distance < dis4)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			Native1782ResetAnimation(native1782Animation, animType);
		}
	}

	private static void Native1782MoveTile(int MoveTileNum, int animRowNum, int animTileNum)
	{
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (!(native1782Row.Distance > 5f) && !(native1782Row.Distance < -5f))
		{
			float num = 5f - native1782Row.Distance;
			if (num > (float)MoveTileNum)
			{
				num = MoveTileNum;
			}
			Tile tile = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index];
			TransformUtils.SetY(tile.AdditionTransform, num, true);
		}
	}

	private static void Native1782AnimatorPlayCreatReset(int animRowNum, int animTileNum, float dis1, float dis2, float dis3, float dis4, string animName, bool sound)
	{
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (native1782Distance >= dis1)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			if (native1782Animation != null && native1782Animation[animName].normalizedTime != 0f)
			{
				native1782Animation.Play();
				native1782Animation[animName].normalizedTime = 0f;
				native1782Animation.Sample();
				native1782Animation.Stop();
			}
		}
		else if (native1782Distance > dis2)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			native1782Animation.gameObject.SetActive(true);
			if (native1782Distance < dis3 && !native1782Row.IsPlayed && native1782Row.FirstCheck(animTileNum))
			{
				if (sound)
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				if (!animName.Equals(string.Empty))
				{
					native1782Animation.Play();
					native1782Animation[animName].normalizedTime = 0f;
					native1782Animation.Sample();
					native1782Animation.Stop();
					native1782Animation[animName].speed = 1f;
					native1782Animation.Play(animName);
				}
				native1782Row.IsPlayed = true;
			}
			else if (native1782Distance <= 0f && native1782Row.SecondCheck(animTileNum))
			{
				native1782Animation[animName].speed = 0f;
			}
		}
		else if (native1782Distance < dis4)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			native1782Animation.Play();
			native1782Animation[animName].normalizedTime = 0f;
			native1782Animation.Sample();
			native1782Animation.Stop();
			Native1782ResetAnimation(native1782Animation);
		}
	}

	public static void Native1782AnimatorPlayShakeCamerastatic(int animRowNum, int animTileNum, float dis1, float dis2, float dis3, float dis4, string animName, bool sound, Tile.AnimType animType = Tile.AnimType.TeslaSlider)
	{
		native1782Row = animatedRows[animRowNum];
		native1782Distance = native1782Row.Distance;
		if (native1782Distance >= dis1)
		{
			return;
		}
		if (native1782Distance > dis2)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			native1782Animation.gameObject.SetActive(true);
			if (native1782Distance < dis3 && native1782Distance > dis3 - 3f && !native1782Row.IsPlayed && native1782Row.FirstCheck(animTileNum))
			{
				if (sound)
				{
					GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].PlaySound();
				}
				if (!animName.Equals(string.Empty))
				{
					native1782Animation.Play(animName);
				}
				native1782Row.IsPlayed = true;
			}
			else if (native1782Distance < dis3 - 3f && native1782Row.SecondCheck(animTileNum))
			{
				// The accepted camera has no donor post-effect argument.
				ShakeCamera(0.8f);
			}
		}
		else if (native1782Distance < dis4)
		{
			native1782Animation = GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].newAnimTrasFromAnimation;
			Native1782ResetAnimation(native1782Animation, animType);
		}
	}

	private static void Native1782ResetAnimation(Animation anim, Tile.AnimType animType = Tile.AnimType.Riser)
	{
		if (animType != Tile.AnimType.EnemyLv32Cannon)
		{
			anim.gameObject.transform.localPosition = Vector3.zero;
		}
		anim.gameObject.SetActive(false);
	}

	private static void Native1782MoveJumpTileLeft(int animRowNum, int animTileNum)
	{
		Native1782BaseMoveJumpTile(animRowNum, animTileNum, 1, 2, true);
	}

	private static void Native1782MoveJumpTileRight(int animRowNum, int animTileNum)
	{
		Native1782BaseMoveJumpTile(animRowNum, animTileNum, 1, 2, false);
	}

	private static void Native1782BaseMoveJumpTile(int animRowNum, int animTileNum, int offset, int beginLoopNum, bool ifLeft)
	{
		float num = 4f;
		float num2 = 2 * offset;
		float distance = animatedRows[animRowNum].Distance;
		float num3 = 0f;
		if (!(distance > 16f))
		{
			float num4 = distance / (num2 * num) - (float)Mathf.FloorToInt(distance / (num2 * num));
			num3 = ((!(num4 > 0.5f)) ? ((float)offset * num4 * 2f) : ((float)offset * (1f - num4) * 2f));
			num3 = ((!ifLeft) ? (0f - num3) : num3);
			TransformUtils.SetX(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].GeoTransform, num3, true);
		}
	}

	private static void Native1782MoveJumpTileBefore(int animRowNum, int animTileNum)
	{
		Native1782BaseMoveJumpTileTop(animRowNum, animTileNum, 1, 2, true);
	}

	private static void Native1782MoveJumpTileAfter(int animRowNum, int animTileNum)
	{
		Native1782BaseMoveJumpTileTop(animRowNum, animTileNum, 1, 2, false);
	}

	private static void Native1782BaseMoveJumpTileTop(int animRowNum, int animTileNum, int offset, int beginLoopNum, bool ifTop)
	{
		float num = 4f;
		float num2 = 2 * offset;
		float distance = animatedRows[animRowNum].Distance;
		float num3 = 0f;
		if (!(distance > 16f))
		{
			float num4 = distance / (num2 * num) - (float)Mathf.FloorToInt(distance / (num2 * num));
			num3 = ((!(num4 > 0.5f)) ? ((float)offset * num4 * 2f) : ((float)offset * (1f - num4) * 2f));
			num3 = ((!ifTop) ? (0f - num3) : num3);
			TransformUtils.SetY(GameManager.TileBoard[animatedRows[animRowNum][animTileNum].index].GeoTransform, num3, true);
		}
	}
}
