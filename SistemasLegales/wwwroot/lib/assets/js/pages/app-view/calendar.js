! function(e, t, o, n) {
    function a(e, t) {
        var n = o("#remove-after-drop");
        e.fullCalendar({
            header: {
                left: "prev,next today",
                center: "title",
                right: "month,agendaWeek,agendaDay"
            },
            buttonIcons: {
                prev: " fa fa-caret-left",
                next: " fa fa-caret-right"
            },
            buttonText: {
                today: "today",
                month: "month",
                week: "week",
                day: "day"
            },
            editable: !0,
            droppable: !0,
            drop: function(t, a) {
                var r = o(this),
                    i = r.data("calendarEventObject");
                if (i) {
                    var l = o.extend({}, i);
                    l.start = t, l.allDay = a, l.backgroundColor = r.css("background-color"), l.borderColor = r.css("border-color"), e.fullCalendar("renderEvent", l, !0), n.is(":checked") && r.remove()
                }
            },
            eventDragStart: function(e, t, o) {
                l = e
            },
            events: t
        })
    }

    function r(e) {
        var t = o(".external-events");
        new s(t.children("div"));
        var n = "#f6504d",
            a = o(".external-event-add-btn"),
            r = o(".external-event-name"),
            i = o(".external-event-color-selector .cirkle");
        o(".external-events-trash").droppable({
            accept: ".fc-event",
            activeClass: "active",
            hoverClass: "hovered",
            tolerance: "touch",
            drop: function(t, o) {
                if (l) {
                    var n = l.id || l._id;
                    e.fullCalendar("removeEvents", n), o.draggable.remove(), l = null
                }
            }
        }), i.click(function(e) {
            e.preventDefault();
            var t = o(this);
            n = t.css("background-color"), i.removeClass("selected"), t.addClass("selected")
        }), a.click(function(e) {
            e.preventDefault();
            var a = r.val();
            if ("" !== o.trim(a)) {
                var i = o("<div/>").css({
                    "background-color": n,
                    "border-color": n,
                    color: "#fff"
                }).html(a);
                t.prepend(i), new s(i), r.val("")
            }
        })
    }

    function i() {
        var e = new Date,
            t = e.getDate(),
            o = e.getMonth(),
            n = e.getFullYear();
        return [{
            title: "All Day Event",
            start: new Date(n, o, 1),
            backgroundColor: "#F44336",
            borderColor: "#F44336"
        }, {
            title: "Long Event",
            start: new Date(n, o, t - 5),
            end: new Date(n, o, t - 2),
            backgroundColor: "#00BCD4",
            borderColor: "#00BCD4"
        }, {
            title: "Meeting",
            start: new Date(n, o, t, 10, 30),
            allDay: !1,
            backgroundColor: "#4CAF50",
            borderColor: "#4CAF50"
        }, {
            title: "Lunch",
            start: new Date(n, o, t, 12, 0),
            end: new Date(n, o, t, 14, 0),
            allDay: !1,
            backgroundColor: "#009688",
            borderColor: "#009688"
        }, {
            title: "Birthday Party",
            start: new Date(n, o, t + 1, 19, 0),
            end: new Date(n, o, t + 1, 22, 30),
            allDay: !1,
            backgroundColor: "#FF9800",
            borderColor: "#FF9800"
        }, {
            title: "Open Google",
            start: new Date(n, o, 28),
            end: new Date(n, o, 29),
            url: "//google.com/",
            backgroundColor: "#9E9E9E",
            borderColor: "#9E9E9E"
        }]
    }
    if (o.fn.fullCalendar) {
        o(function() {
            var e = o("#calendar"),
                t = i();
            r(e), a(e, t)
        });
        var l = null,
            s = function(e) {
                e && e.each(function() {
                    var e = o(this),
                        t = {
                            title: o.trim(e.text())
                        };
                    e.data("calendarEventObject", t), e.draggable({
                        zIndex: 1070,
                        revert: !0,
                        revertDuration: 0
                    })
                })
            }
    }
}(window, document, window.jQuery);