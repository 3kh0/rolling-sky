using UnityEngine;

public class GemsManager : MonoBehaviour
{
	private static PersistantSecured.Int gems = new PersistantSecured.Int("GFX", 0);

	public static void AddGem()
	{
		gems.Append();
	}

	public static int GetGemsCollected()
	{
		return gems.Get();
	}
}
