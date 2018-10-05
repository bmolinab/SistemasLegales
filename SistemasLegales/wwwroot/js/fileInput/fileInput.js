MiApp.FileInput = function () {
    return {
        init: function (idElemento) {
            $("#" + idElemento).fileinput({
                showUpload: false,
                language: 'es'
            });
        }
    }
}();