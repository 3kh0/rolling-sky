using UnityEngine;

public class PortalAnimator : MonoBehaviour
{
	private enum State
	{
		Waiting = 0,
		Dialing = 1,
		Activating = 2,
		Active = 3,
		Deactivating = 4
	}

	private struct PortalPart
	{
		public static float DepthPerRing;

		public readonly Transform Transform;

		public readonly int Number;

		public readonly int Ring;

		public readonly int RingPos;

		public readonly float RingPercent;

		public readonly float NumberAsFloat;

		public readonly float RingAsFloat;

		public readonly float RingPosAsFloat;

		public readonly float RingOffset;

		private readonly float startingHeight;

		private FrameAnim.SequenceAuto geo;

		public PortalPart(Transform transform, int partNumber, int ring, int ringPos, float ringPercent, float ringsTotal)
		{
			Transform = transform;
			Number = partNumber;
			Ring = ring;
			RingPos = ringPos;
			RingPercent = ringPercent;
			NumberAsFloat = Number;
			RingAsFloat = Ring;
			RingPosAsFloat = RingPos;
			RingOffset = RingAsFloat / ringsTotal;
			startingHeight = RingAsFloat * DepthPerRing - 1f;
			geo = new FrameAnim.SequenceAuto(Transform);
			Reset();
		}

		public void UpdatePart(float animCycle, bool fullAnim, float? animWeight = null)
		{
			float num2;
			float num3;
			float num;
			if (fullAnim)
			{
				float value = (animCycle + RingPercent + RingOffset) * 2f;
				num = FloatAnim.Wave01(value);
				num2 = startingHeight;
				num3 = 0.125f;
				num *= FloatAnim.Smooth(RingOffset, false, true);
			}
			else
			{
				float value = animCycle + RingOffset;
				num = FloatAnim.Wave01(value);
				num2 = 0f;
				num3 = 0.125f;
			}
			float num4 = num2 + num * num3;
			if (animWeight.HasValue)
			{
				num4 *= animWeight.Value;
			}
			TransformUtils.SetY(Transform, num4, true);
		}

		public void Show(int portalNumber)
		{
			geo.Show(portalNumber);
		}

		public void Activate(float transitionPercent, float portalAnimCycle)
		{
			float num = MathUtils.ToPercent01(transitionPercent, 0.5f, 1f);
			if (num > RingOffset)
			{
				UpdatePart(portalAnimCycle, false, num);
				if (geo.isLast)
				{
					geo.ShowFirst();
				}
			}
			else
			{
				float value = MathUtils.ToPercent01(transitionPercent, 0f, 0.5f) * RingOffset;
				float newY = FloatAnim.Wave01(value) * 0.1f;
				TransformUtils.SetY(Transform, newY, true);
			}
		}

		public void Deactivate(float transitionPercent, float portalAnimCycle)
		{
			float num = MathUtils.ToPercent01(transitionPercent, 0.5f, 1f);
			if (num <= RingOffset)
			{
				UpdatePart(portalAnimCycle, false, num);
				if (geo.isFirst)
				{
					geo.ShowLast();
				}
			}
			else
			{
				float value = MathUtils.ToPercent01(transitionPercent, 0f, 0.5f) * RingOffset;
				float newY = FloatAnim.Wave01(value) * 0.1f;
				TransformUtils.SetY(Transform, newY, true);
			}
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} {2} {3} {4}", Transform.name, Number, Ring, RingPos, RingPercent);
		}

