using UnityEngine;

public class PersistantSecured : MonoBehaviour
{
	public abstract class ClassTypePersistantSecured<Type> : Persistant.ClassTypePersistant
	{
		protected Type localValue;

		protected Nullable<Type> startingValue;

		private SecurityString securityString;

		private bool valueChangedSinceSecuring;

		protected void ConfigureSecured(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			startingValue = default(Nullable<Type>);
			securityString = new SecurityString(playerPrefsKey, base.Name);
		}

		protected void ConfigureSecured(string playerPrefsKey, string variableName, string ownerName, Type startingValue)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			this.startingValue = new Nullable<Type>(startingValue);
			securityString = new SecurityString(playerPrefsKey, base.Name);
		}

		public bool Set(Type newValue)
		{
			SetInsecured(newValue);
			return SecureCurrentValue();
		}

		protected virtual void SetInsecured(Type newValue)
		{
			if (IsNotEqualTo(newValue))
			{
				localValue = newValue;
				SaveValue();
			}
		}

		protected bool SecureCurrentValue()
		{
			bool flag = valueChangedSinceSecuring;
			if (flag)
			{
				securityString.SetFor(GetAsIntKey());
				valueChangedSinceSecuring = false;
			}
			return flag;
		}

		public void Append()
		{
			ShiftBy(GetOneValue(), true);
		}

		public void Descend()
		{
			ShiftBy(GetOneValue(), false);
		}

		public void Append(Type ammount)
		{
			ShiftBy(ammount, true);
		}

		public void Descend(Type ammount)
		{
			ShiftBy(ammount, false);
		}

		public Type Get()
		{
			if (IsCurrentValueInsecure())
			{
				int asIntKey = GetAsIntKey();
				int expectedIntKey = GetExpectedIntKey();
				int num = MathUtils.SmallestInt(asIntKey, expectedIntKey);
				Debug.LogError(string.Format("PRST: ERROR: Found unsecured persisted int value of {0} for {1} (expected {2}). Reseting to {3}", asIntKey, base.Name, expectedIntKey, num));
				SetInsecured(FromIntKey(num));
			}
			return GetInsecured();
		}

		protected Type GetInsecured()
		{
			bool requiredLoading;
			return GetInsecured(out requiredLoading);
		}

		protected Type GetInsecured(out bool requiredLoading)
		{
			requiredLoading = TryLoadValue();
			return localValue;
		}

		public float ToFloat()
		{
			GetInsecured();
			return GetValueAsFloat();
		}

		public override string ToString()
		{
			GetInsecured();
			return GetValueAsString();
		}

		public string ToString(string format)
		{
			GetInsecured();
			return GetValueAsString(format);
		}

		public float ToFloat(out bool requiredLoading)
		{
			GetInsecured(out requiredLoading);
			return GetValueAsFloat();
		}

		public string ToString(out bool requiredLoading)
		{
			GetInsecured(out requiredLoading);
			return GetValueAsString();
		}

		public string ToString(string format, out bool requiredLoading)
		{
			GetInsecured(out requiredLoading);
			return GetValueAsString(format);
		}

		public void Clear()
		{
			Reset();
		}

		public bool IsCurrentValueSecure()
		{
			bool securityStringWasEmpty;
			if (securityString.IsFor(GetAsIntKey(), out securityStringWasEmpty))
			{
				return true;
			}
			if (securityStringWasEmpty)
			{
				return IsEqualTo(GetStartingValue());
			}
			return false;
		}

		public bool IsCurrentValueInsecure()
		{
			return !IsCurrentValueSecure();
		}

		private int GetExpectedIntKey()
		{
			if (securityString.IsEmpty())
			{
				return GetStartingValueAsIntKey();
			}
			return securityString.GetAsInt();
		}

		private void ShiftBy(Type ammount, bool ammountIsPositive)
		{
			Type insecured = GetInsecured();
			Set(GetSummed(insecured, ammount, ammountIsPositive));
		}

		private int GetAsIntKey()
		{
			GetInsecured();
			return ToIntKey(localValue);
		}

		private void Reset()
		{
			Set(GetStartingValue());
		}

		private Type GetStartingValue()
		{
			return (!startingValue.HasValue) ? GetInvalidValue() : startingValue.Value;
		}

		private int GetStartingValueAsIntKey()
		{
			return ToIntKey(GetStartingValue());
		}

		protected override void WriteToPlayerPrefs()
		{
			WriteValueToPlayerPrefs();
		}

