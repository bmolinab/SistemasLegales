$(document).ready(function(){

    $("#todo, #inprogress, #completed").sortable({
        connectWith: ".connectList",
        update: function( event, ui ) {

            var todo = $( "#todo" ).sortable( "toArray" );
            var inprogress = $( "#inprogress" ).sortable( "toArray" );
            var completed = $( "#completed" ).sortable( "toArray" );
            $('.output').html("ToDo: " + window.JSON.stringify(todo) + "<br/>" + "In Progress: " + window.JSON.stringify(inprogress) + "<br/>" + "Completed: " + window.JSON.stringify(completed));
        }
    }).disableSelection();

    $("#add_new_task").on("click",function(){
        var nt = $("#new_task").val();
        if(nt != ''){

            var task = '<li class="warning-element">'+nt
                +'<div class="taskb-detail">'
                +'<a href="#" class="pull-right btn btn-xs btn-default">Tag</a>'
                +'<i class="fa fa-clock-o"></i> Now'
                +'</div>'
                +'</li>';

            $("#todo").prepend(task);
        }
    });
});