$(document).ready(function() {
    ////mensaje
    //$(".msge-row-x").click(function() {
    //    document.cookie = "closed-alert=true";
    //    $(".msge-row").hide();
    //})
    //if ($('.site-content').hasClass("dashboard")) {
    //    $('.site-wrapper').addClass("grid-bg");
    //}
    //$("#lnk_int_CerrarSesion").click(function() {
    //    document.cookie = "closed-alert=false";
    //})

    //mensaje
    $(".msge-row-x").click(function(){
        document.cookie="closed-alert=true";
        $(".msge-row").hide();
    })
      if ($('.site-content').hasClass("dashboard")) {
        $('.site-wrapper').addClass("grid-bg");
      }
    $("#lnk_int_CerrarSesion").click(function(){
        document.cookie="closed-alert=false";
    })

    
    var tour = new Tour({ 
      backdrop: true,
      debug: true,
      storage: false,
      steps: [
      {
        element: "#first-message",
        title: "Atajos rápidos",
        content: "Encuentra lo que necesites en un solo paso. Dando clic en uno de los iconos."
      },
      {
        element: "#temperatura",
        title: "Temperatura",
        content: "Aqui podrás ver como esta el clima en nuestra ciudad"
      },

      {
        element: "#agenda",
        title: "Friamente calculado",
        content: "Aqui puedes programar y ver que planes hay para hoy, reuniones o eventos",
        placement: "left"
      },

    ]});

    // Initialize the tour
    tour.init();

    // Start the tour
    tour.start();   



    // $('#myModal').modal('toggle');


function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i=0; i<ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length,c.length);
    }
    return "";
}


$('#js-flip-2').bind('click mouseleave', function() {
    $('#js-flip-2 .card').toggleClass('flipped');
});

    $('#txtBuscarMobile, #txtPalabrasClave').focusout(function () {
        $('#txtBuscarMobile').val(this.value);
        $('#txtPalabrasClave').val(this.value);
    })

    // $('#myModal').modal('toggle');
    
    $("#btnImgBusqueda").click(function () {
        var palabra = $.trim($('#txtPalabrasClave').val())

        if(palabra =="")
            return false
    });
    
    $(".tutorial").click(function () {
        $("#tutorial li").each(function (index) {
            $('img', this).attr('src', '/images/tutorial/pantalla'+index+'.png');
        });

    });

    $(".tutorial_mob").click(function () {
        $("#tutorial li").each(function (index) {
            $('img', this).attr('src', '/images/tutorial_mob/pantalla' + index + '.png');
        });

    });
});

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

