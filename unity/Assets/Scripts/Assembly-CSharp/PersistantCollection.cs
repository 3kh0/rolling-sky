using UnityEngine;

public class PersistantCollection : MonoBehaviour
{
	public class Array<Type> : ArrayUtils.Array<Type>
	{
		private const string classCode = "PTAR";

		private const string className = "Persistant Array";

		private const bool startFull = true;

		private TypeUtils.PlayerPrefsInterface<Type> playerPrefsInterface;

		public Array(string playerPrefsKey, int length)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeSmartCollection(length, true, "PTAR", "Persistant Array");
		}

		public Array(string playerPrefsKey, params Type[] startingValues)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeSmartCollection(startingValues, true, "PTAR", "Persistant Array");
		}

		protected override void OnInitialize(Type[] startingValues)
		{
			values = playerPrefsInterface.ReadStartingValues(values, startingValues);
		}

		protected override void PreChange(int index, Type value)
		{
			playerPrefsInterface.WriteNew(value, values, index);
		}

		protected override void PreClear(bool removeEntries)
		{
			if (removeEntries)
			{
				playerPrefsInterface.RemoveAll(values);
			}
		}

		protected override void OnContract(int indexRemoved, int totalRemoved)
		{
			playerPrefsInterface.RemoveLast(totalRemoved, values.Length);
		}
	}

	public class List<Type> : ArrayUtils.List<Type>
	{
		private const string classCode = "PTLT";

		private const string className = "Persistant List";

		private const bool startFull = false;

		private TypeUtils.PlayerPrefsInterface<Type> playerPrefsInterface;

		public List(string playerPrefsKey)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeSmartCollection(false, "PTLT", "Persistant List");
		}

		public List(string playerPrefsKey, int startingSize)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeSmartCollection(startingSize, false, "PTLT", "Persistant List");
		}

		public List(string playerPrefsKey, params Type[] startingValues)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeSmartCollection(startingValues, false, "PTLT", "Persistant List");
		}

		protected override void OnInitialize(Type[] startingValues)
		{
			values = playerPrefsInterface.ReadStartingValues(values, startingValues);
		}

		protected override void PreChange(int index, Type value)
		{
			playerPrefsInterface.WriteNew(value, values, index);
		}

		protected override void PreClear(bool removeEntries)
		{
			if (removeEntries)
			{
				playerPrefsInterface.RemoveAll(values);
			}
		}

		protected override void OnContract(int indexRemoved, int totalRemoved)
		{
			playerPrefsInterface.RemoveLast(totalRemoved, values.Length);
		}
	}

	public class Buffer<Type> : ArrayUtils.Buffer<Type>
	{
		private const string classCode = "PTBF";

		private const string className = "Persistant Buffer";

		private const bool startFull = false;

		private TypeUtils.PlayerPrefsInterface<Type> playerPrefsInterface;

		public Buffer(string playerPrefsKey, int startingSize)
		{
			playerPrefsInterface = new TypeUtils.PlayerPrefsInterface<Type>(playerPrefsKey);
			InitializeBuffer(startingSize);
		}

		protected override void OnInitialize(Type[] startingValues)
		{
			values = playerPrefsInterface.ReadStartingValues(values, startingValues);
		}

		protected override void PreChange(int index, Type value)
		{
			playerPrefsInterface.WriteNew(value, values, index);
		}

		protected override void PreClear(bool removeEntries)
		{
			if (removeEntries)
			{
				playerPrefsInterface.RemoveAll(values);
			}
		}

		protected override void OnContract(int indexRemoved, int totalRemoved)
		{
			playerPrefsInterface.RemoveLast(totalRemoved, values.Length);
		}
	}

	public abstract class ClassTypePersistantList<Type> : Persistant.ClassTypePersistant
	{
		protected Type[] localValues;

		private bool autoSort;

		private bool sorted;

		private int? maxLength;

		public int Length { get; protected set; }

		public Type this[int index]
		{
			get
			{
				return Get(index);
			}
			set
			{
				Set(index, value);
			}
		}

		protected void ConfigureList(string playerPrefsKey, string variableName, string ownerName, bool autoSort, int? maxLength)
		{
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			this.autoSort = autoSort;
			this.maxLength = maxLength;
			if (autoSort && maxLength.HasValue)
			{
				Debug.LogError("PSPL: ERROR: Attempt to create an auto-sorted Persistant List WITH a maximum size - these two functions are incompatible.  Check logic that allowed the creation of this invalid listtype");
			}
		}

		public void Set(Type[] newValues)
		{
			Set(newValues, false);
		}

		private void Set(Type[] newValues, bool forceSet)
		{
			TryLoadValue();
			if (forceSet || IsArrayNotEqualTo(newValues))
			{
				localValues = new Type[newValues.Length];
				for (int i = 0; i < newValues.Length; i++)
				{
					localValues[i] = newValues[i];
				}
				Length = ((localValues != null) ? localValues.Length : 0);
				SortAndSave();
			}
		}

		public void Set(int index, Type newValue)
		{
			CanSet(index, newValue);
		}

		public bool CanSet(int index, Type newValue)
		{
			TryLoadValue();
			if (IsValidIndex(ref index))
			{
				if (IsValueNotEqualTo(newValue, index))
				{
					localValues[index] = newValue;
					SortAndSave();
				}
				return true;
			}
			return false;
		}

		public bool CannotSet(int index, Type newValue)
		{
			return !CanSet(index, newValue);
		}

		public Type Append(int index)
		{
			return Append(index, GetOneValue());
		}

		public Type Append(int index, Type appendAmmount)
		{
			Type appendedValue;
			CanAppend(index, out appendedValue);
			return appendedValue;
		}

		public bool CanAppend(int index)
		{
			return CanAppend(index, GetOneValue());
		}

		public bool CanAppend(int index, Type appendAmmount)
		{
			Type appendedValue;
			return CanAppend(index, appendAmmount, out appendedValue);
		}

		public bool CanAppend(int index, out Type appendedValue)
		{
			return CanAppend(index, GetOneValue(), out appendedValue);
		}

		public bool CanAppend(int index, Type appendAmmount, out Type appendedValue)
		{
			TryLoadValue();
			bool result;
			if (IsValidIndex(ref index))
			{
				appendedValue = AppendAndGetValue(index, appendAmmount);
				SortAndSave();
				result = true;
			}
			else
			{
				Debug.Log("!4: " + index);
				result = false;
				appendedValue = GetInvalidValue();
			}
			return result;
		}

		public bool CannotAppend(int index)
		{
			return !CanAppend(index);
		}

		public bool CannotAppend(int index, Type appendAmmount)
		{
			return !CanAppend(index, appendAmmount);
		}

		public bool CannotAppend(int index, out Type appendedValue)
		{
			return !CanAppend(index, out appendedValue);
		}

		public bool CannotAppend(int index, Type appendAmmount, out Type appendedValue)
		{
			return !CanAppend(index, appendAmmount, out appendedValue);
		}

		public void Add(Type newValue)
		{
			TryLoadValue();
			Type[] array;
			if (localValues == null)
			{
				array = new Type[1] { newValue };
			}
			else
			{
				int num = Length + 1;
				int num2 = 0;
				if (maxLength.HasValue && num > maxLength.Value)
				{
					num2 = num - maxLength.Value;
					num = maxLength.Value;
				}
				array = new Type[num];
				int num3 = num - 1;
				for (int i = 0; i < num3; i++)
				{
					array[i] = localValues[i + num2];
				}
				array[num3] = newValue;
			}
			Set(array);
		}

		public void AddNew(Type newValue)
		{
			CanAddNew(newValue);
		}

		public bool CanAddNew(Type newValue)
		{
			if (Contains(newValue))
			{
				return false;
			}
			Add(newValue);
			return true;
		}

		public bool CannotAddNew(Type newValue)
		{
			return !CanAddNew(newValue);
		}

		public void Sort()
		{
			Sort(true);
		}

		public void Sort(bool sortAscending)
		{
			if (sorted || !IsNotEmpty() || Length <= 1)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < Length - 1; i++)
			{
				for (int j = 0; j < Length - 1; j++)
				{
					if (sortAscending ^ IsSmallerThanNext(j))
					{
						Type val = localValues[j];
						localValues[j] = localValues[j + 1];
						localValues[j + 1] = val;
					}
					flag = true;
				}
			}
			sorted = true;
			if (flag)
			{
				SaveValue();
			}
		}

		public void Clear()
		{
			if (IsNotEmpty())
			{
				for (int i = 0; i < Length; i++)
				{
					PlayerPrefs.DeleteKey(GetPlayerPrefsEntryNameFor(i));
				}
				localValues = null;
				Length = 0;
				sorted = false;
				PlayerPrefs.DeleteKey(base.playerPrefsKey);
			}
		}

		public Type[] Get()
		{
			bool requiredLoading;
			return Get(out requiredLoading);
		}

		public Type[] Get(out bool requiredLoading)
		{
			requiredLoading = TryLoadValue();
			return localValues;
		}

		public Type Get(int index)
		{
			bool requiredLoading;
			return Get(index, out requiredLoading);
		}

		public Type Get(int index, out bool requiredLoading)
		{
			TryLoadValue();
			Type result;
			if (IsValidIndex(ref index))
			{
				result = localValues[index];
				requiredLoading = true;
			}
			else
			{
				result = GetInvalidValue();
				requiredLoading = false;
			}
			return result;
		}

		public Type GetLast()
		{
			return Get(-1);
		}

		public Type GetLast(out bool requiredLoading)
		{
			return Get(-1, out requiredLoading);
		}

		public float GetAverage()
		{
			float num = 0f;
			if (IsNotEmpty())
			{
				for (int i = 0; i < Length; i++)
				{
					num += GetAsFloat(i);
				}
				num /= (float)Length;
			}
			return num;
		}

		public int GetAverageAsInt()
		{
			return (int)GetAverage();
		}

		public Type GetSum()
		{
			Type val = GetZeroValue();
			if (IsNotEmpty())
			{
				for (int i = 0; i < Length; i++)
				{
					val = GetSummed(val, i);
				}
			}
			return val;
		}

		public bool Contains(Type queryValue)
		{
			int foundAtIndex;
			return Contains(queryValue, out foundAtIndex);
		}

		public bool Contains(Type queryValue, out int foundAtIndex)
		{
			TryLoadValue();
			bool result = false;
			foundAtIndex = -1;
			if (IsNotEmpty())
			{
				for (int i = 0; i < Length; i++)
				{
					if (IsValueEqualTo(queryValue, i))
					{
						result = true;
						foundAtIndex = i;
						break;
					}
				}
			}
			return result;
		}

		public bool ContainsNot(Type queryValue)
		{
			return !Contains(queryValue);
		}

		public bool ContainsNot(Type queryValue, out int foundAtIndex)
		{
			return !Contains(queryValue, out foundAtIndex);
		}

		public bool ContainsNew(Type queryValue)
		{
			int foundAtIndex;
			return ContainsNew(queryValue, out foundAtIndex);
		}

		public bool ContainsNew(Type queryValue, out int foundAtIndex)
		{
			bool flag = Contains(queryValue, out foundAtIndex);
			if (!flag)
			{
				Add(queryValue);
			}
			return flag;
		}

		public bool ContainsNotNew(Type queryValue)
		{
			return !ContainsNew(queryValue);
		}

		public bool ContainsNotNew(Type queryValue, out int foundAtIndex)
		{
			return !ContainsNew(queryValue, out foundAtIndex);
		}

		public bool IsEmpty()
		{
			TryLoadValue();
			return localValues == null;
		}

		public bool IsNotEmpty()
		{
			return !IsEmpty();
		}

		public string GetAsString()
		{
			return ConvertToString();
		}

		public override string ToString()
		{
			return ConvertToString();
		}

		public string GetAsString(int valueIndex)
		{
			return ConvertToString(valueIndex);
		}

		public string ToString(int valueIndex)
		{
			return ConvertToString(valueIndex);
		}

		public void SortAndSave()
		{
			sorted = false;
			if (autoSort)
			{
				Sort();
			}
			SaveValue();
		}

		protected override void WriteToPlayerPrefs()
		{
			if (localValues != null)
			{
				for (int i = 0; i < Length; i++)
				{
					WriteValueToPlayerPrefs(GetPlayerPrefsEntryNameFor(i), i);
				}
				PlayerPrefs.SetInt(base.playerPrefsKey, Length);
			}
		}

		protected override bool ReadFromPlayerPrefs()
		{
			if (PlayerPrefs.HasKey(base.playerPrefsKey))
			{
				Length = PlayerPrefs.GetInt(base.playerPrefsKey);
				if (Length > 0)
				{
					if (localValues == null || localValues.Length != Length)
					{
						localValues = new Type[Length];
					}
					for (int i = 0; i < Length; i++)
					{
						ReadValueFromPlayerPrefs(GetPlayerPrefsEntryNameFor(i), i);
					}
				}
				return true;
			}
			return false;
		}

		private bool IsValidIndex(ref int queryIndex)
		{
			if (IsEmpty())
			{
				return false;
			}
			if (queryIndex < 0)
			{
				queryIndex = Length + queryIndex;
			}
			return IsValidIndex(queryIndex);
		}

		private bool IsValidIndex(int queryIndex)
		{
			return queryIndex >= 0 && queryIndex < Length;
		}

		private string GetPlayerPrefsEntryNameFor(int index)
		{
			return string.Format("{0}[{1}]", base.playerPrefsKey, index);
		}

		private string ConvertToString(int? valueIndex = null)
		{
			TryLoadValue();
			string text;
			if (localValues == null)
			{
				text = "Null";
			}
			else if (valueIndex.HasValue)
			{
				text = ((!IsValidIndex(valueIndex.Value)) ? "Invalid" : ValueToString(valueIndex.Value));
			}
			else if (Length == 0)
			{
				text = "Empty";
			}
			else
			{
				text = ValueToString(0);
				for (int i = 1; i < Length; i++)
				{
					text = text + ", " + ValueToString(i);
				}
			}
			return text;
		}

		private bool IsArrayNotEqualTo(Type[] queryValues)
		{
			return !IsArrayEqualTo(queryValues);
		}

		private bool IsArrayEqualTo(Type[] queryValues)
		{
			bool result;
			if (queryValues == null && localValues == null)
			{
				result = true;
			}
			else if (queryValues.Length != Length)
			{
				result = false;
			}
			else
			{
				result = true;
				for (int i = 0; i < queryValues.Length; i++)
				{
					if (IsValueNotEqualTo(queryValues[i], i))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		protected abstract Type GetOneValue();

		protected abstract Type GetZeroValue();

		protected abstract Type GetInvalidValue();

		protected abstract Type GetSummed(Type firstValue, int indexOfSecondValue);

		protected abstract Type AppendAndGetValue(int index, Type appendAmmount);

		protected abstract float GetAsFloat(int index);

		protected abstract bool IsValueEqualTo(Type queryValue, int index);

		private bool IsValueNotEqualTo(Type queryValue, int index)
		{
			return !IsValueEqualTo(queryValue, index);
		}

		protected abstract bool IsGreaterThanNext(int index);

		private bool IsSmallerThanNext(int index)
		{
			return !IsGreaterThanNext(index);
		}

		protected abstract void WriteValueToPlayerPrefs(string playerPrefsValueName, int index);

		protected abstract void ReadValueFromPlayerPrefs(string playerPrefsValueName, int index);

		protected abstract string ValueToString(int index, string foramt = null);
	}

	public class IntList : ClassTypePersistantList<int>
	{
		private const int oneValue = 1;

		private const int zeroValue = 0;

		private const int invalidValue = -1;

		public IntList(string playerPrefsKey)
		{
			ConfigureList(playerPrefsKey, null, null, false, null);
		}

		public IntList(string playerPrefsKey, string variableName)
		{
			ConfigureList(playerPrefsKey, variableName, null, false, null);
		}

		public IntList(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureList(playerPrefsKey, variableName, ownerName, false, null);
		}

		public IntList(string playerPrefsKey, bool autoSort)
		{
			ConfigureList(playerPrefsKey, null, null, autoSort, null);
		}

		public IntList(string playerPrefsKey, string variableName, bool autoSort)
		{
			ConfigureList(playerPrefsKey, variableName, null, autoSort, null);
		}

		public IntList(string playerPrefsKey, string variableName, string ownerName, bool autoSort)
		{
			ConfigureList(playerPrefsKey, variableName, ownerName, autoSort, null);
		}

		public IntList(string playerPrefsKey, int maxLength)
		{
			ConfigureList(playerPrefsKey, null, null, false, maxLength);
		}

		public IntList(string playerPrefsKey, string variableName, int maxLength)
		{
			ConfigureList(playerPrefsKey, variableName, null, false, maxLength);
		}

		public IntList(string playerPrefsKey, string variableName, string ownerName, int maxLength)
		{
			ConfigureList(playerPrefsKey, variableName, ownerName, false, maxLength);
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

		protected override float GetAsFloat(int index)
		{
			return localValues[index];
		}

		protected override int GetSummed(int firstValue, int indexOfSecondValue)
		{
			return firstValue + localValues[indexOfSecondValue];
		}

		protected override int AppendAndGetValue(int index, int appendAmmount)
		{
			localValues[index] += appendAmmount;
			return localValues[index];
		}

		protected override bool IsValueEqualTo(int queryValue, int index)
		{
			return queryValue == localValues[index];
		}

		protected override bool IsGreaterThanNext(int index)
		{
			return localValues[index] > localValues[index + 1];
		}

		protected override string ValueToString(int index, string format = null)
		{
			return localValues[index].ToString();
		}

		protected override void WriteValueToPlayerPrefs(string playerPrefsValueName, int index)
		{
			PlayerPrefs.SetInt(playerPrefsValueName, localValues[index]);
		}

		protected override void ReadValueFromPlayerPrefs(string playerPrefsValueName, int index)
		{
			localValues[index] = PlayerPrefs.GetInt(playerPrefsValueName);
		}
	}

	public abstract class ClassTypePersistantDict<KeyType, ValueType> : Persistant.ClassTypePersistant
	{
		private const string keysSuffix = "Keys";

		private const string valuesSuffix = "Values";

		private const string stringFormat = "{0}:{1}";

		private bool autoSort;

		private bool sorted;

		private ValueType defaultValue;

		private ValueType oneValue;

		public int Length
		{
			get
			{
				return GetStoredKeys().Length;
			}
		}

		public ValueType this[KeyType key]
		{
			get
			{
				return Get(key);
			}
			set
			{
				Set(key, value);
			}
		}

		protected void ConfigureDict(string playerPrefsKey, string variableName, string ownerName, bool autoSort, ValueType defaultValue, ValueType oneValue)
		{
			if (string.IsNullOrEmpty(variableName))
			{
				variableName = playerPrefsKey;
			}
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			string keysPlayerPrefsKey = playerPrefsKey + "Keys";
			string valuesPlayerPrefsKey = playerPrefsKey + "Values";
			string keysVariableName = variableName + "'s Keys";
			string valuesVariableName = variableName + "'s Values";
			CreateKeysAndValues(keysPlayerPrefsKey, keysVariableName, valuesPlayerPrefsKey, valuesVariableName);
			this.autoSort = autoSort;
			this.defaultValue = defaultValue;
			this.oneValue = oneValue;
		}

		public void Set(KeyType key, ValueType newValue)
		{
			int foundAtIndex;
			if (Contains(key, out foundAtIndex))
			{
				if (IsValueEqualTo(GetStoredValues()[foundAtIndex], newValue))
				{
					SetStoredValue(foundAtIndex, newValue);
				}
			}
			else
			{
				AddKeyValueAndSort(key, newValue);
			}
		}

		public void Add(KeyType newKey, ValueType newValue)
		{
			if (CannotAdd(newKey, newValue))
			{
				Debug.LogError(string.Format("PSID: ERROR: Attempted to add duplicate key of {0} to Persistant.IntDict {1}\nCurrent values are: {2}", newKey, base.Name, ToString()));
			}
		}

		public bool CannotAdd(KeyType newKey, ValueType newValue)
		{
			return !CanAdd(newKey, newValue);
		}

		public bool CanAdd(KeyType newKey, ValueType newValue)
		{
			if (ContainsNot(newKey))
			{
				AddKeyValueAndSort(newKey, newValue);
				return true;
			}
			return false;
		}

		public ValueType Append(KeyType key)
		{
			return Append(key, oneValue);
		}

		public ValueType Append(KeyType key, ValueType appendAmmount)
		{
			int foundAtIndex;
			if (Contains(key, out foundAtIndex))
			{
				return AppendStoredValue(foundAtIndex, appendAmmount);
			}
			AddKeyValueAndSort(key, appendAmmount);
			return appendAmmount;
		}

		public void SortKeys()
		{
			SortKeys(true);
		}

		public void SortKeys(bool sortAscending)
		{
			if (sorted || !IsNotEmpty() || Length <= 1)
			{
				return;
			}
			KeyType[] array = new KeyType[Length];
			ValueType[] array2 = new ValueType[Length];
			array = GetStoredKeys();
			array2 = GetStoredValues();
			for (int i = 0; i < Length - 1; i++)
			{
				for (int j = 0; j < Length - 1; j++)
				{
					if (sortAscending ^ IsKeyLesserThan(array[j], array[j + 1]))
					{
						KeyType val = array[j];
						array[j] = array[j + 1];
						array[j + 1] = val;
						ValueType val2 = array2[j];
						array2[j] = array2[j + 1];
						array2[j + 1] = val2;
					}
				}
			}
			sorted = true;
			SetStoredKeys(array);
			SetStoredValues(array2);
		}

		public void Clear()
		{
			ClearStored();
		}

		public KeyType[] GetKeys()
		{
			return GetStoredKeys();
		}

		public ValueType[] GetValues()
		{
			return GetStoredValues();
		}

		public ValueType Get(KeyType key)
		{
			bool requiredLoading;
			return Get(key, out requiredLoading);
		}

		public ValueType Get(KeyType key, out bool requiredLoading)
		{
			TryLoadValue();
			int foundAtIndex;
			ValueType result;
			if (Contains(key, out foundAtIndex))
			{
				result = GetStoredValues()[foundAtIndex];
				requiredLoading = true;
			}
			else
			{
				result = defaultValue;
				requiredLoading = false;
			}
			return result;
		}

		public KeyType GetKeyByIndex(int index)
		{
			return GetStoredKeys()[index];
		}

		public ValueType GetValueByIndex(int index)
		{
			return GetStoredValues()[index];
		}

		public bool Contains(KeyType queryKey)
		{
			int foundAtIndex;
			return ContainsStoredKey(queryKey, out foundAtIndex);
		}

		public bool ContainsValue(ValueType queryValue)
		{
			int foundAtIndex;
			return ContainsStoredValue(queryValue, out foundAtIndex);
		}

		public bool ContainsNot(KeyType queryKey)
		{
			int foundAtIndex;
			return !ContainsStoredKey(queryKey, out foundAtIndex);
		}

		public bool ContainsNotValue(ValueType queryValue)
		{
			int foundAtIndex;
			return !ContainsStoredValue(queryValue, out foundAtIndex);
		}

		public bool Contains(KeyType queryKey, out int foundAtIndex)
		{
			return ContainsStoredKey(queryKey, out foundAtIndex);
		}

		public bool ContainsValue(ValueType queryValue, out int foundAtIndex)
		{
			return ContainsStoredValue(queryValue, out foundAtIndex);
		}

		public bool ContainsNot(KeyType queryKey, out int foundAtIndex)
		{
			return !ContainsStoredKey(queryKey, out foundAtIndex);
		}

		public bool ContainsNotValue(ValueType queryValue, out int foundAtIndex)
		{
			return !ContainsStoredValue(queryValue, out foundAtIndex);
		}

		public bool IsEmpty()
		{
			return Length > 0;
		}

		public bool IsNotEmpty()
		{
			return !IsEmpty();
		}

		public string GetAsString()
		{
			return ConvertToString();
		}

		public override string ToString()
		{
			return ConvertToString();
		}

		public string GetAsString(int index)
		{
			return ConvertToString(index);
		}

		public string ToString(int index)
		{
			return ConvertToString(index);
		}

		private void AddKeyValueAndSort(KeyType newKey, ValueType newValue)
		{
			AddKeyValue(newKey, newValue);
			sorted = false;
			if (autoSort)
			{
				SortKeys();
			}
		}

		private bool IsValidIndex(int queryIndex)
		{
			return IsNotEmpty() && queryIndex >= 0 && queryIndex < Length;
		}

		private string ConvertToString(int? valueIndex = null)
		{
			string text;
			if (valueIndex.HasValue)
			{
				text = ((!IsValidIndex(valueIndex.Value)) ? "Invalid" : KeyValueAsString(valueIndex.Value));
			}
			else if (Length == 0)
			{
				text = "Empty";
			}
			else
			{
				text = KeyValueAsString(0);
				for (int i = 1; i < Length; i++)
				{
					text = text + ", " + KeyValueAsString(i);
				}
			}
			return text;
		}

		private string KeyValueAsString(int preVerifiedIndex)
		{
			return string.Format("{0}:{1}", GetStoredKeys()[preVerifiedIndex], GetStoredValues()[preVerifiedIndex]);
		}

		protected abstract void CreateKeysAndValues(string keysPlayerPrefsKey, string keysVariableName, string valuesPlayerPrefsKey, string valuesVariableName);

		protected abstract KeyType[] GetStoredKeys();

		protected abstract ValueType[] GetStoredValues();

		protected abstract void SetStoredKeys(KeyType[] newKeys);

		protected abstract void SetStoredValues(ValueType[] newValues);

		protected abstract void SetStoredValue(int index, ValueType newValue);

		protected abstract ValueType AppendStoredValue(int index, ValueType appendAmmount);

		protected abstract bool ContainsStoredKey(KeyType queryKey, out int foundAtIndex);

		protected abstract bool ContainsStoredValue(ValueType queryValue, out int foundAtIndex);

		protected abstract void AddKeyValue(KeyType newKey, ValueType newValue);

		protected abstract void ClearStored();

		protected abstract bool IsKeyLesserThan(KeyType queryKey, KeyType comparissonKey);

		protected abstract bool IsValueEqualTo(ValueType queryValue, ValueType comparissonValue);

		protected override void WriteToPlayerPrefs()
		{
		}

		protected override bool ReadFromPlayerPrefs()
		{
			return false;
		}
	}

	public class IntDict : ClassTypePersistantDict<int, int>
	{
		private const int defaultDefaultValue = 0;

		private const int oneValue = 1;

		private IntList keys;

		private IntList values;

		public IntDict(string playerPrefsKey)
		{
			ConfigureDict(playerPrefsKey, null, null, false, 0, 1);
		}

		public IntDict(string playerPrefsKey, string variableName)
		{
			ConfigureDict(playerPrefsKey, variableName, null, false, 0, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureDict(playerPrefsKey, variableName, ownerName, false, 0, 1);
		}

		public IntDict(string playerPrefsKey, bool autoSort)
		{
			ConfigureDict(playerPrefsKey, null, null, autoSort, 0, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, bool autoSort)
		{
			ConfigureDict(playerPrefsKey, variableName, null, autoSort, 0, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, string ownerName, bool autoSort)
		{
			ConfigureDict(playerPrefsKey, variableName, ownerName, autoSort, 0, 1);
		}

		public IntDict(string playerPrefsKey, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, null, null, false, defaultValue, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, variableName, null, false, defaultValue, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, string ownerName, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, variableName, ownerName, false, defaultValue, 1);
		}

		public IntDict(string playerPrefsKey, bool autoSort, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, null, null, autoSort, defaultValue, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, bool autoSort, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, variableName, null, autoSort, defaultValue, 1);
		}

		public IntDict(string playerPrefsKey, string variableName, string ownerName, bool autoSort, int defaultValue)
		{
			ConfigureDict(playerPrefsKey, variableName, ownerName, autoSort, defaultValue, 1);
		}

		protected override void CreateKeysAndValues(string keysPlayerPrefsKey, string keysVariableName, string valuesPlayerPrefsKey, string valuesVariableName)
		{
			keys = new IntList(keysPlayerPrefsKey);
			values = new IntList(valuesPlayerPrefsKey);
		}

		protected override void SetStoredKeys(int[] newKeys)
		{
			keys.Set(newKeys);
		}

		protected override void SetStoredValues(int[] newValues)
		{
			values.Set(newValues);
		}

		protected override void SetStoredValue(int index, int newValue)
		{
			values[index] = newValue;
		}

		protected override int AppendStoredValue(int index, int appendAmmount)
		{
			return values.Append(index, appendAmmount);
		}

		protected override void AddKeyValue(int newKey, int newValue)
		{
			keys.Add(newKey);
			values.Add(newValue);
		}

		protected override void ClearStored()
		{
			keys.Clear();
			values.Clear();
		}

		protected override int[] GetStoredKeys()
		{
			return keys.Get();
		}

		protected override int[] GetStoredValues()
		{
			return values.Get();
		}

		protected override bool ContainsStoredKey(int queryKey, out int foundAtIndex)
		{
			return keys.Contains(queryKey, out foundAtIndex);
		}

		protected override bool ContainsStoredValue(int queryValue, out int foundAtIndex)
		{
			return values.Contains(queryValue, out foundAtIndex);
		}

		protected override bool IsKeyLesserThan(int queryValue, int comparissonValue)
		{
			return queryValue < comparissonValue;
		}

		protected override bool IsValueEqualTo(int queryValue, int comparissonValue)
		{
			return queryValue == comparissonValue;
		}
	}

	public class RunningValue : Persistant.ClassTypePersistant
	{
		private const int invalidValue = -1;

		private const int defaultStartingSum = 0;

		private const int defaultStartingCount = 0;

		private const string defaultFormat = "0.0";

		private const string sumSuffix = "Sum";

		private const string countSuffix = "Count";

		private Persistant.Int sum;

		private Persistant.Int count;

		protected int startingSum;

		protected int startingCount;

		public RunningValue(string playerPrefsKey)
		{
			ConfigureFloat(playerPrefsKey, null, null, null, null);
		}

		public RunningValue(string playerPrefsKey, int startingSum)
		{
			ConfigureFloat(playerPrefsKey, null, null, startingSum, null);
		}

		public RunningValue(string playerPrefsKey, string variableName)
		{
			ConfigureFloat(playerPrefsKey, variableName, null, null, null);
		}

		public RunningValue(string playerPrefsKey, string variableName, int startingSum)
		{
			ConfigureFloat(playerPrefsKey, variableName, null, startingSum, null);
		}

		public RunningValue(string playerPrefsKey, string variableName, string ownerName)
		{
			ConfigureFloat(playerPrefsKey, variableName, ownerName, null, null);
		}

		public RunningValue(string playerPrefsKey, string variableName, string ownerName, int startingSum)
		{
			ConfigureFloat(playerPrefsKey, variableName, ownerName, startingSum, null);
		}

		public RunningValue(string playerPrefsKey, int startingSum, int startingCount)
		{
			ConfigureFloat(playerPrefsKey, null, null, startingSum, startingCount);
		}

		public RunningValue(string playerPrefsKey, string variableName, int startingSum, int startingCount)
		{
			ConfigureFloat(playerPrefsKey, variableName, null, startingSum, startingCount);
		}

		public RunningValue(string playerPrefsKey, string variableName, string ownerName, int startingSum, int startingCount)
		{
			ConfigureFloat(playerPrefsKey, variableName, ownerName, startingSum, startingCount);
		}

		protected RunningValue()
		{
		}

		private void ConfigureFloat(string playerPrefsKey, string variableName, string ownerName, int? startingSum, int? startingCount)
		{
			if (string.IsNullOrEmpty(variableName))
			{
				variableName = playerPrefsKey;
			}
			ConfigurePersistant(playerPrefsKey, variableName, ownerName);
			string text = playerPrefsKey + "Sum";
			string text2 = playerPrefsKey + "Count";
			string variableName2 = variableName + "'s Sum";
			string variableName3 = variableName + "'s Count";
			this.startingSum = (startingSum.HasValue ? startingSum.Value : 0);
			this.startingCount = (startingCount.HasValue ? startingCount.Value : 0);
			sum = new Persistant.Int(text, variableName2, ownerName, this.startingSum);
			count = new Persistant.Int(text2, variableName3, ownerName, this.startingCount);
		}

		public void Append(int value)
		{
			Append(value, 1);
		}

		public void Append(int value, int countForValue)
		{
			sum.Append(value);
			count.Append(countForValue);
		}

		public void Clear()
		{
			Set(startingSum, startingCount);
		}

		public int GetSum()
		{
			return sum.Get();
		}

		public int GetSum(out bool requiredLoading)
		{
			return sum.Get(out requiredLoading);
		}

		public float GetSumAsFloat()
		{
			return sum.ToFloat();
		}

		public float GetSumAsFloat(out bool requiredLoading)
		{
			return sum.ToFloat(out requiredLoading);
		}

		public string GetSumAsString()
		{
			return sum.ToString();
		}

		public string GetSumAsString(out bool requiredLoading)
		{
			return sum.ToString(out requiredLoading);
		}

		public int GetCount()
		{
			return count.Get();
		}

		public int GetCount(out bool requiredLoading)
		{
			return count.Get(out requiredLoading);
		}

		public float GetCountAsFloat()
		{
			return count.ToFloat();
		}

		public float GetCountAsFloat(out bool requiredLoading)
		{
			return count.ToFloat(out requiredLoading);
		}

		public string GetCountAsString()
		{
			return count.ToString();
		}

		public string GetCountAsString(out bool requiredLoading)
		{
			return count.ToString(out requiredLoading);
		}

		public float GetAverage()
		{
			bool requiredLoading;
			return GetAverage(out requiredLoading);
		}

		public float GetAverage(out bool requiredLoading)
		{
			bool requiredLoading2;
			int num = sum.Get(out requiredLoading2);
			bool requiredLoading3;
			float result;
			if (count.Get(out requiredLoading3) == 0)
			{
				if (num == 0)
				{
					result = 0f;
				}
				else
				{
					Debug.LogError(string.Format("PSRV: ERROR: Error in Persistant.RunningValue.GetAverage - somehow count had a value of 0, while sum had a value of {0}.  Recheck logic to see how this could have happened.  Returning max-float-value average", num));
					result = float.MaxValue;
				}
			}
			else
			{
				result = sum.ToFloat(out requiredLoading2) / count.ToFloat(out requiredLoading3);
			}
			requiredLoading = requiredLoading2 || requiredLoading3;
			return result;
		}

		public int GetAverageAsInt()
		{
			return (int)GetAverage();
		}

		public int GetAverageAsInt(out bool requiredLoading)
		{
			return (int)GetAverage(out requiredLoading);
		}

		public string GetAverageAsString()
		{
			return GetAverage().ToString("0.0");
		}

		public string GetAverageAsString(out bool requiredLoading)
		{
			return GetAverage(out requiredLoading).ToString("0.0");
		}

		public string GetAverageAsString(string format)
		{
			return GetAverage().ToString(format);
		}

		public string GetAverageAsString(out bool requiredLoading, string format)
		{
			return GetAverage(out requiredLoading).ToString(format);
		}

		private void Set(int newTotal, int newCount)
		{
			sum.Set(newTotal);
			count.Set(newCount);
		}

		protected override void WriteToPlayerPrefs()
		{
		}

		protected override bool ReadFromPlayerPrefs()
		{
			return true;
		}
	}
}
