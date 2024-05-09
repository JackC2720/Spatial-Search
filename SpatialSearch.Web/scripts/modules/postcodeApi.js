
export const getLocationData = async (postcode, distance) => {
    try {
        const response = await fetch("/umbraco/api/Postcodes/GetPostcodeResults", {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                postcode,
                distance
            }),
        });
        if (!response.ok) {
            throw new Error(`API call failed with status: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

export const addLocationToDatabase = async (postcode) => {
    try {
        const response = await fetch("/umbraco/api/Postcodes/AddPostcodeToDatabase", {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                postcode,
            }),
        });
        if (!response.ok) {
            throw new Error(`API call failed with status: ${response.status}`);
        }
        const data = await response.text();
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

export const addRandomLocationsToDatabase = async (number) => {
    try {
        const response = await fetch("/umbraco/api/Postcodes/AddRandomPostcodeToDatabase", {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                number,
            }),
        });
        if (!response.ok) {
            throw new Error(`API call failed with status: ${response.status}`);
        }
        const data = await response.text();
        return data;
    } catch (error) {
        throw new Error(error);
    }
};