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
    public partial class frmListRM : System.Web.UI.Page
    {
        protected int Dias;
        protected int IdArea, IdExpediente, IdUsuario, IdRol;
        private string errores = string.Empty;
        protected int inStartRowIndex, inEndRowIndex;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE;
        public DataKey keyExpediente;
        public DataKey keyReprogramacion;
        public enum MessageType { Success, Error, Info, Warning };

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
                ListArea();
                ListEstado();
                ListMotivo();
                ListEnsayo();                                
                ListarMuestra();
                ListEvaluador();
                ListConclusion();
                ListTipoCliente();
                ListTipoProducto();
                ListClaseProducto();                
                ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);

            }

        }

        #region "EVENTOS"

        #region "EVENTO PAGINA"
        protected void gdvExpediente_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyExpediente = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(index)];
            string Expediente;

            HideIdExpediente.Value = Convert.ToString(keyExpediente["IDEXPEDIENTE"]);
            Expediente = Convert.ToString(keyExpediente["CodigoExpediente"]);

            if (e.CommandName == "Actualizar")
            {
                txtTabExpediente.Text = Expediente;
                HideAccion.Value = Convert.ToString(Constante.TipoGrabado.ACTUALIZA);
                GetExpedienteRecepcionMuestrabyId(Convert.ToInt32(HideIdExpediente.Value));
                btnRM_ModalPopupExtender.Show();
                
            }
            else if (e.CommandName == "Mostrar")
            {
                lblRpExpediente.Text = Expediente;
                GetDetalleExpedienteRecepcionMuestrabyId(Convert.ToInt32(HideIdExpediente.Value));
                btnDetalle_ModalPopupExtender.Show();
            }
            else if (e.CommandName == "Reprogramar")
            {
                //Hide/Show Botones
                ImgActReprog.Visible = false;
                ImgAddReprog.Visible = true;

                txtRepExpediente.Text = Expediente;
                ListReprogramacion(Convert.ToInt32(HideIdExpediente.Value));
                btnReprogramacion_ModalPopupExtender.Show();
            }

        }
        protected void gdvExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataKey key = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(e.Row.RowIndex)];

                //Colorear las filas del gridview con el estado Anulado y deshabilitar las columnas de edicion y evaluacion
                Label lblEstado = (Label)e.Row.FindControl("lblGvEstado");             
                CheckBox chk = (CheckBox)e.Row.FindControl("chkboxSelect");

                if (lblEstado.Text == Convert.ToString(Constante.Estados.Anulado).ToUpper())
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#f1f1f1");                
                    chk.Visible = false;
                }

                //Mostrar fechas nulas en blanco
                Label lblFechaCotizacion = (Label)e.Row.FindControl("lblgvFecCotizacion");
                if (lblFechaCotizacion.Text.Trim() == "01/01/1900")
                {
                    lblFechaCotizacion.Text = string.Empty;
                }

                Label lblFechaIngreso = (Label)e.Row.FindControl("lblgvFecIngreso");
                if (lblFechaIngreso.Text.Trim() == "01/01/1900")
                {
                    lblFechaIngreso.Text = string.Empty;
                }

                Label lblFechaIngLab = (Label)e.Row.FindControl("lblgvFecIngLab");
                if (lblFechaIngLab.Text.Trim() == "01/01/1900")
                {
                    lblFechaIngLab.Text = string.Empty;
                }

                Label lblFechaEntCliente = (Label)e.Row.FindControl("lblgvFecEntCliente");
                if (lblFechaEntCliente.Text.Trim() == "01/01/1900")
                {
                    lblFechaEntCliente.Text = string.Empty;
                }

                Label lblFechaRecepcion = (Label)e.Row.FindControl("lblgvFecRecepcion");
                if (lblFechaRecepcion.Text.Trim() == "01/01/1900")
                {
                    lblFechaRecepcion.Text = string.Empty;
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
                gdvExpediente.Columns[14].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[15].HeaderStyle.BackColor = System.Drawing.Color.Orange;

                gdvExpediente.Columns[16].HeaderStyle.BackColor = System.Drawing.Color.Green;
                gdvExpediente.Columns[17].HeaderStyle.BackColor = System.Drawing.Color.Green;

                gdvExpediente.Columns[18].HeaderStyle.BackColor = System.Drawing.Color.Brown;
                gdvExpediente.Columns[19].HeaderStyle.BackColor = System.Drawing.Color.Brown;

            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //LimpiarNuevo();
            HideAccion.Value = Convert.ToString(Constante.TipoGrabado.NUEVO);
            btnCotizacion_ModalPopupExtender.Show();

        }
        protected void btnGrabarCoti_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(HideAccion.Value) == Convert.ToString(Constante.TipoGrabado.NUEVO)) // Grabar
            {
                SaveCotizacion();
            }          

            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void btnCerrarCoti_Click(object sender, EventArgs e)
        {
            btnCotizacion_ModalPopupExtender.Hide();
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void btnGrabarRM_Click(object sender, EventArgs e)
        {
            UpdateCotizacion(Convert.ToInt32(HideIdExpediente.Value));
            SaveCustodia(Convert.ToInt32(HideIdExpediente.Value));
            SaveContraMuestra(Convert.ToInt32(HideIdExpediente.Value));
            SaveFacturacion(Convert.ToInt32(HideIdExpediente.Value));
            
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void ddlRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            gdvExpediente.PageSize = Int32.Parse(ddlRows.SelectedValue);
            PAGE_SIZE = Int32.Parse(ddlRows.SelectedValue);
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void btnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelPlantilla(Response);
        }

        #endregion

        #region "DETALLE RECEPCION MUESTRA"


        #endregion

        #region "REPROGRAMACION"

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


        #endregion

        #region "METODOS Y FUNCIONES "

        #region "METODOS DE PAGINA"
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
        private void ListConclusion()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Siete);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlTFConclusion.DataSource = lstConfiguracion;
                ddlTFConclusion.DataValueField = "Key";
                ddlTFConclusion.DataTextField = "Value";
                ddlTFConclusion.DataBind();
                ddlTFConclusion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));               

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListArea()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Cuatro);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlTCMCArea.DataSource = lstConfiguracion;
                ddlTCMCArea.DataValueField = "Key";
                ddlTCMCArea.DataTextField = "Value";
                ddlTCMCArea.DataBind();
                ddlTCMCArea.Items.Insert(0, new ListItem("-- Seleccione --", "0"));


                ddlTCMCArea2.DataSource = lstConfiguracion;
                ddlTCMCArea2.DataValueField = "Key";
                ddlTCMCArea2.DataTextField = "Value";
                ddlTCMCArea2.DataBind();
                ddlTCMCArea2.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListClaseProducto()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Cinco);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlTCClasProd.DataSource = lstConfiguracion;
                ddlTCClasProd.DataValueField = "Key";
                ddlTCClasProd.DataTextField = "Value";
                ddlTCClasProd.DataBind();
                ddlTCClasProd.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void ListTipoProducto()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Seis);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlTCTipoProd.DataSource = lstConfiguracion;
                ddlTCTipoProd.DataValueField = "Key";
                ddlTCTipoProd.DataTextField = "Value";
                ddlTCTipoProd.DataBind();
                ddlTCTipoProd.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
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

                ddlTipoCliente.DataSource = dt;
                ddlTipoCliente.DataValueField = "Key";
                ddlTipoCliente.DataTextField = "Value";
                ddlTipoCliente.DataBind();
                ddlTipoCliente.Items.Insert(0, new ListItem("-- Seleccione --", "0"));                

                ddlFilTipoCliente.DataSource = lstConfiguracion;
                ddlFilTipoCliente.DataValueField = "Key";
                ddlFilTipoCliente.DataTextField = "Value";
                ddlFilTipoCliente.DataBind();
                ddlFilTipoCliente.Items.Insert(0, new ListItem("-- TODOS --", "0"));

                ddlCotTipCliente.DataSource = lstConfiguracion;
                ddlCotTipCliente.DataValueField = "Key";
                ddlCotTipCliente.DataTextField = "Value";
                ddlCotTipCliente.DataBind();
                ddlCotTipCliente.Items.Insert(0, new ListItem("-- seleccione --", "0"));                

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
        private void ListarMuestra()
        {
            DataTable dtProcede = new DataTable();
            dtProcede.Columns.AddRange(new DataColumn[2] {new DataColumn("IdMuestra", typeof(Int32)),
                                                                  new DataColumn("Nombre", typeof(string))});
            dtProcede.Rows.Add(1, "SI");
            dtProcede.Rows.Add(2, "NO");

            ddlTFMuestra.DataSource = dtProcede;
            ddlTFMuestra.DataValueField = "IdMuestra";
            ddlTFMuestra.DataTextField = "Nombre";
            ddlTFMuestra.DataBind();
            ddlTFMuestra.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

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

                if (txtFilFecIngreso.Text == string.Empty)
                {
                    objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                }
                else
                { objeto.FechaIngreso = Convert.ToDateTime(txtFilFecIngreso.Text); }               

                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(Constante.Numeros.Cero);

                var lstExpediente = new ExpedienteBLL().ListExpedienteRM(objeto);
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
        private int GetTotalPages(double totalRows)
        {
            int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);

            return totalPages;
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
        private void GetExpedientebyId(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;

                var lstExpediente = new ExpedienteBLL().GetExpediente(objeto);

                foreach (ExpedienteBE oExpediente in lstExpediente)
                {
                    ListTipoCliente();
                    /*
                    ddlTipoCliente.SelectedValue = Convert.ToString(oExpediente.IdTipoCliente);
                    txtCotizacion.Text = oExpediente.CodigoCotizacion;
                    txtExpediente.Text = oExpediente.CodigoExpediente;
                    txtFecCotizacion.Text = string.Format("{0:d}", oExpediente.FechaCotizacion);
                    txtCliente.Text = oExpediente.Cliente;
                    txtProducto.Text = oExpediente.Producto;
                    txtLote.Text = oExpediente.Lote;
                    txtFecVencimiento.Text = string.Format("{0:d}", oExpediente.FechaVencimiento);
                    txtCorreo.Text = oExpediente.Correo;
                    txtDocFisico.Text = oExpediente.DocumentoFisico;
                    txtDocAnexo.Text = oExpediente.DocumentoAnexo;
                    */
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void GetExpedienteRecepcionMuestrabyId(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                var lstExpediente = new ExpedienteBLL().GetExpedienteRecepcionMuestraId(objeto);

                if (lstExpediente.Count > 0)
                {

                    foreach (ExpedienteBE oExpediente in lstExpediente)
                    {
                        //Datos de Cotizacion
                        ddlCotTipCliente.SelectedIndex = Convert.ToInt32(oExpediente.IdTipoCliente);
                        txtCotOficio.Text = Convert.ToString(oExpediente.Oficio);
                        txtCotFecOficio.Text = string.Format("{0:d}", oExpediente.FechaOficio);                        
                        txtCotFecRecep.Text = string.Format("{0:d}", oExpediente.FechaRecepcion);                        
                        txtCotExpediente.Text = Convert.ToString(oExpediente.CodigoExpediente);

                        //Datos de Custodia
                        txtTCFec.Text = string.Format("{0:d}", oExpediente.FechaIngreso);
                        txtTCDoc.Text = Convert.ToString(oExpediente.DocumentoCustodia);
                        ListClaseProducto();
                        ddlTCClasProd.SelectedValue = Convert.ToString(oExpediente.IdClaseProducto);
                        ListTipoProducto();
                        ddlTCTipoProd.SelectedValue = Convert.ToString(oExpediente.IdTipoProducto);
                        txtTCCant.Text = Convert.ToString(oExpediente.CantidadCustodia);
                        txtTCCond.Text = Convert.ToString(oExpediente.CondicionAmbiental);
                        txtTCContramuestra.Text = Convert.ToString(oExpediente.ContraMuestra);
                        txtTCCamFria.Text = Convert.ToString(oExpediente.CamaraFria);
                        txtTCPreCamara.Text = Convert.ToString(oExpediente.PreCamara);

                        //Datos de Contramuestra
                        txtTCMFecLab.Text = string.Format("{0:d}", oExpediente.FechaIngresoLab);
                        txtTCMFecEnt.Text = string.Format("{0:d}", oExpediente.FechaEntregaCliente);
                        txtTCMCER.Text = Convert.ToString(oExpediente.ContramuestraCER);
                        txtTCMFQ.Text = Convert.ToString(oExpediente.ContramuestraFQ);
                        txtTCMMic.Text = Convert.ToString(oExpediente.ContramuestraMicrob);
                        txtTCMOficio.Text = Convert.ToString(oExpediente.MuestraOficio);
                        txtTCMCCantidad.Text = Convert.ToString(oExpediente.MuestraCantidad);

                        ListArea();
                        ddlTCMCArea.SelectedValue = Convert.ToString(oExpediente.IdAreaRetiro1);
                        ddlTCMCArea2.SelectedValue = Convert.ToString(oExpediente.IdAreaRetiro2);
                        txtTCMCCantidad1.Text = Convert.ToString(oExpediente.CantidadRetiro1);
                        txtTCMCCantidad2.Text = Convert.ToString(oExpediente.CantidadRetiro2);
                        txtTCMCSaldo.Text = Convert.ToString(oExpediente.Saldo);
                        txtTCMCUbicacion.Text = Convert.ToString(oExpediente.MuestraUbicacion);

                        //Datos de Facturacion
                        txtTFFecha.Text = string.Format("{0:d}", oExpediente.FechaRecepcionIE);
                        txtTFInforme.Text = Convert.ToString(oExpediente.InformeEnsayo);
                        ListConclusion();
                        ddlTFConclusion.SelectedValue = Convert.ToString(oExpediente.Conclusion);
                        txtTFProforma.Text = Convert.ToString(oExpediente.Proforma);
                        txtTFFactura.Text = Convert.ToString(oExpediente.Factura);
                        txtTFFecEntrega.Text = string.Format("{0:d}", oExpediente.FechaEntregaPool);
                        ListarMuestra();
                        ddlTFMuestra.SelectedValue = Convert.ToString(oExpediente.Muestras);

                    }
                }
                else
                {
                    LimpiarRecepcionMuestra();
                }
            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }      
        private void LimpiarNuevo()
        {

            ddlTipoCliente.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero); 
            txtOficio.Text = string.Empty;
            txtFecOficio.Text = string.Format("{0:d}", DateTime.Now.ToString());
            txtExpediente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtFecVencimiento.Text = string.Empty;
            txtCorreo.Text = string.Empty;

        }
        private void SaveCotizacion()
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.CodigoExpediente = txtExpediente.Text.Trim();
                objeto.CodigoCotizacion = string.Empty;
                objeto.IdTipoCliente = Convert.ToInt32(ddlTipoCliente.SelectedValue);
                objeto.FechaCotizacion = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                objeto.Correo = string.Empty;
                objeto.DocumentoFisico = string.Empty;
                objeto.DocumentoAnexo = string.Empty;
                objeto.Oficio = txtOficio.Text.Trim();

                if (txtFecOficio.Text != string.Empty)
                { objeto.FechaOficio = Convert.ToDateTime(txtFecOficio.Text); }
                else
                { objeto.FechaOficio = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }
                
                objeto.ActaPesquisa = txtActa.Text;
                objeto.FechaPesquisa = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                objeto.IdUsuario = 1;

                capanegocios.SaveCotizacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void UpdateCotizacion(int pIdExpediente)
        {

            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                objeto.CodigoExpediente = txtCotExpediente.Text.Trim();                
                objeto.IdTipoCliente = Convert.ToInt32(ddlCotTipCliente.SelectedValue);                               
                objeto.Oficio = txtCotOficio.Text.Trim();
                objeto.FechaOficio = Convert.ToDateTime(txtCotFecOficio.Text);                                
                objeto.FechaRecepcion = Convert.ToDateTime(txtCotFecRecep.Text);
                objeto.IdUsuario = 1;

                capanegocios.UpdateCotizacionRM(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }            
        private void SaveCustodia(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;

                if (txtTCFec.Text != string.Empty)
                { objeto.FechaIngreso = Convert.ToDateTime(txtTCFec.Text); }
                else
                { objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }

                
                objeto.DocumentoCustodia = txtTCDoc.Text.Trim();
                objeto.IdTipoProducto = Convert.ToInt32(ddlTCTipoProd.SelectedValue);
                objeto.IdClaseProducto = Convert.ToInt32(ddlTCClasProd.SelectedValue);
                objeto.CantidadCustodia = Convert.ToInt32(txtTCCant.Text.Trim());
                objeto.CondicionAmbiental = txtTCCond.Text.Trim();
                objeto.ContraMuestra = txtTCContramuestra.Text.Trim();
                objeto.CamaraFria = txtTCCamFria.Text.Trim();
                objeto.PreCamara = txtTCPreCamara.Text.Trim();              
                objeto.IdUsuarioActualiza = 1;

                capanegocios.UpdateExpedienteCustodia(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void SaveContraMuestra(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;

                if (txtTCMFecLab.Text != string.Empty)
                { objeto.FechaIngresoLab = Convert.ToDateTime(txtTCMFecLab.Text); }
                else
                { objeto.FechaIngresoLab = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }

                if (txtTCMFecEnt.Text != string.Empty)
                { objeto.FechaEntregaCliente = Convert.ToDateTime(txtTCMFecEnt.Text); }
                else
                { objeto.FechaEntregaCliente = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }

                objeto.ContramuestraCER = txtTCMCER.Text.Trim();
                objeto.ContramuestraFQ = txtTCMFQ.Text.Trim();
                objeto.ContramuestraMicrob = txtTCMMic.Text.Trim();
                objeto.MuestraOficio = txtTCMOficio.Text.Trim();
                objeto.MuestraCantidad = Convert.ToInt32(txtTCMCCantidad.Text);
                objeto.IdAreaRetiro1 = Convert.ToInt32(ddlTCMCArea.SelectedValue);
                objeto.CantidadRetiro1 = Convert.ToInt32(txtTCMCCantidad1.Text);
                objeto.IdAreaRetiro2 = Convert.ToInt32(ddlTCMCArea2.SelectedValue);
                objeto.CantidadRetiro2 = Convert.ToInt32(txtTCMCCantidad2.Text);
                objeto.Saldo = Convert.ToDecimal(txtTCMCSaldo.Text);
                objeto.MuestraUbicacion = txtTCMCUbicacion.Text;
                objeto.IdUsuarioActualiza = 1;
                capanegocios.UpdateExpedienteContraMuestra(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void SaveFacturacion(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;

                if (txtTFFecha.Text != string.Empty)
                { objeto.FechaRecepcionIE = Convert.ToDateTime(txtTFFecha.Text); }
                else
                { objeto.FechaRecepcionIE = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }

                
                objeto.InformeEnsayo = txtTFInforme.Text.Trim();
                objeto.Conclusion = Convert.ToInt32(ddlTFConclusion.SelectedValue);
                objeto.Proforma = txtTFProforma.Text.Trim();
                objeto.Factura = txtTFFactura.Text.Trim();

                if (txtTFFecEntrega.Text != string.Empty)
                { objeto.FechaEntregaPool = Convert.ToDateTime(txtTFFecha.Text); }
                else
                { objeto.FechaEntregaPool = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula()); }
                                
                objeto.Muestras = Convert.ToInt32(ddlTFMuestra.SelectedValue);
                objeto.IdUsuarioActualiza = 1;
                capanegocios.UpdateExpedienteFacturacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void LimpiarRecepcionMuestra()
        {
            //Datos de Custodia
            txtTCFec.Text = string.Empty;
            txtTCDoc.Text = string.Empty;            
            ddlTCClasProd.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);           
            ddlTCTipoProd.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            txtTCCant.Text = string.Empty;
            txtTCCond.Text = string.Empty;
            txtTCContramuestra.Text = string.Empty;
            txtTCCamFria.Text = string.Empty;
            txtTCPreCamara.Text = string.Empty;

            //Datos de Contramuestra
            txtTCMFecLab.Text = string.Empty;
            txtTCMFecEnt.Text = string.Empty;
            txtTCMCER.Text = string.Empty;
            txtTCMFQ.Text = string.Empty;
            txtTCMMic.Text = string.Empty;
            txtTCMOficio.Text = string.Empty;
            txtTCMCCantidad.Text = string.Empty;            
            ddlTCMCArea.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlTCMCArea2.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            txtTCMCCantidad1.Text = string.Empty;
            txtTCMCCantidad2.Text = string.Empty;
            txtTCMCSaldo.Text = string.Empty;
            txtTCMCUbicacion.Text = string.Empty;

            //Datos de Facturacion
            txtTFFecha.Text = string.Empty;
            txtTFInforme.Text = string.Empty;            
            ddlTFConclusion.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            txtTFProforma.Text = string.Empty;
            txtTFFactura.Text = string.Empty;
            txtTFFecEntrega.Text = string.Empty;            
            ddlTFMuestra.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
        }
        private int ObtenerPlazosAlertasInternas()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Padre, Parametro;

            try
            {
                Padre = Convert.ToInt32(Constante.Numeros.Cuatro);
                Parametro = Convert.ToInt32(Constante.Numeros.Dos);

                var lstConfiguracion = new ConfiguracionBLL().GetListValue(Padre, Parametro);

                Dias = Convert.ToInt32(lstConfiguracion.Where(pair => pair.Value == Convert.ToString(Constante.Area.RECEPCION_MUESTRA))
                              .Select(pair => pair.Key)
                              .FirstOrDefault());

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

            return Dias;
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

                if (txtFilFecIngreso.Text == string.Empty)
                {
                    objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                }
                else
                { objeto.FechaIngreso = Convert.ToDateTime(txtFilFecIngreso.Text); }

                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(Constante.Numeros.Cero);

                var lstExpediente = new ExpedienteBLL().ExportExpediente(objeto);

                //crear datatable para enviar al metodo de descarga
                var dataTableDescargar = new System.Data.DataTable(Guid.NewGuid().ToString());
                var Expediente = new DataColumn("Expediente", typeof(string));
                var Cotizacion = new DataColumn("Cotizacion", typeof(string));
                var FechaCotizacion = new DataColumn("FechaCotizacion", typeof(string));
                var Cliente = new DataColumn("Cliente", typeof(string));
                var TipoCliente = new DataColumn("TipoCliente", typeof(string));
                var Producto = new DataColumn("Producto", typeof(string));
                var Lote = new DataColumn("Lote", typeof(string));
                var Correo = new DataColumn("Correo", typeof(string));
                var ActaPesquisa = new DataColumn("ActaPesquisa", typeof(string));                
                var Ubicacion = new DataColumn("Ubicacion", typeof(string));
                var Estado = new DataColumn("Estado", typeof(string));
                var FechaIngreso = new DataColumn("FechaIngreso", typeof(string));
                var DocCustodia = new DataColumn("DocCustodia", typeof(string));
                var FechaIngresoLab = new DataColumn("FechaIngresoLab", typeof(string));                
                var FechaRecepcion = new DataColumn("FechaRecepcion", typeof(string));
                var InformeEnsayo = new DataColumn("InformeEnsayo", typeof(string));

                dataTableDescargar.Columns.AddRange(new DataColumn[] {
                    Expediente, Cotizacion, FechaCotizacion, Cliente, TipoCliente, Producto, Lote, Correo, ActaPesquisa, Ubicacion, Estado, FechaIngreso, DocCustodia, FechaIngresoLab,  FechaRecepcion, InformeEnsayo
                });

                //Llenar data al datatable
                foreach (ExpedienteBE oExpediente in lstExpediente)
                {
                    DataRow RowFile = dataTableDescargar.NewRow();

                    RowFile["Expediente"] = oExpediente.CodigoExpediente;
                    RowFile["Cotizacion"] = oExpediente.CodigoCotizacion;

                    if (oExpediente.FechaCotizacion != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaCotizacion"] = string.Format("{0:d}", oExpediente.FechaCotizacion);
                    }
                    else
                    { RowFile["FechaCotizacion"] = string.Empty; }

                    RowFile["Cliente"] = oExpediente.Cliente;
                    RowFile["TipoCliente"] = oExpediente.TipoCliente;
                    RowFile["Producto"] = oExpediente.Producto;
                    RowFile["Lote"] = oExpediente.Lote;
                    RowFile["Correo"] = oExpediente.Correo;
                    RowFile["ActaPesquisa"] = oExpediente.ActaPesquisa;                    
                    RowFile["Ubicacion"] = oExpediente.Usuario.Login;
                    RowFile["Estado"] = oExpediente.Estado;
                    
                    if (oExpediente.FechaIngreso != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaIngreso"] = string.Format("{0:d}", oExpediente.FechaIngreso);
                    }
                    else
                    { RowFile["FechaIngreso"] = string.Empty; }

                    RowFile["DocCustodia"] = oExpediente.DocumentoCustodia;                

                    if (oExpediente.FechaIngresoLab != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaIngresoLab"] = string.Format("{0:d}", oExpediente.FechaIngresoLab);
                    }
                    else
                    { RowFile["FechaIngresoLab"] = string.Empty; }

                    if (oExpediente.FechaRecepcion != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaRecepcion"] = string.Format("{0:d}", oExpediente.FechaRecepcion);
                    }
                    else
                    { RowFile["FechaRecepcion"] = string.Empty; }

                    RowFile["InformeEnsayo"] = oExpediente.InformeEnsayo;

                    dataTableDescargar.Rows.Add(RowFile);
                }

                ExportarUtils ObjExporExcel = new ExportarUtils();

                object[] pNombreCabeceras = new object[] { "Expediente", "Cotizacion", "Fecha Cotizacion", "Cliente", "Tipo Cliente", "Producto", "Lote", "Correo", "Acta Pesquisa", "Ubicacion", "Estado", "Fecha Ingreso", "Doc Custodia", "Fecha Ingreso Lab", "Fecha Recepcion", "Informe Ensayo" };
                object[] pEstilos = new object[] { new object[] { 3, OleDbType.Date }, new object[] { 12, OleDbType.Date }, new object[] { 14, OleDbType.Date }, new object[] { 15, OleDbType.Date } };
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

        #region "METODO DETALLE"
        private void GetDetalleExpedienteRecepcionMuestrabyId(int pIdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                var lstExpediente = new ExpedienteBLL().GetExpedienteRecepcionMuestraId(objeto);

                if (lstExpediente.Count > 0)
                {

                    foreach (ExpedienteBE oExpediente in lstExpediente)
                    {

                        //Datos de Custodia                        
                        lblTitRpFecIngreso.Text = string.Format("{0:d}", oExpediente.FechaIngreso);
                        lblTitRpDocumento.Text = Convert.ToString(oExpediente.DocumentoCustodia);
                        lblTitRpTipProducto.Text = Convert.ToString(oExpediente.TipoProducto);
                        lblTitRpClaProducto.Text = Convert.ToString(oExpediente.ClaseProducto);
                        lblTitRpCantidad.Text = Convert.ToString(oExpediente.CantidadCustodia);
                        lblTitRpCondicion.Text = Convert.ToString(oExpediente.CondicionAmbiental);
                        lblTitRpContramuestra.Text = Convert.ToString(oExpediente.ContraMuestra);
                        lblTitRpCamara.Text = Convert.ToString(oExpediente.CamaraFria);
                        lblTitRpPreCamara.Text = Convert.ToString(oExpediente.PreCamara);

                        //Datos de Contramuestra
                        lblTitRpFecIngLab.Text = string.Format("{0:d}", oExpediente.FechaIngresoLab);
                        lblTitRpFecEntrega.Text = string.Format("{0:d}", oExpediente.FechaEntregaCliente);
                        lblTitRpCER.Text = Convert.ToString(oExpediente.ContramuestraCER);
                        lblTitRpFQ.Text = Convert.ToString(oExpediente.ContramuestraFQ);
                        lblTitRpMicro.Text = Convert.ToString(oExpediente.ContramuestraMicrob);
                        lblTitRpOficio.Text = Convert.ToString(oExpediente.MuestraOficio);
                        lblTitRpCantidadRet.Text = Convert.ToString(oExpediente.MuestraCantidad);
                        lblTitRpArea.Text = Convert.ToString(oExpediente.AreaRetiro1);
                        lblTitRpCantidadRet1.Text = Convert.ToString(oExpediente.CantidadRetiro1);
                        lblTitRpArea2.Text = Convert.ToString(oExpediente.AreaRetiro2);
                        txtTCMCCantidad2.Text = Convert.ToString(oExpediente.CantidadRetiro2);
                        lblTitRpSaldo.Text = Convert.ToString(oExpediente.Saldo);
                        lblTitRpUbic.Text = Convert.ToString(oExpediente.MuestraUbicacion);

                        //Datos de Facturacion
                        lblTitRpFecRec.Text = string.Format("{0:d}", oExpediente.FechaRecepcionIE);
                        lblTitRpInfEns.Text = Convert.ToString(oExpediente.InformeEnsayo);
                        lblTitRpConclusion.Text = Convert.ToString(oExpediente.NombreConclusion);
                        lblTitRpProforma.Text = Convert.ToString(oExpediente.Proforma);
                        lblTitRpFactura.Text = Convert.ToString(oExpediente.Factura);
                        lblTitRpFecEntPool.Text = string.Format("{0:d}", oExpediente.FechaEntregaPool);
                        lblTitRpMuestras.Text = Convert.ToString(oExpediente.NombreMuestras);

                    }
                }
                else
                {
                    LimpiarRecepcionMuestra();
                }
            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }

        #endregion

        #region "METODO REPROGRAMACION"

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

            
                    foreach(ReprogramacionBE oReprogramacion in lstReprogramacion)
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