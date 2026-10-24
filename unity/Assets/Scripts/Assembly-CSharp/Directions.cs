using UnityEngine;

public class Directions : MonoBehaviour
{
	public static Direction[] MainDirections = new Direction[4]
	{
		Direction.Top,
		Direction.Right,
		Direction.Bottom,
		Direction.Left
	};

	public static Direction[] AllDirections = new Direction[8]
	{
		Direction.Top,
		Direction.TopRight,
		Direction.Right,
		Direction.BottomRight,
		Direction.Bottom,
		Direction.BottomLeft,
		Direction.Left,
		Direction.TopLeft
	};

	private static int totalDirections = AllDirections.Length;

	public static Direction GetClockwise(Direction inputDirection)
	{
		return GetClockwise(inputDirection, 1);
	}

	public static Direction GetClockwise(Direction inputDirection, int rotations)
	{
		return AllDirections[MathUtils.Indexed(ToInt(inputDirection) + rotations, totalDirections)];
	}

	public static Direction GetCounterClockwise(Direction inputDirection)
	{
		return GetClockwise(inputDirection, -1);
	}

	public static Direction GetCounterClockwise(Direction inputDirection, int rotations)
	{
		return GetClockwise(inputDirection, -rotations);
	}

	public static Direction GetFlipped(Direction inputDirection)
	{
		return GetClockwise(inputDirection, 4);
	}

	public static Direction GetFlippedX(Direction inputDirection)
	{
		return GetFlippedHorizontal(inputDirection);
	}

	public static Direction GetFlippedHorizontal(Direction inputDirection)
	{
		switch (inputDirection)
		{
		case Direction.Left:
			return Direction.Right;
		case Direction.Right:
			return Direction.Left;
		case Direction.Top:
			return Direction.Top;
		case Direction.Bottom:
			return Direction.Bottom;
		case Direction.TopLeft:
			return Direction.TopRight;
		case Direction.TopRight:
			return Direction.TopLeft;
		case Direction.BottomLeft:
			return Direction.BottomRight;
		case Direction.BottomRight:
			return Direction.BottomLeft;
		default:
			Debug.LogError(string.Format("DRTS: ERROR: Recieved unhandled Direction '{0}' in GetFlippedHorizontal's case statement", inputDirection));
			return inputDirection;
		}
	}

	public static Direction GetFlippedY(Direction inputDirection)
	{
		return GetFlippedVertical(inputDirection);
	}

	public static Direction GetFlippedVertical(Direction direction)
	{
		switch (direction)
		{
		case Direction.Top:
			return Direction.Bottom;
		case Direction.Bottom:
			return Direction.Top;
		case Direction.Left:
			return Direction.Left;
		case Direction.Right:
			return Direction.Right;
		case Direction.TopLeft:
			return Direction.BottomLeft;
		case Direction.TopRight:
			return Direction.BottomRight;
		case Direction.BottomLeft:
			return Direction.TopLeft;
		case Direction.BottomRight:
			return Direction.TopRight;
		default:
			Debug.LogError(string.Format("DRTS: ERROR: Recieved unhandled Direction '{0}' in GetFlippedVertical's case statement. Recheck logic", direction));
			return direction;
		}
	}

	public static bool IsCorner(Direction direction)
	{
		return direction == Direction.TopLeft || direction == Direction.TopRight || direction == Direction.BottomLeft || direction == Direction.BottomRight;
	}

	public static bool IsCorner(int directionIndex)
	{
		return IsCorner((Direction)directionIndex);
	}

	public static bool IsNotCorner(Direction direction)
	{
		return !IsCorner(direction);
	}

	public static bool IsNotCorner(int directionIndex)
	{
		return !IsCorner(directionIndex);
	}

	public static int ToInt(Direction direction)
	{
		return (int)direction;
	}

	public static float ToFloat(Direction direction)
	{
		return ToInt(direction);
	}

	public static MathUtils.IntPair ToOffset(Direction direction)
	{
		int xOffset = 0;
		int yOffset = 0;
		ToOffset(direction, ref xOffset, ref yOffset);
		return new MathUtils.IntPair(xOffset, yOffset);
	}

	public static MathUtils.IntPair ToOffset(int directionIndex)
	{
		return ToOffset(ToDirection(directionIndex));
	}

