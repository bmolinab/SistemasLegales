MiApp.DatePicker = function () {
    this.inicializarBtnEliminar = function () {
        $(".btndatepicker").on("click", function (e) {
            var btndatepicker = $(e.currentTarget);
            var idDatapicker = btndatepicker.data("picker");
            $("#" + idDatapicker).focus();
        });
    };

    return {
        init: function () {
            this.inicializarDatepicker();
            inicializarBtnEliminar();
        },

        inicializarDatepicker: function () {
            $('.datepicker').datepicker({
                language: 'es',
                clearBtn: true
            }).on("changeDate", function (e) {
                if (MiApp.DatePicker.fnCallbackChangeDatepicker)
                    MiApp.DatePicker.fnCallbackChangeDatepicker();
            });

            $('.datepickerYear').datepicker({
                format: "yyyy",
                viewMode: "years",
                minViewMode: "years",
                language: 'es',
                clearBtn: true
            }).on("changeDate", function (e) {
                if (MiApp.DatePicker.fnCallbackChangeDatepicker)
                    MiApp.DatePicker.fnCallbackChangeDatepicker();
            });
        }
    }
}();
MiApp.DatePicker.init();