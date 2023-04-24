export function getOutputSource() {
    let viewer = document.querySelector(".component-viewer");
    let markup = viewer.innerHTML;
    markup = markup.replaceAll("<!--!-->", "");
    markup = markup.replaceAll(/\n/g, "");
    return format(markup);
}

export function postRender(markup) {
    setOutputMarkup(markup);
    highlightCodeBlock();
}

function setOutputMarkup(markup) {
    let block = document.querySelector('.test-component-output-source pre code');
    block.textContent = markup;
}

function highlightCodeBlock() {
    let blocks = document.querySelectorAll('pre code');
    Array.prototype.forEach.call(blocks, hljs.highlightBlock);
};

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

