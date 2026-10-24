using UnityEngine;

public class MaterialManager : MonoBehaviour
{
	public struct TextureSet
	{
		public ArrayUtils.GeniList<Texture> Textures;

		public Texture this[MaterialName materialName]
		{
			get
			{
				int index = texturedMaterials.IndexOf(materialName);
				return Textures[index];
			}
		}

		public TextureSet(params Texture[] textures)
		{
			Textures = new ArrayUtils.GeniList<Texture>();
			for (int i = 0; i < textures.Length; i++)
			{
				Textures.Add(textures[i]);
			}
		}
	}

	public const int FogDistanceDefault = 25;

	public const float SliderBorder = 0.5f;

	public const int TilesPerColor = 80;

	public const int TilesPerColorShift = 5;

	public const float ScoreBrightness = 0.65f;

	public const int FirstColor = 0;

	private const float pickupGloss = 0.05f;

	private const string colorName = "_Color";

	private const string colorNameAlt = "_ColorAlt";

	private const string colorNameR = "_ColorR";

	private const string colorNameG = "_ColorG";

	private const string colorNameB = "_ColorB";

	private const string colorNameA = "_ColorA";

	private const string colorNameF = "_ColorF";

	private const string colorNameD = "_ColorD";

	private const string colorNameS = "_ColorS";

	private const string colorNameSa = "_ColorSa";

	private const string colorNameE = "_ColorE";

	private const string colorNameEAlt = "_ColorEAlt";

	private const string ammountName = "_Ammount";

	private const string specName = "_Specular";

	private const string glossName = "_Gloss";

	private const string alphaName = "_Alpha";

