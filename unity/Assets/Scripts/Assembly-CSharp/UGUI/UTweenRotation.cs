using System;
using UnityEngine;

namespace UGUI
{
	[AddComponentMenu("Tools/UGUI/Tween/Tween Rotation")]
	public class UTweenRotation : UTweener
	{
		public Vector3 from;

		public Vector3 to;

		public bool quaternionLerp;

		private Transform mTrans;

		public Transform cachedTransform
		{
			get
			{
				if (mTrans == null)
				{
					mTrans = base.transform;
				}
				return mTrans;
			}
		}

		[Obsolete("Use 'value' instead")]
		public Quaternion rotation
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		public Quaternion value
		{
			get
			{
				return cachedTransform.localRotation;
			}
			set
			{
				cachedTransform.localRotation = value;
			}
		}

		protected override void OnUpdate(float factor, bool isFinished)
		{
			value = ((!quaternionLerp) ? Quaternion.Euler(new Vector3(Mathf.Lerp(from.x, to.x, factor), Mathf.Lerp(from.y, to.y, factor), Mathf.Lerp(from.z, to.z, factor))) : Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), factor));
		}

		public static UTweenRotation Begin(GameObject go, float duration, Quaternion rot)
		{
			UTweenRotation uTweenRotation = UTweener.Begin<UTweenRotation>(go, duration);
			uTweenRotation.from = uTweenRotation.value.eulerAngles;
			uTweenRotation.to = rot.eulerAngles;
			if (duration <= 0f)
			{
				uTweenRotation.Sample(1f, true);
				uTweenRotation.enabled = false;
			}
			return uTweenRotation;
		}

		[ContextMenu("Set 'From' to current value")]
		public override void SetStartToCurrentValue()
		{
			from = value.eulerAngles;
		}

		[ContextMenu("Set 'To' to current value")]
		public override void SetEndToCurrentValue()
		{
			to = value.eulerAngles;
		}

		[ContextMenu("Assume value of 'From'")]
		private void SetCurrentValueToStart()
		{
			value = Quaternion.Euler(from);
		}

		[ContextMenu("Assume value of 'To'")]
		private void SetCurrentValueToEnd()
		{
			value = Quaternion.Euler(to);
		}
	}
}
