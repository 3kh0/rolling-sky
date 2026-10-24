using UnityEngine;

public class FinishManager : MonoBehaviour
{
	private class GoalWall
	{
		private enum State
		{
			Offscreen = 0,
			Waiting = 1,
			Visible = 2,
			Ready = 3,
			Shattering = 4
		}

		private Transform wall;

		private WallTile[] wallTiles;

		private State state;

		private float shatterTimer;

		private float shatterTimeMax;

		public static float WorldCenter;

		private static float StartingDistance;

		public int Distance { get; private set; }

		public float DistanceAsFloat { get; private set; }

		public bool IsHighscore { get; private set; }

		public bool IsFinish
		{
			get
			{
				return !IsHighscore;
			}
		}

		public bool IsShown { get; private set; }

		public GoalWall(bool isHighscore)
		{
			IsHighscore = isHighscore;
			shatterTimeMax = ((!isHighscore) ? 4f : 1f);
			Reset();
		}

		public void Reset()
		{
			if (IsShown)
			{
				Clear();
			}
			state = State.Offscreen;
			IsShown = false;
			if (IsFinish)
			{
				StartingDistance = CalculateBallDistanceTo(GoalAsFloat);
			}
		}

		public void ShowAt(int tileDistance)
		{
			if (IsShown)
			{
				Debug.LogWarning(string.Format("FGGW: WARNING: Asked to show {0} goal twice, once at {1} and again at {2}", (!IsFinish) ? "highscore" : "finish", Distance, tileDistance));
				Clear();
			}
			Distance = tileDistance;
			DistanceAsFloat = Distance;
			if (IsHighscore)
			{
				Distance += 2;
				DistanceAsFloat += 2f;
			}
			wall = BufferManager.GetGeo(PrefabType.General, PrefabName.FinishLine);
			Transform parentTransform = TransformUtils.FindChild(wall, "anim");
			ArrayUtils.List<Transform> list = new ArrayUtils.List<Transform>();
			Transform[] children = TransformUtils.GetChildren(parentTransform);
			foreach (Transform transform in children)
			{
				if (StartsWith(transform.name, "block"))
				{
					list.Add(transform);
				}
			}
			Transform[] array = list.ToArray();
			float num = 6f;
			wallTiles = new WallTile[array.Length];
			for (int j = 0; j < wallTiles.Length; j++)
			{
				wallTiles[j] = new WallTile(array[j], j, IsHighscore);
			}
			float y = DistanceAsFloat - 0.5f;
			wall.position = new Vector3(WorldCenter, y, -0.5f);
			IsShown = true;
			state = State.Waiting;
		}

		public void UpdateGoalWall()
		{
			float num = -1f;
			if (IsShown || IsFinish)
			{
				num = CalculateBallDistanceAndPercent();
			}
			if (!IsShown)
			{
				return;
			}
			switch (state)
			{
			case State.Offscreen:
				break;
			case State.Waiting:
				if (num <= (float)highlightRange.Max)
				{
					state = State.Visible;
				}
				break;
			case State.Visible:
			{
				float num2 = highlightRange.ToPercent(num);
				if (num2 <= 0f)
				{
					num2 = 0f;
					state = State.Ready;
				}
				break;
			}
			case State.Ready:
				if (num <= 0f && !ballDead)
				{
					state = State.Shattering;
					shatterTimer = 0f;
					float ballXPos = GameManager.BallPosition.x - WorldCenter;
					bool isJumping = GameManager.theBall.IsJumping;
					for (int j = 0; j < wallTiles.Length; j++)
					{
						wallTiles[j].SetHitPoint(ballXPos, isJumping);
					}
					if (IsFinish)
					{
						SaveProgress();
						GameManager.StartEndingWorld();
					}
					else
					{
						GameManager.ApplyScoreFor(Tile.PickupType.Big);
					}
				}
				break;
			case State.Shattering:
			{
				float smoothDeltaTime = Time.smoothDeltaTime;
				shatterTimer += smoothDeltaTime;
				float shatterPercent = ((!(shatterTimer >= shatterTimeMax)) ? (shatterTimer / shatterTimeMax) : 1f);
				for (int i = 0; i < wallTiles.Length; i++)
				{
					wallTiles[i].UpdateShatter(smoothDeltaTime, shatterTimer, shatterPercent);
				}
				if (shatterTimer >= shatterTimeMax)
				{
					Clear();
				}
				break;
			}
			}
		}

