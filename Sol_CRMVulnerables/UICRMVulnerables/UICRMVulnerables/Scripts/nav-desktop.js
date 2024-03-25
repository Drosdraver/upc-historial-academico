$(document).ready(function () {
    // $('.nav-main > li.menu').removeClass('open active-menu');
    var $navMain = $('.nav-main > li.menu');
    $navMain.hover(function () {
        $(this).siblings().removeClass('open active-menu');
        $(this).toggleClass('open');
        $(this).toggleClass('active-menu');
    });

    // Function for smooth proportional scroll to vertical side menu
    var scrollPercentage = $(window).scrollTop() / ($(document).height() - $(window).height());
    var availableHeight = $(window).height() - $('.site-header .container-fluid').height();
    var contenido = $('.site-header .container-fluid').height() + $('.site-menu').height();

    //Se guarda el tama�o inicial
    $('#cntInicial').css('min-height', contenido);

    $(window).scroll(function () {
        var scrollPercentage = $(window).scrollTop() / ($(document).height() - $(window).height());
        var availableHeight = $(window).height() - $('.site-header .container-fluid').height();

        if (parseInt($('.site-menu').css('top')) + availableHeight < $('.nav-main').height()) {
            $('.site-menu').css('top', -($('.nav-main').height() - availableHeight) * (scrollPercentage));
        } else {
            $('.site-menu').css('top', '0px');
        }
    });

    //if ($(window).height() > contenido)
    //    {
    //    alert("VENTANA: ");
    //}
    //else
    //{

    //}

});