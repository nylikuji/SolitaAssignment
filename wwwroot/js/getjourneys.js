const uri = "journeys";
const journeytablecolumns = document.getElementById("journeytable").innerHTML;

function getjourneys() {
    let pageid = document.getElementById("pageid").value.trim();
    console.log(pageid);
    fetch(uri + "/?pageid=" + pageid, {
        method: 'GET',
        headers: {

        }
    })
    .then((response) => response.text())
    .then((text) => {
        document.getElementById("journeytable").innerHTML = journeytablecolumns + text;
    });
}
