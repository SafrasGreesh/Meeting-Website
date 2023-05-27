function sendGetRequest() {
    fetch('/Users/TakeToken')
        .then(response => {
            if (response.ok) {
                // ќбработка успешного ответа
            } else if (response.status === 400) {
                // ѕользователь не авторизован, выполните перенаправление
                //alert("ѕользователь не авторизован");
                window.location.href = '/Index';
            } else {
                // ќбработка других ошибок
            }
        })
        .catch(error => {
            // ќбработка ошибок при выполнении запроса
        });
}

sendGetRequest();