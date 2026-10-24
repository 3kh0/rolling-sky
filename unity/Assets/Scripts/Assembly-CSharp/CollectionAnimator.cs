using UnityEngine;

public class CollectionAnimator : MonoBehaviour
{
	private Transform target;

	private Vector3 startPosition;

	private Quaternion startRotation;

	private PrefabID prefabID;

	private Tile.PickupType pickupType;

	private int value;

	private TimePeices.Timer collectionTimer;

	private float autoCollectionDistance;

	private float targetPositionOffsetZ;

	private bool initialized;

	private bool geoGivenAway;

	private Vector3 targetPosition;

	public void Initialize(Transform target, float collectionTime, float autoCollectionDistance, float targetPositionOffsetZ, PrefabID prefabID, Tile.PickupType pickupType, int value)
	{
		this.target = target;
		collectionTimer = new TimePeices.Timer(collectionTime);
		this.autoCollectionDistance = autoCollectionDistance;
		this.targetPositionOffsetZ = targetPositionOffsetZ;
		base.transform.parent = null;
		startPosition = base.transform.position;
		startRotation = base.transform.rotation;
		this.prefabID = prefabID;
		this.pickupType = pickupType;
		this.value = value;
		initialized = true;
		geoGivenAway = false;
	}

	public void Update()
	{
		if (initialized && !geoGivenAway)
		{
			collectionTimer.Update();
			UpdateTargetPosition();
			if (collectionTimer.HasElapsed || WithinPickupRange())
			{
				geoGivenAway = true;
				base.transform.position = Vector3.zero;
				base.transform.localEulerAngles = Vector3.zero;
				BufferManager.GiveGeo(base.transform, prefabID);
				GameManager.ApplyScoreFor(pickupType);
				Object.Destroy(base.gameObject.GetComponent<CollectionAnimator>());
			}
			else
			{
				float percentElapsed = collectionTimer.PercentElapsed;
				float t = FloatAnim.Smooth(percentElapsed, true, false);
				base.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
			}
		}
	}

	private bool WithinPickupRange()
	{
		return Vector3.Distance(targetPosition, base.transform.position) <= autoCollectionDistance;
	}

	private void UpdateTargetPosition()
	{
		targetPosition = target.position;
		targetPosition.z += targetPositionOffsetZ;
	}
}
