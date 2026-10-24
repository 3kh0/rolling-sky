using UnityEngine;

public class Firework : MonoBehaviour
{
	private const string explodeFloatName = "_Ammount";

	private const string fadeFloatName = "_Alpha";

	public float explodeTime = 1f;

	public float growRate = 0.5f;

	public float fadeTime = 3f;

	private bool emitting;

	private Material material;

	private float explodeSize;

	private float animTimer;

	private bool exploding;

	private bool growing;

	private bool fading;

	public float TotalAnimTime
	{
		get
		{
			return explodeTime + fadeTime;
		}
	}

	public bool emit
	{
		get
		{
			return emitting;
		}
		set
		{
			if (value)
			{
				Emit();
			}
			else
			{
				Reset();
			}
		}
	}

	private void OnEnable()
	{
		explodeSize = base.transform.localScale.x;
		material = base.transform.GetComponent<Renderer>().material;
		Reset();
	}

	public void Reset()
	{
		base.transform.localScale = Vector3.zero;
		material.SetFloat("_Ammount", 0f);
		material.SetFloat("_Alpha", 1f);
		fading = false;
		emitting = false;
	}

	private void Update()
	{
		if (!emitting)
		{
			return;
		}
		if (exploding)
		{
			float animPercent;
			if (FloatAnim.AnimElapsed(ref animTimer, explodeTime, out animPercent, false, true))
			{
				exploding = false;
				fading = true;
				animTimer = fadeTime;
			}
			material.SetFloat("_Ammount", animPercent);
			if (growing)
			{
				float num = animPercent / growRate;
				if (num >= 1f)
				{
					num = 1f;
					growing = false;
				}
				base.transform.localScale = Vector3.one * (explodeSize * num);
			}
		}
		else if (fading)
		{
			float animPercent2;
			if (FloatAnim.AnimElapsed(ref animTimer, fadeTime, out animPercent2, false, true, true))
			{
				Reset();
			}
			else
			{
				material.SetFloat("_Alpha", animPercent2);
			}
		}
	}

	public void Emit()
	{
		if (!emitting)
		{
			emitting = true;
			exploding = true;
			growing = true;
			animTimer = explodeTime;
			base.transform.LookAt(Camera.main.transform);
			base.transform.Rotate(new Vector3(0f, 180f, 0f));
		}
	}
}
