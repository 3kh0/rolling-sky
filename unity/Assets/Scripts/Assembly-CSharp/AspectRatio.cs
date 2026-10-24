using UnityEngine;

public class AspectRatio
{
	public float desired { get; private set; }

	public float current { get; private set; }

	public float divider { get; private set; }

	public float multiplier { get; private set; }

	public AspectRatio(float desired)
	{
		this.desired = desired;
		current = (float)Screen.width / (float)Screen.height;
		multiplier = current / desired;
		divider = desired / current;
	}
}
