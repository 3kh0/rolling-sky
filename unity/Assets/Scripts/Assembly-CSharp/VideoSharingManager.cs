using System.Collections.Generic;
using UnityEngine;

public class VideoSharingManager : MonoBehaviour
{
	private enum Command
	{
		Record = 0,
		Stop = 1,
		Pause = 2,
		Resume = 3,
		Play = 4
	}

	private enum State
	{
		Configuring = 0,
		Waiting = 1,
		Recording = 2,
		Paused = 3,
		HasRecorded = 4
	}

	private const bool debugsEnabled = false;

	private const bool debugInitialize = false;

	private const bool debugChecks = false;

	private const bool debugCommands = false;

	private const int defaultChosenSharingQuality = 2;

	private const string userChosenQualityTag = "EP_Quality";

	private const int textureId = 1;

	private const bool useEveryplayCallback = false;

	public static bool videoSharingEnabled = true;

	private static bool videoSharing_isCurrentlyEnabled;

	private static State state;

	private bool thumbnailReady;

	private Texture2D thumbnail;

	private static bool videoSharing_isCurrentlyDisabled
	{
		get
		{
			return !videoSharing_isCurrentlyEnabled;
		}
	}

	public static bool IsEnabled
	{
		get
		{
			return videoSharing_isCurrentlyEnabled;
		}
	}

	public static bool IsDisabled
	{
		get
		{
			return videoSharing_isCurrentlyDisabled;
		}
	}

	public static bool DeviceCanSupportRecording
	{
		get
		{
			return GetIfEveryplaySupportsThisDevice() && GetDeviceQuality() > 0;
		}
	}

	public static bool DeviceCannotSupportRecording
	{
		get
		{
			return !DeviceCanSupportRecording;
		}
	}

	public static bool HasRecorded
	{
		get
		{
			return videoSharing_isCurrentlyEnabled && state == State.HasRecorded;
		}
	}

	public static bool HasNotRecorded
	{
		get
		{
			return !HasRecorded;
		}
	}

	public static string CurrentStateAsString
	{
		get
		{
			return state.ToString();
		}
	}

	public static bool IsRecording
	{
		get
		{
			bool result = false;
			if (videoSharing_isCurrentlyEnabled)
			{
			}
			return result;
		}
	}

	public static bool IsRecordingPaused
	{
		get
		{
			bool result = false;
			if (videoSharing_isCurrentlyEnabled)
			{
			}
			return result;
		}
	}

	public static bool IsNotRecording
	{
		get
		{
			return !IsRecording;
		}
	}

	public static void Initialize()
	{
		if (videoSharingEnabled)
		{
			ConfigureQuality();
		}
	}

	public static void Reset()
	{
		state = State.Waiting;
	}

	public static void StartRecording()
	{
		EnactCommand(Command.Record);
	}

	public static void StopRecording()
	{
		EnactCommand(Command.Stop);
	}

	public static void StopRecording(Dictionary<string, object> metaData)
	{
		EnactCommand(Command.Stop);
		SetMetaData(metaData);
	}

	public static void PauseRecording()
	{
		EnactCommand(Command.Pause);
	}

	public static void ResumeRecording()
	{
		EnactCommand(Command.Resume);
	}

	public static void PlayRecording()
	{
		EnactCommand(Command.Play);
	}

	public static void ShareRecording()
	{
		if (videoSharing_isCurrentlyEnabled)
		{
			if (state != State.HasRecorded)
			{
				Debug.LogError("VIDEO SHARING: Attempt to Share an Everplay recording that has not finished recording");
			}
			else
			{
				Debug.LogError("VIDEO SHARING: Sharing an Everplay recording...");
			}
		}
	}

	public static void SetMetaData(Dictionary<string, object> metaData)
	{
		if (!videoSharing_isCurrentlyEnabled)
		{
		}
	}

	private static void ConfigureQuality()
	{
		if (!GetIfEveryplaySupportsThisDevice())
		{
			return;
		}
		int deviceQuality = GetDeviceQuality();
		int userChosenQuality = GetUserChosenQuality();
		int num = ((deviceQuality >= userChosenQuality) ? userChosenQuality : deviceQuality);
		if (num == 0)
		{
			videoSharing_isCurrentlyEnabled = false;
			return;
		}
		videoSharing_isCurrentlyEnabled = true;
		switch (num)
		{
		case 1:
		{
			string text = "LOW";
			bool flag = true;
			int num2 = 30;
			break;
		}
		case 2:
		{
			string text = "HIGH";
			bool flag = false;
			bool flag2 = true;
			int num2 = 60;
			break;
		}
		default:
		{
			Debug.LogError(string.Format("Recieved an unexpected integer quality-value for of '{0}' for Everyplay quality-setting - default to low quality setting", num));
			string text = "LOW";
			bool flag = true;
			int num2 = 30;
			break;
		}
		}
		state = State.Waiting;
	}

	private static bool GetIfEveryplaySupportsThisDevice()
	{
		return false;
	}

	private static int GetDeviceQuality()
	{
		return DeviceQualityChecker.GetDeviceQualityAsInt();
	}

	private static int GetUserChosenQuality()
	{
		return PlayerPrefs.GetInt("EP_Quality", 2);
	}

	private static void EnactCommand(Command command)
	{
		if (videoSharing_isCurrentlyEnabled)
		{
			bool flag = true;
			if ((command == Command.Pause || command == Command.Resume || command == Command.Stop) && IsNotRecording)
			{
				Debug.LogWarning(string.Format("VIDEO SHARING: Attempt to {0} an Everplay recording that has not even started", command.ToString()));
				flag = false;
			}
			else if (command == Command.Play && state != State.HasRecorded)
			{
				Debug.LogError("VIDEO SHARING: Attempt to Play an Everplay recording that has not finished recording");
				flag = false;
			}
			if (flag)
			{
				flag = false;
			}
		}
	}
}
