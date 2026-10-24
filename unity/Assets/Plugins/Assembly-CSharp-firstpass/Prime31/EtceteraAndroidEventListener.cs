using System.Collections.Generic;
using UnityEngine;

namespace Prime31
{
	public class EtceteraAndroidEventListener : MonoBehaviour
	{
		private void OnEnable()
		{
			EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
			EtceteraAndroidManager.alertCancelledEvent += alertCancelledEvent;
			EtceteraAndroidManager.promptFinishedWithTextEvent += promptFinishedWithTextEvent;
			EtceteraAndroidManager.promptCancelledEvent += promptCancelledEvent;
			EtceteraAndroidManager.twoFieldPromptFinishedWithTextEvent += twoFieldPromptFinishedWithTextEvent;
			EtceteraAndroidManager.twoFieldPromptCancelledEvent += twoFieldPromptCancelledEvent;
			EtceteraAndroidManager.webViewCancelledEvent += webViewCancelledEvent;
			EtceteraAndroidManager.inlineWebViewJSCallbackEvent += inlineWebViewJSCallbackEvent;
			EtceteraAndroidManager.albumChooserCancelledEvent += albumChooserCancelledEvent;
			EtceteraAndroidManager.albumChooserSucceededEvent += albumChooserSucceededEvent;
			EtceteraAndroidManager.photoChooserCancelledEvent += photoChooserCancelledEvent;
			EtceteraAndroidManager.photoChooserSucceededEvent += photoChooserSucceededEvent;
			EtceteraAndroidManager.videoRecordingCancelledEvent += videoRecordingCancelledEvent;
			EtceteraAndroidManager.videoRecordingSucceededEvent += videoRecordingSucceededEvent;
			EtceteraAndroidManager.ttsInitializedEvent += ttsInitializedEvent;
			EtceteraAndroidManager.ttsFailedToInitializeEvent += ttsFailedToInitializeEvent;
			EtceteraAndroidManager.askForReviewDontAskAgainEvent += askForReviewDontAskAgainEvent;
			EtceteraAndroidManager.askForReviewRemindMeLaterEvent += askForReviewRemindMeLaterEvent;
			EtceteraAndroidManager.askForReviewWillOpenMarketEvent += askForReviewWillOpenMarketEvent;
			EtceteraAndroidManager.notificationReceivedEvent += notificationReceivedEvent;
			EtceteraAndroidManager.contactsLoadedEvent += contactsLoadedEvent;
		}

		private void OnDisable()
		{
			EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
			EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
			EtceteraAndroidManager.promptFinishedWithTextEvent -= promptFinishedWithTextEvent;
			EtceteraAndroidManager.promptCancelledEvent -= promptCancelledEvent;
			EtceteraAndroidManager.twoFieldPromptFinishedWithTextEvent -= twoFieldPromptFinishedWithTextEvent;
			EtceteraAndroidManager.twoFieldPromptCancelledEvent -= twoFieldPromptCancelledEvent;
			EtceteraAndroidManager.webViewCancelledEvent -= webViewCancelledEvent;
			EtceteraAndroidManager.inlineWebViewJSCallbackEvent -= inlineWebViewJSCallbackEvent;
			EtceteraAndroidManager.albumChooserCancelledEvent -= albumChooserCancelledEvent;
			EtceteraAndroidManager.albumChooserSucceededEvent -= albumChooserSucceededEvent;
			EtceteraAndroidManager.photoChooserCancelledEvent -= photoChooserCancelledEvent;
			EtceteraAndroidManager.photoChooserSucceededEvent -= photoChooserSucceededEvent;
			EtceteraAndroidManager.videoRecordingCancelledEvent -= videoRecordingCancelledEvent;
			EtceteraAndroidManager.videoRecordingSucceededEvent -= videoRecordingSucceededEvent;
			EtceteraAndroidManager.ttsInitializedEvent -= ttsInitializedEvent;
			EtceteraAndroidManager.ttsFailedToInitializeEvent -= ttsFailedToInitializeEvent;
			EtceteraAndroidManager.askForReviewDontAskAgainEvent -= askForReviewDontAskAgainEvent;
			EtceteraAndroidManager.askForReviewRemindMeLaterEvent -= askForReviewRemindMeLaterEvent;
			EtceteraAndroidManager.askForReviewWillOpenMarketEvent -= askForReviewWillOpenMarketEvent;
			EtceteraAndroidManager.notificationReceivedEvent -= notificationReceivedEvent;
			EtceteraAndroidManager.contactsLoadedEvent -= contactsLoadedEvent;
		}

		private void alertButtonClickedEvent(string positiveButton)
		{
			Debug.Log("alertButtonClickedEvent: " + positiveButton);
		}

		private void alertCancelledEvent()
		{
			Debug.Log("alertCancelledEvent");
		}

		private void promptFinishedWithTextEvent(string param)
		{
			Debug.Log("promptFinishedWithTextEvent: " + param);
		}

		private void promptCancelledEvent()
		{
			Debug.Log("promptCancelledEvent");
		}

		private void twoFieldPromptFinishedWithTextEvent(string text1, string text2)
		{
			Debug.Log("twoFieldPromptFinishedWithTextEvent: " + text1 + ", " + text2);
		}

		private void twoFieldPromptCancelledEvent()
		{
			Debug.Log("twoFieldPromptCancelledEvent");
		}

		private void webViewCancelledEvent()
		{
			Debug.Log("webViewCancelledEvent");
		}

		private void inlineWebViewJSCallbackEvent(string message)
		{
			Debug.Log("inlineWebViewJSCallbackEvent: " + message);
		}

		private void albumChooserCancelledEvent()
		{
			Debug.Log("albumChooserCancelledEvent");
		}

		private void albumChooserSucceededEvent(string imagePath)
		{
			Debug.Log("albumChooserSucceededEvent: " + imagePath);
			Debug.Log("image size: " + EtceteraAndroid.getImageSizeAtPath(imagePath));
		}

		private void photoChooserCancelledEvent()
		{
			Debug.Log("photoChooserCancelledEvent");
		}

		private void photoChooserSucceededEvent(string imagePath)
		{
			Debug.Log("photoChooserSucceededEvent: " + imagePath);
			Debug.Log("image size: " + EtceteraAndroid.getImageSizeAtPath(imagePath));
		}

		private void videoRecordingCancelledEvent()
		{
			Debug.Log("videoRecordingCancelledEvent");
		}

		private void videoRecordingSucceededEvent(string path)
		{
			Debug.Log("videoRecordingSucceededEvent: " + path);
		}

		private void ttsInitializedEvent()
		{
			Debug.Log("ttsInitializedEvent");
		}

		private void ttsFailedToInitializeEvent()
		{
			Debug.Log("ttsFailedToInitializeEvent");
		}

		private void askForReviewDontAskAgainEvent()
		{
			Debug.Log("askForReviewDontAskAgainEvent");
		}

		private void askForReviewRemindMeLaterEvent()
		{
			Debug.Log("askForReviewRemindMeLaterEvent");
		}

		private void askForReviewWillOpenMarketEvent()
		{
			Debug.Log("askForReviewWillOpenMarketEvent");
		}

		private void notificationReceivedEvent(string extraData)
		{
			Debug.Log("notificationReceivedEvent: " + extraData);
		}

		private void contactsLoadedEvent(List<EtceteraAndroid.Contact> contacts)
		{
			Debug.Log("contactsLoadedEvent");
			Utils.logObject(contacts);
		}
	}
}
