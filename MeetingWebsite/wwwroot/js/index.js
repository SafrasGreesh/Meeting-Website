//const { password } = require("../../server/config");

const startChatBtn = document.querySelector('#startChatBtn');
const menuModel = document.querySelector('#menuModel');
const room_id = document.querySelector('#room-id');
const room_password = document.querySelector('#room-password');
const user_name = document.querySelector('#user-name');
const user_password = document.querySelector('#user-password');

// let room_id_value;
// let room_password_value;
let user_name_value;
let user_password_value;

// input from html
startChatBtn.addEventListener('click', async _ => {
  // room_id_value =  room_id.value;
  // room_password_value = room_password.value;
  user_name_value = user_name.value;
  user_password_value = user_password.value;

  let data = {name: user_name_value, password:user_password_value};
  const response = await fetch('/login', {
    method: "POST",
    headers: {'Content-Type': 'application/json'}, 
    body: JSON.stringify(data)
    }).then(res => res.json()).then(data => {
      console.log(data);
      window.location.replace('/chat?username='+user_name_value +'&accessToken='+data['accessToken']+
        '&refreshToken='+data['refreshToken']);
  }).catch(error => console.error('Error:', error));
});
