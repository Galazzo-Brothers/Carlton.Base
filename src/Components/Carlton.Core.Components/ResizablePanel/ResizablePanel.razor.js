export function initResizablePanel() {
    const bar = document.querySelector('.splitter-horizontal');
    const top = document.querySelector('.panel-top');
    const topHeightOffset = 100;
    let mouse_is_down = false;

    bar.addEventListener('mousedown', (e) => {
        mouse_is_down = true;
    });

    document.addEventListener('mousemove', (e) => {
        if (!mouse_is_down) return;

        top.style.cursor = "row-resize";
        top.style.height = `${e.clientY - topHeightOffset}px`;
    });

    document.addEventListener('mouseup', () => {
        mouse_is_down = false;
        top.style.cursor = "default";
    });
}