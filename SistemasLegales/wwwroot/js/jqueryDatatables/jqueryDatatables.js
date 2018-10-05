MiApp.Datatables = function () {
    return {
        init: function (idTabla) {
            this.inicializarDatatable(idTabla);
        },

        inicializarDatatable: function (idTabla) {
            var idTable = idTabla && idTabla != null ? idTabla : 'datatable-responsive';
            $('#' + idTable).DataTable();
        },

        eventoBtnEliminar: function (e) {
            var btnEliminar = $(e);
            var descripcion = btnEliminar.data("descripcion");
            var id = btnEliminar.prop("id");
            MiApp.Bootbox.init("Eliminar", descripcion, null, [], {
                isGuardar: true, hideAlGuardar: true, callbackGuardar: function () {
                    $("#id").val(id);
                    $("#formEliminar").submit();
                }
            });
        }
    }
}();
MiApp.Datatables.init();