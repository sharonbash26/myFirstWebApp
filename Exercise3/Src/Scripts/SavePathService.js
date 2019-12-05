
function savePlaneRoute(locations, actionURL, filename) {
    const url = `${actionURL}?filename=${filename}`
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(locations)
    });
}