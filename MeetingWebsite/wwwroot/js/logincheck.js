function sendGetRequest() {
    fetch('/Users/TakeToken')
        .then(response => {
            if (response.ok) {
                // ��������� ��������� ������
            } else if (response.status === 400) {
                // ������������ �� �����������, ��������� ���������������
                //alert("������������ �� �����������");
                window.location.href = '/Index';
            } else {
                // ��������� ������ ������
            }
        })
        .catch(error => {
            // ��������� ������ ��� ���������� �������
        });
}

sendGetRequest();