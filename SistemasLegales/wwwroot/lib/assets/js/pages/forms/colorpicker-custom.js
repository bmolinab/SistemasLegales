$(function () {
    $('.colorpicker').colorpicker();
    $('#cp1').colorpicker({
        color: "#88cc33",
        horizontal: true
    });
    $('#cp_inline').colorpicker({
        color: '#ffaa00',
        container: true,
        inline: true
    });
    $('#color_palette').colorpicker({
        colorSelectors: {
            'black': '#000000',
            'white': '#ffffff',
            'red': '#FF0000',
            'default': '#777777',
            'primary': '#337ab7',
            'success': '#5cb85c',
            'info': '#5bc0de',
            'warning': '#f0ad4e',
            'danger': '#d9534f'
        }
    });
    $('#widget_size').colorpicker({
        customClass: 'colorpicker-2x',
        sliders: {
            saturation: {
                maxLeft: 200,
                maxTop: 200
            },
            hue: {
                maxTop: 200
            },
            alpha: {
                maxTop: 200
            }
        }
    });
    //Enable or Disable button
    $(".disable-button").click(function(e) {
        e.preventDefault();
        $("#cl_button").colorpicker('disable');
    });

    $(".enable-button").click(function(e) {
        e.preventDefault();
        $("#cl_button").colorpicker('enable');
    });
    $('#cl_button').colorpicker();
});

