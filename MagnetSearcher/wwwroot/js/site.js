// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
        // call on next available tick
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) {
            return decodeURIComponent(pair[1].replaceAll("+", " "));
        }
    }
    return (false);
}

document.getElementsByClassName('search')[0]
    .addEventListener('click', function (event) {
        window.location = `//${window.location.host}/Search/?KeyWord=${document.getElementById('searchKeyword').value}&Skip=0&Take=20`
});

docReady(() => {
    if (getQueryVariable("KeyWord")) {
        document.getElementById('searchKeyword').value = getQueryVariable("KeyWord");
    }
})