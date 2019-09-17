using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using doMain.Utils;
using doMain.INS.Entity;
using doMain.INS.Business;
using System.Data.OleDb;



namespace doMain.INS.Presentation.Seguridad
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        public DataKey keyUsuario;
        public static string errores;
        protected static int PAGE_SIZE;
        protected static int currentPageNumber;
        protected int inStartRowIndex, inEndRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                HideUsuario.Value = "1";
                //Paginacion Gridview
                currentPageNumber = 1;
                PAGE_SIZE = 10;

                //Lista los expedientes gridview y Listas Maestras //
                ListRol();
                ListArea();
                ListarUsuario(false);               

            }

        }

        #region "METODOS Y EVENTOS DE PAGINA"

        #region "METODOS"
        private void ListRol()
        {

            EnsayoBLL capanegocios = new EnsayoBLL();

            try
            {

                var lstRol = new RolBLL().List();
                ddlFRol.DataSource = lstRol;
                ddlFRol.DataValueField = "IdRol";
                ddlFRol.DataTextField = "Nombre";
                ddlFRol.DataBind();
                ddlFRol.Items.Insert(0, new ListItem("-- TODOS --", "0"));

                ddlRol.DataSource = lstRol;
                ddlRol.DataValueField = "IdRol";
                ddlRol.DataTextField = "Nombre";
                ddlRol.DataBind();
                ddlRol.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

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
                var lstConfiguracion = new ConfiguracionBLL().GetListArea(Parametro);

                ddlFArea.DataSource = lstConfiguracion;
                ddlFArea.DataValueField = "Key";
                ddlFArea.DataTextField = "Value";
                ddlFArea.DataBind();
                ddlFArea.Items.Insert(0, new ListItem("-- TODOS --", "0"));

                ddlArea.DataSource = lstConfiguracion;
                ddlArea.DataValueField = "Key";
                ddlArea.DataTextField = "Value";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, new ListItem("-- Seleccione --", "0"));                

            }
            catch (Exception ex)
            {
                errores = ex.Message;

            }
        }
        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtApePaterno.Text = string.Empty;
            txtApeMaterno.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtcorreo.Text = string.Empty;
            txtFecCaducidad.Text = string.Empty;
            ddlArea.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            ddlRol.SelectedIndex = Convert.ToInt32(Constante.Numeros.Cero);
            txtRegUsuario.Text = string.Empty;
            txtRegClave.Text = string.Empty;
        }
        private void ListarUsuario(bool aIsCompleteData)
        {
            Int32 Total;
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objeto = new UsuarioBE();
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

                if (txtFiltro.Text != string.Empty)
                { objeto.ApellidoPaterno = txtFiltro.Text; }
                else
                { objeto.ApellidoPaterno = "*"; }
                
                objeto.IdArea = Convert.ToInt32(ddlFArea.SelectedValue);
                objeto.IdRol = Convert.ToInt32(ddlFRol.SelectedValue);

                var lista = new UsuarioBLL().ListUsuario(objeto);                
                
                if (lista != null)
                {
                    lblContador.Text = "Número total de registro(s): " + lista.Count.ToString() + " usuarios.";
                    gdvUsuario.DataSource = lista;
                    gdvUsuario.DataBind();

                    if (lista.Count > 0)
                    {
                        Total = lista[0].Total;

                        lblContador.Text = "Total de registro(s): " + Total + " usuario(s).";
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
              else
              {
                    lblContador.Text = "Número total de registro(s): 0 usuarios.";
                    gdvUsuario.DataSource = null;
                    gdvUsuario.DataBind();
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

            ListarUsuario(false);
        }
        private void ExportarUsuario()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objeto = new UsuarioBE();            
            ExportarUtils objExport = new ExportarUtils();                       
            
            try
            {
                if (txtFiltro.Text != string.Empty)
                { objeto.ApellidoPaterno = txtFiltro.Text; }
                else
                { objeto.ApellidoPaterno = "*"; }

                objeto.IdArea = Convert.ToInt32(ddlFArea.SelectedValue);
                objeto.IdRol = Convert.ToInt32(ddlFRol.SelectedValue);

                var lista = new UsuarioBLL().ExportUsuario(objeto);               

                //crear datatable para enviar al metodo de descarga
                var dataTableDescargar = new System.Data.DataTable(Guid.NewGuid().ToString());
                var Nombre = new DataColumn("Nombre", typeof(string));
                var ApellidoPaterno = new DataColumn("ApellidoPaterno", typeof(string));
                var ApellidoMaterno = new DataColumn("ApellidoMaterno", typeof(string));
                var DNI = new DataColumn("DNI", typeof(string));
                var Usuario = new DataColumn("Usuario", typeof(string));
                var Area = new DataColumn("Area", typeof(string));
                var Celular = new DataColumn("Celular", typeof(string));
                var Correo = new DataColumn("Correo", typeof(string));
                var FechaCaducidad = new DataColumn("FechaCaducidad", typeof(string));
                var Rol = new DataColumn("Rol", typeof(string));

                dataTableDescargar.Columns.AddRange(new DataColumn[] {
                  Nombre,ApellidoPaterno, ApellidoMaterno, DNI, Usuario, Area, Celular, Correo, FechaCaducidad, Rol
                });

                //Agregar registros al datatable
                foreach (UsuarioBE oUsuario in lista)
                {
                    DataRow RowFile = dataTableDescargar.NewRow();

                    RowFile["Nombre"] = oUsuario.Nombre;
                    RowFile["ApellidoPaterno"] = oUsuario.ApellidoPaterno;
                    RowFile["ApellidoMaterno"] = oUsuario.ApellidoMaterno;
                    RowFile["DNI"] = oUsuario.DNI;
                    RowFile["Usuario"] = oUsuario.Login;
                    RowFile["Area"] = oUsuario.Area;
                    RowFile["Celular"] = oUsuario.Celular;
                    RowFile["Correo"] = oUsuario.MailTrabajo;

                    if (oUsuario.FechaCaducidad != Convert.ToDateTime("01/01/1901"))
                    {
                        RowFile["FechaCaducidad"] = string.Format("{0:d}", oUsuario.FechaCaducidad);
                    }
                    else
                    { RowFile["FechaCaducidad"] = string.Empty; }                    

                   
                    RowFile["Rol"] = oUsuario.Rol.Nombre;
                  

                    dataTableDescargar.Rows.Add(RowFile);
                }

                object[] pNombreCabeceras = new object[] { "Nombre", "Apellido Paterno", "Apellido Materno","DNI", "Usuario","Area", "Celular","Correo", "Fecha Caducidad","Rol" };
                object[] pEstilos = new object[] { new object[] { 9, OleDbType.Date } };
                string MessageOut = objExport.DescargarDataTableToExcel(dataTableDescargar, "Exportación Usuarios", pNombreCabeceras, pEstilos, Page.Response);

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

        #region "EVENTOS"

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            ListarUsuario(false);
        }
        protected void ddlRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            gdvUsuario.PageSize = Int32.Parse(ddlRows.SelectedValue);
            PAGE_SIZE = Int32.Parse(ddlRows.SelectedValue);
            ListarUsuario(false);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarUsuario(false);
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtRegClave.Enabled = true;
            LimpiarControles();
            HideAccion.Value = Convert.ToString(Constante.TipoGrabado.NUEVO);
            btnUsuario_ModalPopupExtender.Show();
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarUsuario();
        }
        protected void gdvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            keyUsuario = (DataKey)gdvUsuario.DataKeys[Convert.ToInt32(index)];
            HideIdUsuario.Value = Convert.ToString(keyUsuario["IDUSUARIO"]);

            if (e.CommandName == "Actualizar")
            {
                txtRegClave.Enabled = false;                

                HideAccion.Value = Convert.ToString(Constante.TipoGrabado.ACTUALIZA);
                HideTipoMsje.Value = "UpdUsuario";
                lblMensaje.Text = "¿Está seguro que desea Actualizar el registro de Usuario.?";
                btnMsje_ModalPopupExtender.Show();

            }
            else if (e.CommandName == "Eliminar")
            {
                HideTipoMsje.Value = "DelUsuario";
                lblMensaje.Text = "¿Está seguro que desea Eliminar el registro de Usuario.?";
                btnMsje_ModalPopupExtender.Show();
            }

        }
        protected void gdvUsuario_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gdvUsuario.PageIndex = e.NewPageIndex;
            ListarUsuario(false);
        }

        #endregion

        #endregion

        #region "METODOS Y EVENTOS USUARIO"

        #region "EVENTOS"
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(HideAccion.Value) == Convert.ToString(Constante.TipoGrabado.NUEVO)) // Grabar
            {               
                GrabarUsuario();
            }
            else
            {             
                ActualizarUsuario();
            }

            txtRegClave.Enabled = true;
            ListarUsuario(false);
            LimpiarControles();

        }

        protected void btnMsjAceptar_Click(object sender, EventArgs e)
        {

            if (HideTipoMsje.Value == "DelUsuario") //Proceso de eliminacion de Prestamo
            {
                EliminarUsuario();
                ListarUsuario(false);

            }
            else if (HideTipoMsje.Value == "UpdUsuario") //Proceso de actualizacion de Prestamo
            {
                ObtenerUsuario();
                btnUsuario_ModalPopupExtender.Show();
            }
        }
        #endregion

        #region "METODOS"

        private void ObtenerUsuario()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objUsuario = new UsuarioBE();

            try
            {
                objUsuario.IdUsuario = Convert.ToInt32(HideIdUsuario.Value);

                var lista = new UsuarioBLL().GetUsuario(objUsuario);

                foreach (UsuarioBE oUsuario in lista)
                {
                    txtNombre.Text = oUsuario.Nombre;
                    txtApePaterno.Text = oUsuario.ApellidoPaterno;
                    txtApeMaterno.Text = oUsuario.ApellidoMaterno;
                    txtDNI.Text = oUsuario.DNI;
                    txtcorreo.Text = oUsuario.MailTrabajo;
                    txtCelular.Text = Convert.ToString(oUsuario.Celular);
                    txtFecCaducidad.Text = string.Format("{0:d}", oUsuario.FechaCaducidad);  

                    ListArea();
                    ddlArea.SelectedValue = Convert.ToString(oUsuario.IdArea);

                    txtRegUsuario.Text = oUsuario.Login;
                    txtRegClave.Text = oUsuario.Password;                                       

                    ListRol();
                    ddlRol.SelectedValue = Convert.ToString(oUsuario.IdRol);

                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }

        private void ActualizarUsuario()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objUsuario = new UsuarioBE();

            try
            {
                objUsuario.IdUsuario = Convert.ToInt32(HideIdUsuario.Value);
                objUsuario.IdRol = Convert.ToInt32(ddlRol.SelectedValue);
                objUsuario.IdArea = Convert.ToInt32(ddlArea.SelectedValue);
                objUsuario.DNI = Convert.ToString(txtDNI.Text);
                objUsuario.Nombre = txtNombre.Text.Trim();
                objUsuario.ApellidoPaterno = txtApePaterno.Text.Trim();
                objUsuario.ApellidoMaterno = txtApeMaterno.Text.Trim();
                objUsuario.Celular = Convert.ToInt32(txtCelular.Text.Trim());
                objUsuario.MailTrabajo = txtcorreo.Text.Trim();
                objUsuario.FechaCaducidad = Convert.ToDateTime(txtFecCaducidad.Text);
                objUsuario.Login = txtRegUsuario.Text.Trim();
                objUsuario.IdUsuarioActualiza = Convert.ToInt32(HideUsuario.Value);

                capanegocios.UpdateUsuario(objUsuario);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }
        private void EliminarUsuario()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objUsuario = new UsuarioBE();

            try
            {
                objUsuario.IdUsuario = Convert.ToInt32(HideIdUsuario.Value);
                capanegocios.DeleteUsuario(objUsuario);

            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }

        }
        private void GrabarUsuario()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objUsuario = new UsuarioBE();

            try
            {
                objUsuario.IdRol = Convert.ToInt32(ddlRol.SelectedValue);
                objUsuario.IdArea = Convert.ToInt32(ddlArea.SelectedValue.Trim());
                objUsuario.DNI = Convert.ToString(txtDNI.Text.Trim());
                objUsuario.Nombre = txtNombre.Text.Trim();
                objUsuario.ApellidoPaterno = txtApePaterno.Text.Trim();
                objUsuario.ApellidoMaterno = txtApeMaterno.Text.Trim();
                objUsuario.Celular = Convert.ToInt32(txtCelular.Text.Trim());
                objUsuario.MailTrabajo = txtcorreo.Text.Trim();
                objUsuario.FechaCaducidad = Convert.ToDateTime(txtFecCaducidad.Text);
                objUsuario.Login = txtRegUsuario.Text.Trim();                
                objUsuario.Password = Utils.EncryptorUtils.Encriptar(txtRegClave.Text.Trim());
                objUsuario.IdUsuarioRegistro = Convert.ToInt32(HideUsuario.Value);

                capanegocios.SaveUsuario(objUsuario);

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
