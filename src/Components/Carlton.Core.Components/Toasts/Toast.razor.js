export function initNewToast(dotnetHelper, dotnetCallbackMethod, id) {
    const toastElement = document.getElementById(`toast-${id}`);
    //Add the event listener
    let onTransitionedCallback = onTransitionend.bind(this, dotnetHelper, dotnetCallbackMethod);
    toastElement.addEventListener('transitionend', onTransitionedCallback);
}


//// Function to be executed when the animation ends
function onTransitionend(dotnetHelper, dotnetCallbackMethod) {
    dotnetHelper.invokeMethodAsync(dotnetCallbackMethod);
}
