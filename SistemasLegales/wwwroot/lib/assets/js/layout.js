
! function(e, t, o, n) {
    if ("undefined" == typeof o) throw new Error("This application's JavaScript requires jQuery");
    o(function() {
        var e = o("body");
        (new StateToggler).restoreState(e), o("#chk-fixed").prop("checked", e.hasClass("layout-fixed")), o("#chk-collapsed").prop("checked", e.hasClass("aside-collapsed")), o("#chk-collapsed-text").prop("checked", e.hasClass("aside-collapsed-text")), o("#chk-boxed").prop("checked", e.hasClass("layout-boxed")), o("#chk-float").prop("checked", e.hasClass("aside-float")), o("#chk-hover").prop("checked", e.hasClass("aside-hover")), o(".offsidebar.hide").removeClass("hide"), o.ajaxPrefilter(function(e, t, o) {
            e.async = !0
        })
    })
}(window, document, window.jQuery),


    function(e, t, o, n) {
        e.APP_COLORS = {
            red: '#F44336',
            pink: '#E91E63',
            purple: '#9C27B0',
            deepPurple: '#673AB7',
            indigo: '#3F51B5',
            blue: '#2196F3',
            lightBlue: '#03A9F4',
            cyan: '#00BCD4',
            teal: '#009688',
            green: '#4CAF50',
            lightGreen: '#8BC34A',
            lime: '#CDDC39',
            yellow: '#ffe821',
            amber: '#FFC107',
            orange: '#FF9800',
            deepOrange: '#FF5722',
            brown: '#795548',
            grey: '#9E9E9E',
            blueGrey: '#607D8B',
            black: '#000000',
            white: '#ffffff'
        }, e.APP_MEDIAQUERY = {
            desktopLG: 1200,
            desktop: 992,
            tablet: 768,
            mobile: 480
        }
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        "undefined" != typeof screenfull && o(function() {
            function n(e) {
                screenfull.isFullscreen ? e.children("em").html("fullscreen_exit") : e.children("em").html("fullscreen")
            }
            var a = o(t),
                r = o("[data-toggle-fullscreen]"),
                i = e.navigator.userAgent;
            (i.indexOf("MSIE ") > 0 || i.match(/Trident.*rv\:11\./)) && r.addClass("hide"), r.is(":visible") && (r.on("click", function(e) {
                e.preventDefault(), screenfull.enabled ? (screenfull.toggle(), n(r)) : console.log("Fullscreen not enabled")
            }), screenfull.raw && screenfull.raw.fullscreenchange && a.on(screenfull.raw.fullscreenchange, function() {
                n(r)
            }))
        })
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        function a(e) {
            var t = "autoloaded-stylesheet",
                n = o("#" + t).attr("id", t + "-old");
            return o("head").append(o("<link/>").attr({
                id: t,
                rel: "stylesheet",
                href: e
            })), n.length && n.remove(), o("#" + t)
        }
        o(function() {
            o("[data-load-css]").on("click", function(e) {
                var t = o(this);
                t.is("a") && e.preventDefault();
                var n, r = t.data("loadCss");
                r ? (n = a(r), n || o.error("Error creating stylesheet link element.")) : o.error("No stylesheet location defined.")
            })
        })
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        o(function() {
            var e = new a,
                n = o("[data-search-open]");
            n.on("click", function(e) {
                e.stopPropagation()
            }).on("click", e.toggle);
            var r = o("[data-search-dismiss]"),
                i = '.navbar-form input[type="text"]';
            o(i).on("click", function(e) {
                e.stopPropagation()
            }).on("keyup", function(t) {
                27 == t.keyCode && e.dismiss()
            }), o(t).on("click", e.dismiss), r.on("click", function(e) {
                e.stopPropagation()
            }).on("click", e.dismiss)
        });
        var a = function() {
            var e = "form.navbar-form";
            return {
                toggle: function() {
                    var t = o(e);
                    t.toggleClass("open");
                    var n = t.hasClass("open");
                    t.find("input")[n ? "focus" : "blur"]()
                },
                dismiss: function() {
                    o(e).removeClass("open").find('input[type="text"]').blur()
                }
            }
        }
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        function a() {
            var e = o("<div/>", {
                class: "dropdown-backdrop"
            });
            e.insertAfter(".aside").on("click mouseenter", function() {
                l()
            })
        }

        function r(e) {
            e.siblings("li").removeClass("open").end().toggleClass("open")
        }

        function i(e) {
            l();
            var t = e.children("ul");
            if (!t.length) return o();
            if (e.hasClass("open")) return r(e), o();
            var n = o(".aside"),
                a = o(".aside-inner"),
                i = parseInt(a.css("padding-top"), 0) + parseInt(n.css("padding-top"), 0),
                s = t.clone().appendTo(n);
            r(e);
            var c = e.position().top + i - g.scrollTop(),
                u = f.height();
            return s.addClass("nav-floating").css({
                position: d() ? "fixed" : "absolute",
                top: c,
                bottom: s.outerHeight(!0) + c > u ? 0 : "auto"
            }), s.on("mouseleave", function() {
                r(e), s.remove()
            }), s
        }

        function l() {
            o(".sidebar-subnav.nav-floating").remove(), o(".dropdown-backdrop").remove(), o(".sidebar li.open").removeClass("open")
        }

        function s() {
            return p.hasClass("touch")
        }

        function c() {
            return h.hasClass("aside-collapsed") || h.hasClass("aside-collapsed-text")
        }

        function d() {
            return h.hasClass("layout-fixed")
        }

        function u() {
            return h.hasClass("aside-hover")
        }
        var f, p, h, g, m;
        o(function() {
            f = o(e), p = o("html"), h = o("body"), g = o(".sidebar"), m = APP_MEDIAQUERY;
            var t = g.find(".collapse");

            t.on("show.bs.collapse", function(e) {
                var openmenu = $(this).parent('li');
                openmenu.siblings('.sub-menu-open').find(".in").collapse("hide");
                openmenu.siblings('.sub-menu-open').removeClass('sub-menu-open');

                if($('.menu-open').find(this).length>0){
                    //t.closest('li').removeClass('sub-menu-open');
                    openmenu.addClass('sub-menu-open');
                    //alert("open-if");
                }else{
                    //alert("open-else");
                    t.closest('li').removeClass('menu-open');
                    openmenu.addClass('menu-open');

                }

                e.stopPropagation(), 0 === o(this).parents(".collapse").length && t.filter(".in").collapse("hide")
            });

            t.on("hide.bs.collapse", function(e) {
                e.stopPropagation();
                var openmenu = $(this).parent('li');
                if($(this).parent().hasClass('sub-menu-open')){
                    //alert("hide-if");
                    openmenu.removeClass('sub-menu-open');
                }else{
                    //alert("hide-else");
                    openmenu.removeClass('menu-open');
                }
            });

            var n = o(".sidebar .active").parents("li");
            u() || n.addClass("active").children(".collapse").collapse("show"), g.find("li > a + ul").on("show.bs.collapse", function(e) {
                u() && e.preventDefault()
            });
            var r = s() ? "click" : "mouseenter",
                l = o();
            g.on(r, ".nav > li", function() {
                (c() || u()) && (l.trigger("mouseleave"), l = i(o(this)), a())
            });
            var d = g.data("sidebarAnyclickClose");
            "undefined" != typeof d && o(".wrapper").on("click.sidebar", function(e) {
                if (h.hasClass("aside-toggled")) {
                    var t = o(e.target);
                    t.parents(".aside").length || t.is("#user-block-toggle") || t.parent().is("#user-block-toggle") || h.removeClass("aside-toggled")
                }
            })
        })
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        o(function() {
            o("[data-scrollable]").each(function() {
                var e = o(this),
                    t = 250;
                e.slimScroll({
                    height: e.data("height") || t
                })
            })
        })
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        o(function() {
            var t = o("body");
            toggle = new StateToggler, o("[data-toggle-state]").on("click", function(a) {
                a.stopPropagation();
                var r = o(this),
                    i = r.data("toggleState"),
                    l = r.data("target"),
                    s = r.attr("data-no-persist") !== n,
                    c = l ? o(l) : t;
                i && (c.hasClass(i) ? (c.removeClass(i), s || toggle.removeState(i)) : (c.addClass(i), s || toggle.addState(i))), o(e).resize()
            })
        }), e.StateToggler = function() {
            var e = "jq-toggleState",
                t = {
                    hasWord: function(e, t) {
                        return new RegExp("(^|\\s)" + t + "(\\s|$)").test(e)
                    },
                    addWord: function(e, t) {
                        if (!this.hasWord(e, t)) return e + (e ? " " : "") + t
                    },
                    removeWord: function(e, t) {
                        if (this.hasWord(e, t)) return e.replace(new RegExp("(^|\\s)*" + t + "(\\s|$)*", "g"), "")
                    }
                };
            return {
                addState: function(n) {
                    var a = o.localStorage.get(e);
                    a = a ? t.addWord(a, n) : n, o.localStorage.set(e, a)
                },
                removeState: function(n) {
                    var a = o.localStorage.get(e);
                    a && (a = t.removeWord(a, n), o.localStorage.set(e, a))
                },
                restoreState: function(t) {
                    var n = o.localStorage.get(e);
                    n && t.addClass(n);
                }
            }
        }
    }(window, document, window.jQuery),

    function(e, t, o, n) {
        o(function() {
            var n = o("[data-trigger-resize]"),
                a = n.data("triggerResize");
            n.on("click", function() {
                setTimeout(function() {
                    var o = t.createEvent("UIEvents");
                    o.initUIEvent("resize", !0, !1, e, 0), e.dispatchEvent(o)
                }, a || 300)
            })
        })
    }(window, document, window.jQuery),
    function(e, t, o) {
        "use strict";
        var n = e("html"),
            a = e(t);
        e.support.transition = function() {
            var e = function() {
                var e, t = o.body || o.documentElement,
                    n = {
                        WebkitTransition: "webkitTransitionEnd",
                        MozTransition: "transitionend",
                        OTransition: "oTransitionEnd otransitionend",
                        transition: "transitionend"
                    };
                for (e in n)
                    if (void 0 !== t.style[e]) return n[e]
            }();
            return e && {
                    end: e
                }
        }(), e.support.animation = function() {
            var e = function() {
                var e, t = o.body || o.documentElement,
                    n = {
                        WebkitAnimation: "webkitAnimationEnd",
                        MozAnimation: "animationend",
                        OAnimation: "oAnimationEnd oanimationend",
                        animation: "animationend"
                    };
                for (e in n)
                    if (void 0 !== t.style[e]) return n[e]
            }();
            return e && {
                    end: e
                }
        }(), e.support.requestAnimationFrame = t.requestAnimationFrame || t.webkitRequestAnimationFrame || t.mozRequestAnimationFrame || t.msRequestAnimationFrame || t.oRequestAnimationFrame || function(e) {
            t.setTimeout(e, 1e3 / 60)
        }, e.support.touch = "ontouchstart" in t && navigator.userAgent.toLowerCase().match(/mobile|tablet/) || t.DocumentTouch && document instanceof t.DocumentTouch || t.navigator.msPointerEnabled && t.navigator.msMaxTouchPoints > 0 || t.navigator.pointerEnabled && t.navigator.maxTouchPoints > 0 || !1, e.support.mutationobserver = t.MutationObserver || t.WebKitMutationObserver || t.MozMutationObserver || null, e.Utils = {}, e.Utils.debounce = function(e, t, o) {
            var n;
            return function() {
                var a = this,
                    r = arguments,
                    i = function() {
                        n = null, o || e.apply(a, r)
                    },
                    l = o && !n;
                clearTimeout(n), n = setTimeout(i, t), l && e.apply(a, r)
            }
        }, e.Utils.removeCssRules = function(e) {
            var t, o, n, a, r, i, l, s, c, d;
            e && setTimeout(function() {
                try {
                    for (d = document.styleSheets, a = 0, l = d.length; a < l; a++) {
                        for (n = d[a], o = [], n.cssRules = n.cssRules, t = r = 0, s = n.cssRules.length; r < s; t = ++r) n.cssRules[t].type === CSSRule.STYLE_RULE && e.test(n.cssRules[t].selectorText) && o.unshift(t);
                        for (i = 0, c = o.length; i < c; i++) n.deleteRule(o[i])
                    }
                } catch (e) {}
            }, 0)
        }, e.Utils.isInView = function(t, o) {
            var n = e(t);
            if (!n.is(":visible")) return !1;
            var r = a.scrollLeft(),
                i = a.scrollTop(),
                l = n.offset(),
                s = l.left,
                c = l.top;
            return o = e.extend({
                topoffset: 0,
                leftoffset: 0
            }, o), c + n.height() >= i && c - o.topoffset <= i + a.height() && s + n.width() >= r && s - o.leftoffset <= r + a.width()
        }, e.Utils.options = function(t) {
            if (e.isPlainObject(t)) return t;
            var o = t ? t.indexOf("{") : -1,
                n = {};
            if (o != -1) try {
                n = new Function("", "var json = " + t.substr(o) + "; return JSON.parse(JSON.stringify(json));")()
            } catch (e) {}
            return n
        }, e.Utils.events = {}, e.Utils.events.click = e.support.touch ? "tap" : "click", e.langdirection = "rtl" == n.attr("dir") ? "right" : "left", e(function() {
            if (e.support.mutationobserver) {
                var t = new e.support.mutationobserver(e.Utils.debounce(function(t) {
                    e(o).trigger("domready")
                }, 300));
                t.observe(document.body, {
                    childList: !0,
                    subtree: !0
                })
            }
        }), n.addClass(e.support.touch ? "touch" : "no-touch")
    }(jQuery, window, document),
    function(e, t, o, n) {
        o(function() {})
    }(window, document, window.jQuery);