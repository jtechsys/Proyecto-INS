using System;
using System.Collections.Generic;
using System.Text;

namespace pryUtil
{
    public enum enumGeneric
    {
        Default = 0,
        InfoMessage = 1,
        ErrorMessage = 2,

        ServiceError = -200,
        DataBaseError = -300,
        DataBaseRaiseError = -350,
        NoRecords = -400,
        ProxyError = -500,
    }

    public enum enumMessage
    {
        Information = 1,
        Warning = 2,
    }

    public enum enumDialog
    {
        OK = 1,
        Cancel = 2,
    }

    public enum enumTipoEnvioCorreo
    {
        EnvioAutomaticoNomina = 1,
        Alerta = 2,
        ReporteAutomatico = 3,
    }

    public enum enumLogEnvioCorreo
    {
        Enviado = 1,
        Cancelado = 2,
        Error = 3,
    }

    public enum enumEstadoLogEnvio
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Errores = 3,
        Fatal = 4,
    }

    public enum enumComboRecord
    {
        Select = 1,
        All = 2,
        None = 0
    }

    public enum enumProcessType
    {
        Ninguno = 0,
        Envio = 1,
        Recepcion = 2,
        Conciliacion = 3
    }


     public static class clsConstantsGeneric
    {
        public static string NoRecords = "Sin Información.";
        public static string NoRecordsFull = "El datatable no contiene registros para los parametros ingresados.";
        public static string ServiceError = "Error de Servicio.";
        public static string DataBaseError = "Error en Base de datos.";
        public static string ProxyError = "El Servicio no Responde.";
        public static string RaiseError = "Error controlado de base de datos.";
        public static string UniqueKey = "Error, registro existente";
    }

    public static class clsMensajesGeneric
    {
        public static string ParametroVacio = "Error: el valor &1 no puede ser vacio";
        public static string CampoNoNumerico = "Error: el valor &1 no es numérico";
        public static string ResultadoVacio = "Error: la consulta del Proceso: &1, devolvio vacio para el campo: &2";
        public static string ProcesoYAGenerado = "Error: El Cliente: &1, ya tiene generado un Proceso para el periodo: &2 - &3";
        public static string ExcepcionControlada = "Excepcion controlada para el Proceso: &1. Tipo de error: &2, el mensaje fue: &3";

        public static string ProcesoMensajeEnviadoExitoso = "Info del Proceso: &1, del cliente: &2 - &3. Mensaje enviado exitosamente";
        public static string ProcesoMensajeEnviadoError = "Error del Proceso: &1, del cliente: &2 - &3. Error al enviar el Correo.";
        public static string ProcesoMensajeYaExiste = "Error del Proceso: &1, del cliente: &2 - &3. El Proceso ya existe";
        public static string ProcesoNoExisteCorreo = "Error del Proceso: &1, del cliente: &2 - &3. No existe correos asociados.";
        public static string ProcesoClienteError = "Error al obtener los datos del Cliente con tipo de documento: &1 y número de documento: &2";
        public static string ProcesoListaError = "Error: No se puede obtener la lista de Clientes disponibles";
        public static string ProcesoCantidadRegistros = "Total de Registros: &1, cantidad de registros procesados: &2, cantidad de registros erroneos: &3";
    }

    public static class clsTiposSystemParameters
    {
        public static string TipoDocumento = "TipoDocumento";
        public static string TipoEnvioCorreo = "TipoEnvioCorreo";
        public static string EstadoEnvioCorreo = "EstadoEnvioCorreo";
    }

    public class MailSource
    {
        private string _Mail;

        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }

        public MailSource(string pstrMail)
        {
            this._Mail = pstrMail;
        }
    }
}

