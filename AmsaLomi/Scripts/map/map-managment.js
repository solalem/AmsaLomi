//$(document).ready(function () {
//    loadRegionalMap();
//});

$(document).ready(function () {
    $.getJSON("api/maps/getmap?level=1&parentname=Ethiopia", function (data) {
        drawMap(data);
    })
});

function drawMap(data) {
    // Animation speed
    var animationSpeed = 300;
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
        fill: "#aaa"
    }

    // If there is no map return
    var pathCount = 0;
    if (data)
        pathCount = data.mapItems.length;
    if (pathCount == 0)
        return;

    // Create Raphael
    var MAP_WIDTH = "100%";
    var MAP_HEIGHT = "100%";

    // Remove previous map
    el = document.getElementById("map-div");
    el.childNodes.forEach(function (el) {
        el.remove();
    });

    // Create new map
    map = Raphael(el, MAP_WIDTH, MAP_HEIGHT);

    //Loop over all of the counties in the JSON file
    data.mapItems.forEach(function (item) {
        //The county's polygon definition 
        var thisPath = map.path(item.path);
        var cap = map.text(0, 0, item.name);
        //and its ID 
        thisPath.id = item.id;
        thisPath.name = item.name;
        thisPath.type = item.type;

        var bbox = thisPath[0].getBBox();
        cap.translate(bbox.x + (bbox.width - cap.getBBox().width) / 2, bbox.y + (bbox.height - cap.getBBox().height) / 2);

        //Give the paths the appearance you want
        thisPath.attr(style);

        //Add event listener for mouse over
        thisPath.mouseover(function (e) {
            thisPath.animate(hoverStyle, animationSpeed);
        }, true);

        //Add event listener for mouse out
        thisPath.mouseout(function (e) {
            thisPath.animate(style, animationSpeed);
        }, true);

        //Add event listener for mouse click
        thisPath.click(function () {
            // Drill down on the map
            $.getJSON("api/maps/getmap?level=" + (data.level + 1) + "&parentname=" + thisPath.name, function (data) {
                drawMap(data);
            })
        }, true);
    });
}

