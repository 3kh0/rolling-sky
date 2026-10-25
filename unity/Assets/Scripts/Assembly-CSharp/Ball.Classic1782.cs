using System.Collections.Generic;
using UnityEngine;

public partial class Ball
{
	public enum JumpState { Normal, SmartJump, TripleJump, SuperJump, SuperJumperFictitious, ShootBasketball, BackJump, Jump10Tile }
	public JumpState mJumpState;
	public Transform ShootBallJumpPowerupGroup;
	public Transform XunYunGroup;
	public AudioSource TeleportingSound;
	public AudioSource SuperJumpSound;
	public Vector3 m_tempBallPos;
	public float mBackTileNumber;
	public float mChangeBallXPostion;
	public float mChangeBallYPostion;
	public float mTempTime = 0.1f;
	public bool mIsBackJumping;
	public int mRevserBallNumber = -1;
	public static bool Classic1782ReverseInput;
	public static bool IsLv4OHiddenMode;
	public static readonly List<Vector3> Classic1782OutsideWalls = new List<Vector3>();
	private int classic1782LastRevealRow = -1;

	private void Reset1782Power()
	{
		mJumpState = JumpState.Normal;
		mBackTileNumber = mChangeBallXPostion = mChangeBallYPostion = 0f;
		mTempTime = 0.1f;
		m_tempBallPos = Vector3.zero;
		mIsBackJumping = Classic1782ReverseInput = IsLv4OHiddenMode = false;
		mRevserBallNumber = classic1782LastRevealRow = -1;
		if (ShootBallJumpPowerupGroup != null) ShootBallJumpPowerupGroup.gameObject.SetActive(false);
		if (XunYunGroup != null) XunYunGroup.gameObject.SetActive(false);
	}

	public void ShowXunYunEffects(bool show) { XunYunGroup.gameObject.SetActive(show); }

	private float Start1782Jump(Tile tile)
	{
		if (tile != null)
		{
			switch ((TileVal)tile.TypeVal)
			{
			case TileVal.EnemySuperJumperFictitious: mJumpState = JumpState.SuperJumperFictitious; break;
			case TileVal.EnemySuperJumper: mJumpState = JumpState.SuperJump; break;
			case TileVal.EnemyLv37Pan: mJumpState = JumpState.SuperJump; Play1782SuperJumpSound(); break;
			case TileVal.BeanLv28BouncePlate: mJumpState = JumpState.ShootBasketball; Play1782SuperJumpSound(); break;
			case TileVal.BeanJump10Tile: mJumpState = JumpState.Jump10Tile; break;
			case TileVal.BeanLv34Jump1Tile: mJumpState = JumpState.SmartJump; break;
			default: mJumpState = tile.IsSuccessiveJump || inSuccessiveJump ? JumpState.TripleJump : JumpState.Normal; break;
			}
		}
		if ((tile != null && tile.IsJump || inSuccessiveJump) && !inSuccessiveJump) tile.Jump();
		switch (mJumpState)
		{
		case JumpState.SmartJump: SuperJumpPowerupGroup.gameObject.SetActive(false); return 2f;
		case JumpState.SuperJump:
		case JumpState.SuperJumperFictitious:
		case JumpState.Jump10Tile:
			ShootBallJumpPowerupGroup.gameObject.SetActive(false);
			ballPowerupSuperJump.Show();
			SuperJumpPowerupGroup.gameObject.SetActive(true);
			inSuperJump = true;
			ballWingAngle.ForceSetTarget(0f);
			return mJumpState == JumpState.Jump10Tile ? 10f : mJumpState == JumpState.SuperJumperFictitious ? 6f : 8f;
		case JumpState.ShootBasketball:
			inSuperJump = true;
			SuperJumpPowerupGroup.gameObject.SetActive(false);
			ShootBallJumpPowerupGroup.gameObject.SetActive(true);
			return 8f;
		case JumpState.TripleJump: return StartSuccessiveJump();
		default:
			ShootBallJumpPowerupGroup.gameObject.SetActive(false);
			SuperJumpPowerupGroup.gameObject.SetActive(false);
			return 4f;
		}
	}

