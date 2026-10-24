using UnityEngine;

public class SegmentTracker : MonoBehaviour
{
	public struct SegmentID
	{
		public readonly int Number;

		public readonly SegmentType Type;

		public readonly int TotalRows;

		public readonly int Difficulty;

		public readonly int BucketNumber;

		public SegmentID(int segmentNumber, SegmentType segmentType, int segmentDifficulty, int segmentBucketNumber, int totalRowsThisSegment)
		{
			Number = segmentNumber;
			Type = segmentType;
			Difficulty = segmentDifficulty;
			BucketNumber = segmentBucketNumber;
			TotalRows = totalRowsThisSegment;
		}

		public override string ToString()
		{
			return string.Format("{2} {0}{1}", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Difficulty], BucketNumber, Type);
		}
	}

	private const bool debugEnabled = false;

	private const bool debugAdding = false;

	private const bool debugMoving = false;

	private const bool debugChanging = false;

	private const int startingRow = 2;

	private static ArrayUtils.List<SegmentID> segmentQueue;

	private static ArrayUtils.List<float> segmentOffsets;

	private static int rowsLeftThisSegment;

	private static bool haveBoards;

	private static bool initialized;

	public static SegmentID CurrentSegmentID { get; private set; }

	public static SegmentID LastSegmentID { get; private set; }

	public static SegmentID LatestSegmentID { get; private set; }

	public static SegmentID NextSegmentID { get; private set; }

	public static bool HasLastSegment { get; private set; }

	public static bool HasNextSegment { get; private set; }

	public static int CurrentSegmentCount { get; private set; }

	public static string CurrentSegmentCountAsString
	{
		get
		{
			return CurrentSegmentCount.ToString();
		}
	}

	public static SegmentType CurrentSegmentType
	{
		get
		{
			return CurrentSegmentID.Type;
		}
	}

	public static string CurrentSegmentIDAsString
	{
		get
		{
			return CurrentSegmentID.ToString();
		}
	}

	public static int CurrentSegmentTypeAsInt
	{
		get
		{
			return (int)CurrentSegmentID.Type;
		}
	}

	public static SegmentType LatestSegmentType
	{
		get
		{
			return LatestSegmentID.Type;
		}
	}

	public static string LatestSegementIDAsString
	{
		get
		{
			return LatestSegmentID.ToString();
		}
	}

	public static int LatestSegmentTypeAsInt
	{
		get
		{
			return (int)LatestSegmentID.Type;
		}
	}

	public static SegmentType LastSegmentType
	{
		get
		{
			return (!HasLastSegment) ? SegmentType.Start : LastSegmentID.Type;
		}
	}

	public static string LastSegmentIDAsString
	{
		get
		{
			return (!HasLastSegment) ? "Null" : LastSegmentID.ToString();
		}
	}

	public static int LastSegmentTypeAsInt
	{
		get
		{
			return (int)((!HasLastSegment) ? ((SegmentType)(-1)) : LastSegmentID.Type);
		}
	}

	public static SegmentType NextSegmentType
	{
		get
		{
			return (!HasNextSegment) ? SegmentType.Start : NextSegmentID.Type;
		}
	}

	public static string NextSegmentIDAsString
	{
		get
		{
			return (!HasNextSegment) ? "Null" : NextSegmentID.ToString();
		}
	}

	public static int NextSegmentTypeAsInt
	{
		get
		{
			return (int)((!HasNextSegment) ? ((SegmentType)(-1)) : NextSegmentID.Type);
		}
	}

	private static void TryInitialize()
	{
		if (!initialized)
		{
			Reset();
		}
	}

	public static void Reset()
	{
		if (segmentQueue != null)
		{
			segmentQueue.Clear();
		}
		else
		{
			segmentQueue = new ArrayUtils.List<SegmentID>();
		}
		if (segmentOffsets != null)
		{
			segmentOffsets.Clear();
		}
		else
		{
			segmentOffsets = new ArrayUtils.List<float>();
		}
		rowsLeftThisSegment = 0;
		haveBoards = false;
		HasLastSegment = false;
		CurrentSegmentCount = 0;
		initialized = true;
	}

	public static void AddSegment(int number, SegmentType type, int difficulty, int bucketNumber, int rowsTotal, float offset, int bucketsTotal, bool wasFlipped)
	{
		TryInitialize();
		LatestSegmentID = new SegmentID(number, type, difficulty, bucketNumber, rowsTotal);
		segmentQueue.Add(LatestSegmentID);
		if (!haveBoards)
		{
			CurrentSegmentID = LatestSegmentID;
			rowsLeftThisSegment = rowsTotal - 2;
			haveBoards = true;
		}
		segmentOffsets.Add(offset);
		if (number != segmentOffsets.LastIndex)
		{
			Debug.LogWarning(string.Format("BTTK: DEBUG: Problem adding Segment Offset - Segment {5}'s offset was stored under index {6}.  Will cause issues attempting to use SegmentTracket.GetOffset(int segmentIndex)\n(Segment type {0}, difficulty {1}, number {2}, length {3} rows and offset {4})", type, difficulty, bucketNumber, rowsTotal, offset, number, segmentOffsets.LastIndex));
		}
	}

	public static void OnMoveForward()
	{
		rowsLeftThisSegment--;
		if (rowsLeftThisSegment == 0)
		{
			PlayerProfiler.AddNewSurvived(CurrentSegmentTypeAsInt);
			if (HasLastSegment)
			{
				LastSegmentID = CurrentSegmentID;
			}
			else
			{
				LastSegmentID = segmentQueue.First;
				segmentQueue.RemoveFirst();
				HasLastSegment = true;
			}
			CurrentSegmentID = segmentQueue.First;
			segmentQueue.RemoveFirst();
			rowsLeftThisSegment = CurrentSegmentID.TotalRows;
			HasNextSegment = segmentQueue.IsNotEmpty;
			if (HasNextSegment)
			{
				NextSegmentID = segmentQueue.First;
			}
			CurrentSegmentCount++;
		}
	}

	public static float GetSegmentOffset(int segmentNumber)
	{
		if (segmentOffsets.ContainsIndex(segmentNumber))
		{
			return segmentOffsets[segmentNumber];
		}
		Debug.LogWarning(string.Format("SGTK: ERROR: Was unable to find segmentNumber {0} in segmentOffsets, whose indexes range from 0 to {1}", segmentNumber, segmentOffsets.LastIndex));
		return 0f;
	}
}
