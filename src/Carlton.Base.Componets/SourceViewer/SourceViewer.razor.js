
export function highlightCodeBlock(selector) {
    let block = document.querySelector(selector);
    hljs.highlightElement(block);
};

export function getOutputSource(selector) {
    let viewer = document.querySelector(selector);
    let markup = viewer.innerHTML;
    markup = markup.replaceAll("<!--!-->", "");
    markup = markup.replaceAll(/\n/g, "");
    return format(markup);
}

export function setCodeBlock(selector, source) {
    let blocks = document.querySelector(selector);
    blocks.textContent = source;
}

function format(html) {
    let tab = '\t';
    let result = '';
    let indent = '';

    html.split(/>\s*</).forEach(function (element) {
        if (element.match(/^\/\w/)) {
            indent = indent.substring(tab.length);
        }

        result += indent + '<' + element + '>\r\n';

        if (element.match(/^<?\w[^>]*[^\/]$/) && !element.startsWith("input")) {
            indent += tab;
        }
    });

    return result.substring(1, result.length - 3);
}