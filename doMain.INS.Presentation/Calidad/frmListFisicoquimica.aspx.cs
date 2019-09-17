using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using doMain.Utils;
using doMain.INS.Entity;
using doMain.INS.Business;
using System.Data.OleDb;
using System.Text;


namespace doMain.INS.Presentation.Calidad
{
    public partial class frmListFisicoquimica : System.Web.UI.Page
    {
        protected int Dias;
        public DataKey keyExpediente;
        public DataKey keyLaboratorio;
        public DataKey keyReprogramacion;
        protected static int PAGE_SIZE;
        private string errores = string.Empty;
        protected static int currentPageNumber;
        protected int inStartRowIndex, inEndRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Asignacion de Variables                
                //HideArea.Value = Convert.ToString(Session["IdArea"]);
                HideArea.Value = "1";

                //Paginacion Gridview
                currentPageNumber = 1;
                PAGE_SIZE = 10;

                //Mostrar plazo de dias                
                lblPlazo.Text = "Plazo atención interno: " + Convert.ToString(ObtenerPlazosAlertasInternas()) + " días.";


                //Lista los expedientes gridview y Listas Maestras
                ListEstado();
                ListMotivo();
                ListEnsayo();
                ListEvaluador();
                ListTipoCliente();
                ListTipoClasificacion();                               
                ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);

            }
        }


        #region "METODOS Y EVENTOS DE PAGINA"

        #region "EVENTOS"

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void GetPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber = 1;
                    break;

                case "Previous":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber = Int32.Parse(lblTotalPages.Text);
                    break;
            }

            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void ddlRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            gdvExpediente.PageSize = Int32.Parse(ddlRows.SelectedValue);
            PAGE_SIZE = Int32.Parse(ddlRows.SelectedValue);
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void gdvExpediente_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyExpediente = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(index)];                      

            HideIdExpediente.Value = Convert.ToString(keyExpediente["IDEXPEDIENTE"]);
            string Expediente = Convert.ToString(keyExpediente["CodigoExpediente"]);
            txtEExpediente.Text = Expediente;

            if (e.CommandName == "Actualizar")
            {
                
                HideAccion.Value = Convert.ToString(Constante.TipoGrabado.ACTUALIZA);                
               
               
                btnRegistro_ModalPopupExtender.Show();
            }
            else if (e.CommandName == "Evaluar")
            {
                //Hide/Show Botones
                ImgActEval.Visible = false;
                ImgAddEval.Visible = true;
                LimpiarControlesLaboratorio();
                ListLaboratorio(Convert.ToInt32(HideIdExpediente.Value));
                btnEnsayo_ModalPopupExtender.Show();
               
            }
            else if (e.CommandName == "Reprogramar")
            {
                //Hide/Show Botones
                ImgActReprog.Visible = false;
                ImgAddReprog.Visible = true;

                txtRepExpediente.Text = Expediente;
                //ListReprogramacion(Convert.ToInt32(HideIdExpediente.Value));
                btnReprogramacion_ModalPopupExtender.Show();
            }

        }
        protected void gdvExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataKey key = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(e.Row.RowIndex)];

                //Mostrar fechas nulas en blanco
                Label lblgvFecIngreso = (Label)e.Row.FindControl("lblgvFecIngreso");
                if (lblgvFecIngreso.Text.Trim() == "01/01/1900")
                {
                    lblgvFecIngreso.Text = string.Empty;
                }

                Label lblgvFecIngSIGEL = (Label)e.Row.FindControl("lblgvFecIngSIGEL");
                if (lblgvFecIngreso.Text.Trim() == "01/01/1900")
                {
                    lblgvFecIngSIGEL.Text = string.Empty;
                }

                //Pintar Alertas
                Label lblAlerta = (Label)e.Row.FindControl("lblAlerta");
                Image ImgAlertaInterno = (Image)e.Row.FindControl("ImgAlertaInt");

                if (Convert.ToInt32(lblAlerta.Text) >= Convert.ToInt32(Constante.Numeros.Cero) && Convert.ToInt32(lblAlerta.Text) <= Convert.ToInt32(Constante.Numeros.Cuatro))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_green.png";
                }
                else if (Convert.ToInt32(lblAlerta.Text) == Convert.ToInt32(Constante.Numeros.Cinco))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_yellow.png";
                }
                else if (Convert.ToInt32(lblAlerta.Text) > Convert.ToInt32(Constante.Numeros.Seis))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_red.png";
                }

                //Colorea las columnas de la cabeceras del gridview                           
                gdvExpediente.Columns[8].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[9].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[10].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[11].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[12].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[13].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[14].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[15].HeaderStyle.BackColor = System.Drawing.Color.Orange;

            }

        }
        protected void btnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelPlantilla(Response);
        }

        #endregion

        #region "METODOS"
        private int ObtenerPlazosAlertasInternas()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Padre, Parametro;

            try
            {
                Padre = Convert.ToInt32(Constante.Numeros.Cuatro);
                Parametro = Convert.ToInt32(Constante.Numeros.Cinco);

                var lstConfiguracion = new ConfiguracionBLL().GetListValue(Padre, Parametro);

                Dias = Convert.ToInt32(lstConfiguracion.Where(pair => pair.Value == Convert.ToString(Constante.Area.FISICOQUIMICA))
                              .Select(pair => pair.Key)
                              .FirstOrDefault());

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

            return Dias;
        }
        private int GetTotalPages(double totalRows)
        {
            int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);

            return totalPages;
        }
        private void ListTipoCliente()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;
            DataTable dt = new DataTable();

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Uno);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                dt.Columns.Add("key");
                dt.Columns.Add("Value");

                foreach (var row in lstConfiguracion)
                {
                    if (Convert.ToInt32(row.Key) != Convert.ToInt32(Constante.Numeros.Uno)) //PARTICULAR
                    {
                        dt.Rows.Add(row.Key, row.Value);
                    }
                }              

                ddlFilTipoCliente.DataSource = lstConfiguracion;
                ddlFilTipoCliente.DataValueField = "Key";
                ddlFilTipoCliente.DataTextField = "Value";
                ddlFilTipoCliente.DataBind();
                ddlFilTipoCliente.Items.Insert(0, new ListItem("-- TODOS --", "0"));            

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListTipoClasificacion()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;
            DataTable dt = new DataTable();

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Ocho);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                dt.Columns.Add("key");
                dt.Columns.Add("Value");

                foreach (var row in lstConfiguracion)
                {
                    if (Convert.ToInt32(row.Key) != Convert.ToInt32(Constante.Numeros.Uno)) //PARTICULAR
                    {
                        dt.Rows.Add(row.Key, row.Value);
                    }
                }

                ddlClasificacion.DataSource = lstConfiguracion;
                ddlClasificacion.DataValueField = "Key";
                ddlClasificacion.DataTextField = "Value";
                ddlClasificacion.DataBind();
                ddlClasificacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListEstado()
        {
            EstadoBLL capanegocios = new EstadoBLL();

            try
            {
                var lstEstado = new EstadoBLL().List();

                ddlFilEstado.DataSource = lstEstado;
                ddlFilEstado.DataValueField = "IdEstado";
                ddlFilEstado.DataTextField = "Nombre";
                ddlFilEstado.DataBind();
                ddlFilEstado.Items.Insert(0, new ListItem("-- TODOS --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListEnsayo()
        {
            EnsayoBLL capanegocios = new EnsayoBLL();

            try
            {

                var lstEnsayo = new EnsayoBLL().List();
                ddlFilEnsayo.DataSource = lstEnsayo;
                ddlFilEnsayo.DataValueField = "IdEnsayo";
                ddlFilEnsayo.DataTextField = "Nombre";
                ddlFilEnsayo.DataBind();
                ddlFilEnsayo.Items.Insert(0, new ListItem("-- TODOS --", "0"));

                ddlRepEnsayo.DataSource = lstEnsayo;
                ddlRepEnsayo.DataValueField = "IdEnsayo";
                ddlRepEnsayo.DataTextField = "Nombre";
                ddlRepEnsayo.DataBind();
                ddlRepEnsayo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ddlEEnsayo.DataSource = lstEnsayo;
                ddlEEnsayo.DataValueField = "IdEnsayo";
                ddlEEnsayo.DataTextField = "Nombre";
                ddlEEnsayo.DataBind();
                ddlEEnsayo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListEvaluador()
        {
            EvaluadorBLL capanegocios = new EvaluadorBLL();

            try
            {

                var lstEvaluador = new EvaluadorBLL().List();
                ddlFilAnalista.DataSource = lstEvaluador;
                ddlFilAnalista.DataValueField = "IdEvaluador";
                ddlFilAnalista.DataTextField = "Evaluador";
                ddlFilAnalista.DataBind();
                ddlFilAnalista.Items.Insert(0, new ListItem("-- TODOS --", "0"));

                ddlRepAnalista.DataSource = lstEvaluador;
                ddlRepAnalista.DataValueField = "IdEvaluador";
                ddlRepAnalista.DataTextField = "Evaluador";
                ddlRepAnalista.DataBind();
                ddlRepAnalista.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ddlEAnalista.DataSource = lstEvaluador;
                ddlEAnalista.DataValueField = "IdEvaluador";
                ddlEAnalista.DataTextField = "Evaluador";
                ddlEAnalista.DataBind();
                ddlEAnalista.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void ListMotivo()
        {
            MotivoBLL capanegocios = new MotivoBLL();

            try
            {
                var lstMotivo = new MotivoBLL().List();

                ddlRepMotivo.DataSource = lstMotivo;
                ddlRepMotivo.DataValueField = "IdMotivo";
                ddlRepMotivo.DataTextField = "Nombre";
                ddlRepMotivo.DataBind();
                ddlRepMotivo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListbyCriteriosExpediente(int pIdArea, bool aIsCompleteData)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();
            Double Total;


            try
            {
                inStartRowIndex = ((currentPageNumber - 1) * PAGE_SIZE) + 1;

                if (aIsCompleteData)
                {
                    inEndRowIndex = (currentPageNumber * PAGE_SIZE);
                }
                else
                {
                    inEndRowIndex = (currentPageNumber * PAGE_SIZE);
                }

                objeto.StartRowIndex = inStartRowIndex;
                objeto.EndRowIndex = inEndRowIndex;

                if (txtFilExpediente.Text.Trim() != string.Empty)
                { objeto.CodigoExpediente = txtFilExpediente.Text.Trim(); }
                else
                { objeto.CodigoExpediente = "*"; }

                if (txtFilCotizacion.Text.Trim() != string.Empty)
                { objeto.CodigoCotizacion = txtFilCotizacion.Text.Trim(); }
                else
                { objeto.CodigoCotizacion = "*"; }

                objeto.IdTipoCliente = Convert.ToInt32(ddlFilTipoCliente.SelectedValue);

                if (txtFilCliente.Text.Trim() != string.Empty)
                { objeto.Cliente = txtFilCliente.Text.Trim(); }
                else
                { objeto.Cliente = "*"; }

                objeto.Producto = txtFilProducto.Text.Trim();

                if (txtFilFecCotiz.Text == string.Empty)
                {
                    objeto.FechaCotizacion = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                }
                else
                { objeto.FechaCotizacion = Convert.ToDateTime(txtFilFecCotiz.Text); }

               
                objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
               

                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(Constante.Numeros.Cero);

                var lstExpediente = new ExpedienteBLL().ListExpedienteFisicoQuimico(objeto);
                gdvExpediente.DataSource = lstExpediente;
                gdvExpediente.DataBind();

                if (lstExpediente.Count > 0)
                {
                    Total = lstExpediente[0].NroExpediente;

                    lblContador.Text = "Total de Expediente(s): " + Total + " registro(s).";

                    lblTotalPages.Text = GetTotalPages(Total).ToString();

                    ddlPage.Items.Clear();
                    for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
                    {
                        ddlPage.Items.Add(new ListItem(i.ToString()));
                    }
                    ddlPage.SelectedValue = currentPageNumber.ToString();
                }

                if (currentPageNumber == 1)
                {
                    lnkbtnPre.Enabled = false;
                    lnkbtnPre.CssClass = "GridPagePreviousInactive";
                    lnkbtnFirst.Enabled = false;
                    lnkbtnFirst.CssClass = "GridPageFirstInactive";

                    if (Int32.Parse(lblTotalPages.Text) > 0)
                    {
                        lnkbtnNext.Enabled = true;
                        lnkbtnNext.CssClass = "GridPageNextActive";
                        lnkbtnLast.Enabled = true;
                        lnkbtnLast.CssClass = "GridPageLastActive";
                    }
                    else
                    {
                        lnkbtnNext.Enabled = false;
                        lnkbtnNext.CssClass = "GridPageNextInactive";
                        lnkbtnLast.Enabled = false;
                        lnkbtnLast.CssClass = "GridPageLastInactive";
                    }
                }

                else
                {
                    lnkbtnPre.Enabled = true;
                    lnkbtnPre.CssClass = "GridPagePreviousActive";
                    lnkbtnFirst.Enabled = true;
                    lnkbtnFirst.CssClass = "GridPageFirstActive";

                    if (currentPageNumber == Int32.Parse(lblTotalPages.Text))
                    {
                        lnkbtnNext.Enabled = false;
                        lnkbtnNext.CssClass = "GridPageNextInactive";
                        lnkbtnLast.Enabled = false;
                        lnkbtnLast.CssClass = "GridPageLastInactive";
                    }
                    else
                    {
                        lnkbtnNext.Enabled = true;
                        lnkbtnNext.CssClass = "GridPageNextActive";
                        lnkbtnLast.Enabled = true;
                        lnkbtnLast.CssClass = "GridPageLastActive";
                    }
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ExportarExcelPlantilla(System.Web.HttpResponse Response)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            DataTable dtExpediente = new DataTable();

            try
            {
                if (txtFilExpediente.Text.Trim() != string.Empty)
                { objeto.CodigoExpediente = txtFilExpediente.Text.Trim(); }
                else
                { objeto.CodigoExpediente = "*"; }

                if (txtFilCotizacion.Text.Trim() != string.Empty)
                { objeto.CodigoCotizacion = txtFilCotizacion.Text.Trim(); }
                else
                { objeto.CodigoCotizacion = "*"; }

                objeto.IdTipoCliente = Convert.ToInt32(ddlFilTipoCliente.SelectedValue);

                if (txtFilCliente.Text.Trim() != string.Empty)
                { objeto.Cliente = txtFilCliente.Text.Trim(); }
                else
                { objeto.Cliente = "*"; }

                objeto.Producto = txtFilProducto.Text.Trim();

                if (txtFilFecCotiz.Text == string.Empty)
                {
                    objeto.FechaCotizacion = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                }
                else
                { objeto.FechaCotizacion = Convert.ToDateTime(txtFilFecCotiz.Text); }


                objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());


                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(Constante.Numeros.Cero);

                var lstExpediente = new ExpedienteBLL().ExportExpediente(objeto);

                //crear datatable para enviar al metodo de descarga
                var dataTableDescargar = new System.Data.DataTable(Guid.NewGuid().ToString());
                var Expediente = new DataColumn("Expediente", typeof(string));                
                var Cliente = new DataColumn("Cliente", typeof(string));
                var TipoCliente = new DataColumn("TipoCliente", typeof(string));
                var Producto = new DataColumn("Producto", typeof(string));
                var Lote = new DataColumn("Lote", typeof(string));
                var DCI = new DataColumn("DCI", typeof(string));
                var Clasificacion = new DataColumn("Clasificacion", typeof(string));
                var Red = new DataColumn("Red", typeof(string));
                var Norma = new DataColumn("Norma", typeof(string));
                var Estado = new DataColumn("Estado", typeof(string));
                var FechaIngreso = new DataColumn("FechaIngreso", typeof(string));
                var FechaIngresoSIGEL = new DataColumn("FechaIngresoSIGEL", typeof(string));                

                dataTableDescargar.Columns.AddRange(new DataColumn[] {
                    Expediente, Cliente, TipoCliente, Producto, Lote, DCI, Clasificacion, Red, Norma, Estado, FechaIngreso, FechaIngresoSIGEL
                });

                //Llenar data al datatable
                foreach (ExpedienteBE oExpediente in lstExpediente)
                {
                    DataRow RowFile = dataTableDescargar.NewRow();

                    RowFile["Expediente"] = oExpediente.CodigoExpediente;                                       
                    RowFile["Cliente"] = oExpediente.Cliente;
                    RowFile["TipoCliente"] = oExpediente.TipoCliente;
                    RowFile["Producto"] = oExpediente.Producto;
                    RowFile["Lote"] = oExpediente.Lote;
                    RowFile["DCI"] = oExpediente.DCI;
                    RowFile["Clasificacion"] = oExpediente.Clasificacion;
                    RowFile["Red"] = oExpediente.Red;
                    RowFile["Norma"] = oExpediente.Norma;
                    RowFile["Estado"] = oExpediente.Estado;

                    if (oExpediente.FechaIngreso != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaIngreso"] = string.Format("{0:d}", oExpediente.FechaIngreso);
                    }
                    else
                    { RowFile["FechaIngreso"] = string.Empty; }

                    if (oExpediente.FechaIngresoSIGEL != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaIngresoSIGEL"] = string.Format("{0:d}", oExpediente.FechaIngresoSIGEL);
                    }
                    else
                    { RowFile["FechaIngresoSIGEL"] = string.Empty; }                  

                    dataTableDescargar.Rows.Add(RowFile);
                }

                ExportarUtils ObjExporExcel = new ExportarUtils();

                object[] pNombreCabeceras = new object[] { "Expediente", "Cliente", "Tipo Cliente", "Producto", "Lote", "DCI", "Clasificacion", "Red", "Norma", "Estado", "Fecha Ingreso", "Fecha Ingreso SIGEL"};
                object[] pEstilos = new object[] { new object[] { 11, OleDbType.Date }, new object[] { 12, OleDbType.Date }};
                string MessageOut = ObjExporExcel.DescargarDataTableToExcel(dataTableDescargar, "Archivo Seguimiento Expediente", pNombreCabeceras, pEstilos, Page.Response);

                if (MessageOut != "OK")
                {
                    //objMsgBox.MsgBoxCC(lblMensaje, MessageOut, "Error");
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }

        #endregion

        #endregion


        #region "METODOS Y EVENTOS LABORATORIO"

        #region "EVENTOS"

        protected void ImgAddEval_Click(object sender, EventArgs e)
        {
            SaveLaboratorio();
            LimpiarControlesLaboratorio();
            ListLaboratorio(Convert.ToInt32(HideIdExpediente.Value));
            btnEnsayo_ModalPopupExtender.Show();
        }
        protected void ImgActEval_Click(object sender, EventArgs e)
        {
            //Hide/Show Botones
            ImgActEval.Visible = false;
            ImgAddEval.Visible = true;

            UpdateLaboratorio();
            LimpiarControlesLaboratorio();
            ListLaboratorio(Convert.ToInt32(HideIdExpediente.Value));
            btnEnsayo_ModalPopupExtender.Show();
        }
        protected void gdvLaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyExpediente = (DataKey)gdvLaboratorio.DataKeys[Convert.ToInt32(index)];
            keyLaboratorio = (DataKey)gdvLaboratorio.DataKeys[Convert.ToInt32(index)];

            //HideIdExpediente.Value = Convert.ToString(keyExpediente["IDEXPEDIENTE"]);
            HideIdLaboratorio.Value = Convert.ToString(keyLaboratorio["IDLABORATORIO"]);

            if (e.CommandName == "Actualizar")
            {
                //Hide/Show Botones
                ImgActEval.Visible = true;
                ImgAddEval.Visible = false;
                GetLaboratorio(Convert.ToInt32(HideIdLaboratorio.Value));
            }
            else if (e.CommandName == "Eliminar")
            {
                DeleteLaboratorio(Convert.ToInt32(HideIdLaboratorio.Value));
                
            }

            ListLaboratorio(Convert.ToInt32(HideIdExpediente.Value));
            btnEnsayo_ModalPopupExtender.Show();

        }

        #endregion

        #region "METODOS"
        
        private void LimpiarControlesLaboratorio()
        {

            txtEOrden.Text = string.Empty;
            txtEFecVenc.Text = string.Empty;
            txtEFecEntrega.Text = string.Empty;
            txtEConfirmacion.Text = string.Empty;
            txtEPesquisa.Text = string.Empty;
            txtEEnsayoHPLC.Text = string.Empty;
            txtECondicion.Text = string.Empty;
            txtEObservacion.Text = string.Empty;
            ddlEEnsayo.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlEAnalista.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            

        }
        private void SaveLaboratorio()
        {
            LaboratorioBLL capanegocios = new LaboratorioBLL();
            LaboratorioBE objeto = new LaboratorioBE();

            try
            {
                objeto.IdExpediente = Convert.ToInt32(HideIdExpediente.Value);
                objeto.OrdenServicio = Convert.ToString(txtEOrden.Text.Trim());
                objeto.IdEnsayo = Convert.ToInt32(ddlEEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlEAnalista.SelectedValue);                
                objeto.FechaVencimiento = Convert.ToDateTime(txtEFecVenc.Text.Trim());
                objeto.FechaEntregaMax = Convert.ToDateTime(txtEFecEntrega.Text.Trim());
                objeto.Confirmacion = Convert.ToString(txtEConfirmacion.Text);
                objeto.Pesquisa = Convert.ToString(txtEPesquisa.Text);
                objeto.EnsayoHPLC = Convert.ToString(txtEEnsayoHPLC.Text);
                objeto.Condicion = Convert.ToString(txtECondicion.Text);
                objeto.Observaciones = Convert.ToString(txtEObservacion.Text);
                capanegocios.SaveLaboratorio(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void UpdateLaboratorio()
        {
            LaboratorioBLL capanegocios = new LaboratorioBLL();
            LaboratorioBE objeto = new LaboratorioBE();

            try
            {
                objeto.IdLaboratorio = Convert.ToInt32(HideIdLaboratorio.Value);
                objeto.OrdenServicio = Convert.ToString(txtEOrden.Text.Trim());
                objeto.IdEnsayo = Convert.ToInt32(ddlEEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlEAnalista.SelectedValue);
                objeto.FechaVencimiento = Convert.ToDateTime(txtEFecVenc.Text.Trim());
                objeto.FechaEntregaMax = Convert.ToDateTime(txtEFecEntrega.Text.Trim());
                objeto.Confirmacion = Convert.ToString(txtEConfirmacion.Text);
                objeto.Pesquisa = Convert.ToString(txtEPesquisa.Text);
                objeto.EnsayoHPLC = Convert.ToString(txtEEnsayoHPLC.Text);
                objeto.Condicion = Convert.ToString(txtECondicion.Text);
                objeto.Observaciones = Convert.ToString(txtEObservacion.Text);

                capanegocios.UpdateLaboratorio(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void DeleteLaboratorio(int pIdLaboratorio)
        {
            LaboratorioBLL capanegocios = new LaboratorioBLL();
            LaboratorioBE objeto = new LaboratorioBE();

            try
            {
                objeto.IdLaboratorio = pIdLaboratorio;
                
                capanegocios.DeleteLaboratorio(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void ListLaboratorio(int pIdExpediente)
           {
            LaboratorioBLL capanegocios = new LaboratorioBLL();
            LaboratorioBE objeto = new LaboratorioBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                var lstLaboratorio = new LaboratorioBLL().List(objeto);

                gdvLaboratorio.DataSource = lstLaboratorio;
                gdvLaboratorio.DataBind();

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void GetLaboratorio(int pIdLaboratorio)
        {
            LaboratorioBLL capanegocios = new LaboratorioBLL();
            LaboratorioBE objeto = new LaboratorioBE();

            try
            {
                objeto.IdLaboratorio = pIdLaboratorio;
                var lstLaboratorio = new LaboratorioBLL().GetLaboratorio(objeto);


                foreach (LaboratorioBE oLabotorio in lstLaboratorio)
                {
                    txtEOrden.Text = oLabotorio.OrdenServicio;

                    ListEnsayo();
                    ddlEEnsayo.SelectedValue = Convert.ToString(oLabotorio.IdEnsayo);

                    ListEvaluador();
                    ddlEAnalista.SelectedValue = Convert.ToString(oLabotorio.IdAnalista);

                    txtEFecVenc.Text = string.Format("{0:d}", oLabotorio.FechaVencimiento);
                    txtEFecEntrega.Text = string.Format("{0:d}", oLabotorio.FechaEntregaMax);

                    txtEConfirmacion.Text = Convert.ToString(oLabotorio.Confirmacion);
                    txtEPesquisa.Text = Convert.ToString(oLabotorio.Pesquisa);
                    txtEEnsayoHPLC.Text = Convert.ToString(oLabotorio.EnsayoHPLC);
                    txtECondicion.Text = Convert.ToString(oLabotorio.Condicion);
                    txtEObservacion.Text = Convert.ToString(oLabotorio.Observaciones);
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }

        #endregion

        #endregion

        #region "METODOS Y EVENTOS REPROGRAMACION"

        #region "EVENTOS"
        protected void ImgActReprog_Click(object sender, EventArgs e)
        {
            UpdateReprogramacion();

            //Hide/Show Botones
            ImgActReprog.Visible = false;
            ImgAddReprog.Visible = true;

            ListReprogramacion(Convert.ToInt32(HideIdExpediente.Value));
            btnReprogramacion_ModalPopupExtender.Show();
        }
        protected void ImgAddReprog_Click(object sender, EventArgs e)
        {
            SaveReprogramacion();
            LimpiarControlesReprogramacion();
            ListReprogramacion(Convert.ToInt32(HideIdExpediente.Value));
            btnReprogramacion_ModalPopupExtender.Show();
        }
        protected void gdvReprogramacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyReprogramacion = (DataKey)gdvReprogramacion.DataKeys[Convert.ToInt32(index)];

            HideIdReprogramacion.Value = Convert.ToString(keyReprogramacion["IDREPROGRAMACION"]);

            if (e.CommandName == "Actualizar")
            {
                //Hide/Show Botones
                ImgActReprog.Visible = true;
                ImgAddReprog.Visible = false;
                GetReprogramacionID(Convert.ToInt32(HideIdReprogramacion.Value));

            }
            else if (e.CommandName == "Eliminar")
            {

                DeleteReprogramacion(Convert.ToInt32(HideIdReprogramacion.Value));
            }

            ListReprogramacion(Convert.ToInt32(HideIdExpediente.Value));
            btnReprogramacion_ModalPopupExtender.Show();
        }

        #endregion

        #region "METODOS"

        private void LimpiarControlesReprogramacion()
        {
            txtRepOficio.Text = string.Empty;
            txtRepFecOficio.Text = string.Empty;
            txtRepCorreo.Text = string.Empty;
            txtRepPlazo.Text = string.Empty;
            txtRepOficioRpta.Text = string.Empty;
            txtRepFecOficioRpta.Text = string.Empty;

            ddlRepEnsayo.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlRepAnalista.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlRepMotivo.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
        }
        private void ListReprogramacion(int pIdExpediente)
        {
            ReprogramacionBLL capanegocios = new ReprogramacionBLL();
            ReprogramacionBE objeto = new ReprogramacionBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                var lstReprogramacion = new ReprogramacionBLL().GetReprogramacionbyExpediente(objeto);

                gdvReprogramacion.DataSource = lstReprogramacion;
                gdvReprogramacion.DataBind();

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void GetReprogramacionID(int pIdReprogramacion)
        {
            ReprogramacionBLL capanegocios = new ReprogramacionBLL();
            ReprogramacionBE objeto = new ReprogramacionBE();

            try
            {
                objeto.IdReprogramacion = pIdReprogramacion;
                var lstReprogramacion = new ReprogramacionBLL().GetReprogramacion(objeto);


                foreach (ReprogramacionBE oReprogramacion in lstReprogramacion)
                {
                    txtRepOficio.Text = oReprogramacion.Oficio;
                    txtRepFecOficio.Text = string.Format("{0:d}", oReprogramacion.FechaOficio);
                    txtRepCorreo.Text = string.Format("{0:d}", oReprogramacion.FechaCorreo);

                    ListEnsayo();
                    ddlRepEnsayo.SelectedValue = Convert.ToString(oReprogramacion.IdEnsayo);

                    ListEvaluador();
                    ddlRepAnalista.SelectedValue = Convert.ToString(oReprogramacion.IdAnalista);

                    txtRepPlazo.Text = Convert.ToString(oReprogramacion.Plazo);

                    ListMotivo();
                    ddlRepMotivo.SelectedValue = Convert.ToString(oReprogramacion.IdMotivo);

                    txtRepOficioRpta.Text = Convert.ToString(oReprogramacion.OficioRpta);
                    txtRepFecOficioRpta.Text = string.Format("{0:d}", oReprogramacion.FechaOficioRpta);
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void UpdateReprogramacion()
        {
            ReprogramacionBLL capanegocios = new ReprogramacionBLL();
            ReprogramacionBE objeto = new ReprogramacionBE();

            try
            {
                objeto.IdReprogramacion = Convert.ToInt32(HideIdReprogramacion.Value);
                objeto.Oficio = Convert.ToString(txtRepOficio.Text.Trim());
                objeto.FechaOficio = Convert.ToDateTime(txtRepFecOficio.Text.Trim());
                objeto.FechaCorreo = Convert.ToDateTime(txtRepCorreo.Text.Trim());
                objeto.IdEnsayo = Convert.ToInt32(ddlRepEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlRepAnalista.SelectedValue);
                objeto.Plazo = Convert.ToInt32(txtRepPlazo.Text);
                objeto.IdMotivo = Convert.ToInt32(ddlRepMotivo.SelectedValue);
                objeto.OficioRpta = Convert.ToString(txtRepOficioRpta.Text.Trim());
                objeto.FechaOficioRpta = Convert.ToDateTime(txtRepFecOficioRpta.Text.Trim());


                capanegocios.UpdateReprogramacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void SaveReprogramacion()
        {
            ReprogramacionBLL capanegocios = new ReprogramacionBLL();
            ReprogramacionBE objeto = new ReprogramacionBE();

            try
            {
                objeto.IdExpediente = Convert.ToInt32(HideIdExpediente.Value);
                objeto.Oficio = Convert.ToString(txtRepOficio.Text.Trim());
                objeto.FechaOficio = Convert.ToDateTime(txtRepFecOficio.Text.Trim());
                objeto.FechaCorreo = Convert.ToDateTime(txtRepCorreo.Text.Trim());
                objeto.IdEnsayo = Convert.ToInt32(ddlRepEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlRepAnalista.SelectedValue);
                objeto.Plazo = Convert.ToInt32(txtRepPlazo.Text);
                objeto.IdMotivo = Convert.ToInt32(ddlRepMotivo.SelectedValue);
                objeto.OficioRpta = Convert.ToString(txtRepOficioRpta.Text.Trim());
                objeto.FechaOficioRpta = Convert.ToDateTime(txtRepFecOficioRpta.Text.Trim());


                capanegocios.SaveReprogramacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void DeleteReprogramacion(int pIdReprogramacion)
        {
            ReprogramacionBLL capanegocios = new ReprogramacionBLL();
            ReprogramacionBE objeto = new ReprogramacionBE();

            try
            {
                objeto.IdReprogramacion = pIdReprogramacion;
                capanegocios.DeleteReprogramacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }

        #endregion

        #endregion
    }
}
