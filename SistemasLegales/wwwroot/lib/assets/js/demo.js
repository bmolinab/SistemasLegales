$(function () {
    setSkin();
    skinChanger();
    activateNotificationAndTasksScroll();
    setSkinListHeightAndScroll();
    setSettingListHeightAndScroll();
    $(window).resize(function () {
        setSkinListHeightAndScroll();
        setSettingListHeightAndScroll();
    });
});

//Set Skin changer
function setSkin() {
    var $body = $('body');
    var currentTheme = localStorage.eagle_maintheme;
    var currentColor = localStorage.eagle_themeColor;
    if (currentTheme) {
        if(currentTheme == "dark"){
            $body.removeClass('light').addClass(currentTheme);
        }else if(currentTheme == "light"){
            $body.removeClass('dark').addClass(currentTheme);
        }
        $('.offsidebar #sidebar_clr_selector li').removeClass('active');
        $('[data-theme='+currentTheme+']').addClass('active');
    }

    if (currentColor) {
        var existTheme = $('.offsidebar #skin_selector li.active').data('theme');
        $('.offsidebar #skin_selector li').removeClass('active');
        $('[data-theme='+currentColor+']').addClass('active');
        $body.removeClass('theme-' + existTheme);
        $body.addClass('theme-'+currentColor);
    }
}
//Skin changer
function skinChanger() {
    $('.offsidebar .skin_selector li').on('click', function () {
        var $body = $('body');
        var $this = $(this);
        if($this.data('theme') == "dark"){
            $body.removeClass('light');
            $('.offsidebar #sidebar_clr_selector li').removeClass('active');
            $this.addClass('active');
            $body.addClass('dark');
            var currentStyle = $this.data('theme');
            localStorage.setItem('eagle_maintheme', currentStyle);
        }else if($this.data('theme') == "light"){
            $body.removeClass('dark');
            $('.offsidebar #sidebar_clr_selector li').removeClass('active');
            $this.addClass('active');
            $body.addClass('light');
            var currentStyle = $this.data('theme');
            localStorage.setItem('eagle_maintheme', currentStyle);
        }else{
            var existTheme = $('.offsidebar #skin_selector li.active').data('theme');
            $('.offsidebar #skin_selector li').removeClass('active');
            $body.removeClass('theme-' + existTheme);
            $this.addClass('active');
            $body.addClass('theme-' + $this.data('theme'));
            var currentColor = $this.data('theme');
            localStorage.setItem('eagle_themeColor', currentColor);
        }

    });
}

//Skin tab content set height and show scroll
function setSkinListHeightAndScroll() {
    var height = $(window).height() - ($('.navbar').innerHeight() + $('.offsidebar .nav-tabs').outerHeight());
    var $el = $('#skin_selector');

    $el.slimScroll({ destroy: true }).height('auto');
    $el.parent().find('.slimScrollBar, .slimScrollRail').remove();

    $el.slimscroll({
        height: height + 'px',
        size: '4px',
        alwaysVisible: false,
        borderRadius: '0',
        railBorderRadius: '0'
    });
}

//Setting tab content set height and show scroll
function setSettingListHeightAndScroll() {
    var height = $(window).height() - ($('.navbar').innerHeight() + $('.offsidebar .nav-tabs').outerHeight());
    var $el = $('.demo-settings');

    $el.slimScroll({ destroy: true }).height('auto');
    $el.parent().find('.slimScrollBar, .slimScrollRail').remove();

    $el.slimscroll({
        height: height + 'px',
        size: '4px',
        alwaysVisible: false,
        borderRadius: '0',
        railBorderRadius: '0'
    });
}
//Activate notification and task dropdown on top right menu
function activateNotificationAndTasksScroll() {
    $('.navbar-right .dropdown-menu .body .menu').slimscroll({
        height: '254px',
        size: '4px',
        alwaysVisible: false,
        borderRadius: '0',
        railBorderRadius: '0'
    });
}
//Set Waves
Waves.attach('.menu li a', ['waves-block']);
Waves.init();

