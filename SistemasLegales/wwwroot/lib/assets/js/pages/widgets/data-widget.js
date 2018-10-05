
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

/*...............slim scroll for mail widget..............................*/
$(function(){
    $('#slimscroll').slimScroll({
        height: '336px'
    });
});
/*...............slim scroll for news..............................*/
$(function(){
    $('#scroll-news').slimScroll({
        height: '335px'
    });
});


