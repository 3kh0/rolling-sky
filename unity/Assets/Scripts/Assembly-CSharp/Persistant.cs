using System;
using System.Globalization;
using UnityEngine;

public class Persistant : MonoBehaviour
{
	public abstract class ClassTypePersistant
	{
		private const bool debugsEnabled = true;

		private const bool debugSuccess = false;

		private bool hasValue;

		protected bool hasNoLocalValue;

		public string Name { get; private set; }

		public bool HasValue
		{
			get
			{
				if (!hasValue && hasNoLocalValue)
				{
					TryLoadValue();
				}
				return hasValue;
			}
			private set
			{
				hasValue = value;
			}
		}

		protected string playerPrefsKey { get; private set; }

		protected void ConfigurePersistant(string playerPrefsKey, string variableName, string ownerName)
		{
			if (string.IsNullOrEmpty(variableName))
			{
				variableName = playerPrefsKey;
			}
			if (string.IsNullOrEmpty(ownerName))
			{
				Name = variableName;
			}
			else
			{
				Name = ownerName + "'s " + variableName;
			}
			this.playerPrefsKey = playerPrefsKey;
			hasNoLocalValue = true;
			if (!allKeys.ContainsKey(playerPrefsKey))
			{
				allKeys.Add(playerPrefsKey, Name);
			}
		}

		protected void SaveValue()
		{
			WriteToPlayerPrefs();
			hasNoLocalValue = false;
			HasValue = true;
			OnValueChanged();
		}

		protected bool TryLoadValue()
		{
			bool result = true;
			if (hasNoLocalValue)
			{
				HasValue = ReadFromPlayerPrefs();
				result = false;
				hasNoLocalValue = false;
			}
			return result;
		}

		protected abstract void WriteToPlayerPrefs();

		protected abstract bool ReadFromPlayerPrefs();

		protected virtual void OnValueChanged()
		{
		}

		protected string GetDebugWritingString(object debugValue, bool playerPrefsSuccess)
		{
			if (playerPrefsSuccess)
			{
				return string.Format("IAPM DEBUG: wrote {0}'s value of {1} to playerPrefs using key: {2}", Name, debugValue, playerPrefsKey);
			}
			return string.Format("IAPM DEBUG: skipped writing {0}'s invalid value of {1} to playerPrefs using key: {1}", Name, debugValue, playerPrefsKey);
		}

		protected string GetDebugReadingString(bool playerPrefsSuccess, bool localSuccess)
		{
			if (localSuccess)
			{
				return string.Format("IAPM DEBUG: read {0}'s value from local variable", Name);
			}
			if (playerPrefsSuccess)
			{
				return string.Format("IAPM DEBUG: read {0}'s value from playerPrefs using key: {1}", Name, playerPrefsKey);
			}
			return string.Format("IAPM DEBUG: unable to find {0} in playerPrefs using key: {1}", Name, playerPrefsKey);
		}
	}

	public abstract class ClassTypePersistantBasic<Type> : ClassTypePersistant
	{
		protected Type localValue;

		protected Nullable<Type> startingValue;

		protected void ConfigurePersistantBasic(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			localValue = GetInvalidValue();
		}

		protected void ConfigurePersistantBasic(string playerPrefsKey, string variableName, string ownerName, Type startingValue)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			this.startingValue = new Nullable<Type>(startingValue);
			localValue = startingValue;
		}

		public void Set(Type newValue)
		{
			if (IsNotEqualTo(newValue))
			{
				localValue = newValue;
				SaveValue();
			}
		}

		public void Clear()
		{
			Reset();
		}

		public Type Get()
		{
			bool requiredLoading;
			return Get(out requiredLoading);
		}

		public Type Get(out bool requiredLoading)
		{
			requiredLoading = TryLoadValue();
			return localValue;
		}

		protected override bool ReadFromPlayerPrefs()
		{
			bool result;
			if (PlayerPrefs.HasKey(base.playerPrefsKey))
			{
				result = true;
				ReadValueFromPlayerPrefs();
			}
			else
			{
				result = false;
				Reset();
			}
			return result;
		}

