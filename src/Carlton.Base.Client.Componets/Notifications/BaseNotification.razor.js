export function applyTransitionedCallback() {
    const transitionElements = document.querySelectorAll('.carlton-notification');

    for (let elem of transitionElements) {
        elem.ontransitionend = () => {
            elem.parentElement.remove();
        };
    }
}