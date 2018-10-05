
/*...............Weather Widget ..............................*/

var icons = new Skycons({"color": "#757575"}),
    list  = [
        "partly-cloudy-night"
    ],
    i;

for(i = list.length; i--; )
    icons.set(list[i], list[i]);
icons.play();

var icons = new Skycons({"color": "#999"}),
    list  = [
        "clear-day","sleet","snow","fog","wind","partly-cloudy-day"
    ],
    i;

for(i = list.length; i--; )
    icons.set(list[i], list[i]);
icons.play();


var icons = new Skycons({"color": "#fff"}),
    list  = [
        "partly_cloudy_day_2","snow_2","rain_2"

    ],
    i;

for(i = list.length; i--; )
    icons.set(list[i], list[i]);

icons.play();
