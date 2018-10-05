$(function () {
    $(".sparkline-demo").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
});


new Chartist.Bar('#stacked-bar', {
    labels: ['A', 'B', 'C', 'D'],
    series: [
        [800000, 1200000, 1400000, 1300000],
        [200000, 400000, 500000, 300000],
        [100000, 200000, 400000, 600000]
    ]
}, {
    stackBars: true,
    axisY: {
        labelInterpolationFnc: function(value) {
            return (value / 1000) + '';
        }
    }
}).on('draw', function(data) {
        if(data.type === 'bar') {
            data.element.attr({
                style: 'stroke-width: 20px'
            });
        }
    });

$(function () {
    getMorris('area', 'area_chart');
    getMorris('donut', 'chart_donut');
});
function getMorris(type, element) {
   if (type === 'area') {
        Morris.Area({
            element: element,
            data: [
                { y: '2010', a: 10, b: 20 },
                { y: '2011', a: 75,  b: 65 },
                { y: '2012', a: 50,  b: 40 },
                { y: '2013', a: 100,  b: 120 },
                { y: '2014', a: 50,  b: 40 },
                { y: '2015', a: 75,  b: 65 },
                { y: '2016', a: 100, b: 90 }
            ],
            xkey: 'y',
            ykeys: ['a', 'b'],
            labels: ['Series A', 'Series B'],
            pointSize: 2,
            hideHover: 'auto',
            lineColors: ['#00BCD4', '#FFEB3B']
        });
    } else if (type === 'donut') {
        Morris.Donut({
            element: element,
            data: [{
                label: 'Type A',
                value: 25
            }, {
                label: 'Type B',
                value: 40
            }, {
                label: 'Type C',
                value: 10
            }],
            colors: ['#FFEB3B', '#00BCD4', '#F44336'],
            formatter: function (y) {
                return y + '%'
            }
        });
    }
}
