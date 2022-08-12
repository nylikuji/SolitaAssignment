const uri = "journeys";
const journeytablecolumns = document.getElementById("journeytable").innerHTML;
let sortby = "covereddistance";

function getjourneys() {
    let pageid = document.getElementById("pageid").value.trim();
    console.log(pageid);
    fetch(uri + "/?pageid=" + pageid + "&sortby=" + sortby, {
        method: 'GET',
        headers: {

        }
    })
    .then((response) => response.text())
    .then((text) => {
        document.getElementById("journeytable").innerHTML = journeytablecolumns + text;
    });
    document.getElementById("sortingby").innerHTML = "sorting by " + sortby;
}

function sortlist(str) {
    sortby = str;
    getjourneys();
}