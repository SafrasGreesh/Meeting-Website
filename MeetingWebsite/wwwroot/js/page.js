function calculateAge(birthDate) {
    var today = new Date();
    var birthDate = new Date(birthDate);

    var age = today.getFullYear() - birthDate.getFullYear();
    var monthDiff = today.getMonth() - birthDate.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }

    return age;
}


function getNumberFromServer() {
    return new Promise((resolve, reject) => {
        fetch('/users/TakeId')
            .then(response => response.json())
            .then(data => {
                const number = data; // Предполагается, что сервер возвращает только число
                console.log(number);
                // Дальнейшая обработка числа
                // Например, можно сохранить число в отдельную переменную или выполнить другие действия с ним
                resolve(number); // Разрешить промис с числом
            })
            .catch(error => {
                console.error(error);
                reject(error); // Отклонить промис с ошибкой
            });
    });
}