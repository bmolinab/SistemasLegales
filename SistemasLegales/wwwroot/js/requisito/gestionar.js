MiApp.Requisito = function () {
    this.eventoOrganismoControl = function () {
        $("#Documento_RequisitoLegal_IdOrganismoControl").on("change", function (e) {
            partialViewRequisitoLegal();
        });
    };

    this.eventoRequisitoLegal = function () {
        $("#Documento_IdRequisitoLegal").on("change", function (e) {
            partialViewDocumento();
        });
    };

    this.partialViewRequisitoLegal = function () {
        MiApp.LoadingPanel.mostrarNotificacion("bodyTemplate", "Cargando requisitos legales...");
        $.ajax({
            url: urlRequisitoLegalSelectResult,
            method: "POST",
            data: { idOrganismoControl: $("#Documento_RequisitoLegal_IdOrganismoControl").val() },
            success: function (data) {
                $("#divRequisitoLegal").html(data);
            },
            error: function (errorMessage) {
                MiApp.Mensajes.mostrarNotificacion("error", "Ocurrió un error al cargar los requisitos legales, inténtelo nuevamente.");
            },
            complete: function () {
                partialViewDocumento();
                eventoOrganismoControl();
            }
        });
    };

    this.partialViewDocumento = function () {
        MiApp.LoadingPanel.mostrarNotificacion("bodyTemplate", "Cargando documentos...");
        $.ajax({
            url: urlDocumentoSelectResult,
            method: "POST",
            data: { idRequisitoLegal: $("#Documento_IdRequisitoLegal").val() },
            success: function (data) {
                $("#divDocumento").html(data);
            },
            error: function (errorMessage) {
                MiApp.Mensajes.mostrarNotificacion("error", "Ocurrió un error al cargar los documentos, inténtelo nuevamente.");
            },
            complete: function () {
                eventoRequisitoLegal();
                MiApp.Select.init();
                $("#bodyTemplate").waitMe("hide");
            }
        });
    };

    return {
        init: function (idElemento) {
            MiApp.FileInput.init("file");
            eventoOrganismoControl();
            eventoRequisitoLegal();
        }
    }
}();
MiApp.Requisito.init();