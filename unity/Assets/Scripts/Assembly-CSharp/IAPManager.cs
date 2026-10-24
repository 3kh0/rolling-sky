using System;
using System.Collections.Generic;
using System.Globalization;
using Prime31;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
	public delegate void ResultCallback(bool result);

	private const string shouldRefreshProductPricesKey = "iapm_shouldrefreshprices";

	private static int minutesBetweenServerRefresh = 10;

	private static string _purchaseProductID;

	private static bool restoringPurchases;

	private static ResultCallback OnResult;

	public static string ProductIDUnlimitedBalls
	{
		get
		{
			string text = "RSIAPUNLIMITEDBALLS";
			return text.ToLowerInvariant();
		}
	}

	public static string ProductUnlimitedBallsFormattedPrice
	{
		get
		{
			return PlayerPrefs.GetString(string.Format("iapm_ub_formattedprice_{0}", ProductIDUnlimitedBalls), string.Empty);
		}
		private set
		{
			PlayerPrefs.SetString(string.Format("iapm_ub_formattedprice_{0}", ProductIDUnlimitedBalls), value);
		}
	}

	public static string ProductUnlimitedBallsPriceString
	{
		get
		{
			return PlayerPrefs.GetString(string.Format("iapm_ub_pricestring_{0}", ProductIDUnlimitedBalls), string.Empty);
		}
		private set
		{
			PlayerPrefs.SetString(string.Format("iapm_ub_pricestring_{0}", ProductIDUnlimitedBalls), value);
		}
	}

	public static float? ProductUnlimitedBallsPrice
	{
		get
		{
			string s = PlayerPrefs.GetString(string.Format("iapm_ub_pricevalue_{0}", ProductIDUnlimitedBalls), string.Empty);
			float result;
			if (float.TryParse(s, out result))
			{
				return result;
			}
			return null;
		}
		private set
		{
			string value2 = string.Empty;
			if (value.HasValue)
			{
				value2 = value.Value.ToString();
			}
			PlayerPrefs.SetString(string.Format("iapm_ub_pricevalue_{0}", ProductIDUnlimitedBalls), value2);
		}
	}

	public static string ProductCurrencySymbol
	{
		get
		{
			return PlayerPrefs.GetString(string.Format("iapm_currencysymbol_{0}", ProductIDUnlimitedBalls), string.Empty);
		}
		private set
		{
			PlayerPrefs.SetString(string.Format("iapm_currencysymbol_{0}", ProductIDUnlimitedBalls), value);
		}
	}

	public static string ProductCurrencyCode
	{
		get
		{
			return PlayerPrefs.GetString(string.Format("iapm_currencycode_{0}", ProductIDUnlimitedBalls), string.Empty);
		}
		private set
		{
			PlayerPrefs.SetString(string.Format("iapm_currencycode_{0}", ProductIDUnlimitedBalls), value);
		}
	}

	private void TryResultCallback(bool result)
	{
		Debug.Log(string.Format("{0}, {1}", result, OnResult == null));
		if (OnResult != null)
		{
			OnResult(result);
			OnResult = null;
		}
	}

	private void Start()
	{
		string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArHHR6U+SlXHS0cQ2Ma/PGFaxrdk/9xPacHxl3Um9LtZ7JL6jiHmGYTO5MWsza6xw5XTSyh83Y7TyFZmmDCr0PLtVco8aUlEu6cFOu5q3z9/4gkFY7aefk4URrToYE4CFFHpXxrUJcAooQ9o07IR8ZgTT7gioDrCnT/vhiMwCyRMgEnTOmWEvVjvqn4VJg4N/Md3rlp9WcmzLba+mMKRSY93iIAVsJgmkmIQvpJJulOs68PrytNxbvT8jnvJ87qApp40acT2YNHIT4R82AHYBF4hNO6hViyNSW6Jdo+gMk+TNVDpSV45mfbUV5dp8rPjzvPdXL2MBSljp5ktE4LjItQIDAQAB";
		GoogleIAB.init(publicKey);
		if (ShouldRefreshProductPrices())
		{
			RefreshProductPrices();
		}
	}

	private void OnEnable()
	{
		GoogleIABManager.billingSupportedEvent += BillingSupportedEventHandler;
		GoogleIABManager.queryInventorySucceededEvent += QueryInventorySucceededEventHandler;
		GoogleIABManager.purchaseSucceededEvent += PurchaseSucceededEventHandler;
		GoogleIABManager.purchaseFailedEvent += PurchaseFailedEventHandler;
	}

	private void OnDisable()
	{
		GoogleIABManager.billingSupportedEvent -= BillingSupportedEventHandler;
		GoogleIABManager.queryInventorySucceededEvent -= QueryInventorySucceededEventHandler;
		GoogleIABManager.purchaseSucceededEvent -= PurchaseSucceededEventHandler;
		GoogleIABManager.purchaseFailedEvent -= PurchaseFailedEventHandler;
	}

	public static void RefreshProductPrices()
	{
		string[] skus = new string[1] { ProductIDUnlimitedBalls };
		GoogleIAB.queryInventory(skus);
	}

	public static void PurchaseProduct(string productID, ResultCallback OnResultCallback)
	{
		if (HasInternet())
		{
			OnResult = OnResultCallback;
			restoringPurchases = false;
			GoogleIAB.purchaseProduct(productID.ToLowerInvariant());
		}
		else
		{
			AlertManager.ShowNoInternetAlert("make purchases", true);
		}
	}

	public static void RestoreProductPurchases(ResultCallback OnResultCallback)
	{
		if (HasInternet())
		{
			restoringPurchases = true;
			OnResult = OnResultCallback;
			string[] skus = new string[1] { ProductIDUnlimitedBalls };
			GoogleIAB.queryInventory(skus);
		}
		else
		{
			AlertManager.ShowNoInternetAlert("restore previous purchases", true);
		}
	}

	private static bool ShouldRefreshProductPrices()
	{
		if (!PlayerPrefs.HasKey("iapm_shouldrefreshprices") || string.IsNullOrEmpty(ProductUnlimitedBallsPriceString))
		{
			return true;
		}
		DateTime result = DateTime.UtcNow.AddYears(-1);
		string text = PlayerPrefs.GetString("iapm_shouldrefreshprices");
		if (DateTime.TryParseExact(text, "s", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
		{
			int num = (int)(DateTime.UtcNow - result).TotalMinutes;
			if (num <= minutesBetweenServerRefresh)
			{
				return false;
			}
			return true;
		}
		Debug.LogError(string.Format("IAP Manager: Error: Unable to convert stored string of last price refresh into datetime.\nString stored in PlayerPrefs under name '{0}', value: {1}", "iapm_shouldrefreshprices", text));
		return true;
	}

	private static void StoreLastPriceRefreshDates()
	{
		PlayerPrefs.SetString("iapm_shouldrefreshprices", DateTime.UtcNow.ToString("s"));
		PlayerPrefs.Save();
	}

	public static bool HasInternet()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}

	public static bool HasNotInternet()
	{
		return Application.internetReachability == NetworkReachability.NotReachable;
	}

	private void BillingSupportedEventHandler()
	{
	}

	private void QueryInventorySucceededEventHandler(List<GooglePurchase> productsOwned, List<GoogleSkuInfo> productQueries)
	{
		bool flag = false;
		if (restoringPurchases)
		{
			bool result = false;
			foreach (GooglePurchase item in productsOwned)
			{
				if (item.productId.ToLowerInvariant() == ProductIDUnlimitedBalls)
				{
					PlayerProfiler.OnPremiumModePurchase();
					LivesManager.RechargeLives(GameManager.StartingLives);
					result = true;
				}
			}
			restoringPurchases = false;
			TryResultCallback(result);
		}
		foreach (GoogleSkuInfo productQuery in productQueries)
		{
			if (productQuery.productId.ToLowerInvariant() == ProductIDUnlimitedBalls)
			{
				flag = true;
				ProductUnlimitedBallsFormattedPrice = productQuery.price;
				ProductUnlimitedBallsPriceString = productQuery.price;
				ProductCurrencySymbol = productQuery.priceCurrencyCode;
				ProductCurrencyCode = productQuery.priceCurrencyCode;
				float result2;
				if (float.TryParse(productQuery.price, out result2))
				{
					ProductUnlimitedBallsPrice = result2;
				}
			}
		}
		if (flag)
		{
			StoreLastPriceRefreshDates();
		}
	}

	private void PurchaseSucceededEventHandler(GooglePurchase googlePurchase)
	{
		if (googlePurchase.productId.ToLowerInvariant() == ProductIDUnlimitedBalls)
		{
			TryResultCallback(true);
		}
	}

	private void PurchaseFailedEventHandler(string message, int response)
	{
		if (!string.IsNullOrEmpty(message) && message.ToLowerInvariant().Contains("already owned"))
		{
			TryResultCallback(true);
			AlertManager.ShowAlert(Localizer.GetTerm("Restore Completed"), Localizer.GetLine("You have previously purchased Upgrade. Upgrade restored."));
		}
		else
		{
			string message2 = ((!string.IsNullOrEmpty(message)) ? string.Format("{0}. {1}: {2}", Localizer.GetTerm("Purchase attempt failed"), response, message) : Localizer.GetTerm("Purchase attempt failed"));
			AlertManager.ShowAlert(Localizer.GetTerm("Purchase Failed"), message2);
			TryResultCallback(false);
		}
	}
}
