using System.Collections.Generic;
using UnityEngine;

public class TransformUtils : MonoBehaviour
{
	private struct NumberedTransform
	{
		public Transform Transform;

		public int Number;

		public NumberedTransform(Transform inputTransform, int transformNumber)
		{
			Transform = inputTransform;
			Number = transformNumber;
		}

		public override string ToString()
		{
			return string.Format("{0}. {1}", Number, Transform.name);
		}
	}

	private const string removedNamePrefix = "Removed_";

	private const string removedParentName = "_Removed";

	private const float hideDistance = 100000f;

	private const float hideCheckDistance = -50000f;

	private static Transform removedParent;

	public static void Freeze(GameObject subject)
	{
		if (subject != null)
		{
			FreezeTransform(subject.transform, true);
		}
	}

	public static void Freeze(GameObject subject, bool freezeTranslation, bool freezeRoation, bool freezeScaling)
	{
		if (subject != null)
		{
			FreezeTransform(subject.transform, true, freezeTranslation, freezeRoation, freezeScaling);
		}
	}

	public static void Freeze(Transform subject)
	{
		FreezeTransform(subject, false);
	}

	public static void Freeze(Transform subject, bool freezeTranslation, bool freezeRoation, bool freezeScaling)
	{
		FreezeTransform(subject.transform, false, freezeTranslation, freezeRoation, freezeScaling);
	}

	private static void FreezeTransform(Transform subject, bool preVerified)
	{
		FreezeTransform(subject, preVerified, true, true, false);
	}

	private static void FreezeTransform(Transform subject, bool preVerified, bool freezeTranslation, bool freezeRotation, bool freezeScaling)
	{
		if (preVerified || subject != null)
		{
			if (freezeTranslation)
			{
				subject.localPosition = Vector3.zero;
			}
			if (freezeRotation)
			{
				subject.localEulerAngles = Vector3.zero;
			}
			if (freezeScaling)
			{
				subject.localScale = Vector3.one;
			}
		}
	}

	public static void Align(GameObject subject, GameObject target)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(GameObject subject, Component target)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(GameObject subject, Transform target)
	{
		Align(subject.transform, target, Vector3.zero, 0f, false);
	}

	public static void Align(Component subject, GameObject target)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(Component subject, Component target)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(Component subject, Transform target)
	{
		Align(subject.transform, target, Vector3.zero, 0f, false);
	}

	public static void Align(Transform subject, GameObject target)
	{
		Align(subject, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(Transform subject, Component target)
	{
		Align(subject, target.transform, Vector3.zero, 0f, false);
	}

	public static void Align(Transform subject, Transform target)
	{
		Align(subject, target, Vector3.zero, 0f, false);
	}

	public static void Align(GameObject subject, GameObject target, Vector3 alignOnly)
	{
		Align(subject.transform, target.transform, alignOnly, 0f, false);
	}

	public static void Align(GameObject subject, Component target, Vector3 alignOnly)
	{
		Align(subject.transform, target.transform, alignOnly, 0f, false);
	}

	public static void Align(GameObject subject, Transform target, Vector3 alignOnly)
	{
		Align(subject.transform, target, alignOnly, 0f, false);
	}

	public static void Align(Component subject, GameObject target, Vector3 alignOnly)
	{
		Align(subject.transform, target.transform, alignOnly, 0f, false);
	}

	public static void Align(Component subject, Component target, Vector3 alignOnly)
	{
		Align(subject.transform, target.transform, alignOnly, 0f, false);
	}

	public static void Align(Component subject, Transform target, Vector3 alignOnly)
	{
		Align(subject.transform, target, alignOnly, 0f, false);
	}

	public static void Align(Transform subject, GameObject target, Vector3 alignOnly)
	{
		Align(subject, target.transform, alignOnly, 0f, false);
	}

	public static void Align(Transform subject, Component target, Vector3 alignOnly)
	{
		Align(subject, target.transform, alignOnly, 0f, false);
	}

	public static void Align(Transform subject, Transform target, Vector3 alignOnly)
	{
		Align(subject, target, alignOnly, 0f, false);
	}

	public static void Align(GameObject subject, GameObject target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target.transform, alignOnly, offset, false);
	}

	public static void Align(GameObject subject, Component target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target.transform, alignOnly, offset, false);
	}

