$(function () {
    $(".sparkline-line").each(function () {
        var $this = $(this);
        $this.sparkline('html', {  type: 'line', width: '100%', height: '150px ', lineColor: 'rgba(3, 169, 244, 0.7)', fillColor: 'rgba(3, 169, 244, 0.1)', spotColor: 'rgba(3, 169, 244, 0.7)',
            minSpotColor: 'rgba(3, 169, 244, 0.7)', maxSpotColor: 'rgba(3, 169, 244, 0.7)', highlightSpotColor: 'rgba(0, 188, 212, 0.7)', highlightLineColor: '(3, 169, 244, 0.5)', spotRadius: '3'});
    });

    $(".sparkline-bar").each(function () {
        var $this = $(this);
        $this.sparkline('html', { type: 'bar', barColor: '#03A9F4', height: '150px', barWidth: 16, barSpacing: 10});
    });

    $(".sparkline-Compositebar").each(function () {
        var $this = $(this);
        $this.sparkline('html', { type: 'bar', barColor: '#03A9F4', height: '150px', barWidth: 16, barSpacing: 10});
        $this.sparkline([4, 5, 6, 9, 12, 10, 13, 12, 9, 11, 16, 10, 15, 10, 7, 18, 12, 8],
            { composite: true, fillColor: false, lineColor: 'orange'});
    });

    $('.sparkline-pie').sparkline('html', {
        type: 'pie',
        offset: 90,
        width: '150px',
        height: '150px',
        sliceColors: ['#2196F3', '#FF9800', '#F44336','#CDDC39']
    })

    drawDocSparklines();
    drawMouseSpeedDemo();
});

//Taken from http://omnipotent.net/jquery.sparkline ================
function drawDocSparklines() {

    // Bar + line composite charts
    $('#compositebar').sparkline('html', { type: 'bar', barColor: '#03A9F4', height: '35px', barWidth: '9', barSpacing: '7'});
    $('#compositebar').sparkline([4, 1, 5, 7, 9, 9, 8, 7, 6, 6, 4, 7, 8, 4, 3, 2, 2, 5, 6, 7],
        { composite: true, fillColor: false, lineColor: 'orange'});


    // Line charts taking their values from the tag
    $('.sparkline-1').sparkline('html',
        {  type: 'line', height: '2.5em', width: '10em', lineColor: 'rgba(3, 169, 244, 0.7)', fillColor: 'rgba(3, 169, 244, 0.1)'});

    // Larger line charts for the docs
    $('.largeline').sparkline('html',
        { type: 'line', height: '2.5em', width: '4em' });

    // Customized line chart
    $('#linecustom').sparkline('html',
        {
            height: '2.5em', width: '10em', lineColor: 'rgba(3, 169, 244, 0.7)', fillColor: 'rgba(3, 169, 244, 0.1)',
            minSpotColor: false, maxSpotColor: false, spotColor: '#77f', spotRadius: 3
        });

    // Bar charts using inline values
    $('.sparkbar').sparkline('html', { type: 'bar', height: '25px', barWidth: '4', barSpacing: '3'});
    $('.sparkbar-demo').sparkline('html', { type: 'bar', height: '35px', barWidth: '9', barSpacing: '7'});

    $('.barformat').sparkline([1, 3, 5, 3, 8], {
        type: 'bar',
        tooltipFormat: '{{value:levels}} - {{value}}',
        tooltipValueLookups: {
            levels: $.range_map({ ':2': 'Low', '3:6': 'Medium', '7:': 'High' })
        }
    });

    // Tri-state charts using inline values
    $('.sparktristate').sparkline('html', { type: 'tristate', height: '3.0em', barWidth: '10', barSpacing: '2',
        posBarColor: '#3030db', negBarColor: '#ed3838', zeroBarColor: '#cccccc'});
    $('.sparktristatecols').sparkline('html',
        { type: 'tristate', height: '3.0em', barWidth: '10', barSpacing: '2', colorMap: { '-2': '#ed3838', '2': '#3030db' } });

    // Composite line charts, the second using values supplied via javascript
    $('#compositeline').sparkline('html', { fillColor: false, changeRangeMin: 0, chartRangeMax: 10, height: '2.5em', width: '10em' });
    $('#compositeline').sparkline([4, 1, 5, 7, 9, 9, 8, 7, 6, 6, 4, 7, 8, 4, 3, 2, 2, 5, 6, 7],
        { composite: true, fillColor: false, lineColor: 'red', changeRangeMin: 0, chartRangeMax: 10 });

    // Line charts with normal range marker
    $('#normalline').sparkline('html',
        { fillColor: false, normalRangeMin: -1, normalRangeMax: 8, height: '3.0em', width: '10em' });
    $('#normalExample').sparkline('html',
        { fillColor: false, normalRangeMin: 80, normalRangeMax: 95, normalRangeColor: '#4f4', height: '2.5em', width: '12em'});

    // Discrete charts
    $('.discrete1').sparkline('html',
        { type: 'discrete', lineColor: 'blue', xwidth: 18, height: '2.5em'});
    $('#discrete2').sparkline('html',
        { type: 'discrete', height: '2.5em', lineColor: 'blue', thresholdColor: 'red', thresholdValue: 4 });

    // Bullet charts
    $('.sparkbullet').sparkline('html', { type: 'bullet', height: '1.2em', width: '4em' });

    // Pie charts
    $('.sparkpie').sparkline('html', { type: 'pie', height: '3.0em' });

    // Box plots
    $('.sparkboxplot').sparkline('html', { type: 'box', height: '1.3em', width: '5em', boxFillColor: '#d0bee2' });
    $('.sparkboxplotraw').sparkline([1, 3, 5, 8, 10, 15, 18],
        { type: 'box', height: '1.3em', boxFillColor: '#d0bee2', width: '5em', raw: true, showOutliers: true, target: 6 });

    // Box plot with specific field order
    $('.boxfieldorder').sparkline('html', {
        type: 'box',
        tooltipFormatFieldlist: ['med', 'lq', 'uq'],
        tooltipFormatFieldlistKey: 'field'
    });

    // click event demo sparkline
    $('.clickdemo').sparkline();
    $('.clickdemo').bind('sparklineClick', function (ev) {
        var sparkline = ev.sparklines[0],
            region = sparkline.getCurrentRegionFields();
        value = region.y;
        alert("Clicked on x=" + region.x + " y=" + region.y);
    });

    // mouseover event demo sparkline
    $('.mouseoverdemo').sparkline();
    $('.mouseoverdemo').bind('sparklineRegionChange', function (ev) {
        var sparkline = ev.sparklines[0],
            region = sparkline.getCurrentRegionFields();
        value = region.y;
        $('.mouseoverregion').text("x=" + region.x + " y=" + region.y);
    }).bind('mouseleave', function () {
        $('.mouseoverregion').text('');
    });
}

