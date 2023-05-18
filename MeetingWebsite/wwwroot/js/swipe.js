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


async function getUsers() {
    try {
        const response = await fetch('/users/swipe');
        const data = await response.json();

        const people = data.map((user) => ({
            name: user.name,
            age: calculateAge(user.birthDate),
            city: user.city,
            img: user.photo,
            description: user.description,
        }));

        const cardsContainer = document.querySelector(".cards-container");
        const likeButton = document.querySelector("#like-button");
        const dislikeButton = document.querySelector("#dislike-button");

        let currentCardIndex = 0;
        showActiveCard();
        // Функция для отображения активной карточки
        function showActiveCard() {

            if (currentCardIndex >= people.length) {
                cardsContainer.innerHTML = `<div class="card" style="background-color: #696969;">
                                                            <div class="card-body">
                                                                <h2 class="card-title" style="color: white;">Анкеты закончились</h2>
                                                            </div>
                                                        </div>`;
            } else {
                cardsContainer.innerHTML = `

                                                        <div class="card" style="background-image: url('${people[currentCardIndex].img}');">
                                                            <div class="card-body">
                                                                <h5 class="card-title" style="color: white;">${people[currentCardIndex].name}, ${people[currentCardIndex].age}</h5>
                                                                <p class="card-text" style="color: white;">${people[currentCardIndex].city}</p>
                                                                <p style="color: white;">${people[currentCardIndex].description}</p>
                                                            </div>
                                                        </div>

                                    `;
            }
        }

        // Показываем первую карточку при загрузке страницы
        showActiveCard();

        // Обработчик нажатия кнопки "Лайк"
        // Обработчик нажатия кнопки "Лайк"
        likeButton.addEventListener("click", () => {
            const card = cardsContainer.querySelector(".card");
            card.classList.add("card-liked");
            currentCardIndex++;
            setTimeout(() => {
                card.classList.remove("card-liked");
                showActiveCard();
            }, 500);
        });

        // Обработчик нажатия кнопки "Дизлайк"
        dislikeButton.addEventListener("click", () => {
            const card = cardsContainer.querySelector(".card");
            card.classList.add("card-disliked");
            currentCardIndex++;
            setTimeout(() => {
                card.classList.remove("card-disliked");
                showActiveCard();
            }, 500);
        });


        console.log(people);
    } catch (error) {
        console.log('Произошла ошибка:', error);
    }
}
