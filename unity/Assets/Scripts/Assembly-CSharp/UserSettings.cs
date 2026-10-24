using UnityEngine;

public class UserSettings : MonoBehaviour
{
	private const bool debugsEnabled = false;

	private static Persistant.Bool music = new Persistant.Bool("Music", "Settings: Music On/Off", true);

	private static Persistant.Bool sound = new Persistant.Bool("Sound Effects", "Settings: Sound Effects On/Off", true);

	private static Persistant.Bool alerts = new Persistant.Bool("Alerts", "Settings: Alerts On/Off", true);

	private static Persistant.Bool autoPause = new Persistant.Bool("Auto-Pause", "Settings: Auto-Pause On/Off", true);

	private static Persistant.Bool recording = new Persistant.Bool("Recording", "Settings: Recording On/Off", true);

	public static void SetMusicTo(bool on)
	{
		music.Set(on);
	}

	public static void SetSoundTo(bool on)
	{
		sound.Set(on);
	}

	public static void SetAlertsTo(bool on)
	{
		alerts.Set(on);
	}

	public static void SetAutoPauseTo(bool on)
	{
		autoPause.Set(on);
	}

	public static void SetRecordingTo(bool on)
	{
		recording.Set(on);
	}

	public static void ToggleMusic()
	{
		music.Set(!music.Get());
	}

	public static void ToggleSound()
	{
		sound.Set(!sound.Get());
	}

	public static void ToggleAlerts()
	{
		alerts.Set(!alerts.Get());
	}

	public static void ToggleAutoPause()
	{
		autoPause.Set(!autoPause.Get());
	}

	public static void ToggleRecording()
	{
		recording.Set(!recording.Get());
	}

	public static bool IsMusicOn()
	{
		return music.Get();
	}

	public static bool IsSoundOn()
	{
		return sound.Get();
	}

	public static bool AreAlertsOn()
	{
		return alerts.Get();
	}

	public static bool IsAutoPauseOn()
	{
		return autoPause.Get();
	}

	public static bool IsRecordingOn()
	{
		return recording.Get();
	}

	public static bool IsMusicOff()
	{
		return !IsMusicOn();
	}

	public static bool IsSoundOff()
	{
		return !IsSoundOn();
	}

	public static bool AreAlertsOff()
	{
		return !AreAlertsOn();
	}

	public static bool IsAutoPauseOff()
	{
		return !IsAutoPauseOn();
	}

	public static bool IsRecordingOff()
	{
		return !IsRecordingOn();
	}
}
