importScripts('https://www.gstatic.com/firebasejs/7.21.1/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/7.21.1/firebase-messaging.js');

const config = {
    apiKey: "AIzaSyA9qLULQCsT_ksDA72JKmBrD5kMCcZO5sg",
    authDomain: "testtask-31960.firebaseapp.com",
    databaseURL: "https://testtask-31960.firebaseio.com",
    projectId: "testtask-31960",
    storageBucket: "testtask-31960.appspot.com",
    messagingSenderId: "963595274668",
    appId: "1:963595274668:web:b5c1da4d52fdeda79e9209",
    measurementId: "G-GWQRHCMTN6"
}

firebase.initializeApp(config);

const messaging = firebase.messaging();


messaging.setBackgroundMessageHandler(function(payload) {
   console.log('[firebase-messaging-sw.js] Received background message ', payload)
   const notificationTitle = 'Background Message Title';
   const notificationOptions = {
      body: 'Background Message body.'
   };
   return self.registration.showNotification(notificationTitle,
       notificationOptions)
});

//self.addEventListener('notificationclick', event => {
//    const urlToRedirect = event.notification.data.url;
//    event.notification.close();
//    event.waitUntil(self.clients.openWindow(urlToRedirect));
//});