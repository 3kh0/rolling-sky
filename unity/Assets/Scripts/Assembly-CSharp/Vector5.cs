public struct Vector5
{
	public float x;

	public float y;

	public float z;

	public float w;

	public float v;

	public static Vector5 Zero
	{
		get
		{
			return new Vector5(0f);
		}
	}

	public static Vector5 One
	{
		get
		{
			return new Vector5(1f);
		}
	}

	public Vector5(float sharedStartingValue)
	{
		x = sharedStartingValue;
		y = sharedStartingValue;
		z = sharedStartingValue;
		w = sharedStartingValue;
		v = sharedStartingValue;
	}

	public Vector5(float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV)
	{
		x = startingValueX;
		y = startingValueY;
		z = startingValueZ;
		w = startingValueW;
		v = startingValueV;
	}
}