	private void Play1782SuperJumpSound()
	{
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds) SuperJumpSound.Play();
	}

	private float Get1782JumpParabola()
	{
		float height = mJumpState == JumpState.ShootBasketball ? 2.5f : mJumpState == JumpState.SmartJump ? 1.5f : 2f;
		float distance = jumpEndPos - jumpStartPos;
		float progress = Position.y - jumpStartPos;
		return height - 4f * height / Mathf.Pow(distance, 2f) * Mathf.Pow(progress - distance / 2f, 2f);
	}

	private Vector3 Apply1782Motion(Vector3 velocity, Vector3 position)
	{
		if (state == State.ShootBall && mChangeBallXPostion != 0f)
		{
			position.x = mChangeBallXPostion;
			position.y = Position.y;
			position.z = Position.z + velocity.z;
			if (m_tempBallPos == Vector3.zero) m_tempBallPos = new Vector3(position.x, position.y + 0.5f, -0.5f);
			position = Vector3.MoveTowards(position, m_tempBallPos, Time.deltaTime * 4f);
			if (position.z >= -0.5f)
			{
				m_tempBallPos = Vector3.zero;
				mChangeBallXPostion = 0f;
				inSuperJump = true;
				EndJump();
				state = State.Rolling;
			}
		}
		else if (state == State.BackJump)
		{
			mIsBackJumping = true;
			position.x = Position.x + velocity.x;
			position.y = Position.y - velocity.y;
			position.z = Position.z + velocity.z;
			mBackTileNumber += velocity.y;
			m_tempBallPos = new Vector3(position.x, position.y, -0.5f);
			m_tempBallPos = Vector3.MoveTowards(position, m_tempBallPos, Time.deltaTime * 4f);
			position.z = m_tempBallPos.z;
			int theme = LevelDesigner.CurrentDonorThemeNumber;
			float distance = theme == 47 || theme == 48 || theme == 49 || theme == 52 || theme == 53 ? 8f : 2.8f;
			if (mBackTileNumber >= distance)
			{
				mIsBackJumping = false;
				mBackTileNumber = 0f;
				m_tempBallPos = Vector3.zero;
				state = State.Rolling;
			}
		}
		else if (mIsBackJumping && state == State.Falling) position.y = Position.y;
		if (state == State.TeleportingStart)
		{
			mTempTime -= Time.deltaTime;
			GeoGroup.localScale = Vector3.MoveTowards(GeoGroup.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * 16f);
			if (mTempTime <= 0f)
			{
				mTempTime = 0.1f;
				position -= new Vector3(0f, 0f, 10000f);
				state = State.Teleportinging;
			}
		}
		if (state == State.Teleportinging)
		{
			if (GeoGroup.localScale != Vector3.one) GeoGroup.localScale = Vector3.one;
			if (GameManager.BallRowNum > mChangeBallYPostion)
			{
				state = State.Rolling;
				GameManager.KillBall(Tile.DangerType.BackwardQueryLv26shunyiStart);
			}
		}
		if (state == State.TeleportingEnd)
		{
			mTempTime -= Time.deltaTime;
			GeoGroup.localScale = Vector3.MoveTowards(GeoGroup.localScale, Vector3.one, Time.deltaTime * 16f);
			if (mTempTime <= 0f)
			{
				mTempTime = 0.1f;
				currentTargetXPos = position.x;
				GeoGroup.localScale = Vector3.one;
				position += new Vector3(0f, 0f, 10000f);
				state = State.Rolling;
			}
		}
		return position;
	}

	// Called after the ball's position has advanced, before collision queries.
	private void Update1782Floor()
	{
		if (!ExpansionGameplay.Classic1782 || GameManager.BallRowNum == classic1782LastRevealRow) return;
		classic1782LastRevealRow = GameManager.BallRowNum;
		if (!IsLv4OHiddenMode) return;
		for (int col = 0; col < 5; col++)
		{
			Tile tile = GameManager.TileBoard[GameManager.BallRowNum + 6, col];
			if (tile != null && tile.IsHidden) tile.HideRoot(false);
		}
	}

	private bool Check1782OutsideWall()
	{
		bool hit = false;
		foreach (Vector3 wall in Classic1782OutsideWalls)
		{
			if (Position.y < wall.y + 0.5f || Position.y > wall.y + 5.5f) continue;
			if ((wall.x == 0.5f && Position.x <= 0f) || (wall.x == 4.5f && Position.x >= 5f))
			{
				GameManager.KillBall(Tile.DangerType.TallTree);
				hit = true;
			}
		}
		return hit;
	}
}
