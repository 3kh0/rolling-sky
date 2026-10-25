using UnityEngine;

namespace UGUI
{
	public class URealTime : MonoBehaviour
	{
		public static float time
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		public static float deltaTime
		{
			get
			{
				return Time.unscaledDeltaTime;
			}
		}
	}
}
