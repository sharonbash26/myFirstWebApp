

class DrawModule {
    constructor(canvasId) {
        this.canvas = document.getElementById(canvasId);
        this.ctx = this.canvas.getContext("2d");
        this.fitCanvas();
    }

    get width() {
        return this.canvas.width;
    }

    get height() {
        return this.canvas.height;
    }

    fitCanvas() {
        this.canvas.width = document.body.clientWidth;
        this.canvas.height = document.body.clientHeight;
    }

    clearCanvas() {
        this.ctx.clearRect(0, 0, this.width, this.height);
    }

    drawCircle(center, radius) {
        this.ctx.beginPath();
        this.ctx.arc(center.x + radius / 2, center.y + radius / 2, radius, 0, 2 * Math.PI);
        this.ctx.fill();
        this.ctx.stroke();
    }

    drawLine(src, dst) {
        this.ctx.beginPath();
        this.ctx.moveTo(src.x, src.y);
        this.ctx.lineTo(dst.x, dst.y);
        this.ctx.stroke();
    }

    set fillColor(fillColor) {
        this.ctx.fillStyle = fillColor;
    }

    set strokeColor(strokeColor) {
        this.ctx.strokeStyle = strokeColor;
    }

    set lineWidth(lineWidth) {
        this.ctx.lineWidth = lineWidth;
    }

}



