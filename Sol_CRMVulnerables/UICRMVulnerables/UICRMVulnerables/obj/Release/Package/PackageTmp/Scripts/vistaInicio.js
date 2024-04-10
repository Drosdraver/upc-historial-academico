$(document).ready(function () {
    showLoading(true);

    previousUrl = document.referrer;
    urlCodLineaNegocio = getURLParameter("CodLineaNegocio");
    urlCodAlumno = getURLParameter("CodAlumno");
    urlCodModalEst = getURLParameter("CodModalEst");
    urlCodPeriodo = getURLParameter("CodPeriodo");

    //urlCodLineaNegocio = "U";
    //urlCodAlumno = "202010172";
    //urlCodModalEst = "AC";
    //urlCodPeriodo = "202220";

    procesarServicios(procesarHistorialAcademico);
})

function procesarHistorialAcademico() {
    $.ajax({
        dataType: "json",
        url: "/HistorialAcademico/HistorialAcademicoResultado",
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
                mostrarDatosAlumno(jsonRes.DatosVista.DTODatosAlumno);
                mostrarDatosGenerales(jsonRes.DatosVista.DTOTabDatosGenerales);
                mostrarAvanceCurricular(jsonRes.DatosVista.DTOTabAvanceCurricular);
                mostrarHistorialNotas(jsonRes.DatosVista.DTOTabHistorialNotas);
                mostrarHorarioAlumno(jsonRes.DatosVista.DTOTabHorarioAlumno);
                mostrarAvanceNotas(jsonRes.DatosVista.DTOTabAvanceNotas);
                mostrarInasistencias(jsonRes.DatosVista.DTOTabInasistencias);
                mostrarDeudas(jsonRes.DatosVista.DTOTabDeudas);
                mostrarPromedioPonderado(jsonRes.DatosVista.DTOTabPromedioPonderado);
                mostrarTramites(jsonRes.DatosVista.DTOTabTramites);

                $(document).on('change', '#datosGenerales-sel-listaPeriodos', function () {
                    llenarPeriodoDatosGenerales($(this).val());
                })

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