		protected override bool ReadFromPlayerPrefs()
		{
			if (PlayerPrefs.HasKey(base.playerPrefsKey))
			{
				localValue = ReadValueFromPlayerPrefs();
			}
			else
			{
				Reset();
			}
			return IsNotEqualTo(GetStartingValue());
		}

		protected override void OnValueChanged()
		{
			if (!valueChangedSinceSecuring)
			{
				valueChangedSinceSecuring = true;
			}
		}

		protected abstract Type GetOneValue();

		protected abstract Type GetZeroValue();

		protected abstract Type GetInvalidValue();

		protected abstract float GetValueAsFloat();

		protected abstract string GetValueAsString();

		protected abstract string GetValueAsString(string format);

		protected abstract Type GetSummed(Type firstValue, Type secondValue, bool secondValuePositive);

		protected abstract bool IsEqualTo(Type queryValue);

		protected bool IsNotEqualTo(Type queryValue)
		{
			return !IsEqualTo(queryValue);
		}

		protected abstract void WriteValueToPlayerPrefs();

		protected abstract Type ReadValueFromPlayerPrefs();

		protected abstract int ToIntKey(Type value);

		protected abstract Type FromIntKey(int intKey);
	}

	public class Int : ClassTypePersistantSecured<int>
	{
		private const int oneValue = 1;

		private const int zeroValue = 0;

		private const int invalidValue = -1;

		public Int(string playerPrefsKey)
		{
			ConfigureSecured(playerPrefsKey, null, null);
		}

		public Int(string playerPrefsKey, string variableName)
		{
			ConfigureSecured(playerPrefsKey, variableName, null);
		}

		public Int(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureSecured(playerPrefsKey, variableName, ownerName);
		}

		public Int(string playerPrefsKey, int startingValue)
		{
			ConfigureSecured(playerPrefsKey, null, null, startingValue);
		}

		public Int(string playerPrefsKey, string variableName, int startingValue)
		{
			ConfigureSecured(playerPrefsKey, variableName, null, startingValue);
		}

		public Int(string playerPrefsKey, string variableName, string ownerName, int startingValue)
		{
			ConfigureSecured(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected Int()
		{
		}

		protected override int GetOneValue()
		{
			return 1;
		}

		protected override int GetZeroValue()
		{
			return 0;
		}

		protected override int GetInvalidValue()
		{
			return -1;
		}

		protected override float GetValueAsFloat()
		{
			return localValue;
		}

		protected override string GetValueAsString()
		{
			return localValue.ToString();
		}

		protected override string GetValueAsString(string format)
		{
			return localValue.ToString(format);
		}

		protected override int GetSummed(int firstValue, int secondValue, bool secondValuePositive)
		{
			if (!secondValuePositive)
			{
				secondValue *= -1;
			}
			return firstValue + secondValue;
		}

		protected override bool IsEqualTo(int queryValue)
		{
			return localValue == queryValue;
		}

		protected override void WriteValueToPlayerPrefs()
		{
			PlayerPrefs.SetInt(base.playerPrefsKey, localValue);
		}

		protected override int ReadValueFromPlayerPrefs()
		{
			localValue = PlayerPrefs.GetInt(base.playerPrefsKey);
			return localValue;
		}

		protected override int ToIntKey(int value)
		{
			return value;
		}

		protected override int FromIntKey(int intKey)
		{
			return intKey;
		}
	}

	public class Float : ClassTypePersistantSecured<float>
	{
		private const int DecimalIndexDigits = 2;

		private const float oneValue = 1f;

		private const float zeroValue = 0f;

		private const float invalidValue = -1f;

		private static string DecimalIndexFormat = "D" + 2;

		public Float(string playerPrefsKey)
		{
			ConfigureSecured(playerPrefsKey, null, null);
		}

		public Float(string playerPrefsKey, string variableName)
		{
			ConfigureSecured(playerPrefsKey, variableName, null);
		}

		public Float(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureSecured(playerPrefsKey, variableName, ownerName);
		}

		public Float(string playerPrefsKey, float startingValue)
		{
			ConfigureSecured(playerPrefsKey, null, null, startingValue);
		}

		public Float(string playerPrefsKey, string variableName, float startingValue)
		{
			ConfigureSecured(playerPrefsKey, variableName, null, startingValue);
		}

		public Float(string playerPrefsKey, string variableName, string ownerName, float startingValue)
		{
			ConfigureSecured(playerPrefsKey, variableName, ownerName, startingValue);
		}

		protected Float()
		{
		}

		protected override float GetOneValue()
		{
			return 1f;
		}

		protected override float GetZeroValue()
		{
			return 0f;
		}

		protected override float GetInvalidValue()
		{
			return -1f;
		}

		protected override float GetValueAsFloat()
		{
			return localValue;
		}

		protected override string GetValueAsString()
		{
			return localValue.ToString();
		}

		protected override string GetValueAsString(string format)
		{
			return localValue.ToString(format);
		}

		protected override float GetSummed(float firstValue, float secondValue, bool secondValuePositive)
		{
			if (!secondValuePositive)
			{
				secondValue *= -1f;
			}
			return firstValue + secondValue;
		}

		protected override bool IsEqualTo(float queryValue)
		{
			return localValue == queryValue;
		}

		protected override void WriteValueToPlayerPrefs()
		{
			PlayerPrefs.SetFloat(base.playerPrefsKey, localValue);
		}

		protected override float ReadValueFromPlayerPrefs()
		{
			localValue = PlayerPrefs.GetFloat(base.playerPrefsKey);
			return localValue;
		}

		protected override int ToIntKey(float value)
		{
			string text = value.ToString();
			string[] array = text.Split('.');
			string text2 = array[0];
			int num;
			if (array.Length == 1)
			{
				num = 0;
			}
			else
			{
				num = array[0].Length;
				text2 += array[1];
			}
			string text3 = num.ToString(DecimalIndexFormat);
			text2 += text3;
			return int.Parse(text2);
		}

		protected override float FromIntKey(int intKey)
		{
			string text = intKey.ToString();
			string s = text.Substring(text.Length - 2, 2);
			int num = int.Parse(s);
			if (num == 0)
			{
				string s2 = text.Substring(0, text.Length - 2);
				return float.Parse(s2);
			}
			string text2 = text.Substring(0, num);
			string text3 = text.Substring(num, text.Length - (2 + num));
			string s3 = text2 + '.' + text3;
			return float.Parse(s3);
		}
	}

	public class IntRanged : Int
	{
		private MathUtils.RangeInt range;

		public IntRanged(string playerPrefsKey, int minimum, int maximum)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, null, null, null);
		}

		public IntRanged(string playerPrefsKey, int minimum, int maximum, string variableName)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, variableName, null, null);
		}

		public IntRanged(string playerPrefsKey, int minimum, int maximum, string variableName, string ownerName)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, variableName, ownerName, null);
		}

		public IntRanged(string playerPrefsKey, int minimum, int maximum, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, null, null, startingValue);
		}

		public IntRanged(string playerPrefsKey, int minimum, int maximum, string variableName, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, variableName, null, startingValue);
		}

		public IntRanged(string playerPrefsKey, int minimum, int maximum, string variableName, string ownerName, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, minimum, maximum, variableName, ownerName, startingValue);
		}

		protected IntRanged()
		{
		}

		protected void ConfigureIntRangedSecured(string playerPrefsKey, int minimum, int maximum, string variableName, string ownerName, int? startingValue)
		{
			range = new MathUtils.RangeInt(minimum, maximum);
			if (startingValue.HasValue)
			{
				ConfigureSecured(playerPrefsKey, variableName, ownerName, startingValue.Value);
			}
			else
			{
				ConfigureSecured(playerPrefsKey, variableName, ownerName, minimum);
			}
		}

		protected override void SetInsecured(int newValue)
		{
			bool wasBelow;
			if (range.ContainsNot(newValue, out wasBelow))
			{
				newValue = ((!wasBelow) ? range.Max : range.Min);
			}
			if (IsNotEqualTo(newValue))
			{
				localValue = newValue;
				SaveValue();
			}
		}

		public bool SetToMin()
		{
			return Set(range.Min);
		}

		public bool SetToMax()
		{
			return Set(range.Max);
		}

		public float GetAsPercent()
		{
			return range.ToPercent(Get());
		}

		public virtual int GetAsPercentageInt()
		{
			return MathUtils.ToPercentageInt(GetAsPercent());
		}

		public bool IsMinimum()
		{
			return Get() == range.Min;
		}

		public bool IsMaximum()
		{
			return Get() == range.Max;
		}

		public bool IsNotMinimum()
		{
			return !IsMinimum();
		}

		public bool IsNotMaximum()
		{
			return !IsMaximum();
		}

		public bool IsBelowPercent(float percent)
		{
			return IsBelowPercent(percent, true);
		}

		public bool IsBelowPercent(float percent, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercent() < percent;
			}
			return GetAsPercent() <= percent;
		}

		public bool IsAbovePercent(float percent)
		{
			return IsAbovePercent(percent, true);
		}

		public bool IsAbovePercent(float percent, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercent() > percent;
			}
			return GetAsPercent() >= percent;
		}

		public bool IsBelowPercentageInt(int percentage)
		{
			return IsBelowPercentageInt(percentage, true);
		}

		public bool IsBelowPercentageInt(int percentage, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercentageInt() < percentage;
			}
			return GetAsPercentageInt() <= percentage;
		}

		public bool IsAbovePercentageInt(int percentage)
		{
			return IsAbovePercentageInt(percentage, true);
		}

		public bool IsAbovePercentageInt(int percentage, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercentageInt() > percentage;
			}
			return GetAsPercentageInt() >= percentage;
		}
	}

	public class IntPercentage : IntRanged
	{
		private const int minimum = 0;

		private const int maximum = 100;

		public IntPercentage(string playerPrefsKey)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, null, null, null);
		}

		public IntPercentage(string playerPrefsKey, string variableName)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, variableName, null, null);
		}

		public IntPercentage(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, variableName, ownerName, null);
		}

		public IntPercentage(string playerPrefsKey, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, null, null, startingValue);
		}

		public IntPercentage(string playerPrefsKey, string variableName, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, variableName, null, startingValue);
		}

		public IntPercentage(string playerPrefsKey, string variableName, string ownerName, int startingValue)
		{
			ConfigureIntRangedSecured(playerPrefsKey, 0, 100, variableName, ownerName, startingValue);
		}

		public override int GetAsPercentageInt()
		{
			return Get();
		}
	}

	public class FloatRanged : Float
	{
		private MathUtils.Range range;

		public FloatRanged(string playerPrefsKey, float minimum, float maximum)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, null, null, null);
		}

		public FloatRanged(string playerPrefsKey, float minimum, float maximum, string variableName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, variableName, null, null);
		}

		public FloatRanged(string playerPrefsKey, float minimum, float maximum, string variableName, string ownerName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, variableName, ownerName, null);
		}

		public FloatRanged(string playerPrefsKey, float minimum, float maximum, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, null, null, startingValue);
		}

		public FloatRanged(string playerPrefsKey, float minimum, float maximum, string variableName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, variableName, null, startingValue);
		}

		public FloatRanged(string playerPrefsKey, float minimum, float maximum, string variableName, string ownerName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, minimum, maximum, variableName, ownerName, startingValue);
		}

		protected FloatRanged()
		{
		}

		protected void ConfigureFloatRangedSecured(string playerPrefsKey, float minimum, float maximum, string variableName, string ownerName, float? startingValue)
		{
			range = new MathUtils.Range(minimum, maximum);
			if (startingValue.HasValue)
			{
				ConfigureSecured(playerPrefsKey, variableName, ownerName, startingValue.Value);
			}
			else
			{
				ConfigureSecured(playerPrefsKey, variableName, ownerName, minimum);
			}
		}

		protected override void SetInsecured(float newValue)
		{
			bool wasBelow;
			if (range.ContainsNot(newValue, out wasBelow))
			{
				newValue = ((!wasBelow) ? range.Max : range.Min);
			}
			if (IsNotEqualTo(newValue))
			{
				localValue = newValue;
				SaveValue();
			}
		}

		public virtual float GetAsPercent()
		{
			return range.ToPercent(Get());
		}

		public int GetAsPercentageInt()
		{
			return MathUtils.ToPercentageInt(GetAsPercent());
		}

		public bool IsMinimum()
		{
			return Get() == range.Min;
		}

		public bool IsMaximum()
		{
			return Get() == range.Max;
		}

		public bool IsBelowPercent(float percent)
		{
			return IsBelowPercent(percent, true);
		}

		public bool IsBelowPercent(float percent, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercent() < percent;
			}
			return GetAsPercent() <= percent;
		}

		public bool IsAbovePercent(float percent)
		{
			return IsAbovePercent(percent, true);
		}

		public bool IsAbovePercent(float percent, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercent() > percent;
			}
			return GetAsPercent() >= percent;
		}

		public bool IsBelowPercentageInt(int percentage)
		{
			return IsBelowPercentageInt(percentage, true);
		}

		public bool IsBelowPercentageInt(int percentage, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercentageInt() < percentage;
			}
			return GetAsPercentageInt() <= percentage;
		}

		public bool IsAbovePercentageInt(int percentage)
		{
			return IsAbovePercentageInt(percentage, true);
		}

		public bool IsAbovePercentageInt(int percentage, bool exclusive)
		{
			if (exclusive)
			{
				return GetAsPercentageInt() > percentage;
			}
			return GetAsPercentageInt() >= percentage;
		}
	}

	public class Float01 : FloatRanged
	{
		private const float minimum = 0f;

		private const float maximum = 1f;

		private const float oneValue = 0.01f;

		public Float01(string playerPrefsKey)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, null, null, null);
		}

		public Float01(string playerPrefsKey, string variableName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, null, null);
		}

		public Float01(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, ownerName, null);
		}

		public Float01(string playerPrefsKey, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, null, null, startingValue);
		}

		public Float01(string playerPrefsKey, string variableName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, null, startingValue);
		}

		public Float01(string playerPrefsKey, string variableName, string ownerName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, ownerName, startingValue);
		}

		public Float01(string playerPrefsKey, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, null, null, (float)startingValue);
		}

		public Float01(string playerPrefsKey, string variableName, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, null, (float)startingValue);
		}

		public Float01(string playerPrefsKey, string variableName, string ownerName, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, 0f, 1f, variableName, ownerName, (float)startingValue);
		}

		public override float GetAsPercent()
		{
			return Get();
		}

		protected override float GetOneValue()
		{
			return 0.01f;
		}
	}

	public class Float11 : FloatRanged
	{
		private const float minimum = -1f;

		private const float maximum = 1f;

		private const float oneValue = 0.01f;

		public Float11(string playerPrefsKey)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, null, null, null);
		}

		public Float11(string playerPrefsKey, string variableName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, null, null);
		}

		public Float11(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, ownerName, null);
		}

		public Float11(string playerPrefsKey, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, null, null, startingValue);
		}

		public Float11(string playerPrefsKey, string variableName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, null, startingValue);
		}

		public Float11(string playerPrefsKey, string variableName, string ownerName, float startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, ownerName, startingValue);
		}

		public Float11(string playerPrefsKey, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, null, null, (float)startingValue);
		}

		public Float11(string playerPrefsKey, string variableName, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, null, (float)startingValue);
		}

		public Float11(string playerPrefsKey, string variableName, string ownerName, double startingValue)
		{
			ConfigureFloatRangedSecured(playerPrefsKey, -1f, 1f, variableName, ownerName, (float)startingValue);
		}

		protected override float GetOneValue()
		{
			return 0.01f;
		}
	}

	private class SecurityString : Persistant.String
	{
		private const int divisionUnit = 93;

		private const int additionUnit = 33;

		private const string securityStringKeyFormat = "S{0}K";

		private const string securityStringName = "securityString";

		public SecurityString(string ownerPlayerPrefsKey, string ownerName)
		{
			string text = null;
			for (int i = 0; i < ownerPlayerPrefsKey.Length; i++)
			{
				text += ownerPlayerPrefsKey[ownerPlayerPrefsKey.Length - (i + 1)];
			}
			text = string.Format("S{0}K", text);
			ConfigurePersistant(text, "securityString", ownerName);
		}

		public string GetFor(int value)
		{
			return CalculateSecurityStringFor(value);
		}

		public int GetAsInt()
		{
			return ReverseSecurityString(Get());
		}

		public void SetFor(int value)
		{
			Set(CalculateSecurityStringFor(value));
		}

		public bool IsFor(int queryValue, out bool securityStringWasEmpty)
		{
			string text = Get();
			securityStringWasEmpty = string.IsNullOrEmpty(text);
			if (securityStringWasEmpty)
			{
				return false;
			}
			string value = CalculateSecurityStringFor(queryValue);
			return text.Equals(value);
		}

		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(Get());
		}

		private static string CalculateSecurityStringFor(int value)
		{
			string text = null;
			int i;
			for (i = 1; MathUtils.PowerInt(93, i) < (float)value; i++)
			{
			}
			int[] array = new int[i];
			int resultSansRemainder = value;
			for (int j = 0; j < i; j++)
			{
				array[j] = MathUtils.RemainderInt(resultSansRemainder, 93, out resultSansRemainder);
				text = ((text != null) ? (text + array[j]) : array[j].ToString());
			}
			string text2 = null;
			for (int k = 0; k < array.Length; k++)
			{
				char c = (char)(array[k] + 33);
				text2 += c;
			}
			return text2;
		}

		private static int ReverseSecurityString(string securityString)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(securityString))
			{
				for (int i = 0; i < securityString.Length; i++)
				{
					int num2 = securityString[i];
					num2 -= 33;
					num2 *= (int)MathUtils.PowerInt(93, i);
					num += num2;
				}
			}
			return num;
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
}
