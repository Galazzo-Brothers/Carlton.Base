export function initNewToast(id) {
    const toastElement = document.getElementById(`toast-${id}`);

    // Add the event listener
    toastElement.addEventListener('transitionend', onTransitionend);
}


// Function to be executed when the animation ends
function onTransitionend() {
    // Remove the event listener
    this.removeEventListener('transitionend', onTransitionend);

    // Remove the element from the DOM
    this.remove();
}
