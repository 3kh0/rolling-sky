using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prime31
{
	public class GPGMultiplayer
	{
		private class RealTimeMessageReceivedListener : AndroidJavaProxy
		{
			public RealTimeMessageReceivedListener()
				: base("com.prime31.IRealTimeMessageReceivedListener")
			{
			}

			public void onMessageReceived(string senderParticipantId, string messageData)
			{
				byte[] message = Convert.FromBase64String(messageData);
				GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId, message);
			}

			public void onRawMessageReceived(AndroidJavaObject senderParticipantId, AndroidJavaObject messageData)
			{
				string senderParticipantId2 = senderParticipantId.Call<string>("toString", new object[0]);
				byte[] message = AndroidJNI.FromByteArray(messageData.GetRawObject());
				GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId2, message);
			}

			public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] args)
			{
				if (methodName == "onRawMessageReceived")
				{
					onRawMessageReceived(args[0], args[1]);
					return null;
				}
				return base.Invoke(methodName, args);
			}

			public string toString()
			{
				return "RealTimeMessageReceivedListener class instance from Unity";
			}
		}

		private static AndroidJavaObject _plugin;

		static GPGMultiplayer()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return;
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
			{
				_plugin = androidJavaClass.CallStatic<AndroidJavaObject>("realtimeMultiplayerInstance", new object[0]);
				_plugin.Call("setRealtimeMessageListener", new RealTimeMessageReceivedListener());
			}
		}

		public static void registerDeviceToken(byte[] deviceToken, bool isProductionEnvironment)
		{
		}

		public static void showInvitationInbox()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showInvitationInbox");
			}
		}

		public static void startQuickMatch(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0, int variant = 1)
		{
			createRoomProgrammatically(minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask, variant);
		}

		public static void createRoomProgrammatically(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0, int variant = 1)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("createRoomProgrammatically", minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask, variant);
			}
		}

		public static void showPlayerSelector(int minPlayers, int maxPlayers, int variant = 1)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showPlayerSelector", minPlayers, maxPlayers, variant);
			}
		}

		public static void joinRoomWithInvitation(string invitationId)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("joinRoomWithInvitation", invitationId);
			}
		}

		public static void showWaitingRoom(int minParticipantsToStart)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("showWaitingRoom", minParticipantsToStart);
			}
		}

		public static void leaveRoom()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("leaveRoom");
			}
		}

		public static GPGRoom getRoom()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return new GPGRoom();
			}
			string json = _plugin.Call<string>("getRoom", new object[0]);
			return Json.decode<GPGRoom>(json);
		}

		public static List<GPGMultiplayerParticipant> getParticipants(bool ignoreCurrentPlayer = false)
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return null;
			}
			string json = _plugin.Call<string>("getParticipants", new object[1] { ignoreCurrentPlayer });
			return Json.decode<List<GPGMultiplayerParticipant>>(json);
		}

		public static string getCurrentPlayerParticipantId()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return null;
			}
			return _plugin.Call<string>("getCurrentPlayerParticipantId", new object[0]);
		}

		public static void sendReliableRealtimeMessage(string participantId, byte[] message)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("sendReliableRealtimeMessage", participantId, message);
			}
		}

		public static void sendReliableRealtimeMessageToAll(byte[] message)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("sendReliableRealtimeMessageToAll", message);
			}
		}

		public static void sendUnreliableRealtimeMessage(string participantId, byte[] message)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("sendUnreliableRealtimeMessage", participantId, message);
			}
		}

		public static void sendUnreliableRealtimeMessageToAll(byte[] message)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				_plugin.Call("sendUnreliableRealtimeMessageToAll", message);
			}
		}
	}
}
