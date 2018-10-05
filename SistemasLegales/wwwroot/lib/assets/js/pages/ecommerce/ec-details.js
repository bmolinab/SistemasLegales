// Can also be used with $(document).ready()
$(window).load(function() {
    $('.flexslider').flexslider({
        animation: "slide",
        controlNav: "thumbnails"
    });
});
$.fn.raty.defaults.path = '../../plugins/raty/images';
$('#readOnly').raty({ readOnly: true, score: 3 });