using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTONotasActualesRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public AvanceNota AvanceNota { get; set; }
        public List<ListaNotaItem> ListaNota { get; set; }
    }

    public class AvanceNota
    {
        public object CodLineaNegocio { get; set; }
        public object CodModalEst { get; set; }
        public string CodPeriodo { get; set; }
        public string CodAlumno { get; set; }
        public string CodUsuario { get; set; }
        public string ApellidoPatern { get; set; }
        public object ApellidoMatern { get; set; }
        public string Nombres { get; set; }
        public string ApePatImag { get; set; }
        public object ApeMatImag { get; set; }
        public string NombresImag { get; set; }
    }

    public class ListaNotaItem
    {
        public object DescEspecial { get; set; }
        public object DescAbreCurso { get; set; }
        public string CodCurso { get; set; }
        public string DescCurso { get; set; }
        public object CodFormula { get; set; }
        public object DscFormula { get; set; }
        public object PorcentajeAvance { get; set; }
        public object PromedioCurso { get; set; }
        public object TipoNota { get; set; }
        public List<Notas> notas { get; set; }
    }

    public class Notas
    {
        public string CodTipoPrueba { get; set; }
        public string DesTipoPrueba { get; set; }
        public int? NumPrueba { get; set; }
        public int? PesoPonderado { get; set; }
        public int? Nota { get; set; }
    }
}
