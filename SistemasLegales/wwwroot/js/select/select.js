MiApp.Select = function () {
    return {
        init: function () {
            $.eagle.select = {
                activate: function () {
                    if ($.fn.selectpicker) { $('select:not(.ms)').selectpicker(); }
                }
            }
            $(function () {
                $.eagle.select.activate();
                $.eagle.input.activate();
                $.eagle.dropdownMenu.activate();
                setTimeout(function () { $('.page-loader-wrapper').fadeOut(); }, 50);
            });
        }
    }
}();