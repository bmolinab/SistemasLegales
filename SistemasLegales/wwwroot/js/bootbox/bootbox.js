MiApp.Bootbox = function () {
    var arrBootBox = [];

    return {
        init: function (titulo, cuerpo, size, buttonsAdicionales, objConfiguracionGuardar, callbackCancelar) {
            var objBootBox = {
                title: titulo,
                message: cuerpo
            };

            if (size)
                objBootBox.size = size;

            objBootBox.buttons = {
                cancel: {
                    label: "Cancelar",
                    className: 'btn-default',
                    callback: function () {
                        if (callbackCancelar)
                            callbackCancelar();

                        MiApp.Bootbox.closeBootBox();
                        return false;
                    }
                }
            };

            if (buttonsAdicionales && buttonsAdicionales != null) {
                $.each(buttonsAdicionales, function (index, value) {
                    objBootBox.buttons[value.key] = value.obj;
                });
            }

            if (objConfiguracionGuardar && objConfiguracionGuardar != null) {
                if (objConfiguracionGuardar.isGuardar) {
                    objBootBox.buttons.confirm = {
                        label: "Aceptar",
                        className: 'btn-primary',
                        callback: function () {
                            if (objConfiguracionGuardar.callbackGuardar)
                                objConfiguracionGuardar.callbackGuardar();

                            if (objConfiguracionGuardar.hideAlGuardar)
                                MiApp.Bootbox.closeBootBox();
                            return false;
                        }
                    };
                }
            }
            var openedDialog = bootbox.dialog(objBootBox);
            arrBootBox.push(openedDialog);

            MiApp.Bootbox.ajustarHeightBootbox();

            openedDialog.on("hidden.bs.modal", function () {
                arrBootBox.splice(arrBootBox.length - 1, 1);

                if (callbackCancelar)
                    callbackCancelar();
            });
        },

        closeBootBox: function () {
            arrBootBox[arrBootBox.length - 1].modal("hide");
        },

        ajustarBootboxPorCiento: function (porCiento) {
            var innerDialog = arrBootBox[arrBootBox.length - 1].find(".modal-dialog");
            innerDialog.prop("style", "width:" + porCiento + "% !important;");
        },

        ajustarHeightBootbox: function () {
            $('.modal-body').attr('style', 'max-height:' + (($(window).height() - $(window).height() / 4)) + 'px;overflow-y:auto;');
        }
    }
}();