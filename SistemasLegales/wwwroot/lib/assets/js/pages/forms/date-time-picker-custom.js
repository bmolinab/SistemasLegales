//    Material Date Picker plugin


$(function () {
    $('#date').bootstrapMaterialDatePicker({ weekStart : 0, time: false });

    $('#time').bootstrapMaterialDatePicker({ date: false });
    $('#date-format').bootstrapMaterialDatePicker({ format : 'dddd DD MMMM YYYY - HH:mm' });
    $('#min-date').bootstrapMaterialDatePicker({ format : 'DD/MM/YYYY HH:mm', minDate : new Date() });

    //events
    $('#date-end').bootstrapMaterialDatePicker({ weekStart : 0 });
    $('#date-start').bootstrapMaterialDatePicker({ weekStart : 0 }).on('change', function(e, date)
    {
        $('#date-end').bootstrapMaterialDatePicker('setMinDate', date);
    });
});



//    Bootstrap Date Picker plugin


$('.datepicker').datepicker()

$('#date-picker-startend').datepicker({
    startDate: "07/17/2017",
    endDate: "07/25/2017"
});

$('#date-disabled').datepicker({
    daysOfWeekDisabled: "1,5"
});

$('#date-range').datepicker({});

$('#date-range-disable').datepicker({
    startDate: "07/17/2017",
    endDate: "07/25/2017"
});



//    Bootstrap DateTimePicker plugin


$(".form_datetime").datetimepicker({
    format: 'yyyy-mm-dd hh:ii',
    fontAwesome: !0,
    autoclose: true,
    pickerPosition: "bottom-left"

});
$(".form_advance_datetime").datetimepicker({
    format: "dd MM yyyy - hh:ii",
    autoclose: true,
    fontAwesome: !0,
    todayBtn: true,
    startDate: "2013-02-14 10:00",
    minuteStep: 10,
    pickerPosition: "bottom-left"
});
$(".form_meridian_datetime").datetimepicker({
    format: "dd MM yyyy - HH:ii P",
    showMeridian: true,
    autoclose: true,
    fontAwesome: !0,
    todayBtn: true,
    pickerPosition: "bottom-left"
});


//    Bootstrap TimePicker plugin
$('#timepicker-second').timepicker({
    autoclose: !0,
    showSeconds: !0,
    minuteStep: 1
});
$('#timepicker1').timepicker({
    autoclose: !0,
    minuteStep: 5,
    defaultTime: !1
});
$('#timepicker-24hour').timepicker({
    autoclose: !0,
    minuteStep: 5,
    showSeconds: !1,
    showMeridian: !1
});


//    Bootstrap Date Range plugin

$(function() {
    $('.example-date').daterangepicker({
        buttonClasses: ['btn', 'btn-sm'],
        applyClass: 'btn-info',
        cancelClass: 'btn-default'
    });
    $('.example-time').daterangepicker({
        timePicker: true,
        format: 'MM/DD/YYYY h:mm A',
        timePickerIncrement: 30,
        timePicker12Hour: true,
        timePickerSeconds: false,
        buttonClasses: ['btn', 'btn-sm'],
        applyClass: 'btn-info',
        cancelClass: 'btn-default'
    });

    var start = moment().subtract(29, 'days');
    var end = moment();

    function cb(start, end) {
        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        buttonClasses: ['btn', 'btn-sm'],
        applyClass: 'btn-info',
        cancelClass: 'btn-default'
    }, cb);

    cb(start, end);

});


// Clock Face plugin

$(function(){
    $('#t1').clockface();
    $('#t2').clockface({
        format: 'HH:mm',
        trigger: 'manual'
    });

    $('#t2_toggle').click(function(e){
        e.stopPropagation();
        $('#t2').clockface('toggle');
    });
    $('#t3').clockface({
        format: 'H:mm'
    }).clockface('show', '14:30');
});


// Clock Picker plugin


$('.clockpicker').clockpicker({
    donetext: 'Done'
});