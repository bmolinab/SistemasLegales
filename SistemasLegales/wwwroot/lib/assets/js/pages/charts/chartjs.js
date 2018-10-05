$(function () {
    new Chart(document.getElementById("line_chart").getContext("2d"), getChartJs('line'));
    new Chart(document.getElementById("bar_chart").getContext("2d"), getChartJs('bar'));
    new Chart(document.getElementById("radar_chart").getContext("2d"), getChartJs('radar'));
    new Chart(document.getElementById("pie_chart").getContext("2d"), getChartJs('pie'));
});

function getChartJs(type) {
    var config = null;

    if (type === 'line') {
        config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [34, 22, 41, 99, 76, 25, 44],
                    borderColor: 'rgba(0, 188, 212, 0.75)',
                    backgroundColor: 'rgba(0, 188, 212, 0.3)',
                    pointBorderColor: 'rgba(0, 188, 212, 1)',
                    pointBackgroundColor: 'rgba(0, 188, 212, 0.9)',
                    pointBorderWidth: 1
                }, {
                        label: "My Second dataset",
                        data: [45, 66, 51, 49, 89, 45, 15],
                        borderColor: 'rgba(205, 220, 57, 0.75)',
                        backgroundColor: 'rgba(205, 220, 57, 0.3)',
                        pointBorderColor: 'rgba(205, 220, 57, 1)',
                        pointBackgroundColor: 'rgba(205, 220, 57, 0.9)',
                        pointBorderWidth: 1
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'bar') {
        config = {
            type: 'bar',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [23, 88, 45, 65, 77, 44, 55],
                    backgroundColor: 'rgba(0, 188, 212, 1)'
                }, {
                        label: "My Second dataset",
                        data: [45, 66, 33, 45, 89, 20, 77],
                        backgroundColor: 'rgba(205, 220, 57, 1)'
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'radar') {
        config = {
            type: 'radar',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [23, 66, 77, 100, 56, 55, 40],
                    borderColor: 'rgba(0, 188, 212, 0.75)',
                    backgroundColor: 'rgba(0, 188, 212, 0.3)',
                    pointBorderColor: 'rgba(0, 188, 212, 1)',
                    pointBackgroundColor: 'rgba(0, 188, 212, 0.9)',
                    pointBorderWidth: 1
                }, {
                        label: "My Second dataset",
                        data: [22, 55, 77, 19, 65, 45, 100],
                        borderColor: 'rgba(205, 220, 57, 0.75)',
                        backgroundColor: 'rgba(205, 220, 57, 0.3)',
                        pointBorderColor: 'rgba(205, 220, 57, 1)',
                        pointBackgroundColor: 'rgba(205, 220, 57, 0.9)',
                        pointBorderWidth: 1
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'pie') {
        config = {
            type: 'pie',
            data: {
                datasets: [{
                    data: [130, 99, 45, 60],
                    backgroundColor: [
                        "rgb(205, 220, 57)",
                        "rgb(244, 67, 54)",
                        "rgb(0, 188, 212)",
                        "rgb(76, 175, 80)"
                    ]
                }],
                labels: [
                    "Lime",
                    "Red",
                    "Cyan",
                    "Green"
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    return config;
}