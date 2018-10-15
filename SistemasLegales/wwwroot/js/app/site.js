MiApp.Generales = function () {
    var form = $(this);

    this.gestionarMsg = function () {
        var mensaje = $("#spanMensaje").html();
        if (mensaje != "" && mensaje != null) {
            var arr_msg = mensaje.split('|');
            MiApp.Mensajes.mostrarNotificacion(arr_msg[0], arr_msg[1]);
        }
    };

    this.onSubmitForm = function () {
        form.on("submit", function (e) {
            try {
                var l = Ladda.create(document.querySelector('#btn-guardar'));
                l.start();
                $("#laddaSpanGuardar").addClass("padding-left-20");

                var ll = Ladda.create(document.querySelector('#btn-guardar-1'));
                ll.start();
                $("#laddaSpanGuardar-1").addClass("padding-left-20");
            } catch (e) { }

        });
    };

    return {
        init: function () {
            gestionarMsg();
            onSubmitForm();
        }
    }
}();
MiApp.Generales.init();