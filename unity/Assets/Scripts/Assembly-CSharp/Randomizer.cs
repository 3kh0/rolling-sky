using System;
using UnityEngine;

public class Randomizer
{
	public class RandomRoller
	{
		public float chance;

		public RandomRoller(float chance)
		{
			if (chance < 0f || chance > 1f)
			{
				Debug.Log(string.Format("MTUT: ERROR: OOR: recieved a 'chance' with a value of {0} out the expected range of 0 to 1", chance));
				this.chance = 0.5f;
			}
			this.chance = chance;
		}

		public bool Roll()
		{
			return RollAgainst(chance);
		}
	}

	public class RandomNext
	{
		private const bool debugMode = false;

		private MathUtils.RangeInt range;

		private MathUtils.BufferInt previous;

		private int previousTotal;

		private bool hasValue;

		private bool singleMode;

		private int single;

		private bool indexMode;

		private int? unconfirmedChoice;

		public RandomNext(int max)
		{
			ConfigureRandNext(0, max, null, false);
		}

		public RandomNext(int min, int max)
		{
			ConfigureRandNext(min, max, null, false);
		}

		public RandomNext(int min, int max, int startingAt)
		{
			ConfigureRandNext(min, max, startingAt, false);
		}

		protected RandomNext()
		{
		}

		protected void ConfigureRandNext(int min, int max, int? startingAt, bool indexMode)
		{
			this.indexMode = indexMode;
			if (indexMode)
			{
				max--;
			}
			if (min == max)
			{
				singleMode = true;
				single = min;
			}
			else
			{
				range = new MathUtils.RangeInt(min, max, indexMode);
				previous = new MathUtils.BufferInt(MathUtils.HalfCeiled(range.Length));
			}
			if (startingAt.HasValue)
			{
				ForceTo(startingAt.Value);
			}
		}

		public virtual void Reconfigure(int max, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(0, max, null, clearCurrentValues);
		}

		public virtual void Reconfigure(int min, int max, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(min, max, null, clearCurrentValues);
		}

		public virtual void Reconfigure(int min, int max, int startingAt, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(min, max, startingAt, clearCurrentValues);
		}

		protected void ReconfigureRandChoice(int newMin, int newMax, int? startingAt, bool clearCurrentValues)
		{
			if (indexMode)
			{
				newMax--;
			}
			if (newMin == newMax)
			{
				singleMode = true;
				single = newMin;
			}
			else
			{
				singleMode = false;
				if (range == null)
				{
					range = new MathUtils.RangeInt(newMin, newMax, indexMode);
				}
				else
				{
					range.SetMinMax(newMin, newMax);
				}
				if (previous == null)
				{
					previous = new MathUtils.BufferInt(MathUtils.HalfCeiled(range.Length));
				}
				else
				{
					previous.SetNewLength(MathUtils.HalfCeiled(range.Length), clearCurrentValues);
				}
			}
			if (startingAt.HasValue)
			{
				ForceTo(startingAt.Value);
			}
		}

		public int GetNext()
		{
			return GetNext(true);
		}

		protected int GetNext(bool confirmed)
		{
			if (singleMode)
			{
				return single;
			}
			int num = range.Random;
			if (previous.IsEmpty)
			{
				previous.Add(num);
			}
			else
			{
				bool flag = false;
				for (int i = 0; i < range.Length; i++)
				{
					if (previous.Contains(num))
					{
						num = range.GetAppendedWithin(num);
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					previous.Add(num);
				}
				else
				{
					num = range.GetRandomWithin();
					Debug.LogError(string.Format("MTUT: ERROR: UCC: randNext class was unable to choose a new next choice after {0} attempts - clearing buffer of previous choices({1})and choosing {2} instead", range.Length, previous.ToString(), num));
					previous.Clear();
					previous.Add(num);
				}
			}
			if (!hasValue)
			{
				hasValue = true;
			}
			if (confirmed)
			{
				ConfirmChoice(num);
			}
			else
			{
				unconfirmedChoice = num;
			}
			return num;
		}

		protected void ConfirmChoice(int choiceToConfirm)
		{
			previous.Add(choiceToConfirm);
		}

		protected void ConfirmChoice()
		{
			if (unconfirmedChoice.HasValue)
			{
				ConfirmChoice(unconfirmedChoice.Value);
				unconfirmedChoice = null;
			}
			else
			{
				Debug.LogWarning("RNNT: Attempt to confirm choice when no choice has been confirmed");
			}
		}

		public int GetValue()
		{
			if (singleMode)
			{
				return single;
			}
			if (!hasValue)
			{
				return GetNext();
			}
			return previous.GetLast();
		}

		public int ForceTo(int forcedValue)
		{
			if (range.HasWithin(forcedValue))
			{
				previous.Add(forcedValue);
				if (!hasValue)
				{
					hasValue = true;
				}
			}
			return forcedValue;
		}

		public bool Contains(int queryValue)
		{
			return range.HasWithin(queryValue);
		}

		public override string ToString()
		{
			string text = "Rand choice: [";
			if (hasValue)
			{
				return text + previous.GetLast() + range.ToString() + previous.ToString() + "]";
			}
			return text + "empty]";
		}
	}

