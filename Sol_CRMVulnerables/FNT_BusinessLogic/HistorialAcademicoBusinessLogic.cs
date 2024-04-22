using System;
using System.Configuration;
using FNT_BusinessEntities;
using FNT_BusinessEntities.WebServiceRespuesta;
using FNT_BusinessEntities.DatosVista;
using FNT_Common.Enum;
using FNT_Common.Resources;
using FNT_Common;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using static FNT_BusinessEntities.WebServiceRespuesta.Banner.DTOAvanceCurricularBannerRespuesta;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Historial Académico.
    /// </summary>
    public class HistorialAcademicoBusinessLogic
    {
        /// <summary>
        /// Obtención y procesamiento de toda la información necesaria para llenar la vista de Inicio (todas las pestañas).
        /// </summary>
        public async Task<DTOHistorialAcademico> getHistorialAcademicoBanner(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabDatosGenerales = new DTOTabDatosGenerales(),
                DTOTabAvanceCurricular = new DTOTabAvanceCurricular(),
                DTOTabHistorialNotas = new DTOTabHistorialNotas(),
                DTOTabHorarioAlumno = new DTOTabHorarioAlumno(),
                DTOTabAvanceNotas = new DTOTabAvanceNotas(),
                DTOTabInasistencias = new DTOTabInasistencias(),
                DTOTabDeudas = new DTOTabDeudas(),
                DTOTabPromedioPonderado = new DTOTabPromedioPonderado(),
                DTOTabTramites = new DTOTabTramites()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno,
                CodModalEst = pc_CodModalEst,
                CodPeriodo = pc_CodPeriodo
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();
            DTOWebServiceRespuestasBanner wsRespuestasBanner = new DTOWebServiceRespuestasBanner();

            try
            {
                //Finalizado
                #region Lectura de datos de alumno
                // Obtención de datos de alumno
                AlumnosBusinessLogic alumnosBusinessLogic = new AlumnosBusinessLogic();
                String cod_nivel_banner = oParams.CodLineaNegocio + "G";
                //DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                DTOAlumnosRespuestaBanner wsAlumnosRespuestaBanner = await alumnosBusinessLogic.getAlumnosBanner(cod_nivel_banner, oParams.CodAlumno);

                //Validación de Respuesta para Get Alumno Banner
                if (wsAlumnosRespuestaBanner.cabecera.codigoRespuesta != "0000")
                    throw new Exception(wsAlumnosRespuestaBanner.cabecera.mensajeRespuesta);
                else if (wsAlumnosRespuestaBanner.detalle == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);
                else if (wsAlumnosRespuestaBanner.detalle.listaAlumno == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);

                wsRespuestasBanner.DTOAlumnosRespuestaBanner = wsAlumnosRespuestaBanner;
                oDatosVista.DTODatosAlumno = PreparandoDataAlumnoBanner(wsRespuestasBanner.DTOAlumnosRespuestaBanner);
                #endregion

                //Parámetros para los servicios de Banner y UClass
                DTOParametrosServiciosBanner oParamsBanner = new DTOParametrosServiciosBanner()
                {
                    CodAlumnoBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoAlumno,
                    CodNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.codigo,
                    DescripcionNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.descripcion,
                    CodProgramaBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().codigo,
                    DescripcionProgramalBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().descripcion,
                    CodPidmBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().pidm,
                    IdBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().idBanner,
                    CodUsuarioBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoUsuario,
                    CodModalidadBanner = pc_CodModalEst,
                };
                //Finalizado
                #region Lectura de datos generales
                // Obtención de datos de hechos importantes
                DTOHechosImportantesRespuesta wsHechosImportantesRespuesta = HechosImportantesBusinessLogic.getHechosImportantes(oParams.CodLineaNegocio, oParams.CodAlumno);
                if (wsHechosImportantesRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    throw new Exception(wsHechosImportantesRespuesta.DTOHeader.DescRetorno);

                //wsRespuestas.DTOHechosImportantesRespuesta = wsHechosImportantesRespuesta;

                //oDatosVista.DTOTabDatosGenerales = PreparandoDataDatosGenerales(wsRespuestas.DTOMatriculasRespuesta, wsRespuestas.DTOAlumnosFacturablesRespuesta, wsRespuestas.DTOCarreraProfesionalRespuesta, wsRespuestas.DTOHechosImportantesRespuesta, oParams.CodModalEst, oParams.CodPeriodo);
                //Armar la información según los servicios de Banner
                MatriculasBusinessLogic matriculasBusinessLogic = new MatriculasBusinessLogic();
                DetalleMatriculaBusinessLogic detalleMatriculaBusinessLogic = new DetalleMatriculaBusinessLogic();
                HistoriaAlumnoBusinessLogic historiaAlumnoBusinessLogic = new HistoriaAlumnoBusinessLogic();

                DTOMatriculaBannerRespuesta wsMatriculaRespuestaBanner = await matriculasBusinessLogic.getMatriculasBanner(oParamsBanner.CodNivelBanner,oParamsBanner.IdBanner, oParamsBanner.CodProgramaBanner);
                DTODetalleMatriculaBannerRespuesta wsDetalleMatriculaRespuestaBanner = await detalleMatriculaBusinessLogic.getDetalleMatriculaBanner(oParamsBanner.CodNivelBanner,oParamsBanner.CodProgramaBanner,oParamsBanner.CodAlumnoBanner);
                DTOHistoriaAlumnoBannerRespuesta wsHistoriaAlumnoBanner = await historiaAlumnoBusinessLogic.getHistoriaAlumnoBanner(oParamsBanner.CodNivelBanner, oParamsBanner.CodProgramaBanner, oParamsBanner.CodPidmBanner);

                oDatosVista.DTOTabDatosGenerales = await PreparandoDataDatosGeneralesBanner(wsRespuestasBanner.DTOAlumnosRespuestaBanner, wsHechosImportantesRespuesta, wsMatriculaRespuestaBanner, wsDetalleMatriculaRespuestaBanner, wsHistoriaAlumnoBanner, oParamsBanner);

                #endregion

                //Finalizado
                #region Lectura de avance curricular
                AvanceCurricularBusinessLogic avanceCurricularBusinessLogic = new AvanceCurricularBusinessLogic();
                //Comodin: Inicio
                DTOAvanceCurricularBannerRespuesta wsAvanceCurricularBanner = await avanceCurricularBusinessLogic.getAvanceCurricularBanner("UG", "UFC_INGI_SP1", "N01716779");
                //Comodin: Fin
                //DTOAvanceCurricularBannerRespuesta wsAvanceCurricularBanner = await avanceCurricularBusinessLogic.getAvanceCurricularBanner(oParamsBanner.CodNivelBanner, oParamsBanner.CodProgramaBanner, oParamsBanner.IdBanner);
                if (wsAvanceCurricularBanner.cabecera.codigoRespuesta != "0000")
                    throw new Exception(wsAvanceCurricularBanner.cabecera.mensajeRespuesta);
                else if (wsAvanceCurricularBanner.detalle == null)
                    throw new Exception(Messages.ErrorInfoAvanceCuirricularBanner + " Nivel : " + oParamsBanner.CodNivelBanner + " Programa : " + oParamsBanner.CodProgramaBanner + " Id Banner: " + oParamsBanner.IdBanner);
                else if (wsAvanceCurricularBanner.detalle.avanceCurricular == null)
                    throw new Exception(Messages.ErrorInfoAvanceCuirricularBanner + " Nivel : " + oParamsBanner.CodNivelBanner + " Programa : " + oParamsBanner.CodProgramaBanner + " Id Banner: " + oParamsBanner.IdBanner);

                oDatosVista.DTOTabAvanceCurricular = PreparandoDataAvanceCurricularBanner(wsAvanceCurricularBanner);
                #endregion

                //Finalizado
                #region Lectura de historial notas

                Detail respuesta = new Detail();
                HistoriaAlumnoBusinessLogic historialNotasBusinessLogic = new HistoriaAlumnoBusinessLogic();
                //Comodin: Inicio
                DTOHistoriaAlumnoBannerRespuesta wsHistorialNotasBanner = await historialNotasBusinessLogic.getHistoriaAlumnoBanner("UG", "UAC_ADMA_SP1", 851269);
                //Comodin: Fin
                //DTOHistoriaAlumnoBannerRespuesta wsHistorialNotasBanner = await historialNotasBusinessLogic.getHistoriaAlumnoBanner(oParamsBanner.CodNivelBanner, oParamsBanner.CodProgramaBanner, oParamsBanner.CodPidmBanner);
                // Agrupados los cursos Agrupados
                Dictionary<string, List<CursoHistoriaAlumno>> cursosAgrupados = respuesta.AgrupamientoCursosPeriodo(wsHistorialNotasBanner);
                oDatosVista.DTOTabHistorialNotas = PreparandoDataHistorialNotasBanner(cursosAgrupados, wsHistorialNotasBanner);
                #endregion

                //Pendiente
                #region Lectura de datos de horario
                // Obtención de datos de horario
                DateTime datAhora = DateTime.Now;
                Int16 diaSemana = Convert.ToInt16(datAhora.DayOfWeek);
                if (diaSemana == 0) diaSemana = 7;
                DateTime datFecha1 = datAhora.AddDays(-diaSemana + 1);
                DateTime datFecha2 = datAhora.AddDays(7 - diaSemana);
                oParams.FechaSesion1 = datFecha1.ToString("yyyy-MM-dd'T'00:00:00Z");
                oParams.FechaSesion2 = datFecha2.ToString("yyyy-MM-dd'T'00:00:00Z");

                DTOHorarioAlumnoResultado wsHorarioRespuesta = HorarioBusinessLogic.getHorario(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, oParams.CodPeriodo, oParams.FechaSesion1, oParams.FechaSesion2);
                //if (wsHorarioRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                //    throw new Exception(wsHorarioRespuesta.DTOHeader.DescRetorno);

                if (wsHorarioRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    wsRespuestas.DTOHorarioAlumnoResultado = wsHorarioRespuesta;
                    oDatosVista.DTOTabHorarioAlumno = PreparandoDataHorario(wsRespuestas.DTOHorarioAlumnoResultado, datFecha1, datFecha2);
                }
                #endregion

                //Finalizado
                #region Lectura de datos de avance de notas y Lectura de datos de inasistencias

                NotasActualesBusinessLogic notasActualesBusinessLogic = new NotasActualesBusinessLogic();
                DTONotasActualesRespuesta wsNotasActualesRespuestaUapi = new DTONotasActualesRespuesta();

                List<DTONotasActualesRespuesta> listwsNotasActualesRespuestaUapi = new List<DTONotasActualesRespuesta>();

                InasistenciasBusinessLogic inasistenciasbusiness = new InasistenciasBusinessLogic();
                DTOInasistenciasResultado wsInasistenciasRespuestaUapi = new DTOInasistenciasResultado();

                List<DTOInasistenciasResultado> listwsInasistenciasRespuestaUapi = new List<DTOInasistenciasResultado>();
                
                string cod_periodo_compuesto_prueba = "UG" + "-" + "202320";

                //Descomentar: Inicio
                //oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(wsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                //foreach (var listaCursosBanner in wsDetalleMatriculaRespuestaBanner.detalle.listaAlumnos.First().listaDetalleMatricula.First().listaCursos)
                //{
                //    string cod_periodo_compuesto = oParamsBanner.CodNivelBanner + "-" + wsMatriculaRespuestaBanner.detalle.listaProductos.First().listaMatriculas.First().periodo;

                //    //Notas Actuales
                //    wsNotasActualesRespuestaUapi = await notasActualesBusinessLogic.getNotasActualesUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                //    listwsNotasActualesRespuestaUapi.Add(wsNotasActualesRespuestaUapi);
                //    //Inasistencias
                //    wsInasistenciasRespuestaUapi = await inasistenciasbusiness.getInasistenciasUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                //    listwsInasistenciasRespuestaUapi.Add(wsInasistenciasRespuestaUapi);
                //}
                //Lenar Datos para Tab Avance de Notas
                //oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(listwsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                //Lenar Datos para Tab Inasistencias
                //oDatosVista.DTOTabInasistencias = PreparandoDataInasistencias(listwsInasistenciasRespuestaUapi);
                //Descomentar: Fin

                //Comodin: Comentar Inicio
                wsNotasActualesRespuestaUapi = await notasActualesBusinessLogic.getNotasActualesUapi(cod_periodo_compuesto_prueba, "202115595", "1ACC0238");
                oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(wsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);

                wsInasistenciasRespuestaUapi = await inasistenciasbusiness.getInasistenciasUapi(cod_periodo_compuesto_prueba, "202115595", "1ACC0238");
                wsRespuestas.DTOInasistenciasResultado = wsInasistenciasRespuestaUapi;

                oDatosVista.DTOTabInasistencias = PreparandoDataInasistencias(wsRespuestas.DTOInasistenciasResultado);
                //Comodin: Comentar Fin
                #endregion

                //Finalizado
                #region Lectura de datos de deudas
                string nombreAlumno = String.Format("{0} {1} {2}",
                    wsRespuestasBanner.DTOAlumnosRespuestaBanner.detalle.listaAlumno.First().nombres,
                    wsRespuestasBanner.DTOAlumnosRespuestaBanner.detalle.listaAlumno.First().apellidos,
                    wsRespuestasBanner.DTOAlumnosRespuestaBanner.detalle.listaAlumno.First().apellidos);

                // Obtención de datos de clientes
                string codUsuarioAux = wsRespuestasBanner.DTOAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoUsuario;
                string codAlumnoAux = wsRespuestasBanner.DTOAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoAlumno;
                if (codUsuarioAux.Length == 7)
                {
                    string primCar = codUsuarioAux.Substring(0, 1);
                    oParams.CodUsuario = primCar + codAlumnoAux;
                }
                else{
                    oParams.CodUsuario = codUsuarioAux;
                }

                DTOClientesResultado wsClientesRespuesta = ClientesBusinessLogic.getClientes(oParams.CodUsuario);

                if (wsClientesRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    if (wsClientesRespuesta.ListaCliente != null)
                    {
                        if (wsClientesRespuesta.ListaCliente.FirstOrDefault() != null)
                        {
                            oParams.ClienteCobrara = wsClientesRespuesta.ListaCliente.First().PERSONA;
                            wsRespuestas.DTOClientesResultado = wsClientesRespuesta;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(oParams.ClienteCobrara))
                {
                    DTODocumentoFiscalResultado wsDocumentoFiscalResultado = DocumentoFiscalBusinessLogic.getDocumentoFiscal(oParams.ClienteCobrara);
                  
                    if (wsDocumentoFiscalResultado.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                    {
                        if (wsDocumentoFiscalResultado.ListaCriteria != null)
                        {
                            wsRespuestas.DTODocumentoFiscalResultado = wsDocumentoFiscalResultado;
                            oDatosVista.DTOTabDeudas = PreparandoDataDeudas(wsRespuestas.DTODocumentoFiscalResultado, oParams.CodAlumno, nombreAlumno);
                        }
                    }
                }
                #endregion

                //Pendiente
                #region Lectura de datos de promedio ponderado
                //oDatosVista.DTOTabPromedioPonderado = PreparandoDataPromedioPonderado(tmpListaMatriculaDet);
                oDatosVista.DTOTabPromedioPonderado = new DTOTabPromedioPonderado();
                #endregion

                //Finalizado se oculto Datos de EPG
                #region trámites
                DTOTramitesResultado wsTramitesMiUpc = TramitesBusinessLogic.getTramites(ConfigurationManager.AppSettings["Ws_tramitator_mi_upc"], oParams.CodLineaNegocio, oParams.CodModalEst, oParams.CodAlumno);
                DTOTramitesResultado wsTramitesIntranet = TramitesBusinessLogic.getTramites(ConfigurationManager.AppSettings["Ws_tramitator_intranet"], oParams.CodLineaNegocio, oParams.CodModalEst, oParams.CodAlumno);
                //DTOTramitesResultado wsTramitesEpg = TramitesBusinessLogic.getTramites(ConfigurationManager.AppSettings["Ws_tramitator_epe"], oParams.CodLineaNegocio, oParams.CodModalEst, oParams.CodAlumno);

                if (wsTramitesMiUpc.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    wsTramitesMiUpc = new DTOTramitesResultado();
                if (wsTramitesIntranet.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    wsTramitesIntranet = new DTOTramitesResultado();

                wsRespuestas.DTOTramitesMiUpcRespuesta = wsTramitesMiUpc;
                wsRespuestas.DTOTramitesIntranetRespuesta = wsTramitesIntranet;

                oDatosVista.DTOTabTramites = PreparandoDataTramites(wsRespuestas.DTOTramitesMiUpcRespuesta, wsRespuestas.DTOTramitesIntranetRespuesta, wsRespuestas.DTOTramitesEpgRespuesta);
                #endregion

                oHistorialAcademico.RespuestaExitosa = true;
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            oHistorialAcademico.ParametrosServicios = oParams;
            oHistorialAcademico.DatosVista = oDatosVista;

            return oHistorialAcademico;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pc_CodLineaNegocio"></param>
        /// <param name="pc_CodAlumno"></param>
        /// <param name="pc_CodModalEst"></param>
        /// <param name="pc_CodPeriodo"></param>
        /// <returns></returns>
        public async Task<DTOHistorialAcademico> getNotasActualesBanner(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabDatosGenerales = new DTOTabDatosGenerales(),
                DTOTabAvanceCurricular = new DTOTabAvanceCurricular(),
                DTOTabHistorialNotas = new DTOTabHistorialNotas(),
                DTOTabHorarioAlumno = new DTOTabHorarioAlumno(),
                DTOTabAvanceNotas = new DTOTabAvanceNotas(),
                DTOTabInasistencias = new DTOTabInasistencias(),
                DTOTabDeudas = new DTOTabDeudas(),
                DTOTabPromedioPonderado = new DTOTabPromedioPonderado(),
                DTOTabTramites = new DTOTabTramites()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno,
                CodModalEst = pc_CodModalEst,
                CodPeriodo = pc_CodPeriodo
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();
            DTOWebServiceRespuestasBanner wsRespuestasBanner = new DTOWebServiceRespuestasBanner();

            try
            {
                #region Lectura de datos de alumno
                // Obtención de datos de alumno
                AlumnosBusinessLogic alumnosBusinessLogic = new AlumnosBusinessLogic();
                String cod_nivel_banner = oParams.CodLineaNegocio + "G";
                //DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                DTOAlumnosRespuestaBanner wsAlumnosRespuestaBanner = await alumnosBusinessLogic.getAlumnosBanner(cod_nivel_banner, oParams.CodAlumno);

                //Validación de Respuesta para Get Alumno Banner
                if (wsAlumnosRespuestaBanner.cabecera.codigoRespuesta != "0000")
                    throw new Exception(wsAlumnosRespuestaBanner.cabecera.mensajeRespuesta);
                else if (wsAlumnosRespuestaBanner.detalle == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);
                else if (wsAlumnosRespuestaBanner.detalle.listaAlumno == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);

                wsRespuestasBanner.DTOAlumnosRespuestaBanner = wsAlumnosRespuestaBanner;
                oDatosVista.DTODatosAlumno = PreparandoDataAlumnoBanner(wsRespuestasBanner.DTOAlumnosRespuestaBanner);
                #endregion

                DTOParametrosServiciosBanner oParamsBanner = new DTOParametrosServiciosBanner()
                {
                    CodAlumnoBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoAlumno,
                    CodNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.codigo,
                    DescripcionNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.descripcion,
                    CodProgramaBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().codigo,
                    DescripcionProgramalBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().descripcion,
                    CodPidmBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().pidm,
                    IdBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().idBanner,
                    CodUsuarioBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoUsuario,
                    CodModalidadBanner = pc_CodModalEst,
                };

                //Consulta de servicios
                MatriculasBusinessLogic matriculasBusinessLogic = new MatriculasBusinessLogic();
                DetalleMatriculaBusinessLogic detalleMatriculaBusinessLogic = new DetalleMatriculaBusinessLogic();

                DTOMatriculaBannerRespuesta wsMatriculaRespuestaBanner = await matriculasBusinessLogic.getMatriculasBanner(oParamsBanner.CodNivelBanner, oParamsBanner.IdBanner, oParamsBanner.CodProgramaBanner);
                DTODetalleMatriculaBannerRespuesta wsDetalleMatriculaRespuestaBanner = await detalleMatriculaBusinessLogic.getDetalleMatriculaBanner(oParamsBanner.CodNivelBanner, oParamsBanner.CodProgramaBanner, oParamsBanner.CodAlumnoBanner);
               

                #region Lectura de Notas Actuales
                NotasActualesBusinessLogic notasActualesBusinessLogic = new NotasActualesBusinessLogic();
                DTONotasActualesRespuesta wsNotasActualesRespuestaUapi = new DTONotasActualesRespuesta();

                List<DTONotasActualesRespuesta> listwsNotasActualesRespuestaUapi = new List<DTONotasActualesRespuesta>();

                InasistenciasBusinessLogic inasistenciasbusiness = new InasistenciasBusinessLogic();
                DTOInasistenciasResultado wsInasistenciasRespuestaUapi = new DTOInasistenciasResultado();

                List<DTOInasistenciasResultado> listwsInasistenciasRespuestaUapi = new List<DTOInasistenciasResultado>();

                string cod_periodo_compuesto_prueba = "UG" + "-" + "202320";

                //Descomentar: Inicio
                oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(wsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                foreach (var listaCursosBanner in wsDetalleMatriculaRespuestaBanner.detalle.listaAlumnos.First().listaDetalleMatricula.First().listaCursos)
                {
                    string cod_periodo_compuesto = oParamsBanner.CodNivelBanner + "-" + wsMatriculaRespuestaBanner.detalle.listaProductos.First().listaMatriculas.First().periodo;

                    //Notas Actuales
                    wsNotasActualesRespuestaUapi = await notasActualesBusinessLogic.getNotasActualesUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                    listwsNotasActualesRespuestaUapi.Add(wsNotasActualesRespuestaUapi);
                    //Inasistencias
                    wsInasistenciasRespuestaUapi = await inasistenciasbusiness.getInasistenciasUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                    listwsInasistenciasRespuestaUapi.Add(wsInasistenciasRespuestaUapi);
                }
                //Lenar Datos para Tab Avance de Notas
                oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(listwsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                //Descomentar: Fin

                //Comodin: Comentar Inicio
                //wsNotasActualesRespuestaUapi = await notasActualesBusinessLogic.getNotasActualesUapi(cod_periodo_compuesto_prueba, "202115595", "1ACC0238");
                //oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(wsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                //Comodin: Comentar Fin
                #endregion
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            return oHistorialAcademico;
        }

        public async Task<DTOHistorialAcademico> getInasistenciasBanner(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabDatosGenerales = new DTOTabDatosGenerales(),
                DTOTabAvanceCurricular = new DTOTabAvanceCurricular(),
                DTOTabHistorialNotas = new DTOTabHistorialNotas(),
                DTOTabHorarioAlumno = new DTOTabHorarioAlumno(),
                DTOTabAvanceNotas = new DTOTabAvanceNotas(),
                DTOTabInasistencias = new DTOTabInasistencias(),
                DTOTabDeudas = new DTOTabDeudas(),
                DTOTabPromedioPonderado = new DTOTabPromedioPonderado(),
                DTOTabTramites = new DTOTabTramites()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno,
                CodModalEst = pc_CodModalEst,
                CodPeriodo = pc_CodPeriodo
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();
            DTOWebServiceRespuestasBanner wsRespuestasBanner = new DTOWebServiceRespuestasBanner();

            try
            {
                #region Lectura de datos de alumno
                // Obtención de datos de alumno
                AlumnosBusinessLogic alumnosBusinessLogic = new AlumnosBusinessLogic();
                String cod_nivel_banner = oParams.CodLineaNegocio + "G";
                //DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                DTOAlumnosRespuestaBanner wsAlumnosRespuestaBanner = await alumnosBusinessLogic.getAlumnosBanner(cod_nivel_banner, oParams.CodAlumno);

                //Validación de Respuesta para Get Alumno Banner
                if (wsAlumnosRespuestaBanner.cabecera.codigoRespuesta != "0000")
                    throw new Exception(wsAlumnosRespuestaBanner.cabecera.mensajeRespuesta);
                else if (wsAlumnosRespuestaBanner.detalle == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);
                else if (wsAlumnosRespuestaBanner.detalle.listaAlumno == null)
                    throw new Exception(Messages.ErrorInfoAlumnoBanner);

                wsRespuestasBanner.DTOAlumnosRespuestaBanner = wsAlumnosRespuestaBanner;
                oDatosVista.DTODatosAlumno = PreparandoDataAlumnoBanner(wsRespuestasBanner.DTOAlumnosRespuestaBanner);
                #endregion

                DTOParametrosServiciosBanner oParamsBanner = new DTOParametrosServiciosBanner()
                {
                    CodAlumnoBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoAlumno,
                    CodNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.codigo,
                    DescripcionNivelBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().nivel.descripcion,
                    CodProgramaBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().codigo,
                    DescripcionProgramalBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().programas.First().descripcion,
                    CodPidmBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().pidm,
                    IdBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().idBanner,
                    CodUsuarioBanner = wsAlumnosRespuestaBanner.detalle.listaAlumno.First().codigoUsuario,
                    CodModalidadBanner = pc_CodModalEst,
                };

                //Consulta de servicios
                MatriculasBusinessLogic matriculasBusinessLogic = new MatriculasBusinessLogic();
                DetalleMatriculaBusinessLogic detalleMatriculaBusinessLogic = new DetalleMatriculaBusinessLogic();

                DTOMatriculaBannerRespuesta wsMatriculaRespuestaBanner = await matriculasBusinessLogic.getMatriculasBanner(oParamsBanner.CodNivelBanner, oParamsBanner.IdBanner, oParamsBanner.CodProgramaBanner);
                DTODetalleMatriculaBannerRespuesta wsDetalleMatriculaRespuestaBanner = await detalleMatriculaBusinessLogic.getDetalleMatriculaBanner(oParamsBanner.CodNivelBanner, oParamsBanner.CodProgramaBanner, oParamsBanner.CodAlumnoBanner);

                #region Lectura de datos de inasistencias
                NotasActualesBusinessLogic notasActualesBusinessLogic = new NotasActualesBusinessLogic();
                DTONotasActualesRespuesta wsNotasActualesRespuestaUapi = new DTONotasActualesRespuesta();

                List<DTONotasActualesRespuesta> listwsNotasActualesRespuestaUapi = new List<DTONotasActualesRespuesta>();

                InasistenciasBusinessLogic inasistenciasbusiness = new InasistenciasBusinessLogic();
                DTOInasistenciasResultado wsInasistenciasRespuestaUapi = new DTOInasistenciasResultado();

                List<DTOInasistenciasResultado> listwsInasistenciasRespuestaUapi = new List<DTOInasistenciasResultado>();

                string cod_periodo_compuesto_prueba = "UG" + "-" + "202320";

                //Descomentar: Inicio
                oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotasBanner(wsNotasActualesRespuestaUapi, wsDetalleMatriculaRespuestaBanner);
                foreach (var listaCursosBanner in wsDetalleMatriculaRespuestaBanner.detalle.listaAlumnos.First().listaDetalleMatricula.First().listaCursos)
                {
                    string cod_periodo_compuesto = oParamsBanner.CodNivelBanner + "-" + wsMatriculaRespuestaBanner.detalle.listaProductos.First().listaMatriculas.First().periodo;

                    //Notas Actuales
                    wsNotasActualesRespuestaUapi = await notasActualesBusinessLogic.getNotasActualesUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                    listwsNotasActualesRespuestaUapi.Add(wsNotasActualesRespuestaUapi);
                    //Inasistencias
                    wsInasistenciasRespuestaUapi = await inasistenciasbusiness.getInasistenciasUapi(cod_periodo_compuesto, oParamsBanner.CodAlumnoBanner, listaCursosBanner.materia.codigo + listaCursosBanner.numeroCurso);
                    listwsInasistenciasRespuestaUapi.Add(wsInasistenciasRespuestaUapi);
                }
                //Lenar Datos para Tab Inasistencias
                oDatosVista.DTOTabInasistencias = PreparandoDataInasistencias(listwsInasistenciasRespuestaUapi);
                //Descomentar: Fin

                //Comodin: Comentar Inicio
                //wsInasistenciasRespuestaUapi = await inasistenciasbusiness.getInasistenciasUapi(cod_periodo_compuesto_prueba, "202115595", "1ACC0238");
                //wsRespuestas.DTOInasistenciasResultado = wsInasistenciasRespuestaUapi;
                //oDatosVista.DTOTabInasistencias = PreparandoDataInasistencias(wsRespuestas.DTOInasistenciasResultado);
                //Comodin: Comentar Fin
                #endregion
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            return oHistorialAcademico;
        }

        private DTOTabHistorialNotas PreparandoDataHistorialNotasBanner(Dictionary<string, List<CursoHistoriaAlumno>> cursosAgrupados, DTOHistoriaAlumnoBannerRespuesta pc_Historia_AlumnoBanner)
        {
            DTOTabHistorialNotas oDTOTabHistorialNotas = new DTOTabHistorialNotas()
            {
                listaHistorialNotas = new List<DTOHistorialNotas>()
            };

            DTOHistorialNotas dTOHistorialNotas;
            DTOHistorialNotasDet dTOHistorialNotasDet;
            foreach (var cabecera in cursosAgrupados)
            {
                dTOHistorialNotas = new DTOHistorialNotas();

                dTOHistorialNotas.Periodo = cabecera.Key;
                dTOHistorialNotas.Carrera = pc_Historia_AlumnoBanner.detalle.listaHistoriaAlumno.First().programa.descripcionEspecial;
                foreach(var detalle in cabecera.Value)
                {
                    dTOHistorialNotasDet = new DTOHistorialNotasDet();

                    dTOHistorialNotasDet.CodigoCurso = detalle.materia.codigo + detalle.numeroCurso;
                    dTOHistorialNotasDet.DescripcionCurso = detalle.nombreLargo;
                    dTOHistorialNotasDet.Creditos = detalle.totalCreditos.ToString();
                    dTOHistorialNotasDet.CodigoCurso = detalle.materia.codigo + detalle.numeroCurso;
                    dTOHistorialNotasDet.PromedioFinal = detalle.calificacion.notaFinal;
                    dTOHistorialNotasDet.NumeroVeces = "";
                    dTOHistorialNotasDet.Aprobado = "";

                    dTOHistorialNotas.listaHistorialNotasDet.Add(dTOHistorialNotasDet);
                }
                oDTOTabHistorialNotas.listaHistorialNotas.Add(dTOHistorialNotas);
            }

            return oDTOTabHistorialNotas;
        }

        private DTOTabAvanceCurricular PreparandoDataAvanceCurricularBanner(DTOAvanceCurricularBannerRespuesta wsAvanceCurricularBanner)
        {
            List<Area> listaAreasObligatorias = wsAvanceCurricularBanner.detalle.avanceCurricular.listaAreasObligatorias;
            DTOTabAvanceCurricular oDTOTabAvanceCurricular = new DTOTabAvanceCurricular()
            {
                listaAvanceCurricularCiclos = new List<DTOAvanceCurricularCiclo>()
            };

            DTOAvanceCurricularCiclo dTOAvanceCurricularCiclo;
            DTOAvanceCurricularDet avanceCurricular; 
            foreach (var lista in listaAreasObligatorias)
            {
                dTOAvanceCurricularCiclo = new DTOAvanceCurricularCiclo();
                dTOAvanceCurricularCiclo.Ciclo= lista.nombre.Substring(lista.nombre.Length - 2);

                foreach(var listareglas in lista.listaReglas)
                {
                    avanceCurricular = new DTOAvanceCurricularDet();
                    avanceCurricular.CodCurso = listareglas.nombre;
                    avanceCurricular.DescCurso = listareglas.descripcion;
                    
                    foreach(var listacursos in listareglas.listaCursos)
                    {
                        avanceCurricular.NotaCurso = listacursos.calificacion.notaFinal;
                        avanceCurricular.CodPeriodo = listacursos.codigoPeriodo;

                    }
                    avanceCurricular.EstadoAprob = listareglas.cumplido == "SI" ? "CUMPLIDO" : "PENDIENTE";

                    dTOAvanceCurricularCiclo.listaDTOAvanceCurricular.Add(avanceCurricular);
                }

                oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.Add(dTOAvanceCurricularCiclo);
            }

            return oDTOTabAvanceCurricular;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabTramites para poblar los datos de la pestaña de "Trámites".
        /// </summary>
        /// <returns></returns>
        private static DTOTabTramites PreparandoDataTramites(DTOTramitesResultado wsTramitesMiUpc, DTOTramitesResultado wsTramitesIntranet, DTOTramitesResultado wsTramitesEpg)
        {
            DTOTabTramites tramitesResultado = new DTOTabTramites();

            tramitesResultado.DTOTramitesMiUpc = wsTramitesMiUpc;
            tramitesResultado.DTOTramitesIntranet = wsTramitesIntranet;
            tramitesResultado.DTOTramitesEpg = new DTOTramitesResultado();

            return tramitesResultado;
        }

        /// <summary>
        /// Obtención y procesamiento de toda la información necesaria para llenar la vista de Notas actuales.
        /// </summary>
        public static DTOHistorialAcademico getNotasActuales(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabAvanceNotas = new DTOTabAvanceNotas()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno,
                CodModalEst = pc_CodModalEst,
                CodPeriodo = pc_CodPeriodo
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();

            try
            {
                #region Lectura de datos de alumno
                // Obtención de datos de alumno
                DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                if (wsAlumnosRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    throw new Exception(wsAlumnosRespuesta.DTOHeader.DescRetorno);
                else if (wsAlumnosRespuesta.ListaDTOAlumnos == null)
                    throw new Exception(Messages.ErrorInfoAlumno);
                else if (wsAlumnosRespuesta.ListaDTOAlumnos.FirstOrDefault() == null)
                    throw new Exception(Messages.ErrorInfoAlumno);

                wsRespuestas.DTOAlumnosRespuesta = wsAlumnosRespuesta;

                oDatosVista.DTODatosAlumno = PreparandoDataAlumno(wsRespuestas.DTOAlumnosRespuesta);
                #endregion

                #region Lectura de datos de avance de notas
                // Obtención de datos de matrículas
                DTOMatriculasRespuesta wsMatriculasRespuesta = MatriculasBusinessLogic.getMatriculas(oParams.CodLineaNegocio, oParams.CodAlumno);
                if (wsMatriculasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    throw new Exception(wsMatriculasRespuesta.DTOHeader.DescRetorno);

                // Filtrar resultados de matrículas por CodModalEst
                if (wsMatriculasRespuesta.ListaDTOMatricula != null)
                {
                    for (int i = wsMatriculasRespuesta.ListaDTOMatricula.Count - 1; i >= 0; i--)
                    {
                        if (wsMatriculasRespuesta.ListaDTOMatricula[i].DTOMatriculaCab.CodModalEst != oParams.CodModalEst)
                            wsMatriculasRespuesta.ListaDTOMatricula.RemoveAt(i);
                    }
                    if (wsMatriculasRespuesta.ListaDTOMatricula.Count == 0)
                        throw new Exception(Messages.ErrorInfoMatriculas);
                }
                else
                    throw new Exception(Messages.ErrorInfoMatriculas);

                List<DTOMatriculaDet> tmpListaMatriculaDet = wsMatriculasRespuesta.ListaDTOMatricula.First().ListaDTOMatriculaDet;
                if (tmpListaMatriculaDet.Count > 0)
                {
                    DTOMatriculaDet tmpMatriculaDet = tmpListaMatriculaDet.Where(p => p.CodPeriodMat == oParams.CodPeriodo).FirstOrDefault();
                    if (tmpMatriculaDet == null)
                    {
                        // Si el parámetro url de periodo no coincide con ningún periodo existente, se usa el ultimo periodo
                        tmpMatriculaDet = tmpListaMatriculaDet.OrderByDescending(p => p.CodPeriodMat).First();
                        oParams.CodPeriodo = tmpMatriculaDet.CodPeriodMat;
                    }

                    oParams.CodProducto = tmpMatriculaDet.CodProducMat;
                    oParams.CodCurriculo = tmpMatriculaDet.CodCurricMAt.Value.ToString();
                }
                else
                    throw new Exception(Messages.ErrorInfoMatriculas);

                wsRespuestas.DTOMatriculasRespuesta = wsMatriculasRespuesta;

                // Obtención de datos de matrículas
                DTOMallaCurricularRespuesta wsMallaCurricular = MallaCurricularBusinessLogic.getMallaCurricular(oParams.CodLineaNegocio, oParams.CodProducto, oParams.CodCurriculo);
                if (wsMallaCurricular.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    throw new Exception(wsMallaCurricular.DTOHeader.DescRetorno);

                wsRespuestas.DTOMallaCurricularRespuesta = wsMallaCurricular;

                // Obtención de datos de detalle de matrícula para cada periodo encontrado
                DTODetMatriculaResultado wsDetalleMatricula = new DTODetMatriculaResultado()
                {
                    ListaDTODetMatriculaOBJ = new List<DTODetMatricula>()
                };

                List<string> listPeriodos = new List<string>();
                foreach (var madet in tmpListaMatriculaDet)
                    if (!listPeriodos.Contains(madet.CodPeriodMat))
                        listPeriodos.Add(madet.CodPeriodMat);
                listPeriodos = listPeriodos.OrderByDescending(p => p).ToList();

                DTODetMatriculaResultado tmpWsDetMat;
                bool primerDetMat = true;

                foreach (var per in listPeriodos)
                {
                    tmpWsDetMat = new DTODetMatriculaResultado();
                    tmpWsDetMat = DetalleMatriculaBusinessLogic.getDetalleMatricula(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, per);
                    if (tmpWsDetMat.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString() ||
                        tmpWsDetMat.ListaDTODetMatriculaOBJ.FirstOrDefault() == null)
                        continue;
                    else
                    {
                        if (primerDetMat)
                        {
                            wsDetalleMatricula.DTOHeader = tmpWsDetMat.DTOHeader;
                            primerDetMat = false;
                        }
                        wsDetalleMatricula.ListaDTODetMatriculaOBJ.AddRange(tmpWsDetMat.ListaDTODetMatriculaOBJ);
                    }
                }

                wsRespuestas.DTODetMatriculaResultado = wsDetalleMatricula;

                // Obtención de datos de avance de notas de todos los periodos
                //DTOAvanceNotasRespuesta wsAvanceNotasRespuesta = new DTOAvanceNotasRespuesta()
                //{
                //    ListaNota = new List<DTOListaNota>()
                //};
                //DTOAvanceNotasRespuesta tmpWsAvNot;
                //bool primerAvNot = true;

                //foreach (var per in listPeriodos)
                //{
                //    tmpWsAvNot = new DTOAvanceNotasRespuesta();
                //    tmpWsAvNot = AvanceNotasBusinessLogic.getAvanceNotas(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, per);
                //    if (tmpWsAvNot.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString() ||
                //        tmpWsAvNot.ListaNota.FirstOrDefault() == null)
                //        continue;
                //    else
                //    {
                //        if (primerAvNot)
                //        {
                //            wsAvanceNotasRespuesta.DTOHeader = tmpWsAvNot.DTOHeader;
                //            wsAvanceNotasRespuesta.AvanceNota = tmpWsAvNot.AvanceNota;
                //            primerAvNot = false;
                //        }
                //        wsAvanceNotasRespuesta.ListaNota.AddRange(tmpWsAvNot.ListaNota);
                //    }
                //}

                //if (wsAvanceNotasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                //{
                //    if (wsAvanceNotasRespuesta.ListaNota != null)
                //    {
                //        if (wsAvanceNotasRespuesta.ListaNota.Count > 0)
                //        {
                //            wsRespuestas.DTOAvanceNotasRespuesta = wsAvanceNotasRespuesta;
                //            oDatosVista.DTOTabAvanceNotas = PreparandoAvanceNotas(wsRespuestas.DTOAvanceNotasRespuesta, wsRespuestas.DTODetMatriculaResultado, wsRespuestas.DTOMallaCurricularRespuesta);
                //        }
                //    }
                //}

                // Obtención de datos de avance de notas del periodo seleccionado por url
                DTOAvanceNotasRespuesta wsAvanceNotasRespuesta = AvanceNotasBusinessLogic.getAvanceNotas(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, oParams.CodPeriodo);

                if (wsAvanceNotasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    if (wsAvanceNotasRespuesta.ListaNota != null)
                    {
                        if (wsAvanceNotasRespuesta.ListaNota.Count > 0)
                        {
                            wsRespuestas.DTOAvanceNotasRespuesta = wsAvanceNotasRespuesta;
                            oDatosVista.DTOTabAvanceNotas = PreparandoDataAvanceNotas(wsRespuestas.DTOAvanceNotasRespuesta, wsRespuestas.DTODetMatriculaResultado, wsRespuestas.DTOMallaCurricularRespuesta);
                        }
                    }
                }
                #endregion

                oHistorialAcademico.RespuestaExitosa = true;
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            oHistorialAcademico.ParametrosServicios = oParams;
            oHistorialAcademico.DatosVista = oDatosVista;

            return oHistorialAcademico;
        }

        /// <summary>
        /// Obtención y procesamiento de toda la información necesaria para llenar la vista de Inasistencias.
        /// </summary>
        public static DTOHistorialAcademico getInasistencias(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabInasistencias = new DTOTabInasistencias()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno,
                CodModalEst = pc_CodModalEst,
                CodPeriodo = pc_CodPeriodo
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();

            try
            {
                #region Lectura de datos de alumno
                // Obtención de datos de alumno
                DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                if (wsAlumnosRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    throw new Exception(wsAlumnosRespuesta.DTOHeader.DescRetorno);
                else if (wsAlumnosRespuesta.ListaDTOAlumnos == null)
                    throw new Exception(Messages.ErrorInfoAlumno);
                else if (wsAlumnosRespuesta.ListaDTOAlumnos.FirstOrDefault() == null)
                    throw new Exception(Messages.ErrorInfoAlumno);

                wsRespuestas.DTOAlumnosRespuesta = wsAlumnosRespuesta;

                oDatosVista.DTODatosAlumno = PreparandoDataAlumno(wsRespuestas.DTOAlumnosRespuesta);
                #endregion

                #region Lectura de datos de avance de notas
                // Obtención de datos de avance de notas del periodo seleccionado por url
                DTOAvanceNotasRespuesta wsAvanceNotasRespuesta = AvanceNotasBusinessLogic.getAvanceNotas(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, oParams.CodPeriodo);

                if (wsAvanceNotasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    if (wsAvanceNotasRespuesta.ListaNota != null)
                    {
                        if (wsAvanceNotasRespuesta.ListaNota.Count > 0)
                        {
                            wsRespuestas.DTOAvanceNotasRespuesta = wsAvanceNotasRespuesta;
                        }
                    }
                }
                #endregion

                #region Lectura de datos de inasistencias
                // Obtención de datos de inasistencias
                DTOInasistenciasResultado wsInasistenciasRespuesta = InasistenciasBusinessLogic.getInasistencias(oParams.CodLineaNegocio, oParams.CodAlumno, oParams.CodModalEst, oParams.CodPeriodo);
                //if (wsInasistenciasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                //    throw new Exception(wsInasistenciasRespuesta.DTOHeader.DescRetorno);

                if (wsInasistenciasRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    if (wsInasistenciasRespuesta.ListaDTOInasistenciasAlumnos != null)
                    {
                        wsRespuestas.DTOInasistenciasResultado = wsInasistenciasRespuesta;
                        oDatosVista.DTOTabInasistencias = PreparandoDataInasistencias(wsRespuestas.DTOInasistenciasResultado, wsRespuestas.DTOAvanceNotasRespuesta);
                    }
                }
                #endregion

                oHistorialAcademico.RespuestaExitosa = true;
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            oHistorialAcademico.ParametrosServicios = oParams;
            oHistorialAcademico.DatosVista = oDatosVista;

            return oHistorialAcademico;
        }


        /// <summary>
        /// Obtención y procesamiento de toda la información necesaria para llenar la vista de Pagos pendientes.
        /// </summary>
        public static DTOHistorialAcademico getPagosPendientes(string pc_CodLineaNegocio, string pc_CodAlumno)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            DTOHistorialAcademico oHistorialAcademico = new DTOHistorialAcademico();

            DTODatosVista oDatosVista = new DTODatosVista()
            {
                DTODatosAlumno = new DTODatosAlumno(),
                DTOTabDeudas = new DTOTabDeudas()
            };

            DTOParametrosServicios oParams = new DTOParametrosServicios()
            {
                CodLineaNegocio = pc_CodLineaNegocio,
                CodAlumno = pc_CodAlumno
            };

            DTOWebServiceRespuestas wsRespuestas = new DTOWebServiceRespuestas();

            try
            {
                string nombreAlumno = string.Empty;

                //JV 23/05/2017 
                if (oParams.CodLineaNegocio.Equals("E")) {

                    oParams.CodUsuario = oParams.CodLineaNegocio + oParams.CodAlumno;
                }
                else {
                    //alumno modalidad U

                    #region Lectura de datos de alumno
                    // Obtención de datos de alumno
                    DTOAlumnosRespuesta wsAlumnosRespuesta = AlumnosBusinessLogic.getAlumnos(oParams.CodLineaNegocio, oParams.CodAlumno);
                    if (wsAlumnosRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                        throw new Exception(wsAlumnosRespuesta.DTOHeader.DescRetorno);
                    else if (wsAlumnosRespuesta.ListaDTOAlumnos == null)
                        throw new Exception(Messages.ErrorInfoAlumno);
                    else if (wsAlumnosRespuesta.ListaDTOAlumnos.FirstOrDefault() == null)
                        throw new Exception(Messages.ErrorInfoAlumno);

                    wsRespuestas.DTOAlumnosRespuesta = wsAlumnosRespuesta;

                    oDatosVista.DTODatosAlumno = PreparandoDataAlumno(wsRespuestas.DTOAlumnosRespuesta);
                    #endregion

                    // Lectura de datos de deudas

                    nombreAlumno = String.Format("{0} {1} {2}",
                        wsRespuestas.DTOAlumnosRespuesta.ListaDTOAlumnos.First().Nombres,
                        wsRespuestas.DTOAlumnosRespuesta.ListaDTOAlumnos.First().ApellidoPatern,
                        wsRespuestas.DTOAlumnosRespuesta.ListaDTOAlumnos.First().ApellidoMatern);

                    // Obtención de datos de clientes
                    string codUsuarioAux = wsRespuestas.DTOAlumnosRespuesta.ListaDTOAlumnos.First().CodUsuario;
                    string codAlumnoAux = wsRespuestas.DTOAlumnosRespuesta.ListaDTOAlumnos.First().CodAlumno;
                    if (codUsuarioAux.Length == 7)
                    {
                        string primCar = codUsuarioAux.Substring(0, 1);
                        oParams.CodUsuario = primCar + codAlumnoAux;
                    }
                    else
                    {
                        oParams.CodUsuario = codUsuarioAux;
                    }
                }

               
                DTOClientesResultado wsClientesRespuesta = ClientesBusinessLogic.getClientes(oParams.CodUsuario);
                //if (wsClientesRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                //    throw new Exception(wsClientesRespuesta.DTOHeader.DescRetorno);

                if (wsClientesRespuesta.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                {
                    if (wsClientesRespuesta.ListaCliente != null)
                    {
                        if (wsClientesRespuesta.ListaCliente.FirstOrDefault() != null)
                        {
                            oParams.ClienteCobrara = wsClientesRespuesta.ListaCliente.First().PERSONA;
                            wsRespuestas.DTOClientesResultado = wsClientesRespuesta;
                        }
                    }
                }



                if (!String.IsNullOrEmpty(oParams.ClienteCobrara))
                {
                    // Obtención de datos de documento fiscal del cliente
                    DTODocumentoFiscalResultado wsDocumentoFiscalResultado = DocumentoFiscalBusinessLogic.getDocumentoFiscal(oParams.ClienteCobrara);
                    //if (wsDocumentoFiscalResultado.DTOHeader.CodigoRetorno == HeaderEnum.Incorrecto.ToString())
                    //    throw new Exception(wsDocumentoFiscalResultado.DTOHeader.DescRetorno);

                    if (wsDocumentoFiscalResultado.DTOHeader.CodigoRetorno == HeaderEnum.Correcto.ToString())
                    {
                        if (wsDocumentoFiscalResultado.ListaCriteria != null)
                        {
                            wsRespuestas.DTODocumentoFiscalResultado = wsDocumentoFiscalResultado;
                            oDatosVista.DTOTabDeudas = PreparandoDataDeudas(wsRespuestas.DTODocumentoFiscalResultado, oParams.CodAlumno, nombreAlumno);
                        }
                    }
                }
              

                oHistorialAcademico.RespuestaExitosa = true;
            }
            catch (Exception ex)
            {
                oHistorialAcademico.RespuestaExitosa = false;
                oHistorialAcademico.MensajeError = ex.Message;
            }

            oHistorialAcademico.ParametrosServicios = oParams;
            oHistorialAcademico.DatosVista = oDatosVista;

            return oHistorialAcademico;
        }

        // Preparando data

        /// <summary>
        /// Llenado de datos de la clase DTODatosAlumno para poblar los datos del alumno en la zona superior derecha de la pantalla.
        /// </summary>
        private static DTODatosAlumno PreparandoDataAlumno(DTOAlumnosRespuesta pc_AlumnosRespuesta)
        {
            DTODatosAlumno oDTODatosAlumno = new DTODatosAlumno();

            DTOAlumnos alumno = pc_AlumnosRespuesta.ListaDTOAlumnos.First();
            oDTODatosAlumno.Nombres = alumno.Nombres;
            oDTODatosAlumno.ApellidoPaterno = alumno.ApellidoPatern;
            oDTODatosAlumno.ApellidoMaterno = alumno.ApellidoMatern;
            oDTODatosAlumno.CodigoUsuario = alumno.CodUsuario;
            oDTODatosAlumno.UrlImagen = alumno.FotoUrl;

            return oDTODatosAlumno;
        }
        /// <summary>
        /// Llenado de datos de la clase DTODatosAlumno de banner para poblar los datos del alumno en la zona superior derecha de la pantalla.
        /// </summary>
        /// <param name="pc_AlumnosRespuesta"></param>
        /// <returns></returns>
        private static DTODatosAlumno PreparandoDataAlumnoBanner(DTOAlumnosRespuestaBanner pc_AlumnosRespuesta)
        {
            DTODatosAlumno oDTODatosAlumno = new DTODatosAlumno();

            AlumnoBanner alumno = pc_AlumnosRespuesta.detalle.listaAlumno.First();
            oDTODatosAlumno.Nombres = alumno.nombres;
            oDTODatosAlumno.ApellidoPaterno = alumno.apellidos;
            //oDTODatosAlumno.ApellidoMaterno = alumno.ApellidoMatern;
            oDTODatosAlumno.CodigoUsuario = alumno.codigoUsuario;
            oDTODatosAlumno.UrlImagen = alumno.fotoAlumno;

            return oDTODatosAlumno;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabDatosGenerales para poblar los datos de la pestaña de "Datos generales".
        /// </summary>
        private static DTOTabDatosGenerales PreparandoDataDatosGenerales(DTOMatriculasRespuesta pc_Matriculas, DTOAlumnosFacturablesRespuesta pc_AlumnosFacturables, DTOCarreraProfesionalRespuesta pc_CarreraProfesional, DTOHechosImportantesRespuesta pc_HechosImportantes, string pc_SelCodModalEst, string pc_SelCodPeriodo)
        {
            DTOTabDatosGenerales oDTOTabDatosGenerales = new DTOTabDatosGenerales()
            {
                listaPeriodos = new List<DTODatosGeneralesPeriodos>(),
                listaModalidades = new List<DTODatosGeneralesModalidades>(),
                listaDatosPorPeriodo = new List<DTODatosGeneralesPorPeriodo>(),
                listaHechosImportantes = new List<DTODatosGeneralesHechosImportantes>()
            };

            Dictionary<string, string> ValModalidades = ObtenerValoresWebConfig("ValModalidades");
            Dictionary<string, string> ValCampus = ObtenerValoresWebConfig("ValCampus");
            Dictionary<string, string> ValTipoOrdenMerito = ObtenerValoresWebConfig("ValTipoOrdenMerito");

            oDTOTabDatosGenerales.listaModalidades.Add(new DTODatosGeneralesModalidades() { ModalidadVal = pc_SelCodModalEst, ModalidadDes = ObtenerValorDeLlave(ValModalidades, pc_SelCodModalEst) });

            if (pc_Matriculas.ListaDTOMatricula == null)
            {
                return oDTOTabDatosGenerales;
            }
            else if (pc_Matriculas.ListaDTOMatricula.FirstOrDefault() == null)
            {
                return oDTOTabDatosGenerales;
            }

            foreach (var madet in pc_Matriculas.ListaDTOMatricula.First().ListaDTOMatriculaDet)
                oDTOTabDatosGenerales.listaPeriodos.Add(new DTODatosGeneralesPeriodos() { PeriodoVal = madet.CodPeriodMat, PeriodoDes = madet.CodPeriodMat });
            oDTOTabDatosGenerales.listaPeriodos = oDTOTabDatosGenerales.listaPeriodos.OrderByDescending(p => p.PeriodoVal).ToList();
            
            DTOListaCarreraProfesionalDet carreraProfesionalDet = new DTOListaCarreraProfesionalDet();
            if (pc_CarreraProfesional.oDTOListaCarreraProfesional.Where(p => p.oDTOListaCarreraProfesionalDet.Any()).Any())
                carreraProfesionalDet = pc_CarreraProfesional.oDTOListaCarreraProfesional.FirstOrDefault().oDTOListaCarreraProfesionalDet.FirstOrDefault();

            oDTOTabDatosGenerales.CodPeriodo = pc_SelCodPeriodo;
            oDTOTabDatosGenerales.CodModalidad = pc_SelCodModalEst;
            oDTOTabDatosGenerales.Facultad = carreraProfesionalDet.FacultadDescripcion;
            oDTOTabDatosGenerales.Carrera = carreraProfesionalDet.Descripcion;

            DTODatosGeneralesPorPeriodo tmpDatosGenPeriodo;
            DTOAlumnosFacturables tmpAlumnosFact;
            ListaDTOAlumnosFacturablesDet tmpAlumnosFactDet;

            foreach (var md in pc_Matriculas.ListaDTOMatricula.First().ListaDTOMatriculaDet)
            {
                tmpDatosGenPeriodo = new DTODatosGeneralesPorPeriodo();

                tmpDatosGenPeriodo.CodPeriodo = md.CodPeriodMat;
                tmpDatosGenPeriodo.Campus = ObtenerValorDeLlave(ValCampus, md.CodLocal);
                tmpDatosGenPeriodo.CicloAlumno = md.CicloAlumno;
                tmpDatosGenPeriodo.EstadoMatricula = md.DescEstadoMatric;
                tmpDatosGenPeriodo.Orden = !md.OrdenMeritoProd.HasValue ? String.Empty : md.OrdenMeritoProd.ToString();
                tmpDatosGenPeriodo.OrdenMeritoAcumulado = !md.OrdenMeritoProdAcum.HasValue ? String.Empty : md.OrdenMeritoProdAcum.ToString();
                tmpDatosGenPeriodo.TipoMeritoCarrera = ObtenerValorDeLlave(ValTipoOrdenMerito, md.TipoMeritoProd);
                tmpDatosGenPeriodo.TipoMeritoCarreraAcumulado = ObtenerValorDeLlave(ValTipoOrdenMerito, md.TipoMeritoProdAcum);
                tmpDatosGenPeriodo.PonderadoActual = !md.PondActual.HasValue ? String.Empty : md.PondActual.ToString();
                tmpDatosGenPeriodo.PonderadoAcumulado = !md.PondAcumulado.HasValue ? String.Empty : md.PondAcumulado.ToString();
                tmpDatosGenPeriodo.PonderadoBeca = !md.PondBeca.HasValue ? String.Empty : md.PondBeca.ToString();
                tmpDatosGenPeriodo.Egresado = md.EstadoEgresado;
                tmpDatosGenPeriodo.Pronabec = md.IndPronabec;

                if (pc_AlumnosFacturables.ListaDTOAlumnosFacturables.Any())
                {
                    tmpAlumnosFact = new DTOAlumnosFacturables();
                    tmpAlumnosFact = pc_AlumnosFacturables.ListaDTOAlumnosFacturables.Where(p => p.DTOAlumnosFacturablesCab.CodPeriodo == md.CodPeriodMat).FirstOrDefault();

                    if (tmpAlumnosFact != null)
                    {
                        tmpAlumnosFactDet = new ListaDTOAlumnosFacturablesDet();
                        tmpAlumnosFactDet = tmpAlumnosFact.ListaDTOAlumnosFacturablesDet.FirstOrDefault();

                        if (tmpAlumnosFactDet != null)
                        {
                            tmpDatosGenPeriodo.Categoria = tmpAlumnosFactDet.CodCategoria;
                        }
                    }
                }

                oDTOTabDatosGenerales.listaDatosPorPeriodo.Add(tmpDatosGenPeriodo);
            }

            if (pc_HechosImportantes.ListaHechoImportantes != null)
            {
                foreach (var hi in pc_HechosImportantes.ListaHechoImportantes)
                {
                    foreach (var hid in hi.DTOHechoImportantesDet)
                    {
                        DTODatosGeneralesHechosImportantes hechoImportante = new DTODatosGeneralesHechosImportantes();
                        hechoImportante.FechaHecho = !hid.FecCreacion.HasValue ? String.Empty : hid.FecCreacion.Value.ToString("dd.MM.yyyy");
                        hechoImportante.HoraHecho = !hid.FecCreacion.HasValue ? String.Empty : hid.FecCreacion.Value.ToString("hh:mm");
                        hechoImportante.TipoRegistro = String.IsNullOrEmpty(hid.DesTipoRegistro) ? String.Empty : hid.DesTipoRegistro;
                        hechoImportante.Descripcion = String.IsNullOrEmpty(hid.Hecho) ? String.Empty : hid.Hecho;
                        hechoImportante.RegistradoPor = String.IsNullOrEmpty(hid.UsuarioCreacion) ? String.Empty : hid.UsuarioCreacion;
                        hechoImportante.Activo = String.IsNullOrEmpty(hid.Estado) ? String.Empty : hid.Estado;
                        hechoImportante.PeriodoEliminado = String.IsNullOrEmpty(hid.CodPeriodCambio) ? String.Empty : hid.CodPeriodCambio;

                        oDTOTabDatosGenerales.listaHechosImportantes.Add(hechoImportante);
                    }
                }
                oDTOTabDatosGenerales.listaHechosImportantes = oDTOTabDatosGenerales.listaHechosImportantes.OrderBy(p => p.FechaHecho).ToList();
            }

            return oDTOTabDatosGenerales;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabDatosGenerales de banner para poblar los datos de la pestaña de "Datos generales" con información de los servicios de Banner.
        /// </summary>
        /// <param name="pc_AlumnoBanner"></param>
        /// <param name="pc_token"></param>
        /// <param name="pc_HechosImportantes"></param>
        /// <returns></returns>
        private async Task<DTOTabDatosGenerales> PreparandoDataDatosGeneralesBanner(DTOAlumnosRespuestaBanner pc_AlumnoBanner, DTOHechosImportantesRespuesta pc_HechosImportantes, DTOMatriculaBannerRespuesta wsMatriculaRespuestaBanner, DTODetalleMatriculaBannerRespuesta wsDetalleMatriculaRespuestaBanner, DTOHistoriaAlumnoBannerRespuesta wsHistoriaAlumnoBanner, DTOParametrosServiciosBanner oParamsBanner)
        {
            //Inicialización de ViewModel para la Vista
            DTOTabDatosGenerales dtoTabDatosGeneralesBanner = new DTOTabDatosGenerales()
            {
                listaPeriodos = new List<DTODatosGeneralesPeriodos>(),
                listaModalidades = new List<DTODatosGeneralesModalidades>(),
                listaDatosPorPeriodo = new List<DTODatosGeneralesPorPeriodo>(),
                listaHechosImportantes = new List<DTODatosGeneralesHechosImportantes>()
            };

            Dictionary<string, string> ValModalidades = ObtenerValoresWebConfig("ValModalidades");
            Dictionary<string, string> ValCampus = ObtenerValoresWebConfig("ValCampus");
            Dictionary<string, string> ValTipoOrdenMerito = ObtenerValoresWebConfig("ValTipoOrdenMerito");

            dtoTabDatosGeneralesBanner.listaModalidades.Add(new DTODatosGeneralesModalidades() { ModalidadVal = oParamsBanner.CodModalidadBanner, ModalidadDes = ObtenerValorDeLlave(ValModalidades, oParamsBanner.CodModalidadBanner) });

            if (wsMatriculaRespuestaBanner.detalle == null)
            {
                return dtoTabDatosGeneralesBanner;
            }

            foreach (var madet in wsDetalleMatriculaRespuestaBanner.detalle.listaAlumnos.First().listaDetalleMatricula)
                dtoTabDatosGeneralesBanner.listaPeriodos.Add(new DTODatosGeneralesPeriodos() { PeriodoVal = madet.periodo, PeriodoDes = madet.periodo });
            dtoTabDatosGeneralesBanner.listaPeriodos = dtoTabDatosGeneralesBanner.listaPeriodos.OrderByDescending(p => p.PeriodoVal).ToList();

            dtoTabDatosGeneralesBanner.CodPeriodo = wsMatriculaRespuestaBanner.detalle.listaProductos.First().listaMatriculas.First().periodo;
            dtoTabDatosGeneralesBanner.CodModalidad = oParamsBanner.CodModalidadBanner;
            dtoTabDatosGeneralesBanner.Facultad = wsHistoriaAlumnoBanner.detalle.listaHistoriaAlumno.First().listaCursos.First().facultad.descripcion;
            dtoTabDatosGeneralesBanner.Carrera = wsMatriculaRespuestaBanner.detalle.listaProductos.First().descripcion;

            DTODatosGeneralesPorPeriodo tmpDatosGenPeriodo;
            foreach (var md in wsDetalleMatriculaRespuestaBanner.detalle.listaAlumnos.First().listaDetalleMatricula)
            {
                tmpDatosGenPeriodo = new DTODatosGeneralesPorPeriodo();

                tmpDatosGenPeriodo.CodPeriodo = md.periodo;
                tmpDatosGenPeriodo.Campus = pc_AlumnoBanner.detalle.listaAlumno.First().campus.descripcion.ToUpper();
                tmpDatosGenPeriodo.CicloAlumno = "";
                tmpDatosGenPeriodo.EstadoMatricula = "";
                tmpDatosGenPeriodo.Orden = "";
                tmpDatosGenPeriodo.OrdenMeritoAcumulado = "";
                tmpDatosGenPeriodo.TipoMeritoCarrera = "";
                tmpDatosGenPeriodo.TipoMeritoCarreraAcumulado = "";
                tmpDatosGenPeriodo.PonderadoActual = "";
                tmpDatosGenPeriodo.PonderadoAcumulado = wsHistoriaAlumnoBanner.detalle.listaHistoriaAlumno.First().promedioGlobalAcumulado.ToString();
                tmpDatosGenPeriodo.Categoria= pc_AlumnoBanner.detalle.listaAlumno.First().codigoUsuario.Substring(0, 1);
                tmpDatosGenPeriodo.PonderadoBeca = "";
                tmpDatosGenPeriodo.Egresado = "";
                tmpDatosGenPeriodo.Pronabec = "";

                dtoTabDatosGeneralesBanner.listaDatosPorPeriodo.Add(tmpDatosGenPeriodo);
            }



            //Hechos Importantes desde Sócrates
            if (pc_HechosImportantes.ListaHechoImportantes != null)
            {
                foreach (var hi in pc_HechosImportantes.ListaHechoImportantes)
                {
                    foreach (var hid in hi.DTOHechoImportantesDet)
                    {
                        DTODatosGeneralesHechosImportantes hechoImportante = new DTODatosGeneralesHechosImportantes();
                        hechoImportante.FechaHecho = !hid.FecCreacion.HasValue ? String.Empty : hid.FecCreacion.Value.ToString("dd.MM.yyyy");
                        hechoImportante.HoraHecho = !hid.FecCreacion.HasValue ? String.Empty : hid.FecCreacion.Value.ToString("hh:mm");
                        hechoImportante.TipoRegistro = String.IsNullOrEmpty(hid.DesTipoRegistro) ? String.Empty : hid.DesTipoRegistro;
                        hechoImportante.Descripcion = String.IsNullOrEmpty(hid.Hecho) ? String.Empty : hid.Hecho;
                        hechoImportante.RegistradoPor = String.IsNullOrEmpty(hid.UsuarioCreacion) ? String.Empty : hid.UsuarioCreacion;
                        hechoImportante.Activo = String.IsNullOrEmpty(hid.Estado) ? String.Empty : hid.Estado;
                        hechoImportante.PeriodoEliminado = String.IsNullOrEmpty(hid.CodPeriodCambio) ? String.Empty : hid.CodPeriodCambio;

                        dtoTabDatosGeneralesBanner.listaHechosImportantes.Add(hechoImportante);
                    }
                }
                dtoTabDatosGeneralesBanner.listaHechosImportantes = dtoTabDatosGeneralesBanner.listaHechosImportantes.OrderBy(p => p.FechaHecho).ToList();
            }



            return dtoTabDatosGeneralesBanner;
        }
            /// <summary>
            /// Llenado de datos de la clase DTOTabAvanceCurricular para poblar los datos de la pestaña de "Avance curricular".
            /// </summary>
        private static DTOTabAvanceCurricular PreparandoDataAvanceCurricular(DTOMallaCurricularRespuesta pc_MallaCurricular, DTODetMatriculaResultado pc_DetalleMatricula)
        {
            DTOTabAvanceCurricular oDTOTabAvanceCurricular = new DTOTabAvanceCurricular()
            {
                 listaAvanceCurricularCiclos = new List<DTOAvanceCurricularCiclo>()
            };

            if (pc_MallaCurricular.ListaMallasCurriculares == null)
            {
                return oDTOTabAvanceCurricular;
            }
            else if (!pc_MallaCurricular.ListaMallasCurriculares.Any(p => p.ListaDTOMallasCurricularesDet.Any()))
            {
                return oDTOTabAvanceCurricular;
            }

            Dictionary<string, string> ValEstadoAprobacion = ObtenerValoresWebConfig("ValEstadoAprobacion");

            List<DTODetMatriculaDet> listaDetalleMatricula = new List<DTODetMatriculaDet>();
            foreach (var dm in pc_DetalleMatricula.ListaDTODetMatriculaOBJ)
            {
                if (dm.DTODetMatriculaCab != null && dm.ListaDTODetMatriculaDet != null)
                {
                    foreach (var dmd in dm.ListaDTODetMatriculaDet)
                    {
                        dmd.customCodPeriodo = dm.DTODetMatriculaCab.CodPeriodo;
                    }
                    listaDetalleMatricula.AddRange(dm.ListaDTODetMatriculaDet);
                }
            }

            DTOAvanceCurricularDet avanceCurricular;
            DTODetMatriculaDet cursoDetMatricula;

            foreach (var mcd in pc_MallaCurricular.ListaMallasCurriculares.First().ListaDTOMallasCurricularesDet)
            {
                DTOAvanceCurricularCiclo acc = new DTOAvanceCurricularCiclo();
                acc = oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.Where(p => p.Ciclo == mcd.Ciclo).FirstOrDefault();

                if (acc == null)
                {
                    acc = new DTOAvanceCurricularCiclo() { Ciclo = mcd.Ciclo, listaDTOAvanceCurricular = new List<DTOAvanceCurricularDet>() };
                    oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.Add(acc);
                }

                avanceCurricular = new DTOAvanceCurricularDet();
                avanceCurricular.CodCurso = mcd.CodCurso;
                avanceCurricular.DescCurso = mcd.DescCurso;
                avanceCurricular.CantCreditos = mcd.CantCreditos.Value;

                cursoDetMatricula = new DTODetMatriculaDet();
                cursoDetMatricula = listaDetalleMatricula.Where(p => p.CodCurso == mcd.CodCurso).ToList().OrderByDescending(p => p.NumVezCurso).FirstOrDefault();

                if (cursoDetMatricula != null)
                {
                    avanceCurricular.CodPeriodo = cursoDetMatricula.customCodPeriodo;
                    avanceCurricular.NumVezCurso = cursoDetMatricula.NumVezCurso.HasValue ? cursoDetMatricula.NumVezCurso.Value.ToString() : String.Empty;
                    avanceCurricular.NotaCurso = cursoDetMatricula.NotaCurso.HasValue ? cursoDetMatricula.NotaCurso.Value.ToString() : String.Empty;
                    avanceCurricular.EstadoAprob = ObtenerValorDeLlave(ValEstadoAprobacion, cursoDetMatricula.EstadoAprob);
                }
                else
                {
                    avanceCurricular.CodPeriodo = "---";
                    avanceCurricular.NumVezCurso = "---";
                    avanceCurricular.NotaCurso = "---";
                    avanceCurricular.EstadoAprob = ObtenerValorDeLlave(ValEstadoAprobacion, Constants.DefaultValue);
                }

                acc.listaDTOAvanceCurricular.Add(avanceCurricular);
            }

            oDTOTabAvanceCurricular.listaAvanceCurricularCiclos = oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.OrderBy(p => p.Ciclo).ToList();
            return oDTOTabAvanceCurricular;
        }

        private static DTOTabAvanceCurricular PreparandoDataAvanceCurricularBanner(DTOMallaCurricularRespuesta pc_MallaCurricular, DTODetMatriculaResultado pc_DetalleMatricula)
        {
            DTOTabAvanceCurricular oDTOTabAvanceCurricular = new DTOTabAvanceCurricular()
            {
                listaAvanceCurricularCiclos = new List<DTOAvanceCurricularCiclo>()
            };

            if (pc_MallaCurricular.ListaMallasCurriculares == null)
            {
                return oDTOTabAvanceCurricular;
            }
            else if (!pc_MallaCurricular.ListaMallasCurriculares.Any(p => p.ListaDTOMallasCurricularesDet.Any()))
            {
                return oDTOTabAvanceCurricular;
            }

            Dictionary<string, string> ValEstadoAprobacion = ObtenerValoresWebConfig("ValEstadoAprobacion");

            List<DTODetMatriculaDet> listaDetalleMatricula = new List<DTODetMatriculaDet>();
            foreach (var dm in pc_DetalleMatricula.ListaDTODetMatriculaOBJ)
            {
                if (dm.DTODetMatriculaCab != null && dm.ListaDTODetMatriculaDet != null)
                {
                    foreach (var dmd in dm.ListaDTODetMatriculaDet)
                    {
                        dmd.customCodPeriodo = dm.DTODetMatriculaCab.CodPeriodo;
                    }
                    listaDetalleMatricula.AddRange(dm.ListaDTODetMatriculaDet);
                }
            }

            DTOAvanceCurricularDet avanceCurricular;
            DTODetMatriculaDet cursoDetMatricula;

            foreach (var mcd in pc_MallaCurricular.ListaMallasCurriculares.First().ListaDTOMallasCurricularesDet)
            {
                DTOAvanceCurricularCiclo acc = new DTOAvanceCurricularCiclo();
                acc = oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.Where(p => p.Ciclo == mcd.Ciclo).FirstOrDefault();

                if (acc == null)
                {
                    acc = new DTOAvanceCurricularCiclo() { Ciclo = mcd.Ciclo, listaDTOAvanceCurricular = new List<DTOAvanceCurricularDet>() };
                    oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.Add(acc);
                }

                avanceCurricular = new DTOAvanceCurricularDet();
                avanceCurricular.CodCurso = mcd.CodCurso;
                avanceCurricular.DescCurso = mcd.DescCurso;
                avanceCurricular.CantCreditos = mcd.CantCreditos.Value;

                cursoDetMatricula = new DTODetMatriculaDet();
                cursoDetMatricula = listaDetalleMatricula.Where(p => p.CodCurso == mcd.CodCurso).ToList().OrderByDescending(p => p.NumVezCurso).FirstOrDefault();

                if (cursoDetMatricula != null)
                {
                    avanceCurricular.CodPeriodo = cursoDetMatricula.customCodPeriodo;
                    avanceCurricular.NumVezCurso = cursoDetMatricula.NumVezCurso.HasValue ? cursoDetMatricula.NumVezCurso.Value.ToString() : String.Empty;
                    avanceCurricular.NotaCurso = cursoDetMatricula.NotaCurso.HasValue ? cursoDetMatricula.NotaCurso.Value.ToString() : String.Empty;
                    avanceCurricular.EstadoAprob = ObtenerValorDeLlave(ValEstadoAprobacion, cursoDetMatricula.EstadoAprob);
                }
                else
                {
                    avanceCurricular.CodPeriodo = "---";
                    avanceCurricular.NumVezCurso = "---";
                    avanceCurricular.NotaCurso = "---";
                    avanceCurricular.EstadoAprob = ObtenerValorDeLlave(ValEstadoAprobacion, Constants.DefaultValue);
                }

                acc.listaDTOAvanceCurricular.Add(avanceCurricular);
            }

            oDTOTabAvanceCurricular.listaAvanceCurricularCiclos = oDTOTabAvanceCurricular.listaAvanceCurricularCiclos.OrderBy(p => p.Ciclo).ToList();
            return oDTOTabAvanceCurricular;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabHistorialNotas para poblar los datos de la pestaña de "Historial de notas".
        /// </summary>
        private static DTOTabHistorialNotas PreparandoDataHistorialNotas(DTOMallaCurricularRespuesta pc_MallaCurricular, DTOMatriculasRespuesta pc_Matriculas, DTODetMatriculaResultado pc_DetalleMatricula)
        {
            DTOTabHistorialNotas oDTOTabHistorialNotas = new DTOTabHistorialNotas()
            {
                listaHistorialNotas = new List<DTOHistorialNotas>()
            };

            Dictionary<string, string> ValTipoOrdenMerito = ObtenerValoresWebConfig("ValTipoOrdenMerito");

            List<DTOMatriculaDet> listaMatriculaDet = pc_Matriculas.ListaDTOMatricula.First().ListaDTOMatriculaDet;
            listaMatriculaDet = listaMatriculaDet.OrderByDescending(p => p.CodPeriodMat).ToList();

            DTOHistorialNotas tmpHistNotas;
            DTODetMatricula tmpDetalleMatricula;
            DTOHistorialNotasDet tmpHistNotasDet;
            DTOMallasCurricularesDet tmpMallaCurrDet;

            foreach (var md in listaMatriculaDet)
            {
                tmpHistNotas = new DTOHistorialNotas()
                {
                    listaHistorialNotasDet = new List<DTOHistorialNotasDet>()
                };
                tmpHistNotas.Periodo = md.CodPeriodMat;
                tmpHistNotas.Carrera = md.DescProducto;
                tmpHistNotas.TipoMatricula = md.DescTipoMatric;
                tmpHistNotas.EstadoMatricula = md.DescEstadoMatric;
                tmpHistNotas.Ciclo = md.CicloAlumno;
                tmpHistNotas.PonderadoActual = md.PondActual.ToString();
                tmpHistNotas.PonderadoAcumulado = md.PondAcumulado.ToString();
                tmpHistNotas.ObservacionesConst = md.NumObsCons.ToString();
                tmpHistNotas.ObservacionesAlt = md.NumObsAlte.ToString();
                tmpHistNotas.OrdenMerito = md.OrdenMeritoProd.ToString();
                tmpHistNotas.PertenenciaMerito = ObtenerValorDeLlave(ValTipoOrdenMerito, md.TipoMeritoProd);

                tmpDetalleMatricula = new DTODetMatricula();
                tmpDetalleMatricula = pc_DetalleMatricula.ListaDTODetMatriculaOBJ.Where(p => p.DTODetMatriculaCab.CodPeriodo == md.CodPeriodMat).FirstOrDefault();

                if (tmpDetalleMatricula != null)
                {
                    if (tmpDetalleMatricula.ListaDTODetMatriculaDet != null)
                    {
                        foreach (var dmd in tmpDetalleMatricula.ListaDTODetMatriculaDet)
                        {
                            tmpHistNotasDet = new DTOHistorialNotasDet();
                            tmpHistNotasDet.CodigoCurso = dmd.CodCurso;
                            tmpHistNotasDet.DescripcionCurso = dmd.DesCurso;
                            tmpHistNotasDet.NumeroVeces = dmd.NumVezCurso.Value.ToString();
                            tmpHistNotasDet.PromedioFinal = dmd.NotaCurso.Value.ToString();
                            tmpHistNotasDet.Aprobado = dmd.EstadoAprob;

                            tmpMallaCurrDet = new DTOMallasCurricularesDet();
                            tmpMallaCurrDet = pc_MallaCurricular.ListaMallasCurriculares.First().ListaDTOMallasCurricularesDet.Where(p => p.CodCurso == dmd.CodCurso).FirstOrDefault();

                            if (tmpMallaCurrDet != null)
                            {
                                tmpHistNotasDet.Nivel = tmpMallaCurrDet.Ciclo;
                                tmpHistNotasDet.Creditos = tmpMallaCurrDet.CantCreditos.Value.ToString();
                            }

                            tmpHistNotas.listaHistorialNotasDet.Add(tmpHistNotasDet);
                        }
                    }
                }

                oDTOTabHistorialNotas.listaHistorialNotas.Add(tmpHistNotas);
            }

            return oDTOTabHistorialNotas;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabHorarioAlumno para poblar los datos de la pestaña de "Horario".
        /// </summary>
        private static DTOTabHorarioAlumno PreparandoDataHorario(DTOHorarioAlumnoResultado pc_HorarioAlumno, DateTime pc_FechaSemanaInicio, DateTime pc_FechaSemanaFin)
        {
            DTOTabHorarioAlumno oDTOTabHorarioAlumno = new DTOTabHorarioAlumno()
            {
                FechaSemanaInicio = pc_FechaSemanaInicio.ToString("dd/MM/yyyy"),
                FechaSemanaFin = pc_FechaSemanaFin.ToString("dd/MM/yyyy"),
                listaFilasHorario = new List<DTOFilaHorario>()
            };

            Dictionary<string, string> ValTipoSesion = ObtenerValoresWebConfig("ValTipoSesion");
            Dictionary<string, string> ValCampus = ObtenerValoresWebConfig("ValCampus");

            if (pc_HorarioAlumno.ListaDTOHorarioOBJAlumno == null)
            {
                return oDTOTabHorarioAlumno;
            }

            List<DTOHorarioAlumnoDet> listaHorarioDetalle = new List<DTOHorarioAlumnoDet>();
            foreach (var ha in pc_HorarioAlumno.ListaDTOHorarioOBJAlumno)
            {
                listaHorarioDetalle.AddRange(ha.ListaDTOHorarioAlumnoDet);
            }

            for (int i = listaHorarioDetalle.Count - 1; i >= 0; i--)
            {
                if (!listaHorarioDetalle[i].HoraInicioSesion.HasValue || !listaHorarioDetalle[i].HoraTerminoSesion.HasValue)
                    listaHorarioDetalle.RemoveAt(i);
            }

            List<DTOHorarioAlumnoDet> listHorarioDetLunes = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetMartes = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetMiercoles = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetJueves = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetViernes = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetSabado = new List<DTOHorarioAlumnoDet>();
            List<DTOHorarioAlumnoDet> listHorarioDetDomingo = new List<DTOHorarioAlumnoDet>();

            listHorarioDetLunes = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 1).ToList();
            listHorarioDetMartes = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 2).ToList();
            listHorarioDetMiercoles = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 3).ToList();
            listHorarioDetJueves = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 4).ToList();
            listHorarioDetViernes = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 5).ToList();
            listHorarioDetSabado = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 6).ToList();
            listHorarioDetDomingo = listaHorarioDetalle.Where(p => Convert.ToInt16(p.HoraInicioSesion.Value.DayOfWeek) == 0).ToList();

            List<int> listaHorasInicio = new List<int>();
            foreach (var lhd in listaHorarioDetalle)
            {
                if (!listaHorasInicio.Contains(lhd.HoraInicioSesion.Value.Hour))
                    listaHorasInicio.Add(lhd.HoraInicioSesion.Value.Hour);
            }
            listaHorasInicio = listaHorasInicio.OrderBy(p => p).ToList();

            DTOFilaHorario filaHorario;
            DTOHorarioAlumnoDet horarioLunes, horarioMartes, horarioMiercoles, horarioJueves, horarioViernes, horarioSabado, horarioDomingo;

            foreach (int hora in listaHorasInicio)
            {
                filaHorario = new DTOFilaHorario()
                {
                    listaCasillasHorario = new List<DTOCasillaHorario>()
                };

                horarioLunes = new DTOHorarioAlumnoDet();
                horarioMartes = new DTOHorarioAlumnoDet();
                horarioMiercoles = new DTOHorarioAlumnoDet();
                horarioJueves = new DTOHorarioAlumnoDet();
                horarioViernes = new DTOHorarioAlumnoDet();
                horarioSabado = new DTOHorarioAlumnoDet();
                horarioDomingo = new DTOHorarioAlumnoDet();

                horarioLunes = listHorarioDetLunes.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioMartes = listHorarioDetMartes.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioMiercoles = listHorarioDetMiercoles.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioJueves = listHorarioDetJueves.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioViernes = listHorarioDetViernes.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioSabado = listHorarioDetSabado.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();
                horarioDomingo = listHorarioDetDomingo.Where(p => p.HoraInicioSesion.Value.Hour == hora).FirstOrDefault();

                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioLunes, ValTipoSesion, ValCampus, 1));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioMartes, ValTipoSesion, ValCampus, 2));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioMiercoles, ValTipoSesion, ValCampus, 3));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioJueves, ValTipoSesion, ValCampus, 4));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioViernes, ValTipoSesion, ValCampus, 5));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioSabado, ValTipoSesion, ValCampus, 6));
                filaHorario.listaCasillasHorario.Add(ConstruirCasillaHorario(horarioDomingo, ValTipoSesion, ValCampus, 7));

                oDTOTabHorarioAlumno.listaFilasHorario.Add(filaHorario);
            }

            return oDTOTabHorarioAlumno;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabAvanceNotas para poblar los datos de la pestaña de "Notas actuales".
        /// </summary>
        private static DTOTabAvanceNotas PreparandoDataAvanceNotas(DTOAvanceNotasRespuesta pc_AvanceNotas, DTODetMatriculaResultado pc_DetalleMatricula, DTOMallaCurricularRespuesta pc_MallaCurricular)
        {
            DTOTabAvanceNotas oDTOTabAvanceNotas = new DTOTabAvanceNotas()
            {
                NotaRoja = 0,
                listaAvanceNotas = new List<DTOAvanceNotasCursos>()
            };

            if (pc_AvanceNotas.ListaNota == null)
            {
                return oDTOTabAvanceNotas;
            }
            if (pc_AvanceNotas.ListaNota.Count == 0)
            {
                return oDTOTabAvanceNotas;
            }

            Double dblNotaRoja = 0;
            if (Double.TryParse(ConfigurationManager.AppSettings["NotaRojo"], out dblNotaRoja))
                oDTOTabAvanceNotas.NotaRoja = dblNotaRoja;

            // Lista de detalle de cursos llevados por el alumno
            List<DTODetMatriculaDet> listaDetalleMatricula = new List<DTODetMatriculaDet>();
            if (pc_DetalleMatricula.ListaDTODetMatriculaOBJ != null)
            {
                foreach (var dm in pc_DetalleMatricula.ListaDTODetMatriculaOBJ)
                {
                    if (dm.DTODetMatriculaCab != null && dm.ListaDTODetMatriculaDet != null)
                    {
                        listaDetalleMatricula.AddRange(dm.ListaDTODetMatriculaDet);
                    }
                }
            }

            // Lista de detalle de cursos listados según la malla curricular actual del alumno
            List<DTOMallasCurricularesDet> listaMallasCurricularesDet = new List<DTOMallasCurricularesDet>();
            if (pc_MallaCurricular.ListaMallasCurriculares != null)
            {
                foreach (var mc in pc_MallaCurricular.ListaMallasCurriculares)
                {
                    if (mc.ListaDTOMallasCurricularesDet != null)
                    {
                        listaMallasCurricularesDet.AddRange(mc.ListaDTOMallasCurricularesDet);
                    }
                }
            }

            DTOAvanceNotasCursos tmpAvNotCurso;
            DTOAvanceNotasDetalle tmpAvNotDetalle;
            DTODetMatriculaDet tmpDetMatriculaDet;
            DTOMallasCurricularesDet tmpMallasCurricularesDet;

            foreach (var av in pc_AvanceNotas.ListaNota)
            {
                tmpAvNotCurso = new DTOAvanceNotasCursos()
                {
                    listaDetalleNotas = new List<DTOAvanceNotasDetalle>()
                };

                tmpAvNotCurso.CodigoCurso = av.CodCurso;
                tmpAvNotCurso.DescripcionCurso = av.DescCurso.ToUpper();

                tmpDetMatriculaDet = new DTODetMatriculaDet();
                tmpDetMatriculaDet = listaDetalleMatricula.Where(p => p.CodCurso == av.CodCurso).OrderByDescending(p => p.NumVezCurso).FirstOrDefault();

                if (tmpDetMatriculaDet != null)
                {
                    tmpAvNotCurso.Seccion = tmpDetMatriculaDet.Seccion;
                    tmpAvNotCurso.Grupo = tmpDetMatriculaDet.Grupo;
                    tmpAvNotCurso.Vez = tmpDetMatriculaDet.NumVezCurso.HasValue ? tmpDetMatriculaDet.NumVezCurso.Value.ToString() : String.Empty;
                }

                tmpMallasCurricularesDet = new DTOMallasCurricularesDet();
                tmpMallasCurricularesDet = listaMallasCurricularesDet.Where(p => p.CodCurso == av.CodCurso).FirstOrDefault();

                if (tmpMallasCurricularesDet != null)
                {
                    tmpAvNotCurso.Creditos = tmpMallasCurricularesDet.CantCreditos.HasValue ? tmpMallasCurricularesDet.CantCreditos.Value.ToString() : String.Empty;
                }

                tmpAvNotCurso.AvancePorcentual = av.PorcentajeAvance;
                tmpAvNotCurso.NotaNoOficial = av.Promedio.HasValue? av.Promedio.ToString() : "0";

                // Division entre nota no oficial con porcentaje
                double dblPorcentajeAvance = 0, dblNotaProyectada = 0;
                bool huboConvercion = Double.TryParse(av.PorcentajeAvance.Replace('%', ' ').Trim(), out dblPorcentajeAvance);

                if (av.Promedio.HasValue && huboConvercion)
                {
                    //dblPorcentajeAvance = dblPorcentajeAvance / 100;
                    //dblNotaProyectada = dblPorcentajeAvance * av.Promedio.Value;
                    //dblNotaProyectada = av.Promedio.Value > 0 ? dblPorcentajeAvance / av.Promedio.Value : 0;
                    dblNotaProyectada = dblPorcentajeAvance > 0 ? (av.Promedio.Value * 100) / dblPorcentajeAvance : 0;
                }
                tmpAvNotCurso.NotaProyectada = dblNotaProyectada.ToString();
                //tmpAvNotCurso.NotaProyectada = av.Promedio.HasValue ? av.Promedio.ToString() : String.Empty;

                if (av.Notas == null) continue;

                foreach (var no in av.Notas)
                {
                    //if (no.CodTipoPrueba == ConfigurationManager.AppSettings["CodigoTipoPruebaExcluido"])
                    //    continue;

                    tmpAvNotDetalle = new DTOAvanceNotasDetalle();

                    tmpAvNotDetalle.CodigoTipoPrueba = no.CodTipoPrueba;
                    tmpAvNotDetalle.DescripcionTipoPrueba = no.DesTipoPrueba.ToUpper();
                    tmpAvNotDetalle.NumeroPrueba = no.NumPrueba.HasValue ? no.NumPrueba.Value.ToString() : String.Empty;
                    tmpAvNotDetalle.PesoPonderado = no.PesoPonderado;
                    tmpAvNotDetalle.Nota = no.Nota;
                    //tmpAvNotDetalle.ActualizadoPor;
                    //tmpAvNotDetalle.FechaActualizacion;
                    //tmpAvNotDetalle.Observaciones;

                    tmpAvNotCurso.listaDetalleNotas.Add(tmpAvNotDetalle);
                }

                oDTOTabAvanceNotas.listaAvanceNotas.Add(tmpAvNotCurso);

                /*
                List<DTODetMatriculaDet> listaDetalleMatricula = new List<DTODetMatriculaDet>();
                foreach (var dm in pc_DetalleMatricula.ListaDTODetMatriculaOBJ)
                {
                    if (dm.DTODetMatriculaCab != null && dm.ListaDTODetMatriculaDet != null)
                    {
                        foreach (var dmd in dm.ListaDTODetMatriculaDet)
                        {
                            dmd.customCodPeriodo = dm.DTODetMatriculaCab.CodPeriodo;
                        }
                        listaDetalleMatricula.AddRange(dm.ListaDTODetMatriculaDet);
                    }
                } 
                */
            }

            return oDTOTabAvanceNotas;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabAvanceNotas para poblar los datos de la pestaña de "Notas actuales" desde Banner.
        /// </summary>
        /// <param name="pc_AvanceNotas"></param>
        /// <param name="pc_DetalleMatricula"></param>
        /// <param name="pc_MallaCurricular"></param>
        /// <returns></returns>
        private static DTOTabAvanceNotas PreparandoDataAvanceNotasBanner(DTONotasActualesRespuesta pc_AvanceNotasBanner, DTODetalleMatriculaBannerRespuesta pc_DetalleMatriculaBanner)
        {
            DTOTabAvanceNotas dTOTabAvanceNotas = new DTOTabAvanceNotas()
            {
                NotaRoja = 0,
                listaAvanceNotas = new List<DTOAvanceNotasCursos>()
            };

            DTOAvanceNotasCursos tmpListaCurso;
            DTOAvanceNotasDetalle tmpListaDetalleNota;

            DetalleDetalleMatricula detalleDetalleMatricula = pc_DetalleMatriculaBanner.detalle.listaAlumnos.First().listaDetalleMatricula.First();

            Double dblNotaRoja = 0;
            if (Double.TryParse(ConfigurationManager.AppSettings["NotaRojo"], out dblNotaRoja))
                dTOTabAvanceNotas.NotaRoja = dblNotaRoja;

            foreach (var curso in detalleDetalleMatricula.listaCursos)
            {
                tmpListaCurso = new DTOAvanceNotasCursos()
                {
                    listaDetalleNotas = new List<DTOAvanceNotasDetalle>()
                };
                tmpListaCurso.CodigoCurso = curso.materia.codigo + curso.numeroCurso;
                tmpListaCurso.DescripcionCurso = curso.titulo;
                tmpListaCurso.Seccion = "";
                tmpListaCurso.Vez = "";
                tmpListaCurso.Creditos = "";
                tmpListaCurso.AvancePorcentual = "";
                tmpListaCurso.NotaNoOficial = "";
                tmpListaCurso.NotaProyectada = "";

                foreach (var itemdetallenota in pc_AvanceNotasBanner.ListaNota.First().notas)
                {
                    tmpListaDetalleNota = new DTOAvanceNotasDetalle();

                    tmpListaDetalleNota.CodigoTipoPrueba = itemdetallenota.CodTipoPrueba;
                    tmpListaDetalleNota.DescripcionTipoPrueba = itemdetallenota.DesTipoPrueba.ToUpper();
                    tmpListaDetalleNota.NumeroPrueba = itemdetallenota.NumPrueba.HasValue ? itemdetallenota.NumPrueba.Value.ToString() : "--";
                    tmpListaDetalleNota.PesoPonderado = itemdetallenota.PesoPonderado.HasValue ? itemdetallenota.PesoPonderado.Value.ToString() : "--";
                    tmpListaDetalleNota.Nota = itemdetallenota.Nota.HasValue ? itemdetallenota.Nota.Value.ToString() : "--";

                    tmpListaCurso.listaDetalleNotas.Add(tmpListaDetalleNota);
                }

                dTOTabAvanceNotas.listaAvanceNotas.Add(tmpListaCurso);
            }

            return dTOTabAvanceNotas;
        }

        private static DTOTabAvanceNotas PreparandoDataAvanceNotasBanner(List<DTONotasActualesRespuesta> pc_AvanceNotasBanner, DTODetalleMatriculaBannerRespuesta pc_DetalleMatriculaBanner)
        {
            DTOTabAvanceNotas dTOTabAvanceNotas = new DTOTabAvanceNotas()
            {
                NotaRoja = 0,
                listaAvanceNotas = new List<DTOAvanceNotasCursos>()
            };

            Double dblNotaRoja = 0;
            if (Double.TryParse(ConfigurationManager.AppSettings["NotaRojo"], out dblNotaRoja))
                dTOTabAvanceNotas.NotaRoja = dblNotaRoja;
            DTOAvanceNotasCursos tmpListaCurso;
            DTOAvanceNotasDetalle tmpListaDetalleNota;
            foreach (var totalservicio in pc_AvanceNotasBanner)
            {
                if (totalservicio.ListaNota == null)
                   continue;
                    foreach (var listacurso in totalservicio.ListaNota)
                    {
                        //Validación si no hay info que continue
                        if (listacurso == null)
                           continue;

                            tmpListaCurso = new DTOAvanceNotasCursos()
                            {
                                listaDetalleNotas = new List<DTOAvanceNotasDetalle>()
                            };
                            tmpListaCurso.CodigoCurso = listacurso.CodCurso;
                            tmpListaCurso.DescripcionCurso = listacurso.DescCurso;

                            foreach (var listanota in listacurso.notas)
                            {
                                tmpListaDetalleNota = new DTOAvanceNotasDetalle();
                                tmpListaDetalleNota.CodigoTipoPrueba = listanota.CodTipoPrueba;
                                tmpListaDetalleNota.DescripcionTipoPrueba = listanota.DesTipoPrueba.ToUpper();
                                tmpListaDetalleNota.NumeroPrueba = listanota.NumPrueba.HasValue ? listanota.NumPrueba.Value.ToString() : String.Empty;
                                tmpListaDetalleNota.PesoPonderado = listanota.PesoPonderado.HasValue ? listanota.PesoPonderado.Value.ToString() : String.Empty;
                                tmpListaDetalleNota.Nota = listanota.Nota.HasValue ? listanota.Nota.Value.ToString() : String.Empty;

                                tmpListaCurso.listaDetalleNotas.Add(tmpListaDetalleNota);
                            }
                    }                
            }

            return dTOTabAvanceNotas;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabInasistencias para poblar los datos de la pestaña de "Inasistencias".
        /// </summary>
        private static DTOTabInasistencias PreparandoDataInasistencias(DTOInasistenciasResultado pc_Inasistencias, DTOAvanceNotasRespuesta pc_AvanceNotas)
        {
            DTOTabInasistencias oDTOTabAvanceNotas = new DTOTabInasistencias()
            {
                listaInasistencias = new List<DTOInasistenciasAsignaturas>()
            };

            DTOInasistenciasAsignaturas tmpInasisAsignat;

            foreach (var ina in pc_Inasistencias.ListaDTOInasistenciasAlumnos)
            {
                if (ina.DTOInasistenciasCab == null || ina.ListaDTOInasistenciasDet == null)
                    continue;

                //if (ina.ListaDTOInasistenciasDet.Count == 0)
                //    continue;

                tmpInasisAsignat = new DTOInasistenciasAsignaturas()
                {
                    listaFechasInasistencia = new List<string>()
                };

                tmpInasisAsignat.CodigoCurso = ina.DTOInasistenciasCab.CodCurso;
                tmpInasisAsignat.DescripcionCurso = ina.DTOInasistenciasCab.DesCurso;
                tmpInasisAsignat.ClasesDictadas = ina.DTOInasistenciasCab.ClasesDictadas.HasValue ? ina.DTOInasistenciasCab.ClasesDictadas.Value.ToString() : String.Empty;
                tmpInasisAsignat.ClasesAsistidas = ina.DTOInasistenciasCab.ClasesAsistidas.HasValue ? ina.DTOInasistenciasCab.ClasesAsistidas.Value.ToString() : String.Empty;
                tmpInasisAsignat.ClasesInasistidas = ina.DTOInasistenciasCab.ClasesInasistidas.HasValue ? ina.DTOInasistenciasCab.ClasesInasistidas.Value.ToString() : String.Empty;
                tmpInasisAsignat.InasistenciasEfectivas = ina.DTOInasistenciasCab.ClasesEfectivasIna.HasValue ? ina.DTOInasistenciasCab.ClasesEfectivasIna.Value.ToString() : String.Empty;
                //tmpInasisAsignat.NombreDocente;

                foreach (var inad in ina.ListaDTOInasistenciasDet)
                    if (inad.FechaSesion.HasValue)
                        tmpInasisAsignat.listaFechasInasistencia.Add(inad.FechaSesion.Value.ToString("dd.MM.yyyy"));

                oDTOTabAvanceNotas.listaInasistencias.Add(tmpInasisAsignat);
            }

            //JV 18/07/2019
            if (pc_AvanceNotas == null)
            {
                return oDTOTabAvanceNotas;
            }
            
            if (pc_AvanceNotas.ListaNota == null)
            {
                return oDTOTabAvanceNotas;
            }
            if (pc_AvanceNotas.ListaNota.Count == 0)
            {
                return oDTOTabAvanceNotas;
            }

            foreach (var av in pc_AvanceNotas.ListaNota)
            {
                if (String.IsNullOrEmpty(av.CodCurso))
                    continue;

                if (oDTOTabAvanceNotas.listaInasistencias.Where(p => p.CodigoCurso == av.CodCurso).Any())
                {
                    tmpInasisAsignat = new DTOInasistenciasAsignaturas()
                    {
                        listaFechasInasistencia = new List<string>()
                    };

                    tmpInasisAsignat.CodigoCurso = av.CodCurso;
                    tmpInasisAsignat.DescripcionCurso = av.DescCurso;
                    tmpInasisAsignat.ClasesDictadas = "--";
                    tmpInasisAsignat.ClasesAsistidas = "--";
                    tmpInasisAsignat.ClasesInasistidas = "0";
                    tmpInasisAsignat.InasistenciasEfectivas = "0";

                    //oDTOTabAvanceNotas.listaInasistencias.Add(tmpInasisAsignat);
                }
            }

            return oDTOTabAvanceNotas;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOT   abInasistencias para poblar los datos de la pestaña de "Inasistencias" - Nuevo servicio.
        /// </summary>
        /// <param name="pc_Inasistencias"></param>
        /// <returns></returns>
        private static DTOTabInasistencias PreparandoDataInasistencias(DTOInasistenciasResultado pc_Inasistencias)
        {
            DTOTabInasistencias dTOTabInasistencias = new DTOTabInasistencias()
            {
                listaInasistencias = new List<DTOInasistenciasAsignaturas>()
            };
            DTOInasistenciasAsignaturas tmpInasisAsignat;

            foreach (var ina in pc_Inasistencias.ListaDTOInasistenciasAlumnos)
            {
                if (ina.DTOInasistenciasCab == null || ina.ListaDTOInasistenciasDet == null)
                    continue;

                //if (ina.ListaDTOInasistenciasDet.Count == 0)
                //    continue;

                tmpInasisAsignat = new DTOInasistenciasAsignaturas()
                {
                    listaFechasInasistencia = new List<string>()
                };

                tmpInasisAsignat.CodigoCurso = ina.DTOInasistenciasCab.CodCurso;
                tmpInasisAsignat.DescripcionCurso = ina.DTOInasistenciasCab.DesCurso;
                tmpInasisAsignat.ClasesDictadas = ina.DTOInasistenciasCab.ClasesDictadas.HasValue ? ina.DTOInasistenciasCab.ClasesDictadas.Value.ToString() : String.Empty;
                tmpInasisAsignat.ClasesAsistidas = ina.DTOInasistenciasCab.ClasesAsistidas.HasValue ? ina.DTOInasistenciasCab.ClasesAsistidas.Value.ToString() : String.Empty;
                tmpInasisAsignat.ClasesInasistidas = ina.DTOInasistenciasCab.ClasesInasistidas.HasValue ? ina.DTOInasistenciasCab.ClasesInasistidas.Value.ToString() : String.Empty;
                tmpInasisAsignat.InasistenciasEfectivas = ina.DTOInasistenciasCab.ClasesEfectivasIna.HasValue ? ina.DTOInasistenciasCab.ClasesEfectivasIna.Value.ToString() : String.Empty;
                tmpInasisAsignat.NombreDocente = "";

                foreach (var inad in ina.ListaDTOInasistenciasDet)
                    if (inad.FechaSesion.HasValue)
                        tmpInasisAsignat.listaFechasInasistencia.Add(inad.FechaSesion.Value.ToString("dd.MM.yyyy"));

                dTOTabInasistencias.listaInasistencias.Add(tmpInasisAsignat);
            }

            return dTOTabInasistencias;
        }

        private static DTOTabInasistencias PreparandoDataInasistencias(List<DTOInasistenciasResultado> pc_Inasistencias)
        {
            DTOTabInasistencias dTOTabInasistencias = new DTOTabInasistencias()
            {
                listaInasistencias = new List<DTOInasistenciasAsignaturas>()
            };
            DTOInasistenciasAsignaturas tmpInasisAsignat;

            foreach (var cabecera in pc_Inasistencias)
            {
                if(cabecera == null)
                    continue;

                foreach (var ina in cabecera.ListaDTOInasistenciasAlumnos)
                {
                    if (ina.DTOInasistenciasCab == null || ina.ListaDTOInasistenciasDet == null)
                        continue;

                    //if (ina.ListaDTOInasistenciasDet.Count == 0)
                    //    continue;

                    tmpInasisAsignat = new DTOInasistenciasAsignaturas()
                    {
                        listaFechasInasistencia = new List<string>()
                    };

                    tmpInasisAsignat.CodigoCurso = ina.DTOInasistenciasCab.CodCurso;
                    tmpInasisAsignat.DescripcionCurso = ina.DTOInasistenciasCab.DesCurso;
                    tmpInasisAsignat.ClasesDictadas = ina.DTOInasistenciasCab.ClasesDictadas.HasValue ? ina.DTOInasistenciasCab.ClasesDictadas.Value.ToString() : String.Empty;
                    tmpInasisAsignat.ClasesAsistidas = ina.DTOInasistenciasCab.ClasesAsistidas.HasValue ? ina.DTOInasistenciasCab.ClasesAsistidas.Value.ToString() : String.Empty;
                    tmpInasisAsignat.ClasesInasistidas = ina.DTOInasistenciasCab.ClasesInasistidas.HasValue ? ina.DTOInasistenciasCab.ClasesInasistidas.Value.ToString() : String.Empty;
                    tmpInasisAsignat.InasistenciasEfectivas = ina.DTOInasistenciasCab.ClasesEfectivasIna.HasValue ? ina.DTOInasistenciasCab.ClasesEfectivasIna.Value.ToString() : String.Empty;
                    tmpInasisAsignat.NombreDocente = "";

                    foreach (var inad in ina.ListaDTOInasistenciasDet)
                        if (inad.FechaSesion.HasValue)
                            tmpInasisAsignat.listaFechasInasistencia.Add(inad.FechaSesion.Value.ToString("dd.MM.yyyy"));

                    dTOTabInasistencias.listaInasistencias.Add(tmpInasisAsignat);
                }
            }

            

            return dTOTabInasistencias;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabDeudas para poblar los datos de la pestaña de "Deudas".
        /// </summary>
        private static DTOTabDeudas PreparandoDataDeudas(DTODocumentoFiscalResultado pc_DocumentoFiscal, string pc_CodigoAlumno, string pc_NombreAlumno)
        {
            DTOTabDeudas oDTOTabDeudas = new DTOTabDeudas()
            {
                FechaActual = DateTime.Now.ToString("dd - MM - yyyy"),
                CodigoAlumno = pc_CodigoAlumno,
                NombreAlumno = pc_NombreAlumno,
                listaFilasDeuda = new List<DTODeudaFila>(),
                ImporteSoles = 0,
                MoraSoles = 0,
                TotalSoles = 0,
                ImporteDolares = 0,
                MoraDolares = 0,
                TotalDolares = 0
            };

            Dictionary<string, string> ValMoneda = ObtenerValoresWebConfig("ValMoneda");

            double montoPagado = 0;
            double montoTotal = 0;

            DTODeudaFila tmpDeudaFila;
            foreach (var lc in pc_DocumentoFiscal.ListaCriteria)
            {
                if (lc != null)
                {
                    if (lc.Clientes != null)
                    {
                        foreach (var cl in lc.Clientes)
                        {
                            if (cl.Documentos != null)
                            {
                                foreach (var doc in cl.Documentos)
                                {
                                    tmpDeudaFila = new DTODeudaFila();
                                    //JV 20/09/2018 cambio en formato de fecha
                                    tmpDeudaFila.Documento = String.Format("{0}-{1}", doc.TIPODOCUMENTO, doc.NUMERODOCUMENTO);
                                    tmpDeudaFila.FechaEmision = Convert.ToDateTime(doc.FECHADOCUMENTO.Split('T')[0]).ToString("dd-MM-yyyy");//.HasValue ? doc.FECHADOCUMENTO.Value.ToString("dd-MM-yyyy") : String.Empty;
                                    //tmpDeudaFila.FechaEmision = Fecha.fechaADateTime(doc.FECHADOCUMENTO).ToString("dd-MM-yyyy");//.HasValue ? doc.FECHADOCUMENTO.Value.ToString("dd-MM-yyyy") : String.Empty;
                                    tmpDeudaFila.FechaVencimiento = Convert.ToDateTime(doc.FECHAVENCIMIENTO.Split('T')[0]).ToString("dd-MM-yyyy");//.HasValue ? doc.FECHAVENCIMIENTO.Value.ToString("dd-MM-yyyy") : String.Empty;
                                    //tmpDeudaFila.FechaVencimiento = Fecha.fechaADateTime(doc.FECHAVENCIMIENTO).ToString("dd-MM-yyyy");//.HasValue ? doc.FECHAVENCIMIENTO.Value.ToString("dd-MM-yyyy") : String.Empty;
                                    tmpDeudaFila.TipoVenta = ConfigurationManager.AppSettings["TipoVenta"];
                                    tmpDeudaFila.Moneda = ObtenerValorDeLlave(ValMoneda, doc.MONEDADOCUMENTO);
                                    montoTotal = doc.MONTOTOTAL.HasValue ? doc.MONTOTOTAL.Value : 0;
                                    montoPagado = doc.MONTOPAGADO.HasValue ? doc.MONTOPAGADO.Value : 0;
                                    //JV 06/09/2017
                                    tmpDeudaFila.Importe = montoTotal - montoPagado;
                                    tmpDeudaFila.Mora = doc.MONTOMORA.HasValue ? doc.MONTOMORA.Value : 0;
                                    tmpDeudaFila.Total = tmpDeudaFila.Importe + tmpDeudaFila.Mora;

                                    if (tmpDeudaFila.Moneda == ValMoneda.First().Value)
                                    {
                                        oDTOTabDeudas.ImporteSoles += tmpDeudaFila.Importe;
                                        oDTOTabDeudas.MoraSoles += tmpDeudaFila.Mora;
                                        oDTOTabDeudas.TotalSoles += tmpDeudaFila.Total;
                                    }
                                    else
                                    {
                                        oDTOTabDeudas.ImporteDolares += tmpDeudaFila.Importe;
                                        oDTOTabDeudas.MoraDolares += tmpDeudaFila.Mora;
                                        oDTOTabDeudas.TotalDolares += tmpDeudaFila.Total;
                                    }

                                    oDTOTabDeudas.listaFilasDeuda.Add(tmpDeudaFila);
                                }
                            }
                        }
                    }
                }
            }

            return oDTOTabDeudas;
        }

        /// <summary>
        /// Llenado de datos de la clase DTOTabPromedioPonderado para poblar los datos de la pestaña de "Promedio ponderado".
        /// </summary>
        private static DTOTabPromedioPonderado PreparandoDataPromedioPonderado(List<DTOMatriculaDet> pc_ListaMatriculaDetalle)
        {
            DTOTabPromedioPonderado oDTOTabPromedioPonderado = new DTOTabPromedioPonderado()
            {
                promedioRojo = 0,
                promedioNaranja = 0,
                listaPromediosPonderados = new List<DTOPromedioPonderado>()
            };

            Double dblPromedioRojo = 0, dblPromedioNaranja = 0;
            if (Double.TryParse(ConfigurationManager.AppSettings["PromedioPonderadoRojo"], out dblPromedioRojo))
                oDTOTabPromedioPonderado.promedioRojo = dblPromedioRojo;
            if (Double.TryParse(ConfigurationManager.AppSettings["PromedioPonderadoNaranja"], out dblPromedioNaranja))
                oDTOTabPromedioPonderado.promedioNaranja = dblPromedioNaranja;

            DTOPromedioPonderado tmpPromPond;
            double tmpPromedio;

            foreach (var md in pc_ListaMatriculaDetalle)
            {
                tmpPromPond = new DTOPromedioPonderado();
                tmpPromedio = 0;

                tmpPromPond.CodigoPeriodo = md.CodPeriodMat;

                if (md.PondActual.HasValue)
                    tmpPromedio = md.PondActual.Value < 0 || md.PondActual > 20 ? 0 : md.PondActual.Value;
                else
                    tmpPromedio = 0;
                tmpPromPond.PromedioPonderado = tmpPromedio;

                tmpPromPond.PorcentajeNota = Math.Round((tmpPromedio / 20) * 100, 2);

                oDTOTabPromedioPonderado.listaPromediosPonderados.Add(tmpPromPond);
            }

            return oDTOTabPromedioPonderado;
        }

        // Utilidades

        /// <summary>
        /// Utilitario para la generación de un diccionario, a partir del nombre de la llave de los valores del Web.config que se encuentran debajo del comentario VALORES (ver Web.config).
        /// </summary>
        public static Dictionary<string, string> ObtenerValoresWebConfig(string pc_appSettingKey)
        {
            Dictionary<string, string> dicEstadoAprobacion = new Dictionary<string, string>();

            string estadoAprobacionValues = ConfigurationManager.AppSettings[pc_appSettingKey];
            foreach (var row in estadoAprobacionValues.Split(','))
            {
                string[] values = row.Split(':');
                if (values.Length == 2) dicEstadoAprobacion.Add(values[0].ToUpper(), values[1]);
            }

            return dicEstadoAprobacion;
        }

        /// <summary>
        /// Utilitario para la obtención de un valor del diccionario obtenido en el método ObtenerValoresWebConfig. En caso que la variable ingresada no coincida con ningún valor del diccionario, éste devuelve el contenido de una variable por DEFECTO definida dentro del diccionario. Dicho valor, al igual que los demás valores del diccionario, debe estar definido dentro del Web.config.
        /// </summary>
        public static string ObtenerValorDeLlave(Dictionary<string, string> pc_dic, string pc_variable)
        {
            string res = Constants.DefaultValue.ToUpper();

            if (!String.IsNullOrEmpty(pc_variable))
            {
                if (pc_dic.ContainsKey(pc_variable.ToUpper()))
                    res = pc_variable.ToUpper();
            }

            return pc_dic[res];
        }

        /// <summary>
        /// Llenado de datos de una de las casillas del horario mostrado en la pestaña "Horario".
        /// </summary>
        public static DTOCasillaHorario ConstruirCasillaHorario(DTOHorarioAlumnoDet pc_horario, Dictionary<string, string> pc_dicSesion, Dictionary<string, string> pc_dicCampus, Int16 pc_diaSemana)
        {
            DTOCasillaHorario ch = new DTOCasillaHorario();


            if (pc_horario != null)
            {
                ch.HayClase = true;
                ch.DiaSemana = pc_diaSemana;
                ch.HoraInicioSesion = pc_horario.HoraInicioSesion.Value.ToString("hh:mmtt", CultureInfo.InvariantCulture).ToLower();
                ch.HoraTerminoSesion = pc_horario.HoraTerminoSesion.Value.ToString("hh:mmtt", CultureInfo.InvariantCulture).ToLower();
                ch.Seccion = pc_horario.Seccion;
                ch.Grupo = pc_horario.Grupo;
                ch.CodigoCurso = pc_horario.CodCurso;
                ch.DescripcionCurso = pc_horario.DesCurso;
                ch.CodigoAula = pc_horario.CodAula;
                ch.CodigoLocal = ObtenerValorDeLlave(pc_dicCampus, pc_horario.CodLocal);
                ch.NombreCompletoDocente = String.Format("{0} {1} {2}", pc_horario.Nombres, pc_horario.ApellidoPatern, pc_horario.ApellidoMatern);
                ch.DocenteCategoria = pc_horario.DescripcionTpCategDoce;
                ch.TipoClase = pc_horario.DescripcionTipoClase;
                ch.CodigoTipoSesion = ObtenerValorDeLlave(pc_dicSesion, pc_horario.DescripcionTipoSesion);
                ch.DescripcionTipoSesion = pc_horario.DescripcionTipoSesion;
            }
            else
            {
                ch.HayClase = false;
                ch.DiaSemana = pc_diaSemana;
            }

            return ch;
        }
    }
}
