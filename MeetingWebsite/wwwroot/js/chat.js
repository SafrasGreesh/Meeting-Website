"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

var selectedImage = null; // Переменная для хранения выбранной фотографии

connection.on("ReceiveMessage", function (user, message, image, timestamp) {
    var li = createMessageListItem(user, message, image, timestamp);
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = "Алексей";
    var message = document.getElementById("messageInput").value;
    var file = selectedImage;

    if (message.trim() !== "" || file) {
        if (file) {
            var reader = new FileReader();
            reader.onload = function () {
                var img = document.createElement("img");
                img.src = reader.result;
                img.classList.add("message-image");
                var li = createMessageListItem(user, message, img, getCurrentTimestamp());
                document.getElementById("messagesList").appendChild(li);
                connection.invoke("SendMessage", user, message, reader.result, getCurrentTimestamp()).catch(function (err) {
                    return console.error(err.toString());
                });
                resetPhotoInput();
            };
            reader.readAsDataURL(file);
        } else {
            var li = createMessageListItem(user, message, null, getCurrentTimestamp());
            document.getElementById("messagesList").appendChild(li);
            connection.invoke("SendMessage", user, message, null, getCurrentTimestamp()).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

    event.preventDefault();
});

document.getElementById("photoInput").addEventListener("change", function () {
    var file = this.files[0];
    selectedImage = file; // Сохраняем выбранную фотографию
});

function createMessageListItem(user, message, image, timestamp) {
    var li = document.createElement("li");
    li.classList.add("message-item");

    var userSpan = document.createElement("span");
    userSpan.classList.add("message-user");
    userSpan.textContent = user + ": ";
    li.appendChild(userSpan);

    var textSpan = document.createElement("span");
    textSpan.classList.add("message-text");
    textSpan.textContent = message;
    li.appendChild(textSpan);

    if (image) {
        var imageContainer = document.createElement("div");
        imageContainer.appendChild(image);
        li.appendChild(imageContainer);
    }

    // Создание кнопки удаления
    var btnDelete = document.createElement("button");
    btnDelete.textContent = "Удалить";
    btnDelete.classList.add("delete-button");
    btnDelete.onclick = function () {
        var confirmDelete = confirm("Вы действительно хотите удалить сообщение?");
        if (confirmDelete) {
            li.remove();
        }
    };
    li.appendChild(btnDelete);

    // Создание кнопки редактирования
    var btnEdit = document.createElement("button");
    btnEdit.textContent = "Изменить";
    btnEdit.classList.add("edit-button");
    btnEdit.onclick = function () {
        var newText = prompt("Enter new message");
        textSpan.textContent = newText;
    };
    li.appendChild(btnEdit);

    var timeSpan = document.createElement("span");
    timeSpan.classList.add("message-time");
    timeSpan.textContent = formatTimestamp(timestamp);
    li.appendChild(timeSpan);

    return li;
}

function resetPhotoInput() {
    var fileInput = document.getElementById("photoInput");
    fileInput.value = "";
    selectedImage = null;
}

function getCurrentTimestamp() {
    return new Date().toISOString();
}

function formatTimestamp(timestamp) {
    var date = new Date(timestamp);
    var options = {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false, // Использование 24-часового формата
        timeZone: "UTC", // Установка временной зоны на UTC
        hourCycle: "h24" // Используем 24-часовой формат
    };
    return date.toLocaleString("en-US", options).replace("at", "").trim(); // Удаляем слово "at" и лишние пробелы
}
