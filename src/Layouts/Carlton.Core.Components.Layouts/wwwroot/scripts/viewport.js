export { viewport };

let viewport = {
	getViewport: getViewport,
	registerViewportChangedHandler: registerViewportChangedHandler,
	removeViewportChangedHandlers: removeViewportChangedHandlers
}

let registeredCallbacks = [];

function registerViewportChangedHandler(dotnetHelper, dotnetCallbackMethod) {
	let onResizeHandler = handleResize.bind(this, dotnetHelper, dotnetCallbackMethod);
	registeredCallbacks.push(onResizeHandler);
	window.addEventListener('resize', onResizeHandler);
}

function removeViewportChangedHandlers() {

	registeredCallbacks.forEach(function (item) {
		window.removeEventListener('resize', item)
	});

	registeredCallbacks = [];
}


function getViewport() {
	return {
		height: window.innerHeight,
		width: window.innerWidth
	}
}

function handleResize(dotnetHelper, dotnetCallbackMethod) {
	let result = getViewport();
	dotnetHelper.invokeMethodAsync(dotnetCallbackMethod, result);
}


