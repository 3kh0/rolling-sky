using UnityEngine;

public class BallHolder : MonoBehaviour
{
	private const int ballsPerRing = 6;

	private Transform[] rings;

	private Transform[] balls;

	private int ballsShown;

	private bool isInitialized;

	public void Initialize(int ballsToShow)
	{
		if (!isInitialized)
		{
			Transform[] array = TransformUtils.FindAll("part", base.transform);
			int num = MathUtils.FlooredIntDivision(array.Length, 6);
			int num2 = array.Length - num;
			rings = new Transform[num];
			balls = new Transform[num2];
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (i % 7 == 0)
				{
					rings[num3] = array[i];
					num3++;
				}
				else
				{
					balls[num4] = array[i];
					num4++;
				}
			}
			isInitialized = true;
		}
		ShowBalls(ballsToShow);
		ballsShown = ballsToShow;
		for (int j = 0; j < rings.Length; j++)
		{
			rings[j].localEulerAngles = Vector3.zero;
		}
	}

	public void UpdateHolder()
	{
		if (isInitialized)
		{
			float num = Time.smoothDeltaTime * 30f;
			for (int i = 0; i < rings.Length; i++)
			{
				rings[i].Rotate(Vector3.up * num * (i + 1));
			}
		}
	}

	public void TakeBall()
	{
		if (isInitialized)
		{
			float? num = null;
			int num2 = 0;
			int num3 = MathUtils.MinInt(6, ballsShown);
			for (int i = 0; i < num3; i++)
			{
				float y = balls[i].position.y;
				if (!num.HasValue || y < num.Value)
				{
					num = y;
					num2 = i;
				}
			}
			TransformUtils.Hide(balls[num2]);
		}
		else
		{
			Debug.LogError("BLHD: ERROR: Attempt to TakeBall() from BallHolder BEFORE Initialize() was called");
		}
	}

	private void ShowBalls(int totalToShow)
	{
		for (int i = 0; i < balls.Length; i++)
		{
			if (i < totalToShow)
			{
				TransformUtils.Show(balls[i]);
			}
			else
			{
				TransformUtils.Hide(balls[i]);
			}
		}
	}
}
