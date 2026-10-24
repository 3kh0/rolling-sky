using UnityEngine;

public class ParseUtils : MonoBehaviour
{
	public static bool CanParseToInt(string intString, out int returnInt)
	{
		return CanParseToInt(intString, true, out returnInt);
	}

	public static bool CanParseToInt(string intString, bool trimString, out int returnInt)
	{
		bool result = false;
		returnInt = -1;
		if (trimString)
		{
			intString = intString.Trim(' ');
		}
		if (!string.IsNullOrEmpty(intString))
		{
			result = int.TryParse(intString, out returnInt);
		}
		return result;
	}

	public static bool CannotParseToInt(string intString, out int returnInt)
	{
		return !CanParseToInt(intString, out returnInt);
	}

	public static bool CannotParseToInt(string intString, bool trimString, out int returnInt)
	{
		return !CanParseToInt(intString, trimString, out returnInt);
	}

	public static int? ParseToNullableInt(string intString)
	{
		return ParseToNullableInt(intString, true);
	}

	public static int? ParseToNullableInt(string intString, bool trimString)
	{
		int? result = null;
		if (trimString)
		{
			intString = intString.Trim(' ');
		}
		int result2;
		if (!string.IsNullOrEmpty(intString) && int.TryParse(intString, out result2))
		{
			result = result2;
		}
		return result;
	}

	public static bool CanParseToFloat(string floatString, out float returnFloat)
	{
		return CanParseToFloat(floatString, true, out returnFloat);
	}

	public static bool CanParseToFloat(string floatString, bool trimString, out float returnFloat)
	{
		bool result = false;
		returnFloat = -1f;
		if (trimString)
		{
			floatString = floatString.Trim(' ');
		}
		if (!string.IsNullOrEmpty(floatString))
		{
			result = float.TryParse(floatString, out returnFloat);
		}
		return result;
	}

	public static bool CannotParseToFloat(string floatString, out float returnFloat)
	{
		return !CanParseToFloat(floatString, out returnFloat);
	}

	public static bool CannotParseToFloat(string floatString, bool trimString, out float returnFloat)
	{
		return !CanParseToFloat(floatString, trimString, out returnFloat);
	}

	public static float? ParseToNullableFloat(string floatString)
	{
		int? num = ParseToNullableInt(floatString, true);
		return (!num.HasValue) ? ((float?)null) : new float?(num.Value);
	}

	public static float? ParseToNullableFloat(string floatString, bool trimString)
	{
		float? result = null;
		if (trimString)
		{
			floatString = floatString.Trim(' ');
		}
		float result2;
		if (!string.IsNullOrEmpty(floatString) && float.TryParse(floatString, out result2))
		{
			result = result2;
		}
		return result;
	}
}
