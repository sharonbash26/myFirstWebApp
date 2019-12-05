// global module 'utils'
var utils = utils || {};

// executes the function 'fn', 'freq' times per second, for 'duration' seconds.
const performIntervalFor = (fn, freq, duration) => {
    return new Promise((resolve) => {
        const executeEach = 1000 / freq;
        let ntimes = (1000 * duration) / executeEach;
        const interval = setInterval(() => {
            try { fn(); } catch (e) { }
            if (--ntimes <= 0) {
                clearInterval(interval);
                resolve();
            }
        }, executeEach);
    });
}

utils.performIntervalFor = utils.performIntervalFor || performIntervalFor;