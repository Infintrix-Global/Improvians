jQuery(document).ready(function ($) {
    if ('serviceWorker' in navigator) {
        console.log('CLIENT: service worker registration in progress.');
        navigator.serviceWorker.register('./sw.js').then(function () {
            console.log('CLIENT: service worker registration complete.');
        }).catch(function (err) {
            console.log("Service Worker Failed to Register", err);
        })
    } else {
        console.log('CLIENT: service worker is not supported.');
    }
});