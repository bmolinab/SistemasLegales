MiApp.Mensajes = function () {
    return {
        mostrarNotificacion: function (tipo, texto) {
            var titulo = "";
            switch (tipo) {
                case "success": titulo = "Información"; break;
                case "error": titulo = "Error"; break;
                case "warning": titulo = "Aviso"; break;
            }
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "rtl": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": 300,
                "hideDuration": 1000,
                "timeOut": 5000,
                "extendedTimeOut": 1000,
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            toastr[tipo](texto, titulo);
        }
    }
}();