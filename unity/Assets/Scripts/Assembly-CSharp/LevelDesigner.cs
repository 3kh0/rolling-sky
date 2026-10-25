using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
	public class WorldData
	{
		private MathUtils.Range velocity;

		private MathUtils.RangeInt difficulty;

		public string Name { get; private set; }

		public int Length { get; private set; }

		public float LengthAsFloat { get; private set; }

		public bool IsEndless { get; private set; }

		public bool IsNotEndless { get; private set; }

		public float BeatLength { get; private set; }

		public int RowOfMaxVelocity { get; private set; }

		public int DifficultyLength { get; private set; }

		public bool FixedDifficulty { get; private set; }

		public bool VariableDifficulty { get; private set; }

		public bool RandomDifficulty { get; private set; }

		public int RowOfMaxDifficulty { get; private set; }

		public float RowOfMaxDifficultyAsFloat { get; private set; }

		public float VelocityMin
		{
			get
			{
				return velocity.Min;
			}
		}

		public float VelocityMax
		{
			get
			{
				return velocity.Max;
			}
		}

		public float Acceleration { get; private set; }

		public bool VelocityIsMaxed { get; private set; }

		public bool VelocityIsStatic { get; private set; }

		public bool VelocityIsNotMaxed
		{
			get
			{
				return !VelocityIsMaxed;
			}
		}

		public bool VelocityIsDynamic
		{
			get
			{
				return !VelocityIsStatic;
			}
		}

		public int DifficultyMin
		{
			get
			{
				return difficulty.Min;
			}
		}

		public int DifficultyMax
		{
			get
			{
				return difficulty.Max;
			}
		}

		public bool DifficultyIsMaxed { get; private set; }

		public bool DifficultyIsNotMaxed
		{
			get
			{
				return !DifficultyIsMaxed;
			}
		}

		public WorldData(string name)
		{
			ConfigureWorldData(name, null, 1f, 1f, 1f, 6f, null, null, null);
		}

		public WorldData(string name, int? goalLength, float startingVelocityPercent, float maxVelocityPercent, float worldPercentForMaxVelocity, float beatLength)
		{
			ConfigureWorldData(name, goalLength, startingVelocityPercent, maxVelocityPercent, worldPercentForMaxVelocity, beatLength, null, null, null);
		}

		public WorldData(string name, int? goalLength, float startingVelocityPercent, float maxVelocityPercent, float worldPercentForMaxVelocity, float beatLength, int difficultyMin, int difficultyMax)
		{
			ConfigureWorldData(name, goalLength, startingVelocityPercent, maxVelocityPercent, worldPercentForMaxVelocity, beatLength, difficultyMin, difficultyMax, null);
		}

		public WorldData(string name, int? goalLength, float startingVelocityPercent, float maxVelocityPercent, float worldPercentForMaxVelocity, float beatLength, int difficultyMin, int difficultyMax, float worldPercentForMaxDifficulty)
		{
			ConfigureWorldData(name, goalLength, startingVelocityPercent, maxVelocityPercent, worldPercentForMaxVelocity, beatLength, difficultyMin, difficultyMax, worldPercentForMaxDifficulty);
		}

		private void ConfigureWorldData(string name, int? goalLength, float startingVelocityPercent, float maxVelocityPercent, float worldPercentForMaxVelocity, float beatLength, int? difficultyMin, int? difficultyMax, float? worldPercentForMaxDifficulty)
		{
			Name = name;
			IsNotEndless = goalLength.HasValue;
			IsEndless = !IsNotEndless;
			Length = ((!IsNotEndless) ? (-1) : goalLength.Value);
			LengthAsFloat = Length;
			BeatLength = beatLength;
			VelocityIsStatic = startingVelocityPercent == maxVelocityPercent;
			float min = GlobalVelocityRange.FromPercent(startingVelocityPercent);
			float max = GlobalVelocityRange.FromPercent(maxVelocityPercent);
			velocity = new MathUtils.Range(min, max);
			Acceleration = velocity.Average;
			RowOfMaxVelocity = MathUtils.Floored(LengthAsFloat * worldPercentForMaxVelocity);
			if (difficultyMin.HasValue && difficultyMax.HasValue)
			{
				difficulty = new MathUtils.RangeInt(difficultyMin.Value, difficultyMax.Value);
				if (worldPercentForMaxDifficulty.HasValue)
				{
					RowOfMaxDifficultyAsFloat = LengthAsFloat * worldPercentForMaxDifficulty.Value;
					RowOfMaxDifficulty = MathUtils.Floored(RowOfMaxDifficultyAsFloat);
					VariableDifficulty = true;
					RandomDifficulty = false;
				}
				else
				{
					RandomDifficulty = true;
					VariableDifficulty = false;
				}
				FixedDifficulty = false;
			}
			else
			{
				FixedDifficulty = true;
				VariableDifficulty = false;
				RandomDifficulty = false;
			}
			Reset();
		}

		public void Reset()
		{
			DifficultyIsMaxed = FixedDifficulty;
			VelocityIsMaxed = false;
		}

		public int GetDifficulty(int rowNum)
		{
			if (FixedDifficulty)
			{
				return GameManager.WorldIndex;
			}
			if (RandomDifficulty)
			{
				return difficulty.Random;
			}
			if (DifficultyIsMaxed)
			{
				return difficulty.Max;
			}
			if (rowNum >= RowOfMaxDifficulty)
			{
				DifficultyIsMaxed = true;
				return difficulty.Max;
			}
			float num = rowNum;
			float percent = num / RowOfMaxDifficultyAsFloat;
			return MathUtils.Floored(difficulty.FromPercent(percent));
		}

		public float GetVelocity(int rowNum)
		{
			if (VelocityIsMaxed || VelocityIsStatic)
			{
				return velocity.Max;
			}
			bool wasBelow;
			bool wasAbove;
			float percent = MathUtils.ToPercentInt01(rowNum, 0, RowOfMaxVelocity, out wasBelow, out wasAbove);
			if (wasAbove)
			{
				VelocityIsMaxed = true;
			}
			return velocity.FromPercent(percent);
		}

		public float TimeOf(int rowNum)
		{
			if (VelocityIsStatic)
			{
				return (float)rowNum / velocity.Max;
			}
			float num = GetVelocity(rowNum);
			float num2 = (MathUtils.Squared(num) - MathUtils.Squared(velocity.Min)) / (2f * (float)rowNum);
			return (num - velocity.Min) / num2;
		}

		public int RowOf(int secondsPlayed)
		{
			int num;
			if (VelocityIsStatic)
			{
				num = MathUtils.Floored(velocity.Max * (float)secondsPlayed);
			}
			else
			{
				float num2 = TimeOf(Length);
				float max = velocity.Max;
				float min = velocity.Min;
				float num3 = (max - min) / num2;
				float num4 = secondsPlayed;
				float value = min * num4 + 0.5f * num3 * MathUtils.Squared(num4);
				num = MathUtils.Floored(value);
			}
			return num + 3;
		}
	}

	public class Period
	{
		private ArrayUtils.WeightedListRandom<SegmentType> choices;

		private readonly SegmentType? singleChoice;

		public readonly int Time;

		public Period(int periodTime, params SegmentType[] periodChoices)
		{
			Time = periodTime;
			if (periodChoices.Length == 1)
			{
				singleChoice = periodChoices[0];
			}
			else
			{
				choices = new ArrayUtils.WeightedListRandom<SegmentType>(periodChoices);
			}
		}

		public void ClearPrevious()
		{
			if (!singleChoice.HasValue)
			{
				choices.ClearPrevious();
			}
		}

		public SegmentType ChooseSegmentType()
		{
			if (singleChoice.HasValue)
			{
				return singleChoice.Value;
			}
			return choices.GetRandomNext();
		}
	}

	public class WorldRhythm
	{
		private Period[] periods;

		private int currentPeriod;

		private int lastPeriod;

		private bool inLastPeriod;

		public WorldRhythm(params Period[] rhythmPeriods)
		{
			periods = rhythmPeriods;
			lastPeriod = periods.Length - 1;
			Reset();
		}

		public void Reset()
		{
			currentPeriod = 0;
			inLastPeriod = currentPeriod == lastPeriod;
			for (int i = 0; i < periods.Length; i++)
			{
				periods[i].ClearPrevious();
			}
		}

		public SegmentType ChooseSegmentType(float secondsIntoLevel, out bool nextPeriod)
		{
			nextPeriod = false;
			if (!inLastPeriod)
			{
				while ((float)periods[currentPeriod].Time <= secondsIntoLevel)
				{
					currentPeriod++;
					nextPeriod = true;
					if (currentPeriod == lastPeriod)
					{
						inLastPeriod = true;
						break;
					}
				}
			}
			return periods[currentPeriod].ChooseSegmentType();
		}
	}

	public struct Theme
	{
		private const bool defaultGenerateTrees = false;

		private const bool defaultGenerateTowers = false;

		private const bool defaultGenerateAltGround = false;

		private const bool defaultGeneratePyramids = false;

		private const bool defaultGeneratePalmTrees = false;

		private const bool defaultGenerateHammers = true;

		private const bool defaultGenerateSlashers = false;

		private const bool defaultGenerateHammersAndSlashers = false;

		public readonly int WorldThemeNumber;

		public readonly int WorldThemeIndex;

		public readonly int Distance;

		public readonly float TransitionTime;

		public readonly bool GenerateTrees;

		public readonly bool GenerateTowers;

		public readonly bool GenerateAltGround;

		public readonly bool GeneratePyramids;

		public readonly bool GeneratePalmTrees;

		public readonly bool GenerateHammers;

		public readonly bool GenerateSlashers;

		public readonly bool GenerateHammersAndSlashers;

		// Donor 1.4.7.1 flags. Legacy constructors default these to false.
		public readonly bool GenerateBuildings;
		public readonly bool GenerateFloodLights;
		public readonly bool GenerateAltPalmTrees;
		public readonly bool GenerateTeslas;
		public readonly bool FilmGrainEffect;
		public readonly bool RetroEffect;
		public readonly bool GreenMonitorEffect;
		public readonly bool ColorMonitorEffect;

		public Theme(int themeTime, int worldThemeNumber, float transitionTime) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = false;
			GenerateTowers = false;
			GenerateAltGround = false;
			GeneratePyramids = false;
			GeneratePalmTrees = false;
			GenerateHammers = true;
			GenerateSlashers = false;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = useTrees;
			GenerateTowers = false;
			GenerateAltGround = false;
			GeneratePyramids = false;
			GeneratePalmTrees = false;
			GenerateHammers = true;
			GenerateSlashers = false;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees, bool useTowers) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = useTrees;
			GenerateTowers = useTowers;
			GenerateAltGround = false;
			GeneratePyramids = false;
			GeneratePalmTrees = false;
			GenerateHammers = true;
			GenerateSlashers = false;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees, bool useTowers, bool useAltGround) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = useTrees;
			GenerateTowers = useTowers;
			GenerateAltGround = useAltGround;
			GeneratePyramids = false;
			GeneratePalmTrees = false;
			GenerateHammers = true;
			GenerateSlashers = false;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees, bool useTowers, bool useAltGround, bool usePyramids, bool usePalmTrees) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = useTrees;
			GenerateTowers = useTowers;
			GenerateAltGround = useAltGround;
			GeneratePyramids = usePyramids;
			GeneratePalmTrees = usePalmTrees;
			GenerateHammers = true;
			GenerateSlashers = false;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees, bool useTowers, bool useAltGround, bool usePyramids, bool usePalmTrees, bool useSlashers) : this()
		{
			Distance = themeTime;
			WorldThemeNumber = worldThemeNumber;
			WorldThemeIndex = worldThemeNumber - 1;
			TransitionTime = transitionTime;
			GenerateTrees = useTrees;
			GenerateTowers = useTowers;
			GenerateAltGround = useAltGround;
			GeneratePyramids = usePyramids;
			GeneratePalmTrees = usePalmTrees;
			GenerateHammers = !useSlashers;
			GenerateSlashers = useSlashers;
			GenerateHammersAndSlashers = false;
		}

		public Theme(int themeTime, int worldThemeNumber, float transitionTime, bool useTrees, bool useTowers, bool useAltGround, bool usePyramids, bool usePalmTrees, bool useSlashers, bool useBuildings, bool useFloodLights, bool useFilmGrainEffect, bool useAltPalmTrees = false, bool useTeslas = false, bool useRetroEffects = false, bool useGreenMonitorEffects = false, bool useColorMonitorEffect = false)
			: this(themeTime, worldThemeNumber, transitionTime, useTrees, useTowers, useAltGround, usePyramids, usePalmTrees, useSlashers)
		{
			GenerateBuildings = useBuildings;
			GenerateFloodLights = useFloodLights;
			FilmGrainEffect = useFilmGrainEffect;
			GenerateAltPalmTrees = useAltPalmTrees;
			GenerateTeslas = useTeslas;
			RetroEffect = useRetroEffects;
			GreenMonitorEffect = useGreenMonitorEffects;
			ColorMonitorEffect = useColorMonitorEffect;
		}
	}

	public class WorldTheme
	{
		private Theme[] themes;

		private int themeIndexCurrent;

		private int themeIndexGenerating;

		private int themeIndexFinal;

		private bool themeIndexCurrentIsFinal;

		private bool themeIndexGeneratingIsFinal;

		public int CurrentWorldThemeNumber
		{
			get
			{
				return themes[themeIndexCurrent].WorldThemeNumber;
			}
		}

		public int CurrentWorldThemeIndex
		{
			get
			{
				return themes[themeIndexCurrent].WorldThemeIndex;
			}
		}

		public int GeneratingWorldThemeNumber
		{
			get
			{
				return themes[themeIndexGenerating].WorldThemeNumber;
			}
		}

		public int GeneratingWorldThemeIndex
		{
			get
			{
				return themes[themeIndexGenerating].WorldThemeIndex;
			}
		}

		public float CurrentWorldTransition
		{
			get
			{
				return themes[themeIndexCurrent].TransitionTime;
			}
		}

		public bool GenerateTrees
		{
			get
			{
				return themes[themeIndexGenerating].GenerateTrees;
			}
		}

		public bool GenerateTowers
		{
			get
			{
				return themes[themeIndexGenerating].GenerateTowers;
			}
		}

		public bool GeneratePyramids
		{
			get
			{
				return themes[themeIndexGenerating].GeneratePyramids;
			}
		}

		public bool GeneratePalmTrees
		{
			get
			{
				return themes[themeIndexGenerating].GeneratePalmTrees;
			}
		}

		public bool GenerateAltGround
		{
			get
			{
				return themes[themeIndexGenerating].GenerateAltGround;
			}
		}

		public bool GenerateHammers
		{
			get
			{
				return themes[themeIndexGenerating].GenerateHammers;
			}
		}

		public bool GenerateSlashers
		{
			get
			{
				return themes[themeIndexGenerating].GenerateSlashers;
			}
		}

		public bool GenerateHammersAndSlashers
		{
			get
			{
				return themes[themeIndexGenerating].GenerateHammersAndSlashers;
			}
		}

		public bool GenerateBuildings { get { return themes[themeIndexGenerating].GenerateBuildings; } }
		public bool GenerateFloodLights { get { return themes[themeIndexGenerating].GenerateFloodLights; } }
		public bool GenerateAltPalmTrees { get { return themes[themeIndexGenerating].GenerateAltPalmTrees; } }
		public bool GenerateTeslas { get { return themes[themeIndexGenerating].GenerateTeslas; } }
		// These deliberately use the generating cursor, as in the donor.
		public bool FilmGrainEffect { get { return themes[themeIndexGenerating].FilmGrainEffect; } }
		public bool RetroEffect { get { return themes[themeIndexGenerating].RetroEffect; } }
		public bool GreenMonitorEffect { get { return themes[themeIndexGenerating].GreenMonitorEffect; } }
		public bool ColorMonitorEffect { get { return themes[themeIndexGenerating].ColorMonitorEffect; } }
		public int StartingThemeNumber { get { return themes[0].WorldThemeNumber; } }
		public int StartingThemeIndex { get { return themes[0].WorldThemeIndex; } }

		public WorldTheme(params Theme[] worldThemes)
		{
			themes = worldThemes;
			themeIndexFinal = themes.Length - 1;
			Reset();
		}

		public void Reset()
		{
			themeIndexCurrent = 0;
			themeIndexGenerating = 0;
			themeIndexCurrentIsFinal = themeIndexCurrent == themeIndexFinal;
			themeIndexGeneratingIsFinal = themeIndexGenerating == themeIndexFinal;
		}

		public bool ShouldChangeTheme(int rowsIntoLevel)
		{
			bool result = false;
			if (!themeIndexCurrentIsFinal)
			{
				while (!themeIndexCurrentIsFinal && themes[themeIndexCurrent + 1].Distance <= rowsIntoLevel)
				{
					themeIndexCurrent++;
					result = true;
					if (themeIndexCurrent == themeIndexFinal)
					{
						themeIndexCurrentIsFinal = true;
						break;
					}
				}
			}
			return result;
		}

		public void TryChangeGeneratingTheme(int rowsIntoLevel)
		{
			if (themeIndexGeneratingIsFinal)
			{
				return;
			}
			while (!themeIndexGeneratingIsFinal && themes[themeIndexGenerating + 1].Distance <= rowsIntoLevel)
			{
				themeIndexGenerating++;
				if (themeIndexGenerating == themeIndexFinal)
				{
					themeIndexGeneratingIsFinal = true;
					break;
				}
			}
		}
	}

	private const bool debugsEnabled = false;

	private const bool debugPeriods = false;

	private const bool debugTimesUsed = false;

	private const bool debugChains = false;

	private const bool debugAvailability = false;

	private const bool debugDifficulty = false;

	private const bool debugPausing = false;

	private const bool debugFrequencies = false;

	public const int MaxDifficulty = 5;

	public static MathUtils.Range GlobalVelocityRange = new MathUtils.Range(3.75f, 8f);

	private static WorldData[] worldData = new WorldData[41]
	{
		new WorldData("1", 500, 0.525f, 0.525f, 1f, 6f),
		new WorldData("2", 650, 0.625f, 0.625f, 1f, 7f),
		new WorldData("3", 805, 0.725f, 0.8f, 1f, 5f),
		new WorldData("4", 970, 0.8f, 1f, 1f, 6f),
		new WorldData("5", 1170, 0.85f, 1f, 0.5f, 6f),
		new WorldData("6", 1095, 0.85f, 0.9f, 0.9f, 6f),
		new WorldData("7", 1150, 0.85f, 0.9f, 0.9f, 6f),
		new WorldData("8", 914, 0.895f, 0.9f, 0.85f, 6f),
		new WorldData("9", 1118, 0.87f, 0.9f, 0.9f, 6f),
		new WorldData("10", 1112, 0.8f, 0.9f, 1f, 6f),
		new WorldData("11", 1136, 0.85f, 1f, 1f, 6f),
		new WorldData("12", 1100, 0.8f, 0.9f, 1f, 6f),
		new WorldData("13", 1043, 0.85f, 0.85f, 1f, 6f), // Native 14.
		new WorldData("14", 1042, 0.875f, 0.875f, 1f, 7f), // Native 15.
		new WorldData("15", 1079, 0.875f, 0.875f, 1f, 7f), // Native 17.
		new WorldData("16", 1022, 0.85f, 1f, 1f, 7f), // Native 19.
		new WorldData("17", 1188, 0.85f, 0.875f, 1f, 7f), // Native 21, 1.5.6.1.
		new WorldData("18", 1150, 0.85f, 0.9f, 0.9f, 6f), // Native 23, 1.5.6.1.
		new WorldData("19", 1150, 0.85f, 0.85f, 0.85f, 6f), // Native 25, 1.5.6.1.
		new WorldData("20", 650, 0.625f, 0.625f, 1f, 7f), // Native 13.
		new WorldData("21", 1035, 0.725f, 0.8f, 1f, 5f), // Native 16.
		new WorldData("22", 1070, 0.725f, 0.8f, 1f, 7f), // Native 22, 1.5.6.1.
		new WorldData("23", 1040, 0.725f, 0.8f, 1f, 7f), // Native 24, 1.5.6.1.
		// Native 27/28/29/33/35/36/37/38/40/41, 1.7.8.2.
		new WorldData("24", 1130, 0.8f, 0.8f, 0.85f, 6f),
		new WorldData("25", 1142, 0.87f, 1f, 1f, 7f),
		new WorldData("26", 1050, 0.725f, 0.8f, 0.85f, 6f),
		new WorldData("27", 712, 0.6f, 0.7f, 1f, 7f),
		new WorldData("28", 728, 0.625f, 0.7f, 1f, 7f),
		new WorldData("29", 456, 0.45f, 0.625f, 1f, 7f),
		new WorldData("30", 1118, 0.87f, 0.9f, 0.9f, 6f),
		new WorldData("31", 944, 0.625f, 0.625f, 1f, 7f),
		new WorldData("32", 1100, 0.8f, 1f, 1f, 7f),
		new WorldData("33", 845, 0.525f, 0.7f, 1f, 7f),
		new WorldData("34", 780, 0.65f, 0.65f, 1f, 7f),
		new WorldData("35", 946, 0.7f, 0.75f, 1f, 7f),
		new WorldData("36", 1150, 0.85f, 0.85f, 0.85f, 6f),
		new WorldData("37", 1142, 0.87f, 1f, 1f, 7f),
		new WorldData("38", 1105, 0.85f, 0.9f, 1f, 7f),
		new WorldData("39", 1180, 0.87f, 0.9f, 0.9f, 6f),
		new WorldData("40", 764, 0.725f, 0.8f, 0.9f, 6f),
		new WorldData("41", 490, 0.5f, 0.6f, 1f, 6f)
	};

	private static WorldRhythm[] worldRhythms = new WorldRhythm[41]
	{
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		// The 1.7.8.2 donor uses a single Level period for each added world.
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level)),
		new WorldRhythm(new Period(100, SegmentType.Level))
	};

	private static WorldTheme[] worldThemes = new WorldTheme[41]
	{
		new WorldTheme(new Theme(0, 1, 1f), new Theme(105, 2, 1f), new Theme(207, 3, 1f, true), new Theme(312, 9, 1f, false, false, false, false, false, false), new Theme(415, 1, 1f)),
		new WorldTheme(new Theme(0, 2, 1f), new Theme(123, 5, 1f, false, true, true), new Theme(306, 4, 1f), new Theme(551, 2, 1f)),
		new WorldTheme(new Theme(0, 3, 1f, true), new Theme(153, 9, 1f, false, false, false, false, false, true), new Theme(275, 9, 1f, false, false, false, false, false, false), new Theme(420, 2, 1f), new Theme(554, 3, 1f, true), new Theme(691, 5, 1f, false, true, true)),
		new WorldTheme(new Theme(0, 4, 1f), new Theme(236, 7, 1f), new Theme(475, 2, 1f), new Theme(598, 9, 1f), new Theme(721, 7, 1f), new Theme(850, 4, 1f)),
		new WorldTheme(new Theme(0, 5, 1f, false, true, true)),
		// Donor themes 8/9/6 -> runtime slots 10/11/9. Never replace baseline slot 9.
		new WorldTheme(new Theme(0, 10, 1f, false, false, false, false, false, true, true, true, true), new Theme(75, 11, 1f, false, false, false, false, false, true, true, true, false), new Theme(221, 10, 1f, false, false, false, false, false, true, true, true, true), new Theme(347, 11, 1f, false, false, false, false, false, false, true, true, false), new Theme(356, 10, 1f, false, false, false, false, false, false, true, true, true), new Theme(366, 11, 1f, false, false, false, false, false, false, true, true, false), new Theme(809, 9, 1f, false, false, false, false, false, true, true, true, false), new Theme(956, 11, 1f, false, false, false, false, false, false, true, true, false)),
		new WorldTheme(new Theme(0, 12, 1f, false, false, false, false, true, true, true, false, false, true, true, true), new Theme(388, 13, 1f, false, false, false, false, true, false, true, false, false, true, true, true), new Theme(517, 13, 1f, false, false, false, false, true, false, true, false, false, true, true, false, true), new Theme(644, 12, 1f, false, false, false, false, true, false, true, false, false, true, true, true)),
		new WorldTheme(new Theme(0, 16, 1f, true, false, false, false, true, true, false, false, false, true, false, false), new Theme(103, 14, 1f, true, false, false, false, true, false, false, false, false, true, false, false), new Theme(354, 15, 1f, false, false, false, false, true, false, false, false, false, true, true, false), new Theme(582, 16, 1f, true, false, false, false, true, true, false, false, false, true, false, false), new Theme(641, 14, 1f, true, false, false, false, true, false, false, false, false, true, true, false), new Theme(769, 15, 1f, true, false, false, false, true, true, false, false, false, true, false, false)),
		new WorldTheme(new Theme(0, 17, 1f, false, false, false, true, true, true, true, false, false, true, false, false, false, false), new Theme(277, 18, 1f, false, false, false, true, true, false, true, false, false, true, false, false, false, true), new Theme(486, 17, 1f, false, true, false, false, true, false, true, false, false, true, false, false), new Theme(737, 19, 1f, false, false, false, false, true, false, false, false, false, true, true, false, false, false), new Theme(909, 17, 1f, false, false, false, false, true, false, true, false, false, true, false, false)),
		new WorldTheme(new Theme(0, 20, 1f, false, false, false, false, true, true, true, false, false, true, false, false, false, false), new Theme(138, 21, 1f, false, false, false, false, true, true, true, true, false, false, false, false, false, false), new Theme(274, 23, 1f, false, false, false, false, true, false, true, false, false, true, false, false, false), new Theme(551, 20, 1f, false, false, false, false, true, true, true, true, false, true, false, false, false, false), new Theme(831, 22, 1f, false, false, false, false, true, true, true, false, false, true, false, false, false, false), new Theme(971, 20, 1f, false, false, false, false, true, false, true, false, false, true, false, false)),
		new WorldTheme(new Theme(0, 24, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(196, 25, 1f, true, false, false, false, true, true, false, false, false, false, false, false, false, false), new Theme(327, 25, 1f, true, false, false, false, true, true, false, false, false, false, false, false, false, false), new Theme(590, 24, 1f, true, false, false, false, true, true, false, false, false, false, false, false, false, false), new Theme(727, 24, 1f, true, false, false, false, true, true, false, false, false, false, false, false, false, false), new Theme(862, 25, 1f, true, false, false, false, true, true, false, false, false, false, false, false, false, false)),
		new WorldTheme(new Theme(0, 26, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(180, 26, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(332, 26, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(552, 26, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(840, 26, 1f, true, false, false, false, true, true, false, false, false, true, false, false, false, false)),
		new WorldTheme(new Theme(0, 27, 1f, false, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(160, 28, 1f, false, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(412, 27, 1f, false, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(666, 28, 1f, false, false, false, false, true, true, false, false, false, true, false, false, false, false), new Theme(918, 27, 1f, false, false, false, false, true, true, false, false, false, true, false, false, false, false)),
		new WorldTheme(new Theme(0, 29, 1f, false, false, false, false, false, true), new Theme(134, 30, 1f, false, false, false, false, false, true), new Theme(397, 29, 1f, false, false, false, false, false, true), new Theme(529, 30, 1f, false, false, false, false, false, true), new Theme(921, 29, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 32, 1f, false, false, false, false, false, true), new Theme(168, 33, 1f, false, false, false, false, false, true), new Theme(336, 32, 1f, false, false, false, false, false, true), new Theme(546, 33, 1f, false, false, false, false, false, true), new Theme(807, 32, 1f, false, false, false, false, false, true), new Theme(952, 33, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 36, 1f, false, false, false, false, false, true), new Theme(167, 34, 1f, false, false, false, false, false, true), new Theme(308, 35, 1f, false, false, false, false, false, true), new Theme(451, 36, 1f, false, false, false, false, false, true), new Theme(572, 35, 1f, false, false, false, false, false, true), new Theme(719, 34, 1f, false, false, false, false, false, true), new Theme(854, 36, 1f, false, false, false, false, false, true)),
		// Native 21/23/25 from 1.5.6.1 only; earlier schedules remain 1.4.7.1.
		new WorldTheme(new Theme(0, 40, 1f, false, false, false, false, false, true), new Theme(282, 39, 1f, false, false, false, false, false, true), new Theme(440, 40, 1f, false, false, false, false, false, true), new Theme(601, 39, 1f, false, false, false, false, false, true), new Theme(761, 40, 1f, false, false, false, false, false, true), new Theme(921, 39, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 41, 1f, false, false, false, false, false, true), new Theme(141, 42, 1f, false, false, false, false, false, true), new Theme(281, 41, 1f, false, false, false, false, false, true), new Theme(431, 42, 1f, false, false, false, false, false, true), new Theme(571, 41, 1f, false, false, false, false, false, true), new Theme(716, 42, 1f, false, false, false, false, false, true), new Theme(850, 41, 1f, false, false, false, false, false, true), new Theme(1000, 42, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 43, 1f, false, false, false, false, false, true), new Theme(188, 44, 1f, false, false, false, false, false, true), new Theme(327, 43, 1f, false, false, false, false, false, true), new Theme(451, 44, 1f, false, false, false, false, false, true), new Theme(574, 43, 1f, false, false, false, false, false, true), new Theme(750, 44, 1f, false, false, false, false, false, true), new Theme(825, 43, 1f, false, false, false, false, false, true), new Theme(982, 44, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 9, 1f, false, false, false, false, false, false), new Theme(166, 37, 1f, false, false, false, false, false, false), new Theme(292, 9, 1f, false, false, false, false, false, false), new Theme(398, 37, 1f), new Theme(479, 9, 1f, false, false, false, false, false, false)),
		new WorldTheme(new Theme(0, 31, 1f, false, false, false, false, true, true, true, false, false, true, false, false, false, false), new Theme(197, 31, 1f, false, false, false, false, true, true, true, true, false, false, false, false, false, false), new Theme(345, 38, 1f, true), new Theme(510, 31, 1f, false, false, false, false, true, true, true, true, false, true, false, false, false, false), new Theme(725, 38, 1f, true), new Theme(897, 31, 1f, false, false, false, false, true, false, true, false, false, true, false, false)),
		new WorldTheme(new Theme(0, 39, 1f, false, false, false, false, false, true), new Theme(282, 40, 1f, false, false, false, false, false, true), new Theme(440, 39, 1f, false, false, false, false, false, true), new Theme(601, 40, 1f, false, false, false, false, false, true), new Theme(761, 39, 1f, false, false, false, false, false, true), new Theme(921, 40, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 42, 1f, false, false, false, false, false, true), new Theme(141, 41, 1f, false, false, false, false, false, true), new Theme(281, 42, 1f, false, false, false, false, false, true), new Theme(431, 41, 1f, false, false, false, false, false, true), new Theme(571, 42, 1f, false, false, false, false, false, true), new Theme(716, 41, 1f, false, false, false, false, false, true), new Theme(850, 42, 1f, false, false, false, false, false, true), new Theme(1000, 41, 1f, false, false, false, false, false, true)),
		// Exact 1.7.8.2 schedules, using separate runtime palette slots 45..62.
		new WorldTheme(new Theme(0, 48, 1f, false, false, false, false, false, true), new Theme(156, 47, 1f, false, false, false, false, false, true), new Theme(352, 48, 1f, false, false, false, false, false, true), new Theme(444, 47, 1f, false, false, false, false, false, true), new Theme(513, 48, 1f, false, false, false, false, false, true), new Theme(705, 47, 1f, false, false, false, false, false, true), new Theme(859, 48, 1f, false, false, false, false, false, true), new Theme(975, 47, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 51, 1f, false, false, false, false, false, true), new Theme(103, 51, 1f, false, false, false, false, false, true), new Theme(558, 52, 1f, false, false, false, false, false, true), new Theme(885, 51, 1f, false, false, false, false, false, true), new Theme(1000, 51, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 50, 1f, false, false, false, false, false, true), new Theme(168, 49, 1f, false, false, false, false, false, true), new Theme(294, 50, 1f, false, false, false, false, false, true), new Theme(411, 49, 1f, false, false, false, false, false, true), new Theme(641, 50, 1f, false, false, false, false, false, true), new Theme(791, 49, 1f, false, false, false, false, false, true), new Theme(874, 50, 1f, false, false, false, false, false, true), new Theme(1009, 49, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 52, 1f, false, false, false, false, false, true), new Theme(321, 51, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 55, 1f, false, false, true, false, false, true), new Theme(162, 56, 1f, false, false, true, false, false, true), new Theme(327, 55, 1f, false, false, true, false, false, true), new Theme(401, 56, 1f, false, false, true, false, false, true), new Theme(557, 55, 1f, false, false, true, false, false, true)),
		new WorldTheme(new Theme(0, 57, 1f), new Theme(145, 45, 1f), new Theme(238, 46, 1f), new Theme(339, 57, 1f)),
		CreateNeonTheme(),
		new WorldTheme(new Theme(0, 54, 1f, false, false, false, false, false, true), new Theme(186, 53, 1f, false, false, false, false, false, true), new Theme(414, 54, 1f, false, false, false, false, false, true), new Theme(527, 53, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 61, 1f, false, false, true, false, false, true), new Theme(133, 62, 1f, false, false, true, false, false, true), new Theme(256, 61, 1f, false, false, true, false, false, true), new Theme(478, 62, 1f, false, false, true, false, false, true), new Theme(621, 61, 1f, false, false, true, false, false, true), new Theme(844, 62, 1f, false, false, true, false, false, true), new Theme(1003, 61, 1f, false, false, true, false, false, true)),
		new WorldTheme(new Theme(0, 62, 1f, false, false, true, false, false, true), new Theme(174, 61, 1f, false, false, true, false, false, true), new Theme(290, 62, 1f, false, false, true, false, false, true), new Theme(468, 61, 1f, false, false, true, false, false, true), new Theme(651, 62, 1f, false, false, true, false, false, true)),
		// Native 18/20/26/30/32/39, retaining donor order even for Ignite's 360 -> 321 transition.
		new WorldTheme(new Theme(0, 63, 1f, false, false, false, true, false, true), new Theme(126, 64, 1f, true, false, false, false, false, true), new Theme(354, 63, 1f, false, false, false, true, false, true), new Theme(472, 64, 1f, true, false, false, true, false, true), new Theme(582, 63, 1f, false, false, false, true, false, true)),
		new WorldTheme(new Theme(0, 66, 1f, false, false, false, false, false, true), new Theme(135, 67, 1f, false, false, false, false, false, true), new Theme(253, 65, 1f, false, false, false, false, false, true), new Theme(373, 66, 1f, false, false, false, false, false, true), new Theme(475, 65, 1f, false, false, false, false, false, true), new Theme(590, 67, 1f, false, false, false, false, false, true), new Theme(709, 66, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 49, 1f, false, false, false, false, false, true), new Theme(140, 50, 1f, false, false, false, false, false, true), new Theme(294, 49, 1f, false, false, false, false, false, true), new Theme(427, 50, 1f, false, false, false, false, false, true), new Theme(598, 49, 1f, false, false, false, false, false, true), new Theme(759, 50, 1f, false, false, false, false, false, true), new Theme(885, 49, 1f, false, false, false, false, false, true), new Theme(1000, 50, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 68, 1f, false, false, false, false, false, true), new Theme(121, 69, 1f, false, false, false, false, false, true), new Theme(264, 70, 1f, false, false, false, false, false, true), new Theme(411, 68, 1f, false, false, false, false, false, true), new Theme(539, 69, 1f, false, false, false, false, false, true), new Theme(796, 70, 1f, false, false, false, false, false, true), new Theme(1000, 68, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 53, 1f, false, false, false, false, false, true), new Theme(295, 54, 1f, false, false, false, false, false, true), new Theme(360, 53, 1f, false, false, false, false, false, true), new Theme(321, 53, 1f, false, false, false, false, false, true), new Theme(693, 54, 1f, false, false, false, false, false, true), new Theme(776, 53, 1f, false, false, false, false, false, true), new Theme(990, 53, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 71, 1f, false, false, false, false, false, true), new Theme(140, 72, 1f, false, false, false, false, false, true), new Theme(415, 71, 1f, false, false, false, false, false, true), new Theme(676, 72, 1f, false, false, false, false, false, true), new Theme(991, 71, 1f, false, false, false, false, false, true)),
		new WorldTheme(new Theme(0, 58, 1f, false, false, false, true, true, true, true, false, false, true, false, true, false, false), new Theme(56, 59, 1f, false, false, false, true, true, true, true, false, false, true, false, true, false, false), new Theme(252, 58, 1f, false, false, false, true, true, true, true, false, false, true, false, true, false, false), new Theme(368, 60, 1f, false, false, false, true, true, true, true, false, false, true, false, true, false, false)),
		new WorldTheme(new Theme(0, 74, 1f, false, false, false, false, false, true), new Theme(102, 73, 1f, false, true, true), new Theme(358, 74, 1f, false, false, false, false, false, true))
	};

	private static WorldTheme CreateNeonTheme()
	{
		// Native level 37 has a denser beat-flash schedule at High quality.
		int[] rows = DeviceQualityChecker.QualityIsHigh()
			? new int[] { 0, 141, 144, 147, 150, 153, 157, 159, 161, 164, 167, 170, 173, 176, 179, 193, 256, 291, 302, 326, 330, 334, 339, 398, 401, 418, 556, 628, 660, 695, 730, 772, 834 }
			: new int[] { 0, 141, 256, 398, 628, 772, 834 };
		Theme[] themes = new Theme[rows.Length];
		for (int i = 0; i < rows.Length; i++)
		{
			int theme = i == 0 || i == rows.Length - 1 ? 60 : i % 2 == 1 ? 59 : 58;
			themes[i] = new Theme(rows[i], theme, 1f, false, false, false, true, true, true, true,
				false, false, true, false, i != rows.Length - 1, false, false);
		}
		return new WorldTheme(themes);
	}

	// Runtime theme slots 17 onward, in order. Donor 2/3 get separate slots:
	// their 1.4.7.1 palettes differ from the accepted baseline worlds 2/3.
	// Slots 39..44 contain 1.5.6.1 donor themes 37..42 in a distinct resource root.
	private static readonly int[] expansionDonorThemes = { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 34, 35, 36, 2, 3, 37, 38, 39, 40, 41, 42 };
	private static readonly int[] newerDonorThemes = { 2, 16, 41, 42, 43, 44, 45, 46, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 32, 33, 34, 35, 36, 47, 48, 49, 60, 61, 1, 92 };
	public static int ClassicThemeCount { get { return 44 + newerDonorThemes.Length; } }
	public static bool IsClassicWorld { get { return GameManager.WorldIndex >= 5; } }

	public static string ClassicThemeResourceRoot(int runtimeThemeNumber)
	{
		return runtimeThemeNumber >= 73 ? "Classic325" : runtimeThemeNumber >= 71 ? "Classic1792" : runtimeThemeNumber >= 45 ? "Classic1782" : runtimeThemeNumber >= 39 ? "Classic1561" : "Classic1471";
	}

	public static bool HasWorldData(int worldNumber)
	{
		return worldNumber >= 1 && worldNumber <= worldData.Length && worldData[worldNumber - 1] != null && worldThemes[worldNumber - 1] != null;
	}

	private static void RequireWorldData(int worldNumber)
	{
		if (!HasWorldData(worldNumber)) throw new System.NotSupportedException("Original world/theme data not recovered for runtime world " + worldNumber);
	}

	public static int ClassicThemeNumber(int donorThemeNumber)
	{
		switch (donorThemeNumber)
		{
			case 6: return 9;
			case 8: return 10;
			case 9: return 11;
			case 10: return 12;
			case 11: return 13;
			case 12: return 14;
			case 13: return 15;
			case 14: return 16;
		}
		for (int i = 0; i < expansionDonorThemes.Length; i++)
			if (expansionDonorThemes[i] == donorThemeNumber) return i + 17;
		throw new System.ArgumentOutOfRangeException("donorThemeNumber");
	}

	public static int DonorThemeNumber(int runtimeThemeNumber)
	{
		if (runtimeThemeNumber == 9) return 6;
		if (runtimeThemeNumber >= 10 && runtimeThemeNumber <= 16) return runtimeThemeNumber - 2;
		if (runtimeThemeNumber >= 17 && runtimeThemeNumber <= 44) return expansionDonorThemes[runtimeThemeNumber - 17];
		if (runtimeThemeNumber >= 45 && runtimeThemeNumber <= ClassicThemeCount) return newerDonorThemes[runtimeThemeNumber - 45];
		return runtimeThemeNumber;
	}

	public static int CurrentDonorThemeNumber { get { return DonorThemeNumber(CurrentWorldThemeNumber); } }
	public static int GeneratingDonorThemeNumber { get { return DonorThemeNumber(CurrentWorldTheme.GeneratingWorldThemeNumber); } }

	public static int GetStartingThemeNumber(int worldNumber)
	{
		RequireWorldData(worldNumber);
		return worldThemes[worldNumber - 1].StartingThemeNumber;
	}

	public static int GetMenuThemeIndex(int worldIndex)
	{
		return worldIndex >= 5 ? GetStartingThemeNumber(worldIndex + 1) - 1 : worldIndex;
	}

	private static int maxDifficultyForPlayer;

	private static int segmentsUntilMaxDifficultyForPlayer;

	private static float difficultyExtender;

	private static int chainedSegmentsLeft;

	private static int? lastChosenTypeNum;

	public static WorldData CurrentWorldData
	{
		get
		{
			RequireWorldData(GameManager.WorldIndex + 1);
			return worldData[GameManager.WorldIndex];
		}
	}

	public static int WorldLength
	{
		get
		{
			return CurrentWorldData.Length;
		}
	}

	public static float WorldLengthAsFloat
	{
		get
		{
			return CurrentWorldData.LengthAsFloat;
		}
	}

	public static bool WorldIsEndless
	{
		get
		{
			return CurrentWorldData.IsEndless;
		}
	}

	public static bool WorldIsNotEndless
	{
		get
		{
			return CurrentWorldData.IsNotEndless;
		}
	}

	public static string WorldName
	{
		get
		{
			return CurrentWorldData.Name;
		}
	}

	public static string NextWorldName
	{
		get
		{
			return GetName(GameManager.GetNextWorldIndex() + 1);
		}
	}

	public static string MaxDifficultyAsChar
	{
		get
		{
			return GameManager.IntToChar(5);
		}
	}

	public static float VelocityMin
	{
		get
		{
			return CurrentWorldData.VelocityMin;
		}
	}

	public static float VelocityMax
	{
		get
		{
			return CurrentWorldData.VelocityMax;
		}
	}

	public static bool VelocityIsMaxed
	{
		get
		{
			return CurrentWorldData.VelocityIsMaxed;
		}
	}

	public static bool VelocityIsNotMaxed
	{
		get
		{
			return CurrentWorldData.VelocityIsNotMaxed;
		}
	}

	public static float Acceleration
	{
		get
		{
			return CurrentWorldData.Acceleration;
		}
	}

	public static WorldTheme CurrentWorldTheme
	{
		get
		{
			RequireWorldData(GameManager.WorldIndex + 1);
			return worldThemes[GameManager.WorldIndex];
		}
	}

	public static int CurrentWorldThemeNumber
	{
		get
		{
			return CurrentWorldTheme.CurrentWorldThemeNumber;
		}
	}

	public static int CurrentWorldThemeIndex
	{
		get
		{
			return CurrentWorldTheme.CurrentWorldThemeIndex;
		}
	}

	public static float CurrentWorldThemeTransition
	{
		get
		{
			return CurrentWorldTheme.CurrentWorldTransition;
		}
	}

	public static float BeatLength
	{
		get
		{
			return CurrentWorldData.BeatLength;
		}
	}

	public static bool GenerateTrees
	{
		get
		{
			return CurrentWorldTheme.GenerateTrees;
		}
	}

	public static bool GenerateTowers
	{
		get
		{
			return CurrentWorldTheme.GenerateTowers;
		}
	}

	public static bool GenerateAltGround
	{
		get
		{
			return CurrentWorldTheme.GenerateAltGround;
		}
	}

	public static bool GeneratePyramids
	{
		get
		{
			return CurrentWorldTheme.GeneratePyramids;
		}
	}

	public static bool GeneratePalmTrees
	{
		get
		{
			return CurrentWorldTheme.GeneratePalmTrees;
		}
	}

	public static bool GenerateHammers
	{
		get
		{
			return CurrentWorldTheme.GenerateHammers;
		}
	}

	public static bool GenerateSlashers
	{
		get
		{
			return CurrentWorldTheme.GenerateSlashers;
		}
	}

	public static bool GenerateHammersAndSlashers
	{
		get
		{
			return CurrentWorldTheme.GenerateHammersAndSlashers;
		}
	}

	public static bool GenerateBuildings { get { return CurrentWorldTheme.GenerateBuildings; } }
	public static bool GenerateFloodLights { get { return CurrentWorldTheme.GenerateFloodLights; } }
	public static bool GenerateAltPalmTrees { get { return CurrentWorldTheme.GenerateAltPalmTrees; } }
	public static bool GenerateTeslas { get { return CurrentWorldTheme.GenerateTeslas; } }
	public static bool FilmGrainEffect { get { return CurrentWorldTheme.FilmGrainEffect; } }
	public static bool RetroEffect { get { return CurrentWorldTheme.RetroEffect; } }
	public static bool GreenMonitorEffect { get { return CurrentWorldTheme.GreenMonitorEffect; } }
	public static bool ColorMonitorEffect { get { return CurrentWorldTheme.ColorMonitorEffect; } }

	public static string GetName(int worldNum)
	{
		return worldData[worldNum - 1].Name;
	}

	public static void Reset()
	{
		for (int i = 0; i < worldData.Length; i++)
		{
			worldData[i].Reset();
		}
		for (int j = 0; j < worldRhythms.Length; j++)
		{
			worldRhythms[j].Reset();
		}
		for (int k = 0; k < worldThemes.Length; k++)
		{
			worldThemes[k].Reset();
		}
	}

	public static bool ShouldRecolorWorld(int currentRowNum)
	{
		currentRowNum += GameManager.StartOffset;
		return worldThemes[GameManager.WorldIndex].ShouldChangeTheme(currentRowNum);
	}

	public static void TryChangeGeneratingTheme(int generatingRowNum)
	{
		generatingRowNum += GameManager.StartOffset;
		worldThemes[GameManager.WorldIndex].TryChangeGeneratingTheme(generatingRowNum);
	}

	public static SegmentID GetSegmentChoice(int segmentNum, int rowNum)
	{
		SegmentType type;
		if (GameManager.DebugSegmentType.HasValue)
		{
			type = GameManager.DebugSegmentType.Value;
		}
		else
		{
			float secondsIntoLevel = ((segmentNum != 0) ? TimeOf(rowNum) : 0f);
			bool nextPeriod;
			type = worldRhythms[GameManager.WorldIndex].ChooseSegmentType(secondsIntoLevel, out nextPeriod);
		}
		int difficulty = CurrentWorldData.GetDifficulty(rowNum);
		return new SegmentID(type, difficulty);
	}

	public static float GetVelocity(int rowNum)
	{
		return CurrentWorldData.GetVelocity(rowNum);
	}

	public static float TimeOf(int rowNum)
	{
		return CurrentWorldData.TimeOf(rowNum);
	}

	public static int RowOf(int secondsPlayed)
	{
		return CurrentWorldData.RowOf(secondsPlayed);
	}
}
