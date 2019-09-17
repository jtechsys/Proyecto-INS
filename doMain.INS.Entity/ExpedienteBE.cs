using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class ExpedienteBE
    {
        public int IdExpediente { get; set; }
        public string CodigoCotizacion { get; set; }
        public string CodigoExpediente { get; set; }
        public string Cliente { get; set; }
        public int IdTipoCliente { get; set; }
        public string TipoCliente { get; set; }
        public string Situacion { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public string Procedencia { get; set; }    
        public int IdProducto { get; set;}
        public string Producto { get; set; }
        public string Lote { get; set;}
        public DateTime FechaVencimiento { get; set; }
        public string Correo { get; set; }
        public string DocumentoFisico { get; set; }
        public string DocumentoAnexo { get; set; }
        public int IdEnsayo { get; set; }
        public int IdAnalista { get; set; }
        public int IdProcedencia { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuarioActualiza { get; set; }
        public DateTime FechaActualiza { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string MotivoAnulacion { get; set; }
        public int NroEvaluacion { get; set; }
        public int NroExpediente { get; set; }
        public int Alerta { get; set;}
        public string Oficio { get; set; }
        public DateTime FechaOficio { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string ActaPesquisa { get; set; }
        public DateTime FechaPesquisa { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DocumentoCustodia { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
        public int IdClaseProducto { get; set; }
        public string ClaseProducto { get; set; }
        public int CantidadCustodia { get; set; }
        public string CondicionAmbiental { get; set; }
        public string ContraMuestra { get; set; }
        public string CamaraFria { get; set; }
        public string PreCamara { get; set; }
        public DateTime FechaIngresoLab { get; set; }
        public DateTime FechaEntregaCliente { get; set; }
        public string ContramuestraCER { get; set; }
        public string ContramuestraFQ { get; set; }
        public string ContramuestraMicrob { get; set; }
        public string MuestraOficio { get; set; }
        public int MuestraCantidad { get; set; }
        public int IdAreaRetiro1 { get; set; }
        public string AreaRetiro1 { get; set; }
        public int CantidadRetiro1 { get; set; }        
        public int IdAreaRetiro2 { get; set; }
        public string AreaRetiro2 { get; set; }
        public int CantidadRetiro2 { get; set; }
        public Decimal Saldo { get; set; }
        public string MuestraUbicacion { get; set; }
        public DateTime FechaRecepcionIE { get; set; }
        public string InformeEnsayo { get; set; }
        public int Conclusion { get; set; }
        public string NombreConclusion { get; set; }
        public string Proforma { get; set; }
        public string Factura { get; set; }
        public DateTime FechaEntregaPool { get; set; }
        public int Muestras { get; set; }
        public string NombreMuestras { get; set; }
        public int NroReprogramacion { get; set; }
        public string DCI { get; set; }
        public string Clasificacion { get; set; }
        public int Red { get; set; }
        public string Norma { get; set; }
        public DateTime FechaIngresoSIGEL { get; set; }
        public DateTime FechaEntregaRM { get; set; }
        public DateTime FechaEntregaEval { get; set; }
        public DateTime FechaOrdenServicio { get; set; }
        public DateTime FechaEmisionResult { get; set; }
        public string InformeResult { get; set; }
        public string InformeNroEnsayoResult { get; set; }
        public string InformeVersionResult { get; set; }
        public string InformeConclResult { get; set; }
        public string InformeObserResult { get; set; }
        public string OficioComunicacion { get; set; }
        public DateTime FechaOficioComunicacion { get; set; }
        public DateTime FechaCargoOficio { get; set; }               


        /* Atributos de Paginacion */
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public string SortRow { get; set; }

        /* Referencia a otras clases */
        public EvaluadorBE Evaluador { get; set; }
        public EnsayoBE Ensayo { get; set; }
        public EvaluacionBE Evaluacion { get; set; }
        public UsuarioBE Usuario { get; set; }

    }
}
