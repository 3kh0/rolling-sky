using System;
using UnityEngine;

namespace UGUI
{
	[AddComponentMenu("Tools/UGUI/Tween/Tween Scale")]
	public class UTweenScale : UTweener
	{
		public Vector3 from = Vector3.one;

		public Vector3 to = Vector3.one;

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

		public Vector3 value
		{
			get
			{
				return cachedTransform.localScale;
			}
			set
			{
				cachedTransform.localScale = value;
			}
		}

		[Obsolete("Use 'value' instead")]
		public Vector3 scale
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

		protected override void OnUpdate(float factor, bool isFinished)
		{
			value = from * (1f - factor) + to * factor;
		}

		public static UTweenScale Begin(GameObject go, float duration, Vector3 scale)
		{
			UTweenScale uTweenScale = UTweener.Begin<UTweenScale>(go, duration);
			uTweenScale.from = uTweenScale.value;
			uTweenScale.to = scale;
			if (duration <= 0f)
			{
				uTweenScale.Sample(1f, true);
				uTweenScale.enabled = false;
			}
			return uTweenScale;
		}

		[ContextMenu("Set 'From' to current value")]
		public override void SetStartToCurrentValue()
		{
			from = value;
		}

		[ContextMenu("Set 'To' to current value")]
		public override void SetEndToCurrentValue()
		{
			to = value;
		}

		[ContextMenu("Assume value of 'From'")]
		private void SetCurrentValueToStart()
		{
			value = from;
		}

		[ContextMenu("Assume value of 'To'")]
		private void SetCurrentValueToEnd()
		{
			value = to;
		}
	}
}
