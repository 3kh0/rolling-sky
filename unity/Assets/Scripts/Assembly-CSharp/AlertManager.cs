using Prime31;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
	public delegate void FunctionForAlertPress(bool result);

	private const string defaultButton = "Got it";

	private const string defaultYesButton = "OK";

	private const string defaultNoButton = "No thanks";

	private const string noInternetTitle = "No Internet";

	private const string noInternetMessageStart = "You must be connected to the internet to ";

	private const string noInternetMessageEnd = ".";

	private const string confirmPurchasePriceSuffix = "Price";

	private const string confirmPurchaseMessageEnd = "?";

	private const string instanceName = "AlertManager";

	private const bool defaultLocalize = true;

	private static bool isInitialized;

	private static GameObject instance;

	private static string expectedYesButton;

	private static string expectedNoButton;

	private static FunctionForAlertPress OnAlertPress;

	private void OnEnable()
	{
		EtceteraAndroidManager.alertButtonClickedEvent += AlertButtonClicked;
	}

	private void OnDisable()
	{
		EtceteraAndroidManager.alertButtonClickedEvent -= AlertButtonClicked;
	}

	private void TryOnAlertPress(bool result)
	{
		if (OnAlertPress != null)
		{
			OnAlertPress(result);
			OnAlertPress = null;
		}
	}

	private static void TryInitialize()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			instance = new GameObject("AlertManager");
			instance.AddComponent<AlertManager>();
			Object.DontDestroyOnLoad(instance);
		}
	}

	public static void ShowAlert(string title, string message)
	{
		ShowAlert(title, message, "Got it", true);
	}

	public static void ShowAlert(string title, string message, bool localize)
	{
		ShowAlert(title, message, "Got it", localize);
	}

	public static void ShowAlert(string title, string message, string button)
	{
		ShowAlert(title, message, button, true);
	}

	public static void ShowAlert(string title, string message, string button, bool localize)
	{
		string title2 = ((!localize) ? title : Localizer.GetTerm(title));
		string message2 = ((!localize) ? message : Localizer.GetLine(message));
		string text = ((!localize) ? button : Localizer.GetTerm(button));
		EnactAlert(title2, message2, text);
	}

	private static void ShowAlert(string title, string message, params string[] buttonTitles)
	{
		EnactAlert(title, message, buttonTitles);
	}

	public static void ShowNoInternetAlert(string actionRequiringInternet)
	{
		ShowNoInternetAlert(actionRequiringInternet, true);
	}

	public static void ShowNoInternetAlert(string actionRequiringInternet, bool localize)
	{
		string title = ((!localize) ? "No Internet" : Localizer.GetTerm("No Internet"));
		string text = ((!localize) ? "You must be connected to the internet to " : Localizer.GetLine("You must be connected to the internet to "));
		string text2 = ((!localize) ? actionRequiringInternet : Localizer.GetLine(actionRequiringInternet));
		string text3 = ((!localize) ? "." : Localizer.GetTerm("."));
		string text4 = ((!localize) ? "Got it" : Localizer.GetTerm("Got it"));
		string message = text + text2 + text3;
		EnactAlert(title, message, text4);
	}

	public static void ShowYesNoAlert(string title, string message, FunctionForAlertPress onPressCallback)
	{
		ShowYesNoAlert(title, message, onPressCallback, true);
	}

	public static void ShowYesNoAlert(string title, string message, FunctionForAlertPress onPressCallback, bool localize)
	{
		OnAlertPress = onPressCallback;
		string title2 = ((!localize) ? title : Localizer.GetTerm(title));
		string message2 = ((!localize) ? message : Localizer.GetLine(message));
		expectedYesButton = ((!localize) ? "OK" : Localizer.GetLine("OK"));
		expectedNoButton = ((!localize) ? "No thanks" : Localizer.GetTerm("No thanks"));
		EnactAlert(title2, message2, expectedYesButton, expectedNoButton);
	}

	public static void ShowConfirmPurchaseAlert(string title, string message, string formattedPrice, FunctionForAlertPress onPressCallback)
	{
		ShowConfirmPurchaseAlert(title, message, formattedPrice, onPressCallback, true);
	}

	public static void ShowConfirmPurchaseAlert(string title, string message, string formattedPrice, FunctionForAlertPress onPressCallback, bool localize)
	{
		OnAlertPress = onPressCallback;
		string title2 = ((!localize) ? title : Localizer.GetTerm(title));
		string text = ((!localize) ? message : Localizer.GetLine(message));
		expectedYesButton = ((!localize) ? "OK" : Localizer.GetLine("OK"));
		expectedNoButton = ((!localize) ? "No thanks" : Localizer.GetTerm("No thanks"));
		if (!string.IsNullOrEmpty(formattedPrice))
		{
			string arg = ((!localize) ? "Price" : Localizer.GetTerm("Price"));
			text = string.Format("{0}\n{1}: {2}", text, arg, formattedPrice);
		}
		EnactAlert(title2, text, expectedYesButton, expectedNoButton);
	}

	private static void EnactAlert(string title, string message, params string[] buttonTitles)
	{
		TryInitialize();
		EtceteraAndroid.setAlertDialogTheme(5);
		if (buttonTitles == null || buttonTitles.Length == 0)
		{
			Debug.LogError(string.Format("Alert Manager: ERROR: incorrect usage of Helpers.ShowAlert - must specify atleast ONE Button Title for the Alert (alert title: '{0}')", title));
			return;
		}
		if (buttonTitles.Length == 1)
		{
			EtceteraAndroid.showAlert(title, message, buttonTitles[0]);
			return;
		}
		if (buttonTitles.Length > 2)
		{
			Debug.LogWarning(string.Format("Alter Manager: Warning: incorrect usage of Helpers.ShowAlert - can specify a maximum of TWO Button Titles for the Alert (alert title: '{0}')", title));
		}
		EtceteraAndroid.showAlert(title, message, buttonTitles[0], buttonTitles[1]);
	}

	private void AlertButtonClicked(string buttonText)
	{
		bool? flag = null;
		if (buttonText.Equals(expectedYesButton))
		{
			flag = true;
		}
		else if (buttonText.Equals(expectedNoButton))
		{
			flag = false;
		}
		if (flag.HasValue)
		{
			expectedYesButton = null;
			expectedNoButton = null;
			TryOnAlertPress(flag.Value);
		}
	}
}
