using System;
using System.Globalization;
using UnityEngine;

public class MathUtils
{
	private struct TimeUnit
	{
		public int level;

		public string name;

		public float inNext;

		private string decimalFormat;

		public TimeUnit(int level, string name, float inNext, int decimalPlaces = 1)
		{
			this.level = level;
			this.name = name;
			this.inNext = inNext;
			decimalFormat = "0.0";
			if (decimalPlaces > 1)
			{
				for (int i = 1; i < decimalPlaces; i++)
				{
					decimalFormat += "0";
				}
			}
		}

		public string ToString(float ammount)
		{
			return string.Concat(str2: (name.Length == 1) ? name : ((name[-1] != 'y') ? (name + "s") : (name.Substring(name.Length - 2) + "ie")), str0: ammount.ToString(decimalFormat), str1: " ");
		}
	}

	public class RunningFloat
	{
		private bool minMode;

		private bool firstValue;

		public float result { get; private set; }

		public RunningFloat(bool minMode = false)
		{
			this.minMode = minMode;
			result = -1f;
			firstValue = true;
		}

		public bool CheckAgainst(float checkValue)
		{
			if (firstValue)
			{
				firstValue = false;
			}
			else if (minMode)
			{
				if (!(checkValue < result))
				{
					goto IL_0049;
				}
			}
			else if (!(checkValue > result))
			{
				goto IL_0049;
			}
			result = checkValue;
			return true;
			IL_0049:
			return false;
		}

		public void Add(float newValue)
		{
			CheckAgainst(newValue);
		}
	}

	public class RunningInt
	{
		private bool minMode;

		private bool firstValue;

		public int result { get; private set; }

		public RunningInt(bool minMode = false)
		{
			this.minMode = minMode;
			result = -1;
			firstValue = true;
		}

		public bool CheckAgainst(int checkValue)
		{
			if (firstValue)
			{
				firstValue = false;
			}
			else if (minMode)
			{
				if (checkValue >= result)
				{
					goto IL_0049;
				}
			}
			else if (checkValue <= result)
			{
				goto IL_0049;
			}
			result = checkValue;
			return true;
			IL_0049:
			return false;
		}

		public void Add(int newValue)
		{
			CheckAgainst(newValue);
		}
	}

	public class RangeInt
	{
		private float? minFloat;

		private float? maxFloat;

		private bool indexMode;

		public int Min { get; protected set; }

		public int Max { get; protected set; }

		public float MinAsFloat
		{
			get
			{
				if (!minFloat.HasValue)
				{
					minFloat = Min;
				}
				return minFloat.Value;
			}
			protected set
			{
				minFloat = value;
			}
		}

		public float MaxAsFloat
		{
			get
			{
				if (!maxFloat.HasValue)
				{
					maxFloat = Max;
				}
				return maxFloat.Value;
			}
			protected set
			{
				maxFloat = value;
			}
		}

		public int Length { get; protected set; }

		public int Random
		{
			get
			{
				return Randomizer.GetRandomInt(Min, Max);
			}
		}

		public RangeInt(int max)
		{
			InitializeIntRange(0, max, false);
		}

		public RangeInt(int min, int max)
		{
			InitializeIntRange(min, max, false);
		}

		public RangeInt(int max, bool indexMode)
		{
			InitializeIntRange(0, max, indexMode);
		}

		public RangeInt(int min, int max, bool indexMode)
		{
			InitializeIntRange(min, max, indexMode);
		}

		protected RangeInt()
		{
		}

		protected void InitializeIntRange(int min, int max, bool indexMode)
		{
			Min = min;
			Max = max;
			this.indexMode = indexMode;
			CalculateLength();
		}

		protected void CalculateLength()
		{
			Length = AbsInt(Max - Min);
			if (indexMode)
			{
				Length++;
			}
		}

		public virtual bool Contains(int queryValue)
		{
			return HasWithin(queryValue);
		}

		public virtual bool Contains(int queryValue, out bool wasBelow)
		{
			return HasWithin(queryValue, out wasBelow);
		}

		public virtual bool Contains(int queryValue, out int overshoot)
		{
			return HasWithin(queryValue, out overshoot);
		}

		public virtual bool ContainsNot(int queryValue)
		{
			return HasNotWithin(queryValue);
		}

		public virtual bool ContainsNot(int queryValue, out bool wasBelow)
		{
			return HasNotWithin(queryValue, out wasBelow);
		}

		public virtual bool ContainsNot(int queryValue, out int overshoot)
		{
			return HasNotWithin(queryValue, out overshoot);
		}

		public virtual bool HasWithin(int queryValue)
		{
			return IsWithin(queryValue, Min, Max);
		}

