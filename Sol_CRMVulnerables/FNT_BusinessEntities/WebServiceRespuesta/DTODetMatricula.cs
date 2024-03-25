using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Detalle Matricula.
    /// </summary>
    public class DTODetMatriculaResultado
    {
        public DTOHeader DTOHeader { get; set; }
        public Int32? DTOTotalMatriculadosVigentes { get; set; }
        public Int32? DTOTotalMatriculadosRetirados { get; set; }
        public Int32? DTOTotalMatriculadosSancionados { get; set; }
        public List<DTODetMatricula> ListaDTODetMatriculaOBJ { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODetMatriculaResultado.
    /// </summary>
    public class DTODetMatricula
    {
        public DTODetMatriculaCab DTODetMatriculaCab { get; set; }
        public List<DTODetMatriculaDet> ListaDTODetMatriculaDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODetMatriculaResultado.
    /// </summary>
    public class DTODetMatriculaCab
    {
        public Int32? MatriculaId { get; set; }
        public String CodLineaNegocio { get; set; }
        public String CodModal_Est { get; set; }
        public String DesModalidad { get; set; }
        public String CodPeriodo { get; set; }
        public String CodAlumno { get; set; }
        public String CodProducAlum { get; set; }
        public String DesProducAlum { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODetMatriculaResultado.
    /// </summary>
    public class DTODetMatriculaDet
    {
        public String CodCurso { get; set; }
        public String DesCurso { get; set; }
        public String Seccion { get; set; }
        public String Grupo { get; set; }
        public String CodProducto { get; set; }
        public Int32? CodCurriculo { get; set; }
        public String CodTipoMatric { get; set; }
        public String CodEstadoMat { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public Int32? NumVezCurso { get; set; }
        public String EstadoValidez { get; set; }
        public Double? NotaCurso { get; set; }
        public String EstadoAprob { get; set; }
        public String EstadoCierre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public String CodCursoOrig { get; set; }
        public String CodCurriculoOrig { get; set; }
        public String CicloOrig { get; set; }
        public String GrupoMat { get; set; }
        public Int32? IdPlan { get; set; }
        public String Vendcor { get; set; }
        public String Vendmar { get; set; }
        public Int32? NotaSubsanada { get; set; }
        public String Observacion { get; set; }
        public String MatAdicional { get; set; }
        public String CodTipMatadic { get; set; }
        public String FlgPonderadoMatadic { get; set; }
        public String CodModalConv { get; set; }
        public String CodPeriodConv { get; set; }
        public String CodProducConv { get; set; }
        public String CodCurricConv { get; set; }
        public String CodCursoConv { get; set; }
        public String CodTipoConv { get; set; }
        public String IndPronabec { get; set; }
        public String DescEspecialCurso { get; set; }
        public String DescMinisterioCurso { get; set; }

        // Campos que no pertenecen al servicio
        public String customCodPeriodo { get; set; }  /// Este campo no pertenece al servicio, es poblado posteriormente para facilitar el procesamiento de los datos del servicio
    }
}
