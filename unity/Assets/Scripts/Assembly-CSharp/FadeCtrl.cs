using UnityEngine;

public class FadeCtrl : MonoBehaviour
{
	protected enum State
	{
		FadeIn = 0,
		FadeInMid = 1,
		FadingIn = 2,
		Shown = 3,
		FadeOut = 4,
		FadeOutMid = 5,
		FadingOut = 6,
		Hidden = 7
	}

	private const string classCode = "FCT";

	private const string colorName = "_TintColor";

	private const string textureName = "_MainTex";

	private const float fadeTime = 1f;

	protected State state;

	protected float fadeTimer;

	protected float slideTimer;

	private bool fadeCmd;

	public bool hidden
	{
		get
		{
			return state == State.Hidden;
		}
	}

	public bool shown
	{
		get
		{
			return state == State.Shown;
		}
	}

	public bool fadingOut
	{
		get
		{
			return state == State.FadeOut || state == State.FadingOut;
		}
	}

	public bool fadingIn
	{
		get
		{
			return state == State.FadeIn || state == State.FadingIn;
		}
	}

	public bool fadable { get; protected set; }

	public bool overrideStateCtrl { get; protected set; }

	private void Update()
	{
		if (fadable)
		{
			if (fadingOut)
			{
				Fading();
			}
			else if (fadingIn)
			{
				Fading(false);
			}
		}
	}

	public void Hide()
	{
		if (overrideStateCtrl || state != State.Hidden)
		{
			SetVisibility(false);
			state = State.Hidden;
		}
	}

	public void Show()
	{
		if (overrideStateCtrl || state != State.Shown)
		{
			SetVisibility();
			state = State.Shown;
		}
	}

	protected virtual void SetVisibility(bool visible = true)
	{
		if (visible)
		{
			TransformUtils.Show(base.transform);
		}
		else
		{
			TransformUtils.Hide(base.transform);
		}
	}

	public void FadeIn()
	{
		if (!fadable)
		{
			Show();
		}
		else if (state != State.FadeIn)
		{
			if (state == State.Hidden)
			{
				Show();
			}
			SetFade(0f);
			state = State.FadeIn;
			fadeCmd = true;
		}
	}

	public void FadeOut()
	{
		if (!fadable)
		{
			Hide();
		}
		else if (state != State.FadeOut)
		{
			if (state == State.Hidden)
			{
				Show();
			}
			SetFade(1f);
			state = State.FadeOut;
			fadeCmd = true;
		}
	}

	private void Fading(bool fadeOut = true)
	{
		if (fadeCmd)
		{
			if ((state == State.FadeOut) | (state == State.FadeIn))
			{
				fadeTimer = 1f;
				switch (state)
				{
				case State.FadeOut:
					state = State.FadingOut;
					break;
				case State.FadeIn:
					state = State.FadingIn;
					break;
				default:
					Debug.LogError(string.Format("Error FDG_IFC - unexpected fade-initialize command of {0} sent to FadeCtrl.Fading", state));
					break;
				}
			}
			fadeCmd = false;
		}
		float num = MathUtils.ToPercent(fadeTimer, 0f, 1f, !fadingOut);
		SetFade(Mathf.Clamp01(FloatAnim.Smooth(MathUtils.Abs(num), true, true)));
		if ((fadeOut && num <= 0f) || (!fadeOut && num >= 1f))
		{
			if (fadeOut)
			{
				Hide();
			}
			else
			{
				Show();
			}
		}
		else
		{
			fadeTimer -= Time.deltaTime;
		}
	}

	public virtual void SetFade(float fadeAmnt)
	{
	}
}
