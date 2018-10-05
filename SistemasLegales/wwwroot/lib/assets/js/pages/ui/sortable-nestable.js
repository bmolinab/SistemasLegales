
// jQuery $('document').ready(); function
$('document').ready(function(){

    $('.dd').nestable({ /* config options */ });

    /* Buttons to  */
    $('#nestable-menu').on('click', function(e)
    {
        var target = $(e.target),
            action = target.data('action');
        if (action === 'expand-all') {
            $('.dd').nestable('expandAll');
        }
        if (action === 'collapse-all') {
            $('.dd').nestable('collapseAll');
        }
    });


    // activate Nestable for list 2
    $('#nestable2').nestable({
        group: 1
    });

});
