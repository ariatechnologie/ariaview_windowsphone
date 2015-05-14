var map
var kmlUrl = "http://web.aria.fr/awa/destination/ARIAVIEW/JUAREZ/GEARTH/RESULT_LcS/20130223/20130223.kml";


function initialize() {

    //window.external.notify("initialize");
    var maProp = {
        zoom: 6,
        center: new google.maps.LatLng(50, 50),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById('mapwindow'), maProp);

    //google.maps.event.addListener(map, 'click', function (event) {
    //    placeMarker(event.latLng);
    //});

    //function placeMarker(location) {
    //    var marker = new google.maps.Marker({
    //        position: location,
    //        map: map
    //    });
    //}

    //var kmlOptions = {
    //    suppressInfoWindows: true,
    //    preserveViewport: false,
    //    map: map
    //};
    //var kmlLayer = new google.maps.KmlLayer(kmlUrl);
    //kmlLayer.setMap(map);
}

google.maps.event.addDomListener(window, 'load', initialize);


function changeCenter(lat,long)
{
    var position = new google.maps.LatLng(lat, long);
    map.setCenter(Position);
    //window.external.notify(value);
}