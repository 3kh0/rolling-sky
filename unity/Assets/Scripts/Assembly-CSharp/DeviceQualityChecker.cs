using UnityEngine;

public class DeviceQualityChecker : MonoBehaviour
{
	public enum Quality
	{
		Pour = 0,
		Low = 1,
		High = 2
	}

	private const bool debugsEnabled = false;

	private const bool debugQualityCheck = false;

	private const float padScreenWidthMinimum = 3f;

	private static Quality? debugQuality = GameManager.DebugQuality;

	private static Quality? quality;

	private static bool? isPad;

	public static int androidHighQualityMemoryMinimum = 436;

	public static Quality GetDeviceQuality()
	{
		if (!quality.HasValue)
		{
			if (debugQuality.HasValue)
			{
				quality = debugQuality.Value;
			}
			else
			{
				quality = Quality.Pour;
				int systemMemorySize = SystemInfo.systemMemorySize;
				if (systemMemorySize >= androidHighQualityMemoryMinimum)
				{
					quality = Quality.High;
				}
				else
				{
					quality = Quality.Pour;
				}
				TryDebugAndroidDeviceStats();
			}
		}
		return quality.Value;
	}

	public static int GetDeviceQualityAsInt()
	{
		return (int)GetDeviceQuality();
	}

	public static bool QualityIsPour()
	{
		return GetDeviceQuality() == Quality.Pour;
	}

	public static bool QualityIsLow()
	{
		return GetDeviceQuality() == Quality.Low;
	}

	public static bool QualityIsHigh()
	{
		return GetDeviceQuality() == Quality.High;
	}

	public static bool QualityIsNotPour()
	{
		return GetDeviceQuality() != Quality.Pour;
	}

	public static bool QualityIsNotLow()
	{
		return GetDeviceQuality() != Quality.Low;
	}

	public static bool QualityIsNotHigh()
	{
		return GetDeviceQuality() != Quality.High;
	}

	public static bool CanGetPhysicalScreenWidth(out float screenWidth)
	{
		float screenPixelWidth;
		return CanGetPhysicalScreenWidth(out screenWidth, out screenPixelWidth);
	}

	public static bool CanGetPhysicalScreenWidth(out float screenPhysicalWidth, out float screenPixelWidth)
	{
		//screenPixelWidth = MathUtils.Min(Screen.width, Screen.height);
		screenPixelWidth = Screen.width;
		float dpi = Screen.dpi;
		bool result;
		if (dpi == 0f)
		{
			result = false;
			screenPhysicalWidth = -1f;
		}
		else
		{
			result = true;
			screenPhysicalWidth = ((dpi != 0f) ? (screenPixelWidth / dpi) : (-1f));
		}
		return result;
	}

	public static bool IsPad()
	{
		return IsPad(true);
	}

	public static bool IsPad(bool alsoCheckScreenSize)
	{
		if (!isPad.HasValue)
		{
			isPad = false;
			Debug.LogWarning("DEVICE QUALITY: This class is not yet setup to detect Android tablets/pads - so returning false for IsPad");
			if (!isPad.HasValue || !isPad.Value)
			{
				if (alsoCheckScreenSize)
				{
					float screenWidth;
					if (CanGetPhysicalScreenWidth(out screenWidth))
					{
						isPad = screenWidth > 3f;
					}
					else
					{
						isPad = false;
					}
				}
				else
				{
					isPad = false;
				}
			}
		}
		return isPad.Value;
	}

	private static void TryDebugAndroidDeviceStats()
	{
	}
}
