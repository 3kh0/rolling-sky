using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	public class BucketGroup
	{
		public readonly SegmentType Type;

		public readonly int TypeAsInt;

		public readonly TextAsset BucketFile;

		private readonly ArrayUtils.List<SegmentBucket> segmentBuckets = new ArrayUtils.List<SegmentBucket>();

		public bool Loaded { get; private set; }

		public SegmentBucket this[int segmentDifficulty]
		{
			get
			{
				if (segmentDifficulty >= segmentBuckets.Length)
				{
					segmentDifficulty = segmentBuckets.Length - 1;
				}
				return segmentBuckets[segmentDifficulty];
			}
		}

		public BucketGroup(SegmentType type, TextAsset bucketFile)
		{
			Type = type;
			BucketFile = bucketFile;
		}

		public void AppendClassicWorlds()
		{
			if (!Loaded || segmentBuckets.Length != 5 || (Type != SegmentType.Level && Type != SegmentType.End))
			{
				throw new System.InvalidOperationException("Classic maps require the five original Level/End buckets first.");
			}
			for (int world = 6; world <= GameManager.TotalWorlds; world++)
			{
				LevelCatalog.Level level = LevelCatalog.Get(world);
				string path = level.ResourceRoot + "/Maps/" + Type + level.SourceNumber;
				TextAsset asset = Resources.Load<TextAsset>(path);
				if (asset == null)
				{
					throw new System.InvalidOperationException("Missing classic map: " + path);
				}
				try
				{
					SegmentBucket bucket = new SegmentBucket(Type, world - 1);
					int? extraLayer = bucket.Load(asset.text.Split('\n'), null);
					if (extraLayer.HasValue || bucket.IsAborted || bucket.SegmentsTotal == 0)
					{
						throw new System.InvalidOperationException("Invalid classic map: " + path);
					}
					segmentBuckets.Add(bucket);
				}
				finally
				{
					Resources.UnloadAsset(asset);
				}
			}
			SegmentBucket.FinalizeReading();
		}

		public void Reset()
		{
			for (int i = 0; i < segmentBuckets.Length; i++)
			{
				segmentBuckets[i].Reset();
			}
		}

		public int[] Load()
		{
			int[] array = new int[6];
			string text = BucketFile.text;
			string[] fileLines = text.Split('\n');
			Resources.UnloadAsset(BucketFile);
			int? startAtLineNum = null;
			int num = 0;
			do
			{
				if (num > 5)
				{
					Debug.LogError(string.Format("LVLD: ERROR: Encountered difficulty level {0} ({1}) segments in {2}'s SegmentBucket TextFile, which is above the maximum difficulty level specified in LevelDesigner of {3} ({4})", num, GameManager.IntToChar(num), Type, 5, LevelDesigner.MaxDifficultyAsChar));
					break;
				}
				segmentBuckets.Add(new SegmentBucket(Type, num));
				startAtLineNum = segmentBuckets[num].Load(fileLines, startAtLineNum);
				array[num] = segmentBuckets[num].SegmentsTotal;
				num++;
			}
			while (startAtLineNum.HasValue);
			if (num <= 5)
			{
				array[num] = -1;
			}
			Loaded = true;
			SegmentBucket.FinalizeReading();
			return array;
		}
	}

	public class SegmentBucket
	{
		private enum FileSection
		{
			Start = 0,
			Header = 1,
			TileSet = 2,
			TilesStart = 3,
			TilesBlank = 4,
			Tiles = 5
		}

		public const int RowWidth = 5;

		public readonly SegmentID ID;

		public string type;

		private ArrayUtils.WeightedListRandom<int>[] segmentChoices;

		private MathUtils.Index segmentIndex;

		private int segmentNum;

		private int lineNum;

		private int tileNum;

		private bool lastLine;

		private bool nextSegment;

		private bool bucketEnd;

		private bool awaitingLastJump;

		private bool lineIsEmpty;

		private bool lineHasJump;

		private int gapLength;

		private int lastJumpLength;

		private bool segmentWeighted;

		private bool segmentRestricted;

		private ArrayUtils.Array<int> segmentWeights;

		public SegmentType Type
		{
			get
			{
				SegmentID iD = ID;
				return iD.Type;
			}
		}

		public int TypeAsInt
		{
			get
			{
				return ID.TypeAsInt;
			}
		}

		public int Difficulty
		{
			get
			{
				SegmentID iD = ID;
				return iD.Difficulty;
			}
		}

		public string DifficultyAsString
		{
			get
			{
				return ID.DifficultyAsString;
			}
		}

		public List<Segment> Segments { get; private set; }

		public int SegmentsTotal { get; private set; }

		public bool IsFound { get; private set; }

		public bool IsAborted { get; private set; }

		public bool IsLoaded { get; private set; }

		public bool IsEmpty { get; private set; }

		public bool IsNotFound
		{
			get
			{
				return !IsFound;
			}
		}

		public bool IsNotAborted
		{
			get
			{
				return !IsAborted;
			}
		}

		public bool IsNotLoaded
		{
			get
			{
				return !IsLoaded;
			}
		}

		public bool IsNotEmpty
		{
			get
			{
				return !IsEmpty;
			}
		}

		public bool IsLevel { get; private set; }

		public bool IsEnd { get; private set; }

		public int StartOffset { get; private set; }

		public Segment this[int segmentIndex]
		{
			get
			{
				return Segments[segmentIndex];
			}
		}

		public SegmentBucket(SegmentType type, int difficulty)
		{
			ID = new SegmentID(type, difficulty);
			IsAborted = false;
			IsLoaded = false;
			IsEmpty = true;
			IsLevel = type == SegmentType.Level;
			IsEnd = type == SegmentType.End;
		}

		public int? Load(string[] fileLines, int? startAtLineNum)
		{
			int? num = null;
			int? result = null;
			FileSection fileSection;
			int num2;
			if (startAtLineNum.HasValue)
			{
				StartBucket();
				fileSection = FileSection.TileSet;
				num2 = startAtLineNum.Value;
			}
			else
			{
				fileSection = FileSection.Start;
				num2 = 0;
			}
			for (int i = num2; i < fileLines.Length; i++)
			{
				string text = fileLines[i];
				string[] array;
				switch (fileSection)
				{
				case FileSection.Start:
					if (text == "[header]")
					{
						fileSection = FileSection.Header;
					}
					break;
				case FileSection.Header:
					if (text == "[tilesets]")
					{
						fileSection = FileSection.TileSet;
						StartBucket();
					}
					break;
				case FileSection.TileSet:
					if (text == "[layer]")
					{
						fileSection = FileSection.TilesStart;
					}
					break;
				case FileSection.TilesStart:
					array = text.Split('=');
					switch (array[0])
					{
					case "type":
						type = array[1];
						break;
					case "data":
						fileSection = FileSection.TilesBlank;
						break;
					}
					break;
				case FileSection.TilesBlank:
				case FileSection.Tiles:
				{
					if (string.IsNullOrEmpty(text))
					{
						if (fileSection == FileSection.Tiles)
						{
							num = i;
						}
						break;
					}
					array = text.Split(',');
					int num3 = array.Length;
					if (num3 == 1)
					{
						break;
					}
					if (fileSection == FileSection.TilesBlank)
					{
						for (int j = 0; j < 5; j++)
						{
							string text2 = array[j];
							int parsedInt;
							if (CanParseToTile(text2, out parsedInt))
							{
								if (parsedInt != 0)
								{
									fileSection = FileSection.Tiles;
									break;
								}
								continue;
							}
							Debug.LogError(string.Format("LVLD: ERROR: problem reading buckets {0}'s textFile: level line number {1}, tile {2} (Tiled Index '{4}, {3}') couldn't be converted to an integer(its value is '{5}')", ID, i + 1, j + 1, i, j, text2));
							IsAborted = true;
						}
					}
					if (fileSection == FileSection.Tiles)
					{
						NewLine();
						for (int k = 0; k < 5; k++)
						{
							NewTile(array[k]);
						}
					}
					break;
				}
				}
				array = null;
				if (num.HasValue)
				{
					break;
				}
			}
			IsLoaded = true;
			if (num.HasValue)
			{
				for (int l = num.Value + 1; l < fileLines.Length; l++)
				{
					if (!string.IsNullOrEmpty(fileLines[l]))
					{
						result = l;
						break;
					}
				}
			}
			EndBucket();
			return result;
		}

		public void CalculateWeights()
		{
			segmentChoices = new ArrayUtils.WeightedListRandom<int>[6];
			for (int i = 0; i < segmentChoices.Length; i++)
			{
				segmentChoices[i] = new ArrayUtils.WeightedListRandom<int>(Segments.Count, 1);
			}
			for (int j = 0; j < Segments.Count; j++)
			{
				if (Segments[j].IsWeighted)
				{
					for (int k = 0; k < segmentChoices.Length; k++)
					{
						segmentChoices[k].Add(j, Segments[j].Frequencies[k]);
					}
				}
				else
				{
					for (int l = 0; l < segmentChoices.Length; l++)
					{
						segmentChoices[l].Add(j, 1);
					}
				}
			}
			for (int m = 0; m < segmentChoices.Length; m++)
			{
				int num = m + 1;
				string arg = ((num != 6) ? num.ToString() : "E");
				if (debugSegmentWeights)
				{
					Debug.Log(string.Format("LVLD: DEBUG: ID: {0}, World: {1}, Choices: {2}", ID, arg, segmentChoices[m].ToString()));
				}
			}
		}

		public void Clear()
		{
			IsLoaded = false;
		}

		public void Reset()
		{
			if (IsLevel)
			{
				segmentIndex.Reset();
			}
			else if (!IsEnd)
			{
				for (int i = 0; i < segmentChoices.Length; i++)
				{
					segmentChoices[i].ClearPrevious();
				}
			}
		}

		public static void FinalizeReading()
		{
			Segment.FinizeReading();
		}

		public void ChooseSegment()
		{
			if (IsNotLoaded || Segments == null)
			{
				Debug.LogError(string.Format("LVLD: ERROR: Attempt to ChooseSegment() of Bucket {0}, which has not yet been loaded!", ID));
			}
			else if (GameManager.DebugSegmentNum.HasValue && Type != SegmentType.Tutorial)
			{
				ChosenSegmentIndex = GameManager.DebugSegmentNum.Value;
			}
			else if (IsLevel)
			{
				segmentIndex.Descend();
				ChosenSegmentIndex = segmentIndex.Value;
			}
			else if (IsEnd)
			{
				ChosenSegmentIndex = 0;
				if (debugEndChoice)
				{
					Debug.Log(string.Format("LVLD: DEBUG: Detected End type segment for {0}, forcing segmentIndex choice to: {1}", Difficulty, ChosenSegmentIndex));
				}
			}
			else
			{
				ChosenSegmentIndex = segmentChoices[GameManager.WorldIndex].GetRandomNext();
				if (debugSegmentAdding)
				{
					Debug.Log(string.Format("LVLD: DEBUG: Adding Segment: {0} ({1} of {2})", ID, SegmentsTotal - ChosenSegmentIndex, SegmentsTotal));
				}
			}
		}

		public new string ToString()
		{
			return string.Format("Bucket {0} (containing boards {1})", DifficultyAsString, SegmentsTotal);
		}

		private void StartBucket()
		{
			Segments = new List<Segment>();
			segmentNum = -1;
			lineNum = -1;
			tileNum = -1;
			SegmentsTotal = 0;
			NewSegment();
		}

		private void EndBucket()
		{
			CalculateWeights();
			FinalizeLastSegment();
			if (!IsLevel)
			{
				return;
			}
			int startAt = 0;
			if (GameManager.DebugStart.HasValue)
			{
				bool flag = false;
				int num = GameManager.DebugStart.Value;
				int num2 = SegmentsTotal - 1;
				do
				{
					int length = Segments[num2].Tiles.GetLength(0);
					if ((float)(num - length) <= 0f)
					{
						startAt = num2 + 1;
						flag = true;
						StartOffset = num;
					}
					else
					{
						num -= length;
						num2--;
					}
				}
				while (!flag);
			}
			segmentIndex = new MathUtils.Index(SegmentsTotal, startAt);
		}

		private void NewSegment()
		{
			if (segmentNum != -1)
			{
				FinalizeLastSegment();
			}
			Segments.Add(new Segment());
			segmentNum++;
			SegmentsTotal++;
			nextSegment = false;
			gapLength = 0;
			lastJumpLength = 0;
			lastLine = true;
			lineIsEmpty = false;
			lineHasJump = false;
			awaitingLastJump = true;
			segmentWeighted = false;
			segmentRestricted = false;
		}

		private void NewLine()
		{
			if (lineIsEmpty)
			{
				gapLength++;
				if (gapLength == 4)
				{
					DebugImpossibleSegment("contained a gap of 4 or more tiles");
				}
			}
			lineIsEmpty = true;
			if (IsNotAborted)
			{
				if (nextSegment)
				{
					NewSegment();
				}
				lineNum++;
				Segments[segmentNum].NewRow(5);
				if (tileNum != -1 && tileNum != 4)
				{
					Debug.LogError(string.Format("LVLD: ERROR: problem reading buckets {0}'s textFile: level line number {1} has less tiles than its File Header describes({2} instead of expected {3})", ID, lineNum + 1, tileNum + 1, 5));
					IsAborted = true;
				}
				tileNum = -1;
			}
		}

		private void NewTile(string tileString)
		{
			if (!IsNotAborted)
			{
				return;
			}
			tileNum++;
			if (tileNum >= 5)
			{
				Debug.LogError(string.Format("LVLD: ERROR: problem reading buckets {0}'s textFile: level line number {1} is more tiles wide than its header describes(expected {2})", ID, lineNum + 1, 5));
				IsAborted = true;
			}
			int? num = null;
			int? num2 = null;
			bool? flag = null;
			int parsedInt;
			if (CanParseToTile(tileString, out parsedInt))
			{
				bool flag2 = false;
				switch (parsedInt)
				{
				case 109:
				case 110:
				case 111:
				case 112:
					nextSegment = true;
					flag2 = true;
					break;
				case 113:
				case 114:
				case 115:
				case 116:
					bucketEnd = true;
					flag2 = true;
					break;
				case 133:
					num = 6;
					flag = true;
					break;
				case 134:
					num = 1;
					flag = true;
					break;
				case 135:
					num = 2;
					flag = true;
					break;
				case 136:
					num = 3;
					flag = true;
					break;
				case 137:
					num = 4;
					flag = true;
					break;
				case 138:
					num = 5;
					flag = true;
					break;
				case 139:
					num = 6;
					flag = false;
					break;
				case 140:
					num = 1;
					flag = false;
					break;
				case 141:
					num = 2;
					flag = false;
					break;
				case 142:
					num = 3;
					flag = false;
					break;
				case 143:
					num = 4;
					flag = false;
					break;
				case 144:
					num = 5;
					flag = false;
					break;
				case 121:
					num = 6;
					num2 = 1;
					break;
				case 122:
					num = 1;
					num2 = 1;
					break;
				case 123:
					num = 2;
					num2 = 1;
					break;
				case 124:
					num = 3;
					num2 = 1;
					break;
				case 125:
					num = 4;
					num2 = 1;
					break;
				case 126:
					num = 5;
					num2 = 1;
					break;
				}
				if (num2.HasValue || flag.HasValue)
				{
					int num3 = ((!num.HasValue) ? (-1) : (num.Value - 1));
					if (!segmentWeighted)
					{
						segmentWeights = new ArrayUtils.Array<int>(6);
						segmentWeights.FillWith(1);
						segmentWeighted = true;
					}
					if (flag.HasValue)
					{
						for (int i = 0; i < 6; i++)
						{
							if (flag.Value)
							{
								if (i == num3)
								{
									ArrayUtils.Array<int> array = segmentWeights;
									int index = i;
									ArrayUtils.Array<int> array3;
									ArrayUtils.Array<int> array2 = (array3 = segmentWeights);
									int index3;
									int index2 = (index3 = i);
									index3 = array3[index3];
									int value;
									array2[index2] = (value = index3) + 1;
									array[index] = value;
								}
								else if (segmentRestricted)
								{
									if (segmentWeights[i] == 1)
									{
										segmentWeights[i] = 0;
									}
								}
								else
								{
									segmentWeights[i] = 0;
								}
							}
							else if (i == num3)
							{
								segmentWeights[i] = 0;
							}
						}
						segmentRestricted = true;
					}
					else if (num.HasValue)
					{
						if (segmentWeights[num3] != 0)
						{
							segmentWeights[num3] += num2.Value;
						}
					}
					else
					{
						for (int j = 0; j < 6; j++)
						{
							if (segmentWeights[j] != 0)
							{
								segmentWeights[j] += num2.Value;
							}
						}
					}
					parsedInt = 0;
				}
				if (flag2)
				{
					switch (parsedInt)
					{
					case 109:
					case 113:
						parsedInt = 0;
						break;
					case 110:
					case 114:
						parsedInt = 1;
						break;
					case 111:
					case 115:
						parsedInt = 3;
						break;
					case 112:
					case 116:
						parsedInt = 2;
						break;
					default:
						Debug.LogError(string.Format("LVLD: ERROR: Attempted to remap unhandled Start/End TileVal of {0}.  Check SegmentBucket.NewTile()'s second case statement", parsedInt));
						parsedInt = 0;
						break;
					}
				}
				if (lineIsEmpty && parsedInt != 0 && parsedInt != 109 && parsedInt != 113)
				{
					lineIsEmpty = false;
					gapLength = 0;
				}
				if (awaitingLastJump && !lineHasJump && (parsedInt == 2 || parsedInt == 54))
				{
					lineHasJump = true;
				}
				Segments[segmentNum].NewTile(parsedInt);
			}
			else
			{
				Debug.LogError(string.Format("LVLD: ERROR: problem reading buckets {0}'s textFile: level line number {1}, tile {2} (Tiled Index '{4}, {3}') couldn't be converted to an integer(its value is '{5}')", ID, lineNum + 1, tileNum + 1, lineNum, tileNum, tileString));
				IsAborted = true;
			}
		}

		private void FinalizeLastSegment()
		{
			Segments[segmentNum].FinalizeSegment();
			if (Segments[segmentNum].IsEmpty)
			{
				Debug.Log(string.Format("LVLD: ERROR: Found empty segment in Bucket {0}, segment {1}", ID, segmentNum));
			}
			if (Segments[segmentNum].Length > 3)
			{
				bool flag = false;
				for (int i = 0; i < 5; i++)
				{
					int num = Segments[segmentNum].Tiles[3, i];
					if (num == 2 || num == 54)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					bool flag2 = false;
					for (int j = 0; j < 5; j++)
					{
						if (Segments[segmentNum].Tiles[0, j] != 0)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						string text = null;
						for (int k = 0; k < 5; k++)
						{
							text = ((k != 0) ? (text + ", " + Segments[segmentNum].Tiles[3, k]) : Segments[segmentNum].Tiles[3, 0].ToString());
						}
						text = "4th row tiles: " + text;
						DebugImpossibleSegment("did not have a final jump tile in the 4th row from the end", text);
					}
				}
			}
			if (segmentWeighted)
			{
				Segments[segmentNum].SetWeightsTo(segmentWeights.ToArray());
				if (debugSegmentWeightAdding)
				{
					Debug.Log(string.Format("LVLD: DEBUG: Frequencing Bucket {0}, segment {1} to {2}", ID, segmentNum, Segments[segmentNum].WeightsString()));
				}
			}
		}

		private int ToInt(string convertString)
		{
			short result;
			if (short.TryParse(convertString, out result))
			{
				return result;
			}
			string arg = null;
			if (lineNum <= -1)
			{
				arg = string.Format(" level line number {0}, tile {1}", lineNum + 1, tileNum + 1);
			}
			Debug.LogError(string.Format("LVLD: ERROR: problem reading buckets {0}'s textFile: unable to convert{1} to an integer(its value is '{2}')", ID, arg, convertString));
			IsAborted = true;
			return -1;
		}

		private void DebugImpossibleSegment(string impossibleReason, string extraLine = null)
		{
			if (!IsLevel)
			{
				if (extraLine != null)
				{
					extraLine = "\n  " + extraLine;
				}
				Debug.LogError(string.Format("LVLD: ERROR: Bucket {0}{1} is impossible: {2}{3}", ID, segmentNum, impossibleReason, extraLine));
			}
		}

		private bool CanParseToTile(string convertString, out int parsedInt)
		{
			bool flag = false;
			if (ParseUtils.CanParseToInt(convertString, out parsedInt))
			{
				flag = EnumUtils.CanParseToEnum<TileVal>(parsedInt);
				if (!flag)
				{
					Debug.LogError(string.Format("LVLD: ERROR: Bucket {0}, segment {1} contains an INVALID tile value of {2}", ID, segmentNum, parsedInt));
				}
			}
			if (!flag)
			{
				parsedInt = -1;
			}
			return flag;
		}
	}

	public class Segment
	{
		private const int maxSegmentSize = 100;

		private static ArrayUtils.List<int[]> rowsList = new ArrayUtils.List<int[]>(100);

		public int[,] Tiles;

		public int[] Frequencies;

		public int[] Stamps;

		public int CurrentRow { get; private set; }

		public int CurrentTile { get; private set; }

		public bool IsEmpty { get; private set; }

		public bool IsWeighted { get; private set; }

		public int Length
		{
			get
			{
				return rowsList.Length;
			}
		}

		public Segment()
		{
			rowsList.Reset();
			CurrentRow = -1;
			CurrentTile = 0;
			IsEmpty = true;
			IsWeighted = false;
		}

		public void NewRow(int lineWidth)
		{
			rowsList.Add(new int[lineWidth]);
			CurrentRow++;
			CurrentTile = 0;
		}

		public void NewTile(int tileCode)
		{
			rowsList[CurrentRow][CurrentTile] = tileCode;
			CurrentTile++;
			if (IsEmpty && tileCode != 0)
			{
				IsEmpty = false;
			}
		}

		public void SetWeightsTo(int[] frequencies)
		{
			Frequencies = frequencies;
			IsWeighted = true;
		}

		public string WeightsString()
		{
			string text;
			if (IsWeighted)
			{
				bool flag = true;
				int num = Frequencies[0];
				text = Frequencies[0].ToString();
				for (int i = 1; i < Frequencies.Length; i++)
				{
					if (flag)
					{
						flag = num == Frequencies[i];
					}
					text = text + ", " + Frequencies[i];
				}
				if (flag)
				{
					text = ((num != 0) ? num.ToString() : "ERROR - stamped with frequencies that were all equal to zero.  Check logic, as this is either redundant or erroneous");
				}
			}
			else
			{
				text = "none";
			}
			return text;
		}

		public void FinalizeSegment()
		{
			Tiles = new int[rowsList.Length, rowsList[0].Length];
			for (int i = 0; i < rowsList.Length; i++)
			{
				for (int j = 0; j < rowsList[0].Length; j++)
				{
					Tiles[i, j] = rowsList[i][j];
				}
			}
		}

		public static void FinizeReading()
		{
			rowsList.Clear();
		}
	}

	private const bool deviceDebugBenchmarking = false;

	private const string benchmarkingPrefix = "File Loading";

	private const int benchmarkingTimeStream = 3;

	private const bool canFlipBoards = true;

	private const bool canFlipBinaries = false;

	private const string BucketMasterDirectoryName = "Buckets";

	private const string BucketFileName = "Bucket";

	private const string BucketFileType = "txt";

	private const string segmentTypeFlag = "type";

	private const string BucketDataFlag = "data";

	private const char EqualsOperator = '=';

	private const char ElementSpacer = ',';

	private const char ElementDivider = '|';

	private const char GroupSpacer = ';';

	private static bool debugsEnabled = true;

	private static bool debugLoading = false;

	private static bool debugBucketFileLoading = false;

	private static bool debugSegmentAdding = false;

	private static bool debugSegmentWeights = false;

	private static bool debugSegmentWeightAdding = false;

	private static bool debugBenchmarking = false;

	private static bool debugEndChoice = (debugsEnabled ? true : false);

	private static Dictionary<SegmentType, BucketGroup> buckets = new Dictionary<SegmentType, BucketGroup>();

	private static int totalsegmentTypes;

	private static bool bucketsRead;

	private static bool bucketsLoaded;

	public static string BenchmarkingString { get; private set; }

	public static float BenchmarkingFloat { get; private set; }

	public static SegmentType ChosenType { get; private set; }

	public static int ChosenDifficulty { get; private set; }

	public static int ChosenSegmentIndex { get; private set; }

	public static Segment ChosenSegment
	{
		get
		{
			return buckets[ChosenType][ChosenDifficulty][ChosenSegmentIndex];
		}
	}

	public static int[,] ChosenSegmentTiles
	{
		get
		{
			return buckets[ChosenType][ChosenDifficulty][ChosenSegmentIndex].Tiles;
		}
	}

	public static int ChosenSegmentLength
	{
		get
		{
			return ChosenSegmentTiles.GetLength(0);
		}
	}

	public static int ChosenSegmentWidth
	{
		get
		{
			return ChosenSegmentTiles.GetLength(1);
		}
	}

	public static bool ChosenSegmentCanFlip
	{
		get
		{
			return ChosenType != SegmentType.Binary && ChosenType != SegmentType.BinaryMini && ChosenType != SegmentType.Tutorial && ChosenType != SegmentType.Level;
		}
	}

	public static int ChosenSegmentStart
	{
		get
		{
			return buckets[ChosenType][ChosenDifficulty].StartOffset;
		}
	}

	public static int ChosenSegmentBucketLength
	{
		get
		{
			return buckets[ChosenType][ChosenDifficulty].SegmentsTotal;
		}
	}

	public static void ReadWorlds()
	{
		TryReadBuckets();
	}

	public static void LoadWorlds()
	{
		TryLoadAllBuckets();
	}

	public static void Reset()
	{
		TryResetChoices();
	}

	public static void ChooseSegment(int segmentsTravelled, float segmentOffset, SegmentType? forcedType = null)
	{
		TryReadBuckets();
		TryLoadAllBuckets();
		SegmentID segmentID = ((!forcedType.HasValue) ? LevelDesigner.GetSegmentChoice(segmentsTravelled, GameManager.TileBoard.Length) : ((forcedType.Value != SegmentType.End) ? new SegmentID(forcedType.Value, 0) : new SegmentID(forcedType.Value, GameManager.WorldIndex)));
		ChosenType = segmentID.Type;
		ChosenDifficulty = segmentID.Difficulty;
		buckets[ChosenType][ChosenDifficulty].ChooseSegment();
	}

	public static void TryResetChoices()
	{
		foreach (SegmentType key in buckets.Keys)
		{
			buckets[key].Reset();
		}
	}

	private static void TryReadBuckets()
	{
		if (bucketsRead)
		{
			return;
		}
		int num = 0;
		if (debugBenchmarking ? true : false)
		{
			Benchmarking.StartTiming(3);
		}
		string text = string.Format("{0}/", "Buckets");
		int i = 0;
		totalsegmentTypes = 0;
		SegmentType parsedEnum;
		for (; EnumUtils.CanParseToEnum<SegmentType>(i, out parsedEnum); i++)
		{
			string text2 = parsedEnum.ToString();
			string text3 = text + text2;
			TextAsset textAsset = (TextAsset)Resources.Load(text3, typeof(TextAsset));
			if (textAsset == null)
			{
				Debug.LogWarning(string.Format("LVLD: WARNING: Found NO buckets of type {0}\n  Directory searched: {1}\n  First name tried: {2}", parsedEnum, text, text3));
			}
			else
			{
				buckets.Add(parsedEnum, new BucketGroup(parsedEnum, textAsset));
				if (debugBucketFileLoading)
				{
					Debug.Log(string.Format("LVLD: Found the {0} bucket", parsedEnum));
				}
				num++;
			}
			totalsegmentTypes++;
		}
		bucketsRead = true;
		if (debugBenchmarking ? true : false)
		{
			Benchmarking.TryDebugSecondsTimed(num, "File Loading");
			BenchmarkingString = Benchmarking.GetSecondsTimedString(num);
			BenchmarkingFloat = Benchmarking.GetSecondsTimed();
			Benchmarking.ResetTimeStream();
		}
	}

	private static void TryLoadAllBuckets()
	{
		if (bucketsLoaded)
		{
			return;
		}
		int num = 0;
		string text = null;
		SegmentType[] array = new SegmentType[2]
		{
			SegmentType.Level,
			SegmentType.End
		};
		foreach (SegmentType segmentType in array)
		{
			int[] array2 = buckets[segmentType].Load();
			buckets[segmentType].AppendClassicWorlds();
			if (debugLoading)
			{
				int num2 = 0;
				string text2 = null;
				for (int j = 0; j < array2.Length && array2[j] != -1; j++)
				{
					num2 += array2[j];
					string text3 = string.Format("{0} : {1}", GameManager.IntToChar(j), array2[j]);
					text2 = text2 + "\t" + text3;
				}
				num += num2;
				string text4 = null;
				for (int k = 0; k < 10 - segmentType.ToString().Length; k++)
				{
					text4 += "  ";
				}
				string text5 = string.Format("\n    {0} {3}\t= {1} \t= {2}", segmentType, num2, text2, text4);
				text += text5;
			}
		}
		if (debugLoading)
		{
			Debug.Log(string.Format("LVLD: DEBUG: Loaded {0} bucket-types up to difficulty level {1}\n  Total of {2} buckets actually loaded):{3}", totalsegmentTypes, LevelDesigner.MaxDifficultyAsChar, num, text));
		}
		bucketsLoaded = true;
	}
}
