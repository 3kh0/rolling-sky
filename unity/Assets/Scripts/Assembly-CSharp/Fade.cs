using System.Collections.Generic;
using UnityEngine;

public class Fade : FadeCtrl
{
	private enum TextCmd
	{
		Set = 0,
		Center = 1
	}

	private const string classCode = "FDE";

	public const float fadeTime = 1f;

	private const string colorName = "_TintColor";

	private const string textureName = "_MainTex";

	private List<TextMesh> textMeshs;

	private float fontSizeDefault;

	private Color color;

	private Color colorDefault;

	public bool isText { get; private set; }

	public void Configure(Color colorDefault, bool isText, bool fadable = true)
	{
		this.colorDefault = colorDefault;
		this.isText = isText;
		base.fadable = fadable;
		if (isText)
		{
			textMeshs = new List<TextMesh>();
			TextMesh[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextMesh>();
			foreach (TextMesh item in componentsInChildren)
			{
				textMeshs.Add(item);
			}
			fontSizeDefault = textMeshs[0].fontSize;
		}
		color = colorDefault;
		state = State.Shown;
	}

	public void SetColor(Color color)
	{
		this.color = color;
	}

	public void ResetColor()
	{
		color = colorDefault;
	}

	public override void SetFade(float fadeAmnt)
	{
		Color color = this.color;
		color.a = fadeAmnt;
		if (isText)
		{
			foreach (TextMesh textMesh in textMeshs)
			{
				textMesh.color = color;
			}
			return;
		}
		base.GetComponent<Renderer>().material.SetColor("_TintColor", color);
	}

	public void SetTextFormat(Color textColor, float textSize, bool textBold)
	{
		ColorText(textColor);
		SizeText(textSize);
		BoldText(textBold);
	}

	public void SetText(string text, int textPart = 0)
	{
		AlterText(text, textPart, TextCmd.Set);
	}

	public void CenterText(int textPart = 0)
	{
		AlterText(null, textPart, TextCmd.Center);
	}

	private void AlterText(string text, int textPart, TextCmd cmd)
	{
		if (textPart < 0 || textPart >= textMeshs.Count)
		{
			Debug.LogError(string.Format("Error STX_UTP - attempt to {0} text on text part {1} out of available range 0 to {2} for fade element {3}", cmd, textPart, textMeshs.Count - 1, base.gameObject.name));
			textPart = ((textPart >= 0) ? (textMeshs.Count - 1) : 0);
		}
		if (!(textMeshs[textPart] == null))
		{
			switch (cmd)
			{
			case TextCmd.Set:
				textMeshs[textPart].text = text;
				break;
			case TextCmd.Center:
				textMeshs[textPart].alignment = TextAlignment.Center;
				break;
			default:
				Debug.LogError(string.Format("Error STX_UCD - unexpected TextCmd type cmd of {0} sent to script Fade.AlterText for gameObject {1}", cmd, base.gameObject.name));
				textMeshs[textPart].text = text;
				break;
			}
		}
	}

	private void ColorText(Color color)
	{
		foreach (TextMesh textMesh in textMeshs)
		{
			textMesh.color = color;
		}
	}

	private void SizeText(float size)
	{
		if (!(textMeshs[0] == null))
		{
			textMeshs[0].fontSize = (int)(fontSizeDefault * size);
		}
	}

	private void BoldText(bool bold = true)
	{
		if (!(textMeshs[0] == null))
		{
			if (bold)
			{
				textMeshs[0].fontStyle = FontStyle.Bold;
			}
			else
			{
				textMeshs[0].fontStyle = FontStyle.Normal;
			}
		}
	}
}
