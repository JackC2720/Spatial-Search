import L from "leaflet";

export const CreateMap = (locationData) => {
    var search = locationData.pop();
    const mapContainer = document.getElementById("mapContainer");
    //Init map
    const map = L.map('map', {
        center: L.latLng(search.lat, search.long),
        zoom: 13,
    });
    //Add map ref
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    ////Add circle bounds
    var circle = L.circle([search.lat, search.long], {
        color: 'red',
        fillOpacity: 0,
        radius: search.distance * 1000,
    }).addTo(map);

    //Set zoom to the bounds of the circle
    map.fitBounds(circle.getBounds());

    locationData.forEach((item) => {
        L.marker([item.lat, item.long]).addTo(map).bindPopup(item.postcode);
    }); 
}