using System.Runtime.InteropServices;
using UnityEngine;

public class ArrayUtils
{
	public class Array<Type> : ClassTypeSmartCollectionLight<Type>
	{
		private const string classCode = "ARAY";

		private const string className = "Array";

		private const bool startFull = true;

		public Array(int length)
		{
			InitializeSmartCollection(length, true, "ARAY", "Array");
		}

		public Array(params Type[] startingValues)
		{
			InitializeSmartCollection(startingValues, true, "ARAY", "Array");
		}

		protected Array()
		{
		}
	}

	public class List<Type> : ClassTypeSmartCollection<Type>
	{
		private const string classCode = "LIST";

		private const string className = "List";

		private const bool startFull = false;

		public List()
		{
			InitializeSmartCollection(false, "LIST", "List");
		}

		public List(int startingSize)
		{
			InitializeSmartCollection(startingSize, false, "LIST", "List");
		}

		public List(params Type[] startingValues)
		{
			InitializeSmartCollection(startingValues, false, "LIST", "List");
		}
	}

	public class Buffer<Type> : ClassTypeSmartCollectionNumeric<Type>
	{
		private const string classCode = "BUFF";

		private const string className = "Buffer";

		private const bool startFull = false;

		public Buffer(int startingSize)
		{
			InitializeBuffer(startingSize);
		}

		protected Buffer()
		{
		}

		protected void InitializeBuffer(int startingSize)
		{
			InitializeSmartCollection(startingSize, false, "BUFF", "Buffer");
		}

		protected override bool ShouldExtend()
		{
			for (int i = 1; i < values.Length; i++)
			{
				values[i - 1] = values[i];
			}
			return false;
		}
	}

	public class MultiArray<Type> : ClassTypeMultiCollection<Type>
	{
		private const string classCode = "MDAR";

		private const string className = "Multi-Array";

		private const bool startFull = true;

		public MultiArray(params int[] dimensions)
		{
			InitializeSmartMultiCollection(dimensions, true, "MDAR", "Multi-Array");
		}

		public MultiArray(MathUtils.IntPair dimensions)
		{
			InitializeSmartMultiCollection(dimensions, true, "MDAR", "Multi-Array");
		}

		public MultiArray(MathUtils.IntTrio dimensions)
		{
			InitializeSmartMultiCollection(dimensions, true, "MDAR", "Multi-Array");
		}
	}

	public class BooleanedArray<Type> : ClassTypeBooleanedCollectionLight<Type>
	{
		private const string classCode = "BLAR";

		private const string className = "Booleaned Array";

		private const bool startFull = true;

		public BooleanedArray(int length)
		{
			InitializeParallelCollection(length, true, "BLAR", "Booleaned Array");
		}

		public BooleanedArray(params Type[] startingValues)
		{
			InitializeParallelCollection(startingValues, true, "BLAR", "Booleaned Array");
		}
	}

	public class BooleanedList<Type> : ClassTypeBooleanedCollection<Type>
	{
		private const string classCode = "BLLI";

		private const string className = "Booleaned List";

		private const bool startFull = false;

		public BooleanedList()
		{
			InitializeParallelCollection(false, "BLLI", "Booleaned List");
		}

		public BooleanedList(int startingSize)
		{
			InitializeParallelCollection(startingSize, false, "BLLI", "Booleaned List");
		}

		public BooleanedList(params Type[] startingValues)
		{
			InitializeParallelCollection(startingValues, false, "BLLI", "Booleaned List");
		}
	}

	public class BooleanedMultiArray<Type> : ClassTypeBooleanedMultiCollection<Type>
	{
		private const string classCode = "BLMA";

		private const string className = "Booleaned Multi-Dimensional Array";

		private const bool startFull = true;

		public BooleanedMultiArray(params int[] dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "BLMA", "Booleaned Multi-Dimensional Array");
		}

