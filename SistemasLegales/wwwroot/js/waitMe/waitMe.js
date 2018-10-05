MiApp.LoadingPanel = function () {
    return {
        mostrarNotificacion: function (idElemento, texto) {
            $('#' + idElemento).waitMe({
                effect: 'roundBounce',
                text: texto,
                bg: 'rgba(255, 255, 255, 0.7)',
                color: '#00bcd4',
                fontSize: '15px'
            });
        }
    }
}();