using System;

namespace UnityEngine.Advertisements
{
	public class ShowOptions
	{
		[Obsolete("ShowOptions.pause is no longer supported and does nothing, video ads will always pause the game")]
		public bool pause { get; set; }

		public Action<ShowResult> resultCallback { get; set; }

		public string gamerSid { get; set; }
	}
}
