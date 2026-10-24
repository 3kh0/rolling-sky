using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class ServerConfigs : MonoBehaviour
{
	private delegate void FunctionToRefreshGlobalParameters();

	private const bool debugsEnabled = false;

	private const bool debugRefresh = false;

	private const bool debugDateTime = false;

	private const bool debugVariableRefresh = false;

	private const int minutesBetweenServerRefresh = 5;

	private const bool skipCloudServer = false;

	private const bool useDebugConfig = false;

	private const string serverConfigDirectory = "http://d58wl5rslb22i.cloudfront.net/";

	private const string serverConfigDirectoryDirect = "https://s3-us-west-1.amazonaws.com/rollingskyandroid/";

	private const string serverConfigFilename = "config.txt";

	private const string serverConfigFilenameDebug = "configDebug.txt";

	private const string playerPrefsRefreshKey = "SC_LastRefreshDataDate";

	private const string serverVariablePlayerPrefsPrefix = "SC_";

	private static FunctionToRefreshGlobalParameters RefreshGlobalParameters = GameManager.RefreshServerParameters;

	private static ServerConfigs instance;

	private static bool hasLoadedDataFromGlobalRefresh = false;

	public static ServerConfigs Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("ServerConfig Controller").AddComponent<ServerConfigs>();
				UnityEngine.Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public void OnApplicationQuit()
	{
		instance = null;
	}

	public static void Refresh()
	{
		if (!hasLoadedDataFromGlobalRefresh)
		{
			RefreshGlobalParameters();
			Instance.StartCoroutine(RefreshData());
			hasLoadedDataFromGlobalRefresh = true;
		}
		else if (!DataWasRefreshedInPeriod())
		{
			Instance.StartCoroutine(RefreshData());
		}
	}

	public static string GetStringFromPlayerPrefs(string currentValue, string variableName)
	{
		string variableStringValue;
		if (TryGetStringFromPlayerPrefs(variableName, out variableStringValue))
		{
			return variableStringValue;
		}
		Debug.LogWarning(string.Format("Server Configs: Warning: '{0}' couldn't be found in PlayerPrefs. Defaulting to existing value of '{1}'", variableName, (!string.IsNullOrEmpty(currentValue)) ? currentValue : "null"));
		return currentValue;
	}

	public static int GetIntFromPlayerPrefs(int currentValue, string variableName)
	{
		string variableStringValue;
		if (TryGetStringFromPlayerPrefs(variableName, out variableStringValue))
		{
			short result;
			if (short.TryParse(variableStringValue, out result))
			{
				return result;
			}
			Debug.LogError(string.Format("Server Configs: Error: problem parsing '{0}'s stored string-value of '{1}' couldn't be pasred into an INTEGER. Defaulting to existing value of '{2}'", variableName, variableStringValue, currentValue));
			return currentValue;
		}
		Debug.LogWarning(string.Format("Server Configs: Warning: '{0}' couldn't be found in PlayerPrefs. Defaulting to existing value of '{1}'", variableName, currentValue));
		return currentValue;
	}

	public static float GetFloatFromPlayerPrefs(float currentValue, string variableName)
	{
		string variableStringValue;
		if (TryGetStringFromPlayerPrefs(variableName, out variableStringValue))
		{
			float result;
			if (float.TryParse(variableStringValue, out result))
			{
				return result;
			}
			Debug.LogError(string.Format("Server Configs: Error: problem parsing '{0}'s stored string-value of '{1}' couldn't be parsed into a FLOAT. Defaulting to existing value of '{1}'", variableName, variableStringValue, currentValue));
			return currentValue;
		}
		Debug.LogWarning(string.Format("Server Configs: Warning: '{0}' couldn't be found in PlayerPrefs. Defaulting to existing value of '{1}'", variableName, currentValue));
		return currentValue;
	}

	public static bool GetBoolFromPlayerPrefs(bool currentValue, string variableName)
	{
		string variableStringValue;
		if (TryGetStringFromPlayerPrefs(variableName, out variableStringValue))
		{
			bool result;
			if (bool.TryParse(variableStringValue, out result))
			{
				return result;
			}
			short result2;
			if (short.TryParse(variableStringValue, out result2))
			{
				switch (result2)
				{
				case 0:
					return false;
				case 1:
					return true;
				default:
					Debug.LogError(string.Format("Server Configs: Error: problem parsing '{0}'s stored string-value of '{1}' couldn't be parsed into a BOOLEAN - please use one of the following values: true, false, 0, 1.\nDefaulting to existing value of '{2}'", variableName, variableStringValue, currentValue));
					return currentValue;
				}
			}
			Debug.LogError(string.Format("Server Configs: Error: problem parsing '{0}'s stored string-value of '{1}' couldn't be parsed into a BOOLEAN (or even an INTEGER) - please use one of the following values: true, false, 0, 1.\nDefaulting to existing value of '{2}'", variableName, variableStringValue, currentValue));
			return currentValue;
		}
		Debug.LogWarning(string.Format("Server Configs: Warning: '{0}' couldn't be found in PlayerPrefs. Defaulting to existing value of {1}", variableName, currentValue));
		return currentValue;
	}

	private static bool DataWasRefreshedInPeriod()
	{
		if (!PlayerPrefs.HasKey("SC_LastRefreshDataDate"))
		{
			return false;
		}
		DateTime result = DateTime.UtcNow.AddYears(-1);
		string s = PlayerPrefs.GetString("SC_LastRefreshDataDate");
		string text = null;
		if (DateTime.TryParseExact(s, "s", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
		{
			int num = (int)(DateTime.UtcNow - result).TotalMinutes;
			return num <= 5;
		}
		return false;
	}

	private static void UpdateDataRefreshDate()
	{
		PlayerPrefs.SetString("SC_LastRefreshDataDate", DateTime.UtcNow.ToString("s"));
		PlayerPrefs.Save();
	}

	private static IEnumerator RefreshData()
	{
		if (!HasInternet())
		{
			yield break;
		}
		string serverDirectory = "http://d58wl5rslb22i.cloudfront.net/";
		string serverFilename = "config.txt";
		string serverUrl = serverDirectory + serverFilename;
		string debugString = null;
		WWW www = new WWW(serverUrl);
		yield return www;
		try
		{
			UpdateDataRefreshDate();
			if (string.IsNullOrEmpty(www.error))
			{
				string wwwText = www.text;
				wwwText = ((wwwText == null) ? string.Empty : wwwText.Trim());
				ParseSettingsText(wwwText, debugString);
			}
			else
			{
				Debug.LogError(string.Format("Server Configs: Error: Problem trying to get www from: {0}.  Error message: {1}", serverUrl, www.error));
			}
		}
		catch (Exception ex)
		{
			Exception exception = ex;
			string exceptionString = ((exception == null) ? "(null exception)" : exception.Message);
			Debug.LogWarning(string.Format("Server Configs: Error: {0}", exceptionString));
		}
		PlayerPrefs.Save();
	}

	private static void ParseSettingsText(string settingsText, string debugString)
	{
		if (settingsText == null || settingsText.Length < 1)
		{
			return;
		}
		string[] array = settingsText.Split('\n');
		bool flag = true;
		bool flag2 = false;
		for (int i = 0; i < array.Length; i++)
		{
			if (flag)
			{
				flag = false;
				continue;
			}
			string text = ((i >= 10) ? i.ToString() : (i + "  "));
			string text2 = array[i];
			string[] array2 = text2.Split('"');
			if (array2.Length < 4)
			{
				continue;
			}
			string text3 = array2[1];
			text3 = text3.Trim().ToLowerInvariant();
			string text4 = array2[3];
			text4 = text4.Trim();
			bool flag3 = string.IsNullOrEmpty(text3);
			bool flag4 = string.IsNullOrEmpty(text4);
			if (!flag3)
			{
				PlayerPrefs.SetString("SC_" + text3, text4);
				if (!flag2)
				{
					flag2 = true;
				}
			}
		}
		if (flag2)
		{
			RefreshGlobalParameters();
		}
	}

	private static bool TryGetStringFromPlayerPrefs(string variableName, out string variableStringValue)
	{
		bool flag = false;
		string key = "SC_" + variableName.ToLowerInvariant();
		if (PlayerPrefs.HasKey(key))
		{
			variableStringValue = PlayerPrefs.GetString(key);
			variableStringValue = PlayerPrefs.GetString(key);
			return true;
		}
		variableStringValue = null;
		return false;
	}

	private static bool HasInternet()
	{
		Debug.Log(Application.internetReachability);
		return Application.internetReachability != NetworkReachability.NotReachable;
	}
}
