$(document).ready(function () {
    showLoading(true);

    previousUrl = document.referrer;
    urlCodLineaNegocio = getURLParameter("CodLineaNegocio");
    urlCodAlumno = getURLParameter("CodAlumno");
    urlCodModalEst = getURLParameter("CodModalEst");
    urlCodPeriodo = getURLParameter("CodPeriodo");

    procesarServicios(procesarInasistencias);
})

function procesarInasistencias() {
    $.ajax({
        dataType: "json",
        url: "/HistorialAcademico/InasistenciasResultado",
        method: "POST",
        data: {
            pc_CodLineaNegocio: urlCodLineaNegocio,
            pc_CodAlumno: urlCodAlumno,
            pc_CodModalEst: urlCodModalEst,
            pc_CodPeriodo: urlCodPeriodo
        },
        complete: function () {
            showLoading(false);
        },
        success: function (res) {
            var jsonRes = JSON.parse(res);
            if (jsonRes.RespuestaExitosa) {
                mostrarInasistencias(jsonRes.DatosVista.DTOTabInasistencias);
            }
            else {
                showError(jsonRes.MensajeError);
            }
        },
    });
}