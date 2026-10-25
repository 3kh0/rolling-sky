using UnityEngine;

// The donor uses localized labels, not numbered WorldTitle textures. Reuse its
// original Sen-Bold font already bound to both scenes' percentage/gem text.
public static class ClassicWorldTitle
{
	private const string childName = "ClassicWorldTitle";

	public static bool Configure(Transform quad, TextMesh fontSource, string world, Color color)
	{
		int number;
		string title = int.TryParse(world, out number) && number >= 1 && number <= LevelCatalog.Count
			? LevelCatalog.Get(number).Title.ToUpperInvariant() : null;
		Transform child = quad.Find(childName);
		quad.GetComponent<Renderer>().enabled = title == null;
		if (child != null) child.gameObject.SetActive(title != null);
		if (title == null) return false;
		if (child == null)
		{
			GameObject label = new GameObject(childName, typeof(TextMesh));
			label.layer = quad.gameObject.layer;
			child = label.transform;
			child.SetParent(quad, false);
		}
		TextMesh text = child.GetComponent<TextMesh>();
		text.font = fontSource.font;
		text.fontSize = 100;
		text.fontStyle = FontStyle.Normal;
		text.characterSize = 0.1f;
		text.anchor = TextAnchor.MiddleCenter;
		text.alignment = TextAlignment.Center;
		text.color = color;
		text.text = title;
		Renderer renderer = child.GetComponent<Renderer>();
		renderer.sharedMaterial = text.font.material;
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		renderer.receiveShadows = false;
		text.font.RequestCharactersInTexture(title, text.fontSize, text.fontStyle);
		float width = 0f;
		float top = 0f;
		float bottom = 0f;
		foreach (char character in title)
		{
			CharacterInfo info;
			if (!text.font.GetCharacterInfo(character, out info, text.fontSize, text.fontStyle))
				throw new System.InvalidOperationException("Missing original title glyph: " + character);
			width += info.advance;
			top = Mathf.Max(top, info.maxY);
			bottom = Mathf.Min(bottom, info.minY);
		}
		Vector3 size = quad.GetComponent<MeshFilter>().sharedMesh.bounds.size;
		Vector3 quadScale = quad.localScale;
		// Cancel the title quad's nonuniform scaling so letters are not stretched.
		float scale = Mathf.Min(size.x * Mathf.Abs(quadScale.x) / width,
			size.y * Mathf.Abs(quadScale.y) / (top - bottom)) * 0.9f / (text.characterSize * 0.1f);
		child.localScale = new Vector3(scale / quadScale.x, scale / quadScale.y, 1f);
		child.localPosition = Vector3.zero;
		child.localRotation = Quaternion.identity;
		return true;
	}

	public static void SetAlpha(Transform quad, float alpha)
	{
		Transform child = quad.Find(childName);
		if (child == null) return;
		TextMesh text = child.GetComponent<TextMesh>();
		Color color = text.color;
		color.a = alpha;
		text.color = color;
	}
}
