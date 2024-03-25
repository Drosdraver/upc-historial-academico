var previousUrl;
var urlCodLineaNegocio;
var urlCodAlumno;
var urlCodModalEst;
var urlCodPeriodo;

var dataGeneralesPeriodo;
var dataNotasCurso;
var dataNotaRoja;

var errorMsg = '';
var keepLoading = false;

var fnServicioVista;

// PROCESAMIENTO DE SERVICIOS

function procesarServicios(funcionServicio) {
    $.ajax({
        dataType: "html",
        url: "/HistorialAcademico/UrlCRM",
        method: "POST",
        beforeSend: function () {
            showLoading(true);
        },
        complete: function () {
            if (!keepLoading) {
                showLoading(false);
            }
        },
        success: function (res) {
            if (previousUrl.substring(0, res.length).toLowerCase() == res.toLowerCase()) {
                if (isEmpty(urlCodLineaNegocio) || isEmpty(urlCodAlumno) || isEmpty(urlCodModalEst) || isEmpty(urlCodPeriodo)) {
                    errorMsg = erroMessageUrlParams;
                    showLoading(false);
                    showError(errorMsg);
                }
                else {
                    keepLoading = true;
                    funcionServicio();
                }
            }
            else {
                errorMsg = erroMessageCRM;
                showLoading(false);
                showError(errorMsg);
            }
        },
    });
}

// PROCESAMIENTO DE DATA

function mostrarDatosAlumno(data) {
    $('#datosAlumno-img-imagen').attr('src', validText(data.UrlImagen));
    $('#datosAlumno-span-nombres').text(capitalizeFirstLetter(validText(data.Nombres)));
    $('#datosAlumno-span-apePaterno').text(capitalizeFirstLetter(validText(data.ApellidoPaterno)));
    $('#datosAlumno-span-apeMaterno').text(capitalizeFirstLetter(validText(data.ApellidoMaterno)));
    $('#datosAlumno-span-codigoUsuario').text(validText(data.CodigoUsuario));
}