/**
 ** Draw the little mouse speed animated graph
 ** This just attaches a handler to the mousemove event to see
 ** (roughly) how far the mouse has moved
 ** and then updates the display a couple of times a second via
 ** setTimeout()
 **/
function drawMouseSpeedDemo() {
    var mrefreshinterval = 500; // update display every 500ms
    var lastmousex = -1;
    var lastmousey = -1;
    var lastmousetime;
    var mousetravel = 0;
    var mpoints = [];
    var mpoints_max = 30;
    $('html').mousemove(function (e) {
        var mousex = e.pageX;
        var mousey = e.pageY;
        if (lastmousex > -1) {
            mousetravel += Math.max(Math.abs(mousex - lastmousex), Math.abs(mousey - lastmousey));
        }
        lastmousex = mousex;
        lastmousey = mousey;
    });
    var mdraw = function () {
        var md = new Date();
        var timenow = md.getTime();
        if (lastmousetime && lastmousetime != timenow) {
            var pps = Math.round(mousetravel / (timenow - lastmousetime) * 1000);
            mpoints.push(pps);
            if (mpoints.length > mpoints_max)
                mpoints.splice(0, 1);
            mousetravel = 0;
            $('#mousespeed').sparkline(mpoints, { width: mpoints.length * 5, height: '2.5em', lineColor: 'rgba(3, 169, 244, 0.7)', fillColor: 'rgba(3, 169, 244, 0.1)', tooltipSuffix: ' pixels per second' });
        }
        lastmousetime = timenow;
        setTimeout(mdraw, mrefreshinterval);
    };
    // We could use setInterval instead, but I prefer to do it this way
    setTimeout(mdraw, mrefreshinterval);
}

//=================================================================