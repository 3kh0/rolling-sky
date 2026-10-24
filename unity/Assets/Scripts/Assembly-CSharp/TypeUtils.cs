using System;
using UnityEngine;

public class TypeUtils
{
	public class TypeInterface<Type>
	{
		protected enum StoredType
		{
			Integer = 0,
			IntegerPair = 1,
			Float = 2,
			String = 3,
			Bool = 4,
			Null = 5
		}

		protected StoredType storedType;

		public TypeInterface()
		{
			StoreType();
		}

		protected void StoreType()
		{
			if (typeof(Type) == typeof(float))
			{
				storedType = StoredType.Float;
				return;
			}
			if (typeof(Type) == typeof(int))
			{
				storedType = StoredType.Integer;
				return;
			}
			if (typeof(Type) == typeof(MathUtils.IntPair))
			{
				storedType = StoredType.IntegerPair;
				return;
			}
			if (typeof(Type) == typeof(string))
			{
				storedType = StoredType.String;
				return;
			}
			if (typeof(Type) == typeof(bool))
			{
				storedType = StoredType.Bool;
				return;
			}
			TryDebugError("create");
			storedType = StoredType.Null;
		}

		public Type GetZeroValue()
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(0f);
			case StoredType.Integer:
				return Convert<int, Type>(0);
			case StoredType.IntegerPair:
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(0, 0));
			case StoredType.String:
				return Convert<string, Type>(null);
			case StoredType.Bool:
				return Convert<bool, Type>(false);
			default:
				return default(Type);
			}
		}

		public Type GetOneValue()
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(1f);
			case StoredType.Integer:
				return Convert<int, Type>(1);
			case StoredType.IntegerPair:
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(1, 1));
			case StoredType.String:
				return Convert<string, Type>(null);
			case StoredType.Bool:
				return Convert<bool, Type>(true);
			default:
				return default(Type);
			}
		}

		public Type GetSentinalValue()
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(-1f);
			case StoredType.Integer:
				return Convert<int, Type>(-1);
			case StoredType.IntegerPair:
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(-1, -1));
			case StoredType.String:
				return Convert<string, Type>(null);
			case StoredType.Bool:
				return Convert<bool, Type>(false);
			default:
				return default(Type);
			}
		}

		public Type AbsOf(Type value)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(MathUtils.Abs(Convert<Type, float>(value)));
			case StoredType.Integer:
				return Convert<int, Type>(MathUtils.AbsInt(Convert<Type, int>(value)));
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(value);
				int x = MathUtils.AbsInt(intPair2.x);
				int y = MathUtils.AbsInt(intPair2.y);
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(x, y));
			}
			default:
				return default(Type);
			}
		}

		public Type SumOf(Type firstValue, Type secondValue)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(Convert<Type, float>(firstValue) + Convert<Type, float>(secondValue));
			case StoredType.Integer:
				return Convert<int, Type>(Convert<Type, int>(firstValue) + Convert<Type, int>(secondValue));
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(firstValue);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(secondValue);
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(intPair2.x + intPair3.x, intPair2.y + intPair3.y));
			}
			case StoredType.String:
				return Convert<string, Type>(Convert<Type, string>(firstValue) + Convert<Type, string>(secondValue));
			case StoredType.Bool:
				return Convert<bool, Type>(Convert<Type, bool>(firstValue) && Convert<Type, bool>(secondValue));
			default:
				return default(Type);
			}
		}

		public Type Divide(Type dividee, Type divider)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<float, Type>(Convert<Type, float>(dividee) / Convert<Type, float>(divider));
			case StoredType.Integer:
				return Convert<int, Type>(Convert<Type, int>(dividee) / Convert<Type, int>(divider));
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(dividee);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(divider);
				return Convert<MathUtils.IntPair, Type>(new MathUtils.IntPair(intPair2.x / intPair3.x, intPair2.y / intPair3.y));
			}
			default:
				return default(Type);
			}
		}

		public Type DivideByInt(Type dividee, int divider)
		{
			return Convert<float, Type>(DivideByIntAsFloat(dividee, divider));
		}

		public float DivideByIntAsFloat(Type dividee, int divider)
		{
			float num = divider;
			switch (storedType)
			{
			case StoredType.Integer:
			case StoredType.Float:
				return Convert<Type, float>(dividee) / num;
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(dividee);
				float num2 = intPair2.x;
				float num3 = intPair2.y;
				return (num2 + num3) / (float)divider;
			}
			default:
				return 0f;
			}
		}

		public bool IsSmaller(Type firstValue, Type secondValue)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<Type, float>(firstValue) < Convert<Type, float>(secondValue);
			case StoredType.Integer:
				return Convert<Type, int>(firstValue) < Convert<Type, int>(secondValue);
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(firstValue);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(secondValue);
				return intPair2.x + intPair2.y < intPair3.x + intPair3.y;
			}
			case StoredType.String:
				return Convert<Type, string>(firstValue).Length < Convert<Type, string>(secondValue).Length;
			default:
				return false;
			}
		}

		public bool IsLarger(Type firstValue, Type secondValue)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return Convert<Type, float>(firstValue) > Convert<Type, float>(secondValue);
			case StoredType.Integer:
				return Convert<Type, int>(firstValue) > Convert<Type, int>(secondValue);
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(firstValue);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(secondValue);
				return intPair2.x + intPair2.y > intPair3.x + intPair3.y;
			}
			case StoredType.String:
				return Convert<Type, string>(firstValue).Length > Convert<Type, string>(secondValue).Length;
			default:
				return false;
			}
		}

		public bool IsAbsSmaller(Type firstValue, Type secondValue)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return MathUtils.Abs(Convert<Type, float>(firstValue)) < MathUtils.Abs(Convert<Type, float>(secondValue));
			case StoredType.Integer:
				return MathUtils.AbsInt(Convert<Type, int>(firstValue)) < MathUtils.AbsInt(Convert<Type, int>(secondValue));
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(firstValue);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(secondValue);
				return MathUtils.AbsInt(intPair2.x) + MathUtils.AbsInt(intPair2.y) < MathUtils.AbsInt(intPair3.x) + MathUtils.AbsInt(intPair3.y);
			}
			case StoredType.String:
				return Convert<Type, string>(firstValue).Length < Convert<Type, string>(secondValue).Length;
			default:
				return false;
			}
		}

		public bool IsAbsLarger(Type firstValue, Type secondValue)
		{
			switch (storedType)
			{
			case StoredType.Float:
				return MathUtils.Abs(Convert<Type, float>(firstValue)) > MathUtils.Abs(Convert<Type, float>(secondValue));
			case StoredType.Integer:
				return MathUtils.AbsInt(Convert<Type, int>(firstValue)) > MathUtils.AbsInt(Convert<Type, int>(secondValue));
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(firstValue);
				MathUtils.IntPair intPair3 = Convert<Type, MathUtils.IntPair>(secondValue);
				return MathUtils.AbsInt(intPair2.x) + MathUtils.AbsInt(intPair2.y) > MathUtils.AbsInt(intPair3.x) + MathUtils.AbsInt(intPair3.y);
			}
			case StoredType.String:
				return Convert<Type, string>(firstValue).Length > Convert<Type, string>(secondValue).Length;
			default:
				return false;
			}
		}

		protected virtual void TryDebugError(string debugAction)
		{
			Debug.LogError(string.Format("PCPM: ERROR: Attempt to {0} a NumericInterface of type '{1}'.   However, this specific type of NmericInterface which has not yet been configured", debugAction, typeof(Type).ToString()));
		}
	}

	public class PlayerPrefsInterface<Type> : TypeInterface<Type>
	{
		private const string indexFormat = "D2";

		private string key;

		public Type DefaultValue { get; private set; }

		public PlayerPrefsInterface(string playerPrefsKey)
		{
			ConfigurePlayerPrefsInterface(playerPrefsKey, default(Type));
		}

		public PlayerPrefsInterface(string playerPrefsKey, Type startingValue)
		{
			ConfigurePlayerPrefsInterface(playerPrefsKey, startingValue);
		}

		private void ConfigurePlayerPrefsInterface(string playerPrefsKey, Type startingValue)
		{
			key = playerPrefsKey;
			DefaultValue = startingValue;
		}

		public void Write(Type value)
		{
			WriteToPlayerPrefs(key, value);
		}

		public void Write(Type value, int keyIndex)
		{
			WriteToPlayerPrefs(ToKey(key, keyIndex), value);
		}

		public void Write(Type value, string keyPostfix)
		{
			WriteToPlayerPrefs(ToKey(key, keyPostfix), value);
		}

		public void Write(Type[] values, int firstIndex)
		{
			for (int i = firstIndex; i < values.Length; i++)
			{
				WriteToPlayerPrefs(ToKey(key, i), values[i]);
			}
		}

		public void WriteNew(Type newValue, Type[] oldValues, int index)
		{
			if (!oldValues[index].Equals(newValue))
			{
				Write(newValue, index);
			}
		}

		public void Remove()
		{
			RemoveFromPlayerPrefs(key);
		}

		public void Remove(int keyIndex)
		{
			RemoveFromPlayerPrefs(ToKey(key, keyIndex));
		}

		public void Remove(string keyPostfix)
		{
			RemoveFromPlayerPrefs(ToKey(key, keyPostfix));
		}

		public void RemoveAll(int arrayLength)
		{
			for (int i = 0; i < arrayLength; i++)
			{
				Remove(i);
			}
		}

		public void RemoveAll(Type[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				Remove(i);
			}
		}

		public void RemoveFirst(int totalToRemove)
		{
			RemoveAll(totalToRemove);
		}

		public void RemoveLast(int totalToRemove, int arrayLength)
		{
			for (int i = arrayLength - totalToRemove; i < arrayLength; i++)
			{
				Remove(i);
			}
		}

		public bool CanFind()
		{
			return CanFindInPlayerPrefs(key);
		}

		public bool CanFind(int keyIndex)
		{
			return CanFindInPlayerPrefs(ToKey(key, keyIndex));
		}

		public bool CanFind(string keyPostfix)
		{
			return CanFindInPlayerPrefs(ToKey(key, keyPostfix));
		}

		public bool CanFind(out Type foundValue)
		{
			return CanFindInPlayerPrefs(key, out foundValue);
		}

		public bool CanFind(int keyIndex, out Type foundValue)
		{
			return CanFindInPlayerPrefs(ToKey(key, keyIndex), out foundValue);
		}

		public bool CanFind(string keyPostfix, out Type foundValue)
		{
			return CanFindInPlayerPrefs(ToKey(key, keyPostfix), out foundValue);
		}

		public Type Read()
		{
			return ReadFromPlayerPrefs(key);
		}

		public Type Read(int keyIndex)
		{
			return ReadFromPlayerPrefs(ToKey(key, keyIndex));
		}

		public Type Read(string keyPostfix)
		{
			return ReadFromPlayerPrefs(ToKey(key, keyPostfix));
		}

		public Type[] ReadStartingValues(Type[] array, Type[] startingValues)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Type foundValue;
				if (startingValues == null)
				{
					array[i] = Read(i);
				}
				else if (CanFind(i, out foundValue))
				{
					array[i] = foundValue;
				}
				else if (i < startingValues.Length)
				{
					array[i] = startingValues[i];
				}
				else
				{
					array[i] = DefaultValue;
				}
			}
			return array;
		}

		private void WriteToPlayerPrefs(string playerPrefsKey, Type value)
		{
			switch (storedType)
			{
			case StoredType.Integer:
				PlayerPrefs.SetInt(playerPrefsKey, Convert<Type, int>(value));
				break;
			case StoredType.Float:
				PlayerPrefs.SetFloat(playerPrefsKey, Convert<Type, float>(value));
				break;
			case StoredType.String:
				PlayerPrefs.SetString(playerPrefsKey, Convert<Type, string>(value));
				break;
			case StoredType.Bool:
			{
				int value2 = (Convert<Type, bool>(value) ? 1 : 0);
				PlayerPrefs.SetInt(playerPrefsKey, value2);
				break;
			}
			case StoredType.IntegerPair:
			{
				MathUtils.IntPair intPair2 = Convert<Type, MathUtils.IntPair>(value);
				int x = intPair2.x;
				int y = intPair2.y;
				PlayerPrefs.SetInt(playerPrefsKey + ".x", x);
				PlayerPrefs.SetInt(playerPrefsKey + ".y", y);
				break;
			}
			default:
				TryDebugError("write to");
				break;
			}
		}

		private void RemoveFromPlayerPrefs(string playerPrefsKey)
		{
			if (CanFindInPlayerPrefs(playerPrefsKey))
			{
				PlayerPrefs.DeleteKey(playerPrefsKey);
			}
		}

		private bool CanFindInPlayerPrefs(string playerPrefsKey, out Type foundValue)
		{
			bool wasFound;
			foundValue = ReadFromPlayerPrefs(playerPrefsKey, out wasFound);
			return wasFound;
		}

		private Type ReadFromPlayerPrefs(string playerPrefsKey)
		{
			bool wasFound;
			return ReadFromPlayerPrefs(playerPrefsKey, out wasFound);
		}

		private Type ReadFromPlayerPrefs(string playerPrefsKey, out bool wasFound)
		{
			wasFound = CanFindInPlayerPrefs(playerPrefsKey);
			if (wasFound)
			{
				switch (storedType)
				{
				case StoredType.Integer:
					return Convert<int, Type>(PlayerPrefs.GetInt(playerPrefsKey));
				case StoredType.Float:
					return Convert<float, Type>(PlayerPrefs.GetFloat(playerPrefsKey));
				case StoredType.String:
					return Convert<string, Type>(PlayerPrefs.GetString(playerPrefsKey));
				case StoredType.Bool:
					return Convert<bool, Type>(PlayerPrefs.GetInt(playerPrefsKey) == 1);
				case StoredType.IntegerPair:
				{
					int x = PlayerPrefs.GetInt(playerPrefsKey + ".x");
					int y = PlayerPrefs.GetInt(playerPrefsKey + ".y");
					return ConvertFrom(new MathUtils.IntPair(x, y));
				}
				default:
					TryDebugError("read from");
					return default(Type);
				}
			}
			return DefaultValue;
		}

		private bool CanFindInPlayerPrefs(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		private NewType ConvertTo<NewType>(Type value)
		{
			return Convert<Type, NewType>(value);
		}

		private Type ConvertFrom<OldType>(OldType value)
		{
			return Convert<OldType, Type>(value);
		}

		private string ToKey(string key, string keyPostfix)
		{
			return key + keyPostfix;
		}

		private string ToKey(string key, int keyIndex)
		{
			return ToKey(key, keyIndex.ToString("D2"));
		}

		protected override void TryDebugError(string debugAction)
		{
			Debug.LogError(string.Format("PCPM: ERROR: Attempt to {0} a PlayerPrefsInterface of type '{1}' for Persistant variable with a PlayerPrefs key of '{2}'.   However, this specific type of PlayerPrefsInterface which has not yet been configured", debugAction, typeof(Type).ToString(), key));
		}
	}

	public static NewType Convert<Type, NewType>(Type value)
	{
		return (NewType)System.Convert.ChangeType(value, typeof(NewType));
	}

	public static string ToString<Type>(Type value)
	{
		if (typeof(Type) == typeof(float))
		{
			return Convert<Type, float>(value).ToString();
		}
		if (typeof(Type) == typeof(int))
		{
			return Convert<Type, int>(value).ToString();
		}
		if (typeof(Type) == typeof(MathUtils.IntPair))
		{
			return Convert<Type, MathUtils.IntPair>(value).ToString();
		}
		if (typeof(Type) == typeof(string))
		{
			return Convert<Type, string>(value);
		}
		if (typeof(Type) == typeof(bool))
		{
			return Convert<Type, bool>(value).ToString();
		}
		if (typeof(Type) == typeof(Transform))
		{
			Transform transform = Convert<Type, Transform>(value);
			return (!(transform == null)) ? transform.name : "null";
		}
		if (typeof(Type) == typeof(Component))
		{
			Component component = Convert<Type, Component>(value);
			return (!(component == null)) ? component.transform.name : "null";
		}
		if (typeof(Type) == typeof(Texture))
		{
			Texture texture = Convert<Type, Texture>(value);
			return (!(texture == null)) ? texture.name : "null";
		}
		if (typeof(Type) == typeof(Texture2D))
		{
			Texture texture2 = Convert<Type, Texture2D>(value);
			return (!(texture2 == null)) ? texture2.name : "null";
		}
		if (typeof(Type) == typeof(Texture3D))
		{
			Texture texture3 = Convert<Type, Texture3D>(value);
			return (!(texture3 == null)) ? texture3.name : "null";
		}
		Debug.LogError(string.Format("TYUT: ERROR: Attempt to convert a value of type '{0}' to a string.   However, this specific type of value has not yet been configured for TypeUtils.ToString()", typeof(Type).ToString()));
		return null;
	}
}
