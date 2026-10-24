using UnityEngine;

public partial class Ball
{
	public Transform SuccessiveJumpGroup;
	public AudioSource SuccessiveJumperSound;
	public AudioSource AccelerateSound;
	public AudioSource DecelerationSound;
	public float mBallSpeedcoefficient = 1f;
	private int coefficientNum = -1;
	private FrameAnim.Frame ballSuccessiveJump;
	private bool successiveJumpBound;
	private bool inSuccessiveJump;
	private int successiveJumpIndex;
	private static readonly int[] successiveJumpDistance = { 4, 5, 4, 5 };
	private bool isJumperAnimtorStart;
	private int jumperState;

	private void ResetLaterPower()
	{
		coefficientNum = -1;
		mBallSpeedcoefficient = 1f;
		inSuccessiveJump = isJumperAnimtorStart = false;
		successiveJumpIndex = jumperState = 0;
		if (SuccessiveJumpGroup != null)
		{
			ballSuccessiveJump = new FrameAnim.Frame(SuccessiveJumpGroup);
			successiveJumpBound = true;
			ballSuccessiveJump.Hide();
		}
	}
	private Vector3 ApplyLaterSpeed(Vector3 velocity)
	{
		if (mBallSpeedcoefficient != 1f)
		{
			velocity *= mBallSpeedcoefficient;
			if (coefficientNum == -1) coefficientNum = TilePosition.y + (mBallSpeedcoefficient > 1f ? 40 : 20);
			if (TilePosition.y >= coefficientNum) { coefficientNum = -1; mBallSpeedcoefficient = 1f; }
		}
		if (isJumperAnimtorStart) UpdateSuccessiveSquash();
		return velocity;
	}
	private Vector3 ApplyLaterBridge(Vector3 position)
	{
		if (state == State.BrideUp)
		{
			Vector3 target = position; target.z = -1.5f;
			position = Vector3.MoveTowards(position, target, Time.deltaTime * 4f);
			if (position.z == -1.5f) state = State.BrideRolling;
		}
		if (state == State.BrideRolling) position.z = -1.5f;
		if (state == State.BrideDown)
		{
			Vector3 target = position; target.z = -0.5f;
			position = Vector3.MoveTowards(position, target, Time.deltaTime * 4f);
			// Native changes to Rolling immediately, not on arrival.
			state = State.Rolling;
		}
		return position;
	}
	private float StartSuccessiveJump()
	{
		if (!successiveJumpBound)
		{
			if (SuccessiveJumpGroup == null) throw new System.InvalidOperationException("Missing native Ball.SuccessiveJumpGroup binding");
			ballSuccessiveJump = new FrameAnim.Frame(SuccessiveJumpGroup);
			successiveJumpBound = true;
		}
		if (!ballSuccessiveJump.IsShown) { ballSuccessiveJump.Show(); ballWingAngle.ForceSetTarget(0f); }
		inSuccessiveJump = isJumperAnimtorStart = true;
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds) SuccessiveJumperSound.Play();
		return successiveJumpDistance[successiveJumpIndex];
	}
	private void EndLaterJump()
	{
		if (inSuccessiveJump && ++successiveJumpIndex > successiveJumpDistance.Length - 1)
		{
			successiveJumpIndex = 0;
			inSuccessiveJump = false;
			if (ballSuccessiveJump.IsShown) ballSuccessiveJump.Hide();
		}
		if (currentTile != null && currentTile.IsJump) Jump(currentTile, false, false);
		else if (inSuccessiveJump)
		{
			if (successiveJumpIndex <= successiveJumpDistance.Length && currentTile != null) Jump(currentTile, false, false);
		}
		else state = State.Landing;
	}
	private void UpdateSuccessiveSquash()
	{
		Vector3 down = new Vector3(1.2f, 1f, 0.5f), up = new Vector3(0.8f, 1f, 1.2f);
		if (jumperState == 0) { GeoGroup.localScale = Vector3.one; jumperState = 1; }
		else if (jumperState == 1)
		{
			GeoGroup.localScale = Vector3.MoveTowards(GeoGroup.localScale, down, Time.deltaTime * 16f);
			if (GeoGroup.localScale == down) jumperState = 2;
		}
		else if (jumperState == 2)
		{
			GeoGroup.localScale = Vector3.MoveTowards(GeoGroup.localScale, up, Time.deltaTime * 4f);
			if (GeoGroup.localScale == up) jumperState = 3;
		}
		else
		{
			GeoGroup.localScale = Vector3.MoveTowards(GeoGroup.localScale, Vector3.one, Time.deltaTime * 4f);
			if (GeoGroup.localScale == Vector3.one) { jumperState = 0; isJumperAnimtorStart = false; }
		}
	}
}
