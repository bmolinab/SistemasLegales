MiApp.Principal = function () {
    return {
        init: function (idTabla) {
            $("#divCard").css("min-height", $(window).height() - 250);
        }
    }
}();
MiApp.Principal.init();