using System;

namespace UnityEngine.Advertisements.Optional
{
	public class ShowOptionsExtended : ShowOptions
	{
		[Obsolete("Please use gamerSid on ShowOptions instead of ShowOptionsExtended")]
		public new string gamerSid { get; set; }
	}
}
