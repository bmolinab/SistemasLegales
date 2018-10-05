$.fn.raty.defaults.path = '../../plugins/raty/images';

$('#default').raty();
$('#score').raty({ score: 3 });
$('#score-callback').raty({
    score: function() {
        return $(this).attr('data-score');
    }
});
$('#number').raty({ number: 10 });
$('#number-callback').raty({
    number: function() {
        return $(this).attr('data-number');
    }
});

$('#numberMax').raty({
    numberMax : 6,
    number    : 100
});

$('#readOnly').raty({ readOnly: true, score: 3 });

$('#readOnly-callback').raty({
    readOnly: function() {
        return 'true becomes readOnly' == 'true becomes readOnly';
    }
});

$('#noRatedMsg').raty({
    readOnly   : true,
    noRatedMsg : "I'am readOnly and I haven't rated yet!"
});
$('#half').raty({
    half  : true,
    score    : 3.26,
    hints : [['bad 1/2', 'bad'], ['poor 1/2', 'poor'], ['regular 1/2', 'regular'], ['good 1/2', 'good'], ['gorgeous 1/2', 'gorgeous']]
});
$('#click').raty({
    click: function(score, evt) {
        alert('ID: ' + this.id + "\nscore: " + score + "\nevent: " + evt.type);
    }
});
$('#cancel').raty({ cancel: true });

$('#cancelHint').raty({
    cancel     : true,
    cancelHint : 'My cancel hint!'
});

$('#cancelPlace').raty({
    cancel      : true,
    cancelPlace : 'right'
});
$('#target-div').raty({
    cancel : true,
    target : '#target-div-hint'
});
$('#targetType').raty({
    cancel     : true,
    target     : '#targetType-hint',
    targetType : 'percentage'
});
$('#starType').raty({
    cancel   : true,
    half     : true,
    starType : 'i'
});