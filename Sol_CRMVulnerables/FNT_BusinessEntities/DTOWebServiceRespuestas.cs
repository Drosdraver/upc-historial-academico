using FNT_BusinessEntities.WebServiceRespuesta;

namespace FNT_BusinessEntities
{
    /// <summary>
    /// Clase que contiene netamente las respuestas de cada uno de los servicios leídos.
    /// </summary>
    public class DTOWebServiceRespuestas
    {
        public DTOAlumnosRespuesta DTOAlumnosRespuesta { get; set; }
        public DTOAlumnosFacturablesRespuesta DTOAlumnosFacturablesRespuesta { get; set; }
        public DTOAvanceNotasRespuesta DTOAvanceNotasRespuesta { get; set; }
        public DTOCarreraProfesionalRespuesta DTOCarreraProfesionalRespuesta { get; set; }
        public DTODetMatriculaResultado DTODetMatriculaResultado { get; set; }
        public DTOHechosImportantesRespuesta DTOHechosImportantesRespuesta { get; set; }
        public DTOHorarioAlumnoResultado DTOHorarioAlumnoResultado { get; set; }
        public DTOInasistenciasResultado DTOInasistenciasResultado { get; set; }
        public DTOMallaCurricularRespuesta DTOMallaCurricularRespuesta { get; set; }
        public DTOMatriculasRespuesta DTOMatriculasRespuesta { get; set; }
        public DTOClientesResultado DTOClientesResultado { get; set; }
        public DTODocumentoFiscalResultado DTODocumentoFiscalResultado { get; set; }
    }
}
