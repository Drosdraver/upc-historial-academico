using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Avance curricular".
    /// </summary>
    public class DTOTabAvanceCurricular
    {
        public List<DTOAvanceCurricularCiclo> listaAvanceCurricularCiclos { get; set; } = new List<DTOAvanceCurricularCiclo>();
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabAvanceCurricular.
    /// </summary>
    public class DTOAvanceCurricularCiclo
    {
        public String Ciclo { get; set; }
        public List<DTOAvanceCurricularDet> listaDTOAvanceCurricular { get; set; } = new List<DTOAvanceCurricularDet>();
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabAvanceCurricular.
    /// </summary>
    public class DTOAvanceCurricularDet
    {
        public String CodCurso { get; set; }
        public String DescCurso { get; set; }
        public Double? CantCreditos { get; set; }
        public String CodPeriodo { get; set; }
        public String NumVezCurso { get; set; }
        public String NotaCurso { get; set; }
        public String EstadoAprob { get; set; }
    }
}
