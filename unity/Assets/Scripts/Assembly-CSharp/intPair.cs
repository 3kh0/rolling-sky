public struct intPair
{
	private const int nullCode = -1;

	public int i;

	public int j;

	private bool isNull
	{
		get
		{
			return (i == -1) & (j == -1);
		}
	}

	private bool hasNull
	{
		get
		{
			return (i == -1) | (j == -1);
		}
	}

	public intPair(int i)
	{
		this.i = i;
		j = -1;
	}

	public intPair(int i, int j)
	{
		this.i = i;
		this.j = j;
	}

	public intPair(float i)
	{
		this.i = (int)i;
		j = -1;
	}

	public intPair(float i, float j)
	{
		this.i = (int)i;
		this.j = (int)j;
	}
}
