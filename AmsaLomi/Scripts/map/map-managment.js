//$(document).ready(function () {
//    loadRegionalMap();
//});

$(document).ready(function () {
    $.getJSON("../Content/maps/ethiopia-map.json", function (data) {
        drawMap(data);
    })
});

function drawMap(data) {
    // Animation speed
    var animationSpeed = 200;
    // Normal style
    var style = {
        fill: "#ddd",
        stroke: "#aaa",
        "stroke-width": 1,
        "stroke-linejoin": "round",
        cursor: "pointer"
    };
    // Hover style
    var hoverStyle = {
        fill: "#999"
    }

    // Create Raphael
    var MAP_WIDTH = 1000;
    var MAP_HEIGHT = 1000;

    map = Raphael(document.getElementById("map-div"), MAP_WIDTH, MAP_HEIGHT);

    //Loop over all of the counties in the JSON file
    var pathCount = data.paths.length;
    data.paths.forEach(function (path) {
        //The county's polygon definition is available at data[i][2
        var thisPath = map.path(path.path);
        //and its ID 
        thisPath.id = path.id;
        thisPath.name = path.name;
        thisPath.type = path.type;

        //Give the paths the appearance you want
        thisPath.attr(style);

        //Add event listener for mouse over
        thisPath.mouseover(function (e) {
            var target = e.target;
            //target.animate(hoverStyle, animationSpeed);
            thisPath.attr(hoverStyle);
        }, true);

        //Add event listener for mouse out
        thisPath.mouseout(function (e) {
            var target = e.target;
            //target.animate(style, animationSpeed);
            thisPath.attr(style);
        }, true);

        //Add event listener for mouse click
        thisPath.click(function () {
            // Drill down on the map
            $.getJSON("/maps/" + thisPath.name + "-map.json", function (data) {
                drawMap(data);
            })
        }, true);
    });
}

