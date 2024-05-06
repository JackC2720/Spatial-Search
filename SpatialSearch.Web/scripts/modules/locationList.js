export const CreateList = (locationData) => {
    const listContainer = document.getElementById("listResultsContainer");
    const list = document.getElementById("resultsList");
    locationData.forEach((item) => {
        var listItem = CreateListItem(item.postcode, item.distance);
        list.appendChild(listItem);
    });
    listContainer.style.display = "block";
};

const CreateListItem = (postcode, distance) => {
    var listItem  = document.createElement("li");
    listItem.classList.add("postcode-results");
    var text = `${postcode}</br>Distance: ${distance}km`;
    var p = document.createElement("p");
    p.innerHTML = text;
    listItem.appendChild(p);
    return listItem;
};