using UnityEngine;

public class WorldDisplay : MonoBehaviour
{
	private const string overlayName = "_Ammount";

	private const float overlayAmmount = 0.5f;

	private const float oneDigitGemsTotalX = 0.34f;

	private const float twoDigitGemsTotalX = 0.664f;

	private const float oneDigitGemsIconX = -0.93f;

	private const float twoDigitGemsIconX = -1.242f;

	public Renderer border;

	public Renderer padlock;

	public Renderer infinity;

	public TextMesh worldPercent;

	public TextMesh worldPercentOutline;

	public TextMesh worldGems;

	public TextMesh worldGemsOutline;

	public TextMesh worldGemsTotal;

	public TextMesh worldGemsTotalOutline;

	public Renderer worldGemsIcon;

	public Renderer worldGemsIconOutline;

	public Transform worldGemsGroup;

	public Transform worldTitleQuad;

	private void LoadTextureNativePNG(string resourceFilePath, Material targetMaterial)
	{
		Texture2D mainTexture = Resources.Load(resourceFilePath) as Texture2D;
		targetMaterial.mainTexture = mainTexture;
	}

	public void Configure(string name, Color lightColor, Color lightAltColor, Color midColor, Color midAltColor, Color darkColor, int progressPercent, int gemsEarned, int gemsMax, bool startLocked, bool isEndless)
	{
		worldPercent.color = lightAltColor;
		worldGems.color = midAltColor;
		int crownWorldNumber;
		if (int.TryParse(name, out crownWorldNumber) && crownWorldNumber >= 1 && crownWorldNumber <= LevelCatalog.Count)
			ClassicCrownHud.Configure(worldGems, crownWorldNumber, ProgressManager.GetCrowns(crownWorldNumber - 1), !startLocked && !isEndless);
		string text = name.ToUpperInvariant();
		if (!ClassicWorldTitle.Configure(worldTitleQuad, worldPercent, name, lightAltColor))
			LoadTextureNativePNG(string.Format("WorldTitle{0}", name), worldTitleQuad.GetComponent<Renderer>().material);
		if (startLocked)
		{
			TransformUtils.Show(padlock);
			SetOverlay(0.5f);
		}
		else
		{
			TransformUtils.Hide(padlock);
			SetOverlay(0f);
		}
		string text2 = ((!startLocked && !isEndless) ? (progressPercent + "%") : null);
		if (startLocked)
		{
			int worldNumber;
			if (int.TryParse(name, out worldNumber))
			{
				int? parent = LevelCatalog.Get(worldNumber).UnlockParentWorldNumber;
				if (parent.HasValue)
				{
					text2 = "BEAT " + LevelCatalog.Get(parent.Value).Title.ToUpperInvariant();
					float size = 3f / text2.Length;
					worldPercent.characterSize *= size;
					worldPercentOutline.characterSize *= size;
				}
			}
		}
		worldPercent.text = text2;
		worldPercentOutline.text = text2;
		if (isEndless)
		{
			TransformUtils.Show(infinity);
			infinity.material.SetColor("_ColorR", lightAltColor);
			TransformUtils.Hide(worldGemsGroup);
			return;
		}
		TransformUtils.Hide(infinity);
		worldGemsIcon.material.color = midAltColor;
		worldGems.color = midAltColor;
		worldGemsTotal.color = midAltColor;
		string text3 = gemsEarned.ToString();
		worldGems.text = text3;
		worldGemsOutline.text = text3;
		string text4 = "/" + gemsMax;
		worldGemsTotal.text = text4;
		worldGemsTotalOutline.text = text4;
		bool flag = gemsEarned >= 10;
		float newX = ((!flag) ? 0.34f : 0.664f);
		float newX2 = ((!flag) ? (-0.93f) : (-1.242f));
		TransformUtils.SetX(worldGemsTotal, newX, true);
		TransformUtils.SetX(worldGemsIcon, newX2, true);
	}

	public void SetTexture(Texture worldImage)
	{
		base.GetComponent<Renderer>().material.mainTexture = worldImage;
	}

	public void SetOverlay(float overlayPercent)
	{
		base.GetComponent<Renderer>().material.SetFloat("_Ammount", overlayPercent);
	}
}
