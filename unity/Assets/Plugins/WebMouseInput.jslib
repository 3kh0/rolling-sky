mergeInto(LibraryManager.library, {
	RollingSky_GetSafeInset: function (edge) {
		var insets = Module['rollingSkySafeArea'];
		return insets ? insets[edge] : 0;
	},
	RollingSky_GetMousePosition: function () {
		// Pointer events distinguish a real mouse from touch-generated mouse events.
		// Keep the original Unity drag path for touch and pen input.
		var state = Module['rollingSkyMouse'];
		if (!state) {
			state = Module['rollingSkyMouse'] = { x: null, y: null };
			var clear = function () {
				state.x = null;
				state.y = null;
			};
			var update = function (event) {
				if (event.pointerType === 'mouse') {
					state.x = event.clientX;
					state.y = event.clientY;
				} else {
					clear();
				}
			};
			Module.canvas.addEventListener('pointermove', update);
			Module.canvas.addEventListener('pointerdown', update);
			Module.canvas.addEventListener('pointerleave', clear);
			Module.canvas.addEventListener('pointercancel', clear);
			window.addEventListener('blur', clear);
			document.addEventListener('visibilitychange', clear);
		}

		if (state.x === null || document.hidden) {
			return -1;
		}
		// Use CSS coordinates, not backing pixels: DPR and portrait letterboxing
		// must not change where the mouse points. Re-read bounds after resizing.
		var bounds = Module.canvas.getBoundingClientRect();
		if (bounds.width <= 0 || bounds.height <= 0 ||
			state.x < bounds.left || state.x > bounds.right ||
			state.y < bounds.top || state.y > bounds.bottom) {
			return -1;
		}
		return (state.x - bounds.left) / bounds.width;
	}
});
