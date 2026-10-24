using System;
using System.Collections;

namespace UnityEngine.Advertisements
{
	internal class AsyncExec
	{
		private static GameObject asyncExecGameObject;

		private static MonoBehaviour coroutineHost;

		private static AsyncExec asyncImpl;

		private static bool init;

		private static MonoBehaviour getImpl()
		{
			if (!init)
			{
				asyncImpl = new AsyncExec();
				GameObject gameObject = new GameObject("Unity Ads Coroutine Host");
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				asyncExecGameObject = gameObject;
				coroutineHost = asyncExecGameObject.AddComponent<MonoBehaviour>();
				Object.DontDestroyOnLoad(asyncExecGameObject);
				init = true;
			}
			return coroutineHost;
		}

		private static AsyncExec getAsyncImpl()
		{
			if (!init)
			{
				getImpl();
			}
			return asyncImpl;
		}

		public static void runWithCallback<K, T>(Func<K, Action<T>, IEnumerator> asyncMethod, K arg0, Action<T> callback)
		{
			getImpl().StartCoroutine(asyncMethod(arg0, callback));
		}
	}
}
