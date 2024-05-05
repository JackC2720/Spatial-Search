import L from "leaflet";

const initialiseModule = () => {

    const CreateMap = (mapData) => {
        //Init map
        const map = L.map('map', {
            center: L.latLng(50.880518, -1.349916),
            zoom: 13,
        });
        //Add map ref
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map);

        //Add circle bounds
        var circle = L.circle([50.880518, -1.349916], {
            color: 'red',
            fillOpacity: 0,
            radius: 10000
        }).addTo(map);
        //Set zoom to the bounds of the circle
        map.fitBounds(circle.getBounds());

        //Create markers
        L.marker([52.5, -1.09]).addTo(map).bindPopup("I am a circle.");
    }

    //Classes
    class MapData {
        constructor(searchPostcode, searchDistance, locations) {
            this.searchPostcode = searchPostcode;
            this.searchDistance = searchDistance;
            this.locations = locations;
        }
    }
    class Location {
        constructor(postcode, lat, lon, distance) {
            this.postcode = postcode;
            this.lat = lat;
            this.lon = lon;
            this.distance = distance;
        }
    }

    //Fetch data from API
    // api fetch method
    getSurvey = async (searchPostcode, searchDistance) => {
        try {
            const response = await fetch("/umbraco/api/postcodes/GetPostcodes", {
                method: "POST",
                headers: {
                    Accept: "application/json",
                    "Content-type": "application/json", // Set content type to JSON, this is in the header of the request so the api knows what type of data it is e.g. json, html, xml
                },
                body: searchPostcode, searchDistance
            });
            if (!response.ok) {
                throw new Error(`Error: ${response}`);
            }
            const data = await response.json(); // this will be the survey response
            const jsonData = JSON.parse(data);
            return jsonData;
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

};

export default initialiseModule;