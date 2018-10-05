$(document).ready(function(c) {
    $('.alert-close').on('click', function(c){
        $('.cart_remove').fadeOut('slow', function(c){
            $('.cart_remove').remove();
        });
    });
    $('.alert-close1').on('click', function(c){
        $('.cart_remove2').fadeOut('slow', function(c){
            $('.cart_remove2').remove();
        });
    });
});