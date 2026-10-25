using UnityEngine;

public partial class Ball : MonoBehaviour
{
	public enum State
	{
		Waiting = 0,
		Rolling = 1,
		Jumping = 2,
		Landing = 3,
		Stopped = 4,
		Falling = 5,
		Flying = 6,
		Crushed = 7,
		Sliding = 8,
		Exploding = 9,
		Exploded = 10,
		Ending = 11,
		EndingJumping = 12,
		Ended = 13,
		BrideUp,
		BrideRolling,
		BrideDown,
		TeleportingStart,
		Teleportinging,
		TeleportingEnd,
		BackJump,
		ShootBall
	}

	public enum VelocityState
	{
		Normal = 0,
		SlowingDown = 1,
		Slow = 2,
		SpeedingUp = 3
	}

	public enum LandState
	{
		PreLand = 0,
		TouchDown = 1,
		Landing = 2,
		Landed = 3
	}

	public enum HorizMoveDir
	{
		MoveLeft = 0,
		MoveRight = 1,
		Straight = 2
	}

	private enum PortalAnim
	{
		Waiting = 0,
		Appearing = 1,
		EnteringWorld = 2,
		ExitingWorld = 3
	}

	private struct Fragment
	{
		private Transform transform;

		private Vector3 moveVector;

		public Fragment(Renderer fragmentRenderer)
		{
			transform = fragmentRenderer.transform;
			transform.localPosition = Vector3.zero;
			moveVector = new Vector3(0f, 0f, 1f) * 12.5f;
			transform.GetComponent<Renderer>().sharedMaterial = MaterialManager.Materials[MaterialName.Ball];
		}

		public void UpdateExploding()
		{
			Vector3 localPosition = transform.localPosition;
			localPosition += moveVector * Time.smoothDeltaTime;
			transform.localPosition = localPosition;
		}

		public void SetPos(float newPosZ)
		{
			TransformUtils.SetZ(transform, newPosZ, true);
		}

		public void Energize()
		{
			transform.GetComponent<Renderer>().sharedMaterial = MaterialManager.Materials[MaterialName.BallInner];
		}

