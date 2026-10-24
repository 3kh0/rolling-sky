namespace Prime31
{
	public class GPGRoom
	{
		public int autoMatchWaitEstimateSeconds { get; set; }

		public long creationTimestamp { get; set; }

		public string creatorId { get; set; }

		public string description { get; set; }

		public string roomId { get; set; }

		public int status { get; set; }

		public int variant { get; set; }

		public bool hasData
		{
			get
			{
				return !string.IsNullOrEmpty(roomId);
			}
		}

		public string statusString
		{
			get
			{
				switch (status)
				{
				case -1:
					return "ROOM_VARIANT_ANY";
				case 0:
					return "ROOM_STATUS_INVITING";
				case 1:
					return "ROOM_STATUS_AUTO_MATCHING";
				case 2:
					return "ROOM_STATUS_CONNECTING";
				case 3:
					return "ROOM_STATUS_ACTIVE";
				default:
					return "Unknown Status";
				}
			}
		}

		public override string ToString()
		{
			if (!hasData)
			{
				return "[GPGRoom] No data available. API returned a null room. This could mean the room is still in the connecting phast. If you continue to get a null room restarting your devices often fixes the issue.";
			}
			return string.Format("[GPGRoom] autoMatchWaitEstimateSeconds: {0}, creationTimestamp: {1}, creatorId: {2}, description: {3}, roomId: {4}, status: {5}, statusString: {6}", autoMatchWaitEstimateSeconds, creationTimestamp, creatorId, description, roomId, status, statusString);
		}
	}
}
