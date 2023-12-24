export function applyTransitionedCallback() { 
    const transitionElements = document.querySelectorAll('.notification-bar .carlton-notification');

    for (let elem of transitionElements) {
        elem.ontransitionend = removeNode;
    }
}

export function removeTransitionedCallback() { 
    const transitionElements = document.querySelectorAll('.notification-bar .carlton-notification.dismissed');

    for (let elem of transitionElements) {
        elem.removeEventListener("transitionend", removeNode);
    }
}

function removeNode(event) {
    let notificationElm = event.target.parentElement.parentElement;
    notificationElm.remove();
}