		public BooleanedMultiArray(MathUtils.IntPair dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "BLMA", "Booleaned Multi-Dimensional Array");
		}

		public BooleanedMultiArray(MathUtils.IntTrio dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "BLMA", "Booleaned Multi-Dimensional Array");
		}
	}

	public class TrinitiedMultiArray<Type> : ClassTypeTrinitiedMultiCollection<Type>
	{
		private const string classCode = "TRMA";

		private const string className = "Trinitied Multi-Dimensional Array";

		private const bool startFull = true;

		public TrinitiedMultiArray(params int[] dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "TRMA", "Trinitied Multi-Dimensional Array");
		}

		public TrinitiedMultiArray(MathUtils.IntPair dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "TRMA", "Trinitied Multi-Dimensional Array");
		}

		public TrinitiedMultiArray(MathUtils.IntTrio dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "TRMA", "Trinitied Multi-Dimensional Array");
		}
	}

	public class QuadedMultiArray<Type> : ClassTypeQuadedMultiCollection<Type>
	{
		private const string classCode = "QDMA";

		private const string className = "Quaded Multi-Dimensional Array";

		private const bool startFull = true;

		public QuadedMultiArray(params int[] dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "QDMA", "Quaded Multi-Dimensional Array");
		}

		public QuadedMultiArray(MathUtils.IntPair dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "QDMA", "Quaded Multi-Dimensional Array");
		}

		public QuadedMultiArray(MathUtils.IntTrio dimensions)
		{
			InitializeParallelMultiCollection(dimensions, true, "QDMA", "Quaded Multi-Dimensional Array");
		}
	}

	public class WeightedList<Type> : ClassTypeWeightedCollection<Type>
	{
		private const string classCode = "WTLT";

		private const string className = "Weighted List";

		private const bool startFull = false;

		public WeightedList()
		{
			InitializeSmartCollection(false, "WTLT", "Weighted List");
		}

		public WeightedList(int startingSize)
		{
			InitializeSmartCollection(startingSize, false, "WTLT", "Weighted List");
		}

		public WeightedList(Type[] startingValues)
		{
			InitializeSmartCollection(false, "WTLT", "Weighted List");
			SmartAdd(startingValues);
		}
	}

	public class WeightedListRandom<Type> : ClassTypeWeightedCollection<Type>
	{
		private const string classCode = "WTLR";

		private const string className = "Weighted List Random";

		private const bool startFull = false;

		private Buffer<int> previous;

		private int? previousSize;

		public WeightedListRandom()
		{
			InitializeSmartCollection(false, "WTLR", "Weighted List Random");
			previousSize = null;
		}

		public WeightedListRandom(int startingSize)
		{
			InitializeSmartCollection(startingSize, false, "WTLR", "Weighted List Random");
			previousSize = null;
		}

		public WeightedListRandom(int startingSize, int previousSize)
		{
			InitializeSmartCollection(startingSize, false, "WTLR", "Weighted List Random");
			this.previousSize = previousSize;
		}

		public WeightedListRandom(Type[] startingValues)
		{
			InitializeSmartCollection(false, "WTLR", "Weighted List Random");
			previousSize = null;
			SmartAdd(startingValues);
		}

		public void ClearPrevious()
		{
			previous.Clear();
		}

		public Type GetRandom()
		{
			int indexOfEntry;
			return GetRandom(out indexOfEntry);
		}

		public Type GetRandom(out int indexOfEntry)
		{
			Type val;
			if (base.IsEmpty)
			{
				val = default(Type);
				indexOfEntry = -1;
				Debug.LogError(string.Format("ARUT.WTRN: ERROR: Attempt to GetRandom() from WeightedListRandom BEFORE any entries were added.  Returning default {0} value of '{1}' instead", typeof(Type), val));
			}
			else
			{
				val = GetRandomPreverified(out indexOfEntry);
			}
			return val;
		}

		public int GetRandomIndex()
		{
			int result;
			if (base.IsEmpty)
			{
				result = -1;
				Debug.LogError("ARUT.WTRN: ERROR: Attempt to GetRandomIndex() from WeightedListRandom BEFORE any entries were added.  Returning index of -1 instead");
			}
			else
			{
				result = GetRandomIndexPreverified();
			}
			return result;
		}

		public Type GetRandomNext()
		{
			Type val;
			if (base.IsEmpty)
			{
				val = default(Type);
				Debug.LogError(string.Format("ARUT.WTRN: ERROR: Attempt to GetRandomNext() from WeightedListRandom BEFORE any entries were added.  Returning default {0} value of '{1}' instead", typeof(Type), val));
			}
			else
			{
				bool flag = false;
				int num = 0;
				int value = 0;
				for (int i = 0; i < indexList.Length; i++)
				{
					num = GetRandomIndex();
					value = indexList[num];
					if (!previous.Contains(value))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int j = 1; j < indexList.Length; j++)
					{
						int index = MathUtils.Indexed(num + j, indexList.Length);
						value = indexList[index];
						if (!previous.Contains(value))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Debug.LogWarning(string.Format("ARUT.WTRN: ERROR: Unable to GetRandomNext() entry for WeightedListRandom, as there are apparently NO available entries that have not recently been returned\n  Total entries: {0}\n  Total previous tracked indexes: {1}\n  Entries: {2}\n  Previously tracked indexes: {3}", base.LengthReal, previous.Length, ToString(true), previous.ToString()));
					num = GetRandomIndex();
					value = indexList[num];
				}
				val = base[num];
				previous.Add(value);
			}
			return val;
		}

		private int GetRandomIndexPreverified()
		{
			return Randomizer.GetRandomIndex(indexList.Length);
		}

		private int GetRandomIndexPreverified(out int trueIndex)
		{
			int randomIndex = Randomizer.GetRandomIndex(indexList.Length);
			trueIndex = indexList[randomIndex];
			return randomIndex;
		}

		private Type GetRandomPreverified()
		{
			return base[GetRandomIndexPreverified()];
		}

		private Type GetRandomPreverified(out int indexOfEntry)
		{
			indexOfEntry = GetRandomIndexPreverified();
			return base[indexOfEntry];
		}

		private Type GetRandomPreverified(out int indexOfEntry, out int trueIndexOfEntry)
		{
			indexOfEntry = GetRandomIndexPreverified();
			trueIndexOfEntry = indexList[indexOfEntry];
			return base[indexOfEntry];
		}

		protected override void OnInitialize(Type[] startingValues)
		{
			indexList = new List<int>();
			if (!previousSize.HasValue)
			{
				previousSize = MathUtils.HalfIntCeiled(base.startingSize);
			}
			previous = new Buffer<int>(previousSize.Value);
		}
	}

	public abstract class ClassTypeSmartCollectionLight<Type>
	{
		protected enum Function
		{
			Get = 0,
			GetKey = 1,
			Set = 2,
			SetKey = 3,
			Insert = 4
		}

		protected const string defaultNumericToStringFormat = "0.00f";

		protected const int defaultStartingSize = 1;

		protected bool isInitialized;

		protected Type[] values;

		protected string ClassCode { get; private set; }

		protected string ClassName { get; private set; }

		protected bool StartFull { get; private set; }

		protected bool isNotInitialized
		{
			get
			{
				return !isInitialized;
			}
		}

		public bool IsEmpty { get; protected set; }

		public bool IsFull { get; protected set; }

		public bool IsNotEmpty
		{
			get
			{
				return !IsEmpty;
			}
		}

		public bool IsNotFull
		{
			get
			{
				return !IsFull;
			}
		}

		public int LastIndex
		{
			get
			{
				return length - 1;
			}
		}

		public virtual int Length
		{
			get
			{
				return length;
			}
		}

		protected int length { get; private set; }

		protected int startingSize { get; private set; }

		protected int size
		{
			get
			{
				return values.Length;
			}
		}

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

		public Type First
		{
			get
			{
				return values[0];
			}
		}

		public Type Last
		{
			get
			{
				return values[length - 1];
			}
		}

		protected void InitializeSmartCollection(bool startFull, string classCode, string className)
		{
			InitializeSmartCollection(1, null, startFull, classCode, className);
		}

		protected void InitializeSmartCollection(int startingSize, bool startFull, string classCode, string className)
		{
			InitializeSmartCollection(startingSize, null, startFull, classCode, className);
		}

		protected void InitializeSmartCollection(Type[] startingValues, bool startFull, string classCode, string className)
		{
			InitializeSmartCollection(1, startingValues, startFull, classCode, className);
		}

		protected void InitializeSmartCollection(int startingSize, Type[] startingValues, bool startFull, string classCode, string className)
		{
			ClassCode = classCode;
			ClassName = className;
			StartFull = startFull;
			EnactClear(false);
			if (startingValues != null)
			{
				this.startingSize = MathUtils.MaxInt(startingSize, startingValues.Length);
				InitializeWith(startingValues);
				return;
			}
			this.startingSize = startingSize;
			if (StartFull)
			{
				Initialize();
			}
		}

		protected void TryInitialize()
		{
			if (isNotInitialized)
			{
				Initialize();
			}
		}

		private void Initialize()
		{
			if (startingSize <= 0)
			{
				startingSize = 1;
			}
			values = new Type[startingSize];
			if (StartFull)
			{
				length = startingSize;
				IsEmpty = false;
				IsFull = true;
			}
			else
			{
				length = 0;
				IsEmpty = true;
				IsFull = false;
			}
			isInitialized = true;
			OnInitialize(null);
		}

		private void InitializeWith(Type[] startingValues)
		{
			values = new Type[startingSize];
			for (int i = 0; i < startingValues.Length; i++)
			{
				values[i] = startingValues[i];
			}
			if (StartFull)
			{
				length = startingSize;
				IsEmpty = false;
				IsFull = true;
			}
			else
			{
				length = startingValues.Length;
				IsEmpty = length != 0;
				IsFull = length >= startingSize;
			}
			isInitialized = true;
			OnInitialize(startingValues);
		}

		public void Clear()
		{
			EnactClear(true);
		}

		public void Reset()
		{
			EnactClear(false);
		}

		protected void EnactClear(bool removeEntries)
		{
			PreClear(removeEntries);
			values = null;
			isInitialized = false;
			IsEmpty = true;
			IsFull = false;
			length = 0;
			OnClear(removeEntries);
		}

		public void Remove(Type value)
		{
			int foundAtIndex;
			if (Contains(value, out foundAtIndex))
			{
				EnactContract(foundAtIndex);
			}
		}

		public void RemoveAt(int index)
		{
			if (IsValid(index))
			{
				EnactContract(index);
			}
		}

		public void RemoveAt(int index, int totalToRemove)
		{
			if (IsValid(index))
			{
				EnactContract(index, totalToRemove);
			}
		}

		public void RemoveLast()
		{
			if (isInitialized)
			{
				EnactContract(length - 1);
			}
		}

		public void RemoveLast(int totalEntriesToRemove)
		{
			if (isInitialized)
			{
				if (totalEntriesToRemove >= length)
				{
					EnactClear(true);
				}
				else
				{
					EnactContract(length - totalEntriesToRemove, totalEntriesToRemove);
				}
			}
		}

		public void RemoveFirst()
		{
			if (isInitialized)
			{
				EnactContract(0);
			}
		}

		public void RemoveFirst(int totalEntriesToRemove)
		{
			if (isInitialized)
			{
				if (totalEntriesToRemove >= length)
				{
					EnactClear(true);
				}
				else
				{
					EnactContract(0, totalEntriesToRemove);
				}
			}
		}

		public void RemoveConsequetive(Type value)
		{
			int firstIndex;
			int totalConsequetiveFound;
			if (ContainsConsequetive(value, out firstIndex, out totalConsequetiveFound))
			{
				RemoveAt(firstIndex, totalConsequetiveFound);
			}
		}

		public void FillWith(Type value)
		{
			if (isInitialized)
			{
				int num = MathUtils.MaxInt(startingSize, values.Length);
				if (length < num)
				{
					length = num;
				}
			}
			else
			{
				Initialize();
				length = startingSize;
			}
			for (int i = 0; i < length; i++)
			{
				values[i] = value;
			}
			IsEmpty = false;
			IsFull = true;
		}

		public bool Contains(Type value)
		{
			int foundAtIndex;
			return Contains(value, out foundAtIndex);
		}

		public bool Contains(Type value, out int foundAtIndex)
		{
			bool result = false;
			foundAtIndex = -1;
			if (IsNotEmpty)
			{
				for (int i = 0; i < length; i++)
				{
					if (values[i].Equals(value))
					{
						foundAtIndex = i;
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public bool ContainsNot(Type value)
		{
			return !Contains(value);
		}

		public bool ContainsNot(Type value, out int foundAtIndex)
		{
			return !Contains(value, out foundAtIndex);
		}

		public bool ContainsAll(params Type[] values)
		{
			bool result;
			if (values != null && values.Length > 0)
			{
				result = true;
				for (int i = 0; i < values.Length; i++)
				{
					bool flag = true;
					for (int j = 0; j < length; j++)
					{
						if (values[i].Equals(values[j]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool ContainsNotAll(Type[] values)
		{
			return !ContainsAll(values);
		}

		public bool ContainsAny(params Type[] values)
		{
			bool flag = false;
			if (values != null && values.Length > 0)
			{
				for (int i = 0; i < values.Length; i++)
				{
					for (int j = 0; j < length; j++)
					{
						if (values[i].Equals(values[j]))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		public bool ContainsNotAny(Type[] values)
		{
			return !ContainsNotAny(values);
		}

		public bool ContainsIndex(int index)
		{
			return isInitialized && index < length;
		}

		public bool ContainsNotIndex(int index)
		{
			return !ContainsIndex(index);
		}

		public bool Contains(Type value, out int firstIndex, out int totalFound)
		{
			totalFound = TotalConsequetiveOf(value, out firstIndex);
			return totalFound != 0;
		}

		public bool ContainsNot(Type value, out int firstIndex, out int totalFound)
		{
			return !Contains(value, out firstIndex, out totalFound);
		}

		public bool ContainsConsequetive(Type value, out int firstIndex, out int totalConsequetiveFound)
		{
			totalConsequetiveFound = TotalOf(value, out firstIndex);
			return totalConsequetiveFound != 0;
		}

		public bool ContainsNotConsequetive(Type value, out int firstIndex, out int totalConsequetiveFound)
		{
			return !Contains(value, out firstIndex, out totalConsequetiveFound);
		}

		public void Extend()
		{
			TryEnactExtend();
		}

		public int TotalOf(Type value)
		{
			int firstIndex;
			return TotalOf(value, out firstIndex);
		}

		public int TotalOf(Type value, out int firstIndex)
		{
			return CountTotalOf(value, false, out firstIndex);
		}

		public int TotalConsequetiveOf(Type value)
		{
			int firstIndex;
			return TotalConsequetiveOf(value, out firstIndex);
		}

		public int TotalConsequetiveOf(Type value, out int firstIndex)
		{
			return CountTotalOf(value, true, out firstIndex);
		}

		public int IndexOf(Type value)
		{
			int foundAtIndex;
			Contains(value, out foundAtIndex);
			return foundAtIndex;
		}

		public Type[] ToArray()
		{
			if (isInitialized)
			{
				return values;
			}
			return null;
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public virtual string ToString(string entryFormat)
		{
			return ArrayToString(entryFormat, true);
		}

		protected Type Get(int index)
		{
			TryInitialize();
			AdjustIndex(ref index);
			if (IsValid(index))
			{
				return values[index];
			}
			Debug.LogError(ErrorString(index, Function.Get));
			return default(Type);
		}

		protected void Set(int index, Type value)
		{
			TryInitialize();
			AdjustIndex(ref index);
			if (IsValid(index))
			{
				PreChange(index, value);
				values[index] = value;
				OnChange(index, value);
				if (IsEmpty)
				{
					IsEmpty = false;
				}
			}
			else
			{
				Debug.LogError(ErrorString(index, Function.Set, value));
			}
		}

		protected void TryEnactExtend(int? indexToInsert = null)
		{
			bool flag = true;
			if (isInitialized)
			{
				if (length >= values.Length)
				{
					if (ShouldExtend())
					{
						Type[] array = new Type[values.Length + 1];
						int num = 0;
						for (int i = 0; i < values.Length; i++)
						{
							if (indexToInsert.HasValue && i == indexToInsert.Value)
							{
								num = 1;
							}
							array[i + num] = values[i];
						}
						values = array;
						OnExtend(indexToInsert);
					}
					else
					{
						flag = false;
					}
				}
			}
			else
			{
				Initialize();
			}
			if (flag)
			{
				length++;
				IsFull = length == values.Length;
			}
			if (IsEmpty)
			{
				IsEmpty = false;
			}
		}

		protected void EnactContract(int indexToRemove, int totalToRemove = 1)
		{
			if (!isInitialized)
			{
				return;
			}
			if (length <= totalToRemove)
			{
				EnactClear(true);
			}
			else
			{
				if (indexToRemove < length)
				{
					for (int i = indexToRemove; i < length - totalToRemove; i++)
					{
						if (i + totalToRemove < length)
						{
							values[i] = values[i + totalToRemove];
						}
					}
				}
				length -= totalToRemove;
				IsFull = false;
			}
			OnContract(indexToRemove, totalToRemove);
		}

		protected bool IsValid(int index)
		{
			return index >= 0 && index < length;
		}

		protected int CountTotalOf(Type value, bool countConsequentiveOnly, out int firstFoundIndex)
		{
			int num = 0;
			bool flag = false;
			firstFoundIndex = -1;
			if (IsNotEmpty)
			{
				for (int i = 0; i < length; i++)
				{
					if (values[i].Equals(value))
					{
						num++;
						if (!flag)
						{
							firstFoundIndex = i;
						}
					}
					else if (flag && countConsequentiveOnly)
					{
						break;
					}
				}
			}
			return num;
		}

		protected string ErrorString(int index, Function operation)
		{
			return GetErrorString(index.ToString(), operation, default(Type));
		}

		protected string ErrorString(int index, Function operation, Type value)
		{
			return GetErrorString(index.ToString(), operation, value);
		}

		protected string ErrorString(string indexString, Function operation)
		{
			return GetErrorString(indexString, operation, default(Type));
		}

		private string GetErrorString(string indexString, Function operation, Type value)
		{
			string text = null;
			string text2 = null;
			switch (operation)
			{
			case Function.Get:
			case Function.GetKey:
				text = "get value from";
				break;
			case Function.Set:
			case Function.SetKey:
				text = string.Format("set value of {0} for", value);
				text2 = string.Format(".  Returning default {0} value of {1}", typeof(Type), default(Type));
				break;
			case Function.Insert:
				text = string.Format("insert new value of {0} into", value);
				break;
			default:
				Debug.LogError(string.Format("{0}: Internal logic error: recieved unhandled Operation case of {1} - please extend the switch statement in {2}.ErrorString to include a {0}-case", ClassCode, operation, ClassName));
				break;
			}
			switch (operation)
			{
			case Function.GetKey:
			case Function.SetKey:
				indexString = "unound key: " + indexString;
				break;
			default:
				indexString = string.Format("invalid index: {0} (outside of current index range 0 to {1} ({2}){3}", indexString, Length, length, text2);
				break;
			}
			return string.Format("{0}: attempt to {1} {2} using the {3}", ClassCode, text, ClassName, indexString);
		}

		protected string ArrayToString(string entryFormat, bool useRealLength)
		{
			string returnString;
			if (CanReturnString(out returnString))
			{
				bool isNumeric = object.ReferenceEquals(typeof(Type), typeof(float)) || object.ReferenceEquals(typeof(Type), typeof(float));
				bool hasGameObject = object.ReferenceEquals(typeof(Type), typeof(GameObject)) || object.ReferenceEquals(typeof(Type), typeof(Transform)) || object.ReferenceEquals(typeof(Type), typeof(Component));
				string entryToStringFormat = ((!string.IsNullOrEmpty(entryFormat)) ? entryFormat : "0.00f");
				int num = ((!useRealLength) ? Length : length);
				returnString = EntryToString(0, isNumeric, hasGameObject, entryFormat, useRealLength);
				for (int i = 1; i < num; i++)
				{
					returnString = returnString + ", " + EntryToString(i, isNumeric, hasGameObject, entryToStringFormat, useRealLength);
				}
			}
			return returnString;
		}

		protected bool CanReturnString(out string returnString)
		{
			bool result;
			if (isNotInitialized)
			{
				result = false;
				returnString = "Null";
			}
			else if (length == 0)
			{
				result = false;
				returnString = "Empty";
			}
			else
			{
				result = true;
				returnString = null;
			}
			return result;
		}

		private string EntryToString(int index, bool isNumeric, bool hasGameObject, string entryToStringFormat, bool usingRealLength)
		{
			if (!usingRealLength)
			{
				AdjustIndex(ref index);
			}
			if (isNumeric)
			{
				return values[index].ToString();
			}
			return values[index].ToString();
		}

		protected int AddEntry(Type value)
		{
			TryEnactExtend();
			values[LastIndex] = value;
			OnAdd(LastIndex, true);
			return LastIndex;
		}

		protected void AddNewEntry(Type value)
		{
			if (ContainsNot(value))
			{
				AddEntry(value);
			}
		}

		protected void InsertEntry(int index, Type value)
		{
			if (IsValid(index))
			{
				TryEnactExtend(index);
				values[index] = value;
				OnAdd(index, false);
			}
			else
			{
				Debug.LogError(ErrorString(index, Function.Insert, value));
			}
		}

		protected virtual void PreInitialize()
		{
		}

		protected virtual void OnInitialize(Type[] startingValues)
		{
		}

		protected virtual void PreClear(bool removeEntries)
		{
		}

		protected virtual void OnClear(bool removeEntries)
		{
		}

		protected virtual void PreChange(int indexToChange, Type newValue)
		{
		}

		protected virtual void OnChange(int indexToChange, Type newValue)
		{
		}

		protected virtual void AdjustIndex(ref int index)
		{
		}

		protected virtual void OnAdd(int indexAdded, bool adedToEnd)
		{
		}

		protected virtual void OnExtend(int? indexInserted)
		{
		}

		protected virtual void OnContract(int indexRemoved, int totalRemoved)
		{
		}

		protected virtual bool ShouldExtend()
		{
			return true;
		}
	}

	public abstract class ClassTypeSmartCollection<Type> : ClassTypeSmartCollectionLight<Type>
	{
		public int Add(Type value)
		{
			return AddEntry(value);
		}

		public void AddNew(Type value)
		{
			AddNewEntry(value);
		}

		public void Insert(int index, Type value)
		{
			InsertEntry(index, value);
		}
	}

	public abstract class ClassTypeSmartCollectionNumeric<Type> : ClassTypeSmartCollection<Type>
	{
		private enum Operation
		{
			Minimum = 0,
			Maximum = 1,
			Smallest = 2,
			Largest = 3
		}

		private TypeUtils.TypeInterface<Type> typeInterface;

		public Type AddAndGetMinimum(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetMinimum();
		}

		public Type AddAndGetMaximum(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetMaximum();
		}

		public Type AddAndGetSmallest(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetSmallest();
		}

		public Type AddAndGetLargest(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetLargest();
		}

		public Type AddAndGetSum(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetSum();
		}

		public float AddAndGetAverage(Type valueToAdd)
		{
			Add(valueToAdd);
			return GetAverage();
		}

		public Type GetMinimum()
		{
			return Get(Operation.Minimum);
		}

		public Type GetMaximum()
		{
			return Get(Operation.Maximum);
		}

		public Type GetSmallest()
		{
			return Get(Operation.Smallest);
		}

		public Type GetLargest()
		{
			return Get(Operation.Largest);
		}

		public Type GetSum()
		{
			Type val;
			if (base.IsEmpty)
			{
				val = typeInterface.GetZeroValue();
			}
			else
			{
				val = values[0];
				for (int i = 1; i < values.Length; i++)
				{
					val = typeInterface.SumOf(val, values[i]);
				}
			}
			return val;
		}

		public float GetAverage()
		{
			if (base.IsEmpty)
			{
				return 0f;
			}
			return typeInterface.DivideByIntAsFloat(GetSum(), Length);
		}

		public int GetAverageAsInt()
		{
			return (int)GetAverage();
		}

		private Type Get(Operation goal)
		{
			if (base.IsEmpty)
			{
				return typeInterface.GetSentinalValue();
			}
			Type val = values[0];
			for (int i = 1; i <= base.LastIndex; i++)
			{
				switch (goal)
				{
				case Operation.Minimum:
					if (typeInterface.IsSmaller(values[i], val))
					{
						val = values[i];
					}
					break;
				case Operation.Maximum:
					if (typeInterface.IsLarger(values[i], val))
					{
						val = values[i];
					}
					break;
				case Operation.Smallest:
					if (typeInterface.IsAbsSmaller(values[i], val))
					{
						val = values[i];
					}
					break;
				case Operation.Largest:
					if (typeInterface.IsAbsLarger(values[i], val))
					{
						val = values[i];
					}
					break;
				default:
					Debug.LogError(string.Format("ARUT.CTBU: ERROR: Received unhandled Operation type 'goal' of {0} in ClassTtypeBufferNumeric.Get()'s case statement.  Check logic", goal));
					break;
				}
			}
			return val;
		}

		protected override void PreInitialize()
		{
			typeInterface = new TypeUtils.TypeInterface<Type>();
		}
	}

	private class MultiIndexer
	{
		private int[] dimensionBases;

		public int Dimension { get; private set; }

		public int[] Dimensions { get; private set; }

		public int Size { get; private set; }

		public MultiIndexer(int[] dimensions)
		{
			InitializeMultiIndexer(dimensions);
		}

		public MultiIndexer(MathUtils.IntPair dimensions)
		{
			InitializeMultiIndexer(dimensions.ToArray());
		}

		public MultiIndexer(MathUtils.IntTrio dimensions)
		{
			InitializeMultiIndexer(dimensions.ToArray());
		}

		private void InitializeMultiIndexer(int[] dimensions)
		{
			if (dimensions != null && dimensions.Length > 0)
			{
				Dimensions = dimensions;
				Dimension = dimensions.Length;
				dimensionBases = new int[dimensions.Length];
				dimensionBases[0] = 1;
				for (int i = 1; i < dimensions.Length; i++)
				{
					dimensionBases[i] = dimensionBases[i - 1] * dimensions[i - 1];
				}
				int num = dimensions.Length - 1;
				Size = dimensionBases[num] * dimensions[num];
			}
			else
			{
				Debug.LogError("ARUT:  MTIN: Received zero indexes for configuring a multi-dimensional collection");
				Dimension = 1;
				dimensions = new int[1] { 1 };
				dimensionBases = new int[1] { 1 };
				Size = 1;
			}
		}

		public bool CanGetIndex(int[] indices, out int singleIndex)
		{
			bool result;
			if (indices.Length == 1)
			{
				singleIndex = indices[0];
				if (IsValid(singleIndex))
				{
					result = true;
				}
				else
				{
					result = false;
					Debug.LogError(string.Format("ARUT: CTMC: Problem with received single index: {0},\n  which is outside the range of this array (whose indexes are 0 to {1})\n  dimensionBases: {2}", singleIndex, Size - 1, IntArrayToString(dimensionBases)));
				}
			}
			else if (indices.Length == Dimension)
			{
				singleIndex = ToIndex(indices);
				if (IsValid(singleIndex))
				{
					result = true;
				}
				else
				{
					result = false;
					Debug.LogError(string.Format("ARUT: CTMC: Problem converting indices: {0} indexes to a {1}-dimensional index: {2},\n  which is outside the range of this array (whose indexes are 0 to {3})\n  dimensionBases: {4}", IntArrayToString(indices), Dimension, singleIndex, Size - 1, IntArrayToString(dimensionBases)));
				}
				result = true;
			}
			else
			{
				Debug.LogError(string.Format("ARUT: CTMC: Received {0} indexes for a {1}-dimensional array", indices.Length, Dimension));
				singleIndex = -1;
				result = false;
			}
			return result;
		}

		public int ToIndex(int[] indices)
		{
			int num = 0;
			for (int i = 0; i < Dimension; i++)
			{
				num += indices[i] * dimensionBases[i];
			}
			return num;
		}

		public int[] ToIndices(int singleIndex)
		{
			int[] array = new int[Dimension];
			for (int num = Dimension - 1; num >= 0; num--)
			{
				while (singleIndex >= dimensionBases[num])
				{
					singleIndex -= dimensionBases[num];
					array[num]++;
				}
			}
			return array;
		}

		public int GetIndexResetsFor(int singleIndex)
		{
			int num;
			if (singleIndex == 0)
			{
				num = 0;
			}
			else
			{
				int[] array = ToIndices(singleIndex);
				int[] array2 = ToIndices(singleIndex - 1);
				num = 0;
				for (int i = 0; i < Dimension; i++)
				{
					if (array[i] == 0 && array2[i] != 0)
					{
						num++;
					}
				}
			}
			return num;
		}

		public string GetSeperatorFor(int singleIndex)
		{
			int indexResetsFor = GetIndexResetsFor(singleIndex);
			string text;
			if (indexResetsFor == 0)
			{
				text = ", ";
			}
			else
			{
				text = null;
				for (int i = 0; i < indexResetsFor; i++)
				{
					text += "\n";
				}
			}
			return text;
		}

		private bool IsValid(int singleIndex)
		{
			return singleIndex >= 0 && singleIndex < Size;
		}
	}

	public abstract class ClassTypeMultiCollection<Type> : ClassTypeSmartCollectionLight<Type>
	{
		private MultiIndexer indexer;

		public Type this[params int[] indices]
		{
			get
			{
				int singleIndex;
				if (indexer.CanGetIndex(indices, out singleIndex))
				{
					return Get(singleIndex);
				}
				return default(Type);
			}
			set
			{
				int singleIndex;
				if (indexer.CanGetIndex(indices, out singleIndex))
				{
					Set(singleIndex, value);
				}
			}
		}

		public Type this[MathUtils.IntPair indexPair]
		{
			get
			{
				return this[new int[2] { indexPair.x, indexPair.y }];
			}
			set
			{
				this[new int[2] { indexPair.x, indexPair.y }] = value;
			}
		}

		public Type this[MathUtils.IntTrio indexTrio]
		{
			get
			{
				return this[new int[3] { indexTrio.x, indexTrio.y, indexTrio.z }];
			}
			set
			{
				this[new int[3] { indexTrio.x, indexTrio.y, indexTrio.z }] = value;
			}
		}

		protected void InitializeSmartMultiCollection(MathUtils.IntPair dimensions, bool startFull, string classCode, string className)
		{
			InitializeSmartMultiCollection(dimensions.ToArray(), startFull, classCode, className);
		}

		protected void InitializeSmartMultiCollection(MathUtils.IntTrio dimensions, bool startFull, string classCode, string className)
		{
			InitializeSmartMultiCollection(dimensions.ToArray(), startFull, classCode, className);
		}

		protected void InitializeSmartMultiCollection(int[] dimensions, bool startFull, string classCode, string className)
		{
			indexer = new MultiIndexer(dimensions);
			InitializeSmartCollection(indexer.Size, startFull, classCode, className);
		}

		public override string ToString()
		{
			string returnString;
			if (CanReturnString(out returnString))
			{
				returnString = this[new int[1]].ToString();
				for (int i = 1; i < indexer.Size; i++)
				{
					returnString = returnString + indexer.GetSeperatorFor(i) + this[new int[1] { i }].ToString();
				}
			}
			return returnString;
		}
	}

	public abstract class ClassTypeParallelCollectionLight<Type, ParallelType> : ClassTypeSmartCollectionLight<Type>
	{
		private SmartCollection<ParallelType> parallels;

		protected void InitializeParallelCollection(bool startFull, string classCode, string className)
		{
			parallels = new SmartCollection<ParallelType>(startFull);
			InitializeSmartCollection(startFull, classCode, className);
		}

		protected void InitializeParallelCollection(int startingSize, bool startFull, string classCode, string className)
		{
			parallels = new SmartCollection<ParallelType>(startingSize, startFull);
			InitializeSmartCollection(startingSize, startFull, classCode, className);
		}

		protected void InitializeParallelCollection(Type[] startingValues, bool startFull, string classCode, string className)
		{
			parallels = new SmartCollection<ParallelType>(startingValues.Length, true);
			InitializeSmartCollection(startingValues, startFull, classCode, className);
		}

		protected void SetParallel(int index, ParallelType parallelValue)
		{
			parallels[index] = parallelValue;
		}

		protected ParallelType GetParallel(int index)
		{
			return parallels[index];
		}

		protected override void OnClear(bool removeEntries)
		{
			parallels.Clear(removeEntries);
		}

		protected override void OnChange(int indexToChange, Type newValue)
		{
			parallels[indexToChange] = default(ParallelType);
		}

		protected override void OnExtend(int? indexInserted)
		{
			parallels.Extend(indexInserted);
		}

		protected override void OnContract(int indexRemoved, int totalRemoved)
		{
			parallels.Contract(indexRemoved, totalRemoved);
		}

		protected override void OnAdd(int indexAdded, bool entryAddedToEnd)
		{
			if (entryAddedToEnd)
			{
				parallels.Add(default(ParallelType));
			}
			else
			{
				parallels.Insert(indexAdded, default(ParallelType));
			}
		}

		public override string ToString()
		{
			string returnString;
			if (CanReturnString(out returnString))
			{
				returnString = EntryToString(0);
				for (int i = 1; i < Length; i++)
				{
					returnString = returnString + ", " + EntryToString(i);
				}
			}
			return returnString;
		}

		protected string EntryToString(int index)
		{
			return string.Format("{0} : {1}", values[index], parallels[index]);
		}
	}

	public abstract class ClassTypeParallelCollection<Type, ParallelType> : ClassTypeParallelCollectionLight<Type, ParallelType>
	{
		public int Add(Type value)
		{
			return AddEntry(value);
		}

		public void AddNew(Type value)
		{
			AddNewEntry(value);
		}

		public void Insert(int index, Type value)
		{
			InsertEntry(index, value);
		}

		protected int AddBoth(Type value, ParallelType parallelValue)
		{
			int num = AddEntry(value);
			SetParallel(num, parallelValue);
			return num;
		}
	}

	public abstract class ClassTypeBooleanedCollectionLight<Type> : ClassTypeParallelCollectionLight<Type, bool>
	{
		public void Flag(int index)
		{
			FlagAs(true, index);
		}

		public void FlagAs(bool trueOrFalse, int index)
		{
			SetParallel(index, trueOrFalse);
		}

		public bool IsTrue(int index)
		{
			return GetParallel(index);
		}

		public bool IsFalse(int index)
		{
			return !IsTrue(index);
		}
	}

	public abstract class ClassTypeBooleanedCollection<Type> : ClassTypeParallelCollection<Type, bool>
	{
		public void Add(Type value, bool flag)
		{
			AddBoth(value, flag);
		}

		public void Flag(int index)
		{
			FlagAs(true, index);
		}

		public void FlagAs(bool trueOrFalse, int index)
		{
			SetParallel(index, trueOrFalse);
		}

		public bool IsTrue(int index)
		{
			return GetParallel(index);
		}

		public bool IsFalse(int index)
		{
			return !IsTrue(index);
		}
	}

	public abstract class ClassTypeParallelMultiCollection<Type, ParallelType> : ClassTypeParallelCollectionLight<Type, ParallelType>
	{
		private MultiIndexer indexer;

		public Type this[params int[] indices]
		{
			get
			{
				int singleIndex;
				if (CanGetIndex(indices, out singleIndex))
				{
					return Get(singleIndex);
				}
				return default(Type);
			}
			set
			{
				int singleIndex;
				if (CanGetIndex(indices, out singleIndex))
				{
					Set(singleIndex, value);
				}
			}
		}

		public Type this[MathUtils.IntPair indexPair]
		{
			get
			{
				return this[new int[2] { indexPair.x, indexPair.y }];
			}
			set
			{
				this[new int[2] { indexPair.x, indexPair.y }] = value;
			}
		}

		public Type this[MathUtils.IntTrio indexTrio]
		{
			get
			{
				return this[new int[3] { indexTrio.x, indexTrio.y, indexTrio.z }];
			}
			set
			{
				this[new int[3] { indexTrio.x, indexTrio.y, indexTrio.z }] = value;
			}
		}

		protected void InitializeParallelMultiCollection(MathUtils.IntPair dimensions, bool startFull, string classCode, string className)
		{
			InitializeParallelMultiCollection(dimensions.ToArray(), startFull, classCode, className);
		}

		protected void InitializeParallelMultiCollection(MathUtils.IntTrio dimensions, bool startFull, string classCode, string className)
		{
			InitializeParallelMultiCollection(dimensions.ToArray(), startFull, classCode, className);
		}

		protected void InitializeParallelMultiCollection(int[] dimensions, bool startFull, string classCode, string className)
		{
			indexer = new MultiIndexer(dimensions);
			InitializeParallelCollection(indexer.Size, startFull, classCode, className);
		}

		public int ToIndex(params int[] indices)
		{
			return indexer.ToIndex(indices);
		}

		public int[] ToIndices(int singleIndex)
		{
			return indexer.ToIndices(singleIndex);
		}

		protected bool CanGetIndex(int[] indices, out int singleIndex)
		{
			return indexer.CanGetIndex(indices, out singleIndex);
		}

		public override string ToString()
		{
			string returnString;
			if (CanReturnString(out returnString))
			{
				returnString = EntryToString(0).ToString();
				for (int i = 1; i < indexer.Size; i++)
				{
					returnString = returnString + indexer.GetSeperatorFor(i) + EntryToString(i);
				}
			}
			return returnString;
		}
	}

	public abstract class ClassTypeBooleanedMultiCollection<Type> : ClassTypeParallelMultiCollection<Type, bool>
	{
		public void Flag(int singleIndex)
		{
			FlagAs(true, singleIndex);
		}

		public void Flag(params int[] indices)
		{
			FlagAs(true, indices);
		}

		public void FlagAs(bool trueOrFalse, int singleIndex)
		{
			SetParallel(singleIndex, trueOrFalse);
		}

		public void FlagAs(bool trueOrFalse, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				SetParallel(singleIndex, trueOrFalse);
			}
		}

		public bool IsTrue(int index)
		{
			return GetParallel(index);
		}

		public bool IsFalse(int index)
		{
			return !IsTrue(index);
		}

		public bool IsTrue(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex);
			}
			return false;
		}

		public bool IsFalse(params int[] indices)
		{
			return !IsTrue(indices);
		}
	}

	public abstract class ClassTypeTrinitiedMultiCollection<Type> : ClassTypeParallelMultiCollection<Type, MathUtils.Tri>
	{
		public void Flag(int singleIndex)
		{
			FlagAs(true, singleIndex);
		}

		public void Flag(params int[] indices)
		{
			FlagAs(true, indices);
		}

		public void Flag(MathUtils.IntPair indices)
		{
			Flag(indices.x, indices.y);
		}

		public void Flag(MathUtils.IntTrio indices)
		{
			Flag(indices.x, indices.y, indices.z);
		}

		public void FlagSpecialTrue(int singleIndex)
		{
			FlagSpecialTrueAs(true, singleIndex);
		}

		public void FlagSpecialTrue(params int[] indices)
		{
			FlagSpecialTrueAs(true, indices);
		}

		public void FlagSpecialTrue(MathUtils.IntPair indices)
		{
			FlagSpecialTrue(indices.x, indices.y);
		}

		public void FlagSpecialTrue(MathUtils.IntTrio indices)
		{
			FlagSpecialTrue(indices.x, indices.y, indices.z);
		}

		public void FlagAs(bool trueOrFalse, int singleIndex)
		{
			FlagAs(trueOrFalse, false, singleIndex);
		}

		public void FlagAs(bool trueOrFalse, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagAs(trueOrFalse, singleIndex);
			}
		}

		public void FlagSpecialTrueAs(bool specialTrueOrFalse, int singleIndex)
		{
			FlagAs(IsTrue(singleIndex), specialTrueOrFalse, singleIndex);
		}

		public void FlagSpecialTrueAs(bool specialTrueOrFalse, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagSpecialTrueAs(specialTrueOrFalse, singleIndex);
			}
		}

		public void FlagAs(bool trueOrFalse, bool specialTrueOrNot, int singleIndex)
		{
			SetParallel(singleIndex, new MathUtils.Tri(trueOrFalse, specialTrueOrNot));
		}

		public void FlagAs(bool trueOrFalse, bool specialTrueOrNot, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagAs(trueOrFalse, specialTrueOrNot, singleIndex);
			}
		}

		public void FlagAs(bool trueOrFalse, MathUtils.IntPair indices)
		{
			FlagAs(trueOrFalse, indices.x, indices.y);
		}

		public void FlagAs(bool trueOrFalse, MathUtils.IntTrio indices)
		{
			FlagAs(trueOrFalse, indices.x, indices.y, indices.z);
		}

		public void FlagSpecialTrueAs(bool specialTrueOrFalse, MathUtils.IntPair indices)
		{
			FlagSpecialTrueAs(specialTrueOrFalse, indices.x, indices.y);
		}

		public void FlagSpecialTrueAs(bool specialTrueOrFalse, MathUtils.IntTrio indices)
		{
			FlagSpecialTrueAs(specialTrueOrFalse, indices.x, indices.y, indices.z);
		}

		public bool IsTrue(int index)
		{
			return GetParallel(index).IsTrue;
		}

		public bool IsSpecialTrue(int index)
		{
			return GetParallel(index).IsSpecialTrue;
		}

		public bool IsNormalTrue(int index)
		{
			return GetParallel(index).IsNormalTrue;
		}

		public bool IsNormal(int index)
		{
			return GetParallel(index).IsNormal;
		}

		public bool IsFalse(int index)
		{
			return GetParallel(index).IsFalse;
		}

		public bool IsNotNormalTrue(int index)
		{
			return GetParallel(index).IsNotNormalTrue;
		}

		public bool IsNotSpecialTrue(int index)
		{
			return GetParallel(index).IsNotSpecialTrue;
		}

		public bool IsTrue(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsTrue(singleIndex);
		}

		public bool IsFalse(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsFalse(singleIndex);
		}

		public bool IsSpecialTrue(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsSpecialTrue(singleIndex);
		}

		public bool IsNormalTrue(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsNormalTrue(singleIndex);
		}

		public bool IsNotSpecialTrue(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsNotSpecialTrue(singleIndex);
		}

		public bool IsNotNormalTrue(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsNotNormalTrue(singleIndex);
		}

		public bool IsNormal(params int[] indices)
		{
			int singleIndex;
			return CanGetIndex(indices, out singleIndex) && IsNormal(singleIndex);
		}

		public bool IsTrue(MathUtils.IntPair indices)
		{
			return IsTrue(indices.x, indices.y);
		}

		public bool IsTrue(MathUtils.IntTrio indices)
		{
			return IsTrue(indices.x, indices.y, indices.z);
		}

		public bool IsFalse(MathUtils.IntPair indices)
		{
			return IsFalse(indices.x, indices.y);
		}

		public bool IsFalse(MathUtils.IntTrio indices)
		{
			return IsFalse(indices.x, indices.y, indices.z);
		}

		public bool IsSpecialTrue(MathUtils.IntPair indices)
		{
			return IsSpecialTrue(indices.x, indices.y);
		}

		public bool IsSpecialTrue(MathUtils.IntTrio indices)
		{
			return IsSpecialTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNormalTrue(MathUtils.IntPair indices)
		{
			return IsNormalTrue(indices.x, indices.y);
		}

		public bool IsNormalTrue(MathUtils.IntTrio indices)
		{
			return IsNormalTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNotSpecialTrue(MathUtils.IntPair indices)
		{
			return IsNotSpecialTrue(indices.x, indices.y);
		}

		public bool IsNotSpecialTrue(MathUtils.IntTrio indices)
		{
			return IsNotSpecialTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNotNormalTrue(MathUtils.IntPair indices)
		{
			return IsNotNormalTrue(indices.x, indices.y);
		}

		public bool IsNotNormalTrue(MathUtils.IntTrio indices)
		{
			return IsNotNormalTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNormal(MathUtils.IntPair indices)
		{
			return IsNormal(indices.x, indices.y);
		}

		public bool IsNormal(MathUtils.IntTrio indices)
		{
			return IsNormal(indices.x, indices.y, indices.z);
		}
	}

	public abstract class ClassTypeQuadedMultiCollection<Type> : ClassTypeParallelMultiCollection<Type, MathUtils.Quad>
	{
		public void Flag(int singleIndex)
		{
			FlagAs(true, singleIndex);
		}

		public void Flag(params int[] indices)
		{
			FlagAs(true, indices);
		}

		public void Flag(MathUtils.IntPair indices)
		{
			Flag(indices.x, indices.y);
		}

		public void Flag(MathUtils.IntTrio indices)
		{
			Flag(indices.x, indices.y, indices.z);
		}

		public void FlagSpecial(int singleIndex)
		{
			FlagSpecialAs(true, singleIndex);
		}

		public void FlagSpecial(params int[] indices)
		{
			FlagSpecialAs(true, indices);
		}

		public void FlagSpecial(MathUtils.IntPair indices)
		{
			FlagSpecial(indices.x, indices.y);
		}

		public void FlagSpecial(MathUtils.IntTrio indices)
		{
			FlagSpecial(indices.x, indices.y, indices.z);
		}

		public void FlagNormalTrue(int singleIndex)
		{
			FlagAs(true, false, singleIndex);
		}

		public void FlagNormalTrue(params int[] indices)
		{
			FlagAs(true, false, indices);
		}

		public void FlagNormalTrue(MathUtils.IntPair indices)
		{
			FlagNormalTrue(indices.x, indices.y);
		}

		public void FlagNormalTrue(MathUtils.IntTrio indices)
		{
			FlagNormalTrue(indices.x, indices.y, indices.z);
		}

		public void FlagNormalFalse(int singleIndex)
		{
			FlagAs(false, false, singleIndex);
		}

		public void FlagNormalFalse(params int[] indices)
		{
			FlagAs(false, false, indices);
		}

		public void FlagNormalFalse(MathUtils.IntPair indices)
		{
			FlagNormalFalse(indices.x, indices.y);
		}

		public void FlagNormalFalse(MathUtils.IntTrio indices)
		{
			FlagNormalFalse(indices.x, indices.y, indices.z);
		}

		public void FlagSpecialTrue(int singleIndex)
		{
			FlagAs(true, true, singleIndex);
		}

		public void FlagSpecialTrue(params int[] indices)
		{
			FlagAs(true, true, indices);
		}

		public void FlagSpecialTrue(MathUtils.IntPair indices)
		{
			FlagSpecialTrue(indices.x, indices.y);
		}

		public void FlagSpecialTrue(MathUtils.IntTrio indices)
		{
			FlagSpecialTrue(indices.x, indices.y, indices.z);
		}

		public void FlagSpecialFalse(int singleIndex)
		{
			FlagAs(false, true, singleIndex);
		}

		public void FlagSpecialFalse(params int[] indices)
		{
			FlagAs(false, true, indices);
		}

		public void FlagSpecialFalse(MathUtils.IntPair indices)
		{
			FlagSpecialFalse(indices.x, indices.y);
		}

		public void FlagSpecialFalse(MathUtils.IntTrio indices)
		{
			FlagSpecialFalse(indices.x, indices.y, indices.z);
		}

		public void FlagAs(bool trueOrFalse, int singleIndex)
		{
			FlagAs(trueOrFalse, IsSpecial(singleIndex), singleIndex);
		}

		public void FlagAs(bool trueOrFalse, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagAs(trueOrFalse, singleIndex);
			}
		}

		public void FlagAs(bool trueOrFalse, MathUtils.IntPair indices)
		{
			FlagAs(trueOrFalse, indices.x, indices.y);
		}

		public void FlagAs(bool trueOrFalse, MathUtils.IntTrio indices)
		{
			FlagAs(trueOrFalse, indices.x, indices.y, indices.z);
		}

		public void FlagSpecialAs(bool specialOrNormal, int singleIndex)
		{
			FlagAs(IsTrue(singleIndex), specialOrNormal, singleIndex);
		}

		public void FlagSpecialAs(bool specialOrNormal, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagSpecialAs(specialOrNormal, singleIndex);
			}
		}

		public void FlagSpecialAs(bool specialOrNormal, MathUtils.IntPair indices)
		{
			FlagSpecialAs(specialOrNormal, indices.x, indices.y);
		}

		public void FlagSpecialAs(bool specialOrNormal, MathUtils.IntTrio indices)
		{
			FlagSpecialAs(specialOrNormal, indices.x, indices.y, indices.z);
		}

		public void FlagAs(bool trueOrFalse, bool specialOrNormal, int singleIndex)
		{
			SetParallel(singleIndex, new MathUtils.Quad(trueOrFalse, specialOrNormal));
		}

		public void FlagAs(bool trueOrFalse, bool specialOrNormal, params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				FlagAs(trueOrFalse, specialOrNormal, singleIndex);
			}
		}

		public void FlagAs(bool trueOrFalse, bool specialOrNormal, MathUtils.IntPair indices)
		{
			FlagAs(trueOrFalse, specialOrNormal, indices.x, indices.y);
		}

		public void FlagAs(bool trueOrFalse, bool specialOrNormal, MathUtils.IntTrio indices)
		{
			FlagAs(trueOrFalse, specialOrNormal, indices.x, indices.y, indices.z);
		}

		public bool IsTrue(int index)
		{
			return GetParallel(index).IsTrue;
		}

		public bool IsSpecial(int index)
		{
			return GetParallel(index).IsSpecial;
		}

		public bool IsNormalTrue(int index)
		{
			return GetParallel(index).IsNormalTrue;
		}

		public bool IsNormalFalse(int index)
		{
			return GetParallel(index).IsNormalFalse;
		}

		public bool IsSpecialTrue(int index)
		{
			return GetParallel(index).IsSpecialTrue;
		}

		public bool IsSpecialFalse(int index)
		{
			return GetParallel(index).IsSpecialFalse;
		}

		public bool IsFalse(int index)
		{
			return !IsTrue(index);
		}

		public bool IsNormal(int index)
		{
			return !IsSpecial(index);
		}

		public bool IsNotNormalTrue(int index)
		{
			return !IsNormalTrue(index);
		}

		public bool IsNotNormalFalse(int index)
		{
			return !IsNormalFalse(index);
		}

		public bool IsNotSpecialTrue(int index)
		{
			return !IsSpecialTrue(index);
		}

		public bool IsNotSpecialFalse(int index)
		{
			return !IsSpecialFalse(index);
		}

		public bool HasTrue(int index)
		{
			return !IsNormalFalse(index);
		}

		public bool HasFalse(int index)
		{
			return !IsSpecialTrue(index);
		}

		public bool IsTrue(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsTrue;
			}
			return false;
		}

		public bool IsTrue(MathUtils.IntPair indices)
		{
			return IsTrue(indices.x, indices.y);
		}

		public bool IsTrue(MathUtils.IntTrio indices)
		{
			return IsTrue(indices.x, indices.y, indices.z);
		}

		public bool IsSpecial(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsSpecial;
			}
			return false;
		}

		public bool IsSpecial(MathUtils.IntPair indices)
		{
			return IsSpecial(indices.x, indices.y);
		}

		public bool IsSpecial(MathUtils.IntTrio indices)
		{
			return IsSpecial(indices.x, indices.y, indices.z);
		}

		public bool IsNormalTrue(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsNormalTrue;
			}
			return false;
		}

		public bool IsNormalTrue(MathUtils.IntPair indices)
		{
			return IsNormalTrue(indices.x, indices.y);
		}

		public bool IsNormalTrue(MathUtils.IntTrio indices)
		{
			return IsNormalTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNormalFalse(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsNormalFalse;
			}
			return false;
		}

		public bool IsNormalFalse(MathUtils.IntPair indices)
		{
			return IsNormalFalse(indices.x, indices.y);
		}

		public bool IsNormalFalse(MathUtils.IntTrio indices)
		{
			return IsNormalFalse(indices.x, indices.y, indices.z);
		}

		public bool IsSpecialTrue(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsSpecialTrue;
			}
			return false;
		}

		public bool IsSpecialTrue(MathUtils.IntPair indices)
		{
			return IsSpecialTrue(indices.x, indices.y);
		}

		public bool IsSpecialTrue(MathUtils.IntTrio indices)
		{
			return IsSpecialTrue(indices.x, indices.y, indices.z);
		}

		public bool IsSpecialFalse(params int[] indices)
		{
			int singleIndex;
			if (CanGetIndex(indices, out singleIndex))
			{
				return GetParallel(singleIndex).IsSpecialFalse;
			}
			return false;
		}

		public bool IsSpecialFalse(MathUtils.IntPair indices)
		{
			return IsSpecialFalse(indices.x, indices.y);
		}

		public bool IsSpecialFalse(MathUtils.IntTrio indices)
		{
			return IsSpecialFalse(indices.x, indices.y, indices.z);
		}

		public bool IsFalse(params int[] indices)
		{
			return !IsTrue(indices);
		}

		public bool IsFalse(MathUtils.IntPair indices)
		{
			return IsFalse(indices.x, indices.y);
		}

		public bool IsFalse(MathUtils.IntTrio indices)
		{
			return IsFalse(indices.x, indices.y, indices.z);
		}

		public bool IsNormal(params int[] indices)
		{
			return !IsSpecial(indices);
		}

		public bool IsNormal(MathUtils.IntPair indices)
		{
			return IsNormal(indices.x, indices.y);
		}

		public bool IsNormal(MathUtils.IntTrio indices)
		{
			return IsNormal(indices.x, indices.y, indices.z);
		}

		public bool IsNotNormalTrue(params int[] indices)
		{
			return !IsNormalTrue(indices);
		}

		public bool IsNotNormalTrue(MathUtils.IntPair indices)
		{
			return IsNotNormalTrue(indices.x, indices.y);
		}

		public bool IsNotNormalTrue(MathUtils.IntTrio indices)
		{
			return IsNotNormalTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNotNormalFalse(params int[] indices)
		{
			return !IsNormalFalse(indices);
		}

		public bool IsNotNormalFalse(MathUtils.IntPair indices)
		{
			return IsNotNormalFalse(indices.x, indices.y);
		}

		public bool IsNotNormalFalse(MathUtils.IntTrio indices)
		{
			return IsNotNormalFalse(indices.x, indices.y, indices.z);
		}

		public bool IsNotSpecialTrue(params int[] indices)
		{
			return !IsSpecialTrue(indices);
		}

		public bool IsNotSpecialTrue(MathUtils.IntPair indices)
		{
			return IsNotSpecialTrue(indices.x, indices.y);
		}

		public bool IsNotSpecialTrue(MathUtils.IntTrio indices)
		{
			return IsNotSpecialTrue(indices.x, indices.y, indices.z);
		}

		public bool IsNotSpecialFalse(params int[] indices)
		{
			return !IsSpecialFalse(indices);
		}

		public bool IsNotSpecialFalse(MathUtils.IntPair indices)
		{
			return IsNotSpecialFalse(indices.x, indices.y);
		}

		public bool IsNotSpecialFalse(MathUtils.IntTrio indices)
		{
			return IsNotSpecialFalse(indices.x, indices.y, indices.z);
		}

		public bool HasTrue(params int[] indices)
		{
			return !IsNormalFalse(indices);
		}

		public bool HasTrue(MathUtils.IntPair indices)
		{
			return HasTrue(indices.x, indices.y);
		}

		public bool HasTrue(MathUtils.IntTrio indices)
		{
			return HasTrue(indices.x, indices.y, indices.z);
		}

		public bool HasFalse(params int[] indices)
		{
			return !IsSpecialTrue(indices);
		}

		public bool HasFalse(MathUtils.IntPair indices)
		{
			return HasFalse(indices.x, indices.y);
		}

		public bool HasFalse(MathUtils.IntTrio indices)
		{
			return HasFalse(indices.x, indices.y, indices.z);
		}
	}

	public abstract class ClassTypeReIndexedCollectionLight<Type> : ClassTypeSmartCollectionLight<Type>
	{
		protected List<int> indexList;

		public override int Length
		{
			get
			{
				return indexList.Length;
			}
		}

		public int LengthReal
		{
			get
			{
				return base.length;
			}
		}

		public string ToString(bool showRealEntries)
		{
			return ArrayToString(null, showRealEntries);
		}

		public string ToString(bool showRealEntries, string entryFormat)
		{
			return ArrayToString(entryFormat, showRealEntries);
		}

		protected override void OnInitialize(Type[] startingValues)
		{
			if (startingValues != null)
			{
				indexList = new List<int>(startingValues.Length);
			}
			else
			{
				indexList = new List<int>();
			}
		}

		protected override void OnContract(int indexRemoved, int totalRemoved)
		{
			indexList.RemoveConsequetive(indexRemoved);
			if (totalRemoved > 1)
			{
				for (int i = 1; i < totalRemoved; i++)
				{
					indexList.RemoveConsequetive(indexRemoved + 1);
				}
			}
		}

		protected override void AdjustIndex(ref int index)
		{
			ReIndex(ref index);
		}

		protected abstract void ReIndex(ref int index);
	}

	public abstract class ClassTypeReIndexedCollection<Type> : ClassTypeReIndexedCollectionLight<Type>
	{
		public int Add(Type value)
		{
			return AddEntry(value);
		}

		public void AddNew(Type value)
		{
			AddNewEntry(value);
		}

		public void Insert(int index, Type value)
		{
			InsertEntry(index, value);
		}
	}

	public abstract class ClassTypeWeightedCollection<Type> : ClassTypeReIndexedCollection<Type>
	{
		public int Add(Type value, int weights)
		{
			int num = -1;
			if (weights < 0)
			{
				Debug.LogError(string.Format("{0}: Input error: received invalid value of {2} for weights in {1}.Add() - please enter a value of 1 or more", base.ClassCode, base.ClassName, weights));
			}
			else if (weights > 0)
			{
				num = Add(value);
				if (weights > 1)
				{
					OnAdd(num, weights - 1);
				}
			}
			return num;
		}

		public int SmartAdd(Type[] values)
		{
			List<MathUtils.IntPair> list = new List<MathUtils.IntPair>(values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (list.IsEmpty)
				{
					list.Add(new MathUtils.IntPair(i, 1));
					continue;
				}
				int? num = null;
				for (int j = 0; j < list.Length; j++)
				{
					if (values[list[j].x].Equals(values[i]))
					{
						num = j;
						break;
					}
				}
				if (num.HasValue)
				{
					list[num.Value] = new MathUtils.IntPair(list[num.Value].x, list[num.Value].y + 1);
				}
				else
				{
					list.Add(new MathUtils.IntPair(i, 1));
				}
			}
			for (int k = 0; k < list.Length; k++)
			{
				Add(values[list[k].x], list[k].y);
			}
			return list.Length;
		}

		private void OnAdd(int indexAdded, int numberOfTimesToAdd)
		{
			for (int i = 1; i <= numberOfTimesToAdd; i++)
			{
				indexList.Add(indexAdded);
			}
		}

		protected override void ReIndex(ref int index)
		{
			index = indexList[index];
		}

		protected override void OnAdd(int indexAdded, bool addedToEnd)
		{
			OnAdd(indexAdded, 1);
		}

		public override string ToString()
		{
			return ArrayToString(null, false);
		}

		public override string ToString(string entryFormat)
		{
			return ArrayToString(entryFormat, false);
		}
	}

	public class GeniList<Type> : ClassTypeReIndexedCollectionLight<Type>
	{
		private const string classCode = "GNLT";

		private const string className = "Genius List";

		private const bool startFull = false;

		public GeniList()
		{
			InitializeSmartCollection(false, "GNLT", "Genius List");
		}

		public GeniList(int startingSize)
		{
			InitializeSmartCollection(startingSize, false, "GNLT", "Genius List");
		}

		public GeniList(params Type[] startingValues)
		{
			InitializeSmartCollection(startingValues, false, "GNLT", "Genius List");
		}

		public int Add(Type value)
		{
			int foundAtIndex;
			if (Contains(value, out foundAtIndex))
			{
				OnAdd(foundAtIndex, 1);
			}
			else
			{
				AddEntry(value);
			}
			return base.LastIndex;
		}

		private void OnAdd(int indexAdded, int numberOfTimesToAdd)
		{
			for (int i = 1; i <= numberOfTimesToAdd; i++)
			{
				indexList.Add(indexAdded);
			}
		}

		protected override void ReIndex(ref int index)
		{
			index = indexList[index];
		}

		protected override void OnAdd(int indexAdded, bool addedToEnd)
		{
			OnAdd(indexAdded, 1);
		}

		public override string ToString()
		{
			return ArrayToString(null, false);
		}

		public override string ToString(string entryFormat)
		{
			return ArrayToString(entryFormat, false);
		}
	}

	public class Dynarray<Type>
	{
		public readonly int MaxLength;

		private Type[] values;

		private bool valuesNotInitialized;

		private Type[] storedValues;

		private bool storedValuesUpToDate;

		public int Length { get; private set; }

		public bool IsEmpty { get; private set; }

		public bool IsFull { get; private set; }

		public bool IsNotEmpty
		{
			get
			{
				return !IsEmpty;
			}
		}

		public bool IsNotFull
		{
			get
			{
				return !IsFull;
			}
		}

		public Dynarray(int maximumArrayLength)
		{
			MaxLength = maximumArrayLength;
			Clear();
		}

		public Dynarray(params Type[] startingValues)
		{
			MaxLength = startingValues.Length;
			Clear();
			for (int i = 0; i < startingValues.Length; i++)
			{
				Add(startingValues[i]);
			}
		}

		public Dynarray(int maximumLength, params Type[] startingValues)
		{
			MaxLength = maximumLength;
			Clear();
			for (int i = 0; i < startingValues.Length; i++)
			{
				Add(startingValues[i]);
			}
		}

		public void Clear()
		{
			if (values != null)
			{
				values = null;
			}
			if (storedValues != null)
			{
				storedValues = null;
			}
			valuesNotInitialized = true;
			Reset();
		}

		public void Reset()
		{
			Length = 0;
			IsEmpty = true;
			IsFull = false;
			storedValuesUpToDate = false;
		}

		public bool Add(Type newValue)
		{
			if (IsFull)
			{
				return false;
			}
			if (IsEmpty)
			{
				if (valuesNotInitialized)
				{
					values = new Type[MaxLength];
					valuesNotInitialized = false;
				}
				IsEmpty = false;
			}
			values[Length] = newValue;
			Length++;
			if (Length == MaxLength)
			{
				IsFull = true;
			}
			storedValuesUpToDate = false;
			return true;
		}

		public bool AddNew(Type newValue)
		{
			if (Contains(newValue))
			{
				return false;
			}
			return Add(newValue);
		}

		public bool Remove(Type valueToRemove)
		{
			bool result = false;
			int foundAtIndex;
			if (Contains(valueToRemove, out foundAtIndex))
			{
				for (int i = foundAtIndex; i < Length - 1; i++)
				{
					values[i] = values[i + 1];
				}
				Length--;
				if (IsFull)
				{
					IsFull = false;
				}
				if (Length == 0)
				{
					IsEmpty = true;
				}
				storedValuesUpToDate = false;
				result = true;
			}
			return result;
		}

		public bool ContainsNot(Type queryValue)
		{
			int foundAtIndex;
			return !Contains(queryValue, out foundAtIndex);
		}

		public bool Contains(Type queryValue)
		{
			int foundAtIndex;
			return Contains(queryValue, out foundAtIndex);
		}

		public bool ContainsNot(Type queryValue, out int foundAtIndex)
		{
			return !Contains(queryValue, out foundAtIndex);
		}

		public bool Contains(Type queryValue, out int foundAtIndex)
		{
			bool result = false;
			foundAtIndex = -1;
			if (IsNotEmpty)
			{
				for (int i = 0; i < Length; i++)
				{
					if (values[i].Equals(queryValue))
					{
						result = true;
						foundAtIndex = i;
						break;
					}
				}
			}
			return result;
		}

		public Type[] GetStored()
		{
			if (!storedValuesUpToDate)
			{
				if (IsEmpty)
				{
					storedValues = null;
				}
				else
				{
					storedValues = new Type[Length];
					for (int i = 0; i < Length; i++)
					{
						storedValues[i] = values[i];
					}
				}
				storedValuesUpToDate = true;
			}
			return storedValues;
		}

		public Type GetRandom()
		{
			Type randomValue;
			CanGetRandom(out randomValue);
			return randomValue;
		}

		public bool CannotGetRandom(out Type randomValue)
		{
			return !CanGetRandom(out randomValue);
		}

		public bool CanGetRandom(out Type randomValue)
		{
			bool result;
			if (IsEmpty)
			{
				result = false;
				randomValue = default(Type);
			}
			else
			{
				result = true;
				int randomIndex = Randomizer.GetRandomIndex(Length);
				randomValue = values[randomIndex];
			}
			return result;
		}

		public override string ToString()
		{
			string text;
			if (IsEmpty)
			{
				text = "Null";
			}
			else
			{
				text = null;
				for (int i = 0; i < Length; i++)
				{
					text = ConcatListed(text, values[i].ToString());
				}
			}
			return text;
		}

		private string ConcatListed(string oldString, string newString)
		{
			if (string.IsNullOrEmpty(oldString))
			{
				return newString;
			}
			return oldString + ", " + newString;
		}
	}

	public class ActiveArray<Type>
	{
		protected Type[] values;

		protected bool[] activeStates;

		protected Dynarray<Type> actives;

		protected Dynarray<Type> inactives;

		public bool IsValid { get; private set; }

		public bool IsNotValid
		{
			get
			{
				return !IsValid;
			}
		}

		public int Length { get; private set; }

		public bool IsEmpty { get; protected set; }

		public bool IsNotEmpty
		{
			get
			{
				return !IsEmpty;
			}
		}

		public bool HasActive
		{
			get
			{
				return actives.IsNotEmpty;
			}
		}

		public bool NoneActive
		{
			get
			{
				return actives.IsEmpty;
			}
		}

		public bool AllActive
		{
			get
			{
				return actives.IsFull;
			}
		}

		public bool HasInactive
		{
			get
			{
				return inactives.IsNotEmpty;
			}
		}

		public bool NoneInactive
		{
			get
			{
				return inactives.IsEmpty;
			}
		}

		public bool AllInactive
		{
			get
			{
				return inactives.IsFull;
			}
		}

		public int TotalActive
		{
			get
			{
				return actives.Length;
			}
		}

		public int TotalInactive
		{
			get
			{
				return inactives.Length;
			}
		}

		public ActiveArray(params Type[] startingValues)
		{
			ConfigureActiveArray(startingValues);
		}

		protected void ConfigureActiveArray(Type[] startingValues)
		{
			if (startingValues == null)
			{
				Length = 0;
			}
			else
			{
				Length = startingValues.Length;
			}
			IsValid = Length > 0;
			Clear();
			if (IsValid)
			{
				for (int i = 0; i < Length; i++)
				{
					values[i] = startingValues[i];
				}
				IsEmpty = false;
			}
			actives = new Dynarray<Type>(Length);
			inactives = new Dynarray<Type>(startingValues);
		}

		public void Clear()
		{
			if (IsValid)
			{
				values = new Type[Length];
				activeStates = new bool[Length];
				IsEmpty = true;
				OnClear();
				Reset();
			}
		}

		public void Reset()
		{
			if (IsValid)
			{
				if (actives != null)
				{
					actives.Reset();
				}
				if (inactives != null)
				{
					inactives.Reset();
				}
				for (int i = 0; i < Length; i++)
				{
					activeStates[i] = false;
				}
				OnReset();
			}
		}

		public bool Activate(Type value)
		{
			return SetByValue(value, true);
		}

		public bool Deactivate(Type value)
		{
			return SetByValue(value, false);
		}

		public bool ContainsNot(Type queryValue)
		{
			int foundAtIndex;
			return !Contains(queryValue, out foundAtIndex);
		}

		public bool Contains(Type queryValue)
		{
			int foundAtIndex;
			return Contains(queryValue, out foundAtIndex);
		}

		public bool ContainsNot(Type queryValue, out int foundAtIndex)
		{
			return !Contains(queryValue, out foundAtIndex);
		}

		public bool Contains(Type queryValue, out int foundAtIndex)
		{
			bool result = false;
			foundAtIndex = -1;
			if (IsValid && IsNotEmpty)
			{
				for (int i = 0; i < Length; i++)
				{
					if (queryValue.Equals(values[i]))
					{
						result = true;
						foundAtIndex = i;
						break;
					}
				}
			}
			return result;
		}

		public bool IsNotValidIndex(int queryIndex)
		{
			return !IsValidIndex(queryIndex);
		}

		public bool IsValidIndex(int queryIndex)
		{
			return IsNotEmpty && queryIndex >= 0 && queryIndex < Length;
		}

		public Type[] GetActives()
		{
			return actives.GetStored();
		}

		public Type[] GetInactives()
		{
			return inactives.GetStored();
		}

		public Type GetRandomActive()
		{
			return actives.GetRandom();
		}

		public bool CanGetRandomActive(out Type randomActive)
		{
			return actives.CanGetRandom(out randomActive);
		}

		public Type GetRandomInactive()
		{
			return inactives.GetRandom();
		}

		public bool CanGetRandomInactive(out Type randomInactive)
		{
			return inactives.CanGetRandom(out randomInactive);
		}

		public override string ToString()
		{
			if (IsEmpty)
			{
				return "Null";
			}
			string text = null;
			if (HasActive)
			{
				text = "Active: " + actives.ToString();
			}
			if (HasInactive)
			{
				text = ConcatListed(text, "Inactive: " + inactives.ToString());
			}
			return AppendToString(text);
		}

		public string ToString(bool includeBooleansString)
		{
			string text = ToString();
			if (includeBooleansString)
			{
				string text2 = null;
				for (int i = 0; i < Length; i++)
				{
					text2 = ConcatListed(text2, activeStates[i].ToString());
				}
				text = text + "\n  " + text2;
			}
			return text;
		}

		private bool SetByValue(Type value, bool newState)
		{
			int foundAtIndex;
			if (Contains(value, out foundAtIndex))
			{
				return SetByIndex(foundAtIndex, newState, true);
			}
			return false;
		}

		private bool SetByIndex(int index, bool newState, bool preVerified = false)
		{
			if ((preVerified || IsValidIndex(index)) && activeStates[index] != newState)
			{
				activeStates[index] = newState;
				if (newState)
				{
					actives.Add(values[index]);
					inactives.Remove(values[index]);
				}
				else
				{
					inactives.Add(values[index]);
					actives.Remove(values[index]);
				}
				return true;
			}
			return false;
		}

		protected string ConcatListed(string oldString, string newString)
		{
			if (string.IsNullOrEmpty(oldString))
			{
				return newString;
			}
			return oldString + ", " + newString;
		}

		protected virtual string AppendToString(string returnString)
		{
			return returnString;
		}

		protected virtual void OnClear()
		{
		}

		protected virtual void OnReset()
		{
		}
	}

	public class ActiveArrayAdv<Type> : ActiveArray<Type>
	{
		private bool[] pauseStates;

		private Dynarray<Type> paused;

		public bool HasPaused
		{
			get
			{
				return paused.IsNotEmpty;
			}
		}

		public bool AllPaused
		{
			get
			{
				return paused.IsFull;
			}
		}

		public bool NonePaused
		{
			get
			{
				return paused.IsEmpty;
			}
		}

		public int TotalPaused
		{
			get
			{
				return paused.Length;
			}
		}

		public ActiveArrayAdv(params Type[] startingValues)
			: base(new Type[0])
		{
			ConfigureActiveAdvArray(startingValues);
		}

		protected void ConfigureActiveAdvArray(Type[] startingValues)
		{
			ConfigureActiveArray(startingValues);
			paused = new Dynarray<Type>(startingValues.Length);
		}

		protected override void OnClear()
		{
			pauseStates = new bool[base.Length];
		}

		protected override void OnReset()
		{
			paused.Reset();
			for (int i = 0; i < base.Length; i++)
			{
				pauseStates[i] = false;
			}
		}

		public bool Pause(Type value)
		{
			return PauseByValue(value, true);
		}

		public bool Unpause(Type value)
		{
			return PauseByValue(value, false);
		}

		protected override string AppendToString(string returnString)
		{
			if (HasPaused)
			{
				returnString = ConcatListed(returnString, "Paused: " + paused.ToString());
			}
			return returnString;
		}

		private bool PauseByValue(Type value, bool newPauseState)
		{
			int foundAtIndex;
			if (Contains(value, out foundAtIndex))
			{
				return PauseByIndex(foundAtIndex, newPauseState, true);
			}
			return false;
		}

		private bool PauseByIndex(int index, bool newPauseState, bool preVerified = false)
		{
			if ((preVerified || IsValidIndex(index)) && pauseStates[index] != newPauseState)
			{
				pauseStates[index] = newPauseState;
				if (newPauseState)
				{
					paused.Add(values[index]);
					if (activeStates[index])
					{
						actives.Remove(values[index]);
					}
					else
					{
						inactives.Remove(values[index]);
					}
				}
				else
				{
					paused.Remove(values[index]);
					if (activeStates[index])
					{
						actives.Add(values[index]);
					}
					else
					{
						inactives.Add(values[index]);
					}
				}
				return true;
			}
			return false;
		}
	}

	public class ActiveCount : ActiveArray<int>
	{
		public ActiveCount(int countUpTo, int startCountingAt = 0)
			: base(new int[0])
		{
			int[] startingValues = CountOutArray(countUpTo, startCountingAt);
			ConfigureActiveArray(startingValues);
		}
	}

	public class ActiveAdvCount : ActiveArrayAdv<int>
	{
		public ActiveAdvCount(int countUpTo, int startCountingAt = 0)
			: base(new int[0])
		{
			int[] startingValues = CountOutArray(countUpTo, startCountingAt);
			ConfigureActiveAdvArray(startingValues);
		}
	}

	public class SmartDict<KeyType, ValueType>
	{
		private enum Operation
		{
			Get = 0,
			Set = 1,
			Add = 2
		}

		private List<KeyType> keys;

		private List<ValueType> values;

		private bool allowDuplicateKeys;

		public int LastIndex
		{
			get
			{
				return keys.LastIndex;
			}
		}

		public int Length
		{
			get
			{
				return keys.Length;
			}
		}

		public ValueType this[KeyType key]
		{
			get
			{
				int foundAtIndex;
				if (keys.Contains(key, out foundAtIndex))
				{
					return values[foundAtIndex];
				}
				Debug.LogWarning(KeyErrorString(key, Operation.Get));
				return default(ValueType);
			}
			set
			{
				int foundAtIndex;
				if (keys.Contains(key, out foundAtIndex))
				{
					values[foundAtIndex] = value;
				}
				else
				{
					Debug.LogWarning(KeyErrorString(key, Operation.Set, value));
				}
			}
		}

		public KeyType[] Keys
		{
			get
			{
				return keys.ToArray();
			}
		}

		public ValueType[] Values
		{
			get
			{
				return values.ToArray();
			}
		}

		public ValueType Last
		{
			get
			{
				return values[LastIndex];
			}
		}

		public SmartDict(bool allowDuplicateKeys = false)
		{
			keys = new List<KeyType>();
			values = new List<ValueType>();
			this.allowDuplicateKeys = allowDuplicateKeys;
		}

		public SmartDict(List<KeyType> startingKeys, List<ValueType> startingValues, bool allowDuplicateKeys = false)
		{
			keys = startingKeys;
			values = startingValues;
			this.allowDuplicateKeys = allowDuplicateKeys;
		}

		public KeyType GetKeyByIndex(int index)
		{
			KeyType keyAtIndex;
			TryGetKeyByIndex(index, out keyAtIndex);
			return keyAtIndex;
		}

		public void SetKeyByIndex(int index, KeyType replacementKey)
		{
			TrySetKeyByIndex(index, replacementKey);
		}

		public ValueType GetValueByIndex(int index)
		{
			ValueType valueAtIndex;
			TryGetValueByIndex(index, out valueAtIndex);
			return valueAtIndex;
		}

		public void SetValueByIndex(int index, ValueType replacementValue)
		{
			TrySetValueByIndex(index, replacementValue);
		}

		public KeyType GetKeyByIndex(int index, out bool indexWasValid)
		{
			KeyType keyAtIndex;
			indexWasValid = TryGetKeyByIndex(index, out keyAtIndex);
			return keyAtIndex;
		}

		public void SetKeyByIndex(int index, KeyType replacementKey, out bool indexWasValid)
		{
			indexWasValid = TrySetKeyByIndex(index, replacementKey);
		}

		public ValueType GetValueByIndex(int index, out bool indexWasValid)
		{
			ValueType valueAtIndex;
			indexWasValid = TryGetValueByIndex(index, out valueAtIndex);
			return valueAtIndex;
		}

		public void SetValueByIndex(int index, ValueType replacementValue, out bool indexWasValid)
		{
			indexWasValid = TrySetValueByIndex(index, replacementValue);
		}

		public bool TryGetKeyByIndex(int index, out KeyType keyAtIndex)
		{
			if (keys.ContainsIndex(index))
			{
				keyAtIndex = keys[index];
				return true;
			}
			Debug.LogError(IndexErrorString(index, Operation.Get));
			keyAtIndex = default(KeyType);
			return false;
		}

		public bool TrySetKeyByIndex(int index, KeyType replacementKey)
		{
			if (keys.ContainsIndex(index))
			{
				keys[index] = replacementKey;
				return true;
			}
			Debug.LogError(IndexErrorString(index, Operation.Set));
			return false;
		}

		public bool TryGetValueByIndex(int index, out ValueType valueAtIndex)
		{
			if (values.ContainsIndex(index))
			{
				valueAtIndex = values[index];
				return true;
			}
			Debug.LogError(IndexErrorString(index, Operation.Get));
			valueAtIndex = default(ValueType);
			return false;
		}

		public bool TrySetValueByIndex(int index, ValueType replacementValue)
		{
			if (values.ContainsIndex(index))
			{
				values[index] = replacementValue;
				return true;
			}
			Debug.LogError(IndexErrorString(index, Operation.Set));
			return false;
		}

		public int Add(KeyType key, ValueType value)
		{
			if (allowDuplicateKeys || ContainsNotKey(key))
			{
				keys.Add(key);
				values.Add(value);
				return LastIndex;
			}
			Debug.LogWarning(KeyErrorString(key, Operation.Add));
			return IndexOfKey(key);
		}

		public int AddNew(KeyType key, ValueType value)
		{
			if (ContainsNotKey(key))
			{
				return Add(key, value);
			}
			return IndexOfKey(key);
		}

		public void Remove(KeyType key)
		{
			int foundAtIndex;
			if (keys.Contains(key, out foundAtIndex))
			{
				keys.RemoveAt(foundAtIndex);
				values.RemoveAt(foundAtIndex);
			}
		}

		public void RemoveAt(int index)
		{
			if (keys.ContainsIndex(index))
			{
				keys.RemoveAt(index);
				values.RemoveAt(index);
			}
		}

		public void Clear()
		{
			keys.Clear();
			values.Clear();
		}

		public bool ContainsKey(KeyType key)
		{
			return keys.Contains(key);
		}

		public bool ContainsValue(ValueType value)
		{
			return values.Contains(value);
		}

		public bool ContainsKey(KeyType key, out int foundAtIndex)
		{
			return keys.Contains(key, out foundAtIndex);
		}

		public bool ContainsValue(ValueType value, out int foundAtIndex)
		{
			return values.Contains(value, out foundAtIndex);
		}

		public bool ContainsNotKey(KeyType key)
		{
			return !ContainsKey(key);
		}

		public bool ContainsNotValue(ValueType value)
		{
			return !ContainsValue(value);
		}

		public bool ContainsNotKey(KeyType key, out int foundAtIndex)
		{
			return !ContainsKey(key, out foundAtIndex);
		}

		public bool ContainsNotValue(ValueType value, out int foundAtIndex)
		{
			return !ContainsValue(value, out foundAtIndex);
		}

		public bool ContainsIndex(int index)
		{
			return keys.ContainsIndex(index);
		}

		public bool ContainsNotIndex(int index)
		{
			return keys.ContainsNotIndex(index);
		}

		public int IndexOfKey(KeyType key)
		{
			int foundAtIndex;
			ContainsKey(key, out foundAtIndex);
			return foundAtIndex;
		}

		public int IndexOfValue(ValueType value)
		{
			int foundAtIndex;
			ContainsValue(value, out foundAtIndex);
			return foundAtIndex;
		}

		public string ToString(string entrySeperator = ", ", string keyToValueSeperator = ":")
		{
			string text;
			if (keys.Length == 0)
			{
				text = "Null";
			}
			else
			{
				text = string.Concat(keys[0], keyToValueSeperator, values[0]);
				for (int i = 1; i < keys.Length; i++)
				{
					text = string.Concat(text, entrySeperator, keys[i], keyToValueSeperator, values[i]);
				}
			}
			return text;
		}

		private string KeyErrorString(KeyType key, Operation operation, [Optional] ValueType value)
		{
			return ErrorString(key, -1, true, operation, value);
		}

		private string IndexErrorString(int index, Operation operation, [Optional] ValueType value)
		{
			return ErrorString(default(KeyType), index, false, operation, value);
		}

		private string ErrorString(KeyType key, int index, bool usingKey, Operation operation, ValueType value)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			switch (operation)
			{
			case Operation.Get:
				text = "get value from";
				text2 = "invalid";
				break;
			case Operation.Set:
				text = string.Format("set value of '{0}' for", value);
				text4 = string.Format(".  Returning {0} value of {1}", typeof(ValueType), default(ValueType));
				text2 = "invalid";
				break;
			case Operation.Add:
				text = string.Format("add a new value of '{0}' to", value);
				text2 = "duplicate";
				break;
			default:
				Debug.LogError(string.Format("SMDT: Internal logic error: recieved unhandled Operation case of {0} - please extend the switch statement in SmartDict.ErrorString to include a {0}-case", operation));
				break;
			}
			text3 = ((!usingKey) ? string.Format("index: {0} (outside of current index range of 0 to {1})", index, keys.Length - 1) : string.Format("key: '{0}'", key));
			return string.Format("SMDT: attempt to {0} a SmartDict using the {1} {2}{3}", text, text2, text3, text4);
		}

		private bool IsValid(int index)
		{
			return index >= 0 && index < keys.Length;
		}
	}

	private class SmartCollection<Type> : ClassTypeSmartCollection<Type>
	{
		private const string classCode = "SMCL";

		private const string className = "Smart Collection";

		public SmartCollection(bool startFull)
		{
			InitializeSmartCollection(startFull, "SMCL", "Smart Collection");
		}

		public SmartCollection(int startingSize, bool startFull)
		{
			InitializeSmartCollection(startingSize, startFull, "SMCL", "Smart Collection");
		}

		public SmartCollection(Type[] startingValues, bool startFull)
		{
			InitializeSmartCollection(startingValues, startFull, "SMCL", "Smart Collection");
		}

		public void Extend(int? indexToInsert)
		{
			TryEnactExtend(indexToInsert);
		}

		public void Contract(int indexToRemove, int totalToRemove)
		{
			EnactContract(indexToRemove, totalToRemove);
		}

		public void Clear(bool removeEntries)
		{
			EnactClear(removeEntries);
		}
	}

	private static string IntArrayToString(int[] array)
	{
		string text;
		if (array == null)
		{
			text = "NULL";
		}
		else if (array.Length == 0)
		{
			text = "EMPTY";
		}
		else
		{
			text = array[0].ToString();
			for (int i = 1; i < array.Length; i++)
			{
				text = text + ", " + array[i];
			}
		}
		return text;
	}

	private static int[] CountOutArray(int upTo, int startingAt = 0)
	{
		int num = upTo + 1 - startingAt;
		int[] array;
		if (num <= 0)
		{
			array = null;
		}
		else
		{
			array = new int[num];
			int num2 = startingAt;
			for (int i = 0; i < num; i++)
			{
				array[i] = num2;
				num2++;
			}
		}
		return array;
	}
}
