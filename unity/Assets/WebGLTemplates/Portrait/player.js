(function () {
  'use strict';

  var player = document.getElementById('player');
  var container = document.getElementById('gameContainer');
  var soundButton = document.getElementById('enableSound');
  var audioContext = null;
  var loaded = false;

  if (window.RollingSkyMusic) {
    window.RollingSkyMusic.onBlocked = updateSoundButton;
  }

  function updateSoundButton() {
    var musicBlocked = window.RollingSkyMusic && window.RollingSkyMusic.blocked();
    soundButton.hidden = !loaded || ((!audioContext || audioContext.state === 'running') && !musicBlocked);
    soundButton.disabled = !!audioContext && audioContext.state === 'closed';
    soundButton.textContent = soundButton.disabled ? 'Audio stopped — reload to retry' : 'Tap to enable sound';
  }

  function audioReady(context) {
    if (audioContext === context) return;
    if (audioContext) audioContext.removeEventListener('statechange', updateSoundButton);
    audioContext = context;
    audioContext.addEventListener('statechange', updateSoundButton);
    updateSoundButton();
  }

  function resumeAudio(event) {
    if (!event.isTrusted || document.hidden) return;
    if (event.type === 'keydown' && event.key !== 'Enter' && event.key !== ' ') return;
    if (window.RollingSkyMusic) window.RollingSkyMusic.unlock();
    // Include Safari's interrupted state after a phone call or app switch.
    // Call resume inside the gesture, not in a timer or a later Unity frame.
    if (audioContext && (audioContext.state === 'suspended' || audioContext.state === 'interrupted')) {
      audioContext.resume().then(updateSoundButton, function (error) {
        console.warn('Rolling Sky audio could not resume:', error);
        updateSoundButton();
      });
    }
  }

  // Capture before Unity consumes touch events. Do not preventDefault or
  // synthesize mouse events: the native touch/pen drag path must stay intact.
  ['pointerup', 'touchend', 'click', 'keydown'].forEach(function (type) {
    player.addEventListener(type, resumeAudio, true);
  });
  document.addEventListener('visibilitychange', updateSoundButton);
  window.addEventListener('pageshow', updateSoundButton);

  function resizeGame() {
    var viewport = window.visualViewport;
    var availableWidth = viewport ? viewport.width : window.innerWidth;
    var availableHeight = viewport ? viewport.height : window.innerHeight;
    var height = Math.floor(availableHeight);
    var width = Math.floor(Math.min(availableWidth, height * 9 / 16));
    document.body.style.height = availableHeight + 'px';
    player.style.width = width + 'px';
    player.style.height = height + 'px';
    var instance = window.gameInstance;
    if (instance && instance.Module && instance.Module.canvas) {
      var style = getComputedStyle(player);
      instance.Module.rollingSkySafeArea = [
        (parseFloat(style.getPropertyValue('--safe-area-top')) || 0) / height,
        (parseFloat(style.getPropertyValue('--safe-area-bottom')) || 0) / height
      ];
      if (instance.Module.canvas.width !== width) instance.Module.canvas.width = width;
      if (instance.Module.canvas.height !== height) instance.Module.canvas.height = height;
    }
  }

  window.addEventListener('resize', resizeGame);
  window.addEventListener('orientationchange', resizeGame);
  if (window.visualViewport) window.visualViewport.addEventListener('resize', resizeGame);

  resizeGame();
  window.gameInstance = UnityLoader.instantiate(container, container.dataset.buildUrl, {
    compatibilityCheck: function (instance, success, failure) {
      // Unity 2017 warns about mobile user agents even when their WebGL works.
      // Keep the actual capability gate, not its obsolete device allowlist.
      if (UnityLoader.SystemInfo.hasWebGL) {
        success();
      } else {
        container.textContent = 'WebGL is unavailable. Enable graphics acceleration or try another browser.';
        failure();
      }
    },
    onProgress: function (instance, progress) {
      loaded = progress === 1;
      updateSoundButton();
      resizeGame();
    },
    Module: { onRollingSkyAudioReady: audioReady }
  });
  resizeGame();
})();
