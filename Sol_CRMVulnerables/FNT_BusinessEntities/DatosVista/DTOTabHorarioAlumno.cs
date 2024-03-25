using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Horario".
    /// </summary>
    public class DTOTabHorarioAlumno
    {
        public String FechaSemanaInicio { get; set; }
        public String FechaSemanaFin { get; set; }
        public List<DTOFilaHorario> listaFilasHorario { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabHorarioAlumno.
    /// </summary>
    public class DTOFilaHorario
    {
        public List<DTOCasillaHorario> listaCasillasHorario { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabHorarioAlumno.
    /// </summary>
    public class DTOCasillaHorario
    {
        public Boolean HayClase { get; set; }
        public Int16 DiaSemana { get; set; }
        public String HoraInicioSesion { get; set; }
        public String HoraTerminoSesion { get; set; }
        public String Seccion { get; set; }
        public String Grupo { get; set; }
        public String CodigoCurso { get; set; }
        public String DescripcionCurso { get; set; }
        public String CodigoAula { get; set; }
        public String CodigoLocal { get; set; }
        public String NombreCompletoDocente { get; set; }
        public String DocenteCategoria { get; set; }
        public String TipoClase { get; set; }
        public String CodigoTipoSesion { get; set; }
        public String DescripcionTipoSesion { get; set; }
    }
}
