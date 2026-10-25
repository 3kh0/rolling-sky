using System;
using UnityEngine;

// Ported from the Mono 1.7.8.2 source; selected by resource identity, not slot.
public partial class Tile
{
	public bool IsHidden { get; set; }
	public bool IsShootBall { get; private set; }
	public bool isQuery;
	public bool Is1782MovingGround { get { return animType == AnimType.GroundTileLeft || animType == AnimType.GroundTileRight || animType == AnimType.GroundTileBefore || animType == AnimType.GroundTileAfter; } }

	public void HideRoot(bool hidden)
	{
		IsHidden = hidden;
		gameObject.SetActive(!hidden);
	}

	private bool Try1782KillBall(DangerType dangerTypeUsed, bool ballJumping, Vector3 ballPos, out bool kill)
	{
		kill = false;
		if (!ExpansionGameplay.Classic1782) return false;
		switch (dangerTypeUsed)
		{
		// Recovered 3.2.5 native Tile.ShouldKillBall (0x583c54).
		case DangerType.Enemy_807_Level69_mofang:
			kill = true;
			break;
		case DangerType.Enemy_808_Level69_yanjing:
		case DangerType.Enemy_809_Level69_banyuanL:
		case DangerType.Enemy_810_Level69_banyuanR:
			kill = !ballJumping;
			break;
		case DangerType.EnemyLv42ThreeGlass:
		case DangerType.EnemyLv42MonocycleRight:
		case DangerType.EnemyLv42MonocycleLeft:
		case DangerType.BeanLv42ClownDown:
		case DangerType.EnemyLv42Circle:
		case DangerType.SimulationAirKillBall:
			kill = false;
			break;
		case DangerType.EnemyLv42ClownUp:
		case DangerType.EnemyLv42PendulumLeft:
		case DangerType.EnemyLv42PendulumRight:
		case DangerType.EnemyLv42TrojanLeft:
		case DangerType.EnemyLv42TrojanRight:
			kill = true;
			break;
		case DangerType.EnemyLv42Column:
		case DangerType.EnemyLv42Rise:
		case DangerType.EnemyLv42FieryRings:
			kill = !ballJumping;
			break;
		case DangerType.EnemyLv42Door:
			kill = ballJumping;
			break;
		case DangerType.EnemyLv26DaDiShuLife:
		case DangerType.EnemyLv34Snow:
		case DangerType.EnemyLv28HatPlayer:
		case DangerType.EnemyLv30Door:
		case DangerType.EnemyLv30MoveAfterTile:
		case DangerType.EnemyLv30MoveBeforeTile:
		case DangerType.EnemyLv37Pan:
		case DangerType.EnemyLv37Door:
		case DangerType.EnemyLv40PyramidBack:
		case DangerType.EnemyLv40PyramidRight:
		case DangerType.EnemyLv40PyramidLeft:
		case DangerType.EnemyLv40LeftWall:
		case DangerType.EnemyLv40RightWall:
		case DangerType.EnemyLv40_5TileLeft:
		case DangerType.EnemyLv40_5TileRight:
		case DangerType.EnemyLv40_4TileLeft:
		case DangerType.EnemyLv40_4TileRight:
		case DangerType.EnemyLv40_3TileLeft:
		case DangerType.EnemyLv40_3TileRight:
		case DangerType.EnemyLv40_2TileLeft:
		case DangerType.EnemyLv40_2TileRight:

				kill = false;
				break;
		case DangerType.EnemyLv26Pillars1:
		case DangerType.EnemyLv26God1:
		case DangerType.EnemyLv26Pillars2:
		case DangerType.EnemyLv26God2:
		case DangerType.EnemyLv26Tree:
		case DangerType.EnemyLv30Frankenstein:
		case DangerType.EnemyLv30Corpse:
		case DangerType.EnemyLv30Tombstone:
		case DangerType.EnemyLv30Vampire:
		case DangerType.EnemyLv37Sound:
		case DangerType.EnemyLv40Column2:
		case DangerType.EnemyLv40Eye:
		case DangerType.EnemyLv40BackTriangle:
		case DangerType.EnemyLv40Triangle:
		case DangerType.EnemyLv40Snake:

				kill = true;
				break;
		case DangerType.EnemyLv26Dici:
		case DangerType.EnemyLv26DaDiShuDie:
		case DangerType.EnemyLv28BlackPlayer:
		case DangerType.EnemyLv28PlayerLeft:
		case DangerType.EnemyLv28PlayerRight:
		case DangerType.EnemyLv28Tyre:
		case DangerType.EnemyLv30SpiderDie:
		case DangerType.EnemyLv30Pumpkin:
		case DangerType.EnemyLv30Pumpkin2:
		case DangerType.EnemyLv30Vampire2:
		case DangerType.EnemyLv32VerticalLever:
		case DangerType.EnemyLv32HorizontalLever:
		case DangerType.EnemyLv34BigCane:
		case DangerType.EnemyLv34Tree1:
		case DangerType.EnemyLv37Frisbee:
		case DangerType.EnemyLv37RobotLeft:
		case DangerType.EnemyLv37RobotRight:
		case DangerType.EnemyLv37KeyboardRight:
		case DangerType.EnemyLv37Gear:
		case DangerType.EnemyLv37KeyboardLeft:
		case DangerType.EnemyLv40Bug:
		case DangerType.EnemyLv40Column1:
		case DangerType.EnemyLv40_CrossLeft:
		case DangerType.EnemyLv40_CrossRight:

				kill = !ballJumping;
				break;
		case DangerType.EnemyLv26Door:
		case DangerType.EnemyLv26Castle:
		case DangerType.EnemyLv26Owl:
		case DangerType.EnemyLv28Doors:
		case DangerType.EnemyLv30WitchRight:
		case DangerType.EnemyLv30WitchLeft:
		case DangerType.EnemyLv30SpiderLife:
		case DangerType.EnemyLv32AirWheel:
		case DangerType.EnemyLv40Door:

				kill = ballJumping;
				break;
		case DangerType.EnemyLv28Wall:
		case DangerType.EnemyLv28Doors2:
		case DangerType.EnemyLv28NetDie:
		case DangerType.EnemyLv32LightLeft:
		case DangerType.EnemyLv32LightRight:

				kill = true;
				break;
		case DangerType.EnemyLv28Wall2:

				kill = !IsShootBall;
				break;
		case DangerType.EnemyLv32Cannon:

				kill = true;
				break;
		case DangerType.EnemyLv32Door:

				kill = ((!IsShootBall && !ballJumping) ? true : false);
				break;
		case DangerType.EnemyLv37Forward1Tile:
		case DangerType.EnemyLv37Forward2Tile:
		case DangerType.EnemyLv37Forward3Tile:
		case DangerType.EnemyLv37Forward4Tile:
		case DangerType.EnemyLv37Forward5Tile:

				IsFallen = true;
				break;
		default: return false;
		}
		return true;
	}
	private bool Eat1782Bean()
	{
		if (!ExpansionGameplay.Classic1782) return false;
		switch (TypeVal)
		{
		// Signed Mono 1.7.9.2 Tile.EatBean:8089-8117. Circle occupies
		// the opposite half of the aerial trigger on its adjacent lane.
		case (int)TileVal.SimulationAirKillBall:
		{
			if (GameManager.theBall.IsJumping)
			{
				MathUtils.IntTrio previous = new MathUtils.IntTrio(SegmentIndex, RowIndex, TileIndex - 1);
				bool circleOnLeft = GameManager.TileBoard.IsValid(previous) && GameManager.TileBoard[previous] != null &&
					GameManager.TileBoard[previous].TypeVal == (int)TileVal.EnemyLv42Circle;
				if (circleOnLeft ? Ball.Position.x <= TileIndex + 0.5f : TileIndex + 0.5f <= Ball.Position.x)
					GameManager.KillBall(DangerType.EnemyLv42Door);
			}
			isEat = true;
			return true;
		}
		case (int)TileVal.BeanLv25Accelerate:
			GameManager.theBall.mBallSpeedcoefficient = 2f;
			if (UserSettings.IsSoundOn() && !GameManager.DisableSounds) GameManager.theBall.AccelerateSound.Play();
			isEat = true;
			break;
		case (int)TileVal.BeanLv25Deceleration:
		case (int)TileVal.BeanLv30Cobweb:
		case (int)TileVal.BeanSimulationDeceleration:
			GameManager.theBall.mBallSpeedcoefficient = 0.5f;
			if (UserSettings.IsSoundOn() && !GameManager.DisableSounds) GameManager.theBall.DecelerationSound.Play();
			isEat = true;
			break;
		case (int)TileVal.BeanLv25BrigdeUp:
			GameManager.theBall.state = Ball.State.BrideUp;
			isEat = true;
			break;
		case (int)TileVal.BeanLv25BrigdeDown:
			GameManager.theBall.state = Ball.State.BrideDown;
			isEat = true;
			break;
		case (int)TileVal.BeanLv28BouncePlate:

			if (GameManager.theBall.state != Ball.State.Jumping)
			{
				CameraControl.ShakeCamera(0.25f);
				GameManager.theBall.mJumpState = Ball.JumpState.ShootBasketball;
				isEat = true;
			}
			break;
		case (int)TileVal.BeanLv28BasketballStandsLeft:
		case (int)TileVal.BeanLv28BasketballStandsRight:
		case (int)TileVal.BeanLv28BasketballStandsCenter:

			if (Math.Abs(Ball.Position.x - ((float)TileIndex + 0.5f)) <= 0.4f)
			{
				GameManager.theBall.m_tempBallPos = Vector3.zero;
				CameraControl.ShakeCamera(0.3f);
				GameManager.theBall.mChangeBallXPostion = (float)TileIndex + 0.5f;
				GameManager.theBall.state = Ball.State.ShootBall;
			}
			else
			{
				GameObject gameObject = AdditionTransform.FindChild("nani/part1/tx_Miss").gameObject;
				gameObject.SetActive(true);
				ParticleSystem component7 = gameObject.GetComponent<ParticleSystem>();
				component7.Play();
				GameManager.FallToDeath();
			}
			isEat = true;
			break;
		case (int)TileVal.BeanLv28Net:
		case (int)TileVal.BeanSimulationNet:

			if (GameManager.theBall.mJumpState == Ball.JumpState.BackJump)
			{
				isEat = true;
				return true;
			}
			if (GameManager.theBall.mJumpState == Ball.JumpState.SuperJump)
			{
				GameManager.theBall.mBackTileNumber = 0f;
				GameManager.theBall.state = Ball.State.BackJump;
				GameManager.theBall.mJumpState = Ball.JumpState.BackJump;
			}
			else if (GameManager.theBall.mJumpState != Ball.JumpState.ShootBasketball)
			{
				GameManager.KillBall(DangerType.TallTree);
			}
			isEat = true;
			break;
		case (int)TileVal.EnemySimulationDie:

			GameManager.FallToDeath();
			isEat = true;
			break;
		case (int)TileVal.BeanLv28Button:

			if (LevelDesigner.GeneratingDonorThemeNumber == 50 || LevelDesigner.GeneratingDonorThemeNumber == 51)
			{
				QueryBackTile((int)TileVal.EnemyLv32Door, "anim2");
			}
			else
			{
				QueryBackTile((int)TileVal.EnemyLv28Wall2, "anim1");
			}
			isEat = true;
			break;
		case (int)TileVal.BeanBack6Tile:

			if (Ball.Position.z >= -0.6f)
			{
				GameManager.theBall.inSuperJump = true;
				GameManager.theBall.EndJump();
				GameManager.theBall.mBackTileNumber = 0f;
				GameManager.theBall.state = Ball.State.BackJump;
				GameManager.theBall.mJumpState = Ball.JumpState.BackJump;
				Animation component4 = AdditionTransform.Find("state01/level_30_fantanban").GetComponent<Animation>();
				component4.Play("anim1");
				if (UserSettings.IsSoundOn() && !GameManager.DisableSounds)
				{
					GameManager.theBall.DecelerationSound.Play();
				}
				isEat = true;
			}
			break;
		case (int)TileVal.BeanJump10Tile:

			if (GameManager.theBall.state == Ball.State.Jumping)
			{
				return true;
			}
			if (GameManager.theBall.mJumpState == Ball.JumpState.SuperJump)
			{
				break;
			}
			GameManager.theBall.mIsBackJumping = false;
			IsJump = true;
			IsSuperJump = true;
			if (AdditionTransform != null)
			{
				ParticleSystem component6 = AdditionTransform.Find("state01/chufa").GetComponent<ParticleSystem>();
				if (component6 != null)
				{
					component6.Play();
				}
			}
			GameManager.theBall.Jump(thisTile, false, false);
			isEat = true;
			break;
		case (int)TileVal.BeanLv32StartReverse:
		case (int)TileVal.BeanLv32EndReverse:

		{
			if (GameManager.theBall.mRevserBallNumber != GameManager.BallRowNum)
			{
				Ball.Classic1782ReverseInput = !Ball.Classic1782ReverseInput;
				GameManager.theBall.ShowXunYunEffects(Ball.Classic1782ReverseInput);
			}
			GameManager.theBall.mRevserBallNumber = GameManager.BallRowNum;
			ParticleSystem component5 = AdditionTransform.Find("state01/tx_chenkpoint_after/lizi").GetComponent<ParticleSystem>();
			component5.transform.localPosition = new Vector3(Ball.Position.x - 2.5f, 0f, 0f);
			component5.Play();
			isEat = true;
			break;
		}
		case (int)TileVal.BeanLv34Jump1Tile:

			if (GameManager.theBall.state == Ball.State.Jumping)
			{
				return true;
			}
			isEat = true;
			break;
		case (int)TileVal.BeanLv40ShowButton:

		{
			ParticleSystem component = AdditionTransform.Find("state01/tx_button").GetComponent<ParticleSystem>();
			component.Play();
			Animation component2 = AdditionTransform.Find("state01/level40_anniu").GetComponent<Animation>();
			component2.Play("anim1");
			Ball.IsLv4OHiddenMode = true;
			isEat = true;
			break;
		}
		case (int)TileVal.BeanLv40HideButton:

			Ball.IsLv4OHiddenMode = false;
			isEat = true;
			break;
				default: return false;
		}
		if (AudioPlayer != null) PlaySound();
		return true;
	}
	public void QueryEnd()
	{
		isQuery = true;
		switch (TypeVal)
		{
		case (int)TileVal.BackwardQueryLv26shunyiStart:
			GameManager.theBall.state = Ball.State.TeleportingStart;
			GameManager.theBall.mChangeBallYPostion = base.transform.localPosition.y + 5f;
			if (UserSettings.IsSoundOn())
			{
				GameManager.theBall.TeleportingSound.Play();
			}
			break;
		case (int)TileVal.BackwardQueryLv26shunyiEnd:
			if (GameManager.theBall.state != Ball.State.Teleportinging)
			{
				GameManager.KillBall(DangerType.BackwardQueryLv26shunyiEnd);
			}
			else
			{
				GameManager.theBall.state = Ball.State.TeleportingEnd;
			}
			if (UserSettings.IsSoundOn())
			{
				GameManager.theBall.TeleportingSound.Play();
			}
			break;
		}
		if (AudioPlayer != null)
		{
			PlaySound();
		}
	}
	public void QueryBackTile(int tileID, string animName)
	{
		bool flag = false;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				MathUtils.IntTrio tileIndex = new MathUtils.IntTrio(Index.x, i + RowIndex, j);
				if (GameManager.TileBoard[tileIndex] != null && GameManager.TileBoard[tileIndex].TypeVal == tileID && GameManager.TileBoard[tileIndex].newAnimTrasFromAnimation != null)
				{
					GameManager.TileBoard[tileIndex].newAnimTrasFromAnimation.Play(animName);
					GameManager.TileBoard[tileIndex].IsShootBall = true;
					flag = true;
				}
			}
			if (flag)
			{
				break;
			}
		}
	}
}
