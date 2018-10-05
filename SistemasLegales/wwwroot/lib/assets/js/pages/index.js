$(function(){
    $('#slimscroll').slimScroll({
        height: '336px'
    });
});

$(function () {
    initCounters();
    // initMorrisChart();
    initCharts();
    drawDocSparklines();
});

//Widgets count plugin
function initCounters() {
    $('.count-to').countTo();
}
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
// function initMorrisChart() {
//     Morris.Area({
//         element: 'area_chart',
//         data: [
//             { y: '2010', a: 10, b: 20 },
//             { y: '2011', a: 75,  b: 65 },
//             { y: '2012', a: 50,  b: 40 },
//             { y: '2013', a: 100,  b: 120 },
//             { y: '2014', a: 50,  b: 40 },
//             { y: '2015', a: 75,  b: 65 },
//             { y: '2016', a: 100, b: 90 }
//         ],
//         xkey: 'y',
//         ykeys: ['a', 'b'],
//         labels: ['Series A', 'Series B'],
//         pointSize: 2,
//         hideHover: 'auto',
//         lineColors: ['#00BCD4', '#FFEB3B']
//     });

//     Morris.Donut({
//         element: 'chart_donut',
//         data: [{
//             label: 'Type A',
//             value: 25
//         }, {
//             label: 'Type B',
//             value: 40
//         }, {
//             label: 'Type C',
//             value: 10
//         }],
//         colors: ['#FFEB3B', '#00BCD4', '#F44336'],
//         formatter: function (y) {
//             return y + '%'
//         }
//     });
// }

function drawDocSparklines() {
    // Bar charts using inline values
    $('.sparkbar').sparkline('html', { type: 'bar', height: '25px', barWidth: '4', barSpacing: '3'});
}

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