	public static void Align(GameObject subject, Transform target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target, alignOnly, offset, false);
	}

	public static void Align(Component subject, GameObject target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target.transform, alignOnly, offset, false);
	}

	public static void Align(Component subject, Component target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target.transform, alignOnly, offset, false);
	}

	public static void Align(Component subject, Transform target, Vector3 alignOnly, float offset)
	{
		Align(subject.transform, target, alignOnly, offset, false);
	}

	public static void Align(Transform subject, GameObject target, Vector3 alignOnly, float offset)
	{
		Align(subject, target.transform, alignOnly, offset, false);
	}

	public static void Align(Transform subject, Component target, Vector3 alignOnly, float offset)
	{
		Align(subject, target.transform, alignOnly, offset, false);
	}

	public static void Align(Transform subject, Transform target, Vector3 alignOnly, float offset)
	{
		Align(subject, target, alignOnly, offset, false);
	}

	public static void Align(GameObject subject, GameObject target, bool leaveParented)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(GameObject subject, Component target, bool leaveParented)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(GameObject subject, Transform target, bool leaveParented)
	{
		Align(subject.transform, target, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Component subject, GameObject target, bool leaveParented)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Component subject, Component target, bool leaveParented)
	{
		Align(subject.transform, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Component subject, Transform target, bool leaveParented)
	{
		Align(subject.transform, target, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Transform subject, GameObject target, bool leaveParented)
	{
		Align(subject, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Transform subject, Component target, bool leaveParented)
	{
		Align(subject, target.transform, Vector3.zero, 0f, leaveParented);
	}

	public static void Align(Transform subject, Transform target, Vector3 alignOnly, float offset, bool leaveParented)
	{
		if (alignOnly != Vector3.zero)
		{
			if (alignOnly == Vector3.left || alignOnly == Vector3.right)
			{
				SetX(subject, target.position.x + offset);
			}
			else if (alignOnly == Vector3.up || alignOnly == Vector3.down)
			{
				SetY(subject, target.position.y + offset);
			}
			else if (alignOnly == Vector3.forward || alignOnly == Vector3.back)
			{
				SetZ(subject, target.position.z + offset);
			}
			else
			{
				ErrorStrings.ValueUnexpected(alignOnly, "alignOnly Vector");
			}
		}
		else
		{
			Transform parent = subject.parent;
			subject.parent = target.transform;
			subject.localPosition = Vector3.zero;
			subject.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			if (!leaveParented)
			{
				subject.parent = parent;
			}
		}
	}

	public static void SetX(GameObject subject, float newX)
	{
		if (subject != null)
		{
			SetX(subject.transform, newX, false);
		}
	}

	public static void SetX(GameObject subject, float newX, bool local)
	{
		if (subject != null)
		{
			SetX(subject.transform, newX, local);
		}
	}

	public static void SetX(Component subject, float newX)
	{
		if (subject != null)
		{
			SetX(subject.transform, newX, false);
		}
	}

	public static void SetX(Component subject, float newX, bool local)
	{
		if (subject != null)
		{
			SetX(subject.transform, newX, local);
		}
	}

	public static void SetX(Transform subject, float newX)
	{
		if (subject != null)
		{
			SetX(subject, newX, false);
		}
	}

	public static void SetX(Transform subject, float newX, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(newX, subject.localPosition.y, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(newX, subject.position.y, subject.position.z);
		}
	}

	public static void SetY(GameObject subject, float newY)
	{
		if (subject != null)
		{
			SetY(subject.transform, newY, false);
		}
	}

	public static void SetY(GameObject subject, float newY, bool local)
	{
		if (subject != null)
		{
			SetY(subject.transform, newY, local);
		}
	}

	public static void SetY(Component subject, float newY)
	{
		if (subject != null)
		{
			SetY(subject.transform, newY, false);
		}
	}

	public static void SetY(Component subject, float newY, bool local)
	{
		if (subject != null)
		{
			SetY(subject.transform, newY, local);
		}
	}

	public static void SetY(Transform subject, float newY)
	{
		if (subject != null)
		{
			SetY(subject, newY, false);
		}
	}

	public static void SetY(Transform subject, float newY, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, newY, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, newY, subject.position.z);
		}
	}

	public static void SetZ(GameObject subject, float newZ)
	{
		if (subject != null)
		{
			SetZ(subject.transform, newZ, false);
		}
	}

	public static void SetZ(GameObject subject, float newZ, bool local)
	{
		if (subject != null)
		{
			SetZ(subject.transform, newZ, local);
		}
	}

	public static void SetZ(Component subject, float newZ)
	{
		if (subject != null)
		{
			SetZ(subject.transform, newZ, false);
		}
	}

	public static void SetZ(Component subject, float newZ, bool local)
	{
		if (subject != null)
		{
			SetZ(subject.transform, newZ, local);
		}
	}

	public static void SetZ(Transform subject, float newZ)
	{
		if (subject != null)
		{
			SetZ(subject, newZ, false);
		}
	}

	public static void SetZ(Transform subject, float newZ, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, subject.localPosition.y, newZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, subject.position.y, newZ);
		}
	}

	public static void SetXY(GameObject subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, false);
		}
	}

	public static void SetXY(GameObject subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, local);
		}
	}

	public static void SetXY(GameObject subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, false);
		}
	}

	public static void SetXY(GameObject subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, local);
		}
	}

	public static void SetXY(GameObject subject, float newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, newXY, false);
		}
	}

	public static void SetXY(GameObject subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, newXY, local);
		}
	}

	public static void SetXY(GameObject subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newX, newY, false);
		}
	}

	public static void SetXY(GameObject subject, float newX, float newY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newX, newY, local);
		}
	}

	public static void SetXY(Component subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, false);
		}
	}

	public static void SetXY(Component subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, local);
		}
	}

	public static void SetXY(Component subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, false);
		}
	}

	public static void SetXY(Component subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, local);
		}
	}

	public static void SetXY(Component subject, float newXY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, newXY, false);
		}
	}

	public static void SetXY(Component subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newXY, newXY, local);
		}
	}

	public static void SetXY(Component subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newX, newY, false);
		}
	}

	public static void SetXY(Component subject, float newX, float newY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject.transform, newX, newY, local);
		}
	}

	public static void SetXY(Transform subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetXY(subject, newXY.x, newXY.y, false);
		}
	}

	public static void SetXY(Transform subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject, newXY.x, newXY.y, local);
		}
	}

	public static void SetXY(Transform subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetXY(subject, newXY.y, newXY.z, false);
		}
	}

	public static void SetXY(Transform subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject, newXY.y, newXY.z, local);
		}
	}

	public static void SetXY(Transform subject, float newXY)
	{
		if (subject != null)
		{
			SetXY(subject, newXY, newXY, false);
		}
	}

	public static void SetXY(Transform subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetXY(subject, newXY, newXY, local);
		}
	}

	public static void SetXY(Transform subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetXY(subject, newX, newY, false);
		}
	}

	public static void SetXY(Transform subject, float newX, float newY, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(newX, newY, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(newX, newY, subject.position.z);
		}
	}

	public static void SetXZ(GameObject subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetXZ(GameObject subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetXZ(GameObject subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetXZ(GameObject subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetXZ(GameObject subject, float newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, newXZ, false);
		}
	}

	public static void SetXZ(GameObject subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, newXZ, local);
		}
	}

	public static void SetXZ(GameObject subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newX, newZ, false);
		}
	}

	public static void SetXZ(GameObject subject, float newX, float newZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newX, newZ, local);
		}
	}

	public static void SetXZ(Component subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetXZ(Component subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetXZ(Component subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetXZ(Component subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetXZ(Component subject, float newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, newXZ, false);
		}
	}

	public static void SetXZ(Component subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newXZ, newXZ, local);
		}
	}

	public static void SetXZ(Component subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newX, newZ, false);
		}
	}

	public static void SetXZ(Component subject, float newX, float newZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject.transform, newX, newZ, local);
		}
	}

	public static void SetXZ(Transform subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ.x, newXZ.y, false);
		}
	}

	public static void SetXZ(Transform subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ.x, newXZ.y, local);
		}
	}

	public static void SetXZ(Transform subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ.y, newXZ.z, false);
		}
	}

	public static void SetXZ(Transform subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ.y, newXZ.z, local);
		}
	}

	public static void SetXZ(Transform subject, float newXZ)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ, newXZ, false);
		}
	}

	public static void SetXZ(Transform subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetXZ(subject, newXZ, newXZ, local);
		}
	}

	public static void SetXZ(Transform subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetXZ(subject, newX, newZ, false);
		}
	}

	public static void SetXZ(Transform subject, float newX, float newZ, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(newX, subject.localPosition.y, newZ);
		}
		else
		{
			subject.position = new Vector3(newX, subject.position.y, newZ);
		}
	}

	public static void SetYZ(GameObject subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetYZ(GameObject subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetYZ(GameObject subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetYZ(GameObject subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetYZ(GameObject subject, float newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, newYZ, false);
		}
	}

	public static void SetYZ(GameObject subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, newYZ, local);
		}
	}

	public static void SetYZ(GameObject subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newY, newZ, false);
		}
	}

	public static void SetYZ(GameObject subject, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newY, newZ, local);
		}
	}

	public static void SetYZ(Component subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetYZ(Component subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetYZ(Component subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetYZ(Component subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetYZ(Component subject, float newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, newYZ, false);
		}
	}

	public static void SetYZ(Component subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newYZ, newYZ, local);
		}
	}

	public static void SetYZ(Component subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newY, newZ, false);
		}
	}

	public static void SetYZ(Component subject, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject.transform, newY, newZ, local);
		}
	}

	public static void SetYZ(Transform subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ.x, newYZ.y, false);
		}
	}

	public static void SetYZ(Transform subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ.x, newYZ.y, local);
		}
	}

	public static void SetYZ(Transform subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ.y, newYZ.z, false);
		}
	}

	public static void SetYZ(Transform subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ.y, newYZ.z, local);
		}
	}

	public static void SetYZ(Transform subject, float newYZ)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ, newYZ, false);
		}
	}

	public static void SetYZ(Transform subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetYZ(subject, newYZ, newYZ, local);
		}
	}

	public static void SetYZ(Transform subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetYZ(subject, newY, newZ, false);
		}
	}

	public static void SetYZ(Transform subject, float newY, float newZ, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, newY, newZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, newY, newZ);
		}
	}

	public static void Set(GameObject subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, false);
		}
	}

	public static void Set(GameObject subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, local);
		}
	}

	public static void Set(GameObject subject, float newXYZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void Set(GameObject subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void Set(GameObject subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newX, newY, newZ, false);
		}
	}

	public static void Set(GameObject subject, float newX, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newX, newY, newZ, local);
		}
	}

	public static void Set(Component subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, false);
		}
	}

	public static void Set(Component subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, local);
		}
	}

	public static void Set(Component subject, float newXYZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void Set(Component subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void Set(Component subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			Set(subject.transform, newX, newY, newZ, false);
		}
	}

	public static void Set(Component subject, float newX, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			Set(subject.transform, newX, newY, newZ, local);
		}
	}

	public static void Set(Transform subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			Set(subject, newXYZ.x, newXYZ.y, newXYZ.z, false);
		}
	}

	public static void Set(Transform subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject, newXYZ.x, newXYZ.y, newXYZ.z, local);
		}
	}

	public static void Set(Transform subject, float newXYZ)
	{
		if (subject != null)
		{
			Set(subject, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void Set(Transform subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			Set(subject, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void Set(Transform subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			Set(subject, newX, newY, newZ, false);
		}
	}

	public static void Set(Transform subject, float newX, float newY, float newZ, bool local)
	{
		if (local)
		{
			subject.localPosition = new Vector3(newX, newY, newZ);
		}
		else
		{
			subject.position = new Vector3(newX, newY, newZ);
		}
	}

	public static Vector3 GetSetX(Vector3 oldVector, float newX)
	{
		return new Vector3(newX, oldVector.y, oldVector.z);
	}

	public static Vector3 GetSetY(Vector3 oldVector, float newY)
	{
		return new Vector3(oldVector.x, newY, oldVector.z);
	}

	public static Vector3 GetSetZ(Vector3 oldVector, float newZ)
	{
		return new Vector3(oldVector.x, oldVector.y, newZ);
	}

	public static Vector3 GetSetXY(Vector3 oldVector, float newX, float newY)
	{
		return new Vector3(newX, newY, oldVector.z);
	}

	public static Vector3 GetSetXY(Vector3 oldVector, Vector2 newXY)
	{
		return new Vector3(newXY.x, newXY.y, oldVector.z);
	}

	public static Vector3 GetSetXY(Vector3 oldVector, Vector3 newXY)
	{
		return new Vector3(newXY.x, newXY.y, oldVector.z);
	}

	public static Vector3 GetSetXZ(Vector3 oldVector, float newX, float newZ)
	{
		return new Vector3(newX, oldVector.y, newZ);
	}

	public static Vector3 GetSetXZ(Vector3 oldVector, Vector2 newXZ)
	{
		return new Vector3(newXZ.x, oldVector.y, newXZ.y);
	}

	public static Vector3 GetSetXZ(Vector3 oldVector, Vector3 newXZ)
	{
		return new Vector3(newXZ.x, oldVector.y, newXZ.z);
	}

	public static Vector3 GetSetYZ(Vector3 oldVector, float newY, float newZ)
	{
		return new Vector3(oldVector.x, newY, newZ);
	}

	public static Vector3 GetSetYZ(Vector3 oldVector, Vector2 newYZ)
	{
		return new Vector3(oldVector.x, newYZ.x, newYZ.y);
	}

	public static Vector3 GetSetYZ(Vector3 oldVector, Vector3 newYZ)
	{
		return new Vector3(oldVector.x, newYZ.y, newYZ.z);
	}

	public static void MoveX(GameObject subject, float moveByX)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, false, false);
		}
	}

	public static void MoveX(GameObject subject, float moveByX, bool local)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, local, false);
		}
	}

	public static void MoveX(GameObject subject, float moveByX, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, local, reverse);
		}
	}

	public static void MoveX(Component subject, float moveByX)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, false, false);
		}
	}

	public static void MoveX(Component subject, float moveByX, bool local)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, local, false);
		}
	}

	public static void MoveX(Component subject, float moveByX, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveX(subject.transform, moveByX, local, reverse);
		}
	}

	public static void MoveX(Transform subject, float moveByX)
	{
		if (subject != null)
		{
			MoveX(subject, moveByX, false, false);
		}
	}

	public static void MoveX(Transform subject, float moveByX, bool local)
	{
		if (subject != null)
		{
			MoveX(subject, moveByX, local, false);
		}
	}

	public static void MoveX(Transform subject, float moveByX, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByX *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x + moveByX, subject.localPosition.y, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(subject.position.x + moveByX, subject.position.y, subject.position.z);
		}
	}

	public static void MoveY(GameObject subject, float moveByY)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, false, false);
		}
	}

	public static void MoveY(GameObject subject, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, local, false);
		}
	}

	public static void MoveY(GameObject subject, float moveByY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, local, reverse);
		}
	}

	public static void MoveY(Component subject, float moveByY)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, false, false);
		}
	}

	public static void MoveY(Component subject, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, local, false);
		}
	}

	public static void MoveY(Component subject, float moveByY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveY(subject.transform, moveByY, local, reverse);
		}
	}

	public static void MoveY(Transform subject, float moveByY)
	{
		if (subject != null)
		{
			MoveY(subject, moveByY, false, false);
		}
	}

	public static void MoveY(Transform subject, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveY(subject, moveByY, local, false);
		}
	}

	public static void MoveY(Transform subject, float moveByY, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByY *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, subject.localPosition.y + moveByY, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, subject.position.y + moveByY, subject.position.z);
		}
	}

	public static void MoveZ(GameObject subject, float moveByZ)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, false, false);
		}
	}

	public static void MoveZ(GameObject subject, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, local, false);
		}
	}

	public static void MoveZ(GameObject subject, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, local, reverse);
		}
	}

	public static void MoveZ(Component subject, float moveByZ)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, false, false);
		}
	}

	public static void MoveZ(Component subject, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, local, false);
		}
	}

	public static void MoveZ(Component subject, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveZ(subject.transform, moveByZ, local, reverse);
		}
	}

	public static void MoveZ(Transform subject, float moveByZ)
	{
		if (subject != null)
		{
			MoveZ(subject, moveByZ, false, false);
		}
	}

	public static void MoveZ(Transform subject, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveZ(subject, moveByZ, local, false);
		}
	}

	public static void MoveZ(Transform subject, float moveByZ, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByZ *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, subject.localPosition.y, subject.localPosition.z + moveByZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, subject.position.y, subject.position.z + moveByZ);
		}
	}

	public static void MoveXY(GameObject subject, Vector2 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, false, false);
		}
	}

	public static void MoveXY(GameObject subject, Vector2 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, false);
		}
	}

	public static void MoveXY(GameObject subject, Vector2 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(GameObject subject, Vector3 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, false, false);
		}
	}

	public static void MoveXY(GameObject subject, Vector3 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, false);
		}
	}

	public static void MoveXY(GameObject subject, Vector3 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(GameObject subject, float moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, false, false);
		}
	}

	public static void MoveXY(GameObject subject, float moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, local, false);
		}
	}

	public static void MoveXY(GameObject subject, float moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(GameObject subject, float moveByX, float moveByY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, false, false);
		}
	}

	public static void MoveXY(GameObject subject, float moveByX, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, local, false);
		}
	}

	public static void MoveXY(GameObject subject, float moveByX, float moveByY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, local, reverse);
		}
	}

	public static void MoveXY(Component subject, Vector2 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, false, false);
		}
	}

	public static void MoveXY(Component subject, Vector2 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, false);
		}
	}

	public static void MoveXY(Component subject, Vector2 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(Component subject, Vector3 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, false, false);
		}
	}

	public static void MoveXY(Component subject, Vector3 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, false);
		}
	}

	public static void MoveXY(Component subject, Vector3 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(Component subject, float moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, false, false);
		}
	}

	public static void MoveXY(Component subject, float moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, local, false);
		}
	}

	public static void MoveXY(Component subject, float moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByXY, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(Component subject, float moveByX, float moveByY)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, false, false);
		}
	}

	public static void MoveXY(Component subject, float moveByX, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, local, false);
		}
	}

	public static void MoveXY(Component subject, float moveByX, float moveByY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject.transform, moveByX, moveByY, local, reverse);
		}
	}

	public static void MoveXY(Transform subject, Vector2 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, false, false);
		}
	}

	public static void MoveXY(Transform subject, Vector2 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, local, false);
		}
	}

	public static void MoveXY(Transform subject, Vector2 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, local, reverse);
		}
	}

	public static void MoveXY(Transform subject, Vector3 moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, false, false);
		}
	}

	public static void MoveXY(Transform subject, Vector3 moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, local, false);
		}
	}

	public static void MoveXY(Transform subject, Vector3 moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY.x, moveByXY.y, local, reverse);
		}
	}

	public static void MoveXY(Transform subject, float moveByXY)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY, moveByXY, false, false);
		}
	}

	public static void MoveXY(Transform subject, float moveByXY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY, moveByXY, local, false);
		}
	}

	public static void MoveXY(Transform subject, float moveByXY, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByXY, moveByXY, local, reverse);
		}
	}

	public static void MoveXY(Transform subject, float moveByX, float moveByY)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByX, moveByY, false, false);
		}
	}

	public static void MoveXY(Transform subject, float moveByX, float moveByY, bool local)
	{
		if (subject != null)
		{
			MoveXY(subject, moveByX, moveByY, local, false);
		}
	}

	public static void MoveXY(Transform subject, float moveByX, float moveByY, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByX *= -1f;
			moveByY *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x + moveByX, subject.localPosition.y + moveByY, subject.localPosition.z);
		}
		else
		{
			subject.position = new Vector3(subject.position.x + moveByX, subject.position.y + moveByY, subject.position.z);
		}
	}

	public static void MoveXZ(GameObject subject, Vector2 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(GameObject subject, Vector2 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(GameObject subject, Vector2 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(GameObject subject, Vector3 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(GameObject subject, Vector3 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(GameObject subject, Vector3 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByX, float moveByZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, false, false);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByX, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, local, false);
		}
	}

	public static void MoveXZ(GameObject subject, float moveByX, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, local, reverse);
		}
	}

	public static void MoveXZ(Component subject, Vector2 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(Component subject, Vector2 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(Component subject, Vector2 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(Component subject, Vector3 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(Component subject, Vector3 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(Component subject, Vector3 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(Component subject, float moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(Component subject, float moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(Component subject, float moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByXZ, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(Component subject, float moveByX, float moveByZ)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, false, false);
		}
	}

	public static void MoveXZ(Component subject, float moveByX, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, local, false);
		}
	}

	public static void MoveXZ(Component subject, float moveByX, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject.transform, moveByX, moveByZ, local, reverse);
		}
	}

	public static void MoveXZ(Transform subject, Vector2 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.y, false, false);
		}
	}

	public static void MoveXZ(Transform subject, Vector2 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.y, local, false);
		}
	}

	public static void MoveXZ(Transform subject, Vector2 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.y, local, reverse);
		}
	}

	public static void MoveXZ(Transform subject, Vector3 moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.z, false, false);
		}
	}

	public static void MoveXZ(Transform subject, Vector3 moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.z, local, false);
		}
	}

	public static void MoveXZ(Transform subject, Vector3 moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ.x, moveByXZ.z, local, reverse);
		}
	}

	public static void MoveXZ(Transform subject, float moveByXZ)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ, moveByXZ, false, false);
		}
	}

	public static void MoveXZ(Transform subject, float moveByXZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ, moveByXZ, local, false);
		}
	}

	public static void MoveXZ(Transform subject, float moveByXZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByXZ, moveByXZ, local, reverse);
		}
	}

	public static void MoveXZ(Transform subject, float moveByX, float moveByZ)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByX, moveByZ, false, false);
		}
	}

	public static void MoveXZ(Transform subject, float moveByX, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveXZ(subject, moveByX, moveByZ, local, false);
		}
	}

	public static void MoveXZ(Transform subject, float moveByX, float moveByZ, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByX *= -1f;
			moveByZ *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x + moveByX, subject.localPosition.y, subject.localPosition.z + moveByZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x + moveByX, subject.position.y, subject.position.z + moveByZ);
		}
	}

	public static void MoveYZ(GameObject subject, Vector2 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(GameObject subject, Vector2 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(GameObject subject, Vector2 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(GameObject subject, Vector3 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(GameObject subject, Vector3 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(GameObject subject, Vector3 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, false, false);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, local, false);
		}
	}

	public static void MoveYZ(GameObject subject, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, local, reverse);
		}
	}

	public static void MoveYZ(Component subject, Vector2 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(Component subject, Vector2 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(Component subject, Vector2 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(Component subject, Vector3 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(Component subject, Vector3 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(Component subject, Vector3 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(Component subject, float moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(Component subject, float moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(Component subject, float moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByYZ, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(Component subject, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, false, false);
		}
	}

	public static void MoveYZ(Component subject, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, local, false);
		}
	}

	public static void MoveYZ(Component subject, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject.transform, moveByY, moveByZ, local, reverse);
		}
	}

	public static void MoveYZ(Transform subject, Vector2 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.x, moveByYZ.y, false, false);
		}
	}

	public static void MoveYZ(Transform subject, Vector2 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.x, moveByYZ.y, local, false);
		}
	}

	public static void MoveYZ(Transform subject, Vector2 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.x, moveByYZ.y, local, reverse);
		}
	}

	public static void MoveYZ(Transform subject, Vector3 moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.y, moveByYZ.z, false, false);
		}
	}

	public static void MoveYZ(Transform subject, Vector3 moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.y, moveByYZ.z, local, false);
		}
	}

	public static void MoveYZ(Transform subject, Vector3 moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ.y, moveByYZ.z, local, reverse);
		}
	}

	public static void MoveYZ(Transform subject, float moveByYZ)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ, moveByYZ, false, false);
		}
	}

	public static void MoveYZ(Transform subject, float moveByYZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ, moveByYZ, local, false);
		}
	}

	public static void MoveYZ(Transform subject, float moveByYZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByYZ, moveByYZ, local, reverse);
		}
	}

	public static void MoveYZ(Transform subject, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByY, moveByZ, false, false);
		}
	}

	public static void MoveYZ(Transform subject, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			MoveYZ(subject, moveByY, moveByZ, local, false);
		}
	}

	public static void MoveYZ(Transform subject, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByY *= -1f;
			moveByZ *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x, subject.localPosition.y + moveByY, subject.localPosition.z + moveByZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x, subject.position.y + moveByY, subject.position.z + moveByZ);
		}
	}

	public static void Move(GameObject subject, Vector3 moveBy)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, false, false);
		}
	}

	public static void Move(GameObject subject, Vector3 moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, local, false);
		}
	}

	public static void Move(GameObject subject, Vector3 moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, local, reverse);
		}
	}

	public static void Move(GameObject subject, float moveBy)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, false, false);
		}
	}

	public static void Move(GameObject subject, float moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, local, false);
		}
	}

	public static void Move(GameObject subject, float moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, local, reverse);
		}
	}

	public static void Move(GameObject subject, float moveByX, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, false, false);
		}
	}

	public static void Move(GameObject subject, float moveByX, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, local, false);
		}
	}

	public static void Move(GameObject subject, float moveByX, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, local, reverse);
		}
	}

	public static void Move(Component subject, Vector3 moveBy)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, false, false);
		}
	}

	public static void Move(Component subject, Vector3 moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, local, false);
		}
	}

	public static void Move(Component subject, Vector3 moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, local, reverse);
		}
	}

	public static void Move(Component subject, float moveBy)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, false, false);
		}
	}

	public static void Move(Component subject, float moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, local, false);
		}
	}

	public static void Move(Component subject, float moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveBy, moveBy, moveBy, local, reverse);
		}
	}

	public static void Move(Component subject, float moveByX, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, false, false);
		}
	}

	public static void Move(Component subject, float moveByX, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, local, false);
		}
	}

	public static void Move(Component subject, float moveByX, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject.transform, moveByX, moveByY, moveByZ, local, reverse);
		}
	}

	public static void Move(Transform subject, Vector3 moveBy)
	{
		if (subject != null)
		{
			Move(subject, moveBy.x, moveBy.y, moveBy.z, false, false);
		}
	}

	public static void Move(Transform subject, Vector3 moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject, moveBy.x, moveBy.y, moveBy.z, local, false);
		}
	}

	public static void Move(Transform subject, Vector3 moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject, moveBy.x, moveBy.y, moveBy.z, local, reverse);
		}
	}

	public static void Move(Transform subject, float moveBy)
	{
		if (subject != null)
		{
			Move(subject, moveBy, moveBy, moveBy, false, false);
		}
	}

	public static void Move(Transform subject, float moveBy, bool local)
	{
		if (subject != null)
		{
			Move(subject, moveBy, moveBy, moveBy, local, false);
		}
	}

	public static void Move(Transform subject, float moveBy, bool local, bool reverse)
	{
		if (subject != null)
		{
			Move(subject, moveBy, moveBy, moveBy, local, reverse);
		}
	}

	public static void Move(Transform subject, float moveByX, float moveByY, float moveByZ)
	{
		if (subject != null)
		{
			Move(subject, moveByX, moveByY, moveByZ, false, false);
		}
	}

	public static void Move(Transform subject, float moveByX, float moveByY, float moveByZ, bool local)
	{
		if (subject != null)
		{
			Move(subject, moveByX, moveByY, moveByZ, local, false);
		}
	}

	public static void Move(Transform subject, float moveByX, float moveByY, float moveByZ, bool local, bool reverse)
	{
		if (reverse)
		{
			moveByX *= -1f;
			moveByY *= -1f;
			moveByZ *= -1f;
		}
		if (local)
		{
			subject.localPosition = new Vector3(subject.localPosition.x + moveByX, subject.localPosition.y + moveByY, subject.localPosition.z + moveByZ);
		}
		else
		{
			subject.position = new Vector3(subject.position.x + moveByX, subject.position.y + moveByY, subject.position.z + moveByZ);
		}
	}

	public static void SpeedX(GameObject subject, float velocityX)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime);
	}

	public static void SpeedX(GameObject subject, float velocityX, bool local)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local);
	}

	public static void SpeedX(GameObject subject, float velocityX, bool local, bool reverse)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedX(Component subject, float velocityX)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime);
	}

	public static void SpeedX(Component subject, float velocityX, bool local)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local);
	}

	public static void SpeedX(Component subject, float velocityX, bool local, bool reverse)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedX(Transform subject, float velocityX)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime);
	}

	public static void SpeedX(Transform subject, float velocityX, bool local)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local);
	}

	public static void SpeedX(Transform subject, float velocityX, bool local, bool reverse)
	{
		MoveX(subject, velocityX * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedY(GameObject subject, float velocityY)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedY(GameObject subject, float velocityY, bool local)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedY(GameObject subject, float velocityY, bool local, bool reverse)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedY(Component subject, float velocityY)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedY(Component subject, float velocityY, bool local)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedY(Component subject, float velocityY, bool local, bool reverse)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedY(Transform subject, float velocityY)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedY(Transform subject, float velocityY, bool local)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedY(Transform subject, float velocityY, bool local, bool reverse)
	{
		MoveY(subject, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedZ(GameObject subject, float velocityZ)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedZ(GameObject subject, float velocityZ, bool local)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedZ(GameObject subject, float velocityZ, bool local, bool reverse)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedZ(Component subject, float velocityZ)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedZ(Component subject, float velocityZ, bool local)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedZ(Component subject, float velocityZ, bool local, bool reverse)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedZ(Transform subject, float velocityZ)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedZ(Transform subject, float velocityZ, bool local)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedZ(Transform subject, float velocityZ, bool local, bool reverse)
	{
		MoveZ(subject, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(GameObject subject, Vector2 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(GameObject subject, Vector2 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(GameObject subject, Vector2 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(GameObject subject, Vector3 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(GameObject subject, Vector3 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(GameObject subject, Vector3 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(GameObject subject, float velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(GameObject subject, float velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(GameObject subject, float velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(GameObject subject, float velocityX, float velocityY)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(GameObject subject, float velocityX, float velocityY, bool local)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(GameObject subject, float velocityX, float velocityY, bool local, bool reverse)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Component subject, Vector2 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Component subject, Vector2 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Component subject, Vector2 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Component subject, Vector3 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Component subject, Vector3 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Component subject, Vector3 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Component subject, float velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Component subject, float velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Component subject, float velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Component subject, float velocityX, float velocityY)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Component subject, float velocityX, float velocityY, bool local)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Component subject, float velocityX, float velocityY, bool local, bool reverse)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Transform subject, Vector2 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Transform subject, Vector2 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Transform subject, Vector2 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Transform subject, Vector3 velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Transform subject, Vector3 velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Transform subject, Vector3 velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Transform subject, float velocityXY)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Transform subject, float velocityXY, bool local)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Transform subject, float velocityXY, bool local, bool reverse)
	{
		MoveXY(subject, velocityXY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXY(Transform subject, float velocityX, float velocityY)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime);
	}

	public static void SpeedXY(Transform subject, float velocityX, float velocityY, bool local)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local);
	}

	public static void SpeedXY(Transform subject, float velocityX, float velocityY, bool local, bool reverse)
	{
		MoveXY(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(GameObject subject, Vector2 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(GameObject subject, Vector2 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(GameObject subject, Vector2 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(GameObject subject, Vector3 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(GameObject subject, Vector3 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(GameObject subject, Vector3 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(GameObject subject, float velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(GameObject subject, float velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(GameObject subject, float velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(GameObject subject, float velocityX, float velocityZ)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(GameObject subject, float velocityX, float velocityZ, bool local)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(GameObject subject, float velocityX, float velocityZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Component subject, Vector2 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Component subject, Vector2 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Component subject, Vector2 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Component subject, Vector3 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Component subject, Vector3 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Component subject, Vector3 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Component subject, float velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Component subject, float velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Component subject, float velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Component subject, float velocityX, float velocityZ)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Component subject, float velocityX, float velocityZ, bool local)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Component subject, float velocityX, float velocityZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Transform subject, Vector2 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Transform subject, Vector2 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Transform subject, Vector2 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Transform subject, Vector3 velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Transform subject, Vector3 velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Transform subject, Vector3 velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Transform subject, float velocityXZ)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Transform subject, float velocityXZ, bool local)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Transform subject, float velocityXZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityXZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedXZ(Transform subject, float velocityX, float velocityZ)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedXZ(Transform subject, float velocityX, float velocityZ, bool local)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedXZ(Transform subject, float velocityX, float velocityZ, bool local, bool reverse)
	{
		MoveXZ(subject, velocityX * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(GameObject subject, Vector2 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(GameObject subject, Vector2 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(GameObject subject, Vector2 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(GameObject subject, Vector3 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(GameObject subject, Vector3 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(GameObject subject, Vector3 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(GameObject subject, float velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(GameObject subject, float velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(GameObject subject, float velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(GameObject subject, float velocityY, float velocityZ)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(GameObject subject, float velocityY, float velocityZ, bool local)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(GameObject subject, float velocityY, float velocityZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Component subject, Vector2 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Component subject, Vector2 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Component subject, Vector2 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Component subject, Vector3 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Component subject, Vector3 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Component subject, Vector3 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Component subject, float velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Component subject, float velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Component subject, float velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Component subject, float velocityY, float velocityZ)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Component subject, float velocityY, float velocityZ, bool local)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Component subject, float velocityY, float velocityZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Transform subject, Vector2 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Transform subject, Vector2 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Transform subject, Vector2 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Transform subject, Vector3 velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Transform subject, Vector3 velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Transform subject, Vector3 velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Transform subject, float velocityYZ)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Transform subject, float velocityYZ, bool local)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Transform subject, float velocityYZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityYZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SpeedYZ(Transform subject, float velocityY, float velocityZ)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void SpeedYZ(Transform subject, float velocityY, float velocityZ, bool local)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void SpeedYZ(Transform subject, float velocityY, float velocityZ, bool local, bool reverse)
	{
		MoveYZ(subject, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(GameObject subject, Vector3 velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(GameObject subject, Vector3 velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(GameObject subject, Vector3 velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(GameObject subject, float velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(GameObject subject, float velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(GameObject subject, float velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(GameObject subject, float velocityX, float velocityY, float velocityZ)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void Speed(GameObject subject, float velocityX, float velocityY, float velocityZ, bool local)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void Speed(GameObject subject, float velocityX, float velocityY, float velocityZ, bool local, bool reverse)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Component subject, Vector3 velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(Component subject, Vector3 velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(Component subject, Vector3 velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Component subject, float velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(Component subject, float velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(Component subject, float velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Component subject, float velocityX, float velocityY, float velocityZ)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void Speed(Component subject, float velocityX, float velocityY, float velocityZ, bool local)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void Speed(Component subject, float velocityX, float velocityY, float velocityZ, bool local, bool reverse)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Transform subject, Vector3 velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(Transform subject, Vector3 velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(Transform subject, Vector3 velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Transform subject, float velocity)
	{
		Move(subject, velocity * Time.smoothDeltaTime);
	}

	public static void Speed(Transform subject, float velocity, bool local)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local);
	}

	public static void Speed(Transform subject, float velocity, bool local, bool reverse)
	{
		Move(subject, velocity * Time.smoothDeltaTime, local, reverse);
	}

	public static void Speed(Transform subject, float velocityX, float velocityY, float velocityZ)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime);
	}

	public static void Speed(Transform subject, float velocityX, float velocityY, float velocityZ, bool local)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local);
	}

	public static void Speed(Transform subject, float velocityX, float velocityY, float velocityZ, bool local, bool reverse)
	{
		Move(subject, velocityX * Time.smoothDeltaTime, velocityY * Time.smoothDeltaTime, velocityZ * Time.smoothDeltaTime, local, reverse);
	}

	public static void SetScaleX(GameObject subject, float newScaleX)
	{
		if (subject != null)
		{
			SetScaleX(subject.transform, newScaleX, false);
		}
	}

	public static void SetScaleX(GameObject subject, float newScaleX, bool reverse)
	{
		if (subject != null)
		{
			SetScaleX(subject.transform, newScaleX, reverse);
		}
	}

	public static void SetScaleX(Component subject, float newScaleX)
	{
		if (subject != null)
		{
			SetScaleX(subject.transform, newScaleX, false);
		}
	}

	public static void SetScaleX(Component subject, float newScaleX, bool reverse)
	{
		if (subject != null)
		{
			SetScaleX(subject.transform, newScaleX, reverse);
		}
	}

	public static void SetScaleX(Transform subject, float newScaleX)
	{
		if (subject != null)
		{
			SetScaleX(subject, newScaleX, false);
		}
	}

	public static void SetScaleX(Transform subject, float newScaleX, bool reverse)
	{
		if (reverse)
		{
			newScaleX *= -1f;
		}
		subject.localScale = new Vector3(newScaleX, subject.localScale.y, subject.localScale.z);
	}

	public static void SetScaleY(GameObject subject, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleY(subject.transform, newScaleY, false);
		}
	}

	public static void SetScaleY(GameObject subject, float newScaleY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleY(subject.transform, newScaleY, reverse);
		}
	}

	public static void SetScaleY(Component subject, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleY(subject.transform, newScaleY, false);
		}
	}

	public static void SetScaleY(Component subject, float newScaleY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleY(subject.transform, newScaleY, reverse);
		}
	}

	public static void SetScaleY(Transform subject, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleY(subject, newScaleY, false);
		}
	}

	public static void SetScaleY(Transform subject, float newScaleY, bool reverse)
	{
		if (reverse)
		{
			newScaleY *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, newScaleY, subject.localScale.z);
	}

	public static void SetScaleZ(GameObject subject, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleZ(subject.transform, newScaleZ, false);
		}
	}

	public static void SetScaleZ(GameObject subject, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleZ(subject.transform, newScaleZ, reverse);
		}
	}

	public static void SetScaleZ(Component subject, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleZ(subject.transform, newScaleZ, false);
		}
	}

	public static void SetScaleZ(Component subject, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleZ(subject.transform, newScaleZ, reverse);
		}
	}

	public static void SetScaleZ(Transform subject, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleZ(subject, newScaleZ, false);
		}
	}

	public static void SetScaleZ(Transform subject, float newScaleZ, bool reverse)
	{
		if (reverse)
		{
			newScaleZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, subject.localScale.y, newScaleZ);
	}

	public static void SetScaleXY(GameObject subject, Vector2 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, false);
		}
	}

	public static void SetScaleXY(GameObject subject, Vector2 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(GameObject subject, Vector3 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, false);
		}
	}

	public static void SetScaleXY(GameObject subject, Vector3 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(GameObject subject, float newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, newScaleXY, false);
		}
	}

	public static void SetScaleXY(GameObject subject, float newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(GameObject subject, float newScaleX, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleX, newScaleY, false);
		}
	}

	public static void SetScaleXY(GameObject subject, float newScaleX, float newScaleY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleX, newScaleY, reverse);
		}
	}

	public static void SetScaleXY(Component subject, Vector2 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, false);
		}
	}

	public static void SetScaleXY(Component subject, Vector2 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(Component subject, Vector3 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, false);
		}
	}

	public static void SetScaleXY(Component subject, Vector3 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(Component subject, float newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, newScaleXY, false);
		}
	}

	public static void SetScaleXY(Component subject, float newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleXY, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(Component subject, float newScaleX, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleX, newScaleY, false);
		}
	}

	public static void SetScaleXY(Component subject, float newScaleX, float newScaleY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject.transform, newScaleX, newScaleY, reverse);
		}
	}

	public static void SetScaleXY(Transform subject, Vector2 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY.x, newScaleXY.y, false);
		}
	}

	public static void SetScaleXY(Transform subject, Vector2 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY.x, newScaleXY.y, reverse);
		}
	}

	public static void SetScaleXY(Transform subject, Vector3 newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY.x, newScaleXY.y, false);
		}
	}

	public static void SetScaleXY(Transform subject, Vector3 newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY.x, newScaleXY.y, reverse);
		}
	}

	public static void SetScaleXY(Transform subject, float newScaleXY)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY, newScaleXY, false);
		}
	}

	public static void SetScaleXY(Transform subject, float newScaleXY, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleXY, newScaleXY, reverse);
		}
	}

	public static void SetScaleXY(Transform subject, float newScaleX, float newScaleY)
	{
		if (subject != null)
		{
			SetScaleXY(subject, newScaleX, newScaleY, false);
		}
	}

	public static void SetScaleXY(Transform subject, float newScaleX, float newScaleY, bool reverse)
	{
		if (reverse)
		{
			newScaleX *= -1f;
			newScaleY *= -1f;
		}
		subject.localScale = new Vector3(newScaleX, newScaleY, subject.localScale.z);
	}

	public static void SetScaleXZ(GameObject subject, Vector2 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(GameObject subject, Vector2 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(GameObject subject, Vector3 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(GameObject subject, Vector3 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(GameObject subject, float newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(GameObject subject, float newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(GameObject subject, float newScaleX, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleX, newScaleZ, false);
		}
	}

	public static void SetScaleXZ(GameObject subject, float newScaleX, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleX, newScaleZ, reverse);
		}
	}

	public static void SetScaleXZ(Component subject, Vector2 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(Component subject, Vector2 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(Component subject, Vector3 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(Component subject, Vector3 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(Component subject, float newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(Component subject, float newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleXZ, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(Component subject, float newScaleX, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleX, newScaleZ, false);
		}
	}

	public static void SetScaleXZ(Component subject, float newScaleX, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject.transform, newScaleX, newScaleZ, reverse);
		}
	}

	public static void SetScaleXZ(Transform subject, Vector2 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ.x, newScaleXZ.y, false);
		}
	}

	public static void SetScaleXZ(Transform subject, Vector2 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ.x, newScaleXZ.y, reverse);
		}
	}

	public static void SetScaleXZ(Transform subject, Vector3 newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ.x, newScaleXZ.y, false);
		}
	}

	public static void SetScaleXZ(Transform subject, Vector3 newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ.x, newScaleXZ.y, reverse);
		}
	}

	public static void SetScaleXZ(Transform subject, float newScaleXZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ, newScaleXZ, false);
		}
	}

	public static void SetScaleXZ(Transform subject, float newScaleXZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleXZ, newScaleXZ, reverse);
		}
	}

	public static void SetScaleXZ(Transform subject, float newScaleX, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleXZ(subject, newScaleX, newScaleZ, false);
		}
	}

	public static void SetScaleXZ(Transform subject, float newScaleX, float newScaleZ, bool reverse)
	{
		if (reverse)
		{
			newScaleX *= -1f;
			newScaleZ *= -1f;
		}
		subject.localScale = new Vector3(newScaleX, subject.localScale.y, newScaleZ);
	}

	public static void SetScaleYZ(GameObject subject, Vector2 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(GameObject subject, Vector2 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(GameObject subject, Vector3 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(GameObject subject, Vector3 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(GameObject subject, float newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(GameObject subject, float newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(GameObject subject, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScaleYZ(GameObject subject, float newScaleY, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleY, newScaleZ, reverse);
		}
	}

	public static void SetScaleYZ(Component subject, Vector2 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(Component subject, Vector2 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(Component subject, Vector3 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(Component subject, Vector3 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(Component subject, float newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(Component subject, float newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleYZ, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(Component subject, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScaleYZ(Component subject, float newScaleY, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject.transform, newScaleY, newScaleZ, reverse);
		}
	}

	public static void SetScaleYZ(Transform subject, Vector2 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ.x, newScaleYZ.y, false);
		}
	}

	public static void SetScaleYZ(Transform subject, Vector2 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ.x, newScaleYZ.y, reverse);
		}
	}

	public static void SetScaleYZ(Transform subject, Vector3 newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ.x, newScaleYZ.y, false);
		}
	}

	public static void SetScaleYZ(Transform subject, Vector3 newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ.x, newScaleYZ.y, reverse);
		}
	}

	public static void SetScaleYZ(Transform subject, float newScaleYZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ, newScaleYZ, false);
		}
	}

	public static void SetScaleYZ(Transform subject, float newScaleYZ, bool reverse)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleYZ, newScaleYZ, reverse);
		}
	}

	public static void SetScaleYZ(Transform subject, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScaleYZ(subject, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScaleYZ(Transform subject, float newScaleY, float newScaleZ, bool reverse)
	{
		if (reverse)
		{
			newScaleY *= -1f;
			newScaleZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, newScaleY, newScaleZ);
	}

	public static void SetScale(GameObject subject, Vector3 newScale)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, false);
		}
	}

	public static void SetScale(GameObject subject, Vector3 newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, reverse);
		}
	}

	public static void SetScale(GameObject subject, float newScale)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, newScale, newScale, false);
		}
	}

	public static void SetScale(GameObject subject, float newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, newScale, newScale, reverse);
		}
	}

	public static void SetScale(GameObject subject, float newScaleX, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScaleX, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScale(GameObject subject, float newScaleX, float newScaleY, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScaleX, newScaleY, newScaleZ, reverse);
		}
	}

	public static void SetScale(Component subject, Vector3 newScale)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, false);
		}
	}

	public static void SetScale(Component subject, Vector3 newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, reverse);
		}
	}

	public static void SetScale(Component subject, float newScale)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, newScale, newScale, false);
		}
	}

	public static void SetScale(Component subject, float newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScale, newScale, newScale, reverse);
		}
	}

	public static void SetScale(Component subject, float newScaleX, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScaleX, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScale(Component subject, float newScaleX, float newScaleY, float newScaleZ, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject.transform, newScaleX, newScaleY, newScaleZ, reverse);
		}
	}

	public static void SetScale(Transform subject, Vector3 newScale)
	{
		if (subject != null)
		{
			SetScale(subject, newScale.x, newScale.y, newScale.z, false);
		}
	}

	public static void SetScale(Transform subject, Vector3 newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject, newScale.x, newScale.y, newScale.z, reverse);
		}
	}

	public static void SetScale(Transform subject, float newScale)
	{
		if (subject != null)
		{
			SetScale(subject, newScale, newScale, newScale, false);
		}
	}

	public static void SetScale(Transform subject, float newScale, bool reverse)
	{
		if (subject != null)
		{
			SetScale(subject, newScale, newScale, newScale, reverse);
		}
	}

	public static void SetScale(Transform subject, float newScaleX, float newScaleY, float newScaleZ)
	{
		if (subject != null)
		{
			SetScale(subject, newScaleX, newScaleY, newScaleZ, false);
		}
	}

	public static void SetScale(Transform subject, float newScaleX, float newScaleY, float newScaleZ, bool reverse)
	{
		if (reverse)
		{
			newScaleX *= -1f;
			newScaleY *= -1f;
			newScaleZ *= -1f;
		}
		subject.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);
	}

	public static void ScaleX(GameObject subject, float scaleByX)
	{
		if (subject != null)
		{
			ScaleX(subject.transform, scaleByX, false);
		}
	}

	public static void ScaleX(GameObject subject, float scaleByX, bool reverse)
	{
		if (subject != null)
		{
			ScaleX(subject.transform, scaleByX, reverse);
		}
	}

	public static void ScaleX(Component subject, float scaleByX)
	{
		if (subject != null)
		{
			ScaleX(subject.transform, scaleByX, false);
		}
	}

	public static void ScaleX(Component subject, float scaleByX, bool reverse)
	{
		if (subject != null)
		{
			ScaleX(subject.transform, scaleByX, reverse);
		}
	}

	public static void ScaleX(Transform subject, float scaleByX)
	{
		if (subject != null)
		{
			ScaleX(subject, scaleByX, false);
		}
	}

	public static void ScaleX(Transform subject, float scaleByX, bool reverse)
	{
		if (reverse)
		{
			scaleByX *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x * scaleByX, subject.localScale.y, subject.localScale.z);
	}

	public static void ScaleY(GameObject subject, float scaleByY)
	{
		if (subject != null)
		{
			ScaleY(subject.transform, scaleByY, false);
		}
	}

	public static void ScaleY(GameObject subject, float scaleByY, bool reverse)
	{
		if (subject != null)
		{
			ScaleY(subject.transform, scaleByY, reverse);
		}
	}

	public static void ScaleY(Component subject, float scaleByY)
	{
		if (subject != null)
		{
			ScaleY(subject.transform, scaleByY, false);
		}
	}

	public static void ScaleY(Component subject, float scaleByY, bool reverse)
	{
		if (subject != null)
		{
			ScaleY(subject.transform, scaleByY, reverse);
		}
	}

	public static void ScaleY(Transform subject, float scaleByY)
	{
		if (subject != null)
		{
			ScaleY(subject, scaleByY, false);
		}
	}

	public static void ScaleY(Transform subject, float scaleByY, bool reverse)
	{
		if (reverse)
		{
			scaleByY *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, subject.localScale.y * scaleByY, subject.localScale.z);
	}

	public static void ScaleZ(GameObject subject, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleZ(subject.transform, scaleByZ, false);
		}
	}

	public static void ScaleZ(GameObject subject, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleZ(subject.transform, scaleByZ, reverse);
		}
	}

	public static void ScaleZ(Component subject, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleZ(subject.transform, scaleByZ, false);
		}
	}

	public static void ScaleZ(Component subject, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleZ(subject.transform, scaleByZ, reverse);
		}
	}

	public static void ScaleZ(Transform subject, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleZ(subject, scaleByZ, false);
		}
	}

	public static void ScaleZ(Transform subject, float scaleByZ, bool reverse)
	{
		if (reverse)
		{
			scaleByZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, subject.localScale.y, subject.localScale.z * scaleByZ);
	}

	public static void ScaleXY(GameObject subject, Vector2 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, false);
		}
	}

	public static void ScaleXY(GameObject subject, Vector2 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(GameObject subject, Vector3 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, false);
		}
	}

	public static void ScaleXY(GameObject subject, Vector3 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(GameObject subject, float scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, scaleByXY, false);
		}
	}

	public static void ScaleXY(GameObject subject, float scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(GameObject subject, float scaleByX, float scaleByY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByX, scaleByY, false);
		}
	}

	public static void ScaleXY(GameObject subject, float scaleByX, float scaleByY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByX, scaleByY, reverse);
		}
	}

	public static void ScaleXY(Component subject, Vector2 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, false);
		}
	}

	public static void ScaleXY(Component subject, Vector2 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(Component subject, Vector3 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, false);
		}
	}

	public static void ScaleXY(Component subject, Vector3 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(Component subject, float scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, scaleByXY, false);
		}
	}

	public static void ScaleXY(Component subject, float scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByXY, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(Component subject, float scaleByX, float scaleByY)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByX, scaleByY, false);
		}
	}

	public static void ScaleXY(Component subject, float scaleByX, float scaleByY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject.transform, scaleByX, scaleByY, reverse);
		}
	}

	public static void ScaleXY(Transform subject, Vector2 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY.x, scaleByXY.y, false);
		}
	}

	public static void ScaleXY(Transform subject, Vector2 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY.x, scaleByXY.y, reverse);
		}
	}

	public static void ScaleXY(Transform subject, Vector3 scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY.x, scaleByXY.y, false);
		}
	}

	public static void ScaleXY(Transform subject, Vector3 scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY.x, scaleByXY.y, reverse);
		}
	}

	public static void ScaleXY(Transform subject, float scaleByXY)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY, scaleByXY, false);
		}
	}

	public static void ScaleXY(Transform subject, float scaleByXY, bool reverse)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByXY, scaleByXY, reverse);
		}
	}

	public static void ScaleXY(Transform subject, float scaleByX, float scaleByY)
	{
		if (subject != null)
		{
			ScaleXY(subject, scaleByX, scaleByY, false);
		}
	}

	public static void ScaleXY(Transform subject, float scaleByX, float scaleByY, bool reverse)
	{
		if (reverse)
		{
			scaleByX *= -1f;
			scaleByY *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x * scaleByX, subject.localScale.y * scaleByY, subject.localScale.z);
	}

	public static void ScaleXZ(GameObject subject, Vector2 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(GameObject subject, Vector2 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(GameObject subject, Vector3 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(GameObject subject, Vector3 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(GameObject subject, float scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(GameObject subject, float scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(GameObject subject, float scaleByX, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByX, scaleByZ, false);
		}
	}

	public static void ScaleXZ(GameObject subject, float scaleByX, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByX, scaleByZ, reverse);
		}
	}

	public static void ScaleXZ(Component subject, Vector2 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(Component subject, Vector2 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(Component subject, Vector3 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(Component subject, Vector3 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(Component subject, float scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(Component subject, float scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByXZ, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(Component subject, float scaleByX, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByX, scaleByZ, false);
		}
	}

	public static void ScaleXZ(Component subject, float scaleByX, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject.transform, scaleByX, scaleByZ, reverse);
		}
	}

	public static void ScaleXZ(Transform subject, Vector2 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ.x, scaleByXZ.y, false);
		}
	}

	public static void ScaleXZ(Transform subject, Vector2 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ.x, scaleByXZ.y, reverse);
		}
	}

	public static void ScaleXZ(Transform subject, Vector3 scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ.x, scaleByXZ.y, false);
		}
	}

	public static void ScaleXZ(Transform subject, Vector3 scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ.x, scaleByXZ.y, reverse);
		}
	}

	public static void ScaleXZ(Transform subject, float scaleByXZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ, scaleByXZ, false);
		}
	}

	public static void ScaleXZ(Transform subject, float scaleByXZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByXZ, scaleByXZ, reverse);
		}
	}

	public static void ScaleXZ(Transform subject, float scaleByX, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleXZ(subject, scaleByX, scaleByZ, false);
		}
	}

	public static void ScaleXZ(Transform subject, float scaleByX, float scaleByZ, bool reverse)
	{
		if (reverse)
		{
			scaleByX *= -1f;
			scaleByZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x * scaleByX, subject.localScale.y, subject.localScale.z * scaleByZ);
	}

	public static void ScaleYZ(GameObject subject, Vector2 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(GameObject subject, Vector2 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(GameObject subject, Vector3 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(GameObject subject, Vector3 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(GameObject subject, float scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(GameObject subject, float scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(GameObject subject, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByY, scaleByZ, false);
		}
	}

	public static void ScaleYZ(GameObject subject, float scaleByY, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByY, scaleByZ, reverse);
		}
	}

	public static void ScaleYZ(Component subject, Vector2 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(Component subject, Vector2 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(Component subject, Vector3 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(Component subject, Vector3 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(Component subject, float scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(Component subject, float scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByYZ, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(Component subject, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByY, scaleByZ, false);
		}
	}

	public static void ScaleYZ(Component subject, float scaleByY, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject.transform, scaleByY, scaleByZ, reverse);
		}
	}

	public static void ScaleYZ(Transform subject, Vector2 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ.x, scaleByYZ.y, false);
		}
	}

	public static void ScaleYZ(Transform subject, Vector2 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ.x, scaleByYZ.y, reverse);
		}
	}

	public static void ScaleYZ(Transform subject, Vector3 scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ.x, scaleByYZ.y, false);
		}
	}

	public static void ScaleYZ(Transform subject, Vector3 scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ.x, scaleByYZ.y, reverse);
		}
	}

	public static void ScaleYZ(Transform subject, float scaleByYZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ, scaleByYZ, false);
		}
	}

	public static void ScaleYZ(Transform subject, float scaleByYZ, bool reverse)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByYZ, scaleByYZ, reverse);
		}
	}

	public static void ScaleYZ(Transform subject, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			ScaleYZ(subject, scaleByY, scaleByZ, false);
		}
	}

	public static void ScaleYZ(Transform subject, float scaleByY, float scaleByZ, bool reverse)
	{
		if (reverse)
		{
			scaleByY *= -1f;
			scaleByZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x, subject.localScale.y * scaleByY, subject.localScale.z * scaleByZ);
	}

	public static void Scale(GameObject subject, Vector3 scaleBy)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, false);
		}
	}

	public static void Scale(GameObject subject, Vector3 scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, reverse);
		}
	}

	public static void Scale(GameObject subject, float scaleBy)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, scaleBy, scaleBy, false);
		}
	}

	public static void Scale(GameObject subject, float scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, scaleBy, scaleBy, reverse);
		}
	}

	public static void Scale(GameObject subject, float scaleByX, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleByX, scaleByY, scaleByZ, false);
		}
	}

	public static void Scale(GameObject subject, float scaleByX, float scaleByY, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleByX, scaleByY, scaleByZ, reverse);
		}
	}

	public static void Scale(Component subject, Vector3 scaleBy)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, false);
		}
	}

	public static void Scale(Component subject, Vector3 scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, reverse);
		}
	}

	public static void Scale(Component subject, float scaleBy)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, scaleBy, scaleBy, false);
		}
	}

	public static void Scale(Component subject, float scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleBy, scaleBy, scaleBy, reverse);
		}
	}

	public static void Scale(Component subject, float scaleByX, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleByX, scaleByY, scaleByZ, false);
		}
	}

	public static void Scale(Component subject, float scaleByX, float scaleByY, float scaleByZ, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject.transform, scaleByX, scaleByY, scaleByZ, reverse);
		}
	}

	public static void Scale(Transform subject, Vector3 scaleBy)
	{
		if (subject != null)
		{
			Scale(subject, scaleBy.x, scaleBy.y, scaleBy.z, false);
		}
	}

	public static void Scale(Transform subject, Vector3 scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject, scaleBy.x, scaleBy.y, scaleBy.z, reverse);
		}
	}

	public static void Scale(Transform subject, float scaleBy)
	{
		if (subject != null)
		{
			Scale(subject, scaleBy, scaleBy, scaleBy, false);
		}
	}

	public static void Scale(Transform subject, float scaleBy, bool reverse)
	{
		if (subject != null)
		{
			Scale(subject, scaleBy, scaleBy, scaleBy, reverse);
		}
	}

	public static void Scale(Transform subject, float scaleByX, float scaleByY, float scaleByZ)
	{
		if (subject != null)
		{
			Scale(subject, scaleByX, scaleByY, scaleByZ, false);
		}
	}

	public static void Scale(Transform subject, float scaleByX, float scaleByY, float scaleByZ, bool reverse)
	{
		if (reverse)
		{
			scaleByX *= -1f;
			scaleByY *= -1f;
			scaleByZ *= -1f;
		}
		subject.localScale = new Vector3(subject.localScale.x * scaleByX, subject.localScale.y * scaleByY, subject.localScale.z * scaleByZ);
	}

	public static void SetAngleX(GameObject subject, float newX)
	{
		if (subject != null)
		{
			SetAngleX(subject.transform, newX, false);
		}
	}

	public static void SetAngleX(GameObject subject, float newX, bool local)
	{
		if (subject != null)
		{
			SetAngleX(subject.transform, newX, local);
		}
	}

	public static void SetAngleX(Component subject, float newX)
	{
		if (subject != null)
		{
			SetAngleX(subject.transform, newX, false);
		}
	}

	public static void SetAngleX(Component subject, float newX, bool local)
	{
		if (subject != null)
		{
			SetAngleX(subject.transform, newX, local);
		}
	}

	public static void SetAngleX(Transform subject, float newX)
	{
		if (subject != null)
		{
			SetAngleX(subject, newX, false);
		}
	}

	public static void SetAngleX(Transform subject, float newX, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(newX, subject.localEulerAngles.y, subject.localEulerAngles.z);
		}
		else
		{
			subject.eulerAngles = new Vector3(newX, subject.eulerAngles.y, subject.eulerAngles.z);
		}
	}

	public static void SetAngleY(GameObject subject, float newY)
	{
		if (subject != null)
		{
			SetAngleY(subject.transform, newY, false);
		}
	}

	public static void SetAngleY(GameObject subject, float newY, bool local)
	{
		if (subject != null)
		{
			SetAngleY(subject.transform, newY, local);
		}
	}

	public static void SetAngleY(Component subject, float newY)
	{
		if (subject != null)
		{
			SetAngleY(subject.transform, newY, false);
		}
	}

	public static void SetAngleY(Component subject, float newY, bool local)
	{
		if (subject != null)
		{
			SetAngleY(subject.transform, newY, local);
		}
	}

	public static void SetAngleY(Transform subject, float newY)
	{
		if (subject != null)
		{
			SetAngleY(subject, newY, false);
		}
	}

	public static void SetAngleY(Transform subject, float newY, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(subject.localEulerAngles.x, newY, subject.localEulerAngles.z);
		}
		else
		{
			subject.eulerAngles = new Vector3(subject.eulerAngles.x, newY, subject.eulerAngles.z);
		}
	}

	public static void SetAngleZ(GameObject subject, float newZ)
	{
		if (subject != null)
		{
			SetAngleZ(subject.transform, newZ, false);
		}
	}

	public static void SetAngleZ(GameObject subject, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleZ(subject.transform, newZ, local);
		}
	}

	public static void SetAngleZ(Component subject, float newZ)
	{
		if (subject != null)
		{
			SetAngleZ(subject.transform, newZ, false);
		}
	}

	public static void SetAngleZ(Component subject, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleZ(subject.transform, newZ, local);
		}
	}

	public static void SetAngleZ(Transform subject, float newZ)
	{
		if (subject != null)
		{
			SetAngleZ(subject, newZ, false);
		}
	}

	public static void SetAngleZ(Transform subject, float newZ, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(subject.localEulerAngles.x, subject.localEulerAngles.y, newZ);
		}
		else
		{
			subject.eulerAngles = new Vector3(subject.eulerAngles.x, subject.eulerAngles.y, newZ);
		}
	}

	public static void SetAngleXY(GameObject subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, false);
		}
	}

	public static void SetAngleXY(GameObject subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, local);
		}
	}

	public static void SetAngleXY(GameObject subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, false);
		}
	}

	public static void SetAngleXY(GameObject subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, local);
		}
	}

	public static void SetAngleXY(GameObject subject, float newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, newXY, false);
		}
	}

	public static void SetAngleXY(GameObject subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, newXY, local);
		}
	}

	public static void SetAngleXY(GameObject subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newX, newY, false);
		}
	}

	public static void SetAngleXY(GameObject subject, float newX, float newY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newX, newY, local);
		}
	}

	public static void SetAngleXY(Component subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, false);
		}
	}

	public static void SetAngleXY(Component subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, local);
		}
	}

	public static void SetAngleXY(Component subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, false);
		}
	}

	public static void SetAngleXY(Component subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, local);
		}
	}

	public static void SetAngleXY(Component subject, float newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, newXY, false);
		}
	}

	public static void SetAngleXY(Component subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newXY, newXY, local);
		}
	}

	public static void SetAngleXY(Component subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newX, newY, false);
		}
	}

	public static void SetAngleXY(Component subject, float newX, float newY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject.transform, newX, newY, local);
		}
	}

	public static void SetAngleXY(Transform subject, Vector2 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY.x, newXY.y, false);
		}
	}

	public static void SetAngleXY(Transform subject, Vector2 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY.x, newXY.y, local);
		}
	}

	public static void SetAngleXY(Transform subject, Vector3 newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY.y, newXY.z, false);
		}
	}

	public static void SetAngleXY(Transform subject, Vector3 newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY.y, newXY.z, local);
		}
	}

	public static void SetAngleXY(Transform subject, float newXY)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY, newXY, false);
		}
	}

	public static void SetAngleXY(Transform subject, float newXY, bool local)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newXY, newXY, local);
		}
	}

	public static void SetAngleXY(Transform subject, float newX, float newY)
	{
		if (subject != null)
		{
			SetAngleXY(subject, newX, newY, false);
		}
	}

	public static void SetAngleXY(Transform subject, float newX, float newY, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(newX, newY, subject.localEulerAngles.z);
		}
		else
		{
			subject.eulerAngles = new Vector3(newX, newY, subject.eulerAngles.z);
		}
	}

	public static void SetAngleXZ(GameObject subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetAngleXZ(GameObject subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetAngleXZ(GameObject subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetAngleXZ(GameObject subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetAngleXZ(GameObject subject, float newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, newXZ, false);
		}
	}

	public static void SetAngleXZ(GameObject subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, newXZ, local);
		}
	}

	public static void SetAngleXZ(GameObject subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newX, newZ, false);
		}
	}

	public static void SetAngleXZ(GameObject subject, float newX, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newX, newZ, local);
		}
	}

	public static void SetAngleXZ(Component subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetAngleXZ(Component subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetAngleXZ(Component subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, false);
		}
	}

	public static void SetAngleXZ(Component subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, local);
		}
	}

	public static void SetAngleXZ(Component subject, float newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, newXZ, false);
		}
	}

	public static void SetAngleXZ(Component subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newXZ, newXZ, local);
		}
	}

	public static void SetAngleXZ(Component subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newX, newZ, false);
		}
	}

	public static void SetAngleXZ(Component subject, float newX, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject.transform, newX, newZ, local);
		}
	}

	public static void SetAngleXZ(Transform subject, Vector2 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ.x, newXZ.y, false);
		}
	}

	public static void SetAngleXZ(Transform subject, Vector2 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ.x, newXZ.y, local);
		}
	}

	public static void SetAngleXZ(Transform subject, Vector3 newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ.y, newXZ.z, false);
		}
	}

	public static void SetAngleXZ(Transform subject, Vector3 newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ.y, newXZ.z, local);
		}
	}

	public static void SetAngleXZ(Transform subject, float newXZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ, newXZ, false);
		}
	}

	public static void SetAngleXZ(Transform subject, float newXZ, bool local)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newXZ, newXZ, local);
		}
	}

	public static void SetAngleXZ(Transform subject, float newX, float newZ)
	{
		if (subject != null)
		{
			SetAngleXZ(subject, newX, newZ, false);
		}
	}

	public static void SetAngleXZ(Transform subject, float newX, float newZ, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(newX, subject.localEulerAngles.y, newZ);
		}
		else
		{
			subject.eulerAngles = new Vector3(newX, subject.eulerAngles.y, newZ);
		}
	}

	public static void SetAngleYZ(GameObject subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetAngleYZ(GameObject subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetAngleYZ(GameObject subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetAngleYZ(GameObject subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetAngleYZ(GameObject subject, float newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, newYZ, false);
		}
	}

	public static void SetAngleYZ(GameObject subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, newYZ, local);
		}
	}

	public static void SetAngleYZ(GameObject subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newY, newZ, false);
		}
	}

	public static void SetAngleYZ(GameObject subject, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newY, newZ, local);
		}
	}

	public static void SetAngleYZ(Component subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetAngleYZ(Component subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetAngleYZ(Component subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, false);
		}
	}

	public static void SetAngleYZ(Component subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, local);
		}
	}

	public static void SetAngleYZ(Component subject, float newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, newYZ, false);
		}
	}

	public static void SetAngleYZ(Component subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newYZ, newYZ, local);
		}
	}

	public static void SetAngleYZ(Component subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newY, newZ, false);
		}
	}

	public static void SetAngleYZ(Component subject, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject.transform, newY, newZ, local);
		}
	}

	public static void SetAngleYZ(Transform subject, Vector2 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ.x, newYZ.y, false);
		}
	}

	public static void SetAngleYZ(Transform subject, Vector2 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ.x, newYZ.y, local);
		}
	}

	public static void SetAngleYZ(Transform subject, Vector3 newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ.y, newYZ.z, false);
		}
	}

	public static void SetAngleYZ(Transform subject, Vector3 newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ.y, newYZ.z, local);
		}
	}

	public static void SetAngleYZ(Transform subject, float newYZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ, newYZ, false);
		}
	}

	public static void SetAngleYZ(Transform subject, float newYZ, bool local)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newYZ, newYZ, local);
		}
	}

	public static void SetAngleYZ(Transform subject, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngleYZ(subject, newY, newZ, false);
		}
	}

	public static void SetAngleYZ(Transform subject, float newY, float newZ, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(subject.localEulerAngles.x, newY, newZ);
		}
		else
		{
			subject.eulerAngles = new Vector3(subject.eulerAngles.x, newY, newZ);
		}
	}

	public static void SetAngle(GameObject subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, false);
		}
	}

	public static void SetAngle(GameObject subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, local);
		}
	}

	public static void SetAngle(GameObject subject, float newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void SetAngle(GameObject subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void SetAngle(GameObject subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newX, newY, newZ, false);
		}
	}

	public static void SetAngle(GameObject subject, float newX, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newX, newY, newZ, local);
		}
	}

	public static void SetAngle(Component subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, false);
		}
	}

	public static void SetAngle(Component subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, local);
		}
	}

	public static void SetAngle(Component subject, float newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void SetAngle(Component subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void SetAngle(Component subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newX, newY, newZ, false);
		}
	}

	public static void SetAngle(Component subject, float newX, float newY, float newZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject.transform, newX, newY, newZ, local);
		}
	}

	public static void SetAngle(Transform subject, Vector3 newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject, newXYZ.x, newXYZ.y, newXYZ.z, false);
		}
	}

	public static void SetAngle(Transform subject, Vector3 newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject, newXYZ.x, newXYZ.y, newXYZ.z, local);
		}
	}

	public static void SetAngle(Transform subject, float newXYZ)
	{
		if (subject != null)
		{
			SetAngle(subject, newXYZ, newXYZ, newXYZ, false);
		}
	}

	public static void SetAngle(Transform subject, float newXYZ, bool local)
	{
		if (subject != null)
		{
			SetAngle(subject, newXYZ, newXYZ, newXYZ, local);
		}
	}

	public static void SetAngle(Transform subject, float newX, float newY, float newZ)
	{
		if (subject != null)
		{
			SetAngle(subject, newX, newY, newZ, false);
		}
	}

	public static void SetAngle(Transform subject, float newX, float newY, float newZ, bool local)
	{
		if (local)
		{
			subject.localEulerAngles = new Vector3(newX, newY, newZ);
		}
		else
		{
			subject.eulerAngles = new Vector3(newX, newY, newZ);
		}
	}

	public static Vector3 GetPositionBefore(Transform target, float distance = 1f)
	{
		return GetPositionBefore(target.position, target.eulerAngles, distance);
	}

	public static Vector3 GetPositionBefore(Transform target, Transform forwardFacer, float distance = 1f)
	{
		return GetPositionBefore(target.position, forwardFacer.eulerAngles, distance);
	}

	public static Vector3 GetPositionBefore(Vector3 position, Vector3 rotations, float distance = 1f)
	{
		Vector3 result = new Vector3(MathUtils.SinFromDegrees(rotations.y), 0f - MathUtils.TanFromDegrees(rotations.x), MathUtils.CosFromDegrees(rotations.y)) * distance;
		if (position != Vector3.zero)
		{
			result += position;
		}
		return result;
	}

	public static void PositionBefore(Component subject, Component target)
	{
		PositionBefore(ToTransform(subject), ToTransform(target), 1f);
	}

	public static void PositionBefore(Transform subject, Component target)
	{
		PositionBefore(subject, ToTransform(target), 1f);
	}

	public static void PositionBefore(Component subject, Transform target)
	{
		PositionBefore(ToTransform(subject), target, 1f);
	}

	public static void PositionBefore(Transform subject, Transform target)
	{
		PositionBefore(ToTransform(subject), ToTransform(target), 1f);
	}

	public static void PositionBefore(Component subject, Component target, float distanceBefore)
	{
		PositionBefore(ToTransform(subject), ToTransform(target), distanceBefore);
	}

	public static void PositionBefore(Transform subject, Component target, float distanceBefore)
	{
		PositionBefore(subject, ToTransform(target), distanceBefore);
	}

	public static void PositionBefore(Component subject, Transform target, float distanceBefore)
	{
		PositionBefore(ToTransform(subject), target, distanceBefore);
	}

	public static void PositionBefore(Transform subject, Transform target, float distanceBefore)
	{
		if (subject != null && target != null)
		{
			subject.position = GetPositionBefore(target, distanceBefore);
		}
	}

	public static void LookAt(Transform looker, Transform target)
	{
		LookAt(looker, target, 1f);
	}

	public static void LookAt(Transform looker, Transform target, float weight)
	{
		if (!(looker != null) || !(target != null))
		{
			return;
		}
		if (weight < 1f)
		{
			if (looker.parent == null)
			{
				GameObject gameObject = new GameObject(looker.name + "'s Parent");
				Align(gameObject, looker);
				looker.parent = gameObject.transform;
			}
			Vector3 positionBefore = GetPositionBefore(looker.parent);
			LookFromTo(looker, positionBefore, target, weight);
		}
		else
		{
			LookFromTo(looker, Vector3.zero, target, 1f);
		}
	}

	public static void LookFromTo(Transform looker, Transform fromTarget, Transform toTarget, float weight)
	{
		if (fromTarget != null && toTarget != null)
		{
			LookFromTo(looker, fromTarget.position, toTarget.position, weight);
		}
	}

	public static void LookFromTo(Transform looker, Vector3 fromTargetPosition, Transform toTarget, float weight)
	{
		if (toTarget != null)
		{
			LookFromTo(looker, fromTargetPosition, toTarget.position, weight);
		}
	}

	public static void LookFromTo(Transform looker, Transform fromTarget, Vector3 toTargetPosition, float weight)
	{
		if (fromTarget != null)
		{
			LookFromTo(looker, fromTarget.position, toTargetPosition, weight);
		}
	}

	public static void LookFromTo(Transform looker, Vector3 fromTargetPosition, Vector3 toTargetPosition, float weight)
	{
		if (looker != null)
		{
			looker.eulerAngles = GetLookAt(looker.position, fromTargetPosition, toTargetPosition, weight);
		}
	}

	public static Vector3 GetLookAt(Vector3 lookPos, Vector3 fromTarget, Vector3 toTarget, float weight)
	{
		Vector3 vector = ((!(weight < 1f)) ? toTarget : ((!(weight <= 0f)) ? Vector3.Lerp(fromTarget, toTarget, weight) : fromTarget));
		float num = vector.x - lookPos.x;
		float num2 = vector.y - lookPos.y;
		float secondSide = vector.z - lookPos.z;
		float num3 = MathUtils.Hypotenuse(num, secondSide);
		float x = 0f - MathUtils.ArcTanInDegrees(num2 / num3);
		float y = MathUtils.ArcSinInDegrees(num / num3);
		return new Vector3(x, y, 0f);
	}

	public static Vector3 GetLookAt(Transform looker, Transform target)
	{
		return GetLookAt(looker, target, 1f);
	}

	public static Vector3 GetLookAt(Transform looker, Transform target, float lookWeight)
	{
		return GetLookAt(looker.position, looker.eulerAngles, target.position, lookWeight);
	}

	public static float GetLookAtDegrees(Transform looker, Transform target)
	{
		if (looker == null || target == null)
		{
			return -1f;
		}
		Vector2 lookAtXY = GetLookAtXY(looker, target, true);
		return MathUtils.Hypotenuse(lookAtXY);
	}

	public static Vector2 GetLookAtXY(Transform looker, Transform target)
	{
		return GetLookAtXY(looker, target, false);
	}

	private static Vector2 GetLookAtXY(Transform looker, Transform target, bool preVerified)
	{
		if (!preVerified && (looker == null || target == null))
		{
			return Vector2.zero;
		}
		Vector2 vector = GetLookAt(looker.position, looker.eulerAngles, target.position, 1f);
		Vector2 result = new Vector2(looker.eulerAngles.x - vector.x, looker.eulerAngles.y - vector.y);
		return result;
	}

	public static float GetLookAtAngle(float xDistance, float yDistance)
	{
		float num;
		if (yDistance == 0f)
		{
			num = ((!(xDistance <= 0f)) ? (-90f) : 90f);
		}
		else
		{
			num = MathUtils.ArcTanInDegrees(xDistance / yDistance);
			if (yDistance > 0f)
			{
				num += 180f;
			}
		}
		return num;
	}

	public static void RotateAroundXY(Transform toRotate, Transform center, float degrees)
	{
		if (center != null)
		{
			RotateAroundXY(toRotate, center.position.x, center.position.y, degrees);
		}
	}

	public static void RotateAroundXY(Transform toRotate, float centerX, float centerY, float degrees)
	{
		if (toRotate != null)
		{
			float radians = MathUtils.ToRadians(degrees);
			float num = MathUtils.Distance(toRotate.position.x, centerX, false);
			float num2 = MathUtils.Distance(toRotate.position.y, centerY, false);
			float num3 = MathUtils.Sin(radians);
			float num4 = MathUtils.Cos(radians);
			float newX = num4 * num - num3 * num2 + centerX;
			float newY = num3 * num - num4 * num2 + centerY;
			SetXY(toRotate, newX, newY, false);
		}
	}

	public static float DistanceX(Transform firstTransform, Transform secondTransform)
	{
		return DistanceX(firstTransform, secondTransform, true, false);
	}

	public static float DistanceX(Transform firstTransform, Transform secondTransform, bool returnAbs)
	{
		return DistanceX(firstTransform, secondTransform, returnAbs, false);
	}

	public static float DistanceX(Transform firstTransform, Transform secondTransform, bool returnAbs, bool preVerified)
	{
		if (preVerified || (firstTransform != null && secondTransform != null))
		{
			return MathUtils.Distance(firstTransform.position.x, secondTransform.position.x, returnAbs);
		}
		return -1f;
	}

	public static float DistanceY(Transform firstTransform, Transform secondTransform)
	{
		return DistanceY(firstTransform, secondTransform, true, false);
	}

	public static float DistanceY(Transform firstTransform, Transform secondTransform, bool returnAbs)
	{
		return DistanceY(firstTransform, secondTransform, returnAbs, false);
	}

	public static float DistanceY(Transform firstTransform, Transform secondTransform, bool returnAbs, bool preVerified)
	{
		if (preVerified || (firstTransform != null && secondTransform != null))
		{
			return MathUtils.Distance(firstTransform.position.y, secondTransform.position.y, returnAbs);
		}
		return -1f;
	}

	public static float DistanceZ(Transform firstTransform, Transform secondTransform)
	{
		return DistanceZ(firstTransform, secondTransform, true, false);
	}

	public static float DistanceZ(Transform firstTransform, Transform secondTransform, bool returnAbs)
	{
		return DistanceZ(firstTransform, secondTransform, returnAbs, false);
	}

	public static float DistanceZ(Transform firstTransform, Transform secondTransform, bool returnAbs, bool preVerified)
	{
		if (preVerified || (firstTransform != null && secondTransform != null))
		{
			return MathUtils.Distance(firstTransform.position.z, secondTransform.position.z, returnAbs);
		}
		return -1f;
	}

	public static float DistanceXY(Transform firstTransform, Transform secondTransform)
	{
		Vector3 distances = GetDistances(firstTransform, secondTransform, true, true, false);
		return MathUtils.Hypotenuse(distances.x, distances.y);
	}

	public static float DistanceXZ(Transform firstTransform, Transform secondTransform)
	{
		Vector3 distances = GetDistances(firstTransform, secondTransform, true, false, true);
		return MathUtils.Hypotenuse(distances.x, distances.z);
	}

	public static float DistanceYZ(Transform firstTransform, Transform secondTransform)
	{
		Vector3 distances = GetDistances(firstTransform, secondTransform, false, true, true);
		return MathUtils.Hypotenuse(distances.y, distances.z);
	}

	public static Vector3 DistanceXandY(Transform firstTransform, Transform secondTransform)
	{
		return GetDistances(firstTransform, secondTransform, true, true, false);
	}

	public static Vector3 DistanceXandZ(Transform firstTransform, Transform secondTransform)
	{
		return GetDistances(firstTransform, secondTransform, true, false, true);
	}

	public static Vector3 DistanceYandZ(Transform firstTransform, Transform secondTransform)
	{
		return GetDistances(firstTransform, secondTransform, false, true, true);
	}

	public static Vector3 DistanceXYandZ(Transform firstTransform, Transform secondTransform)
	{
		return GetDistances(firstTransform, secondTransform, true, true, true);
	}

	private static Vector3 GetDistances(Transform firstTransform, Transform secondTransform, bool calcX, bool calcY, bool calcZ, bool preVerified = false)
	{
		Vector3 zero = Vector3.zero;
		if (preVerified || (firstTransform != null && secondTransform != null))
		{
			if (calcX)
			{
				zero.x = MathUtils.Distance(firstTransform.position.x, secondTransform.position.x);
			}
			if (calcY)
			{
				zero.y = MathUtils.Distance(firstTransform.position.y, secondTransform.position.y);
			}
			if (calcZ)
			{
				zero.z = MathUtils.Distance(firstTransform.position.z, secondTransform.position.z);
			}
		}
		return zero;
	}

	public static Transform[] GetChildren(Transform parentTransform)
	{
		if (parentTransform == null)
		{
			return null;
		}
		Transform[] componentsInChildren = parentTransform.GetComponentsInChildren<Transform>();
		if (componentsInChildren.Length <= 1)
		{
			return null;
		}
		Transform[] array = new Transform[componentsInChildren.Length - 1];
		bool flag = false;
		for (int i = 0; i < componentsInChildren.Length - 1; i++)
		{
			if (!flag && componentsInChildren[i] == parentTransform)
			{
				flag = true;
			}
			int num = ((!flag) ? i : (i + 1));
			array[i] = componentsInChildren[num];
		}
		return array;
	}

	public static ComponentType[] GetChildrenAs<ComponentType>(Transform parentTransform) where ComponentType : Component
	{
		Transform[] children = GetChildren(parentTransform);
		ComponentType[] array;
		if (children == null)
		{
			array = null;
		}
		else
		{
			array = new ComponentType[children.Length];
			int num = 0;
			for (int i = 0; i < children.Length; i++)
			{
				ComponentType component = children[i].GetComponent<ComponentType>();
				if (component == null)
				{
					num++;
				}
				else
				{
					array[i - num] = component;
				}
			}
			if (num != 0)
			{
				int num2 = children.Length - num;
				ComponentType[] array2 = new ComponentType[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = array[j];
				}
				array = array2;
			}
		}
		return array;
	}

	public static bool IsBarren(Transform parentTransform)
	{
		return parentTransform == null || parentTransform.gameObject.GetComponentsInChildren<Transform>().Length <= 1;
	}

	public static Transform FindChild(Transform parentTransform, string childName)
	{
		if (parentTransform == null)
		{
			return null;
		}
		if (parentTransform.name == childName)
		{
			return parentTransform;
		}
		Transform[] children = GetChildren(parentTransform);
		if (children == null)
		{
			return null;
		}
		Transform[] array = children;
		foreach (Transform transform in array)
		{
			if (transform.gameObject.name == childName)
			{
				return transform;
			}
		}
		return null;
	}

	public static void TransferChildren(Transform oldParent, Transform newParent)
	{
		if (oldParent == null || newParent == null)
		{
			Debug.LogError(ErrorStrings.ValueNull(oldParent, "oldParent", newParent, "newParent"));
		}
		else if (!(oldParent == newParent))
		{
			Transform[] children = GetChildren(oldParent);
			foreach (Transform transform in children)
			{
				transform.parent = newParent;
			}
		}
	}

	public static void SwitchParents(Transform oldParent, Transform newParent)
	{
		SwitchParents(oldParent, newParent, true);
	}

	public static void SwitchParents(Transform oldParent, Transform newParent, bool transferSiblings)
	{
		if ((oldParent == null) | (newParent == null))
		{
			Debug.LogError(ErrorStrings.ValueNull(oldParent, "oldParent", newParent, "newParent"));
		}
		else if (!(oldParent.parent == newParent.parent))
		{
			Transform parent = oldParent.parent;
			oldParent.parent = newParent.parent;
			if (transferSiblings)
			{
				TransferChildren(oldParent, newParent);
			}
			newParent.parent = parent;
		}
	}

	public static Transform InstantiatePrefab(Object prefab)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(prefab);
		return gameObject.transform;
	}

	public static Transform Duplicate(Transform existingTransform)
	{
		return Duplicate(existingTransform.gameObject, null).transform;
	}

	public static Transform Duplicate(Transform existingTransform, string newName)
	{
		return Duplicate(existingTransform.gameObject, newName).transform;
	}

	public static GameObject Duplicate(GameObject existingGameObject)
	{
		return Duplicate(existingGameObject, null);
	}

	public static GameObject Duplicate(GameObject existingGameObject, string newName)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(existingGameObject);
		gameObject.transform.parent = existingGameObject.transform.parent;
		if (!string.IsNullOrEmpty(newName))
		{
			gameObject.name = newName;
		}
		Align(gameObject, existingGameObject);
		if (gameObject.transform.localScale != existingGameObject.transform.localScale)
		{
			gameObject.transform.localScale = existingGameObject.transform.localScale;
		}
		return gameObject;
	}

	public static void Hide(Transform item)
	{
		if (item != null)
		{
			SetVisibility(item, false, false);
		}
	}

	public static void Hide(GameObject item)
	{
		if (item != null)
		{
			SetVisibility(item.transform, false, true);
		}
	}

	public static void Hide(Component item)
	{
		if (item != null)
		{
			SetVisibility(item.transform, false, true);
		}
	}

	public static void Hide(params Transform[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Hide(items[i]);
		}
	}

	public static void Hide(params GameObject[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Hide(items[i]);
		}
	}

	public static void Hide(params Component[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Hide(items[i]);
		}
	}

	public static void Show(Transform item)
	{
		SetVisibility(item, true, false);
	}

	public static void Show(GameObject item)
	{
		if (item != null)
		{
			SetVisibility(item.transform, true, true);
		}
	}

	public static void Show(Component item)
	{
		if (item != null)
		{
			SetVisibility(item.transform, true, true);
		}
	}

	public static void Show(params Transform[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Show(items[i]);
		}
	}

	public static void Show(params GameObject[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Show(items[i]);
		}
	}

	public static void Show(params Component[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Show(items[i]);
		}
	}

	public static void SetVisibility(Transform item, bool show)
	{
		SetVisibility(item, show, false);
	}

	public static void SetVisibility(GameObject item, bool show)
	{
		if (item != null)
		{
			SetVisibility(item.transform, show, true);
		}
	}

	public static void SetVisibility(Component item, bool show)
	{
		if (item != null)
		{
			SetVisibility(item.transform, show, true);
		}
	}

	private static void SetVisibility(Transform item, bool show, bool preVerified)
	{
		if (!preVerified && !(item != null))
		{
			return;
		}
		if (show)
		{
			if (item.localPosition.x < -50000f)
			{
				item.localPosition += Vector3.right * 100000f;
			}
		}
		else if (item.localPosition.x > -50000f)
		{
			item.localPosition += Vector3.left * 100000f;
		}
	}

	public static Transform Find(string transformName)
	{
		Transform foundTransform;
		TryFind(out foundTransform, transformName);
		return foundTransform;
	}

	public static Transform Find(string transformName, Transform parent)
	{
		Transform foundTransform;
		TryFind(out foundTransform, transformName, parent);
		return foundTransform;
	}

	public static Transform FindWithTag(string transformName, string tag)
	{
		Transform foundTransform;
		TryFindWithTag(out foundTransform, transformName, tag);
		return foundTransform;
	}

	public static Transform FindChildWithTag(string transformName, string tag, Transform parent)
	{
		Transform foundTransform;
		TryFindChildWithTag(out foundTransform, transformName, tag, parent);
		return foundTransform;
	}

	public static bool TryFind(out Transform foundTransform, string transformName)
	{
		bool flag = false;
		bool result = false;
		foundTransform = null;
		string text = ((!flag) ? transformName.ToLowerInvariant() : null);
		string text2 = ((!flag) ? transformName.ToUpperInvariant() : null);
		string[] array = new string[3] { transformName, text, text2 };
		foreach (string text3 in array)
		{
			GameObject gameObject = GameObject.Find(text3);
			if (gameObject != null)
			{
				foundTransform = gameObject.transform;
				result = true;
				break;
			}
		}
		return result;
	}

	public static bool TryFind(out Transform foundTransform, string transformName, Transform parent)
	{
		bool flag = false;
		bool result = false;
		foundTransform = null;
		if (parent == null)
		{
			result = TryFind(out foundTransform, transformName);
		}
		else
		{
			string value = ((!flag) ? transformName.ToLowerInvariant() : null);
			Transform[] children = GetChildren(parent);
			foreach (Transform transform in children)
			{
				if ((!flag) ? transform.name.ToLowerInvariant().Equals(value) : transform.name.Equals(transformName))
				{
					foundTransform = transform;
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public static Transform[] FindAll(string transformName, Transform parent, bool sortByNumber = false)
	{
		if (parent == null)
		{
			Debug.LogError("TFUT: ERROR: Null transform passed to FindAll");
			return null;
		}
		if (sortByNumber)
		{
			return SortedSearch(transformName, parent);
		}
		return UnsortedSearch(transformName, parent);
	}

	public static bool TryFindWithTag(out Transform foundTransform, string transformName, string tag, bool matchCase = false)
	{
		return TryFindChildWithTag(out foundTransform, transformName, tag, null, matchCase);
	}

	public static bool TryFindChildWithTag(out Transform foundTransform, string transformName, string tag, Transform parent, bool matchCase = false)
	{
		string transformNameLower = null;
		string transformNameUpper = null;
		string tagLower = null;
		string tagUpper = null;
		if (matchCase)
		{
			transformNameLower = transformName.ToLowerInvariant();
			transformNameUpper = transformName.ToUpperInvariant();
			tagLower = tag.ToLowerInvariant();
			tagUpper = tag.ToUpperInvariant();
		}
		return SearchForTagged(out foundTransform, transformName, transformNameLower, transformNameUpper, tag, tagLower, tagUpper, parent, matchCase);
	}

	private static bool SearchForTagged(out Transform foundTransform, string transformName, string transformNameLower, string transformNameUpper, string tag, string tagLower, string tagUpper, Transform parent = null, bool matchCase = false)
	{
		bool flag = false;
		GameObject gameObject = null;
		foundTransform = null;
		if (parent == null)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
			if (!matchCase)
			{
				if (array.Length == 0)
				{
					array = GameObject.FindGameObjectsWithTag(tagLower);
				}
				if (array.Length == 0)
				{
					array = GameObject.FindGameObjectsWithTag(tagUpper);
				}
			}
			GameObject[] array2 = array;
			foreach (GameObject gameObject2 in array2)
			{
				if (gameObject2.name.Equals(transformName))
				{
					gameObject = gameObject2;
					flag = true;
					break;
				}
				if (gameObject2.name.Equals(transformNameLower) || gameObject2.name.Equals(transformNameUpper))
				{
					gameObject = gameObject2;
					flag = true;
					break;
				}
			}
			if (flag)
			{
				foundTransform = gameObject.transform;
			}
			else
			{
				foundTransform = null;
			}
		}
		else
		{
			Transform[] children = GetChildren(parent);
			foreach (Transform transform in children)
			{
				if (transform.tag != null)
				{
					bool flag2 = ((!matchCase) ? transform.tag.ToLowerInvariant().Equals(tag.ToLowerInvariant()) : transform.tag.Equals(tag));
					if (flag2 && ((!matchCase) ? transform.name.ToLowerInvariant().Equals(transformName.ToLowerInvariant()) : transform.name.Equals(transformName)))
					{
						foundTransform = transform;
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	public static Transform[] FindAllTagged(string tag)
	{
		return ToTransforms(GameObject.FindGameObjectsWithTag(tag));
	}

	public static Transform ToTransform(GameObject gameObject)
	{
		Transform result = null;
		if (gameObject != null)
		{
			result = gameObject.transform;
		}
		return result;
	}

	public static Transform ToTransform(Component component)
	{
		Transform result = null;
		if (component != null)
		{
			result = component.transform;
		}
		return result;
	}

	public static Transform[] ToTransforms(GameObject[] gameObjects)
	{
		Transform[] array = null;
		if (gameObjects != null)
		{
			array = new Transform[gameObjects.Length];
			for (int i = 0; i < gameObjects.Length; i++)
			{
				array[i] = ToTransform(gameObjects[i]);
			}
		}
		return array;
	}

	public static Transform[] ToTransforms(Component[] components)
	{
		Transform[] array = null;
		if (components != null)
		{
			array = new Transform[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				array[i] = ToTransform(components[i]);
			}
		}
		return array;
	}

	private static Transform[] ToArray(List<Transform> transformsList)
	{
		Transform[] array;
		if (transformsList == null)
		{
			array = null;
		}
		else
		{
			array = new Transform[transformsList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = transformsList[i];
			}
		}
		return array;
	}

	private static bool StartsWith(string text, string query)
	{
		string remaining;
		return StartsWith(text, query, out remaining);
	}

	private static bool StartsNotWith(string text, string query)
	{
		string remaining;
		return !StartsWith(text, query, out remaining);
	}

	private static bool StartsNotWith(string text, string query, out string remaining)
	{
		return !StartsWith(text, query, out remaining);
	}

	private static bool StartsWith(string text, string query, out string remaining)
	{
		bool result;
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(query) && query.Length <= text.Length)
		{
			string text2 = text.Substring(0, query.Length);
			result = text2.Equals(query);
			if (query.Length + 1 <= text.Length)
			{
				remaining = text.Substring(query.Length);
			}
			else
			{
				remaining = null;
			}
		}
		else
		{
			result = false;
			remaining = null;
		}
		return result;
	}

	private static Transform[] UnsortedSearch(string transformName, Transform parent)
	{
		List<Transform> list = new List<Transform>();
		Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (StartsWith(transform.name, transformName))
			{
				list.Add(transform);
			}
		}
		if (list.Count <= 0)
		{
			Debug.LogError(ErrorStrings.UnableToFind<Transform>(transformName, parent.name));
			list = null;
		}
		return ToArray(list);
	}

	private static Transform[] SortedSearch(string transformName, Transform parent)
	{
		Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
		ArrayUtils.List<NumberedTransform> list = new ArrayUtils.List<NumberedTransform>(componentsInChildren.Length);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			string remaining;
			if (StartsWith(componentsInChildren[i].name, transformName, out remaining))
			{
				int result;
				if (!int.TryParse(remaining, out result))
				{
					result = i + 1000;
				}
				list.Add(new NumberedTransform(componentsInChildren[i], result));
			}
		}
		Transform[] array;
		if (list.IsEmpty)
		{
			Debug.LogError(ErrorStrings.UnableToFind<Transform>(transformName, parent.name));
			array = null;
		}
		else
		{
			for (int j = 0; j < list.Length - 1; j++)
			{
				for (int k = 0; k < list.Length - (1 + j); k++)
				{
					if (list[k].Number > list[k + 1].Number)
					{
						NumberedTransform value = list[k];
						list[k] = list[k + 1];
						list[k + 1] = value;
					}
				}
			}
			array = new Transform[list.Length];
			for (int l = 0; l < list.Length; l++)
			{
				array[l] = list[l].Transform;
			}
		}
		return array;
	}

	public static void SetRelativeToCorner(Component component, float workingAspectRatio)
	{
		if (component != null)
		{
			SetRelativeToCorner(component.transform, workingAspectRatio, true);
		}
	}

	public static void SetRelativeToCorner(Component component, float workingAspectRatio, bool preVerified)
	{
		if (preVerified || component != null)
		{
			SetRelativeToCorner(component.transform, workingAspectRatio, preVerified);
		}
	}

	public static void SetRelativeToCorner(Transform transform, float workingAspectRatio)
	{
		SetRelativeToCorner(transform, workingAspectRatio, false);
	}

	public static void SetRelativeToCorner(Transform transform, float workingAspectRatio, bool preVerified)
	{
		if (preVerified || transform != null)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			float num = PortraitViewport.ReferenceAspect;
#else
			float num = MathUtils.IntDivision(Screen.width, Screen.height);
#endif
			float num2 = num / workingAspectRatio;
			SetX(transform, transform.localPosition.x * num2, true);
		}
	}
}
