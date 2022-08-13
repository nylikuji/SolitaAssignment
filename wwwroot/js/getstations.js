const stationuri = "stations";
const stationtablecolumns = document.getElementById("stationtable").innerHTML;
let stationsortby = "fid ASC";
let stationascordesc = "ASC";

function getstations() {
    let pageid = document.getElementById("stationpageid").value.trim();
    fetch(stationuri + "?pageid=" + pageid + "&sortby=" + stationsortby + " " + stationascordesc, {
        method: 'GET',
        headers: {

        }
    })
    .then((response) => response.text())
    .then((text) => {
        document.getElementById("stationtable").innerHTML = stationtablecolumns + text;
        
        if (stationascordesc == "ASC") {
            document.getElementById(stationsortby).innerHTML += "⯅";
        }
        else {
            document.getElementById(stationsortby).innerHTML += "⯆";
        }

    });
}

function sortstations(str) {
    if (stationascordesc == "ASC") {
        stationascordesc = "DESC";
    }
    else {
        stationascordesc = "ASC";
    }
    stationsortby = str;
    getstations();
}