	public static void ToOffset(Direction direction, ref int xOffset, ref int yOffset)
	{
		switch (direction)
		{
		case Direction.Top:
			yOffset++;
			break;
		case Direction.Bottom:
			yOffset--;
			break;
		case Direction.Left:
			xOffset--;
			break;
		case Direction.Right:
			xOffset++;
			break;
		case Direction.TopLeft:
			yOffset++;
			xOffset--;
			break;
		case Direction.TopRight:
			yOffset++;
			xOffset++;
			break;
		case Direction.BottomLeft:
			yOffset--;
			xOffset--;
			break;
		case Direction.BottomRight:
			yOffset--;
			xOffset++;
			break;
		default:
			Debug.LogError(string.Format("DRTS: ERROR: Recieved unhandled Direction '{0}' in Directions.ToOffset()'s case statement. Recheck logic", direction));
			break;
		}
	}

	public static void ToOffset(int directionIndex, ref int xOffset, ref int yOffset)
	{
		ToOffset(ToDirection(directionIndex), ref xOffset, ref yOffset);
	}

	public static Direction ToDirection(int directionIndex)
	{
		return AllDirections[MathUtils.Indexed(directionIndex, totalDirections)];
	}

	public static Direction ToDirection(Vector2 xy)
	{
		return ToDirection(xy.x, xy.y);
	}

	public static Direction ToDirection(Vector3 xyz)
	{
		return ToDirection(xyz.x, xyz.y);
	}

	public static Direction ToDirection(MathUtils.IntPair xy)
	{
		return ToDirection(xy.x, xy.y);
	}

	public static Direction ToDirection(MathUtils.IntTrio xyz)
	{
		return ToDirection(xyz.x, xyz.y);
	}

	public static Direction ToDirection(int x, int y)
	{
		if (x == 0)
		{
			if (y == 0)
			{
				return Direction.Null;
			}
			if (y > 0)
			{
				return Direction.Top;
			}
			return Direction.Bottom;
		}
		if (x > 0)
		{
			if (y == 0)
			{
				return Direction.Right;
			}
			if (y > 0)
			{
				return Direction.TopRight;
			}
			return Direction.BottomRight;
		}
		if (y == 0)
		{
			return Direction.Left;
		}
		if (y > 0)
		{
			return Direction.TopLeft;
		}
		return Direction.BottomLeft;
	}

	public static Direction ToDirection(float x, float y)
	{
		if (x == 0f)
		{
			if (y == 0f)
			{
				return Direction.Null;
			}
			if (y > 0f)
			{
				return Direction.Top;
			}
			return Direction.Bottom;
		}
		if (x > 0f)
		{
			if (y == 0f)
			{
				return Direction.Left;
			}
			if (y > 0f)
			{
				return Direction.TopLeft;
			}
			return Direction.BottomLeft;
		}
		if (y == 0f)
		{
			return Direction.Right;
		}
		if (y > 0f)
		{
			return Direction.TopRight;
		}
		return Direction.BottomRight;
	}

	public static string ToDescription(Vector2 xy)
	{
		return ToDescription(ToDirection(xy));
	}

	public static string ToDescription(Vector3 xyz)
	{
		return ToDescription(ToDirection(xyz));
	}

	public static string ToDescription(MathUtils.IntPair xy)
	{
		return ToDescription(ToDirection(xy));
	}

	public static string ToDescription(MathUtils.IntTrio xyz)
	{
		return ToDescription(ToDirection(xyz));
	}

	public static string ToDescription(int x, int y)
	{
		return ToDescription(ToDirection(x, y));
	}

	public static string ToDescription(float x, float y)
	{
		return ToDescription(ToDirection(x, y));
	}

	public static string ToDescription(Direction direction)
	{
		switch (direction)
		{
		case Direction.Top:
			return "Upper";
		case Direction.TopLeft:
			return "Upper Left";
		case Direction.Left:
			return "Left";
		case Direction.BottomLeft:
			return "Lower Left";
		case Direction.Bottom:
			return "Lower";
		case Direction.BottomRight:
			return "Lower Right";
		case Direction.Right:
			return "Right";
		case Direction.TopRight:
			return "Upper Right";
		case Direction.Null:
			return "Same";
		default:
			Debug.LogError(string.Format("DIRC: ERROR: recieved unhandled Direction input of '{0}' for ToDescription()'s case statement.  Check logic", direction));
			return "Error";
		}
	}
}
