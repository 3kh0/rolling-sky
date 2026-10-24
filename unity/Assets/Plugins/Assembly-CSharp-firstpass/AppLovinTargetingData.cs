using UnityEngine;

public class AppLovinTargetingData
{
	public const string GENDER_MALE = "m";

	public const string GENDER_FEMALE = "f";

	public AndroidJavaObject currentActivity;

	public AndroidJavaClass applovinFacade = new AndroidJavaClass("com.applovin.sdk.unity.AppLovinFacade");

	public AppLovinTargetingData()
	{
	}

	public AppLovinTargetingData(AndroidJavaObject activity)
	{
		if (activity == null)
		{
			throw new MissingReferenceException("No parent activity specified");
		}
		currentActivity = activity;
	}

	public void setGender(string gender)
	{
		applovinFacade.CallStatic("SetGender", currentActivity, gender);
	}

	public void setBirthYear(int birthYear)
	{
		applovinFacade.CallStatic("SetBirthYear", currentActivity, birthYear);
	}

	public void setLanguage(string language)
	{
		applovinFacade.CallStatic("SetLanguage", currentActivity, language);
	}

	public void setCountry(string country)
	{
		applovinFacade.CallStatic("SetCountry", currentActivity, country);
	}

	public void setCarrier(string carrier)
	{
		applovinFacade.CallStatic("SetCarrier", currentActivity, carrier);
	}

	public void setInterests(params string[] interests)
	{
		applovinFacade.CallStatic("SetInterests", currentActivity, interests);
	}

	public void setKeywords(params string[] keywords)
	{
		applovinFacade.CallStatic("SetKeywords", currentActivity, keywords);
	}

	public void putExtra(string key, string val)
	{
		applovinFacade.CallStatic("PutExtra", currentActivity, key, val);
	}
}
