$(document).ready(function () {
    drawMap("../api/maps/getmap?name1=Ethiopia&name2=&name3=");
});

function drawMap(address) {
    //Map dimensions (in pixels)
    var width = 800, height = 600;

    //Map projection
    var projection = d3.geo.mercator()
        .scale(2177.82640586829)
        .center([40.49310346888979, 9.158131730684925]) //projection center
        .translate([width / 2, height / 2]) //translate to center the map in view

    //Generate paths based on projection
    var path = d3.geo.path()
        .projection(projection);

    //Create an SVG
    var svg = d3.select("#maparea").select("svg").remove();
    var svg = d3.select("#maparea").append("svg").attr("width", width).attr("height", height);

    //Group for the map features
    var features = svg.append("g").attr("class", "features");

    //Create zoom/pan listener
    //Change [1,Infinity] to adjust the min/max zoom scale
    var zoom = d3.behavior.zoom().scaleExtent([1, Infinity]).on("zoom", zoomed);

    svg.call(zoom);

    d3.json(address, function (error, result) {
        if (error) return console.log(error); //unknown error, check the console

        //Create a path for each map feature in the data
        features.selectAll("path")
          .data(result.features)
          .enter()
          .append("path")
          .attr("d", path)
          .on("click", clicked);

        // Center the graph
        var bBox = svg.node().getBBox();

        var padding = 2,
          hRatio = height / (bBox.height + padding),
          wRatio = width / (bBox.width + padding),
          scale = hRatio < wRatio ? hRatio : wRatio;

        svg.transition().duration(2000)
          .call(zoom.translate([(width - scale * bBox.width) / 2 - scale * bBox.x, (height - scale * bBox.height) / 2 - scale * bBox.y])
          .scale(scale)
          .event);
        //zoom.translate([(width - scale * bBox.width) / 2 - scale * bBox.x, (height - scale * bBox.height) / 2 - scale * bBox.y])
        //  .scale(scale)
        //  .event(svg);
    });

    // Add optional onClick events for features here
    // d.properties contains the attributes (e.g. d.properties.name, d.properties.population)
    function clicked(d, i) {
        if (d.properties.NAME_3) {
            window.location.href = '/places/GetPlaceByName/' + d.properties.NAME_3;
        }
        else {
            // Center the graph
            var scale = 1,
                bBox = svg.node().getBBox();
            // Zoom back
            svg.transition().duration(1000)
              .call(zoom.translate([(width - scale * bBox.width) / 2 - scale * bBox.x, (height - scale * bBox.height) / 2 - scale * bBox.y])
              .scale(scale)
              .event).each("end", function () {
                  // Drill down on the map
                  drawMap("../api/maps/getmap?name1=" + d.properties.NAME_0 + "&name2=" + d.properties.NAME_1 + "&name3=" + d.properties.NAME_2);
              });
        }

    }

    //Update map on zoom/pan
    function zoomed() {
        features.attr("transform", "translate(" + zoom.translate() + ")scale(" + zoom.scale() + ")")
            .selectAll("path").style("stroke-width", 1 / zoom.scale() + "px");
    }
}

