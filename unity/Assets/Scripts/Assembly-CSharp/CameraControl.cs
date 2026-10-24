using UnityEngine;

public class CameraControl : MonoBehaviour
{
	private const float shakeTimeMax = 1f;

	private const float slowTime = 2f;

	private static Transform mainCamGroup;

	private static Transform mainCamShaker;

	private static Elastic.Single camPosX = new Elastic.Single(6.5f, 0f, 0f, 0.001f);

	private static float camPosTimer;

	private static float camSpeed;

	private static float? lastCamPosY;

	private static float lastCamPosX;

	private static float camPosZStart;

	private static bool isPaused;

	private static bool isSlowing;

	private static bool isStopped;

	private static bool isShaking;

	private static float shakeAmmount;

	private static float shakePercent;

	private static float shakePercentStarting;

	private static float shakeTimer;

	private static float shakeTimerStarting;

	private static MathUtils.Buffer camSpeedBuffer = new MathUtils.Buffer(10);

	private static float slowTimer;

	private static bool isCentering;

	private static bool isInitialized;

	private static bool isNotPaused
	{
		get
		{
			return !isPaused;
		}
	}

	private static bool isNotSlowing
	{
		get
		{
			return !isSlowing;
		}
	}

	private static bool isNotStopped
	{
		get
		{
			return !isStopped;
		}
	}

	public static void Initialize(bool portalStart)
	{
		mainCamGroup = Camera.main.transform.parent.parent;
		mainCamShaker = Camera.main.transform.parent;
		if (mainCamGroup == null)
		{
			Debug.LogError("CMCT: ERROR: mainCameraGroup NULL passed to CameraControl.Initialize()");
		}
		camPosX.ForceSetTarget(2.5f);
		camPosZStart = ((!portalStart) ? 4f : 5f);
		camPosTimer = 1f;
		isPaused = true;
		isShaking = false;
		isSlowing = false;
		isStopped = true;
		shakePercent = 0f;
		isCentering = false;
		isInitialized = true;
	}

	public static void UpdateCamera()
	{
		if (!isInitialized)
		{
			return;
		}
		if (isShaking)
		{
			CalculateShakeAmmount();
		}
		if (Ball.IsTrackable)
		{
			camPosX.SetTarget(MathUtils.Lerp(GameManager.BallPosition.x, 2.5f, 0.25f));
			camPosX.Update();
			float num;
			float num2;
			if (isPaused)
			{
				num = 3f;
				num2 = camPosZStart;
			}
			else if (camPosTimer >= 0f)
			{
				camPosTimer -= Time.deltaTime;
				float value = camPosTimer / 1f;
				float weight = FloatAnim.Smooth(value, true, true);
				num = MathUtils.Lerp(3.5f, 3f, weight);
				num2 = MathUtils.Lerp(4f, camPosZStart, weight);
			}
			else
			{
				num = 3.5f;
				num2 = 4f;
			}
			if (mainCamGroup == null)
			{
				Debug.Log("CMCT: DEBUG: mainCamGroup null!");
			}
			mainCamGroup.position = new Vector3(camPosX.SmoothValue, GameManager.BallPosition.y - num, 0f - num2);
			if (lastCamPosY.HasValue)
			{
				float valueToAdd = mainCamGroup.position.y - lastCamPosY.Value;
				camSpeedBuffer.Add(valueToAdd);
			}
			lastCamPosY = mainCamGroup.position.y;
			lastCamPosX = mainCamGroup.position.x;
		}
		else if (isNotStopped)
		{
			if (isNotSlowing)
			{
				camSpeed = camSpeedBuffer.GetAverage();
				slowTimer = 2f;
				isSlowing = true;
			}
			float animPercent;
			if (FloatAnim.AnimElapsed(ref slowTimer, 2f, out animPercent, true))
			{
				isStopped = true;
			}
			else
			{
				float num3 = camSpeed * animPercent;
				float x;
				if (isCentering)
				{
					float weight2 = FloatAnim.Smooth(animPercent, true, true);
					x = MathUtils.Lerp(2.5f, lastCamPosX, weight2);
				}
				else
				{
					x = lastCamPosX;
				}
				mainCamGroup.position = new Vector3(x, mainCamGroup.position.y + num3, mainCamGroup.position.z);
			}
		}
		if (isShaking)
		{
			mainCamShaker.localPosition = new Vector3(0f, 0f, shakeAmmount);
		}
	}

	public static void StartCamera()
	{
		isPaused = false;
		isStopped = false;
	}

	public static void ShakeCamera(float shakeForcePercent)
	{
		if (shakeForcePercent > shakePercent)
		{
			isShaking = true;
			shakePercent = shakeForcePercent;
			shakePercentStarting = shakePercent;
			shakeTimer = 1f * shakePercent;
			shakeTimerStarting = shakeTimer;
		}
	}

	public static void Clear()
	{
		isInitialized = false;
		isCentering = false;
	}

	public static void StartCentering()
	{
		isCentering = true;
	}

	private static void CalculateShakeAmmount()
	{
		shakeTimer -= Time.smoothDeltaTime;
		if (shakeTimer < 0f)
		{
			shakeTimer = 0f;
			isShaking = false;
		}
		shakePercent = shakeTimer / shakeTimerStarting;
		float num = FloatAnim.Wave11(shakePercent * 7f);
		shakeAmmount = 0.5f * num * shakePercentStarting;
	}
}
