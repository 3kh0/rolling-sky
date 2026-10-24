using System;
using UnityEngine;

public class EnumUtils : MonoBehaviour
{
	public static bool CanParseToEnum<EnumType>(int enumInt)
	{
		return Enum.IsDefined(typeof(EnumType), enumInt);
	}

	public static bool CannotParseToEnum<EnumType>(int enumInt)
	{
		return !CanParseToEnum<EnumType>(enumInt);
	}

	public static bool CanParseToEnum<EnumType>(int enumInt, out EnumType parsedEnum)
	{
		if (CanParseToEnum<EnumType>(enumInt))
		{
			parsedEnum = (EnumType)Enum.Parse(typeof(EnumType), enumInt.ToString(), true);
			return true;
		}
		parsedEnum = default(EnumType);
		return false;
	}

	public static bool CannotParseToEnum<EnumType>(int enumInt, out EnumType parsedEnum)
	{
		return !CanParseToEnum<EnumType>(enumInt, out parsedEnum);
	}

	public static EnumType ParseToEnum<EnumType>(int enumInt)
	{
		bool parseSuccesful;
		return ParseToEnum<EnumType>(enumInt, out parseSuccesful);
	}

	public static EnumType ParseToEnum<EnumType>(int enumInt, out bool parseSuccesful)
	{
		EnumType parsedEnum;
		parseSuccesful = CanParseToEnum<EnumType>(enumInt, out parsedEnum);
		return parsedEnum;
	}

	public static bool CanParseToEnum<EnumType>(string enumString)
	{
		bool stringWasNumber;
		return CanParseToEnum<EnumType>(enumString, false, out stringWasNumber);
	}

	public static bool CanParseToEnum<EnumType>(string enumString, bool stringCanBeNumber)
	{
		bool stringWasNumber;
		return CanParseToEnum<EnumType>(enumString, stringCanBeNumber, out stringWasNumber);
	}

	public static bool CanParseToEnum<EnumType>(string enumString, bool stringCanBeNumber, out bool stringWasNumber)
	{
		bool flag = false;
		stringWasNumber = false;
		if (!string.IsNullOrEmpty(enumString))
		{
			int result;
			if (stringCanBeNumber && int.TryParse(enumString, out result))
			{
				stringWasNumber = CanParseToEnum<EnumType>(result);
				flag = stringWasNumber;
			}
			if (!flag)
			{
				stringWasNumber = false;
				try
				{
					Enum.Parse(typeof(EnumType), enumString, true);
					flag = true;
				}
				catch
				{
				}
			}
		}
		return flag;
	}

	public static bool CannotParseToEnum<EnumType>(string enumString)
	{
		return !CanParseToEnum<EnumType>(enumString);
	}

	public static bool CannotParseToEnum<EnumType>(string enumString, bool stringCanBeNumber)
	{
		return !CanParseToEnum<EnumType>(enumString, stringCanBeNumber);
	}

	public static bool CannotParseToEnum<EnumType>(string enumString, bool stringCanBeNumber, out bool stringWasNumber)
	{
		return !CanParseToEnum<EnumType>(enumString, stringCanBeNumber, out stringWasNumber);
	}

	public static EnumType ParseToEnum<EnumType>(string enumString)
	{
		EnumType parsedEnum;
		CanParseToEnum<EnumType>(enumString, out parsedEnum);
		return parsedEnum;
	}

	public static bool CanParseToEnum<EnumType>(string enumString, out EnumType parsedEnum)
	{
		bool stringWasNumber;
		return CanParseToEnum<EnumType>(enumString, out parsedEnum, false, out stringWasNumber);
	}