	private static ColorSet[] colorSets = new ColorSet[16]
	{
		new ColorSet(249, 183, 79, 250, 129, 48, 248, 183, 79, 248, 183, 79, 117, 207, 220, 117, 207, 220, 7, 145, 148, 206, 173, 79, 202, 253, 255, 0, 0, 0, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 47, 4, 255, 47, 4, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 216, 47, 0, 255, 255, 255, 122, 68, 68, 18, 217, 224, 231, 124, 114, 255, 255, 255, 0.3f, 0.1f, 5, 25),
		new ColorSet(238, 238, 255, 234, 134, 191, 241, 211, 242, 241, 211, 242, 235, 134, 192, 235, 134, 192, 130, 55, 126, 65, 0, 120, 202, 253, 255, 65, 0, 120, 0.65f, 255, 255, 255, 100, 255, 255, 204, 68, 255, 125, 76, 171, 175, 131, 255, 255, 255, 255, 255, 192, 255, 138, 84, 169, 210, 145, 255, 188, 132, 235, 4, 27, 255, 255, 255, 255, 122, 68, 68, 65, 0, 120, 215, 114, 239, 255, 255, 255, 1f, 0.1f, 5, 25),
		new ColorSet(104, 216, 103, 44, 163, 124, 132, 255, 130, 132, 255, 130, 54, 181, 152, 54, 181, 152, 25, 99, 91, 58, 129, 96, 202, 253, 255, 9, 64, 51, 0.25f, 255, 255, 255, 255, 255, 0, 255, 164, 50, 255, 98, 9, 255, 83, 19, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 121, 9, 255, 255, 222, 115, 76, 56, 0, 87, 67, 60, 190, 255, 255, 255, 255, 1.5f, 0.5f, 5, 25),
		new ColorSet(105, 181, 255, 255, 124, 230, 202, 248, 253, 202, 248, 253, 173, 238, 254, 75, 124, 174, 75, 124, 174, 117, 120, 142, 202, 253, 255, 34, 97, 152, 0.5f, 255, 255, 255, 255, 156, 255, 85, 50, 198, 20, 36, 51, 0, 81, 165, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 0, 0, 0, 255, 255, 255, 100, 100, 100, 255, 255, 255, 225, 225, 225, 255, 255, 255, 1.5f, 1f, 5, 45),
		new ColorSet(49, 47, 64, 249, 42, 42, 244, 41, 42, 246, 157, 16, 241, 41, 42, 241, 41, 42, 150, 29, 29, 15, 0, 0, 202, 253, 255, 15, 0, 0, 0.25f, 255, 247, 176, 255, 120, 42, 183, 39, 13, 255, 0, 0, 255, 255, 0, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 216, 112, 0, 255, 195, 0, 109, 13, 0, 35, 35, 42, 156, 42, 22, 167, 178, 255, 1f, 1.5f, 10, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 248, 183, 79, 248, 183, 79, 117, 207, 220, 117, 207, 220, 44, 145, 143, 206, 173, 79, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 47, 4, 255, 47, 4, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 47, 4, 255, 255, 255, 122, 68, 68, 18, 217, 224, 231, 124, 114, 255, 255, 255, 0.3f, 0.1f, 5, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 248, 183, 79, 248, 183, 79, 117, 207, 220, 117, 207, 220, 44, 145, 143, 150, 101, 39, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 0, 0, 255, 0, 55, 128, 0, 61, 139, 51, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 126, 0, 255, 253, 249, 112, 28, 28, 203, 83, 49, 255, 97, 26, 255, 179, 75, 1f, 0.25f, 10, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 241, 211, 242, 241, 211, 242, 235, 134, 192, 235, 134, 192, 130, 55, 126, 38, 114, 116, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 56, 15, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 126, 0, 255, 253, 249, 112, 28, 28, 203, 83, 49, 255, 97, 26, 255, 179, 75, 1f, 0.25f, 10, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 202, 248, 253, 173, 238, 254, 173, 238, 254, 75, 124, 174, 182, 31, 102, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 56, 15, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 248, 0, 255, 253, 249, 112, 28, 28, 11, 11, 10, 11, 11, 10, 255, 163, 163, 1f, 0.25f, 10, 25),
		// Exact donor colorSets[7..13], in runtime slots 10..16.
		new ColorSet(255, 128, 128, 177, 147, 107, 50, 50, 50, 255, 128, 128, 232, 194, 142, 232, 194, 142, 177, 147, 107, 50, 50, 50, 255, 255, 255, 0, 0, 0, 0.25f, 255, 255, 255, 255, 255, 128, 255, 128, 0, 139, 51, 51, 255, 179, 75, 255, 255, 255, 255, 128, 128, 255, 0, 0, 255, 64, 64, 255, 0, 0, 229, 165, 99, 255, 255, 0, 53, 0, 0, 128, 128, 128, 0, 0, 0, 255, 179, 75, 1f, 2f, 10, 25),
		new ColorSet(255, 128, 128, 177, 147, 107, 50, 50, 50, 255, 128, 128, 232, 194, 142, 232, 194, 142, 177, 147, 107, 50, 50, 50, 255, 255, 255, 0, 0, 0, 0.25f, 255, 255, 255, 255, 255, 128, 255, 128, 0, 139, 51, 51, 255, 179, 75, 255, 255, 255, 255, 128, 128, 255, 0, 0, 255, 64, 64, 255, 0, 0, 229, 156, 99, 255, 255, 0, 53, 0, 0, 0, 0, 0, 0, 0, 0, 255, 179, 75, 1f, 2f, 10, 25),
		new ColorSet(62, 90, 180, 176, 255, 254, 176, 255, 254, 176, 255, 254, 176, 255, 255, 62, 90, 180, 60, 70, 102, 38, 114, 116, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 210, 56, 210, 137, 213, 203, 255, 255, 255, 255, 213, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 211, 26, 219, 188, 0, 18, 18, 18, 45, 185, 173, 143, 207, 206, 255, 255, 255, 1.5f, 0.5f, 15, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 202, 248, 253, 173, 238, 254, 173, 238, 254, 75, 124, 174, 182, 31, 102, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 56, 15, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 249, 212, 0, 255, 255, 255, 124, 0, 0, 11, 11, 10, 11, 11, 10, 255, 163, 163, 1f, 0.25f, 10, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 202, 248, 253, 173, 238, 254, 173, 238, 254, 75, 124, 174, 182, 31, 102, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 56, 15, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 255, 185, 0, 255, 253, 249, 73, 1, 1, 125, 32, 186, 40, 20, 120, 255, 255, 255, 1f, 0.25f, 15, 25),
		new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 240, 250, 255, 184, 221, 255, 98, 217, 227, 28, 55, 139, 28, 55, 139, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 0, 0, 56, 106, 104, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 43, 57, 201, 200, 200, 200, 8, 86, 141, 40, 87, 183, 10, 42, 105, 255, 255, 255, 1f, 0.25f, 10, 30),
		new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 240, 250, 255, 184, 221, 255, 98, 217, 227, 28, 55, 139, 28, 55, 139, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 0, 0, 56, 106, 104, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 43, 57, 201, 200, 200, 200, 8, 86, 141, 40, 87, 183, 10, 42, 105, 255, 255, 255, 1f, 0.25f, 10, 30)
	};

	// Donor neutral theme 6 differs from the accepted neutral palette. It is used
	// only by new worlds, without changing colorSets[8] or its first-five callers.
	private static ColorSet classicNeutralColors = new ColorSet(249, 183, 79, 250, 129, 48, 202, 248, 253, 202, 248, 253, 173, 238, 254, 173, 238, 254, 75, 124, 174, 182, 31, 102, 202, 253, 255, 38, 114, 116, 0.25f, 255, 255, 255, 90, 255, 255, 55, 200, 255, 255, 56, 15, 255, 179, 75, 255, 255, 255, 255, 128, 0, 255, 0, 128, 255, 64, 64, 255, 0, 0, 249, 212, 0, 255, 255, 255, 124, 0, 0, 11, 11, 10, 11, 11, 10, 255, 163, 163, 1f, 0.25f, 10, 25);

	private static TextureSet classicNeutralTextures;

	private static ArrayUtils.Array<MaterialName> texturedMaterials = new ArrayUtils.Array<MaterialName>(MaterialName.BallQuad, MaterialName.BallTrail, MaterialName.Pickup, MaterialName.General, MaterialName.Fragile, MaterialName.FragileActive, MaterialName.Mover, MaterialName.MoverAuto, MaterialName.Enemy, MaterialName.Laser, MaterialName.Portal, MaterialName.Finish, MaterialName.Midground, MaterialName.Background);

	public Material ballMaterial;

	public Material ballQuadMaterial;

	public Material ballTrailMaterial;

	public Material ballInnerMaterial;

	public Material pickupMaterial;

	public Material generalMasterMaterial;

	public Material fragileMasterMaterial;

	public Material fragileActiveMasterMaterial;

	public Material moverMasterMaterial;

	public Material moverAutoMasterMaterial;

	public Material enemyMasterMaterial;

	public Material midgroundMaterial;

	public Material backgroundMaterial;

	public Material finishMaterial;

	public Material laserMaterial;

	public Material portalMaterial;

	public Material hudForegroundMaterial;

	public Material hudBackgroundMaterial;

	public Material inGameHudMaterial;

	public Material menuOverlayMaterial;

	public Material nextWorldRevealMaterial;

	public Shader unlitShader;

	public Shader unlitBallQuadShader;

	public Shader litBallGeoShader;

	public Shader litPickupShader;

	public Shader litEnemyShader;

	public Shader fullPickupShader;

	public Shader transparentShader;

	public Shader backgroundShaderNormal;

	public Shader backgroundShaderLerped;

	public Shader midgroundShaderNormal;

	public Shader midgroundShaderLerped;

	public Shader fullBallInnerShader;

	public Shader basicBallInnerShader;

	public Texture[] WorldBallQuadTextures;

	public Texture[] WorldGeneralTextures;

	public Texture[] WorldFragileTextures;

	public Texture[] WorldFragileActiveTextures;

	public Texture[] WorldMoverTextures;

	public Texture[] WorldMoverAutoTextures;

	public Texture[] WorldEnemyTextures;

	public Texture[] WorldBackgroundTextures;

	public Texture[] WorldMenuImages;

	private static ArrayUtils.SmartDict<MaterialName, ShaderPair> Shaders;

	private static bool isSetup;

	private static bool transitioning;

	private static int lastColorNum;

	private static int currentColorNum;

	private static MathUtils.Index colorNumNext;

	private static TextMesh score;

	private static Light mainLight;

	private static ShaderPair backgroundShader;

	private static ShaderPair midgroundShader;

	private static bool usingLitShaders;

	private static bool usingGeoBall;

	private static bool usingFullShaders;

	private static bool menuMode;

	public static ArrayUtils.SmartDict<MaterialName, Material> Materials { get; private set; }

	public static ArrayUtils.Array<TextureSet> WorldTextures { get; private set; }

	public static ArrayUtils.Array<Texture> WorldMenuTextures { get; private set; }

	private static ColorSet ColorsForTheme(int themeIndex)
	{
		if (themeIndex >= 16) EnsureExpansionColors();
		return themeIndex == 8 && (LevelDesigner.IsClassicWorld || menuMode) ? classicNeutralColors : colorSets[themeIndex];
	}

	private static ColorSet ColorsForWorld(int worldNumber)
	{
		int themeNumber = worldNumber >= 6 ? LevelDesigner.GetStartingThemeNumber(worldNumber) : worldNumber;
		if (themeNumber >= 17) EnsureExpansionColors();
		// Menu queries must not depend on the currently selected GameManager world.
		if (worldNumber >= 6 && themeNumber == 9) return classicNeutralColors;
		return colorSets[themeNumber - 1];
	}

	public static TextureSet GetWorldTextureSet(int themeIndex)
	{
		return themeIndex == 8 && (LevelDesigner.IsClassicWorld || menuMode) ? classicNeutralTextures : WorldTextures[themeIndex];
	}

	public static TextureSet GetWorldTextureSetForWorld(int worldIndex)
	{
		int themeIndex = LevelDesigner.GetMenuThemeIndex(worldIndex);
		return worldIndex >= 5 && themeIndex == 8 ? classicNeutralTextures : WorldTextures[themeIndex];
	}

	private static void EnsureExpansionColors()
	{
		if (colorSets.Length == LevelDesigner.ClassicThemeCount) return;
		ColorSet[] expanded = new ColorSet[LevelDesigner.ClassicThemeCount];
		System.Array.Copy(colorSets, expanded, colorSets.Length);
		for (int i = 16; i < expanded.Length; i++)
			expanded[i] = LoadClassicColors(LevelDesigner.DonorThemeNumber(i + 1), LevelDesigner.ClassicThemeResourceRoot(i + 1));
		colorSets = expanded;
	}

	private static ColorSet LoadClassicColors(int donorTheme, string resourceRoot)
	{
		TextAsset palette = Resources.Load<TextAsset>(resourceRoot + "/Themes/Palette" + donorTheme);
		if (palette == null) throw new System.InvalidOperationException("Missing original palette: " + donorTheme);
		string[] values = palette.text.Trim().Split(',');
		if (values.Length != 83) throw new System.InvalidOperationException("Invalid original palette: " + donorTheme);
		int[] c = new int[83];
		for (int i = 0; i < c.Length; i++)
			if (i != 30 && i != 79 && i != 80) c[i] = int.Parse(values[i], System.Globalization.CultureInfo.InvariantCulture);
		float alpha = float.Parse(values[30], System.Globalization.CultureInfo.InvariantCulture);
		float specular = float.Parse(values[79], System.Globalization.CultureInfo.InvariantCulture);
		float gloss = float.Parse(values[80], System.Globalization.CultureInfo.InvariantCulture);
		// Original ColorSet constructor order; its shader/material behavior is unchanged.
		return new ColorSet(c[0], c[1], c[2], c[3], c[4], c[5], c[6], c[7], c[8], c[9],
			c[10], c[11], c[12], c[13], c[14], c[15], c[16], c[17], c[18], c[19],
			c[20], c[21], c[22], c[23], c[24], c[25], c[26], c[27], c[28], c[29], alpha,
			c[31], c[32], c[33], c[34], c[35], c[36], c[37], c[38], c[39], c[40],
			c[41], c[42], c[43], c[44], c[45], c[46], c[47], c[48], c[49], c[50],
			c[51], c[52], c[53], c[54], c[55], c[56], c[57], c[58], c[59], c[60],
			c[61], c[62], c[63], c[64], c[65], c[66], c[67], c[68], c[69], c[70],
			c[71], c[72], c[73], c[74], c[75], c[76], c[77], c[78], specular, gloss, c[81], c[82]);
	}

	private static TextureSet LoadClassicTextures(int donorTheme, string resourceRoot = "Classic1471")
	{
		TextAsset binding = Resources.Load<TextAsset>(resourceRoot + "/Themes/Theme" + donorTheme);
		if (binding == null) throw new System.InvalidOperationException("Missing classic texture bindings: " + donorTheme);
		string[] paths = binding.text.Trim().Split('\n');
		if (paths.Length != 8) throw new System.InvalidOperationException("Invalid classic texture binding count: " + donorTheme);
		Texture[] textures = new Texture[8];
		for (int i = 0; i < paths.Length; i++)
		{
			textures[i] = Resources.Load<Texture2D>(paths[i].Trim());
			if (textures[i] == null) throw new System.InvalidOperationException("Missing classic texture: " + paths[i]);
		}
		return new TextureSet(textures[0], textures[6], textures[2], textures[1], textures[2], textures[3], textures[4], textures[5], textures[6], textures[6], textures[6], textures[3], textures[2], textures[7]);
	}

	public static void Initialize(int startingColorNum)
	{
		// This overload is the menu's world-number entry point.
		Initialize(LevelDesigner.GetMenuThemeIndex(startingColorNum - 1) + 1, null);
	}

	public static void Initialize(int startingColorNum, Transform scoreTextTransform)
	{
		if (!isSetup)
		{
			Debug.LogWarning("MTMN: ERROR: Attempt to Initialize() MaterialManager BEFORE it was setup in this scene");
		}
		menuMode = scoreTextTransform == null;
		currentColorNum = startingColorNum - 1;
		colorNumNext = new MathUtils.Index(startingColorNum <= 9 && !LevelDesigner.IsClassicWorld ? 9 : startingColorNum <= 16 && GameManager.WorldIndex < 8 ? 16 : LevelDesigner.ClassicThemeCount, currentColorNum);
		if (scoreTextTransform != null)
		{
			score = scoreTextTransform.GetComponent<TextMesh>();
		}
		mainLight = Camera.main.GetComponentInChildren<Light>();
		EndRecolor();
		Materials[MaterialName.Ball].SetFloat("_Ammount", 0f);
		ClassicPostEffects.ApplyTheme(true);
	}

	private void OnEnable()
	{
		TrySetup();
	}

	private void TrySetup()
	{
		if (isSetup)
		{
			return;
		}
		Materials = new ArrayUtils.SmartDict<MaterialName, Material>();
		Materials.Add(MaterialName.General, generalMasterMaterial);
		Materials.Add(MaterialName.Fragile, fragileMasterMaterial);
		Materials.Add(MaterialName.FragileActive, fragileActiveMasterMaterial);
		Materials.Add(MaterialName.Mover, moverMasterMaterial);
		Materials.Add(MaterialName.MoverAuto, moverAutoMasterMaterial);
		Materials.Add(MaterialName.Enemy, enemyMasterMaterial);
		Materials.Add(MaterialName.Midground, midgroundMaterial);
		Materials.Add(MaterialName.Background, backgroundMaterial);
		Materials.Add(MaterialName.Pickup, pickupMaterial);
		Materials.Add(MaterialName.Finish, finishMaterial);
		Materials.Add(MaterialName.Laser, laserMaterial);
		Materials.Add(MaterialName.Portal, portalMaterial);
		Materials.Add(MaterialName.HudForeground, hudForegroundMaterial);
		Materials.Add(MaterialName.HudBackground, hudBackgroundMaterial);
		Materials.Add(MaterialName.InGameHud, inGameHudMaterial);
		Materials.Add(MaterialName.MenuOverlay, menuOverlayMaterial);
		Materials.Add(MaterialName.NextWorldReveal, nextWorldRevealMaterial);
		Materials.Add(MaterialName.Ball, ballMaterial);
		Materials.Add(MaterialName.BallQuad, ballQuadMaterial);
		Materials.Add(MaterialName.BallTrail, ballTrailMaterial);
		Materials.Add(MaterialName.BallInner, ballInnerMaterial);
		WorldMenuTextures = new ArrayUtils.Array<Texture>(Mathf.Max(LevelCatalog.Count, WorldMenuImages.Length));
		for (int i = 0; i < WorldMenuImages.Length; i++) WorldMenuTextures[i] = WorldMenuImages[i];
		for (int world = 6; world <= 8; world++)
		{
			WorldMenuTextures[world - 1] = Resources.Load<Texture2D>("Classic1471/Themes/Textures/WorldShow/world" + world);
			if (WorldMenuTextures[world - 1] == null) throw new System.InvalidOperationException("Missing classic menu texture: " + world);
		}
		for (int world = 9; world <= LevelCatalog.Count; world++)
		{
			string resource = LevelCatalog.Get(world).ResourceRoot + "/Themes/Menu" + LevelCatalog.Get(world).SourceNumber;
			TextAsset binding = Resources.Load<TextAsset>(resource);
			if (binding == null) throw new System.InvalidOperationException("Missing original menu binding: " + resource);
			WorldMenuTextures[world - 1] = Resources.Load<Texture2D>(binding.text.Trim());
			if (WorldMenuTextures[world - 1] == null) throw new System.InvalidOperationException("Missing original menu texture: " + binding.text.Trim());
		}
		WorldTextures = new ArrayUtils.Array<TextureSet>(LevelDesigner.ClassicThemeCount);
		for (int i = 0; i < 9; i++)
		{
			WorldTextures[i] = new TextureSet(WorldBallQuadTextures[i], WorldEnemyTextures[i], WorldFragileTextures[i], WorldGeneralTextures[i], WorldFragileTextures[i], WorldFragileActiveTextures[i], WorldMoverTextures[i], WorldMoverAutoTextures[i], WorldEnemyTextures[i], WorldEnemyTextures[i], WorldEnemyTextures[i], WorldFragileActiveTextures[i], WorldFragileTextures[i], WorldBackgroundTextures[i]);
		}
		classicNeutralTextures = LoadClassicTextures(6);
		for (int donorTheme = 8; donorTheme <= 14; donorTheme++)
		{
			WorldTextures[LevelDesigner.ClassicThemeNumber(donorTheme) - 1] = LoadClassicTextures(donorTheme);
		}
		for (int runtimeTheme = 17; runtimeTheme <= LevelDesigner.ClassicThemeCount; runtimeTheme++)
			WorldTextures[runtimeTheme - 1] = LoadClassicTextures(LevelDesigner.DonorThemeNumber(runtimeTheme), LevelDesigner.ClassicThemeResourceRoot(runtimeTheme));
		EnsureExpansionColors();
		usingLitShaders = DeviceQualityChecker.QualityIsNotPour();
		usingGeoBall = DeviceQualityChecker.QualityIsNotPour();
		usingFullShaders = DeviceQualityChecker.QualityIsHigh();
		if (usingLitShaders)
		{
			Materials[MaterialName.Enemy].shader = litEnemyShader;
			if (usingFullShaders)
			{
				Materials[MaterialName.Fragile].shader = transparentShader;
				Materials[MaterialName.FragileActive].shader = unlitShader;
				Materials[MaterialName.Finish].shader = transparentShader;
				Materials[MaterialName.BallInner].shader = fullBallInnerShader;
				Materials[MaterialName.Pickup].shader = fullPickupShader;
				Materials[MaterialName.Pickup].SetFloat("_Gloss", 0.05f);
			}
			else
			{
				Materials[MaterialName.Pickup].shader = litPickupShader;
				Materials[MaterialName.Fragile].shader = transparentShader;
				Materials[MaterialName.FragileActive].shader = unlitShader;
				Materials[MaterialName.Finish].shader = transparentShader;
				Materials[MaterialName.BallInner].shader = basicBallInnerShader;
			}
		}
		else
		{
			Materials[MaterialName.Pickup].shader = unlitShader;
			Materials[MaterialName.Enemy].shader = unlitShader;
			Materials[MaterialName.Fragile].shader = unlitShader;
			Materials[MaterialName.FragileActive].shader = unlitShader;
			Materials[MaterialName.Finish].shader = unlitShader;
			Materials[MaterialName.BallInner].shader = basicBallInnerShader;
		}
		if (usingGeoBall)
		{
			Materials[MaterialName.Ball].shader = litBallGeoShader;
		}
		else
		{
			Materials[MaterialName.BallQuad].shader = unlitBallQuadShader;
		}
		backgroundShader = new ShaderPair(backgroundShaderNormal, backgroundShaderLerped);
		midgroundShader = new ShaderPair(midgroundShaderNormal, midgroundShaderLerped);
		isSetup = true;
	}

	public static int StartRecolor()
	{
		return StartRecolor(true);
	}

	public static int StartRecolor(bool toNext)
	{
		if (transitioning)
		{
			lastColorNum = currentColorNum;
		}
		int newColorNum = ((!toNext) ? colorNumNext.GetPrevious() : colorNumNext.GetNext());
		StartRecolorTo(newColorNum);
		return currentColorNum;
	}

	public static int StartRecolorTo(int newColorNum)
	{
		if (transitioning)
		{
			lastColorNum = currentColorNum;
		}
		currentColorNum = newColorNum;
		transitioning = true;
		ClassicPostEffects.ApplyTheme(false);
		return currentColorNum;
	}

	public static int StartMenuRecolorTo(int worldIndex)
	{
		return StartRecolorTo(LevelDesigner.GetMenuThemeIndex(worldIndex));
	}

	public static void Recolor(float transitionPercent, bool inversePercent)
	{
		Recolor(1f - transitionPercent);
	}

	public static void Recolor(float transitionPercent)
	{
		if (transitionPercent >= 1f)
		{
			EndRecolor();
		}
		else
		{
			EnactPartialRecolor(transitionPercent);
		}
	}

	public static void EndRecolor()
	{
		lastColorNum = currentColorNum;
		ColorSet colors = ColorsForTheme(currentColorNum);
		EnactRecolor(colors);
		transitioning = false;
	}

	public static void StartLerping(int firstTextureIndex, int secondTextureIndex)
	{
		// Menu callers supply world indices, not runtime theme indices.
		TextureSet first = GetWorldTextureSetForWorld(firstTextureIndex);
		TextureSet second = GetWorldTextureSetForWorld(secondTextureIndex);
		Materials[MaterialName.Background].shader = backgroundShader.Alternate;
		Materials[MaterialName.Midground].shader = midgroundShader.Alternate;
		UpdateLerping(0f);
		Materials[MaterialName.Background].SetTexture("_MainTex", first[MaterialName.Background]);
		Materials[MaterialName.Background].SetTexture("_AltTex", second[MaterialName.Background]);
		Materials[MaterialName.Midground].SetTexture("_MainTex", first[MaterialName.Midground]);
		Materials[MaterialName.Midground].SetTexture("_AltTex", second[MaterialName.Midground]);
	}

	public static void UpdateLerping(float lerpPercent)
	{
		Materials[MaterialName.Background].SetFloat("_Ammount", lerpPercent);
		Materials[MaterialName.Midground].SetFloat("_Ammount", lerpPercent);
	}

	public static void EndLerping(int? endingTextureIndex)
	{
		Materials[MaterialName.Background].shader = backgroundShader.Normal;
		Materials[MaterialName.Midground].shader = midgroundShader.Normal;
		if (endingTextureIndex.HasValue)
		{
			TextureSet ending = GetWorldTextureSetForWorld(endingTextureIndex.Value);
			Materials[MaterialName.Background].mainTexture = ending[MaterialName.Background];
			Materials[MaterialName.Midground].mainTexture = ending[MaterialName.Midground];
		}
	}

	public static Color GetPrimaryColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).Primary.color;
	}

	public static Color GetSecondaryColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).Secondary.color;
	}

	public static Color GetGuiLightColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiLight.color;
	}

	public static Color GetGuiLightAltColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiLightAlt.color;
	}

	public static Color GetGuiMidColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiMid.color;
	}

	public static Color GetGuiMidAltColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiMidAlt.color;
	}

	public static Color GetGuiDarkColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiDark.color;
	}

	public static Color GetGuiButtonTextColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiButtonText.color;
	}

	public static Color GetGuiRevealColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiReveal.color;
	}

	public static Color GetGuiOverlayColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiOverlay.color;
	}

	public static float GetGuiOverlayAlpha(int WorldNum)
	{
		return ColorsForWorld(WorldNum).GuiOverlayAlpha;
	}

	public static Color GetEnergyHighColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyHigh.color;
	}

	public static Color GetEnergyMidHighColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyMidHigh.color;
	}

	public static Color GetEnergyColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).Energy.color;
	}

	public static Color GetEnergyMidLowColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyMidLow.color;
	}

	public static Color GetEnergyLowColor(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyLow.color;
	}

	public static Color[] GetEnergyColors(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyColors;
	}

	public static Color[] GetEnergyParticleColors(int WorldNum)
	{
		return ColorsForWorld(WorldNum).EnergyParticles;
	}

	public static Color[] GetPickupParticleColors(int WorldNum)
	{
		return ColorsForWorld(WorldNum).PickupParticles;
	}

	public static Color GetPrimaryColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).Primary.color;
	}

	public static Color GetSecondaryColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).Secondary.color;
	}

	public static Color GetGuiLightColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiLight.color;
	}

	public static Color GetGuiLightAltColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiLightAlt.color;
	}

	public static Color GetGuiMidColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiMid.color;
	}

	public static Color GetGuiMidAltColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiMidAlt.color;
	}

	public static Color GetGuiDarkColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiDark.color;
	}

	public static Color GetGuiButtonTextColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiButtonText.color;
	}

	public static Color GetGuiRevealColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiReveal.color;
	}

	public static Color GetGuiOverlayColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiOverlay.color;
	}

	public static float GetGuiOverlayAlpha()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).GuiOverlayAlpha;
	}

	public static Color GetEnergyHighColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyHigh.color;
	}

	public static Color GetEnergyMidHighColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyMidHigh.color;
	}

	public static Color GetEnergyColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).Energy.color;
	}

	public static Color GetEnergyMidLowColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyMidLow.color;
	}

	public static Color GetEnergyLowColor()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyLow.color;
	}

	public static Color[] GetEnergyColors()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyColors;
	}

	public static Color[] GetEnergyParticleColors()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).EnergyParticles;
	}

	public static Color[] GetPickupParticleColors()
	{
		return ColorsForTheme(LevelDesigner.CurrentWorldThemeIndex).PickupParticles;
	}

	public static Color GetNextPrimaryColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).Primary.color;
	}

	public static Color GetNextSecondaryColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).Secondary.color;
	}

	public static Color GetNextGuiLightColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiLight.color;
	}

	public static Color GetNextGuiLightAltColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiLightAlt.color;
	}

	public static Color GetNextGuiMidColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiMid.color;
	}

	public static Color GetNextGuiMidAltColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiMidAlt.color;
	}

	public static Color GetNextGuiDarkColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiDark.color;
	}

	public static Color GetNextGuiButtonTextColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiButtonText.color;
	}

	public static Color GetNextGuiRevealColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiReveal.color;
	}

	public static Color GetNextGuiOverlayColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiOverlay.color;
	}

	public static float GetNextGuiOverlayAlpha()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).GuiOverlayAlpha;
	}

	public static Color GetNextEnergyHighColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyHigh.color;
	}

	public static Color GetNextEnergyMidHighColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyMidHigh.color;
	}

	public static Color GetNextEnergyColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).Energy.color;
	}

	public static Color GetNextEnergyMidLowColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyMidLow.color;
	}

	public static Color GetNextEnergyLowColor()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyLow.color;
	}

	public static Color[] GetNextEnergyColors()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyColors;
	}

	public static Color[] GetNextEnergyParticleColors()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).EnergyParticles;
	}

	public static Color[] GetNextPickupParticleColors()
	{
		return ColorsForWorld(GameManager.GetNextWorldIndex() + 1).PickupParticles;
	}

	private static void EnactRecolor(ColorSet colors)
	{
		int textureIndex = MathUtils.Indexed(currentColorNum, WorldTextures.Length);
		ApplyTexture(textureIndex);
		if (usingLitShaders)
		{
			if (usingFullShaders)
			{
				Materials[MaterialName.Pickup].SetColor("_ColorD", colors.PickupDiff.color);
				Materials[MaterialName.Pickup].SetColor("_ColorS", colors.PickupSpec.color);
				Materials[MaterialName.Pickup].SetColor("_ColorR", colors.PickupRefl.color);
				Materials[MaterialName.BallInner].SetColor("_ColorE", colors.EnergyHigh.color);
				Materials[MaterialName.BallInner].SetColor("_ColorEAlt", colors.EnergyLow.color);
			}
			else
			{
				Materials[MaterialName.Pickup].SetColor("_Color", colors.PickupSpec.color);
				Materials[MaterialName.Pickup].SetFloat("_Specular", colors.EnemySpecAmnt);
				Materials[MaterialName.Pickup].SetFloat("_Gloss", colors.EnemyGloss);
			}
			if (usingGeoBall)
			{
				Materials[MaterialName.Ball].SetColor("_ColorD", colors.BallDiff.color);
				ClassicBoxShield.ThemeColorChanged(colors.BallDiff.color);
				Materials[MaterialName.Ball].SetColor("_ColorS", colors.BallSpec.color);
				Materials[MaterialName.Ball].SetColor("_ColorSa", colors.BallSpecAmbi.color);
			}
			Materials[MaterialName.Enemy].SetColor("_Color", colors.EnemySpec.color);
			Materials[MaterialName.Enemy].SetFloat("_Specular", colors.EnemySpecAmnt);
			Materials[MaterialName.Enemy].SetFloat("_Gloss", colors.EnemyGloss);
			Materials[MaterialName.Midground].SetColor("_ColorF", colors.BackgroundFog.color);
		}
		Materials[MaterialName.BallInner].SetColor("_Color", colors.Energy.color);
		Materials[MaterialName.Ball].SetColor("_ColorF", colors.BackgroundFog.color);
		Camera.main.backgroundColor = colors.Camera.color;
		RenderSettings.fogColor = colors.ForegroundFog.color;
		RenderSettings.fogStartDistance = colors.FogStart;
		RenderSettings.fogEndDistance = colors.FogEnd;
		Color color = colors.Score.color;
		if (score != null)
		{
			score.color = color;
		}
		Materials[MaterialName.InGameHud].SetColor("_Color", color);
		Materials[MaterialName.NextWorldReveal].SetColor("_Color", colors.GuiReveal.color);
		Materials[MaterialName.MenuOverlay].SetColor("_Color", colors.GuiOverlay.color);
		Materials[MaterialName.MenuOverlay].SetFloat("_Alpha", colors.GuiOverlayAlpha);
	}

	private static void EnactPartialRecolor(float transitionPercent)
	{
		ColorSet colorSet = ColorsForTheme(lastColorNum);
		ColorSet colorSet2 = ColorsForTheme(currentColorNum);
		if (usingLitShaders)
		{
			if (usingFullShaders)
			{
				Materials[MaterialName.Pickup].SetColor("_ColorD", colorSet.PickupDiff.GetBlended(colorSet2.PickupDiff, transitionPercent));
				Materials[MaterialName.Pickup].SetColor("_ColorS", colorSet.PickupSpec.GetBlended(colorSet2.PickupSpec, transitionPercent));
				Materials[MaterialName.Pickup].SetColor("_ColorR", colorSet.PickupRefl.GetBlended(colorSet2.PickupRefl, transitionPercent));
				Materials[MaterialName.BallInner].SetColor("_ColorE", colorSet.EnergyHigh.GetBlended(colorSet2.EnergyHigh, transitionPercent));
				Materials[MaterialName.BallInner].SetColor("_ColorEAlt", colorSet.EnergyLow.GetBlended(colorSet2.EnergyLow, transitionPercent));
			}
			else
			{
				Materials[MaterialName.Pickup].SetColor("_Color", colorSet.PickupSpec.GetBlended(colorSet2.PickupSpec, transitionPercent));
				Materials[MaterialName.Pickup].SetFloat("_Specular", MathUtils.Lerp(colorSet.EnemySpecAmnt, colorSet2.EnemySpecAmnt, transitionPercent));
				Materials[MaterialName.Pickup].SetFloat("_Gloss", MathUtils.Lerp(colorSet.EnemyGloss, colorSet2.EnemyGloss, transitionPercent));
			}
			if (usingGeoBall)
			{
				Color ballDiffuse = colorSet.BallDiff.GetBlended(colorSet2.BallDiff, transitionPercent);
				Materials[MaterialName.Ball].SetColor("_ColorD", ballDiffuse);
				ClassicBoxShield.ThemeColorChanged(ballDiffuse);
				Materials[MaterialName.Ball].SetColor("_ColorS", colorSet.BallSpec.GetBlended(colorSet2.BallSpec, transitionPercent));
				Materials[MaterialName.Ball].SetColor("_ColorSa", colorSet.BallSpecAmbi.GetBlended(colorSet2.BallSpecAmbi, transitionPercent));
			}
			Materials[MaterialName.Enemy].SetColor("_Color", colorSet.EnemySpec.GetBlended(colorSet2.EnemySpec, transitionPercent));
			Materials[MaterialName.Enemy].SetFloat("_Specular", MathUtils.Lerp(colorSet.EnemySpecAmnt, colorSet2.EnemySpecAmnt, transitionPercent));
			Materials[MaterialName.Enemy].SetFloat("_Gloss", MathUtils.Lerp(colorSet.EnemyGloss, colorSet2.EnemyGloss, transitionPercent));
			Materials[MaterialName.Midground].SetColor("_ColorF", colorSet.BackgroundFog.GetBlended(colorSet2.BackgroundFog, transitionPercent));
		}
		Materials[MaterialName.BallInner].SetColor("_Color", colorSet.Energy.GetBlended(colorSet2.Energy, transitionPercent));
		Materials[MaterialName.Ball].SetColor("_ColorF", colorSet.BackgroundFog.GetBlended(colorSet2.BackgroundFog, transitionPercent));
		Camera.main.backgroundColor = colorSet.Camera.GetBlended(colorSet2.Camera, transitionPercent);
		RenderSettings.fogColor = colorSet.ForegroundFog.GetBlended(colorSet2.ForegroundFog, transitionPercent);
		RenderSettings.fogStartDistance = MathUtils.Lerp(colorSet.FogStart, colorSet2.FogStart, transitionPercent);
		RenderSettings.fogEndDistance = MathUtils.Lerp(colorSet.FogEnd, colorSet2.FogEnd, transitionPercent);
		Color blended = colorSet.Score.GetBlended(colorSet2.Score, transitionPercent);
		if (score != null)
		{
			score.color = blended;
		}
		Materials[MaterialName.InGameHud].SetColor("_Color", blended);
		Materials[MaterialName.NextWorldReveal].SetColor("_Color", colorSet.GuiReveal.GetBlended(colorSet2.GuiReveal, transitionPercent));
		Materials[MaterialName.MenuOverlay].SetColor("_Color", colorSet.GuiOverlay.GetBlended(colorSet2.GuiOverlay, transitionPercent));
		Materials[MaterialName.MenuOverlay].SetFloat("_Alpha", MathUtils.Lerp(colorSet.GuiOverlayAlpha, colorSet2.GuiOverlayAlpha, transitionPercent));
	}

	private static void ApplyTexture(int textureIndex)
	{
		for (int i = 0; i < texturedMaterials.Length; i++)
		{
			Materials[texturedMaterials[i]].mainTexture = GetWorldTextureSet(textureIndex)[texturedMaterials[i]];
		}
		ClassicBoxShield.ThemeTextureChanged(Materials[MaterialName.BallQuad].mainTexture);
	}
}
