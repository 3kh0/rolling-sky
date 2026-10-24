using UnityEngine;

public class DebugTimer : MonoBehaviour
{
	private const string version = "O";

	private void Awake()
	{
		base.gameObject.GetComponent<TextMesh>().text = Benchmarking.GetSecondsTimed().ToString("0.00") + " " + "O";
		Benchmarking.TryDebugSecondsTimed("Debug Scene Start");
	}
}
