(function () {
  'use strict';

  // Native progressive media playback: never fetch/arrayBuffer/decodeAudioData.
  // Two persistent elements allow menu crossfades, bounded even on fast swipes.
  var slots = [];
  var selected = null;
  var volume = 1;
  var pitch = 1;
  var sequence = 0;
  var animation = 0;
  var lastFrame = 0;
  var statusKey = '';
  var wasBlocked = false;
  var api = window.RollingSkyMusic = { command: command, unlock: unlock, blocked: blocked };

  function notify() {
    var state = !selected ? 0 : selected.failed ? 2 : ready(selected) ? 0 : 1;
    var key = state + ':' + (selected ? selected.track : -1);
    if (api.onStatus && key !== statusKey) {
      statusKey = key;
      api.onStatus(state, selected ? selected.title : '');
    }
    var isBlocked = blocked();
    if (api.onBlocked && isBlocked !== wasBlocked) { wasBlocked = isBlocked; api.onBlocked(); }
  }

  function ready(slot) {
    if (slot.failed || slot.audio.readyState < 3 || slot.audio.seeking) return false;
    if (!slot.wanted) return true;
    if (!slot.playing || slot.audio.paused) return false;
    // A cold native output pipeline can emit playing while its clock is still
    // stationary. Wait for real clock progress, including after seeks/resume.
    if (slot.clockAt !== null) {
      if (Math.abs(slot.audio.currentTime - slot.clockAt) < 0.04) return false;
      slot.clockAt = null;
    }
    return true;
  }

  function blocked() {
    return slots.some(function (slot) {
      return slot.wanted && !slot.failed && (slot.blocked || (slot.audio.paused && !slot.playPending));
    });
  }

  function makeSlot() {
    var audio = new Audio();
    audio.loop = true;
    audio.preload = 'auto';
    var slot = { audio: audio, track: -1, generation: 0, used: 0, wanted: false, gain: 1 };
    slots.push(slot);
    return slot;
  }

  function stop(slot) {
    slot.wanted = false;
    slot.paused = false;
    slot.playing = false;
    slot.blocked = false;
    slot.playPending = false;
    slot.session = false;
    slot.fade = null;
    slot.clockAt = null;
    slot.audio.pause();
    // Keep this source's compressed browser cache for instant same-song retries.
  }

  function choose(track, url, title) {
    var slot = slots.filter(function (item) { return item.track === track; })[0];
    if (!slot) {
      slot = slots.length < 2 ? makeSlot() : slots.reduce(function (a, b) { return a.used < b.used ? a : b; });
      stop(slot);
      slot.generation++;
      slot.audio.removeAttribute('src');
      slot.audio.load(); // Abort a replaced request and release the old media pipeline.
      slot.track = track;
      slot.failed = false;
      slot.offset = null;
      slot.title = title;
      slot.audio.onplaying = function () { slot.playing = true; slot.blocked = false; notify(); };
      slot.audio.onwaiting = slot.audio.onseeking = slot.audio.onpause = function () {
        slot.playing = false; slot.clockAt = slot.audio.currentTime; notify();
      };
      slot.audio.oncanplay = slot.audio.onseeked = function () { seek(slot); play(slot); notify(); };
      slot.audio.onloadedmetadata = function () { seek(slot); };
      slot.audio.ontimeupdate = notify;
      slot.audio.onerror = function () {
        slot.failed = true;
        slot.playing = false;
        slot.playPending = false;
        notify();
      };
      // Resolve against the document, not /assets or the Blob-backed Unity code.
      slot.audio.src = new URL(url, document.baseURI).href;
      slot.audio.load();
    }
    slot.used = ++sequence;
    selected = slot;
    notify();
    return slot;
  }

  function seek(slot) {
    if (slot.offset === null || slot.audio.readyState < 1) return;
    var offset = slot.offset;
    slot.offset = null;
    slot.audio.currentTime = offset;
  }

  function play(slot) {
    if (!slot.wanted || slot.failed || slot.playPending || slot.blocked || !slot.audio.paused) return;
    var generation = slot.generation;
    slot.playPending = true;
    var result = slot.audio.play();
    if (result && result.then) result.then(function () {
      if (generation !== slot.generation) return;
      slot.playPending = false;
      if (!slot.wanted) slot.audio.pause();
      notify();
    }, function (error) {
      if (generation !== slot.generation) return;
      slot.playPending = false;
      if (!slot.wanted || error.name === 'AbortError') return;
      if (error.name === 'NotAllowedError') slot.blocked = true;
      else slot.failed = true;
      notify();
    });
  }

  function start(slot, offset) {
    slot.playing = false;
    slot.paused = false;
    slot.wanted = true;
    slot.offset = Math.max(0, offset);
    slot.clockAt = slot.offset;
    slot.audio.playbackRate = pitch;
    // Unity pitch changes also change pitch, rather than time-stretching.
    slot.audio.preservesPitch = false;
    slot.audio.webkitPreservesPitch = false;
    seek(slot);
    play(slot);
  }

  function setGain(slot, gain) {
    slot.gain = gain;
    slot.audio.volume = Math.max(0, Math.min(1, volume * gain));
  }

  function fade(slot, target, seconds) {
    if (!slot.wanted) return;
    slot.fade = { from: slot.gain, to: target, duration: Math.max(0.001, seconds), elapsed: 0 };
    wakeFades();
  }

  function wakeFades() {
    if (!animation) { lastFrame = performance.now(); animation = requestAnimationFrame(updateFades); }
  }

  function updateFades(now) {
    var delta = (now - lastFrame) / 1000;
    lastFrame = now;
    animation = 0;
    var active = false;
    slots.forEach(function (slot) {
      var f = slot.fade;
      if (!f || slot.paused) return;
      f.elapsed += delta;
      var t = Math.min(1, f.elapsed / f.duration);
      var smooth = t * t * (3 - 2 * t);
      setGain(slot, f.from + (f.to - f.from) * smooth);
      if (t === 1) { slot.fade = null; if (f.to === 0) stop(slot); }
      else active = true;
    });
    if (active) animation = requestAnimationFrame(updateFades);
  }

  function unlock() {
    slots.forEach(function (slot) {
      slot.blocked = false;
      play(slot); // Synchronously within the trusted gesture, independently of SFX.
    });
    notify();
  }

  function command(name, track, url, title, value, offset) {
    var slot;
    if (name === 'volume' || name === 'pitch') {
      if (name === 'volume') volume = value; else pitch = value;
      slots.forEach(function (item) { setGain(item, item.gain); item.audio.playbackRate = pitch; });
      return 1;
    }
    if (name === 'stop' || name === 'pause' || name === 'unpause' || name === 'out') {
      slots.forEach(function (item) {
        if (track >= 0 && item.track !== track) return;
        if (name === 'stop') stop(item);
        if (name === 'pause' && item.wanted) {
          item.wanted = false; item.paused = true; item.playing = false; item.audio.pause();
        }
        if (name === 'unpause' && item.paused) { item.paused = false; item.wanted = true; item.clockAt = item.audio.currentTime; play(item); if (item.fade) wakeFades(); }
        if (name === 'out') fade(item, 0, value);
      });
      if (name === 'stop') selected = null;
      notify();
      return 1;
    }
    if (name === 'ready') {
      slot = slots.filter(function (item) { return item.track === track; })[0];
      notify();
      return slot && ready(slot) && !document.hidden ? 1 : 0;
    }
    slot = choose(track, url, title);
    if (name === 'prepare') return ready(slot) ? 1 : 0;
    if (name === 'begin') {
      if (!slot.session) {
        slots.forEach(function (other) { if (other !== slot) stop(other); });
        slot.fade = null;
        volume = 1;
        setGain(slot, 1);
        slot.session = true;
        start(slot, offset);
      }
    } else if (name === 'resume') {
      if (slot.paused) { slot.paused = false; slot.wanted = true; slot.clockAt = slot.audio.currentTime; play(slot); if (slot.fade) wakeFades(); }
    } else {
      var crossfade = name === 'into' || name === 'select';
      if (crossfade) slots.forEach(function (other) {
        if (other === slot) return;
        if (name === 'select') stop(other); else fade(other, 0, value);
      });
      if (!slot.wanted || name === 'restart') {
        setGain(slot, name === 'in' || name === 'into' ? 0 : 1);
        start(slot, offset);
      }
      if (name === 'in' || name === 'into') fade(slot, 1, value);
    }
    notify();
    return ready(slot) && !document.hidden ? 1 : 0;
  }
})();
