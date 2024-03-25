using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Clientes.
    /// </summary>
    public class DTOClientesResultado
    {
        public String Detalle { get; set; }
        public List<DTOClientesDetalle> ListaCliente { get; set; }
        public Int32? Resutado { get; set; }
        public DTOHeader DTOHeader { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOClientesResultado.
    /// </summary>
    public class DTOClientesDetalle
    {
        public String APELLIDOMATERNO { get; set; }
        public String APELLIDOPATERNO { get; set; }
        public String BANCOMONEDAEXTRANJERA { get; set; }
        public String BANCOMONEDALOCAL { get; set; }
        public String BREVETE { get; set; }
        public String BUSQUEDA { get; set; }
        public String CARNETEXTRANJERIA { get; set; }
        public String CELULAR { get; set; }
        public String CELULAREMERGENCIA { get; set; }
        public String CIUDADNACIMIENTO { get; set; }
        public String CLASEPERSONACODIGO { get; set; }
        public String CODIGOBARRAS { get; set; }
        public String CODIGOLDN { get; set; }
        public String CODIGOPOSTAL { get; set; }
        public String CORREOELECTRONICO { get; set; }
        public String CUENTAMONEDAEXTRANJERA { get; set; }
        public String CUENTAMONEDALOCAL { get; set; }
        public String DEPARTAMENTO { get; set; }
        public String DIRECCION { get; set; }
        public String DIRECCIONEMERGENCIA { get; set; }
        public String DOCUMENTO { get; set; }
        public String DOCUMENTOFISCAL { get; set; }
        public String DOCUMENTOIDENTIDAD { get; set; }
        public String DOCUMENTOMILITARFA { get; set; }
        public String ENFERMEDADGRAVEFLAG { get; set; }
        public String ESCLIENTE { get; set; }
        public String ESEMPLEADO { get; set; }
        public String ESOTRO { get; set; }
        public String ESPROVEEDOR { get; set; }
        public String ESTADO { get; set; }
        public String ESTADOCIVIL { get; set; }
        public String FAX { get; set; }
        public String FECHANACIMIENTO { get; set; }
        public String FLAGACTUALIZACION { get; set; }
        public String GRUPOEMPRESARIAL { get; set; }
        public String INGRESOAPLICACIONCODIGO { get; set; }
        public object INGRESOFECHAREGISTRO { get; set; }
        public String INGRESOUSUARIO { get; set; }
        public String LUGARNACIMIENTO { get; set; }
        public String MSREPL_TRAN_VERSION { get; set; }
        public String NACIONALIDAD { get; set; }
        public String NIVELINSTRUCCION { get; set; }
        public String NOMBRECODIGOPOSTAL { get; set; }
        public String NOMBRECOMPLETO { get; set; }
        public String NOMBREDEPARTAMENTE { get; set; }
        public String NOMBREEMERGENCIA { get; set; }
        public String NOMBREPROVINICA { get; set; }
        public String NOMBRES { get; set; }
        public String ORIGEN { get; set; }
        public String PAISEMISOR { get; set; }
        public String PARENTESCOEMERGENCIA { get; set; }
        public String PASAPORTE { get; set; }
        public String PERSONA { get; set; }
        public String PERSONAANT { get; set; }
        public String PERSONACLASIFICACION { get; set; }
        public String PROVINCIA { get; set; }
        public String PYMEFLAG { get; set; }
        public String SEXO { get; set; }
        public String SUNATDOMICILIADO { get; set; }
        public String SUNATNACIONALIDAD { get; set; }
        public String SUNATNACIONALIDADTEXTO { get; set; }
        public String SUNATUBIGEO { get; set; }
        public String SUNATUBIGEOTEXTO { get; set; }
        public String SUNATVIA { get; set; }
        public String SUNATVIATEXTO { get; set; }
        public String SUNATZONA { get; set; }
        public String SUNATZONATEXTO { get; set; }
        public String TARJETADECREDITO { get; set; }
        public String TELEFONO { get; set; }
        public String TELEFONOEMERGENCIA { get; set; }
        public String TIPOBREVETE { get; set; }
        public String TIPOCUENTAEXTRANJERA { get; set; }
        public String TIPOCUENTALOCAL { get; set; }
        public String TIPODOCUMENTO { get; set; }
        public String TIPOPERSONA { get; set; }
        public String TIPOPERSONAUSUARIO { get; set; }
        public String ULTIMAFECHAMODIF { get; set; }
        public String ULTIMOUSUARIO { get; set; }
        public String URBANIZACION { get; set; }
    }
}
