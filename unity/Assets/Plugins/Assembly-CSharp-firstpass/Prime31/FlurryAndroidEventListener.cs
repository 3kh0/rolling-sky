using UnityEngine;

namespace Prime31
{
	public class FlurryAndroidEventListener : MonoBehaviour
	{
		private void OnEnable()
		{
			FlurryAndroidManager.adAvailableForSpaceEvent += adAvailableForSpaceEvent;
			FlurryAndroidManager.adNotAvailableForSpaceEvent += adNotAvailableForSpaceEvent;
			FlurryAndroidManager.onAdClosedEvent += onAdClosedEvent;
			FlurryAndroidManager.onApplicationExitEvent += onApplicationExitEvent;
			FlurryAndroidManager.onRenderFailedEvent += onRenderFailedEvent;
			FlurryAndroidManager.spaceDidFailToReceiveAdEvent += spaceDidFailToReceiveAdEvent;
			FlurryAndroidManager.spaceDidReceiveAdEvent += spaceDidReceiveAdEvent;
			FlurryAndroidManager.onAdClickedEvent += onAdClickedEvent;
			FlurryAndroidManager.onAdOpenedEvent += onAdOpenedEvent;
			FlurryAndroidManager.onVideoCompletedEvent += onVideoCompletedEvent;
		}

		private void OnDisable()
		{
			FlurryAndroidManager.adAvailableForSpaceEvent -= adAvailableForSpaceEvent;
			FlurryAndroidManager.adNotAvailableForSpaceEvent -= adNotAvailableForSpaceEvent;
			FlurryAndroidManager.onAdClosedEvent -= onAdClosedEvent;
			FlurryAndroidManager.onApplicationExitEvent -= onApplicationExitEvent;
			FlurryAndroidManager.onRenderFailedEvent -= onRenderFailedEvent;
			FlurryAndroidManager.spaceDidFailToReceiveAdEvent -= spaceDidFailToReceiveAdEvent;
			FlurryAndroidManager.spaceDidReceiveAdEvent -= spaceDidReceiveAdEvent;
			FlurryAndroidManager.onAdClickedEvent -= onAdClickedEvent;
			FlurryAndroidManager.onAdOpenedEvent -= onAdOpenedEvent;
			FlurryAndroidManager.onVideoCompletedEvent -= onVideoCompletedEvent;
		}

		private void adAvailableForSpaceEvent(string adSpace)
		{
			Debug.Log("adAvailableForSpaceEvent: " + adSpace);
		}

		private void adNotAvailableForSpaceEvent(string adSpace)
		{
			Debug.Log("adNotAvailableForSpaceEvent: " + adSpace);
		}

		private void onAdClosedEvent(string adSpace)
		{
			Debug.Log("onAdClosedEvent: " + adSpace);
		}

		private void onApplicationExitEvent(string adSpace)
		{
			Debug.Log("onApplicationExitEvent: " + adSpace);
		}

		private void onRenderFailedEvent(string adSpace)
		{
			Debug.Log("onRenderFailedEvent: " + adSpace);
		}

		private void spaceDidFailToReceiveAdEvent(string adSpace)
		{
			Debug.Log("spaceDidFailToReceiveAdEvent: " + adSpace);
		}

		private void spaceDidReceiveAdEvent(string adSpace)
		{
			Debug.Log("spaceDidReceiveAdEvent: " + adSpace);
		}

		private void onAdClickedEvent(string adSpace)
		{
			Debug.Log("onAdClickedEvent: " + adSpace);
		}

		private void onAdOpenedEvent(string adSpace)
		{
			Debug.Log("onAdOpenedEvent: " + adSpace);
		}

		private void onVideoCompletedEvent(string adSpace)
		{
			Debug.Log("onVideoCompletedEvent: " + adSpace);
		}
	}
}
