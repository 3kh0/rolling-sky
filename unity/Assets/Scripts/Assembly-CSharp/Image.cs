using UnityEngine;

public class Image : MonoBehaviour
{
	private const string classCode = "IMG";

	public const string colorName = "_TintColor";

	public const string textureName = "_MainTex";

	public const string crossName = "Cross";

	public const float fadeTime = 1f;

	protected const float textSizeNormal = 1f;

	protected const float textSizeHover = 1.1f;

	protected const float textSizePressed = 1.1f;

	protected const float textSizeLocked = 1f;

	protected const bool textBoldNormal = false;

	protected const bool textBoldHover = true;

	protected const bool textBoldPressed = true;

	protected const bool textBoldLocked = false;

	protected static Color textColorNormal = new Color(0f, 0f, 0f, 1f);

	protected static Color textColorHover = new Color(0f, 0f, 0f, 1f);

	protected static Color textColorPressed = new Color(1f, 1f, 1f, 1f);

	protected static Color textColorLocked = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	protected Fade fade;

	protected Fade crossFade;

	private Texture lastTexture;

	private Color colorDefault;

	private string lastText;

	public bool imageConfigured { get; private set; }

	public bool fadable { get; private set; }

	public bool isText { get; private set; }

	public bool locked { get; protected set; }

	public bool hidden
	{
		get
		{
			return fade.hidden;
		}
	}

	public bool shown
	{
		get
		{
			return fade.shown;
		}
	}

	public bool fadingOut
	{
		get
		{
			return fade.fadingOut;
		}
	}

	public bool fadingIn
	{
		get
		{
			return fade.fadingIn;
		}
	}

	private void Awake()
	{
		ConfigureImage();
	}

	public void ConfigureImage()
	{
		if (imageConfigured)
		{
			return;
		}
		TextMesh component = base.gameObject.GetComponent<TextMesh>();
		Renderer component2 = base.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			bool flag = (isText = true);
			fadable = flag;
		}
		else if (component2 != null)
		{
			fadable = component2.material.HasProperty("_TintColor");
		}
		else
		{
			Debug.Log(string.Format("Error INI_RNF - unable to find Renderer or Text Mesh on Image transform '{0}'", base.transform.name));
		}
		if (fadable)
		{
			GameObject gameObject;
			Transform transform;
			Renderer component3;
			if (isText)
			{
				gameObject = new GameObject();
				transform = gameObject.transform;
				TextMesh textMesh = gameObject.AddComponent<TextMesh>();
				textMesh.offsetZ = component.offsetZ;
				textMesh.characterSize = component.characterSize;
				textMesh.lineSpacing = component.lineSpacing;
				textMesh.anchor = component.anchor;
				textMesh.alignment = component.alignment;
				textMesh.tabSize = component.tabSize;
				textMesh.fontSize = component.fontSize;
				textMesh.fontStyle = component.fontStyle;
				textMesh.richText = component.richText;
				textMesh.font = component.font;
				component3 = gameObject.GetComponent<MeshRenderer>();
				colorDefault = component.color;
				textMesh.color = colorDefault;
			}
			else
			{
				gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
				transform = gameObject.transform;
				component3 = gameObject.GetComponent<MeshRenderer>();
				colorDefault = component2.material.GetColor("_TintColor");
				component3.material.SetColor("_TintColor", colorDefault);
			}
			Object.Destroy(gameObject.GetComponent<Collider>());
			transform.name = base.gameObject.name + "Cross";
			transform.parent = base.transform;
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
			component3.castShadows = component2.castShadows;
			component3.receiveShadows = component2.receiveShadows;
			component3.material = component2.material;
			crossFade = gameObject.AddComponent<Fade>();
			crossFade.Configure(colorDefault, isText, fadable);
			crossFade.Hide();
			lastTexture = component2.material.GetTexture("_MainTex");
		}
		fade = base.gameObject.AddComponent<Fade>();
		fade.Configure(colorDefault, isText, fadable);
		imageConfigured = true;
	}

	public void Hide()
	{
		fade.Hide();
		if (isText)
		{
			lastText = string.Empty;
		}
		else
		{
			lastTexture = null;
		}
	}

	public void Show()
	{
		fade.Show();
	}

	public void FadeIn()
	{
		if (fadable)
		{
			fade.FadeIn();
		}
		else
		{
			fade.Show();
		}
	}

	public void FadeOut()
	{
		if (fadable)
		{
			fade.FadeOut();
		}
		else
		{
			fade.Hide();
		}
		if (isText)
		{
			lastText = string.Empty;
		}
		else
		{
			lastTexture = null;
		}
	}

	public void SetTexture(Texture texture)
	{
		base.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		lastTexture = texture;
	}

	public void ChangeTexture(Texture texture)
	{
		if (fadable)
		{
			if (hidden || lastTexture == null)
			{
				SetTexture(texture);
				fade.FadeIn();
			}
			else
			{
				crossFade.GetComponent<Renderer>().material.SetTexture("_MainTex", lastTexture);
				SetTexture(texture);
				crossFade.Show();
				crossFade.FadeOut();
				fade.Hide();
				fade.FadeIn();
			}
			lastTexture = texture;
		}
		else
		{
			if (hidden)
			{
				fade.Show();
			}
			SetTexture(texture);
		}
	}

	public void SetText(string text)
	{
		fade.SetText(text);
		lastText = text;
	}

	public void CenterText()
	{
		fade.CenterText();
	}

	public void ChangeText(string text)
	{
		if (fadable)
		{
			crossFade.SetText(lastText);
			fade.SetText(text);
			crossFade.Show();
			crossFade.FadeOut();
			fade.FadeIn();
			lastText = text;
		}
		else
		{
			SetText(text);
		}
	}

	public void Lock()
	{
		if (!locked)
		{
			fade.SetColor(textColorLocked);
			fade.SetTextFormat(textColorLocked, 1f, false);
			locked = true;
		}
	}

	public void Unlock()
	{
		if (locked)
		{
			fade.ResetColor();
			fade.SetTextFormat(textColorNormal, 1f, false);
			locked = false;
		}
	}
}
