using UnityEngine;

namespace Prime31
{
	public class AdMobAndroidEventListener : MonoBehaviour
	{
		private void OnEnable()
		{
			AdMobAndroidManager.dismissingScreenEvent += dismissingScreenEvent;
			AdMobAndroidManager.failedToReceiveAdEvent += failedToReceiveAdEvent;
			AdMobAndroidManager.leavingApplicationEvent += leavingApplicationEvent;
			AdMobAndroidManager.presentingScreenEvent += presentingScreenEvent;
			AdMobAndroidManager.receivedAdEvent += receivedAdEvent;
			AdMobAndroidManager.interstitialDismissingScreenEvent += interstitialDismissingScreenEvent;
			AdMobAndroidManager.interstitialFailedToReceiveAdEvent += interstitialFailedToReceiveAdEvent;
			AdMobAndroidManager.interstitialLeavingApplicationEvent += interstitialLeavingApplicationEvent;
			AdMobAndroidManager.interstitialPresentingScreenEvent += interstitialPresentingScreenEvent;
			AdMobAndroidManager.interstitialReceivedAdEvent += interstitialReceivedAdEvent;
		}

		private void OnDisable()
		{
			AdMobAndroidManager.dismissingScreenEvent -= dismissingScreenEvent;
			AdMobAndroidManager.failedToReceiveAdEvent -= failedToReceiveAdEvent;
			AdMobAndroidManager.leavingApplicationEvent -= leavingApplicationEvent;
			AdMobAndroidManager.presentingScreenEvent -= presentingScreenEvent;
			AdMobAndroidManager.receivedAdEvent -= receivedAdEvent;
			AdMobAndroidManager.interstitialDismissingScreenEvent -= interstitialDismissingScreenEvent;
			AdMobAndroidManager.interstitialFailedToReceiveAdEvent -= interstitialFailedToReceiveAdEvent;
			AdMobAndroidManager.interstitialLeavingApplicationEvent -= interstitialLeavingApplicationEvent;
			AdMobAndroidManager.interstitialPresentingScreenEvent -= interstitialPresentingScreenEvent;
			AdMobAndroidManager.interstitialReceivedAdEvent -= interstitialReceivedAdEvent;
		}

		private void dismissingScreenEvent()
		{
			Debug.Log("dismissingScreenEvent");
		}

		private void failedToReceiveAdEvent(string error)
		{
			Debug.Log("failedToReceiveAdEvent: " + error);
		}

		private void leavingApplicationEvent()
		{
			Debug.Log("leavingApplicationEvent");
		}

		private void presentingScreenEvent()
		{
			Debug.Log("presentingScreenEvent");
		}

		private void receivedAdEvent()
		{
			Debug.Log("receivedAdEvent");
		}

		private void interstitialDismissingScreenEvent()
		{
			Debug.Log("interstitialDismissingScreenEvent");
		}

		private void interstitialFailedToReceiveAdEvent(string error)
		{
			Debug.Log("interstitialFailedToReceiveAdEvent: " + error);
		}

		private void interstitialLeavingApplicationEvent()
		{
			Debug.Log("interstitialLeavingApplicationEvent");
		}

		private void interstitialPresentingScreenEvent()
		{
			Debug.Log("interstitialPresentingScreenEvent");
		}

		private void interstitialReceivedAdEvent()
		{
			Debug.Log("interstitialReceivedAdEvent");
		}
	}
}
