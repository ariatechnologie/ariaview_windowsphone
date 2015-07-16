//Author Jérôme Cambray
//Version 1.0


var map,
    north,
    east,
    south,
    west,
    imageURL,
    centerlat,
    centerlng,
    fogImageOverlay,
    pinMode,
    marker;




callMapViewMethod("SetScriptVariables");

function initialize() {

    pinMode = "false"

    var options = {
        zoom: 0,
        center: { lat: centerlat, lng: centerlng },
        disableDefaultUI: true,
        streetViewControl: false,
        zoomControl: true
      
    };
    map = new google.maps.Map(document.getElementById('mapwindow'), options);
   
    var imageBounds = new google.maps.LatLngBounds(
     new google.maps.LatLng(south, west),
     new google.maps.LatLng(north, east));
    fogImageOverlay = new google.maps.GroundOverlay(
         imageURL,
         imageBounds
         );

    google.maps.event.addListener(fogImageOverlay, 'click', function (event) {
        placeMarker(event.latLng);
    });

    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });

    fogImageOverlay.setMap(map);
    map.setZoom(11);
}


google.maps.event.addDomListener(window, 'load', initialize);



function placeMarker(location) {
    if (pinMode == "false")
        return;
    if (marker != null)
        marker.setMap(null);
    marker = new google.maps.Marker({
        position: location,
        map: map
    });
    callMapViewMethod("UnsetPinMode");
}

//init the variables
function setValues(n, e, s, w, imagePath,x,y) {
    imageURL = imagePath;

    if (x.indexOf(",") >= 0) {
        x = x.replace(',', '.');
    }
    if (y.indexOf(",") >= 0) {
        y = y.replace(',', '.');
    }
    if (n.indexOf(",") >= 0) {
        n = n.replace(',', '.');
    }
    if (e.indexOf(",") >= 0) {
        e = e.replace(',', '.');
    }
    if (s.indexOf(",") >= 0) {
        s = s.replace(',', '.');
    }
    if (w.indexOf(",") >= 0) {
        w = w.replace(',', '.');
    }

    north = parseFloat(n);
    east = parseFloat(e);
    south = parseFloat(s);
    west = parseFloat(w);
    centerlat = parseFloat(x);
    centerlng = parseFloat(y);
}

//send a notification that contains the prameter string (methodname)
function callMapViewMethod(methodName) {
    window.external.notify(methodName);
}


//update the groundOveray 
function changeOverlay(image)
{
    fogImageOverlay.setMap(null);
    var imageBounds = new google.maps.LatLngBounds(
    new google.maps.LatLng(south, west),
    new google.maps.LatLng(north, east));
    fogImageOverlay = new google.maps.GroundOverlay(
         image,
         imageBounds
         );
    fogImageOverlay.setMap(map);
    google.maps.event.addListener(fogImageOverlay, 'click', function (event) {
        placeMarker(event.latLng);
    });
    map.setCenter(new google.maps.LatLng(centerlat, centerlng));
}

//update the global variables when the site changes
function changeLocationInfos(n, e, s, w, x, y) {
    if (x.indexOf(",") >= 0) {
        x = x.replace(',', '.');
    }
    if (y.indexOf(",") >= 0) {
        y = y.replace(',', '.');
    }
    if (n.indexOf(",") >= 0) {
        n = n.replace(',', '.');
    }
    if (e.indexOf(",") >= 0) {
        e = e.replace(',', '.');
    }
    if (s.indexOf(",") >= 0) {
        s = s.replace(',', '.');
    }
    if (w.indexOf(",") >= 0) {
        w = w.replace(',', '.');
    }

    north = parseFloat(n);
    east = parseFloat(e);
    south = parseFloat(s);
    west = parseFloat(w);
    centerlat = parseFloat(x);
    centerlng = parseFloat(y);   
}

function setPinMode(value) {
    pinMode = value;
}

//send the marker's position to the webview
function ExtractMarkerData(){
    if (marker == null)
        return;
    var position = "GetExtractionData" + "," + marker.getPosition().lat().toString() + "," + marker.getPosition().lng().toString();
    window.external.notify(position);
}
