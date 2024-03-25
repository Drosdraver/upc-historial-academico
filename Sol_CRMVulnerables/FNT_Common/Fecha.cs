using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FNT_Common
{
    public static class Fecha
    {
        public static DateTime fechaADateTime(string fechaddmmaaa)
        {
            DateTime fechaFinal;
            string diax, mesx, anhox;
            int dia, mes, anho;

            diax = fechaddmmaaa.Substring(0, 2);
            mesx = fechaddmmaaa.Substring(3, 2);
            anhox = fechaddmmaaa.Substring(6, 4);
            dia = Convert.ToInt16(diax);
            mes = Convert.ToInt16(mesx);
            anho = Convert.ToInt32(anhox);

            fechaFinal = new DateTime(anho, mes, dia);
            return fechaFinal;
        }
    }
}
