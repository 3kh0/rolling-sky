using System.Collections.Generic;
using UnityEngine;

public class GameHud : MonoBehaviour
{
	private enum State
	{
		Prompt = 0,
		PromptPaused = 1,
		Gameplay = 2,
		GameplayPaused = 3,
		GameplayUnpausing = 4,
		GameplayEnded = 5,
		GameOverShown = 6,
		NextWorldAppearing = 7,
		NextWorldUnlocking = 8,
		NextWorldRevealing = 9,
		NextWorldSelecting = 10,
		NextWorldSelected = 11,
		GetLivesAppearing = 12,
		GetLivesShown = 13,
		GetLivesDisappearing = 14,
		TutorialShown = 15,
		Finished = 16
	}

	private enum Display
	{
		Normal = 0,
		Highscore = 1,
		Complete = 2,
		OutOfLives = 3
	}

	private struct SlidingPart
	{
		private Transform transform;

		private float startingPosition;

		private float slideDistance;

		public readonly float PartHeight;

		private float slideDelay;

		public SlidingPart(Transform partTransform, float slideDistance)
		{
			transform = partTransform;
			startingPosition = transform.localPosition.x;
			this.slideDistance = slideDistance;
			PartHeight = transform.position.z;
			slideDelay = 0f;
		}

		public void SetSlideDelay(float newSlideDelay)
		{
			slideDelay = newSlideDelay;
		}

		public void Slide(float slidePercent)
		{
			slidePercent *= 1f - slideDelay;
			slidePercent += slideDelay;
			float num = FloatAnim.Smooth(slidePercent, false, true);
			float num2 = (1f - num) * slideDistance;
			transform.localPosition = new Vector3(startingPosition + num2, transform.localPosition.y, transform.localPosition.z);
		}
	}

	private struct DisplayData
	{
		public readonly Display Type;

		public readonly string TitleText;

		public DisplayData(Display displayType, string titleText)
		{
			Type = displayType;
			TitleText = titleText;
		}
	}

	private const bool debugsEnabled = false;

	private const bool debugButtons = false;

	private const bool debugButtonAutoHits = false;

	private const bool debugInterstitialFailures = false;

	private const bool debugTextureLoading = false;

	private const bool isEnabled = true;

	private const bool isDisabled = false;

	private const bool showShadows = false;

	private const bool showLock = false;

	private const float nextWorldDepthEnding = 4f;

	private const bool tutorialInstantlyDismisssable = false;

	private const string ballWord = "balls";

	private const string moreWord = "more";

	private const string livesDescriptor = "balls remaining";

	private const string lifeDescriptor = "ball remaining!";

	private const string noLivesDescriptor = "out of balls";

	private const string rechargeDescriptor = "Recharge in:";

	private const string getLivesSeperator = "or";

	private const string getLivesAltDescriptor = "get balls";

	private const string getLivesFreeDescriptor = "free";

	private const string getLivesIapDescriptor = "Unlimited";

	private const string pausedDescriptor = "Paused";

	private const string getLivesNotificationMessage = "Your balls have recharged and are ready to be used";

	private const string recordingPopupTitle = "Everyplay";

	private const string watchVideoNoneCachedTitle = "Not Ready";

	private const string iapTitle = "Upgrade";

	private const string notificationTitle = "Notifications";

	private const string watchVideoNoInternetMessage = "watch a video for free balls";

	private const string recordingPopupMessage = "Pressing this button will record your next game for sharing online";

	private const string watchVideoNoneCachedMessage = "Sadly a video is not yet cached and ready for you - just a moment";

	private const string iapMessage = "Would you like to upgrade to the full version for unlimited balls?";

	private const string notificationMessage = "Would you like to be automatically notified when your balls have recharged?";

	private const string tutorialPopupDirectory = "Tutorial";

	private const string tutorialPopupFilenamePhone = "TUTORIAL_IPHONE";

	private const string tutorialPopupFilenamePad = "TUTORIAL_IPAD";

	private const string tutorialTitleText = "Tip";

	private const string tutorialPhoneMessageText = "Slide THUMB below ball\nfor better control";

	private const string tutorialPadMessageText = "Slide finger below ball\nfor better control";

	private const string tutorialButtonText = "OK";

	private const string ammountFloatName = "_Ammount";

	private const string redColorName = "_ColorR";

	private const string greenColorName = "_ColorG";

	private const string blueColorName = "_ColorB";

	private const string alphaFloatName = "_Alpha";

	private const float hudMaxAlpha = 0.2f;

	private const float backgroundOverlayAlpha = 0.25f;

	private const float progressPosWithoutRate = 1.325f;

	private const float everyplayMovedPos = -0.896966f;

	private const float everyplayDepressTime = 0.5f;

	private const float minHighscoreToShow = 0.1f;

	private const float gameOverSlideDistance = 1f;

	private const float gameOverSlideUpDistance = 1.5f;

	private const float nextWorldSlideDistance = 1.745f;

	private const float slideTime = 1f;

	private const float getLivesAnimTime = 1.5f;

	private const float nextWorldHoldTime = 0.5f;

	private const float nextWorldRevealTime = 1f;

	private const float progressTime = 1f;

	private const float gameOverSlideDelayMax = 0.5f;

	private const float gameOverBackgroundAlpha = 0.25f;

	private const string fadeAlphaName = "_Alpha";

	private const float hudTopSlideAmmount = 0.5f;

	private const float nextWorldLockShakeAmnt = 0.025f;

	private const float nextWorldLockShakeFrequency = 10f;