		public virtual bool HasWithin(int queryValue, out bool wasBelow)
		{
			return IsWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasWithin(int queryValue, out int overshoot)
		{
			return IsWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotWithin(int queryValue)
		{
			return IsNotWithin(queryValue, Min, Max);
		}

		public virtual bool HasNotWithin(int queryValue, out bool wasBelow)
		{
			return IsNotWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotWithin(int queryValue, out int overshoot)
		{
			return IsNotWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasBetween(int queryValue)
		{
			return IsBetween(queryValue, Min, Max);
		}

		public virtual bool HasBetween(int queryValue, out bool wasBelow)
		{
			return IsBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasBetween(int queryValue, out int overshoot)
		{
			return IsBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotBetween(int queryValue)
		{
			return IsNotBetween(queryValue, Min, Max);
		}

		public virtual bool HasNotBetween(int queryValue, out bool wasBelow)
		{
			return IsNotBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotBetween(int queryValue, out int overshoot)
		{
			return IsNotBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool DoesBorder(int queryValue)
		{
			return MathUtils.DoesBorder(queryValue, Min, Max);
		}

		public virtual bool DoesNotBorder(int queryValue)
		{
			return !DoesBorder(queryValue);
		}

		public virtual int Clamped(int queryValue)
		{
			return Clamp(queryValue, Min, Max);
		}

		public virtual int Indexed(int queryValue)
		{
			return MathUtils.Indexed(queryValue, Min, Max);
		}

		public virtual int Rebounded(int queryValue)
		{
			return MathUtils.Rebounded(queryValue, Min, Max);
		}

		public float ToPercent(int value)
		{
			return FromPercentInt(value, Min, Max);
		}

		public float ToPercent(int value, bool inverse)
		{
			return FromPercentInt(value, Min, Max, inverse);
		}

		public float ToPercent(float value)
		{
			return MathUtils.ToPercent(value, (float)Min, (float)Max);
		}

		public float ToPercent(float value, bool inverse)
		{
			return MathUtils.ToPercent(value, (float)Min, (float)Max, inverse);
		}

		public float FromPercent(float percent)
		{
			return FromPercentInt(percent, Min, Max);
		}

		public float FromPercent(float percent, bool inverse)
		{
			return FromPercentInt(percent, Min, Max, inverse);
		}

		public virtual void SetMin(int newMin)
		{
			Min = newMin;
			minFloat = null;
			CalculateLength();
		}

		public virtual void SetMax(int newMax)
		{
			Max = newMax;
			maxFloat = null;
			CalculateLength();
		}

		public virtual void SetMinMax(int newMin, int newMax)
		{
			Min = newMin;
			Max = newMax;
			minFloat = null;
			maxFloat = null;
			CalculateLength();
		}

		public virtual void ShiftMin(int by)
		{
			Min += by;
			minFloat = null;
			CalculateLength();
		}

		public virtual void ShiftMax(int by)
		{
			Max += by;
			maxFloat = null;
			CalculateLength();
		}

		public virtual void ShiftMinMax(int by)
		{
			Min += by;
			Max += by;
			minFloat = null;
			maxFloat = null;
			CalculateLength();
		}

		public virtual int GetAppendedWithin(int intToAppend)
		{
			return Shifted(intToAppend, 1, true);
		}

		public virtual int GetDescendedWithin(int intToDescend)
		{
			return Shifted(intToDescend, -1, true);
		}

		public virtual int GetAppendedBetween(int intToAppend)
		{
			return Shifted(intToAppend, 1, false);
		}

		public virtual int GetDescendedBetween(int intToDescend)
		{
			return Shifted(intToDescend, -1, false);
		}

		private int Shifted(int intToShift, int shiftAmnt, bool inclusive)
		{
			int num = intToShift + shiftAmnt;
			if ((inclusive && HasWithin(num)) || (!inclusive && HasBetween(num)))
			{
				return num;
			}
			return MathUtils.Indexed(num, Min, Max);
		}

		public int GetRandomWithin()
		{
			return Random;
		}

		public override string ToString()
		{
			return string.Format("Range: {0} to {1}, length: {2}", Min, Max, Length);
		}
	}

	public class Range
	{
		public float Min { get; private set; }

		public float Max { get; private set; }

		public float Length { get; private set; }

		public float Random
		{
			get
			{
				return Randomizer.GetRandom(Min, Max);
			}
		}

		public float Average
		{
			get
			{
				return Average(Min, Max);
			}
		}

		public float Distance
		{
			get
			{
				return Distance(Min, Max);
			}
		}

		public Range(float max)
		{
			InitializeRange(0f, max);
		}

		public Range(float min, float max)
		{
			InitializeRange(min, max);
		}

		protected void InitializeRange(float min, float max)
		{
			Min = min;
			Max = max;
			CalculateLength();
		}

		private void CalculateLength()
		{
			Length = Abs(Max - Min);
		}

		public virtual bool Contains(float queryValue)
		{
			return HasWithin(queryValue);
		}

		public virtual bool Contains(float queryValue, out bool wasBelow)
		{
			return HasWithin(queryValue, out wasBelow);
		}

		public virtual bool Contains(float queryValue, out float overshoot)
		{
			return HasWithin(queryValue, out overshoot);
		}

		public virtual bool Contains(float queryValue, out bool wasBelow, out float overshoot)
		{
			return HasWithin(queryValue, out wasBelow, out overshoot);
		}

		public virtual bool ContainsNot(float queryValue)
		{
			return HasNotWithin(queryValue);
		}

		public virtual bool ContainsNot(float queryValue, out bool wasBelow)
		{
			return HasNotWithin(queryValue, out wasBelow);
		}

		public virtual bool ContainsNot(float queryValue, out float overshoot)
		{
			return HasNotWithin(queryValue, out overshoot);
		}

		public virtual bool ContainsNot(float queryValue, out bool wasBelow, out float overshoot)
		{
			return HasNotWithin(queryValue, out wasBelow, out overshoot);
		}

		public virtual bool HasWithin(float queryValue)
		{
			return IsWithin(queryValue, Min, Max);
		}

		public virtual bool HasWithin(float queryValue, out bool wasBelow)
		{
			return IsWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasWithin(float queryValue, out float overshoot)
		{
			return IsWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasWithin(float queryValue, out bool wasBelow, out float overshoot)
		{
			return IsWithin(queryValue, Min, Max, out wasBelow, out overshoot);
		}

		public virtual bool HasNotWithin(float queryValue)
		{
			return IsNotWithin(queryValue, Min, Max);
		}

		public virtual bool HasNotWithin(float queryValue, out bool wasBelow)
		{
			return IsNotWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotWithin(float queryValue, out float overshoot)
		{
			return IsNotWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotWithin(float queryValue, out bool wasBelow, out float overshoot)
		{
			return IsNotWithin(queryValue, Min, Max, out wasBelow, out overshoot);
		}

		public virtual bool HasBetween(float queryValue)
		{
			return IsBetween(queryValue, Min, Max);
		}

		public virtual bool HasBetween(float queryValue, out bool wasBelow)
		{
			return IsBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasBetween(float queryValue, out float overshoot)
		{
			return IsBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasBetween(float queryValue, out bool wasBelow, out float overshoot)
		{
			return IsBetween(queryValue, Min, Max, out wasBelow, out overshoot);
		}

		public virtual bool HasNotBetween(float queryValue)
		{
			return IsNotBetween(queryValue, Min, Max);
		}

		public virtual bool HasNotBetween(float queryValue, out bool wasBelow)
		{
			return IsNotBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotBetween(float queryValue, out float overshoot)
		{
			return IsNotBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotBetween(float queryValue, out bool wasBelow, out float overshoot)
		{
			return IsNotBetween(queryValue, Min, Max, out wasBelow, out overshoot);
		}

		public virtual bool DoesBorder(float queryValue)
		{
			return MathUtils.DoesBorder(queryValue, (float?)Min, (float?)Max);
		}

		public virtual bool DoesNotBorder(float queryValue)
		{
			return !DoesBorder(queryValue);
		}

		public virtual float Clamped(float queryValue)
		{
			return Clamp(queryValue, Min, Max);
		}

		public virtual float Indexed(float queryValue)
		{
			return IndexedFloat(queryValue, Min, Max);
		}

		public virtual float Rebounded(float queryValue)
		{
			return ReboundedFloat(queryValue, Min, Max);
		}

		public float ToPercent(float value)
		{
			return MathUtils.ToPercent(value, Min, Max);
		}

		public float ToPercent(float value, bool inverse)
		{
			return MathUtils.ToPercent(value, Min, Max, inverse);
		}

		public float ToPercent01(float value)
		{
			return MathUtils.ToPercent01(value, Min, Max);
		}

		public float ToPercent01(float value, bool inverse)
		{
			return MathUtils.ToPercent01(value, Min, Max, inverse);
		}

		public float FromPercent(float percent)
		{
			return MathUtils.FromPercent(percent, Min, Max);
		}

		public float FromPercent(float percent, bool inverse)
		{
			return MathUtils.FromPercent(percent, Min, Max, inverse);
		}

		public float FromPercent01(float percent)
		{
			return MathUtils.FromPercent01(percent, Min, Max);
		}

		public float FromPercent01(float percent, bool inverse)
		{
			return MathUtils.FromPercent01(percent, Min, Max, inverse);
		}

		public virtual void SetMin(float newMin)
		{
			Min = newMin;
			CalculateLength();
		}

		public virtual void SetMax(float newMax)
		{
			Max = newMax;
			CalculateLength();
		}

		public virtual void Set(float newMin, float newMax)
		{
			Min = newMin;
			Max = newMax;
			CalculateLength();
		}

		public virtual void ShiftMin(float by)
		{
			Min += by;
			CalculateLength();
		}

		public virtual void ShiftMax(float by)
		{
			Max += by;
			CalculateLength();
		}

		public virtual void Shift(float by)
		{
			Min += by;
			Max += by;
			CalculateLength();
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public string ToString(bool includeLength)
		{
			if (includeLength)
			{
				return string.Format("Range: {0} to {1}, length: {2}", Min, Max, Length);
			}
			return string.Format("Range: {0} to {1}", Min, Max);
		}
	}

	public class RangeNullable
	{
		public float? Min { get; private set; }

		public float? Max { get; private set; }

		public bool HasValues { get; private set; }

		public bool HasAValue { get; private set; }

		public bool HasNotValues { get; private set; }

		public bool HasNoValues { get; private set; }

		public float? Length { get; private set; }

		public float Random
		{
			get
			{
				return (!HasValues) ? (-1f) : Randomizer.GetRandom(Min.Value, Max.Value);
			}
		}

		public float Average
		{
			get
			{
				return (!HasValues) ? (-1f) : Average(Min.Value, Max.Value);
			}
		}

		public float Distance
		{
			get
			{
				return (!HasValues) ? (-1f) : Distance(Min.Value, Max.Value);
			}
		}

		public RangeNullable(int? min, int? max)
		{
			float? min2 = null;
			float? max2 = null;
			if (min.HasValue)
			{
				min2 = min.Value;
			}
			if (max.HasValue)
			{
				max2 = max.Value;
			}
			InitializeRange(min2, max2);
		}

		public RangeNullable(float? min, float? max)
		{
			InitializeRange(min, max);
		}

		protected void InitializeRange(float? min, float? max)
		{
			if (min.HasValue && max.HasValue)
			{
				if (min.HasValue && max.HasValue && min.Value < max.Value)
				{
					Min = min;
					Max = max;
				}
				else
				{
					Min = max;
					Max = min;
				}
			}
			else
			{
				Min = min;
				Max = max;
			}
			CalculateLength();
		}

		private void CalculateLength()
		{
			HasValues = Min.HasValue && Max.HasValue;
			HasAValue = Min.HasValue || Max.HasValue;
			HasNotValues = !HasValues;
			HasNoValues = !HasAValue;
			if (HasValues)
			{
				Length = Abs(Max.Value - Min.Value);
			}
			else
			{
				Length = null;
			}
		}

		public virtual bool Contains(float queryValue)
		{
			return HasWithin(queryValue);
		}

		public virtual bool Contains(float queryValue, out bool wasBelow)
		{
			return HasWithin(queryValue, out wasBelow);
		}

		public virtual bool Contains(float queryValue, out float overshoot)
		{
			return HasWithin(queryValue, out overshoot);
		}

		public virtual bool ContainsNot(float queryValue)
		{
			return HasNotWithin(queryValue);
		}

		public virtual bool ContainsNot(float queryValue, out bool wasBelow)
		{
			return HasNotWithin(queryValue, out wasBelow);
		}

		public virtual bool ContainsNot(float queryValue, out float overshoot)
		{
			return HasNotWithin(queryValue, out overshoot);
		}

		public virtual bool HasWithin(float queryValue)
		{
			return IsWithin(queryValue, Min, Max);
		}

		public virtual bool HasWithin(float queryValue, out bool wasBelow)
		{
			return IsWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasWithin(float queryValue, out float overshoot)
		{
			return IsWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotWithin(float queryValue)
		{
			return IsNotWithin(queryValue, Min, Max);
		}

		public virtual bool HasNotWithin(float queryValue, out bool wasBelow)
		{
			return IsNotWithin(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotWithin(float queryValue, out float overshoot)
		{
			return IsNotWithin(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasBetween(float queryValue)
		{
			return IsBetween(queryValue, Min, Max);
		}

		public virtual bool HasBetween(float queryValue, out bool wasBelow)
		{
			return IsBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasBetween(float queryValue, out float overshoot)
		{
			return IsBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool HasNotBetween(float queryValue)
		{
			return IsNotBetween(queryValue, Min, Max);
		}

		public virtual bool HasNotBetween(float queryValue, out bool wasBelow)
		{
			return IsNotBetween(queryValue, Min, Max, out wasBelow);
		}

		public virtual bool HasNotBetween(float queryValue, out float overshoot)
		{
			return IsNotBetween(queryValue, Min, Max, out overshoot);
		}

		public virtual bool DoesBorder(float queryValue)
		{
			return MathUtils.DoesBorder(queryValue, Min, Max);
		}

		public virtual bool DoesNotBorder(float queryValue)
		{
			return !DoesBorder(queryValue);
		}

		public virtual float Clamped(float queryValue)
		{
			return Clamp(queryValue, Min, Max);
		}

		public float ToPercent(float value)
		{
			return (!HasValues) ? (-1f) : MathUtils.ToPercent(value, Min.Value, Max.Value);
		}

		public float ToPercent(float value, bool inverse)
		{
			return (!HasValues) ? (-1f) : MathUtils.ToPercent(value, Min.Value, Max.Value, inverse);
		}

		public float FromPercent(float percent)
		{
			return (!HasValues) ? (-1f) : MathUtils.FromPercent(percent, Min.Value, Max.Value);
		}

		public float FromPercent(float percent, bool inverse)
		{
			return (!HasValues) ? (-1f) : MathUtils.FromPercent(percent, Min.Value, Max.Value, inverse);
		}

		public virtual void SetMin(float? newMin)
		{
			Min = newMin;
			CalculateLength();
		}

		public virtual void SetMax(float? newMax)
		{
			Max = newMax;
			CalculateLength();
		}

		public virtual void Set(float? newMin, float? newMax)
		{
			Min = newMin;
			Max = newMax;
			CalculateLength();
		}

		public virtual void ShiftMin(float ammount, bool shiftNull = false)
		{
			AlterMinBy(ammount, shiftNull, false);
			CalculateLength();
		}

		public virtual void ShiftMax(float ammount, bool shiftNull = false)
		{
			AlterMaxBy(ammount, shiftNull, false);
			CalculateLength();
		}

		public virtual void Shift(float ammount, bool shiftNull = false)
		{
			AlterBy(ammount, ammount, shiftNull, false, false);
		}

		public virtual void Compress(float ammount, bool shiftNull = false)
		{
			AlterBy(ammount, ammount, shiftNull, false, true);
		}

		public virtual void Expand(float ammount, bool shiftNull = false)
		{
			AlterBy(ammount, ammount, shiftNull, true, false);
		}

		public virtual void Shift(float minsAmmount, float maxsAmmount, bool shiftNull = false)
		{
			AlterBy(minsAmmount, maxsAmmount, shiftNull, false, false);
		}

		public virtual void Compress(float minsAmmount, float maxsAmmount, bool shiftNull = false)
		{
			AlterBy(minsAmmount, maxsAmmount, shiftNull, false, true);
		}

		public virtual void Expand(float minsAmmount, float maxsAmmount, bool shiftNull = false)
		{
			AlterBy(minsAmmount, maxsAmmount, shiftNull, true, false);
		}

		private void AlterBy(float minsAmmount, float maxsAmmount, bool shiftNull, bool reverseShiftMin, bool reverseShiftMax)
		{
			AlterMinBy(minsAmmount, shiftNull, reverseShiftMin);
			AlterMaxBy(maxsAmmount, shiftNull, reverseShiftMax);
			CalculateLength();
		}

		private void AlterMinBy(float ammount, bool shiftNull, bool reverseShift)
		{
			if (Min.HasValue)
			{
				if (reverseShift)
				{
					float? min = Min;
					Min = ((!min.HasValue) ? ((float?)null) : new float?(min.Value - ammount));
				}
				else
				{
					float? min2 = Min;
					Min = ((!min2.HasValue) ? ((float?)null) : new float?(min2.Value + ammount));
				}
			}
			else if (shiftNull)
			{
				Min = ammount;
			}
		}

		private void AlterMaxBy(float ammount, bool shiftNull, bool reverseShift)
		{
			if (Max.HasValue)
			{
				if (reverseShift)
				{
					float? max = Max;
					Max = ((!max.HasValue) ? ((float?)null) : new float?(max.Value - ammount));
				}
				else
				{
					float? max2 = Max;
					Max = ((!max2.HasValue) ? ((float?)null) : new float?(max2.Value + ammount));
				}
			}
			else if (shiftNull)
			{
				Max = ammount;
			}
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public string ToString(bool includeLength)
		{
			if (HasValues)
			{
				if (includeLength)
				{
					return string.Format("{0} to {1}, length: {2}", Min, Max, Length);
				}
				return string.Format("{0} to {1}", Min, Max);
			}
			if (HasNoValues)
			{
				return "Null";
			}
			string arg = ((!Min.HasValue) ? "Null" : Min.ToString());
			string arg2 = ((!Max.HasValue) ? "Null" : Max.ToString());
			return string.Format("{0} to {1}", arg, arg2);
		}
	}

	public class RangePair
	{
		private Range rangeX;

		private Range rangeY;

		public float MinX
		{
			get
			{
				return rangeX.Min;
			}
		}

		public float MinY
		{
			get
			{
				return rangeY.Min;
			}
		}

		public float MaxX
		{
			get
			{
				return rangeX.Max;
			}
		}

		public float MaxY
		{
			get
			{
				return rangeY.Max;
			}
		}

		public Vector2 Random
		{
			get
			{
				return new Vector2(rangeX.Random, rangeY.Random);
			}
		}

		public float RandomX
		{
			get
			{
				return rangeX.Random;
			}
		}

		public float RandomY
		{
			get
			{
				return rangeY.Random;
			}
		}

		public Vector2 Average
		{
			get
			{
				return new Vector2(rangeX.Average, rangeY.Average);
			}
		}

		public float AverageX
		{
			get
			{
				return rangeX.Average;
			}
		}

		public float AverageY
		{
			get
			{
				return rangeY.Average;
			}
		}

		public Vector2 Distance
		{
			get
			{
				return new Vector2(rangeX.Distance, rangeY.Distance);
			}
		}

		public float DistanceX
		{
			get
			{
				return rangeX.Distance;
			}
		}

		public float DistanceY
		{
			get
			{
				return rangeY.Distance;
			}
		}

		public RangePair(float maxX, float maxY)
		{
			InitializeRangePair(0f, maxX, 0f, maxY);
		}

		public RangePair(float minX, float maxX, float minY, float maxY)
		{
			InitializeRangePair(minX, maxX, minY, maxY);
		}

		protected void InitializeRangePair(float minX, float maxX, float minY, float maxY)
		{
			rangeX = new Range(minX, maxX);
			rangeY = new Range(minY, maxY);
		}

		public bool Contains(Vector2 queryValues)
		{
			return rangeX.Contains(queryValues.x) && rangeY.Contains(queryValues.y);
		}

		public bool Contains(float queryValueX, float queryValueY)
		{
			return rangeX.Contains(queryValueX) && rangeY.Contains(queryValueY);
		}

		public bool ContainsX(float queryValueX)
		{
			return rangeX.Contains(queryValueX);
		}

		public bool ContainsY(float queryValueY)
		{
			return rangeY.Contains(queryValueY);
		}

		public bool ContainsNot(Vector2 queryValues)
		{
			return rangeX.ContainsNot(queryValues.x) && rangeY.ContainsNot(queryValues.y);
		}

		public bool ContainsNot(float queryValueX, float queryValueY)
		{
			return rangeX.ContainsNot(queryValueX) && rangeY.ContainsNot(queryValueY);
		}

		public bool ContainsNotX(float queryValueX)
		{
			return rangeX.ContainsNot(queryValueX);
		}

		public bool ContainsNotY(float queryValueY)
		{
			return rangeY.ContainsNot(queryValueY);
		}

		public Vector2 Clamp(Vector2 values)
		{
			return new Vector2(rangeX.Clamped(values.x), rangeY.Clamped(values.y));
		}

		public Vector2 Clamp(float valueX, float valueY)
		{
			return new Vector2(rangeX.Clamped(valueX), rangeY.Clamped(valueY));
		}

		public float ClampX(float valueX)
		{
			return rangeX.Clamped(valueX);
		}

		public float ClampY(float valueY)
		{
			return rangeY.Clamped(valueY);
		}

		public Vector2 ToPercent(Vector2 values)
		{
			return new Vector2(rangeX.ToPercent(values.x), rangeY.ToPercent(values.y));
		}

		public Vector2 ToPercent(Vector2 values, bool inverse)
		{
			return new Vector2(rangeX.ToPercent(values.x, inverse), rangeY.ToPercent(values.y, inverse));
		}

		public Vector2 ToPercent(float valuesX, float valueY)
		{
			return new Vector2(rangeX.ToPercent(valuesX), rangeY.ToPercent(valueY));
		}

		public Vector2 ToPercent(float valuesX, float valueY, bool inverse)
		{
			return new Vector2(rangeX.ToPercent(valuesX, inverse), rangeY.ToPercent(valueY, inverse));
		}

		public float ToPercentX(float valueX)
		{
			return rangeX.ToPercent(valueX);
		}

		public float ToPercentX(float valueX, bool inverse)
		{
			return rangeX.ToPercent(valueX, inverse);
		}

		public float ToPercentY(float valueY)
		{
			return rangeX.ToPercent(valueY);
		}

		public float ToPercentY(float valueY, bool inverse)
		{
			return rangeX.ToPercent(valueY, inverse);
		}

		public Vector2 FromPercent(Vector2 percents)
		{
			return new Vector2(rangeX.FromPercent(percents.x), rangeY.FromPercent(percents.y));
		}

		public Vector2 FromPercent(Vector2 percents, bool inverse)
		{
			return new Vector2(rangeX.FromPercent(percents.x, inverse), rangeY.FromPercent(percents.y, inverse));
		}

		public Vector2 FromPercent(float percentX, float percentY)
		{
			return new Vector2(rangeX.FromPercent(percentX), rangeY.FromPercent(percentY));
		}

		public Vector2 FromPercent(float percentX, float percentY, bool inverse)
		{
			return new Vector2(rangeX.FromPercent(percentX, inverse), rangeY.FromPercent(percentY, inverse));
		}

		public float FromPercentX(float percentX)
		{
			return rangeX.FromPercent(percentX);
		}

		public float FromPercentX(float percentX, bool inverse)
		{
			return rangeX.FromPercent(percentX, inverse);
		}

		public float FromPercentY(float percentY)
		{
			return rangeX.FromPercent(percentY);
		}

		public float FromPercentY(float percentY, bool inverse)
		{
			return rangeX.FromPercent(percentY, inverse);
		}

		public bool DoesBorder(Vector2 values, bool requireBoth = false)
		{
			if (requireBoth)
			{
				return rangeX.DoesBorder(values.x) && rangeY.DoesBorder(values.y);
			}
			return rangeX.DoesBorder(values.x) || rangeY.DoesBorder(values.y);
		}

		public bool DoesBorder(float valueX, float valueY, bool requireBoth = false)
		{
			if (requireBoth)
			{
				return rangeX.DoesBorder(valueX) && rangeY.DoesBorder(valueY);
			}
			return rangeX.DoesBorder(valueX) || rangeY.DoesBorder(valueY);
		}

		public bool DoesBorderX(float valueX)
		{
			return rangeX.DoesBorder(valueX);
		}

		public bool DoesBorderY(float valueY)
		{
			return rangeX.DoesBorder(valueY);
		}

		public virtual void SetMinX(float newMinX)
		{
			rangeX.SetMin(newMinX);
		}

		public virtual void SetMaxX(float newMaxX)
		{
			rangeY.SetMax(newMaxX);
		}

		public virtual void SetMinY(float newMinY)
		{
			rangeX.SetMin(newMinY);
		}

		public virtual void SetMaxY(float newMaxY)
		{
			rangeY.SetMax(newMaxY);
		}

		public virtual void SetX(float newMinX, float newMaxX)
		{
			rangeX.Set(newMinX, newMaxX);
		}

		public virtual void SetY(float newMinY, float newMaxY)
		{
			rangeY.Set(newMinY, newMaxY);
		}

		public virtual void ShiftMinX(float byX)
		{
			rangeX.ShiftMin(byX);
		}

		public virtual void ShiftMaxX(float byX)
		{
			rangeX.ShiftMax(byX);
		}

		public virtual void ShiftMinY(float byY)
		{
			rangeY.ShiftMin(byY);
		}

		public virtual void ShiftMaxY(float byY)
		{
			rangeY.ShiftMax(byY);
		}

		public virtual void ShiftMax(Vector2 by)
		{
			rangeX.ShiftMax(by.x);
			rangeX.ShiftMax(by.y);
		}

		public virtual void ShiftMax(float byX, float byY)
		{
			rangeX.ShiftMax(byX);
			rangeX.ShiftMax(byY);
		}

		public virtual void ShiftMax(float by)
		{
			rangeX.ShiftMax(by);
			rangeX.ShiftMax(by);
		}

		public virtual void Shift(Vector2 by)
		{
			rangeX.Shift(by.x);
			rangeY.Shift(by.y);
		}

		public virtual void Shift(float byX, float byY)
		{
			rangeX.Shift(byX);
			rangeY.Shift(byY);
		}

		public virtual void Shift(float by)
		{
			rangeX.Shift(by);
			rangeY.Shift(by);
		}

		public virtual void ShiftX(float by)
		{
			rangeX.Shift(by);
		}

		public virtual void ShiftY(float by)
		{
			rangeY.Shift(by);
		}

		public override string ToString()
		{
			return ToString(true, false);
		}

		public string ToString(bool bracket)
		{
			return ToString(bracket, false);
		}

		public string ToString(bool bracket, bool includeLengths)
		{
			if (bracket)
			{
				if (includeLengths)
				{
					return string.Format("(RangePair: X {0} to {1}, length: {2}; Y {3} to {4}, length: {5})", rangeX.Min, rangeX.Max, rangeX.Length, rangeY.Min, rangeY.Max, rangeY.Length);
				}
				return string.Format("(RangePair: X {0} to {1}, Y {2} to {3})", rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max);
			}
			if (includeLengths)
			{
				return string.Format("RangePair: X {0} to {1}, length: {2}, Y {3} to {4}, length: {5}", rangeX.Min, rangeX.Max, rangeX.Length, rangeY.Min, rangeY.Max, rangeY.Length);
			}
			return string.Format("RangePair: X {0} to {1}, Y {2} to {3}", rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max);
		}

		public string ToStringX()
		{
			return rangeX.ToString();
		}

		public string ToStringX(bool includeLength)
		{
			return rangeX.ToString(includeLength);
		}

		public string ToStringY()
		{
			return rangeY.ToString();
		}

		public string ToStringY(bool includeLength)
		{
			return rangeY.ToString(includeLength);
		}
	}

	public class Index : RangeInt
	{
		private int startingValue;

		public int Value { get; private set; }

		public bool IsValid { get; private set; }

		public bool IsntValid
		{
			get
			{
				return !IsValid;
			}
		}

		public Index(int arrayLength)
		{
			InitializeIndex(0, arrayLength - 1, 0);
		}

		public Index(int arrayLength, int startAt)
		{
			InitializeIndex(0, arrayLength - 1, startAt);
		}

		public Index(int minIndex, int maxIndex, int startAt)
		{
			InitializeIndex(minIndex, maxIndex, startAt);
		}

		public Index(UnityEngine.Object[] array)
		{
			InitializeIndex(0, GetLength(array) - 1, 0);
		}

		public Index(UnityEngine.Object[] array, int startAt)
		{
			InitializeIndex(0, GetLength(array) - 1, startAt);
		}

		private void InitializeIndex(int min, int max, int startAt)
		{
			InitializeIntRange(min, max, true);
			IsValid = max > 0 && min >= 0 && max >= min;
			startingValue = startAt;
			Reset();
		}

		public bool Append()
		{
			return ShiftIndex(1);
		}

		public bool Descend()
		{
			return ShiftIndex(-1);
		}

		public bool Append(int by)
		{
			return ShiftIndex(by);
		}

		public bool Descend(int by)
		{
			return ShiftIndex(-by);
		}

		public int GetAppended()
		{
			return ShiftedIndex(1);
		}

		public int GetDescended()
		{
			return ShiftedIndex(-1);
		}

		public int GetAppended(int by)
		{
			return ShiftedIndex(by);
		}

		public int GetDescended(int by)
		{
			return ShiftedIndex(-by);
		}

		public int GetNext()
		{
			return GetAppended();
		}

		public int GetPrevious()
		{
			return GetDescended();
		}

		private bool ShiftIndex(int by)
		{
			if (IsntValid)
			{
				return false;
			}
			Value += by;
			Value = Indexed(Value);
			return true;
		}

		private int ShiftedIndex(int by)
		{
			if (ShiftIndex(by))
			{
				return Value;
			}
			return -1;
		}

		public bool Set(Index to)
		{
			return Set(to.Value);
		}

		public bool Set(int? to)
		{
			return Set(to.Value);
		}

		public bool Set(int to)
		{
			if (HasWithin(to))
			{
				Value = to;
				return true;
			}
			Value = Indexed(to);
			return false;
		}

		public void Reset()
		{
			Set(startingValue);
		}

		public override int Indexed(int value)
		{
			return MathUtils.Indexed(value, base.Min, base.Max);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}

	public abstract class ClasstypeBuffers
	{
		protected int NextFreeIndex;

		protected bool isFilled;

		protected bool hasMultipleValues;

		public int LengthMax { get; protected set; }

		public int TotalStored { get; protected set; }

		protected int LastIndex
		{
			get
			{
				return NextFreeIndex - 1;
			}
		}

		public bool IsEmpty { get; protected set; }

		protected void InitializeBuffer(int length, bool clearCurrentValues = true)
		{
			if (length <= 0)
			{
				Debug.LogError(string.Format("MTUT: CTBF: ERROR: attempt to create a buffer of length {0} - overriding to length of 1", length));
				length = 1;
			}
			LengthMax = length;
			if (clearCurrentValues)
			{
				Clear();
			}
		}

		public void Clear()
		{
			NextFreeIndex = 0;
			TotalStored = 0;
			isFilled = false;
			IsEmpty = true;
			hasMultipleValues = false;
		}

		protected void AppendTotalsAndFlags()
		{
			NextFreeIndex++;
			TotalStored++;
			if (NextFreeIndex >= LengthMax)
			{
				isFilled = true;
			}
			if (IsEmpty)
			{
				IsEmpty = false;
			}
			if (!hasMultipleValues && TotalStored > 1)
			{
				hasMultipleValues = true;
			}
		}
	}

	public class BufferInt : ClasstypeBuffers
	{
		private enum Operation
		{
			Minimum = 0,
			Maximum = 1,
			Smallest = 2,
			Largest = 3
		}

		private int[] array;

		public BufferInt(int length)
		{
			array = new int[length];
			InitializeBuffer(length);
		}

		public void SetNewLength(int newLength, bool clearCurrentValues = true)
		{
			array = new int[newLength];
			InitializeBuffer(newLength, clearCurrentValues);
		}

		public void Add(int valueToAdd)
		{
			if (isFilled)
			{
				for (int i = 0; i < base.LengthMax - 1; i++)
				{
					array[i] = array[i + 1];
				}
				array[base.LengthMax - 1] = valueToAdd;
				return;
			}
			try
			{
				array[NextFreeIndex] = valueToAdd;
			}
			catch
			{
				Debug.LogError(string.Format("MTUT: INBF: ERROR: attempt to add value {0} to index {1} of array of length {2}", valueToAdd, NextFreeIndex, array.Length));
			}
			AppendTotalsAndFlags();
		}

		public float AddAndGetAverage(int valueToAdd)
		{
			Add(valueToAdd);
			return GetAverage();
		}

		public int AddAndGetMinimum(int valueToAdd)
		{
			Add(valueToAdd);
			return GetMinimum();
		}

		public int AddAndGetMaximum(int valueToAdd)
		{
			Add(valueToAdd);
			return GetMaximum();
		}

		public int AddAndGetSmallest(int valueToAdd)
		{
			Add(valueToAdd);
			return GetSmallest();
		}

		public int AddAndGetLargest(int valueToAdd)
		{
			Add(valueToAdd);
			return GetLargest();
		}

		public int GetMinimum()
		{
			return Get(Operation.Minimum);
		}

		public int GetMaximum()
		{
			return Get(Operation.Maximum);
		}

		public int GetSmallest()
		{
			return Get(Operation.Smallest);
		}

		public int GetLargest()
		{
			return Get(Operation.Largest);
		}

		private int Get(Operation goal)
		{
			if (base.IsEmpty)
			{
				return -1;
			}
			if (!hasMultipleValues)
			{
				return array[0];
			}
			int num = array[0];
			if (goal == Operation.Smallest || goal == Operation.Largest)
			{
				num = AbsInt(num);
			}
			for (int i = 1; i <= base.LastIndex; i++)
			{
				switch (goal)
				{
				case Operation.Minimum:
					if (array[i] < num)
					{
						num = array[i];
					}
					break;
				case Operation.Maximum:
					if (array[i] > num)
					{
						num = array[i];
					}
					break;
				case Operation.Smallest:
					if (AbsInt(array[i]) < num)
					{
						num = array[i];
					}
					break;
				case Operation.Largest:
					if (AbsInt(array[i]) > num)
					{
						num = array[i];
					}
					break;
				}
			}
			return num;
		}

		private float GetAverage()
		{
			if (base.IsEmpty)
			{
				return -1f;
			}
			if (!hasMultipleValues)
			{
				return array[0];
			}
			int num = array[0];
			for (int i = 1; i <= base.LastIndex; i++)
			{
				num += array[i];
			}
			return (float)num / (float)base.TotalStored;
		}

		public int GetLast()
		{
			return (!base.IsEmpty) ? array[base.LastIndex] : (-1);
		}

		public bool Contains(int queryValue)
		{
			if (base.IsEmpty)
			{
				return false;
			}
			for (int i = 0; i <= base.LastIndex; i++)
			{
				if (array[i] == queryValue)
				{
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			string text = "IntBuffer: [";
			if (base.IsEmpty)
			{
				return text + "empty]";
			}
			text += array[0];
			if (array.Length != 1)
			{
				for (int i = 1; i < array.Length; i++)
				{
					text = text + ", " + array[i];
				}
			}
			return text + "]";
		}
	}

	public class Buffer : ClasstypeBuffers
	{
		private enum Operation
		{
			Minimum = 0,
			Maximum = 1,
			Smallest = 2,
			Largest = 3,
			Average = 4
		}

		private float[] array;

		public Buffer(int length)
		{
			array = new float[length];
			InitializeBuffer(length);
		}

		public void SetNew(int newLength, bool clearCurrentValues = true)
		{
			array = new float[newLength];
			InitializeBuffer(newLength, clearCurrentValues);
		}

		public void Add(float valueToAdd)
		{
			if (isFilled)
			{
				for (int i = 0; i < base.LengthMax - 1; i++)
				{
					array[i] = array[i + 1];
				}
				array[base.LengthMax - 1] = valueToAdd;
				return;
			}
			try
			{
				array[NextFreeIndex] = valueToAdd;
			}
			catch
			{
				Debug.LogError(string.Format("MTUT: FLBF: ERROR: attempt to add value {0} to index {1} of array of length {2}", valueToAdd, NextFreeIndex, array.Length));
			}
			AppendTotalsAndFlags();
		}

		public float AddAndGetAverage(float valueToAdd)
		{
			Add(valueToAdd);
			return GetAverage();
		}

		public float AddAndGetMinimum(float valueToAdd)
		{
			Add(valueToAdd);
			return GetMinimum();
		}

		public float AddAndGetMaximum(float valueToAdd)
		{
			Add(valueToAdd);
			return GetMaximum();
		}

		public float AddAndGetSmallest(float valueToAdd)
		{
			Add(valueToAdd);
			return GetSmallest();
		}

		public float AddAndGetLargest(float valueToAdd)
		{
			Add(valueToAdd);
			return GetLargest();
		}

		public float GetAverage()
		{
			return Get(Operation.Average);
		}

		public float GetMinimum()
		{
			return Get(Operation.Minimum);
		}

		public float GetMaximum()
		{
			return Get(Operation.Maximum);
		}

		public float GetSmallest()
		{
			return Get(Operation.Smallest);
		}

		public float GetLargest()
		{
			return Get(Operation.Largest);
		}

		private float Get(Operation goal)
		{
			if (base.IsEmpty)
			{
				return -1f;
			}
			if (!hasMultipleValues)
			{
				return array[0];
			}
			float num = array[0];
			if (goal == Operation.Smallest || goal == Operation.Largest)
			{
				num = Abs(num);
			}
			for (int i = 1; i <= base.LastIndex; i++)
			{
				switch (goal)
				{
				case Operation.Minimum:
					if (array[i] < num)
					{
						num = array[i];
					}
					break;
				case Operation.Maximum:
					if (array[i] > num)
					{
						num = array[i];
					}
					break;
				case Operation.Smallest:
					if (Abs(array[i]) < num)
					{
						num = array[i];
					}
					break;
				case Operation.Largest:
					if (Abs(array[i]) > num)
					{
						num = array[i];
					}
					break;
				case Operation.Average:
					num += array[i];
					break;
				}
			}
			if (goal == Operation.Average)
			{
				num /= (float)base.TotalStored;
			}
			return num;
		}

		public float GetLast()
		{
			return (!base.IsEmpty) ? array[base.LastIndex] : (-1f);
		}

		public bool Contains(int queryValue)
		{
			if (base.IsEmpty)
			{
				return false;
			}
			for (int i = 0; i <= base.LastIndex; i++)
			{
				if (array[i] == (float)queryValue)
				{
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			string text = "Buffer: [";
			if (base.IsEmpty)
			{
				return text + "empty]";
			}
			text += array[0].ToString("0.00");
			if (array.Length != 1)
			{
				for (int i = 1; i < array.Length; i++)
				{
					text = text + ", " + array[i];
				}
			}
			return text + "]";
		}
	}

	public class IntFloatSort
	{
		public readonly int Length;

		public bool IsEmpty;

		private IntFloat[] sortedArray;

		public int LastIndex { get; private set; }

		public int this[int index]
		{
			get
			{
				if (ContainsIndex(index))
				{
					return sortedArray[index].intValue;
				}
				return -1;
			}
		}

		public IntFloatSort(int length)
		{
			Length = length;
			sortedArray = new IntFloat[Length];
			LastIndex = -1;
			IsEmpty = true;
		}

		public IntFloatSort(params float[] orderedFloatValues)
		{
			Length = orderedFloatValues.Length;
			sortedArray = new IntFloat[Length];
			LastIndex = -1;
			for (int i = 0; i < Length; i++)
			{
				Add(orderedFloatValues[i]);
			}
		}

		public void Add(float nextFloat)
		{
			LastIndex++;
			sortedArray[LastIndex] = new IntFloat(LastIndex, nextFloat);
			for (int num = LastIndex; num >= 1; num--)
			{
				if (sortedArray[num - 1].floatValue > sortedArray[num].floatValue)
				{
					IntFloat intFloat = sortedArray[num];
					sortedArray[num] = sortedArray[num - 1];
					sortedArray[num - 1] = intFloat;
				}
			}
		}

		public void Clear()
		{
			LastIndex = -1;
			IsEmpty = true;
		}

		private bool ContainsIndex(int queryIndex)
		{
			return queryIndex >= 0 && queryIndex <= LastIndex;
		}

		public int[] GetAsArray()
		{
			int[] array = new int[LastIndex + 1];
			for (int i = 0; i <= LastIndex; i++)
			{
				array[i] = sortedArray[i].intValue;
			}
			return array;
		}

		public override string ToString()
		{
			return ToString(", ");
		}

		public string ToString(string elementSpacer)
		{
			string text = null;
			if (LastIndex <= -1)
			{
				text = "Null";
			}
			else
			{
				for (int i = 0; i <= LastIndex; i++)
				{
					string text2 = sortedArray[i].ToString();
					text = ((text != null) ? (text + elementSpacer + text2) : text2);
				}
			}
			return text;
		}
	}

	public struct IntPair
	{
		public int x;

		public int y;

		public int Product
		{
			get
			{
				return x * y;
			}
		}

		public static IntPair Zero
		{
			get
			{
				return new IntPair(0, 0);
			}
		}

		public static IntPair One
		{
			get
			{
				return new IntPair(1, 1);
			}
		}

		public IntPair(int x)
		{
			this.x = x;
			y = 0;
		}

		public IntPair(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public IntPair(float x, float y)
		{
			this.x = Floored(x);
			this.y = Floored(y);
		}

		public IntPair(Vector2 xy)
		{
			x = Floored(xy.x);
			y = Floored(xy.y);
		}

		public IntPair(Vector3 xy)
		{
			x = Floored(xy.x);
			y = Floored(xy.y);
		}

		public Vector2 ToVector()
		{
			return new Vector2(x, y);
		}

		public bool Equals(IntPair otherIntPair)
		{
			return x == otherIntPair.x && y == otherIntPair.y;
		}

		public bool Equals(int x)
		{
			return this.x == x;
		}

		public bool Equals(int x, int y)
		{
			return this.x == x && this.y == y;
		}

		public bool Equals(Vector2 xy)
		{
			return (float)x == xy.x && (float)y == xy.y;
		}

		public int[] ToArray()
		{
			return new int[2] { x, y };
		}

		public static bool AreEqual(IntPair first, IntPair second)
		{
			return first.x == second.x && first.y == second.y;
		}

		public static bool AreNotEqual(IntPair first, IntPair second)
		{
			return !AreEqual(first, second);
		}

		public static bool Equals(IntPair intPair, int x)
		{
			return intPair.x == x;
		}

		public static bool Equals(IntPair intPair, int x, int y)
		{
			return intPair.x == x && intPair.y == y;
		}

		public static bool Equals(IntPair intPair, Vector2 xy)
		{
			return (float)intPair.x == xy.x && (float)intPair.y == xy.y;
		}

		public static bool Equals(IntPair intPair, Vector3 xyz)
		{
			return (float)intPair.x == xyz.x && (float)intPair.y == xyz.y;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", x, y);
		}
	}

	public struct IntTrio
	{
		public int x;

		public int y;

		public int z;

		public int Product
		{
			get
			{
				return x * y * z;
			}
		}

		public static IntTrio Zero
		{
			get
			{
				return new IntTrio(0, 0, 0);
			}
		}

		public static IntTrio One
		{
			get
			{
				return new IntTrio(1, 1, 1);
			}
		}

		public IntTrio(int x)
		{
			this.x = x;
			y = 0;
			z = 0;
		}

		public IntTrio(int x, int y)
		{
			this.x = x;
			this.y = y;
			z = 0;
		}

		public IntTrio(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public IntTrio(int[] xyz)
		{
			x = xyz[0];
			y = xyz[1];
			z = xyz[2];
		}

		public IntTrio(float x, float y, float z)
		{
			this.x = Floored(x);
			this.y = Floored(y);
			this.z = Floored(z);
		}

		public IntTrio(Vector3 xyz)
		{
			x = Floored(xyz.x);
			y = Floored(xyz.y);
			z = Floored(xyz.z);
		}

		public Vector3 ToVector()
		{
			return new Vector3(x, y, z);
		}

		public bool Equals(IntTrio otherIntTrio)
		{
			return x == otherIntTrio.x && y == otherIntTrio.y && z == otherIntTrio.z;
		}

		public bool Equals(int x)
		{
			return this.x == x;
		}

		public bool Equals(int x, int y)
		{
			return this.x == x && this.y == y;
		}

		public bool Equals(int x, int y, int z)
		{
			return this.x == x && this.y == y && this.z == z;
		}

		public bool Equals(Vector2 xy)
		{
			return (float)x == xy.x && (float)y == xy.y;
		}

		public bool Equals(Vector3 xyz)
		{
			return (float)x == xyz.x && (float)y == xyz.y && (float)z == xyz.z;
		}

		public int[] ToArray()
		{
			return new int[3] { x, y, z };
		}

		public static bool AreEqual(IntTrio first, IntTrio second)
		{
			return first.x == second.x && first.y == second.y && first.z == second.z;
		}

		public static bool AreNotEqual(IntTrio first, IntTrio second)
		{
			return !AreEqual(first, second);
		}

		public static bool Equals(IntTrio intTrio, int x)
		{
			return intTrio.x == x;
		}

		public static bool Equals(IntTrio intTrio, int x, int y)
		{
			return intTrio.x == x && intTrio.y == y;
		}

		public static bool Equals(IntTrio intTrio, int x, int y, int z)
		{
			return intTrio.x == x && intTrio.y == y && intTrio.z == z;
		}

		public static bool Equals(IntTrio intTrio, Vector2 xy)
		{
			return (float)intTrio.x == xy.x && (float)intTrio.y == xy.y;
		}

		public static bool Equals(IntTrio intTrio, Vector3 xyz)
		{
			return (float)intTrio.x == xyz.x && (float)intTrio.y == xyz.y && (float)intTrio.z == xyz.z;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", x, y, z);
		}
	}

	public struct IntPairNullable
	{
		private int? nullableX;

		private int? nullableY;

		public int x
		{
			get
			{
				return FromNullableInt(nullableX);
			}
			set
			{
				nullableX = value;
			}
		}

		public int y
		{
			get
			{
				return FromNullableInt(nullableY);
			}
			set
			{
				nullableY = value;
			}
		}

		public bool HasValueX
		{
			get
			{
				return nullableX.HasValue;
			}
		}

		public bool HasValueY
		{
			get
			{
				return nullableY.HasValue;
			}
		}

		public bool HasAValue
		{
			get
			{
				return HasValueX || HasValueY;
			}
		}

		public bool HasValues
		{
			get
			{
				return HasValueX && HasValueY;
			}
		}

		public bool HasOneValue
		{
			get
			{
				return HasValueX ^ HasValueY;
			}
		}

		public bool IsNullX
		{
			get
			{
				return !HasValueX;
			}
		}

		public bool IsNullY
		{
			get
			{
				return !HasValueY;
			}
		}

		public bool IsNull
		{
			get
			{
				return IsNullX && IsNullY;
			}
		}

		public bool HasNull
		{
			get
			{
				return IsNullX || IsNullY;
			}
		}

		public bool OneNull
		{
			get
			{
				return IsNullX ^ IsNullY;
			}
		}

		public bool IsNotNullX
		{
			get
			{
				return HasValueX;
			}
		}

		public bool IsNotNullY
		{
			get
			{
				return HasValueY;
			}
		}

		public bool IsNotNull
		{
			get
			{
				return HasValues;
			}
		}

		public bool HasNotValueX
		{
			get
			{
				return IsNullX;
			}
		}

		public bool HasNotValueY
		{
			get
			{
				return IsNullY;
			}
		}

		public bool HasNoValues
		{
			get
			{
				return IsNull;
			}
		}

		public IntPairNullable(int x)
		{
			nullableX = x;
			nullableY = null;
		}

		public IntPairNullable(int x, int y)
		{
			nullableX = x;
			nullableY = y;
		}

		public IntPairNullable(int? x)
		{
			nullableX = x;
			nullableY = null;
		}

		public IntPairNullable(int? x, int? y)
		{
			nullableX = x;
			nullableY = y;
		}

		public override string ToString()
		{
			if (IsNull)
			{
				return "Null";
			}
			return string.Format("{0}, {1}", StringFromNullableInt(nullableX), StringFromNullableInt(nullableY));
		}
	}

	public struct NIntTrio
	{
		private int? nullableX;

		private int? nullableY;

		private int? nullableZ;

		public int x
		{
			get
			{
				return FromNullableInt(nullableX);
			}
			set
			{
				nullableX = value;
			}
		}

		public int y
		{
			get
			{
				return FromNullableInt(nullableY);
			}
			set
			{
				nullableY = value;
			}
		}

		public int z
		{
			get
			{
				return FromNullableInt(nullableZ);
			}
			set
			{
				nullableZ = value;
			}
		}

		public NIntTrio(int x)
		{
			nullableX = x;
			nullableY = null;
			nullableZ = null;
		}

		public NIntTrio(int x, int y)
		{
			nullableX = x;
			nullableY = y;
			nullableZ = null;
		}

		public NIntTrio(int x, int y, int z)
		{
			nullableX = x;
			nullableY = y;
			nullableZ = z;
		}

		public bool IsNullX()
		{
			return nullableX.HasValue;
		}

		public bool IsNullY()
		{
			return nullableY.HasValue;
		}

		public bool IsNullZ()
		{
			return nullableZ.HasValue;
		}

		public bool IsNull()
		{
			return IsNullX() && IsNullY() && IsNullZ();
		}

		public bool HasNull()
		{
			return IsNullX() || IsNullY() || IsNullZ();
		}

		public override string ToString()
		{
			if (IsNull())
			{
				return "Null";
			}
			return string.Format("{0}, {1}, {2}", StringFromNullableInt(nullableX), StringFromNullableInt(nullableY), StringFromNullableInt(nullableZ));
		}
	}

	public struct IntFloat
	{
		public int intValue;

		public float floatValue;

		public IntFloat(int intValue)
		{
			this.intValue = intValue;
			floatValue = 0f;
		}

		public IntFloat(int intValue, float floatValue)
		{
			this.intValue = intValue;
			this.floatValue = floatValue;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", intValue, floatValue);
		}
	}

	public class SteppedFloat
	{
		private const float defaultStepSize = 0.25f;

		private float value;

		private Range range;

		private float step;

		private bool rebound;

		public SteppedFloat(float stepSize = 0.25f, float minValue = 0f, float maxValue = 1f)
		{
			InitializeSteppedFloat(stepSize, minValue, maxValue, false);
		}

		public SteppedFloat(float stepSize, float minValue, float maxValue, bool reboundOffMax)
		{
			InitializeSteppedFloat(stepSize, minValue, maxValue, reboundOffMax);
		}

		private void InitializeSteppedFloat(float stepSize, float minValue, float maxValue, bool reboundOffMax)
		{
			range = new Range(minValue, maxValue);
			value = range.Min;
			if (Abs(stepSize) > range.Distance)
			{
				stepSize = range.Indexed(stepSize);
			}
			step = stepSize;
			rebound = reboundOffMax;
		}

		public float GetNext()
		{
			value += step;
			if (range.ContainsNot(value))
			{
				if (rebound)
				{
					value = range.Rebounded(value);
					step *= -1f;
				}
				else
				{
					value = range.Indexed(value);
					if (range.DoesNotBorder(value))
					{
						value -= step;
					}
				}
			}
			return value;
		}
	}

	private class FrameRate
	{
		private float valueStored;

		public float value
		{
			get
			{
				return valueStored;
			}
			set
			{
				valueStored = value;
				deltaTime = 1f / value;
			}
		}

		public float deltaTime { get; private set; }

		public FrameRate(float value)
		{
			this.value = value;
		}
	}

	public struct Tri
	{
		private bool boolean;

		private bool special;

		public bool Value
		{
			get
			{
				return boolean;
			}
			set
			{
				boolean = value;
				special = false;
			}
		}

		public bool Special
		{
			get
			{
				return special;
			}
			set
			{
				special = value;
				boolean = value;
			}
		}

		public bool IsTrue
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		public bool IsFalse
		{
			get
			{
				return !Value;
			}
			set
			{
				Value = !value;
			}
		}

		public bool IsSpecialTrue
		{
			get
			{
				return Special;
			}
			set
			{
				Special = value;
			}
		}

		public bool IsNormalTrue
		{
			get
			{
				return Value && !Special;
			}
			set
			{
				Value = value;
				Special = !value;
			}
		}

		public bool IsNormal
		{
			get
			{
				return !Special;
			}
			set
			{
				Special = !value;
			}
		}

		public bool Boolean
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		public bool IsNotSpecialTrue
		{
			get
			{
				return !IsSpecialTrue;
			}
		}

		public bool IsNotNormalTrue
		{
			get
			{
				return !IsNormalTrue;
			}
		}

		public Tri(bool isTrue)
		{
			boolean = isTrue;
			special = false;
		}

		public Tri(bool isTrue, bool isSpecial)
		{
			boolean = isTrue;
			special = isSpecial;
		}

		public Tri(int value)
		{
			switch (value)
			{
			case 0:
				boolean = false;
				special = false;
				break;
			case 1:
				boolean = true;
				special = false;
				break;
			case 2:
				boolean = true;
				special = true;
				break;
			default:
				Debug.LogError("MTUT: ERROR: Tri struct recieved unexpected input INTEGER value of: " + value);
				boolean = value <= 0;
				special = false;
				break;
			}
		}

		public override string ToString()
		{
			if (Special)
			{
				return "Special";
			}
			if (Value)
			{
				return "True";
			}
			return "False";
		}
	}

	public struct Quad
	{
		private bool boolean;

		private bool special;

		public bool Value
		{
			get
			{
				return boolean;
			}
			set
			{
				boolean = value;
			}
		}

		public bool Special
		{
			get
			{
				return special;
			}
			set
			{
				special = value;
			}
		}

		public bool IsTrue
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		public bool IsFalse
		{
			get
			{
				return !Value;
			}
			set
			{
				Value = !value;
			}
		}

		public bool IsSpecial
		{
			get
			{
				return Special;
			}
			set
			{
				Special = value;
			}
		}

		public bool IsNormal
		{
			get
			{
				return !Special;
			}
			set
			{
				Special = !value;
			}
		}

		public bool IsNormalTrue
		{
			get
			{
				return Value && !Special;
			}
			set
			{
				Value = value;
				Special = !value;
			}
		}

		public bool IsNormalFalse
		{
			get
			{
				return !Value && !Special;
			}
			set
			{
				Value = !value;
				Special = !value;
			}
		}

		public bool IsSpecialTrue
		{
			get
			{
				return Value && Special;
			}
			set
			{
				Value = !value;
				Special = value;
			}
		}

		public bool IsSpecialFalse
		{
			get
			{
				return !Value && Special;
			}
			set
			{
				Value = !value;
				Special = value;
			}
		}

		public bool HasTrue
		{
			get
			{
				return Value || Special;
			}
		}

		public bool HasFalse
		{
			get
			{
				return !Value || !Special;
			}
		}

		public bool Boolean
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		public Quad(bool isTrue)
		{
			boolean = isTrue;
			special = false;
		}

		public Quad(bool isTrue, bool isSpecial)
		{
			boolean = isTrue;
			special = isSpecial;
		}

		public Quad(int value)
		{
			switch (value)
			{
			case 0:
				boolean = false;
				special = false;
				break;
			case 1:
				boolean = true;
				special = false;
				break;
			case 2:
				boolean = true;
				special = true;
				break;
			case 3:
				boolean = false;
				special = true;
				break;
			default:
				Debug.LogError("MTUT: ERROR: Quad struct recieved unexpected input INTEGER value of: " + value);
				boolean = value <= 0;
				special = false;
				break;
			}
		}

		public override string ToString()
		{
			if (Special)
			{
				if (Value)
				{
					return "Special True";
				}
				return "Special False";
			}
			if (Value)
			{
				return "True";
			}
			return "False";
		}
	}

	public const float Pi = (float)Math.PI;

	public const float TwoPi = (float)Math.PI * 2f;

	public const float HalfPi = (float)Math.PI / 2f;

	private static TimeUnit[] timeUnits = new TimeUnit[10]
	{
		new TimeUnit(1, "second", 60f, 2),
		new TimeUnit(1, "minute", 60f),
		new TimeUnit(1, "hour", 24f),
		new TimeUnit(1, "day", 7f),
		new TimeUnit(1, "week", 52f),
		new TimeUnit(1, "year", 10f),
		new TimeUnit(1, "decade", 10f),
		new TimeUnit(1, "century", 10f),
		new TimeUnit(1, "millenium", 10f),
		new TimeUnit(1, "eternity", 2.1474836E+09f)
	};

	private static FrameRate frameRateMinimum = new FrameRate(30f);

	public static float frameRateMin
	{
		get
		{
			return frameRateMinimum.value;
		}
		set
		{
			frameRateMinimum.value = value;
		}
	}

	public static float SmoothedDeltaTime
	{
		get
		{
			float smoothDeltaTime = Time.smoothDeltaTime;
			return (!(smoothDeltaTime > frameRateMinimum.deltaTime)) ? smoothDeltaTime : frameRateMinimum.deltaTime;
		}
	}

	public static float Min(float firstFloat, float secondFloat)
	{
		return (!(firstFloat < secondFloat)) ? secondFloat : firstFloat;
	}

	public static float Max(float firstFloat, float secondFloat)
	{
		return (!(firstFloat > secondFloat)) ? secondFloat : firstFloat;
	}

	public static int MinInt(int firstInt, int secondInt)
	{
		return (firstInt >= secondInt) ? secondInt : firstInt;
	}

	public static int MaxInt(int firstInt, int secondInt)
	{
		return (firstInt <= secondInt) ? secondInt : firstInt;
	}

	public static float Abs(float value)
	{
		return (!(value < 0f)) ? value : (value * -1f);
	}

	public static int AbsInt(int value)
	{
		return (value >= 0) ? value : (value * -1);
	}

	public static int AbsAsInt(float value)
	{
		return (int)Abs(value);
	}

	public static float Average(float firstFloat, float secondFloat)
	{
		return (firstFloat + secondFloat) / 2f;
	}

	public static int AverageInt(int firstInt, int secondInt)
	{
		return AverageAsInt(firstInt, secondInt);
	}

	public static int AverageAsInt(float firstFloat, float secondFloat)
	{
		return (int)Average(firstFloat, secondFloat);
	}

	public static float Distance(float firstFloat, float secondFloat)
	{
		return Abs(firstFloat - secondFloat);
	}

	public static float Distance(float firstFloat, float secondFloat, bool returnAbs)
	{
		if (returnAbs)
		{
			return Distance(firstFloat, secondFloat);
		}
		return firstFloat - secondFloat;
	}

	public static int DistanceInt(int firstValue, int secondValue)
	{
		return AbsInt(firstValue - secondValue);
	}

	public static int DistanceAsInt(float firstValue, float secondValue)
	{
		return AbsAsInt(firstValue - secondValue);
	}

	public static float Distance(Vector2 pairOfValues)
	{
		return Distance(pairOfValues.x, pairOfValues.y);
	}

	public static int DistanceAsInt(Vector2 pairOfValues)
	{
		return DistanceAsInt(pairOfValues.x, pairOfValues.y);
	}

	public static float Distance(float firstX, float secondX, float firstY, float secondY)
	{
		float firstSide = Distance(firstX, secondX);
		float secondSide = Distance(firstY, secondY);
		return Hypotenuse(firstSide, secondSide);
	}

	public static int DistanceAsInt(float firstX, float secondX, float firstY, float secondY)
	{
		return (int)Distance(firstX, secondX, firstY, secondY);
	}

	public static float DistanceInt(int firstIntX, int secondIntX, int firstIntY, int secondIntY)
	{
		int firstSide = DistanceInt(firstIntX, secondIntX);
		int secondSide = DistanceInt(firstIntY, secondIntY);
		return HypotenuseInt(firstSide, secondSide);
	}

	public static float Distance(float firstX, float secondX, float firstY, float secondY, float firstZ, float secondZ)
	{
		float firstSide = Distance(firstX, secondX);
		float secondSide = Distance(firstY, secondY);
		float secondSide2 = Distance(firstZ, secondZ);
		return Hypotenuse(Hypotenuse(firstSide, secondSide), secondSide2);
	}

	public static float Distance(float firstX, float firstY, float firstZ, Vector3 secondXYZ)
	{
		return Distance(firstX, secondXYZ.x, firstY, secondXYZ.y, firstZ, secondXYZ.z);
	}

	public static float Distance(Vector3 firstXYZ, float secondX, float secondY, float secondZ)
	{
		return Distance(firstXYZ.x, secondX, firstXYZ.y, secondY, firstXYZ.z, secondZ);
	}

	public static float Distance(Vector3 firstXYZ, Vector3 secondXYZ)
	{
		return Distance(firstXYZ.x, secondXYZ.x, firstXYZ.y, secondXYZ.y, firstXYZ.z, secondXYZ.z);
	}

	public static float Hypotenuse(float firstSide, float secondSide)
	{
		return Sqrt(Sqrd(firstSide) + Sqrd(secondSide));
	}

	public static float Hypotenuse(Vector2 firstAndSecondSide)
	{
		return Hypotenuse(firstAndSecondSide.x, firstAndSecondSide.y);
	}

	public static int HypotenuseInt(int firstSide, int secondSide)
	{
		return (int)Hypotenuse(firstSide, secondSide);
	}

	public static float Smallest(float firstFloat, float secondFloat)
	{
		return (!IsSmaller(firstFloat, secondFloat)) ? secondFloat : firstFloat;
	}

	public static float Largest(float firstFloat, float secondFloat)
	{
		return (!IsLarger(firstFloat, secondFloat)) ? secondFloat : firstFloat;
	}

	public static int SmallestInt(int firstInt, int secondInt)
	{
		return (!IsSmallerInt(firstInt, secondInt)) ? secondInt : firstInt;
	}

	public static int LargestInt(int firstInt, int secondInt)
	{
		return (!IsLargerInt(firstInt, secondInt)) ? secondInt : firstInt;
	}

	public static bool IsSmaller(float queryFloat, float testAgainstFloat)
	{
		return Abs(queryFloat) < Abs(testAgainstFloat);
	}

	public static bool IsLarger(float queryFloat, float testAgainstFloat)
	{
		return Abs(queryFloat) > Abs(testAgainstFloat);
	}

	public static bool IsSmallerInt(int queryInt, int testAgainstInt)
	{
		return AbsInt(queryInt) < AbsInt(testAgainstInt);
	}

	public static bool IsLargerInt(int queryInt, int testAgainstInt)
	{
		return AbsInt(queryInt) > AbsInt(testAgainstInt);
	}

	public static float Inverse(float value)
	{
		return 1f - value;
	}

	public static void Inverse(ref float value)
	{
		value = Inverse(value);
	}

	public static float InverseClamped(float value)
	{
		return Inverse(Clamp01(value));
	}

	public static void InverseClamped(ref float value)
	{
		value = InverseClamped(value);
	}

	public static bool IsEven(int value)
	{
		return value % 2 == 0;
	}

	public static bool IsEven(float value)
	{
		return value % 2f == 0f;
	}

	public static bool IsOdd(int value)
	{
		return value % 2 != 0;
	}

	public static bool IsOdd(float value)
	{
		return value % 2f != 0f;
	}

	public static bool IsPositive(int value)
	{
		return value > 0;
	}

	public static bool IsPositive(float value)
	{
		return value > 0f;
	}

	public static bool IsNegative(int value)
	{
		return value < 0;
	}

	public static bool IsNegative(float value)
	{
		return value < 0f;
	}

	public static bool IsNotPositive(int value)
	{
		return value <= 0;
	}

	public static bool IsNotPositive(float value)
	{
		return value <= 0f;
	}

	public static bool IsNotNegative(int value)
	{
		return value >= 0;
	}

	public static bool IsNotNegative(float value)
	{
		return value >= 0f;
	}

	public static bool IsFractional(float value)
	{
		return Abs(value % 1f) != 0f;
	}

	public static bool IsntFractional(float value)
	{
		return Abs(value % 1f) == 0f;
	}

	public static bool IsFractionOf1(float value)
	{
		return Abs(value) < 1f;
	}

	public static bool IsntFractionOf1(float value)
	{
		return Abs(value) >= 1f;
	}

	public static float ToRadians(float degrees)
	{
		return degrees * (float)Math.PI / 180f;
	}

	public static float ToDegrees(float radians)
	{
		return radians * 180f / (float)Math.PI;
	}

	public static float Sin(float radians)
	{
		return Mathf.Sin(radians);
	}

	public static float Cos(float radians)
	{
		return Mathf.Cos(radians);
	}

	public static float Tan(float radians)
	{
		return Mathf.Tan(radians);
	}

	public static float SinFromDegrees(float degrees)
	{
		return Sin(ToRadians(degrees));
	}

	public static float CosFromDegrees(float degrees)
	{
		return Cos(ToRadians(degrees));
	}

	public static float TanFromDegrees(float degrees)
	{
		return Tan(ToRadians(degrees));
	}

	public static float ArcSin(float oppositeOverHypotinuse)
	{
		return Mathf.Asin(oppositeOverHypotinuse);
	}

	public static float ArcSin(float opposite, float hypotinuse)
	{
		return ArcSin(opposite / hypotinuse);
	}

	public static float ArcSinInDegrees(float oppositeOverHypotinuse)
	{
		return ToDegrees(Mathf.Asin(oppositeOverHypotinuse));
	}

	public static float ArcSinInDegrees(float opposite, float hypotinuse)
	{
		return ArcSinInDegrees(opposite / hypotinuse);
	}

	public static float ArcCos(float adjacentOverHypotinuse)
	{
		return Mathf.Acos(adjacentOverHypotinuse);
	}

	public static float ArcCos(float adjacent, float hypotinuse)
	{
		return ArcCos(adjacent / hypotinuse);
	}

	public static float ArcCosInDegrees(float adjacentOverHypotinuse)
	{
		return ToDegrees(Mathf.Acos(adjacentOverHypotinuse));
	}

	public static float ArcCosInDegrees(float adjacent, float hypotinuse)
	{
		return ArcCosInDegrees(adjacent / hypotinuse);
	}

	public static float ArcTan(float oppositeOverAdjacent)
	{
		return Mathf.Atan(oppositeOverAdjacent);
	}

	public static float ArcTan(float opposite, float adjacent)
	{
		return ArcTan(opposite / adjacent);
	}

	public static float ArcTanInDegrees(float oppositeOverAdjacent)
	{
		return ToDegrees(Mathf.Atan(oppositeOverAdjacent));
	}

	public static float ArcTanInDegrees(float opposite, float adjacent)
	{
		return ArcTanInDegrees(opposite / adjacent);
	}

	public static Vector2 FromPollar(float radians, float radius)
	{
		return new Vector2(Cos(radians), Sin(radians)) * radius;
	}

	public static Vector2 FromEliptical(float radians, float xRadius, float yRadius)
	{
		return new Vector2(xRadius * Cos(radians), yRadius * Sin(radians));
	}

	public static Vector2 FromEliptical(float radians, Vector2 radii)
	{
		return new Vector2(radii.x * Cos(radians), radii.y * Sin(radians));
	}

	public static Vector2 FromCurvedX(float radians, float xRadius, float yRadius)
	{
		return new Vector2(xRadius * (Cos(radians) - 1f), yRadius * Sin(radians));
	}

	public static Vector2 FromCurvedX(float radians, float radius)
	{
		return new Vector2(Cos(radians) - 1f, Sin(radians)) * radius;
	}

	public static Vector2 FromCircular(float radians, float radius)
	{
		return FromPollar(radians, radius);
	}

	public static Vector2 FromCircular(float radians, float xRadius, float yRadius)
	{
		return FromEliptical(radians, xRadius, yRadius);
	}

	public static Vector2 FromCircular(float radians, Vector2 radii)
	{
		return FromEliptical(radians, radii);
	}

	public static float ToPercent(float value, float minimum, float maximum)
	{
		return ToPercent(value, minimum, maximum, false);
	}

	public static float ToPercent(float value, float minimum, float maximum, bool inverse)
	{
		float num = value - minimum;
		float num2 = maximum - minimum;
		float num3 = num / num2;
		return (!inverse) ? num3 : Inverse(num3);
	}

	public static float ToPercent01(float value, float minimum, float maximum)
	{
		return ToPercent01(value, minimum, maximum, false);
	}

	public static float ToPercent01(float value, float minimum, float maximum, bool inverse)
	{
		bool wasBelow;
		bool wasAbove;
		return ToPercent01(value, minimum, maximum, inverse, out wasBelow, out wasAbove);
	}

	public static float ToPercent01(float value, float minimum, float maximum, out bool wasWithin01)
	{
		return ToPercent01(value, minimum, maximum, false, out wasWithin01);
	}

	public static float ToPercent01(float value, float minimum, float maximum, bool inverse, out bool wasWithin01)
	{
		bool wasBelow;
		bool wasAbove;
		float result = ToPercent01(value, minimum, maximum, inverse, out wasBelow, out wasAbove);
		wasWithin01 = !wasBelow && !wasAbove;
		return result;
	}

	public static float ToPercent01(float value, float minimum, float maximum, out bool wasBelow0, out bool wasAbove1)
	{
		return ToPercent01(value, minimum, maximum, false, out wasBelow0, out wasAbove1);
	}

	public static float ToPercent01(float value, float minimum, float maximum, bool inverse, out bool wasBelow0, out bool wasAbove1)
	{
		bool flag;
		if (maximum < minimum)
		{
			Switch(ref minimum, ref maximum);
			flag = true;
		}
		else
		{
			flag = false;
		}
		bool flag2 = flag ^ inverse;
		float result;
		if (value < minimum)
		{
			result = ((!flag2) ? 0f : 1f);
			wasBelow0 = !flag;
			wasAbove1 = flag;
		}
		else if (value > maximum)
		{
			result = ((!flag2) ? 1f : 0f);
			wasBelow0 = flag;
			wasAbove1 = !flag;
		}
		else
		{
			result = ToPercent(value, minimum, maximum, flag2);
			wasBelow0 = false;
			wasAbove1 = false;
		}
		return result;
	}

	public static float FromPercent(float percent, float minimum, float maximum)
	{
		return FromPercent(percent, minimum, maximum, false);
	}

	public static float FromPercent(float percent, float minimum, float maximum, bool inverse)
	{
		if (inverse)
		{
			percent = 1f - percent;
		}
		return (maximum - minimum) * percent + minimum;
	}

	public static float FromPercent01(float percent, float minimum, float maximum)
	{
		return FromPercent(Clamp01(percent), minimum, maximum);
	}

	public static float FromPercent01(float percent, float minimum, float maximum, bool inverse)
	{
		return FromPercent(Clamp01(percent), minimum, maximum, inverse);
	}

	public static float ThroughPercent(float value, float minValue, float maxValue, float minResult, float maxResult)
	{
		return ThroughPercent(value, minValue, maxValue, minResult, maxResult, false, false);
	}

	public static float ThroughPercent(float value, float minValue, float maxValue, float minResult, float maxResult, bool inverse)
	{
		return ThroughPercent(value, minValue, maxValue, minResult, maxResult, inverse, false);
	}

	public static float ThroughPercent(float value, float minValue, float maxValue, float minResult, float maxResult, bool inverse, bool clamp01)
	{
		float num = ToPercent(value, minValue, maxValue, inverse);
		if (clamp01)
		{
			num = Clamp01(num);
		}
		return FromPercent(num, minResult, maxResult);
	}

	public static float ThroughPercent01(float value, float minValue, float maxValue, float minResult, float maxResult)
	{
		return ThroughPercent(value, minValue, maxValue, minResult, maxResult, false, true);
	}

	public static float ThroughPercent01(float value, float minValue, float maxValue, float minResult, float maxResult, bool inverse)
	{
		return ThroughPercent(value, minValue, maxValue, minResult, maxResult, inverse, true);
	}

	public static float ToPercentInt(int value, int minimum, int maximum)
	{
		return ToPercentInt(value, minimum, maximum, false);
	}

	public static float ToPercentInt(int value, int minimum, int maximum, bool inverse)
	{
		return ToPercent(value, minimum, maximum, inverse);
	}

	public static float ToPercentInt01(int value, int minimum, int maximum)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, false));
	}

	public static float ToPercentInt01(int value, int minimum, int maximum, bool inverse)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, inverse));
	}

	public static float ToPercentInt01(int value, int minimum, int maximum, out bool wasWithin01)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, false), out wasWithin01);
	}

	public static float ToPercentInt01(int value, int minimum, int maximum, bool inverse, out bool wasWithin01)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, inverse), out wasWithin01);
	}

	public static float ToPercentInt01(int value, int minimum, int maximum, out bool wasBelow0, out bool wasAbove1)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, false), out wasBelow0, out wasAbove1);
	}

	public static float ToPercentInt01(int value, int minimum, int maximum, bool inverse, out bool wasBelow0, out bool wasAbove1)
	{
		return Clamp01(ToPercentInt(value, minimum, maximum, inverse), out wasBelow0, out wasAbove1);
	}

	public static float FromPercentInt(float percent, int minimum, int maximum)
	{
		return FromPercentInt(percent, minimum, maximum, false);
	}

	public static float FromPercentInt(float percent, int minimum, int maximum, bool inverse)
	{
		return FromPercent(percent, minimum, maximum, inverse);
	}

	public static float FromPercentInt01(float percent, int minimum, int maximum)
	{
		return FromPercentInt(Clamp01(percent), minimum, maximum);
	}

	public static float FromPercentInt01(float percent, int minimum, int maximum, bool inverse)
	{
		return FromPercentInt(Clamp01(percent), minimum, maximum, inverse);
	}

	public static int FromPercentIntFloored(float percent, int minimum, int maximum)
	{
		return Floored(FromPercentInt(percent, minimum, maximum));
	}

	public static int FromPercentIntFloored(float percent, int minimum, int maximum, bool inverse)
	{
		return Floored(FromPercentInt(percent, minimum, maximum, inverse));
	}

	public static int FromPercentInt01Floored(float percent, int minimum, int maximum)
	{
		return Floored(FromPercentInt01(percent, minimum, maximum));
	}

	public static int FromPercentInt01Floored(float percent, int minimum, int maximum, bool inverse)
	{
		return Floored(FromPercentInt01(percent, minimum, maximum, inverse));
	}

	public static int FromPercentIntCeiled(float percent, int minimum, int maximum)
	{
		return Ceiled(FromPercentInt(percent, minimum, maximum));
	}

	public static int FromPercentIntCeiled(float percent, int minimum, int maximum, bool inverse)
	{
		return Ceiled(FromPercentInt(percent, minimum, maximum, inverse));
	}

	public static int FromPercentInt01Ceiled(float percent, int minimum, int maximum)
	{
		return Ceiled(FromPercentInt01(percent, minimum, maximum));
	}

	public static int FromPercentInt01Ceiled(float percent, int minimum, int maximum, bool inverse)
	{
		return Ceiled(FromPercentInt01(percent, minimum, maximum, inverse));
	}

	public static int FromPercentIntRounded(float percent, int minimum, int maximum, bool roundDown)
	{
		return Rounded(FromPercentInt(percent, minimum, maximum), roundDown);
	}

	public static int FromPercentIntRounded(float percent, int minimum, int maximum, bool inverse, bool roundDown)
	{
		return Rounded(FromPercentInt(percent, minimum, maximum, inverse), roundDown);
	}

	public static int FromPercentInt01Rounded(float percent, int minimum, int maximum, bool roundDown)
	{
		return Rounded(FromPercentInt01(percent, minimum, maximum), roundDown);
	}

	public static int FromPercentInt01Rounded(float percent, int minimum, int maximum, bool inverse, bool roundDown)
	{
		return Rounded(FromPercentInt01(percent, minimum, maximum, inverse), roundDown);
	}

	public static float ThroughPercentInt(int value, int minValue, int maxValue, int minResult, int maxResult)
	{
		return ThroughPercentInt(value, minValue, maxValue, minResult, maxResult, false, false);
	}

	public static float ThroughPercentInt(int value, int minValue, int maxValue, int minResult, int maxResult, bool inverse)
	{
		return ThroughPercentInt(value, minValue, maxValue, minResult, maxResult, inverse, false);
	}

	public static float ThroughPercentInt(int value, int minValue, int maxValue, int minResult, int maxResult, bool inverse, bool clamp01)
	{
		return ThroughPercent(value, minValue, maxValue, minResult, maxResult, inverse, clamp01);
	}

	public static float ThroughPercentInt01(int value, int minValue, int maxValue, int minResult, int maxResult)
	{
		return ThroughPercentInt(value, minValue, maxValue, minResult, maxResult, false, true);
	}

	public static float ThroughPercentInt01(int value, int minValue, int maxValue, int minResult, int maxResult, bool inverse)
	{
		return ThroughPercentInt(value, minValue, maxValue, minResult, maxResult, inverse, true);
	}

	public static float ToPercentIndex(int index, int arrayLength)
	{
		return ToPercentInt(index, 0, arrayLength - 1);
	}

	public static float ToPercentIndex(int index, int arrayLength, bool inverse)
	{
		return ToPercentInt(index, 0, arrayLength - 1, inverse);
	}

	public static float ToPercentIndex01(int index, int arrayLength)
	{
		return ToPercentInt01(index, 0, arrayLength - 1);
	}

	public static float ToPercentIndex01(int index, int arrayLength, bool inverse)
	{
		return ToPercentInt01(index, 0, arrayLength - 1, inverse);
	}

	public static float ToPercentIndex01(int index, int arrayLength, out bool wasWithin01)
	{
		return ToPercentInt01(index, 0, arrayLength - 1, out wasWithin01);
	}

	public static float ToPercentIndex01(int index, int arrayLength, bool inverse, out bool wasWithin01)
	{
		return ToPercentInt01(index, 0, arrayLength - 1, inverse, out wasWithin01);
	}

	public static float ToPercentIndex01(int index, int arrayLength, out bool wasBelow0, out bool wasAbove1)
	{
		return ToPercentInt01(index, 0, arrayLength - 1, out wasBelow0, out wasAbove1);
	}

	public static float ToPercentIndex01(int index, int arrayLength, bool inverse, out bool wasBelow0, out bool wasAbove1)
	{
		return ToPercentInt01(index, 0, arrayLength - 1, inverse, out wasBelow0, out wasAbove1);
	}

	public static float ToPercentIndex(int index, int arrayLength, int minIndex)
	{
		return ToPercentInt(index, minIndex, arrayLength - 1);
	}

	public static float ToPercentIndex(int index, int arrayLength, int minIndex, bool inverse)
	{
		return ToPercentInt(index, minIndex, arrayLength - 1, inverse);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex, bool inverse)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1, inverse);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex, out bool wasWithin01)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1, out wasWithin01);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex, bool inverse, out bool wasWithin01)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1, inverse, out wasWithin01);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex, out bool wasBelow0, out bool wasAbove1)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1, out wasBelow0, out wasAbove1);
	}

	public static float ToPercentIndex01(int index, int arrayLength, int minIndex, bool inverse, out bool wasBelow0, out bool wasAbove1)
	{
		return ToPercentInt01(index, minIndex, arrayLength - 1, inverse, out wasBelow0, out wasAbove1);
	}

	public static float FromPercentIndexAsFloat(float percent, int arrayLength)
	{
		return FromPercentInt(percent, 0, arrayLength - 1);
	}

	public static float FromPercentIndexAsFloat(float percent, int arrayLength, bool inverse)
	{
		return FromPercentInt(percent, 0, arrayLength - 1, inverse);
	}

	public static float FromPercentIndex01AsFloat(float percent, int arrayLength)
	{
		return FromPercentInt01(percent, 0, arrayLength - 1);
	}

	public static float FromPercentIndex01AsFloat(float percent, int arrayLength, bool inverse)
	{
		return FromPercentInt01(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex(float percent, int arrayLength)
	{
		return FromPercentIntFloored(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndex(float percent, int arrayLength, bool inverse)
	{
		return FromPercentIntFloored(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01(float percent, int arrayLength)
	{
		return FromPercentInt01Floored(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndex01(float percent, int arrayLength, bool inverse)
	{
		return FromPercentInt01Floored(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexFloored(float percent, int arrayLength)
	{
		return FromPercentIntFloored(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndexFloored(float percent, int arrayLength, bool inverse)
	{
		return FromPercentIntFloored(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01Floored(float percent, int arrayLength)
	{
		return FromPercentInt01Floored(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndex01Floored(float percent, int arrayLength, bool inverse)
	{
		return FromPercentInt01Floored(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexCeiled(float percent, int arrayLength)
	{
		return FromPercentIntCeiled(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndexCeiled(float percent, int arrayLength, bool inverse)
	{
		return FromPercentIntCeiled(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01Ceiled(float percent, int arrayLength)
	{
		return FromPercentInt01Ceiled(percent, 0, arrayLength - 1);
	}

	public static int FromPercentIndex01Ceiled(float percent, int arrayLength, bool inverse)
	{
		return FromPercentInt01Ceiled(percent, 0, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexRounded(float percent, int arrayLength, bool roundDown)
	{
		return FromPercentIntRounded(percent, 0, arrayLength - 1, roundDown);
	}

	public static int FromPercentIndexRounded(float percent, int arrayLength, bool inverse, bool roundDown)
	{
		return FromPercentIntRounded(percent, 0, arrayLength - 1, inverse, roundDown);
	}

	public static int FromPercentIndex01Rounded(float percent, int arrayLength, bool roundDown)
	{
		return FromPercentInt01Rounded(percent, 0, arrayLength - 1, roundDown);
	}

	public static int FromPercentIndex01Rounded(float percent, int arrayLength, bool inverse, bool roundDown)
	{
		return FromPercentInt01Rounded(percent, 0, arrayLength - 1, inverse, roundDown);
	}

	public static int FromPercentIndex(float percent, int arrayLength, int minIndex)
	{
		return FromPercentIntFloored(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndex(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentIntFloored(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01(float percent, int arrayLength, int minIndex)
	{
		return FromPercentInt01Floored(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndex01(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentInt01Floored(percent, minIndex, arrayLength - 1, inverse);
	}

	public static float FromPercentIndexAsFloat(float percent, int arrayLength, int minIndex)
	{
		return FromPercentInt(percent, minIndex, arrayLength - 1);
	}

	public static float FromPercentIndexAsFloat(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentInt(percent, minIndex, arrayLength - 1, inverse);
	}

	public static float FromPercentIndex01AsFloat(float percent, int arrayLength, int minIndex)
	{
		return FromPercentInt01(percent, minIndex, arrayLength - 1);
	}

	public static float FromPercentIndex01AsFloat(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentInt01(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexFloored(float percent, int arrayLength, int minIndex)
	{
		return FromPercentIntFloored(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndexFloored(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentIntFloored(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01Floored(float percent, int arrayLength, int minIndex)
	{
		return FromPercentInt01Floored(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndex01Floored(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentInt01Floored(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexCeiled(float percent, int arrayLength, int minIndex)
	{
		return FromPercentIntCeiled(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndexCeiled(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentIntCeiled(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndex01Ceiled(float percent, int arrayLength, int minIndex)
	{
		return FromPercentInt01Ceiled(percent, minIndex, arrayLength - 1);
	}

	public static int FromPercentIndex01Ceiled(float percent, int arrayLength, int minIndex, bool inverse)
	{
		return FromPercentInt01Ceiled(percent, minIndex, arrayLength - 1, inverse);
	}

	public static int FromPercentIndexRounded(float percent, int arrayLength, int minIndex, bool roundDown)
	{
		return FromPercentIntRounded(percent, minIndex, arrayLength - 1, roundDown);
	}

	public static int FromPercentIndexRounded(float percent, int arrayLength, int minIndex, bool inverse, bool roundDown)
	{
		return FromPercentIntRounded(percent, minIndex, arrayLength - 1, inverse, roundDown);
	}

	public static int FromPercentIndex01Rounded(float percent, int arrayLength, int minIndex, bool roundDown)
	{
		return FromPercentInt01Rounded(percent, minIndex, arrayLength - 1, roundDown);
	}

	public static int FromPercentIndex01Rounded(float percent, int arrayLength, int minIndex, bool inverse, bool roundDown)
	{
		return FromPercentInt01Rounded(percent, minIndex, arrayLength - 1, inverse, roundDown);
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int arrayLengthResult)
	{
		return Floored(ThroughPercentInt(index, 0, arrayLength - 1, 0, arrayLengthResult - 1));
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int arrayLengthResult, bool inverse)
	{
		return Floored(ThroughPercentInt(index, 0, arrayLength - 1, 0, arrayLengthResult - 1, inverse));
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int arrayLengthResult, bool inverse, bool clamp01)
	{
		return Floored(ThroughPercentInt(index, 0, arrayLength - 1, 0, arrayLengthResult - 1, inverse, clamp01));
	}

	public static int ThroughPercentIndex01(int index, int arrayLength, int arrayLengthResult)
	{
		return Floored(ThroughPercentInt01(index, 0, arrayLength - 1, 0, arrayLengthResult - 1));
	}

	public static int ThroughPercentIndex01(int index, int arrayLength, int arrayLengthResult, bool inverse)
	{
		return Floored(ThroughPercentInt01(index, 0, arrayLength - 1, 0, arrayLengthResult - 1, inverse));
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int minIndex, int arrayLengthResult, int minIndexResult)
	{
		return Floored(ThroughPercentInt(index, minIndex, arrayLength - 1, minIndexResult, arrayLengthResult - 1));
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int minIndex, int arrayLengthResult, int minIndexResult, bool inverse)
	{
		return Floored(ThroughPercentInt(index, minIndex, arrayLength - 1, minIndexResult, arrayLengthResult - 1, inverse));
	}

	public static int ThroughPercentIndex(int index, int arrayLength, int minIndex, int arrayLengthResult, int minIndexResult, bool inverse, bool clamp01)
	{
		return Floored(ThroughPercentInt(index, minIndex, arrayLength - 1, minIndexResult, arrayLengthResult - 1, inverse, clamp01));
	}

	public static int ThroughPercentIndex01(int index, int arrayLength, int minIndex, int arrayLengthResult, int minIndexResult)
	{
		return Floored(ThroughPercentInt01(index, minIndex, arrayLength - 1, minIndexResult, arrayLengthResult - 1));
	}

	public static int ThroughPercentIndex01(int index, int arrayLength, int minIndex, int arrayLengthResult, int minIndexResult, bool inverse)
	{
		return Floored(ThroughPercentInt01(index, minIndex, arrayLength - 1, minIndexResult, arrayLengthResult - 1, inverse));
	}

	public static int ToPercentageInt(float percent)
	{
		return FlooredMultiplication(percent, 100f);
	}

	public static float FromPercentageInt(int percentage)
	{
		return IntDivision(percentage, 100);
	}

	public static void Switch(ref float firstFloat, ref float secondFloat)
	{
		float num = firstFloat;
		firstFloat = secondFloat;
		secondFloat = num;
	}

	public static void SwitchInt(ref int firstInt, ref int secondInt)
	{
		int num = firstInt;
		firstInt = secondInt;
		secondInt = num;
	}

	public static bool IsWithin(int value, IntPair range)
	{
		return IsWithin(value, range.x, range.y);
	}

	public static bool IsBetween(int value, IntPair range)
	{
		return IsBetween(value, range.x, range.y);
	}

	public static bool IsNotWithin(int value, IntPair range)
	{
		return IsNotWithin(value, range.x, range.y);
	}

	public static bool IsNotBetween(int value, IntPair range)
	{
		return IsNotBetween(value, range.x, range.y);
	}

	public static bool IsWithin(int value, RangeInt range)
	{
		return IsWithin(value, range.Min, range.Max);
	}

	public static bool IsBetween(int value, RangeInt range)
	{
		return IsBetween(value, range.Min, range.Max);
	}

	public static bool IsNotWithin(int value, RangeInt range)
	{
		return IsNotWithin(value, range.Min, range.Max);
	}

	public static bool IsNotBetween(int value, RangeInt range)
	{
		return IsNotBetween(value, range.Min, range.Max);
	}

	public static bool IsWithin01(int value)
	{
		return IsWithin(value, 0, 1);
	}

	public static bool IsBetween01(int value)
	{
		return IsBetween(value, 0, 1);
	}

	public static bool IsNotWithin01(int value)
	{
		return IsNotWithin(value, 0, 1);
	}

	public static bool IsNotBetween01(int value)
	{
		return IsNotBetween(value, 0, 1);
	}

	public static bool IsWithin11(int value)
	{
		return IsWithin(value, -1, 1);
	}

	public static bool IsBetween11(int value)
	{
		return IsBetween(value, -1, 1);
	}

	public static bool IsNotWithin11(int value)
	{
		return IsNotWithin(value, -1, 1);
	}

	public static bool IsNotBetween11(int value)
	{
		return IsNotBetween(value, -1, 1);
	}

	public static bool IsWithin(float value, Vector2 range)
	{
		return IsWithin(value, range.x, range.y);
	}

	public static bool IsBetween(float value, Vector2 range)
	{
		return IsBetween(value, range.x, range.y);
	}

	public static bool IsNotWithin(float value, Vector2 range)
	{
		return IsNotWithin(value, range.x, range.y);
	}

	public static bool IsNotBetween(float value, Vector2 range)
	{
		return IsNotBetween(value, range.x, range.y);
	}

	public static bool IsWithin(float value, Range range)
	{
		return IsWithin(value, range.Min, range.Max);
	}

	public static bool IsBetween(float value, Range range)
	{
		return IsBetween(value, range.Min, range.Max);
	}

	public static bool IsNotWithin(float value, Range range)
	{
		return IsNotWithin(value, range.Min, range.Max);
	}

	public static bool IsNotBetween(float value, Range range)
	{
		return IsNotBetween(value, range.Min, range.Max);
	}

	public static bool IsWithin01(float value)
	{
		return IsWithin(value, 0f, 1f);
	}

	public static bool IsBetween01(float value)
	{
		return IsBetween(value, 0f, 1f);
	}

	public static bool IsNotWithin01(float value)
	{
		return IsNotWithin(value, 0f, 1f);
	}

	public static bool IsNotBetween01(float value)
	{
		return IsNotBetween(value, 0f, 1f);
	}

	public static bool IsWithin11(float value)
	{
		return IsWithin(value, -1f, 1f);
	}

	public static bool IsBetween11(float value)
	{
		return IsBetween(value, -1f, 1f);
	}

	public static bool IsNotWithin11(float value)
	{
		return IsNotWithin(value, -1f, 1f);
	}

	public static bool IsNotBetween11(float value)
	{
		return IsNotBetween(value, -1f, 1f);
	}

	public static bool IsWithin(int value, IntPair range, out bool wasBelow)
	{
		return IsWithin(value, range.x, range.y, out wasBelow);
	}

	public static bool IsBetween(int value, IntPair range, out bool wasBelow)
	{
		return IsBetween(value, range.x, range.y, out wasBelow);
	}

	public static bool IsNotWithin(int value, IntPair range, out bool wasBelow)
	{
		return IsNotWithin(value, range.x, range.y, out wasBelow);
	}

	public static bool IsNotBetween(int value, IntPair range, out bool wasBelow)
	{
		return IsNotBetween(value, range.x, range.y, out wasBelow);
	}

	public static bool IsWithin(int value, RangeInt range, out bool wasBelow)
	{
		return IsWithin(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsBetween(int value, RangeInt range, out bool wasBelow)
	{
		return IsBetween(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsNotWithin(int value, RangeInt range, out bool wasBelow)
	{
		return IsNotWithin(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsNotBetween(int value, RangeInt range, out bool wasBelow)
	{
		return IsNotBetween(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsWithin01(int value, out bool wasBelow)
	{
		return IsWithin((float)value, 0f, 1f, out wasBelow);
	}

	public static bool IsBetween01(int value, out bool wasBelow)
	{
		return IsBetween((float)value, 0f, 1f, out wasBelow);
	}

	public static bool IsNotWithin01(int value, out bool wasBelow)
	{
		return IsNotWithin((float)value, 0f, 1f, out wasBelow);
	}

	public static bool IsNotBetween01(int value, out bool wasBelow)
	{
		return IsNotBetween((float)value, 0f, 1f, out wasBelow);
	}

	public static bool IsWithin11(int value, out bool wasBelow)
	{
		return IsWithin((float)value, -1f, 1f, out wasBelow);
	}

	public static bool IsBetween11(int value, out bool wasBelow)
	{
		return IsBetween((float)value, -1f, 1f, out wasBelow);
	}

	public static bool IsNotWithin11(int value, out bool wasBelow)
	{
		return IsNotWithin((float)value, -1f, 1f, out wasBelow);
	}

	public static bool IsNotBetween11(int value, out bool wasBelow)
	{
		return IsNotBetween((float)value, -1f, 1f, out wasBelow);
	}

	public static bool IsWithin(float value, Vector2 range, out bool wasBelow)
	{
		return IsWithin(value, range.x, range.y, out wasBelow);
	}

	public static bool IsBetween(float value, Vector2 range, out bool wasBelow)
	{
		return IsBetween(value, range.x, range.y, out wasBelow);
	}

	public static bool IsNotWithin(float value, Vector2 range, out bool wasBelow)
	{
		return IsNotWithin(value, range.x, range.y, out wasBelow);
	}

	public static bool IsNotBetween(float value, Vector2 range, out bool wasBelow)
	{
		return IsNotBetween(value, range.x, range.y, out wasBelow);
	}

	public static bool IsWithin(float value, Range range, out bool wasBelow)
	{
		return IsWithin(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsBetween(float value, Range range, out bool wasBelow)
	{
		return IsBetween(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsNotWithin(float value, Range range, out bool wasBelow)
	{
		return IsNotWithin(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsNotBetween(float value, Range range, out bool wasBelow)
	{
		return IsNotBetween(value, range.Min, range.Max, out wasBelow);
	}

	public static bool IsWithin01(float value, out bool wasBelow)
	{
		return IsWithin(value, 0f, 1f, out wasBelow);
	}

	public static bool IsBetween01(float value, out bool wasBelow)
	{
		return IsBetween(value, 0f, 1f, out wasBelow);
	}

	public static bool IsNotWithin01(float value, out bool wasBelow)
	{
		return IsNotWithin(value, 0f, 1f, out wasBelow);
	}

	public static bool IsNotBetween01(float value, out bool wasBelow)
	{
		return IsNotBetween(value, 0f, 1f, out wasBelow);
	}

	public static bool IsWithin11(float value, out bool wasBelow)
	{
		return IsWithin(value, -1f, 1f, out wasBelow);
	}

	public static bool IsBetween11(float value, out bool wasBelow)
	{
		return IsBetween(value, -1f, 1f, out wasBelow);
	}

	public static bool IsNotWithin11(float value, out bool wasBelow)
	{
		return IsNotWithin(value, -1f, 1f, out wasBelow);
	}

	public static bool IsNotBetween11(float value, out bool wasBelow)
	{
		return IsNotBetween(value, -1f, 1f, out wasBelow);
	}

	public static bool IsWithin(int value, int maximum)
	{
		return IsWithin(value, 0, maximum);
	}

	public static bool IsNotWithin(int value, int maximum)
	{
		return !IsWithin(value, 0, maximum);
	}

	public static bool IsWithin(int value, int minimum, int maximum)
	{
		return value >= minimum && value <= maximum;
	}

	public static bool IsNotWithin(int value, int minimum, int maximum)
	{
		return value < minimum || value > maximum;
	}

	public static bool IsWithin(int value, int minimum, int maximum, out bool wasBelow)
	{
		if (maximum < minimum)
		{
			SwitchInt(ref minimum, ref maximum);
		}
		bool result;
		if (value <= minimum)
		{
			result = false;
			wasBelow = true;
		}
		else if (value >= maximum)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotWithin(int value, int minimum, int maximum, out bool wasBelow)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow);
	}

	public static bool IsWithin(int value, int minimum, int maximum, out int outsideBy)
	{
		bool wasBelow;
		return IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotWithin(int value, int minimum, int maximum, out int outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out outsideBy);
	}

	public static bool IsWithin(int value, int minimum, int maximum, out bool wasBelow, out int outsideBy)
	{
		bool result = IsWithin(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotWithin(int value, int minimum, int maximum, out bool wasBelow, out int outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsBetween(int value, int maximum)
	{
		return IsBetween(value, 0, maximum);
	}

	public static bool IsNotBetween(int value, int maximum)
	{
		return !IsBetween(value, 0, maximum);
	}

	public static bool IsBetween(int value, int minimum, int maximum)
	{
		return value > minimum && value < maximum;
	}

	public static bool IsNotBetween(int value, int minimum, int maximum)
	{
		return value <= minimum || value >= maximum;
	}

	public static bool IsBetween(int value, int minimum, int maximum, out bool wasBelow)
	{
		if (maximum < minimum)
		{
			SwitchInt(ref minimum, ref maximum);
		}
		bool result;
		if (value < minimum)
		{
			result = false;
			wasBelow = true;
		}
		else if (value > maximum)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotBetween(int value, int minimum, int maximum, out bool wasBelow)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow);
	}

	public static bool IsBetween(int value, int minimum, int maximum, out int outsideBy)
	{
		bool wasBelow;
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotBetween(int value, int minimum, int maximum, out int outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out outsideBy);
	}

	public static bool IsBetween(int value, int minimum, int maximum, out bool wasBelow, out int outsideBy)
	{
		bool result = IsBetween(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotBetween(int value, int minimum, int maximum, out bool wasBelow, out int outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsWithin(float value, float maximum)
	{
		return IsWithin(value, 0f, maximum);
	}

	public static bool IsNotWithin(float value, float maximum)
	{
		return !IsWithin(value, 0f, maximum);
	}

	public static bool IsWithin(float value, float minimum, float maximum)
	{
		return value >= minimum && value <= maximum;
	}

	public static bool IsNotWithin(float value, float minimum, float maximum)
	{
		return value < minimum || value > maximum;
	}

	public static bool IsWithin(float value, float minimum, float maximum, out bool wasBelow)
	{
		if (maximum < minimum)
		{
			Switch(ref minimum, ref maximum);
		}
		bool result;
		if (value < minimum)
		{
			result = false;
			wasBelow = true;
		}
		else if (value > maximum)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotWithin(float value, float minimum, float maximum, out bool wasBelow)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow);
	}

	public static bool IsWithin(float value, float minimum, float maximum, out float outsideBy)
	{
		bool wasBelow;
		return IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotWithin(float value, float minimum, float maximum, out float outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out outsideBy);
	}

	public static bool IsWithin(float value, float minimum, float maximum, out bool wasBelow, out float outsideBy)
	{
		bool result = IsWithin(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotWithin(float value, float minimum, float maximum, out bool wasBelow, out float outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsBetween(float value, float maximum)
	{
		return IsBetween(value, 0f, maximum);
	}

	public static bool IsNotBetween(float value, float maximum)
	{
		return !IsBetween(value, 0f, maximum);
	}

	public static bool IsBetween(float value, float minimum, float maximum)
	{
		return value > minimum && value < maximum;
	}

	public static bool IsNotBetween(float value, float minimum, float maximum)
	{
		return value <= minimum || value >= maximum;
	}

	public static bool IsBetween(float value, float minimum, float maximum, out bool wasBelow)
	{
		if (maximum < minimum)
		{
			Switch(ref minimum, ref maximum);
		}
		bool result;
		if (value <= minimum)
		{
			result = false;
			wasBelow = true;
		}
		else if (value >= maximum)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotBetween(float value, float minimum, float maximum, out bool wasBelow)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow);
	}

	public static bool IsBetween(float value, float minimum, float maximum, out float outsideBy)
	{
		bool wasBelow;
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotBetween(float value, float minimum, float maximum, out float outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out outsideBy);
	}

	public static bool IsBetween(float value, float minimum, float maximum, out bool wasBelow, out float outsideBy)
	{
		bool result = IsBetween(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotBetween(float value, float minimum, float maximum, out bool wasBelow, out float outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsWithin(int value, int? minimum, int? maximum)
	{
		return (!minimum.HasValue || value > minimum.Value) && (!maximum.HasValue || value < maximum.Value);
	}

	public static bool IsNotWithin(int value, int? minimum, int? maximum)
	{
		return !IsWithin(value, minimum, maximum);
	}

	public static bool IsWithin(int value, int? minimum, int? maximum, out bool wasBelow)
	{
		bool result;
		if (minimum.HasValue && minimum.HasValue && value <= minimum.Value)
		{
			result = false;
			wasBelow = true;
		}
		else if (maximum.HasValue && maximum.HasValue && value >= maximum.Value)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotWithin(int value, int? minimum, int? maximum, out bool wasBelow)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow);
	}

	public static bool IsWithin(int value, int? minimum, int? maximum, out int outsideBy)
	{
		bool wasBelow;
		return IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotWithin(int value, int? minimum, int? maximum, out int outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out outsideBy);
	}

	public static bool IsWithin(int value, int? minimum, int? maximum, out bool wasBelow, out int outsideBy)
	{
		bool result = IsWithin(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotWithin(int value, int? minimum, int? maximum, out bool wasBelow, out int outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsBetween(int value, int? minimum, int? maximum)
	{
		return (!minimum.HasValue || value >= minimum.Value) && (!maximum.HasValue || value <= maximum.Value);
	}

	public static bool IsNotBetween(int value, int? minimum, int? maximum)
	{
		return !IsBetween(value, minimum, maximum);
	}

	public static bool IsBetween(int value, int? minimum, int? maximum, out bool wasBelow)
	{
		bool result;
		if (minimum.HasValue && minimum.HasValue && value < minimum.Value)
		{
			result = false;
			wasBelow = true;
		}
		else if (maximum.HasValue && maximum.HasValue && value > maximum.Value)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotBetween(int value, int? minimum, int? maximum, out bool wasBelow)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow);
	}

	public static bool IsBetween(int value, int? minimum, int? maximum, out int outsideBy)
	{
		bool wasBelow;
		return IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotBetween(int value, int? minimum, int? maximum, out int outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out outsideBy);
	}

	public static bool IsBetween(int value, int? minimum, int? maximum, out bool wasBelow, out int outsideBy)
	{
		bool result = IsBetween(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotBetween(int value, int? minimum, int? maximum, out bool wasBelow, out int outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsWithin(float value, float? minimum, float? maximum)
	{
		return (!minimum.HasValue || !(value <= minimum.Value)) && (!maximum.HasValue || !(value >= maximum.Value));
	}

	public static bool IsNotWithin(float value, float? minimum, float? maximum)
	{
		return !IsWithin(value, minimum, maximum);
	}

	public static bool IsWithin(float value, float? minimum, float? maximum, out bool wasBelow)
	{
		bool result;
		if (minimum.HasValue && minimum.HasValue && value <= minimum.Value)
		{
			result = false;
			wasBelow = true;
		}
		else if (maximum.HasValue && maximum.HasValue && value >= maximum.Value)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotWithin(float value, float? minimum, float? maximum, out bool wasBelow)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow);
	}

	public static bool IsWithin(float value, float? minimum, float? maximum, out float outsideBy)
	{
		bool wasBelow;
		return IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotWithin(float value, float? minimum, float? maximum, out float outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out outsideBy);
	}

	public static bool IsWithin(float value, float? minimum, float? maximum, out bool wasBelow, out float outsideBy)
	{
		bool result = IsWithin(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotWithin(float value, float? minimum, float? maximum, out bool wasBelow, out float outsideBy)
	{
		return !IsWithin(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsBetween(float value, float? minimum, float? maximum)
	{
		return (!minimum.HasValue || !(value < minimum.Value)) && (!maximum.HasValue || !(value > maximum.Value));
	}

	public static bool IsNotBetween(float value, float? minimum, float? maximum)
	{
		return !IsBetween(value, minimum, maximum);
	}

	public static bool IsBetween(float value, float? minimum, float? maximum, out bool wasBelow)
	{
		bool result;
		if (minimum.HasValue && minimum.HasValue && value < minimum.Value)
		{
			result = false;
			wasBelow = true;
		}
		else if (maximum.HasValue && maximum.HasValue && value > maximum.Value)
		{
			result = false;
			wasBelow = false;
		}
		else
		{
			result = true;
			wasBelow = false;
		}
		return result;
	}

	public static bool IsNotBetween(float value, float? minimum, float? maximum, out bool wasBelow)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow);
	}

	public static bool IsBetween(float value, float? minimum, float? maximum, out float outsideBy)
	{
		bool wasBelow;
		return IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	public static bool IsNotBetween(float value, float? minimum, float? maximum, out float outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out outsideBy);
	}

	public static bool IsBetween(float value, float? minimum, float? maximum, out bool wasBelow, out float outsideBy)
	{
		bool result = IsBetween(value, minimum, maximum, out wasBelow);
		outsideBy = GetOutsideBy(value, minimum, maximum, result, wasBelow);
		return result;
	}

	public static bool IsNotBetween(float value, float? minimum, float? maximum, out bool wasBelow, out float outsideBy)
	{
		return !IsBetween(value, minimum, maximum, out wasBelow, out outsideBy);
	}

	private static int GetOutsideBy(int value, int minimum, int maximum, bool result, bool wasBelow)
	{
		if (result)
		{
			return 0;
		}
		if (wasBelow)
		{
			return minimum - value;
		}
		return value - maximum;
	}

	private static int GetOutsideBy(int value, int? minimum, int? maximum, bool result, bool wasBelow)
	{
		if (result)
		{
			return 0;
		}
		if (wasBelow)
		{
			return minimum.Value - value;
		}
		return value - maximum.Value;
	}

	private static float GetOutsideBy(float value, float minimum, float maximum, bool result, bool wasBelow)
	{
		if (result)
		{
			return 0f;
		}
		if (wasBelow)
		{
			return minimum - value;
		}
		return value - maximum;
	}

	private static float GetOutsideBy(float value, float? minimum, float? maximum, bool result, bool wasBelow)
	{
		if (result)
		{
			return 0f;
		}
		if (wasBelow)
		{
			return minimum.Value - value;
		}
		return value - maximum.Value;
	}

	public static bool DoesBorder(int value, int minimum, int maximum)
	{
		return value == minimum && value == maximum;
	}

	public static bool DoesNotBorder(int value, int minimum, int maximum)
	{
		return value != minimum || value != maximum;
	}

	public static bool DoesBorder(int value, IntPair range)
	{
		return DoesBorder(value, range.x, range.y);
	}

	public static bool DoesNotBorder(int value, IntPair range)
	{
		return DoesNotBorder(value, range.x, range.y);
	}

	public static bool DoesBorder(int value, RangeInt range)
	{
		return DoesBorder(value, range.Min, range.Max);
	}

	public static bool DoesNotBorder(int value, RangeInt range)
	{
		return DoesNotBorder(value, range.Min, range.Max);
	}

	public static bool DoesBorder01(int value)
	{
		return DoesBorder(value, 0, 1);
	}

	public static bool DoesNotBorder01(int value)
	{
		return DoesNotBorder(value, 0, 1);
	}

	public static bool DoesBorder11(int value)
	{
		return DoesBorder(value, -1, 1);
	}

	public static bool DoesNotBorder11(int value)
	{
		return DoesNotBorder(value, -1, 1);
	}

	public static bool DoesBorder(int value, int? minimum, int? maximum)
	{
		return (minimum.HasValue && value == minimum.Value) || (maximum.HasValue && value == maximum.Value);
	}

	public static bool DoesNotBorder(int value, int? minimum, int? maximum)
	{
		return !DoesBorder(value, minimum, maximum);
	}

	public static bool DoesBorder(float value, float? minimum, float? maximum)
	{
		return (minimum.HasValue && value == minimum.Value) || (maximum.HasValue && value == maximum.Value);
	}

	public static bool DoesNotBorder(float value, float? minimum, float? maximum)
	{
		return !DoesBorder(value, minimum, maximum);
	}

	public static int Clamp01(int value)
	{
		return Clamp(value, 0, 1);
	}

	public static int Clamp11(int value)
	{
		return Clamp(value, -1, 1);
	}

	public static int Clamp(int value, int min, int max)
	{
		if (value > max)
		{
			return max;
		}
		if (value < min)
		{
			return min;
		}
		return value;
	}

	public static float Clamp01(float value)
	{
		return Clamp(value, 0f, 1f);
	}

	public static float Clamp11(float value)
	{
		return Clamp(value, -1f, 1f);
	}

	public static float Clamp(float value, float min, float max)
	{
		if (value > max)
		{
			return max;
		}
		if (value < min)
		{
			return min;
		}
		return value;
	}

	public static void Clamp01(ref int value)
	{
		value = Clamp01(value);
	}

	public static void Clamp11(ref int value)
	{
		value = Clamp11(value);
	}

	public static void Clamp(ref int value, int min, int max)
	{
		value = Clamp(value, min, max);
	}

	public static void Clamp01(ref float value)
	{
		value = Clamp01(value);
	}

	public static void Clamp11(ref float value)
	{
		value = Clamp11(value);
	}

	public static void Clamp(ref float value, float min, float max)
	{
		value = Clamp(value, min, max);
	}

	public static float Clamp(float value, float? min, float? max)
	{
		if (max.HasValue && max.HasValue && value > max.Value)
		{
			return max.Value;
		}
		if (min.HasValue && min.HasValue && value < min.Value)
		{
			return min.Value;
		}
		return value;
	}

	public static float Clamp01(float value, out bool wasWithin)
	{
		return Clamp(value, 0f, 1f, out wasWithin);
	}

	public static float Clamp11(float value, out bool wasWithin)
	{
		return Clamp(value, -1f, 1f, out wasWithin);
	}

	public static float Clamp(float value, float min, float max, out bool wasWithin)
	{
		float result;
		if (value > max)
		{
			result = max;
			wasWithin = false;
		}
		else if (value < min)
		{
			result = min;
			wasWithin = false;
		}
		else
		{
			result = value;
			wasWithin = true;
		}
		return result;
	}

	public static float Clamp01(float value, out bool wasBelow, out bool wasAbove)
	{
		return Clamp(value, 0f, 1f, out wasBelow, out wasAbove);
	}

	public static float Clamp11(float value, out bool wasBelow, out bool wasAbove)
	{
		return Clamp(value, -1f, 1f, out wasBelow, out wasAbove);
	}

	public static float Clamp(float value, float min, float max, out bool wasBelowMin, out bool wasAboveMax)
	{
		float result;
		if (value > max)
		{
			result = max;
			wasBelowMin = false;
			wasAboveMax = true;
		}
		else if (value < min)
		{
			result = min;
			wasBelowMin = true;
			wasAboveMax = false;
		}
		else
		{
			result = value;
			wasBelowMin = false;
			wasAboveMax = false;
		}
		return result;
	}

	public static Vector2 Clamp01(Vector2 value)
	{
		return Clamp(value, 0f, 1f);
	}

	public static Vector2 Clamp11(Vector2 value)
	{
		return Clamp(value, -1f, 1f);
	}

	public static Vector2 Clamp(Vector2 value, float min, float max)
	{
		value.x = Clamp(value.x, min, max);
		value.y = Clamp(value.y, min, max);
		return value;
	}

	public static Vector3 Clamp01(Vector3 value)
	{
		return Clamp(value, 0f, 1f);
	}

	public static Vector3 Clamp11(Vector3 value)
	{
		return Clamp(value, -1f, 1f);
	}

	public static Vector3 Clamp(Vector3 value, float min, float max)
	{
		value.x = Clamp(value.x, min, max);
		value.y = Clamp(value.y, min, max);
		value.z = Clamp(value.z, min, max);
		return value;
	}

	public static Vector4 Clamp01(Vector4 value)
	{
		return Clamp(value, 0f, 1f);
	}

	public static Vector4 Clamp11(Vector4 value)
	{
		return Clamp(value, -1f, 1f);
	}

	public static Vector4 Clamp(Vector4 value, float min, float max)
	{
		value.x = Clamp(value.x, min, max);
		value.y = Clamp(value.y, min, max);
		value.z = Clamp(value.z, min, max);
		value.w = Clamp(value.w, min, max);
		return value;
	}

	public static int BoolTo01Int(bool boolValue)
	{
		return boolValue ? 1 : 0;
	}

	public static int BoolTo11Int(bool boolValue)
	{
		return boolValue ? 1 : (-1);
	}

	public static float BoolTo01(bool boolValue)
	{
		return (!boolValue) ? -0f : 1f;
	}

	public static float BoolTo11(bool boolValue)
	{
		return (!boolValue) ? (-1f) : 1f;
	}

	public static float FlooredAsFloat(float value)
	{
		return Rounded(value, true);
	}

	public static float CeiledAsFloat(float value)
	{
		return Rounded(value, false);
	}

	public static float RoundedAsFloat(float value, bool roundDown)
	{
		float remainder;
		return RoundedAsFloat(value, roundDown, out remainder);
	}

	public static float RoundedAsFloat(float value, bool roundDown, out float remainder)
	{
		bool flag = IsNegative(value);
		if (flag)
		{
			value *= -1f;
			roundDown = !roundDown;
		}
		remainder = value % 1f;
		float num = value - remainder;
		if (!roundDown)
		{
			num += 1f;
		}
		if (flag)
		{
			num *= -1f;
		}
		return num;
	}

	public static float HalfFlooredAsFloat(float value)
	{
		return Rounded(value / 2f, true);
	}

	public static float HalfCeiledAsFloat(float value)
	{
		return Rounded(value / 2f, false);
	}

	public static float HalfCeiledAsFloat(float value, bool roundDown)
	{
		return Rounded(value / 2f, roundDown);
	}

	public static int Floored(float value)
	{
		return Rounded(value, true);
	}

	public static int Ceiled(float value)
	{
		return Rounded(value, false);
	}

	public static int Rounded(float value, bool roundDown)
	{
		return (int)RoundedAsFloat(value, roundDown);
	}

	public static int HalfFloored(float value)
	{
		return Rounded(value / 2f, true);
	}

	public static int HalfCeiled(float value)
	{
		return Rounded(value / 2f, false);
	}

	public static int HalfRounded(float value, bool roundDown)
	{
		return Rounded(value / 2f, roundDown);
	}

	public static int HalfIntFloored(int value)
	{
		return HalfRounded(value, true);
	}

	public static int HalfIntCeiled(int value)
	{
		return HalfRounded(value, false);
	}

	public static int HalfIntRounded(int value, bool roundDown)
	{
		if (IsEven(value))
		{
			return value / 2;
		}
		float num = value;
		return Rounded(num / 2f, roundDown);
	}

	public static int Floored(float value, out float remainder)
	{
		return RoundedToInt(value, true, out remainder);
	}

	public static int Ceiled(float value, out float remainder)
	{
		return RoundedToInt(value, false, out remainder);
	}

	public static int RoundedToInt(float value, bool roundDown, out float remainder)
	{
		return (int)RoundedAsFloat(value, roundDown, out remainder);
	}

	public static int HalfFloored(float value, out float remainder)
	{
		return RoundedToInt(value / 2f, true, out remainder);
	}

	public static int HalfCeiled(float value, out float difference)
	{
		return RoundedToInt(value / 2f, false, out difference);
	}

	public static int HalfCeiled(float value, bool roundDown, out float difference)
	{
		return RoundedToInt(value / 2f, roundDown, out difference);
	}

	public static int FlooredDivision(float value, float divider)
	{
		return RoundedDivision(value, divider, true);
	}

	public static int CeiledDivision(float value, float divider)
	{
		return RoundedDivision(value, divider, false);
	}

	public static int RoundedDivision(float value, float divider, bool roundDown)
	{
		return Rounded(value / divider, roundDown);
	}

	public static int FlooredMultiplication(float value, float multiplier)
	{
		return RoundedMultiplication(value, multiplier, true);
	}

	public static int CeiledMultiplication(float value, float multiplier)
	{
		return RoundedMultiplication(value, multiplier, false);
	}

	public static int RoundedMultiplication(float value, float multiplier, bool roundDown)
	{
		return Rounded(value * multiplier, roundDown);
	}

	public static int FlooredIntDivision(int value, int divider)
	{
		return RoundedDivision(value, divider, true);
	}

	public static int CeiledIntDivision(int value, int divider)
	{
		return RoundedDivision(value, divider, false);
	}

	public static int RoundedIntDivision(int value, int divider, bool roundDown)
	{
		return Rounded(IntDivision(value, divider), roundDown);
	}

	public static float IntDivision(int value, int divider)
	{
		return (float)value / (float)divider;
	}

	public static float IntDivisionOf1(int divider)
	{
		return 1f / (float)divider;
	}

	public static float IntMultiplication(int value, float multiplier)
	{
		return (float)value * multiplier;
	}

	public static float IntMultiplication(int value, float multiplier, bool inversed)
	{
		if (inversed)
		{
			multiplier = 1f - multiplier;
		}
		return IntMultiplication(value, multiplier);
	}

	public static int IntMultiplicationAsInt(int value, float multiplier)
	{
		return (int)IntMultiplication(value, multiplier);
	}

	public static int IntMultiplicationAsInt(int value, float multiplier, bool inversed)
	{
		return (int)IntMultiplication(value, multiplier, inversed);
	}

	public static string FractionStringInt(int value, int divider)
	{
		string text;
		if (value == 0)
		{
			text = "0";
		}
		else if (divider == 0)
		{
			text = "infinity";
		}
		else
		{
			bool flag = IsNegative(value);
			bool flag2 = IsNegative(divider);
			bool flag3 = flag ^ flag2;
			if (flag)
			{
				value *= -1;
			}
			if (flag2)
			{
				divider *= -1;
			}
			if (value < divider)
			{
				text = string.Format("{0}/{1}", value, divider);
			}
			else
			{
				text = FlooredDivision(value, divider).ToString();
				int num = value % divider;
				if (num > 0)
				{
					text = string.Format("{0} {1}", text, FractionStringInt(num, divider));
				}
			}
			if (flag3)
			{
				text = "-" + text;
			}
		}
		return text;
	}

	public static int RemainderInt(int value, int divider)
	{
		int resultSansRemainder;
		return RemainderInt(value, divider, out resultSansRemainder);
	}

	public static int RemainderInt(int value, int divider, out int resultSansRemainder)
	{
		int result = value % divider;
		resultSansRemainder = FlooredDivision(value, divider);
		return result;
	}

	public static int MultiplesInt(int smallerValue, int largerValue)
	{
		int remainder;
		return MultiplesInt(smallerValue, largerValue, out remainder);
	}

	public static int MultiplesInt(int smallerValue, int largerValue, out int remainder)
	{
		int num;
		if (smallerValue == 0)
		{
			num = 0;
			remainder = 0;
		}
		else
		{
			bool flag = IsNegative(largerValue);
			bool flag2 = IsNegative(smallerValue);
			bool flag3 = false;
			if (flag || flag2)
			{
				if (flag && flag2)
				{
					largerValue = -largerValue;
					smallerValue = -smallerValue;
				}
				else
				{
					if (flag)
					{
						largerValue = -largerValue;
					}
					if (flag2)
					{
						smallerValue = -smallerValue;
					}
					flag3 = true;
				}
			}
			remainder = largerValue % smallerValue;
			num = (largerValue - remainder) / smallerValue;
			if (flag3)
			{
				num *= -1;
			}
		}
		return num;
	}

	public static float MultiplesIntAsFloat(int smallerValue, int largerValue)
	{
		return MultiplesInt(smallerValue, largerValue);
	}

	public static float MultiplesIntAsFloat(int smallerValue, int largerValue, out int remainder)
	{
		return MultiplesInt(smallerValue, largerValue, out remainder);
	}

	public static float MultiplesIntAsFloat(int smallerValue, int largerValue, out float remainder)
	{
		int remainder2;
		float result = MultiplesInt(smallerValue, largerValue, out remainder2);
		remainder = remainder2;
		return result;
	}

	public static int Multiples(float smallerValue, float largerValue)
	{
		float remainder;
		return (int)MultiplesAsFloat(smallerValue, largerValue, out remainder);
	}

	public static int Multiples(float smallerValue, float largerValue, out int remainder)
	{
		float remainder2;
		int result = (int)MultiplesAsFloat(smallerValue, largerValue, out remainder2);
		remainder = (int)remainder2;
		return result;
	}

	public static float MultiplesAsFloat(float smallerValue, float largerValue)
	{
		float remainder;
		return MultiplesAsFloat(smallerValue, largerValue, out remainder);
	}

	public static float MultiplesAsFloat(float smallerValue, float largerValue, out float remainder)
	{
		float num;
		if (smallerValue == 0f)
		{
			num = 0f;
			remainder = 0f;
		}
		else
		{
			bool flag = IsNegative(largerValue);
			bool flag2 = IsNegative(smallerValue);
			bool flag3 = false;
			if (flag || flag2)
			{
				if (flag && flag2)
				{
					largerValue = 0f - largerValue;
					smallerValue = 0f - smallerValue;
				}
				else
				{
					if (flag)
					{
						largerValue = 0f - largerValue;
					}
					if (flag2)
					{
						smallerValue = 0f - smallerValue;
					}
					flag3 = true;
				}
			}
			remainder = largerValue % smallerValue;
			num = (largerValue - remainder) / smallerValue;
			if (flag3)
			{
				num *= -1f;
			}
		}
		return num;
	}

	public static bool HasEvenMultiples(float smallerValue, float largerValue)
	{
		return IsEven(MultiplesAsFloat(smallerValue, largerValue));
	}

	public static bool HasOddMultiples(float smallerValue, float largerValue)
	{
		return IsOdd(MultiplesAsFloat(smallerValue, largerValue));
	}

	public static bool HasEvenMultiples(float smallerValue, float largerValue, out float remainder)
	{
		return IsEven(MultiplesAsFloat(smallerValue, largerValue, out remainder));
	}

	public static bool HasOddMultiples(float smallerValue, float largerValue, out float remainder)
	{
		return IsOdd(MultiplesAsFloat(smallerValue, largerValue, out remainder));
	}

	public static float Modulus1(float value)
	{
		return CalculateModulus(value, 1f);
	}

	public static void Modulus1(ref float value)
	{
		CalculateModulus(ref value, 1f);
	}

	public static float Modulus1(float value, out bool wasNegative)
	{
		return CalculateModulus(value, 1f, out wasNegative);
	}

	public static void Modulus1(ref float value, out bool wasNegative)
	{
		CalculateModulus(ref value, 1f, out wasNegative);
	}

	public static float Modulus1Abs(float value)
	{
		return CalculateModulusAbs(value, 1f);
	}

	public static void Modulus1Abs(ref float value)
	{
		CalculateModulusAbs(ref value, 1f);
	}

	public static float Modulus1Abs(float value, out bool wasNegative)
	{
		return CalculateModulusAbs(value, 1f, out wasNegative);
	}

	public static void Modulus1Abs(ref float value, out bool wasNegative)
	{
		CalculateModulusAbs(ref value, 1f, out wasNegative);
	}

	public static float Modulus(float value, float modulator)
	{
		return CalculateModulus(value, modulator);
	}

	public static void Modulus(ref float value, float modulator)
	{
		CalculateModulus(ref value, modulator);
	}

	public static float Modulus(float value, float modulator, out bool wasNegative)
	{
		return CalculateModulus(value, modulator, out wasNegative);
	}

	public static void Modulus(ref float value, float modulator, out bool wasNegative)
	{
		CalculateModulus(ref value, modulator, out wasNegative);
	}

	public static float ModulusAbs(float value, float modulator)
	{
		return CalculateModulusAbs(value, modulator);
	}

	public static void ModulusAbs(ref float value, float modulator)
	{
		CalculateModulusAbs(ref value, modulator);
	}

	public static float ModulusAbs(float value, float modulator, out bool wasNegative)
	{
		return CalculateModulusAbs(value, modulator, out wasNegative);
	}

	public static void ModulusAbs(ref float value, float modulator, out bool wasNegative)
	{
		CalculateModulusAbs(ref value, modulator, out wasNegative);
	}

	private static void CalculateModulusAbs(ref float value, float modulator, out bool wasNegative)
	{
		wasNegative = value < 0f;
		if (wasNegative)
		{
			value *= -1f;
		}
		value %= modulator;
	}

	private static void CalculateModulus(ref float value, float modulator, out bool wasNegative)
	{
		CalculateModulusAbs(ref value, modulator, out wasNegative);
		if (wasNegative)
		{
			value *= -1f;
		}
	}

	private static float CalculateModulusAbs(float value, float modulator, out bool wasNegative)
	{
		CalculateModulusAbs(ref value, modulator, out wasNegative);
		return value;
	}

	private static float CalculateModulusAbs(float value, float modulator)
	{
		bool wasNegative;
		CalculateModulusAbs(ref value, modulator, out wasNegative);
		return value;
	}

	private static void CalculateModulusAbs(ref float value, float modulator)
	{
		bool wasNegative;
		CalculateModulusAbs(ref value, modulator, out wasNegative);
	}

	private static float CalculateModulus(float value, float modulator, out bool wasNegative)
	{
		CalculateModulus(ref value, modulator, out wasNegative);
		return value;
	}

	private static float CalculateModulus(float value, float modulator)
	{
		bool wasNegative;
		CalculateModulus(ref value, modulator, out wasNegative);
		return value;
	}

	private static void CalculateModulus(ref float value, float modulator)
	{
		bool wasNegative;
		CalculateModulus(ref value, modulator, out wasNegative);
	}

	public static float PowerInt(int value, int power)
	{
		if (power > 0)
		{
			if (power == 1)
			{
				return value;
			}
			int num = value;
			for (int i = 2; i <= power; i++)
			{
				num *= value;
			}
			return num;
		}
		if (power < 0)
		{
			return 1f / Power(value, -power);
		}
		return 1f;
	}

	public static float SquaredInt(int value)
	{
		return PowerInt(value, 2);
	}

	public static float Power(float value, int power)
	{
		if (power > 0)
		{
			if (power == 1)
			{
				return value;
			}
			float num = value;
			for (int i = 2; i <= power; i++)
			{
				num *= value;
			}
			return num;
		}
		if (power < 0)
		{
			return 1f / Power(value, -power);
		}
		return 1f;
	}

	public static float Squared(float value)
	{
		return Power(value, 2);
	}

	public static float Sqrd(float value)
	{
		return Power(value, 2);
	}

	public static float Sqrt(float value)
	{
		return Mathf.Sqrt(value);
	}

	public static bool IndexIsWithin<ArrayType>(int index, ArrayType[] array)
	{
		return array != null && IsWithin(index, 0, array.Length - 1);
	}

	public static bool IndexIsWithin(int index, int arrayLength)
	{
		return IsWithin(index, 0, arrayLength - 1);
	}

	public static bool IndexIsNotWithin<ArrayType>(int index, ArrayType[] array)
	{
		return array != null && !IndexIsWithin(index, array);
	}

	public static bool IndexIsNotWithin(int index, int arrayLength)
	{
		return !IndexIsWithin(index, arrayLength);
	}

	public static int Reversed(int index, int max)
	{
		return max - 1 - index;
	}

	public static int[] Flipped(int[] array)
	{
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[Reversed(i, array2.Length)];
		}
		return array2;
	}

	public static int ToWithin(int index, int arrayLength)
	{
		if (arrayLength <= 0)
		{
			Debug.Log(string.Format("MTUT: ERROR: EXV: recieved unexpected value for 'arrayLength' of '{0}'", arrayLength));
			return 0;
		}
		while (index < 0)
		{
			index += arrayLength;
		}
		while (index >= arrayLength)
		{
			index -= arrayLength;
		}
		return index;
	}

	public static bool ArrayContains<ArrayType>(ArrayType[] array, ArrayType queryValue)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(queryValue))
			{
				return true;
			}
		}
		return false;
	}

	public static bool ArrayContains(IntPair[] array, int queryIValue)
	{
		int returnJValue;
		return ArrayContains(array, queryIValue, out returnJValue);
	}

	public static bool ArrayContains(IntPair[] array, int queryIValue, out int returnJValue)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].x == queryIValue)
			{
				returnJValue = array[i].y;
				return true;
			}
		}
		returnJValue = -1;
		return false;
	}

	public static bool ArrayContainsNot<ArrayType>(ArrayType[] array, ArrayType queryValue)
	{
		return !ArrayContains(array, queryValue);
	}

	public static bool ArrayContainsNot(IntPair[] array, int queryIValue)
	{
		return !ArrayContains(array, queryIValue);
	}

	public static bool ArrayContainsNot(IntPair[] array, int queryIValue, out int returnJValue)
	{
		return !ArrayContains(array, queryIValue, out returnJValue);
	}

	public static int Indexed(int value, string charactersString)
	{
		return Indexed(value, charactersString.Length);
	}

	public static int Indexed(int value, int arrayLength)
	{
		if (value >= arrayLength)
		{
			value -= arrayLength * MultiplesInt(arrayLength, value);
		}
		else if (value < 0)
		{
			value += arrayLength * (MultiplesInt(arrayLength, -value) + 1);
		}
		return value;
	}

	public static int Indexed(int value, RangeInt range)
	{
		return Indexed(value, range.Min, range.Max);
	}

	public static int Indexed(int value, Index range)
	{
		return Indexed(value, range.Min, range.Max);
	}

	public static int Indexed(int value, int minIndex, int maxIndex)
	{
		if (minIndex == maxIndex)
		{
			return minIndex;
		}
		if (minIndex > maxIndex)
		{
			int num = maxIndex;
			maxIndex = minIndex;
			minIndex = num;
		}
		int num2 = 0;
		int num3 = 0;
		if (minIndex != 0)
		{
			num2 = minIndex;
			maxIndex -= minIndex;
			minIndex = 0;
		}
		if (maxIndex < 0)
		{
			maxIndex *= -1;
			num3 = maxIndex;
		}
		maxIndex++;
		return Indexed(value - num2, maxIndex) + num2 - num3;
	}

	public static float IndexedFloat(float value, float max)
	{
		if (value > max)
		{
			value -= max * MultiplesAsFloat(max, value);
		}
		else if (value < 0f)
		{
			value += max * (MultiplesAsFloat(max, 0f - value) + 1f);
		}
		return value;
	}

	public static float IndexedFloat(float value, Range range)
	{
		return IndexedFloat(value, range.Min, range.Max);
	}

	public static float IndexedFloat(float value, float min, float max)
	{
		float valueShift;
		float valueShiftFinal;
		if (RangeShifted(ref min, ref max, out valueShift, out valueShiftFinal))
		{
			return IndexedFloat(value - valueShift, max) + valueShift - valueShiftFinal;
		}
		return min;
	}

	public static int Rebounded(int value, int arrayLength)
	{
		return Rebounded(value, 0, arrayLength - 1);
	}

	public static int Rebounded(int value, RangeInt range)
	{
		return Rebounded(value, range.Min, range.Max);
	}

	public static int Rebounded(int value, int minIndex, int maxIndex)
	{
		int result = minIndex;
		if (minIndex != maxIndex)
		{
			if (minIndex > maxIndex)
			{
				SwitchInt(ref minIndex, ref maxIndex);
			}
			int num = 0;
			bool flag = false;
			if (value > maxIndex)
			{
				num = value - maxIndex;
			}
			else if (value < minIndex)
			{
				num = minIndex - value;
				flag = true;
			}
			if (num != 0)
			{
				int num2 = DistanceInt(minIndex, maxIndex);
				bool flag2 = false;
				if (IsLarger(num, num2))
				{
					int num3 = Multiples(num, num2);
					num -= num3 * num2;
					flag2 = IsEven(num3);
				}
				if (flag2)
				{
					flag = !flag;
				}
				result = ((!flag) ? (maxIndex - num) : (minIndex + num));
			}
		}
		return result;
	}

	public static float ReboundedFloat(float value, Range range)
	{
		return ReboundedFloat(value, range.Min, range.Max);
	}

	public static float ReboundedFloat(float value, float min, float max)
	{
		float result = min;
		if (min != max)
		{
			if (min > max)
			{
				Switch(ref min, ref max);
			}
			float num = 0f;
			bool flag = false;
			if (value > max)
			{
				num = value - max;
			}
			else if (value < min)
			{
				num = min - value;
				flag = true;
			}
			if (num != 0f)
			{
				float num2 = Distance(min, max);
				bool flag2 = false;
				if (IsLarger(num, num2))
				{
					int num3 = Multiples(num, num2);
					num -= (float)num3 * num2;
					flag2 = IsEven(num3);
				}
				if (flag2)
				{
					flag = !flag;
				}
				result = ((!flag) ? (max - num) : (min + num));
			}
		}
		return result;
	}

	private static bool RangeShifted(ref float min, ref float max, out float valueShift, out float valueShiftFinal)
	{
		valueShift = 0f;
		valueShiftFinal = 0f;
		if (min == max)
		{
			return false;
		}
		if (min > max)
		{
			float num = max;
			max = min;
			min = num;
		}
		if (min != 0f)
		{
			valueShift = min;
			max -= min;
			min = 0f;
		}
		if (max < 0f)
		{
			max *= -1f;
			valueShiftFinal = max;
		}
		return true;
	}

	public static int GetLength(UnityEngine.Object[] array)
	{
		int result = 0;
		if (array == null)
		{
			result = array.Length;
		}
		return result;
	}

	public static int[,] ToParamsIndices(int paramsLength, int entriesNeeded, int partsPerEntry)
	{
		int num = entriesNeeded * partsPerEntry;
		int num2 = ((paramsLength >= num) ? partsPerEntry : ((paramsLength > partsPerEntry) ? FlooredDivision(paramsLength, entriesNeeded) : 0));
		int[,] array = new int[entriesNeeded, partsPerEntry];
		int num3 = 0;
		for (int i = 0; i < partsPerEntry; i++)
		{
			bool flag = i >= num2;
			for (int j = 0; j < entriesNeeded; j++)
			{
				int num4 = ((!flag) ? j : 0);
				array[j, i] = num3 + num4;
			}
			int num5 = (flag ? 1 : entriesNeeded);
			num3 += num5;
		}
		return array;
	}

	public static int MinutesSince(DateTime givenDateTime)
	{
		return (DateTime.UtcNow - givenDateTime).Minutes;
	}

	public static int SecondsSince(DateTime givenDateTime)
	{
		return (DateTime.UtcNow - givenDateTime).Seconds;
	}

	public static int MinutesSince(DateTime? givenDateTime)
	{
		int totalSince;
		TryGetTimeSince(givenDateTime, true, out totalSince);
		return totalSince;
	}

	public static int SecondsSince(DateTime? givenDateTime)
	{
		int totalSince;
		TryGetTimeSince(givenDateTime, false, out totalSince);
		return totalSince;
	}

	public static int MinutesSince(string givenDateTimeString)
	{
		int totalSince;
		TryGetTimeSince(givenDateTimeString, true, out totalSince);
		return totalSince;
	}

	public static int SecondsSince(string givenDateTimeString)
	{
		int totalSince;
		TryGetTimeSince(givenDateTimeString, false, out totalSince);
		return totalSince;
	}

	public static bool TryGetMinutesSince(DateTime? givenDateTime, out int minutesSince)
	{
		return TryGetTimeSince(givenDateTime, true, out minutesSince);
	}

	public static bool TryGetSecondsSince(DateTime? givenDateTime, out int secondsSince)
	{
		return TryGetTimeSince(givenDateTime, false, out secondsSince);
	}

	public static bool TryGetMinutesSince(string givenDateTimeString, out int minutesSince)
	{
		return TryGetTimeSince(givenDateTimeString, true, out minutesSince);
	}

	public static bool TryGetSecondsSince(string givenDateTimeString, out int secondsSince)
	{
		return TryGetTimeSince(givenDateTimeString, false, out secondsSince);
	}

	private static bool TryGetTimeSince(DateTime? givenDateTime, bool getInMinutes, out int totalSince)
	{
		if (givenDateTime.HasValue)
		{
			totalSince = ((!getInMinutes) ? SecondsSince(givenDateTime) : MinutesSince(givenDateTime));
			return true;
		}
		totalSince = -1;
		return false;
	}

	private static bool TryGetTimeSince(string givenDateTimeString, bool getInMinutes, out int totalSince)
	{
		DateTime result;
		if (DateTime.TryParseExact(givenDateTimeString, "s", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
		{
			totalSince = ((!getInMinutes) ? SecondsSince(result) : MinutesSince(result));
			return true;
		}
		totalSince = -1;
		return false;
	}

	public static string ToPriceString(float price)
	{
		return ToPriceString(price, false, false);
	}

	public static string ToPriceString(float price, bool cents)
	{
		return ToPriceString(price, cents, false);
	}

	public static string ToPriceString(float price, bool cents, bool spaced)
	{
		string text;
		if (price == 0f)
		{
			text = "FREE";
		}
		else
		{
			bool flag = price < 0f;
			if (flag)
			{
				price *= -1f;
			}
			if (!cents || !(Abs(price) < 1f))
			{
				text = ((!spaced) ? ("$" + price.ToString("0.00")) : ("$ " + price.ToString("0.00")));
			}
			else
			{
				float num = (int)(price * 100f);
				text = ((!spaced) ? (num + "c") : (num + " c"));
			}
			if (flag)
			{
				text = ((!spaced) ? ("-" + text) : ("- " + text));
			}
		}
		return text;
	}

	public static string ToRangeString(float rangeMax)
	{
		return ToRangeString(0f, rangeMax, -1f);
	}

	public static string ToRangeString(float rangeMin, float rangeMax)
	{
		return ToRangeString(rangeMin, rangeMax, -1f);
	}

	public static string ToRangeString(float rangeMin, float rangeMax, float length)
	{
		string text = rangeMin + " to " + rangeMax;
		if (length != -1f)
		{
			text = text + ", length: " + length;
		}
		return "range: " + text;
	}

	public static bool CanParseToTime(float seconds, out string timeString)
	{
		bool result = false;
		timeString = null;
		float num = seconds;
		for (int i = 0; i < timeUnits.Length; i++)
		{
			if ((num < timeUnits[i].inNext) | (i == timeUnits.Length - 1))
			{
				int num2 = i;
				timeString = timeUnits[num2].ToString(num);
				result = true;
				break;
			}
			num /= timeUnits[i].inNext;
		}
		return result;
	}

	public static bool CannotParseToTime(float seconds, out string timeString)
	{
		return !CanParseToTime(seconds, out timeString);
	}

	public static float FromNullable(float? nullableFloat, float valueIfNull = 0f)
	{
		return (!nullableFloat.HasValue) ? valueIfNull : nullableFloat.Value;
	}

	public static int FromNullableInt(int? nullableInt, int valueIfNull = 0)
	{
		return (!nullableInt.HasValue) ? valueIfNull : nullableInt.Value;
	}

	public static string StringFromNullable(float? nullableFloat, string valueIfNull = "NULL", string stringFormat = "0.00")
	{
		return (!nullableFloat.HasValue) ? valueIfNull : nullableFloat.Value.ToString(stringFormat);
	}

	public static string StringFromNullableInt(int? nullableInt, string valueIfNull = "NULL")
	{
		return (!nullableInt.HasValue) ? valueIfNull : nullableInt.Value.ToString();
	}

	public static float ToDeviation(float value, float minOrMax, float median, bool normalized = true)
	{
		float num = value - median;
		if (!normalized)
		{
			return num;
		}
		float num2 = Abs(median - minOrMax);
		return num / num2;
	}

	public static float Lerp(float first, float second, float weight)
	{
		weight = Clamp01(weight);
		return first * (1f - weight) + second * weight;
	}

	public static int Lerp(int first, int second, float weight)
	{
		return (int)Lerp((float)first, (float)second, weight);
	}

	public static int Lerp(int[] array, int firstIndex, int secondIndex, float weight)
	{
		return (int)Lerp((float)array[firstIndex], (float)array[secondIndex], weight);
	}

	public static float Lerp(float[] array, int firstIndex, int secondIndex, float weight)
	{
		return Lerp(array[firstIndex], array[secondIndex], weight);
	}

	public static void ToGrid(int number, int rowWidth, out int rowPos, out int colPos)
	{
		ToGrid(number, rowWidth, false, out rowPos, out colPos);
	}

	public static void ToGrid(int number, int rowWidth, bool startAtZero, out int rowPos, out int colPos)
	{
		rowPos = number;
		colPos = 1;
		while (rowPos > rowWidth)
		{
			rowPos -= rowWidth;
			colPos++;
		}
		if (startAtZero)
		{
			rowPos--;
			colPos--;
		}
	}

	public static void ToGrid(int number, int rowWidth, out float rowPos, out float colPos)
	{
		ToGrid(number, rowWidth, false, out rowPos, out colPos);
	}

	public static void ToGrid(int number, int rowWidth, bool startAtZero, out float rowPos, out float colPos)
	{
		int rowPos2;
		int colPos2;
		ToGrid(number, rowWidth, false, out rowPos2, out colPos2);
		rowPos = rowPos2;
		colPos = colPos2;
	}

	public static void ToGrid(int number, int rowWidth, out Vector2 gridPos)
	{
		ToGrid(number, rowWidth, false, out gridPos);
	}

	public static void ToGrid(int number, int rowWidth, bool startAtZero, out Vector2 gridPos)
	{
		float rowPos;
		float colPos;
		ToGrid(number, rowWidth, false, out rowPos, out colPos);
		gridPos = new Vector2(rowPos, colPos);
	}

	public static IntPair ToGridPair(int number, int rowWidth)
	{
		return ToGridPair(number, rowWidth, false);
	}

	public static IntPair ToGridPair(int number, int rowWidth, bool startAtZero)
	{
		int rowPos;
		int colPos;
		ToGrid(number, rowWidth, startAtZero, out rowPos, out colPos);
		return new IntPair(rowPos, colPos);
	}

	public static Vector2 ToGridVector(int number, int rowWidth)
	{
		return ToGridVector(number, rowWidth, false);
	}

	public static Vector2 ToGridVector(int number, int rowWidth, bool startAtZero)
	{
		float rowPos;
		float colPos;
		ToGrid(number, rowWidth, startAtZero, out rowPos, out colPos);
		return new Vector2(rowPos, colPos);
	}

	public static float ToZollyDistance(float fieldOfView)
	{
		return 0.5f / TanFromDegrees(fieldOfView / 2f);
	}

	public static float ToZollyDistance(float fieldOfView, float startingFieldOfView)
	{
		return ToZollyDistance(fieldOfView) - ToZollyDistance(startingFieldOfView);
	}

	public static float ToZollyDistance(float fieldOfView, float startingFieldOfView, float multIfLesserThanStarting, float multIfGreaterThanStarting)
	{
		float num = ToZollyDistance(fieldOfView, startingFieldOfView);
		float num2 = ((!(fieldOfView < startingFieldOfView)) ? multIfGreaterThanStarting : multIfLesserThanStarting);
		return num * num2;
	}

	public static Quad WithinRanges(float distanceSigned, Range rangePos, Range rangeNeg, out float distancePercent)
	{
		distancePercent = 0f;
		int value = 0;
		if (!((distanceSigned > rangePos.Max) | (distanceSigned < 0f - rangeNeg.Max)))
		{
			if (rangePos.Contains(distanceSigned))
			{
				distancePercent = rangePos.ToPercent(distanceSigned);
				value = 1;
			}
			else if (rangeNeg.Contains(0f - distanceSigned))
			{
				distancePercent = rangeNeg.ToPercent(0f - distanceSigned);
				value = 2;
			}
			else
			{
				distancePercent = 1f;
				value = 3;
			}
		}
		return new Quad(value);
	}
}
