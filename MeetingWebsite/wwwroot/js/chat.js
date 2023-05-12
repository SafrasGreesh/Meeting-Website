"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    var btnDelete = document.createElement("button");
    var btnEdit = document.createElement("button");
    var text = document.createElement("span");
    document.getElementById("messagesList").appendChild(li);

    // Установка текста сообщения
    text.textContent = `${user} : ${message}`;
    li.appendChild(text);

    // Создание кнопки удаления
    btnDelete.textContent = "Delete";
    btnDelete.onclick = function () {
        li.remove();
    }
    li.appendChild(btnDelete);

    // Создание кнопки редактирования
    btnEdit.textContent = "Edit";
    btnEdit.onclick = function () {
        var newText = prompt("Enter new message");
        text.textContent = `${user} : ${newText}`;
    }
    li.appendChild(btnEdit);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
