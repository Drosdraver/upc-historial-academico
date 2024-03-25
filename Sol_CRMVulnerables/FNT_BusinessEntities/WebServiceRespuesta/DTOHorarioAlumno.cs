using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Horario.
    /// </summary>
    public class DTOHorarioAlumnoResultado
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOHorarioAlumno> ListaDTOHorarioOBJAlumno { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHorarioAlumnoResultado.
    /// </summary>
    public class DTOHorarioAlumno
    {
        public DTOHorarioAlumnoCab DTOHorarioAlumnoCab { get; set; }
        public List<DTOHorarioAlumnoDet> ListaDTOHorarioAlumnoDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHorarioAlumnoResultado.
    /// </summary>
    public class DTOHorarioAlumnoCab
    {
        public String CodLineaNegocio { get; set; }
        public String CodModalEst { get; set; }
        public String NombreDia { get; set; }
        public String CodPeriodo { get; set; }
        public DateTime? FechaSesion { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHorarioAlumnoResultado.
    /// </summary>
    public class DTOHorarioAlumnoDet
    {
        public Int32? IdSesion { get; set; }
        public DateTime? HoraInicioSesion { get; set; }
        public String Seccion { get; set; }
        public String Grupo { get; set; }
        public String CodCurso { get; set; }
        public DateTime? HoraTerminoSesion { get; set; }
        public String CodAula { get; set; }
        public String CodLineaNegAula { get; set; }
        public String CodTipoClase { get; set; }
        public String Vigente { get; set; }
        public String CodTipoPrueba { get; set; }
        public Int32? NumTipoPrueba { get; set; }
        public String HoraLista { get; set; }
        public String Tema { get; set; }
        public String CodProducto { get; set; }
        public Int32? VacanteExamen { get; set; }
        public Int32? IdWsesion { get; set; }
        public String UsuarioCreador { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public Int32? IdPrestaAula { get; set; }
        public String Envio { get; set; }
        public String TipoMail { get; set; }
        public String Observacion { get; set; }
        public String VistoExtranet { get; set; }
        public String MedioCalificacion { get; set; }
        public String IndPronabec { get; set; }
        public Int32? IdDocenSecc { get; set; }
        public String HoraAsistenciaDocSes { get; set; }
        public String JustificacionDocSes { get; set; }
        public String PagoSesionDocSes { get; set; }
        public String UsuarioCreadorDocSes { get; set; }
        public DateTime? FechaCreacionDocSes { get; set; }
        public String UsuarioMoficaDocSes { get; set; }
        public DateTime? FechaModificaDocSes { get; set; }
        public String AsistioDocSes { get; set; }
        public String AvisoDocSes { get; set; }
        public String DireccionIPDocSes { get; set; }
        public Int32? CantidadReclamosDocSes { get; set; }
        public DateTime? FechaDevolucionDocSes { get; set; }
        public DateTime? FechaADevolverDocSes { get; set; }
        public String DevolvioReclamosDocSes { get; set; }
        public Int32? CantidadDevueltaDocSes { get; set; }
        public String ObservacionDocSes { get; set; }
        public String CodDocente { get; set; }
        public String CodCategDoc { get; set; }
        public String CodTarifaDocSes { get; set; }
        public String CodSubCatDoce { get; set; }
        public String CodLocal { get; set; }
        public String CodPabellon { get; set; }
        public String CodPiso { get; set; }
        public String CodTipoAula { get; set; }
        public Int32? VacantesAula { get; set; }
        public String EstadoAula { get; set; }
        public String ReservaAula { get; set; }
        public Int32? VacantesRecuperacionAula { get; set; }
        public String CodProductoGrpCursec { get; set; }
        public Int32? VacanteDisponibleGrpCursec { get; set; }
        public Int32? VacanteUsadaGrpCursec { get; set; }
        public String FecheCierreGrpCursec { get; set; }
        public Int32? VacanteUsadaMAutoGrpCursec { get; set; }
        public String MedioCalificacionMC { get; set; }
        public String DescripcionTipoClase { get; set; }
        public String DescripcionTipoPrueba { get; set; }
        public String CodTipoSesion { get; set; }
        public String DescripcionTipoSesion { get; set; }
        public String SesionReprograma { get; set; }
        public String ApePatImag { get; set; }
        public String ApeMatImag { get; set; }
        public String NombresImag { get; set; }
        public String ApellidoPatern { get; set; }
        public String ApellidoMatern { get; set; }
        public String Nombres { get; set; }
        public String TipoDocumento { get; set; }
        public String DocumenIdentida { get; set; }
        public String DescripcionTpCategDoce { get; set; }
        public String DescripcionTpSubcatDoce { get; set; }
        public String DescMinisterioCurso { get; set; }
        public String DescEspecialCurso { get; set; }
        public String CodDireccionCurso { get; set; }
        public String CodSedeAlumno { get; set; }
        public Int32? CodDocenteDocSec { get; set; }
        public String CodCategDocDocSec { get; set; }
        public String CodTarifaDocSec { get; set; }
        public String CodSubCatDocSec { get; set; }
        public String IndOnlineCurso { get; set; }
        public String DesCurso { get; set; }
    }
}
