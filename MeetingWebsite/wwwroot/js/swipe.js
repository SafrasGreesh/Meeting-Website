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
            id: user.id,
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
        // ������� ��� ����������� �������� ��������
        function showActiveCard() {

            if (currentCardIndex >= people.length) {
                cardsContainer.innerHTML = `<div class="card" style="background-color: #696969;">
                                                            <div class="card-body">
                                                                <h2 class="card-title" style="color: white;">������ �����������</h2>
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

        // ���������� ������ �������� ��� �������� ��������
        showActiveCard();

        function sendLikeRequest(id, like) {
            const url = '/Users/likes'; // ������� ���������� URL-���� ��� ��������� ������� �� �������

            const data = {
                id: id,
                like: like
            };

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('Network response was not ok');
                    }
                })
                .then(data => {
                    console.log(data);
                    // ��������� ��������� ������ �������
                })
                .catch(error => {
                    console.error('��������� ������ ��� �������� �������:', error);
                    // ��������� ������ ��� �������� �������
                });
        }


        // ���������� ������� ������ "����"
        // ���������� ������� ������ "����"
        likeButton.addEventListener("click", () => {
            const id = 5; // ����� ����������� ������ ID
            const like = true; // ����� ����������� �������� �����
            sendLikeRequest(id, like);
            const card = cardsContainer.querySelector(".card");
            card.classList.add("card-liked");
            currentCardIndex++;
            setTimeout(() => {
                card.classList.remove("card-liked");
                showActiveCard();
            }, 500);

            

        });

        // ���������� ������� ������ "�������"
        dislikeButton.addEventListener("click", () => {
            const id = 5; // ����� ����������� ������ ID
            const like = false; // ����� ����������� �������� �����
            sendLikeRequest(id, like);
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
        console.log('��������� ������:', error);
    }
}