		public void Clear()
		{
			if (IsShown)
			{
				for (int i = 0; i < wallTiles.Length; i++)
				{
					wallTiles[i].Reset();
				}
				BufferManager.GiveGeo(wall, prefabID);
				IsShown = false;
			}
			state = State.Offscreen;
		}

		private float CalculateBallDistanceAndPercent()
		{
			float num;
			if (IsShown)
			{
				num = CalculateBallDistanceTo(DistanceAsFloat);
				if (IsFinish)
				{
					CalculateWorldProgress(num);
				}
			}
			else if (IsFinish)
			{
				num = CalculateBallDistanceTo(GoalAsFloat);
				CalculateWorldProgress(num);
			}
			else
			{
				num = -1f;
			}
			return num;
		}

		private float CalculateBallDistanceTo(float goal)
		{
			return goal - (GameManager.BallPosition.y + 1f);
		}

		private void CalculateWorldProgress(float distance)
		{
			ProgressPercent = 1f - distance / StartingDistance;
			int num = MathUtils.Floored(ProgressPercent * 100f);
			if (!GoalReached && !ballDead && num > ProgressPercentage)
			{
				ProgressPercentage = num;
				if (!GoalReached && ProgressPercentage >= 100)
				{
					GoalReached = true;
				}
				GameHud.DisplayProgress(ProgressPercentage, ProgressPercent);
			}
		}
	}

	private class WallTile
	{
		private int firstFrame;

		private bool fullAnim;

		private Transform transform;

		private float riseDelay;

		private float proximity;

		private float proximityAbs;

		private float animDirection;

		private Vector3 startPosition;

		private Vector3 startRotation;

		private Vector3 moveVector;

		private float riseSpeedMax;

		private float xPercent;

		private bool onTopRow;

		private bool risingUp;

		private bool risingUpFast;

		private FrameAnim.SequenceAuto block;

		public WallTile(Transform transform, int number, bool isHighscore)
		{
			startPosition = transform.localPosition;
			startRotation = transform.localEulerAngles;
			firstFrame = (isHighscore ? 2 : 0);
			fullAnim = !isHighscore;
			this.transform = transform;
			int rowPos;
			int colPos;
			MathUtils.ToGrid(number + 1, 2, out rowPos, out colPos);
			xPercent = 0f - (transform.localPosition.x + 0.5f) / 5f;
			onTopRow = MathUtils.IsOdd(rowPos);
			block = new FrameAnim.SequenceAuto(transform);
			block.Show(1 + firstFrame);
		}

		public void Highlight()
		{
			block.Show(firstFrame);
		}

		public void SetHitPoint(float ballXPos, bool ballJumping)
		{
			animDirection = ((!ballJumping) ? 1f : (-1f));
			float num = (ballXPos + 2.5f) / 5f;
			proximity = xPercent - num;
			proximityAbs = MathUtils.Abs(proximity);
			riseSpeedMax = ((!onTopRow) ? (-0.1f) : (-0.1f));
			moveVector = new Vector3(proximity * 0.1f, 1f, 0f);
			float num2 = FloatAnim.Smooth(MathUtils.Abs(proximity), true, false);
			if (onTopRow)
			{
				num2 -= 0.1f;
			}
			riseDelay = num2 * 1.5f;
			risingUp = false;
			risingUpFast = false;
			Highlight();
		}

