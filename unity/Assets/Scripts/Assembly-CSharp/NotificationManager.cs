using Prime31;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	public const bool debugsEnabled = true;

	public const bool debugSetting = true;

	public const bool debugCancelling = true;

	public const bool debugRegistering = true;

	private static Persistant.Bool haveRegistered = new Persistant.Bool("NotificationsRegistered");

	private static int AndroidRechargeNotificationID
	{
		get
		{
			return PlayerPrefs.GetInt("RSNMARNID", 0);
		}
		set
		{
			PlayerPrefs.SetInt("RSNMARNID", value);
		}
	}

	public static bool HaveRegisteredBefore
	{
		get
		{
			return haveRegistered.Get();
		}
	}

	public static bool HaveNotRegisteredBefore
	{
		get
		{
			return !HaveRegisteredBefore;
		}
	}

	public static void Register()
	{
		haveRegistered.Set(true);
		Debug.Log("Notication Manager: Debug: Registering for notifications");
	}

	public static void NeverRegister()
	{
		haveRegistered.Set(true);
	}

	public static void ClearNotificationBadge()
	{
	}

	public static void SetNotification(int secondsUntilNotification, string notificationMessage)
	{
		if (UserSettings.AreAlertsOn() && AndroidRechargeNotificationID != 0)
		{
			EtceteraAndroid.cancelNotification(AndroidRechargeNotificationID);
		}
	}

	public static void CancelNotifications()
	{
		ClearNotificationBadge();
		if (AndroidRechargeNotificationID != 0)
		{
			EtceteraAndroid.cancelNotification(AndroidRechargeNotificationID);
		}
		Debug.Log("Notication Manager: Debug: Cancelling all active notifications");
	}
}
