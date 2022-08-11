const uri = "journeys";

function getjourneys() {
    fetch(uri, {
        method: 'GET',
        headers: {

        }
    })
        .then((response) => response.text())
        .then((text) => {
            document.getElementById("journeytable").innerHTML += text;
        });
}
