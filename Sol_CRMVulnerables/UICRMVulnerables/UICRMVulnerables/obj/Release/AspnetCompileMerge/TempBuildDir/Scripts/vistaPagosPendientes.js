$(document).ready(function () {
    showLoading(true);

    previousUrl = document.referrer;
    urlCodLineaNegocio = getURLParameter("CodLineaNegocio");
    urlCodAlumno = getURLParameter("CodAlumno");
    urlCodModalEst = "x";
    urlCodPeriodo = "x";

    procesarServicios(procesarPagosPendientes);
})

function procesarPagosPendientes() {
    $.ajax({
        dataType: "json",
        url: "/HistorialAcademico/PagosPendientesResultado",
        method: "POST",
        data: {
            pc_CodLineaNegocio: urlCodLineaNegocio,
            pc_CodAlumno: urlCodAlumno
        },
        beforeSend: function () {
            showLoading(true);
        },
        complete: function () {
            showLoading(false);
        },
        success: function (res) {
            var jsonRes = JSON.parse(res);
            if (jsonRes.RespuestaExitosa) {
                mostrarDeudas(jsonRes.DatosVista.DTOTabDeudas);
            }
            else {
                showError(jsonRes.MensajeError);
            }
        },
    });
}