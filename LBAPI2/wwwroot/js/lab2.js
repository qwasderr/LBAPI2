const uri = ' api/Countries';
let countries = [];
document.getElementById('editForm').style.display = 'none';
function getCountries() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCountries(data))
        .catch(error => console.error('Unable to get countries.', error));
}
function addCountry() {
    const addNameTextbox = document.getElementById('add-name');
    const addRatingTextbox = document.getElementById('add-rating');
    const country = {
        name: addNameTextbox.value.trim(),
        rating: addRatingTextbox.value.trim(),
    };

    const country2 = countries.find(c => c.name == country.name);
    //console.log(country);
    //console.log(country2);
    //console.log(countries);
    if (country2 != null || country.rating<=0) { }
    else {
        fetch(uri, {
            method: 'POST',
            headers: {

                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(country)
        })
            .then(response => response.json())
            .then(() => {
                getCountries();
                addNameTextbox.value = '';
                addRatingTextbox.value = '';
            })
            .catch(error => console.error('Unable to add country.', error));
    }
}
function deleteCountry(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCountries())
        .catch(error => console.error('Unable to delete country.', error));
}
function displayEditForm(id) {
    const country = countries.find(c => c.id === id);
    document.getElementById('edit-id').value = country.id;
    document.getElementById('edit-name').value = country.name;
    document.getElementById('edit-rating').value = country.rating;
    document.getElementById('editForm').style.display = 'block';
}
function updateCountry() {
    const countryId = document.getElementById('edit-id').value;
    const country = {
        id: parseInt(countryId, 10),
        name: document.getElementById('edit-name').value.trim(),
        rating: document.getElementById('edit-rating').value.trim()
    };
    const country2 = countries.find(c => c.name == country.name);
    if (country2 != null || country.rating <= 0) { closeInput(); }
    else {
        fetch(`${uri}/${countryId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(country)
        })
            .then(() => getCountries())
            .catch(error => console.error('Unable to update country.', error));
        closeInput();
        return false;
    }
}
function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}
function _displayCountries(data) {
    const tBody = document.getElementById('countries');

    tBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(country => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${country.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCountry(${country.id})`);
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(country.name);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(country.rating);
        td2.appendChild(textNodeInfo);
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    countries = data;
}