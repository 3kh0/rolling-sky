using UnityEngine;

public class SmartBoard
{
	public const int Width = 5;

	private const int SegmentsToBufferMax = 25;

	private const int RowsToBufferBehind = 4;

	public const int WidthInterior = 4;

	public const float Center = 2.5f;

	public const float WidthAsFloat = 5f;

	public const float WidthInteriorAsFloat = 4f;

	public const float WidthHalf = 2.5f;

	public const float WidthInteriorHalf = 2f;

	private Tile[][,] allSegments;

	private int nextFreeSegmentIndex;

	private int nextSegmentIndex;

	private int segmentsShifted;

	private int rowsShifted;

	private int currentRowIndex;

	public int Length { get; private set; }

	public int CurrentSegmentIndex { get; private set; }

	public bool WorldIsFinished { get; private set; }

	public bool WorldIsNotFinished
	{
		get
		{
			return !WorldIsFinished;
		}
	}

	public int FinalRow { get; private set; }

	public Tile[,] CurrentSegment
	{
		get
		{
			if (allSegments == null)
			{
				Debug.LogError("SMBD: ERROR: Attempt to get CurrentBoard when allBoards is null");
				return null;
			}
			if (CurrentSegmentIndex < 0 || CurrentSegmentIndex >= allSegments.Length)
			{
				Debug.LogError(string.Format("SMBD: ERROR: Attempt to get CurrentBoard using invalid currentBoardIndex (value was {0}, which was outside range of boards 0 to {1}", CurrentSegmentIndex, allSegments.Length - 1));
				return null;
			}
			return allSegments[CurrentSegmentIndex];
		}
		set
		{
			allSegments[CurrentSegmentIndex] = value;
		}
	}

	public int CurrentWidth
	{
		get
		{
			return CurrentSegment.GetLength(1);
		}
	}

	public Tile this[int rowIndex, int tileIndex]
	{
		get
		{
			Tile result = null;
			int segmentIndex;
			int adjustedSegmentIndex;
			if (CanGetTileIndicesFrom(rowIndex, tileIndex, out segmentIndex, out adjustedSegmentIndex))
			{
				try
				{
					result = allSegments[segmentIndex][adjustedSegmentIndex, tileIndex];
				}
				catch
				{
					Debug.LogError(string.Format("SMBD: error trying to get tile {0}, {1} from board {2} (based on original indexes ({3}, {4})", adjustedSegmentIndex, tileIndex, segmentIndex, rowIndex, tileIndex));
				}
			}
			return result;
		}
		set
		{
			int segmentIndex;
			int adjustedSegmentIndex;
			if (CanGetTileIndicesFrom(rowIndex, tileIndex, out segmentIndex, out adjustedSegmentIndex))
			{
				try
				{
					allSegments[segmentIndex][adjustedSegmentIndex, tileIndex] = value;
				}
				catch
				{
					Debug.LogError(string.Format("SMBD: error trying to set tile {0}, {1} from board {2} (based on original indexes ({3}, {4})", adjustedSegmentIndex, tileIndex, segmentIndex, rowIndex, tileIndex));
				}
			}
		}
	}

	public Tile this[int segmentIndex, int rowIndex, int tileIndex]
	{
		get
		{
			Tile result = null;
			int num = segmentIndex - segmentsShifted;
			try
			{
				result = allSegments[num][rowIndex, tileIndex];
			}
			catch
			{
				Debug.LogError(string.Format("SMBD: error trying to get tile {0}, {1} from board {2} (based on indicdes ({3}: {4}, {5})", rowIndex, tileIndex, num, segmentIndex, rowIndex, tileIndex));
			}
			return result;
		}
		set
		{
			int num = segmentIndex - segmentsShifted;
			try
			{
				allSegments[num][rowIndex, tileIndex] = value;
			}
			catch
			{
				Debug.LogError(string.Format("SMBD: error trying to set tile {0}, {1} from board {2} (based on indicdes ({3}: {4}, {5})", rowIndex, tileIndex, num, segmentIndex, rowIndex, tileIndex));
			}
		}
	}

	public Tile this[MathUtils.IntTrio tileIndex]
	{
		get
		{
			return this[tileIndex.x, tileIndex.y, tileIndex.z];
		}
		set
		{
			this[tileIndex.x, tileIndex.y, tileIndex.z] = value;
		}
	}

