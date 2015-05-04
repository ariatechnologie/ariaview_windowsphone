var map


function Initialize() {
    map = new google.maps.Map(document.getElementById('mapwindow'), {
        zoom: 3,
        center: new google.maps.LatLng(40, -187.3),
        mapTypeId: google.maps.MapTypeId.TERRAIN
    });
}