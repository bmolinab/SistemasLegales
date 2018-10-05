$(function(){
    $('#slimscroll').slimScroll({
        height: '336px'
    });

    $('.automatic-slider').unslider({
        autoplay: true,
        keys: false,
        arrows: false,
        nav: false
    });
});

$(function () {
    initCharts();
    drawDocSparklines();
});

function initCharts()  {
    $(".sparkline-demo").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });

    //Chart Bar
    $('.chart-bar').sparkline(undefined, {
        type: 'bar',
        barColor: '#fff',
        negBarColor: '#fff',
        barWidth: '4px',
        height: '34px'
    });

    //Chart Pie
    $('.chart-pie').sparkline(undefined, {
        type: 'pie',
        height: '50px',
        sliceColors: ['rgba(255,255,255,0.70)', 'rgba(255,255,255,0.85)', 'rgba(255,255,255,0.95)', 'rgba(255,255,255,1)']
    });
}

function drawDocSparklines() {
    // Bar charts using inline values
    $('.sparkbar').sparkline('html', { type: 'bar', height: '25px', barWidth: '4', barSpacing: '3'});
}

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

$(document).ready(function () {
    // Todo widget add list
    $('#add_todo').bind('keypress', function(e) {
        var len = $('.list-todo li').prevAll().length+1;
        if(e.keyCode==13){
            e.preventDefault();
            $('.add-to-input').before('<li class="list-todo-item">' +
            '<div class="ms-hover">' +
            '<input type="checkbox" class="mark-complete" id="todo'+len+'">' +
            '<label for="todo'+len+'"><span></span>' + $(this).val() + '</label>' +
            '</div>' +
            '</li>');
            $(this).val("");

        }
    });

    // Todo checkboc check event
    $(document).on('change', '.mark-complete', function(){
        if($(this).prop("checked"))
        {
            $(this).closest('.list-todo-item').addClass('completed');
        }
        else
        {
            $(this).closest('.list-todo-item').removeClass('completed');
        }
    });

    // Todo mark all list items
    $('.mark-all').click(function () {
        if(this.checked) { // check select status
            $('input:checkbox').each(function() { //loop through each checkbox
                this.checked = true;  //select all checkboxes with class "checkbox"
                $('input:checkbox').prop('checked', this.checked),$( '.todo_widget .list-todo-item' ).addClass('completed');
            });
        }else{
            $('input:checkbox').each(function() { //loop through each checkbox
                this.checked = false; //deselect all checkboxes with class "checkbox"
                $('input:checkbox').prop('checked', this.checked),$( '.todo_widget .list-todo-item' ).removeClass('completed');
            });
        }
        // $('input:checkbox').prop('checked', this.checked),$( '.todo_widget .list-group-item' ).toggleClass('completed');
    });
});


var icons = new Skycons({"color": "#fff"}),
    list  = [
        "partly_cloudy_day_2","snow_2","rain_2","clear-day","sleet","snow","fog","wind","partly-cloudy-day"

    ],
    i;

for(i = list.length; i--; )
    icons.set(list[i], list[i]);

icons.play();