$.eagle = {};
$.eagle.options = {
    dropdownMenu: {
        effectIn: 'zoomIn',
        effectOut: 'zoomOut'
    }
}
$.eagle.dropdownMenu = {
    activate: function () {
        var _this = this;

        $('.dropdown, .dropup, .btn-group').on({
            "show.bs.dropdown": function () {
                var dropdown = _this.dropdownEffect(this);
                _this.dropdownEffectStart(dropdown, dropdown.effectIn);
            },
            "shown.bs.dropdown": function () {
                var dropdown = _this.dropdownEffect(this);
                if (dropdown.effectIn && dropdown.effectOut) {
                    _this.dropdownEffectEnd(dropdown, function () { });
                }
            },
            "hide.bs.dropdown": function (e) {
                var dropdown = _this.dropdownEffect(this);
                if (dropdown.effectOut) {
                    e.preventDefault();
                    _this.dropdownEffectStart(dropdown, dropdown.effectOut);
                    _this.dropdownEffectEnd(dropdown, function () {
                        dropdown.dropdown.removeClass('open');
                    });
                }
            }
        });

        //Set Waves
        Waves.attach('.dropdown-menu li a', ['waves-classic']);
        Waves.init();
    },
    dropdownEffect: function (target) {
        var effectIn = $.eagle.options.dropdownMenu.effectIn, effectOut = $.eagle.options.dropdownMenu.effectOut;
        var dropdown = $(target), dropdownMenu = $('.dropdown-menu', target);

        if (dropdown.length > 0) {
            var udEffectIn = dropdown.data('effect-in');
            var udEffectOut = dropdown.data('effect-out');
            if (udEffectIn !== undefined) { effectIn = udEffectIn; }
            if (udEffectOut !== undefined) { effectOut = udEffectOut; }
        }

        return {
            target: target,
            dropdown: dropdown,
            dropdownMenu: dropdownMenu,
            effectIn: effectIn,
            effectOut: effectOut
        };
    },
    dropdownEffectStart: function (data, effectToStart) {
        if (effectToStart) {
            data.dropdown.addClass('dropdown-animating');
            data.dropdownMenu.addClass('animated dropdown-animated');
            data.dropdownMenu.addClass(effectToStart);
        }
    },
    dropdownEffectEnd: function (data, callback) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        data.dropdown.one(animationEnd, function () {
            data.dropdown.removeClass('dropdown-animating');
            data.dropdownMenu.removeClass('animated dropdown-animated');
            data.dropdownMenu.removeClass(data.effectIn);
            data.dropdownMenu.removeClass(data.effectOut);

            if (typeof callback == 'function') {
                callback();
            }
        });
    }
}
$.eagle.input = {
    activate: function () {
        //On focus event
        $('.form-control').focus(function () {
            $(this).parent().addClass('focused');
        });

        //On focusout event
        $('.form-control').focusout(function () {
            var $this = $(this);
            if ($this.parents('.form-group').hasClass('form-float')) {
                if ($this.val() == '') { $this.parents('.form-line').removeClass('focused'); }
            }
            else {
                $this.parents('.form-line').removeClass('focused');
            }
        });

        //On label click
        $('body').on('click', '.form-float .form-line .form-label', function () {
            $(this).parent().find('input').focus();
        });
    }
}

$.eagle.select = {
    activate: function () {
        if ($.fn.selectpicker) { $('select:not(.ms)').selectpicker(); }
    }
}
$(function () {
    $.eagle.select.activate();
    $.eagle.input.activate();
    $.eagle.dropdownMenu.activate();
    setTimeout(function () { $('.page-loader-wrapper').fadeOut(); }, 50);
});

/****************************************************
 card control - Function
 ****************************************************/

$(document).ready(function(){
    $(".card-fullscreen").on("click",function(){
        card_fullscreen($(this).parents(".card"));
        return false;
    });

    $(".card-collapse").on("click",function(){
        card_collapse($(this).parents(".card"));
        $(this).parents(".dropdown").removeClass("open");
        return false;
    });
    $(".card-remove").on("click",function(){
        card_remove($(this).parents(".card"));
        $(this).parents(".dropdown").removeClass("open");
        return false;
    });
    $(".card-refresh").on("click",function(){
        var card = $(this).parents(".card");
        card_refresh(card);

        setTimeout(function(){
            card_refresh(card);
        },3200);

        $(this).parents(".dropdown").removeClass("open");
        return false;
    });

});

/* card FUNCTIONS */
function card_fullscreen(card){

    if(card.hasClass("card-fullscreened")){
        card.removeClass("card-fullscreened").unwrap();
        card.find(".body").css("height","");
        card.find(".card-fullscreen .material-icons").html("fullscreen");

        $(window).resize();
    }else{
        var head    = card.find(".header");
        var body    = card.find(".body");
        var footer  = card.find(".footer");
        var hplus   = 30;

        if(body.hasClass("card-body-table") || body.hasClass("padding-0")){
            hplus = 0;
        }
        if(head.length > 0){
            hplus += head.height()+21;
        }
        if(footer.length > 0){
            hplus += footer.height()+21;
        }

        card.find(".body").height($(window).height() - hplus);


        card.addClass("card-fullscreened").wrap('<div class="card-fullscreen-wrap"></div>');
        card.find(".card-fullscreen .material-icons").html("fullscreen_exit");

        $(window).resize();
    }
}

function card_collapse(card,action,callback){

    if(card.hasClass("card-toggled")){
        card.removeClass("card-toggled");

        card.find(".card-collapse .material-icons").html("expand_more");

        if(action && action === "shown" && typeof callback === "function")
            callback();

    }else{
        card.addClass("card-toggled");

        card.find(".card-collapse .material-icons").html("expand_less");

        if(action && action === "hidden" && typeof callback === "function")
            callback();

    }
}

function card_refresh(card,action,callback){
    if(!card.hasClass("card-refreshing")){
        card.append('<div class="card-refresh-layer"><img src="../images/loader/default.gif"/></div>');
        card.find(".card-refresh-layer").width(card.width()).height(card.height());
        card.addClass("card-refreshing");

        if(action && action === "shown" && typeof callback === "function")
            callback();
    }else{
        card.find(".card-refresh-layer").remove();
        card.removeClass("card-refreshing");

        if(action && action === "hidden" && typeof callback === "function")
            callback();
    }
    onload();
}

function card_remove(card,action,callback){
    if(action && action === "before" && typeof callback === "function")
        callback();

    card.animate({'opacity':0},200,function(){
        card.parent(".card-fullscreen-wrap").remove();
        $(this).remove();
        if(action && action === "after" && typeof callback === "function")
            callback();


        onload();
    });
}