	private const float gameOverHighscoreBarMaxX = 0.3132f;

	private const float promptSlideTime = 2f;

	private const float recheckVideoTime = 5f;

	private const float progressCharacterSize = 0.0825f;

	private const float tutorialOverlayAlpha = 0.7f;

	private const float tutorialCooldownTime = 1.5f;

	private const float unpauseTime = 2.75f;

	private const float oneDigitGemsTotalX = 0.34f;

	private const float twoDigitGemsTotalX = 0.664f;

	private const float oneDigitGemsIconX = -0.93f;

	private const float twoDigitGemsIconX = -1.242f;

	private static ArrayUtils.List<DisplayData> displayData = new ArrayUtils.List<DisplayData>(new DisplayData(Display.Normal, "Last"), new DisplayData(Display.Highscore, "Best"), new DisplayData(Display.Complete, "Complete"), new DisplayData(Display.OutOfLives, "Last"));

	private static Elastic.Single savesSmoothed = new Elastic.Single(4f);

	private static Elastic.Single progressSmoothed = new Elastic.Single(4f);

	private static ArrayUtils.SmartDict<ButtonUtils.Name, ButtonUtils.Button> buttons;

	private static bool progressMaxed;

	private static FrameAnim.Frame gameOver;

	private static FrameAnim.Frame nextWorld;

	private static FrameAnim.Frame getLives;

	private static FrameAnim.Frame paused;

	private static FrameAnim.Frame cameraOverlay;

	private static FrameAnim.Frame backgroundOverlay;

	private static FrameAnim.Frame prompt;

	private static FrameAnim.Frame gameplayTop;

	private static FrameAnim.Frame tutorial;

	private static TextMesh livesText;

	private static TextMesh livesTextShadow;

	private static TextMesh savesCount;

	private static Renderer savesBar;

	private static Transform progressGroup;

	private static Transform progressOffsetGroup;

	private static TextMesh progressText;

	private static TextMesh progressTextShadow;

	private static TextMesh progressPercentSymbol;

	private static TextMesh progressPercentSymbolShadow;

	private static Renderer progressBar;

	private static Transform gemsGroup;

	private static TextMesh gemsText;

	private static TextMesh gemsTextShadow;

	private static TextMesh gemsTotalText;

	private static TextMesh gemsTotalTextShadow;

	private static Renderer gemsIcon;

	private static Renderer gemsIconShadow;

	private static TextMesh score;

	private static TextMesh gameplayCrowns;

	private static Transform nextWorldLock;

	private static Material nextWorldMaterial;

	private static Transform nextWorldTitleQuad;

	private static Material promptMaterial;

	private static Material gameOverProgressMaterial;

	private static Material backgroundOverlayMaterial;

	private static Material cameraOverlayMaterial;

	private static bool cameraOverlayFading;

	private static bool cameraOverlayFadingOut;

	private static float cameraOverlayFadeTimer;

	private static float animTimer;

	private static float nextWorldOverlayMax;

	private static bool showingPrompt;

	private static float everyplayDepressTimer;

	private static bool getLivesTimeIsFake;

	private static float timeLastCheckedIfVideoWasReady;

	private static TextMesh getLivesTitle;

	private static TextMesh getLivesTime;

	private static TextMesh getLivesTimeTitle;

	private static Clock getLivesClock;

	private static TextMesh pausedTitle;

	private static Material tutorialMaterial;

	private static TextMesh tutorialTitle;

	private static TextMesh tutorialMessage;

	private static bool showNextWorld;

	private static Color nextWorldColor;

	private static float nextWorldDepthStarting;

	private static bool newHighscore;

	private static float highscoreShown;

	private static bool tutorialDismissable;

	private static float tutorialCooldownTimer;

	private static float unpauseTimer;

	private static int displayedProgressPercentage;

	private static int debugIdsShown;

	private static float altAnimTimer;

	private static State state;

	private static Display menuDisplayed;

	private static float popupWaitTimer;

	private static float starsPopTimer;

	private static float starsPopAmnt;

	private static bool progressNotMaxed
	{
		get
		{
			return !progressMaxed;
		}
	}

	private static bool gameIsOver
	{
		get
		{
			return state == State.GameOverShown;
		}
	}

