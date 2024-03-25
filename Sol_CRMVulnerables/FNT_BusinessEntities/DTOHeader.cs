using System;

namespace FNT_BusinessEntities
{
    /// <summary>
    /// Clase retornada en la mayoría de los servicios, la cual determina la correcta o incorrecta obtención de datos.
    /// </summary>
    public class DTOHeader
    {
        public String CodigoRetorno { get; set; }
        public String DescRetorno { get; set; }
    }
}