import { addLocationToDatabase, addRandomLocationsToDatabase, getLocationData } from './postcodeApi';
import { CreateMap } from './locationMap';
import { CreateList } from './locationList';

const initialiseModule = () => {
    //Add postcode to DB form
    const AddPostcodeFormSubmit = document.getElementById("AddPostcodeSubmit");
    AddPostcodeFormSubmit.addEventListener("click", (b) => {
        b.preventDefault();
        const postcode = document.getElementById("postcodeField").value;
        addLocationToDatabase(postcode)
            .then((responseString) => {
                const messageContainer = document.getElementById("addPostcodeMessage");
                messageContainer.innerHTML = responseString.substring(1, responseString.length - 1);
            });
    });

    //Search postcode form
    const searchPostcodeFormSubmit = document.getElementById("searchSubmit");
    searchPostcodeFormSubmit.addEventListener("click", (b) => {
        b.preventDefault();
        const postcode = document.getElementById("postcodeSearchField").value;
        const distance = document.getElementById("distanceSearchField").value;
        getLocationData(postcode, distance)
            .then((locationData) => {
                CreateMap(locationData);
                CreateList(locationData);
                
            });
    });
    //Add random postcodes to DB form
    const AddRandomPostcodesFormSubmit = document.getElementById("AddRandomPostcodeSubmit");
    AddRandomPostcodesFormSubmit.addEventListener("click", (b) => {
        b.preventDefault();
        const postcode = document.getElementById("numberOfPostcodes").value;
        addRandomLocationsToDatabase(postcode)
            .then((responseString) => {
                const messageContainer = document.getElementById("addRandomPostcodeMessage");
                messageContainer.innerHTML = responseString.substring(1, responseString.length - 1);
            });
    });
};

export default initialiseModule;