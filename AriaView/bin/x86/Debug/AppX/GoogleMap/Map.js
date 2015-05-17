var map,
    north,
    east,
    south,
    west,
    imageURL




callMapViewMethod("SetScriptVariables");

function initialize() {

    var maProp = {
        zoom: 6,
        center: new google.maps.LatLng(0, 0),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById('mapwindow'), maProp);
    var imageBounds = new google.maps.LatLngBounds(
     new google.maps.LatLng(north, east),
     new google.maps.LatLng(south, west));
    fogImageOverlay = new google.maps.GroundOverlay(
         imageBounds,
         imageURL
         );
    fogImageOverlay.setMap(map);
    //map.setCenter(fogImageOverlayBounds.getCenter());
    //map.setZoom(18);
}

google.maps.event.addDomListener(window, 'load', initialize);


function changeCenter(north,east,south,west,imgUrl)
{
    var NECoordinates = new google.maps.LatLng(north, east);
    var SWCoordinates = new google.maps.LatLng(south,west);
    var bounds = new google.maps.LatLngBounds(SWCoordinates, NECoordinates);
    var overlay = new google.maps.GroundOverlay(
        'https://www.lib.utexas.edu/maps/historical/newark_nj_1922.jpg',
        bounds
        );
    overlay.setMap(map);
    //map.panToBounds(bounds);
    //map.setZoom(10);
}


function setValues(n, e, s, w, imagePath) {
    imageURL = imagePath;
    north = n;
    east = e;
    south = s;
    west = w;
}

function callMapViewMethod(methodName) {
    window.external.notify(methodName);
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