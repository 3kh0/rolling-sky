using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
// Web music belongs to HTMLMediaElement, not Unity's full-track WebAudio decoder.
// Keep Unity's AudioContext independent: it still owns short gameplay effects.
public class MusicPlayer : MonoBehaviour
{
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void RollingSky_InitializeBrowserAudio();
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern int RollingSky_IsBrowserAudioReady();
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern int RollingSky_Music(string command, int track, string url, string title, float value, float offset);

	// Retain the serialized field for the editor/native player. The WebGL build
	// callback removes its references before scene dependencies are collected.
	public AudioClip[] music;
	public bool mute;
	private static bool muted;
	private static bool initialized;

	private void Start()
	{
		RollingSky_InitializeBrowserAudio();
		if (mute) Mute();
		Object.DontDestroyOnLoad(gameObject);
	}

	private static int Send(string command, int track = -1, float value = 0f, float offset = 0f)
	{
		LevelCatalog.Level level = track < 0 ? null : LevelCatalog.Get(track + 1);
		string url = level == null ? "" : "assets/music/" + level.SourceNumber + (track < 5 ? ".mp3" : ".flac");
		return RollingSky_Music(command, track, url, level == null ? "" : level.Title, value, offset);
	}

	public static void Initialize() { Initialize(1f); }
	public static void Initialize(float volume)
	{
		if (!initialized)
		{
			initialized = true;
			if (Object.FindObjectOfType<MusicPlayer>() == null)
				new GameObject("MusicPlayer").AddComponent<MusicPlayer>();
		}
		SetVolume(volume);
		AdjustPitch(1f);
	}
	public static void Initialize(AudioClip[] clips) { Initialize(); }
	public static void Initialize(AudioClip[] clips, float volume) { Initialize(volume); }
	public static bool Prepare(int track) { return muted || Send("prepare", track) != 0; }
	public static bool ReadyToStart(int track) { return muted || (Prepare(track) && RollingSky_IsBrowserAudioReady() != 0); }
	public static bool BeginGameplay(int track, float? offset)
	{
		if (muted) return true;
		Prepare(track);
		return RollingSky_IsBrowserAudioReady() != 0 && Send("begin", track, 0f, offset ?? 0f) != 0;
	}
	public static bool ResumeGameplay(int track)
	{
		return muted || (RollingSky_IsBrowserAudioReady() != 0 && Send("resume", track) != 0);
	}
	public static bool PlaybackReady(int track) { return muted || Send("ready", track) != 0; }
	public static void Play(int track) { Play(track, false, null); }
	public static void Play(int track, bool reset) { Play(track, reset, null); }
	public static void Play(int track, float offset) { Play(track, false, offset); }
	public static void Play(int track, bool reset, float offset) { Play(track, reset, (float?)offset); }
	public static void Play(int track, float? offset) { Play(track, false, offset); }
	public static void Play(int track, bool reset, float? offset) { if (!muted) Send(reset ? "restart" : "play", track, 0f, offset ?? 0f); }
	public static void Stop() { Send("stop"); }
	public static void Stop(int track) { Send("stop", track); }
	public static void Pause() { Send("pause"); }
	public static void Unpause() { if (!muted) Send("unpause"); }
	public static void FadeOut() { FadeOut(2f); }
	public static void FadeOut(float seconds) { Send("out", -1, seconds); }
	public static void FadeOut(int track) { FadeOut(track, 2f); }
	public static void FadeOut(int track, float seconds) { Send("out", track, seconds); }
	public static void FadeIn(int track) { FadeIn(track, 2f); }
	public static void FadeIn(int track, float seconds) { if (!muted) Send("in", track, seconds); }
	public static void FadeIn(int track, bool reset) { if (reset) Stop(track); FadeIn(track); }
	public static void FadeInto(int track) { FadeInto(track, 2f); }
	public static void FadeInto(int track, float seconds) { if (!muted) Send("into", track, seconds); }
	public static void FadeIntoManualStart(int track) { if (!muted) Send("select", track); }
	// The original manual fade is an immediate selection; Update/End are no-ops.
	public static void FadeIntoManualUpdate(float percent) { }
	public static void FadeIntoManualEnd() { }
	public static void SetVolume(float volume) { Send("volume", -1, volume); }
	public static void AdjustPitch(float pitch) { Send("pitch", -1, pitch); }
	public static void Mute() { Mute(true); }
	public static void Unmute() { Mute(false); }
	public static void Mute(bool on) { muted = on; if (on) Stop(); }
}
#else
public class MusicPlayer : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void RollingSky_InitializeBrowserAudio();
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern int RollingSky_IsBrowserAudioReady();
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void RollingSky_SetMusicStatus(int state, string title);
#endif

	private enum Command
	{
		Play = 0,
		Stop = 1,
		Pause = 2,
		Unpause = 3,
		FadeOut = 4,
		FadeIn = 5,
		FadeInto = 6,
		FadeOutManual = 7,
		FadeInManual = 8,
		FadeIntoManual = 9,
		SkipTo = 10
	}

	private sealed class Request
	{
		public Command Command;
		public int? TrackNumber;
		public float? FadeTime;
		public bool ResetIfPlaying;
		public bool Exclusive;
		public float? Offset;
	}

	private class TrackPlayer
	{
		private AudioSource firstPlayer;

		private AudioSource secondPlayer;

		private Track firstOwner;

		private Track secondOwner;

		private float firstPlayerAudioPlayTime;

		private float secondPlayerAudioPlayTime;

		private bool usingFirstPlayer;

		private AudioSource player
		{
			get
			{
				if (usingFirstPlayer)
				{
					return firstPlayer;
				}
				return secondPlayer;
			}
			set
			{
				if (usingFirstPlayer)
				{
					firstPlayer = value;
				}
				else
				{
					secondPlayer = value;
				}
			}
		}

		public TrackPlayer(GameObject musicPlayerGameObject)
		{
			firstPlayer = musicPlayerGameObject.AddComponent<AudioSource>();
			secondPlayer = musicPlayerGameObject.AddComponent<AudioSource>();
			firstPlayer.playOnAwake = false;
			secondPlayer.playOnAwake = false;
			firstPlayer.loop = true;
			secondPlayer.loop = true;
			usingFirstPlayer = true;
		}

		public bool BothPlayersPaused
		{
			get { return firstOwner != null && firstOwner.IsPaused && secondOwner != null && secondOwner.IsPaused; }
		}

		public bool Play(Track owner, AudioClip clip, float volume, float pitch, float? skipSeconds)
		{
			// isPlaying is false while paused or while a platform decoder is starting.
			// Ownership, not isPlaying, reserves a source during those intervals.
			if ((usingFirstPlayer ? firstOwner : secondOwner) != null)
			{
				SwitchPlayer();
			}
			Track previousOwner = usingFirstPlayer ? firstOwner : secondOwner;
			if (previousOwner != null && previousOwner.IsPaused)
			{
				SwitchPlayer();
				previousOwner = usingFirstPlayer ? firstOwner : secondOwner;
			}
			if (previousOwner != null)
			{
				// Rapid menu changes can reuse a fading source. Retire its old Track
				// before rebinding so a stale fade cannot stop the new occupant later.
				previousOwner.Stop();
			}
			if (usingFirstPlayer)
			{
				firstOwner = owner;
				firstPlayerAudioPlayTime = 0f;
			}
			else
			{
				secondOwner = owner;
				secondPlayerAudioPlayTime = 0f;
			}
			player.clip = clip;
			player.time = 0f;
			player.volume = volume;
			player.pitch = pitch;
			player.Play();
			if (skipSeconds.HasValue)
			{
				player.time = skipSeconds.Value;
			}
			return usingFirstPlayer;
		}

		public void Stop(bool playerKey)
		{
			if (playerKey)
			{
				firstPlayerAudioPlayTime = 0f;
				firstPlayer.Stop();
				firstPlayer.time = 0f;
				firstPlayer.clip = null;
				firstOwner = null;
			}
			else
			{
				secondPlayerAudioPlayTime = 0f;
				secondPlayer.Stop();
				secondPlayer.time = 0f;
				secondPlayer.clip = null;
				secondOwner = null;
			}
		}

		public bool CanReleaseClip(AudioClip clip)
		{
			return (firstPlayer.clip != clip || (firstOwner == null && !firstPlayer.isPlaying)) &&
				(secondPlayer.clip != clip || (secondOwner == null && !secondPlayer.isPlaying));
		}

		public void ReleaseStoppedClip(AudioClip clip)
		{
			if (firstPlayer.clip == clip) Stop(true);
			if (secondPlayer.clip == clip) Stop(false);
		}

		public void Pause(bool playerKey)
		{
			if (playerKey)
			{
				firstPlayerAudioPlayTime = firstPlayer.time;
				firstPlayer.Stop();
			}
			else
			{
				secondPlayerAudioPlayTime = secondPlayer.time;
				secondPlayer.Stop();
			}
		}

		public void Unpause(bool playerKey)
		{
			if (playerKey)
			{
				if (!firstPlayer.isPlaying)
				{
					firstPlayer.Play();
					if (firstPlayerAudioPlayTime > 0f)
					{
						firstPlayer.time = firstPlayerAudioPlayTime;
					}
				}
			}
			else if (!secondPlayer.isPlaying)
			{
				secondPlayer.Play();
				if (secondPlayerAudioPlayTime > 0f)
				{
					secondPlayer.time = secondPlayerAudioPlayTime;
				}
			}
		}

		public void SetVolume(bool playerKey, float newVolume)
		{
			if (playerKey)
			{
				firstPlayer.volume = newVolume;
			}
			else
			{
				secondPlayer.volume = newVolume;
			}
		}

		public void SetPitch(bool playerKey, float newPitch)
		{
			if (playerKey)
			{
				firstPlayer.pitch = newPitch;
			}
			else
			{
				secondPlayer.pitch = newPitch;
			}
		}

		public void Reset()
		{
			Stop(true);
			Stop(false);
			usingFirstPlayer = true;
		}

		private void SwitchPlayer()
		{
			usingFirstPlayer = !usingFirstPlayer;
		}
	}

	private class Track
	{
		private enum State
		{
			Ready = 0,
			Playing = 1,
			FadingIn = 2,
			FadingOut = 3,
			Paused = 4
		}

		private static TrackPlayer player;

		private State state;

		private State stateBeforePause;

		public long LastUse;

		private float fadeTimer;

		private float fadeTime;

		private AudioClip clip;

		private float volume;

		private float pitch;

		private bool playerKey;

		public bool LoadFailed;

		private bool loadRequested;

		public AudioDataLoadState LoadState
		{
			get
			{
				if (clip == null) return AudioDataLoadState.Unloaded;
				AudioDataLoadState state = clip.loadState;
				// Keep failures sticky across all readiness/cache polls. Native teardown
				// can return a failed decoder to Unloaded; that is not a new cold request.
				if (state == AudioDataLoadState.Failed || (loadRequested && state == AudioDataLoadState.Unloaded))
					LoadFailed = true;
				return state;
			}
		}

		public void Prepare()
		{
			if (LoadState == AudioDataLoadState.Unloaded && !LoadFailed)
			{
				loadRequested = true;
				if (!clip.LoadAudioData()) LoadFailed = true;
			}
		}

		public bool HasClip
		{
			get
			{
				return clip != null;
			}
		}

		public bool CanUnload
		{
			get { return HasClip && state == State.Ready && player.CanReleaseClip(clip); }
		}

		public bool CanStart
		{
			get { return state != State.Ready || !player.BothPlayersPaused; }
		}

		public void LoadClip(string path)
		{
			clip = Resources.Load<AudioClip>(path);
			loadRequested = false;
			LoadFailed = clip == null;
		}

		public void UnloadClip()
		{
			if (!CanUnload) return;
			player.ReleaseStoppedClip(clip);
			AudioClip released = clip;
			clip = null;
			// Imported Resources assets are disk-backed. Unity 2017.4 UnloadAsset
			// releases the whole asset (including audio data), invalidating its object.
			// Do not also retain it in a source or attempt to reuse it after unloading.
			Resources.UnloadAsset(released);
		}

		public bool UnloadData()
		{
			if (!CanUnload || LoadState != AudioDataLoadState.Loaded) return false;
			player.ReleaseStoppedClip(clip);
			// The first five clips are serialized scene references: keep their objects,
			// but release the decoded soundtrack exactly as for Resources-backed songs.
			bool unloaded = clip.UnloadAudioData();
			if (unloaded) loadRequested = false;
			return unloaded;
		}

		public bool IsPlaying
		{
			get
			{
				return state == State.Playing;
			}
		}

		public bool IsFading
		{
			get
			{
				return IsFadingIn || IsFadingOut;
			}
		}

		public bool IsFadingIn
		{
			get
			{
				return state == State.FadingIn;
			}
		}

		public bool IsFadingOut
		{
			get
			{
				return state == State.FadingOut;
			}
		}

		public bool IsPaused
		{
			get
			{
				return state == State.Paused;
			}
		}

		public bool IsNotPlaying
		{
			get
			{
				return !IsPlaying;
			}
		}

		public bool IsNotFading
		{
			get
			{
				return !IsFading;
			}
		}

		public bool IsNotFadingIn
		{
			get
			{
				return !IsFadingIn;
			}
		}

		public bool IsNotFadingOut
		{
			get
			{
				return !IsFadingOut;
			}
		}

		public bool IsPlayingOrFading
		{
			get
			{
				return IsPlaying || IsFading;
			}
		}

		public bool IsPlayingOrFadingIn
		{
			get
			{
				return IsPlaying || IsFadingIn;
			}
		}

		public bool IsPlayingOrFadingOut
		{
			get
			{
				return IsPlaying || IsFadingOut;
			}
		}

		public bool IsNotPlayingNorFading
		{
			get
			{
				return !IsPlayingOrFading;
			}
		}

		public bool IsNotPlayingNorFadingIn
		{
			get
			{
				return !IsPlayingOrFadingIn;
			}
		}

		public bool IsNotPlayingNorFadingOut
		{
			get
			{
				return !IsPlayingOrFadingOut;
			}
		}

		public string TrackName
		{
			get
			{
				return clip.name;
			}
		}

		public Track(AudioClip clip)
		{
			this.clip = clip;
			volume = 0f;
			pitch = 1f;
			state = State.Ready;
		}

		public static void CreatePlayer(GameObject playerGameObject)
		{
			player = new TrackPlayer(playerGameObject);
		}

		public static void ResetPlayer()
		{
			player.Reset();
		}

		public void Play(float? skipSeconds)
		{
			Stop();
			playerKey = player.Play(this, clip, globalVolume, globalPitch, skipSeconds);
			state = State.Playing;
		}

		public void Restart(float? skipSeconds)
		{
			Stop();
			Play(skipSeconds);
		}

		public void Stop()
		{
			if (state != State.Ready) player.Stop(playerKey);
			state = State.Ready;
		}

		public void Pause()
		{
			stateBeforePause = state;
			player.Pause(playerKey);
			state = State.Paused;
		}

		public void Unpause()
		{
			player.Unpause(playerKey);
			state = stateBeforePause;
		}

		public void FadeOut(float? fadeTime)
		{
			float num = ((!fadeTime.HasValue) ? 2f : fadeTime.Value);
			if (IsFadingIn)
			{
				float num2 = fadeTimer / this.fadeTime;
				fadeTimer = 1f - num * num2;
				this.fadeTime = num;
			}
			else
			{
				this.fadeTime = num;
				fadeTimer = this.fadeTime;
			}
			state = State.FadingOut;
		}

		public void FadeIn(float? fadeTime)
		{
			float num = ((!fadeTime.HasValue) ? 2f : fadeTime.Value);
			if (IsFadingOut)
			{
				float num2 = fadeTimer / this.fadeTime;
				fadeTimer = 1f - num * num2;
				this.fadeTime = num;
			}
			else
			{
				Play(null);
				this.fadeTime = num;
				fadeTimer = this.fadeTime;
			}
			state = State.FadingIn;
		}

		public bool FadeInto(float? fadeTime)
		{
			bool flag = IsNotPlaying || IsFadingOut;
			if (flag)
			{
				FadeIn(fadeTime);
			}
			return flag;
		}

		public void FadeOutManual()
		{
			Stop();
		}

		public void FadeInManual()
		{
			Play(null);
		}

		public bool FadeIntoManual()
		{
			bool flag = IsNotPlaying || IsFadingOut;
			if (flag)
			{
				FadeInManual();
			}
			return flag;
		}

		public bool UpdateFade()
		{
			fadeTimer -= Time.smoothDeltaTime;
			return EnactUpdateFade(fadeTimer / fadeTime);
		}

		public bool UpdateFadeManual(float updatePercent)
		{
			return true;
		}

		public void RePitch()
		{
			if (IsPlayingOrFading)
			{
				player.SetPitch(playerKey, globalPitch);
			}
		}

		public void ReVolume()
		{
			if (state == State.Playing)
			{
				player.SetVolume(playerKey, globalVolume);
			}
		}

		public void Reset()
		{
			RePitch();
			ReVolume();
		}

		private bool EnactUpdateFade(float fadePercent)
		{
			bool flag = false;
			if (fadePercent <= 0f)
			{
				flag = true;
				fadePercent = 0f;
			}
			else
			{
				fadePercent = FloatAnim.Smooth(fadePercent, true, true);
			}
			if (state == State.FadingIn)
			{
				player.SetVolume(playerKey, globalVolume * (1f - fadePercent));
				if (flag)
				{
					state = State.Playing;
				}
			}
			else if (state == State.FadingOut)
			{
				player.SetVolume(playerKey, globalVolume * fadePercent);
				if (flag)
				{
					Stop();
				}
			}
			else
			{
				Debug.LogWarning(string.Format("MSPL.TRCK: ERROR: Attempt to UpdateFade() on Track {0}, which is currently not fading at all (its current state is: {1})", TrackName, state));
				flag = true;
			}
			return flag;
		}
	}

	private const bool debugsEnabled = true;

	private const bool debugFading = false;

	private const bool debugPlaying = false;

	private const bool debugPending = false;

	private const bool debugAutoFadingStart = false;

	private const string instanceName = "MusicPlayer";

	private const float defaultFadeTime = 2f;

	private const int firstClassicLevel = 6;

	private const int firstExpansionLevel = 9;

	// Two source owners plus one recently used clip for menu back-swipes/retries.
	private const int expansionCacheCapacity = 3;

	// Bound decoded PCM across ALL songs, including the serialized first five.
	// Keeping only expansion asset objects bounded is insufficient on WebAudio.
	private const int decodedCacheCapacity = 3;

	private static long clipUseSequence;

	private static bool isEnabled = true;

	public AudioClip[] music;

	public bool mute;

	private static AudioClip[] receivedTracks;

	private static bool initialized;

	private static bool gameObjectInitialized;

	private static float globalPitch;

	private static float globalVolume;

	private static bool muted;

	private static Track[] tracks;

	private static ArrayUtils.List<int> tracksFadingAuto;

	private static ArrayUtils.List<int> tracksFadingManual;

	private static Request pendingRequest;

	private static bool pendingPaused;

	// Serialize cold decodes. Fast swipes replace the requested song rather than
	// allocating a full decoded soundtrack for every card the pointer crosses.
	private static Track preparingTrack;

	private static int musicStatus = -1;

	private static int musicStatusTrack = -1;

	private static bool notMuted
	{
		get
		{
			return !muted;
		}
	}

	public static void Initialize()
	{
		Initialize(null, 1f);
	}

	public static void Initialize(float musicVolume)
	{
		Initialize(null, musicVolume);
	}

	public static void Initialize(AudioClip[] musicTracks)
	{
		Initialize(musicTracks, 1f);
	}

	public static void Initialize(AudioClip[] musicTracks, float musicVolume)
	{
		if (!isEnabled)
		{
			return;
		}
		globalPitch = 1f;
		globalVolume = musicVolume;
		if (!initialized)
		{
			if (!gameObjectInitialized)
			{
				receivedTracks = musicTracks;
				// Menu preparation can precede the scene player's Start. Reuse that
				// component rather than creating a second Update loop for shared state.
				if (Object.FindObjectOfType<MusicPlayer>() == null)
				{
					GameObject gameObject = new GameObject("MusicPlayer");
					gameObject.AddComponent<MusicPlayer>();
				}
			}
			initialized = true;
		}
		else if (gameObjectInitialized)
		{
			for (int i = 0; i < tracks.Length; i++)
			{
				tracks[i].Reset();
			}
		}
	}

	private void Start()
	{
		if (!isEnabled || gameObjectInitialized)
		{
			return;
		}
#if UNITY_WEBGL && !UNITY_EDITOR
		RollingSky_InitializeBrowserAudio();
#endif
		if (receivedTracks == null)
		{
			if (MusicNull())
			{
				Debug.LogError("MSPL: ERROR: Music Player had no music specificied, and received no music from an Initialize() call");
			}
		}
		else
		{
			if (!MusicNull())
			{
				Debug.LogError(string.Format("MSPL: ERROR: Music Player had clips specified in its gameObject ('{0}'), but then ALSO received clips from an Initialize() call.  Using received clips", base.name));
			}
			music = receivedTracks;
		}
		Track.CreatePlayer(base.gameObject);
		// Preserve accepted 1-8 preloading; expansion entries start without clips.
		// Their catalogue identities and zero-based track numbers never change.
		int originalTrackCount = music != null ? music.Length : 0;
		tracks = new Track[LevelCatalog.Count];
		for (int i = 0; i < tracks.Length; i++)
		{
			AudioClip clip = i < firstClassicLevel - 1 && i < originalTrackCount ? music[i] : null;
			int levelNumber = i + 1;
			if (levelNumber >= firstClassicLevel && levelNumber < firstExpansionLevel)
			{
				LevelCatalog.Level level = LevelCatalog.Get(levelNumber);
				string path = level.ResourceRoot + "/Audio/Music" + level.SourceNumber;
				clip = Resources.Load<AudioClip>(path);
				if (clip == null)
				{
					Debug.LogError(string.Format("MSPL: Missing original {0} music at Resources/{1}; no substitute track will be played.", level.Title, path));
				}
			}
			tracks[i] = new Track(clip);
		}
		tracksFadingAuto = new ArrayUtils.List<int>(tracks.Length);
		tracksFadingManual = new ArrayUtils.List<int>(tracks.Length);
		muted = mute;
		Object.DontDestroyOnLoad(base.gameObject);
		gameObjectInitialized = true;
	}

	private void Update()
	{
		if (!isEnabled)
		{
			return;
		}
		if (tracksFadingAuto.IsNotEmpty)
		{
			for (int i = tracksFadingAuto.Length - 1; i >= 0; i--)
			{
				Track track = tracks[tracksFadingAuto[i]];
				if (track.IsPaused) continue;
				if (!track.IsFading || track.UpdateFade())
				{
					tracksFadingAuto.RemoveAt(i);
				}
			}
		}
		if (pendingRequest != null && !pendingPaused)
		{
			Request request = pendingRequest;
			pendingRequest = null;
			if (request.TrackNumber.HasValue)
			{
				Enact(request.Command, request.TrackNumber.Value, request.FadeTime,
					request.ResetIfPlaying, request.Exclusive, request.Offset);
			}
			else
			{
				EnactGlobal(request.Command, request.FadeTime, false, false, false);
			}
		}
	}

	private static void UpdateManual(float fadingPercent)
	{
		if (!isEnabled || !tracksFadingManual.IsNotEmpty)
		{
			return;
		}
		for (int i = tracksFadingManual.Length - 1; i >= 0; i--)
		{
			if (tracks[tracksFadingManual[i]].UpdateFadeManual(fadingPercent))
			{
				tracksFadingManual.RemoveAt(i);
			}
		}
	}

	public static void Play(int trackNumber)
	{
		Enact(Command.Play, trackNumber, null, false, false, null);
	}

	public static void Play(int trackNumber, bool resetIfPlaying)
	{
		Enact(Command.Play, trackNumber, null, resetIfPlaying, false, null);
	}

	public static void Play(int trackNumber, float startAtSeconds)
	{
		Enact(Command.Play, trackNumber, null, false, false, startAtSeconds);
	}

	public static void Play(int trackNumber, bool resetIfPlaying, float startAtSeconds)
	{
		Enact(Command.Play, trackNumber, null, resetIfPlaying, false, startAtSeconds);
	}

	public static void Play(int trackNumber, float? startAtSeconds)
	{
		if (startAtSeconds.HasValue)
		{
			Play(trackNumber, startAtSeconds.Value);
		}
		else
		{
			Play(trackNumber);
		}
	}

	public static void Play(int trackNumber, bool resetIfPlaying, float? startAtSeconds)
	{
		if (startAtSeconds.HasValue)
		{
			Play(trackNumber, resetIfPlaying, startAtSeconds.Value);
		}
		else
		{
			Play(trackNumber, resetIfPlaying);
		}
	}

	public static void Pause()
	{
		EnactGlobal(Command.Pause, null, false, false, false);
	}

	public static void Unpause()
	{
		EnactGlobal(Command.Unpause, null, false, false, false);
	}

	public static void Stop()
	{
		EnactGlobal(Command.Stop, null, false, false, false);
	}

	public static void Stop(int trackNumber)
	{
		Enact(Command.Stop, trackNumber, null, false, false, null);
	}

	public static void FadeOut()
	{
		EnactGlobal(Command.FadeOut, null, false, false, false);
	}

	public static void FadeOut(float fadeOutTime)
	{
		EnactGlobal(Command.FadeOut, fadeOutTime, false, false, false);
	}

	public static void FadeOut(int trackNumber)
	{
		Enact(Command.FadeOut, trackNumber, null, false, false, null);
	}

	public static void FadeOut(int trackNumber, float fadeOutTime)
	{
		Enact(Command.FadeOut, trackNumber, null, false, false, null);
	}

	public static void FadeIn(int trackNumber)
	{
		Enact(Command.FadeIn, trackNumber, null, false, false, null);
	}

	public static void FadeIn(int trackNumber, float fadeInTime)
	{
		Enact(Command.FadeIn, trackNumber, fadeInTime, false, false, null);
	}

	public static void FadeIn(int trackNumber, bool resetIfPlaying)
	{
		Enact(Command.FadeIn, trackNumber, null, resetIfPlaying, false, null);
	}

	public static void FadeInto(int trackNumber)
	{
		Enact(Command.FadeInto, trackNumber, null, false, false, null);
	}

	public static void FadeInto(int trackNumber, float fadeInTime)
	{
		Enact(Command.FadeInto, trackNumber, fadeInTime, false, false, null);
	}

	public static void FadeIntoManualStart(int trackNumber)
	{
		Enact(Command.FadeIntoManual, trackNumber, null, false, false, null);
	}

	public static void FadeIntoManualUpdate(float fadePercent)
	{
		UpdateManual(fadePercent);
	}

	public static void FadeIntoManualEnd()
	{
		tracksFadingManual.Reset();
	}

	public static void AdjustPitch(float newPitch)
	{
		if (isEnabled && globalPitch != newPitch)
		{
			globalPitch = newPitch;
			for (int i = 0; i < tracks.Length; i++)
			{
				tracks[i].RePitch();
			}
		}
	}

	public static void SetVolume(float newVolume)
	{
		if (isEnabled && globalVolume != newVolume)
		{
			globalVolume = newVolume;
			for (int i = 0; i < tracks.Length; i++)
			{
				tracks[i].ReVolume();
			}
		}
	}

	public static void Mute()
	{
		muted = true;
		Stop();
	}

	public static void Unmute()
	{
		muted = false;
	}

	public static void Mute(bool on)
	{
		muted = on;
		if (muted)
		{
			Stop();
		}
	}

	private static bool EnsureTrackClip(int trackNumber)
	{
		Track track = tracks[trackNumber];
		if (track.LoadFailed) return false;
		if (trackNumber < firstExpansionLevel - 1) return track.HasClip;
		if (!track.HasClip)
		{
			int loaded = 0;
			Track oldest = null;
			for (int i = firstExpansionLevel - 1; i < tracks.Length; i++)
			{
				Track candidate = tracks[i];
				if (candidate.HasClip) loaded++;
				if (candidate != preparingTrack && candidate.CanUnload && (oldest == null || candidate.LastUse < oldest.LastUse))
				{
					oldest = candidate;
				}
			}
			if (loaded >= expansionCacheCapacity)
			{
				if (oldest == null)
				{
					// The pending selection retries when the in-flight decoder or an
					// active source releases its slot. Never evict their buffers.
					return false;
				}
				oldest.UnloadClip();
			}
			LevelCatalog.Level level = LevelCatalog.Get(trackNumber + 1);
			track.LoadClip(level.ResourceRoot + "/Audio/Music" + level.SourceNumber);
		}
		if (!track.HasClip) return false;
		track.LastUse = ++clipUseSequence;
		return true;
	}

	private static bool EnsureDecodedCapacity(Track requested)
	{
		int loaded = 0;
		Track oldest = null;
		foreach (Track candidate in tracks)
		{
			AudioDataLoadState state = candidate.LoadState;
			if (state == AudioDataLoadState.Loaded || state == AudioDataLoadState.Loading) loaded++;
			if (candidate != requested && candidate != preparingTrack && candidate.CanUnload &&
				state == AudioDataLoadState.Loaded && (oldest == null || candidate.LastUse < oldest.LastUse))
				oldest = candidate;
		}
		if (loaded < decodedCacheCapacity) return true;
		return oldest != null && oldest.UnloadData();
	}

	public static bool Prepare(int trackNumber)
	{
		if (!isEnabled || !notMuted) return true;
		if (!gameObjectInitialized) return false;
		if (!MathUtils.IndexIsWithin(trackNumber, tracks)) return false;
		Track track = tracks[trackNumber];
		if (preparingTrack != null && preparingTrack.LoadState != AudioDataLoadState.Loading)
			preparingTrack = null;
		if (track.HasClip && track.LoadState == AudioDataLoadState.Loaded && !track.LoadFailed)
		{
			track.LastUse = ++clipUseSequence;
			SetMusicStatus(0, trackNumber);
			return true;
		}
		if (preparingTrack != null && preparingTrack != track)
		{
			SetMusicStatus(1, trackNumber);
			return false;
		}
		if (!EnsureTrackClip(trackNumber))
		{
			if (trackNumber < firstExpansionLevel - 1) track.LoadFailed = true;
			if (track.LoadFailed) ReportLoadFailure(trackNumber);
			else SetMusicStatus(1, trackNumber);
			return false;
		}
		if (track.LoadState == AudioDataLoadState.Unloaded && !EnsureDecodedCapacity(track))
		{
			SetMusicStatus(1, trackNumber);
			return false;
		}
		track.Prepare();
		if (track.LoadFailed)
		{
			ReportLoadFailure(trackNumber);
			return false;
		}
		bool ready = track.LoadState == AudioDataLoadState.Loaded;
		preparingTrack = ready ? null : track;
		SetMusicStatus(ready ? 0 : 1, trackNumber);
		return ready;
	}

	public static bool ReadyToStart(int trackNumber)
	{
		if (!isEnabled || !notMuted) return true;
		if (!Prepare(trackNumber)) return false;
#if UNITY_WEBGL && !UNITY_EDITOR
		return RollingSky_IsBrowserAudioReady() != 0;
#else
		return true;
#endif
	}

	public static bool BeginGameplay(int trackNumber, float? offset)
	{
		if (!ReadyToStart(trackNumber)) return false;
		Stop();
		SetVolume(1f);
		Play(trackNumber, true, offset);
		return true;
	}

	public static bool ResumeGameplay(int trackNumber)
	{
		if (!ReadyToStart(trackNumber)) return false;
		Unpause();
		return true;
	}

	public static bool PlaybackReady(int trackNumber) { return true; }

	private static void ReportLoadFailure(int trackNumber)
	{
		if (musicStatus == 2 && musicStatusTrack == trackNumber) return;
		Stop();
		LevelCatalog.Level level = LevelCatalog.Get(trackNumber + 1);
		string path = trackNumber < firstClassicLevel - 1 ? "scene music" :
			"Resources/" + level.ResourceRoot + "/Audio/Music" + level.SourceNumber;
		Debug.LogError("MSPL: Could not prepare " + level.Title + " at " + path + ". Reload or turn music off; gameplay will not start silently.");
		SetMusicStatus(2, trackNumber);
	}

	private static void SetMusicStatus(int state, int trackNumber)
	{
		if (musicStatus == state && musicStatusTrack == trackNumber) return;
		musicStatus = state;
		musicStatusTrack = trackNumber;
		string title = trackNumber < 0 ? "" : LevelCatalog.Get(trackNumber + 1).Title;
#if UNITY_WEBGL && !UNITY_EDITOR
		RollingSky_SetMusicStatus(state, title);
#endif
	}

	private static void Enact(Command command, int trackNumber, float? fadeTime, bool commandBool1, bool commandBool2, float? commandFloat)
	{
		if (!isEnabled)
		{
			return;
		}
		if (gameObjectInitialized)
		{
			if (MathUtils.IndexIsWithin(trackNumber, tracks))
			{
				bool startsTrack = command == Command.Play || command == Command.FadeIn || command == Command.FadeInto ||
					command == Command.FadeInManual || command == Command.FadeIntoManual;
				if (startsTrack)
				{
					pendingRequest = null;
					pendingPaused = false;
					if (!notMuted) return;
					if (!tracks[trackNumber].HasClip) tracks[trackNumber].LoadFailed = false;
					if (!Prepare(trackNumber))
					{
						if (!tracks[trackNumber].LoadFailed)
							DelayCommand(command, trackNumber, fadeTime, commandBool1, commandBool2, commandFloat);
						return;
					}
				}
				else if (pendingRequest != null && pendingRequest.TrackNumber == trackNumber &&
					(command == Command.Stop || command == Command.FadeOut || command == Command.FadeOutManual))
				{
					pendingRequest = null;
					SetMusicStatus(0, -1);
				}
				if (!tracks[trackNumber].HasClip) return;
				if (startsTrack && notMuted && !tracks[trackNumber].CanStart)
				{
					Debug.LogWarning("MSPL: Both music sources are paused; stop or resume them before starting another track.");
					return;
				}
				switch (command)
				{
				case Command.Play:
					if (!notMuted)
					{
						break;
					}
					if (commandBool2)
					{
						for (int j = 0; j < tracks.Length; j++)
						{
							tracks[trackNumber].Stop();
						}
						tracks[trackNumber].Play(commandFloat);
					}
					else if (tracks[trackNumber].IsPlayingOrFadingIn)
					{
						if (commandBool1)
						{
							tracks[trackNumber].Restart(commandFloat);
						}
						else
						{
							Debug.LogWarning(string.Format("MSPL: ERORR: Attempt to Play() track number {0}, which is alreading playing", trackNumber));
						}
					}
					else
					{
						tracks[trackNumber].Play(commandFloat);
					}
					break;
				case Command.Stop:
					if (tracks[trackNumber].IsPlayingOrFading || tracks[trackNumber].IsPaused)
					{
						tracks[trackNumber].Stop();
					}
					else
					{
						Debug.LogWarning(string.Format("MSPL: ERORR: Attempt to Stop() track number {0}, which is not currently playing", trackNumber));
					}
					break;
				case Command.FadeOut:
				case Command.FadeIn:
				case Command.FadeInto:
				case Command.FadeOutManual:
				case Command.FadeInManual:
				case Command.FadeIntoManual:
				{
					if (!notMuted && command != Command.FadeOut && command != Command.FadeOutManual)
					{
						break;
					}
					bool flag = true;
					// A stopped/replaced track may still have an entry until Update.
					// Do not let that stale entry suppress a new menu fade.
					if (!tracks[trackNumber].IsFading) tracksFadingAuto.Remove(trackNumber);
					if ((command == Command.FadeOut || command == Command.FadeOutManual) &&
						tracks[trackNumber].IsNotPlayingNorFading) break;
					if (tracksFadingAuto.Contains(trackNumber))
					{
						bool flag2 = true;
						string arg = null;
						switch (command)
						{
						case Command.FadeOut:
						case Command.FadeOutManual:
							if (tracks[trackNumber].IsFadingOut)
							{
								arg = "FadeOut()";
								flag2 = true;
								flag = false;
							}
							break;
						case Command.FadeIn:
						case Command.FadeInManual:
							if (tracks[trackNumber].IsFadingIn)
							{
								arg = "FadeIn()";
								flag2 = true;
								flag = false;
							}
							break;
						case Command.FadeInto:
						case Command.FadeIntoManual:
							flag2 = false;
							flag = tracks[trackNumber].IsNotPlayingNorFadingIn;
							break;
						default:
							Debug.LogError(string.Format("MSPL: ERROR: Recevied unhandled Command of {0} in MusicPlay.Enact()'s case statement, nested under FadeOut/FadeIn/FadeInIfStopped case.  Check logic", command));
							flag = false;
							flag2 = false;
							break;
						}
						if (flag2)
						{
							Debug.LogWarning(string.Format("MSPL: ERORR: Attempt to fade {0} track number {1}, which is alreading fading {2}", arg, trackNumber, (!tracks[trackNumber].IsFadingIn) ? "out" : "in"));
							flag = false;
						}
					}
					bool flag3 = false;
					if (flag)
					{
						bool flag4 = true;
						switch (command)
						{
						case Command.FadeOut:
							tracks[trackNumber].FadeOut(fadeTime);
							flag3 = true;
							break;
						case Command.FadeIn:
							tracks[trackNumber].FadeIn(fadeTime);
							flag3 = true;
							break;
						case Command.FadeInto:
							flag4 = tracks[trackNumber].FadeInto(fadeTime);
							flag3 = true;
							break;
						case Command.FadeOutManual:
							tracks[trackNumber].FadeOutManual();
							flag3 = false;
							break;
						case Command.FadeInManual:
							tracks[trackNumber].FadeInManual();
							flag3 = false;
							break;
						case Command.FadeIntoManual:
							flag4 = tracks[trackNumber].FadeIntoManual();
							flag3 = false;
							break;
						default:
							Debug.LogError(string.Format("MSPL: ERROR: Recevied unhandled Command of {0} in MusicPlay.Enact()'s case statement, nested under FadeOut/FadeIn/FadeInIfStopped case.  Check logic", command));
							flag3 = false;
							break;
						}
						if (flag4)
						{
							if (flag3)
							{
								tracksFadingAuto.AddNew(trackNumber);
							}
							else
							{
								tracksFadingManual.AddNew(trackNumber);
							}
						}
					}
					if (command != Command.FadeInto && command != Command.FadeIntoManual)
					{
						break;
					}
					for (int i = 0; i < tracks.Length; i++)
					{
						if (i != trackNumber && tracks[i].IsPlayingOrFadingIn)
						{
							if (flag3)
							{
								tracks[i].FadeOut(fadeTime);
								tracksFadingAuto.AddNew(i);
							}
							else
							{
								tracks[i].FadeOutManual();
								tracksFadingManual.AddNew(i);
							}
						}
					}
					break;
				}
				default:
					Debug.LogError(string.Format("MSPL: ERROR: Recevied unhandled Command of {0} in MusicPlay.Enact()'s case statement.  Check logic", command));
					break;
				}
			}
			else if (tracks == null)
			{
				Debug.LogWarning(string.Format("MSPL: ERORR: Attempt to {0}() track number {1} when NO tracks have been initialized yet", command, trackNumber));
			}
			else
			{
				Debug.LogWarning(string.Format("MSPL: ERORR: Attempt to {0}() track number {1}, which is outside the valid range of tracks (0 to {2})", command, trackNumber, tracks.Length));
			}
		}
		else
		{
			DelayCommand(command, trackNumber, fadeTime, commandBool1, commandBool2, commandFloat);
		}
	}

	private static void EnactGlobal(Command command, float? fadeTime, bool commandBool1, bool commandBool2, bool forceCommand)
	{
		if (!isEnabled && !forceCommand)
		{
			return;
		}
		if (command == Command.Stop || command == Command.FadeOut)
		{
			pendingRequest = null;
			pendingPaused = false;
			SetMusicStatus(0, -1);
		}
		if (command == Command.Pause) pendingPaused = true;
		if (command == Command.Unpause) pendingPaused = false;
		if (gameObjectInitialized)
		{
			switch (command)
			{
			case Command.Stop:
			{
				for (int l = 0; l < tracks.Length; l++)
				{
					tracks[l].Stop();
				}
				tracksFadingAuto.Reset();
				tracksFadingManual.Reset();
				break;
			}
			case Command.FadeOut:
			{
				for (int j = 0; j < tracks.Length; j++)
				{
					if (tracks[j].IsPlayingOrFadingIn)
					{
						tracks[j].FadeOut(fadeTime);
						tracksFadingAuto.AddNew(j);
					}
				}
				break;
			}
			case Command.Pause:
			{
				for (int k = 0; k < tracks.Length; k++)
				{
					if (tracks[k].IsPlayingOrFading)
					{
						tracks[k].Pause();
					}
				}
				break;
			}
			case Command.Unpause:
			{
				for (int i = 0; i < tracks.Length; i++)
				{
					if (tracks[i].IsPaused)
					{
						tracks[i].Unpause();
					}
				}
				break;
			}
			default:
				Debug.LogError(string.Format("MSPL: ERROR: Recevied unhandled Command of {0} in MusicPlay.Enact()'s affect-all-tracks case statement.  Check logic", command));
				break;
			}
		}
		else
		{
			DelayCommand(command, null, fadeTime);
		}
	}

	private static void DelayCommand(Command commandToDelay, int? trackNumber, float? fadeTime,
		bool resetIfPlaying = false, bool exclusive = false, float? offset = null)
	{
		// Latest selection wins, including while a previous soundtrack decodes.
		pendingRequest = new Request
		{
			Command = commandToDelay, TrackNumber = trackNumber, FadeTime = fadeTime,
			ResetIfPlaying = resetIfPlaying, Exclusive = exclusive, Offset = offset
		};
	}

	private bool MusicNull()
	{
		bool flag = false;
		if (music != null && music.Length > 0)
		{
			for (int i = 0; i < music.Length; i++)
			{
				if (music[i] != null)
				{
					flag = true;
					break;
				}
			}
		}
		return !flag;
	}
}
#endif