		protected void Reset()
		{
			localValue = ((!startingValue.HasValue) ? GetInvalidValue() : startingValue.Value);
			SaveValue();
		}

		protected abstract Type GetInvalidValue();

		protected abstract bool IsEqualTo(Type queryValue);

		private bool IsNotEqualTo(Type queryValue)
		{
			return !IsEqualTo(queryValue);
		}

		protected abstract void ReadValueFromPlayerPrefs();
	}

	public abstract class ClassTypePersistantNumeric<Type> : ClassTypePersistantBasic<Type>
	{
		public Type Append()
		{
			Set(GetSummedWith(GetOneValue(), true));
			return localValue;
		}

		public Type Append(Type by)
		{
			Set(GetSummedWith(by, true));
			return localValue;
		}

		public Type Desend()
		{
			Set(GetSummedWith(GetOneValue(), false));
			return localValue;
		}

		public Type Descend(Type by)
		{
			Set(GetSummedWith(by, false));
			return localValue;
		}

		protected abstract Type GetOneValue();

		protected abstract Type GetSummedWith(Type valueToAdd, bool additionNotSubtraction);
	}

	public class String : ClassTypePersistantBasic<string>
	{
		private const string invalidValue = null;

		public String(string playerPrefsKey)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null);
		}

		public String(string playerPrefsKey, string variableName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null);
		}

		public String(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName);
		}

		protected String()
		{
		}

		protected override string GetInvalidValue()
		{
			return null;
		}

		protected override bool IsEqualTo(string queryValue)
		{
			if (string.IsNullOrEmpty(localValue))
			{
				return string.IsNullOrEmpty(queryValue);
			}
			return localValue.Equals(queryValue);
		}

		protected override void WriteToPlayerPrefs()
		{
			PlayerPrefs.SetString(base.playerPrefsKey, localValue);
		}

		protected override void ReadValueFromPlayerPrefs()
		{
			localValue = PlayerPrefs.GetString(base.playerPrefsKey);
		}
	}

	public class Float : ClassTypePersistantNumeric<float>
	{
		private const float invalidValue = -1f;

		private const float oneValue = 1f;

		public Float(string playerPrefsKey)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null);
		}

		public Float(string playerPrefsKey, string variableName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null);
		}

		public Float(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName);
		}

		public Float(string playerPrefsKey, float startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null, startingValue);
		}

		public Float(string playerPrefsKey, string variableName, float startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null, startingValue);
		}

		public Float(string playerPrefsKey, string variableName, string ownerName, float startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected Float()
		{
		}

		public int GetAsInt()
		{
			return (int)Get();
		}

		public string GetAsString()
		{
			return Get().ToString();
		}

		public string GetAsString(string format)
		{
			return Get().ToString(format);
		}

		public int GetAsInt(out bool requiredLoading)
		{
			return (int)Get(out requiredLoading);
		}

		public string GetAsString(out bool requiredLoading)
		{
			return Get(out requiredLoading).ToString();
		}

		public string GetAsString(string format, out bool requiredLoading)
		{
			return Get(out requiredLoading).ToString(format);
		}

		protected override float GetInvalidValue()
		{
			return -1f;
		}

		protected override float GetOneValue()
		{
			return 1f;
		}

		protected override bool IsEqualTo(float queryValue)
		{
			return localValue == queryValue;
		}

		protected override float GetSummedWith(float valueToAdd, bool additionOrSubtraction)
		{
			if (additionOrSubtraction)
			{
				return localValue + valueToAdd;
			}
			return localValue - valueToAdd;
		}

		protected override void WriteToPlayerPrefs()
		{
			PlayerPrefs.SetFloat(base.playerPrefsKey, localValue);
		}

		protected override void ReadValueFromPlayerPrefs()
		{
			localValue = PlayerPrefs.GetFloat(base.playerPrefsKey);
		}
	}

	public class Int : ClassTypePersistantNumeric<int>
	{
		private const int invalidValue = -1;

		private const int oneValue = 1;

		public Int(string playerPrefsKey)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null);
		}

		public Int(string playerPrefsKey, string variableName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null);
		}

		public Int(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName);
		}

		public Int(string playerPrefsKey, int startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null, startingValue);
		}

		public Int(string playerPrefsKey, string variableName, int startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null, startingValue);
		}

		public Int(string playerPrefsKey, string variableName, string ownerName, int startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected Int()
		{
		}

		public float ToFloat()
		{
			return Get();
		}

		public override string ToString()
		{
			return Get().ToString();
		}

		public string ToString(string format)
		{
			return Get().ToString(format);
		}

		public float ToFloat(out bool requiredLoading)
		{
			return Get(out requiredLoading);
		}

		public string ToString(out bool requiredLoading)
		{
			return Get(out requiredLoading).ToString();
		}

		public string ToString(string format, out bool requiredLoading)
		{
			return Get(out requiredLoading).ToString(format);
		}

		protected override int GetInvalidValue()
		{
			return -1;
		}

		protected override int GetOneValue()
		{
			return 1;
		}

		protected override bool IsEqualTo(int queryValue)
		{
			return localValue == queryValue;
		}

		protected override int GetSummedWith(int valueToAdd, bool additionOrSubtraction)
		{
			if (additionOrSubtraction)
			{
				return localValue + valueToAdd;
			}
			return localValue - valueToAdd;
		}

		protected override void WriteToPlayerPrefs()
		{
			PlayerPrefs.SetInt(base.playerPrefsKey, localValue);
		}

		protected override void ReadValueFromPlayerPrefs()
		{
			localValue = PlayerPrefs.GetInt(base.playerPrefsKey);
		}
	}

	public class Bool : ClassTypePersistantBasic<bool>
	{
		private const bool invalidValue = false;

		public Bool(string playerPrefsKey)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null);
		}

		public Bool(string playerPrefsKey, string variableName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null);
		}

		public Bool(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName);
		}

		public Bool(string playerPrefsKey, bool startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, null, null, startingValue);
		}

		public Bool(string playerPrefsKey, string variableName, bool startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, null, startingValue);
		}

		public Bool(string playerPrefsKey, string variableName, string ownerName, bool startingValue)
		{
			ConfigurePersistantBasic(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected Bool()
		{
		}

		public float ToInt()
		{
			bool requiredLoading;
			return ToInt(out requiredLoading);
		}

		public float ToIntSign()
		{
			bool requiredLoading;
			return ToIntSign(out requiredLoading);
		}

		public float ToFloat()
		{
			bool requiredLoading;
			return ToFloat(out requiredLoading);
		}

		public float ToFloatSign()
		{
			bool requiredLoading;
			return ToFloatSign(out requiredLoading);
		}

		public override string ToString()
		{
			bool requiredLoading;
			return ToString(out requiredLoading);
		}

		public float ToInt(out bool requiredLoading)
		{
			return Get(out requiredLoading) ? 1 : 0;
		}

		public float ToIntSign(out bool requiredLoading)
		{
			return Get(out requiredLoading) ? 1 : (-1);
		}

		public float ToFloat(out bool requiredLoading)
		{
			return (!Get(out requiredLoading)) ? 0f : 1f;
		}

		public float ToFloatSign(out bool requiredLoading)
		{
			return (!Get(out requiredLoading)) ? (-1f) : 1f;
		}

		public string ToString(out bool requiredLoading)
		{
			return Get(out requiredLoading).ToString();
		}

		protected override bool GetInvalidValue()
		{
			return false;
		}

		protected override bool IsEqualTo(bool queryValue)
		{
			return localValue == queryValue;
		}

		protected override void WriteToPlayerPrefs()
		{
			int value = (localValue ? 1 : 0);
			PlayerPrefs.SetInt(base.playerPrefsKey, value);
		}

		protected override void ReadValueFromPlayerPrefs()
		{
			int num = PlayerPrefs.GetInt(base.playerPrefsKey);
			localValue = num == 1;
		}
	}

	public class DateTime : ClassTypePersistant
	{
		private string stringFormat = "yyyy-MM-dd HH:mm:ss";

		protected System.DateTime localValue;

		protected System.DateTime? startingValue;

		public DateTime(string playerPrefsKey)
		{
			ConfigureDateTime(playerPrefsKey, null, null, null);
		}

		public DateTime(string playerPrefsKey, System.DateTime startingValue)
		{
			ConfigureDateTime(playerPrefsKey, null, null, startingValue);
		}

		public DateTime(string playerPrefsKey, string variableName)
		{
			ConfigureDateTime(playerPrefsKey, variableName, null, null);
		}

		public DateTime(string playerPrefsKey, string variableName, System.DateTime startingValue)
		{
			ConfigureDateTime(playerPrefsKey, variableName, null, startingValue);
		}

		public DateTime(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureDateTime(playerPrefsKey, variableName, ownerName, null);
		}

		public DateTime(string playerPrefsKey, string variableName, string ownerName, System.DateTime startingValue)
		{
			ConfigureDateTime(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected DateTime()
		{
		}

		private void ConfigureDateTime(string playerPrefsKey, string variableName, string ownerName, System.DateTime? startingValue)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			if (startingValue.HasValue)
			{
				this.startingValue = startingValue.Value;
				localValue = startingValue.Value;
			}
		}

		public void Set(System.DateTime newValue)
		{
			if (!newValue.Equals(localValue))
			{
				localValue = newValue;
				SaveValue();
			}
		}

		public void SetAsNow()
		{
			Set(System.DateTime.UtcNow);
		}

		public System.DateTime Get()
		{
			bool requiredLoading;
			return Get(out requiredLoading);
		}

		public System.DateTime Get(out bool requiredLoading)
		{
			requiredLoading = TryLoadValue();
			return localValue;
		}

		public long GetTicks()
		{
			return Get().Ticks;
		}

		public int GetTicksAsInt()
		{
			return (int)Get().Ticks;
		}

		public int GetMillisecond()
		{
			return Get().Millisecond;
		}

		public int GetSecond()
		{
			return Get().Second;
		}

		public int GetMinute()
		{
			return Get().Minute;
		}

		public int GetHour()
		{
			return Get().Hour;
		}

		public int GetDay()
		{
			return Get().Day;
		}

		public int GetDayOfYear()
		{
			return Get().DayOfYear;
		}

		public int GetMonth()
		{
			return Get().Month;
		}

		public int GetYear()
		{
			return Get().Year;
		}

		public long GetTicks(out bool requiredLoading)
		{
			return Get(out requiredLoading).Ticks;
		}

		public int GetTicksAsInt(out bool requiredLoading)
		{
			return (int)Get(out requiredLoading).Ticks;
		}

		public int GetMillisecond(out bool requiredLoading)
		{
			return Get(out requiredLoading).Millisecond;
		}

		public int GetSecond(out bool requiredLoading)
		{
			return Get(out requiredLoading).Second;
		}

		public int GetMinute(out bool requiredLoading)
		{
			return Get(out requiredLoading).Minute;
		}

		public int GetHour(out bool requiredLoading)
		{
			return Get(out requiredLoading).Hour;
		}

		public int GetDay(out bool requiredLoading)
		{
			return Get(out requiredLoading).Day;
		}

		public int GetDayOfYear(out bool requiredLoading)
		{
			return Get(out requiredLoading).DayOfYear;
		}

		public int GetMonth(out bool requiredLoading)
		{
			return Get(out requiredLoading).Month;
		}

		public int GetYear(out bool requiredLoading)
		{
			return Get(out requiredLoading).Year;
		}

		public TimeSpan GetSince(System.DateTime sinceTime)
		{
			return sinceTime - Get();
		}

		public TimeSpan GetSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return sinceTime - Get(out requiredLoading);
		}

		public float GetMillisecondsSince(System.DateTime sinceTime)
		{
			return (float)GetSince(sinceTime).TotalMilliseconds;
		}

		public float GetSecondsSince(System.DateTime sinceTime)
		{
			return (float)GetSince(sinceTime).TotalSeconds;
		}

		public float GetMinutesSince(System.DateTime sinceTime)
		{
			return (float)GetSince(sinceTime).TotalMinutes;
		}

		public float GetHoursSince(System.DateTime sinceTime)
		{
			return (float)GetSince(sinceTime).TotalHours;
		}

		public float GetDaysSince(System.DateTime sinceTime)
		{
			return (float)GetSince(sinceTime).TotalDays;
		}

		public int GetMillisecondsSinceAsInt(System.DateTime sinceTime)
		{
			return (int)GetSince(sinceTime).TotalMilliseconds;
		}

		public int GetSecondsSinceAsInt(System.DateTime sinceTime)
		{
			return (int)GetSince(sinceTime).TotalSeconds;
		}

		public int GetMinutesSinceAsInt(System.DateTime sinceTime)
		{
			return (int)GetSince(sinceTime).TotalMinutes;
		}

		public int GetHoursSinceAsInt(System.DateTime sinceTime)
		{
			return (int)GetSince(sinceTime).TotalHours;
		}

		public int GetDaysSinceAsInt(System.DateTime sinceTime)
		{
			return (int)GetSince(sinceTime).TotalDays;
		}

		public float GetMillisecondsSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (float)GetSince(sinceTime, out requiredLoading).TotalMilliseconds;
		}

		public float GetSecondsSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (float)GetSince(sinceTime, out requiredLoading).TotalSeconds;
		}

		public float GetMinutesSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (float)GetSince(sinceTime, out requiredLoading).TotalMinutes;
		}

		public float GetHoursSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (float)GetSince(sinceTime, out requiredLoading).TotalHours;
		}

		public float GetDaysSince(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (float)GetSince(sinceTime, out requiredLoading).TotalDays;
		}

		public int GetMillisecondsSinceAsInt(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (int)GetSince(sinceTime, out requiredLoading).TotalMilliseconds;
		}

		public int GetSecondsSinceAsInt(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (int)GetSince(sinceTime, out requiredLoading).TotalSeconds;
		}

		public int GetMinutesSinceAsInt(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (int)GetSince(sinceTime, out requiredLoading).TotalMinutes;
		}

		public int GetHoursSinceAsInt(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (int)GetSince(sinceTime, out requiredLoading).TotalHours;
		}

		public int GetDaysSinceAsInt(System.DateTime sinceTime, out bool requiredLoading)
		{
			return (int)GetSince(sinceTime, out requiredLoading).TotalDays;
		}

		public TimeSpan GetSinceNow()
		{
			return GetSince(System.DateTime.UtcNow);
		}

		public TimeSpan GetSinceNow(out bool requiredLoading)
		{
			return GetSince(System.DateTime.UtcNow, out requiredLoading);
		}

		public float GetMillisecondsSinceNow()
		{
			return (float)GetSinceNow().TotalMilliseconds;
		}

		public float GetSecondsSinceNow()
		{
			return (float)GetSinceNow().TotalSeconds;
		}

		public float GetMinutesSinceNow()
		{
			return (float)GetSinceNow().TotalMinutes;
		}

		public float GetHoursSinceNow()
		{
			return (float)GetSinceNow().TotalHours;
		}

		public float GetDaysSinceNow()
		{
			return (float)GetSinceNow().TotalDays;
		}

		public int GetMillisecondsSinceNowAsInt()
		{
			return (int)GetSinceNow().TotalMilliseconds;
		}

		public int GetSecondsSinceNowAsInt()
		{
			return (int)GetSinceNow().TotalSeconds;
		}

		public int GetMinutesSinceNowAsInt()
		{
			return (int)GetSinceNow().TotalMinutes;
		}

		public int GetHoursSinceNowAsInt()
		{
			return (int)GetSinceNow().TotalHours;
		}

		public int GetDaysSinceNowAsInt()
		{
			return (int)GetSinceNow().TotalDays;
		}

		public float GetMillisecondsSinceNow(out bool requiredLoading)
		{
			return (float)GetSinceNow(out requiredLoading).TotalMilliseconds;
		}

		public float GetSecondsSinceNow(out bool requiredLoading)
		{
			return (float)GetSinceNow(out requiredLoading).TotalSeconds;
		}

		public float GetMinutesSinceNow(out bool requiredLoading)
		{
			return (float)GetSinceNow(out requiredLoading).TotalMinutes;
		}

		public float GetHoursSinceNow(out bool requiredLoading)
		{
			return (float)GetSinceNow(out requiredLoading).TotalHours;
		}

		public float GetDaysSinceNow(out bool requiredLoading)
		{
			return (float)GetSinceNow(out requiredLoading).TotalDays;
		}

		public int GetMillisecondsSinceNowAsInt(out bool requiredLoading)
		{
			return (int)GetSinceNow(out requiredLoading).TotalMilliseconds;
		}

		public int GetSecondsSinceNowAsInt(out bool requiredLoading)
		{
			return (int)GetSinceNow(out requiredLoading).TotalSeconds;
		}

		public int GetMinutesSinceNowAsInt(out bool requiredLoading)
		{
			return (int)GetSinceNow(out requiredLoading).TotalMinutes;
		}

		public int GetHourSincesNowAsInt(out bool requiredLoading)
		{
			return (int)GetSinceNow(out requiredLoading).TotalHours;
		}

		public int GetDaySincesNowAsInt(out bool requiredLoading)
		{
			return (int)GetSinceNow(out requiredLoading).TotalDays;
		}

		protected override void WriteToPlayerPrefs()
		{
			string value = localValue.ToString(stringFormat);
			PlayerPrefs.SetString(base.playerPrefsKey, value);
		}

		protected override bool ReadFromPlayerPrefs()
		{
			bool result = false;
			if (PlayerPrefs.HasKey(base.playerPrefsKey))
			{
				string s = PlayerPrefs.GetString(base.playerPrefsKey);
				System.DateTime dateTime = System.DateTime.ParseExact(s, stringFormat, CultureInfo.InvariantCulture);
				localValue = dateTime;
				result = true;
			}
			else if (startingValue.HasValue)
			{
				localValue = startingValue.Value;
				result = true;
			}
			return result;
		}
	}

	public struct Nullable<Type>
	{
		private Type localValue;

		public bool HasValue;

		public Type Value
		{
			get
			{
				if (HasValue)
				{
					return localValue;
				}
				Debug.LogError(string.Format("PSNL: Attempt to get Nullable<{0}>'s value before any value was defined!  Returning {1} instead", typeof(Type).ToString(), default(Type)));
				return default(Type);
			}
			set
			{
				localValue = value;
				if (HasNotValue)
				{
					HasValue = true;
				}
			}
		}

		public bool HasNotValue
		{
			get
			{
				return !HasValue;
			}
			set
			{
				HasValue = !HasNotValue;
			}
		}

		public Nullable(Type startingValue)
		{
			localValue = startingValue;
			HasValue = true;
		}

		public void Nullify()
		{
			HasValue = false;
		}

		public override string ToString()
		{
			if (HasValue)
			{
				return localValue.ToString();
			}
			return "Null";
		}
	}

	private const bool autoSave = true;

	private static ArrayUtils.SmartDict<string, string> allKeys = new ArrayUtils.SmartDict<string, string>();

	private void OnApplicationQuit()
	{
		Save();
	}

	private void OnApplicationPause()
	{
		Save();
	}

	private void OnLevelLoad()
	{
		Save();
	}

	private void Save()
	{
		PlayerPrefs.Save();
	}
}