	public void Reset()
	{
		allSegments = new Tile[25][,];
		WorldIsFinished = false;
		nextFreeSegmentIndex = 0;
		CurrentSegmentIndex = 0;
		nextSegmentIndex = 0;
		Length = 0;
		segmentsShifted = 0;
		rowsShifted = 0;
		FinalRow = 0;
		BufferManager.DisplayStartingRows();
	}

	public void AddSegment(SegmentType? forcedType = null)
	{
		if (WorldIsNotFinished || forcedType.HasValue)
		{
			Tile[,] segment = LevelConstructor.CreateSegment(nextSegmentIndex, Length, forcedType).Segment;
			int length = segment.GetLength(0);
			if (forcedType.HasValue && forcedType.Value == SegmentType.End)
			{
				FinalRow = Length + length - 1;
				GameManager.SetWorldEnd(length, FinalRow + 1);
			}
			if (nextFreeSegmentIndex >= allSegments.Length)
			{
				Debug.LogWarning("SMBD: WARNING: Ran out of Segment Space!  Shifting down segments to make room at end");
				ShiftSegmentsDown();
			}
			allSegments[nextFreeSegmentIndex] = segment;
			nextFreeSegmentIndex++;
			Length += length;
			nextSegmentIndex++;
			if (!WorldIsFinished && LevelDesigner.WorldIsNotEndless && Length >= LevelDesigner.WorldLength)
			{
				WorldIsFinished = true;
				FinishManager.ShowFinishAt(Length + 1);
				AddSegment(SegmentType.End);
			}
		}
	}

	public void UpdateCurrentSegment(int currentRowNum)
	{
		int segmentIndex;
		int adjustedSegmentIndex;
		if (CanGetRowNumFrom(currentRowNum, out segmentIndex, out adjustedSegmentIndex))
		{
			if (segmentIndex != CurrentSegmentIndex)
			{
				CurrentSegmentIndex = segmentIndex;
			}
			int totalAhead;
			int totalBehindInCurrentSegment;
			if (CanCountTotalRowsAround(currentRowNum, out totalAhead, out totalBehindInCurrentSegment))
			{
				if (totalBehindInCurrentSegment >= 4 && CurrentSegmentIndex > 0)
				{
					ShiftSegmentsDown();
				}
			}
			else
			{
				Debug.LogError(string.Format("SMBD: ERROR: Attempt to get row position from invalid row number {0} - most likely cause is a fault in the logic of function CanGetTotalRowsAround"));
			}
		}
		else
		{
			Debug.LogError("SMBD: ERROR: Attempt to update Board to board number to an invalid number - most likely cause it has reached end of buffered boards");
		}
	}

	public bool IsRowValid(int rowIndex)
	{
		return rowIndex >= rowsShifted && rowIndex < Length;
	}

	public bool IsRowInvalid(int rowIndex)
	{
		return !IsRowValid(rowIndex);
	}

	public bool IsValid(int rowIndex, int tileIndex)
	{
		return IsRowValid(rowIndex) && tileIndex >= 0 && tileIndex < CurrentWidth;
	}

	public bool IsValid(MathUtils.IntTrio index)
	{
		return IsValid(index.x, index.y, index.z);
	}

	public bool IsValid(int segmentIndex, int rowIndex, int tileIndex)
	{
		bool result = false;
		int num = segmentIndex - segmentsShifted;
		if ((float)num >= 0f && num < 25 && (float)rowIndex >= 0f && rowIndex < allSegments[num].GetLength(0) && (float)tileIndex >= 0f && tileIndex < allSegments[num].GetLength(1))
		{
			result = true;
		}
		return result;
	}

	public bool IsInvalid(MathUtils.IntTrio index)
	{
		return !IsValid(index);
	}

	public bool IsInvalid(int rowIndex, int tileIndex)
	{
		return !IsValid(rowIndex, tileIndex);
	}

	public bool IsInvalid(int segmentIndex, int rowIndex, int tileIndex)
	{
		return !IsValid(segmentIndex, rowIndex, tileIndex);
	}

	public bool IsNull(MathUtils.IntTrio index)
	{
		return IsInvalid(index) || this[index] == null;
	}

	public bool IsNull(int rowIndex, int tileIndex)
	{
		return IsInvalid(rowIndex, tileIndex) || this[rowIndex, tileIndex] == null;
	}

	public bool IsNull(int segmentIndex, int rowIndex, int tileIndex)
	{
		return IsInvalid(segmentIndex, rowIndex, tileIndex) || this[segmentIndex, rowIndex, tileIndex] == null;
	}