		public void Reset()
		{
			TransformUtils.SetY(Transform, 0f, true);
		}
	}

	private struct PortalDial
	{
		public readonly int Number;

		public readonly int Base;

		private readonly Transform transform;

		private readonly float interval;

		private float endingAngle;

		private float startingAngle;

		private bool animating;

		private bool reversedAnim;

		public PortalDial(int dialNumber, Transform dialTransform, int dialBase)
		{
			Number = dialNumber;
			transform = dialTransform;
			Base = dialBase;
			interval = 360f / (float)Base;
			endingAngle = 0f;
			startingAngle = 0f;
			animating = false;
			reversedAnim = false;
		}

		public void DialTo(int dialedNumber)
		{
			DialTo(dialedNumber, null);
		}

		public void DialTo(int dialedNumber, int? startingDialedNumber)
		{
			if (startingDialedNumber.HasValue)
			{
				startingAngle = ToAngle(startingDialedNumber.Value, null);
				endingAngle = ToAngle(dialedNumber, startingAngle);
			}
			else
			{
				endingAngle = ToAngle(dialedNumber, null);
				startingAngle = endingAngle + ((!MathUtils.IsEven(Number)) ? 180f : (-180f));
			}
			RotateTo(startingAngle);
			animating = endingAngle != startingAngle;
			reversedAnim = startingDialedNumber.HasValue && startingDialedNumber.Value < dialedNumber;
		}

		public void UpdateDial(float animPercent)
		{
			if (animating)
			{
				if (animPercent >= 1f)
				{
					animPercent = 1f;
					animating = false;
				}
				else
				{
					animPercent = FloatAnim.Smooth(animPercent, true, true);
				}
				float angle = MathUtils.FromPercent(animPercent, startingAngle, endingAngle);
				RotateTo(angle);
			}
		}

		private float ToAngle(int dialedNumber, float? lastAngle)
		{
			if (Number > 0)
			{
				dialedNumber = MathUtils.FlooredIntDivision(dialedNumber, Base * Number);
			}
			dialedNumber %= Base;
			float num = (float)dialedNumber * interval;
			if (lastAngle.HasValue && MathUtils.Distance(num, lastAngle.Value) > 180f)
			{
				num -= 360f;
			}
			return num;
		}

		private void RotateTo(float angle)
		{
			transform.localEulerAngles = new Vector3(0f, 0f - angle, 0f);
		}
	}

	private const string portalPartName = "PortalGeo";

	private const int PartsPerRing = 32;

	private const float Depth = 1f;

	private const int MinFps = 30;

	private const int DialBase = 5;

	private const float DialTime = 0f;

	private const float DialSmoothPoint = 0.75f;

	private const float PassiveAnimAmnt = 0.125f;

	private const float PassiveAnimTime = 1f;

	private const float ActivateAnimTime = 0.75f;

	private const float ActivateSwitchPoint = 0.5f;

	private const float ActiveAnimAmnt = 0.125f;

	private const float ActiveAnimTime = 1f;

	private const float SlowActiveAnimTime = 1.5f;

	private const float PortalGroundOffset = 0.49f;

	private const float EndingAnimOffset = 0.525f;

	private const float StartingAnimOffset = 0.5f;

	private const float PortalStartingScale = 0.75f;

	private const float ActivateToSlowRatio = 0.5f;

	private const float MaxDeltaTime = 1f / 30f;

	public Transform PortalStaticActive;

	public Transform PortalStaticPassive;

	public Transform PortalAnimatedGroup;

	public Transform PortalTrimmingInnerGroup;

	public Transform PortalTrimmingOuterGroup;

	public Transform[] PortalDials;

	private State state;

	private Transform AnimTransform;

	private PortalPart[] portalParts;

	private PortalDial[] portalDials;

	private int portalNumber;

	private float animTimer;

	private float animTime;

	private bool isStartPortal;

	private FrameAnim.Sequence portalStatic;

	private FrameAnim.Frame portalActive;

	private FrameAnim.Sequence portalTrimming;

	private bool usingAnimParts;

	private bool isInitialized;

	private bool isEndPortal
	{
		get
		{
			return !isStartPortal;
		}
	}

	public void Initialize(bool endingPortal, bool isAnimated)
	{
		ConfigurePortal(endingPortal, isAnimated);
	}

	public void ConfigurePortal(bool endingPortal, bool isAnimated)
	{
		isStartPortal = !endingPortal;
		usingAnimParts = isAnimated;
		if (isStartPortal)
		{
			portalNumber = 1;
			state = State.Waiting;
		}
		else
		{
			portalNumber = 0;
			state = State.Active;
			animTime = 1f;
			animTimer = animTime;
		}
		if (!isInitialized)
		{
			AnimTransform = TransformUtils.Find("anim", base.transform);
			portalStatic = new FrameAnim.Sequence(PortalStaticActive, PortalStaticPassive);
			portalActive = new FrameAnim.Frame(PortalAnimatedGroup);
			portalTrimming = new FrameAnim.Sequence(PortalTrimmingOuterGroup, PortalTrimmingInnerGroup);
			if (isAnimated)
			{
				FindPortalParts();
				portalStatic.Hide();
			}
			else
			{
				portalActive.Hide();
			}
			portalDials = new PortalDial[PortalDials.Length];
			for (int i = 0; i < portalDials.Length; i++)
			{
				portalDials[i] = new PortalDial(i, PortalDials[i], 5);
			}
			isInitialized = true;
		}
		else if (isAnimated)
		{
			for (int j = 0; j < portalParts.Length; j++)
			{
				portalParts[j].Reset();
			}
		}
		if (isStartPortal)
		{
			portalTrimming.Hide();
			AnimTransform.localEulerAngles = Vector3.zero;
			AnimTransform.localPosition = new Vector3(0f, -0.49f, 0f);
			AnimTransform.localScale = Vector3.one * 0.75f;
		}
		else
		{
			portalTrimming.Show(0);
			AnimTransform.localEulerAngles = new Vector3(90f, 0f, 0f);
			AnimTransform.localPosition = Vector3.zero;
			AnimTransform.localScale = Vector3.one;
		}
		PortalStaticPassive.localEulerAngles = Vector3.zero;
		PortalStaticActive.localEulerAngles = Vector3.zero;
		ShowCurrentPortal();
	}

	public void UpdatePortal()
	{
		if (state == State.Waiting)
		{
			return;
		}
		bool flag = false;
		float num = 0f;
		float num2 = 0f;
		float num3 = MathUtils.Min(Time.smoothDeltaTime, 1f / 30f);
		animTimer -= num3;
		if (state == State.Active)
		{
			if (animTimer <= 0f)
			{
				animTimer += animTime;
			}
			num2 = 1f - animTimer / animTime;
		}
		else if (animTimer <= 0f)
		{
			animTimer = 0f;
			num2 = 1f;
			flag = true;
		}
		else
		{
			num2 = 1f - animTimer / animTime;
		}
		switch (state)
		{
		case State.Dialing:
		{
			if (flag)
			{
				state = State.Activating;
				animTime = 0.75f;
			}
			for (int k = 0; k < portalDials.Length; k++)
			{
				portalDials[k].UpdateDial(num2);
			}
			break;
		}
		case State.Activating:
			if (flag)
			{
				state = State.Active;
				animTime = 1.5f;
				num = 0.75f;
				if (!usingAnimParts)
				{
					portalStatic.ShowFirst();
				}
			}
			if (usingAnimParts)
			{
				float portalAnimCycle2 = num2 * 0.5f;
				for (int j = 0; j < portalParts.Length; j++)
				{
					portalParts[j].Activate(num2, portalAnimCycle2);
				}
			}
			else
			{
				float y2 = 360f * FloatAnim.PowerSmooth(num2, true, false, 3);
				PortalStaticPassive.localEulerAngles = new Vector3(0f, y2, 0f);
			}
			break;
		case State.Active:
			if (usingAnimParts)
			{
				for (int l = 0; l < portalParts.Length; l++)
				{
					portalParts[l].UpdatePart(num2, isEndPortal);
				}
			}
			else
			{
				float y3 = 360f * num2;
				PortalStaticActive.localEulerAngles = new Vector3(0f, y3, 0f);
			}
			break;
		case State.Deactivating:
			if (flag)
			{
				state = State.Waiting;
				animTime = 1.5f;
				num = 0.75f;
			}
			num2 = 1f - num2;
			if (usingAnimParts)
			{
				float portalAnimCycle = num2 * 0.5f;
				for (int i = 0; i < portalParts.Length; i++)
				{
					portalParts[i].Deactivate(num2, portalAnimCycle);
				}
			}
			else
			{
				float y = 360f * FloatAnim.PowerSmooth(num2, true, false, 3);
				PortalStaticPassive.localEulerAngles = new Vector3(0f, y, 0f);
			}
			break;
		}
		if (flag)
		{
			animTimer = animTime - num;
		}
	}

	public void DialTo(int dialedNumber)
	{
		StartDialing(dialedNumber, null);
	}

	public void DialTo(int dialedNumber, int startingDialedNumber)
	{
		StartDialing(dialedNumber, startingDialedNumber);
	}

	public void DialTo(int dialedNumber, int? startingDialedNumber)
	{
		StartDialing(dialedNumber, startingDialedNumber);
	}

	public void TryActivate()
	{
		if (state == State.Waiting)
		{
			Activate();
		}
	}

	public void Activate()
	{
		state = State.Activating;
		animTime = 0.75f;
		animTimer = animTime;
	}

	public void Deactivate()
	{
		state = State.Deactivating;
		animTime = 0.75f;
		animTimer = animTime;
		if (!usingAnimParts)
		{
			portalStatic.ShowLast();
		}
	}

	private void FindPortalParts()
	{
		Transform[] array = TransformUtils.FindAll("PortalGeo", AnimTransform);
		portalParts = new PortalPart[array.Length];
		float num = portalParts.Length / 32;
		PortalPart.DepthPerRing = 1f / num;
		for (int i = 0; i < portalParts.Length; i++)
		{
			int ring = ((i != 0) ? (MathUtils.FlooredIntDivision(i - 1, 32) + 1) : 0);
			int num2 = ((i != 0) ? ((i - 1) % 32) : 0);
			float ringPercent = MathUtils.ToPercentInt(num2, 0, 31);
			portalParts[i] = new PortalPart(array[i], i, ring, num2, ringPercent, num);
		}
	}

	private void StartDialing(int dialedNumber, int? startingDialedNumber)
	{
		for (int i = 0; i < portalDials.Length; i++)
		{
			portalDials[i].DialTo(dialedNumber, startingDialedNumber);
		}
		state = State.Dialing;
		animTime = 0f;
		animTimer = animTime;
	}

	private void SwitchPortal()
	{
		if (portalNumber == 0)
		{
			portalNumber = 1;
			animTime = 1.5f;
		}
		else
		{
			portalNumber = 0;
			animTime = 1f;
		}
		animTimer = animTime;
		ShowCurrentPortal();
	}

	private void ShowCurrentPortal()
	{
		if (usingAnimParts)
		{
			for (int i = 0; i < portalParts.Length; i++)
			{
				portalParts[i].Show(portalNumber);
			}
		}
		else
		{
			portalStatic.Show(portalNumber);
		}
	}
}
