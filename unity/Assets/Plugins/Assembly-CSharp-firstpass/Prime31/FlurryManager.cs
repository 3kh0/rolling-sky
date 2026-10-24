using System;

namespace Prime31
{
	public class FlurryManager : AbstractManager
	{
		public static event Action<string> spaceDidDismissEvent;

		public static event Action<string> spaceWillLeaveApplicationEvent;

		public static event Action<string> spaceDidFailToRenderEvent;

		public static event Action<string> spaceDidFailToReceiveAdEvent;

		public static event Action<string> spaceDidReceiveAdEvent;

		public static event Action<string> videoDidFinishEvent;

		static FlurryManager()
		{
			AbstractManager.initialize(typeof(FlurryManager));
		}

		private void spaceDidDismiss(string space)
		{
			if (FlurryManager.spaceDidDismissEvent != null)
			{
				FlurryManager.spaceDidDismissEvent(space);
			}
		}

		private void spaceWillLeaveApplication(string space)
		{
			if (FlurryManager.spaceWillLeaveApplicationEvent != null)
			{
				FlurryManager.spaceWillLeaveApplicationEvent(space);
			}
		}

		private void spaceDidFailToRender(string space)
		{
			if (FlurryManager.spaceDidFailToRenderEvent != null)
			{
				FlurryManager.spaceDidFailToRenderEvent(space);
			}
		}

		private void spaceDidFailToReceiveAd(string space)
		{
			if (FlurryManager.spaceDidFailToReceiveAdEvent != null)
			{
				FlurryManager.spaceDidFailToReceiveAdEvent(space);
			}
		}

		private void spaceDidReceiveAd(string space)
		{
			if (FlurryManager.spaceDidReceiveAdEvent != null)
			{
				FlurryManager.spaceDidReceiveAdEvent(space);
			}
		}

		private void videoDidFinish(string space)
		{
			if (FlurryManager.videoDidFinishEvent != null)
			{
				FlurryManager.videoDidFinishEvent(space);
			}
		}
	}
}
