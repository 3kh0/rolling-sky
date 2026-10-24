using UnityEngine;

public class Clock : MonoBehaviour
{
	public enum ClockType
	{
		Standard = 0,
		Countdown = 1,
		CountdownSeconds = 2
	}

	public delegate void FunctionToCallOnCountdown();

	public Renderer clockFace;

	public Transform hoursHand;

	public Transform minutesHand;

	public Transform secondsHand;

	public TextMesh timeLeft;

	public TextMesh[] numbers;

	public ClockType Type = ClockType.Countdown;

	public int TotalSeconds = 60;

	public int TotalMinutes = 60;

	public int TotalHours = 24;

	public bool Clockwise = true;

	private Material faceMaterial;

	private float degreesPerSecond;

	private float degreesPerMinute;

	private float? degreesPerHour;

	private bool isInitialized;

	private string timeLeftString;

	private bool callbackTried;

	private float angleDivider;

	private FunctionToCallOnCountdown OnCountdown;

	public float SecondsLeft { get; private set; }

	private void TryCallback()
	{
		if (OnCountdown != null)
		{
			OnCountdown();
		}
		callbackTried = true;
	}

	private void DiscardCallbacks()
	{
		if (OnCountdown != null)
		{
			OnCountdown = null;
		}
	}

	private void OnLevelWasLoaded()
	{
		DiscardCallbacks();
	}

	public void Initialize(float secondsToCountdown)
	{
		Configure(secondsToCountdown, null);
	}

	public void Initialize(float secondsToCountdown, FunctionToCallOnCountdown onCountdown)
	{
		Configure(secondsToCountdown, onCountdown);
	}

	private void Configure(float secondsToCountdown, FunctionToCallOnCountdown onCountdown)
	{
		TotalSeconds = (int)GameManager.SecondsPerRecharge;
		angleDivider = ((!Clockwise) ? (-360f) : 360f);
		degreesPerSecond = angleDivider / (float)TotalSeconds;
		degreesPerMinute = angleDivider / (float)TotalMinutes;
		degreesPerHour = ((TotalHours <= 0) ? 0f : (angleDivider / (float)TotalHours));
		if (clockFace != null)
		{
			faceMaterial = clockFace.material;
		}
		ClockType type = Type;
		if (type == ClockType.Countdown || type == ClockType.CountdownSeconds)
		{
			OnCountdown = onCountdown;
			if (Type == ClockType.CountdownSeconds)
			{
				TransformUtils.Hide(minutesHand);
				TransformUtils.Hide(hoursHand);
			}
			TimeManager.StartCountdown(secondsToCountdown);
			UpdateClock();
			if (numbers != null && numbers.Length > 0)
			{
				int num = (int)MathUtils.IntDivision(TotalSeconds, numbers.Length);
				int num2 = 0;
				for (int i = 0; i < numbers.Length; i++)
				{
					numbers[i].text = num2.ToString();
					num2 += num;
				}
			}
		}
		else
		{
			Debug.LogError(string.Format("CLOK: ERROR: Clock created of type {0}, which has not yet been setup", Type));
		}
		callbackTried = false;
		isInitialized = true;
	}

	public void UpdateClock()
	{
		ClockType type = Type;
		if (type != ClockType.Countdown && type != ClockType.CountdownSeconds)
		{
			return;
		}
		SecondsLeft = TimeManager.GetCountdown();
		string termCapitalized = Localizer.GetTermCapitalized("seconds");
		if (SecondsLeft <= 0f)
		{
			timeLeftString = "0 " + termCapitalized;
		}
		else if (SecondsLeft < 10f)
		{
			timeLeftString = string.Format("{0:0.0} {1}", SecondsLeft, termCapitalized);
		}
		else
		{
			timeLeftString = string.Format("{0:0.} {1}", SecondsLeft, termCapitalized);
		}
		Vector3 angles = GetAngles(SecondsLeft);
		if (Type != ClockType.CountdownSeconds)
		{
			if (hoursHand != null)
			{
				hoursHand.localEulerAngles = new Vector3(0f, 0f, angles.x);
			}
			minutesHand.localEulerAngles = new Vector3(0f, 0f, angles.y);
		}
		secondsHand.localEulerAngles = new Vector3(0f, 0f, angles.z);
		if (timeLeft != null)
		{
			timeLeft.text = timeLeftString;
		}
		if (faceMaterial != null)
		{
			if (Type == ClockType.CountdownSeconds)
			{
				faceMaterial.SetFloat("_Ammount", angles.z / angleDivider);
			}
			else
			{
				faceMaterial.SetFloat("_Ammount", angles.y / angleDivider);
				faceMaterial.SetFloat("_AmmountAlt", angles.z / angleDivider);
			}
		}
		if (!callbackTried && SecondsLeft <= 0f)
		{
			TryCallback();
		}
	}

	public string GetTimeLeftAsString()
	{
		return timeLeftString;
	}

	private Vector3 GetAngles(float totalSeconds)
	{
		Vector3 result;
		if (Type == ClockType.CountdownSeconds)
		{
			float value = totalSeconds * degreesPerSecond;
			value = MathUtils.FlooredAsFloat(value);
			result = new Vector3(0f, 0f, value);
		}
		else
		{
			float num = 0f;
			float value2 = (totalSeconds - num) / 60f;
			float num2 = MathUtils.FlooredAsFloat(value2);
			float num3 = num2 * 60f;
			float value3 = totalSeconds - (num + num3);
			float num4 = MathUtils.FlooredAsFloat(value3);
			float y = degreesPerMinute * num2;
			float z = degreesPerSecond * num4;
			result = new Vector3(0f, y, z);
		}
		return result;
	}
}