function mostrarDatosGenerales(data) {
    $(data.listaPeriodos).each(function () {
        $('#datosGenerales-sel-listaPeriodos').append('<option value="' + this.PeriodoVal + '">' + this.PeriodoDes + '</option>');
        $('#NotasActuales-sel-listaPeriodos').append('<option value="' + this.PeriodoVal + '">' + this.PeriodoDes + '</option>');
        $('#Inasistencias-sel-listaPeriodos').append('<option value="' + this.PeriodoVal + '">' + this.PeriodoDes + '</option>');
    })
    $(data.listaModalidades).each(function () {
        $('#datosGenerales-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#AvanceCurricular-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#HistorialNotas-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#Horario-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#NotasActuales-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#Inasistencias-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#Deudas-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
        $('#Promedio-sel-listaModalidad').append('<option value="' + this.ModalidadVal + '">' + this.ModalidadDes + '</option>');
    })
    $('#datosGenerales-sel-listaModalidad').val(data.CodModalidad);
    $('#AvanceCurricular-sel-listaModalidad').val(data.CodModalidad);
    $('#HistorialNotas-sel-listaModalidad').val(data.CodModalidad);
    $('#Horario-sel-listaModalidad').val(data.CodModalidad);
    $('#NotasActuales-sel-listaModalidad').val(data.CodModalidad);
    $('#Inasistencias-sel-listaModalidad').val(data.CodModalidad);
    $('#Deudas-sel-listaModalidad').val(data.CodModalidad);
    $('#Promedio-sel-listaModalidad').val(data.CodModalidad);

    $('#datosGenerales-td-codPeriodo').html(data.CodPeriodo);
    $('#datosGenerales-td-facultad').html(validText(data.Facultad));
    $('#datosGenerales-td-carrera').html(validText(data.Carrera));

    dataGeneralesPeriodo = data.listaDatosPorPeriodo;
    llenarPeriodoDatosGenerales(data.CodPeriodo);

    var htmlHechoImportante;
    $(data.listaHechosImportantes).each(function () {
        htmlHechoImportante = '';
        htmlHechoImportante +=
            '<tr style="text-align: center; margin-top: 21px;">' +
                '<td data-title="FECHA">' + validText(this.FechaHecho) + '</td>' +
                '<td data-title="HORA">' + validText(this.HoraHecho) + '</td>' +
                '<td data-title="TIPO DE REGISTRO">' + validText(this.TipoRegistro) + '</td>' +
                '<td data-title="DESCRIPCIÓN" class="numeric text-left">' + validText(this.Descripcion) + '</td>' +
                '<td data-title="REGISTRADO POR" class="numeric">' + validText(this.RegistradoPor) + '</td>' +
                '<td data-title="ACTIVO" class="numeric">' + validText(this.Activo) + '</td>' +
                '<td class="height-26" data-title="PERIODO-ELIMINADO">' + validText(this.PeriodoEliminado) + '</td>' +
            '</tr>';

        $('#datosGenerales-tbody-hechosImportantes').append(htmlHechoImportante);
    })
}

function mostrarAvanceCurricular(data) {
    var htmlAvanceCurricular = '';
    $(data.listaAvanceCurricularCiclos).each(function () {
        htmlAvanceCurricular = '';
        htmlAvanceCurricular +=
            '<div class="row border-grey  mb-21">' +
                '<div>' +
                    '<p class="solano-bold-26 pt-21 pl-21">Nivel: ' + validText(this.Ciclo) + '</p>' +
                    '<table class="col-md-12 col-sm-12 pb-21 table-bordered table-condensed cf">' +
                        '<thead class="cf">' +
                            '<tr class="bg-blue" style="color: #fff;">' +
                                '<th class="text-center" style="width:141px;">CÓDIGO</th>' +
                                '<th class="text-center" style="width: 49%;">NOMBRE</th>' +
                                '<th colspan="1" class="numeric text-center">VEZ</th>' +
                                '<th class="numeric text-center">NOTA</th>' +
                                '<th class="numeric text-center">CICLO</th>' +
                                '<th class="numeric text-center">ESTADO</th>' +
                            '</tr>' +
                        '</thead>' +
                        '<tbody class="text-center">';

        $(this.listaDTOAvanceCurricular).each(function () {
            htmlAvanceCurricular +=
                '<tr>' +
                    '<td data-title="CÓDIGO">' + validText(this.CodCurso) + '</td>' +
                    '<td class="text-left" data-title="NOMBRE">' + validText(this.DescCurso).toUpperCase() + '</td>' +
                    '<td data-title="VEZ">' + validText(this.NumVezCurso) + '</td>' +
                    '<td data-title="NOTA">' + validText(this.NotaCurso) + '</td>' +
                    '<td data-title="PERIODO">' + validText(this.CodPeriodo) + '</td>' +
                    '<td data-title="ESTADO">' + validText(this.EstadoAprob) + '</td>' +
                '</tr>';
        })

        htmlAvanceCurricular +=
                        '</tbody>' +
                    '</table>' +
                '</div>' +
            '</div>';

        $('#avanceCurricular-div').append(htmlAvanceCurricular);
    })
}

function mostrarHistorialNotas(data) {
    var htmlHistorialNotas = '';
    $(data.listaHistorialNotas).each(function () {
        htmlHistorialNotas = '';
        htmlHistorialNotas +=
            '<div class="row border-grey  mb-35">' +
                '<div class="mb-14">' +
                    '<table class="col-md-12 col-sm-12 pb-21 table-condensed cf">' +
                        '<thead class="cf">' +
                            '<tr style="border: none;">' +
                                '<div class="col-lg-3 col-md-4 col-sm-3">' +
                                    '<p class="solano-bold-26 pt-21 pl-21">' + validText(this.Periodo) + '</p>' +
                                '</div>' +
                                '<div class="col-lg-8 col-md-8">' +
                                    '<p class="solano-bold-26 pt-21 pl-21">' + validText(this.Carrera) + '</p>' +
                                '</div>' +
                            '</tr>' +
                            '<tr class="bg-blue " style="color: #fff;">' +
                                '<th class="text-center">CÓDIGO</th>' +
                                '<th class="text-center">NOMBRE</th>' +
                                '<th class="numeric text-center">NIVEL</th>' +
                                '<th class="numeric text-center">CRÉDITOS</th>' +
                                '<th class="numeric text-center">PROMEDIO <br>FINAL</th>' +
                                '<th class="numeric text-center">NÚMERO DE VECES</th>' +
                                '<th class="numeric text-center">APROBADO</th>' +
                            '</tr>' +
                        '</thead>' +
                        '<tbody style="background: #fff; text-align: center;">';

        $(this.listaHistorialNotasDet).each(function () {
            htmlHistorialNotas +=
                            '<tr>' +
                                '<td data-title="CÓDIGO">' + validText(this.CodigoCurso) + '</td>' +
                                '<td class="text-left" data-title="NOMBRE">' + validText(this.DescripcionCurso).toUpperCase() + '</td>' +
                                '<td data-title="NIVEL">' + validText(this.Nivel) + '</td>' +
                                '<td data-title="CRÉDITOS">' + validText(this.Creditos) + '</td>' +
                                '<td data-title="PROMEDIO">' + validText(this.PromedioFinal) + '</td>' +
                                '<td data-title="N° DE VECES">' + validText(this.NumeroVeces) + '</td>' +
                                '<td data-title="APROBADO">' + validText(this.Aprobado) + '</td>' +
                            '</tr>';
        });

        htmlHistorialNotas +=
                        '</tbody>' +
                    '</table>' +
                '</div>' +
            '</div>' +
            '<div class="row border-grey  mb-35">' +
                '<div class="mb-14">' +
                    '<table class="col-md-12 col-sm-12 pb-21 table-bordered table-condensed cf">' +
                        '<thead class="cf">' +
                            '<tr class="bg-blue-light " style="color: #1075b1;">' +
                                '<th class="text-center" colspan="2">MATRÍCULA</th>' +
                                '<th class=" text-center" rowspan="2">CICLO</th>' +
                                '<th class="numeric text-center" colspan="2">PONDERADO</th>' +
                                '<th class="numeric text-center" colspan="2">OBSERVACIONES</th>' +
                                '<th class="numeric text-center" colspan="2">MÉRITOS</th>' +
                            '</tr>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">TIPO</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">ESTADO</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">ACTUAL</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">ACUMULADO</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">CONST.</th>' +
                            '<th class="bg-blue-light numeric numeric text-center" style="color: #fff;">ALT.</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">ORDEN</th>' +
                            '<th class="bg-blue-light numeric text-center" style="color: #fff;">PERTENECE</th>' +
                        '</thead>' +
                        '<tbody>' +
                            '<tr style="background: #fff; color: #555;">' +
                                '<td data-title="TIPO" class="text-center">' + validText(this.TipoMatricula) + '</td>' +
                                '<td data-title="ESTADO" class="text-center">' + validText(this.EstadoMatricula) + '</td>' +
                                '<td data-title="CICLO" class="text-center">' + validText(this.Ciclo) + '</td>' +
                                '<td data-title="ACTUAL" class="text-center">' + validText(this.PonderadoActual) + '</td>' +
                                '<td data-title="ACUMULADO." class="text-center">' + validText(this.PonderadoAcumulado) + '</td>' +
                                '<td data-title="CONST." class="text-center">' + validText(this.ObservacionesConst) + '</td>' +
                                '<td data-title="ALT." class="text-center">' + validText(this.ObservacionesAlt) + '</td>' +
                                '<td data-title="ORDEN" class="text-center">' + validText(this.OrdenMerito) + '</td>' +
                                '<td data-title="PERTENECE" class="text-center">' + validText(this.PertenenciaMerito) + '</td>' +
                            '</tr>' +
                        '</tbody>' +
                    '</table>' +
                '</div>' +
            '</div>';

        $('#historialNotas-div').append(htmlHistorialNotas);
    })
}

function mostrarHorarioAlumno(data) {
    $('#horario-p-rangoFechas').text('Del ' + data.FechaSemanaInicio + ' al ' + data.FechaSemanaFin);

    $(data.listaFilasHorario).each(function () {
        var htmlFilaHorario = '';
        htmlFilaHorario += '<tr style="background: #fff; text-align: center;">';

        $(this.listaCasillasHorario).each(function () {
            var diaSemana;
            if (this.DiaSemana == 1) diaSemana = 'LUNES';
            else if (this.DiaSemana == 2) diaSemana = 'MARTES';
            else if (this.DiaSemana == 3) diaSemana = 'MIERCOLES';
            else if (this.DiaSemana == 4) diaSemana = 'JUEVES';
            else if (this.DiaSemana == 5) diaSemana = 'VIERNES';
            else if (this.DiaSemana == 6) diaSemana = 'SABADO';
            else if (this.DiaSemana == 7) diaSemana = 'DOMINGO';
            else diaSemana = 'INDEFINIDO';

            var claseTipoSesion;
            if (this.CodigoTipoSesion == 'NO') claseTipoSesion = 'bg-item-horario-normal';
            else if (this.CodigoTipoSesion == 'RE') claseTipoSesion = 'bg-item-horario-recuperacion';
            else if (this.CodigoTipoSesion == 'EX') claseTipoSesion = 'bg-item-horario-examen';
            else if (this.CodigoTipoSesion == 'AD') claseTipoSesion = 'bg-item-horario-adelanto';
            else if (this.CodigoTipoSesion == 'AI') claseTipoSesion = 'bg-item-horario-adicional';
            else if (this.CodigoTipoSesion == 'DE') claseTipoSesion = 'bg-item-horario-devolucion';
            else claseTipoSesion = '';

            if (this.HayClase) {
                var cursoTooltip =
                    'Curso: ' + validText(this.DescripcionCurso) + '\n' +
                    'Docente: ' + validText(this.NombreCompletoDocente) + '\n' +
                    'Categoría de docente: ' + validText(this.DocenteCategoria) + '\n' +
                    'Tipo de clase: ' + validText(this.TipoClase);

                htmlFilaHorario +=
                    '<td data-title="' + diaSemana + '" style="width: 178px;">' +
                        '<div class="' + claseTipoSesion + ' p-14 boldie" title="' + cursoTooltip + '">' +
                            '<span>' +
                                validText(this.Seccion) + ' - ' + validText(this.Grupo) + '<br>' +
                                validText(this.CodigoCurso) + ' - ' + validText(this.CodigoAula) + '<br>' +
                                '<span class="item-gold-text">' + validText(this.NombreCompletoDocente) + '</span><br>' +
                                validText(this.HoraInicioSesion) + ' - ' + validText(this.HoraTerminoSesion) + '<br>' +
                                validText(this.CodigoLocal);
                '</span>' +
            '</div>' +
        '</td>';
            }
            else {
                htmlFilaHorario +=
                    '<td data-title="' + diaSemana + '" style="width: 178px;">' +
                        '<div class="' + claseTipoSesion + 'p-14 boldie">' +
                            '<span></span>' +
                        '</div>' +
                    '</td>';
            }
        })

        htmlFilaHorario += '</tr>';
        $('#horario-tbody-filasHorario').append(htmlFilaHorario);
    })
}

function mostrarAvanceNotas(data) {
    dataNotaRoja = data.NotaRoja;

    $(data.listaAvanceNotas).each(function () {
        $('#avanceNotas-sel-cursos').append('<option value="' + this.CodigoCurso + '">' + this.DescripcionCurso + '</option>');
    })

    dataNotasCurso = data.listaAvanceNotas;

    var codigoCurso = $('#avanceNotas-sel-cursos').val();
    if (codigoCurso != '') {
        llenarNotasCurso(codigoCurso);
    }
}

function mostrarInasistencias(data) {
    var htmlInasistencia;
    $(data.listaInasistencias).each(function () {
        htmlInasistencia = '';
        htmlInasistencia +=
            '<tr style="background: #fff; text-align: center !important; margin-top:42px;">' +
                '<td data-title="CÓDIGO" class="numeric">' + validText(this.CodigoCurso) + '</td>' +
                '<td class="text-left" data-title="DESCRIPCIÓN">' + validText(this.DescripcionCurso).toUpperCase() + '</td>' +
                '<td data-title="DICTADAS" class="numeric">' + validText(this.ClasesDictadas) + '</td>' +
                '<td data-title="ASISTIDAS" class="numeric">' + validText(this.ClasesAsistidas) + '</td>' +
                '<td data-title="INASISTIDAS" class="numeric">' + validText(this.ClasesInasistidas) + '</td>' +
                '<td data-title="INASISTIDAS EFECTIVAS" class="numeric red">' + validText(this.InasistenciasEfectivas) + '</td>' +
                '<td data-title="FECHA DE ASISTENCIA" class="numeric ">' +
                    '<select class="drop-down-2 w-100" style="padding-left: 7px;" name="" id="">';

        $(this.listaFechasInasistencia).each(function () {
            htmlInasistencia += '<option>' + validText(this) + '</option>';
        })

        htmlInasistencia +=
                    '</select>' +
                '</td>' +
                //'<td data-title="NOMBRE DOCENTE" class="numeric">' + this.NombreDocente + '</td>' +
            '</tr>';
        $('#inasistencias-tbody-listaInasistencias').append(htmlInasistencia);
    })
}

function mostrarDeudas(data) {
    $('#deudas-span-fechaActual').text(validText(data.FechaActual));
    $('#deudas-span-codigoAlumno').text(validText(data.CodigoAlumno));
    $('#deudas-span-nombreAlumno').text(validText(data.NombreAlumno));

    var htmlDeudaFila;
    $(data.listaFilasDeuda).each(function () {
        htmlDeudaFila = '';
        htmlDeudaFila +=
            '<tr style="background: #fff; text-align: center !important; margin-top:42px;">' +
                '<td data-title="DOCUMENTOS" class="numeric">' + validText(this.Documento) + '</td>' +
                '<td data-title="FECHA DE EMISIÓN">' + validText(this.FechaEmision) + '</td>' +
                '<td data-title="FECHA VENCIMIENTO" class="numeric">' + validText(this.FechaVencimiento) + '</td>' +
                '<td data-title="TIPO DE VENTA" class="numeric">' + validText(this.TipoVenta) + '</td>' +
                '<td data-title="MONEDA" class="numeric">' + validText(this.Moneda) + '</td>' +
                '<td data-title="IMPORTE" class="numeric">' + this.Importe.toFixed(2) + '</td>' +
                '<td data-title="MORA" class="numeric ">' + this.Mora.toFixed(2) + '</td>' +
                '<td data-title="TOTAL" class="numeric">' + this.Total.toFixed(2) + '</td>' +
            '</tr>';

        $('#deudas-tbody-tablaDeudas').append(htmlDeudaFila);
    })

    var htmlDeudasTotales = ''
    htmlDeudasTotales +=
        '<tr class="boldie" style="background: #fff; text-align: center !important;">' +
            '<td style="color:#004b9e;" class="text-right pr-21 boldie" colspan="5">Documentos en Moneda Local</td>' +
            '<td class="border-blue-top">' + data.ImporteSoles.toFixed(2) + '</td>' +
            '<td class="border-blue-top">' + data.MoraSoles.toFixed(2) + '</td>' +
            '<td class="border-blue-top">' + data.TotalSoles.toFixed(2) + '</td>' +
        '</tr>' +
        '<tr class="boldie" style="background: #fff; text-align: center !important;">' +
            '<td style="color:#004b9e;" class="text-right pr-21" colspan="5">Documentos en Dólares</td>' +
            '<td>' + data.ImporteDolares.toFixed(2) + '</td>' +
            '<td>' + data.MoraDolares.toFixed(2) + '</td>' +
            '<td>' + data.TotalDolares.toFixed(2) + '</td>' +
        '</tr>';

    $('#deudas-tbody-tablaDeudas').append(htmlDeudasTotales);
}

function mostrarPromedioPonderado(data) {
    var promedioColor;
    $(data.listaPromediosPonderados).each(function () {
        promedioColor = '';
        if (this.PromedioPonderado > 0 && this.PromedioPonderado <= 20) {
            if (this.PromedioPonderado < data.promedioRojo)
                promedioColor = 'red';
            else if (this.PromedioPonderado < data.promedioNaranja)
                promedioColor = 'yellow';
            else
                promedioColor = 'green';
        }
        else
            promedioColor = 'none';

        $('#promedioPonderado-ul-codPeriodo').append('<li>' + this.CodigoPeriodo + '</li>');
        $('#promedioPonderado-span-porcentaje').append('<span class="progress progress-' + promedioColor + '" style="width: ' + this.PorcentajeNota + '%;"></span>');
        $('#promedioPonderado-ul-promedio').append('<li>' + this.PromedioPonderado + '</li>');
    })

    $('.promedioPonderado-span-promedioRojo').text(data.promedioRojo);
    $('.promedioPonderado-span-promedioNaranja').text(data.promedioNaranja);
}

// LLENADO DE DATA POR SELECTOR

function llenarPeriodoDatosGenerales(codPeriodo) {
    var data = null;
    $(dataGeneralesPeriodo).each(function () {
        if (this.CodPeriodo == codPeriodo) data = this;
    })

    if (data != null) {
        $('#datosGenerales-td-codPeriodo').html(codPeriodo);
        $('#datosGenerales-td-campus').html(validText(data.Campus));
        $('#datosGenerales-td-cicloAlumno').html(validText(data.CicloAlumno));
        $('#datosGenerales-td-estadoMatricula').html(validText(data.EstadoMatricula));
        $('#datosGenerales-td-categoria').html(validText(data.Categoria));

        $('#datosGenerales-td-orden').html(validText(data.Orden));
        $('#datosGenerales-td-ordenMeritoAcumulado').html(validText(data.OrdenMeritoAcumulado));
        $('#datosGenerales-td-tipoMeritoCarrera').html(validText(data.TipoMeritoCarrera));
        $('#datosGenerales-td-tipoMeritoCarreraAcumulado').html(validText(data.TipoMeritoCarreraAcumulado));
        $('#datosGenerales-td-ponderadoActual').html(validText(data.PonderadoActual));
        $('#datosGenerales-td-ponderadoAcumulado').html(validText(data.PonderadoAcumulado));
        $('#datosGenerales-td-ponderadoBeca').html(validText(data.PonderadoBeca));
        $('#datosGenerales-td-egresado').html(validText(data.Egresado));
        $('#datosGenerales-td-pronabec').html(validText(data.Pronabec));
    }
}

function llenarNotasCurso(codCurso) {
    var data = null;
    $(dataNotasCurso).each(function () {
        if (this.CodigoCurso == codCurso) data = this;
    })

    if (data != null) {
        $('#avanceNotas-span-codigoCurso').text(validText(data.CodigoCurso));
        $('#avanceNotas-span-seccion').text(validText(data.Seccion));
        $('#avanceNotas-span-grupo').text(validText(data.Grupo));
        $('#avanceNotas-span-vez').text(validText(data.Vez));
        $('#avanceNotas-span-creditos').text(validText(data.Creditos));

        $('#avanceNotas-tbody-notas').empty()
        $(data.listaDetalleNotas).each(function () {
            var htmlNotaDetalle = '';
            var detNotaColor = '';

            if (!isNaN(parseFloat(this.Nota))) {
                if (this.Nota <= dataNotaRoja) {
                    detNotaColor = ' red';
                }
            }

            htmlNotaDetalle +=
                '<tr style="background: #fff; text-align: center !important; margin-top: 21px;">' +
                    '<td data-title="TIPO">' + validText(this.CodigoTipoPrueba) + '</td>' +
                    '<td data-title="EVALUACIÓN" class="text-left">' + validText(this.DescripcionTipoPrueba) + '</td>' +
                    '<td data-title="N°" class="numeric">' + validText(this.NumeroPrueba) + '</td>' +
                    '<td data-title="PESO" class="numeric">' + validText(this.PesoPonderado) + '</td>' +
                    '<td data-title="NOTA" class="numeric' + detNotaColor + '">' + validText(this.Nota) + '</td>' +
                    //'<td data-title="ACTUALIZADO POR" class="numeric">' + validText(this.ActualizadoPor) + '</td>' +
                    //'<td data-title="FECHA ACTUALIZACIÓN" class="numeric">' + validText(this.FechaActualizacion) + '</td>' +
                    //'<td data-title="OBSERVACIONES" class="numeric">' +
                    //    '<div class="btn-group">' +
                    //        '<button class="form-control" style="border: transparent;" data-tooltip="' + validText(this.Observaciones) + '"><img src="../Images/red_eye.png" alt=""></button>' +
                    //    '</div>' +
                    //'</td>' +
                '</tr>';
            $('#avanceNotas-tbody-notas').append(htmlNotaDetalle);
        })

        var notaNoOficialRoja = false;
        if (!isNaN(parseFloat(data.NotaNoOficial))) {
            if (data.NotaNoOficial <= dataNotaRoja) {
                notaNoOficialRoja = true;
            }
        }

        $('#avanceNotas-div-avancePorcentual').html(validText(data.AvancePorcentual));
        $('#avanceNotas-div-notaNoOficial').html(validText(data.NotaNoOficial));
        if (notaNoOficialRoja) $('#avanceNotas-div-notaNoOficial').addClass('red');
        else $('#avanceNotas-div-notaNoOficial').removeClass('red');
        $('#avanceNotas-div-notaProyectada').html(validText(parseFloat(data.NotaProyectada).toFixed(2)));
    }
}

// UTILIDADES

function isEmpty(val) {
    return val === undefined || val == null || val.length <= 0;
}

function validText(val) {
    if (isEmpty(val)) val = '';
    return val;
}

function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [null, ''])[1].replace(/\+/g, '%20')) || null;
}

function capitalizeFirstLetter(text) {
    var res = '';
    var arrText = text.split(' ');

    for (var i = 0; i < arrText.length; i++) {
        res += arrText[i].charAt(0).toUpperCase() + arrText[i].slice(1).toLowerCase() + ' ';
    }

    return res.trim();
}

function showLoading(show) {
    if (show) {
        $('#preloader').removeClass('hide');
        $('#status').removeClass('hide');
    }
    else {
        $('#preloader').addClass('hide');
        $('#status').addClass('hide');
    }
}

function showError(msg) {
    $('#error-dialog-message').text(msg);
    $('#error-modal').modal('show');
}