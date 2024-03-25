using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Avance Notas.
    /// </summary>
    public class DTOAvanceNotasRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public DTOAvanceNotas AvanceNota { get; set; }
        public List<DTOListaNota> ListaNota { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAvanceNotasRespuesta.
    /// </summary>
    public class DTOAvanceNotas
    {
        public String CodLineaNegocio { get; set; }
        public String CodModalEst { get; set; }
        public String CodPeriodo { get; set; }
        public String CodAlumno { get; set; }
        public String CodUsuario { get; set; }
        public String ApellidoPatern { get; set; }
        public String ApellidoMatern { get; set; }
        public String Nombres { get; set; }
        public String ApePatImag { get; set; }
        public String ApeMatImag { get; set; }
        public String NombresImag { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAvanceNotasRespuesta.
    /// </summary>
    public class DTOListaNota
    {
        public String CodCurso { get; set; }
        public String DescCurso { get; set; }
        public String DescEspecial { get; set; }
        public String DescAbreCurso { get; set; }
        public Double? CodFormula { get; set; }
        public String DscFormula { get; set; }
        public String PorcentajeAvance { get; set; }
        public Double? Promedio { get; set; }
        public String TipoNota { get; set; }
        public List<DTONotas> Notas { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAvanceNotasRespuesta.
    /// </summary>
    public class DTONotas
    {
        public String CodTipoPrueba { get; set; }
        public String DesTipoPrueba { get; set; }
        public Double? NumPrueba { get; set; }
        public String PesoPonderado { get; set; }
        public String Nota { get; set; }
    }
}