	public bool IsNotNull(MathUtils.IntTrio index)
	{
		return IsValid(index) && this[index] != null;
	}

	public bool IsNotNull(int rowIndex, int tileIndex)
	{
		return IsValid(rowIndex, tileIndex) && this[rowIndex, tileIndex] != null;
	}

	public bool IsNotNull(int segmentIndex, int rowIndex, int tileIndex)
	{
		return IsValid(segmentIndex, rowIndex, tileIndex) && this[segmentIndex, rowIndex, tileIndex] != null;
	}

	public bool RowIsWithinWorld(int rowIndex)
	{
		return WorldIsNotFinished || rowIndex <= FinalRow;
	}

	public bool RowIsNotWithinWorld(int rowIndex)
	{
		return !RowIsWithinWorld(rowIndex);
	}

	public float GetDeviationFor(float xPos)
	{
		float num = 5f;
		float num2 = 4f;
		return (0f - (xPos - num / 2f)) / num2 * 2f;
	}

	public MathUtils.IntTrio ToIndex(int rowNum, int tileNum)
	{
		int segmentIndex;
		int adjustedSegmentIndex;
		if (CanGetTileIndicesFrom(rowNum, tileNum, out segmentIndex, out adjustedSegmentIndex))
		{
			return new MathUtils.IntTrio(segmentIndex, adjustedSegmentIndex, tileNum);
		}
		Debug.LogError(string.Format("STBD: ERROR: Was unable to get three-part index from two-part index: {0}, {1}.  Returning 0, 0, 0 index as result", rowNum, tileNum));
		return MathUtils.IntTrio.One;
	}

	public int SegmentOf(int rowNum)
	{
		int segmentIndex;
		int adjustedSegmentIndex;
		CanGetTileIndicesFrom(rowNum, 0, out segmentIndex, out adjustedSegmentIndex);
		return segmentIndex;
	}

	private void ShiftSegmentsDown()
	{
		int num = 0;
		for (int i = 0; i < allSegments[0].GetLength(0); i++)
		{
			for (int j = 0; j < allSegments[0].GetLength(1); j++)
			{
				if (allSegments[0][i, j] != null)
				{
					allSegments[0][i, j].ResetTile();
				}
			}
		}
		rowsShifted += allSegments[0].GetLength(0);
		segmentsShifted++;
		for (int k = 0; k < nextFreeSegmentIndex - 1; k++)
		{
			allSegments[k] = allSegments[k + 1];
		}
		CurrentSegmentIndex--;
		nextFreeSegmentIndex--;
	}

	private bool CanGetTileIndicesFrom(int rowIndex, int tileIndex, out int segmentIndex, out int adjustedSegmentIndex)
	{
		bool flag = false;
		bool flag2 = false;
		segmentIndex = -1;
		adjustedSegmentIndex = -1;
		flag = CanGetRowNumFrom(rowIndex, out segmentIndex, out adjustedSegmentIndex);
		if (flag)
		{
			flag2 = tileIndex < allSegments[segmentIndex].GetLength(1);
		}
		return flag && flag2;
	}

	private bool CanGetRowNumFrom(int rowIndex, out int segmentIndex, out int adjustedSegmentIndex)
	{
		bool result = false;
		segmentIndex = -1;
		adjustedSegmentIndex = -1;
		int num = rowIndex - rowsShifted;
		for (int i = 0; i < nextFreeSegmentIndex; i++)
		{
			int length = allSegments[i].GetLength(0);
			if (num < length)
			{
				segmentIndex = i;
				adjustedSegmentIndex = num;
				result = true;
				break;
			}
			num -= length;
		}
		return result;
	}

	private bool CanCountTotalRowsAround(int currentRowIndex, out int totalAhead, out int totalBehindInCurrentSegment)
	{
		int segmentIndex;
		int adjustedSegmentIndex;
		bool result;
		if (CanGetRowNumFrom(currentRowIndex, out segmentIndex, out adjustedSegmentIndex))
		{
			result = true;
			totalBehindInCurrentSegment = adjustedSegmentIndex;
			totalAhead = allSegments[segmentIndex].GetLength(0) - (totalBehindInCurrentSegment + 1);
			for (int i = segmentIndex + 1; i < nextFreeSegmentIndex; i++)
			{
				totalAhead += allSegments[i].GetLength(0);
			}
		}
		else
		{
			result = false;
			totalAhead = -1;
			totalBehindInCurrentSegment = -1;
		}
		return result;
	}
}
