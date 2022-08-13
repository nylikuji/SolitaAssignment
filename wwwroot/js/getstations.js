const stationuri = "stations";
const stationtablecolumns = document.getElementById("stationtable").innerHTML;
let stationsortby = "fid ASC";
let stationascordesc = "ASC";

function getstations() {
    let pageid = document.getElementById("stationpageid").value.trim();
    fetch(stationuri + "?pageid=" + pageid + "&sortby=" + stationsortby, {
        method: 'GET',
        headers: {

        }
    })
    .then((response) => response.text())
    .then((text) => {
        document.getElementById("stationtable").innerHTML = stationtablecolumns + text;
    });
    document.getElementById("stationsortingby").innerHTML = "sorting by " + stationsortby;
}

function sortstations(str) {
    if (stationascordesc == "ASC") {
        stationascordesc = "DESC";
    }
    else {
        stationascordesc = "ASC";
    }
    stationsortby = str + " " + stationascordesc;
    getstations();
}
