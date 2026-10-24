using UnityEngine;
using UnityEngine.UI;

public class BlockPrototype : MonoBehaviour
{
	public Transform masterBlock;

	public Transform masterDebugMarker;

	public Material markerMaterial1;

	public Material markerMaterial2;

	public Text debugText;

	private void Start()
	{
		BlockManager.Initialize(masterBlock, masterDebugMarker, markerMaterial1, markerMaterial2);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			BlockManager.MouseImpact();
		}
		BlockManager.UpdateBlocks();
		debugText.text = BlockManager.TotalActive.ToString();
	}
}
