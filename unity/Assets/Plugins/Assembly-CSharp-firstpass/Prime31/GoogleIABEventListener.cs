using System.Collections.Generic;
using UnityEngine;

namespace Prime31
{
	public class GoogleIABEventListener : MonoBehaviour
	{
		private void OnEnable()
		{
			GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		}

		private void OnDisable()
		{
			GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
			GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		}

		private void billingSupportedEvent()
		{
			Debug.Log("billingSupportedEvent");
		}

		private void billingNotSupportedEvent(string error)
		{
			Debug.Log("billingNotSupportedEvent: " + error);
		}

		private void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
		{
			Debug.Log(string.Format("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
			Utils.logObject(purchases);
			Utils.logObject(skus);
		}

		private void queryInventoryFailedEvent(string error)
		{
			Debug.Log("queryInventoryFailedEvent: " + error);
		}

		private void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
		{
			Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
		}

		private void purchaseSucceededEvent(GooglePurchase purchase)
		{
			Debug.Log("purchaseSucceededEvent: " + purchase);
		}

		private void purchaseFailedEvent(string error, int response)
		{
			Debug.Log("purchaseFailedEvent: " + error + ", response: " + response);
		}

		private void consumePurchaseSucceededEvent(GooglePurchase purchase)
		{
			Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		}

		private void consumePurchaseFailedEvent(string error)
		{
			Debug.Log("consumePurchaseFailedEvent: " + error);
		}
	}
}
