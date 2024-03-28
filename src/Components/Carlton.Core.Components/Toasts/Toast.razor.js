
export function initNewToast(dotnetHelper, dotnetCallbackMethod, id) {
    const toastElement = document.getElementById(`toast-${id}`);
    //Add the event listener
    let onTransitionedCallback = onTransitionend.bind(this, dotnetHelper, dotnetCallbackMethod);
    toastElement.ontransitionend = onTransitionedCallback;
}

export function disposeToast(id) {
    const toastElement = document.getElementById(`toast-${id}`);
				toastElement.ontransitionend = null; //remove the event handler
}

function onTransitionend(dotnetHelper, dotnetCallbackMethod, evt) {
    //Event.AT_TARGET (2) only on the dismiss transition change
    if (evt.eventPhase == 2 && evt.target.classList.contains('dismissed')) {
        dotnetHelper.invokeMethodAsync(dotnetCallbackMethod);
    }
}
