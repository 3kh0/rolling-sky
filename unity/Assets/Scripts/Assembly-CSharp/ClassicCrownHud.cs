using UnityEngine;

// Crown counters reuse the scene's original Sen-Bold TextMesh, not a new font,
// icon font, Canvas or dependency. Parenting to the existing label also preserves
// the HUD's pause/game-over visibility, menu paging and aspect-ratio placement.
public static class ClassicCrownHud
{
	private const string childName = "ClassicCrownCount";

	public static TextMesh Configure(TextMesh source, int worldNumber, int collected, bool visible)
	{
		int maximum = LevelCatalog.Get(worldNumber).MaxCrowns;
		Transform child = source.transform.Find(childName);
		if (maximum == 0)
		{
			if (child != null) child.gameObject.SetActive(false);
			return null;
		}
		if (child == null)
		{
			GameObject label = new GameObject(childName, typeof(TextMesh));
			label.layer = source.gameObject.layer;
			child = label.transform;
			child.SetParent(source.transform, false);
		}
		TextMesh text = child.GetComponent<TextMesh>();
		text.font = source.font;
		text.fontSize = source.fontSize;
		text.fontStyle = source.fontStyle;
		text.characterSize = source.characterSize * 0.45f;
		text.anchor = source.anchor;
		text.alignment = source.alignment;
		text.color = source.color;
		child.localPosition = new Vector3(0f, -0.85f, 0f);
		child.localRotation = Quaternion.identity;
		child.localScale = Vector3.one;
		Renderer renderer = child.GetComponent<Renderer>();
		renderer.sharedMaterial = source.font.material;
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		renderer.receiveShadows = false;
		Display(text, collected, maximum);
		child.gameObject.SetActive(visible);
		return text;
	}

	public static void Display(TextMesh text, int collected, int maximum)
	{
		if (text != null) text.text = "CROWNS " + Mathf.Clamp(collected, 0, maximum) + "/" + maximum;
	}
}
