using UnityEngine;

// The original CameraPostEffectManager disables its high-end grain/retro effects.
// FilmGrain's low-end binding is GrayscaleEffect, not NoiseEffect. Retro is a
// deliberate no-op in this donor. ColorMonitor does run on Low and High quality.
// See analysis/classic-themes/ and analysis/expansion-themes/ for original GLES.
[RequireComponent(typeof(Camera))]
public class ClassicPostEffects : MonoBehaviour
{
	private Material grayscaleMaterial;
	private Material greenMaterial;
	private Material colorMaterial;
	private bool grayscale;
	private bool green;
	private bool colorMonitor;
	// Camera prefab component 81, not ColorMonitor's source-code defaults.
	private Color monitorColor = new Color(1f, 0f, 0.8857936859130859f, 1f);
	private float monitorGreen = 0.5f;
	private float monitorBackground = 0.2f;
	private float monitorIntensity = 1f;

	public static void ApplyTheme(bool initializing)
	{
		Camera camera = Camera.main;
		if (camera == null) return;
		ClassicPostEffects effect = camera.GetComponent<ClassicPostEffects>();
		// Menu scenes also use MaterialManager. Never attach a post effect there,
		// or on any of the accepted first five worlds.
		if (!LevelDesigner.IsClassicWorld || Object.FindObjectOfType<GameManager>() == null)
		{
			if (effect != null) effect.enabled = false;
			return;
		}
		bool quality = DeviceQualityChecker.QualityIsLow() || DeviceQualityChecker.QualityIsHigh();
		// The donor's initial/checkpoint setup calls FilmGrain for GreenMonitor;
		// normal transitions call GreenMonitor. Preserve that distinction.
		bool useGrayscale = quality && (LevelDesigner.FilmGrainEffect || (initializing && LevelDesigner.GreenMonitorEffect));
		bool useGreen = quality && !initializing && LevelDesigner.GreenMonitorEffect;
		bool useColor = quality && LevelDesigner.ColorMonitorEffect;
		if (effect == null && (useGrayscale || useGreen || useColor)) effect = camera.gameObject.AddComponent<ClassicPostEffects>();
		if (effect == null) return;
		effect.grayscale = useGrayscale;
		effect.green = useGreen;
		effect.colorMonitor = useColor;
		if (useColor) effect.ConfigureColorMonitor(LevelDesigner.CurrentDonorThemeNumber);
		effect.enabled = (useGrayscale || useGreen || useColor) && SystemInfo.supportsImageEffects;
		if ((useGrayscale || useGreen || useColor) && !SystemInfo.supportsImageEffects)
			Debug.LogWarning("Classic1471: this platform does not support the donor camera image effects.");
	}

	private void ConfigureColorMonitor(int donorTheme)
	{
		// Exact CameraPostEffectManager.TurnOnColorMonitor overrides. Unlisted
		// themes retain component state, just as the donor does after TurnOff().
		if (donorTheme == 16 || donorTheme == 17 || donorTheme == 19 || donorTheme == 20)
		{
			monitorGreen = 0.43f;
			monitorBackground = 0f;
			monitorIntensity = donorTheme == 20 ? 0.75f : 0.679f;
			monitorColor = new Color(1f, donorTheme == 16 ? 63f / 85f : donorTheme == 19 ? 0.5568628f : donorTheme == 20 ? 0.30980393f : 0f, 0f, 1f);
		}
		if (donorTheme == 22)
		{
			monitorColor = new Color(1f, 69f / 85f, 0.47843137f, 1f);
			monitorGreen = 0.5f;
			monitorBackground = 0.054f;
			monitorIntensity = 0.5f;
		}
	}

	public static void TurnOff()
	{
		if (Camera.main == null) return;
		ClassicPostEffects effect = Camera.main.GetComponent<ClassicPostEffects>();
		if (effect != null) effect.enabled = false;
	}

	private static Material LoadEffect(string resource)
	{
		Shader shader = Resources.Load<Shader>("Classic1471/Themes/Shaders/" + resource);
		if (shader == null || !shader.isSupported)
		{
			Debug.LogError("Classic1471: missing or unsupported original-effect shader " + resource);
			return null;
		}
		Material material = new Material(shader);
		material.hideFlags = HideFlags.HideAndDontSave;
		return material;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (colorMonitor && colorMaterial == null) colorMaterial = LoadEffect("ClassicColorMonitor");
		if (colorMonitor && colorMaterial != null)
		{
			colorMaterial.SetColor("_MainColor", monitorColor);
			colorMaterial.SetFloat("_GreenIntensity", monitorGreen);
			colorMaterial.SetFloat("_BackgroundIntensity", monitorBackground);
			colorMaterial.SetFloat("_EffectIntensity", monitorIntensity);
		}
		if (grayscale && grayscaleMaterial == null)
		{
			grayscaleMaterial = LoadEffect("ClassicGrayscale");
			if (grayscaleMaterial != null)
			{
				// Camera prefab 86 serializes textureRamp=null and rampOffset=0.
				// Preserve the original shader's grayscaleRamp default, not a guessed LUT.
				grayscaleMaterial.SetTexture("_RampTex", null);
				grayscaleMaterial.SetFloat("_RampOffset", 0f);
			}
		}
		if (green && greenMaterial == null)
		{
			greenMaterial = LoadEffect("ClassicGreenMonitor");
			if (greenMaterial != null)
			{
				// Camera prefab 89 values, not GreenMonitor's source-code defaults.
				greenMaterial.SetFloat("_GreenIntensity", 0.5f);
				greenMaterial.SetFloat("_BackgroundIntensity", 0.2f);
				greenMaterial.SetFloat("_EffectIntensity", 1f);
			}
		}
		if ((grayscale && grayscaleMaterial == null) || (green && greenMaterial == null) || (colorMonitor && colorMaterial == null))
		{
			Graphics.Blit(source, destination);
			enabled = false;
			return;
		}
		RenderTexture colorInput = null;
		RenderTexture finalDestination = destination;
		if (colorMonitor)
		{
			// Serialized camera component order: Grayscale, GreenMonitor, ColorMonitor.
			if (!grayscale && !green)
			{
				Graphics.Blit(source, destination, colorMaterial);
				return;
			}
			colorInput = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			destination = colorInput;
		}
		if (grayscale && green)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			Graphics.Blit(source, temporary, grayscaleMaterial);
			Graphics.Blit(temporary, destination, greenMaterial);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else if (grayscale || green)
		{
			Graphics.Blit(source, destination, green ? greenMaterial : grayscaleMaterial);
		}
		else Graphics.Blit(source, destination);
		if (colorInput != null)
		{
			Graphics.Blit(colorInput, finalDestination, colorMaterial);
			RenderTexture.ReleaseTemporary(colorInput);
		}
	}

	private void OnDisable()
	{
		if (grayscaleMaterial != null) Object.DestroyImmediate(grayscaleMaterial);
		if (greenMaterial != null) Object.DestroyImmediate(greenMaterial);
		if (colorMaterial != null) Object.DestroyImmediate(colorMaterial);
		grayscaleMaterial = null;
		greenMaterial = null;
		colorMaterial = null;
	}
}