		public void UpdateShatter(float time, float shatterTimer, float shatterPercent)
		{
			if (fullAnim)
			{
				Vector3 position = transform.position + moveVector * 5f * time;
				if (!risingUp && shatterTimer > riseDelay)
				{
					risingUp = true;
				}
				float num = 0f;
				if (risingUpFast)
				{
					num = 1f;
				}
				else
				{
					num = MathUtils.ToPercent(shatterTimer - riseDelay, 0f, 0.5f);
					if (num > 1f)
					{
						num = 1f;
						risingUpFast = true;
					}
					else
					{
						num = FloatAnim.Smooth(num, true, false);
					}
				}
				if (risingUp)
				{
					position.z += riseSpeedMax * num;
				}
				transform.position = position;
			}
			else
			{
				float num2 = MathUtils.ToPercent01(shatterPercent, proximityAbs, 0.5f + proximityAbs);
				float num3 = FloatAnim.Wave01(MathUtils.ToPercent01(shatterPercent, proximityAbs, 0.25f + proximityAbs));
				transform.localEulerAngles = new Vector3(360f * num2 * animDirection, 0f, 0f);
				transform.localPosition = new Vector3(startPosition.x, startPosition.y + num3 * 0.125f, startPosition.z);
			}
		}

		public void Reset()
		{
			transform.localPosition = startPosition;
			transform.localEulerAngles = startRotation;
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugHighscore = false;

	private const string wallTileName = "block";

	private const int minimumGoal = 50;

	private const int finishToHighscoreGap = 5;

	private const float highscorePosOffset = 0.5f;

	private const float highscoreHeight = 0.5f;

	private const float highscoreWidth = 5f;

	private const float shatterForce = 5f;

	private const float shatterUpwardsUpperPercent = 0.1f;

	private const float shatterUpwardsLowerPercent = 0.1f;

	private const float shatterRiseDelayMax = 1.5f;

	private const float shatterRiseSmoothTime = 0.5f;

	private const float shatterTimeFinishMax = 4f;

	private const float shatterTimeHighscoreMax = 1f;

	private const float splitHorizontallyPercent = 0.1f;

	private const float splitVerticallyPercent = 0.1f;

	private const float goalWidth = 5f;

	private static MathUtils.RangeInt highlightRange = new MathUtils.RangeInt(1, 10);

	private static PrefabID prefabID = new PrefabID(PrefabType.General, PrefabName.FinishLine);

	private static GoalWall highscore;

	private static GoalWall finish;

	private static bool initialized;

	private static bool ballDead;

	public static int Goal { get; private set; }

	public static float GoalAsFloat { get; private set; }

	public static float ProgressPercent { get; private set; }

	public static int ProgressPercentage { get; private set; }

	public static bool GoalReached { get; private set; }

	public static void Initialize(float worldCenterX)
	{
		Goal = LevelDesigner.WorldLength;
		GoalAsFloat = Goal;
		GoalWall.WorldCenter = worldCenterX;
		ProgressPercent = 0f;
		ProgressPercentage = 0;
		GoalReached = false;
		highscore = new GoalWall(true);
		finish = new GoalWall(false);
		float value = ((!ProgressManager.IsWorldComplete()) ? ProgressManager.GetProgressFloat() : ProgressManager.GetProgressFloatThisSession());
		int num = MathUtils.FlooredMultiplication(value, LevelDesigner.WorldLengthAsFloat);
		ballDead = false;
	}

	public static void ShowFinishAt(int tileDistance)
	{
		finish.ShowAt(tileDistance);
	}

	public static void ShowHighscoreAt(int tileDistance)
	{
		highscore.ShowAt(tileDistance);
	}

	public static void UpdateGoals()
	{
		highscore.UpdateGoalWall();
		finish.UpdateGoalWall();
	}

	public static void Clear()
	{
		highscore.Clear();
		finish.Clear();
	}

	public static void OnDeath()
	{
		SaveProgress();
		ballDead = true;
	}

	public static void OnReload()
	{
	}

	private static void SaveProgress()
	{
		ProgressManager.SetProgressFor(GameManager.WorldIndex, ProgressPercentage, GameManager.GemsThisGame, GameManager.CrownsThisGame);
	}

	private static bool StartsWith(string text, string query)
	{
		bool result = false;
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(query) && text.Length >= query.Length)
		{
			string text2 = text.Substring(0, query.Length);
			result = text2.Equals(query);
		}
		return result;
	}
}
