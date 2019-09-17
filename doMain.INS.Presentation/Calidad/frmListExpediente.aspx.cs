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
    public partial class frmListCotizacion : System.Web.UI.Page
    {
        protected int Dias;
        protected int IdArea, IdExpediente, IdUsuario, IdRol;
        private string errores = string.Empty;
        protected int inStartRowIndex, inEndRowIndex;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE;
        public DataKey keyExpediente;
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

                //Asignar Fecha del dia
                txtFecCotizacion.Text =  string.Format("{0:d}", DateTime.Now.ToShortDateString());

                //Mostrar plazo de dias                
                lblPlazo.Text = "Plazo atención interno: " + Convert.ToString(ObtenerPlazosAlertasInternas())  +  " días.";

                //Ocultar por default panel registro Cotizacion
                btnCotizacion_ModalPopupExtender.Hide();
                btnCotAnulacion_ModalPopupExtender.Hide();
                btnRegEvaluacion_ModalPopupExtender.Hide();

                //Lista los expedientes gridview y Listas Maestras
                ListTipoCliente();
                ListEnsayo();
                ListEvaluador();
                ListProcede();
                ListSituacion();
                ListEstado();
                ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value),false);
                //Ocultar objetos
                OcultarPaneles(false, Convert.ToInt32(Constante.Numeros.Uno));                
            }

        }

        #region "METODOS Y EVENTOS DE LA PAGINA"

        #region "EVENTOS"
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarNuevo();
            HideAccion.Value = Convert.ToString(Constante.TipoGrabado.NUEVO);
            btnCotizacion_ModalPopupExtender.Show();

        }
        protected void btnGrabarCoti_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(HideAccion.Value) == Convert.ToString(Constante.TipoGrabado.NUEVO)) // Grabar
            {
                SaveCotizacion();
            }
            else //Actualizacion
            {
                UpdateCotizacion(Convert.ToInt32(HideIdExpediente.Value));
            }

            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void gdvExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataKey key = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(e.Row.RowIndex)];

                //Colorear las filas del gridview con el estado Anulado y deshabilitar las columnas de edicion y evaluacion
                Label lblEstado = (Label)e.Row.FindControl("lblGvEstado");
                LinkButton btnGvEdicion = (LinkButton)e.Row.FindControl("btnActualizar");
                LinkButton btnGvEvaluacion = (LinkButton)e.Row.FindControl("btnGrvEvaluar");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkboxSelect");

                if (lblEstado.Text == Convert.ToString(Constante.Estados.Anulado).ToUpper())
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#f1f1f1");
                    btnGvEdicion.Visible = false;
                    btnGvEvaluacion.Visible = false;
                    chk.Visible = false;
                }

                //Pintar Alertas
                Label lblAlerta = (Label)e.Row.FindControl("lblAlerta");
                Image ImgAlertaInterno = (Image)e.Row.FindControl("ImgAlertaInt");

                if (Convert.ToInt32(lblAlerta.Text) >= Convert.ToInt32(Constante.Numeros.Cero) && Convert.ToInt32(lblAlerta.Text) <= Convert.ToInt32(Constante.Numeros.Dos))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_green.png";
                }
                else if (Convert.ToInt32(lblAlerta.Text) == Convert.ToInt32(Constante.Numeros.Tres))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_yellow.png";
                }
                else if (Convert.ToInt32(lblAlerta.Text) > Convert.ToInt32(Constante.Numeros.Tres))
                {
                    ImgAlertaInterno.ImageUrl = "~/Images/circle_red.png";
                }

                //Colorea las columnas de la cabeceras del gridview
                gdvExpediente.Columns[10].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[11].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[12].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[13].HeaderStyle.BackColor = System.Drawing.Color.Orange;
                gdvExpediente.Columns[14].HeaderStyle.BackColor = System.Drawing.Color.Orange;

                gdvExpediente.Columns[15].HeaderStyle.BackColor = System.Drawing.Color.Green;
                gdvExpediente.Columns[16].HeaderStyle.BackColor = System.Drawing.Color.Green;
                gdvExpediente.Columns[17].HeaderStyle.BackColor = System.Drawing.Color.Green;
                gdvExpediente.Columns[18].HeaderStyle.BackColor = System.Drawing.Color.Green;

            }
        }
        protected void gdvExpediente_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyExpediente = (DataKey)gdvExpediente.DataKeys[Convert.ToInt32(index)];

            HideIdExpediente.Value = Convert.ToString(keyExpediente["IDEXPEDIENTE"]);



            if (e.CommandName == "Actualizar")
            {
                HideAccion.Value = Convert.ToString(Constante.TipoGrabado.ACTUALIZA);
                GetExpedientebyId(Convert.ToInt32(HideIdExpediente.Value));
                btnCotizacion_ModalPopupExtender.Show();
            }
            else if (e.CommandName == "Anular")
            {
                LimpiarAnulacion();
                btnCotAnulacion_ModalPopupExtender.Show();
            }
            else if (e.CommandName == "Evaluar")
            {
                txtCotEval.Text = Convert.ToString(keyExpediente["CodigoCotizacion"]);
                ListEvaluacion(Convert.ToInt32(HideIdExpediente.Value));
                btnRegEvaluacion_ModalPopupExtender.Show();
            }

        }
        protected void ddlProcede_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ddlProcede.SelectedValue) == Convert.ToInt32(Constante.Numeros.Uno))
            {
                OcultarPaneles(true, Convert.ToInt32(Constante.Numeros.Dos));
            }
            else
            {
                OcultarPaneles(false, Convert.ToInt32(Constante.Numeros.Dos));
            }

            btnRegEvaluacion_ModalPopupExtender.Show();
        }             
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (fVerificaChecks() == true)
            {
                EnviarExpediente();
                ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
            }
            else
            {
                ShowMessage("Debe seleccionar por lo menos un registro de expediente.", MessageType.Info);
            }

        }

        protected void btnExportarExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportarExcelPlantilla(Response);
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

        #endregion
        #region "METODOS"
        private void MostrarMsjeConfirmacion(string ID)
        {


            string Mensaje = "ShowConfirm(" + ID + ");";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MsjeConfirmacion", Mensaje, true);
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private Boolean fVerificaChecks()
        {
            CheckBox chkbox;

            for (int I = 0; I <= gdvExpediente.Rows.Count - 1; I++)
            {
                chkbox = (CheckBox)gdvExpediente.Rows[I].Cells[2].FindControl("chkboxSelect");
                if (chkbox.Checked == true)
                {
                    return true;
                }
            }
            return false;
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

                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(ddlFilProcede.SelectedValue);

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
                var DocumentoFisico = new DataColumn("DocumentoFisico", typeof(string));
                var DocumentoAnexo = new DataColumn("DocumentoAnexo", typeof(string));
                var Ubicacion = new DataColumn("Ubicacion", typeof(string));
                var Estado = new DataColumn("Estado", typeof(string));
                var Ensayo = new DataColumn("Ensayo", typeof(string));
                var Analista = new DataColumn("Analista", typeof(string));
                var Procede = new DataColumn("Procede", typeof(string));
                var Situacion = new DataColumn("Situacion", typeof(string));
                var MotivoAnulacion = new DataColumn("MotivoAnulacion", typeof(string));

                dataTableDescargar.Columns.AddRange(new DataColumn[] {
                    Expediente, Cotizacion, FechaCotizacion, Cliente, TipoCliente, Producto, Lote, Correo, DocumentoFisico, DocumentoAnexo, Ubicacion, Estado, Ensayo, Analista, Procede, Situacion, MotivoAnulacion
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
                    RowFile["DocumentoFisico"] = oExpediente.DocumentoFisico;
                    RowFile["DocumentoAnexo"] = oExpediente.DocumentoAnexo;
                    RowFile["Ubicacion"] = oExpediente.Usuario.Login;
                    RowFile["Estado"] = oExpediente.Estado;
                    RowFile["Ensayo"] = oExpediente.Ensayo.Nombre;
                    RowFile["Analista"] = oExpediente.Evaluador.Nombre;
                    RowFile["Procede"] = oExpediente.Evaluacion.Procede;
                    RowFile["Situacion"] = oExpediente.Situacion;
                    RowFile["MotivoAnulacion"] = oExpediente.MotivoAnulacion;

                    dataTableDescargar.Rows.Add(RowFile);
                }

                ExportarUtils ObjExporExcel = new ExportarUtils();

                object[] pNombreCabeceras = new object[] { "Expediente", "Cotizacion", "Fecha Cotizacion", "Cliente", "Tipo Cliente", "Producto", "Lote", "Correo", "Documento Fisico", "Documento Anexo", "Ubicacion", "Estado", "Ensayo", "Analista", "Procede", "Situacion", "Motivo Anulacion" };
                object[] pEstilos = new object[] { new object[] { 3, OleDbType.Date } };
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
        private void EnviarExpediente()
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                ArrayList ArrayList = new ArrayList();
                CheckBox ChkBoxHeader = (CheckBox)gdvExpediente.HeaderRow.FindControl("chkboxSelectAll");

                foreach (GridViewRow row in gdvExpediente.Rows)
                {

                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxSelect");

                    if (ChkBoxRows.Checked == true)
                    {

                        objeto.IdEstado = Convert.ToInt32(Constante.Estados.Enviado);

                        String IdExpediente = Convert.ToString(gdvExpediente.DataKeys[row.RowIndex]["IDEXPEDIENTE"]);

                        objeto.IdExpediente = Convert.ToInt32(IdExpediente);
                        objeto.IdUsuarioActualiza = 1;
                        capanegocios.UpdateEstadoExpediente(objeto);

                    }

                }

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

                objeto.IdEstado = Convert.ToInt32(ddlFilEstado.SelectedValue);
                objeto.IdEnsayo = Convert.ToInt32(ddlFilEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlFilAnalista.SelectedValue);
                objeto.IdProcedencia = Convert.ToInt32(ddlFilProcede.SelectedValue);
                objeto.FechaIngreso = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());

                var lstExpediente = new ExpedienteBLL().ListExpediente(objeto);
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
        private int ObtenerPlazosAlertasInternas()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Padre, Parametro;

            try
            {
                Padre = Convert.ToInt32(Constante.Numeros.Cuatro);
                Parametro = Convert.ToInt32(Constante.Numeros.Uno);

                var lstConfiguracion = new ConfiguracionBLL().GetListValue(Padre, Parametro);

                Dias = Convert.ToInt32(lstConfiguracion.Where(pair => pair.Value == Convert.ToString(Constante.Area.POOL_SECRETARIAL))
                              .Select(pair => pair.Key)
                              .FirstOrDefault());

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

            return Dias;
        }
        private void ListTipoCliente()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Uno);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlTipoCliente.DataSource = lstConfiguracion;
                ddlTipoCliente.DataValueField = "Key";
                ddlTipoCliente.DataTextField = "Value";
                ddlTipoCliente.DataBind();
                ddlTipoCliente.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);

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
        private void ListEnsayo()
        {
            EnsayoBLL capanegocios = new EnsayoBLL();

            try
            {

                var lstEnsayo = new EnsayoBLL().List();

                ddlEnsayo.DataSource = lstEnsayo;
                ddlEnsayo.DataValueField = "IdEnsayo";
                ddlEnsayo.DataTextField = "Nombre";
                ddlEnsayo.DataBind();
                ddlEnsayo.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ddlFilEnsayo.DataSource = lstEnsayo;
                ddlFilEnsayo.DataValueField = "IdEnsayo";
                ddlFilEnsayo.DataTextField = "Nombre";
                ddlFilEnsayo.DataBind();
                ddlFilEnsayo.Items.Insert(0, new ListItem("-- TODOS --", "0"));

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

                ddlEvaluador.DataSource = lstEvaluador;
                ddlEvaluador.DataValueField = "IdEvaluador";
                ddlEvaluador.DataTextField = "Evaluador";
                ddlEvaluador.DataBind();
                ddlEvaluador.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ddlFilAnalista.DataSource = lstEvaluador;
                ddlFilAnalista.DataValueField = "IdEvaluador";
                ddlFilAnalista.DataTextField = "Evaluador";
                ddlFilAnalista.DataBind();
                ddlFilAnalista.Items.Insert(0, new ListItem("-- TODOS --", "0"));

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void ListProcede()
        {
            DataTable dtProcede = new DataTable();
            dtProcede.Columns.AddRange(new DataColumn[2] {new DataColumn("IdProcede", typeof(Int32)),
                                                                  new DataColumn("Nombre", typeof(string))});
            dtProcede.Rows.Add(1, "SI");
            dtProcede.Rows.Add(2, "NO");

            ddlProcede.DataSource = dtProcede;
            ddlProcede.DataValueField = "IdProcede";
            ddlProcede.DataTextField = "Nombre";
            ddlProcede.DataBind();
            ddlProcede.Items.Insert(0, new ListItem("--Seleccione--", "0"));

            ddlFilProcede.DataSource = dtProcede;
            ddlFilProcede.DataValueField = "IdProcede";
            ddlFilProcede.DataTextField = "Nombre";
            ddlFilProcede.DataBind();
            ddlFilProcede.Items.Insert(0, new ListItem("-- TODOS --", "0"));


        }
        private void ListSituacion()
        {
            ConfiguracionBLL capanegocios = new ConfiguracionBLL();
            int Parametro;

            try
            {
                Parametro = Convert.ToInt32(Constante.Numeros.Dos);
                var lstConfiguracion = new ConfiguracionBLL().GetValue(Parametro);

                ddlSituacion.DataSource = lstConfiguracion;
                ddlSituacion.DataValueField = "Key";
                ddlSituacion.DataTextField = "Value";
                ddlSituacion.DataBind();
                ddlSituacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

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
        private void OcultarPaneles(Boolean pEstado, int Condicion)
        {
            switch (Condicion)
            {
                case 1:
                    lblSituacion.Visible = pEstado;
                    lblObserSituacion.Visible = pEstado;
                    ddlSituacion.Visible = pEstado;
                    txtObserSituacion.Visible = pEstado;

                    lblMotivos.Visible = pEstado;
                    chkReactivo.Visible = pEstado;
                    txtReactivo.Visible = pEstado;
                    chkEquipo.Visible = pEstado;
                    txtEquipo.Visible = pEstado;
                    chkInstalacion.Visible = pEstado;
                    txtInstalacion.Visible = pEstado;
                    chkInsumo.Visible = pEstado;
                    txtInsumo.Visible = pEstado;
                    break;

                case 2:

                    lblSituacion.Visible = pEstado;
                    lblObserSituacion.Visible = pEstado;
                    ddlSituacion.Visible = pEstado;
                    txtObserSituacion.Visible = pEstado;

                    lblMotivos.Visible = !pEstado;
                    chkReactivo.Visible = !pEstado;
                    txtReactivo.Visible = !pEstado;
                    chkEquipo.Visible = !pEstado;
                    txtEquipo.Visible = !pEstado;
                    chkInstalacion.Visible = !pEstado;
                    txtInstalacion.Visible = !pEstado;
                    chkInsumo.Visible = !pEstado;
                    txtInsumo.Visible = !pEstado;
                    break;
            }


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
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        #endregion

        #endregion

        #region "METODOS Y EVENTOS OPCION REGISTRO COTIZACION"

        #region "EVENTOS"
        protected void btnCerrarCoti_Click(object sender, EventArgs e)
        {
            btnCotizacion_ModalPopupExtender.Hide();
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }


        #endregion

        #region "METODOS"
        private void LimpiarNuevo()
        {

            ddlTipoCliente.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero); ;
            txtCotizacion.Text = string.Empty;
            txtFecCotizacion.Text = string.Format("{0:d}", DateTime.Now.ToString());
            txtExpediente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtFecVencimiento.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtDocFisico.Text = string.Empty;
            txtDocAnexo.Text = string.Empty;

        }
        private void SaveCotizacion()
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.CodigoExpediente = txtExpediente.Text.Trim();
                objeto.CodigoCotizacion = txtCotizacion.Text.Trim();
                objeto.IdTipoCliente = Convert.ToInt32(ddlTipoCliente.SelectedValue);
                objeto.FechaCotizacion = Convert.ToDateTime(txtFecCotizacion.Text.Trim());
                objeto.Correo = txtCorreo.Text.Trim();
                objeto.DocumentoFisico = txtDocFisico.Text.Trim();
                objeto.DocumentoAnexo = txtDocAnexo.Text.Trim();
                objeto.Oficio = string.Empty;
                objeto.FechaOficio = Convert.ToDateTime(doMain.Utils.FechaNulaUtils.FechaNula());
                objeto.ActaPesquisa = string.Empty;
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
                objeto.CodigoExpediente = txtExpediente.Text.Trim();
                objeto.CodigoCotizacion = txtCotizacion.Text.Trim();
                objeto.IdTipoCliente = Convert.ToInt32(ddlTipoCliente.SelectedValue);
                objeto.FechaCotizacion = Convert.ToDateTime(txtFecCotizacion.Text.Trim());
                objeto.Correo = txtCorreo.Text.Trim();
                objeto.DocumentoFisico = txtDocFisico.Text.Trim();
                objeto.DocumentoAnexo = txtDocAnexo.Text.Trim();
                objeto.IdUsuarioActualiza = 1;

                capanegocios.UpdateCotizacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        

        #endregion

        #endregion

        #region "METODOS Y EVENTOS OPCION ANULACION"

        #region "EVENTOS"
        protected void btnGrabarAnulacion_Click(object sender, EventArgs e)
        {
            AnularCotizacion(Convert.ToInt32(HideIdExpediente.Value));

            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }

        protected void btnCerrarAnulacion_Click(object sender, EventArgs e)
        {
            btnCotAnulacion_ModalPopupExtender.Hide();
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }

        #endregion

        #region "METODOS"
        private void AnularCotizacion(int IdExpediente)
        {
            ExpedienteBLL capanegocios = new ExpedienteBLL();
            ExpedienteBE objeto = new ExpedienteBE();

            try
            {
                objeto.IdExpediente = IdExpediente;
                objeto.MotivoAnulacion = txtanulacion.Text.Trim();

                capanegocios.UpdateAnulacionCotizacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void LimpiarAnulacion()
        {
            txtanulacion.Text = string.Empty;
        }
        #endregion

        #endregion

        #region "METODOS Y EVENTOS OPCION EVALUACION"

        #region "EVENTOS"
        protected void btnGrabarEvaluacion_Click(object sender, EventArgs e)
        {
            SaveEvaluacion(Convert.ToInt32(HideIdExpediente.Value));
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }

        protected void btnCerrarEvaluacion_Click(object sender, EventArgs e)
        {
            btnRegEvaluacion_ModalPopupExtender.Hide();
            ListbyCriteriosExpediente(Convert.ToInt32(HideArea.Value), false);
        }

        #endregion

        #region "METODOS"
        private void SaveEvaluacion(int pIdExpediente)
        {
            EvaluacionBLL capanegocios = new EvaluacionBLL();
            EvaluacionBE objeto = new EvaluacionBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;
                objeto.IdEnsayo = Convert.ToInt32(ddlEnsayo.SelectedValue);
                objeto.IdAnalista = Convert.ToInt32(ddlEvaluador.SelectedValue);
                objeto.IdProcede = Convert.ToInt32(ddlProcede.SelectedValue);
                objeto.IdSituacion = Convert.ToInt32(ddlSituacion.SelectedValue);
                objeto.ObservacionSituacion = txtObserSituacion.Text.Trim();
                capanegocios.SaveEvaluacion(objeto);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void ListEvaluacion(int pIdExpediente)
        {
            EvaluacionBLL capanegocios = new EvaluacionBLL();
            EvaluacionBE objeto = new EvaluacionBE();

            try
            {
                objeto.IdExpediente = pIdExpediente;

                var lstEvaluacion = new EvaluacionBLL().GetEvaluacionbyIdExpediente(objeto);

                if (lstEvaluacion != null)
                {

                    foreach (EvaluacionBE oEvaluacion in lstEvaluacion)
                    {
                        ListEnsayo();
                        ddlEnsayo.SelectedValue = Convert.ToString(oEvaluacion.IdEnsayo);

                        ListEvaluador();
                        ddlEvaluador.SelectedValue = Convert.ToString(oEvaluacion.IdAnalista);

                        ListProcede();
                        ddlProcede.SelectedValue = Convert.ToString(oEvaluacion.Procede);

                        if (oEvaluacion.IdProcede == Convert.ToInt32(Constante.Numeros.Uno)) //Si Procede
                        {
                            OcultarPaneles(true, Convert.ToInt32(Constante.Numeros.Dos));

                            ListSituacion();
                            ddlSituacion.SelectedValue = Convert.ToString(oEvaluacion.IdSituacion);
                            txtObserSituacion.Text = oEvaluacion.ObservacionSituacion;
                        }
                        else //No Procede
                        {
                            OcultarPaneles(false, Convert.ToInt32(Constante.Numeros.Dos));
                        }

                    }
                }
                else
                {
                    LimpiarEvaluacion();
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }

        }
        private void LimpiarEvaluacion()
        {

            ddlEnsayo.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlEvaluador.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlProcede.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlSituacion.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            txtObserSituacion.Text = string.Empty;
            chkReactivo.Checked = false;
            chkEquipo.Checked = false;
            chkInstalacion.Checked = false;
            chkInsumo.Checked = false;
            txtReactivo.Text = string.Empty;
            txtEquipo.Text = string.Empty;
            txtInstalacion.Text = string.Empty;
            txtInsumo.Text = string.Empty;
            OcultarPaneles(false, Convert.ToInt32(Constante.Numeros.Uno));
        }
        #endregion

        #endregion
               


    }
}