	public class RandomNextConfirmed : RandomNext
	{
		public RandomNextConfirmed(int max)
		{
			ConfigureRandNext(0, max, null, false);
		}

		public RandomNextConfirmed(int min, int max)
		{
			ConfigureRandNext(min, max, null, false);
		}

		public RandomNextConfirmed(int min, int max, int startingAt)
		{
			ConfigureRandNext(min, max, startingAt, false);
		}

		public int GetNextUnconfirmed()
		{
			return GetNext(false);
		}

		public void ConfirmLastChoice()
		{
			ConfirmChoice();
		}
	}

	public class RandomIndex : RandomNext
	{
		public RandomIndex(int arrayLength)
		{
			ConfigureRandNext(0, arrayLength, null, true);
		}

		public RandomIndex(int minIndex, int arrayLength)
		{
			ConfigureRandNext(minIndex, arrayLength, null, true);
		}

		public RandomIndex(int minIndex, int arrayLength, int startingAt)
		{
			ConfigureRandNext(minIndex, arrayLength, startingAt, true);
		}

		public override void Reconfigure(int arrayLength, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(0, arrayLength, null, clearCurrentValues);
		}

		public override void Reconfigure(int minIndex, int arrayLength, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(minIndex, arrayLength, null, clearCurrentValues);
		}

		public override void Reconfigure(int minIndex, int arrayLength, int startingAt, bool clearCurrentValues = true)
		{
			ReconfigureRandChoice(minIndex, arrayLength, startingAt, clearCurrentValues);
		}
	}

	public class RandomIndexConfirmed : RandomNext
	{
		public RandomIndexConfirmed(int arrayLength)
		{
			ConfigureRandNext(0, arrayLength, null, true);
		}

		public RandomIndexConfirmed(int minIndex, int arrayLength)
		{
			ConfigureRandNext(minIndex, arrayLength, null, true);
		}

		public RandomIndexConfirmed(int minIndex, int arrayLength, int startingAt)
		{
			ConfigureRandNext(minIndex, arrayLength, startingAt, true);
		}

		public int GetNextUnconfirmed()
		{
			return GetNext(false);
		}

		public void ConfirmLastChoice()
		{
			ConfirmChoice();
		}
	}

	private const float randIntSpacer = 0.4f;

	private static int? randomSeed = GameManager.RandomSeed;

	private static bool haveSeededRandomizer;

	public static void SeedRandom(int seed)
	{
		UnityEngine.Random.seed = seed;
		haveSeededRandomizer = true;
	}

	public static float GetRandom()
	{
		return GetSeededRandomValue();
	}

	public static float GetRandom(float maximum)
	{
		return maximum * GetSeededRandomValue();
	}

	public static float GetRandom(float minimum, float maximum)
	{
		float seededRandomValue = GetSeededRandomValue();
		float num = 1f - seededRandomValue;
		return minimum * num + maximum * seededRandomValue;
	}

	public static float GetRandomSigned()
	{
		return 2f * GetRandom() - 1f;
	}

	public static float GetRandomSigned(float maximum)
	{
		return 2f * GetRandom(maximum) - maximum;
	}

	public static float GetRandomSigned(float minimum, float maximum)
	{
		return GetRandom(minimum, maximum) * GetRandomSign();
	}

	public static float GetRandomAppetite(float foldpoint, float margin, float weight)
	{
		float minimumAppetite = GetMinimumAppetite(foldpoint, margin, weight);
		return GetRandom(minimumAppetite, weight);
	}

	public static float GetRandomSignedAppetite(float foldpoint, float margin, float weight)
	{
		float minimumAppetite = GetMinimumAppetite(foldpoint, margin, weight);
		return GetRandom(minimumAppetite, weight) * GetRandomSign();
	}

	public static float GetMinimumAppetite(float foldpoint, float margin, float weight)
	{
		if (weight < foldpoint)
		{
			if (weight < margin)
			{
				return 0f;
			}
			return weight - margin;
		}
		return (foldpoint - margin) * MathUtils.ToPercent(weight, foldpoint, 1f, true);
	}

	public static float GetTrustReward(float weight, float targetWeight)
	{
		return GetTrustReward(weight, targetWeight, 0.2f);
	}

	public static float GetTrustReward(float weight, float targetWeight, float forgiveness)
	{
		if (weight <= targetWeight)
		{
			return weight / targetWeight;
		}
		return MathUtils.ThroughPercent(weight, targetWeight, targetWeight + forgiveness, 1f, 0f);
	}

