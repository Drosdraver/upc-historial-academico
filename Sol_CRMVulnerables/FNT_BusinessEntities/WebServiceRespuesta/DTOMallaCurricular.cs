using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Malla Curricular.
    /// </summary>
    public class DTOMallaCurricularRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOMallaCurricular> ListaMallasCurriculares { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOMallaCurricular
    {
        public DTOMallasCurricularesCab DTOMallasCurricularesCab { get; set; }
        public List<DTOMallasCurricularesDet> ListaDTOMallasCurricularesDet { get; set; }
        public List<DTOPaquetesElectivo> ListaDTOPaquetesElectivo { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOMallasCurricularesCab
    {
        public String CodLineaNegocio { get; set; }
        public String CodProducto { get; set; }
        public String DescProducto { get; set; }
        public Double? CodCurriculo { get; set; }
        public String Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public String Vigencia { get; set; }
        public DateTime? FechaVigCuenta { get; set; }
        public Double? MaxPeriodos { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOMallasCurricularesDet
    {
        public String CodCurso { get; set; }
        public String DescCurso { get; set; }
        public String DescEspecial { get; set; }
        public String DescMinisterio { get; set; }
        public String ConvalDefecto { get; set; }
        public String Ciclo { get; set; }
        public Double? CantCreditos { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public Double? CantCredequiv { get; set; }
        public String ConvalProgresiva { get; set; }
        public String PermiteAdelantar { get; set; }
        public String Prioridad { get; set; }
        public String CurcucSinNota { get; set; }
        public String Condicion { get; set; }
        public String CodGlosa { get; set; }
        public String DescGlosa { get; set; }
        public List<DTOMallasHorasCursoSubDet> DTOMallasHorasCursoSubDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOMallasHorasCursoSubDet
    {
        public String CodTipoClase { get; set; }
        public String DescTipoClase { get; set; }
        public Double? Horas { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOPaquetesElectivo
    {
        public DTOPaquetesElectivoCab DTOPaquetesElectivoCab { get; set; }
        public List<DTOPaqueteElectivoDet> ListaPaqueteElectivoDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOPaquetesElectivoCab
    {
        public String codcursoElec { get; set; }
        public String DescCursoElec { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMallaCurricularRespuesta.
    /// </summary>
    public class DTOPaqueteElectivoDet
    {
        public String Codcurso { get; set; }
        public String DescEspecial { get; set; }
        public Double? CantidadCreditos { get; set; }
        public List<DTOMallasHorasCursoSubDet> DTOMallasHorasCursoSubDet { get; set; }
    }
}
