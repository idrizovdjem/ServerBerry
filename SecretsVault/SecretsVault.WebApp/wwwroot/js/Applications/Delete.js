const creatorEmail = document.currentScript.getAttribute('creatorEmail');
const applicationName = document.currentScript.getAttribute('applicationName');

const deleteButton = document.getElementById('deleteButton');
const phraseInput = document.getElementById('phraseInput');

deleteButton.disabled = true;

phraseInput.oninput = (event) => {
    const value = event.target.value;
    deleteButton.disabled = value !== `${creatorEmail}/${applicationName}`;
}