	public static void Initialize(Dictionary<HudPart, Transform> hudParts, Ball theBall)
	{
		score = hudParts[HudPart.ScoreText].GetComponent<TextMesh>();
		livesText = hudParts[HudPart.LivesText].GetComponent<TextMesh>();
		livesTextShadow = hudParts[HudPart.LivesTextShadow].GetComponent<TextMesh>();
		savesCount = hudParts[HudPart.SavesText].GetComponent<TextMesh>();
		savesBar = hudParts[HudPart.SavesBar].GetComponent<Renderer>();
		progressGroup = hudParts[HudPart.ProgressGroup];
		progressOffsetGroup = hudParts[HudPart.ProgressOffsetGroup];
		progressText = hudParts[HudPart.ProgressText].GetComponent<TextMesh>();
		progressTextShadow = hudParts[HudPart.ProgressTextShadow].GetComponent<TextMesh>();
		progressPercentSymbol = hudParts[HudPart.ProgressPercentSymbol].GetComponent<TextMesh>();
		progressPercentSymbolShadow = hudParts[HudPart.ProgressPercentSymbolShadow].GetComponent<TextMesh>();
		progressBar = hudParts[HudPart.ProgressBar].GetComponent<Renderer>();
		nextWorldLock = hudParts[HudPart.NextWorldLock];
		nextWorldTitleQuad = hudParts[HudPart.NextWorldTitleQuad];
		gemsGroup = hudParts[HudPart.GemsGroup];
		gemsText = hudParts[HudPart.GemsText].GetComponent<TextMesh>();
		gemsTextShadow = hudParts[HudPart.GemsTextShadow].GetComponent<TextMesh>();
		gemsTotalText = hudParts[HudPart.GemsTotalText].GetComponent<TextMesh>();
		gemsTotalTextShadow = hudParts[HudPart.GemsTotalTextShadow].GetComponent<TextMesh>();
		gemsIcon = hudParts[HudPart.GemsIcon].GetComponent<Renderer>();
		gemsIconShadow = hudParts[HudPart.GemsIconShadow].GetComponent<Renderer>();
		getLivesTitle = hudParts[HudPart.GetLivesTitle].GetComponent<TextMesh>();
		getLivesTime = hudParts[HudPart.GetLivesTime].GetComponent<TextMesh>();
		getLivesTimeTitle = hudParts[HudPart.GetLivesTimeTitle].GetComponent<TextMesh>();
		getLivesClock = hudParts[HudPart.GetLivesClock].GetComponent<Clock>();
		pausedTitle = hudParts[HudPart.PausedTitle].GetComponent<TextMesh>();
		tutorialTitle = hudParts[HudPart.TutorialTitle].GetComponent<TextMesh>();
		tutorialMessage = hudParts[HudPart.TutorialMessage].GetComponent<TextMesh>();
		gameOver = new FrameAnim.Frame(true, hudParts[HudPart.GameOverGroup]);
		nextWorld = new FrameAnim.Frame(hudParts[HudPart.NextWorldGroup]);
		getLives = new FrameAnim.Frame(true, hudParts[HudPart.GetLivesGroup]);
		paused = new FrameAnim.Frame(true, hudParts[HudPart.PausedGroup]);
		cameraOverlay = new FrameAnim.Frame(true, hudParts[HudPart.CameraOverlay]);
		backgroundOverlay = new FrameAnim.Frame(true, hudParts[HudPart.GameOverBackgroundOverlay]);
		prompt = new FrameAnim.Frame(true, hudParts[HudPart.Prompt]);
		gameplayTop = new FrameAnim.Frame(true, hudParts[HudPart.GameplayTop]);
		tutorial = new FrameAnim.Frame(true, hudParts[HudPart.Tutorial]);
		promptMaterial = prompt.GetRoot().GetComponent<Renderer>().material;
		backgroundOverlayMaterial = backgroundOverlay.GetRoot().GetComponent<Renderer>().material;
		nextWorldMaterial = nextWorld.GetRoot().GetComponent<Renderer>().material;
		tutorialMaterial = tutorial.GetRoot().GetComponent<Renderer>().material;
		gameOverProgressMaterial = progressBar.material;
		buttons = new ArrayUtils.SmartDict<ButtonUtils.Name, ButtonUtils.Button>();
		buttons.Add(ButtonUtils.Name.Back, new ButtonUtils.Button(ButtonUtils.Name.Back, hudParts[HudPart.BackButton]));
		buttons.Add(ButtonUtils.Name.Play, new ButtonUtils.Button(ButtonUtils.Name.Play, hudParts[HudPart.PlayButton]));
		buttons.Add(ButtonUtils.Name.Resume, new ButtonUtils.Button(ButtonUtils.Name.Resume, hudParts[HudPart.ResumeButton]));
		buttons.Add(ButtonUtils.Name.Pause, new ButtonUtils.Button(ButtonUtils.Name.Pause, hudParts[HudPart.PauseButton]));
		buttons.Add(ButtonUtils.Name.Next, new ButtonUtils.Button(ButtonUtils.Name.Next, hudParts[HudPart.NextButton]));
		buttons.Add(ButtonUtils.Name.Record, new ButtonUtils.Button(ButtonUtils.Name.Record, hudParts[HudPart.RecordButton]));
		buttons.Add(ButtonUtils.Name.GetLives, new ButtonUtils.Button(ButtonUtils.Name.GetLives, hudParts[HudPart.GetBallsButton]));
		buttons.Add(ButtonUtils.Name.Video, new ButtonUtils.Button(ButtonUtils.Name.Video, hudParts[HudPart.VideoButton]));
		buttons.Add(ButtonUtils.Name.Shop, new ButtonUtils.Button(ButtonUtils.Name.Shop, hudParts[HudPart.ShopButton]));
		buttons.Add(ButtonUtils.Name.OK, new ButtonUtils.Button(ButtonUtils.Name.OK, hudParts[HudPart.TutorialButton]));
		buttons[ButtonUtils.Name.Pause].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Back].SetRelativeToCorner(0.5623053f);
		buttons[ButtonUtils.Name.Record].SetRelativeToCorner(0.5623053f);
		TransformUtils.SetRelativeToCorner(score, 0.5623053f);
		savesBar.GetComponent<Renderer>().material.SetFloat("_Ammount", 0f);
		savesBar.GetComponent<Renderer>().material.SetColor("_ColorR", new Color(1f, 1f, 1f, 0f));
		savesBar.GetComponent<Renderer>().material.SetColor("_ColorG", new Color(1f, 1f, 1f, 0f));
		progressSmoothed.ForceSetTarget(0f);
		cameraOverlayMaterial = hudParts[HudPart.CameraOverlay].GetComponent<Renderer>().material;
		if (GameManager.QueryAndResetFadeInWorld())
		{
			cameraOverlayMaterial.SetFloat("_Alpha", 1f);
			cameraOverlayFadeTimer = 1f;
			cameraOverlayFading = true;
		}
		else
		{
			cameraOverlay.Hide();
		}
		TransformUtils.Hide(livesTextShadow);
		TransformUtils.Hide(progressTextShadow);
		TransformUtils.Hide(progressPercentSymbolShadow);
		TransformUtils.Hide(gemsTextShadow);
		TransformUtils.Hide(gemsTotalTextShadow);
		TransformUtils.Hide(gemsIconShadow);
		newHighscore = false;
		progressMaxed = false;
		highscoreShown = 0f;
		animTimer = 0f;
		buttons[ButtonUtils.Name.Back].Hide();
		state = State.Gameplay;
		ScoreManager.Initialize();
	}

	public static void UpdateHud()
	{
		if (gameplayCrowns != null) gameplayCrowns.color = score.color;
		if (cameraOverlayFading)
		{
			cameraOverlayFadeTimer -= Time.smoothDeltaTime;
			if (cameraOverlayFadeTimer <= 0f)
			{
				cameraOverlayFadeTimer = 0f;
				cameraOverlayFading = false;
			}
			else
			{
				float value = cameraOverlayFadeTimer / 1f;
				float value2 = FloatAnim.Smooth(value, true, false);
				cameraOverlayMaterial.SetFloat("_Alpha", value2);
			}
		}
		bool isAnimationDone;
		switch (state)
		{
		case State.Prompt:
		{
			animTimer += Time.smoothDeltaTime;
			float animPercent = animTimer / 2f;
			float num = 0f - FloatAnim.Wave01(animPercent);
			promptMaterial.SetFloat("_Ammount", num);
			break;
		}
		case State.PromptPaused:
			break;
		case State.GameOverShown:
			break;
		case State.NextWorldAppearing:
		{
			float animPercent = GetAnimPercent(ref animTimer, 1f, out isAnimationDone);
			if (isAnimationDone)
			{
				float num = 1f;
				state = State.NextWorldUnlocking;
				animTimer = 0.5f;
			}
			else
			{
				float num = FloatAnim.Smooth(animPercent, true, true);
			}
			gameOver.SetX((0f - animPercent) * 1.745f, true);
			break;
		}
		case State.NextWorldUnlocking:
		{
			float animPercent = GetAnimPercent(ref animTimer, 0.5f, out isAnimationDone);
			if (isAnimationDone)
			{
				StartNextWorldRevealing();
			}
			break;
		}
		case State.NextWorldRevealing:
		{
			float animPercent = GetAnimPercent(ref animTimer, 1f, out isAnimationDone);
			RevealNextWorld(animPercent, isAnimationDone);
			break;
		}
		case State.NextWorldSelecting:
		{
			float animPercent = GetAnimPercent(ref animTimer, 1f, out isAnimationDone);
			float num;
			if (isAnimationDone)
			{
				num = 1f;
				state = State.NextWorldSelected;
			}
			else
			{
				num = FloatAnim.Smooth(animPercent, true, false);
			}
			cameraOverlayMaterial.SetFloat("_Alpha", num);
			nextWorld.SetZ(MathUtils.FromPercent(num, nextWorldDepthStarting, 4f), true);
			break;
		}
		case State.NextWorldSelected:
			GameManager.EndGame(true);
			state = State.Finished;
			break;
		case State.GetLivesAppearing:
		{
			getLivesClock.UpdateClock();
			float animPercent = GetAnimPercent(ref animTimer, 1.5f, out isAnimationDone);
			float num;
			if (isAnimationDone)
			{
				num = 1f;
				state = State.GetLivesShown;
			}
			else
			{
				num = FloatAnim.Smooth(animPercent, true, true);
			}
			gameOver.SetY(num * 1.5f, true);
			getLives.SetY((num - 1f) * 1.5f, true);
			break;
		}
		case State.GetLivesShown:
			getLivesClock.UpdateClock();
			TryRecheckForRewardVideo();
			break;
		case State.GetLivesDisappearing:
		{
			float animPercent = GetAnimPercent(ref animTimer, 1.5f, out isAnimationDone);
			float num;
			if (isAnimationDone)
			{
				num = 1f;
				getLives.Hide();
				state = State.GameOverShown;
			}
			else
			{
				num = FloatAnim.Smooth(animPercent, true, true);
			}
			gameOver.SetY((num - 1f) * 1.5f, true);
			getLives.SetY(num * 1.5f, true);
			break;
		}
		case State.TutorialShown:
			if (!tutorialDismissable)
			{
				tutorialCooldownTimer -= Time.deltaTime;
				if (tutorialCooldownTimer <= 0f)
				{
					tutorialCooldownTimer = 0f;
					tutorialDismissable = true;
					buttons[ButtonUtils.Name.OK].ShowDepressed();
				}
			}
			break;
		case State.GameplayUnpausing:
			unpauseTimer -= Time.deltaTime;
			if (unpauseTimer <= 0f)
			{
				unpauseTimer = 0f;
				GameManager.UnpauseGame();
			}
			else
			{
				DisplayUnpauseCountdown();
			}
			break;
		case State.Gameplay:
		case State.GameplayPaused:
		case State.GameplayEnded:
			break;
		}
	}

	private static void AnimateGameOver(float animPercent, bool animDone)
	{
		float num;
		float num3;
		if (animDone)
		{
			animPercent = 1f;
			num = 1f;
			float num2 = 1f;
			num3 = 1f;
		}
		else
		{
			float num2 = MathUtils.ToPercent01(animTimer, 1f, 2f, true);
			num3 = FloatAnim.Smooth(num2, false, true);
			num = FloatAnim.Smooth(animPercent, false, true);
		}
		backgroundOverlayMaterial.SetFloat("_Alpha", MaterialManager.GetGuiOverlayAlpha() * num3);
		float value = ProgressManager.GetLastProgressFloat() * num;
		gameOverProgressMaterial.SetFloat("_Ammount", value);
	}

	public static void DisplayProgress(int progressPercentage, float progressPercentAsFloat)
	{
		score.text = progressPercentage + "%";
	}

	public static void SetSaves(float saves, int savesAsInt)
	{
	}

	public static void SetSegmentAdded(string segmentIdString)
	{
	}

	public static bool CheckButtons(out ButtonUtils.Name buttonPressed)
	{
		bool flag = ButtonUtils.ButtonWasHit(buttons, out buttonPressed);
		bool result = false;
		if (flag ? true : false)
		{
			bool? flag2 = null;
			switch (buttonPressed)
			{
			case ButtonUtils.Name.Back:
				if (buttons[ButtonUtils.Name.Back].IsNotPressed)
				{
					result = true;
					flag2 = true;
				}
				break;
			case ButtonUtils.Name.Play:
				if (buttons[buttonPressed].IsNotPressed)
				{
					result = true;
					flag2 = true;
				}
				break;
			case ButtonUtils.Name.GetLives:
				if (buttons[buttonPressed].IsNotPressed)
				{
					ConfigureGetLivesMenu();
					state = State.GetLivesAppearing;
					animTimer = 1.5f;
					flag2 = true;
				}
				break;
			case ButtonUtils.Name.Next:
				if (buttons[buttonPressed].IsNotPressed)
				{
					buttons[buttonPressed].Press();
					buttons[ButtonUtils.Name.Back].Hide();
					state = State.NextWorldAppearing;
					animTimer = 1f;
					TransformUtils.Hide(nextWorldLock);
					flag2 = true;
				}
				break;
			case ButtonUtils.Name.Record:
				if (buttons[buttonPressed].IsPressed)
				{
					GameManager.SetRecording(false);
				}
				else
				{
					GameManager.SetRecording(true);
					if (PlayerProfiler.HaveNotExplainedRecording)
					{
						AlertManager.ShowAlert("Everyplay", "Pressing this button will record your next game for sharing online", true);
						PlayerProfiler.OnExplainedRecording();
					}
				}
				flag2 = buttons[buttonPressed].IsNotPressed;
				break;
			case ButtonUtils.Name.Video:
				if (buttons[buttonPressed].IsNotPressed)
				{
					NotificationManager.CancelNotifications();
					AnalyticsLogEvent.OnVideoRewardClicked();
					VideoRewardsManager.ShowVideoForReward(RewardForVideo);
					flag2 = true;
				}
				else if (buttons[buttonPressed].IsPressed)
				{
					if (GameManager.HasInternet())
					{
						AlertManager.ShowAlert("Not Ready", "Sadly a video is not yet cached and ready for you - just a moment", true);
					}
					else
					{
						AlertManager.ShowNoInternetAlert("watch a video for free balls", true);
					}
				}
				break;
			case ButtonUtils.Name.Shop:
				if (buttons[buttonPressed].IsNotPressed)
				{
					//Allows user to "Purchase" unlimited balls
					buttons[buttonPressed].Press();
						PlayerProfiler.OnPremiumModePurchase();
						AddBalls(GameManager.StartingLives);
						AnalyticsLogEvent.UpgradePackPurchased();
						/*AnalyticsLogEvent.UpgradePackClicked();
						string formattedPrice = ((!IAPManager.ProductUnlimitedBallsPrice.HasValue) ? null : IAPManager.ProductUnlimitedBallsFormattedPrice);
						AlertManager.ShowConfirmPurchaseAlert("Upgrade", "Would you like to upgrade to the full version for unlimited balls?", formattedPrice, TryPurchase);
						PlayerProfiler.OnExplainedIaps();*/
				}
				break;
			case ButtonUtils.Name.Pause:
				if (state != State.GameplayEnded && state != State.GameplayPaused && buttons[buttonPressed].IsNotPressed)
				{
					GameManager.PauseGame();
					flag2 = true;
				}
				break;
			case ButtonUtils.Name.Resume:
				if (buttons[buttonPressed].IsNotPressed)
				{
					buttons[ButtonUtils.Name.Resume].Hide();
					state = State.GameplayUnpausing;
					unpauseTimer = 2.75f;
					DisplayUnpauseCountdown();
				}
				break;
			case ButtonUtils.Name.OK:
				if (tutorialDismissable && buttons[buttonPressed].IsNotPressed)
				{
					flag2 = true;
					result = true;
					tutorial.Hide();
				}
				break;
			default:
				Debug.LogError(string.Format("GMHD: ERROR: Unhandled button hit case of {0} in CheckButtons() SECOND case statement - extend case statement to include this button hit type", buttonPressed));
				break;
			}
			if (flag2.HasValue)
			{
				buttons[buttonPressed].SetPressedTo(flag2.Value);
			}
		}
		return result;
	}

	public static void ShowPrompt()
	{
		state = State.Prompt;
		prompt.Show();
	}

	public static void PausePrompt()
	{
		if (state == State.Prompt)
		{
			state = State.PromptPaused;
		}
	}

	public static void UnpausePrompt()
	{
		if (state == State.PromptPaused)
		{
			state = State.Prompt;
		}
	}

	public static void UpdatePreGame(float preGameTimeLeft)
	{
	}

	public static void EndPreGame()
	{
	}

	public static void OnSceneStart()
	{
		if (VideoSharingManager.IsEnabled && GameManager.HasInternet())
		{
			buttons[ButtonUtils.Name.Record].Show();
			if (GameManager.IsRecording)
			{
				buttons[ButtonUtils.Name.Record].Press();
			}
			else
			{
				buttons[ButtonUtils.Name.Record].Depress();
			}
		}
		else
		{
			buttons[ButtonUtils.Name.Record].Hide();
		}
		buttons[ButtonUtils.Name.Pause].Hide();
		score.text = null;
		savesCount.text = null;
		gameplayCrowns = ClassicCrownHud.Configure(score, GameManager.WorldNum, 0, false);
	}

	public static void OnGameplayStart()
	{
		gameplayTop.Show();
		prompt.Hide();
		state = State.Gameplay;
		animTimer = 0f;
		buttons[ButtonUtils.Name.Record].Hide();
		buttons[ButtonUtils.Name.Pause].ShowDepressed();
		score.text = "0%";
		if (gameplayCrowns != null) gameplayCrowns.gameObject.SetActive(true);
		DisplayCrowns(GameManager.CrownsThisGame);
	}

	public static void ShowHud()
	{
		ConfigureHud(true);
	}

	public static void ShowGetLives()
	{
		ConfigureHud(false);
		ConfigureGetLivesMenu();
		gameOver.SetY(1.5f, true);
		getLives.SetY(0f, true);
		state = State.GetLivesShown;
	}

	public static void ShowTutorial()
	{
		ConfigureTutorial();
		tutorial.Show();
		buttons[ButtonUtils.Name.Record].Hide();
		state = State.TutorialShown;
	}

	public static void HideHud()
	{
		backgroundOverlay.Hide();
		gameOver.Hide();
		gameplayTop.Show();
		buttons[ButtonUtils.Name.Back].Hide();
		state = State.Gameplay;
	}

	public static void ShowPause()
	{
		pausedTitle.text = Localizer.GetTermCapitalized("Paused");
		ShowOverlay();
		gameplayTop.Hide();
		paused.Show();
		buttons[ButtonUtils.Name.Resume].ShowDepressed();
		buttons[ButtonUtils.Name.Back].ShowDepressed();
		state = State.GameplayPaused;
	}

	public static void HidePause()
	{
		paused.Hide();
		gameplayTop.Show();
		backgroundOverlay.Hide();
		buttons[ButtonUtils.Name.Pause].ShowDepressed();
		buttons[ButtonUtils.Name.Back].Hide();
		state = State.Gameplay;
	}

	public static void OnGameplayEnd()
	{
		state = State.GameplayEnded;
	}

	private static void ShowButtons(bool showPlay, bool showNext, bool showGetLives, bool showBack)
	{
		ShowButton(ButtonUtils.Name.Play, showPlay);
		ShowButton(ButtonUtils.Name.Next, showNext);
		ShowButton(ButtonUtils.Name.GetLives, showGetLives);
		ShowButton(ButtonUtils.Name.Back, showBack);
	}

	private static void ShowButton(ButtonUtils.Name buttonName, bool show)
	{
		if (show)
		{
			buttons[buttonName].Show();
			buttons[buttonName].Depress();
		}
		else
		{
			buttons[buttonName].Hide();
		}
	}

	private static void ShowOverlay(float? alphaOverride = null)
	{
		backgroundOverlayMaterial.color = MaterialManager.GetGuiOverlayColor();
		float value = ((!alphaOverride.HasValue) ? MaterialManager.GetGuiOverlayAlpha() : alphaOverride.Value);
		backgroundOverlayMaterial.SetFloat("_Alpha", value);
		backgroundOverlay.Show();
	}

	private static void ConfigureHud(bool fullSetup)
	{
		gameplayTop.Hide();
		gameOver.Show();
		animTimer = 2f;
		DisplayProgressOnHud(ProgressManager.GetLastProgress());
		DisplayGems(ProgressManager.GetLastGems());
		DisplayLivesLeft();
		bool flag = ProgressManager.GetLastProgress() >= 100 && fullSetup;
		bool showPlay = false;
		bool showNext = false;
		bool flag2 = false;
		bool showGetLives = false;
		bool showBack = true;
		if (flag)
		{
			menuDisplayed = Display.Complete;
			PrepareNextWorld();
			showNext = true;
		}
		else if (LivesManager.OutOfLives)
		{
			menuDisplayed = Display.OutOfLives;
			showGetLives = true;
		}
		else
		{
			menuDisplayed = Display.Normal;
			showPlay = true;
		}
		backgroundOverlay.Show();
		backgroundOverlayMaterial.SetFloat("_Alpha", 0f);
		backgroundOverlayMaterial.color = MaterialManager.GetGuiOverlayColor();
		ShowButtons(showPlay, showNext, showGetLives, showBack);
		if (fullSetup)
		{
			TryShowInterstitial();
			if (VideoSharingManager.HasRecorded)
			{
				VideoSharingManager.ShareRecording();
			}
		}
		state = State.GameOverShown;
		AnimateGameOver(1f, true);
	}

	private static void ConfigureGetLivesMenu()
	{
		getLives.Show();
		getLivesTitle.text = Localizer.GetTermCapitalized(Localizer.GetTermCapitalized("balls"));
		getLivesTimeTitle.text = Localizer.GetTermCapitalized(Localizer.GetTermCapitalized("Recharge in:"));
		Color guiButtonTextColor = MaterialManager.GetGuiButtonTextColor();
		buttons[ButtonUtils.Name.Video].SetTextColor(guiButtonTextColor);
		buttons[ButtonUtils.Name.Shop].SetTextColor(guiButtonTextColor);
		buttons[ButtonUtils.Name.Shop].ShowDepressed();
		buttons[ButtonUtils.Name.Shop].SetText(Localizer.GetTermCapitalized("Unlimited"));
		buttons[ButtonUtils.Name.Video].Show();
		string text = string.Format("{0} {1}", Localizer.GetNumber(GameManager.LivesPerVideo), Localizer.GetTermCapitalized("free"));
		if (Localizer.GetCurrentLanguageEnum() == Localizer.Language.English)
		{
			text = string.Format("GET {0}", text);
		}
		buttons[ButtonUtils.Name.Video].SetText(text);
		bool flag = VideoRewardsManager.CanShowVideo();
		if (flag)
		{
			buttons[ButtonUtils.Name.Video].ShowDepressed();
		}
		else
		{
			buttons[ButtonUtils.Name.Video].Hide();
		}
		float secondsForRecharge = ((!flag) ? GameManager.SecondsPerRechargeShort : GameManager.SecondsPerRecharge);
		float secondsUntilRecharge;
		float num;
		if (LivesManager.LivesHaveRecharged(secondsForRecharge, out secondsUntilRecharge))
		{
			num = GameManager.SecondsPerFakeRecharge;
			getLivesTimeIsFake = true;
		}
		else
		{
			num = secondsUntilRecharge;
			getLivesTimeIsFake = false;
		}
		getLivesClock.Initialize(num, RewardForRecharge);
		if (!getLivesTimeIsFake)
		{
			NotificationManager.SetNotification((int)num, "Your balls have recharged and are ready to be used");
		}
		timeLastCheckedIfVideoWasReady = num;
	}

	private static void ConfigureTutorial()
	{
		bool flag = DeviceQualityChecker.IsPad(true);
		if (tutorialMaterial.mainTexture == null)
		{
			string text = ((!flag) ? "TUTORIAL_IPHONE" : "TUTORIAL_IPAD");
			string resourceFilePath = "Tutorial" + "/" + text;
			LoadTextureNativeBytes(resourceFilePath, tutorialMaterial);
		}
		string englishLine = ((!flag) ? "Slide THUMB below ball\nfor better control" : "Slide finger below ball\nfor better control");
		tutorialTitle.text = Localizer.GetTermCapitalized("Tip");
		tutorialMessage.text = Localizer.GetLine(englishLine);
		buttons[ButtonUtils.Name.OK].SetText(Localizer.GetTermCapitalized("OK"));
		ShowOverlay(0.7f);
		tutorialDismissable = false;
		tutorialCooldownTimer = 1.5f;
		buttons[ButtonUtils.Name.OK].Hide();
	}

	private static void TryShowInterstitial()
	{
		bool livesHaveRechargedBefore = LivesManager.LivesHaveRechargedBefore;
		livesHaveRechargedBefore = true;
		if (GameManager.InterstitialsEnabled && livesHaveRechargedBefore && LivesManager.LivesLeft == GameManager.InterstitialLivesNum && PlayerProfiler.HaveNotPurchasedPremium && VideoRewardsManager.CanShowImage())
		{
			VideoRewardsManager.ShowImage();
		}
	}

	private static void TryRecheckForRewardVideo()
	{
		if (getLivesTimeIsFake || !buttons[ButtonUtils.Name.Video].IsHidden)
		{
			return;
		}
		float secondsLeft = getLivesClock.SecondsLeft;
		if (timeLastCheckedIfVideoWasReady - secondsLeft > 5f)
		{
			timeLastCheckedIfVideoWasReady = secondsLeft;
			if (VideoRewardsManager.CanShowVideo())
			{
				buttons[ButtonUtils.Name.Video].ShowDepressed();
			}
		}
	}

	private static void RewardForVideo(bool rewardEarned)
	{
		if (rewardEarned)
		{
			AddBalls(GameManager.LivesPerVideo);
		}
		else
		{
			buttons[ButtonUtils.Name.Video].Depress();
		}
	}

	private static void RewardForRecharge()
	{
		if (!LivesManager.LivesHaveRechargedBefore || !UserSettings.AreAlertsOn() || NotificationManager.HaveNotRegisteredBefore)
		{
		}
		AddBalls(GameManager.LivesPerRecharge);
		AnalyticsLogEvent.OnRecharge();
	}

	private static void AddBalls(int ballsToAdd)
	{
		if (LivesManager.OutOfLives)
		{
			LivesManager.RechargeLives(ballsToAdd);
		}
		animTimer = 1.5f;
		state = State.GetLivesDisappearing;
		buttons[ButtonUtils.Name.Video].Press();
		buttons[ButtonUtils.Name.Shop].Press();
		buttons[ButtonUtils.Name.Play].ShowDepressed();
		buttons[ButtonUtils.Name.GetLives].Hide();
		DisplayLivesLeft();
		NotificationManager.CancelNotifications();
	}

	private static float GetAnimPercent(ref float animationTimer, float totalAnimationTime, out bool isAnimationDone)
	{
		animationTimer -= Time.smoothDeltaTime;
		isAnimationDone = animationTimer <= 0f;
		if (isAnimationDone)
		{
			return 1f;
		}
		return 1f - animationTimer / totalAnimationTime;
	}

	private static void DisplaySaves()
	{
	}

	private static void DisplayProgressOnHud(int progressPercentage)
	{
		TransformUtils.Show(progressGroup);
		string text = progressPercentage.ToString();
		progressText.text = text;
		progressTextShadow.text = text;
		float newX = 0f;
		if (progressPercentage < 10)
		{
			newX = -0.0825f;
		}
		else if (progressPercentage >= 100)
		{
			newX = 0.0825f;
		}
		TransformUtils.SetX(progressOffsetGroup, newX, true);
	}

	private static void DisplayGems(int gemsNumber)
	{
		TransformUtils.Show(gemsGroup);
		string text = gemsNumber.ToString();
		gemsText.text = text;
		gemsTextShadow.text = text;
		string text2 = "/" + LevelCatalog.Get(GameManager.WorldNum).MaxGems;
		gemsTotalText.text = text2;
		gemsTotalTextShadow.text = text2;
		bool flag = gemsNumber >= 10;
		float newX = ((!flag) ? 0.34f : 0.664f);
		float newX2 = ((!flag) ? (-0.93f) : (-1.242f));
		TransformUtils.SetX(gemsTotalText, newX, true);
		TransformUtils.SetX(gemsIcon, newX2, true);
		ClassicCrownHud.Configure(gemsText, GameManager.WorldNum, ProgressManager.GetLastCrowns(), true);
	}

	public static void DisplayCrowns(int crownsNumber)
	{
		ClassicCrownHud.Display(gameplayCrowns, crownsNumber, LevelCatalog.Get(GameManager.WorldNum).MaxCrowns);
	}

	private static void DisplayLivesLeft()
	{
		livesText.text = string.Empty;
		livesTextShadow.text = string.Empty;
	}

	private static void LoadTextureNativePNG(string resourceFilePath, Material targetMaterial)
	{
		Texture2D mainTexture = Resources.Load(resourceFilePath) as Texture2D;
		targetMaterial.mainTexture = mainTexture;
	}

	private static void LoadTextureNativeBytes(string resourceFilePath, Material targetMaterial)
	{
		TextAsset textAsset = Resources.Load(resourceFilePath, typeof(TextAsset)) as TextAsset;
		if (textAsset == null)
		{
			Debug.LogError(string.Format("Game Hud: Error: Unable to find texture to load as bytes from path {0} into material {1}", resourceFilePath, targetMaterial.name));
			return;
		}
		Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false);
		texture2D.LoadImage(textAsset.bytes);
		targetMaterial.mainTexture = texture2D;
		texture2D.Compress(true);
	}

	private static void PrepareNextWorld()
	{
		nextWorld.SetX(1.745f / gameOver.GetRoot().localScale.x, true);
		nextWorldMaterial.mainTexture = MaterialManager.WorldMenuTextures[GameManager.GetNextWorldIndex()];
		if (!ClassicWorldTitle.Configure(nextWorldTitleQuad, gemsText, LevelDesigner.NextWorldName, MaterialManager.GetNextGuiLightAltColor()))
			LoadTextureNativePNG(string.Format("NextWorldTitle{0}", LevelDesigner.NextWorldName), nextWorldTitleQuad.GetComponent<Renderer>().material);
	}

	private static void StartNextWorldRevealing()
	{
		TransformUtils.Hide(nextWorldLock);
		state = State.NextWorldRevealing;
		animTimer = 1f;
		nextWorldOverlayMax = nextWorldMaterial.GetFloat("_Ammount");
	}

	private static void RevealNextWorld(float animPercent, bool animDone)
	{
		if (animDone)
		{
			float num = 1f;
			StartNextWorldSelecting();
		}
		else
		{
			float num = FloatAnim.Smooth(animPercent, true, true);
		}
		nextWorldMaterial.SetFloat("_Ammount", (1f - animPercent) * nextWorldOverlayMax);
		nextWorldColor.a = 1f - animPercent;
		nextWorldTitleQuad.GetComponent<Renderer>().material.SetFloat("_Alpha", 1f - animPercent);
		ClassicWorldTitle.SetAlpha(nextWorldTitleQuad, 1f - animPercent);
	}

	private static void StartNextWorldSelecting()
	{
		state = State.NextWorldSelecting;
		animTimer = 1f;
		nextWorldDepthStarting = nextWorld.GetZ(true);
		cameraOverlay.Show();
	}

	private static bool EndsWith(string text, string query)
	{
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(query) && query.Length <= text.Length)
		{
			string text2 = text.Substring(text.Length - query.Length, query.Length);
			return text2.Equals(query);
		}
		return false;
	}

	private static void ConfirmNotification(bool userConfirmed)
	{
		if (userConfirmed)
		{
			NotificationManager.Register();
		}
		else
		{
			NotificationManager.NeverRegister();
		}
	}

	private static void TryPurchase(bool proceed)
	{
		if (proceed)
		{
			IAPManager.PurchaseProduct(IAPManager.ProductIDUnlimitedBalls, OnPurchase);
		}
		else
		{
			buttons[ButtonUtils.Name.Shop].Depress();
		}
	}

	private static void OnPurchase(bool success)
	{
		if (success)
		{
			PlayerProfiler.OnPremiumModePurchase();
			AddBalls(GameManager.StartingLives);
			AnalyticsLogEvent.UpgradePackPurchased();
		}
		else
		{
			buttons[ButtonUtils.Name.Shop].Depress();
			AnalyticsLogEvent.UpgradePackPurchaseCancelled();
		}
	}

	private static void DisplayUnpauseCountdown()
	{
		pausedTitle.text = string.Format("{0:0.0}", unpauseTimer);
	}
}
