class MapDrawer {
    constructor(drawModule) {
        this.drawModule = drawModule;
        this._locationToPoint = this._locationToPoint.bind(this);
    }

    drawPath(locations) {
        const points = locations.map(this._locationToPoint);
        this.drawModule.clearCanvas();
        this.drawModule.strokeColor = 'red';
        this.drawModule.lineWidth = 2;
        for (let i = 0; i < points.length - 1; ++i) {
            const src = points[i];
            const dst = points[i + 1];
            this.drawModule.drawLine(src, dst);
        }

        this.drawModule.fillColor = 'red';
        this.drawModule.strokeColor = 'blue';
        this.drawModule.lineWidth = 1;
        this.drawModule.drawCircle(points[points.length - 1], 10, 'red', 'blue');
    }

    // from lon,lat to coordinates on the canvas
    // upper left   => bottom right
    // (0,0)        => (1366,768)       // screen, e.g
    // (-180, -90)  => (180, 90)        // lon,lat
    _locationToPoint(loc) {
        const coords = {};
        coords.x = (this.drawModule.width * (loc.Lon + 180)) / 360;
        coords.y = (this.drawModule.height * (loc.Lat + 90)) / 180;
        return coords;
    }
}