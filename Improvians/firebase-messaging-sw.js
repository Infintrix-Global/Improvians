importScripts('https://www.gstatic.com/firebasejs/8.4.1/firebase-app.js');
importScripts('./js/firebase-messaging.js');

// Initialize the Firebase app in the service worker by passing in the
// messagingSenderId.
firebase.initializeApp({
    apiKey: "AIzaSyDqjI1uzyT7VG2T39BLPALZDkcN3vJDLX8",
    authDomain: "growerstrans-1172b.firebaseapp.com",
    projectId: "growerstrans-1172b",
    storageBucket: "growerstrans-1172b.appspot.com",
    messagingSenderId: "342218804881",
    appId: "1:342218804881:web:5ceb489d5eb65407aaf930",
    measurementId: "G-1FYPTX710Q"
});

// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();

messaging.setBackgroundMessageHandler(function (payload) {
    console.log(
        "[firebase-messaging-sw.js] Received background message ",
        payload,
    );
    // Customize notification here
    const notificationTitle = "Background Message Title";
    const notificationOptions = {
        body: "Background Message body1.",
        icon: "/itwonders-web-logo.png",
    };

    return self.registration.showNotification(
        notificationTitle,
        notificationOptions,
    );

});

messaging.onBackgroundMessage((payload) => {
    var postData = JSON.parse(payload.data.postData);

    const notificationTitle = postData.Title;
    var click_action = "https://webportal.growerstrans.com/TESTGEM/DashBoard.aspx";
    const notificationOptions = {
        body: postData.Message,
        icon: 'images/badge.png',
        badge: 'images/badge.png',
        data: {
            url: click_action
        }
    };

    self.registration.showNotification(notificationTitle,
        notificationOptions);



    self.addEventListener('notificationclick', function (event) {
        var notification = event.notification;
        var action = event.action;

        console.log(notification);

        if (action === 'confirm') {
            console.log('Confirm was chosen');
            notification.close();
        } else {
            console.log(action);
            event.waitUntil(
                clients.matchAll()
                    .then(function (clis) {
                        var client = clis.find(function (c) {
                            return c.visibilityState === 'visible';
                        });

                        if (client !== undefined) {
                            client.navigate(notification.data.url);
                            client.focus();
                        } else {
                            clients.openWindow(notification.data.url);
                        }
                        notification.close();
                    })
            );
        }
    });
});
