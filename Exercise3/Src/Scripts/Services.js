
class Services {
    savePlaneRoute(locations, filename, actionURL) {
        const url = `${actionURL}?filename=${filename}`
        return fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(locations)
        });
    }

    getLocation(ip, port, actionURL) {
        return new Promise((resolve, reject) => {
            const url = `${actionURL}?ip=${ip}&port=${port}`;
            fetch(url)
                .then(res => res.json())
                .then(resolve)
                .catch(err => {
                    console.log('err');
                    return reject(err);
                });
        });
    }

    getPlaneRoute(filename, actionURL) {
        return new Promise((resolve, reject) => {
            const url = `${actionURL}?filename=${filename}`;
            // url = '/Home/GetPlaneRoute?filename=thefile'
            fetch(url)
                .then(res => res.json())
                .then(resolve)
                .catch(err => {
                    console.log('err');
                    return reject(err);
                });
        });
    }
}