using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public enum InputState
	{
		Ready = 0,
		Pressing = 1,
		WaitingForDepress = 2,
		Paused = 3
	}

	public enum SwipeState
	{
		Left = 0,
		Right = 1,
		Up = 2,
		Down = 3,
		None = 4
	}

	public enum Menu
	{
		Main = 0,
		Locations = 1,
		Settings = 2,
		Languages = 3,
		Shop = 4
	}

	public enum MenuState
	{
		Loading = 0,
		Ready = 1,
		Transitioning = 2
	}

	public enum LoadingStage
	{
		Reading = 0,
		Loading = 1,
		Configuring = 2,
		Creating = 3,
		Spooling = 4,
		Extending = 5,
		Grooving = 6
	}

	private class World
	{
		private static Transform masterTransform;

		private static string nameStart;

		public readonly int Index;

		public readonly int Number;

		public readonly Transform Transform;

		public readonly WorldDisplay Script;

		private Vector3 normalPos;

		private Vector3 selectedPos;

		private float normalDepth;

		public string Name { get; private set; }

		public Color LightColor { get; private set; }

		public Color LightAltColor { get; private set; }

		public Color MidColor { get; private set; }

		public Color MidAltColor { get; private set; }

		public Color DarkColor { get; private set; }

		public bool Endless { get; private set; }

		public World(WorldDisplay masterWorldDisplay, bool isEndless)
		{
			Script = masterWorldDisplay;
			Transform = Script.transform;
			masterTransform = Transform;
			nameStart = Transform.name;
			nameStart = nameStart.Substring(0, nameStart.Length - 1);
			Index = 0;
			Number = LevelCatalog.AtMenuIndex(Index).WorldNumber;
			Script.SetTexture(MaterialManager.WorldMenuTextures[Number - 1]);
			Endless = isEndless;
			ConfigureWorld();
		}

		public World(int worldIndex, bool isEndless)
		{
			Index = worldIndex;
			Number = LevelCatalog.AtMenuIndex(Index).WorldNumber;
			string newName = nameStart + Number;
			Transform = TransformUtils.Duplicate(masterTransform, newName);
			Script = Transform.GetComponent<WorldDisplay>();
			TransformUtils.MoveX(Transform, 7f * (float)Index, true);
			Script.SetTexture(MaterialManager.WorldMenuTextures[Number - 1]);
			Endless = isEndless;
			ConfigureWorld();
		}

		private void ConfigureWorld()
		{
			Name = LevelDesigner.GetName(Number);
			LightColor = MaterialManager.GetGuiLightColor(Number);
			LightAltColor = MaterialManager.GetGuiLightAltColor(Number);
			MidColor = MaterialManager.GetGuiMidColor(Number);
			MidAltColor = MaterialManager.GetGuiMidAltColor(Number);
			DarkColor = MaterialManager.GetGuiDarkColor(Number);
			normalDepth = Transform.localPosition.z;
			SetDepth(1f, Index == 0);
			int progress = ProgressManager.GetProgress(Number - 1);
			int gems = ProgressManager.GetGems(Number - 1);
			Script.Configure(Name, LightColor, LightAltColor, MidColor, MidAltColor, DarkColor, progress, gems, LevelCatalog.Get(Number).MaxGems, Endless);
		}

		public void Select()
		{
			normalPos = Transform.localPosition;
			selectedPos = worldSelectedPos;
			selectedPos.x = normalPos.x;
		}

		public void Load()
		{
			GameManager.SetWorldNum(Number);
			Application.LoadLevel(1);
		}

		public void UpdateSwipingAnim(float animPercent, bool lastFrame)
		{
			if (Index == currentWorldIndex)
			{
				SetDepth(animPercent, lastFrame);
			}
			else if (Index == lastWorldIndex)
			{
				SetDepth(1f - animPercent, false);
			}
		}

		public void UpdateSelectionAnim(float animPercent)
		{
			animPercent = FloatAnim.Smooth(animPercent, true, false);
			Script.SetOverlay(animPercent);
			Transform.localPosition = Vector3.Lerp(normalPos, selectedPos, animPercent);
		}

		private void SetDepth(float depthPercent, bool zero)
		{
			if (zero)
			{
				ZeroDepth();
				return;
			}
			depthPercent = FloatAnim.Smooth(depthPercent, true, true);
			float newZ = normalDepth + depthPercent * 2f;
			TransformUtils.SetZ(Transform, newZ, true);
			bool flag = false;
			if (currentWorldIndex >= lastWorldIndex)
			{
				if (Index >= currentWorldIndex)
				{
					flag = true;
				}
			}
			else if (Index > currentWorldIndex)
			{
				flag = true;
			}
			float num = depthPercent * 0f;
			if (flag)
			{
				num *= -1f;
			}
			Transform.localEulerAngles = new Vector3(0f, num, 0f);
		}

		public void ZeroDepth()
		{
			Transform.localEulerAngles = Vector3.zero;
			TransformUtils.SetZ(Transform, 0f, true);
		}
	}

	private class Pip
	{
		private static Color deselectedColor;

		private static Color deselectedColorNext;

		private static Color? deselectedColorLast;

		private readonly Material material;

		private readonly Renderer renderer;

		private readonly Color centerColor;

		private readonly Color progressColor;

		private float progress;

		private bool animating;

		private float animTimer;

		public Pip(Renderer renderer, int number)
		{
			this.renderer = renderer;
			material = renderer.material;
			progressColor = MaterialManager.GetGuiDarkColor(number);
			centerColor = MaterialManager.GetGuiMidColor(number);
		}

		public void Position(float offset, float halfSpan)
		{
			renderer.enabled = Mathf.Abs(offset) <= halfSpan + 0.001f;
			TransformUtils.SetX(renderer.transform, offset, true);
		}

		public static void SetTransitionPercent(float percent)
		{
			if (lastWorldIndex == -1)
			{
				deselectedColor = MaterialManager.GetGuiDarkColor(currentWorldNumber);
			}
			else
			{
				deselectedColor = Color.Lerp(MaterialManager.GetGuiDarkColor(currentWorldNumber), MaterialManager.GetGuiDarkColor(lastWorldNumber), percent);
			}
			Color color = deselectedColor;
			LocationArrowMaterial.SetColor("_Color", color);
		}

		public void SetCenterTo(float alphaAmmount)
		{
			Color color = Color.Lerp(deselectedColor, centerColor, alphaAmmount);
			material.SetColor("_Color", color);
		}

		public void SetProgressTo(float progressAmmount)
		{
			progress = progressAmmount;
			if (progress != 0f)
			{
				animating = true;
				animTimer = 2f;
				DisplayProgress(0f);
			}
		}

		public bool StillUpdating()
		{
			bool flag;
			if (animating)
			{
				animTimer -= Time.smoothDeltaTime;
				flag = animTimer <= 0f;
				if (flag)
				{
					float num = 1f;
					animating = false;
				}
				else
				{
					float num = FloatAnim.Smooth(1f - animTimer / 2f, true, true);
				}
			}
			else
			{
				flag = true;
			}
			return !flag;
		}

		private void DisplayProgress(float displayPercent)
		{
			material.SetFloat("_Ammount", progress * displayPercent);
		}
	}

	private class ButtonDepress
	{
		public readonly ButtonUtils.Name Button;

		private float depressTimer;

		private bool pressed;

		public ButtonDepress(ButtonUtils.Name buttonName)
		{
			Button = buttonName;
			depressTimer = 0.2f;
			pressed = true;
		}

		public ButtonDepress(ButtonUtils.Name buttonName, float depressTime)
		{
			Button = buttonName;
			depressTimer = depressTime;
			pressed = true;
		}

		public bool ShouldDepress()
		{
			if (pressed)
			{
				depressTimer -= Time.deltaTime;
				if (depressTimer <= 0f)
				{
					depressTimer = 0f;
					pressed = false;
				}
			}
			return !pressed;
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugProgress = false;

	public const bool Benchmark = false;

	public const bool showGemsOwned = false;

	private const bool showEndless = false;

	private const string settingsDescriptor = "Settings";

	private const string languagesDescriptor = "Languages";

	private const string musicOnDescriptor = "Music On";

	private const string musicOffDescriptor = "Music Off";

	private const string soundsOnDescriptor = "Sounds On";

	private const string soundsOffDescriptor = "Sounds Off";

	private const string alertsOnDescriptor = "Alerts On";

	private const string alertsOffDescriptor = "Alerts Off";

	private const string restoreDescriptor = "Restore";

	private const string languageDescriptor = "Language";

	private const string alertsExplanation = "Turns off recharge notification";

	private const string restoreExplanation = "Use this to restore all previous purchases";

	private const string restoreNoInternetMessage = "restore previous purchases";

	private const bool blendedBackgrounds = true;

	private const float midgroundSpeedUp = 1f;

	private const string worldDisplaySliderName = "World Display Slider";

	private const float minSwipeDistance = 40f;

	private const float minSwipeXDistMult = 0.5f;

	private const float maxSwipeDistance = 150f;

	private const float worldSwipeTimeDefault = 0.5f;

	private const float worldSwipeTimeExtra = 0f;

	public const float WorldSelectionTime = 1f;

	private const float worldSpacing = 7f;

	private const float displayedWorldHalfWidth = 196f;

	private const float musicVolumeOfNormal = 0.5f;

	private const float musicFadeInTime = 3f;

	private const float musicCrossFadeTime = 1f;

	private const float nonCenteredDepthOffset = 3f;

	private const float nonCenteredAngleOffset = -20f;

	private const float progressUpdateTime = 2f;

	private const bool colorProgress = false;

	private const float menuTransitionTimeStarting = 1f;

	private const float menuTransitionTimeNormal = 1f;

	private const float menuTransitionTimeAuto = 0.5f;

	private const float midgroundAppearTime = 1.5f;

	private const float loadingTime = 3f;

	private const float cameraStartingOffset = 22.5f;

	private const float flashClearTime = 0.5f;

	private const float flashAlpha = 0.75f;

	private const int flashSharpness = 1;

	private const float midgroundReapearTime = 0.75f;

	private const float buttonDepressTimeDefault = 0.2f;

	private const float playButtonMaxDeltaTime = 1f / 30f;

	private const float playButtonPreFadeInTime = 1f;

	private const float playButtonFadeInTime = 5f;

	private const float playButtonFadeInTimeTotal = 6f;

	private static Vector3 worldSelectedPos = new Vector3(0f, -1f, -6.5f);

	public LoadingStage loadingStage;

	public Transform gameTitle;

	public Transform cameraOffseter;

	public Transform menusGroup;

	public Transform mainMenuGroup;

	public Transform settingsMenuGroup;

	public Transform languagesMenuGroup;

	public TextMesh loadingText;

	public TextMesh loadingTextOutline;

	public Transform loadingCirclesGroup;

	public Renderer mainSplash;

	public Material mainLoadingMaterial;

	public Material locationArrowMaterial;

	public WorldDisplay worldMaster;

	public Transform worldGeoGroup;

	public Transform progressPipsGroup;

	public TextMesh debugText;

	public Renderer background;

	public Renderer overlay;

	public Transform mainMenuSettingsButton;

	public Transform mainMenuLeaderboardButton;

	public Transform mainMenuAchievementsButton;

	public Transform mainMenuGoogleControllerIcon;

	public Transform mainMenuGoogleControllerActiveIcon;

	public Transform mainMenuPlayButton;

	public Transform settingsMenuMusicButton;

	public Transform settingsMenuSoundButton;

	public Transform settingsMenuAlertsButton;

	public Transform settingsMenuRestoreButton;

	public Transform settingsMenuLanguageButton;

	public Transform settingsMenuBackButton;

	public TextMesh settingsDebugText;

	public TextMesh settingsDebugTextShadow;

	public TextMesh settingsTitle;

	public TextMesh settingsTitleShadow;

	public TextMesh languagesTitle;

	public TextMesh languagesTitleShadow;

	public Transform languageBackButton;

	public Transform[] languageButtons;

	public Transform locationsLeftButton;

	public Transform locationsRightButton;

	public Material playButtonMaterial;

	public Menu menu;

	public MenuState menuState;

	public InputState inputState;

	public SwipeState swipeState;

	public SwipeState? lastSwipeState;

	private static int currentWorldIndex;

	private static int lastWorldIndex;

	private static Material LocationArrowMaterial;

	private static bool alreadyLoaded;

	private static bool startingLoad;

	private ArrayUtils.SmartDict<ButtonUtils.Name, ButtonUtils.Button> buttons;

	private ArrayUtils.SmartDict<Menu, Vector3> menuData;

	private World[] worlds;

	private Vector2 firstPressedPoint;

	private bool loadNextFrame;

	private int? selectedWorldNumber;

	private float worldSelectionTimer;

	private float worldSwipeTimer;

	private float worldSwipeTime;

	private bool worldsSwiping;

	private Transform worldSwiper;

	private MathUtils.Range displayedWorldRange;

	private MathUtils.Range worldSwipeRange;

	private float speed;

	private float speedMultiplier;

	private float lastTilePos;

	private Pip[] progressPips;

	private bool updatingProgressPips;

	private float menuTransitionTimer;

	private Menu? menuTarget;

	private float loadingTimer;

	private float midgroundAppearTimer;

	private bool setupComplete;

	private bool setupQueued;

	private float loadingStageInterval;

	private float backgroundTransitionEndTimer;

	private float backgroundTransitionEndTime;

	private bool backgroundTransitionEnding;

	private bool cameraIsOffset;

	private float lastCamPos;

	private bool midgroundStarting;

	private float menuTransitionTime;

	private float playButtonFadeTimer;

	private bool playButtonFadedIn;

	private bool delayedAutoMoveToMain;

	private bool smoothedMenuTransitionStart;

	private ArrayUtils.List<ButtonDepress> buttonsToDepress;

	private ArrayUtils.List<int> buttonsJustDepressed;

	private static bool googlePlayAuthenticated = false;

	public static DateTime BenchmarkedTime { get; private set; }

	private static int currentWorldNumber
	{
		get
		{
			return LevelCatalog.AtMenuIndex(currentWorldIndex).WorldNumber;
		}
	}

	private static int lastWorldNumber
	{
		get
		{
			return LevelCatalog.AtMenuIndex(lastWorldIndex).WorldNumber;
		}
	}

	private Material backgroundMaterial
	{
		get
		{
			return MaterialManager.Materials[MaterialName.Background];
		}
	}

	private Material midgroundMaterial
	{
		get
		{
			return MaterialManager.Materials[MaterialName.Midground];
		}
	}

	private void Start()
	{
		PortraitViewport viewport = PortraitViewport.Attach(Camera.main);
		if (viewport != null)
		{
			viewport.Pin(gameTitle, true);
			viewport.Pin(mainMenuSettingsButton.parent, false);
			viewport.Pin(settingsMenuBackButton, false);
			viewport.Pin(languageBackButton, false);
			viewport.Pin(progressPipsGroup, false);
		}
		Localizer.SetDefaultSystemLanguageFirstLaunch();
		TransformUtils.Hide(gameTitle);
		if (!googlePlayAuthenticated)
		{
			AchievementManager.AuthenticatePlayer();
			googlePlayAuthenticated = true;
		}
		settingsMenuLanguageButton.position = settingsMenuRestoreButton.position;
		settingsMenuRestoreButton.position = settingsMenuAlertsButton.position;
		TransformUtils.Hide(settingsMenuAlertsButton);
		NotificationManager.ClearNotificationBadge();
		ServerConfigs.Refresh();
		GameManager.PreStart();
		startingLoad = !alreadyLoaded;
		int startingColorNum = (startingLoad ? LevelCatalog.AtMenuIndex(0).WorldNumber : GameManager.WorldNum);
		// Prepare the selected soundtrack during menu construction, not after
		// the last loading stage. MusicPlayer waits for the actual audio data.
		MusicPlayer.Initialize(0.5f);
		if (UserSettings.IsMusicOn()) MusicPlayer.FadeInto(startingColorNum - 1, 3f);
		MaterialManager.Initialize(startingColorNum);
		mainLoadingMaterial.SetFloat("_Ammount", 0f);
		backgroundTransitionEnding = false;
		lastCamPos = Camera.main.transform.position.y;
		OffsetCamera(1f);
		GameManager.SetTargetFrameRate();
		GameManager.TryClearWorld();
		LocationArrowMaterial = locationArrowMaterial;
		loadingStage = LoadingStage.Reading;
		DisplayCurrentLoadingStage();
		buttons = new ArrayUtils.SmartDict<ButtonUtils.Name, ButtonUtils.Button>();
		buttons.Add(ButtonUtils.Name.Settings, new ButtonUtils.Button(ButtonUtils.Name.Settings, mainMenuSettingsButton));
		buttons.Add(ButtonUtils.Name.Leaderboard, new ButtonUtils.Button(ButtonUtils.Name.Leaderboard, mainMenuLeaderboardButton));
		buttons.Add(ButtonUtils.Name.GoogleController, new ButtonUtils.Button(ButtonUtils.Name.GoogleController, mainMenuGoogleControllerIcon));
		buttons.Add(ButtonUtils.Name.GoogleControllerActive, new ButtonUtils.Button(ButtonUtils.Name.GoogleControllerActive, mainMenuGoogleControllerActiveIcon));
		buttons.Add(ButtonUtils.Name.Achievements, new ButtonUtils.Button(ButtonUtils.Name.Achievements, mainMenuAchievementsButton));
		buttons.Add(ButtonUtils.Name.Play, new ButtonUtils.Button(ButtonUtils.Name.Play, mainMenuPlayButton));
		buttons.Add(ButtonUtils.Name.Left, new ButtonUtils.Button(ButtonUtils.Name.Left, locationsLeftButton));
		buttons.Add(ButtonUtils.Name.Right, new ButtonUtils.Button(ButtonUtils.Name.Right, locationsRightButton));
		buttons.Add(ButtonUtils.Name.Music, new ButtonUtils.Button(ButtonUtils.Name.Music, settingsMenuMusicButton));
		buttons.Add(ButtonUtils.Name.Sound, new ButtonUtils.Button(ButtonUtils.Name.Sound, settingsMenuSoundButton));
		buttons.Add(ButtonUtils.Name.Alerts, new ButtonUtils.Button(ButtonUtils.Name.Alerts, settingsMenuAlertsButton));
		buttons.Add(ButtonUtils.Name.Restore, new ButtonUtils.Button(ButtonUtils.Name.Restore, settingsMenuRestoreButton));
		buttons.Add(ButtonUtils.Name.Back, new ButtonUtils.Button(ButtonUtils.Name.Back, settingsMenuBackButton));
		buttons.Add(ButtonUtils.Name.BackAlt, new ButtonUtils.Button(ButtonUtils.Name.BackAlt, languageBackButton));
		buttons.Add(ButtonUtils.Name.Language, new ButtonUtils.Button(ButtonUtils.Name.Language, settingsMenuLanguageButton));
		buttons.Add(ButtonUtils.Name.Language0, new ButtonUtils.Button(ButtonUtils.Name.Language0, languageButtons[0]));
		buttons.Add(ButtonUtils.Name.Language1, new ButtonUtils.Button(ButtonUtils.Name.Language1, languageButtons[1]));
		buttons.Add(ButtonUtils.Name.Language2, new ButtonUtils.Button(ButtonUtils.Name.Language2, languageButtons[2]));
		buttons.Add(ButtonUtils.Name.Language3, new ButtonUtils.Button(ButtonUtils.Name.Language3, languageButtons[3]));
		buttons.Add(ButtonUtils.Name.Language4, new ButtonUtils.Button(ButtonUtils.Name.Language4, languageButtons[4]));
		buttons.Add(ButtonUtils.Name.Language5, new ButtonUtils.Button(ButtonUtils.Name.Language5, languageButtons[5]));
		buttons.Add(ButtonUtils.Name.Language6, new ButtonUtils.Button(ButtonUtils.Name.Language6, languageButtons[6]));
		buttons.Add(ButtonUtils.Name.Language7, new ButtonUtils.Button(ButtonUtils.Name.Language7, languageButtons[7]));
		buttons.Add(ButtonUtils.Name.Language8, new ButtonUtils.Button(ButtonUtils.Name.Language8, languageButtons[8]));
		buttons.Add(ButtonUtils.Name.Language9, new ButtonUtils.Button(ButtonUtils.Name.Language9, languageButtons[9]));
		buttons.Add(ButtonUtils.Name.Language10, new ButtonUtils.Button(ButtonUtils.Name.Language10, languageButtons[10]));
		buttons.Add(ButtonUtils.Name.Language11, new ButtonUtils.Button(ButtonUtils.Name.Language11, languageButtons[11]));
		string[] languagesCapitalized = Localizer.GetLanguagesCapitalized();
		buttons[ButtonUtils.Name.Language0].SetText(languagesCapitalized[0]);
		buttons[ButtonUtils.Name.Language1].SetText(languagesCapitalized[1]);
		buttons[ButtonUtils.Name.Language2].SetText(languagesCapitalized[2]);
		buttons[ButtonUtils.Name.Language3].SetText(languagesCapitalized[3]);
		buttons[ButtonUtils.Name.Language4].SetText(languagesCapitalized[4]);
		buttons[ButtonUtils.Name.Language5].SetText(languagesCapitalized[5]);
		buttons[ButtonUtils.Name.Language6].SetText(languagesCapitalized[6]);
		buttons[ButtonUtils.Name.Language7].SetText(languagesCapitalized[7]);
		buttons[ButtonUtils.Name.Language8].SetText(languagesCapitalized[8]);
		buttons[ButtonUtils.Name.Language9].SetText(languagesCapitalized[9]);
		buttons[ButtonUtils.Name.Language10].SetText(languagesCapitalized[10]);
		buttons[ButtonUtils.Name.Language11].SetText(languagesCapitalized[11]);
		playButtonMaterial.SetFloat("_Alpha", 0f);
		playButtonFadedIn = false;
		playButtonFadeTimer = 6f;
		buttonsToDepress = new ArrayUtils.List<ButtonDepress>(1);
		buttonsJustDepressed = new ArrayUtils.List<int>(1);
		buttons[ButtonUtils.Name.Settings].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Leaderboard].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.GoogleController].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.GoogleControllerActive].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Achievements].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Back].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.BackAlt].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Leaderboard].Hide();
		buttons[ButtonUtils.Name.Achievements].Hide();
		if (startingLoad)
		{
			alreadyLoaded = true;
			loadingStageInterval = 1f / EnumUtils.TotalEnumsAsFloat<LoadingStage>();
			setupComplete = false;
			menuState = MenuState.Loading;
			inputState = InputState.Paused;
			loadingTimer = 3f;
			AnalyticsManager.StartSession();
		}
		else
		{
			OnMainMenuLoaded();
		}
		menuData = new ArrayUtils.SmartDict<Menu, Vector3>();
		menuData.Add(Menu.Main, new Vector3(0f - mainMenuGroup.localPosition.x, 0f, 0f));
		menuData.Add(Menu.Locations, new Vector3(0f, 0f, 0f));
		menuData.Add(Menu.Settings, new Vector3(0f - settingsMenuGroup.localPosition.x, 0f, 0f));
		menuData.Add(Menu.Languages, new Vector3(0f - languagesMenuGroup.localPosition.x, 0f, 0f));
		menu = Menu.Main;
		menusGroup.localPosition = menuData[Menu.Main];
		if (startingLoad)
		{
			buttons[ButtonUtils.Name.Settings].Hide();
			buttons[ButtonUtils.Name.Leaderboard].Hide();
			buttons[ButtonUtils.Name.GoogleControllerActive].Hide();
			buttons[ButtonUtils.Name.GoogleController].Hide();
			buttons[ButtonUtils.Name.Achievements].Hide();
			buttons[ButtonUtils.Name.Play].Hide();
		}
		else
		{
			menuTarget = Menu.Locations;
			menusGroup.localPosition = menuData[menuTarget.Value];
			menu = menuTarget.Value;
			menuTarget = null;
			inputState = InputState.Paused;
		}
	}

	private void QueueSetup()
	{
		DisplayCurrentLoadingStage();
		setupQueued = true;
	}

	private void Setup()
	{
		switch (loadingStage)
		{
		case LoadingStage.Reading:
			LevelLoader.ReadWorlds();
			break;
		case LoadingStage.Loading:
			LevelLoader.LoadWorlds();
			break;
		case LoadingStage.Configuring:
		{
			speed = LevelDesigner.GlobalVelocityRange.Min;
			speedMultiplier = 0f;
			Renderer[] componentsInChildren = progressPipsGroup.GetComponentsInChildren<Renderer>();
			// Reuse the original pip mesh/material for the added world slots.
			for (int i = componentsInChildren.Length; i < GameManager.TotalWorlds; i++)
			{
				TransformUtils.Duplicate(componentsInChildren[0].transform, "Pip" + (i + 1));
			}
			componentsInChildren = progressPipsGroup.GetComponentsInChildren<Renderer>();
			progressPips = new Pip[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				progressPips[i] = new Pip(componentsInChildren[i], LevelCatalog.AtMenuIndex(i).WorldNumber);
			}
			worldSwiper = new GameObject("World Display Slider").transform;
			worldSwiper.parent = worldMaster.transform.parent;
			worldSwiper.localScale = Vector3.one;
			TransformUtils.Freeze(worldSwiper);
			worldMaster.transform.parent = worldSwiper;
			int num = GameManager.TotalWorlds;
			worlds = new World[num];
			worlds[0] = new World(worldMaster, false);
			for (int j = 1; j < num; j++)
			{
				worlds[j] = new World(j, false);
			}
			float num2 = Screen.width;
			float min = num2 / 2f - 196f;
			float max = num2 / 2f + 196f;
			displayedWorldRange = new MathUtils.Range(min, max);
			if (startingLoad)
			{
				currentWorldIndex = 0;
				lastWorldIndex = -1;
			}
			else
			{
				currentWorldIndex = LevelCatalog.MenuIndexOf(GameManager.WorldNum);
				lastWorldIndex = currentWorldIndex - 1;
				float newX = -1f * ((float)currentWorldIndex * 7f);
				TransformUtils.SetX(worldSwiper, newX, true);
				TransformUtils.SetX(worldGeoGroup, newX, true);
				worlds[currentWorldIndex].ZeroDepth();
			}
			SetupPips();
			SetPipsTo(currentWorldIndex);
			buttons[ButtonUtils.Name.Music].SetPressedTo(UserSettings.IsMusicOff());
			buttons[ButtonUtils.Name.Sound].SetPressedTo(UserSettings.IsSoundOff());
			buttons[ButtonUtils.Name.Alerts].SetPressedTo(UserSettings.AreAlertsOff());
			string text = null;
			settingsDebugText.text = text;
			settingsDebugTextShadow.text = text;
			Localize();
			break;
		}
		case LoadingStage.Creating:
			BufferManager.Initialize();
			break;
		case LoadingStage.Spooling:
			VideoSharingManager.Initialize();
			VideoRewardsManager.Initialize();
			break;
		case LoadingStage.Extending:
			MidgroundManager.InitializeForWorld(currentWorldNumber - 1);
			break;
		case LoadingStage.Grooving:
			MusicPlayer.Initialize(0.5f);
			if (UserSettings.IsMusicOn())
			{
				MusicPlayer.FadeInto(currentWorldNumber - 1, 3f);
			}
			else
			{
				MusicPlayer.Mute();
			}
			break;
		}
		EnumUtils.NextEnum(ref loadingStage, out setupComplete);
		setupQueued = false;
	}

	private void DisplayCurrentLoadingStage()
	{
		DisplayLoadingStage(loadingStage);
	}

	private void DisplayLoadingStage(LoadingStage loadingStageToDisplay)
	{
		if (setupComplete)
		{
			loadingText.text = null;
			loadingTextOutline.text = null;
		}
		else
		{
			string term = Localizer.GetTerm(EnumUtils.ToString(loadingStageToDisplay));
			loadingText.text = term;
			loadingTextOutline.text = term;
		}
	}

	private void SetupAll()
	{
		do
		{
			Setup();
		}
		while (!setupComplete);
		DisplayCurrentLoadingStage();
	}

	private void Update()
	{
		if (menuTarget.HasValue)
		{
			UpdateMenuTransition();
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (menu == Menu.Settings)
				{
					TransitionMenuTo(Menu.Main, menuTransitionTime);
				}
				else if (menu == Menu.Locations)
				{
					if (!worldsSwiping)
					{
						if (currentWorldIndex == 0)
						{
							StartMoveToNextWorld(false);
						}
						else
						{
							StartMoveToNextWorld(false, true);
						}
					}
				}
				else if (menu == Menu.Main)
				{
					Application.Quit();
				}
				else
				{
					Debug.LogWarning(string.Format("MEMN: WARNING: Pressed 'Back' button on unhandled menu of {0} - please code logic for pressing back button on {0} menu", menu));
				}
			}
			switch (menu)
			{
			case Menu.Main:
				UpdateMainMenu();
				break;
			case Menu.Locations:
				UpdateLocationsMenu();
				break;
			case Menu.Settings:
				UpdateSettingsMenu();
				break;
			case Menu.Languages:
				UpdateLanguagesMenu();
				break;
			default:
				Debug.LogError(string.Format("MEMN: ERROR: Reached unhandled menu of {0}.  Make sure this menu is configured in Update()'s case statement", menu));
				break;
			}
		}
		UpdateMidground();
		if (buttonsToDepress.IsNotEmpty)
		{
			UpdateButtonDepresses();
		}
	}

	private void UpdateMenuTransition()
	{
		menuTransitionTimer -= Time.smoothDeltaTime;
		Vector3 localPosition;
		if (menuTransitionTimer <= 0f)
		{
			localPosition = menuData[menuTarget.Value];
			OnTransitionMenuComplete();
		}
		else
		{
			float t = FloatAnim.Smooth(1f - menuTransitionTimer / menuTransitionTime, smoothedMenuTransitionStart, true);
			localPosition = Vector3.Lerp(menuData[menu], menuData[menuTarget.Value], t);
		}
		menusGroup.localPosition = localPosition;
	}

	private void UpdateMainMenu()
	{
		switch (menuState)
		{
		case MenuState.Loading:
		{
			if (setupQueued)
			{
				Setup();
			}
			loadingTimer -= Time.smoothDeltaTime;
			if (loadingTimer <= 0f)
			{
				OnMainMenuLoaded();
				break;
			}
			float value2 = MathUtils.ToPercent(loadingTimer, 0f, 3f, true);
			float value3 = MathUtils.ToPercent(loadingTimer, loadingStageInterval * 3f, 3f, true);
			mainLoadingMaterial.SetFloat("_Ammount", value3);
			int num2 = MathUtils.FlooredDivision(value2, loadingStageInterval);
			if ((int)loadingStage < num2)
			{
				QueueSetup();
			}
			break;
		}
		case MenuState.Ready:
			switch (inputState)
			{
			case InputState.Paused:
				break;
			case InputState.Ready:
				if (Input.GetMouseButtonDown(0))
				{
					ButtonUtils.Name buttonThatWasHit;
					if (ButtonUtils.ButtonWasHit(buttons, out buttonThatWasHit))
					{
						if (buttons[buttonThatWasHit].IsNotPressed)
						{
							buttons[buttonThatWasHit].Press();
							switch (buttonThatWasHit)
							{
							case ButtonUtils.Name.Settings:
								TransitionMenuTo(Menu.Settings, 1f);
								break;
							case ButtonUtils.Name.GoogleController:
								buttons[ButtonUtils.Name.Achievements].Hide();
								buttons[ButtonUtils.Name.Leaderboard].Hide();
								buttons[ButtonUtils.Name.GoogleController].Hide();
								buttons[ButtonUtils.Name.GoogleControllerActive].Show();
								break;
							case ButtonUtils.Name.GoogleControllerActive:
								buttons[ButtonUtils.Name.Achievements].Show();
								buttons[ButtonUtils.Name.Leaderboard].Show();
								buttons[ButtonUtils.Name.GoogleController].Show();
								buttons[ButtonUtils.Name.GoogleControllerActive].Hide();
								break;
							case ButtonUtils.Name.Leaderboard:
								AchievementManager.ShowLeaderboard();
								TimedDepress(buttonThatWasHit);
								break;
							case ButtonUtils.Name.Achievements:
								AchievementManager.ShowAchievements();
								TimedDepress(buttonThatWasHit);
								break;
							case ButtonUtils.Name.Play:
								TransitionMenuTo(Menu.Locations, 1f);
								break;
							}
						}
					}
					else
					{
						TransitionMenuTo(Menu.Locations, 1f);
					}
				}
				if (!playButtonFadedIn)
				{
					playButtonFadeTimer -= MathUtils.Max(Time.smoothDeltaTime, 1f / 30f);
					float value;
					if (playButtonFadeTimer <= 0f)
					{
						value = 1f;
						playButtonFadedIn = true;
					}
					else
					{
						float num = MathUtils.ToPercent01(playButtonFadeTimer, 5f, 0f);
						value = num;
					}
					playButtonMaterial.SetFloat("_Alpha", value);
				}
				break;
			default:
				Debug.LogError(string.Format("MEMN: ERROR: Reached unhandled inputState of {0} in Main Menu.  Check logic of UpdateMainMenu()'s nested case statement", inputState));
				break;
			}
			break;
		default:
			Debug.LogError(string.Format("MEMN: ERROR: Reached unhandled menuState of {0} in Main Menu.  Check logic of UpdateMainMenu()'s case statement", menuState));
			break;
		}
	}

	private void UpdateLocationsMenu()
	{
		if (loadNextFrame)
		{
			MidgroundManager.TryClear();
			TryResetShaders(null);
			MusicPlayer.Stop();
			worlds[selectedWorldNumber.Value].Load();
		}
		switch (inputState)
		{
		case InputState.Ready:
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
			{
				ButtonUtils.Name buttonThatWasHit;
				if (ButtonUtils.ButtonWasHit(buttons, out buttonThatWasHit))
				{
					if (buttons[buttonThatWasHit].IsNotPressed)
					{
						bool? flag = null;
						switch (buttonThatWasHit)
						{
						case ButtonUtils.Name.Right:
							flag = true;
							break;
						case ButtonUtils.Name.Left:
							flag = false;
							break;
						default:
							Debug.LogError(string.Format("MEMN: ERROR: Recievd unhandled Locations Menu button press of {0} in UpdateLocationsMenu()'s case function nested under InputState.Ready.  Check logic", buttonThatWasHit));
							break;
						}
						if (flag.HasValue)
						{
							StartMoveToNextWorld(flag.Value);
						}
					}
					inputState = InputState.WaitingForDepress;
				}
				else
				{
					inputState = InputState.Pressing;
					firstPressedPoint = Input.mousePosition;
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				OnClick(Input.mousePosition);
			}
			break;
		case InputState.Pressing:
		{
			if (Input.GetMouseButtonUp(0))
			{
				if (selectedWorldNumber.HasValue)
				{
					loadNextFrame = true;
				}
				else
				{
					OnClick(firstPressedPoint);
				}
				if (inputState != InputState.Paused)
				{
					inputState = InputState.Ready;
				}
				break;
			}
			Vector2 b = Input.mousePosition;
			float num = Vector2.Distance(firstPressedPoint, b);
			if (!(num >= 40f))
			{
				break;
			}
			float num2 = MathUtils.Distance(firstPressedPoint.x, b.x);
			if (num2 >= 20f)
			{
				if (b.x < firstPressedPoint.x)
				{
					swipeState = SwipeState.Left;
				}
				else
				{
					swipeState = SwipeState.Right;
				}
			}
			else if (b.y < firstPressedPoint.y)
			{
				swipeState = SwipeState.Down;
			}
			else
			{
				swipeState = SwipeState.Up;
			}
			switch (swipeState)
			{
			case SwipeState.Left:
				StartMoveToNextWorld(true);
				break;
			case SwipeState.Right:
				StartMoveToNextWorld(false);
				break;
			case SwipeState.Up:
			case SwipeState.Down:
				OnClick();
				break;
			default:
				Debug.LogError(string.Format("MEMN: ERROR: Recievd unexpected SwipeState of {0} in Update()'s case function under Swiping.  Check logic", swipeState));
				break;
			}
			inputState = InputState.WaitingForDepress;
			break;
		}
		case InputState.WaitingForDepress:
			if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0))
			{
				inputState = InputState.Ready;
			}
			break;
		}
		if (worldsSwiping)
		{
			UpdateMoveToNextWorld();
		}
		if (backgroundTransitionEnding)
		{
			UpdateBackgroundTransitionEnd();
		}
		if (selectedWorldNumber.HasValue)
		{
			if (loadNextFrame)
			{
				worldSelectionTimer = 0f;
			}
			else
			{
				worldSelectionTimer -= Time.smoothDeltaTime;
			}
			if (worldSelectionTimer <= 0f)
			{
				worldSelectionTimer = 0f;
				loadNextFrame = true;
			}
			float animPercent = 1f - worldSelectionTimer / 1f;
			worlds[selectedWorldNumber.Value].UpdateSelectionAnim(animPercent);
		}
		if (!updatingProgressPips)
		{
			return;
		}
		bool flag2 = false;
		for (int i = 0; i < progressPips.Length; i++)
		{
			if (progressPips[i].StillUpdating())
			{
				flag2 = true;
			}
		}
		if (!flag2)
		{
			updatingProgressPips = false;
		}
	}

	private void UpdateSettingsMenu()
	{
		switch (inputState)
		{
		case InputState.Paused:
			break;
		case InputState.Ready:
		{
			ButtonUtils.Name buttonThatWasHit;
			if (!Input.GetMouseButtonDown(0) || !ButtonUtils.ButtonWasHit(buttons, out buttonThatWasHit))
			{
				break;
			}
			bool? flag = null;
			bool flag2 = false;
			switch (buttonThatWasHit)
			{
			case ButtonUtils.Name.Music:
				UserSettings.ToggleMusic();
				flag = UserSettings.IsMusicOff();
				if (UserSettings.IsMusicOn())
				{
					MusicPlayer.Unmute();
					MusicPlayer.FadeIn(currentWorldNumber - 1, 3f);
				}
				else
				{
					MusicPlayer.Mute();
				}
				break;
			case ButtonUtils.Name.Sound:
				UserSettings.ToggleSound();
				flag = UserSettings.IsSoundOff();
				break;
			case ButtonUtils.Name.Alerts:
				if (PlayerProfiler.HaveNotExplainedAlerts)
				{
					AlertManager.ShowAlert("Alerts Off", "Turns off recharge notification", true);
					PlayerProfiler.OnExplainedAlerts();
				}
				UserSettings.ToggleAlerts();
				flag = UserSettings.AreAlertsOff();
				break;
			case ButtonUtils.Name.Restore:
				if (buttons[buttonThatWasHit].IsNotPressed)
				{
					flag = true;
					flag2 = true;
					if (GameManager.HasInternet())
					{
						IAPManager.RestoreProductPurchases(RestorePurchases);
					}
					else
					{
						AlertManager.ShowNoInternetAlert("restore previous purchases", true);
					}
					if (PlayerProfiler.HaveNotExplainedRestore)
					{
						PlayerProfiler.OnExplainedRestore();
					}
				}
				break;
			case ButtonUtils.Name.Language:
				if (buttons[buttonThatWasHit].IsNotPressed)
				{
					flag = true;
					TransitionMenuTo(Menu.Languages, menuTransitionTime);
				}
				break;
			case ButtonUtils.Name.Back:
				if (buttons[buttonThatWasHit].IsNotPressed)
				{
					flag = true;
					TransitionMenuTo(Menu.Main, menuTransitionTime);
				}
				break;
			default:
				Debug.LogError(string.Format("MEMN: ERROR: Registered unhandled button press of {0} in Settings Menu.  Make sure this button is added to UpdateSettingMenu()'s nested case statement", buttonThatWasHit));
				break;
			}
			if (flag.HasValue)
			{
				buttons[buttonThatWasHit].SetPressedTo(flag.Value);
				if (flag2)
				{
					TimedDepress(buttonThatWasHit);
				}
			}
			break;
		}
		default:
			Debug.LogError(string.Format("MEMN: ERROR: Reached unhandled inputState of {0} in Settings Menu.  Check logic of UpdateMainMenu()'s nested case statement", inputState));
			break;
		}
	}

	private void UpdateLanguagesMenu()
	{
		switch (inputState)
		{
		case InputState.Paused:
			break;
		case InputState.Ready:
		{
			ButtonUtils.Name buttonThatWasHit;
			if (!Input.GetMouseButtonDown(0) || !ButtonUtils.ButtonWasHit(buttons, out buttonThatWasHit))
			{
				break;
			}
			bool? flag = null;
			bool flag2 = false;
			switch (buttonThatWasHit)
			{
			case ButtonUtils.Name.Language0:
			case ButtonUtils.Name.Language1:
			case ButtonUtils.Name.Language2:
			case ButtonUtils.Name.Language3:
			case ButtonUtils.Name.Language4:
			case ButtonUtils.Name.Language5:
			case ButtonUtils.Name.Language6:
			case ButtonUtils.Name.Language7:
			case ButtonUtils.Name.Language8:
			case ButtonUtils.Name.Language9:
			case ButtonUtils.Name.Language10:
			case ButtonUtils.Name.Language11:
				if (buttons[buttonThatWasHit].IsNotPressed)
				{
					flag = true;
					int languageTo;
					switch (buttonThatWasHit)
					{
					case ButtonUtils.Name.Language0:
						languageTo = 0;
						break;
					case ButtonUtils.Name.Language1:
						languageTo = 1;
						break;
					case ButtonUtils.Name.Language2:
						languageTo = 2;
						break;
					case ButtonUtils.Name.Language3:
						languageTo = 3;
						break;
					case ButtonUtils.Name.Language4:
						languageTo = 4;
						break;
					case ButtonUtils.Name.Language5:
						languageTo = 5;
						break;
					case ButtonUtils.Name.Language6:
						languageTo = 6;
						break;
					case ButtonUtils.Name.Language7:
						languageTo = 7;
						break;
					case ButtonUtils.Name.Language8:
						languageTo = 8;
						break;
					case ButtonUtils.Name.Language9:
						languageTo = 9;
						break;
					case ButtonUtils.Name.Language10:
						languageTo = 10;
						break;
					case ButtonUtils.Name.Language11:
						languageTo = 11;
						break;
					default:
						Debug.LogError(string.Format("MEMN: ERROR: Registered unhandled language-button press of {0} in Languages Menu.  Make sure this button is added to UpdateLanguagesMenu()'s twice-nested case statement", buttonThatWasHit));
						languageTo = 0;
						break;
					}
					Localizer.SetLanguageTo(languageTo);
					LoadLocalisedGameTitle();
					Localize();
					TransitionMenuTo(Menu.Settings, menuTransitionTime);
				}
				break;
			case ButtonUtils.Name.BackAlt:
				if (buttons[buttonThatWasHit].IsNotPressed)
				{
					flag = true;
					TransitionMenuTo(Menu.Settings, menuTransitionTime);
				}
				break;
			default:
				Debug.LogError(string.Format("MEMN: ERROR: Registered unhandled button press of {0} in Languages Menu.  Make sure this button is added to UpdateLanguagesMenu()'s nested case statement", buttonThatWasHit));
				break;
			}
			if (flag.HasValue)
			{
				buttons[buttonThatWasHit].SetPressedTo(flag.Value);
				if (flag2)
				{
					TimedDepress(buttonThatWasHit);
				}
			}
			break;
		}
		default:
			Debug.LogError(string.Format("MEMN: ERROR: Reached unhandled inputState of {0} in Settings Menu.  Check logic of UpdateMainMenu()'s nested case statement", inputState));
			break;
		}
	}

	private void UpdateMidground()
	{
		if (midgroundStarting)
		{
			midgroundAppearTimer -= Time.smoothDeltaTime;
			float offsetPercent;
			if (midgroundAppearTimer <= 0f)
			{
				if (inputState == InputState.Paused)
				{
					inputState = InputState.Ready;
				}
				midgroundStarting = false;
				offsetPercent = 0f;
			}
			else
			{
				offsetPercent = midgroundAppearTimer / 1.5f;
			}
			OffsetCamera(offsetPercent);
		}
		TransformUtils.SpeedY(Camera.main.transform, speed * speedMultiplier);
		MidgroundManager.UpdateMidground();
	}

	private void Localize()
	{
		string termCapitalized = Localizer.GetTermCapitalized("Settings");
		settingsTitle.text = termCapitalized;
		settingsTitleShadow.text = termCapitalized;
		string termCapitalized2 = Localizer.GetTermCapitalized("Languages");
		languagesTitle.text = termCapitalized2;
		languagesTitleShadow.text = termCapitalized2;
		buttons[ButtonUtils.Name.Music].SetText(Localizer.GetTermCapitalized("Music On"), Localizer.GetTermCapitalized("Music Off"));
		buttons[ButtonUtils.Name.Sound].SetText(Localizer.GetTermCapitalized("Sounds On"), Localizer.GetTermCapitalized("Sounds Off"));
		buttons[ButtonUtils.Name.Alerts].SetText(Localizer.GetTermCapitalized("Alerts On"), Localizer.GetTermCapitalized("Alerts Off"));
		buttons[ButtonUtils.Name.Restore].SetText(Localizer.GetTermCapitalized("Restore"));
		buttons[ButtonUtils.Name.Language].SetText(Localizer.GetTermCapitalized("Language"));
	}

	private void TransitionMenuTo(Menu transitionMenuTarget, float timeToTransition, bool smoothStart = true)
	{
		menuTransitionTime = timeToTransition;
		menuTarget = transitionMenuTarget;
		inputState = InputState.Paused;
		menuTransitionTimer = menuTransitionTime;
		smoothedMenuTransitionStart = smoothStart;
	}

	private void OnTransitionMenuComplete()
	{
		switch (menu)
		{
		case Menu.Main:
			buttons[ButtonUtils.Name.Settings].Depress();
			buttons[ButtonUtils.Name.Leaderboard].Depress();
			buttons[ButtonUtils.Name.Achievements].Depress();
			buttons[ButtonUtils.Name.Play].Depress();
			buttons[ButtonUtils.Name.Left].Depress();
			buttons[ButtonUtils.Name.Right].Depress();
			break;
		case Menu.Settings:
			buttons[ButtonUtils.Name.Back].Depress();
			buttons[ButtonUtils.Name.Language].Depress();
			break;
		case Menu.Languages:
			buttons[ButtonUtils.Name.BackAlt].Depress();
			buttons[ButtonUtils.Name.Language0].Depress();
			buttons[ButtonUtils.Name.Language1].Depress();
			buttons[ButtonUtils.Name.Language2].Depress();
			buttons[ButtonUtils.Name.Language3].Depress();
			buttons[ButtonUtils.Name.Language4].Depress();
			buttons[ButtonUtils.Name.Language5].Depress();
			buttons[ButtonUtils.Name.Language6].Depress();
			buttons[ButtonUtils.Name.Language7].Depress();
			buttons[ButtonUtils.Name.Language8].Depress();
			buttons[ButtonUtils.Name.Language9].Depress();
			buttons[ButtonUtils.Name.Language10].Depress();
			buttons[ButtonUtils.Name.Language11].Depress();
			break;
		}
		menu = menuTarget.Value;
		menuTarget = null;
		inputState = InputState.Ready;
	}

	private void TimedDepress(ButtonUtils.Name pressedButton)
	{
		if (buttonsToDepress.IsEmpty)
		{
			buttonsToDepress.Add(new ButtonDepress(pressedButton));
			return;
		}
		bool flag = false;
		for (int i = 0; i < buttonsToDepress.Length; i++)
		{
			if (buttonsToDepress[i].Button == pressedButton)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			buttonsToDepress.Add(new ButtonDepress(pressedButton));
		}
	}

	private void UpdateButtonDepresses()
	{
		for (int i = 0; i < buttonsToDepress.Length; i++)
		{
			if (buttonsToDepress[i].ShouldDepress())
			{
				buttons[buttonsToDepress[i].Button].Depress();
				buttonsJustDepressed.Add(i);
			}
		}
		if (buttonsJustDepressed.IsNotEmpty)
		{
			for (int num = buttonsJustDepressed.Length - 1; num >= 0; num--)
			{
				buttonsToDepress.RemoveAt(buttonsJustDepressed[num]);
			}
			buttonsJustDepressed.Reset();
		}
	}

	private void LoadTextureNativePNGUseDefaultIfNotFound(string resourceFilePath, string defaultResourceFilePath, Material targetMaterial)
	{
		TextAsset textAsset = Resources.Load(resourceFilePath, typeof(TextAsset)) as TextAsset;
		if (textAsset == null)
		{
			textAsset = Resources.Load(defaultResourceFilePath, typeof(TextAsset)) as TextAsset;
		}
		Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
		texture2D.LoadImage(textAsset.bytes);
		targetMaterial.mainTexture = texture2D;
		targetMaterial.shader = Shader.Find("Unlit/Transparent");
		texture2D.Compress(true);
	}

	private void LoadLocalisedGameTitle()
	{
		Localizer.Language currentLanguageEnum = Localizer.GetCurrentLanguageEnum();
		string text = "Localisation/GameTitleEnglish";
		switch (currentLanguageEnum)
		{
		case Localizer.Language.Spanish:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleSpanish", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.ChineseSimplified:
		case Localizer.Language.ChineseTraditional:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleChinese", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.Japanese:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleJapanese", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.French:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleFrench", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.German:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleGerman", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.Italian:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleItalian", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.Russian:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleRussian", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.Arabic:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleArabic", text, gameTitle.GetComponent<Renderer>().material);
			break;
		case Localizer.Language.Korean:
			LoadTextureNativePNGUseDefaultIfNotFound("Localisation/GameTitleKorean", text, gameTitle.GetComponent<Renderer>().material);
			break;
		default:
			LoadTextureNativePNGUseDefaultIfNotFound(text, text, gameTitle.GetComponent<Renderer>().material);
			break;
		}
		TransformUtils.Show(gameTitle);
	}

	private void OnMainMenuLoaded()
	{
		mainLoadingMaterial.SetFloat("_Ammount", 0f);
		TransformUtils.Hide(loadingCirclesGroup);
		LoadLocalisedGameTitle();
		inputState = InputState.Ready;
		menuState = MenuState.Ready;
		midgroundStarting = true;
		midgroundAppearTimer = 1.5f;
		if (!setupComplete)
		{
			SetupAll();
		}
		if (startingLoad)
		{
			buttons[ButtonUtils.Name.Settings].Show();
			buttons[ButtonUtils.Name.GoogleControllerActive].Show();
			buttons[ButtonUtils.Name.Play].Show();
		}
	}

	private void OnClick(Vector2? clickedPosition = null)
	{
		if (clickedPosition.HasValue)
		{
			bool wasBelow;
			if (displayedWorldRange.Contains(clickedPosition.Value.x, out wasBelow))
			{
				SelectCurrentWorld();
				return;
			}
			bool moveRight = !wasBelow;
			StartMoveToNextWorld(moveRight);
		}
		else
		{
			SelectCurrentWorld();
		}
	}

	private void StartMoveToNextWorld(bool moveRight, bool moveToMain = false)
	{
		int num;
		if (moveToMain)
		{
			num = 0;
			delayedAutoMoveToMain = true;
		}
		else
		{
			int num2 = (moveRight ? 1 : (-1));
			num = currentWorldIndex + num2;
		}
		if (MathUtils.IndexIsWithin(num, worlds))
		{
			lastWorldIndex = currentWorldIndex;
			currentWorldIndex = num;
			worldsSwiping = true;
			worldSwipeTime = 0.5f;
			if (moveToMain)
			{
				float num3 = MathUtils.AbsInt(lastWorldIndex - currentWorldIndex) - 1;
				worldSwipeTime += 0f * num3;
			}
			worldSwipeTimer = worldSwipeTime;
			SetupWorldSwipeRange();
			MaterialManager.StartMenuRecolorTo(currentWorldNumber - 1);
			StartBackgroundTransition(lastWorldIndex, currentWorldIndex);
			MusicPlayer.FadeIntoManualStart(currentWorldNumber - 1);
		}
		else if (!moveRight)
		{
			TransitionMenuTo(Menu.Main, 1f);
		}
		if (moveRight)
		{
			if (currentWorldIndex < worlds.Length - 1)
			{
				buttons[ButtonUtils.Name.Right].Press();
			}
			else
			{
				buttons[ButtonUtils.Name.Right].Hide();
			}
		}
		else
		{
			buttons[ButtonUtils.Name.Left].Press();
		}
	}

	private void SetupWorldSwipeRange()
	{
		float x = worldSwiper.localPosition.x;
		float num = -1f * ((float)currentWorldIndex * 7f);
		if (worldSwipeRange == null)
		{
			worldSwipeRange = new MathUtils.Range(num, x);
		}
		else
		{
			worldSwipeRange.Set(num, x);
		}
	}

	private void UpdateMoveToNextWorld()
	{
		worldSwipeTimer -= Time.smoothDeltaTime;
		bool flag = worldSwipeTimer <= 0f;
		float num;
		float num2;
		float num3;
		float newX;
		if (flag)
		{
			worldSwipeTimer = 0f;
			num = 0f;
			num2 = 0f;
			num3 = 1f;
			newX = worldSwipeRange.Min;
			OnMovedToNextWorld();
		}
		else
		{
			num = worldSwipeTimer / worldSwipeTime;
			num2 = FloatAnim.Smooth(num, true, false);
			num3 = 1f - num2;
			newX = ((!delayedAutoMoveToMain) ? worldSwipeRange.FromPercent(num2) : worldSwipeRange.FromPercent(num));
			MusicPlayer.FadeIntoManualUpdate(num);
		}
		TransformUtils.SetX(worldSwiper, newX, true);
		TransformUtils.SetX(worldGeoGroup, newX, true);
		for (int i = 0; i < worlds.Length; i++)
		{
			worlds[i].UpdateSwipingAnim(num, flag);
		}
		MaterialManager.Recolor(1f - num2);
		UpdateBackgroundTransition(num3, lastWorldIndex, currentWorldIndex, true);
		Pip.SetTransitionPercent(num2);
		PositionPips(Mathf.Lerp(lastWorldIndex, currentWorldIndex, num3));
		for (int j = 0; j < progressPips.Length; j++)
		{
			if (j == currentWorldIndex)
			{
				progressPips[j].SetCenterTo(num3);
			}
			else if (j == lastWorldIndex)
			{
				progressPips[j].SetCenterTo(num2);
			}
			else
			{
				progressPips[j].SetCenterTo(0f);
			}
		}
	}

	private void OnMovedToNextWorld()
	{
		worldsSwiping = false;
		MaterialManager.EndRecolor();
		speedMultiplier = 1f;
		StartBackgroundTransitionEnd();
		buttons[ButtonUtils.Name.Left].Depress();
		if (currentWorldIndex < worlds.Length - 1)
		{
			buttons[ButtonUtils.Name.Right].ShowDepressed();
		}
		MusicPlayer.FadeIntoManualEnd();
		if (delayedAutoMoveToMain)
		{
			delayedAutoMoveToMain = false;
			TransitionMenuTo(Menu.Main, 0.5f, false);
		}
	}

	private void SnapToCurrentWorld()
	{
	}

	private void SelectCurrentWorld()
	{
		worlds[currentWorldIndex].Select();
		selectedWorldNumber = currentWorldIndex;
		worldSelectionTimer = 1f;
		inputState = InputState.Paused;
		MusicPlayer.FadeOut(1f);
	}

	private void StartBackgroundTransition(int firstWorldIndex, int secondWorldIndex)
	{
		int first = LevelCatalog.AtMenuIndex(firstWorldIndex).WorldNumber - 1;
		int second = LevelCatalog.AtMenuIndex(secondWorldIndex).WorldNumber - 1;
		MaterialManager.StartLerping(first, second);
		MidgroundManager.StartGeneratingForWorld(second);
	}

	private void UpdateBackgroundTransition(float transitionPercent, int worldAlphaIndex, int worldColorIndex, bool transitionStarting)
	{
		if (transitionStarting)
		{
			MaterialManager.UpdateLerping(transitionPercent);
		}
		speedMultiplier = 1f + transitionPercent * 1f;
	}

	private void StartBackgroundTransitionEnd()
	{
		backgroundTransitionEndTime = 0.75f;
		backgroundTransitionEndTimer = backgroundTransitionEndTime;
		backgroundTransitionEnding = true;
	}

	private void UpdateBackgroundTransitionEnd()
	{
		backgroundTransitionEndTimer -= Time.smoothDeltaTime;
		float transitionPercent;
		if (backgroundTransitionEndTimer <= 0f)
		{
			backgroundTransitionEnding = false;
			transitionPercent = 0f;
		}
		else
		{
			transitionPercent = backgroundTransitionEndTimer / backgroundTransitionEndTime;
		}
		UpdateBackgroundTransition(transitionPercent, currentWorldIndex, currentWorldIndex, false);
	}

	private void EndBackgroundTransition()
	{
		TryResetShaders(currentWorldNumber - 1);
	}

	private void TryResetShaders(int? endingTextureIndex)
	{
		MaterialManager.EndLerping(endingTextureIndex);
	}

	private void SetPipsTo(int pipIndex)
	{
		PositionPips(pipIndex);
		progressPips[pipIndex].SetCenterTo(1f);
	}

	private void PositionPips(float selectedIndex)
	{
		// A seven-dot window keeps the original six-unit span readable on phones.
		int visible = Mathf.Min(7, progressPips.Length);
		float half = (visible - 1) * 0.5f;
		float center = Mathf.Clamp(selectedIndex, half, progressPips.Length - 1 - half);
		for (int i = 0; i < progressPips.Length; i++)
			progressPips[i].Position(i - center, half);
	}

	private void SetupPips()
	{
		Pip.SetTransitionPercent(0f);
		for (int i = 0; i < progressPips.Length; i++)
		{
			progressPips[i].SetCenterTo(0f);
			progressPips[i].SetProgressTo(ProgressManager.GetProgressFloat(LevelCatalog.AtMenuIndex(i).WorldNumber - 1));
		}
		updatingProgressPips = true;
	}

	private void OffsetCamera(float offsetPercent)
	{
		if (offsetPercent == 0f)
		{
			cameraIsOffset = false;
		}
		else
		{
			if (offsetPercent != 1f)
			{
				offsetPercent = FloatAnim.Smooth(offsetPercent, true, false);
			}
			if (!cameraIsOffset)
			{
				cameraIsOffset = true;
			}
		}
		float newZ = -22.5f * offsetPercent;
		TransformUtils.SetZ(cameraOffseter, newZ, true);
		if (startingLoad)
		{
			speedMultiplier = 1f - offsetPercent;
			FloatAnim.Smooth(ref speedMultiplier, false, true);
		}
		else
		{
			speedMultiplier = 1f;
		}
	}

	private static void RestorePurchases(bool success)
	{
		if (success)
		{
			PlayerProfiler.OnPremiumModePurchase();
			LivesManager.RechargeLives(GameManager.StartingLives);
		}
		AlertManager.ShowAlert(Localizer.GetTerm("Restore"), Localizer.GetTerm("Restore Completed"));
	}
}
