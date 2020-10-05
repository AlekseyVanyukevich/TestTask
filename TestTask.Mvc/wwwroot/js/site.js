var firebaseConfig = {
    apiKey: "AIzaSyA9qLULQCsT_ksDA72JKmBrD5kMCcZO5sg",
    authDomain: "testtask-31960.firebaseapp.com",
    databaseURL: "https://testtask-31960.firebaseio.com",
    projectId: "testtask-31960",
    storageBucket: "testtask-31960.appspot.com",
    messagingSenderId: "963595274668",
    appId: "1:963595274668:web:b5c1da4d52fdeda79e9209",
    measurementId: "G-GWQRHCMTN6"
};

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
    $('.alert').click(function (event) {
        if (event.target.closest('.alert-close')) {
            this.remove();
        }
    });
    console.log('sdfsdf');
    $('[data-trigger="push"]').click(() => {
        var token = getToken();
        console.log(localStorage.getItem('token'));

        if (token) {
            $.ajax({
                method: 'GET',
                url: '/Library/Notify',
                data: {
                    token
                }
            });
        }

    });

    var getToken = () => {
        return localStorage.getItem('token');
    }

    firebaseStart();
    /*    const saveToken = token => {
            $.ajax({
                type: 'POST',
                url: '/Library/AddToken',
                data: { token }
            })
        }*/
});

var firebaseStart = () => {
    firebase.initializeApp(firebaseConfig);

    var messaging = firebase.messaging();

    var getRegToken = () => {
        messaging.getToken().then(currentToken => {
            if (currentToken) {
                saveToken(currentToken);
            }
        })
            .catch(err => console.log(err));
    }

    messaging.requestPermission()
        .then(() => {
            getRegToken();
        });

    messaging.onMessage(function (payload) {
        console.log('Payload', payload);
        notificationTitle = payload.notification.title;
        notificationOptions = {
            body: payload.notification.body
        };

        var notification = new Notification(notificationTitle, notificationOptions);
    });

    var saveToken = (token) => {
        localStorage.setItem('token', token);
    }





}