using UnityEngine;

public struct floatTriBool
{
	private const string classCode = "FTB";

	private const float nullCode = -1f;

	public float i;

	public float j;

	public float k;

	public bool boolean;

	public bool isNull
	{
		get
		{
			return (i == -1f) & (j == -1f) & (k == -1f);
		}
	}

	public bool hasNull
	{
		get
		{
			return (i == -1f) | (j == -1f) | (k == -1f);
		}
	}

	public floatTriBool(float i, bool boolean = false)
	{
		this.i = i;
		j = -1f;
		k = -1f;
		this.boolean = boolean;
	}

	public floatTriBool(float i, float j, bool boolean = false)
	{
		this.i = i;
		this.j = j;
		k = -1f;
		this.boolean = boolean;
	}

	public floatTriBool(float i, float j, float k, bool boolean = false)
	{
		this.i = i;
		this.j = j;
		this.k = k;
		this.boolean = boolean;
	}

	public void Set(int index, float value)
	{
		switch (index)
		{
		case 0:
			i = value;
			break;
		case 1:
			j = value;
			break;
		case 2:
			k = value;
			break;
		default:
			PrintRangeError(index);
			break;
		}
	}

	public float Get(int index)
	{
		switch (index)
		{
		case 0:
			return i;
		case 1:
			return j;
		case 2:
			return k;
		default:
			PrintRangeError(index);
			return -1f;
		}
	}

	public void Nullify()
	{
		i = (j = (k = -1f));
	}

	public bool NotNull(int index)
	{
		switch (index)
		{
		case 0:
			return i != -1f;
		case 1:
			return j != -1f;
		case 2:
			return k != -1f;
		default:
			PrintRangeError(index);
			return true;
		}
	}

	private void PrintRangeError(int index)
	{
		Debug.Log(ErrorStrings.IndexOutOfRange(index, "index", 0, 2));
	}
}