	public static float GetRandomWeight(float deviation, float center = 1f)
	{
		return GetRandom(center - deviation, center + deviation);
	}

	public static int GetRandomInt(int max)
	{
		return GetRandomInt(0, max);
	}

	public static int GetRandomInt(int min, int max)
	{
		float seededRandomValue = GetSeededRandomValue();
		float num = 1f - seededRandomValue;
		return (int)((float)min * num + ((float)max + 0.4f) * seededRandomValue);
	}

	public static int GetRandomInt(float max)
	{
		return GetRandomInt(0f, max);
	}

	public static int GetRandomInt(float min, float max)
	{
		float seededRandomValue = GetSeededRandomValue();
		float num = 1f - seededRandomValue;
		return (int)(min * num + max * seededRandomValue);
	}

	public static int GetRandomIndex(int arrayLength)
	{
		return GetRandomIndex(arrayLength, 0);
	}

	public static int GetRandomIndex(int arrayLength, int minIndex)
	{
		return GetRandomInt(minIndex, arrayLength - 1);
	}

	public static ArrayType GetRandomEntry<ArrayType>(ArrayType[] array)
	{
		return array[GetRandomIndex(array.Length)];
	}

	public static ArrayType GetRandomEntry<ArrayType>(ArrayType[] array, int minIndex)
	{
		return array[GetRandomIndex(array.Length, minIndex)];
	}

	public static Vector3 GetRandomVector3(float maxX, float maxY, float maxZ)
	{
		return GetRandomVector3(maxX, maxY, maxZ, false);
	}

	public static Vector3 GetRandomVector3(float maxX, float maxY, float maxZ, bool includeNegatives)
	{
		float minX;
		float minY;
		float minZ;
		if (includeNegatives)
		{
			minX = 0f - maxX;
			minY = 0f - maxY;
			minZ = 0f - maxZ;
		}
		else
		{
			minX = 0f;
			minY = 0f;
			minZ = 0f;
		}
		return GetRandomVector3(minX, maxX, minY, maxY, minZ, maxZ);
	}

	public static Vector3 GetRandomVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
	{
		float random = GetRandom(minX, maxX);
		float random2 = GetRandom(minY, maxY);
		float random3 = GetRandom(minZ, maxZ);
		return new Vector3(random, random2, random3);
	}

	public static Vector3 GetRandomVector3(Vector3 min, Vector3 max)
	{
		return GetRandomVector3(min.x, max.x, min.y, max.y, min.z, max.z);
	}

	public static EnumType GetRandomEnum<EnumType>(int minEnumValue, int maxEnumValue)
	{
		int randomInt = GetRandomInt(minEnumValue, maxEnumValue);
		EnumType parsedEnum;
		if (EnumUtils.CannotParseToEnum<EnumType>(randomInt, out parsedEnum))
		{
			parsedEnum = default(EnumType);
			Debug.LogError(string.Format("MTUT: ERROR: Unable to convert random integer {0} to a {1} enum - returning default {1} of {2} instead", randomInt, typeof(EnumType), parsedEnum));
		}
		return parsedEnum;
	}

	public static int GetRandomEnumAsInt<EnumType>(int minEnumValue, int maxEnumValue)
	{
		return GetRandomInt(minEnumValue, maxEnumValue);
	}

	public static bool RollAgainst(float probabilityValue)
	{
		return GetSeededRandomValue() <= probabilityValue;
	}

	public static bool FlipACoin()
	{
		return GetSeededRandomValue() <= 0.5f;
	}

	public static float GetRandomSign()
	{
		if (FlipACoin())
		{
			return 1f;
		}
		return -1f;
	}

	public static float GetRandomIntSign()
	{
		int num = (FlipACoin() ? 1 : (-1));
		return num;
	}

	public static ArrayType RandomEntry<ArrayType>(ArrayType[] array)
	{
		if (array == null || array.Length == 0)
		{
			return default(ArrayType);
		}
		int randomInt = GetRandomInt(array.Length - 1);
		return array[randomInt];
	}

	public static ArrayType RandomEntry<ArrayType>(ArrayType[] array, int maxIndex)
	{
		if (array == null || array.Length == 0)
		{
			return default(ArrayType);
		}
		if (maxIndex >= array.Length)
		{
			maxIndex = array.Length - 1;
		}
		int randomInt = GetRandomInt(maxIndex);
		return array[randomInt];
	}

	private static float GetSeededRandomValue()
	{
		if (!haveSeededRandomizer)
		{
			if (randomSeed.HasValue)
			{
				UnityEngine.Random.seed = randomSeed.Value;
			}
			else
			{
				UnityEngine.Random.seed = (int)DateTime.Now.Ticks;
			}
			haveSeededRandomizer = true;
		}
		return UnityEngine.Random.value;
	}
}
