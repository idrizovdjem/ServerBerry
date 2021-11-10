const applicationId = document.currentScript.getAttribute("applicationId");

const cancelButton = document.getElementById('cancelButton');
const keyInput = document.getElementById('keyInput');
const environmentInput = document.getElementById('environmentInput');
const editButton = document.getElementById('editButton');
const form = document.getElementById('form');

cancelButton.onclick = (event) => {
    event.preventDefault();
    location.replace("/Applications/Overview?applicationId=" + applicationId);
}

editButton.onclick = async (event) => {
    event.preventDefault();

    const key = keyInput.value;
    const environment = environmentInput.value;

    const keyAvailable = await isKeyAvailable(key, environment);
    if (keyAvailable === false) {
        alert('Invalid key');
        return;
    }

    form.submit();
}

async function isKeyAvailable(key, environment) {
    return await fetch('/api/secrets/IsKeyAvailable', {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ key, environment, applicationId })
    })
        .then(response => response.json())
        .then(data => data)
        .catch(error => false);
}