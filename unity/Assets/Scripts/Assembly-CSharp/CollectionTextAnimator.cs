using UnityEngine;

public class CollectionTextAnimator : MonoBehaviour
{
	private enum State
	{
		Activated = 0,
		DeActivated = 1
	}

	private const float riseTime = 1f;

	private const float riseDistance = 2.5f;

	private const bool faceCamera = false;

	private const float maxCharacterSize = 3f;

	private State state = State.DeActivated;

	private bool geoGivenAway;

	private TimePeices.Timer riseTimer;

	private Vector3 startPosition;

	private Vector3 endPosition;

	private Vector3 onEndPosition;

	private Quaternion onEndRotation;

	private PrefabID prefabId;

	private TextMesh collectionText;

	private Color collectionTextColor;

	private float startingCharacterSize;

	private static Transform collectionTextParent;

	public void Initialize(Transform target, PrefabID prefabId, int oneUpAmmount = 1, float oneUpExtraScale = 0f)
	{
		this.prefabId = prefabId;
		onEndPosition = base.transform.localPosition;
		onEndRotation = base.transform.localRotation;
		base.transform.position = target.position;
		startPosition = base.transform.position;
		endPosition = startPosition;
		endPosition.z -= 2.5f;
		riseTimer = new TimePeices.Timer(1f);
		collectionText = base.gameObject.GetComponentInChildren<TextMesh>();
		collectionTextColor = collectionText.color;
		collectionTextColor.a = 1f;
		collectionText.text = "+" + oneUpAmmount;
		if (collectionTextParent == null)
		{
			collectionTextParent = new GameObject("_Collection Text").transform;
		}
		base.transform.parent = collectionTextParent;
		startingCharacterSize = collectionText.characterSize;
		collectionText.characterSize = MathUtils.FromPercent(oneUpExtraScale, startingCharacterSize, 3f);
		state = State.Activated;
	}

	public void Update()
	{
		if (state == State.Activated)
		{
			riseTimer.Update();
			if (riseTimer.HasElapsed)
			{
				base.transform.position = onEndPosition;
				base.transform.rotation = onEndRotation;
				collectionText.characterSize = startingCharacterSize;
				BufferManager.GiveGeo(base.transform, prefabId);
				state = State.DeActivated;
			}
			else
			{
				float percentElapsed = riseTimer.PercentElapsed;
				float num = FloatAnim.Smooth(percentElapsed, false, true);
				base.transform.position = Vector3.Lerp(startPosition, endPosition, num);
				collectionTextColor.a = 1f - num;
				collectionText.color = collectionTextColor;
			}
		}
	}
}
