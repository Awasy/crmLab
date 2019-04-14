// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    let dates = [];
    

    Notification.requestPermission(function (permission) {
        console.log('Result permission: ', permission);
    });
    
    let pathname = window.location.pathname;
    if (pathname == '/' || pathname == '/Clients') {
        ajaxGetEvents().done(function () { sendNotification() });
    }
    else {
        ajaxGetEvents();
    };

    function ajaxGetEvents() {
       return $.ajax({
            url: " http://localhost:5000/events/GetEvents",
            dataType: "json",
            success: function (response) {
                dates = response;
           },
           error: function (error) {
               alert("Загрузка данных не удалась, перезагрузите страницу ");
           }
        });
    };

    function sendNotification() {
        let now = new Date();
        let splitsStrings;
        let minutes = '';
        let answer = '';
        for (let data of dates) {
            splitsStrings = data.split('<//n>');

            if (now.getMinutes() < 10) {
                minutes = '0' + now.getMinutes();
            }
            else {
                minutes = now.getMinutes();
            }
            //splitsStrings[0].split(' ')[0] - Дата события
            //splitsStrings[1] - Время события
            //splitsStrings[2] - название события
            //splitsStrings[3] - Текст события
            //splitsStrings[4,5,6,7] - Данные клиента
            let date = splitsStrings[0].split(' ')[0];
            if (now.toLocaleDateString() == date && (now.getHours() + ":" + minutes + ":00") == splitsStrings[1]) {
                answer = `Название события: ${splitsStrings[2]}\nТекст события: ${splitsStrings[3]}\nКлиент: ${splitsStrings[4]} ${splitsStrings[5]} ${splitsStrings[6]} ${splitsStrings[7]}\nДата и время: ${date} ${splitsStrings[1]}`;
                let notification = new Notification(splitsStrings[2], { body: answer, dir: "rtl", requireInteraction: true });

                notification.onerror(function () {
                    alert("При формировании уведомления возникла ошибка.");
                })
            }
        }
    };
        
    setInterval(function () { sendNotification() }, 60000);


   
})