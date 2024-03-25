$(document).ready(function () {
    showLoading(true);

    previousUrl = document.referrer;
    urlCodLineaNegocio = getURLParameter("CodLineaNegocio");
    urlCodAlumno = getURLParameter("CodAlumno");
    urlCodModalEst = getURLParameter("CodModalEst");
    urlCodPeriodo = getURLParameter("CodPeriodo");

    procesarServicios(procesarNotasActuales);
})

function procesarNotasActuales() {
    $.ajax({
        dataType: "json",
        url: "/HistorialAcademico/NotasActualesResultado",
        method: "POST",
        data: {
            pc_CodLineaNegocio: urlCodLineaNegocio,
            pc_CodAlumno: urlCodAlumno,
            pc_CodModalEst: urlCodModalEst,
            pc_CodPeriodo: urlCodPeriodo
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
                mostrarAvanceNotas(jsonRes.DatosVista.DTOTabAvanceNotas);

                $(document).on('change', '#avanceNotas-sel-cursos', function () {
                    llenarNotasCurso($(this).val());
                })
            }
            else {
                showError(jsonRes.MensajeError);
            }
        },
    });
}