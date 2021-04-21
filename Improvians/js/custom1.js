jQuery(document).ready(function ($) {
    if ('serviceWorker' in navigator) {
        alert('CLIENT: service worker registration in progress.');
        navigator.serviceWorker.register('./sw.js').then(function () {
            alert('CLIENT: service worker registration complete.');
        }).catch(function (err) {
            alert("Service Worker Failed to Register", err);
        })
    } else {
        alert('CLIENT: service worker is not supported.');
    }
});