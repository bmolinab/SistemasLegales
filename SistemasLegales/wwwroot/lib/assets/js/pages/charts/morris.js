$(function () {
    getMorris('line', 'line_chart');
    getMorris('bar', 'bar_chart');
    getMorris('area', 'area_chart');
    getMorris('donut', 'donut_chart');
});


function getMorris(type, element) {
    if (type === 'line') {
        Morris.Line({
            element: element,
            data: [{
                'period': '2011 Q3',
                'licensed': 3407,
                'sorned': 660
            }, {
                    'period': '2011 Q2',
                    'licensed': 3351,
                    'sorned': 629
                }, {
                    'period': '2011 Q1',
                    'licensed': 3269,
                    'sorned': 618
                }, {
                    'period': '2010 Q4',
                    'licensed': 3246,
                    'sorned': 661
                }, {
                    'period': '2009 Q4',
                    'licensed': 3171,
                    'sorned': 676
                }, {
                    'period': '2008 Q4',
                    'licensed': 3155,
                    'sorned': 681
                }, {
                    'period': '2007 Q4',
                    'licensed': 3226,
                    'sorned': 620
                }, {
                    'period': '2006 Q4',
                    'licensed': 3245,
                    'sorned': null
                }, {
                    'period': '2005 Q4',
                    'licensed': 3289,
                    'sorned': null
                }],
            xkey: 'period',
            ykeys: ['licensed', 'sorned'],
            labels: ['Licensed', 'Off the road'],
            lineColors: ['#00BCD4', '#CDDC39'],
            hideHover: 'auto',
            lineWidth: 3
        });
    } else if (type === 'bar') {
        Morris.Bar({
            element: element,
            data: [{
                x: '2011 Q1',
                y: 3,
                z: 2,
                a: 3
            }, {
                    x: '2011 Q2',
                    y: 2,
                    z: null,
                    a: 1
                }, {
                    x: '2011 Q3',
                    y: 0,
                    z: 2,
                    a: 4
                }, {
                    x: '2011 Q4',
                    y: 2,
                    z: 4,
                    a: 3
                }],
            xkey: 'x',
            ykeys: ['y', 'z', 'a'],
            labels: ['Y', 'Z', 'A'],
            hideHover: 'auto',
            barColors: ['#CDDC39', '#00BCD4', '#F44336']
        });
    } else if (type === 'area') {
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
            lineColors: ['#00BCD4', '#CDDC39']
        });
    } else if (type === 'donut') {
        Morris.Donut({
            element: element,
            data: [{
                label: 'New',
                value: 25
            }, {
                    label: 'Registered',
                    value: 40
                }, {
                    label: 'Returned',
                    value: 25
                }],
            colors: ['#CDDC39', '#00BCD4', '#F44336'],
            formatter: function (y) {
                return y + '%'
            }
        });
    }
}