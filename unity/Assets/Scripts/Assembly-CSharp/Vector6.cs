public struct Vector6
{
	public float x;

	public float y;

	public float z;

	public float w;

	public float v;

	public float u;

	public static Vector6 Zero
	{
		get
		{
			return new Vector6(0f);
		}
	}

	public static Vector6 One
	{
		get
		{
			return new Vector6(1f);
		}
	}

	public Vector6(float sharedStartingValue)
	{
		x = sharedStartingValue;
		y = sharedStartingValue;
		z = sharedStartingValue;
		w = sharedStartingValue;
		v = sharedStartingValue;
		u = sharedStartingValue;
	}

	public Vector6(float startingValueX, float startingValueY, float startingValueZ, float startingValueW, float startingValueV, float startingValueU)
	{
		x = startingValueX;
		y = startingValueY;
		z = startingValueZ;
		w = startingValueW;
		v = startingValueV;
		u = startingValueU;
	}
}