	public static bool CanParseToEnum<EnumType>(string enumString, out EnumType parsedEnum, bool stringCanBeNumber)
	{
		bool stringWasNumber;
		return CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber, out stringWasNumber);
	}

	public static bool CanParseToEnum<EnumType>(string enumString, out EnumType parsedEnum, bool stringCanBeNumber, out bool stringWasNumber)
	{
		bool flag = false;
		stringWasNumber = false;
		parsedEnum = default(EnumType);
		if (!string.IsNullOrEmpty(enumString))
		{
			int result;
			if (stringCanBeNumber && int.TryParse(enumString, out result))
			{
				flag = (stringWasNumber = CanParseToEnum<EnumType>(result, out parsedEnum));
			}
			if (!flag)
			{
				try
				{
					parsedEnum = (EnumType)Enum.Parse(typeof(EnumType), enumString, true);
					flag = true;
				}
				catch
				{
				}
			}
		}
		return flag;
	}

	public static bool CannotParseToEnum<EnumType>(string enumString, out EnumType parsedEnum)
	{
		return !CanParseToEnum<EnumType>(enumString, out parsedEnum);
	}

	public static bool CannotParseToEnum<EnumType>(string enumString, out EnumType parsedEnum, bool stringCanBeNumber)
	{
		return !CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber);
	}

	public static bool CannotParseToEnum<EnumType>(string enumString, out EnumType parsedEnum, bool stringCanBeNumber, out bool stringWasNumber)
	{
		return !CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber, out stringWasNumber);
	}

	public static EnumType ParseToEnum<EnumType>(string enumString, bool stringCanBeNumber)
	{
		EnumType parsedEnum;
		CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber);
		return parsedEnum;
	}

	public static EnumType ParseToEnum<EnumType>(string enumString, bool stringCanBeNumber, out bool stringWasNumber)
	{
		EnumType parsedEnum;
		CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber, out stringWasNumber);
		return parsedEnum;
	}

	public static EnumType ParseToEnum<EnumType>(string enumString, out bool parseSuccesful)
	{
		EnumType parsedEnum;
		parseSuccesful = CanParseToEnum<EnumType>(enumString, out parsedEnum);
		return parsedEnum;
	}

	public static EnumType ParseToEnum<EnumType>(string enumString, out bool parseSuccesful, bool stringCanBeNumber)
	{
		EnumType parsedEnum;
		parseSuccesful = CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber);
		return parsedEnum;
	}

	public static EnumType ParseToEnum<EnumType>(string enumString, out bool parseSuccesful, bool stringCanBeNumber, out bool stringWasNumber)
	{
		EnumType parsedEnum;
		parseSuccesful = CanParseToEnum<EnumType>(enumString, out parsedEnum, stringCanBeNumber, out stringWasNumber);
		return parsedEnum;
	}

	public static int ToInt<EnumType>(EnumType enumValue)
	{
		return (int)Convert.ChangeType(enumValue, typeof(int));
	}

	public static string ToString<EnumType>(EnumType enumValue, bool addSpaces = true)
	{
		string text = (string)Convert.ChangeType(enumValue, typeof(string));
		if (addSpaces)
		{
			bool flag = false;
			for (int i = 1; i < text.Length; i++)
			{
				if (text[i] == char.ToUpperInvariant(text[i]))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				string runningString = null;
				int num = 0;
				for (int j = 0; j < text.Length; j++)
				{
					if (j != 0 && text[j] == char.ToUpperInvariant(text[j]))
					{
						int length = j - num;
						string newWord = text.Substring(num, length);
						AddWord(ref runningString, newWord);
						num = j;
					}
				}
				if (num < text.Length)
				{
					string newWord2 = text.Substring(num);
					AddWord(ref runningString, newWord2);
				}
				text = runningString;
			}
		}
		return text;
	}

	public static EnumType MaxEnum<EnumType>()
	{
		int enumInt = MaxEnumAsInt<EnumType>();
		return ParseToEnum<EnumType>(enumInt);
	}

	public static int MaxEnumAsInt<EnumType>()
	{
		int i;
		for (i = 0; Enum.IsDefined(typeof(EnumType), i); i++)
		{
		}
		return i - 1;
	}

	public static float MaxEnumAsFloat<EnumType>()
	{
		return MaxEnumAsInt<EnumType>();
	}

	public static int TotalEnums<EnumType>()
	{
		return MaxEnumAsInt<EnumType>() + 1;
	}

	public static float TotalEnumsAsFloat<EnumType>()
	{
		return MaxEnumAsFloat<EnumType>() + 1f;
	}

	public static EnumType NextEnum<EnumType>(EnumType enumValue)
	{
		int enumInt = ToInt(enumValue);
		bool wrappedToZero;
		return GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	public static EnumType NextEnum<EnumType>(EnumType enumValue, out bool wrappedToZero)
	{
		int enumInt = ToInt(enumValue);
		return GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	public static void NextEnum<EnumType>(ref EnumType enumValue)
	{
		int enumInt = ToInt(enumValue);
		bool wrappedToZero;
		enumValue = GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	public static void NextEnum<EnumType>(ref EnumType enumValue, out bool wrappedToZero)
	{
		int enumInt = ToInt(enumValue);
		enumValue = GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	public static EnumType NextEnum<EnumType>(int enumInt)
	{
		bool wrappedToZero;
		return GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	public static EnumType NextEnum<EnumType>(int enumInt, out bool wrappedToZero)
	{
		return GetNextEnum<EnumType>(enumInt, out wrappedToZero);
	}

	private static EnumType GetNextEnum<EnumType>(int enumInt, out bool wrappedToZero)
	{
		enumInt++;
		wrappedToZero = !Enum.IsDefined(typeof(EnumType), enumInt);
		if (wrappedToZero)
		{
			enumInt = 0;
		}
		return (EnumType)Enum.Parse(typeof(EnumType), enumInt.ToString(), true);
	}

	private static void AddWord(ref string runningString, string newWord)
	{
		if (string.IsNullOrEmpty(runningString))
		{
			runningString = newWord;
		}
		else
		{
			runningString = runningString + " " + newWord;
		}
	}
}
