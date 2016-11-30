// Random number generator between two numbers
function getRandomDecimal(min, max) {
    return Math.random() * (max - min) + min;
}

// Random integer generator between two numbers
function getRandomInteger(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}

// Circle object
//  parameters
//      x and y are the initial positions
//      rad is the radius
//      vx and vy are the component speeds
function Circle(x, y, rad, vx, vy) {
    var _this = this;

    // constructor
    (function () {
        _this.x = x || null;
        _this.y = y || null;
        _this.radius = rad || null;
        _this.vx = vx || null;
        _this.vy = vy || null;
    })();

    // update function, this will make the circles appear like they are moving.
    this.update = function () {
        _this.x += _this.vx;
        _this.y += _this.vy;

        // these if statments below are what cause the cirlce to appear on the other side of the simulation when they seem to go off screen.
        if (_this.x > canvas.width + _this.radius) {
            _this.x = 0 + _this.radius;
        }
        if (_this.y > canvas.height + _this.radius) {
            _this.y = 0 + _this.radius;
        }
        if (_this.x < 0 + this.radius) {
            _this.x = canvas.width - _this.radius;
        }
        if (_this.y < 0 + _this.radius) {
            _this.y = canvas.height - _this.radius;
        }
    }

    // this method will allow each circle to request the browser to draw it on the screen.
    this.draw = function (ctx, color) {
        if (!_this.x || !_this.y || !_this.radius || !_this.vx || !_this.vy) {
            console.error('Circle requires an x, y, radius, vx and vy');
            return;
        }
        ctx.beginPath();
        ctx.arc(_this.x, _this.y, _this.radius, 0, 2 * Math.PI, false);
        ctx.fillStyle = color;
        ctx.fill();
        // Build an array of all other circles that are within 25px of the current circles and draw a line between that circle and this one
        var neighbors = [];
        var yratio = (canvas.height / canvas.width) / 3;
        var xratio = (canvas.width / canvas.height) * 8;
        for (var i = 0; i < circles.length; i++) {
            // this is what filters what circles are considered neighbors and which ones are not relative to the current cirlce being rendered in frame.
            if (Math.abs(circles[i].x - _this.x) < canvas.height * yratio && Math.abs(circles[i].y - _this.y) < canvas.width / xratio && circles[i].x - _this.x != 0 && circles[i].y - _this.y != 0) {
                neighbors.push(circles[i]);
            }
        }
        // for each neighbor to this cirlce being rendered draw a line.
        for (var i = 0; i < neighbors.length; i++) {
            ctx.moveTo(_this.x, _this.y);
            ctx.lineTo(neighbors[i].x, neighbors[i].y);
            ctx.strokeStyle = color;
            ctx.stroke();
        }
    };
}

// animation loop
function loop() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    for (var i = 0; i < circles.length; i++) {
        circles[i].update();
        circles[i].draw(ctx, "#b3d9ff");
    }
    requestAnimationFrame(loop);
}

function ReloadCanvasAndProjectName(nameID) {
    var newW = window.innerWidth

    if (newW > canvas.width) {
        canvas.width = window.innerWidth;
    }
    
    canvas.height = 400;
    document.getElementById(nameID).style.top = canvas.height * .6 + 'px';
    document.getElementById(nameID).style.left = newW * .5 - ($("#" + nameID).width() / 2) + 'px';
}