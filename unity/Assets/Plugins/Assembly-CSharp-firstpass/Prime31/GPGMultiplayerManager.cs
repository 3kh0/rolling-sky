using System;

namespace Prime31
{
	public class GPGMultiplayerManager : AbstractManager
	{
		private class GPGRoomUpdateInfo
		{
			public GPGRoom room { get; set; }

			public int statusCode { get; set; }

			public GPGRoomUpdateStatus status
			{
				get
				{
					return (GPGRoomUpdateStatus)statusCode;
				}
			}
		}

		public static event Action<string> onInvitationReceivedEvent;

		public static event Action<string> onInvitationRemovedEvent;

		public static event Action<bool> onWaitingRoomCompletedEvent;

		public static event Action<bool> onInvitationInboxCompletedEvent;

		public static event Action<bool> onInvitePlayersCompletedEvent;

		public static event Action<GPGRoom, GPGRoomUpdateStatus> onJoinedRoomEvent;

		public static event Action onLeftRoomEvent;

		public static event Action<GPGRoom, GPGRoomUpdateStatus> onRoomConnectedEvent;

		public static event Action<GPGRoom, GPGRoomUpdateStatus> onRoomCreatedEvent;

		public static event Action<string, byte[]> onRealTimeMessageReceivedEvent;

		public static event Action onConnectedToRoomEvent;

		public static event Action onDisconnectedFromRoomEvent;

		public static event Action<string> onP2PConnectedEvent;

		public static event Action<string> onP2PDisconnectedEvent;

		public static event Action<string> onPeerDeclinedEvent;

		public static event Action<string> onPeerInvitedToRoomEvent;

		public static event Action<string> onPeerJoinedEvent;

		public static event Action<string> onPeerLeftEvent;

		public static event Action<string> onPeerConnectedEvent;

		public static event Action<string> onPeerDisconnectedEvent;

		public static event Action<GPGRoom> onRoomAutoMatchingEvent;

		public static event Action<GPGRoom> onRoomConnectingEvent;

		static GPGMultiplayerManager()
		{
			AbstractManager.initialize(typeof(GPGMultiplayerManager));
		}

		private void onInvitationReceived(string invitationId)
		{
			GPGMultiplayerManager.onInvitationReceivedEvent.fire(invitationId);
		}

		private void onInvitationRemoved(string invitationId)
		{
			GPGMultiplayerManager.onInvitationRemovedEvent.fire(invitationId);
		}

		private void onWaitingRoomCompleted(string success)
		{
			GPGMultiplayerManager.onWaitingRoomCompletedEvent.fire(success == "1");
		}

		private void onInvitationInboxCompleted(string success)
		{
			GPGMultiplayerManager.onInvitationInboxCompletedEvent.fire(success == "1");
		}

		private void onInvitePlayersCompleted(string success)
		{
			GPGMultiplayerManager.onInvitePlayersCompletedEvent.fire(success == "1");
		}

		private void onJoinedRoom(string json)
		{
			GPGRoomUpdateInfo gPGRoomUpdateInfo = Json.decode<GPGRoomUpdateInfo>(json);
			GPGMultiplayerManager.onJoinedRoomEvent.fire(gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
		}

		private void onLeftRoom(string empty)
		{
			GPGMultiplayerManager.onLeftRoomEvent.fire();
		}

		private void onRoomConnected(string json)
		{
			GPGRoomUpdateInfo gPGRoomUpdateInfo = Json.decode<GPGRoomUpdateInfo>(json);
			GPGMultiplayerManager.onRoomConnectedEvent.fire(gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
		}

		private void onRoomCreated(string json)
		{
			GPGRoomUpdateInfo gPGRoomUpdateInfo = Json.decode<GPGRoomUpdateInfo>(json);
			GPGMultiplayerManager.onRoomCreatedEvent.fire(gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
		}

		public static void onRealTimeMessageReceived(string senderParticipantId, byte[] message)
		{
			if (GPGMultiplayerManager.onRealTimeMessageReceivedEvent != null)
			{
				GPGMultiplayerManager.onRealTimeMessageReceivedEvent(senderParticipantId, message);
			}
		}

		private void onConnectedToRoom(string empty)
		{
			GPGMultiplayerManager.onConnectedToRoomEvent.fire();
		}

		private void onDisconnectedFromRoom(string empty)
		{
			GPGMultiplayerManager.onDisconnectedFromRoomEvent.fire();
		}

		private void onP2PConnected(string participantId)
		{
			GPGMultiplayerManager.onP2PConnectedEvent.fire(participantId);
		}

		private void onP2PDisconnected(string participantId)
		{
			GPGMultiplayerManager.onP2PDisconnectedEvent.fire(participantId);
		}

		private void onPeerDeclined(string id)
		{
			GPGMultiplayerManager.onPeerDeclinedEvent.fire(id);
		}

		private void onPeerInvitedToRoom(string id)
		{
			GPGMultiplayerManager.onPeerInvitedToRoomEvent.fire(id);
		}

		private void onPeerJoined(string id)
		{
			GPGMultiplayerManager.onPeerJoinedEvent.fire(id);
		}

		private void onPeerLeft(string id)
		{
			GPGMultiplayerManager.onPeerLeftEvent.fire(id);
		}

		private void onPeerConnected(string id)
		{
			GPGMultiplayerManager.onPeerConnectedEvent.fire(id);
		}

		private void onPeerDisconnected(string id)
		{
			GPGMultiplayerManager.onPeerDisconnectedEvent.fire(id);
		}

		private void onRoomAutoMatching(string json)
		{
			GPGMultiplayerManager.onRoomAutoMatchingEvent.fire(Json.decode<GPGRoom>(json));
		}

		private void onRoomConnecting(string json)
		{
			GPGMultiplayerManager.onRoomConnectingEvent.fire(Json.decode<GPGRoom>(json));
		}
	}
}
