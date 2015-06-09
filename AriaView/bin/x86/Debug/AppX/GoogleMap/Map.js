var map,
    north,
    east,
    south,
    west,
    imageURL,
    centerlat,
    centerlng,
    fogImageOverlay




callMapViewMethod("SetScriptVariables");

function initialize() {


    var options = {
        zoom: 0,
        center: { lat: centerlat, lng: centerlng },
        streetViewControl: false
    };
    map = new google.maps.Map(document.getElementById('mapwindow'), options);
   
    var imageBounds = new google.maps.LatLngBounds(
     new google.maps.LatLng(south, west),
     new google.maps.LatLng(north, east));
    fogImageOverlay = new google.maps.GroundOverlay(
         imageURL,
         imageBounds
         );
    fogImageOverlay.setMap(map);
    map.setZoom(11);
}

google.maps.event.addDomListener(window, 'load', initialize);


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

function callMapViewMethod(methodName) {
    window.external.notify(methodName);
}



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
    map.setCenter(new google.maps.LatLng(centerlat, centerlng));
}


//google.maps.event.addListener(map, 'click', function (event) {
//    placeMarker(event.latLng);
//});

//function placeMarker(location) {
//    var marker = new google.maps.Marker({
//        position: location,
//        map: map
//    });
//}