using UnityEngine;

public class GameInput : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern float RollingSky_GetMousePosition();
#endif

	private enum InputState
	{
		None = 0,
		FirstPress = 1,
		Holding = 2,
		Depressing = 3,
		Depressed = 4
	}

	private const bool debugsEnabled = true;

	private const bool debugStateChanges = false;

	private const bool debugInputOffset = false;

	private const float defaultSensitivity = 0.3f;

	private const float optimalScreenWidth = 1.96319f;

	private const float doubleScreenExtraSensitivity = 0.2f;

	public const float HoldTime = 0.2f;

	private const float holdSmartDepressTime = 0.5f;

	private const float normalizeSpeed = 2f;

	private const float normalizeMargin = 1f;

	private const float normalizeThreshhold = 0.0001f;

	private const int lastPressedPositionsToTrack = 3;

	private static InputState inputState;

	private static InputState? inputStateLast;

	private static float screenWidth;

	private static float offsetNormalize;

	private static float inputOffset;

	private static bool holdRegistered;

	private static float holdTimer;

	private static float depressedTimer;

	private static float depressedTime;

	private static float holdPercentAtDepress;

	private static bool isSmartDepressing;

	private static float sensitivity;

	private static float? lastDepressPosition;

	private static MathUtils.Buffer lastPressedPositions;

	private static bool isInitialized;

	public static bool IsHolding { get; private set; }

	public static bool IsHoldingFull { get; private set; }

	public static bool IsHoldingSmart { get; private set; }

	public static float HoldPercent { get; private set; }

	public static float HoldPercentSmart { get; private set; }

	public static void Initialize()
	{
		if (isInitialized)
		{
			lastPressedPositions.Clear();
		}
		else
		{
			lastPressedPositions = new MathUtils.Buffer(3);
			isInitialized = true;
		}
		lastDepressPosition = null;
		inputStateLast = null;
		inputOffset = 0f;
		holdRegistered = false;
		HoldPercent = 0f;
		HoldPercentSmart = 0f;
		IsHolding = false;
		IsHoldingFull = false;
		IsHoldingSmart = false;
		isSmartDepressing = false;
		holdPercentAtDepress = 0f;
		depressedTimer = 0f;
		depressedTime = 0f;
		float screenPhysicalWidth;
		if (DeviceQualityChecker.CanGetPhysicalScreenWidth(out screenPhysicalWidth, out screenWidth))
		{
			float num = screenPhysicalWidth / 1.96319f;
			float num2 = MathUtils.Max(0f, num - 1f);
			float num3 = num + num2 * 0.2f;
			sensitivity = 0.3f * num3;
		}
		else
		{
			sensitivity = 0.3f;
		}
		offsetNormalize = 2f * (screenWidth / 10f);
		inputState = InputState.None;
	}

	public static void UpdateInputs()
	{
		if (!isInitialized)
		{
			return;
		}
		float mousePosition = -1f;
#if UNITY_WEBGL && !UNITY_EDITOR
		if (Screen.width > 0 && screenWidth > 0f && Screen.width != screenWidth)
		{
			float scale = Screen.width / screenWidth;
			inputOffset *= scale;
			if (lastDepressPosition.HasValue) lastDepressPosition *= scale;
			screenWidth = Screen.width;
			offsetNormalize = 2f * (screenWidth / 10f);
			lastPressedPositions.Clear();
			lastPressedPositions.Add(Input.mousePosition.x);
		}
		mousePosition = RollingSky_GetMousePosition();
#endif
		if (Input.GetMouseButtonDown(0))
		{
			inputState = InputState.FirstPress;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			inputState = InputState.Depressing;
		}
		else if (Input.GetMouseButton(0))
		{
			switch (inputState)
			{
			case InputState.Depressing:
			case InputState.Depressed:
				inputState = InputState.FirstPress;
				break;
			case InputState.FirstPress:
				inputState = InputState.Holding;
				break;
			default:
				Debug.LogWarning(string.Format("GMIN: ERROR: Received unhandled inputState case of '{0}' in GameInput.UpdateInputs()'s first case statement.  Check logic", inputState));
				inputState = InputState.Holding;
				break;
			case InputState.None:
			case InputState.Holding:
				break;
			}
		}
		else if (inputState != InputState.None && inputState != InputState.Depressed)
		{
			switch (inputState)
			{
			case InputState.Depressing:
				inputState = InputState.Depressed;
				break;
			case InputState.FirstPress:
			case InputState.Holding:
				inputState = InputState.Depressing;
				break;
			default:
				Debug.LogWarning(string.Format("GMIN: ERROR: Received unhandled inputState case of '{0}' in GameInput.UpdateInputs()'s second case statement.  Check logic", inputState));
				inputState = InputState.Depressed;
				break;
			}
		}
		if (inputState != InputState.None && inputState != InputState.Depressed)
		{
			float x = Input.mousePosition.x;
			switch (inputState)
			{
			case InputState.FirstPress:
				holdTimer = 0f;
				holdRegistered = false;
				IsHolding = true;
				IsHoldingSmart = true;
				IsHoldingFull = false;
				HoldPercent = HoldPercentSmart;
				isSmartDepressing = false;
				if (!lastDepressPosition.HasValue)
				{
					inputOffset = x - screenWidth / 2f;
				}
				else
				{
					float num2 = inputOffset;
					inputOffset = x - lastDepressPosition.Value + num2;
				}
				lastPressedPositions.Clear();
				lastPressedPositions.Add(x);
				GameManager.OnPress();
				break;
			case InputState.Holding:
				IsHolding = true;
				IsHoldingSmart = true;
				if (!holdRegistered)
				{
					holdTimer += Time.smoothDeltaTime;
					if (holdTimer >= 0.2f)
					{
						holdRegistered = true;
						IsHoldingFull = true;
						HoldPercent = 1f;
						HoldPercentSmart = 1f;
						GameManager.OnHold();
					}
					else
					{
						HoldPercent = holdTimer / 0.2f;
						HoldPercentSmart = HoldPercent;
						GameManager.OnHolding();
					}
				}
				if (MathUtils.Distance(x, lastPressedPositions.GetAverage()) >= 0.0001f)
				{
					if (Mathf.Abs(inputOffset) <= 1f)
					{
						inputOffset = 0f;
					}
					else
					{
						float num = offsetNormalize * Time.smoothDeltaTime;
						if (inputOffset < 0f)
						{
							inputOffset += num;
						}
						else
						{
							inputOffset -= num;
						}
					}
					GameManager.OnDrag();
				}
				lastPressedPositions.Add(x);
				break;
			case InputState.Depressing:
				inputState = InputState.Depressing;
				holdPercentAtDepress = HoldPercent;
				HoldPercentSmart = HoldPercent;
				depressedTime = 0.5f;
				depressedTimer = depressedTime;
				isSmartDepressing = true;
				HoldPercent = 0f;
				IsHoldingFull = false;
				IsHolding = false;
				IsHoldingSmart = true;
				lastDepressPosition = x;
				GameManager.OnDepress();
				break;
			default:
				Debug.LogWarning(string.Format("GMIN: ERROR: Received unhandled inputState case of '{0}' in GameInput.UpdateInputs()'s case statement.  Check logic", inputState));
				break;
			}
			float num3 = x - inputOffset;
			float num4 = num3 / screenWidth;
			float num5 = num4 * 2f - 1f;
			num5 *= 1f + sensitivity;
			float num6 = (num5 + 1f) / 2f;
			float worldPositionPressed = num6 * 5f;
			if (mousePosition < 0f)
			{
				GameManager.OnInput(worldPositionPressed);
			}
		}
		if (mousePosition >= 0f && !GameManager.GamePaused)
		{
			// Only the steering target changes. OnPress still starts the run, and
			// Ball keeps its original smoothing, forward movement and collisions.
			GameManager.OnInput(mousePosition * 5f);
		}
		if (isSmartDepressing)
		{
			depressedTimer -= Time.smoothDeltaTime;
			if (depressedTimer <= 0f)
			{
				isSmartDepressing = false;
				IsHoldingSmart = false;
				HoldPercentSmart = 0f;
			}
			else
			{
				float num7 = depressedTimer / depressedTime;
				HoldPercentSmart = holdPercentAtDepress * num7;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.LogWarning(string.Format("Row: {0}, Segment: {1}", GameManager.BallRowNum, SegmentTracker.CurrentSegmentIDAsString));
		}
	}

	private static float GetSmoothed(float value)
	{
		return FloatAnim.Smooth(value, true, false);
	}
}
