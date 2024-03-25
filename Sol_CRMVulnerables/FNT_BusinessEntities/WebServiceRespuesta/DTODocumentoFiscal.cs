using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Documento Fiscal.
    /// </summary>
    public class DTODocumentoFiscalResultado
    {
        public String Detalle { get; set; }
        public List<DTODocumentoCriteria> ListaCriteria { get; set; }
        public Int32? Resutado { get; set; }
        public DTOHeader DTOHeader { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODocumentoFiscalResultado.
    /// </summary>
    public class DTODocumentoCriteria
    {
        public String CRITERIA { get; set; }
        public List<DTODocumentoCliente> Clientes { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODocumentoFiscalResultado.
    /// </summary>
    public class DTODocumentoCliente
    {
        public String CLIENTENOMBRE { get; set; }
        public Int32? CLIENTENUMERO { get; set; }
        public List<DTODocumentoFiscal> Documentos { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODocumentoFiscalResultado.
    /// </summary>
    public class DTODocumentoFiscal
    {
        public String ALMACENCODIGO { get; set; }
        public object APFECHATRANSFERENCIA { get; set; }
        public String APLICADO { get; set; }
        public Int32? APPROCESONUMERO { get; set; }
        public Int32? APPROCESOSECUENCIA { get; set; }
        public Int32? APROBADOPOR { get; set; }
        public String APTRANSFERIDOFLAG { get; set; }
        public String CAMPOREFERENCIA { get; set; }
        public String CENTROCOSTO { get; set; }
        public Int32? CLIENTECOBRARA { get; set; }
        public String CLIENTEDIRECCION { get; set; }
        public String CLIENTEREFERENCIA { get; set; }
        public String CLIENTERUC { get; set; }
        public String COBRANZADUDOSAESTADO { get; set; }
        public object COBRANZADUDOSAFECHA { get; set; }
        public object COBRANZADUDOSAFECHACLEARING { get; set; }
        public object COBRANZADUDOSAMOTIVO { get; set; }
        public String COBRANZADUDOSAVOUCHER { get; set; }
        public String COBRANZADUDOSAVOUCHERCLEARING { get; set; }
        public String COMENTARIOS { get; set; }
        public String COMENTARIOSIMPRIMIRFLAG { get; set; }
        public Double? COMENTARIOSMONTO { get; set; }
        public object COMERCIALPEDIDOFECHAREQUERIDA { get; set; }
        public String COMERCIALPEDIDONUMERO { get; set; }
        public String COMPANIASOCIO { get; set; }
        public object CONCEPTOFACTURACION { get; set; }
        public String CONTABILIZACIONPENDIENTEFLAG { get; set; }
        public String DIFERIDODOCUMENTO { get; set; }
        public String DOCUMENTOMORAFLAG { get; set; }
        public List<DTODocumentoFiscalDetalle> DetalleDocumento { get; set; }
        public String ESTABLECIMIENTOCODIGO { get; set; }
        public String ESTADO { get; set; }
        public String FECHAAPROBACION { get; set; }
        public String FECHADOCUMENTO { get; set; }
        public String FECHAPREPARACION { get; set; }
        public String FECHAVENCIMIENTO { get; set; }
        public String FECHAVENCIMIENTOORIGINAL { get; set; }
        public Double? FEENVIONUMERO { get; set; }
        public String FEESTADO { get; set; }
        public String FEHASHCODE { get; set; }
        public Double? FEINTERNALNUMBER { get; set; }
        public String FORMADEPAGO { get; set; }
        public String IMPRESIONPENDIENTEFLAG { get; set; }
        public String LETRAAVALDIRECCION { get; set; }
        public String LETRAAVALNOMBRE { get; set; }
        public String LETRAAVALRUC { get; set; }
        public String LETRAAVALTELEFONO { get; set; }
        public String LETRABANCO { get; set; }
        public String LETRACARTERAFLAG { get; set; }
        public String LETRACOBRANZANUMERO { get; set; }
        public String LETRADESCUENTOCANJEFLAG { get; set; }
        public String LETRADESCUENTOCANJEVOUCHER { get; set; }
        public String LETRADESCUENTOCUENTABANCARIA { get; set; }
        public Double? LETRADESCUENTOINTERESES { get; set; }
        public String LETRADESCUENTOVOUCHER { get; set; }
        public String LETRADESCUENTOVOUCHERFLAG { get; set; }
        public String MONEDADOCUMENTO { get; set; }
        public Double? MONTOADELANTOSALDO { get; set; }
        public Double? MONTOAFECTO { get; set; }
        public Double? MONTOBECA { get; set; }
        public Double? MONTODESCUENTOS { get; set; }
        public Double? MONTOIMPUESTOS { get; set; }
        public Double? MONTOIMPUESTOVENTAS { get; set; }
        public Double? MONTOMORA { get; set; }
        public Double? MONTONOAFECTO { get; set; }
        public Double? MONTOPAGADO { get; set; }
        public Double? MONTOTOTAL { get; set; }
        public String NOTACREDITODOCUMENTO { get; set; }
        public String NOTACREDITOMOTIVO { get; set; }
        public String NOTACREDITOSUSTENTO { get; set; }
        public Double? NUMCUPON { get; set; }
        public String NUMERODOCUMENTO { get; set; }
        public String NUMEROINTERNO { get; set; }
        public Double? PREPARADOPOR { get; set; }
        public String PROCESOIMPORTACION { get; set; }
        public object PROCESOIMPORTACIONFECHA { get; set; }
        public String PROCESOIMPORTACIONNUMERO { get; set; }
        public String PROYECTO { get; set; }
        public String SEDE { get; set; }
        public String SUCURSAL { get; set; }
        public String TIPOCANJEFACTURA { get; set; }
        public Double? TIPODECAMBIO { get; set; }
        public String TIPODOCUMENTO { get; set; }
        public String TIPOFACTURACION { get; set; }
        public String TIPOVENTA { get; set; }
        public Double? TRANSFERENCIAGRATUITAIGVFACTOR { get; set; }
        public Double? TRANSFERENCIAGRATUITAMONTO { get; set; }
        public String ULTIMAFECHAMODIF { get; set; }
        public String ULTIMOUSUARIO { get; set; }
        public String UNIDADNEGOCIO { get; set; }
        public String UNIDADREPLICACION { get; set; }
        public String USUARIOAPROBACION { get; set; }
        public object USUARIOAPROBACIONFECHA { get; set; }
        public Double? VENDEDOR { get; set; }
        public String VOUCHERANULACION { get; set; }
        public String VOUCHERNO { get; set; }
        public String VOUCHERPERIODO { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTODocumentoFiscalResultado.
    /// </summary>
    public class DTODocumentoFiscalDetalle
    {
        public Double? CANTIDADENTREGADA { get; set; }
        public Double? CANTIDADPEDIDA { get; set; }
        public String COMPANIASOCIO { get; set; }
        public String DESCRIPCION { get; set; }
        public String ESTADO { get; set; }
        public String ESTADODIFERIDO { get; set; }
        public String FLUJODECAJA { get; set; }
        public String IGVEXONERADOFLAG { get; set; }
        public String IMPRIMIRPUFLAG { get; set; }
        public String ITEMCODIGO { get; set; }
        public Double? LINEA { get; set; }
        public Double? MONTO { get; set; }
        public Double? MONTOFINAL { get; set; }
        public String NUMERODOCUMENTO { get; set; }
        public Double? PRECIOUNITARIO { get; set; }
        public Double? PRECIOUNITARIOFINAL { get; set; }
        public Double? PRECIOUNITARIOGRATUITO { get; set; }
        public String TIPODETALLE { get; set; }
        public String TIPODOCUMENTO { get; set; }
        public String TRANSFERENCIAGRATUITAFLAG { get; set; }
        public String ULTIMAFECHAMODIF { get; set; }
        public String ULTIMOUSUARIO { get; set; }
        public String UNIDADCODIGO { get; set; }
    }
}
