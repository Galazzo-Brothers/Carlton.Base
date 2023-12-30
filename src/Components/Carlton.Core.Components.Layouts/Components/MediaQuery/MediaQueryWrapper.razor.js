import { viewport } from '../../scripts/viewport.js';

let onResizeHandler;

export function initMediaQueryWrapper(dotnetHelper) {
	onResizeHandler = handleResize.bind(this, dotnetHelper);
	window.addEventListener('resize', onResizeHandler);
}

export function cleanupMediaQueryWrapper() {
	window.removeEventListener('resize', onResizeHandler)
}

function handleResize(dotnetHelper) {
	let result = viewport.getViewport();
	dotnetHelper.invokeMethod('ViewportUpdated', result);
}

