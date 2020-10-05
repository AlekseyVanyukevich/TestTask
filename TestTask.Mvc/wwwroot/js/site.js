// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const firebaseConfig = {
    apiKey: "AIzaSyA9qLULQCsT_ksDA72JKmBrD5kMCcZO5sg",
    authDomain: "testtask-31960.firebaseapp.com",
    databaseURL: "https://testtask-31960.firebaseio.com",
    projectId: "testtask-31960",
    storageBucket: "testtask-31960.appspot.com",
    messagingSenderId: "963595274668",
    appId: "1:963595274668:web:b5c1da4d52fdeda79e9209",
    measurementId: "G-GWQRHCMTN6"
};

$(document).ready(function (){
    $('[data-toggle="popover"]').popover();
    $('.alert').click(function ({ target }) {
        if (target.closest('.alert-close')) {
            this.remove();
        }
    });
    $('[data-trigger="push"]').click(() => {
        const token = getToken();
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

    const getToken = () => {
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

const firebaseStart = () => {
    firebase.initializeApp(firebaseConfig);

    const messaging = firebase.messaging();

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

        const notification = new Notification(notificationTitle, notificationOptions);
    });

    const saveToken = (token) => {
        localStorage.setItem('token', token);
    }


    const getRegToken = () => {
        messaging.getToken().then(currentToken => {
                console.log(currentToken);
                if (currentToken) {
                    saveToken(currentToken);
                }
            })
            .catch(err => console.log(err));
    }


}


