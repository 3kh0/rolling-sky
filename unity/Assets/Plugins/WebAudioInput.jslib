mergeInto(LibraryManager.library, {
	RollingSky_InitializeBrowserAudio__deps: ['$WEBAudio'],
	RollingSky_InitializeBrowserAudio: function () {
		// Publish Unity's actual context, not a second audio player or soundtrack.
		// The page resumes it synchronously inside a trusted touch/click/key event.
		var context = WEBAudio.audioContext;
		if (!WEBAudio.audioWebEnabled || !context || !context.resume) {
			return;
		}
		Module['rollingSkyAudioContext'] = context;
		if (Module['onRollingSkyAudioReady']) {
			Module['onRollingSkyAudioReady'](context);
		}
	},
	RollingSky_IsBrowserAudioReady__deps: ['$WEBAudio'],
	RollingSky_IsBrowserAudioReady: function () {
		return !document.hidden && WEBAudio.audioWebEnabled && WEBAudio.audioContext.state === 'running' ? 1 : 0;
	},
	RollingSky_SetMusicStatus: function (state, title) {
		if (Module['onRollingSkyMusicStatus']) {
			Module['onRollingSkyMusicStatus'](state, Pointer_stringify(title));
		}
	},
	RollingSky_Music: function (command, track, url, title, value, offset) {
		return window.RollingSkyMusic.command(Pointer_stringify(command), track,
			Pointer_stringify(url), Pointer_stringify(title), value, offset);
	}
});
