const journeyuri = "journeys";
const journeytablecolumns = document.getElementById("journeytable").innerHTML;
let journeysortby = "departuretime ASC";
let journeyascordesc = "ASC";

function getjourneys() {
    let pageid = document.getElementById("journeypageid").value.trim();
    fetch(journeyuri + "?pageid=" + pageid + "&sortby=" + journeysortby, {
        method: 'GET',
        headers: {

        }
    })
    .then((response) => response.text())
    .then((text) => {
        document.getElementById("journeytable").innerHTML = journeytablecolumns + text;
    });
    document.getElementById("journeysortingby").innerHTML = "sorting by " + journeysortby;
}

function sortjourneys(str) {
    if (journeyascordesc == "ASC") {
        journeyascordesc = "DESC";
    }
    else {
        journeyascordesc = "ASC";
    }
    journeysortby = str + " " + journeyascordesc;
    getjourneys();
}