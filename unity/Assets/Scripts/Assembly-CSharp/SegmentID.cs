public struct SegmentID
{
	public readonly SegmentType Type;

	public readonly int Difficulty;

	public int TypeAsInt
	{
		get
		{
			return (int)Type;
		}
	}

	public string TypeAsString
	{
		get
		{
			return Type.ToString();
		}
	}

	public string DifficultyAsString
	{
		get
		{
			return Difficulty.ToString();
		}
	}

	public string DifficultyChar
	{
		get
		{
			int value = Difficulty;
			string label = string.Empty;
			do
			{
				label = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[value % 26] + label;
				value = value / 26 - 1;
			}
			while (value >= 0);
			return label;
		}
	}

	public SegmentID(SegmentType type, int difficulty)
	{
		Type = type;
		Difficulty = difficulty;
	}

	public override string ToString()
	{
		return string.Format("{0}{1}", Type, DifficultyChar);
	}
}
