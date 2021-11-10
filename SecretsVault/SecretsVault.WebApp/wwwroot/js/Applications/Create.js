const applicationNameInput = document.getElementById('applicationNameInput');
const applicationNameError = document.getElementById('applicationNameError');
const createButton = document.getElementById('createButton');
const cancelButton = document.getElementById('cancelButton');

cancelButton.onclick = (event) => {
    event.preventDefault();
    location.replace("/");
}

applicationNameError.style.display = 'none';

applicationNameInput.oninput = async (event) => {
    const value = event.target.value;
    if (value.length < 3) {
        return;
    }

    const isAvailable = await isNameAvailable(value);
    const errorVisibility = isAvailable == false ? 'block' : 'none';

    createButton.disabled = isAvailable == false;
    applicationNameError.style.display = errorVisibility;
}

async function isNameAvailable(value) {
    return await fetch('/api/applications/isNameAvailable', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: value })
    })
        .then(response => response.json())
        .then(data => data)
        .catch(error => false);
}