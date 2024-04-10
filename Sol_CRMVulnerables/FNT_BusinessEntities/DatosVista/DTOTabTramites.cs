using FNT_BusinessEntities.WebServiceRespuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.DatosVista
{
    public class DTOTabTramites
    {
        public DTOTramitesResultado DTOTramitesMiUpc { get; set; }
        public DTOTramitesResultado DTOTramitesIntranet { get; set; }
        public DTOTramitesResultado DTOTramitesEpg { get; set; }

    }
}
