const applicationId = document.currentScript.getAttribute("applicationId");

window.onload = () => {
    handleShowHideElements();
    handleDeleteElements();
}

function handleShowHideElements() {
    const copyKeyButton = document.getElementById('copyKeyButton');
    copyKeyButton.addEventListener('click', async () => {
        const secretKeyResponse = await getSecretKey();
        if (secretKeyResponse.successfull === false) {
            alert(secretKeyResponse.errorMessage);
            return;
        }

        const secretKey = secretKeyResponse.key;
        navigator.clipboard.writeText(secretKey);
    });

    const passwordShowHideElements = Array.from(document.getElementsByClassName('passwordShowHideElement'));
    passwordShowHideElements.forEach(element => {
        let visible = false;

        element.onclick = async (event) => {
            visible = !visible;
            element.innerHTML = visible ? '<i class="fas fa-eye-slash"></i>' : '<i class="fas fa-eye"></i>';

            let secretValue = '**********';
            const rowElement = element.parentElement.parentElement;

            if (visible === true) {
                const key = rowElement.children[0].innerText;
                const environment = rowElement.children[1].innerText;
                secretValue = await getSecretValue(key, environment);
            }

            rowElement.children[2].innerText = secretValue
        }
    });
}

function handleDeleteElements() {
    const deleteElements = Array.from(document.getElementsByClassName('deleteElement'));
    deleteElements.forEach((element, index) => {
        element.onclick = async (event) => {
            const confirmDelete = confirm('Are you sure you want to delete this secret?');
            if (confirmDelete === false) {
                return;
            }

            const parentElement = element.parentElement.parentElement;
            const secretId = parentElement.lastElementChild.value;
            deleteSecret(secretId);
            parentElement.remove();
        }
    });
}

async function getSecretValue(key, environment) {
    return await fetch('/api/secrets/GetSecretValue', {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ key, environment, applicationId: applicationId })
    })
        .then(response => response.text())
        .then(data => data)
        .catch(error => '');
}

function deleteSecret(secretId) {
    fetch('/api/secrets/DeleteSecret', {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ secretId })
    })
        .catch(error => '');
}

async function getSecretKey() {
    return fetch('/api/applications/GetSecretKey?applicationId=' + applicationId)
        .then(response => response.text())
        .then(data => {
            return {
                successfull: true,
                key: data,
                errorMessage: ''
            };
        })
        .catch(error => {
            return {
                successfull: false,
                key: null,
                errorMessage: 'Something went wrong getting the secret key'
            };
        });
}