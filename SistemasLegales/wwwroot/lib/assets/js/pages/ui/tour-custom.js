
!function(e, t, o, a) {
    o(function() {
        var e = [];
        if (o(".tour-step").each(function() {
                var t = o(this).data();
                t.element = "#" + this.id, e.push(t)
            }), e.length) {
            var t = new Tour({
                backdrop: !0,
                onShown: function(e) {
                    o(".content > container-fluid").css({
                        position: "static"
                    })
                },
                onHide: function(e) {
                    o(".content > container-fluid").css({
                        position: ""
                    })
                },
                steps: e
            });
            t.init(), o("#start-tour").on("click", function() {
                t.restart()
            })
        }
    })
}(window, document, window.jQuery);