		public void Reset()
		{
			SetPos(0f);
			transform.GetComponent<Renderer>().sharedMaterial = MaterialManager.Materials[MaterialName.Ball];
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugFalling = true;

	private const bool debugFallPrevention = false;

	public const float GroundHeight = -0.5f;

	private const string heatName = "_Ammount";

	private const float heatTime = 5f;

	private const float slowPercent = 0.25f;

	private const float slowDownTime = 0.25f;

	private const float slowTime = 1f;

	private const float speedUpTime = 5f;

	private const float hyperSpeed = 50f;

	private const float offsetResetSpeed = 5f;

	private const float offsetResetMargin = 0.001f;

	private const float SlideSpeed = 4f;

	private const float SlideMargin = 0.2f;

	private const float GravityForce = 9.8f;

	private const float ShakeOffsetRatio = 0.85f;

	private const float AnimMargin = 0.01f;

	private const float RollSpeed = 0.01f;

	private const float TurnAmmount = 45f;

	private const float ImpactGravityForce = 125f;

	private const float ImpactMaxForceX = 10f;

	private const float ImpactMaxForceY = 15f;

	private const float ImpactMaxForceZ = 20f;

	private const int ballSpeedRanges = 1;

	private const float ballSpeedMin = 1f;

	private const float ballSpeedMax = 2.85f;

	private const float ballSpeedPost = 1.5f;

	private const float ballSpeedChangeRate = 12f;

	private const float ballSpeedChangeRateFast = 12f;

	private const float ballSpeedResetRate = 2f;

	private const float ballSpeedDistance = 50f;

	private const float ballSpeedResetWarnDistance = 30f;

	private const float ballSlowSpeedFall = 4f;

	private const float ballSlowSpeedFinish = 8f;

	private const int ballJumpLengthStandard = 4;

	private const int ballJumpRanges = 4;

	private const float ballJumpHeightMin = 0.00125f;

	private const int ballBounceRanges = 1;

	private const int ballLongJumpIndex = 4;

	private const int ballShortJumpIndex = 2;

	private const float ballShadowHeightMax = 2.5f;

	private const float ballShadowSizeIncMax = 0.5f;

	private const float ballShadowShiftMax = 2.25f;

	private const float ballShadowAdvPos = 0.5f;

	private const float ballShadowAdvScale = 0.5f;

	private const float ballCrushedHeight = 0.1f;

	private const float ballCrushedGroundPos = 0.625f;

	private const float ballCrushedHorizGroundPos = 0.13f;

	private const float ballCrushedRiserWidth = 0.9125f;

	private const float ballCrushedShadowOffset = -0.125f;

	private const float ballCrushedShadowSize = 1.1f;

	private const float ballCrushedShadowAdvSize = 1.325f;

	private const float ballCrushedHorizScaleX = 0.9f;

	private const float ballCrushedHorizScaleY = 0.5f;

	private const float ballCrushedSlideTime = 1f;

	private const float ExplodeSpeed = 12.5f;

	private const float ExplodeGravityForce = 25f;

	private const float ExplodeTime = 5f;

	private const float ExplodeCrushedSquish = 0.1f;

	private const float ExplodeCrushedPush = 1f;

	private const float portalTransitionTime = 0.75f;

	private const float portallingAmmount = 0.15f;

	private const float portallingRise = 0.9f;

	private const int portallingAnimSmoothAmmount = 6;

	private const float porallingReSmoothAmmount = 0.025f;

	private const float portallingSpinSpeed = 0.5f;

	private const float portallingSpinSpeedUpOnHold = 5f;

	private const int portallingSpinAccelerationOnHold = 3;

	private const int longJumpIndex = 4;

	private const int shortJumpIndex = 2;

	private const int bouncesTotal = 1;

	private const float speedChangeRate = 12f;

	private const float speedChangeRateFast = 12f;

	private const float speedResetRate = 2f;

	private const float spedDistanceMax = 50f;

	private const float warnDistanceMax = 30f;

	private const float ballQuadJumpOffset = 26f;

	private static int[] fallPreventionOrder = new int[3] { 0, -1, 1 };

	private static Vector3 ballQuadAim = new Vector3(50f, -180f, 0f);

	public State state;

	public VelocityState velocityState;

	public LandState landState = LandState.Landed;

	public HorizMoveDir currentHorizMove;

	private PortalAnim portalAnim;

	private bool isEnding;

	private bool isPortalling;

	public Transform GeoQuad;

	public Transform GeoLowPoly;

	public Transform GeoHighPoly;

	public Transform ShadowGroup;

	public Transform InnerGroup;

	public Transform QuadGroup;

	public Transform GeoGroup;

	public Transform FragmentsGroup;

	public Transform DisplayGroup;

	public Transform OffsetGroup;

	public Transform PowerupsGroup;

	public Transform SuperJumpPowerupGroup;

	public AudioSource ExplodeSound;

	public AudioSource PickupSound;

	private FrameAnim.Sequence ballGeo;

	private FrameAnim.Frame ballShadow;

	private FrameAnim.Frame ballFragments;

	private FrameAnim.Frame ballInner;

	private FrameAnim.Frame ballPowerups;

	private FrameAnim.Frame ballPowerupSuperJump;

	private static Vector3 ballMoveVector;

	private Tile currentTile;

	private float velocityTimer;

	private Elastic.Single ballSpeed = new Elastic.Single(12f, 0f, 0f, 0.01f);

	private float jumpStartPos;

	private float jumpMidPos;

	private float jumpEndPos;

	public bool inSuperJump;

	private Elastic.Single ballWingAngle = new Elastic.Single(12f, 0f, 0f, 0.01f);

	private float shakeOffset;

	private float dipOffset;

	private float shadowHeight;

	private float shadowHeightLast;

	private Vector3 shadowPos;

	private Vector3 shadowScale;

	private Vector3 shadowScaleAdv;

	public TrailRenderer BallTrail;

	private bool heated;

	private float heatTimer;

	private float heatPercent;

	private Fragment[] fragmentParts;

	private float explodeTimer;

	private float portalTimer;

	private float portalTime;

	private float portalSpin;

	private float worldEndDistance;

	private float worldDistanceLeft;

	private MathUtils.Range worldEndX;

	private Vector3 fakeBallPos;

	private float fakeBallSpeed;

	private bool animateWithFakeBallPos;

	private bool ballIsOffset;

	private MathUtils.Range ballSlideRange;

	private float ballSlideTimer;

	private float startSpeedPosY;

	private float startWarnPosY;

	private int ballGeoNumber;

	private bool usingGeo;

	private bool usingGeoHigh;

	private bool usingFragments;

	private bool isInitialized;

	private float currentTargetXPos;

	private Vector2 currentPosX;

	private Vector2 destinationPosX;

	public bool IsRolling
	{
		get
		{
			return state == State.Rolling;
		}
	}

	public bool IsJumping
	{
		get
		{
			return state == State.Jumping;
		}
	}

	public bool IsNotJumping
	{
		get
		{
			return !IsJumping;
		}
	}

	public bool IsLanding
	{
		get
		{
			return state == State.Landing;
		}
	}

	public bool IsNotLanding
	{
		get
		{
			return !IsLanding;
		}
	}

	public bool IsFalling
	{
		get
		{
			return state == State.Falling;
		}
	}

	public bool IsNotRotating { get; private set; }

	public bool IsDead { get; private set; }

	public bool IsDestroyed { get; private set; }

	public bool IsImmortal { get; private set; }

	public bool IsVelocityMaxed { get; private set; }

	public bool IsRotating
	{
		get
		{
			return !IsNotRotating;
		}
	}

	public bool IsAlive
	{
		get
		{
			return !IsDead;
		}
	}

	public bool IsNotDestroyed
	{
		get
		{
			return !IsDestroyed;
		}
	}

	public bool IsNotImmortal
	{
		get
		{
			return !IsImmortal;
		}
	}

	public bool IsVelocityNotMaxed
	{
		get
		{
			return !IsVelocityMaxed;
		}
	}

	public bool isMoving
	{
		get
		{
			return state == State.Rolling || state == State.Jumping || state == State.Landing;
		}
	}

	public static bool IsTrackable
	{
		get
		{
			return !IsNotTrackable;
		}
	}

	public static bool IsNotTrackable { get; private set; }

	public static Vector3 Position { get; private set; }

	public static MathUtils.IntPair TilePosition { get; private set; }

	public static int RowNum
	{
		get
		{
			return TilePosition.y;
		}
	}

	public static int TileNum
	{
		get
		{
			return TilePosition.x;
		}
	}

	public static float RowPercent { get; private set; }

	public static float Distance { get; private set; }

	public static float Velocity { get; private set; }

	public static Vector3 MoveVector
	{
		get
		{
			return ballMoveVector;
		}
	}

	public static float startingHeight
	{
		get
		{
			return -0.5f;
		}
	}

	public Vector3 AnimPos
	{
		get
		{
			if (animateWithFakeBallPos)
			{
				return fakeBallPos;
			}
			return Position;
		}
	}

	public static void ResetStoredPosition()
	{
		Position = new Vector3(0f, 0f, 0f);
		TilePosition = new MathUtils.IntPair(0, 0);
		RowPercent = 0f;
		Distance = 0f;
		Velocity = 0f;
	}

	public void Initialize(float startingPosX, float startingPosY)
	{
		ResetExpansionPower();
		if (isInitialized)
		{
			if (usingFragments)
			{
				ResetBallFragments();
			}
		}
		else
		{
			usingGeo = DeviceQualityChecker.QualityIsNotPour();
			usingGeoHigh = DeviceQualityChecker.QualityIsHigh();
			usingFragments = DeviceQualityChecker.QualityIsHigh();
			if (usingGeo)
			{
				if (usingGeoHigh)
				{
					ballGeoNumber = 2;
				}
				else
				{
					ballGeoNumber = 1;
				}
			}
			else
			{
				ballGeoNumber = 0;
			}
			if (usingFragments)
			{
				Renderer[] childrenAs = TransformUtils.GetChildrenAs<Renderer>(FragmentsGroup);
				fragmentParts = new Fragment[childrenAs.Length];
				for (int i = 0; i < childrenAs.Length; i++)
				{
					fragmentParts[i] = new Fragment(childrenAs[i]);
				}
			}
			ballGeo = new FrameAnim.Sequence(QuadGroup, GeoLowPoly, GeoHighPoly);
			ballFragments = new FrameAnim.Frame(FragmentsGroup);
			ballShadow = new FrameAnim.Frame(ShadowGroup);
			ballInner = new FrameAnim.Frame(InnerGroup);
			ballPowerups = new FrameAnim.Frame(PowerupsGroup);
			ballPowerupSuperJump = new FrameAnim.Frame(SuperJumpPowerupGroup);
		}
		IsDead = false;
		animateWithFakeBallPos = false;
		ballIsOffset = false;
		heated = false;
		IsNotRotating = false;
		IsDestroyed = false;
		IsImmortal = false;
		IsVelocityMaxed = false;
		IsNotTrackable = false;
		isEnding = false;
		isPortalling = false;
		inSuperJump = false;
		Velocity = LevelDesigner.VelocityMin;
		velocityState = VelocityState.Normal;
		ballSpeed.ForceSetTarget(1f);
		UpdateBallSpeed();
		ballMoveVector = new Vector3(0f, Velocity, 0f);
		currentTargetXPos = 2.5f;
		TilePosition = default(MathUtils.IntPair);
		SetPositionTo(new Vector3(startingPosX, startingPosY, -0.5f));
		BallTrail.enabled = false;
		OffsetGroup.localPosition = Vector3.zero;
		shakeOffset = 0f;
		shadowHeight = (shadowHeightLast = ShadowGroup.position.z);
		shadowPos = new Vector3(0f, -0.25f, 0f);
		dipOffset = 0f;
		ballGeo.Show(ballGeoNumber);
		ballFragments.Hide();
		ballInner.Hide();
		ballPowerups.Show();
		ballPowerupSuperJump.Hide();
		ballShadow.Show();
		ShadowBall();
		heated = false;
		heatTimer = 0f;
		heatPercent = 0f;
		DisplayHeat();
		isInitialized = true;
		state = State.Waiting;
	}

	public void StartRolling()
	{
		if (state == State.Waiting)
		{
			state = State.Rolling;
			ResetBallOffset(true);
			ballGeo.Show(ballGeoNumber);
		}
	}

	public void SetVelocity(float velocity)
	{
		Velocity = velocity;
	}

	public void SlowMo()
	{
		switch (velocityState)
		{
		case VelocityState.Normal:
			velocityState = VelocityState.SlowingDown;
			velocityTimer = 0.25f;
			break;
		case VelocityState.SlowingDown:
			break;
		case VelocityState.Slow:
			velocityTimer = 1f;
			break;
		case VelocityState.SpeedingUp:
		{
			velocityState = VelocityState.SlowingDown;
			float percent = MathUtils.ToPercent(velocityTimer, 0f, 5f);
			velocityTimer = MathUtils.FromPercent(percent, 0f, 0.25f);
			break;
		}
		default:
			Debug.LogError(string.Format("BALL: ERROR: Encountered unhandled velocityState of {0} in Ball.SlowMo()'s case statement.  Check logic", velocityState));
			break;
		}
	}

	public void UpdateBallSpeed()
	{
		ballSpeed.Update();
		ballMoveVector = TransformUtils.GetSetY(ballMoveVector, ballSpeed.SmoothValue);
	}

	public void SetPromptPos(float promptAnimPercent)
	{
		OffsetBall(promptAnimPercent);
	}

	public void ResetPromptPos()
	{
		ResetBallOffset();
	}

	public void Hide()
	{
		ballGeo.Hide();
		ballFragments.Hide();
		ballInner.Hide();
	}

	public void StartPortalAnimation(bool portallingOut)
	{
		isPortalling = true;
		portalAnim = PortalAnim.Appearing;
	}

	public void EndPortallingAnimation()
	{
		isPortalling = false;
		if (usingFragments)
		{
			ResetBallFragments();
		}
		ballGeo.Show(ballGeoNumber);
		ballFragments.Hide();
		ballInner.Hide();
		DisplayGroup.localPosition = Vector3.zero;
		DisplayGroup.localEulerAngles = Vector3.zero;
	}

	public void UpdateBall()
	{
		UpdateVelocity();
		Vector3 zero = Vector3.zero;
		if (currentHorizMove == HorizMoveDir.MoveLeft)
		{
			CalculateDestinationDistanceFactor();
			if (GameManager.BallPosition.x < currentTargetXPos + 0.2f)
			{
				StopMovingLeftRight();
			}
			else
			{
				SetMovingLeft();
			}
		}
		else if (currentHorizMove == HorizMoveDir.MoveRight)
		{
			CalculateDestinationDistanceFactor();
			if (GameManager.BallPosition.x > currentTargetXPos - 0.2f)
			{
				StopMovingLeftRight();
			}
			else
			{
				SetMovingRight();
			}
		}
		if (IsDead)
		{
			UpdateDeath();
		}
		if (isPortalling)
		{
			UpdatePortalling();
		}
		TransformUtils.SetZ(OffsetGroup, shakeOffset + dipOffset, true);
		zero = Position;
		Vector3 vector = ballMoveVector * (ExpansionGameplay.Later ? ClassicGameplayClock.DeltaTime : Time.smoothDeltaTime);
		if (ExpansionGameplay.Later) vector = ApplyLaterSpeed(vector);
		if (state == State.Waiting)
		{
			vector.y = 0f;
			vector.z = 0f;
			zero = Position + vector;
			base.transform.position = zero;
		}
		else
		{
			zero = Position + vector;
			if (ExpansionGameplay.Classic1782) zero = Apply1782Motion(vector, zero);
			if (state == State.Ending)
			{
				zero = UpdateEnding(vector, zero);
			}
			if (state != State.Stopped)
			{
				if (state == State.Jumping || state == State.EndingJumping)
				{
					zero.z = CalculateJumpHeight(vector) + -0.5f;
				}
				if (ExpansionGameplay.Later) zero = ApplyLaterBridge(zero);
				SetPositionTo(zero);
				if (IsRotating)
				{
					ShadowBall();
				}
				if (heated)
				{
					UpdateHeat();
				}
			}
		}
		if (ExpansionGameplay.Enabled) UpdateExpansionPower();
		Vector3 currentBallPosition;
		if (animateWithFakeBallPos)
		{
			fakeBallPos.y += fakeBallSpeed * Time.smoothDeltaTime;
			currentBallPosition = fakeBallPos;
		}
		else
		{
			currentBallPosition = zero;
		}
		AnimateWorld(currentBallPosition);
	}

	private void SetPositionTo(Vector3 newPosition)
	{
		Position = newPosition;
		base.transform.position = newPosition;
		int num = MathUtils.Floored(Position.y);
		int x = MathUtils.Floored(Position.x);
		if (GameManager.TileBoard.RowIsWithinWorld(num))
		{
			Distance = Position.y;
		}
		else
		{
			num = GameManager.TileBoard.FinalRow;
			Distance = num;
		}
		TilePosition = new MathUtils.IntPair(x, num);
		RowPercent = Position.y % 1f;
	}

	private void UpdateDeath()
	{
		switch (state)
		{
		case State.Falling:
		case State.Exploding:
			if (ballMoveVector.y > 0f)
			{
				ballMoveVector.y -= 4f * Time.smoothDeltaTime;
			}
			else if (ballMoveVector.y < 0f)
			{
				ballMoveVector.y = 0f;
			}
			if (state != State.Exploding)
			{
				break;
			}
			explodeTimer -= Time.smoothDeltaTime;
			if (explodeTimer <= 0f)
			{
				explodeTimer = 0f;
				state = State.Exploded;
			}
			if (usingFragments)
			{
				for (int i = 0; i < fragmentParts.Length; i++)
				{
					fragmentParts[i].UpdateExploding();
				}
			}
			ballMoveVector.z += 25f * Time.smoothDeltaTime;
			break;
		case State.Flying:
			ballMoveVector.z += 125f * Time.smoothDeltaTime;
			break;
		case State.Crushed:
		case State.Sliding:
			break;
		}
	}

	private float CalculateJumpHeight(Vector3 currentVelocity)
	{
		bool wasBelow;
		bool wasAbove;
		float num = MathUtils.ToPercent01(Position.y, jumpStartPos, jumpEndPos, out wasBelow, out wasAbove);
		float num2;
		if (wasAbove)
		{
			num2 = 0f;
			EndJump();
		}
		else
		{
			num2 = GetJumpParabola();
			if (inSuperJump)
			{
				ballPowerupSuperJump.GetRoot().Rotate(Vector3.up * 7.5f);
				if (ExpansionGameplay.Classic1782) ShootBallJumpPowerupGroup.Rotate(Vector3.up * 7.5f);
			}
			if (ExpansionGameplay.Later && inSuccessiveJump) ballSuccessiveJump.GetRoot().Rotate(Vector3.up * 7.5f);
		}
		return 0f - num2;
	}

	private float GetJumpParabola()
	{
		if (ExpansionGameplay.Classic1782 && circusClownJump) return Get1792ClownParabola();
		if (ExpansionGameplay.Classic1782) return Get1782JumpParabola();
		float num = jumpEndPos - jumpStartPos;
		float num2 = Position.y - jumpStartPos;
		return 2f - 8f / Mathf.Pow(num, 2f) * Mathf.Pow(num2 - num / 2f, 2f);
	}

	public void EndJump()
	{
		circusClownJump = false;
		if (ExpansionGameplay.Classic1782) Hide1782BasketballPowerup();
		if (inSuperJump)
		{
			if (ExpansionGameplay.Classic1782) SuperJumpPowerupGroup.gameObject.SetActive(false);
			ballPowerupSuperJump.Hide();
			inSuperJump = false;
		}
		if (state == State.EndingJumping)
		{
			state = State.Ending;
			if (ExpansionGameplay.Classic1782) mJumpState = JumpState.Normal;
		}
		else if (ExpansionGameplay.Later) EndLaterJump();
		else if (currentTile != null && currentTile.IsJump)
		{
			Jump(currentTile, false, false);
		}
		else
		{
			state = State.Landing;
		}
	}

	private void UpdateVelocity()
	{
		bool timerExpired;
		switch (velocityState)
		{
		case VelocityState.Normal:
			ballMoveVector.y = Velocity;
			break;
		case VelocityState.SlowingDown:
		{
			float velocityPercent = GetVelocityPercent(0.25f, out timerExpired);
			if (timerExpired)
			{
				velocityTimer = 1f;
				velocityState = VelocityState.Slow;
				ballMoveVector.y = Velocity * 0.25f;
				RePitchGameMusic(0f);
			}
			else
			{
				float num2 = MathUtils.FromPercent(velocityPercent, 0.25f, 1f);
				ballMoveVector.y = Velocity * num2;
				RePitchGameMusic(velocityPercent);
			}
			break;
		}
		case VelocityState.Slow:
		{
			float velocityPercent = GetVelocityPercent(1f, out timerExpired);
			if (timerExpired)
			{
				velocityTimer = 5f;
				velocityState = VelocityState.SpeedingUp;
			}
			break;
		}
		case VelocityState.SpeedingUp:
		{
			float velocityPercent = GetVelocityPercent(5f, out timerExpired);
			if (timerExpired)
			{
				ballMoveVector.y = Velocity;
				velocityState = VelocityState.Normal;
				RePitchGameMusic(1f);
			}
			else
			{
				float num = MathUtils.FromPercent(velocityPercent, 1f, 0.25f);
				ballMoveVector.y = Velocity * num;
				RePitchGameMusic(velocityPercent);
			}
			break;
		}
		}
	}

	private void UpdateHeat()
	{
		heatTimer -= Time.smoothDeltaTime;
		if (heatTimer <= 0f)
		{
			heatTimer = 0f;
			heated = false;
		}
		heatPercent = heatTimer / 5f;
		DisplayHeat();
	}

	private Vector3 UpdateEnding(Vector3 ballVelocity, Vector3 newBallPos)
	{
		worldDistanceLeft -= ballVelocity.y;
		float num = worldDistanceLeft / worldEndDistance;
		if (num <= 0f)
		{
			num = 0f;
			state = State.Ended;
			ParticleManager.Detonate(PrefabName.PortalEnd);
			ballGeo.Hide();
			CameraControl.ShakeCamera(0.5f);
			animateWithFakeBallPos = true;
			IsNotTrackable = true;
			GameManager.OnWorldEnd();
			newBallPos.x = worldEndX.Max;
		}
		else
		{
			float percent = FloatAnim.Smooth(num, false, true);
			newBallPos.x = worldEndX.FromPercent(percent, true);
			if (IsTrackable && num <= 0.75f)
			{
				IsNotTrackable = true;
			}
		}
		return newBallPos;
	}

	private void UpdatePortalling()
	{
		float z = (1f - GameInput.HoldPercentSmart) * 0.9f;
		DisplayGroup.localPosition = new Vector3(0f, 0f, z);
		float num = 3f * FloatAnim.PowerSmooth(GameInput.HoldPercentSmart, true, false, 3);
		portalSpin += Time.smoothDeltaTime * num;
		DisplayGroup.localEulerAngles = new Vector3(360f * portalSpin, 0f, 0f);
	}

	public void Jump(Tile currentTile, bool forceJump, bool smartJump)
	{
		bool flag = false;
		if (!forceJump && !NotAlreadyActivated(currentTile))
		{
			return;
		}
		if (currentTile != null && !(ExpansionGameplay.Classic1782 && currentTile.TypeVal == (int)TileVal.BeanLv42ClownDown))
		{
			currentTile.PlayNewSound(AudioName.Jump);
		}
		if (currentTile != null && currentTile.IsDangerous)
		{
			bool shielded = ExpansionGameplay.Enabled && ClassicBoxShield.IsActive;
			GameManager.KillBall(Tile.DangerType.Danger);
			if (!shielded) return;
		}
		float num = 4f;
		if (ExpansionGameplay.Classic1782) num = Start1792CompatibleJump(currentTile);
		else if ((currentTile != null && currentTile.IsJump) || (ExpansionGameplay.Later && inSuccessiveJump))
		{
			if (!ExpansionGameplay.Later || !inSuccessiveJump) currentTile.Jump();
			if (currentTile.IsSuperJump)
			{
				num = ExpansionGameplay.Enabled ? 8f : 30f;
				if (ExpansionGameplay.Later && (TileVal)currentTile.TypeVal == TileVal.EnemySuperJumperFictitious) num = 6f;
				ballPowerupSuperJump.Show();
				inSuperJump = true;
				ballWingAngle.ForceSetTarget(0f);
			}
			else if (ExpansionGameplay.Later && (currentTile.IsSuccessiveJump || inSuccessiveJump)) num = StartSuccessiveJump();
		}
		if (state == State.Stopped)
		{
			ballMoveVector.y = Velocity;
		}
		jumpStartPos = GameManager.BallPosition.y;
		float num2 = jumpStartPos % 1f;
		float num3 = 0.25f - num2;
		jumpEndPos = jumpStartPos + num + num3;
		jumpMidPos = (jumpStartPos + jumpEndPos) / 2f;
		state = State.Jumping;
	}

	private void Land(Tile landedTile)
	{
		state = State.Rolling;
		ballMoveVector.z = 0f;
		Vector3 position = Position;
		position.z = -0.5f;
		SetPositionTo(position);
		TryPickupScore(landedTile);
	}

	public void Fall()
	{
		string text = ((!(currentTile == null)) ? string.Format("{0} (W: {1}, F: {2}, A: {3})", currentTile, currentTile.IsInUse, currentTile.IsFallen, currentTile.IsAirbourne) : "EMPTY");
		Debug.Log("TILE: DBEUG: Falling into tile: " + text);
		ballMoveVector.z = 9.8f;
		state = State.Falling;
		OnDeath();
	}

	public void ApplyHeat(float heatPercent)
	{
		if (heatPercent > this.heatPercent)
		{
			this.heatPercent = heatPercent;
			heated = true;
			heatTimer = 5f * heatPercent;
			DisplayHeat();
		}
	}

	public void Destroy(Tile.DangerType killingDangerType)
	{
		if (!IsNotDestroyed)
		{
			return;
		}
		state = State.Exploding;
		IsDestroyed = true;
		IsNotRotating = true;
		explodeTimer = 5f;
		if (usingFragments)
		{
			ballFragments.Show();
		}
		ballGeo.Hide();
		ballShadow.Hide();
		ballInner.Hide();
		if (killingDangerType == Tile.DangerType.Crusher && IsNotJumping && IsNotLanding)
		{
			ballMoveVector.z = -1f;
		}
		ParticleManager.Detonate(PrefabName.PickupBig);
		if (killingDangerType == Tile.DangerType.Heat)
		{
			ApplyHeat(1f);
			if (usingFragments)
			{
				for (int i = 0; i < fragmentParts.Length; i++)
				{
					fragmentParts[i].Energize();
				}
			}
		}
		if (heatPercent >= 0.75f)
		{
			ParticleManager.Detonate(PrefabName.Heat);
		}
		CameraControl.ShakeCamera(0.5f);
		if (UserSettings.IsSoundOn() && !GameManager.DisableSounds)
		{
			ExplodeSound.Play();
		}
		OnDeath();
	}

	public void StartWorldEnd(int worldEndLength)
	{
		if (state == State.Jumping)
		{
			state = State.EndingJumping;
		}
		else
		{
			state = State.Ending;
		}
		isEnding = true;
		IsImmortal = true;
		CameraControl.StartCentering();
		ballMoveVector.x = 0f;
		worldEndDistance = worldEndLength;
		worldEndDistance -= GameManager.BallPosition.y % 1f;
		worldEndDistance -= 1f;
		worldDistanceLeft = worldEndDistance;
		worldEndX = new MathUtils.Range(GameManager.BallPosition.x, 2.5f);
	}

	public bool CanFall(Tile currentTile)
	{
		if (ExpansionGameplay.Classic1782)
		{
			if (currentTile != null && currentTile.IsHidden) currentTile = null;
			if (currentTile != null && currentTile.type == Tile.Type.BackwardQuery && !currentTile.isQuery) currentTile.QueryEnd();
			if (currentTile == null && Check1782OutsideWall()) return false;
		}
		bool result = false;
		this.currentTile = currentTile;
		if (IsAlive)
		{
			if (currentTile != null)
			{
				TryPickupScore(currentTile);
			}
			if (currentTile == null || currentTile.IsAirbourne || currentTile.IsFallen)
			{
				result = !TryPreventFall();
			}
			else
			{
				if (currentTile.IsReset)
				{
					Debug.LogWarning(string.Format("BALL: WARNING: Rolling on reset tile: {0}", currentTile));
				}
				result = false;
				dipOffset = 0f;
				if (state == State.Landing)
				{
					Land(currentTile);
				}
				if (state == State.Rolling)
				{
					RollOnTile(currentTile);
				}
			}
		}
		return result;
	}

	private bool TryPreventFall()
	{
		bool flag;
		if (IsJumping || isEnding)
		{
			flag = true;
		}
		else
		{
			float num = Position.x;
			if (num < 0f)
			{
				num = 1f - MathUtils.Abs(num);
			}
			num %= 1f;
			float num2 = Position.y % 1f;
			float num3 = MathUtils.Distance(num, 0.5f, num2, 0.5f);
			if (num3 >= 0.35f)
			{
				int num4 = ((!(num < 0.5f)) ? 1 : (-1));
				int num5 = ((!(num2 < 0.5f)) ? 1 : (-1));
				int num6 = GameManager.BallTilePosition.x + num4;
				int num7 = GameManager.BallTilePosition.y;
				bool flag2;
				if (GameManager.TileIsSolid(num7, num6))
				{
					flag = true;
					flag2 = true;
				}
				else
				{
					num7 = GameManager.BallTilePosition.y + num5;
					if (GameManager.TileIsSolid(num7, num6))
					{
						flag = true;
						flag2 = false;
					}
					else
					{
						num6 = GameManager.BallTilePosition.x;
						if (GameManager.TileIsSolid(num7, num6))
						{
							flag = false;
							flag2 = true;
						}
						else
						{
							flag = false;
							flag2 = false;
						}
					}
				}
				if (flag && (flag2 || GameManager.TileBoard[num7, num6].IsNotJump))
				{
					RollOnTile(GameManager.TileBoard[num7, num6]);
				}
				if (false || !flag)
				{
					string text = ((!flag) ? " NOT" : null);
					Debug.Log(string.Format("BALL: DEBUG: Ball was{0} able to save itself from fall as the nearest adjacent tile {1}, {2} was{0} a solid tile\n  Current ball tile position: {3}, {4}\n  Inner tile position: {5:0.00}, {6:0.00}\n  Distance from current tile's center: {7:0.00}\n  Adjacent direction: {8}, {9}", text, num6, num7, GameManager.BallTilePosition.x, GameManager.BallTilePosition.y, num, num2, num3, num4, num5));
				}
			}
			else
			{
				flag = false;
				Debug.Log(string.Format("BALL: DEBUG: Ball was too close to center of tile to save\n  Current ball tile position: {0}, {1}\n  Inner tile position: {2:0.00}, {3:0.00}\n  Distance from current tile's center: {4:0.00}\n", GameManager.BallTilePosition.x, GameManager.BallTilePosition.y, num, num2, num3));
			}
		}
		return flag;
	}

	private void RollOnTile(Tile currentTile)
	{
		if (currentTile.IsJump)
		{
			Jump(currentTile, false, false);
		}
		if (currentTile.IsFragile)
		{
			FragmentTiles(currentTile);
		}
		else
		{
			shakeOffset = 0f;
		}
		if (currentTile.IsMobileTrigger && currentTile.IsMover)
		{
			MoveTiles(currentTile);
		}
	}

	private void FragmentTiles(Tile fragileTile)
	{
		if (!fragileTile.IsActivated)
		{
			fragileTile.PlayNewSound(AudioName.Fragile);
		}
		if (fragileTile.IsNotActivated)
		{
			fragileTile.Fragment(Position.y);
		}
	}

	private void MoveTiles(Tile moveTile)
	{
		if (moveTile.IsNotActivated && moveTile.IsNotMoved)
		{
			moveTile.Move(Position.y);
		}
	}

	private void TryPickupScore(Tile pickupTile)
	{
		pickupTile.TryPickup(IsRolling, IsJumping, base.transform);
	}

	private void OffsetBall(float promptPos)
	{
		ballIsOffset = promptPos != 0f;
		TransformUtils.SetX(OffsetGroup, promptPos, true);
	}

	private void ResetBallOffset(bool instantReset = false)
	{
		if (ballIsOffset)
		{
			float x = base.transform.localPosition.x;
			float promptPos = ((!instantReset && !(x <= 0.001f)) ? Mathf.Lerp(x, 0f, 5f * Time.smoothDeltaTime) : 0f);
			OffsetBall(promptPos);
		}
	}

	private void ResetBallFragments()
	{
		for (int i = 0; i < fragmentParts.Length; i++)
		{
			fragmentParts[i].Reset();
		}
	}

	private void OnDeath()
	{
		IsDead = true;
		if (ExpansionGameplay.Enabled) HideCollapsingForce();
		ballShadow.Hide();
		ballPowerups.Hide();
		StartFakingWorldAnimations();
		IsNotTrackable = true;
	}

	private float GetVelocityPercent(float velocityTimeTotal, out bool timerExpired)
	{
		velocityTimer -= Time.smoothDeltaTime;
		timerExpired = velocityTimer <= 0f;
		if (timerExpired)
		{
			return 0f;
		}
		float value = velocityTimer / velocityTimeTotal;
		return FloatAnim.Smooth(value, true, false);
	}

	private void RePitchGameMusic(float rePitchPercent)
	{
		float newPitch = MathUtils.FromPercent(rePitchPercent, 0.9f, 1f);
		MusicPlayer.AdjustPitch(newPitch);
	}

	private void DisplayHeat()
	{
		float num = FloatAnim.Smooth(heatPercent, false, true, 3);
	}

	public void StartFakingWorldAnimations()
	{
		animateWithFakeBallPos = true;
		fakeBallPos = Position;
		fakeBallSpeed = ballMoveVector.y;
	}

	private void AnimateWorld(Vector3 currentBallPosition)
	{
		TileAnimator.BallPos = currentBallPosition;
	}

	private void ShadowBall()
	{
		ballShadow.SetZ(-0.01f);
	}

	public void SetTargetHorizontalWorldXPos(float worldPosXTarget)
	{
		currentTargetXPos = worldPosXTarget;
		if (currentTargetXPos > GameManager.BallPosition.x)
		{
			SetMovingRight();
		}
		else if (currentTargetXPos < GameManager.BallPosition.x)
		{
			SetMovingLeft();
		}
		else
		{
			StopMovingLeftRight();
		}
	}

	private void SetMovingLeft()
	{
		currentHorizMove = HorizMoveDir.MoveLeft;
		CalculateDestinationDistanceFactor();
	}

	private void SetMovingRight()
	{
		currentHorizMove = HorizMoveDir.MoveRight;
		CalculateDestinationDistanceFactor();
	}

	private void StopMovingLeftRight()
	{
		currentHorizMove = HorizMoveDir.Straight;
		ballMoveVector.x = 0f;
	}

	private void CalculateDestinationDistanceFactor()
	{
		currentPosX.x = currentTargetXPos;
		currentPosX.y = 0f;
		destinationPosX.x = GameManager.BallPosition.x;
		destinationPosX.y = 0f;
		float num = Mathf.Abs(Vector2.Distance(currentPosX, destinationPosX));
		if (currentHorizMove == HorizMoveDir.MoveLeft)
		{
			ballMoveVector.x = num * -4f;
		}
		else if (currentHorizMove == HorizMoveDir.MoveRight)
		{
			ballMoveVector.x = num * 4f;
		}
		else
		{
			ballMoveVector.x = 0f;
		}
	}

	private bool NotAlreadyActivated(Tile currentTile)
	{
		return !AlreadyActivated(currentTile);
	}

	private bool AlreadyActivated(Tile currentTile)
	{
		if (currentTile.IsActivated)
		{
			return true;
		}
		GameManager.TileBoard[currentTile.Index].Activate();
		return false;
	}
}
